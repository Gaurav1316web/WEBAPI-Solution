'--02/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient

Public Class frmLeaveAdjustment
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New clsLeaveAdjustment()
                obj.LVADJUSTMENT_CODE = txtCode.Value
                obj.LEAVE_CODE = txtLeaveCode.Value
                obj.PAY_PERIOD_CODE = txtPayPeriodCode.Value
                obj.EMP_CODE = txtEmpCode.Value
                obj.LEAVE_REASON = txtReason.Text
                obj.Location_Code = fndLocation.Value
                obj.ADJUSTMENT_DATE = clsCommon.GetPrintDate(dtpAdjustDate.Value, "dd/MMM/yyyy")
                obj.ADJUST_ALLOTED = txtAdjustAlloted.Value
                obj.ADJUST_AVAILED = txtAdjustAvail.Value
                obj.POSTED = False
                If (obj.SaveData(obj, txtCode.Value, isNewEntry)) Then
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.LVADJUSTMENT_CODE, NavigatorType.Current)
                    Return True
                    'Else
                    '    common.clsCommon.MyMessageBoxShow("This '" & obj.LVADJUSTMENT_CODE & "' already exist ")
                End If
            End If
            Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsLeaveAdjustment()
        obj = clsLeaveAdjustment.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LVADJUSTMENT_CODE) > 0) Then
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
            txtCode.Value = obj.LVADJUSTMENT_CODE
            txtLeaveCode.Value = obj.LEAVE_CODE
            txtPayPeriodCode.Value = obj.PAY_PERIOD_CODE
            txtEmpCode.Value = obj.EMP_CODE
            txtReason.Text = obj.LEAVE_REASON
            dtpAdjustDate.Value = clsCommon.GetPrintDate(obj.ADJUSTMENT_DATE, "dd/MMM/yyyy")
            txtAdjustAlloted.Value = obj.ADJUST_ALLOTED
            txtAdjustAvail.Value = obj.ADJUST_AVAILED
            If clsCommon.myLen(obj.Location_Code) > 0 Then
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            Else
                fndLocation.Value = ""
                lblLocationName.Text = ""
            End If
            txtPayPeriodCode__MYValidating(Nothing, Nothing, False)
            txtEmpCode__MYValidating(Nothing, Nothing, False)
            txtLeaveCode__MYValidating(Nothing, Nothing, False)
        End If

    End Sub

    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LEAVE_ADJUSTMENT where LVADJUSTMENT_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("LVADJUSTMENT_CODE")
        '    txtCode.Focus()
        '    Return False
        'Else
        If clsCommon.myLen(txtLeaveCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Leave Code", Me.Text)
            txtLeaveCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtPayPeriodCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Pay Period Code", Me.Text)
            txtPayPeriodCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtEmpCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Employee Code", Me.Text)
            txtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtReason.Text) <= 0 Then
            myMessages.blankValue(Me, "Leave Reason", Me.Text)
            txtReason.Focus()
            Return False
        ElseIf clsCommon.myLen(dtpAdjustDate.Value) <= 0 Then
            myMessages.blankValue(Me, "Adjustment Date", Me.Text)
            dtpAdjustDate.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
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
                If (clsLeaveAdjustment.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtcode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub frmLeaveAdjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocationName.Text = ""
        End If
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveAdjustment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
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
        txtLeaveCode.Value = Nothing
        lblLeaveName.Text = ""
        txtPayPeriodCode.Value = Nothing
        lblPayPeriodName.Text = ""
        txtEmpCode.Value = Nothing
        lblEmpName.Text = ""
        txtReason.Text = ""
        dtpAdjustDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        txtAdjustAlloted.Value = 0
        txtAdjustAvail.Value = 0
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If

        Dim str As String = "select count(*) from TSPL_LEAVE_ADJUSTMENT where LVADJUSTMENT_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select LVADJUSTMENT_CODE AS Code, EMP_CODE as 'Employee Code', PAY_PERIOD_CODE as 'Pay Period Code', LEAVE_CODE as 'Leave Code', ADJUSTMENT_DATE as 'Adjustment Date',ADJUST_ALLOTED as 'Adjust Alloted', ADJUST_AVAILED as 'Adjust Availed', LEAVE_REASON as 'Leave Reason', POSTED as 'Is Posted',Location_Code As 'Location Code'  from TSPL_LEAVE_ADJUSTMENT"
            txtCode.Value = clsCommon.ShowSelectForm("LEAVE_ADJUSTMENT", qry, "Code", whrcls, txtCode.Value, "LVADJUSTMENT_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmLeaveAdjustment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Private Sub txtPayPeriodCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayPeriodCode._MYValidating
        Dim qry As String = "select PAY_PERIOD_CODE as LVADJUSTMENT_CODE , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtPayPeriodCode.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "LVADJUSTMENT_CODE", "POSTED=1 and FREEZED=0", txtPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtPayPeriodCode.Value, Nothing)
    End Sub

    Private Sub txtEmpCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmpCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim qry As String = " select EMP_CODE as LVADJUSTMENT_CODE,  Emp_Name as Name, LOCATION_CODE As 'LOCATION CODE' from TSPL_EMPLOYEE_MASTER "
        txtEmpCode.Value = clsCommon.ShowSelectForm("EMP_FND", qry, "LVADJUSTMENT_CODE", whrcls, txtEmpCode.Value, "EMP_CODE", isButtonClicked)
        lblEmpName.Text = clsEmployeeMaster.GetName(txtEmpCode.Value, Nothing)
    End Sub
    Private Sub txtLeaveCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLeaveCode._MYValidating
        Dim qry As String = "select LEAVE_CODE AS LVADJUSTMENT_CODE, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_LEAVE_MASTER"
        txtLeaveCode.Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "LVADJUSTMENT_CODE", "", txtLeaveCode.Value, "LEAVE_CODE", isButtonClicked)
        lblLeaveName.Text = clsLeaveMaster.GetName(txtLeaveCode.Value, Nothing)
    End Sub

    Private Sub txtAdjustAlloted_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdjustAlloted.TextChanged
        txtAdjustAvail.Value = 0
    End Sub

    Private Sub txtAdjustAvail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdjustAvail.TextChanged
        txtAdjustAlloted.Value = 0
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsLeaveAdjustment.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
