Imports System.Data.SqlClient
Imports common

Public Class clsCancelLog
#Region "variables"
    Public LOG_ID As Integer
    Public Program_Code As String = Nothing
    Public DOCUMENT_NO As String = Nothing
    Public REASON As String = Nothing
    Public REVERSE_DATE As Date
    Public ACTIVITY_TYPE As String = ""
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public Doc_Created_By As String = Nothing
    Public Doc_Created_Date As Date? = Nothing
    Public Doc_Posted_By As String = Nothing
    Public Doc_Posted_Date As Date? = Nothing

#End Region
#Region "Functions"

    Public Shared Function SaveData(ByVal obj As clsCancelLog, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
            clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", obj.DOCUMENT_NO)
            clsCommon.AddColumnsForChange(coll, "REASON", obj.REASON)
            clsCommon.AddColumnsForChange(coll, "REVERSE_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ACTIVITY_TYPE", obj.ACTIVITY_TYPE)

            '' DOCUMENT CREATE DATE,CREATE USER ,POST DATE AND POST USER

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            'clsCommon.AddColumnsForChange(coll, "Doc_Created_By", clsCommon.myCstr(obj.Doc_Created_By), True)
            'If Not obj.Doc_Created_Date Is Nothing Then
            '    clsCommon.AddColumnsForChange(coll, "Doc_Created_Date", clsCommon.GetPrintDate(obj.Doc_Created_Date, "dd/MMM/yyyy"), True)
            'End If


            'clsCommon.AddColumnsForChange(coll, "Doc_Posted_By", clsCommon.myCstr(obj.Doc_Posted_By), True)
            'If Not obj.Doc_Posted_Date Is Nothing Then
            '    clsCommon.AddColumnsForChange(coll, "Doc_Posted_Date", clsCommon.GetPrintDate(obj.Doc_Posted_Date, "dd/MMM/yyyy"), True)
            'End If

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss"))

                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSACTION_REVERSE_LOG", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSACTION_REVERSE_LOG", OMInsertOrUpdate.Update, "TSPL_TRANSACTION_REVERSE_LOG.LOG_ID='" + obj.LOG_ID + "'", trans)
            End If


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCancelLog
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCancelLog
        Dim obj As clsCancelLog = Nothing
        Dim Arr As List(Of clsCancelLog) = Nothing
        Dim qry As String = "select LOG_ID ,Program_Code,REASON from TSPL_TRANSACTION_REVERSE_LOG where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TRANSACTION_REVERSE_LOG.LOG_ID = (select MIN(LOG_ID) from TSPL_TRANSACTION_REVERSE_LOG WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_TRANSACTION_REVERSE_LOG.LOG_ID = (select Max(LOG_ID) from TSPL_TRANSACTION_REVERSE_LOG WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_TRANSACTION_REVERSE_LOG.LOG_ID = (select top 1 LOG_ID from TSPL_TRANSACTION_REVERSE_LOG WHERE 1=1 " + whrclas + " and LOG_ID='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_TRANSACTION_REVERSE_LOG.LOG_ID = (select Min(LOG_ID) from TSPL_TRANSACTION_REVERSE_LOG where LOG_ID>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_TRANSACTION_REVERSE_LOG.LOG_ID = (select Max(LOG_ID) from TSPL_TRANSACTION_REVERSE_LOG where LOG_ID<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCancelLog()
            obj.LOG_ID = clsCommon.myCdbl(dt.Rows(0)("LOG_ID"))
            obj.Program_Code = clsCommon.myCstr(dt.Rows(0)("Program_Code"))
            obj.REASON = clsCommon.myCstr(dt.Rows(0)("REASON"))
            obj.DOCUMENT_NO = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_NO"))
            obj.REVERSE_DATE = clsCommon.myCDate(dt.Rows(0)("REVERSE_DATE"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
        End If
        Return obj
    End Function
    Public Shared Function CheckForReasonOnDelete(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strq As String
        strq = "SELECT Description FROM TSPL_FIXED_PARAMETER where Type= 'DisplayReasonOnDelete'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Description") = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Description") = 1 Then
            Return True
        End If
        Return True
    End Function
    Public Shared Function CheckForReasonOnUpdateAfterPost(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strq As String
        strq = "SELECT Description FROM TSPL_FIXED_PARAMETER where Type= 'DisplayReasonOnUpdateAfterPost'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Description") = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Description") = 1 Then
            Return True
        End If
        Return True
    End Function
#End Region
End Class
