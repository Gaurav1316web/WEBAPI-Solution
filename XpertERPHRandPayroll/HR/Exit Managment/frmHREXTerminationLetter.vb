Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls

'===============Created By Preeti Gupta==Ticket No[BM00000006306] 24/04/2015===============
Public Class FrmHREXTerminationLetter

    Inherits FrmMainTranScreen
    Dim isnewentry As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim atchqry As String = ""
    Dim dt As DataTable

    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmTerminationLetter)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        BtnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Warning")
        dt.Rows.Add("Termination")

        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Code"
    End Sub
    Function allowtosave()


        If clsCommon.myLen(clsCommon.myCstr(Txtemployeetype.Value)) <= 0 Then
            myMessages.blankValue("Employee ")
            Txtemployeetype.Focus()
            Txtemployeetype.Select()
            Errorcontrol.SetError(Txtemployeetype, "Employee ")
            Return False
        Else
            Errorcontrol.ResetError(Txtemployeetype)
        End If


        If clsCommon.myLen(clsCommon.myCstr(txtResonOfResignation.Rtf)) <= 0 Then
            myMessages.blankValue("Reson Of Termination ")
            txtResonOfResignation.Focus()
            txtResonOfResignation.Select()
            Errorcontrol.SetError(txtResonOfResignation, "Reson Of Termination ")
            Return False
        Else
            Errorcontrol.ResetError(txtResonOfResignation)
        End If


        Return True
    End Function

    Sub savedata()
        Try
            If (allowtosave()) Then
                'cbgQual.Update()
                BtnSave.Focus()
                ' Dim obj As clsResignationLetter

                Dim entry As String
                Dim count As Integer = 0
                Dim i As Integer = 0
                Dim qry As String = "select count(*) from Tspl_HR_EM_Termination_Letter  where Doc_code ='" + txtcode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isnewentry = True
                Else
                    isnewentry = False

                End If


                Dim Termin As New clsHREXTerminationLetter
                Termin.DOC_CODE = clsCommon.myCstr(txtcode.Value)
                Termin.DOC_DATE = txtDate.Text
                Termin.EmpolyeeCode = clsCommon.myCstr(Txtemployeetype.Value)
                Termin.TerminationType = clsCommon.myCstr(ddlType.Text)
                Termin.DepartmentCode = clsCommon.myCstr(txtDepartmentCode.Text)
                Termin.ResonOfTermination = clsCommon.myCstr(txtResonOfResignation.Rtf)
                Termin.ResonOfTermination_DATE = txtTerminationDate.Text
                Termin.Remarks = clsCommon.myCstr(txtremarks.Text)


                If clsHREXTerminationLetter.savedata(Termin, isnewentry) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully")
                    entry = Termin.DOC_CODE
                    getdata(Termin.DOC_CODE, NavigatorType.Current)
                    BtnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    BtnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub

    Sub getdata(ByVal entry As String, ByVal navigatortype As NavigatorType)
        Try
            Dim obj As clsHREXTerminationLetter = clsHREXTerminationLetter.getdata(entry, navigatortype)
            If obj IsNot Nothing Then

                txtcode.Value = obj.DOC_CODE
                txtDate.Text = obj.DOC_DATE
                Txtemployeetype.Value = obj.EmpolyeeCode
                ddlType.Text = obj.TerminationType
                txtEmployeeName.Text = obj.EmpolyeeName
                txtDepartmentCode.Text = obj.DepartmentCode
                txtDepartmentName.Text = obj.DepartmentName
                txtResonOfResignation.Rtf = obj.ResonOfTermination
                txtTerminationDate.Value = obj.ResonOfTermination_DATE
                txtremarks.Text = obj.Remarks
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsHREXTerminationLetter.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    resetdata()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub resetdata()
        txtcode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE()
        Txtemployeetype.Value = ""
        txtEmployeeName.Text = ""
        txtDepartmentCode.Text = ""
        txtDepartmentName.Text = ""
        txtResonOfResignation.Text = ""
        txtTerminationDate.Text = clsCommon.GETSERVERDATE()
        txtremarks.Text = ""
        LoadTypes()
    End Sub


    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        getdata(txtcode.Value, NavType)
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        txtcode.Value = clsHREXTerminationLetter.getFinder("", txtcode.Value, isButtonClicked)
        If clsCommon.myLen(txtcode.Value) > 0 Then
            getdata(txtcode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub Txtemployeetype__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles Txtemployeetype._MYValidating
        Dim qry As String

        qry = "select  TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name  as [Employee Name],TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE as [Department Code] ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME [Department Name]  from TSPL_EMPLOYEE_MASTER left outer join TSPL_DEPARTMENT_MASTER on  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE "
        Txtemployeetype.Value = clsCommon.ShowSelectForm("Resig", qry, "Employee Code", "", Txtemployeetype.Value, "", isButtonClicked)

        If clsCommon.myLen(Txtemployeetype.Value) > 0 Then
            txtEmployeeName.Text = clsDBFuncationality.getSingleValue("select TSPL_EMPLOYEE_MASTER.Emp_Name  as [Employee Name]  from TSPL_EMPLOYEE_MASTER left outer join TSPL_DEPARTMENT_MASTER on  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" + Txtemployeetype.Value + "' ")
            txtDepartmentCode.Text = clsDBFuncationality.getSingleValue("select TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE as [Department Code]  from TSPL_EMPLOYEE_MASTER left outer join TSPL_DEPARTMENT_MASTER on  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" + Txtemployeetype.Value + "' ")
            txtDepartmentName.Text = clsDBFuncationality.getSingleValue("select TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME [Department Name]  from TSPL_EMPLOYEE_MASTER left outer join TSPL_DEPARTMENT_MASTER on  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" + Txtemployeetype.Value + "' ")
        Else
            txtEmployeeName.Text = ""
            txtDepartmentCode.Text = ""
            txtDepartmentName.Text = ""
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        resetdata()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        savedata()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    'Sub MailSend()
    '    Try
    '        Dim repotype As String = ""
    '        Dim invtype As String = ""


    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmTerminationLetter)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Try

    '            Dim strEmail As String = ""


    '            If Process.GetProcessesByName("OutLook").Length < 1 Then
    '                'restarts the Process
    '                Process.Start("OutLook.exe")
    '            End If
    '            Dim oApp As New Outlook.Application()
    '            Dim oMsg As Outlook.MailItem



    '            oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '            strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

    '            Try
    '                If strEmail.Substring(0, 1) = "," Then
    '                    strEmail = strEmail.Substring(1, strEmail.Length - 1)
    '                End If
    '            Catch ex As Exception
    '            End Try

    '            If clsCommon.myLen(strEmail) <= 0 Then
    '                clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
    '                Return
    '            End If

    '            oMsg.Body = obj.mailbody

    '            oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

    '            If oMsg.Body.Contains(clsEmailSMSConstants.Doc_Code) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.DOC_Date) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.EmpCode) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.myCstr(Txtemployeetype.Value))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.EmpName) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(txtEmployeeName.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.DepCode) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.DepName) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.ResignationDate) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(txtTerminationDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.Remarks) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            End If






    '            If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If


    '            oMsg.Subject = obj.mailsubjct

    '            oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '            If oMsg.Body.Contains(clsEmailSMSConstants.Doc_Code) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.DOC_Date) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.EmpCode) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(Txtemployeetype.Value))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.EmpName) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpName, clsCommon.myCstr(txtEmployeeName.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.DepCode) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.DepName) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.ResignationDate) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResignationDate, clsCommon.GetPrintDate(txtTerminationDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.Remarks) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            End If


    '            If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If
    '            '------------------------code for attchament-------------------------------------
    '            If obj.atchmnt = "Y" Then
    '                Dim sDisplayname As [String] = "MyAttachment"
    '                If oMsg.Body Is Nothing Then
    '                    oMsg.Body = " "
    '                End If
    '                Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '                Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '                Dim strRptPath As String = ""

    '                Dim oAttachment As Outlook.Attachment = Nothing
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

    '                If dt.Rows.Count > 0 Then
    '                    Dim subPath As String = Application.StartupPath + "\Mail Reports"

    '                    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

    '                    If (IsExists = False) Then

    '                        System.IO.Directory.CreateDirectory(subPath)
    '                    End If
    '                    strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
    '                    'transportSql.exportdata(Gv1, strRptPath, "Sheet1")
    '                    oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
    '                End If
    '            End If
    '            '---------------------------------------------------------------------------


    '            oMsg.Recipients.Add(strEmail)

    '            oMsg.Send()
    '            oMsg = Nothing
    '            oApp = Nothing

    '            clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try

    '        Try
    '            Dim client As New System.Net.WebClient()

    '            If clsCommon.myLen(obj.smsbody) <= 0 Then
    '                Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
    '            End If

    '            Dim strMes As String = ""

    '            strMes = obj.smsbody
    '            strMes = strMes.Replace("'", " ").Replace("`", "/")

    '            If strMes.Contains(clsEmailSMSConstants.Doc_Code) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.DOC_Date) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.EmpCode) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(Txtemployeetype.Value))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.EmpName) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.EmpName, clsCommon.myCstr(txtEmployeeName.Text))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.DepCode) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.DepName) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.ResignationDate) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.ResignationDate, clsCommon.GetPrintDate(txtTerminationDate.Value, "dd/MMM/yyyy"))
    '            End If

    '            If strMes.Contains(clsEmailSMSConstants.Remarks) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            End If


    '            If strMes.Contains(clsEmailSMSConstants.ReportType) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If


    '            Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

    '            Try
    '                If strphone.Substring(0, 1) = "," Then
    '                    strphone = strphone.Substring(1, strphone.Length - 1)
    '                End If
    '            Catch ex As Exception
    '            End Try

    '            'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '            'Dim data As Stream = client.OpenRead(baseurl)
    '            'Dim reader As StreamReader = New StreamReader(data)
    '            'Dim s As String = reader.ReadToEnd()
    '            'data.Close()
    '            'reader.Close()

    '            Dim UserId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
    '            Dim Paswd As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
    '            Dim SenderId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
    '            Dim SMS_Provider As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))

    '            If clsCommon.CompairString(SMS_Provider, "Bulk SMS") = CompairStringResult.Equal Then
    '                '================send sms through PerfectBulkSMS====================
    '                Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
    '                Dim str As String = "http://www.perfectbulksms.in/Sendsmsapi.aspx?USERID=" + UserId + "&PASSWORD=" + Paswd + "&SENDERID=" + SenderId + "&TO=" & strphone & "&MESSAGE=" & strMes & ""
    '                Dim wrquest As HttpWebRequest = WebRequest.Create(str)
    '                Dim getresponse As HttpWebResponse = Nothing
    '                getresponse = wrquest.GetResponse()

    '                Dim objStream As Stream = getresponse.GetResponseStream()
    '                Dim objSR As StreamReader = New StreamReader(objStream, encode, True)
    '                Dim strResponse As String = objSR.ReadToEnd()
    '                'clsCommon.MyMessageBoxShow(getresponse.StatusDescription)

    '                objSR.Close()
    '                objStream.Close()
    '                getresponse.Close()
    '                '===========================================================
    '            ElseIf clsCommon.CompairString(SMS_Provider, "BSWS") = CompairStringResult.Equal Then
    '                Dim consumeWebService As BSWS.BSWS
    '                consumeWebService = New BSWS.BSWS
    '                Dim xmlResult As XmlElement
    '                xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
    '            End If

    '            clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub

    Private Sub rbtnSetting_Click(sender As Object, e As EventArgs) Handles rbtnSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmResignationLetter
        frm.ShowDialog()
    End Sub

    Private Sub rbtnSend_Click(sender As Object, e As EventArgs) Handles rbtnSend.Click
        'MailSend()
    End Sub
    Public Function funPrint(ByVal strDocNo As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim Qry As String = "select TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_add3 ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as copm_add1 ,TSPL_COMPANY_MASTER.Add2 as comp_add2,TSPL_COMPANY_MASTER.Add3 as comp_add3, Tspl_HR_EM_Termination_Letter.Doc_code as Code,convert(varchar,Tspl_HR_EM_Termination_Letter.Doc_Date,103) as [Document Date],Tspl_HR_EM_Termination_Letter.EMP_CODE  as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],Tspl_HR_EM_Termination_Letter.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],Tspl_HR_EM_Termination_Letter.Reson_Of_termination  as [Reson Of Resignation],convert(varchar,Tspl_HR_EM_Termination_Letter.Termination_Date,103)  as [Resignation Date],Tspl_HR_EM_Termination_Letter.Remarks from Tspl_HR_EM_Termination_Letter   left outer join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER .EMP_CODE =Tspl_HR_EM_Termination_Letter.EMP_CODE  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Termination_Letter.DEPARTMENT_CODE"
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =Tspl_HR_EM_Termination_Letter.Comp_Code "
            Qry += " LEFT outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE "
            Qry += " where 2=2 and  Tspl_HR_EM_Termination_Letter.Doc_code = '" + strDocNo + "' "
            dt = clsDBFuncationality.GetDataTable(Qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("No data found to print.")
        Else
            Dim dt As DataTable = funPrint(txtcode.Value)

            If dt.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HumanResource, dt, "rptHREXTetminationLetter", "Termination Letter")

            Else
                clsCommon.MyMessageBoxShow("No data found.")
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmHREXTerminationLetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(BtnSave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        txtDate.Text = clsCommon.GETSERVERDATE()
        txtTerminationDate.Text = clsCommon.GETSERVERDATE()

        resetdata()
        If clsCommon.myLen(Me.Tag) > 0 Then
            getdata(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub FrmHREXTerminationLetter_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso BtnSave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
End Class
