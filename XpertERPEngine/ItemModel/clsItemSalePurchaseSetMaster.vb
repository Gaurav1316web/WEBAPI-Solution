Public Class clsItemSalePurchaseSetMaster
#Region "Variable"
    Public ItemArrTr As List(Of clsItemSetDetail) = Nothing
    Public SaleArrTr As List(Of clsItemSalePurchaseSetDetail) = Nothing
    Public PurchaseArrTr As List(Of clsItemPurchaseSetDetail) = Nothing
    Public PurchaseMasterArr As List(Of clsPurchaseAccountSets) = Nothing
#End Region


    Public Function Update(ByVal obj As clsItemSalePurchaseSetMaster) As Boolean
        Dim isSaved As Boolean = True
        isSaved = clsItemSetDetail.UpdateData(obj.ItemArrTr)
        isSaved = clsItemSalePurchaseSetDetail.UpdateData(obj.SaleArrTr)
        isSaved = clsItemPurchaseSetDetail.UpdateData(obj.PurchaseArrTr)
        Return isSaved
    End Function
    Public Function PurchaseUpdate(ByVal obj As clsItemSalePurchaseSetMaster) As Boolean
        Dim isSaved As Boolean = True
        isSaved = clsPurchaseAccountSets.UpdateData(obj.PurchaseMasterArr)
        Return isSaved
    End Function
End Class