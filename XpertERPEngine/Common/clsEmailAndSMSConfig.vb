Imports common
Imports System.Data.SqlClient
Public Class clsEmailAndSMSConfig
#Region "Variables"
    Public EMail_SMTP_Client As String = Nothing
    Public EMail_Subject As String = Nothing
    Public EMail_Port As String = Nothing
    Public EMail_ID As String = Nothing
    Public EMail_Pwd As String = Nothing
    Public EMail_Enabel_SSL As Boolean = False
    Public EMail_Text As String = Nothing
    Public SMS_String As String = Nothing
    Public SMS_User_Name As String = Nothing
    Public SMS_Pwd As String = Nothing
    Public SMS_Sender_Name As String = Nothing
    Public SMS_Mobile_no As String = Nothing
    Public SMS_Text As String = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsEmailAndSMSConfig) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_EmailSMS_Config"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMail_SMTP_Client", obj.EMail_SMTP_Client)
            clsCommon.AddColumnsForChange(coll, "EMail_Port", obj.EMail_Port)
            clsCommon.AddColumnsForChange(coll, "EMail_Subject", obj.EMail_Subject)
            clsCommon.AddColumnsForChange(coll, "EMail_ID", obj.EMail_ID)
            clsCommon.AddColumnsForChange(coll, "EMail_Pwd", obj.EMail_Pwd)
            clsCommon.AddColumnsForChange(coll, "EMail_Enabel_SSL", IIf(obj.EMail_Enabel_SSL, 1, 0))
            clsCommon.AddColumnsForChange(coll, "EMail_Text", obj.EMail_Text)
            clsCommon.AddColumnsForChange(coll, "SMS_String", obj.SMS_String)
            clsCommon.AddColumnsForChange(coll, "SMS_User_Name", obj.SMS_User_Name)
            clsCommon.AddColumnsForChange(coll, "SMS_Pwd", obj.SMS_Pwd)
            clsCommon.AddColumnsForChange(coll, "SMS_Sender_Name", obj.SMS_Sender_Name)
            clsCommon.AddColumnsForChange(coll, "SMS_Mobile_no", obj.SMS_Mobile_no)
            clsCommon.AddColumnsForChange(coll, "SMS_Text", obj.SMS_Text)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EmailSMS_Config", OMInsertOrUpdate.Insert, "")
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData() As clsEmailAndSMSConfig
        Dim obj As clsEmailAndSMSConfig = Nothing
        Dim qry As String = "SELECT * from TSPL_EmailSMS_Config"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsEmailAndSMSConfig()
            obj.EMail_SMTP_Client = clsCommon.myCstr(dt.Rows(0)("EMail_SMTP_Client"))
            obj.EMail_Port = clsCommon.myCstr(dt.Rows(0)("EMail_Port"))
            obj.EMail_ID = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
            obj.EMail_Pwd = clsCommon.myCstr(dt.Rows(0)("EMail_Pwd"))
            obj.EMail_Enabel_SSL = IIf(clsCommon.myCdbl(dt.Rows(0)("EMail_Enabel_SSL")) = 1, True, False)
            obj.EMail_Text = clsCommon.myCstr(dt.Rows(0)("EMail_Text"))
            obj.SMS_String = clsCommon.myCstr(dt.Rows(0)("SMS_String"))
            obj.SMS_User_Name = clsCommon.myCstr(dt.Rows(0)("SMS_User_Name"))
            obj.SMS_Pwd = clsCommon.myCstr(dt.Rows(0)("SMS_Pwd"))
            obj.SMS_Sender_Name = clsCommon.myCstr(dt.Rows(0)("SMS_Sender_Name"))
            obj.SMS_Mobile_no = clsCommon.myCstr(dt.Rows(0)("SMS_Mobile_no"))
            obj.SMS_Text = clsCommon.myCstr(dt.Rows(0)("SMS_Text"))
            obj.EMail_Subject = clsCommon.myCstr(dt.Rows(0)("EMail_Subject"))
        End If
        Return obj
    End Function

End Class


Public Class clsEmailSMSRecipients
#Region "Variables"
    Public Transaction_Type As String = Nothing
    Public Emp_code As String = Nothing
    Public Is_Send_Email As Boolean = 0
    Public Is_Send_SMS As Boolean = 0
    Public To_Or_CC As String = 0
    Public BDay_Anniversary_Date As Date?
    Public Trans_Date As Date?
    Public Email_Body As String = Nothing
    Public SMS_Body As String = Nothing

    Public Emp_Name As String = Nothing
    Public EMail_ID As String = Nothing
    Public Phone As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsEmailSMSRecipients), ByVal strCode As String) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_EmailSMS_Recipients  where Transaction_Type='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            For Each obj As clsEmailSMSRecipients In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Transaction_Type", strCode)
                clsCommon.AddColumnsForChange(coll, "Emp_code", obj.Emp_code)
                clsCommon.AddColumnsForChange(coll, "Is_Send_Email", IIf(obj.Is_Send_Email, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_Send_SMS", IIf(obj.Is_Send_SMS, 1, 0))
                clsCommon.AddColumnsForChange(coll, "To_Or_CC", obj.To_Or_CC)

                If Not obj.BDay_Anniversary_Date Is Nothing Then
                    clsCommon.AddColumnsForChange(coll, "BDay_Anniversary_Date", clsCommon.GetPrintDate(obj.BDay_Anniversary_Date, "dd/MMM/yyyy"))
                End If
                If Not obj.Trans_Date Is Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(obj.Trans_Date, "dd/MMM/yyyy"))
                End If

                clsCommon.AddColumnsForChange(coll, "Email_Body", obj.Email_Body, True)
                clsCommon.AddColumnsForChange(coll, "SMS_Body", obj.SMS_Body, True)
                clsCommon.AddColumnsForChange(coll, "EMail_ID", obj.EMail_ID, True)
                clsCommon.AddColumnsForChange(coll, "Phone", obj.Phone, True)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EmailSMS_Recipients", OMInsertOrUpdate.Insert, "")
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsEmailSMSRecipients)
        Dim Arr As List(Of clsEmailSMSRecipients) = Nothing
        Dim qry As String = "select TSPL_EmailSMS_Recipients.Transaction_Type,TSPL_EmailSMS_Recipients.Emp_code,TSPL_EmailSMS_Recipients.Is_Send_Email,TSPL_EmailSMS_Recipients.Is_Send_SMS,TSPL_EmailSMS_Recipients.To_Or_CC,TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_EMPLOYEE_MASTER.EMail_ID,TSPL_EMPLOYEE_MASTER.Phone from TSPL_EmailSMS_Recipients left  outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EmailSMS_Recipients.Emp_code where Transaction_Type='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr = New List(Of clsEmailSMSRecipients)
            For Each dr As DataRow In dt.Rows
                Dim obj As clsEmailSMSRecipients = New clsEmailSMSRecipients()
                obj.Transaction_Type = clsCommon.myCstr(dr("Transaction_Type"))
                obj.Emp_code = clsCommon.myCstr(dr("Emp_code"))

                obj.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                obj.EMail_ID = clsCommon.myCstr(dr("EMail_ID"))
                obj.Phone = clsCommon.myCstr(dr("Phone"))

                obj.Is_Send_Email = IIf(clsCommon.myCdbl(dr("Is_Send_Email")) = 1, True, False)
                obj.Is_Send_SMS = IIf(clsCommon.myCdbl(dr("Is_Send_SMS")) = 1, True, False)
                obj.To_Or_CC = clsCommon.myCstr(dr("To_Or_CC"))
                If Not IsDBNull(dr("BDay_Anniversary_Date")) Then
                    obj.BDay_Anniversary_Date = clsCommon.myCDate(dr("BDay_Anniversary_Date"))
                End If
                If Not IsDBNull(dr("Trans_Date")) Then
                    obj.Trans_Date = clsCommon.myCDate(dr("Trans_Date"))
                End If
                obj.Email_Body = clsCommon.myCstr(dr("Email_Body"))
                obj.SMS_Body = clsCommon.myCstr(dr("SMS_Body"))

                Arr.Add(obj)
            Next
        End If
        Return Arr
    End Function
End Class
