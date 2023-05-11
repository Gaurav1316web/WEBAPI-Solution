''''' bug no BM00000000229 , BM00000000548 ,BM00000000540,BM00000000617,BM00000003009
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Public Class frmPendingSaleInvoiceforChilpPO
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPendingSaleInvoiceforChilpPO)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")


        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")

    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    'Sub LoadBank()
    '    Dim qry As String = "select BANK_CODE as Code, DESCRIPTION as Name from TSPL_BANK_MASTER order by BANK_CODE "
    '    cbgBank.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgBank.ValueMember = "Code"
    '    cbgBank.DisplayMember = "Name"
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Sub RefereshData()

        Try
            Dim strQuery As String = ""
            Dim strDatabasename = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Vendor_Database + "' and code='" + clsFixedParameterCode.Principal_Vendor_Database + "'"))

            strQuery = "select Document_Code as [Invoice No],convert(varchar,Document_Date,103) as [Invoice Date],Against_Shipment_No as [Shipment No],Customer_Code,Customer_Name, " & _
            "Cust_PO_No as [PO No],convert(varchar,Cust_PO_Date,103) as [PO Date]," & _
            "Total_Amt as [Amount] from " + strDatabasename + ".dbo.TSPL_SD_SALE_INVOICE_HEAD   " & _
            "left outer join TSPL_CUSTOMER_MASTER on " + strDatabasename + ".dbo.TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
            "where Cust_PO_No  not in (select isnull(Against_PO,'') from TSPL_SRN_HEAD ) " & _
            "AND CONVERT(date, Document_Date,103) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Text)), "dd/MMM/yyyy") + "' " & _
            "AND CONVERT(date, Document_Date,103) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Text)), "dd/MMM/yyyy") + "' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                SetGridFormatOFGV1()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub SetGridFormatOFGV1()
        'Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Invoice No").IsVisible = True
        gv1.Columns("Invoice No").Width = 150
        gv1.Columns("Invoice No").HeaderText = "Invoice No"


        gv1.Columns("Invoice Date").IsVisible = True
        gv1.Columns("Invoice Date").Width = 150
        gv1.Columns("Invoice Date").HeaderText = "Invoice Date"

        gv1.Columns("Shipment No").IsVisible = True
        gv1.Columns("Shipment No").Width = 100
        gv1.Columns("Shipment No").HeaderText = "Shipment No"

        gv1.Columns("Customer_Code").IsVisible = True
        gv1.Columns("Customer_Code").Width = 100
        gv1.Columns("Customer_Code").HeaderText = "Customer Code"

        gv1.Columns("Customer_Name").IsVisible = True
        gv1.Columns("Customer_Name").Width = 200
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        gv1.Columns("PO No").IsVisible = True
        gv1.Columns("PO No").Width = 100
        gv1.Columns("PO No").HeaderText = "PO No"

        gv1.Columns("PO Date").IsVisible = True
        gv1.Columns("PO Date").Width = 120
        gv1.Columns("PO Date").HeaderText = "PO Date"

        gv1.Columns("Amount").IsVisible = False
        gv1.Columns("Amount").Width = 120
        gv1.Columns("Amount").HeaderText = "Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item5 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Columns.Clear()

    End Sub

    Private Sub frmCheckDepositPaySlip_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub




    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Pending Sale Invoice To be Received", gv1, Nothing, Me.Text)

            Else
                clsCommon.MyExportToPDF("Pending Sale Invoice To be Received", gv1, Nothing, "Pending Sale Invoice To be Received", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub




    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        RefereshData()
    End Sub


End Class
