Imports System.Data.SqlClient
Imports XpertERPEngine
Namespace DAL


    Public Class DALPriceComponant
        Dim dt As DataTable
        Dim da As SqlDataAdapter
        Dim cmd As SqlCommand
        Dim con As SqlConnection

        Public Function FillGLAccDesc(ByVal Code As String) As String
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("FillGLAccDesc", con)
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
        Public Function GetPriceComponantMaster(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetPriceComponantMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@Code", Code)
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
        Public Function GetPCMMaster(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetPCMMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@Code", Code)
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
        Public Function GetAbtMaster(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetAbtMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@Code", Code)
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
        Public Function GetAbtMasterOnDate(ByVal Start As Date) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetAbtMasterOnDate", con)
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
        Public Function NotValidate(ByVal fromdate As Date, ByVal todate As Date, ByVal Code As String) As Boolean
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("NotValidate", con)
                        con.Open()
                        Dim result As Boolean = True
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@fromdate", fromdate)
                        cmd.Parameters.AddWithValue("@todate", todate)
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@result", True)
                        cmd.Parameters("@result").Direction = ParameterDirection.Output
                        cmd.ExecuteNonQuery()
                        result = cmd.Parameters("@result").Value
                        Return result

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try

            End Using
        End Function
        Public Function GetPCMaster() As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetPCMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure


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
        'Public Function InsertPriceComponantMaster(ByVal Code As String, ByVal Desc As String, ByVal GLAcc As String, ByVal UserName As String, ByVal Company As String)
        '    Using con As New SqlConnection(connectSql.SqlCon())
        '        Try
        '            Using cmd As New SqlCommand("InsertPriceComponantMaster", con)
        '                con.Open()
        '                cmd.CommandType = CommandType.StoredProcedure
        '                cmd.Parameters.AddWithValue("@Code", Code)
        '                cmd.Parameters.AddWithValue("@Desc", Desc)
        '                cmd.Parameters.AddWithValue("@GLAcc", GLAcc)
        '                cmd.Parameters.AddWithValue("@CreatedBy", UserName)
        '                cmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
        '                cmd.Parameters.AddWithValue("@Company", Company)
        '                cmd.ExecuteNonQuery()

        '            End Using
        '        Catch ex As Exception
        '            Throw ex
        '        End Try
        '    End Using
        'End Function
        Public Sub InsertAbtMaster(ByVal Code As String, ByVal desc As String, ByVal fromdate As Date, ByVal todate As Date, ByVal rate As Decimal, ByVal CurrentUser As String, ByVal CurrentCompany As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("InsertAbtMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Desc", desc)
                        cmd.Parameters.AddWithValue("@fromdate", fromdate)
                        cmd.Parameters.AddWithValue("@todate", todate)
                        cmd.Parameters.AddWithValue("@rate", rate)
                        cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser)
                        cmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", CurrentCompany)
                        cmd.ExecuteNonQuery()

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub UpdateAbtMaster(ByVal Code As String, ByVal desc As String, ByVal fromdate As Date, ByVal todate As Date, ByVal rate As Decimal, ByVal CurrentUser As String, ByVal CurrentCompany As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("UpdateAbtMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Desc", desc)
                        cmd.Parameters.AddWithValue("@fromdate", fromdate)
                        cmd.Parameters.AddWithValue("@todate", todate)
                        cmd.Parameters.AddWithValue("@rate", rate)
                        cmd.Parameters.AddWithValue("@UpdatedBy", CurrentUser)
                        cmd.Parameters.AddWithValue("@UpdatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", CurrentCompany)
                        cmd.ExecuteNonQuery()

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub InsertPCMMaster(ByVal PriceCode As String, ByVal PriceDesc As String, ByVal Remarks As String, ByVal PCMCode As String, ByVal PCMDesc As String, ByVal Amount As Decimal, ByVal Type As String, ByVal CurrentUser As String, ByVal CurrentCompany As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("InsertPCMMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@PriceCode", PriceCode)
                        cmd.Parameters.AddWithValue("@PriceDesc", PriceDesc)
                        cmd.Parameters.AddWithValue("@Remarks", Remarks)
                        cmd.Parameters.AddWithValue("@PCMCode", PCMCode)
                        cmd.Parameters.AddWithValue("@PCMDesc", PCMDesc)
                        cmd.Parameters.AddWithValue("@Amount", Amount)
                        cmd.Parameters.AddWithValue("@Type", Type)
                        cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser)
                        cmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", CurrentCompany)
                        cmd.ExecuteNonQuery()

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub UpdatePriceComponantMaster(ByVal Code As String, ByVal Desc As String, ByVal GLAcc As String, ByVal UserName As String, ByVal Company As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("UpdatePriceComponantMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Desc", Desc)
                        cmd.Parameters.AddWithValue("@GLAcc", GLAcc)
                        cmd.Parameters.AddWithValue("@UpdatedBy", UserName)
                        cmd.Parameters.AddWithValue("@UpdatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", Company)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub DeletePriceComponantMaster(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeletePriceComponantMaster", con)
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
        Public Sub DeletePCMMaster(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeletePCMMaster", con)
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
        Public Sub DeleteAbtMaster(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeleteAbtMaster", con)
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
