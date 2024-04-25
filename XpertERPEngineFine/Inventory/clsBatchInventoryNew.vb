Imports System.Data.SqlClient
Imports common
''BM00000002899 by Balwinder 
Public Class clsBatchInventoryNew
#Region "Variables"

    Public Code As String = ""
    Public Parent_Line_No As Integer = Nothing
    Public Line_No As Integer = Nothing
    Public Batch_No As String = ""
    Public Item_Code As String = ""
    Public Qty As Decimal
    Public UOM As String = ""
    Public Document_Code As String = ""
    Public Document_Date As DateTime
    Public Document_Type As String = ""
    Public In_Out_Type As String = ""
    Public Against_Inv_Movement_New_Trans_Id As Integer = 0
    Public Location_Code As String = ""
    Public arr As List(Of clsBatchInventoryNew) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocType As String, ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal strInOutType As String, ByVal strICode As String, ByVal strLocation As String, ByVal intParentLineNo As Integer, ByVal dblMRP As Double, ByVal strUOM As String, ByVal Arr As List(Of clsBatchInventoryNew), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each obj As clsBatchInventoryNew In Arr
                qry = " select max(Code) from TSPL_BATCH_ITEM_NEW"
                obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Code) > 0 Then
                    obj.Code = clsCommon.incval(obj.Code)
                Else
                    obj.Code = "BATN00000000000000000000000001"
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Parent_Line_No", intParentLineNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", counter)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "UOM", strUOM)
                'clsCommon.AddColumnsForChange(coll, "MRP", dblMRP)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Type", strDocType)
                clsCommon.AddColumnsForChange(coll, "In_Out_Type", strInOutType)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocation)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BATCH_ITEM_NEW", OMInsertOrUpdate.Insert, "", trans)
                counter += 1

                If clsCommon.CompairString(strInOutType, "O") = CompairStringResult.Equal Then
                    Dim bal As Double = GetBatchBalance(strICode, strLocation, obj.Batch_No, strUOM, strDocNo, strDocType, trans, True)
                    If bal < 0 Then
                        Throw New Exception("Batch Balance is going negative." + Environment.NewLine + "Item:" + strICode + ",Location:" + strLocation + ",Batch" + obj.Batch_No)
                    End If
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction, Optional ByVal strscreenType As String = Nothing) As List(Of clsBatchInventoryNew)
        Dim qry As String = ""
        qry = "select * from TSPL_BATCH_ITEM_New where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'"
        If clsCommon.CompairString("OnlyOutType", strscreenType) = CompairStringResult.Equal Then
            qry += " and TSPL_BATCH_ITEM_New.In_Out_Type ='O' "
        End If
        qry += " order by Line_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr As List(Of clsBatchInventoryNew) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsBatchInventoryNew)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsBatchInventoryNew = New clsBatchInventoryNew()
                objTr.Code = clsCommon.myCstr(dr("Code"))
                objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                objTr.In_Out_Type = clsCommon.myCstr(dr("In_Out_Type"))
                objTr.Against_Inv_Movement_New_Trans_Id = clsCommon.myCdbl(dr("Against_Inv_Movement_New_Trans_Id"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_BATCH_ITEM_NEW", "Document_Code", trans)
        Dim qry As String = "Delete from TSPL_BATCH_ITEM_NEW where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim qry As String = "update TSPL_BATCH_ITEM_NEW set Against_Inv_Movement_New_Trans_Id=null where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function


    Public Shared Function PostData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal InOutType As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction, ByVal BatchSkipOnSetting As String, ByVal isConsumptionRow As Boolean) As Boolean
        If BatchSkipOnSetting OrElse _
             clsCommon.CompairString(strDocType, "MCC-MSRN") = CompairStringResult.Equal OrElse _
             clsCommon.CompairString(strDocType, "DispChallan") = CompairStringResult.Equal OrElse _
             clsCommon.CompairString(strDocType, "DispChallan-RET") = CompairStringResult.Equal OrElse _
             clsCommon.CompairString(strDocType, "DispChallanRet") = CompairStringResult.Equal Then
            Return True ''==========[When batch item is batch type then skip]
        End If

        If clsItemMaster.IsBatchItem(strICode, trans) Then
            Dim WhrCls As String = "where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "'  and In_Out_Type='" + InOutType + "'"
            If isConsumptionRow Then
                WhrCls += " and Parent_Line_No='" + clsCommon.myCstr(0) + "'"
            Else
                WhrCls += " and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'"
            End If

            Dim qry As String = "select 1 from TSPL_BATCH_ITEM_NEW " + WhrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Item is Batch wise but no detail found." + Environment.NewLine + "Document Type='" + strDocType + "' , Document Code='" + strDocNo + "' , Item Code='" + strICode + "' , Parent Line_No='" + clsCommon.myCstr(intParentLineNo) + "' ,In Out Type=" + InOutType)
            End If
            qry = "update TSPL_BATCH_ITEM_NEW set Against_Inv_Movement_New_Trans_Id=(select max(Trans_Id) from TSPL_INVENTORY_MOVEMENT_NEW) " + WhrCls
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return True
    End Function

    Public Shared Function GetBatchBalance(ByVal strItemCode As String, ByVal strLocationCode As String, ByVal strBatchNo As String, ByVal strUOM As String, ByVal strCurrDocNo As String, ByVal strCurrDocType As String, ByVal trans As SqlTransaction, Optional ByVal ForNegative As Boolean = False) As Double
        Dim qry As String = "select  Qty from ( select Batch_No as BatchNo,cast(sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end )) as decimal(18,2)) as Qty from ("
        qry += " select * from ("
        qry += " select TSPL_BATCH_ITEM_NEW.Batch_No,TSPL_BATCH_ITEM_NEW.In_Out_Type,TSPL_BATCH_ITEM_NEW.UOM as OrgUOM,TSPL_BATCH_ITEM_NEW.Qty as OrgQty, (TSPL_BATCH_ITEM_NEW.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ConvertedUOM.Conversion_Factor as Qty"
        qry += " from TSPL_BATCH_ITEM_NEW"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BATCH_ITEM_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM_NEW.UOM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUOM on ConvertedUOM.Item_Code=TSPL_BATCH_ITEM_NEW.Item_Code and ConvertedUOM.UOM_Code='" + strUOM + "'"
        qry += " where TSPL_BATCH_ITEM_NEW.Item_Code='" + strItemCode + "' and TSPL_BATCH_ITEM_NEW.Location_Code='" + strLocationCode + "' and  TSPL_BATCH_ITEM_NEW.Batch_No='" + strBatchNo + "'"
        If ForNegative = False Then
            qry += " and not( TSPL_BATCH_ITEM_NEW.Document_Code = '" + strCurrDocNo + "' and TSPL_BATCH_ITEM_NEW.Document_Type = '" + strCurrDocType + "') "
        End If
        qry += " and 1=(case when TSPL_BATCH_ITEM_NEW.In_Out_Type='I' and TSPL_BATCH_ITEM_NEW.Against_Inv_Movement_New_Trans_Id is not null then 1 else case when TSPL_BATCH_ITEM_NEW.In_Out_Type='O' then 1 else 0 end end) AND TSPL_BATCH_ITEM_NEW.Document_Code NOT IN (sELECT tspl_transfer_order_head.Document_No  FROM tspl_transfer_order_head WHERE STATUS =2)"
        qry += " ) xx where 2=2 "
        qry += " )xxx"
        qry += " group by Batch_No having cast(sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end ))as decimal(18,2)) <>0)xxxx "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function


End Class


