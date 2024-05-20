'-Created by --[Pankaj kumar Chaudhary]--Against Ticket No-[BM00000001544]
Imports common
Imports System.Data.SqlClient

Public Class FrmProjectDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim qry As String
    Dim dt As DataTable

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProjectDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmProjectDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funreset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txtCost.Visible = False
        txtBilling.Visible = False
        txtProfit.Visible = False

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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

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
            runDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")

            qry = "Select '" + FromDate + "' as FromDate, '" + ToDate + "' as ToDate, '" + runDate + "' as RunDate, TSPL_PJC_PROJECT_JOB.PROJECT_CODE, TSPL_PJC_PROJECT.SPECIFICATION as ProjDesc, TSPL_PJC_PROJECT.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, "
            qry += " TSPL_PJC_PROJECT.Project_Manager as PMCode, TSPL_EMPLOYEE_MASTER.Emp_Name as PMName, TSPL_PJC_PROJECT.Project_Type, TSPL_PJC_PROJECT.Account_Method,"
            qry += " TSPL_PJC_PROJECT_JOB.JOB_CODE, TSPL_PJC_JOB.DESCRIPTION as JobDesc, TSPL_PJC_PROJECT_JOB.Billing_Type as JobBillingType, "
            qry += " TSPL_PJC_PROJECT_TASK.Task_Code, TSPL_PJC_TASK.DESCRIPTION as TaskDesc, TSPL_PJC_PROJECT_TASK.Billing_Type as TaskBillingType, TSPL_PJC_PROJECT_TASK.Amount as Cost, TSPL_PJC_PROJECT_TASK.Billing_Amt, (TSPL_PJC_PROJECT_TASK.Billing_Amt-TSPL_PJC_PROJECT_TASK.Amount) as Profit "
            qry += " from TSPL_PJC_PROJECT"
            qry += " LEFT OUTER JOIN TSPL_PJC_PROJECT_JOB ON TSPL_PJC_PROJECT_JOB.PROJECT_CODE=TSPL_PJC_PROJECT.PROJECT_CODE"
            qry += " LEFT OUTER JOIN TSPL_PJC_PROJECT_TASK ON TSPL_PJC_PROJECT_TASK.Job_ID=TSPL_PJC_PROJECT_JOB.JOB_id"
            qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PJC_PROJECT.Project_Manager"
            qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_PJC_PROJECT.Cust_Code"
            qry += " LEFT OUTER JOIN TSPL_PJC_JOB ON TSPL_PJC_JOB.JOB_CODE=TSPL_PJC_PROJECT_JOB.JOB_CODE"
            qry += " LEFT OUTER JOIN TSPL_PJC_TASK ON TSPL_PJC_TASK.TASK_CODE=TSPL_PJC_PROJECT_TASK.Task_Code"
            Qry += " WHERE CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)>='" + FromDate + "' AND CONVERT(DATE,TSPL_PJC_PROJECT.Created_Date,103)<='" + ToDate + "'"

            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                Qry += " AND TSPL_PJC_PROJECT.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkPMSelect.IsChecked And cbgPM.CheckedValue.Count > 0 Then
                Qry += " AND TSPL_PJC_PROJECT.Project_Manager in (" + clsCommon.GetMulcallString(cbgPM.CheckedValue) + ")"
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Record Found")
            Else
                gv.DataSource = dt
                FormatGrid()
                '' for total 
                Dim TotCost As Decimal = 0
                Dim TotBilling As Decimal = 0
                Dim TotProfit As Decimal = 0
                For Each dr As DataRow In dt.Rows
                    TotCost = TotCost + dr.Item("Cost")
                    TotBilling = TotBilling + dr.Item("Billing_Amt")
                    TotProfit = TotProfit + dr.Item("Profit")
                Next
                Me.txtCost.Text = Format(TotCost, "###0.00")
                Me.txtBilling.Text = Format(TotBilling, "###0.00")
                Me.txtProfit.Text = Format(TotProfit, "###0.00")
            End If
            Dim strArr As New List(Of String)
            strArr.Add("Run Date : " + runDate + "")
            strArr.Add("From " + FromDate + " to " + ToDate + "")
            If IsPrint = Exporter.Refresh Then
            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptProjectDetail", "Project Detail")
                frmCRV = Nothing
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
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.AllowAddNewRow = False
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("PROJECT_CODE").IsVisible = True
        gv.Columns("PROJECT_CODE").Width = 100
        gv.Columns("PROJECT_CODE").HeaderText = "Project Code"

        gv.Columns("ProjDesc").IsVisible = True
        gv.Columns("ProjDesc").Width = 250
        gv.Columns("ProjDesc").HeaderText = "Description"

        gv.Columns("Cust_Code").IsVisible = True
        gv.Columns("Cust_Code").Width = 100
        gv.Columns("Cust_Code").HeaderText = "Customer Id"

        gv.Columns("Customer_Name").IsVisible = True
        gv.Columns("Customer_Name").Width = 250
        gv.Columns("Customer_Name").HeaderText = "Customer Name"

        gv.Columns("PMCode").IsVisible = True
        gv.Columns("PMCode").Width = 100
        gv.Columns("PMCode").HeaderText = "Project Mgr. Id"

        gv.Columns("PMName").IsVisible = True
        gv.Columns("PMName").Width = 250
        gv.Columns("PMName").HeaderText = "Project Mgr Name"

        gv.Columns("Project_Type").IsVisible = True
        gv.Columns("Project_Type").Width = 100
        gv.Columns("Project_Type").HeaderText = "Project Type"

        gv.Columns("Account_Method").IsVisible = True
        gv.Columns("Account_Method").Width = 100
        gv.Columns("Account_Method").HeaderText = "Account Method"

        gv.Columns("JOB_CODE").IsVisible = True
        gv.Columns("JOB_CODE").Width = 80
        gv.Columns("JOB_CODE").HeaderText = "Job Code"

        gv.Columns("JobDesc").IsVisible = True
        gv.Columns("JobDesc").Width = 200
        gv.Columns("JobDesc").HeaderText = "Job Description"

        gv.Columns("JobBillingType").IsVisible = True
        gv.Columns("JobBillingType").Width = 100
        gv.Columns("JobBillingType").HeaderText = "Billing Type(Job)"

        gv.Columns("Task_Code").IsVisible = True
        gv.Columns("Task_Code").Width = 80
        gv.Columns("Task_Code").HeaderText = "Task Code"

        gv.Columns("TaskDesc").IsVisible = True
        gv.Columns("TaskDesc").Width = 200
        gv.Columns("TaskDesc").HeaderText = "Task Description"

        gv.Columns("TaskBillingType").IsVisible = True
        gv.Columns("TaskBillingType").Width = 100
        gv.Columns("TaskBillingType").HeaderText = "Billing Type(Task)"

        gv.Columns("Cost").IsVisible = True
        gv.Columns("Cost").Width = 100
        gv.Columns("Cost").HeaderText = "Cost"

        gv.Columns("Billing_Amt").IsVisible = True
        gv.Columns("Billing_Amt").Width = 100
        gv.Columns("Billing_Amt").HeaderText = "Billings"

        gv.Columns("profit").IsVisible = True
        gv.Columns("profit").Width = 100
        gv.Columns("profit").HeaderText = "Profit"

        gv.GroupDescriptors.Add(New GridGroupByExpression("PROJECT_CODE as PROJECT_CODE  format ""{0}: {1}"" group by PROJECT_CODE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("JOB_CODE as JOB_CODE  format ""{0}: {1}"" group by JOB_CODE"))
        gv.MasterTemplate.ExpandAllGroups()
        gv.ShowGroupPanel = False
        
        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim Cost As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Cost)
        Dim billings As New GridViewSummaryItem("Billing_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(billings)
        Dim profit As New GridViewSummaryItem("profit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(profit)
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Try
            LoadData(Exporter.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
