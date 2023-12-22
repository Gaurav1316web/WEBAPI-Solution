'' Work done agaisnt ticket no SWA/23/08/18-000046 
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class rptLeakageDetailsReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()

    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub rptSubsidyCreditReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        gv.Rows.Clear()
        txtCustomerMult.arrValueMember = Nothing


    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
           
            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add(("Customer: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Leakage Details", gv, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Leakage Details", gv, arrHeader, "Leakage Details", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub loadReport()

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If

        Dim sQuery As String = " select Document_Code as [Invoice No],convert(datetime,Document_Date,103) as [Invoice Date],Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],Total_Amt as [Invoice Amount],LeakageAmount as [Leakage Amount],(Total_Amt-LeakageAmount) as [Net Amount] from TSPL_SD_SALE_INVOICE_HEAD"
        sQuery += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "
        sQuery += " where 2=2  and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

   
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += " and Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
            gv.ReadOnly = True
            SetGridFormationOFGV1()
            gv.BestFitColumns()
        Else
            gv.DataSource = Nothing
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub SetGridFormationOFGV1()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            gv.Columns(ii).BestFit()
        Next

        gv.Columns("Invoice No").IsVisible = True
        gv.Columns("Invoice No").Width = 100
        gv.Columns("Invoice No").HeaderText = "Invoice No"

        gv.Columns("Invoice Date").IsVisible = True
        gv.Columns("Invoice Date").Width = 100
        gv.Columns("Invoice Date").HeaderText = "Invoice Date"

        gv.Columns("Customer Code").IsVisible = True
        gv.Columns("Customer Code").Width = 150
        gv.Columns("Customer Code").HeaderText = "Customer Code"

        gv.Columns("Customer Name").IsVisible = True
        gv.Columns("Customer Name").Width = 150
        gv.Columns("Customer Name").HeaderText = "Customer Name"

        gv.Columns("Invoice Amount").IsVisible = True
        gv.Columns("Invoice Amount").Width = 150
        gv.Columns("Invoice Amount").HeaderText = "Invoice Amount"


        gv.Columns("Leakage Amount").IsVisible = True
        gv.Columns("Leakage Amount").Width = 150
        gv.Columns("Leakage Amount").HeaderText = "Leakage Amount"

        gv.Columns("Net Amount").IsVisible = True
        gv.Columns("Net Amount").Width = 150
        gv.Columns("Net Amount").HeaderText = "Net Amount"
     

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Leakage Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Net Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmPrintFreshInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
