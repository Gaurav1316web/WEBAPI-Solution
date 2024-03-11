'--25/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLeaveRegisterReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "LeaveRegisterReport"
    Dim arrBack As New List(Of String)
    Dim arrEmp As New ArrayList()
    Dim arrLeave As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region
    '' changes by shivani against [BM00000008392]
    '' whole query and structure changed by Panch Raj
    Sub LoadData()
        Try
            If rbtnDetail.IsChecked = True Then
                Dim dt As DataTable = clsLeaveMaster.GetLeaveLedgerDetaildt(txtLocation.arrValueMember, txtDivisionMult.arrValueMember, TxtMultiEmployee.arrValueMember, txtMultLeaveCode.arrValueMember, TxtDepartment.arrValueMember, chkShowResigned.Checked, txtFromDate.Value, txtToDate.Value)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dt

                If gv3.Columns.Contains("Balance") Then
                    gv3.Columns("Balance").IsVisible = False
                End If

                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Dim dtgv As DataTable = clsLeaveMaster.GetLeaveLedgerSummarydt(txtLocation.arrValueMember, txtDivisionMult.arrValueMember, TxtMultiEmployee.arrValueMember, txtMultLeaveCode.arrValueMember, TxtDepartment.arrValueMember, chkShowResigned.Checked, txtFromDate.Value, txtToDate.Value)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dtgv
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            gv3.ReadOnly = True
            btnGenrate.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGenrate.Enabled = True
        End Try
    End Sub

    Private Sub frmLeaveRegisterReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveRegisterReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        txtFromPP.MyReadOnly = False
        txtFromPP.Value = Nothing
        txtFromPP.Focus()
        lblFrompp.Text = ""
        btnGenrate.Enabled = True
        rbtnSummary.IsChecked = True
        gv3.DataSource = Nothing
        txtLocation.arrValueMember = Nothing
        txtDivisionMult.arrValueMember = Nothing
        TxtMultiEmployee.arrValueMember = Nothing
        txtMultLeaveCode.arrValueMember = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmLeaveRegisterReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Leave Register Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Leave Register Report", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Leave Register Report", gv3, arr, "Leave Register Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Leave Register Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Leave Register Report", gv3, arr, "Leave Register Report", False)
    End Sub

#Region "grid operations"

    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv3.DeferRefresh()
            Me.gv3.AutoGenerateHierarchy = True
            Me.gv3.MasterTemplate.Reset()
            Me.gv3.TableElement.RowHeight = 20
            'Me.gv3.DataSource = DT
            Me.gv3.MasterTemplate.Columns("empcode").HeaderText = "empcode"
            Me.gv3.MasterTemplate.Columns("EmpName").HeaderText = "empname"
            Me.gv3.MasterTemplate.Columns("leavecode").HeaderText = "leavecode"
            'Me.gv3.MasterTemplate.Columns("LEAVE_Name").HeaderText = "Leave Name"
            'Me.gv3.MasterTemplate.Columns("OPENING").HeaderText = "Opening"
            Me.gv3.MasterTemplate.Columns("Alloted").HeaderText = "Alloted"
            Me.gv3.MasterTemplate.Columns("Taken").HeaderText = "Taken"
            'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_PLUS").HeaderText = "Adjustment Plus"
            'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_MINUS").HeaderText = "Adjustment Minus"
            Me.gv3.MasterTemplate.Columns("Balance").HeaderText = "Balance"
            Me.gv3.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

        End Using
    End Sub

#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Leave Register Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
        'End If
        ''clsCommon.MyExportToExcel("Leave Register Report", gv3, arr, "Salary Register")
        'If gv3.Rows.Count <= 0 Then
        '    gv3.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Leave Register Report", gv3, arr, "Leave Register Report", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Leave Register Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
        'End If
        'clsCommon.MyExportToPDF("Leave Register Report", gv3, arr, "Leave Register Report", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    ' ============= Addded by Preeti gupta============
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmLeaveRegisterReport & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                    arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                End If
                If TxtDepartment.arrValueMember IsNot Nothing AndAlso TxtDepartment.arrValueMember.Count > 0 Then
                    arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(TxtDepartment.arrDispalyMember))
                End If
                If txtMultLeaveCode.arrValueMember IsNot Nothing AndAlso txtMultLeaveCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Leave Code : " + clsCommon.GetMulcallStringWithComma(txtMultLeaveCode.arrDispalyMember))
                End If
                'arrHeader.Add("Pay Period: " + txtFromPP.Value)
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

                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Leave Register Report", gv3, arrHeader, "Leave Register Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv3_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellDoubleClick

        If rbtnSummary.IsChecked Then
            If Not arrBack.Contains("Summary") Then
                arrBack.Add("Summary")
            End If
            rbtnDetail.IsChecked = True

            arrEmp = TxtMultiEmployee.arrValueMember
            Dim tmp As New ArrayList()
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("empCode").Value))
            TxtMultiEmployee.arrValueMember = tmp
            arrLeave = txtMultLeaveCode.arrValueMember
            tmp = New ArrayList
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("leavecode").Value))
            txtMultLeaveCode.arrValueMember = tmp
            PageSetupReport_ID = ReportID + IIf(rbtnSummary.IsChecked = True, "S", "D")
            LoadData()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnDetail.IsChecked Then
                arrBack.Remove("Summary")
                ' MultVendor.arrValueMember = arrVendor
                rbtnSummary.IsChecked = True
                TxtMultiEmployee.arrValueMember = arrEmp
                txtMultLeaveCode.arrValueMember = arrLeave
                PageSetupReport_ID = ReportID + IIf(rbtnSummary.IsChecked = True, "S", "D")
                LoadData()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtMultiEmployee._My_Click
        Dim qry As String = "select EMP_CODE AS [Code],Emp_Name as [Name],tspl_employee_MASTER.Location_Code as Loaction, tspl_employee_MASTER.Devision_Code as Division " & _
            " from tspl_employee_MASTER left join tspl_location_master on tspl_location_master.Location_Code =tspl_employee_MASTER.LOCATION_CODE where 2=2  "
        If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry = qry & " and tspl_employee_MASTER.LOCATION_CODE IN (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & " )"
        End If
        If Not txtDivisionMult.arrValueMember Is Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            qry = qry & " and tspl_employee_MASTER.Devision_Code IN (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If

        TxtMultiEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", TxtMultiEmployee.arrValueMember, TxtMultiEmployee.arrDispalyMember)
    End Sub

    Private Sub txtMultLeaveCode__My_Click(sender As Object, e As EventArgs) Handles txtMultLeaveCode._My_Click
        txtMultLeaveCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LeaveMulSel", "select Leave_Code as Code,Leave_Name as Name from tspl_leave_Master", "Code", "Name", txtMultLeaveCode.arrValueMember, txtMultLeaveCode.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub TxtDepartment__My_Click(sender As Object, e As EventArgs) Handles TxtDepartment._My_Click
        TxtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("DeptMulSel", "select DEPARTMENT_CODE as Code,DEPARTMENT_NAME as Name from TSPL_DEPARTMENT_MASTER", "Code", "Name", TxtDepartment.arrValueMember, TxtDepartment.arrDispalyMember)
    End Sub

End Class
