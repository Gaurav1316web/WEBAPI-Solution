Namespace BAL


    Public Class BALLoadOut
        Public Function FillLineItems(ByVal Start As Date) As DataTable
            Try
                Return (New DAL.DALLoadOut).FillLineItems(Start)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace

