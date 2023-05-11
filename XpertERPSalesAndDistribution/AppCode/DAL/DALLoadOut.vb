Imports System.Data.SqlClient
Imports XpertERPEngine
Namespace DAL
    Public Class DALLoadOut
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim dt As DataTable
        Dim da As SqlDataAdapter
        Public Function FillLineItems(ByVal Start As Date) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("FillLineItems", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Start", Start)
                        dt = New DataTable()
                        da = New SqlDataAdapter(cmd)
                        da.Fill(dt)
                        Return dt
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try

            End Using
        End Function


    End Class
End Namespace

