Imports common
Imports System.Data.SqlClient

Public Class clsItemBasicPrice
#Region "Variables"
    Public Item_Code As String = ""
    Public MRP As Decimal = 0
    Public Basic_Price As Decimal = 0
#End Region

    Public Function SaveData(ByVal obj As clsItemBasicPrice, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select 1 from TSPL_ITEM_BASIC_PRICE where Item_Code='" + obj.Item_Code + "' and MRP='" + clsCommon.myCstr(obj.MRP) + "'"
            isNewEntry = Not clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_BASIC_PRICE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_BASIC_PRICE", OMInsertOrUpdate.Update, "Item_Code='" + obj.Item_Code + "' and MRP='" + obj.MRP + "' ", trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function




    Public Shared Function DeleteData(ByVal itemCode As String, ByVal mrpCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(itemCode) <= 0) Then
            Throw New Exception("Item Code not found to Delete")
        End If
        If (clsCommon.myLen(mrpCode) <= 0) Then
            Throw New Exception("MRP Code not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_ITEM_BASIC_PRICE where Item_Code='" + itemCode + "' and MRP='" + mrpCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function


    Public Shared Function GetBasicPrice(ByVal strItemNo As String, ByVal strMRP As Double, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim qry As String = "select Basic_Price from TSPL_ITEM_BASIC_PRICE where Item_Code='" + strItemNo + "' AND mrp =" + clsCommon.myCstr(strMRP) + " "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
