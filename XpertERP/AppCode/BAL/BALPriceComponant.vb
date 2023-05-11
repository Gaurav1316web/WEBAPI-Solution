Imports System.Threading
Imports System.Globalization

Namespace BAL
    Public Class BALPriceComponant
        Public Sub BALPriceComponant()
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        End Sub



        Public Function FillGLAccDesc(ByVal Code As String) As String

            Try
                Return (New DAL.DALPriceComponant).FillGLAccDesc(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPriceComponantMaster(ByVal Code As String) As DataTable

            Try
                Return (New DAL.DALPriceComponant).GetPriceComponantMaster(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetAbtMaster(ByVal Code As String) As DataTable

            Try
                Return (New DAL.DALPriceComponant).GetAbtMaster(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetAbtMasterOnDate(ByVal Start As Date) As DataTable

            Try
                Return (New DAL.DALPriceComponant).GetAbtMasterOnDate(Start)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPCMMaster(ByVal Code As String) As DataTable

            Try
                Return (New DAL.DALPriceComponant).GetPCMMaster(Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function NotValidate(ByVal fromdate As Date, ByVal todate As Date, ByVal Code As String) As Boolean

            Try
                Return (New DAL.DALPriceComponant).NotValidate(fromdate, todate, Code)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPCMaster() As DataTable

            Try
                Return (New DAL.DALPriceComponant).GetPCMaster()

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub InsertAbtMaster(ByVal Code As String, ByVal desc As String, ByVal fromdate As Date, ByVal todate As Date, ByVal rate As Decimal, ByVal CurrentUser As String, ByVal CurrentCompany As String)

            Try
                'Return (New DAL.DALPriceComponant).InsertAbtMaster(Code, desc, fromdate, todate, rate, CurrentUser, CurrentCompany)
                Dim obj As New DAL.DALPriceComponant
                obj.InsertAbtMaster(Code, desc, fromdate, todate, rate, CurrentUser, CurrentCompany)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub UpdateAbtMaster(ByVal Code As String, ByVal desc As String, ByVal fromdate As Date, ByVal todate As Date, ByVal rate As Decimal, ByVal CurrentUser As String, ByVal CurrentCompany As String)

            Try
                'Return (New DAL.DALPriceComponant).UpdateAbtMaster(Code, desc, fromdate, todate, rate, CurrentUser, CurrentCompany)
                Dim objx As New DAL.DALPriceComponant()
                objx.UpdateAbtMaster(Code, desc, fromdate, todate, rate, CurrentUser, CurrentCompany)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'Public Function InsertPriceComponantMaster(ByVal Code As String, ByVal Desc As String, ByVal GLAcc As String, ByVal UserName As String, ByVal Company As String)

        '    Try
        '        Return (New DAL.DALPriceComponant).InsertPriceComponantMaster(Code, Desc, GLAcc, UserName, Company)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function
        Public Sub InsertPCMMaster(ByVal PriceCode As String, ByVal PriceDesc As String, ByVal Remarks As String, ByVal PCMCode As String, ByVal PCMDesc As String, ByVal Amount As Decimal, ByVal Type As String, ByVal CurrentUser As String, ByVal CurrentCompany As String)

            Try
                'Return (New DAL.DALPriceComponant).InsertPCMMaster(PriceCode, PriceDesc, Remarks, PCMCode, PCMDesc, Amount, Type, CurrentUser, CurrentCompany)
                Dim obj As New DAL.DALPriceComponant
                obj.InsertPCMMaster(PriceCode, PriceDesc, Remarks, PCMCode, PCMDesc, Amount, Type, CurrentUser, CurrentCompany)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub UpdatePriceComponantMaster(ByVal Code As String, ByVal Desc As String, ByVal GLAcc As String, ByVal UserName As String, ByVal Company As String)

            Try
                'Return (New DAL.DALPriceComponant).UpdatePriceComponantMaster(Code, Desc, GLAcc, UserName, Company)
                Dim obj As New DAL.DALPriceComponant
                obj.UpdatePriceComponantMaster(Code, Desc, GLAcc, UserName, Company)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub DeletePriceComponantMaster(ByVal Code As String)

            Try
                'Return (New DAL.DALPriceComponant).DeletePriceComponantMaster(Code)
                Dim obj As New DAL.DALPriceComponant
                obj.DeletePriceComponantMaster(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub DeletePCMMaster(ByVal Code As String)

            Try
                'Return (New DAL.DALPriceComponant).DeletePCMMaster(Code)
                Dim obj As New DAL.DALPriceComponant
                obj.DeletePCMMaster(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub DeleteAbtMaster(ByVal Code As String)

            Try
                ' Return (New DAL.DALPriceComponant).DeleteAbtMaster(Code)
                Dim obj As New DAL.DALPriceComponant
                obj.DeleteAbtMaster(Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class
End Namespace