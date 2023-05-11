Namespace BAL

    Public Class BALPrice
        Public Function FillPriceCatDesc(ByVal Code As String) As String
            Try
                Return (New DAL.DALPrice).FillPriceCatDesc(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function chkForExistItemPrice(ByVal Code As String, ByVal Start As Date, ByVal MRP As Decimal) As Boolean
            Try
                Return (New DAL.DALPrice).chkForExistItemPrice(Code, Start, MRP)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetTaxDetail(ByVal Code As String) As DataTable
            Try
                Return (New DAL.DALPrice).GetTaxDetail(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Public Function InsertItemPrice(ByVal cmdText As String)
        '    Try
        '        Return (New DAL.DALPrice).InsertItemPrice(cmdText)
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function
        'Public Function DeleteItemPrice(ByVal Code As String)
        '    Try
        '        Return (New DAL.DALPrice).DeleteItemPrice(Code)
        '    Catch ex As Exception
        '        Throw x
        '    End Try
        'End Function
        Public Function GetPriceListForItem(ByVal Code As String)
            Try
                Return (New DAL.DALPrice).GetPriceListForItem(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


    End Class

End Namespace