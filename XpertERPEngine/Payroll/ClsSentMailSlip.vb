Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsSentMailSlip
#Region "Variables"
    Public MailCode As String
    Public Emp_Code As String
    Public User_Code As String
    Public Email_Id As String
    Public PAY_PERIOD_CODE As String

#End Region
    Public Shared Function SaveData(ByVal obj As ClsSentMailSlip, ByVal objList As List(Of ClsSentMailSlipDetail), ByVal isNewEntry As Boolean)
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                obj.MailCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.SentMail, "", "")
            End If
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.MailCode) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            Else
                strDocNo = obj.MailCode
            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "MailCode", obj.MailCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SENT_MAIL_SLIP", OMInsertOrUpdate.Insert, "", trans)
            End If
            isSaved = isSaved AndAlso ClsSentMailSlipDetail.SaveData(obj.MailCode, objList, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return isSaved
    End Function
End Class
Public Class ClsSentMailSlipDetail
#Region "Variables"
    '' grid columns
    Public Emp_Code As String
    Public Email_ID As String
    Public Status As String
    Public Error_Log As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsSentMailSlipDetail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsSentMailSlipDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Email_ID", obj.Email_ID)
                clsCommon.AddColumnsForChange(coll, "MailCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "Error_Log", obj.Error_Log, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SENT_MAIL_SLIP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_SENT_MAIL_SLIP_DETAIL.MailCode='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class