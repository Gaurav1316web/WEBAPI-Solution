Imports common
Imports System.Data.SqlClient
Public Class clsQuickSettlementItemDetails
#Region "Variable"
    Public Quick_SettleMent_Id As String = Nothing
    Public Transfer_Number As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public MRP As Double = 0
    Public LoadOut_Qty As Double = 0
    Public LoadInFC_Qty As Double = 0
    Public LoadInFB_Qty As Double = 0
    Public Total_LoadInFC_Qty As Double = 0
    Public Provisional_SaleQty As Double = 0
    Public Retailer_Price As Double = 0
    Public Provisional_Sale_Amount As Double = 0
    Public FOC_FCQty As Double = 0
    Public FOC_FBQty As Double = 0
    Public Total_FOC_FCQty As Double = 0
    Public NetSale_Amount As Double = 0

    Public NetLoad_Qty As Double = 0
    Public FOC_Amount As Double = 0




    Shared Net_SalesQty As Double = 0
    Shared Net_FOCQty As Double = 0
    Shared Net_ProvisionalQty As Double = 0
    Shared Net_LoadInQty As Double = 0
    Shared Net_LoadOutQty As Double = 0
#End Region

    Public Shared Function funSave(ByVal arr As List(Of clsQuickSettlementItemDetails), ByVal strDocNO As String, ByVal strTransferNO As String) As Boolean
        Dim isSaved As Boolean = True
        Dim qry1 As String = Nothing
        Net_LoadOutQty = 0
        Net_LoadInQty = 0
        Net_ProvisionalQty = 0
        Net_FOCQty = 0
        Net_SalesQty = 0
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from tspl_QuickSettleMent_Item_Detail    where Quick_SettleMent_Id='" + strDocNO + "' and Transfer_Number ='" + strTransferNO + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsQuickSettlementItemDetails In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Quick_SettleMent_Id", strDocNO)
                clsCommon.AddColumnsForChange(coll, "Transfer_Number", obj.Transfer_Number)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Name", obj.Item_Name)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "LoadOut_Qty", obj.LoadOut_Qty)
                clsCommon.AddColumnsForChange(coll, "LoadInFC_Qty", obj.LoadInFC_Qty)
                clsCommon.AddColumnsForChange(coll, "LoadInFB_Qty", obj.LoadInFB_Qty)
                clsCommon.AddColumnsForChange(coll, "Total_LoadInFC_Qty", obj.Total_LoadInFC_Qty)
                clsCommon.AddColumnsForChange(coll, "Provisional_SaleQty", obj.Provisional_SaleQty)
                clsCommon.AddColumnsForChange(coll, "Retailer_Price", obj.Retailer_Price)
                clsCommon.AddColumnsForChange(coll, "Provisional_Sale_Amount", obj.Provisional_Sale_Amount)
                clsCommon.AddColumnsForChange(coll, "FOC_FCQty", obj.FOC_FCQty)
                clsCommon.AddColumnsForChange(coll, "FOC_FBQty", obj.FOC_FBQty)
                clsCommon.AddColumnsForChange(coll, "Total_FOC_FCQty", obj.Total_FOC_FCQty)
                clsCommon.AddColumnsForChange(coll, "NetSale_Amount", obj.NetSale_Amount)

                clsCommon.AddColumnsForChange(coll, "NetLoad_Qty", obj.NetLoad_Qty)
                clsCommon.AddColumnsForChange(coll, "FOC_Amount", obj.FOC_Amount)

                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_QuickSettleMent_Item_Detail", OMInsertOrUpdate.Insert, "", trans)
                Net_LoadOutQty = Net_LoadOutQty + obj.LoadOut_Qty
                Net_LoadInQty = Net_LoadInQty + obj.Total_LoadInFC_Qty
                Net_ProvisionalQty = Net_ProvisionalQty + obj.Provisional_SaleQty
                Net_FOCQty = Net_FOCQty + obj.Total_FOC_FCQty
                Net_SalesQty = Net_SalesQty + obj.NetLoad_Qty
            Next
            clsDBFuncationality.ExecuteNonQuery("update tspl_QuickSettleMent set Net_SalesQty='" + clsCommon.myCstr(Net_SalesQty) + "',Net_FOCQty='" + clsCommon.myCstr(Net_FOCQty) + "',Net_ProvisionalQty='" + clsCommon.myCstr(Net_ProvisionalQty) + "',Net_LoadInQty='" + clsCommon.myCstr(Net_LoadInQty) + "',Net_LoadOutQty='" + clsCommon.myCstr(Net_LoadOutQty) + "' where Quick_SettleMent_Id ='" + strDocNO + "'", trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal strTransferNo As String) As List(Of clsQuickSettlementItemDetails)
        Dim Arr As List(Of clsQuickSettlementItemDetails) = Nothing
        Dim Qry As String = "SELECT  NetLoad_Qty, FOC_Amount,  Quick_SettleMent_Id, Transfer_Number, Item_Code, Item_Name, MRP, LoadOut_Qty, LoadInFC_Qty, LoadInFB_Qty, Total_LoadInFC_Qty, Provisional_SaleQty,Retailer_Price, Provisional_Sale_Amount, FOC_FCQty, FOC_FBQty, Total_FOC_FCQty, NetSale_Amount FROM tspl_QuickSettleMent_Item_Detail where Quick_SettleMent_Id='" + strDocNo + "' and Transfer_Number ='" + strTransferNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsQuickSettlementItemDetails)
            Dim objTr As clsQuickSettlementItemDetails
            For Each dr As DataRow In dt.Rows
                objTr = New clsQuickSettlementItemDetails
                objTr.Quick_SettleMent_Id = clsCommon.myCstr(dr("Quick_SettleMent_Id"))
                objTr.Transfer_Number = clsCommon.myCstr(dr("Transfer_Number"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Name"))
                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                objTr.LoadOut_Qty = clsCommon.myCdbl(dr("LoadOut_Qty"))
                objTr.LoadInFC_Qty = clsCommon.myCdbl(dr("LoadInFC_Qty"))
                objTr.LoadInFB_Qty = clsCommon.myCdbl(dr("LoadInFB_Qty"))
                objTr.Total_LoadInFC_Qty = clsCommon.myCdbl(dr("Total_LoadInFC_Qty"))
                objTr.Provisional_SaleQty = clsCommon.myCdbl(dr("Provisional_SaleQty"))
                objTr.Retailer_Price = clsCommon.myCdbl(dr("Retailer_Price"))
                objTr.Provisional_Sale_Amount = clsCommon.myCdbl(dr("Provisional_Sale_Amount"))
                objTr.FOC_FCQty = clsCommon.myCdbl(dr("FOC_FCQty"))
                objTr.FOC_FBQty = clsCommon.myCdbl(dr("FOC_FBQty"))
                objTr.Total_FOC_FCQty = clsCommon.myCdbl(dr("Total_FOC_FCQty"))
                objTr.NetSale_Amount = clsCommon.myCdbl(dr("NetSale_Amount"))

                objTr.FOC_Amount = clsCommon.myCdbl(dr("FOC_Amount"))
                objTr.NetLoad_Qty = clsCommon.myCdbl(dr("NetLoad_Qty"))

                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
