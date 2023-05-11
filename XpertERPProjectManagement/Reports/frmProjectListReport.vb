'-Created By-[Pankaj Kumar Chaudhary]-Against Ticket No-[BM00000001541]
Imports common
Imports System.Data.SqlClient

Public Class FrmProjectListReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim Qry As String = ""

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorLedgerReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub


    Private Sub FrmProjectListReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funreset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

    End Sub

    Sub LoadCustomer()
        Qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER Order By Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub


    Sub LoadProjectMgr()
        Qry = "Select EMP_CODE as Code, Emp_Name as Name from TSPL_EMPLOYEE_MASTER "
        cbgPM.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPM.ValueMember = "Code"
        cbgPM.DisplayMember = "Name"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadProjectMgr()
        chkCustAll.IsChecked = True
        chkPMAll.IsChecked = True
        gv.DataSource = Nothing
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Dim dt As DataTable
    Dim FromDate As String
    Dim ToDate As String
    Dim runDate As String

    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()

            If chkPMSelect.IsChecked And cbgPM.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single location or select all.")
            ElseIf chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single customer or select all.")
            End If

            FromDate = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            runDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE())


            Dim EstQry As String = "Select TSPL_PJC_PROJECT.PROJECT_CODE, TSPL_PJC_PROJECT.SPECIFICATION, TSPL_PJC_PROJECT.PROJECT_STATUS, (TSPL_PJC_PROJECT.Cust_Code+' - '+TSPL_CUSTOMER_MASTER.Customer_Name) as Customer, "
            EstQry += " TSPL_PJC_PROJECT.Created_Date, (TSPL_PJC_PROJECT.Project_Manager+' - '+TSPL_EMPLOYEE_MASTER.Emp_Name) as ProjectMgr, "
            EstQry += " TSPL_PJC_PROJECT.Project_Type, TSPL_PJC_PROJECT.Account_Method, TSPL_PJC_PROJECT.Total_Cost, TSPL_PJC_PROJECT.Total_Billing, TSPL_PJC_PROJECT.Total_Profit from TSPL_PJC_PROJECT "
            EstQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_PJC_PROJECT.Cust_Code"
            EstQry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PJC_PROJECT.Project_Manager"
            EstQry += " WHERE CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)>='" + FromDate + "' AND CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)<='" + ToDate + "'"

            '' query for actual cost

            Dim strExpense = "select PROJECT_CODE,'Expense' as Type,Document_No,Document_Date,TotalCost as Cost,0 as Billing  from  TSPL_PJC_EXPENSE_HEADER  "
            Dim strAP = " union all select PROJECT_ID,'APInvoice' as Type,Document_No,Vendor_Invoice_Date,Document_Total as Cost,0 as Billing  from TSPL_VENDOR_INVOICE_HEAD where PROJECT_ID <> '' "
            Dim strTimesheet = " union all select PROJECT_CODE,'TimeSheet' as Type,CODE,TASK_DATE,TOTAL_COST as Cost,0 as Billing from TSPL_PJC_TIMESHEET "
            Dim strAR = "  select PROJECT_ID as PROJECT_CODE,'ARInvoice' as Type,Document_No,Document_Date,0 as Cost,Document_Total as Billing from TSPL_Customer_Invoice_Head  where PROJECT_ID <> '' "

            Dim strSql As String = "select xxx.PROJECT_CODE,SPECIFICATION,max(TSPL_PJC_PROJECT.Created_Date) as ProjectDate,max(Customer_Name) as Customer,max(Project_Type) as Project_Type, "
            strSql += " max(PROJECT_STATUS) as PROJECT_STATUS,max(Account_Method) as Account_Method,SUM(Cost) as Total_Cost,SUM(Billing) as Total_Billing , "
            strSql += " sum(Billing)-SUM(Cost) as Total_Profit  from( " & strExpense & strAP & strTimesheet & " Union All " & strAR & " ) xxx "
            strSql += " left outer join TSPL_PJC_PROJECT on xxx.PROJECT_CODE=TSPL_PJC_PROJECT.PROJECT_CODE "
            strSql += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_PJC_PROJECT.Cust_Code "
            strSql += " WHERE CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)>='" + FromDate + "' AND CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)<='" + ToDate + "'"
            strSql += " group by xxx.PROJECT_CODE,SPECIFICATION"
            Qry = "select Est.*,Act.Total_Cost as Actual_Cost,Act.Total_Billing as Actual_Billing,Act.Total_Profit as Actual_Profit from (" & EstQry & ") Est left join (" & strSql & ") as Act on Est.Project_Code=Act.Project_Code"
            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                Qry += " AND Est.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkPMSelect.IsChecked And cbgPM.CheckedValue.Count > 0 Then
                Qry += " AND Est.Project_Manager in (" + clsCommon.GetMulcallString(cbgPM.CheckedValue) + ")"
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Record Found")
            Else
                gv.DataSource = dt
                FormatGrid()
            End If

            Dim strArr As New List(Of String)
            strArr.Add("Run Date : " + runDate + "")
            strArr.Add("From " + FromDate + " to " + ToDate + "")

            If IsPrint = Exporter.Refresh Then

            ElseIf IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Project List Report", gv, strArr, Me.Text)
            Else
                clsCommon.MyExportToPDF("Project List Report", gv, strArr, "Project List Report", False)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()
        'Dim strItemCode, head2 As String
        gv.AllowAddNewRow = False
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("PROJECT_CODE").IsVisible = True
        gv.Columns("PROJECT_CODE").Width = 100
        gv.Columns("PROJECT_CODE").HeaderText = "Project Code"

        gv.Columns("Created_Date").IsVisible = True
        gv.Columns("Created_Date").Width = 130
        gv.Columns("Created_Date").HeaderText = "Date"

        gv.Columns("SPECIFICATION").IsVisible = True
        gv.Columns("SPECIFICATION").Width = 250
        gv.Columns("SPECIFICATION").HeaderText = "Description"

        gv.Columns("PROJECT_STATUS").IsVisible = True
        gv.Columns("PROJECT_STATUS").Width = 100
        gv.Columns("PROJECT_STATUS").HeaderText = "Status"

        gv.Columns("Customer").IsVisible = True
        gv.Columns("Customer").Width = 250
        gv.Columns("Customer").HeaderText = "Customer"

        gv.Columns("ProjectMgr").IsVisible = True
        gv.Columns("ProjectMgr").Width = 250
        gv.Columns("ProjectMgr").HeaderText = "Project Manager"

        gv.Columns("Project_Type").IsVisible = True
        gv.Columns("Project_Type").Width = 100
        gv.Columns("Project_Type").HeaderText = "Project Type"

        gv.Columns("Account_Method").IsVisible = True
        gv.Columns("Account_Method").Width = 100
        gv.Columns("Account_Method").HeaderText = "Account Method"

        gv.Columns("Total_Cost").IsVisible = True
        gv.Columns("Total_Cost").Width = 100
        gv.Columns("Total_Cost").HeaderText = "Est. Cost"

        gv.Columns("Total_Billing").IsVisible = True
        gv.Columns("Total_Billing").Width = 100
        gv.Columns("Total_Billing").HeaderText = "Est. Billings"

        gv.Columns("Total_Profit").IsVisible = True
        gv.Columns("Total_Profit").Width = 100
        gv.Columns("Total_Profit").HeaderText = "Est. Profit"

        '' actual columns

        gv.Columns("Actual_Cost").IsVisible = True
        gv.Columns("Actual_Cost").Width = 100
        gv.Columns("Actual_Cost").HeaderText = "Act. Cost"

        gv.Columns("Actual_Billing").IsVisible = True
        gv.Columns("Actual_Billing").Width = 100
        gv.Columns("Actual_Billing").HeaderText = "Act. Billings"

        gv.Columns("Actual_Profit").IsVisible = True
        gv.Columns("Actual_Profit").Width = 100
        gv.Columns("Actual_Profit").HeaderText = "Act. Profit"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim Cost As New GridViewSummaryItem("Total_Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Cost)
        Dim billings As New GridViewSummaryItem("Total_Billing", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(billings)
        Dim profit As New GridViewSummaryItem("Total_Profit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(profit)

        '' actual summary
        Dim ActCost As New GridViewSummaryItem("Actual_Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ActCost)
        Dim Actbillings As New GridViewSummaryItem("Actual_Billing", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Actbillings)
        Dim Actprofit As New GridViewSummaryItem("Actual_Profit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Actprofit)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
            'ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            '    LoadData(Exporter.Print)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Try
            LoadData(Exporter.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Try
            LoadData(Exporter.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPMAll.ToggleStateChanged
        cbgPM.Enabled = Not chkPMAll.IsChecked
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        Dim DocNo As String
        Dim DocType As String
        DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("DocNo").Value)
        DocType = clsCommon.myCstr(gv.CurrentRow.Cells("GLDocType").Value)
        If clsCommon.CompairString(DocType, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(DocType, "AP-MI") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
            'Dim frm As New FrmPaymentNew
            'frm.SetUserMgmt(clsUserMgtCode.PaymentEntryNew)
            'frm.Show()
            'frm.LoadData(DocNo, NavigatorType.Current)
        End If

    End Sub

End Class
