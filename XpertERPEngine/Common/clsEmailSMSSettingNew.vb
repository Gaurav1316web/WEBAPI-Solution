Imports common
Imports System.Data.SqlClient

Public Class clsEmailSMSSettingNew
#Region "Variables"
    Public Formid As String = Nothing
    Public mailsubjct As String = Nothing
    Public mailbody As String = Nothing
    Public smsbody As String = Nothing
    Public atchmnt As String = Nothing
    Public usercode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsEmailSMSSettingNew) As Boolean
        Try
            Dim qry As String = "delete from TSPL_EmailSMS_Config where form_id='" + obj.Formid + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "email_subject", obj.mailsubjct)
            clsCommon.AddColumnsForChange(coll, "email_text", obj.mailbody)
            clsCommon.AddColumnsForChange(coll, "sms_text", obj.smsbody)
            clsCommon.AddColumnsForChange(coll, "form_id", obj.Formid)
            clsCommon.AddColumnsForChange(coll, "attachment", obj.atchmnt)
            clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "user_code", obj.usercode)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EmailSMS_Config", OMInsertOrUpdate.Insert, "")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal FormId As String) As clsEmailSMSSettingNew
        Dim obj As clsEmailSMSSettingNew = Nothing

        Dim qry As String = "select email_subject,email_text,attachment,sms_text,user_code from TSPL_EmailSMS_Config where form_id='" + FormId + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsEmailSMSSettingNew
            obj.mailsubjct = clsCommon.myCstr(dt.Rows(0)("email_subject"))
            obj.mailbody = clsCommon.myCstr(dt.Rows(0)("email_text"))
            obj.smsbody = clsCommon.myCstr(dt.Rows(0)("sms_text"))
            obj.atchmnt = clsCommon.myCstr(dt.Rows(0)("attachment"))
            obj.usercode = clsCommon.myCstr(dt.Rows(0)("user_code"))
        End If

        Return obj

    End Function
End Class

Public Class clsCheckMailSetting
#Region "Variable"

    Public IsSendMail As String = Nothing
#End Region

    Public Shared Function CheckMailRight() As clsCheckMailSetting
        Dim obj As New clsCheckMailSetting()
        Dim qry As String = "select description from TSPL_FIXED_PARAMETER where code='MAILOFF'"
        obj.IsSendMail = clsDBFuncationality.getSingleValue(qry)

        If obj.IsSendMail = "1" Then
            obj.IsSendMail = "YES"
        Else
            obj.IsSendMail = "NO"
        End If
        Return obj
    End Function
End Class
