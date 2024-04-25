Imports System.Data.SqlClient
Public Class clsItemPurchaseSetDetail
#Region "Variable"
    Public Item_Code As String = ""
    Public PurchaseSetCode As String = ""
    Public SalesSetCode As String = ""
    Public InventoryCode As String = ""
    Public PayableCode As String = ""
    Public AdjCode As String = ""
    Public WIPCode As String = ""
    Public RMCode As String = ""
#End Region

    Public Shared Function UpdateData(ByVal Arr As List(Of clsItemPurchaseSetDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemPurchaseSetDetail In Arr
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        Dim qry As String = ""
                        If clsCommon.myLen(obj.InventoryCode) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Inv_Control_Account='" & obj.InventoryCode & "'" &
                                 " where Purchase_Class_Code='" & obj.PurchaseSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.PayableCode) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Inv_Payable_Clearing='" & obj.PayableCode & "'" &
                                    " where Purchase_Class_Code='" & obj.PurchaseSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.AdjCode) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Adjustment_Account='" & obj.AdjCode & "'" &
                                    " where Purchase_Class_Code='" & obj.PurchaseSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.WIPCode) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set WIP_Account='" & obj.WIPCode & "'" &
                                    " where Purchase_Class_Code='" & obj.PurchaseSetCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.RMCode) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set RM_Consumption='" & obj.RMCode & "'" &
                                    " where Purchase_Class_Code='" & obj.PurchaseSetCode & "'"
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