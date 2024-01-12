' ----------------- Created By Anubhooti On 26-Aug-2014 Against BM00000003530-------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class frmAppointmentLetterHR
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAppointmentLetterHR)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Function AllowTosave() As Boolean
        Dim App_Date As DateTime
        Dim Joining_Date As DateTime

        Joining_Date = clsCommon.myCDate(dtpDOJ.Value)
        App_Date = clsCommon.myCDate(dtpAppointmentDate.Value)

        If clsCommon.myLen(txtAppcode.Value) <= 0 Then
            txtAppcode.Focus()
            Throw New Exception("Applicant Code cannot be left blank")
        End If
        If App_Date < Joining_Date Then
            dtpDOJ.Focus()
            Throw New Exception("Please check ! date of appointment must be greater than from joining date")
        End If
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim FixedSal As Double = 0
        Dim DtJoining As String = Nothing
        txtAppcode.MyReadOnly = True
        isNewEntry = False

        Dim obj As ClsAppointmentLetterHR = ClsAppointmentLetterHR.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtAppcode.Value = obj.Applicant_Code
            dtpAppointmentDate.Text = obj.Appointment_Date

            DtJoining = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Date_Of_Joining  From TSPL_HR_OFFER_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If clsCommon.myLen(DtJoining) > 0 Then
                dtpDOJ.Text = DtJoining
            End If

            FixedSal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
        End If
    End Sub

    '' ----------------------------------------------- Nav. Query(=) --------------------------------------------------------------------
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim FixedSal As Double = 0
        Dim DtJoining As String = Nothing
        txtAppcode.MyReadOnly = True
        isNewEntry = False

        Dim obj As ClsAppointmentLetterHR = ClsAppointmentLetterHR.GetDataForNav(strCode, NavTyep)
        If obj IsNot Nothing Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtAppcode.Value = obj.Applicant_Code
            dtpAppointmentDate.Text = obj.Appointment_Date

            DtJoining = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Date_Of_Joining  From TSPL_HR_OFFER_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If clsCommon.myLen(DtJoining) > 0 Then
                dtpDOJ.Text = DtJoining
            End If

            FixedSal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If

            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
        End If
    End Sub
    ' ---------------------------------------------------------
    Sub funReset()
        isNewEntry = True
        txtAppcode.MyReadOnly = False
        txtAppcode.Value = Nothing
        txtAppcode.Focus()

        txtFixedCTC.Value = 0
        dtpAppointmentDate.Text = clsCommon.GETSERVERDATE()
        dtpDOJ.Text = clsCommon.GETSERVERDATE()
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()

        btnsave.Text = "Save"
        btnsave.Enabled = True
    End Sub
    Sub SaveData()
        Try
            If AllowTosave() Then
                Dim obj As New ClsAppointmentLetterHR()
                obj.Applicant_Code = clsCommon.myCstr(txtAppcode.Value)
                obj.Appointment_Date = clsCommon.myCstr(dtpAppointmentDate.Text)

                If (ClsAppointmentLetterHR.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Applicant_Code, NavigatorType.Current)
                        btnsave.Text = "Update"
                    End If
                Else
                    btnsave.Text = "Save"
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Ticket No-TEC/12/08/19-000989
    Private Sub SendSMSandEmail(ByVal isSendForApproval As Boolean)
        ''Try
        'Dim qry As String
        'Dim EmailId As String

        'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmAppointmentLetterHR)

        'If obj Is Nothing Then
        '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '    Return
        'End If
        'If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '    Return
        'End If

        'Dim strContactPerson As String = ""
        'Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
        'strSubject = strSubject.Replace(clsEmailSMSConstants.Appointment_Date, clsCommon.GetPrintDate(dtpAppointmentDate.Text, "dd/MMM/yyyy"))
        ''Dim str As String = UcRequisitionDetail1.AppName
        'Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
        'strbody = strbody.Replace(clsEmailSMSConstants.Offer_Date, clsCommon.GetPrintDate(dtpAppointmentDate.Text, "dd/MMM/yyyy"))
        'strbody = strbody.Replace(clsEmailSMSConstants.DOJ, clsCommon.GetPrintDate(dtpDOJ.Text, "dd/MMM/yyyy"))
        'strbody = strbody.Replace(clsEmailSMSConstants.Salary, txtFixedCTC.Value)
        'strbody = strbody.Replace(clsEmailSMSConstants.ApplicantName, UcRequisitionDetail1.AppName)
        'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)
        'If isSendForApproval Then
        '    Dim lstReceiptents As New List(Of String)
        '    qry = "Select ISNULL(Email,'') As Email From TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        '    EmailId = clsDBFuncationality.getSingleValue(qry)
        '    lstReceiptents.Add(EmailId)
        '    clsMailViaOutlook.SendEmail(strSubject, strbody, lstReceiptents, Nothing, "")
        'End If

        'sanjay

        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmAppointmentLetterHR + "'", Nothing)
        Dim objEmailH As New clsEMailHead()
        objEmailH.arrEMail = New List(Of String)()

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))

                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.App_No, txtAppcode.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Appointment_Date, clsCommon.GetPrintDate(dtpAppointmentDate.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DOJ, clsCommon.GetPrintDate(dtpDOJ.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Salary, txtFixedCTC.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ApplicantName, UcRequisitionDetail1.AppName)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                Dim qry As String = String.Empty
                Dim emailId As String = String.Empty
                qry = "Select ISNULL(Email,'') As Email From TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE ='" + txtAppcode.Value + "' "
                emailId = clsDBFuncationality.getSingleValue(qry)
                objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                objEmailH.SaveData(clsUserMgtCode.frmAppointmentLetterHR, objEmailH, Nothing)
                objEmailH = Nothing

                clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "First do email setting", Me.Text)
        End If
        'sanjay

    End Sub
    Sub MailSend()
        Try
            If clsCommon.myLen(txtAppcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Applicant Code First", Me.Text)
                txtAppcode.Focus()
                txtAppcode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS Of Respective Applicant Code " + txtAppcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtAppcode.Value, NavigatorType.Current)
            'Dim lstUsers As New List(Of String)
            'lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub funPrint()
        Try
            If clsCommon.myLen(txtAppcode.Value) > 0 Then
                Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmAppointmentLetterHR)
                If obj IsNot Nothing Then

                    Dim strContactPerson As String = ""
                    Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
                    strSubject = strSubject.Replace(clsEmailSMSConstants.Appointment_Date, clsCommon.GetPrintDate(dtpAppointmentDate.Text, "dd/MMM/yyyy"))
                    Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
                    strbody = strbody.Replace(clsEmailSMSConstants.Appointment_Date, clsCommon.GetPrintDate(dtpAppointmentDate.Text, "dd/MMM/yyyy"))
                    strbody = strbody.Replace(clsEmailSMSConstants.DOJ, clsCommon.GetPrintDate(dtpDOJ.Text, "dd/MMM/yyyy"))
                    strbody = strbody.Replace(clsEmailSMSConstants.Salary, txtFixedCTC.Value)
                    strbody = strbody.Replace(clsEmailSMSConstants.ApplicantName, UcRequisitionDetail1.AppName)

                    Dim qry As String = "SELECT '" & strSubject & "' AS  EMail_Subject, '" & strbody & "' AS EMail_Text"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.HumanResource, dt, "rptAppointmentLetter", "Appointment Letter")
                Else
                    Throw New Exception("Please check email is not set")
                End If
            Else
                Throw New Exception("Code not found to print")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub frmAppointmentLetterHR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.M AndAlso btnsendmail.Enabled Then
            MailSend()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmAppointmentLetterHR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P  for Print ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnsendmail, "Press Alt+M To Send Mail")
        rmEmail.Visibility = ElementVisibility.Collapsed
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub txtAppcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAppcode._MYNavigator
        Try
            ' LoadData(txtAppcode.Value, NavType)
            Dim obj As New clsJoiningCheckListHead()

            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtAppcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAppcode.MyReadOnly = False
            Else
                txtAppcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtAppcode.Value)
            obj = clsJoiningCheckListHead.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ApplicantCode) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_APPOINTMENT_LETTER WHERE APPLICANT_CODE='" + obj.ApplicantCode + "'"))
                txtAppcode.Value = clsCommon.myCstr(obj.ApplicantCode)
                UcRequisitionDetail1.AppCode = obj.ApplicantCode
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtAppcode.Value, NavType)
                Else
                    isNewEntry = True
                    txtAppcode.MyReadOnly = False
                    txtAppcode.Focus()
                    Dim FixedSal As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
                    If FixedSal > 0 Then
                        txtFixedCTC.Value = FixedSal
                    End If
                    dtpAppointmentDate.Value = clsCommon.GETSERVERDATE()
                    dtpDOJ.Value = clsCommon.GETSERVERDATE()
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtAppcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAppcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_APPOINTMENT_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtAppcode.MyReadOnly = False
        'Else
        '    txtAppcode.MyReadOnly = True
        'End If

        'If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "Select TSPL_HR_JOINING_HEAD.APPLICANT_CODE As Code,First_Name +' ' + Middle_Name + ' ' + Last_Name As Name ,TELEPHONE_NO As [Phone No],Email,Pan_No ,Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As[Applicant Address] From TSPL_HR_JOINING_HEAD inner join  TSPL_HR_APPLICANT_ENTRY  on TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_JOINING_HEAD.Applicant_Code "
        '    txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", " TSPL_HR_JOINING_HEAD.Posted = 1 ", txtAppcode.Value, "", isButtonClicked)
        '    If clsCommon.myLen(txtAppcode.Value) > 0 Then
        '        UcRequisitionDetail1.AppCode = txtAppcode.Value
        '        UcRequisitionDetail1.RefreshData()
        '        Dim FixedSal As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
        '        If FixedSal > 0 Then
        '            txtFixedCTC.Value = FixedSal
        '        End If
        '        Dim DateOfJoining As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Date_Of_Joining  From TSPL_HR_OFFER_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
        '        If clsCommon.myLen(DateOfJoining) > 0 Then
        '            dtpDOJ.Text = DateOfJoining
        '        End If
        '        LoadData(txtAppcode.Value, NavigatorType.Current)

        '    Else
        '        UcRequisitionDetail1.AppCode = ""
        '        UcRequisitionDetail1.RefreshData()
        '        funReset()
        '    End If
        'Else
        '    UcRequisitionDetail1.AppCode = ""
        '    UcRequisitionDetail1.RefreshData()
        '    funReset()
        'End If
        'Dim qry As String = "Select TSPL_HR_JOINING_HEAD.APPLICANT_CODE As Code,First_Name +' ' + Middle_Name + ' ' + Last_Name As Name ,TELEPHONE_NO As [Phone No],Email,Pan_No ,Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As[Applicant Address] From TSPL_HR_JOINING_HEAD inner join  TSPL_HR_APPLICANT_ENTRY  on TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_JOINING_HEAD.Applicant_Code "
        'txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", " TSPL_HR_JOINING_HEAD.Posted = 1 ", txtAppcode.Value, "", isButtonClicked)
        txtAppcode.Value = clsJoiningCheckListHead.GetFinder(" ", txtAppcode.Value, isButtonClicked)
        If clsCommon.myLen(txtAppcode.Value) > 0 Then
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            Dim FixedSal As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If
            Dim DateOfJoining As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Date_Of_Joining  From TSPL_HR_OFFER_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If clsCommon.myLen(DateOfJoining) > 0 Then
                dtpDOJ.Text = DateOfJoining
            End If
            LoadData(txtAppcode.Value, NavigatorType.Current)

        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Private Sub btnsendmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsendmail.Click
        MailSend()
    End Sub

    Private Sub rmEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmEmail.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmAppointmentLetterHR
        frm.ShowDialog()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
#End Region
End Class
