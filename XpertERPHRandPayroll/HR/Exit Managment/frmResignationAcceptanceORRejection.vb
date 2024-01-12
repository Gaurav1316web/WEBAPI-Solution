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
Imports Telerik.WinControls.UI

'===============Created by preeti gupta======================
Public Class FrmResignationAcceptanceORRejection

    Inherits FrmMainTranScreen

#Region "Variables"
    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
    End Enum
    Dim rpt_Name As String = "Leave Approval"
#End Region
    Dim strStatus As String
    Dim atchqry As String = ""
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmResignationAcceptanceOrRejection)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub LoadStatus()
        Dim dtStatus As New DataTable
        dtStatus.Columns.Add("Status", GetType(String))
        dtStatus.Rows.Add("--Select--")
        dtStatus.Rows.Add("Approved")
        'dtStatus.Rows.Add("UnApproved")
        dtStatus.Rows.Add("Pending")
        dtStatus.Rows.Add("Reject")
        ddlStatus.DataSource = dtStatus
        ddlStatus.ValueMember = "Status"
        ddlStatus.DisplayMember = "Status"
    End Sub

    'Sub MailSend()
    '    Try
    '        Dim repotype As String = ""
    '        Dim invtype As String = ""


    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmResignationLetter)

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

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.Doc_Code) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            'End If
    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DOC_Date) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.EmpCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.myCstr(Txtemployeetype.Value))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.EmpName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(txtEmployeeName.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DepCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DepName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.ResignationDate) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(txtReginationDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.Remarks) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.HandoverCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.HandoverCode, clsCommon.myCstr(txtResponsibilityCode.Value))
    '            'End If


    '            'If oMsg.Body.Contains(clsEmailSMSConstants.HandoverName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.HandoverName, clsCommon.myCstr(txtResponsibilityName.Text))
    '            'End If

    '            If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If


    '            oMsg.Subject = obj.mailsubjct

    '            oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.Doc_Code) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            'End If
    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DOC_Date) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.EmpCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(Txtemployeetype.Value))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.EmpName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.EmpName, clsCommon.myCstr(txtEmployeeName.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DepCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.DepName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.ResignationDate) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ResignationDate, clsCommon.GetPrintDate(txtReginationDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.Remarks) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            'End If

    '            'If oMsg.Body.Contains(clsEmailSMSConstants.HandoverCode) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.HandoverCode, clsCommon.myCstr(txtResponsibilityCode.Value))
    '            'End If


    '            'If oMsg.Body.Contains(clsEmailSMSConstants.HandoverName) Then
    '            '    oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.HandoverName, clsCommon.myCstr(txtResponsibilityName.Text))
    '            'End If

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

    '            'If strMes.Contains(clsEmailSMSConstants.Doc_Code) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.Doc_Code, clsCommon.myCstr(txtcode.Value))
    '            'End If
    '            'If strMes.Contains(clsEmailSMSConstants.DOC_Date) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.EmpCode) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.EmpCode, clsCommon.myCstr(Txtemployeetype.Value))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.EmpName) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.EmpName, clsCommon.myCstr(txtEmployeeName.Text))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.DepCode) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.DepCode, clsCommon.myCstr(txtDepartmentCode.Text))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.DepName) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.DepName, clsCommon.myCstr(txtDepartmentName.Text))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.ResonOfResignation) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.ResonOfResignation, clsCommon.myCstr(txtResonOfResignation.Text))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.ResignationDate) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.ResignationDate, clsCommon.GetPrintDate(txtReginationDate.Value, "dd/MMM/yyyy"))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.Remarks) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtremarks.Text))
    '            'End If

    '            'If strMes.Contains(clsEmailSMSConstants.HandoverCode) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.Remarks, clsCommon.myCstr(txtResponsibilityCode.Value))
    '            'End If


    '            'If strMes.Contains(clsEmailSMSConstants.HandoverName) Then
    '            '    strMes = strMes.Replace(clsEmailSMSConstants.HandoverName, clsCommon.myCstr(txtResponsibilityName.Text))
    '            'End If

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

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try
            Dim rpt_Head As String = "Leave Approval"
            'If Not clsCommon.CompairString(ddlStatus.SelectedValue, "") = CompairStringResult.Equal Then
            '    rpt_Head = ""
            '    rpt_Head = "List Of " + ddlStatus.SelectedValue + " Tickets "
            'End If
            rpt_Name = rpt_Head
            If clsCommon.myLen(ddlStatus.SelectedValue) > 0 Then
                rpt_Name = ddlStatus.SelectedValue + "_"
            End If
            gv1.Refresh()
            Dim dt As DataTable = gv1.DataSource
            If dt.Rows.Count > 0 Then
                If dt.Select("[select] = True").Count > 0 Then
                    'Dim dt_tmp As DataTable = dt.Select("[select] = True").CopyToDataTable()
                    Dim dt_tmp As DataTable = dt.Clone()
                    'For Each DR As DataRow In dt.Rows
                    '    'If clsCommon.myCBool(DR("select")) Then
                    '    '    Dim DR_TEMP = dt_tmp.NewRow()
                    '    '    DR_TEMP(0) = DR(0)
                    '    '    DR_TEMP(1) = DR(1)
                    '    '    DR_TEMP(2) = DR(2)
                    '    '    DR_TEMP(3) = DR(3)
                    '    '    DR_TEMP(4) = DR(4)
                    '    '    DR_TEMP(5) = DR(5)
                    '    '    DR_TEMP(6) = DR(6)
                    '    '    DR_TEMP(7) = DR(7)
                    '    '    DR_TEMP(8) = DR(8)
                    '    '    'DR_TEMP(9) = DR(9)
                    '    '    dt_tmp.Rows.Add(DR_TEMP)
                    '    'End If
                    'Next

                    dt_tmp.Columns.RemoveAt(0)
                    'dt_tmp.Columns.RemoveAt(1)
                    'dt_tmp.Columns.RemoveAt(0)
                    dt_tmp.AcceptChanges()
                    dt_tmp.Columns.Add(New DataColumn("Sr. No.", GetType(Int16)))
                    dt_tmp.Columns("Sr. No.").DataType = GetType(Int16)

                    Dim i As Int16 = 0

                    For Each dr As DataRow In dt_tmp.Rows
                        i += 1
                        dr("Sr. No.") = i
                    Next
                    dt_tmp.Columns("Sr. No.").SetOrdinal(0)
                    dt_tmp.AcceptChanges()

                    gvreport.DataSource = Nothing
                    gvreport.Columns.Clear()
                    gvreport.Rows.Clear()
                    gvreport.DataSource = dt_tmp
                    gvreport.BestFitColumns()

                    gv1.AutoSizeRows = DataGridViewAutoSizeRowsMode.DisplayedCells

                    Dim arrHeader As List(Of String) = New List(Of String)()
                    Dim strtemp As String = ""
                    arrHeader.Add(strtemp)
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid(rpt_Head, gv1, arrHeader, rpt_Name)
                    Else

                        clsCommon.MyExportToPDF(rpt_Head, gv1, arrHeader, rpt_Name, True)
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Row Selected to Export.", Me.Text)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Post()
        Try
            Dim ask As MsgBoxResult
            'Dim LeaveFdate, LeaveTdate, TotalDays As String
            ask = clsCommon.MyMessageBoxShow(Me, "Are you sure to Approve Resignnation???", "Approve", MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question)
            If ask = MsgBoxResult.Yes Then
                Dim counter As Integer = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    If grow.Cells("Select").Value = True Then
                        counter += 1
                        Dim qry As String = "update Tspl_HR_EM_Resignation_Letter set STATUS='Approved' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        Dim qry1 As String = "update Tspl_HR_EM_Resignation_Letter set Approved_By='" + objCommonVar.CurrentUserCode + "' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)
                        Dim qry2 As String = "update Tspl_HR_EM_Resignation_Letter set Approved_DATE='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry2)

                        strStatus = "Approved"
                        'MailSend()
                        'clsCommon.MyMessageBoxShow("Mail send Successfully")
                    End If

                Next
                If counter <= 0 Then
                    Throw New Exception("No Row Selected")
                End If
                clsCommon.MyMessageBoxShow(Me, " Resignation Approve Successfully", Me.Text)
                UsLock1.Status = ERPTransactionStatus.Approved
                btnPost.Enabled = False
            ElseIf ask = MsgBoxResult.No Then
                UsLock1.Status = ERPTransactionStatus.Pending
                btnPost.Enabled = True
                'btnSave.Enabled = True
                'btnDelete.Enabled = True
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click

    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Post()
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Reject()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub Reject()
        Try
            'Dim LeaveFdate, LeaveTdate, TotalDays As String
            Dim ask As MsgBoxResult
            ask = clsCommon.MyMessageBoxShow(Me, "Are you sure to Reject Resoignation???", "Reject", MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question)
            If ask = MsgBoxResult.Yes Then
                Dim counter As Integer = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    If grow.Cells("Select").Value = True Then
                        counter += 1
                        Dim qry As String = "update Tspl_HR_EM_Resignation_Letter set STATUS='Reject' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        Dim qry1 As String = "update Tspl_HR_EM_Resignation_Letter set Approved_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)
                        Dim qry2 As String = "update Tspl_HR_EM_Resignation_Letter set Approved_By='" + objCommonVar.CurrentUserCode + "' where Doc_CODE='" + grow.Cells("Doc Code").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry2)
                        strStatus = "Rejected"
                        'LeaveFdate = grow.Cells("LeaveFromDate").Value
                        'LeaveTdate = grow.Cells("LeaveToDate").Value
                        'TotalDays = grow.Cells("TotalDays").Value
                        'sendEMailThroughOUTLOOK(grow.Cells("Code").Value, strStatus, LeaveFdate, LeaveTdate, TotalDays)
                        'clsCommon.MyMessageBoxShow("Mail send Successfully")
                    End If
                Next
                If counter <= 0 Then
                    Throw New Exception("No Row Selected")
                End If
                clsCommon.MyMessageBoxShow(Me, " Resignation Reject Successfully", Me.Text)
                UsLock1.Status = ERPTransactionStatus.Cancel
                btnReject.Enabled = False

            ElseIf ask = MsgBoxResult.No Then
                UsLock1.Status = ERPTransactionStatus.Pending
                btnPost.Enabled = True

                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FrmResignationAcceptanceORRejection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadStatus()
        ddlStatus.SelectedValue = "--Select--"
        btnExport.Enabled = False
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            'LoadBlankGrid()
            Dim st As String = "Pending"
            Dim qry As String = ""
            qry = ""
            qry += " select cast(0 as bit)  as [Select],Tspl_HR_EM_Resignation_Letter.Doc_code as [Doc Code],convert(varchar,Tspl_HR_EM_Resignation_Letter.Doc_Date,103) as [Doc Date] ,TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],convert(varchar,Tspl_HR_EM_Resignation_Letter.Resignation_Date,103) as [Resignation Date],Tspl_HR_EM_Resignation_Letter.Reson_Of_Resignation  as [Reson of Resignation] ,Tspl_HR_EM_Resignation_Letter.Created_By as [Created By] from Tspl_HR_EM_Resignation_Letter"
            qry += "  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER .EMP_CODE =Tspl_HR_EM_Resignation_Letter.EMP_CODE "
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Resignation_Letter.DEPARTMENT_CODE "
            If Not clsCommon.CompairString(ddlStatus.SelectedValue, "") = CompairStringResult.Equal Then
                qry += " where Tspl_HR_EM_Resignation_Letter.STATUS ='" + st + "' "
            End If
            qry += " and convert(date,Tspl_HR_EM_Resignation_Letter.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_HR_EM_Resignation_Letter.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "
            qry += " and Tspl_HR_EM_Resignation_Letter.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
            'qry += " and Tspl_HR_EM_Resignation_Letter.Created_By in (Select User_Code from USER_MASTER Where Reporting_user='" + objCommonVar.CurrentUserCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry += " Order By CODE "
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.BestFitColumns()
                Me.gv1.Columns("Select").ReadOnly = False

                'Me.gv1.Columns("DepartmentCode").Width = 120
                'Me.gv1.Columns("HospitalName").Width = 120
                'Me.gv1.Columns("LeaveFromDate").Width = 110
                'Me.gv1.Columns("LeaveToDate").Width = 110
                'Me.gv1.Columns("ReasonOfLeave").Width = 120
                'Me.gv1.Columns("PlaceToVisit").Width = 120
                'Me.gv1.Columns("TotalDays").Width = 110
                'Me.gv1.Columns("Code").Width = 103
                'Me.gv1.Columns("Status").Width = 103
                'Me.gv1.Columns("Created_By").Width = 103
                If ddlStatus.SelectedValue = "Approved" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnPost.Enabled = False
                    btnReject.Enabled = False
                ElseIf ddlStatus.SelectedValue = "Reject" Then
                    UsLock1.Status = ERPTransactionStatus.Cancel
                    btnReject.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnPost.Enabled = True
                    btnReject.Enabled = True
                End If
                btnExport.Enabled = True
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                btnExport.Enabled = False
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadDataForGoBtn()
        Try

            Dim qry As String = ""
            qry = ""
            qry += " select cast(0 as bit)  as [Select],Tspl_HR_EM_Resignation_Letter.Doc_code as [Doc Code],convert(varchar,Tspl_HR_EM_Resignation_Letter.Doc_Date,103) as [Doc Date] ,TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],convert(varchar,Tspl_HR_EM_Resignation_Letter.Resignation_Date,103) as [Resignation Date],Tspl_HR_EM_Resignation_Letter.Reson_Of_Resignation  as [Reson of Resignation],Tspl_HR_EM_Resignation_Letter.Created_By as [Created By] from Tspl_HR_EM_Resignation_Letter"
            qry += "  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER .EMP_CODE =Tspl_HR_EM_Resignation_Letter.EMP_CODE "
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Resignation_Letter.DEPARTMENT_CODE "
            If Not clsCommon.CompairString(ddlStatus.SelectedValue, "") = CompairStringResult.Equal Then
                qry += " where Tspl_HR_EM_Resignation_Letter.STATUS ='" + ddlStatus.SelectedValue + "' "
            End If
            'qry += " and CREATED_Date >= CONVERT(Date,'" + txtFromDate.Value + "',103)  and CREATED_Date  <= CONVERT(Date,'" + txtToDate.Value + "',103)  "
            qry += " and convert(date,Tspl_HR_EM_Resignation_Letter.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_HR_EM_Resignation_Letter.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  "

            qry += " and Tspl_HR_EM_Resignation_Letter.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
            'qry += " and TSPL_LEAVE_MASTER.Created_By in (Select User_Code from USER_MASTER Where Reporting_user='" + objCommonVar.CurrentUserCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry += " Order By CODE "
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.BestFitColumns()
                Me.gv1.Columns("Select").ReadOnly = False

                'Me.gv1.Columns("DepartmentCode").Width = 120
                'Me.gv1.Columns("HospitalName").Width = 120
                'Me.gv1.Columns("LeaveFromDate").Width = 110
                'Me.gv1.Columns("LeaveToDate").Width = 110
                'Me.gv1.Columns("ReasonOfLeave").Width = 120
                'Me.gv1.Columns("PlaceToVisit").Width = 120
                'Me.gv1.Columns("TotalDays").Width = 110
                'Me.gv1.Columns("Code").Width = 103
                'Me.gv1.Columns("Status").Width = 103
                'Me.gv1.Columns("Created_By").Width = 103
                If ddlStatus.SelectedValue = "Approved" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnPost.Enabled = False
                    btnReject.Enabled = False
                ElseIf ddlStatus.SelectedValue = "Reject" Then
                    UsLock1.Status = ERPTransactionStatus.Cancel
                    btnReject.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnPost.Enabled = True
                    btnReject.Enabled = True
                End If
                btnExport.Enabled = True
                strStatus = ""
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                btnExport.Enabled = False
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                clsCommon.MyMessageBoxShow(Me, "No Data to Display.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UsLock1.Visible = True
        btnReject.Enabled = True
        LoadDataForGoBtn()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim dt As DataTable = gv1.DataSource
        If dt.Rows.Count > 0 Then
            For Each dr As GridViewRowInfo In gv1.Rows
                dr.Cells("select").Value = True
            Next
        End If
        gv1.Focus()
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        Dim dt As DataTable = gv1.DataSource
        If dt.Rows.Count > 0 Then
            For Each dr As GridViewRowInfo In gv1.Rows
                dr.Cells("select").Value = False
            Next
        End If
    End Sub

    'Private Sub sendEMailThroughOUTLOOK(p1 As Object, strStatus As String, LeaveFdate As String, LeaveTdate As String, TotalDays As String)
    '    Throw New NotImplementedException
    'End Sub

End Class
