Namespace BAL
    Public Class BALScheme
        Public Function GetSchemeMaster(ByVal Code As String) As DataTable

            Try
                Return (New DAL.DALScheme).GetSchemeMaster(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetItemMaster() As DataTable

            Try
                Return (New DAL.DALScheme).GetItemMaster()

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetItemMaster(ByVal type As String) As DataTable

            Try
                Return (New DAL.DALScheme).GetItemMaster(type)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSchemeDetailForLoadOut(ByVal Code As String, ByVal Start As Date) As DataTable

            Try
                Return (New DAL.DALScheme).GetSchemeDetailForLoadOut(Code, Start)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSchemeDetail(ByVal Code As String) As DataTable

            Try
                Return (New DAL.DALScheme).GetSchemeDetail(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function FillItemDesc(ByVal Code As String) As String

            Try
                Return (New DAL.DALScheme).FillItemDesc(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub InsertSchemeMaster(ByVal Code As String, ByVal Type As String, ByVal Desc As String, ByVal Start As Date, ByVal EndDate As Date?, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal Amount As Decimal, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal basicPrice As Decimal, ByVal CustCategory As String, ByVal CustCategoryDesc As String)

            Try
                'Return (New DAL.DALScheme).InsertSchemeMaster(Code, Type, Desc, Start, EndDate, MainItem, Quantity, MainItemdesc, Comment, Amount, UserName, Company, UOM, mrp, basicPrice, CustCategory, CustCategoryDesc)
                Dim obj As New DAL.DALScheme
                obj.InsertSchemeMaster(Code, Type, Desc, Start, EndDate, MainItem, Quantity, MainItemdesc, Comment, Amount, UserName, Company, UOM, mrp, basicPrice, CustCategory, CustCategoryDesc)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub UpdateSchemeMaster(ByVal Code As String, ByVal Type As String, ByVal Desc As String, ByVal Start As Date, ByVal EndDate As Date?, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal Amount As Decimal, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal basicPrice As Decimal, ByVal CustCategory As String, ByVal CustCategoryDesc As String)

            Try
                'Return (New DAL.DALScheme).UpdateSchemeMaster(Code, Type, Desc, Start, EndDate, MainItem, Quantity, MainItemdesc, Comment, Amount, UserName, Company, UOM, mrp, basicPrice, CustCategory, CustCategoryDesc)
                Dim obj As New DAL.DALScheme
                obj.UpdateSchemeMaster(Code, Type, Desc, Start, EndDate, MainItem, Quantity, MainItemdesc, Comment, Amount, UserName, Company, UOM, mrp, basicPrice, CustCategory, CustCategoryDesc)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub DeleteSchemeMaster(ByVal Code As String)

            Try
                'Return (New DAL.DALScheme).DeleteSchemeMaster(Code)
                Dim obj As New DAL.DALScheme
                obj.DeleteSchemeMaster(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub DeleteSchemeDetail(ByVal Code As String)

            Try
                'Return (New DAL.DALScheme).DeleteSchemeDetail(Code)
                Dim obj As New DAL.DALScheme
                obj.DeleteSchemeDetail(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub InsertSchemeDetail(ByVal Code As String, ByVal Desc As String, ByVal MainItem As String, ByVal Quantity As Decimal, ByVal MainItemdesc As String, ByVal Comment As String, ByVal UserName As String, ByVal Company As String, ByVal UOM As String, ByVal mrp As Decimal, ByVal priceDate As Date, ByVal basicPrice As Decimal)

            Try
                ' Return (New DAL.DALScheme).InsertSchemeDetail(Code, Desc, MainItem, Quantity, MainItemdesc, Comment, UserName, Company, UOM, mrp, priceDate, basicPrice)
                Dim obj As New DAL.DALScheme
                obj.InsertSchemeDetail(Code, Desc, MainItem, Quantity, MainItemdesc, Comment, UserName, Company, UOM, mrp, priceDate, basicPrice)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub



    End Class
End Namespace
