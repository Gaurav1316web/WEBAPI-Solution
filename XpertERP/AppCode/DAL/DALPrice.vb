Imports System.Data.SqlClient

Namespace DAL


    Public Class DALPrice
        Dim dt As DataTable
        Dim da As SqlDataAdapter
        Dim cmd As SqlCommand
        Dim con As SqlConnection

        Public Function FillPriceCatDesc(ByVal Code As String) As String
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("FillPriceCatDesc", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        Dim dr As SqlDataReader
                        dr = cmd.ExecuteReader()
                        dr.Read()
                        Dim desc As String = dr(0).ToString()
                        dr.Close()
                        Return desc
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try


            End Using
        End Function

        Public Function GetTaxDetail(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetTaxDetail", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        da = New SqlDataAdapter(cmd)
                        dt = New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetPriceListForItem(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetPriceListForItem", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        da = New SqlDataAdapter(cmd)
                        dt = New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function chkForExistItemPrice(ByVal Code As String, ByVal Start As Date, ByVal MRP As Decimal) As Boolean
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("chkForExistItemPrice", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Start", Start)
                        cmd.Parameters.AddWithValue("@MRP", MRP)
                        cmd.Parameters.AddWithValue("@result", 0)
                        cmd.Parameters("@result").Direction = ParameterDirection.Output
                        cmd.ExecuteNonQuery()
                        Return cmd.Parameters("@result").Value
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Sub InsertItemPrice(ByVal cmdText As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try

                    Using cmd As New SqlCommand(cmdText, con)
                        con.Open()
                        cmd.CommandText = "SET DATEFORMAT dmy"
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = cmdText
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub DeleteItemPrice(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeleteItemPrice", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Sub

    End Class
End Namespace