Imports common
Imports System.Data.SqlClient
Public Class FrmProjectProfitReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim Qry As String = ""

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmProjectProfitReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmProjectProfitReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funreset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        rdbProject.IsChecked = True
    End Sub
    Sub LoadCustomer()
        Qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER Order By Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub


    Sub LoadProjectMgr()
        Qry = "Select PROJECT_CODE as Code, SPECIFICATION as Name from TSPL_PJC_PROJECT "
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
        rdbProject.IsChecked = True
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ExportToExcel(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Dim dt As DataTable
    Dim FromDate As String
    Dim ToDate As String
    Dim runDate As String

    Private Function LoadData(ByVal strColumn As String, ByVal strProject As String)
        Try
            If strColumn = "" Then
                gv.DataSource = Nothing
                gv.Columns.Clear()
                gv.Rows.Clear()
            End If

            If chkPMSelect.IsChecked And cbgPM.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single location or select all.")
            ElseIf chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single customer or select all.")
            End If

            FromDate = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            runDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE())

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


            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                strSql += " AND TSPL_PJC_PROJECT.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkPMSelect.IsChecked And cbgPM.CheckedValue.Count > 0 Then
                strSql += " AND TSPL_PJC_PROJECT.PROJECT_CODE in (" + clsCommon.GetMulcallString(cbgPM.CheckedValue) + ")"
            End If
            strSql += " group by xxx.PROJECT_CODE,SPECIFICATION"

            If strColumn = "" Then
                dt = clsDBFuncationality.GetDataTable(strSql)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Record Found")
                Else
                    gv.DataSource = dt
                    FormatGrid()
                End If
            Else

                If clsCommon.CompairString(strColumn, "Total_Cost") = CompairStringResult.Equal And clsCommon.myLen(strProject) > 0 Then
                    strExpense = strExpense & " where  PROJECT_CODE='" & strProject & "' "
                    strAP = strAP & " and  PROJECT_ID='" & strProject & "'"
                    strTimesheet = strTimesheet & " where  PROJECT_CODE='" & strProject & "'"
                    Qry = strExpense & strAP & strTimesheet
                    Qry = "Select PROJECT_CODE as Project,Type,Document_No as [Document No],Document_Date as [Document Date],Cost from (" & Qry & ") a "
                ElseIf clsCommon.CompairString(strColumn, "Total_Billing") = CompairStringResult.Equal Then
                    strAR = strAR & " and  PROJECT_ID='" & strProject & "'"
                    Qry = "Select PROJECT_CODE as Project,Type,Document_No as [Document No],Document_Date as [Document Date],Billing from (" & strAR & ") a "
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
        Return Qry
    End Function
    Private Sub ExportToExcel(ByVal IsPrint As Exporter)
        Try

            Dim arrHeader As New List(Of String)
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            If chkPMSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgPM.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Project : " + strtemp)
            End If

            If chkCustSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
            End If

            If IsPrint = Exporter.Refresh Then

            ElseIf IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Project Profitability Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Project Profitability Report", gv, arrHeader, "Project Profitability Report", False)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        gv.AllowAddNewRow = False
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("PROJECT_CODE").IsVisible = True
        gv.Columns("PROJECT_CODE").Width = 100
        gv.Columns("PROJECT_CODE").HeaderText = "Project Code"

        gv.Columns("ProjectDate").IsVisible = True
        gv.Columns("ProjectDate").Width = 130
        gv.Columns("ProjectDate").HeaderText = "Date"

        gv.Columns("SPECIFICATION").IsVisible = True
        gv.Columns("SPECIFICATION").Width = 250
        gv.Columns("SPECIFICATION").HeaderText = "Description"

        gv.Columns("PROJECT_STATUS").IsVisible = True
        gv.Columns("PROJECT_STATUS").Width = 100
        gv.Columns("PROJECT_STATUS").HeaderText = "Status"

        gv.Columns("Customer").IsVisible = True
        gv.Columns("Customer").Width = 250
        gv.Columns("Customer").HeaderText = "Customer"

        gv.Columns("Project_Type").IsVisible = True
        gv.Columns("Project_Type").Width = 100
        gv.Columns("Project_Type").HeaderText = "Project Type"

        gv.Columns("Account_Method").IsVisible = True
        gv.Columns("Account_Method").Width = 100
        gv.Columns("Account_Method").HeaderText = "Account Method"

        gv.Columns("Total_Cost").IsVisible = True
        gv.Columns("Total_Cost").Width = 100
        gv.Columns("Total_Cost").HeaderText = "Cost"

        gv.Columns("Total_Billing").IsVisible = True
        gv.Columns("Total_Billing").Width = 100
        gv.Columns("Total_Billing").HeaderText = "Billings"

        gv.Columns("Total_Profit").IsVisible = True
        gv.Columns("Total_Profit").Width = 100
        gv.Columns("Total_Profit").HeaderText = "Profit"

        If rdbClient.IsChecked Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("Customer format ""{0}: {1}"" Group By Customer"))
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
        Else
            'gv.GroupDescriptors.Add(New GridGroupByExpression("PROJECT_CODE as Project format ""{0}: {1}"" Group By PROJECT_CODE"))
        End If
       
       

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim Cost As New GridViewSummaryItem("Total_Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Cost)
        Dim billings As New GridViewSummaryItem("Total_Billing", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(billings)
        Dim profit As New GridViewSummaryItem("Total_Profit", "{0:F2}", GridAggregateFunction.Sum)
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
            ExportToExcel(Exporter.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Try
            ExportToExcel(Exporter.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData("", "")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            LoadData("", "")
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

   

    Private Sub gv_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If clsCommon.CompairString(e.Column.Name, "Total_Billing") = CompairStringResult.Equal OrElse clsCommon.CompairString(e.Column.Name, "Total_Cost") = CompairStringResult.Equal Then
            Dim strColumn, strQuery As String
            ' Dim strMRP As Integer
            Dim strProject = gv.Rows(e.RowIndex).Cells(0).Value
            strColumn = e.Column.Name
            strQuery = LoadData(strColumn, strProject)
            Dim frmPJC As New FrmPjcDrilldown
            frmPJC.LoadData(strQuery)
            frmPJC.Show()
        End If
    End Sub
End Class
