Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'============Shivani Tyagi
Public Class FrmShortCloseDODairy
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmShortCloseDOPS)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If

    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnShortCloseDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShortCloseDo.Click
        For Each grow As GridViewRowInfo In GV1.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                Dim Code As String = clsCommon.myCstr(grow.Cells(1).Value)
                Dim Qry As String = "Update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE set Short_Close='Y' where Document_Code = '" & Code & "'"
                clsDBFuncationality.ExecuteNonQuery(Qry)
            End If
        Next
        common.clsCommon.MyMessageBoxShow("DO Closed Successfully")
        btnShortCloseDo.Enabled = False
    End Sub
    Sub Reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        GV1.DataSource = Nothing
        btnShortCloseDo.Enabled = True
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData()
        GV1.DataSource = Nothing
        GV1.Rows.Clear()
        GV1.Columns.Clear()
        Dim Qry As String = "select CAST(1 as bit) as Sel,Document_No,Document_Date,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName , " & _
            "MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName, MAX(Total_Amt) as Document_Amount from ( " & _
            "select TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Status,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date as Document_Date , " & _
            "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as Document_No,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code as Vendor, " & _
            "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Location,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date as TransDate, " & _
            "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Amt from  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  " & _
            "left join TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS " & _
            "left Join (select coalesce (Delivery_Code_PS,'') as Delivery_Code from TSPL_SD_SHIPMENT_DETAIL  )  tt on " & _
            "tt.Delivery_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code where isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted,0)=1 " & _
            "and isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N'   " & _
            ")Final  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location where convert(date ,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' " & _
            "group by Document_No,Document_Date  order by Document_No"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            'GV1.DataSource = Nothing
            'GV1.Rows.Clear()
            'GV1.Columns.Clear()
            GV1.DataSource = dtgv
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            GV1.AllowAddNewRow = False
            GV1.ShowGroupPanel = False
            'GV1.AllowColumnReorder = True
            'GV1.AllowRowReorder = True
            'GV1.EnableSorting = False
            'GV1.MasterTemplate.ShowRowHeaderColumn = False
            'GV1.MasterTemplate.ShowColumnHeaders = True
            'GV1.EnableAlternatingRowColor = True
            GV1.EnableFiltering = True
            GV1.ShowFilteringRow = True


            FormatGrid()
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub
    Sub FormatGrid()
        GV1.TableElement.TableHeaderHeight = 25
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        GV1.Columns("Sel").IsVisible = True
        GV1.Columns("Sel").Width = 100
        GV1.Columns("Sel").HeaderText = " "
        GV1.Columns("Sel").ReadOnly = False



        GV1.Columns("Document_No").IsVisible = True
        GV1.Columns("Document_No").Width = 150
        GV1.Columns("Document_No").HeaderText = "Code"



        GV1.Columns("Document_Date").IsVisible = True
        GV1.Columns("Document_Date").Width = 100
        GV1.Columns("Document_Date").HeaderText = " Date"
        GV1.Columns("Document_Date").FormatString = "{0:d}"

        GV1.Columns("Location").IsVisible = True
        GV1.Columns("Location").Width = 100
        GV1.Columns("Location").HeaderText = "Location"

        GV1.Columns("LocationName").IsVisible = True
        GV1.Columns("LocationName").Width = 100
        GV1.Columns("LocationName").HeaderText = "Location Name"

        GV1.Columns("Vendor").IsVisible = True
        GV1.Columns("Vendor").Width = 100
        GV1.Columns("Vendor").HeaderText = "Customer code"

        GV1.Columns("VendorName").IsVisible = True
        GV1.Columns("VendorName").Width = 150
        GV1.Columns("VendorName").HeaderText = "Vendor Name"



        GV1.Columns("Document_Amount").IsVisible = True
        GV1.Columns("Document_Amount").Width = 100
        GV1.Columns("Document_Amount").HeaderText = "Total Amount"




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0



        GV1.ShowGroupPanel = False
        GV1.MasterTemplate.AutoExpandGroups = True

        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub FrmShortCloseDO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
    End Sub

    Private Sub btnUnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In GV1.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In GV1.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub


    Private Sub FrmShortCloseDO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
End Class
