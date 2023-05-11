'--26/06/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmLeaveAllotment
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colLineNo As String = "LineNo"
    Const colLeaveCode As String = "LeaveCode"
    Const colLeaveName As String = "LeaveName"
    Const colAllotedLeave As String = "AllotedLeave"

    Dim lineNo As GridViewTextBoxColumn
    Dim leaveCode As GridViewTextBoxColumn
    Dim leaveName As GridViewTextBoxColumn
    Dim allotedLeave As GridViewDecimalColumn


#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsLeaveAllotment()
            obj.LVALLOTMENT_CODE = txtCode.Value
            obj.EMP_CODE = txtEmpCode.Value
            obj.PAY_PERIOD_CODE = txtPayPeriodCode.Value
            obj.ALLOTMENT_DATE = clsPayPeriodMaster.GetFromDate(txtPayPeriodCode.Value, Nothing)
            obj.Location_Code = txtBranch.Value
            obj.Division_Code = fndDivision.Value
            obj.ObjList = New List(Of clsLeaveAllotmentDetails)
            For Each grow As GridViewRowInfo In gvLeaveAllotment.Rows
                Dim objTr As New clsLeaveAllotmentDetails()
                objTr.LEAVE_CODE = clsCommon.myCstr(grow.Cells(colLeaveCode).Value)
                objTr.LVALLOTMENT_CODE = txtCode.Value
                objTr.ALLOTED_LEAVE = clsCommon.myCstr(grow.Cells(colAllotedLeave).Value)
                obj.ObjList.Add(objTr)
            Next

            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.LVALLOTMENT_CODE, NavigatorType.Current)
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsLeaveAllotment()
        obj = clsLeaveAllotment.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LVALLOTMENT_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.LVALLOTMENT_CODE
            txtPayPeriodCode.Value = obj.PAY_PERIOD_CODE
            lblPayPeriodName.Text = obj.PayPer_Name
            lblEmpName.Text = obj.Emp_Name
            txtEmpCode.Value = obj.EMP_CODE
            lblDepartmentName.Text = clsEmployeeMaster.GetDepartmentName(txtEmpCode.Value, Nothing)
            lblDesignationName.Text = clsEmployeeMaster.GetDesignationName(txtEmpCode.Value, Nothing)
            lblLocationName.Text = clsEmployeeMaster.GetLocationName(txtEmpCode.Value, Nothing)
            txtBranch.Value = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            fndDivision.Value = obj.Division_Code
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from tspl_devision_master where DEVISION_CODE='" & fndDivision.Value & "'")
            Dim ii As Int16 = 0
            If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                LoadGridColumns()
                For Each objTr As clsLeaveAllotmentDetails In obj.ObjList
                    gvLeaveAllotment.Rows.AddNew()
                    ii = ii + 1
                    gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLineNo).Value = ii
                    gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLeaveCode).Value = objTr.LEAVE_CODE
                    gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLeaveName).Value = objTr.LEAVE_NAME
                    gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colAllotedLeave).Value = objTr.ALLOTED_LEAVE
                Next
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtPayPeriodCode.Value) <= 0 Then
            myMessages.blankValue("Pay Period Code")
            txtPayPeriodCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        End If
        'Dim strchk As String = " select LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT where EMP_CODE = '" + txtEmpCode.Value + "' and PAY_PERIOD_CODE ='" + txtPayPeriodCode.Value + "' "
        'Dim StrAllotmentCode As String = clsDBFuncationality.getSingleValue(strchk)
        'If clsCommon.myLen(StrAllotmentCode) > 0 Then
        '    clsCommon.MyMessageBoxShow("Record already Exists for Selected Employee and Selected Pay Period in Leave Allotment Code : '" + StrAllotmentCode + "' . New Recort can Not be Genrated .")
        '    Return False
        'End If
        If Not CheckForExisingRecord() Then
            Return False
        End If
        Dim II As Int16 = 0
        For Each grow As GridViewRowInfo In gvLeaveAllotment.Rows
            II = II + 1

            If clsCommon.myCdbl(grow.Cells(colAllotedLeave).Value) > 366 Then
                clsCommon.MyMessageBoxShow("Value of Alloted Leave can Not be grater then 366.")
            End If

        Next
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsLeaveAllotment.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmLeaveAllotment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveAllotment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtPayPeriodCode.Value = Nothing
        lblPayPeriodName.Text = ""
        lblEmpName.Text = ""
        txtEmpCode.Value = Nothing
        txtBranch.Value = Nothing
        lblLocationDesc.Text = ""
        LoadGridColumns()
        'LoadDefaultDataInGrid()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        fndDivision.Value = ""
        lblDivisionName.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select LVALLOTMENT_CODE as Code,  PAY_PERIOD_CODE as 'Pay Period Nmae', EMP_CODE as 'Employee Code' from TSPL_LEAVE_ALLOTMENT "
            txtCode.Value = clsCommon.ShowSelectForm("LVALLOTMENT_FND", qry, "Code", "", txtCode.Value, "LVALLOTMENT_CODE", isButtonClicked)
            lblEmpName.Text = clsEmployeeMaster.GetName(txtCode.Value, Nothing)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmLeaveAllotment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtPayPeriodCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayPeriodCode._MYValidating
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtPayPeriodCode.Value = clsPayPeriodMaster.getFinder("POSTED=1 and FREEZED=0", txtPayPeriodCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtPayPeriodCode.Value, Nothing)
        Dim objPayrollSett As clsPayrollSetting = clsPayrollSetting.GetPayrollSetting(txtBranch.Value, Nothing)
        If objPayrollSett Is Nothing Then
            Exit Sub
        End If
        If clsCommon.myLen(txtPayPeriodCode.Value) > 0 Then
            LoadDefaultDataInGrid()
        End If

        'If clsCommon.myLen(txtPayPeriodCode.Value) > 0 Then
        '    CheckForExisingRecord()
        'End If
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating
        'Dim qry As String = " select EMP_CODE as Code,  Emp_Name as Name from TSPL_EMPLOYEE_MASTER "
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location")
            Exit Sub
        End If
        txtEmpCode.Value = clsEmployeeMaster.getFinder("Location_Code='" & txtBranch.Value & "'", txtEmpCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("EMP_FND", qry, "Code", "", txtEmpCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)
        lblDepartmentName.Text = clsEmployeeMaster.GetDepartmentName(txtEmpCode.Value, Nothing)
        lblDesignationName.Text = clsEmployeeMaster.GetDesignationName(txtEmpCode.Value, Nothing)
        lblLocationName.Text = clsEmployeeMaster.GetLocationName(txtEmpCode.Value, Nothing)
        'If clsCommon.myLen(txtEmpCode.Value) > 0 Then
        '    CheckForExisingRecord()
        'End If
    End Sub

    Sub LoadGridColumns()
        gvLeaveAllotment.DataSource = Nothing
        gvLeaveAllotment.Rows.Clear()
        gvLeaveAllotment.Columns.Clear()

        gvLeaveAllotment.ReadOnly = False
        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 50
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveAllotment.Columns.Add(lineNo)

        leaveCode = New GridViewTextBoxColumn()
        leaveCode.FormatString = ""
        leaveCode.HeaderText = "Leave Code"
        leaveCode.Name = colLeaveCode
        leaveCode.Width = 150
        leaveCode.ReadOnly = True
        leaveCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvLeaveAllotment.Columns.Add(leaveCode)

        leaveName = New GridViewTextBoxColumn()
        leaveName.FormatString = ""
        leaveName.HeaderText = "Leave Name"
        leaveName.Name = colLeaveName
        leaveName.Width = 200
        leaveName.ReadOnly = True
        leaveName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvLeaveAllotment.Columns.Add(leaveName)

        allotedLeave = New GridViewDecimalColumn()
        allotedLeave.FormatString = ""
        allotedLeave.HeaderText = "Alloted Leave"
        allotedLeave.Name = colAllotedLeave
        allotedLeave.Width = 150
        allotedLeave.ReadOnly = False
        allotedLeave.Maximum = 366
        allotedLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLeaveAllotment.Columns.Add(allotedLeave)
    End Sub

    Sub LoadDefaultDataInGrid()
        Dim dt As DataTable = clsLeaveAllotmentDetails.BlankDataTableForGrid()
        Dim ii As Int16 = 0
        Dim PP_Start_date As Date
        gvLeaveAllotment.Rows.Clear()
        PP_Start_date = clsPayPeriodMaster.GetFromDate(txtPayPeriodCode.Value, Nothing)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                gvLeaveAllotment.Rows.AddNew()
                ii = ii + 1
                gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLineNo).Value = ii
                gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLeaveCode).Value = dr("LEAVE_CODE")
                gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colLeaveName).Value = dr("LEAVE_NAME")
                gvLeaveAllotment.Rows(gvLeaveAllotment.Rows.Count - 1).Cells(colAllotedLeave).Value = clsLeaveAllotment.GetMonthlyAllotLeave(txtEmpCode.Value, dr("LEAVE_CODE"), txtPayPeriodCode.Value, PP_Start_date)
            Next
        End If
    End Sub

    Private Sub gvLeaveAllotment_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvLeaveAllotment.CurrentColumnChanged
        '    If gvLeaveAllotment.Rows.Count > 0 Then
        '        Dim intCurrRow As Integer = gvLeaveAllotment.CurrentRow.Index
        '        gvLeaveAllotment.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
        '        If intCurrRow = gvLeaveAllotment.Rows.Count - 1 Then
        '            gvLeaveAllotment.Rows.AddNew()
        '            gvLeaveAllotment.CurrentRow = gvLeaveAllotment.Rows(intCurrRow)
        '        End If
        '    End If
    End Sub

    Private Sub gvLeaveAllotment_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvLeaveAllotment.UserDeletingRow
        '    If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '        e.Cancel = True
        '    End If
    End Sub

    Private Sub gvLeaveAllotment_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLeaveAllotment.CellValueChanged

        'If e.Column Is gvLeaveAllotment.Columns("LeaveCode") Then
        '    Dim qry As String = " select LEAVE_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_LEAVE_MASTER"
        '    gvLeaveAllotment.CurrentRow.Cells(1).Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", clsCommon.myCstr(gvLeaveAllotment.CurrentRow.Cells(1).Value), "LEAVE_CODE", False)
        '    gvLeaveAllotment.CurrentRow.Cells(2).Value = clsLeaveMaster.GetName(clsCommon.myCstr(gvLeaveAllotment.CurrentRow.Cells(1).Value), Nothing)
        'End If

    End Sub

    Function CheckForExisingRecord() As Boolean
        If clsCommon.myLen(txtPayPeriodCode.Value) > 0 And clsCommon.myLen(txtEmpCode.Value) > 0 Then
            Dim str As String = "select LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT where PAY_PERIOD_CODE ='" + txtPayPeriodCode.Value + "' and EMP_CODE ='" + txtEmpCode.Value + "' and LVALLOTMENT_CODE <>'" + txtCode.Value + "' "
            Dim Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
            If clsCommon.myLen(Code) > 0 Then
                Dim strmessage As String = "Record Exists for Selected Employee for Selected Pay Period on Allotment Code " + Code + ", New Record can not be genrated on same. Do you want to open Previous Saved ? "
                If clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    LoadData(Code, NavigatorType.Current)
                End If
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub btnAllotAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllotAll.Click
        If clsCommon.myLen(txtBranch.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location !")
            Exit Sub
        End If
        If clsCommon.myLen(txtPayPeriodCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Pay Period Code !")
            Exit Sub
        End If
        Dim objPayrollSett As clsPayrollSetting = clsPayrollSetting.GetPayrollSetting(txtBranch.Value, Nothing)
        If objPayrollSett Is Nothing Then
            Exit Sub
        End If
        Dim total As Integer = clsLeaveAllotment.UpdateLeaveAllotmentAllEmployee(Me.txtEmpCode.Value, "", txtPayPeriodCode.Value, txtBranch.Value, fndDivision.Value)
        If total > 0 Then
            clsCommon.MyMessageBoxShow("" & total & " records saved successfully")
        End If
    End Sub

    Private Sub txtBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBranch._MYValidating
        txtBranch.Value = clsLocation.getFinder("Location_Type='Physical'", Me.txtBranch.Value, isButtonClicked)
        lblLocationDesc.Text = clsLocation.GetName(txtBranch.Value, Nothing)
    End Sub

    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        Dim qry As String = "Select DEVISION_CODE as [Code] ,DEVISION_NAME  as [Name] from tspl_devision_master"
        fndDivision.Value = clsCommon.ShowSelectForm("PaymentMode", qry, "Code", "", fndDivision.Value, "CODE", isButtonClicked)
        If clsCommon.myLen(fndDivision.Value) > 0 Then
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from tspl_devision_master where DEVISION_CODE='" & fndDivision.Value & "'")
        Else
            lblDivisionName.Text = ""
        End If

    End Sub
End Class
