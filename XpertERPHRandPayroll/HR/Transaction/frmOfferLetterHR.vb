' ----------------- Created By Anubhooti On 20-Aug-2014 Against BM00000003528-------------------- '
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

Public Class FrmOfferLetterHR
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmOfferLetterHR)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub

    Function AllowTosave() As Boolean
        Dim Joining_Date As DateTime
        Dim Offer_Date As DateTime

        Joining_Date = clsCommon.myCDate(dtpDOJ.Value)
        Offer_Date = clsCommon.myCDate(dtpaOffrDate.Value)

        If clsCommon.myLen(txtAppcode.Value) <= 0 Then
            txtAppcode.Focus()
            Throw New Exception("Applicant Code cannot be left blank")
        End If
        If Offer_Date >= Joining_Date Then
            dtpDOJ.Focus()
            Throw New Exception("Please check ! date of joining must be greater than from offer date")
        End If
        If btnsave.Text = "Update" Then
            If common.clsCommon.MyMessageBoxShow("Do you want to update this entry (" + clsCommon.myCstr(txtAppcode.Value) + ")", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Else
                Return False
            End If
        End If
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim FixedSal As Double = 0

        txtAppcode.MyReadOnly = True
        'btnsave.Enabled = True
        'btnDelete.Enabled = True
        'btnpost.Enabled = True
        isNewEntry = False

        Dim obj As ClsOfferLetterHR = ClsOfferLetterHR.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnpost.Enabled = True
            'btnDelete.Enabled = True
            'btnpost.Enabled = True

            txtAppcode.Value = obj.Applicant_Code
            dtpaOffrDate.Text = obj.OfferDate
            dtpDOJ.Text = obj.DateOfJoining

            FixedSal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL (Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False

            End If
            UsLock1.Status = obj.Posted
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()

        End If
    End Sub
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim FixedSal As Double = 0
        txtAppcode.MyReadOnly = True
        isNewEntry = False

        Dim obj As ClsOfferLetterHR = ClsOfferLetterHR.GetDataForNav(strCode, NavTyep)
        If obj IsNot Nothing Then

            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btnpost.Enabled = True
            txtAppcode.Value = obj.Applicant_Code
            dtpaOffrDate.Text = obj.OfferDate
            dtpDOJ.Text = obj.DateOfJoining

            FixedSal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL (Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnpost.Enabled = False
            End If
            UsLock1.Status = obj.Posted
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()

        End If
    End Sub
    Sub funReset()
        isNewEntry = True
        txtAppcode.MyReadOnly = False
        txtAppcode.Value = Nothing
        txtAppcode.Focus()

        txtFixedCTC.Value = 0
        dtpaOffrDate.Text = clsCommon.GETSERVERDATE()
        dtpDOJ.Text = clsCommon.GETSERVERDATE()
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
    End Sub
    Sub SaveData()
        Try
            If AllowTosave() Then
                Dim obj As New ClsOfferLetterHR()
                obj.Applicant_Code = clsCommon.myCstr(txtAppcode.Value)
                obj.OfferDate = clsCommon.myCstr(dtpaOffrDate.Text)
                'obj.Fixed_CTC_Rs_Month = clsCommon.myCdbl(txtFixedCTC.Value)
                obj.DateOfJoining = clsCommon.myCstr(dtpDOJ.Text)

                If (ClsOfferLetterHR.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Applicant_Code, NavigatorType.Current)
                        btnsave.Text = "Update"
                        btnpost.Enabled = True
                    End If
                Else
                    btnsave.Text = "Save"
                    btnpost.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub funPrint()
        'Try
        '    If clsCommon.myLen(txtAppcode.Value) > 0 Then

        '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmOfferLetterHR)
        '        If obj IsNot Nothing Then

        '            Dim strContactPerson As String = String.Empty
        '            Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
        '            strSubject = strSubject.Replace(clsEmailSMSConstants.Offer_Date, clsCommon.GetPrintDate(dtpaOffrDate.Text, "dd/MMM/yyyy"))
        '            Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
        '            strbody = strbody.Replace(clsEmailSMSConstants.Offer_Date, clsCommon.GetPrintDate(dtpaOffrDate.Text, "dd/MMM/yyyy"))
        '            strbody = strbody.Replace(clsEmailSMSConstants.DOJ, clsCommon.GetPrintDate(dtpDOJ.Text, "dd/MMM/yyyy"))
        '            strbody = strbody.Replace(clsEmailSMSConstants.Salary, txtFixedCTC.Value)
        '            strbody = strbody.Replace(clsEmailSMSConstants.ApplicantName, UcRequisitionDetail1.AppName)

        '            Dim qry As String = "SELECT '" & strSubject & "' AS  EMail_Subject, '" & strbody & "' AS EMail_Text"

        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            FrmHRViewer.funreport(dt, "rptOfferLetter", "Offer Letter")
        '        Else
        '            Throw New Exception("Please check ! email setting is not set")
        '        End If
        '    Else
        '        Throw New Exception("Code not found to print")
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Try
            Dim Qry As String = "select TSPL_HR_OFFER_LETTER.APPLICANT_CODE as Emp_Code ,TSPL_HR_APPLICANT_ENTRY.First_Name ,TSPL_HR_APPLICANT_ENTRY.Middle_Name ,TSPL_HR_APPLICANT_ENTRY.Last_Name,"

            Qry += " (TSPL_HR_APPLICANT_ENTRY.First_Name +' '+ TSPL_HR_APPLICANT_ENTRY.Middle_Name+'' +TSPL_HR_APPLICANT_ENTRY.Last_Name) as Name,"

            Qry += " TSPL_HR_JOB_TITLE.Job_Title_Code  ,TSPL_HR_JOB_TITLE.Job_Title ,TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1 as Loc_ADd1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_add3  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Comp_Code ,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,TSPL_COMPANY_MASTER.Add1 as Comp_Add1 ,TSPL_COMPANY_MASTER.Add2  as Comp_Add2 ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3 ,TSPL_HR_SALARY_FITMENT.CTC_Rs_Month ,convert(varchar,TSPL_HR_OFFER_LETTER.Date_Of_Joining,103) as Date_Of_Joining   from TSPL_HR_OFFER_LETTER "
            Qry += " left outer join TSPL_HR_APPLICANT_ENTRY on TSPL_HR_OFFER_LETTER.APPLICANT_CODE =TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE "

            Qry += " left outer join tspl_hr_requisition on tspl_hr_requisition.Requisition_Code  =TSPL_HR_APPLICANT_ENTRY.Requisition_Code "
            Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =tspl_hr_requisition.DESIGNATION_ID"
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_HR_APPLICANT_ENTRY.Comp_Code "
            Qry += " left outer join TSPL_HR_SALARY_FITMENT on TSPL_HR_SALARY_FITMENT.APPLICANT_CODE =TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE "
            Qry += " left outer join TSPL_HR_JOB_TITLE on TSPL_HR_JOB_TITLE.Job_Title_Code =tspl_hr_requisition.Job_Title_Code"
            Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_hr_requisition.Location_Code "
            Qry += " where 2=2 and  TSPL_HR_OFFER_LETTER.APPLICANT_CODE = '" + txtAppcode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HumanResource, dt, "rptMultipleOfferLetter", "Offer Letter")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub PostData()
        Try
            Dim msg As String = String.Empty
            Dim qry As String = String.Empty
            Dim dt As DataTable = Nothing
            Dim Applicant_Code As Double = 0
            isFlag = True
            If clsCommon.myLen(txtAppcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Applicant_Code) As Applicant_Code  from TSPL_HR_OFFER_LETTER where Applicant_Code='" + txtAppcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (ClsOfferLetterHR.PostData(MyBase.Form_ID, txtAppcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(txtAppcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering applicant code")
                End If

            Else
                Throw New Exception("Applicant code not found to post")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
        End Try
    End Sub
#End Region

#Region "Events"
    Private Sub txtAppcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAppcode._MYNavigator
        Try
            'LoadData(txtAppcode.Value, NavType)
            Dim obj As New ClsSalaryFitment()

            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtAppcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAppcode.MyReadOnly = False
            Else
                txtAppcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtAppcode.Value)
            obj = ClsSalaryFitment.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_OFFER_LETTER WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'"))
                txtAppcode.Value = clsCommon.myCstr(obj.Applicant_Code)
                UcRequisitionDetail1.AppCode = obj.Applicant_Code
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
                    dtpaOffrDate.Text = clsCommon.GETSERVERDATE()
                    dtpDOJ.Text = clsCommon.GETSERVERDATE()
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                    btnpost.Enabled = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtAppcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtAppcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_OFFER_LETTER where APPLICANT_CODE ='" + txtAppcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtAppcode.MyReadOnly = False
        'Else
        '    txtAppcode.MyReadOnly = True
        'End If

        'If txtAppcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "Select TSPL_HR_SALARY_FITMENT.APPLICANT_CODE As Code,First_Name +' ' + Middle_Name + ' ' + Last_Name As Name ,TELEPHONE_NO As [Phone No],Email,Pan_No ,Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As[Applicant Address] From TSPL_HR_SALARY_FITMENT inner join  TSPL_HR_APPLICANT_ENTRY  on TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_SALARY_FITMENT.Applicant_Code "
        '    txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "TSPL_HR_SALARY_FITMENT.Posted = 1 ", txtAppcode.Value, "", isButtonClicked)
        '    If clsCommon.myLen(txtAppcode.Value) > 0 Then
        '        UcRequisitionDetail1.AppCode = txtAppcode.Value
        '        UcRequisitionDetail1.RefreshData()
        '        Dim FixedSal As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
        '        If FixedSal > 0 Then
        '            txtFixedCTC.Value = FixedSal
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
        'Dim qry As String = "Select TSPL_HR_SALARY_FITMENT.APPLICANT_CODE As Code,First_Name +' ' + Middle_Name + ' ' + Last_Name As Name ,TELEPHONE_NO As [Phone No],Email,Pan_No ,Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As[Applicant Address] From TSPL_HR_SALARY_FITMENT inner join  TSPL_HR_APPLICANT_ENTRY  on TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_SALARY_FITMENT.Applicant_Code "
        'txtAppcode.Value = clsCommon.ShowSelectForm("App_Code", qry, "Code", "TSPL_HR_SALARY_FITMENT.Posted = 1 ", txtAppcode.Value, "", isButtonClicked)
        txtAppcode.Value = ClsSalaryFitment.GetFinder(" ", txtAppcode.Value, isButtonClicked)
        If clsCommon.myLen(txtAppcode.Value) > 0 Then
            UcRequisitionDetail1.AppCode = txtAppcode.Value
            UcRequisitionDetail1.RefreshData()
            Dim FixedSal As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Fixed_CTC_Rs_Month,0) As Fixed_CTC_Rs_Month  From TSPL_HR_SALARY_FITMENT where APPLICANT_CODE ='" + txtAppcode.Value + "'"))
            If FixedSal > 0 Then
                txtFixedCTC.Value = FixedSal
            End If
            LoadData(txtAppcode.Value, NavigatorType.Current)

        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Private Sub FrmOfferLetterHR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.M AndAlso btnsendmail.Enabled Then
            MailSend()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmOfferLetterHR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub rmEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmEmail.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmOfferLetterHR
        frm.ShowDialog()
    End Sub
#End Region
#Region "Email-Setting"
    Private Sub SendSMSandEmail(ByVal isSendForApproval As Boolean)
        'Ticket No-TEC/12/08/19-000989
        'sanjay

        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmOfferLetterHR + "'", Nothing)
        Dim objEmailH As New clsEMailHead()
        objEmailH.arrEMail = New List(Of String)()

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
           
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.App_No, txtAppcode.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Offer_Date, clsCommon.GetPrintDate(dtpaOffrDate.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DOJ, clsCommon.GetPrintDate(dtpDOJ.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Salary, txtFixedCTC.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ApplicantName, UcRequisitionDetail1.AppName)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                Dim qry As String = String.Empty
                Dim emailId As String = String.Empty
                qry = "Select ISNULL(Email,'') As Email From TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE ='" + txtAppcode.Value + "' "
                emailId = clsDBFuncationality.getSingleValue(qry)
                objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                objEmailH.SaveData(clsUserMgtCode.frmOfferLetterHR, objEmailH, Nothing)
                objEmailH = Nothing

                clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
            End If

        Else
            clsCommon.MyMessageBoxShow("First do email setting", Me.Text)
        End If
        'sanjay

        ''Try
        'Dim qry As String
        'Dim EmailId As String

        'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmOfferLetterHR)

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
        'strSubject = strSubject.Replace(clsEmailSMSConstants.Offer_Date, clsCommon.GetPrintDate(dtpaOffrDate.Text, "dd/MMM/yyyy"))
        ''Dim str As String = UcRequisitionDetail1.AppName
        'Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.App_No, txtAppcode.Value)
        'strbody = strbody.Replace(clsEmailSMSConstants.Offer_Date, clsCommon.GetPrintDate(dtpaOffrDate.Text, "dd/MMM/yyyy"))
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

        '------------------------code for attchament-------------------------------------
        'Dim strRptPath As String = ""
        'If obj.atchmnt = "Y" Then
        '    atchqry = GetMailPrintPreview(txtDocNo.Value)
        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '    If dt1.Rows.Count > 0 Then
        '        If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
        '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "WO-G", "Work Order")
        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
        '            strRptPath = PurchaseOrderViewer.funreport1(dt1, "PO-G", "Purchase Order")
        '        Else
        '            Throw New Exception("Not a valid Po Type")
        '            Return
        '        End If

        '    End If
        'End If
        '---------------------------------------------------------------------------

        '    Dim lstReceiptents As New List(Of String)
        '    For Each strUser As String In lstUsers
        '        'lstUsers.Add(dr("User_Code").ToString())
        '        Dim qry As String = ""
        '        Dim emailId As String = ""
        '        If isSendForApproval Then
        '            strContactPerson = strUser
        '            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
        '            emailId = clsDBFuncationality.getSingleValue(qry)
        '        Else
        '            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
        '            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
        '        End If

        '        strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
        '        lstReceiptents.Add(emailId)

        '        Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

        '        clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
        '    Next
        '    clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub
    Sub MailSend()
        Try
            If clsCommon.myLen(txtAppcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Applicant Code First", Me.Text)
                txtAppcode.Focus()
                txtAppcode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Applicant Code " + txtAppcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtAppcode.Value, NavigatorType.Current)
            'Dim lstUsers As New List(Of String)
            'lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(True)
            'clsCommon.MyMessageBoxShow("Mail send succussfully")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub btnsendmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsendmail.Click
        MailSend()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtAppcode.Value) <= 0 Then
            myMessages.blankValue("Offer Letter not found to Print")
        Else
            funPrint()
        End If
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        PostData()
    End Sub
End Class
