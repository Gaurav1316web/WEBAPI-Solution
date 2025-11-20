Imports System.Data.SqlClient
Imports common
Public Class ClsOfficeOrder
    Public Code As String = Nothing
    Public DocumentDate As String = Nothing
    Public TemplateFinder As String = Nothing
    Public Description As String = Nothing
    Public Print As String = Nothing
    Public Subject As String = Nothing
    Public Template As String = Nothing
    Public Status As String = Nothing
    Public CreatedBy As String = Nothing
    Public ModifiedBy As String = Nothing
    Public CreatedDate As String = Nothing
    Public ModifiedDate As String = Nothing
    Public Post_Date As DateTime

    'Public Shared Sub SaveData(obj As ClsOfficeOrder, tran As SqlTransaction)
    '    Try
    '        Dim deleteSql As String = "DELETE FROM TSPL_OFFICE_ORDER WHERE Document_No = '" &
    '        obj.Code.Replace("'", "''") & "'"

    '        Dim htDelete As New Hashtable()
    '        htDelete("@Code") = obj.Code

    '        clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran)

    '        Dim safeSubject As String = obj.Subject.Replace("'", "''")
    '        Dim safeDescription As String = obj.Description.Replace("'", "''")
    '        Dim insertSql As String =
    '        "INSERT INTO TSPL_OFFICE_ORDER " &
    '        "(Document_No, Document_Date, Template_Finder, Description, Subject, Template, Status, Created_By, Created_Date, Modified_By, Modified_Date) " &
    '        "VALUES ('" & obj.Code.Replace("'", "''") & "', '" &
    '        safeSubject & "', '" &
    '        safeDescription & "', 'Admin', GETDATE(), 'Admin', GETDATE())"

    '        clsDBFuncationality.ExecuteNonQuery(False, insertSql, tran)

    '    Catch ex As Exception
    '        Throw New Exception("Error saving Office Order: " & ex.Message)
    '    End Try
    'End Sub


    Public Shared Sub SaveData(obj As ClsOfficeOrder, tran As SqlTransaction)
        Try

            Dim deleteSql As String = "DELETE FROM TSPL_OFFICE_ORDER WHERE Document_No ='" & obj.Code & "'"

            Dim htDelete As New Hashtable()
            htDelete("@Code") = obj.Code

            'clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran, htDelete)
            clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran)


            Dim safeSubject As String = obj.Subject.Replace("'", "''")
            'Dim safeDescription As String = obj.Description.Replace("'", "''")

            obj.Code = clsERPFuncationality.GetNextCode(tran, DateTime.Now, clsDocType.frmofficeorder, "", objCommonVar.strCurrUserLocations)

            'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmNotification, clsDocTransactionType.NOTIFICATIONS, objCommonVar.strCurrUserLocations)
            'If clsCommon.myLen(obj.Code) <= 0 Then
            '        Throw New Exception("Error in Code Generation")
            '    End If
            '    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
            '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            '    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NOTIFICATIONS", OMInsertOrUpdate.Insert, "", trans)




            Dim insertSql As String =
                        "INSERT INTO TSPL_OFFICE_ORDER " &
                        "(Document_No, Document_Date, Template,Subject,Description, Status, Created_By, Created_Date, Modified_By, Modified_Date) " &
                        "VALUES (" &
                        "'" & obj.Code.Replace("'", "''") & "', " &
                        "GETDATE(), " &
                        "'" & obj.Template.Replace("'", "''") & "', " &
                         "'" & obj.Subject.Replace("'", "''") & "', " &
                         "'" & obj.Description.Replace("'", "''") & "', " &
                        "0, " &
                        "'Admin', " &
                        "GETDATE(), " &
                        "'Admin', " &
                        "GETDATE()" &
                        ")"


            clsDBFuncationality.ExecuteNonQuery(False, insertSql, tran)

        Catch ex As Exception
            Throw New Exception("Error saving Office Order: " & ex.Message)
        End Try
    End Sub


    Public Shared Function GetData(code As String, navType As NavigatorType, tran As SqlTransaction) As ClsOfficeOrder
        Try
            Dim sql As String =
            "SELECT Document_No, Document_Date, Template_Finder, Description, 
                Subject, Template, Status, Created_By, Created_Date, Modified_By, Modified_Date 
         FROM TSPL_OFFICE_ORDER 
         WHERE Document_No ='" & code & "'"

            Dim ht As New Hashtable()
            ht("@Code") = code

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, ht, tran)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim row As DataRow = dt.Rows(0)
                Dim obj As New ClsOfficeOrder()

                obj.Code = row("Document_No").ToString()
                obj.DocumentDate = If(IsDBNull(row("Document_Date")), Nothing, row("Document_Date"))
                obj.TemplateFinder = row("Template_Finder").ToString()
                obj.Description = row("Description").ToString()
                'obj.Print = row("Print").ToString()
                obj.Subject = row("Subject").ToString()
                obj.Template = row("Template").ToString()
                obj.Status = row("Status").ToString()
                obj.CreatedBy = row("Created_By").ToString()
                obj.CreatedDate = If(IsDBNull(row("Created_Date")), Nothing, row("Created_Date"))
                obj.ModifiedBy = row("Modified_By").ToString()
                obj.ModifiedDate = If(IsDBNull(row("Modified_Date")), Nothing, row("Modified_Date"))

                Return obj
            End If

            Return Nothing

        Catch ex As Exception
            Throw New Exception("Error loading Office Order: " & ex.Message)
        End Try
    End Function


    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            Dim isSaved As Boolean = DeleteData(StrCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            If trans IsNot Nothing Then
                trans.Rollback()
            End If
            Throw New Exception("Error deleting Office Order Template: " & ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If clsCommon.myLen(StrCode) <= 0 Then
                Throw New Exception("Code No. not found to Delete")
            End If

            Try
                clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, StrCode, "", "", "TSPL_OFFICE_ORDER_TEMPLATE", "Document_No", trans)
            Catch ex As Exception
            End Try

            Dim qry As String = "DELETE FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = '" & StrCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(False, qry, trans)

        Catch ex As Exception
            Throw New Exception("Error deleting Office Order Template: " & ex.Message)
        End Try

        Return isSaved
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
            Dim obj As ClsOfficeOrder = ClsOfficeOrder.GetData(strDocNo, NavigatorType.Current, trans)

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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OFFICE_ORDER", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Code) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_OFFICE_ORDER", "Document_No", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
