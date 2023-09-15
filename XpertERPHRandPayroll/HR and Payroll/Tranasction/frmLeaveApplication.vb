'--01/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class frmLeaveApplication
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim attachqry As String = ""

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsLeaveApplication()

            obj.LVAPPLICATION_CODE = txtCode.Value
            obj.EMP_CODE = txtEmpCode.Value
            obj.LEAVE_CODE = txtLeaveCode.Value
            obj.PAY_PERIOD_CODE = txtPayPeriod.Value
            obj.Location_Code = fndLocation.Value
            If clsCommon.myLen(dtpApplicableFrom.Text) > 0 Then
                obj.APPLICATION_DATE = clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")
            Else
                obj.APPLICATION_DATE = Nothing
            End If

            If clsCommon.myLen(dtpFromDate.Text) > 0 Then
                obj.FROM_DATE = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            Else
                obj.FROM_DATE = Nothing
            End If

            If clsCommon.myLen(dtpToDate.Text) > 0 Then
                obj.TO_DATE = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            Else
                obj.TO_DATE = Nothing
            End If
            If chkFirstHalf.Checked Then
                obj.FIRST_HALF = True
            Else
                obj.FIRST_HALF = False
            End If
            If chkSecondHalf.Checked Then
                obj.SEC_HALF = True
            Else
                obj.SEC_HALF = False
            End If

            obj.TOTAL_DAYS = txtLeaveDays.Text
            obj.LEAVE_REASON = txtReason.Text


            If (obj.SaveData(obj, txtCode.Value, isNewEntry)) Then
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.LVAPPLICATION_CODE, NavigatorType.Current)
                Return True
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If
        End If
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsLeaveApplication()
        obj = clsLeaveApplication.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LVAPPLICATION_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

            txtCode.Value = obj.LVAPPLICATION_CODE
            txtEmpCode.Value = obj.EMP_CODE
            txtLeaveCode.Value = obj.LEAVE_CODE
            txtPayPeriod.Value = obj.PAY_PERIOD_CODE
            dtpApplicableFrom.Value = clsCommon.GetPrintDate(obj.APPLICATION_DATE, "dd/MMM/yyyy")
            dtpFromDate.Value = clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy")
            dtpToDate.Value = clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy")
            chkFirstHalf.Checked = obj.FIRST_HALF
            chkSecondHalf.Checked = obj.SEC_HALF
            txtLeaveDays.Text = obj.TOTAL_DAYS
            txtReason.Text = obj.LEAVE_REASON
            LoadGridColumns()
            txtLeaveCode__MYValidating(Nothing, Nothing, False)
            txtPayPeriod__MYValidating(Nothing, Nothing, False)
            txtEmpCode__MYValidating(Nothing, Nothing, False)
            If clsCommon.myLen(obj.Location_Code) > 0 Then
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            Else
                fndLocation.Value = ""
                lblLocationName.Text = ""
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LEAVE_APPLICATION where LVAPPLICATION_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtEmpCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtLeaveCode.Value) <= 0 Then
            myMessages.blankValue("Leave Code")
            txtLeaveCode.Focus()
            Return False
        End If
        If clsCommon.myLen(dtpApplicableFrom.Value) <= 0 Then
            myMessages.blankValue("Applicable From Date")
            dtpApplicableFrom.Focus()
            Return False
        End If
        If clsCommon.myLen(txtPayPeriod.Value) <= 0 Then
            myMessages.blankValue("Pay Period Code")
            txtPayPeriod.Focus()
            Return False
        End If
        If clsCommon.myLen(dtpFromDate.Value) <= 0 Then
            myMessages.blankValue("From Date")
            dtpFromDate.Focus()
            Return False
        End If
        If clsCommon.myLen(dtpToDate.Value) <= 0 Then
            myMessages.blankValue("To Date")
            dtpToDate.Focus()
            Return False
        End If
        If dtpFromDate.Value > dtpToDate.Value Then
            clsCommon.MyMessageBoxShow("From Date Can Not be Grater then To Date")
            dtpToDate.Focus()
            Return False
        End If
        If clsCommon.myLen(txtLeaveDays.Text) <= 0 Then
            myMessages.blankValue("Leave Days")
            txtLeaveDays.Focus()
            Return False
        End If
        If clsCommon.myLen(txtReason.Text) <= 0 Then
            myMessages.blankValue("Reason")
            txtReason.Focus()
            Return False
        End If
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
                If (clsLeaveApplication.DeleteData(txtCode.Value)) Then
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

    Private Sub frmLeaveApplication_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
        'LoadGridColumns()
        RadMenuItem1.Visibility = ElementVisibility.Collapsed
        funReset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveApplication)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Sub funReset()
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtEmpCode.Value = Nothing
        lblEmpName.Text = ""
        txtLeaveCode.Value = Nothing
        lblLeaveName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpApplicableFrom.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        txtPayPeriod.Value = Nothing
        lblPayPeriodName.Text = ""
        dtpFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        dtpToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        chkFirstHalf.Checked = False
        chkSecondHalf.Checked = False
        txtLeaveDays.Text = ""
        txtReason.Text = ""
        LoadGridColumns()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim StrJoin As String = ""
        Dim StrWhere As String = ""
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " And tspl_user_master.Default_Location='" + LocCode + "'"
            End If
        End If
        If objCommonVar.IsLoginUserHRAdmin = True Then
            StrJoin = ""
            StrWhere = ""
        Else
            StrJoin = " LEFT JOIN tspl_user_master ON TSPL_LEAVE_APPLICATION.EMP_CODE=tspl_user_master.EMP_CODE "
            StrWhere = " tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "' " + whrcls
        End If

        Dim str As String = "select count(*) from TSPL_LEAVE_APPLICATION " + StrJoin + " where 1=1 " + IIf(clsCommon.myLen(StrWhere) > 0, " and " + StrWhere, "") + " and LVAPPLICATION_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select LVAPPLICATION_CODE as Code, TSPL_LEAVE_APPLICATION.EMP_CODE as 'Emp Code', APPLICATION_DATE as 'Application Date',PAY_PERIOD_CODE as 'Pay Peroid Code' ,LEAVE_CODE as 'Leave Code',FROM_DATE as 'From Date', TO_DATE as 'To DATE',FIRST_HALF AS 'First Half',SEC_HALF AS 'Sec Half', TOTAL_DAYS AS 'Total Days', LEAVE_REASON AS 'Leave Reason' , POSTED AS 'Is Posted' from TSPL_LEAVE_APPLICATION " + StrJoin + " "
            txtCode.Value = clsCommon.ShowSelectForm("LEAVE_APPLICATION", qry, "Code", StrWhere, txtCode.Value, "LVAPPLICATION_CODE", isButtonClicked)
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

    Private Sub frmLeaveApplication_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Sub LoadGridColumns()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = "LineNo"
        lineNo.Width = 30
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim leaveCode As New GridViewTextBoxColumn
        leaveCode.FormatString = ""
        leaveCode.HeaderText = "Leave Code"
        leaveCode.Name = "LeaveCode"
        leaveCode.Width = 100
        leaveCode.ReadOnly = True
        leaveCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(leaveCode)

        Dim leaveName As New GridViewTextBoxColumn
        leaveName.FormatString = ""
        leaveName.HeaderText = "Leave Name"
        leaveName.Name = "LeaveName"
        leaveName.Width = 125
        leaveName.ReadOnly = True
        leaveName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(leaveName)

        Dim allotedLeave As New GridViewDecimalColumn
        allotedLeave.FormatString = ""
        allotedLeave.HeaderText = "Alloted Leave"
        allotedLeave.Name = "AllotedLeave"
        allotedLeave.Width = 100
        allotedLeave.ReadOnly = True
        allotedLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(allotedLeave)

        Dim availedLeave As New GridViewDecimalColumn
        availedLeave.FormatString = ""
        availedLeave.HeaderText = "Availed Leave"
        availedLeave.Name = "AvailedLeave"
        availedLeave.Width = 100
        availedLeave.ReadOnly = True
        availedLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(availedLeave)

        Dim balanceLeave As New GridViewDecimalColumn
        balanceLeave.FormatString = ""
        balanceLeave.HeaderText = "Balance Leave"
        balanceLeave.Name = "BalanceLeave"
        balanceLeave.Width = 100
        balanceLeave.ReadOnly = True
        balanceLeave.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(balanceLeave)

    End Sub

    Private Sub frmLeaveApplication_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtLeaveCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLeaveCode._MYValidating
        Dim qry As String = "select LEAVE_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_LEAVE_MASTER"
        txtLeaveCode.Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", txtLeaveCode.Value, "LEAVE_CODE", isButtonClicked)
        lblLeaveName.Text = clsLeaveMaster.GetName(txtLeaveCode.Value, Nothing)
        If clsCommon.myLen(txtLeaveCode.Value) > 0 Then
            LoadGridData()
        End If
    End Sub

    Private Sub txtPayPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayPeriod._MYValidating
        Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtPayPeriod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayPeriod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
        If clsCommon.myLen(txtPayPeriod.Value) > 0 Then
            LoadGridData()
        End If

    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating
        Dim StrJoin As String = ""
        Dim StrWhere As String = ""
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " And tspl_user_master.Default_Location='" + LocCode + "'"
            End If
        End If
        If objCommonVar.IsLoginUserHRAdmin = True Then
            StrJoin = ""
            StrWhere = ""
        Else
            StrJoin = " LEFT JOIN tspl_user_master ON TSPL_EMPLOYEE_MASTER.EMP_CODE=tspl_user_master.EMP_CODE "
            StrWhere = " tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "' " + whrcls
        End If
        Dim qry As String = " select TSPL_EMPLOYEE_MASTER.EMP_CODE as Code,  TSPL_EMPLOYEE_MASTER.Emp_Name as Name from TSPL_EMPLOYEE_MASTER " + StrJoin + " "
        txtEmpCode.Value = clsCommon.ShowSelectForm("EMP_FND", qry, "Code", StrWhere, txtEmpCode.Value, "TSPL_EMPLOYEE_MASTER.EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetData(txtEmpCode.Value, Nothing).Emp_Name
        If clsCommon.myLen(txtEmpCode.Value) > 0 Then
            LoadGridData()
        End If
    End Sub

    Private Sub Leave_Calculate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.Validated, dtpFromDate.Validated, chkFirstHalf.Validated, chkSecondHalf.Validated
        If clsCommon.myLen(dtpFromDate.Value) > 0 And clsCommon.myLen(dtpToDate.Value) > 0 Then
            If dtpFromDate.Value <= dtpToDate.Value Then
                Dim d1 As DateTime = dtpFromDate.Value
                Dim d2 As DateTime = dtpToDate.Value
                Dim Day_lv As Double = 0
                Day_lv = ((d2 - d1).TotalDays) + 1
                If chkSecondHalf.Checked Then
                    Day_lv = Day_lv - 0.5
                End If
                If chkFirstHalf.Checked Then
                    Day_lv = Day_lv - 0.5
                End If
                txtLeaveDays.Text = Day_lv
            Else
                If dtpToDate.Value <> clsCommon.GETSERVERDATE() Then
                    clsCommon.MyMessageBoxShow("From Date Can Not be Grater then To Date")
                    txtLeaveDays.Text = ""
                End If
            End If

        End If
    End Sub

    Private Sub LoadGridData()
        If clsCommon.myLen(txtPayPeriod.Value) > 0 And clsCommon.myLen(txtEmpCode.Value) > 0 Then

            Dim Qry As String = " "
            'Qry += " select final.*, (isnull(final .[Alloted Leave],0) - isnull(final .[Availed Leave],0 )) AS 'Balance Leave'  from ("
            'Qry += "   select TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE AS 'Leave Code',"
            'Qry += "   TSPL_LEAVE_MASTER.LEAVE_NAME AS 'Leave Name' ,"
            'Qry += "   TSPL_LEAVE_ALLOTMENTDETAIL.ALLOTED_LEAVE AS 'Alloted Leave' , "
            'Qry += "   TSPL_LEAVE_ALLOTMENT .EMP_CODE ,"
            'Qry += "   TSPL_LEAVE_ALLOTMENT .PAY_PERIOD_CODE ,"
            'Qry += "   ( select SUM(TSPL_LEAVE_APPLICATION.TOTAL_DAYS ) from  TSPL_LEAVE_APPLICATION where TSPL_LEAVE_APPLICATION.EMP_CODE = '" + txtEmpCode.Value + "' and TSPL_LEAVE_APPLICATION.PAY_PERIOD_CODE = '" + txtPayPeriod.Value + "'  and TSPL_LEAVE_APPLICATION.LEAVE_CODE = TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE"
            'Qry += " group by TSPL_LEAVE_APPLICATION.EMP_CODE ,TSPL_LEAVE_APPLICATION.PAY_PERIOD_CODE,TSPL_LEAVE_APPLICATION.LEAVE_CODE "
            'Qry += " )AS 'Availed Leave' "
            'Qry += " from TSPL_LEAVE_ALLOTMENT "
            'Qry += " left outer join  TSPL_LEAVE_ALLOTMENTDETAIL on TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE = TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE "
            'Qry += " LEFT OUTER JOIN TSPL_LEAVE_MASTER ON TSPL_LEAVE_MASTER.LEAVE_CODE  = TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE "
            'Qry += " where TSPL_LEAVE_ALLOTMENT.EMP_CODE = '" + txtEmpCode.Value + "' "
            'Qry += " and TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE = '" + txtPayPeriod.Value + "' "
            'Qry += " )  as final"
            'If clsCommon.myLen(txtLeaveCode.Value) > 0 Then
            '    Qry += " where final.[Leave Code] = '" + txtLeaveCode.Value + "' "
            'End If
            Qry = " SELECT EMP_CODE AS [Employee Code],lStatus.LEAVE_CODE as [Leave Code],TSPL_LEAVE_MASTER.LEAVE_TYPE as [Leave Type],OPENING as [Opening Balance],ALLOTED as [Allotted]," &
                  " AVAILED as [Availed],ADJUSTMENT_PLUS as [Adjustment Plus],ADJUSTMENT_MINUS as [Adjustment Minus],BALANCE as [Balance] " &
                  " FROM TSPL_FUN_LEAVE_STATUS('" & Me.txtPayPeriod.Value & "') as lStatus inner join TSPL_LEAVE_MASTER on lStatus.LEAVE_CODE=TSPL_LEAVE_MASTER.LEAVE_CODE where EMP_CODE='" & Me.txtEmpCode.Value & "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dt

            'LoadGridColumns()
            'Dim ii As Int16 = 0
            'For Each dr As DataRow In dt.Rows
            '    gv1.Rows.AddNew()
            '    ii = ii + 1
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("LineNo").Value = ii
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("LeaveCode").Value = dr("Leave Code")
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("LeaveName").Value = dr("Leave Name")
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("AllotedLeave").Value = dr("Alloted Leave")
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("AvailedLeave").Value = dr("Availed Leave")
            '    gv1.Rows(gv1.Rows.Count - 1).Cells("BalanceLeave").Value = dr("Balance Leave")
            'Next

        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsLeaveApplication.PostData(MyBase.Form_ID, txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                    Dim lstUsers As New List(Of String)
                    lstUsers.Add(txtEmpCode.Value)
                    'SendSMSandEmail(lstUsers, False)
                    MailSend(lstUsers, False)
                    'Else
                    '    Dim dt As DataTable = Nothing
                    '    Dim msg As String = ""

                    '    Qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    '    dt = clsDBFuncationality.GetDataTable(Qry)
                    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    '        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                    '        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                    '        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                    '            msg = "Level 1 Approval done. "
                    '            If NoOflevel = 1 Then
                    '                msg += "Successfully Posted. "
                    '            Else
                    '                msg += "Level 2 Approval Required."
                    '            End If
                    '        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                    '            msg = "Level 2 Approval done. "
                    '            If NoOflevel = 2 Then
                    '                msg += "Successfully Posted "
                    '            Else
                    '                msg += "Level 3 Approval Required."
                    '            End If
                    '        Else
                    '            msg = "Level 3 Approval done. Successfully Posted. "
                    '        End If
                    '    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    'sanjay Ticket no-TEC/07/06/19-000526
    Private Sub MailSend(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", Nothing)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Leave_App_No, txtCode.Value)
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Application_Date, clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy"))

                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_App_No, txtCode.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Application_Date, clsCommon.GetPrintDate(dtpApplicableFrom.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_Days, txtLeaveDays.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_From, clsCommon.GetPrintDate(dtpFromDate.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_To, clsCommon.GetPrintDate(dtpToDate.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_Reason, txtReason.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Leave_Type, txtReason.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Employee_Name, lblEmpName.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.EMP_CODE, txtEmpCode.Value)

                For Each strUser As String In lstUsers
                    Dim lstReceiptents As New List(Of String)
                    Dim qry As String = ""
                    Dim emailId As String = ""
                    If isSendForApproval Then
                        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                        emailId = clsDBFuncationality.getSingleValue(qry)
                    Else
                        emailId = clsDBFuncationality.getSingleValue("select EMail_ID from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" & strUser & "' ")
                    End If

                    If clsCommon.myLen(emailId) > 0 Then
                        lstReceiptents.Add(emailId)
                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))
                    End If

                Next

                objEmailH.SaveData(MyBase.Form_ID, objEmailH, Nothing)
                objEmailH = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try



    End Sub


    'Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)

    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmLeaveApplication)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.Leave_App_No, txtCode.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.Application_Date, clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.Leave_App_No, txtCode.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Application_Date, clsCommon.GetPrintDate(dtpApplicableFrom.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.Leave_Days, txtLeaveDays.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Leave_From, clsCommon.GetPrintDate(dtpFromDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.Leave_To, clsCommon.GetPrintDate(dtpToDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.Leave_Reason, txtReason.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Leave_Type, txtReason.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Employee_Name, lblEmpName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.EMP_CODE, txtEmpCode.Value)

    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        'If obj.atchmnt = "Y" Then
    '        '    atchqry = GetAtachmntPrint(txtDocNo.Value)
    '        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        SetItemWiseTax(dt1, txtDocNo.Value)
    '        '        strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptShipment", "Shipment Detail")
    '        '    End If
    '        'End If
    '        '---------------------------------------------------------------------------


    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                'strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                'strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select EMail_ID from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" & strUser & "' ")
    '            End If

    '            'strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            If clsCommon.myLen(emailId) > 0 Then
    '                lstReceiptents.Add(emailId)
    '            End If


    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)
    '            If lstReceiptents.Count > 0 Then
    '                clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '            End If

    '        Next
    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)


    '        'If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
    '        '    SMSSENDONLY(False)
    '        'End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub

    'Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
    '    If clsCommon.myLen(txtCode.Value) <= 0 Then
    '        clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
    '        txtCode.Focus()
    '        txtCode.Select()
    '        Return
    '    End If

    '    attachqry = GetAtchmentPrintQuery(txtCode.Value)
    '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)

    '    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
    '        System.Diagnostics.Process.Start(NewSalesReportViewer.Emailreport(dt1, "crptSalesOrderReport", "Sales Order"))
    '    End If
    'End Sub
    Public Function GetAtchmentPrintQuery(ByVal DocNo As String)
        attachqry = " select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate, * from TSPL_LEAVE_APPLICATION "
        Return attachqry
    End Function


    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmLeaveApplication
        frm.ShowDialog()
    End Sub

    Private Sub btnSenApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenApprove.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Application No. " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtCode.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_Mail_Receipt where 1=1 and Form_Id='" + MyBase.Form_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            'SendSMSandEmail(lstUsers, True)
            MailSend(lstUsers, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim frm As New FrmMailReceipt
        frm.Form_Id = MyBase.Form_ID
        frm.Show()
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            Dim Qry As String = "select Location_Code As [Location Code],Location_Desc As [Description] from TSPL_LOCATION_MASTER "
            fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
            ''fndLocation.Value = clsCommon.ShowSelectForm("SalaryLocation", Qry, "Location_Code", whrcls, "", "Location_Code", isButtonClicked)
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
