Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

'created by stuti on 18/10/2016 against ticket no BM00000007376'
Public Class rptEmployeeAdvanceLedger
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "EmployeeAdvanceLedger"
    Private isInsideLoadData As Boolean = False
    Dim qry As String
    Dim dt As DataTable
#End Region


    Private Sub rptEmployeeAdvanceLedger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        dtpFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy")
        dtpToDate.Text = DateTime.Now().ToString("dd/MMM/yyyy")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptEmployeeAdvanceLedger)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btn_savelayout_Click(sender As Object, e As EventArgs) Handles btn_savelayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub btn_deletelayout_Click(sender As Object, e As EventArgs) Handles btn_deletelayout.Click
        If clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        End If
    End Sub

    Sub funReset()
        dtpFromDate.Text = DateTime.Now.AddMonths(-1)
        dtpToDate.Text = DateTime.Now()
        txtLocationMult.arrValueMember = Nothing
        txtDivisionMult.arrValueMember = Nothing
        TxtDepartment.arrValueMember = Nothing
        TxtEmployee.arrValueMember = Nothing
        btnRefresh.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub rptEmployeeAdvanceLedger_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub gv1Format()
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 0 To count - 1
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i

    End Sub

    Sub LoadData()
        If dtpFromDate.Value > dtpToDate.Value Then
            clsCommon.MyMessageBoxShow(Me, "'From date' Cann't Be Greater Than 'To Date'", Me.Text)
            dtpFromDate.Focus()
            Exit Sub
        End If
        If isInsideLoadData Then
            clsCommon.MyMessageBoxShow(Me, "Work in Progress Please Wait...", Me.Text)
            Exit Sub
        End If
        'txtCode.MyReadOnly = True
        btnRefresh.Enabled = True
        isInsideLoadData = True
        btnRefresh.Enabled = False
        gv1.DataSource = Nothing
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim Location As String = "''"
        Dim Division As String = "''"
        Dim Department As String = "''"
        Dim Employee As String = "''"

        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Location = "[" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & "]"
        End If

        If rbtnSummary.IsChecked Then
            qry = "select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code],TSPL_EMPLOYEE_MASTER.EMP_NAME as [Employee Name],convert(varchar,isnull(TSPL_LOAN_APPLICATION.LOAN_DATE,''),103) as [Loan Date], " & _
                " TSPL_LOAN_APPLICATION.LOAN_CODE as [Loan Code],TSPL_LOAN_APPLICATION.LOAN_AMOUNT as [Loan Amount],TSPL_LOAN_APPLICATION.Loan_Status as [Status],TSPL_LOAN_APPLICATION.LOAN_TYPE as [Loan Type], " & _
                " TSPL_LOAN_APPLICATION.NO_OF_EMI as [Loan Tenture (in month)],TSPL_LOAN_APPLICATION.TOTALPAYABLE_AMOUNT as [Total Loan Amount],(select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from " & _
                " TSPL_LOANGENERATION_DETAIL where  TSPL_LOANGENERATION_DETAIL.EMP_CODE =TSPL_LOAN_APPLICATION.EMP_CODE and TSPL_LOANGENERATION_DETAIL.LOAN_CODE = TSPL_LOAN_APPLICATION.LOAN_CODE ) as [Received Amount], " & _
                " (TSPL_LOAN_APPLICATION.TOTALPAYABLE_AMOUNT-(select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from TSPL_LOANGENERATION_DETAIL where  " & _
                " TSPL_LOANGENERATION_DETAIL.EMP_CODE =TSPL_LOAN_APPLICATION.EMP_CODE and TSPL_LOANGENERATION_DETAIL.LOAN_CODE = TSPL_LOAN_APPLICATION.LOAN_CODE )) as [Pending Amount] from TSPL_LOAN_APPLICATION left join " & _
                " TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_LOAN_APPLICATION.EMP_CODE left join TSPL_LOCATION_MASTER on " & _
                " TSPL_LOCATION_MASTER.Location_Desc =TSPL_LOAN_APPLICATION.Location left join TSPL_DEVISION_MASTER on " & _
                " TSPL_DEVISION_MASTER.DEVISION_NAME = TSPL_LOAN_APPLICATION.Division where 2=2 "

        ElseIf rbtndetail.IsChecked Then
            qry = " select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code], TSPL_EMPLOYEE_MASTER.EMP_NAME as [Employee Name],convert(varchar,isnull(TSPL_LOAN_APPLICATION.LOAN_DATE,''),103) as [Loan Date],  TSPL_LOAN_APPLICATION.LOAN_CODE as [Loan Code], " & _
                " TSPL_LOANEMI_DETAIL.EMI_NO as [EMI No],TSPL_LOANEMI_DETAIL.EMI_AMOUNT as [EMI Amount],isnull((select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from TSPL_LOANGENERATION_DETAIL " & _
                "  where TSPL_LOANGENERATION_DETAIL.LOAN_CODE =TSPL_LOANEMI_DETAIL.Loan_Code and TSPL_LOANGENERATION_DETAIL.EMI_NO=TSPL_LOANEMI_DETAIL.EMI_NO),0) as [Paid Amount],( select TSPL_LOANGENERATION_DETAIL.PAY_PERIOD_CODE from TSPL_LOANGENERATION_DETAIL where TSPL_LOANGENERATION_DETAIL.LOAN_CODE =TSPL_LOANEMI_DETAIL.Loan_Code) as [Pay Period Code],  " & _
                "  (TSPL_LOANEMI_DETAIL.EMI_AMOUNT-isnull((select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from TSPL_LOANGENERATION_DETAIL where  " & _
                " TSPL_LOANGENERATION_DETAIL.LOAN_CODE =TSPL_LOANEMI_DETAIL.Loan_Code and TSPL_LOANGENERATION_DETAIL.EMI_NO=TSPL_LOANEMI_DETAIL.EMI_NO),0)) as [Balance Amount] " & _
                " from TSPL_LOANEMI_DETAIL left join TSPL_LOAN_APPLICATION on TSPL_LOAN_APPLICATION.LOAN_CODE=TSPL_LOANEMI_DETAIL.LOAN_CODE left join  TSPL_EMPLOYEE_MASTER  on " & _
                " TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_LOAN_APPLICATION.EMP_CODE left join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Desc =TSPL_LOAN_APPLICATION.Location " & _
                " left join TSPL_DEVISION_MASTER on  TSPL_DEVISION_MASTER.DEVISION_NAME = TSPL_LOAN_APPLICATION.Division where 2=2 "
        End If

        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Location = clsCommon.GetMulcallString(txtLocationMult.arrValueMember)
            qry += " and TSPL_LOCATION_MASTER.Location_Code  in (" + clsCommon.myCstr(Location) + ")"
        End If

        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Division = clsCommon.GetMulcallString(txtDivisionMult.arrValueMember)
            qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE in (" + clsCommon.myCstr(Division) + ")"
        End If

        If TxtDepartment.arrValueMember IsNot Nothing AndAlso TxtDepartment.arrValueMember.Count > 0 Then
            Department = clsCommon.GetMulcallString(TxtDepartment.arrValueMember)
            qry += " and TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE IN (" + clsCommon.myCstr(Department) + ")"
        End If

        If TxtEmployee.arrValueMember IsNot Nothing AndAlso TxtEmployee.arrValueMember.Count > 0 Then
            Employee = clsCommon.GetMulcallString(TxtEmployee.arrValueMember)
            qry += " TSPL_LOAN_APPLICATION.EMP_CODE in (" + clsCommon.myCstr(Employee) + ") "
        End If
        qry += " and convert(varchar,isnull(TSPL_LOAN_APPLICATION.LOAN_DATE,''),103) between ('" + clsCommon.GetPrintDate(dtpFromDate.Text, "dd/MM/yyyy") + "') and ('" + clsCommon.GetPrintDate(dtpToDate.Text, "dd/MM/yyyy") + "')"

        dt = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = dt

        gv1Format()

        isInsideLoadData = False
        btnRefresh.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage2
        '' GRAND TOTAL
        ReStoreGridLayout()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = ReportID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv1
        LoadData()
    End Sub

    Private Sub txtLocationMult_My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY) "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocationMult, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtDivisionMult_My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub

    Private Sub TxtDepartment__My_Click(sender As Object, e As EventArgs) Handles TxtDepartment._My_Click
        Dim qry As String = " select DEPARTMENT_CODE as Code,DEPARTMENT_NAME as Name from TSPL_DEPARTMENT_MASTER"
        TxtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("DepMulSel", qry, "Code", "Name", TxtDepartment.arrValueMember, TxtDepartment.arrDispalyMember)
    End Sub

    Private Sub TxtEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtEmployee._My_Click
        Dim qry As String = " select EMP_CODE as Code,EMP_NAME as Name from TSPL_EMPLOYEE_MASTER"
        TxtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("EmpMulSel", qry, "Code", "Name", TxtEmployee.arrValueMember, TxtEmployee.arrDispalyMember)
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If e.RowIndex >= 0 AndAlso rbtnSummary.IsChecked Then
            Dim qry As String = "SELECT TSPL_LOANEMI_DETAIL.EMI_NO as [EMI No],TSPL_LOANEMI_DETAIL.EMI_AMOUNT as [EMI Amount],isnull((select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from TSPL_LOANGENERATION_DETAIL "
            Dim whr As String = " TSPL_LOANGENERATION_DETAIL.EMP_CODE ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Employee Code").Value) + "' and TSPL_LOANGENERATION_DETAIL.LOAN_CODE ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Loan Code").Value) + "' and TSPL_LOANGENERATION_DETAIL.EMI_NO=TSPL_LOANEMI_DETAIL.EMI_NO),0) as [Paid Amount],( select TSPL_LOANGENERATION_DETAIL.PAY_PERIOD_CODE from TSPL_LOANGENERATION_DETAIL where TSPL_LOANGENERATION_DETAIL.LOAN_CODE =TSPL_LOANEMI_DETAIL.Loan_Code) as [Pay Period Code], " & _
                " (TSPL_LOANEMI_DETAIL.EMI_AMOUNT-isnull((select sum(TSPL_LOANGENERATION_DETAIL.NET_EMI) from TSPL_LOANGENERATION_DETAIL where  TSPL_LOANGENERATION_DETAIL.EMP_CODE ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Employee Code").Value) + "' and " & _
                " TSPL_LOANGENERATION_DETAIL.LOAN_CODE ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Loan Code").Value) + "' and TSPL_LOANGENERATION_DETAIL.EMI_NO=TSPL_LOANEMI_DETAIL.EMI_NO),0)) as [Balance Amount] FROM TSPL_LOANEMI_DETAIL where Loan_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Loan Code").Value) + "' "
            Dim val As String = clsCommon.ShowSelectForm("EMPadv", qry, "EMI No", whr, "", "", True)
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptEmployeeAdvanceLedger & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
            arrHeader.Add("From Date : " & clsCommon.myCstr(dtpFromDate.Text))
            arrHeader.Add("To Date : " & clsCommon.myCstr(dtpToDate.Text))
            arrHeader.Add("Report Type : " & IIf(rbtnSummary.IsChecked = True, "Summary", "Detail"))
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
            End If

            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
            End If

            If TxtDepartment.arrValueMember IsNot Nothing AndAlso TxtDepartment.arrValueMember.Count > 0 Then
                arrHeader.Add(" Department : " + clsCommon.GetMulcallStringWithComma(TxtDepartment.arrDispalyMember))
            End If

            If TxtEmployee.arrValueMember IsNot Nothing AndAlso TxtEmployee.arrValueMember.Count > 0 Then
                arrHeader.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtEmployee.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Employee Advance Ledger Sheet", gv1, arrHeader, "Employee Advance Ledger Sheet", True)
            End If
 Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btn_excel_Click(sender As Object, e As EventArgs) Handles btn_excel.Click
        'Dim arrHeader As List(Of String) = New List(Of String)()

        'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptEmployeeAdvanceLedger & "'"))
        'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        ''arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
        'arrHeader.Add("From Date : " & clsCommon.myCstr(dtpFromDate.Text))
        'arrHeader.Add("To Date : " & clsCommon.myCstr(dtpToDate.Text))

        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
        'End If

        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
        'End If

        'If TxtDepartment.arrValueMember IsNot Nothing AndAlso TxtDepartment.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Department : " + clsCommon.GetMulcallStringWithComma(TxtDepartment.arrDispalyMember))
        'End If

        'If TxtEmployee.arrValueMember IsNot Nothing AndAlso TxtEmployee.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtEmployee.arrDispalyMember))
        'End If
        'arrHeader.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")

        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Employee Advance Ledger Sheet", gv1, arrHeader, "Employee Advance Ledger Sheet", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btn_pdf_Click(sender As Object, e As EventArgs) Handles btn_pdf.Click
        'Dim arrHeader As List(Of String) = New List(Of String)()

        'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptEmployeeAdvanceLedger & "'"))
        'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        ''arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
        'arrHeader.Add("From Date : " & clsCommon.myCstr(dtpFromDate.Text))
        'arrHeader.Add("To Date : " & clsCommon.myCstr(dtpToDate.Text))

        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
        'End If

        'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
        'End If

        'If TxtDepartment.arrValueMember IsNot Nothing AndAlso TxtDepartment.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Department : " + clsCommon.GetMulcallStringWithComma(TxtDepartment.arrDispalyMember))
        'End If

        'If TxtEmployee.arrValueMember IsNot Nothing AndAlso TxtEmployee.arrValueMember.Count > 0 Then
        '    arrHeader.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtEmployee.arrDispalyMember))
        'End If
        'arrHeader.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToPDF("Employee Advance Ledger Sheet", gv1, arrHeader, "Employee Advance Ledger Sheet", True)
        'End If
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class
