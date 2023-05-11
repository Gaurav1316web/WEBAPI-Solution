Imports common
Imports System.Data.SqlClient
Public Class FrmSendSMSEmailSetting
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isLoadData As Boolean = True
#End Region
    ' Ticket No : MIL/22/02/19-000047 By prabhakar
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        ' If Me.Is_EMAIL_Applied OrElse Me.Is_SMS_Applied OrElse Me.Is_Notification_Applied Then
        Dim frmPWD As New FrmPWD(Nothing)
        frmPWD.strType = clsFixedParameterType.SMSEMailPassword
        frmPWD.strCode = clsFixedParameterCode.SMSEMailPassword
        frmPWD.ShowDialog()
        If frmPWD.isPasswordCorrect Then
            'Dim qry As String = "select * from ( select Program_Code+'1' as Code,ES_Trans_Type_1 as Name  from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
            ' "union all" + Environment.NewLine + _
            ' "select Program_Code+'2' as Code,ES_Trans_Type_2 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
            ' "union all" + Environment.NewLine + _
            ' "select Program_Code+'3' as Code,ES_Trans_Type_3 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
            ' "union all" + Environment.NewLine + _
            ' "select Program_Code+'4' as Code,ES_Trans_Type_4 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "'" + Environment.NewLine + _
            ' "union all" + Environment.NewLine + _
            ' "select Program_Code+'5' as Code,ES_Trans_Type_5 as Name from TSPL_PROGRAM_MASTER where Program_Code='" + Me.Form_ID + "')xx where len(isnull(Name,''))>0"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strFormID As String = clsUserMgtCode.ShiftEndForAllMcc
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Dim dr As DataRow = dt.NewRow()
            '    dr("Code") = Me.Form_ID
            '    dr("Name") = "Transaction"
            '    dt.Rows.InsertAt(dr, 0)

            '    Dim frmFC As New FrmFreeComboBox
            '    frmFC.ComboSource = dt
            '    frmFC.ComboValueMember = "Code"
            '    frmFC.ComboDisplayMember = "Name"
            '    frmFC.ShowDialog()
            '    If clsCommon.myLen(frmFC.strRetValue) > 0 Then
            '        strFormID = clsCommon.myCstr(frmFC.strRetValue)
            '    Else
            '        strFormID = ""
            '    End If
            'End If
            If clsCommon.myLen(strFormID) > 0 Then
                Dim frm As New frmEMailAndSMSSetting
                frm.isForSMS = Me.Is_SMS_Applied
                frm.isForEMail = Me.Is_EMAIL_Applied
                frm.isForNotification = Me.Is_Notification_Applied
                frm.Form_ID = strFormID
                frm.ShowDialog()
            End If
        End If
        'End If
    End Sub

    Private Sub FrmSendSMSEmailSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isLoadData = True
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        Reset()
        isLoadData = False
    End Sub
    Sub LoadWeekDaysForEmail()
        cmbWeekDaysForEmail.DataSource = LoadWeekDays()
        cmbWeekDaysForEmail.ValueMember = "Code"
        cmbWeekDaysForEmail.DisplayMember = "Name"
        cmbWeekDaysForEmail.SelectedIndex = 0
    End Sub

    Sub LoadWeekDaysForSMS()
        cmbWeekDaysForSMS.DataSource = LoadWeekDays()
        cmbWeekDaysForSMS.ValueMember = "Code"
        cmbWeekDaysForSMS.DisplayMember = "Name"
        cmbWeekDaysForSMS.SelectedIndex = 0
    End Sub
    Sub LoadSCreenName()
        cboScreenName.DataSource = LoadScreen()
        cboScreenName.ValueMember = "Code"
        cboScreenName.DisplayMember = "Name"
        cboScreenName.SelectedIndex = 0
    End Sub

    Public Function LoadWeekDays() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "-Select-"
        dr("Name") = "-Select-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Monday"
        dr("Name") = "Monday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Tuesday"
        dr("Name") = "Tuesday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Wednesday"
        dr("Name") = "Wednesday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Thursday"
        dr("Name") = "Thursday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Friday"
        dr("Name") = "Friday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Saturday"
        dr("Name") = "Saturday"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Sunday"
        dr("Name") = "Sunday"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Public Function LoadScreen() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "-Select-"
        dr("Name") = "-Select-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ShiftEndForAllMcc
        dr("Name") = "Shift End For All MCC "
        dt.Rows.Add(dr)

        Return dt
    End Function

    'Public SCHEDULER_CODE As String = Nothing
    'Public DESCRIPTION As String = Nothing
    'Public SCREEN_ID As String = Nothing

    'Public IS_EMAIL_EVERY_DAY As Boolean = Nothing
    'Public IS_EMAIL_WEEKLY As Boolean = Nothing
    'Public EMAIL_WEEKLY_DAY As String = Nothing
    'Public IS_EMAIL_MONTHLY As Boolean = Nothing
    'Public MONTHLY_EMAIL_DATE As Date? = Nothing
    'Public IS_EMAIL_LAST_DAY_OF_MONTHLY As Boolean = Nothing
    'Public SCHEDULE_EMAIL_TIME As Date? = Nothing

    'Public IS_SMS_EVERY_DAY As Boolean = Nothing
    'Public IS_SMS_WEEKLY As Boolean = Nothing
    'Public SMS_WEEKLY_DAY As String = Nothing
    'Public IS_SMS_MONTHLY As Boolean = Nothing
    'Public MONTHLY_SMS_DATE As Date? = Nothing
    'Public IS_SMS_LAST_DAY_OF_MONTHLY As Boolean = Nothing
    'Public SCHEDULE_SMS_TIME As Date? = Nothing


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
    End Sub
    Private Sub Reset()
        rdbEmailNone.Checked = False
        rdbSMSNone.Checked = False
        rdbEmailEveryDays.Checked = False
        rdbEmailWeekly.Checked = False
        rdbEmailMonthly.Checked = False
        chkEmailLastDayOfMonth.Checked = False
        rdbSMSEveryDays.Checked = False
        rdbSMSWeekly.Checked = False
        rdbSMSMonthly.Checked = False
        chkSMSLastDayOfMonth.Checked = False
        txtEmailMonthly.Checked = False
        dtpEmailSchedulTime.Checked = False
        dtpEmailSchedulTime.Value = Nothing
        txtSMSMonthly.Checked = False
        dtpSMSSchedulTime.Checked = False
        dtpSMSSchedulTime.Value = Nothing
        txtEmailMonthly.Text = clsCommon.GetPrintDate("01/02/2018", "dd/MM/yyyy")
        txtSMSMonthly.Text = clsCommon.GetPrintDate("01/02/2018", "dd/MM/yyyy")
        txtSchedulerCode.Value = Nothing
        txtDescription.Text = Nothing
        'clsCommon.AddColumnsForChange(coll1, "Route_Time", clsCommon.GetPrintDate(txtRouteTime.Value, "dd/MMM/yyyy hh:mm tt"), True)
        LoadWeekDaysForEmail()
        LoadWeekDaysForSMS()
        LoadSCreenName()
        rbtnSave.Text = "Save"
    End Sub

    Private Sub rbtnClose_Click(sender As Object, e As EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnSave_Click(sender As Object, e As EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub

    Private Sub rbtnDelete_Click(sender As Object, e As EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Description")
            txtDescription.Focus()
            Return False
        End If

        ' For Email
        If rdbEmailEveryDays.Checked = True Or rdbEmailWeekly.Checked = True Or rdbEmailMonthly.Checked = True Then
            If dtpEmailSchedulTime.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please Check Email Schedule Time")
                Return False
            End If
            If String.IsNullOrEmpty(dtpEmailSchedulTime.Text) = True Then
                common.clsCommon.MyMessageBoxShow("Please select Email Schedule Time")
                Return False
            End If
            If dtpEmailSchedulTime.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please checked Email Schedule Time")
                Return False
            End If
        End If

        If rdbEmailWeekly.Checked = True Then
            If clsCommon.CompairString(cmbWeekDaysForEmail.SelectedValue, "-Select-") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("Please select WeekDays For Email")
                Return False
            End If
        End If
        If rdbEmailMonthly.Checked = True Then
            If chkEmailLastDayOfMonth.Checked = False AndAlso txtEmailMonthly.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please select Monthly day / Last day of month For Email")
                Return False
            End If
            If chkEmailLastDayOfMonth.Checked = True Then
                If String.IsNullOrEmpty(txtEmailMonthly.Value) = True Then
                    common.clsCommon.MyMessageBoxShow("Please select Monthly day For Email")
                    Return False
                End If
            End If
        End If

        ' For SMS
        If rdbSMSEveryDays.Checked = True Or rdbSMSWeekly.Checked = True Or rdbSMSMonthly.Checked = True Then
            If dtpEmailSchedulTime.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please Check SMS Schedule Time")
                Return False
            End If
            If String.IsNullOrEmpty(dtpSMSSchedulTime.Text) = True Then
                common.clsCommon.MyMessageBoxShow("Please select SMS Schedule Time")
                Return False
            End If
            If dtpSMSSchedulTime.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please checked SMS Schedule Time")
                Return False
            End If
        End If

        If rdbSMSWeekly.Checked = True Then
            If clsCommon.CompairString(cmbWeekDaysForSMS.SelectedValue, "-Select-") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("Please select WeekDays For SMS")
                Return False
            End If
        End If
        If rdbSMSMonthly.Checked = True Then
            If chkSMSLastDayOfMonth.Checked = False AndAlso txtSMSMonthly.Checked = False Then
                common.clsCommon.MyMessageBoxShow("Please select Monthly day / Last day of month For SMS")
                Return False
            End If
            If chkSMSLastDayOfMonth.Checked = True Then
                If String.IsNullOrEmpty(txtSMSMonthly.Value) = True Then
                    common.clsCommon.MyMessageBoxShow("Please select Monthly day For SMS")
                    Return False
                End If
            End If
        End If

        If rdbEmailEveryDays.Checked = False AndAlso rdbEmailWeekly.Checked = False AndAlso rdbEmailMonthly.Checked = False AndAlso rdbSMSEveryDays.Checked = False AndAlso rdbSMSWeekly.Checked = False AndAlso rdbSMSMonthly.Checked = False Then
            common.clsCommon.MyMessageBoxShow("Please select  Daily/Weekly/Monthly For SMS/Email")
            Return False
        End If
        If rdbEmailNone.Checked = True AndAlso rdbSMSNone.Checked = True Then
            common.clsCommon.MyMessageBoxShow("You can not select Email and SMS None.")
            Return False
        End If

        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim arr As New List(Of clsSendSMSEmailSetting)
                Dim obj As New clsSendSMSEmailSetting()
                obj.SCHEDULER_CODE = txtSchedulerCode.Value
                obj.DESCRIPTION = txtDescription.Text
                obj.SCREEN_ID = cboScreenName.SelectedValue
                ' For Email
                If rdbEmailEveryDays.Checked Then
                    obj.IS_EMAIL_EVERY_DAY = rdbEmailEveryDays.Checked
                End If
                If rdbEmailWeekly.Checked Then
                    obj.IS_EMAIL_WEEKLY = rdbEmailWeekly.Checked
                    obj.EMAIL_WEEKLY_DAY = cmbWeekDaysForEmail.SelectedValue
                End If
                If rdbEmailMonthly.Checked Then
                    If chkEmailLastDayOfMonth.Checked = False Then
                        obj.IS_EMAIL_MONTHLY = rdbEmailMonthly.Checked
                        obj.MONTHLY_EMAIL_DATE = txtEmailMonthly.Value
                    Else
                        obj.IS_EMAIL_MONTHLY = rdbEmailMonthly.Checked
                        obj.IS_EMAIL_LAST_DAY_OF_MONTHLY = chkEmailLastDayOfMonth.Checked
                    End If
                End If
                If rdbEmailEveryDays.Checked = True Or rdbEmailWeekly.Checked = True Or rdbEmailMonthly.Checked = True Then
                    obj.SCHEDULE_EMAIL_TIME = dtpEmailSchedulTime.Value
                End If


                ' For SMS
                If rdbSMSEveryDays.Checked Then
                    obj.IS_SMS_EVERY_DAY = rdbSMSEveryDays.Checked
                End If
                If rdbSMSWeekly.Checked Then
                    obj.IS_SMS_WEEKLY = rdbSMSWeekly.Checked
                    obj.SMS_WEEKLY_DAY = cmbWeekDaysForSMS.SelectedValue
                End If
                If rdbSMSMonthly.Checked Then
                    If chkSMSLastDayOfMonth.Checked = False Then
                        obj.IS_SMS_MONTHLY = rdbSMSMonthly.Checked
                        obj.MONTHLY_SMS_DATE = txtSMSMonthly.Value
                    Else
                        obj.IS_SMS_MONTHLY = rdbSMSMonthly.Checked
                        obj.IS_SMS_LAST_DAY_OF_MONTHLY = chkSMSLastDayOfMonth.Checked
                    End If
                End If

                If rdbSMSEveryDays.Checked = True Or rdbSMSWeekly.Checked = True Or rdbSMSMonthly.Checked = True Then
                    obj.SCHEDULE_SMS_TIME = dtpSMSSchedulTime.Value
                End If
                Dim DocExistOrNot As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count (*) from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE = '" + obj.SCHEDULER_CODE + "'"))
                If DocExistOrNot = False Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If



                arr.Add(obj)
                If obj.SaveData(arr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.SCHEDULER_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = False
            Reset()


            txtSchedulerCode.MyReadOnly = True
            Dim obj As New clsSendSMSEmailSetting()
            obj = clsSendSMSEmailSetting.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SCHEDULER_CODE) > 0) Then
                txtSchedulerCode.Value = obj.SCHEDULER_CODE
                txtDescription.Text = obj.DESCRIPTION
                cboScreenName.SelectedValue = obj.SCREEN_ID
                ' For Email
                If obj.IS_EMAIL_EVERY_DAY Then
                    rdbEmailEveryDays.Checked = obj.IS_EMAIL_EVERY_DAY
                End If
                If obj.IS_EMAIL_WEEKLY Then
                    rdbEmailWeekly.Checked = obj.IS_EMAIL_WEEKLY
                    cmbWeekDaysForEmail.SelectedValue = obj.EMAIL_WEEKLY_DAY
                End If
                If obj.IS_EMAIL_MONTHLY Then
                    If obj.IS_EMAIL_LAST_DAY_OF_MONTHLY = False Then
                        rdbEmailMonthly.Checked = obj.IS_EMAIL_MONTHLY
                        txtEmailMonthly.Value = obj.MONTHLY_EMAIL_DATE
                        txtEmailMonthly.Checked = True
                    Else
                        rdbEmailMonthly.Checked = obj.IS_EMAIL_MONTHLY
                        chkEmailLastDayOfMonth.Checked = obj.IS_EMAIL_LAST_DAY_OF_MONTHLY
                    End If
                End If
                If obj.IS_EMAIL_EVERY_DAY = True Or obj.IS_EMAIL_WEEKLY = True Or obj.IS_EMAIL_MONTHLY = True Then
                    dtpEmailSchedulTime.Value = obj.SCHEDULE_EMAIL_TIME
                    dtpEmailSchedulTime.Checked = True
                End If


                ' For SMS
                If obj.IS_SMS_EVERY_DAY Then
                    rdbSMSEveryDays.Checked = obj.IS_SMS_EVERY_DAY
                End If
                If obj.IS_SMS_WEEKLY Then
                    rdbSMSWeekly.Checked = obj.IS_SMS_WEEKLY
                    cmbWeekDaysForSMS.SelectedValue = obj.SMS_WEEKLY_DAY
                End If
                If obj.IS_SMS_MONTHLY Then
                    If obj.IS_SMS_LAST_DAY_OF_MONTHLY = False Then
                        rdbSMSMonthly.Checked = obj.IS_SMS_MONTHLY
                        txtSMSMonthly.Value = obj.MONTHLY_SMS_DATE
                        txtSMSMonthly.Checked = True
                    Else
                        rdbSMSMonthly.Checked = obj.IS_SMS_MONTHLY
                        chkSMSLastDayOfMonth.Checked = obj.IS_SMS_LAST_DAY_OF_MONTHLY
                    End If
                End If

                If obj.IS_SMS_EVERY_DAY = True Or obj.IS_SMS_WEEKLY = True Or obj.IS_SMS_MONTHLY = True Then
                    dtpSMSSchedulTime.Value = obj.SCHEDULE_SMS_TIME
                    dtpSMSSchedulTime.Checked = True
                End If
                rbtnSave.Text = "Update"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbEmailEveryDays_CheckedChanged(sender As Object, e As EventArgs) Handles rdbEmailEveryDays.CheckedChanged
        Try
            If rdbEmailEveryDays.Checked = True Then
                cmbWeekDaysForEmail.SelectedValue = "-Select-"
                txtEmailMonthly.Checked = False
                chkEmailLastDayOfMonth.Checked = False
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub rdbEmailWeekly_CheckedChanged(sender As Object, e As EventArgs) Handles rdbEmailWeekly.CheckedChanged
        Try
            If rdbEmailWeekly.Checked = True Then
                txtEmailMonthly.Checked = False
                chkEmailLastDayOfMonth.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbEmailMonthly_CheckedChanged(sender As Object, e As EventArgs) Handles rdbEmailMonthly.CheckedChanged
        Try
            If rdbEmailMonthly.Checked = True Then
                cmbWeekDaysForEmail.SelectedValue = "-Select-"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbSMSEveryDays_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSMSEveryDays.CheckedChanged
        Try
            If rdbSMSEveryDays.Checked = True Then
                cmbWeekDaysForSMS.SelectedValue = "-Select-"
                txtSMSMonthly.Checked = False
                chkSMSLastDayOfMonth.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbSMSWeekly_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSMSWeekly.CheckedChanged
        Try
            If rdbSMSWeekly.Checked = True Then
                txtSMSMonthly.Checked = False
                chkSMSLastDayOfMonth.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbSMSMonthly_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSMSMonthly.CheckedChanged
        Try
            If rdbSMSMonthly.Checked = True Then
                cmbWeekDaysForSMS.SelectedValue = "-Select-"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSchedulerCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtSchedulerCode._MYNavigator
        LoadData(txtSchedulerCode.Value, NavType)
    End Sub

    Private Sub txtSchedulerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSchedulerCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_EMAIL_SMS_SCHEDULING where SCHEDULER_CODE='" + txtSchedulerCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtSchedulerCode.MyReadOnly = False
            Else
                txtSchedulerCode.MyReadOnly = True
            End If
            If txtSchedulerCode.MyReadOnly OrElse isButtonClicked Then


                LoadData(clsSendSMSEmailSetting.getFinder("", txtSchedulerCode.Value, isButtonClicked), NavigatorType.Current)
                LoadData(txtSchedulerCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmSendSMSEmailSetting_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso rbtnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsSendSMSEmailSetting.DeleteData(txtSchedulerCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub
End Class
