Imports System.Data.SqlClient
Imports common

Public Class ClsOfficeOrderTemplate
    Public Code As String = Nothing
    Public Subject As String = Nothing
    Public Description As String = Nothing

    'Public Shared Sub SaveData(obj As ClsOfficeOrderTemplate, tran As SqlTransaction)
    '    Try
    '        Dim deleteSql As String = "DELETE FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = '" & obj.Code & "'"
    '        clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran)
    '        Dim insertSql As String =
    '        "INSERT INTO TSPL_OFFICE_ORDER_TEMPLATE " &
    '        "(Document_No, Subject, Description, Created_By, Created_Date, Modified_By, Modified_Date) " &
    '        "VALUES ('" & obj.Code & "', '" & obj.Subject & "', '" & obj.Description & "', 'Admin', GETDATE(), 'Admin', GETDATE())"

    '        clsDBFuncationality.ExecuteNonQuery(False, insertSql, tran)

    '    Catch ex As Exception
    '        Throw New Exception("Error saving Office Order Template: " & ex.Message)
    '    End Try
    'End Sub


    Public Shared Sub SaveData(obj As ClsOfficeOrderTemplate, tran As SqlTransaction)
        Try
            ' Delete previous record
            Dim deleteSql As String =
            "DELETE FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = '" &
            obj.Code.Replace("'", "''") & "'"

            clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran)

            ' Escape single quotes in RTF
            Dim safeSubject As String = obj.Subject.Replace("'", "''")
            Dim safeDescription As String = obj.Description.Replace("'", "''")

            ' Insert new record
            obj.Code = clsERPFuncationality.GetNextCode(tran, DateTime.Now, clsDocType.frmofficeordertemplate, "", objCommonVar.strCurrUserLocations)
            Dim insertSql As String =
            "INSERT INTO TSPL_OFFICE_ORDER_TEMPLATE " &
            "(Document_No, Subject, Description, Created_By, Created_Date, Modified_By, Modified_Date) " &
            "VALUES ('" & obj.Code.Replace("'", "''") & "', '" &
            safeSubject & "', '" &
            safeDescription & "', 'Admin', GETDATE(), 'Admin', GETDATE())"

            clsDBFuncationality.ExecuteNonQuery(False, insertSql, tran)

        Catch ex As Exception
            Throw New Exception("Error saving Office Order Template: " & ex.Message)
        End Try
    End Sub



    Public Shared Function GetData(code As String, navType As NavigatorType, tran As SqlTransaction) As ClsOfficeOrderTemplate
        Dim obj As New ClsOfficeOrderTemplate()

        Try
            Dim sql As String = "SELECT Document_No as Code, Subject, Description FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = '" & code & "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, tran)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Subject = clsCommon.myCstr(dt.Rows(0)("Subject"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            End If

        Catch ex As Exception
            Throw New Exception("Error loading Office Order Template: " & ex.Message)
        End Try

        Return obj
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


End Class


'Imports System.Data.SqlClient
'Imports common

'Public Class ClsOfficeOrderTemplate
'    Public Property Code As String
'    Public Property Subject As String   ' ← RTF text stored here
'    Public Property Description As String


'    ' ======================================
'    ' SAVE DATA (RTF SAFE USING PARAMETERS)
'    ' ======================================
'    Public Shared Sub SaveData(obj As ClsOfficeOrderTemplate, tran As SqlTransaction)
'        Try
'            ' DELETE
'            Dim deleteSql As String = "DELETE FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = @Code"
'            Dim p As New SqlParameter("@Code", obj.Code)
'            clsDBFuncationality.ExecuteNonQuery(False, deleteSql, tran)

'            ' INSERT (RTF SAFE)
'            Dim insertSql As String =
'                "INSERT INTO TSPL_OFFICE_ORDER_TEMPLATE 
'                (Document_No, Subject, Description, Created_By, Created_Date, Modified_By, Modified_Date)
'                VALUES 
'                (@Code, @Subject, @Description, 'Admin', GETDATE(), 'Admin', GETDATE())"

'            Dim param As New Hashtable()
'            param("@Code") = obj.Code
'            param("@Subject") = obj.Subject
'            param("@Description") = obj.Description
'            clsDBFuncationality.ExecuteNonQuery(insertSql, "", tran)

'        Catch ex As Exception
'            Throw New Exception("Error saving Office Order Template: " & ex.Message)
'        End Try
'    End Sub


'    ' ======================================
'    ' LOAD DATA (RTF LOADED SAFELY)
'    ' ======================================
'    Public Shared Function GetData(code As String, navType As NavigatorType, tran As SqlTransaction) As ClsOfficeOrderTemplate
'        Dim obj As New ClsOfficeOrderTemplate()

'        Try
'            Dim sql As String =
'            "SELECT Document_No AS Code, Subject, Description " &
'            "FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = @Code"

'            ' --- PARAMETER TABLE ---
'            Dim param As New Hashtable()
'            param("@Code") = code

'            ' --- CORRECT SIGNATURE ---
'            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, "", tran)

'            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
'                obj.Subject = clsCommon.myCstr(dt.Rows(0)("Subject"))
'                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
'            End If

'        Catch ex As Exception
'            Throw New Exception("Error loading Office Order Template: " & ex.Message)
'        End Try

'        Return obj
'    End Function



'    ' ======================================
'    ' PUBLIC DELETE
'    ' ======================================
'    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
'        Dim trans As SqlTransaction = Nothing
'        Try
'            trans = clsDBFuncationality.GetTransactin()

'            Dim isSaved As Boolean = DeleteData(StrCode, trans)

'            trans.Commit()
'            Return isSaved

'        Catch ex As Exception
'            If trans IsNot Nothing Then trans.Rollback()
'            Throw New Exception("Error deleting Office Order Template: " & ex.Message)
'        End Try
'    End Function


'    ' ======================================
'    ' PRIVATE DELETE WITH TRANSACTION
'    ' ======================================
'    Public Shared Function DeleteData(StrCode As String, trans As SqlTransaction) As Boolean
'        Dim isSaved As Boolean = False
'        Try
'            If clsCommon.myLen(StrCode) <= 0 Then
'                Throw New Exception("Code No. not found to Delete")
'            End If

'            Try
'                clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, StrCode, "",
'                                                        "", "TSPL_OFFICE_ORDER_TEMPLATE", "Document_No", trans)
'            Catch ex As Exception
'            End Try

'            ' DELETE QUERY WITH PARAMETERS
'            Dim qry As String = "DELETE FROM TSPL_OFFICE_ORDER_TEMPLATE WHERE Document_No = @Code"

'            Dim param As New Hashtable()
'            param("@Code") = StrCode

'            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, "", trans)

'        Catch ex As Exception
'            Throw New Exception("Error deleting Office Order Template: " & ex.Message)
'        End Try

'        Return isSaved
'    End Function

'End Class

