Imports System.Data.SqlClient

Public Class clsItemSalePurchaseSetDetail
#Region "Variable"
    Public Item_Code As String = ""
    Public PurchaseSetCode As String = ""
    Public SalesSetCode As String = ""
    Public SaleAccount As String = ""
    Public SaleReturn As String = ""
    Public COGS As String = ""
#End Region

    Public Shared Function UpdateData(ByVal Arr As List(Of clsItemSalePurchaseSetDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemSalePurchaseSetDetail In Arr
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        Dim qry As String = ""
                        If clsCommon.myLen(obj.SaleReturn) > 0 Then
                            ',Weight_UOM = '" & obj.Weight_UOM & "'
                            qry = "update tspl_sales_accounts set Sales_Return_Account='" & obj.SaleReturn & "'" &
                                 " where Sales_Class_Code='" & obj.SalesSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.COGS) > 0 Then
                            qry = "update tspl_sales_accounts set COGT_AC='" & obj.COGS & "'" &
                          " where Sales_Class_Code='" & obj.SalesSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.SaleAccount) > 0 Then
                            qry = "update tspl_sales_accounts set Sales_Account='" & obj.SaleAccount & "'" &
                          " where Sales_Class_Code='" & obj.SalesSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                    End If
                Next
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
            Return False
        End Try
        Return True
    End Function
End Class