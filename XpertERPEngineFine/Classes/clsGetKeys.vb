Imports common
Public Class clsGetKeys
    Public Shared Function GetUniqueKeyName(ByVal TableName As String, ByVal ArrColumns As ArrayList) As String
        Dim UKName As String = ""
        Try
            If ArrColumns Is Nothing OrElse ArrColumns.Count <= 0 Then
                Throw New Exception("Please provide Column ")
            End If
            Dim sQuery = "select unique_key_name from (
SELECT k.name AS unique_key_name
FROM sys.key_constraints k
INNER JOIN sys.index_columns ic ON k.parent_object_id = ic.object_id AND k.unique_index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE k.type = 'UQ' 
AND k.parent_object_id = OBJECT_ID('" + TableName + "') 
AND c.name IN (" + clsCommon.GetMulcallString(ArrColumns) + ")
)xx group by unique_key_name "
            UKName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sQuery))
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return UKName
    End Function
End Class

Public Class clsGridColumn
    Public Const colSelect As String = "colSelect"
    Public Const colSNo As String = "colSlno"
End Class
