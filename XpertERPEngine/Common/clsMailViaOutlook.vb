Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Microsoft.Office.Interop
Imports System
Imports System.Xml
Imports System.Net
Imports System.Net.WebRequest
Imports System.Net.WebResponse
Imports System.Web
Imports System.Configuration
Imports System.Data.SqlClient

'Public Class clsMailViaOutlook
'    Public Shared Sub SendEmail(ByVal strSubject As String, ByVal strBody As String, ByVal arrRecepients As List(Of String), ByVal arrRecepientsCC As List(Of String), ByVal AttachPath As String)
'        Try
'            If objCommonVar.IsMailSend Then
'                Exit Sub
'            End If

'            If arrRecepients IsNot Nothing AndAlso arrRecepients.Count > 0 Then
'                If Process.GetProcessesByName("OutLook").Length < 1 Then
'                    Process.Start("OutLook.exe")
'                End If
'                Dim oApp As New Outlook.Application()
'                Dim oMsg As Outlook.MailItem

'                oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
'                oMsg.Body = strBody
'                oMsg.Subject = strSubject
'                Dim oAttachment As Outlook.Attachment = Nothing

'                If clsCommon.myLen(AttachPath) > 0 Then
'                    Dim sDisplayname As [String] = "MyAttachment"
'                    If oMsg.Body Is Nothing Then
'                        oMsg.Body = " "
'                    End If
'                    Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
'                    Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

'                    oAttachment = oMsg.Attachments.Add(AttachPath, iAtchmentType, iPosition, sDisplayname)

'                End If
'                For Each strRece As String In arrRecepients
'                    oMsg.Recipients.Add(strRece)
'                Next

'                If arrRecepientsCC IsNot Nothing AndAlso arrRecepientsCC.Count > 0 Then
'                    For Each strRece As String In arrRecepientsCC
'                        oMsg.CC += strRece + ";"
'                    Next
'                End If

'                oMsg.Send()
'                oMsg = Nothing
'                oApp = Nothing
'            End If
'        Catch ex As System.Exception
'            Throw New System.Exception(ex.Message)
'        End Try
'    End Sub
'End Class

'Public Class clsSMSSend
'    Public Shared Function SendSMS(ByVal UserMgtCode As String, ByVal strMes As String, ByVal OthePhnNo As String) As Boolean
'        Try
'            Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(UserMgtCode)

'            If clsCommon.myLen(obj.usercode) > 0 AndAlso obj.usercode.Substring(0, 3) = "','" Then
'                obj.usercode = obj.usercode.Substring(2, obj.usercode.Length - 2)
'            End If

'            Dim strphone As String = ""
'            If clsCommon.myLen(obj.usercode) > 0 Then
'                strphone = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in (" & obj.usercode & "') for xml path(''))  ")
'            End If


'            strphone = strphone + "," + OthePhnNo
'            Try
'                If strphone.Substring(0, 1) = "," Then
'                    strphone = strphone.Substring(1, strphone.Length - 1)
'                End If
'            Catch ex As System.Exception
'                Throw New System.Exception(ex.Message)
'            End Try

'            If clsCommon.myLen(strphone) <= 0 Then
'                Throw New System.Exception("Phone No. not found")
'            End If

'            'Dim consumeWebService As BSWS.BSWS
'            'consumeWebService = New BSWS.BSWS
'            'Dim xmlResult As XmlElement
'            'xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
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
'        Catch ex As System.Exception
'            Throw New System.Exception(ex.Message)
'        End Try
'        Return True
'    End Function
'End Class

Public Class clsESConfig
#Region "Variables"
    Public EMail_SMTP_Client As String = Nothing
    Public EMail_Port As String = Nothing
    Public EMail_ID As String = Nothing
    Public EMail_Pwd As String = Nothing
    Public EMail_Enabel_SSL As Boolean = False
    Public SMS_String As String = Nothing
    Public NoOFChar As Integer = 0
#End Region

    Public Function SaveData(ByVal obj As clsESConfig) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_ES_Config"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMail_SMTP_Client", obj.EMail_SMTP_Client)
            clsCommon.AddColumnsForChange(coll, "EMail_Port", obj.EMail_Port)
            clsCommon.AddColumnsForChange(coll, "EMail_ID", obj.EMail_ID)
            clsCommon.AddColumnsForChange(coll, "EMail_Pwd", obj.EMail_Pwd)
            clsCommon.AddColumnsForChange(coll, "EMail_Enabel_SSL", IIf(obj.EMail_Enabel_SSL, 1, 0))
            clsCommon.AddColumnsForChange(coll, "SMS_String", obj.SMS_String)
            clsCommon.AddColumnsForChange(coll, "SplitChar", obj.NoOFChar)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ES_Config", OMInsertOrUpdate.Insert, "")
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData() As clsESConfig
        Dim obj As clsESConfig = Nothing
        Dim qry As String = "SELECT * from TSPL_ES_Config"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsESConfig()
            obj.EMail_SMTP_Client = clsCommon.myCstr(dt.Rows(0)("EMail_SMTP_Client"))
            obj.EMail_Port = clsCommon.myCstr(dt.Rows(0)("EMail_Port"))
            obj.EMail_ID = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
            obj.EMail_Pwd = clsCommon.myCstr(dt.Rows(0)("EMail_Pwd"))
            obj.EMail_Enabel_SSL = IIf(clsCommon.myCdbl(dt.Rows(0)("EMail_Enabel_SSL")) = 1, True, False)
            obj.SMS_String = clsCommon.myCstr(dt.Rows(0)("SMS_String"))
            obj.NoOFChar = clsCommon.myCdbl(dt.Rows(0)("SplitChar"))
        End If
        Return obj
    End Function
End Class

Public Class clsESContent
#Region "Variables"
    Public Form_ID As String = Nothing
    Public EMail_Subject As String = Nothing
    Public EMail_Text As String = Nothing
    Public SMS_Text As String = Nothing
    Public Notification_Caption As String = Nothing
    Public Notification_Text As String = Nothing
    Public Notification_Detail_Text As String = Nothing
    Public Notification_On As String = Nothing



    Public arrAlertEmployeeSMS As ArrayList = Nothing
    Public arrAlertEmployeeEMail As ArrayList = Nothing
    Public arrAlertEmployeeNotification As ArrayList = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsESContent) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_ES_Content_Emp_Detail where Form_ID='" + obj.Form_ID + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "select 1 from TSPL_ES_Content where Form_ID='" + obj.Form_ID + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMail_Subject", obj.EMail_Subject)
            clsCommon.AddColumnsForChange(coll, "EMail_Text", obj.EMail_Text)
            clsCommon.AddColumnsForChange(coll, "Notification_Caption", obj.Notification_Caption)
            clsCommon.AddColumnsForChange(coll, "Notification_Text", obj.Notification_Text)
            clsCommon.AddColumnsForChange(coll, "Notification_Detail_Text", obj.Notification_Detail_Text)
            clsCommon.AddColumnsForChange(coll, "Notification_On", obj.Notification_On)
            clsCommon.AddColumnsForChange(coll, "SMS_Text", obj.SMS_Text)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ''Old Entry
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ES_Content", OMInsertOrUpdate.Update, "Form_ID='" + obj.Form_ID + "'", trans)
            Else
                ''New Entry
                clsCommon.AddColumnsForChange(coll, "Form_ID", obj.Form_ID)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ES_Content", OMInsertOrUpdate.Insert, "", trans)
            End If
            clsESContentEMPDetail.SaveData(obj.Form_ID, 0, arrAlertEmployeeSMS, trans) ''0 for SMS
            clsESContentEMPDetail.SaveData(obj.Form_ID, 1, arrAlertEmployeeEMail, trans) '' 1 for EMAIL
            clsESContentEMPDetail.SaveData(obj.Form_ID, 2, arrAlertEmployeeNotification, trans) '' 2 for Notification
            trans.Commit()
        Catch err As System.Exception
            trans.Rollback()
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strFormID As String) As clsESContent
        Return GetData(strFormID, Nothing)
    End Function
    Public Shared Function GetData(ByVal strFormID As String, ByVal tran As SqlTransaction) As clsESContent
        Dim obj As clsESContent = Nothing
        Dim qry As String = "SELECT * from TSPL_ES_Content where Form_ID='" + strFormID + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsESContent()
            obj.Form_ID = clsCommon.myCstr(dt.Rows(0)("Form_ID"))
            obj.EMail_Subject = clsCommon.myCstr(dt.Rows(0)("EMail_Subject"))
            obj.EMail_Text = clsCommon.myCstr(dt.Rows(0)("EMail_Text"))
            obj.Notification_Caption = clsCommon.myCstr(dt.Rows(0)("Notification_Caption"))
            obj.Notification_Text = clsCommon.myCstr(dt.Rows(0)("Notification_Text"))
            obj.Notification_Detail_Text = clsCommon.myCstr(dt.Rows(0)("Notification_Detail_Text"))
            obj.Notification_On = clsCommon.myCstr(dt.Rows(0)("Notification_On"))
            obj.SMS_Text = clsCommon.myCstr(dt.Rows(0)("SMS_Text"))


            obj.arrAlertEmployeeSMS = clsESContentEMPDetail.GetData(strFormID, 0, tran)
            obj.arrAlertEmployeeEMail = clsESContentEMPDetail.GetData(strFormID, 1, tran)
            'done by stuti on 23/11/2016 regarding notification
            obj.arrAlertEmployeeNotification = clsESContentEMPDetail.GetData(strFormID, 2, tran)
        End If

        Return obj
    End Function
End Class

Public Class clsESContentEMPDetail
#Region "Variables"
    Public Form_ID As String = Nothing
    Public EMP_CODE As String = Nothing
    Public Alert_Type As Integer
#End Region

    Public Shared Function SaveData(ByVal strFormID As String, ByVal AlertType As Integer, ByVal arrEMP As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            ''Alert_Type  0--SMS,1-EMail
            If arrEMP IsNot Nothing AndAlso arrEMP.Count > 0 Then
                For Each StrEmp As String In arrEMP
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Form_Id", strFormID)
                    clsCommon.AddColumnsForChange(coll, "Alert_Type", AlertType)
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", StrEmp)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ES_Content_Emp_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strFormID As String, ByVal AlertType As Integer) As ArrayList
        Return GetData(strFormID, AlertType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strFormID As String, ByVal AlertType As Integer, ByVal tran As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "SELECT EMP_CODE from TSPL_ES_Content_Emp_Detail where Form_ID='" + strFormID + "' and Alert_Type='" + clsCommon.myCstr(AlertType) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("EMP_CODE")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsSMSHead
#Region "Variables"
    Public Code As String = Nothing
    Public SMS_Text As String = Nothing
    Public arrMobilNo As List(Of String) = Nothing
    Public Created_Date As DateTime? = Nothing
#End Region
    Public Function SaveData(ByVal FormID As String, ByVal obj As clsSMSHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = " select max(Code) from TSPL_SMS_HEAD where  Code like (select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SMSPrefix + "' and Type='" + clsFixedParameterType.SMSPrefix + "')+'%'"
            obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(obj.Code) > 0 Then
                obj.Code = clsCommon.incval(obj.Code)
            Else
                Dim SMSPrefix As String = clsFixedParameter.GetData(clsFixedParameterCode.SMSPrefix, clsFixedParameterType.SMSPrefix, trans)
                If clsCommon.myLen(SMSPrefix) <= 0 Then
                    Throw New System.Exception("Please ask you administrator to set SMS Prefix in Fixed parameter")
                End If
                obj.Code = SMSPrefix + "0000000000001"
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "SMS_Text", obj.SMS_Text, False, True)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            If obj.Created_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SMS_HEAD", OMInsertOrUpdate.Insert, "", trans)

            qry = "select TSPL_EMPLOYEE_MASTER.Phone from TSPL_ES_Content_Emp_Detail inner join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_ES_Content_Emp_Detail.Emp_code "
            qry += " where LEN(isnull( Phone,''))>0 and TSPL_ES_Content_Emp_Detail.Form_Id='" + FormID + "' and Alert_Type='0'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If obj.arrMobilNo Is Nothing OrElse obj.arrMobilNo.Count <= 0 Then
                    obj.arrMobilNo = New List(Of String)
                End If
                For Each dr As DataRow In dt.Rows
                    obj.arrMobilNo.Add(clsCommon.myCstr(dr("Phone")))
                Next
            End If
            clsSMSDetail.SaveData(obj.Code, obj.arrMobilNo, trans)
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function


End Class

Public Class clsSMSDetail
#Region "Variables"
    Public Code As String = Nothing
    Public Mobile_No As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim arrRepeat As New List(Of String)
            For Each Item As String In Arr
                If arrRepeat.Contains(Item) Then
                    Continue For
                Else
                    arrRepeat.Add(Item)
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Mobile_No", Item)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SMS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
            arrRepeat = Nothing
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsEMailHead
#Region "Variables"
    Public Code As String = Nothing
    Public Email_Subject As String = Nothing
    Public Email_Text As String = Nothing
    Public Attachment_1_Path As String = Nothing
    Public Attachment_2_Path As String = Nothing
    Public arrEMail As List(Of String) = Nothing
    Private Attachment_1_File_Name As String = Nothing
    Private Attachment_2_File_Name As String = Nothing
    Public Created_Date As DateTime? = Nothing
    Public IsBodyHtml As Integer = 0
    Public Against_PO_NO As String = Nothing
#End Region

    Public Function SaveData(ByVal FormID As String, ByVal obj As clsEMailHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = " select max(Code) from TSPL_EMAIL_HEAD where Code like (select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SMSPrefix + "' and Type='" + clsFixedParameterType.SMSPrefix + "')+'%'"
            obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(obj.Code) > 0 Then
                obj.Code = clsCommon.incval(obj.Code)
            Else
                Dim SMSPrefix As String = clsFixedParameter.GetData(clsFixedParameterCode.SMSPrefix, clsFixedParameterType.SMSPrefix, trans)
                If clsCommon.myLen(SMSPrefix) <= 0 Then
                    Throw New System.Exception("Please ask you administrator to set SMS Prefix in Fixed parameter")
                End If
                obj.Code = SMSPrefix + "0000000000001"
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Email_Subject", obj.Email_Subject)
            clsCommon.AddColumnsForChange(coll, "Email_Text", obj.Email_Text)
            clsCommon.AddColumnsForChange(coll, "Against_PO_NO", obj.Against_PO_NO, True)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            If clsCommon.CompairString(clsCommon.myCstr(obj.IsBodyHtml), "1") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "IsBodyHtml", obj.IsBodyHtml)
            End If
            If obj.Created_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt"))
            End If


            Dim FileInfo As FileInfo
            If clsCommon.myLen(obj.Attachment_1_Path) > 0 Then
                FileInfo = New FileInfo(obj.Attachment_1_Path)
                obj.Attachment_1_File_Name = FileInfo.Name
                clsCommon.AddColumnsForChange(coll, "Attachment_1_File_Name", obj.Attachment_1_File_Name)
            End If
            If clsCommon.myLen(obj.Attachment_2_Path) > 0 Then
                FileInfo = New FileInfo(obj.Attachment_2_Path)
                obj.Attachment_2_File_Name = FileInfo.Name
                clsCommon.AddColumnsForChange(coll, "Attachment_2_File_Name", obj.Attachment_2_File_Name)
            End If
            qry = "select TSPL_EMPLOYEE_MASTER.EMail_ID from TSPL_ES_Content_Emp_Detail inner join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_ES_Content_Emp_Detail.Emp_code "
            qry += " where LEN(isnull( EMail_ID,''))>0 and TSPL_ES_Content_Emp_Detail.Form_Id='" + FormID + "' and Alert_Type='1'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If obj.arrEMail Is Nothing OrElse obj.arrEMail.Count <= 0 Then
                    obj.arrEMail = New List(Of String)
                End If
                For Each dr As DataRow In dt.Rows
                    obj.arrEMail.Add(clsCommon.myCstr(dr("EMail_ID")))
                Next
            End If
            Dim strEmail As String = ""
            If obj.arrEMail Is Nothing OrElse obj.arrEMail.Count <= 0 Then
                Throw New System.Exception("No Email ID  found to send EMail")
            End If
            For Each Item As String In obj.arrEMail
                strEmail += Item + ";"
            Next
            clsCommon.AddColumnsForChange(coll, "EMail_ID", strEmail)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMAIL_HEAD", OMInsertOrUpdate.Insert, "", trans)
            If clsCommon.myLen(obj.Attachment_1_Path) > 0 Then
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(obj.Attachment_1_Path))
                bData = br.ReadBytes(br.BaseStream.Length)
                Dim str As String = " UPDATE TSPL_EMAIL_HEAD set Attachment_1 = @BLOBData  where CODE='" + obj.Code + "'"
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Parameters.Add(prm)
                cmd.ExecuteNonQuery()
                br.Close() ' done by stuti reagrding memory leakage
            End If
            If clsCommon.myLen(obj.Attachment_2_Path) > 0 Then
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(obj.Attachment_2_Path))
                bData = br.ReadBytes(br.BaseStream.Length)
                Dim str As String = " UPDATE TSPL_EMAIL_HEAD set Attachment_2 = @BLOBData  where CODE='" + obj.Code + "'"
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Parameters.Add(prm)
                cmd.ExecuteNonQuery()
                br.Close() ' done by stuti reagrding memory leakage
            End If
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsNotificationReplace
#Region "Variables"
    Public DocNo As String = Nothing
    Public DocDate As String = Nothing
    Public DocAmt As String = Nothing
#End Region

End Class
Public Class clsNotificationHead
#Region "Variables"
    Public Code As String = Nothing
    Public Notification_Text As String = Nothing
    Public Notification_Detail_Text As String = Nothing
    Public Notification_Caption As String = Nothing
    Public Notification_On As String = Nothing
    Public Notification_ConditionDate As DateTime?
    Public Notification_Tanker_Doc_Type As String = Nothing
    Public arrUser As List(Of clsNotificationDetail) = Nothing
#End Region
    Public Function SaveData(ByVal FormID As String, ByVal obj As clsNotificationHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = " select max(Code) from TSPL_Notification_HEAD "
            obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(obj.Code) > 0 Then
                obj.Code = clsCommon.incval(obj.Code)
            Else
                obj.Code = "NOT0000000000001"
            End If
            Dim TempDepartmentCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from tspl_user_master where User_Code='" + objCommonVar.CurrentUserCode + "'", trans))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Notification_Text", obj.Notification_Text)
            clsCommon.AddColumnsForChange(coll, "Notification_Caption", obj.Notification_Caption)
            clsCommon.AddColumnsForChange(coll, "Notification_On", obj.Notification_On)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If obj.Notification_ConditionDate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Condition_Date", clsCommon.GetPrintDate(obj.Notification_ConditionDate, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "Notification_From_Department_Code", TempDepartmentCode, True)
            clsCommon.AddColumnsForChange(coll, "Notification_Tanker_Doc_Type", obj.Notification_Tanker_Doc_Type, True)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Notification_HEAD", OMInsertOrUpdate.Insert, "", trans)

            qry = "select TSPL_ES_Content_Emp_Detail.EMP_CODE as Users from TSPL_ES_Content_Emp_Detail where TSPL_ES_Content_Emp_Detail.Form_Id='" + FormID + "' and Alert_Type='2'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If obj.arrUser Is Nothing OrElse obj.arrUser.Count <= 0 Then
                    obj.arrUser = New List(Of clsNotificationDetail)
                End If
                Dim objTr As clsNotificationDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsNotificationDetail()
                    objTr.Code = obj.Code
                    objTr.User_Name = clsCommon.myCstr(dr("Users"))
                    obj.arrUser.Add(objTr)
                Next
            End If
            clsNotificationDetail.SaveData(obj.Code, obj.arrUser, trans)
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Sub SendNotification(ByVal Form_ID As String, ByVal obj As clsNotificationReplace, ByVal Notify_On As String, ByVal trans As SqlTransaction)
        Dim NotificationText As String = Nothing
        Dim dtNotification As DataTable = clsDBFuncationality.GetDataTable("SELECT Notification_Text,Notification_Caption,Notification_On from TSPL_ES_Content where Form_ID='" + Form_ID + "' and Notification_On='" + Notify_On + "'", trans)
        If dtNotification IsNot Nothing AndAlso dtNotification.Rows.Count > 0 Then
            NotificationText = clsCommon.myCstr(dtNotification.Rows(0).Item("Notification_Text"))
            NotificationText = NotificationText.Replace(clsEmailSMSConstants.DOC_NO, obj.DocNo)
            NotificationText = NotificationText.Replace(clsEmailSMSConstants.DOC_Date, obj.DocDate)
            NotificationText = NotificationText.Replace(clsEmailSMSConstants.Doc_Amount, obj.DocAmt)

            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = NotificationText
            objNotification.Notification_Caption = clsCommon.myCstr(dtNotification.Rows(0).Item("Notification_Caption"))
            objNotification.Notification_On = clsCommon.myCstr(dtNotification.Rows(0).Item("Notification_On"))

            objNotification.arrUser = New List(Of clsNotificationDetail)
            Dim dtNotificationdetail As DataTable = clsDBFuncationality.GetDataTable("SELECT EMP_CODE from TSPL_ES_CONTENT_EMP_DETAIL where Alert_Type='2' and Form_ID='" + Form_ID + "'", trans)
            If dtNotificationdetail IsNot Nothing AndAlso dtNotificationdetail.Rows.Count > 0 Then
                For Each dtrow As DataRow In dtNotificationdetail.Rows
                    Dim objNotificationDetail As New clsNotificationDetail()
                    objNotificationDetail.Code = ""
                    objNotificationDetail.User_Name = clsCommon.myCstr(dtrow("EMP_CODE"))
                    objNotification.arrUser.Add(objNotificationDetail)
                Next
            End If
            objNotification.SaveData(Form_ID, objNotification, trans)
        End If
    End Sub

End Class

Public Class clsNotificationDetail
#Region "Variables"
    Public Code As String = Nothing
    Public User_Name As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsNotificationDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim arrRepeat As New List(Of clsNotificationDetail)
            For Each Item As clsNotificationDetail In Arr

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "User_Name", Item.User_Name)
                clsCommon.AddColumnsForChange(coll, "Sender_Replay", "0")
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Notification_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
            arrRepeat = Nothing
        Catch err As System.Exception
            Throw New System.Exception(err.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMasterDefault
#Region "Variables"
    Public Const colMCCUploaderCode As String = "MCC Uploader Code#"
    Public Const colMCCCode As String = "MCC Code#"
    Public Const colMCCType As String = "MCC Type#"
    Public Const colMCCName As String = "MCC Name#"
    Public Const colMCCChillingVendorCode As String = "MCC Chilling Vendor Code#"
    Public Const colMCCAddress1 As String = "MCC Address1#"
    Public Const colMCCAddress2 As String = "MCC Address2#"
    Public Const colMCCTehsil As String = "MCC Tehsil#"
    Public Const colMCCCity As String = "MCC City Code#"
    Public Const colMCCState As String = "MCC State Code#"
    Public Const colMCCCountry As String = "MCC Country Code#"
    Public Const colMCCPinCode As String = "MCC Pin Code#"
    Public Const colMCCTelphone As String = "MCC Telphone#"
    Public Const colMCCEmail As String = "MCC Email#"
    Public Const colMCCFax As String = "MCC Fax#"
    Public Const colMccSuperArea As String = "MCC Super Area#"
    Public Const colMccSuperAreaUOM As String = "MCC Super Area UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCAreaOfStore As String = "MCC Area Of Store#"
    Public Const colMCCAreaOfOffice As String = "MCC Area Of Office#"
    Public Const colMCCAreaOfOfficeUOM As String = "MCC Area of Office UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCOpenAreaForTanker As String = "MCC Open Area For Tanker#"
    Public Const colMCCOpenAreaForTankerUOM As String = "MCC Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCAreaOfLab As String = "MCC Area Of Lab#"
    Public Const colMCCAreaOfLabUOM As String = "MCC Area of Lab UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCTotalStorageCapacity As String = "MCC Total Storage Capacity#"
    Public Const colMCCAreaOfReceivingDock As String = "MCC Area Of Receiving Dock#"
    Public Const colMCCAreaOfReceivingDockUOM As String = "MCC Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCDripSaver As String = "MCC Drip Saver (Yes/No)#"
    Public Const colMCCCanWasher As String = "MCC Can Washer (Yes/No)#"
    Public Const colMCCCanScrubber As String = "MCC Can Scrubber (Yes/No)#"
    Public Const colMCCFssaiNo As String = "MCC Fssai No#"
    Public Const colMCCETP As String = "MCC ETP (Yes/No)#"
    Public Const colMCCEarthing As String = "MCC Earthing (Yes/No)#"
    Public Const colMCCCoilLength As String = "MCC Coil Length#"
    Public Const colMCCElectricityConnection As String = "MCC Electricity Connection#"
    Public Const colMCCBoiler As String = "MCC Boiler (Yes/No)#"
    Public Const colMCCIndustryType As String = "MCC Industry Type#"
    Public Const colMCCPropName As String = "MCC Prop Name#"
    Public Const colMCCPartnerName As String = "MCC Partner Name#"
    Public Const colMCCDirectorName As String = "MCC Director Name#"
    Public Const colMCCMonthlyProvision As String = "MCC Monthly Provision(Y/N)#"
    Public Const colMCCChillingCharges As String = "MCC Chilling Charges#"
    Public Const colMCCChillingOn As String = "MCC Chilling On#"
    Public Const colMCCChillingMinGuaranteedAvgQty As String = "MCC Chilling Min. Guaranteed Avg. Qty#"
    Public Const colMCCChillingOnUOMKGLTR As String = "MCC Chilling On UOM(KG/LTR)#"
    Public Const colMCCChillingOnQty As String = "MCC Chilling On Qty#"
    Public Const colMCCChillingOnUOMHandledDispatched As String = "MCC Chilling On UOM(Handled/Dispatched)#"
    Public Const colMCCChillingMinGuaranteedPeriod As String = "MCC Chilling Min. Guaranteed Period#"
    Public Const colMCCChillingMinGuaranteedPeriodUOM As String = "MCC Chilling Min. Guaranteed Period UOM (Day/Month/Year)#"
    Public Const colMCCRateofLeaseCharges As String = "MCC Rate of Lease Charges#"
    Public Const colMCCRateofLeasedChargesUOM As String = "MCC Rate of Leased Charges UOM(Day/Month/Year)#"
    Public Const colMCCAreaofStoreUOM As String = "MCC Area of Store UOM(Sq. Ft./Sq. Mt.)#"
    Public Const colMCCAgreement_Status As String = "MCC Agreement_Status#"
    Public Const colMCCAgreement_Date As String = "MCC Agreement_Date#"
    Public Const colMCCAgreementExpiryDate As String = "MCC Agreement Expiry Date#"
    Public Const colMCCSecurity_Status As String = "MCC Security_Status#"
    Public Const colMCCCheque_Amt As String = "MCC Cheque_Amt#"
    Public Const colMCCCheque_No As String = "MCC Cheque_No#"
    Public Const colMCCCheque_Date As String = "MCC Cheque_Date#"
    Public Const colMCCChillingStartingDate As String = "MCC Chilling Starting Date#"
    Public Const colMCCIsTruckSheetMandatory As String = "MCC Is Truck Sheet Mandatory#"
    Public Const colMCCWeighingComPort As String = "MCC Weighing ComPort#"
    Public Const colMCCWeighingMachine As String = "MCC Weighing Machine#"
    Public Const colMCCSampleComPort As String = "MCC Sample ComPort#"
    Public Const colMCCSampleMachine As String = "MCC Sample Machine#"
    Public Const colMCCPaymentCycle As String = "MCC Payment Cycle#"
    Public Const colMCCIncentiveCode As String = "MCC Incentive Code#"
    Public Const colMCCShiftMorningOpeningTime As String = "MCC Shift Morning Opening Time#"
    Public Const colMCCShiftMorningClosingTime As String = "MCC Shift Morning Closing Time#"
    Public Const colMCCShiftEveningOpeningTime As String = "MCC Shift Evening Opening Time#"
    Public Const colMCCShiftEveningClosingTime As String = "MCC Shift Evening Closing Time#"
    Public Const colMCCRM As String = "MCC RM#"
    Public Const colMCCRequiredGateEntry As String = "MCC Required Gate Entry(Yes/No)#"
    Public Const colMCCAllowAutoMilkIn As String = "MCC AllowAutoMilkIn#"
    Public Const colMCCAutoIn_Location As String = "MCC AutoIn_Location#"
    Public Const colMCCSILOIn_Location As String = "MCC SILOIn_Location#"
    Public Const colMCCApplyReceiptWeightTolerance As String = "MCC ApplyReceiptWeightTolerance(Y/N)#"
    Public Const colMCCReceiptWeightToleranceValue As String = "MCC ReceiptWeightToleranceValue#"
    Public Const colMCCApplyFailedSample As String = "MCC Apply Failed Sample(Y/N)#"
    Public Const colMCCFailedSampleFAT As String = "MCC Failed Sample FAT %#"
    Public Const colMCCFailedSampleSNF As String = "MCC Failed Sample SNF %#"
    Public Const colMCCLocSegmentCode As String = "MCC Loc Segment Code#"
    Public Const colMCCBMCC As String = "MCC MCC(1)/BMCC(0)#"
    Public Const colMCCCommissionRate As String = "MCC CommissionRate#"
    Public Const colMCCCommissionMinimumShiftInPaymentCycle As String = "MCC CommissionMinimumShiftInPaymentCycle#"
    Public Const colMCCCommissionMinimumQtyInShift As String = "MCC CommissionMinimumQtyInShift#"
    Public Const colMCCCommissionNoOfPaymentCycleForNewVSP As String = "MCC CommissionNoOfPaymentCycleForNewVSP#"
    Public Const colMCCDeductionMinimumFATPer As String = "MCC DeductionMinimumFATPer#"
    Public Const colMCCDeductionMinimumSNFPer As String = "MCC DeductionMinimumSNFPer#"
    Public Const colMCCDeductionNoOfPaymentCycleForNewVSP As String = "MCC DeductionNoOfPaymentCycleForNewVSP#"
    Public Const colMCCPlant As String = "MCC Plant#"
    Public Const colMCCMorningShiftOpeningTime As String = "MCC Morning Shift Opening Time#"
    Public Const colMCCMorningShiftClosingTime As String = "MCC Morning Shift Closing Time#"
    Public Const colMCCEveningShiftOpeningTime As String = "MCC Evening Shift Opening Time#"
    Public Const colMCCEveningShiftClosingTime As String = "MCC Evening Shift Closing Time#"
    Public Const colDCSTransporterGroupCode As String = "DCS Transporter group code#"
    Public Const colDCSVLCCode As String = "DCS VLC Code#"
    Public Const colDCSVLCName As String = "DCS VLC Name#"
    Public Const colDCSUploaderCode As String = "DCS Uploader Code#"
    Public Const colDCSVillageName As String = "DCS Village Name#"
    Public Const colDCSVSPCode As String = "DCS VSP Code#"
    Public Const colDCSVSPName As String = "DCS VSP Name#"
    Public Const colDCSType As String = "DCS Type#"
    Public Const colDCSVSPAddress As String = "DCS VSP Address#"
    Public Const colDCSState As String = "DCS State#"
    Public Const colDCSVSPGroupCode As String = "DCS Group Code#"
    Public Const colDCSCreatecustomer As String = "DCS Create customer#"
    Public Const colDCSCustomerGroupCode As String = "DCS Customer Group Code#"
    Public Const colVSPPaymentType As String = "DCS Payment type#"
    Public Const colDCSBankCode As String = "DCS Bank Code#"
    Public Const colDCSBankName As String = "DCS Bank Name#"
    Public Const colDCSIFSCCode As String = "DCS IFSC Code#"
    Public Const colDCSBranchName As String = "DCS Branch Name#"
    Public Const colDCSAccountNo As String = "DCS Account No#"
    Public Const colDCSBuffalowTIP As String = "DCS Buffalow TIP#"
    Public Const colDCSCowTIP As String = "DCS Cow TIP#"

    Public Const colMPUploaderCode As String = "Farmer MP Uploader Code#"
    Public Const colMPCode As String = "Farmer Code#"
    Public Const colMPName As String = "Farmer Name#"
    Public Const colMPFatherName As String = "Farmer Father Name#"
    Public Const colMPAddress1 As String = "Farmer Address1#"
    Public Const colMPAddress2 As String = "Farmer Address2#"
    Public Const colMPZila As String = "Farmer Zila#"
    Public Const colMPTehsil As String = "Farmer Tehsil#"
    Public Const colMPCityCode As String = "Farmer City Code#"
    Public Const colMPStateCode As String = "Farmer State Code#"
    Public Const colMPCountryCode As String = "Farmer Country Code#"
    Public Const colMPPinCode As String = "Farmer Pin Code#"
    Public Const colMPTelphone As String = "Farmer Telphone#"
    Public Const colMPEmail As String = "Farmer Email#"
    Public Const colMPAadharNo As String = "Farmer AadharNo#"
    Public Const colMPJanAadharNo As String = "Farmer JanAadharNo#"
    Public Const colMPDateofBirth As String = "Farmer Date of Birth#"
    Public Const colMPEducation As String = "Farmer Education#"
    Public Const colMPLandHolding As String = "Farmer Land Holding#"
    Public Const colMPNoOfMilchAnimal As String = "Farmer No Of Milch Animal#"
    Public Const colMPTotalMilkProduction As String = "Farmer Total Milk Production#"
    Public Const colMPMilkForSelfConsumption As String = "Farmer Milk For Self Consumption#"
    Public Const colMPMilkForSale As String = "Farmer Milk For Sale#"
    Public Const colMPPayeeName As String = "Farmer Payee Name#"
    Public Const colMPBankCode As String = "Farmer Bank Code#"
    Public Const colMPBankBranch As String = "Farmer Bank Branch#"
    Public Const colMPBankCityCode As String = "Farmer Bank City Code#"
    Public Const colMPBankStateCode As String = "Farmer Bank State Code#"
    Public Const colMPIFSCCode As String = "Farmer IFSC Code#"
    Public Const colMPAccountNo As String = "Farmer Account No#"
    Public Const colMPCustomerAccSet As String = "Farmer Customer Acc Set#"
    Public Const colMPVendorAccSet As String = "Farmer Vendor Acc Set#"
    Public Const colMPTOLERANCE As String = "Farmer TOLERANCE#"
#End Region

End Class

Public Class clsDBFTemplate
#Region "Variables"
    Public Const DDate As String = "Date#"
    Public Const Shift As String = "Shift#"
    Public Const Route As String = "Route#"
    Public Const DCS As String = "DCS#"
    Public Const Qty As String = "Qty#"
    Public Const FAT As String = "FAT#"
    Public Const SNF As String = "SNF#"
    Public Const EmpatyCAN As String = "EmpatyCAN#"
    Public Const DockCollectionMilkType As String = "DockCollectionMilkType#"
#End Region

End Class




