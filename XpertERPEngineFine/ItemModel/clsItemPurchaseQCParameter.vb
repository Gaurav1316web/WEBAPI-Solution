Imports System.Data.SqlClient
Public Class clsItemPurchaseQCParameter
    Public SNo As String
    Public Item_Code As String
    Public QC_Code As String
    Public QC_Name As String
    Public Specifications As String

    Public Shared Function SaveData(ByVal strICode As String, ByVal Arr As List(Of clsItemPurchaseQCParameter), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER where Item_Code='" + strICode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsItemPurchaseQCParameter In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
                clsCommon.AddColumnsForChange(coll, "Specifications", obj.Specifications)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal Item_Code As String, ByVal trans As SqlTransaction) As List(Of clsItemPurchaseQCParameter)
        Dim arr As List(Of clsItemPurchaseQCParameter) = Nothing
        Dim qry As String = "select TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.*,TSPL_QC_LOG_SHEET_MASTER.Description from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code='" + Item_Code + "' order by TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsItemPurchaseQCParameter)()
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsItemPurchaseQCParameter
                obj.SNo = clsCommon.myCdbl(dr("SNo"))
                obj.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                obj.QC_Name = clsCommon.myCstr(dr("Description"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Specifications = clsCommon.myCstr(dr("Specifications"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class