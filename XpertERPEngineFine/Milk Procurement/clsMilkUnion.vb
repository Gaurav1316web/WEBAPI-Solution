Imports System.Data.SqlClient
Public Class clsMilkUnion
    Public Shared Function UnionDBName1(ByVal arr As ArrayList) As DataTable
        Dim Whr As String = ""
        If clsCommon.myLen(arr) > 0 Then
            Dim arrValues As String = String.Join("','", arr.Cast(Of String)().ToArray())
            Whr = " and DataBase_Name IN ('" + arrValues + "') "
        End If
        Dim Qry As String = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 " + Whr + "  ORDER BY [TSPL_APP_LOCATION].Location_Name"
        Return clsDBFuncationality.GetDataTable(Qry)
    End Function
    Public Shared Function UnionDBName() As DataTable

        Dim Qry As String = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1  ORDER BY [TSPL_APP_LOCATION].Location_Name"
        Return clsDBFuncationality.GetDataTable(Qry)
    End Function
End Class
