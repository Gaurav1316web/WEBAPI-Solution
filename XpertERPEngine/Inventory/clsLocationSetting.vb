Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Imports Microsoft.Office.Interop

Public Class clsLocationSetting
    Public Shared Function GetData() As ArrayList
        Try
            Dim qry As String = "Select User_Code from TSPL_LOCATION_SETTING"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUser As New ArrayList
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUser.Add(dr("User_Code").ToString())
                Next
            End If
            Return lstUser
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Shared Function SaveUsersForLocationSettings(ByVal lstUsers As List(Of String)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If lstUsers.Count = 0 Then
                Throw New Exception("No Settings Found to Save")
            End If

            Dim qry As String = "delete from TSPL_LOCATION_SETTING"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For Each user As String In lstUsers
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", user)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_SETTING", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
            Return isSaved

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

End Class
