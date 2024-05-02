Imports System.Data.SqlClient

Public Class clsItemSetDetail
#Region "Variable"
    Public Item_Code As String = ""
    Public PurchaseSetCode As String = ""
    Public SalesSetCode As String = ""
    Public AccountCode As String = ""
    Public InventoryCode As String = ""
    Public PayableCode As String = ""
    Public AdjCode As String = ""
    Public WIPCode As String = ""
    Public RMCode As String = ""
    Public SaleAccount As String = ""
    Public SaleReturn As String = ""
    Public COGS As String = ""
    Public ConsigmentAc As String = ""
#End Region

    Public Shared Function UpdateData(ByVal Arr As List(Of clsItemSetDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemSetDetail In Arr
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        Dim qry As String = ""
                        If clsCommon.myLen(obj.SalesSetCode) > 0 Then
                            ',Weight_UOM = '" & obj.Weight_UOM & "'
                            qry = "update tspl_item_master set Sale_Class_Code='" & obj.SalesSetCode & "'" &
                                 " where item_code='" & obj.Item_Code & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.AccountCode) > 0 Then
                            qry = "update TSPL_ITEM_Master set Purchase_Class_Code='" & obj.AccountCode & "'" &
                          " where item_code='" & obj.Item_Code & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.ConsigmentAc) > 0 Then
                            qry = "update TSPL_ITEM_Master set GL_Account='" & obj.ConsigmentAc & "'" &
                          " where item_code='" & obj.Item_Code & "'"
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