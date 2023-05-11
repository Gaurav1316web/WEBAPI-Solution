Imports common
Imports System.Data.SqlClient

Public Class clsSaleOrder
    Dim trans As SqlTransaction
    Dim sql As String
    Public Shared Function PostData(ByVal StrDocNo As String)
        Dim con As SqlConnection = connectSql.OpenConnection()
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim qry As String = "UPDATE TSPL_SALES_ORDER_HEAD SET Is_Post='Y' WHERE Order_No='" + clsCommon.myCstr(StrDocNo) + "'"
            connectSql.RunSqlTransaction(trans, qry)
            trans.Commit()
            Return True
        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return False
        Finally
            con.Close()
        End Try
    End Function

End Class
