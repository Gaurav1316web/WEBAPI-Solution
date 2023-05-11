Imports System.Data.SqlClient
Imports XpertERPEngine
Namespace DAL
    Public Class DALScheme
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim dt As DataTable
        Dim da As SqlDataAdapter

        Public Function GetItemMaster() As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetItemMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure


                        dt = New DataTable()
                        da = New SqlDataAdapter(cmd)
                        dt.Clear()
                        da.Fill(dt)
                        Return dt
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetItemMaster(ByVal type As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetItemMasterByType", con)

                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Type", type)

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

        Public Function GetSchemeMaster(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetSchemeMaster", con)
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
        Public Sub DeleteSchemeMaster(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeleteSchemeMaster", con)
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
        Public Sub DeleteSchemeDetail(ByVal Code As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("DeleteSchemeDetail", con)
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
        Public Sub InsertSchemeMaster(ByVal Code As String, ByVal Type As String, ByVal Desc As String, ByVal Start As Date, ByVal EndDate As Date?, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal Amount As Decimal, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal basicPrice As Decimal, ByVal CustCategory As String, ByVal CustCategoryDesc As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("InsertSchemeMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Type", Type)
                        cmd.Parameters.AddWithValue("@Desc", Desc)
                        cmd.Parameters.AddWithValue("@Start", Start)
                        cmd.Parameters.AddWithValue("@EndDate", EndDate)
                        cmd.Parameters.AddWithValue("@MainItem", MainItem)
                        cmd.Parameters.AddWithValue("@Quantity", Quantity)
                        cmd.Parameters.AddWithValue("@MainItemdesc", MainItemdesc)
                        cmd.Parameters.AddWithValue("@UOM", UOM)
                        cmd.Parameters.AddWithValue("@Comment", Comment)
                        cmd.Parameters.AddWithValue("@Amount", Amount)
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName)
                        cmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", Company)
                        cmd.Parameters.AddWithValue("@MRP", mrp)
                        cmd.Parameters.AddWithValue("@Item_Basic_Price", basicPrice)
                        cmd.Parameters.AddWithValue("@Cust_Cate", CustCategory)
                        cmd.Parameters.AddWithValue("@Cust_Cate_desc", CustCategoryDesc)
                        cmd.ExecuteNonQuery()

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub UpdateSchemeMaster(ByVal Code As String, ByVal Type As String, ByVal Desc As String, ByVal Start As Date, ByVal EndDate As Date?, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal Amount As Decimal, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal basicPrice As Decimal, ByVal CustCategory As String, ByVal CustCategoryDesc As String)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("UpdateSchemeMaster", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Type", Type)
                        cmd.Parameters.AddWithValue("@Desc", Desc)
                        cmd.Parameters.AddWithValue("@Start", Start)
                        cmd.Parameters.AddWithValue("@EndDate", EndDate)
                        cmd.Parameters.AddWithValue("@MainItem", MainItem)
                        cmd.Parameters.AddWithValue("@Quantity", Quantity)
                        cmd.Parameters.AddWithValue("@MainItemdesc", MainItemdesc)
                        cmd.Parameters.AddWithValue("@UOM", UOM)
                        cmd.Parameters.AddWithValue("@Comment", Comment)
                        cmd.Parameters.AddWithValue("@Amount", Amount)
                        cmd.Parameters.AddWithValue("@UpdatedBy", UserName)
                        cmd.Parameters.AddWithValue("@UpdatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", Company)
                        cmd.Parameters.AddWithValue("@MRP", mrp)
                        cmd.Parameters.AddWithValue("@Item_Basic_Price", basicPrice)
                        cmd.Parameters.AddWithValue("@Cust_Cate", CustCategory)
                        cmd.Parameters.AddWithValue("@Cust_Cate_desc", CustCategoryDesc)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Sub InsertSchemeDetail(ByVal Code As String, ByVal Desc As String, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal priceDate As Date, ByVal basicPrice As Decimal)
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("InsertSchemeDetail", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
                        cmd.Parameters.AddWithValue("@Desc", Desc)
                        cmd.Parameters.AddWithValue("@MainItem", MainItem)
                        cmd.Parameters.AddWithValue("@Quantity", Quantity)
                        cmd.Parameters.AddWithValue("@MainItemdesc", MainItemdesc)
                        cmd.Parameters.AddWithValue("@UOM", UOM)
                        cmd.Parameters.AddWithValue("@Comment", Comment)
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName)
                        cmd.Parameters.AddWithValue("@CreatedDate", Date.Now)
                        cmd.Parameters.AddWithValue("@Company", Company)
                        cmd.Parameters.AddWithValue("@MRP", mrp)
                        cmd.Parameters.AddWithValue("@Price_Date", priceDate)
                        cmd.Parameters.AddWithValue("@Item_Basic_Price", basicPrice)
                        cmd.ExecuteNonQuery()

                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Sub
        Public Function GetSchemeDetail(ByVal Code As String) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetSchemeDetail", con)
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
        Public Function GetSchemeDetailForLoadOut(ByVal Code As String, ByVal Start As Date) As DataTable
            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("GetSchemeDetailForLoadOut", con)
                        con.Open()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Code", Code)
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
        Public Function FillItemDesc(ByVal Code As String) As String

            Using con As New SqlConnection(connectSql.SqlCon())
                Try
                    Using cmd As New SqlCommand("FillItemDesc", con)
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

    End Class

End Namespace
