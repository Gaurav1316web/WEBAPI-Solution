Imports System.Data.SqlClient

Public Class ClsNotification
#Region "Variables"
    Public Code As String = Nothing
    Public Document_Date As DateTime
    Public Start_Date As Date
    Public End_Date As Date? = Nothing
    Public Subject As String
    Public Description As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Login_Type As String = Nothing
    Public Arr As List(Of clsNotificationDetails)

#End Region

    Public Function SaveData(ByVal obj As ClsNotification, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As ClsNotification, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_NOTIFICATIONS_USER_TYPE where Document_No='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Subject", obj.Subject)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Document_Date", obj.Document_Date)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            End If
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmNotification, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFICATIONS", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFICATIONS", OMInsertOrUpdate.Update, "TSPL_NOTIFICATIONS.Document_No='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso clsNotificationDetails.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsNotification
        Dim obj As ClsNotification = Nothing

        Try
            Dim strQry As String = "SELECT Document_No as Code,Document_Date as Date,Start_Date,End_Date,Subject,Description,Status FROM TSPL_NOTIFICATIONS where 1=1 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Document_No = (select MIN(Code) from TSPL_NOTIFICATIONS where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Document_No = (Select Max(Code) from TSPL_NOTIFICATIONS where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Document_No = (Select Min(Code) from TSPL_NOTIFICATIONS where Document_No>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Document_No = (select Max(Code) from TSPL_NOTIFICATIONS where Document_No<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Document_No = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsNotification()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Date"))
                obj.Start_Date = clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                    obj.End_Date = clsCommon.GetPrintDate(dt.Rows(0)("End_Date"), "dd/MMM/yyyy")

                End If
                obj.Subject = clsCommon.myCstr(dt.Rows(0)("Subject"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Arr = clsNotificationDetails.GetData(obj.Code, trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As ClsNotification = ClsNotification.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFICATIONS", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(StrCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_NOTIFICATIONS_USER_TYPE where Document_No='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_NOTIFICATIONS where Document_No='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    'Public Function attachmentCount()
    '    Try
    '        Dim obj As ClsNotification
    '        Dim AttachCount As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(1) FROM TSPL_ATTACHMENTS WHERE TransactionId='" & obj.Code & "'")
    '        Dim sql As String = "UPDATE TSPL_NOTIFICATIONS SET Attachment_Count = '" & AttachCount & "' where Document_No = '" & obj.Code & "'"
    '        clsDBFuncationality.ExecuteNonQuery(sql)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
End Class

Public Class clsNotificationDetails
    Public PK_Id As String = Nothing
    Public SNO As String = Nothing
    Public Document_No As String = Nothing
    Public Login_Type As String = Nothing

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsNotificationDetails), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                'For Each obj As clsNotificationDetails In Arr
                For i = 0 To Arr.Count - 1
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Document_No", strCode)
                    clsCommon.AddColumnsForChange(colm, "SNO", i + 1)
                    'clsCommon.AddColumnsForChange(colm, "Login_Type", obj.Login_Type)
                    clsCommon.AddColumnsForChange(colm, "Login_Type", Arr.Item(i).Login_Type)
                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_NOTIFICATIONS_USER_TYPE", OMInsertOrUpdate.Insert, "", trans)
                Next
                'Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsNotificationDetails)
        Dim arr As List(Of clsNotificationDetails) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select PK_Id,SNO,Document_No,Login_Type from TSPL_NOTIFICATIONS_USER_TYPE where Document_No='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsNotificationDetails)
                Dim objTr As clsNotificationDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsNotificationDetails
                    objTr.PK_Id = clsCommon.myCstr(dr("PK_Id"))
                    objTr.SNO = clsCommon.myCstr(dr("SNO"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Login_Type = clsCommon.myCstr(dr("Login_Type"))
                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

End Class
