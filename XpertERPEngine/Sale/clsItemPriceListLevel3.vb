Imports common
Imports System.Data.SqlClient

Public Class clsItemPriceListLevel3
#Region "Variables"
    Public item_code As String = Nothing
    Public MRP As Double = 0
    Public Discount_Per As Double = 0
    Public Net_Amount As Double = 0
    Public Buffer_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsItemPriceListLevel3)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_ITEM_PRICE_LIST3"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsItemPriceListLevel3 In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "item_code", obj.item_code)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Net_Amount", obj.MRP * ((100 - obj.Discount_Per) / 100))
                clsCommon.AddColumnsForChange(coll, "Buffer_Amt", obj.Buffer_Amt)
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_LIST3", OMInsertOrUpdate.Insert, "", trans)
            Next

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData() As List(Of clsItemPriceListLevel3)
        Dim obj As clsItemPriceListLevel3 = Nothing
        Dim qry As String = "select * from TSPL_ITEM_PRICE_LIST3"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsItemPriceListLevel3)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsItemPriceListLevel3
            For Each dr As DataRow In dt.Rows
                objTr = New clsItemPriceListLevel3()
                objTr.item_code = clsCommon.myCstr(dr("item_Code"))
                objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                objTr.Net_Amount = clsCommon.myCdbl(dr("Net_Amount"))
                objTr.Buffer_Amt = clsCommon.myCdbl(dr("Buffer_Amt"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData() As Boolean
        Dim isSaved As Boolean = False
        Try
            Dim qry As String = "delete from TSPL_ITEM_PRICE_LIST3"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class

