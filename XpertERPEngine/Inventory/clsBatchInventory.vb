Imports System.Data.SqlClient
Imports common
''BM00000002899 by Balwinder 
Public Class clsBatchInventory
#Region "Variables"

    Public Code As String = ""
    Public Parent_Line_No As Integer = Nothing
    Public Line_No As Integer = Nothing
    Public Batch_No As String = ""
    Public Manufacture_Date As Date
    Public Expiry_Date As Date
    Public MRP As Double
    Public UOM As String = ""
    Public Qty As Double
    Public Item_Code As String = ""
    Public Document_Code As String = ""
    Public Document_Type As String = ""
    Public In_Out_Type As String = ""
    Public Against_Inv_Movement_Trans_Id As Integer = 0
    Public Location_Code As String = ""
    Public Document_Date As DateTime
    Public Manual_BatchNo As String = Nothing
    Public arr As List(Of clsBatchInventory) = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocType As String, ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal strInOutType As String, ByVal strICode As String, ByVal strLocation As String, ByVal intParentLineNo As Integer, ByVal dblMRP As Double, ByVal strUOM As String, ByVal Arr As List(Of clsBatchInventory), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtDocDate, trans) = True Then
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsBatchInventory In Arr
                    qry = " select max(Code) from TSPL_BATCH_ITEM"
                    obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(obj.Code) > 0 Then
                        obj.Code = clsCommon.incval(obj.Code)
                    Else
                        obj.Code = "BAT000000000000000000000000001"
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Parent_Line_No", intParentLineNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", counter)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                    clsCommon.AddColumnsForChange(coll, "Manufacture_Date", clsCommon.GetPrintDate(obj.Manufacture_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "UOM", strUOM)
                    clsCommon.AddColumnsForChange(coll, "MRP", dblMRP)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Document_Type", strDocType)
                    clsCommon.AddColumnsForChange(coll, "In_Out_Type", strInOutType)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", strLocation)
                    clsCommon.AddColumnsForChange(coll, "Manual_BatchNo", obj.Manual_BatchNo)
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BATCH_ITEM", OMInsertOrUpdate.Insert, "", trans)
                    counter += 1

                    If clsCommon.CompairString(strInOutType, "O") = CompairStringResult.Equal Then
                        Dim bal As Double = GetBatchBalance(strICode, strLocation, obj.Batch_No, dblMRP, strUOM, strDocNo, strDocType, trans, True, clsCommon.myCstr(clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")))
                        If bal < 0 Then
                            Throw New Exception("Batch Balance is going negative." + Environment.NewLine + "Item:" + strICode + ",Location:" + strLocation + ",Batch" + obj.Batch_No)
                        End If
                    End If
                Next
            End If
        End If

        Return True
    End Function

    Public Shared Function GetData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction, Optional ByVal strscreenType As String = Nothing, Optional ByVal strUOM As String = Nothing) As List(Of clsBatchInventory)
        Dim qry As String = ""
        If Not clsCommon.CompairString(strscreenType, "DS") = CompairStringResult.Equal Then
            qry = "select * from TSPL_BATCH_ITEM where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'"
            If clsCommon.CompairString("Transfer", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("ITransfer", strDocType) = CompairStringResult.Equal Then
                qry += " and TSPL_BATCH_ITEM.In_Out_Type in (select case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='T' then 'O' else Transfer_Type end  from TSPL_TRANSFER_ORDER_HEAD  where Document_No='" + strDocNo + "')"
            ElseIf clsCommon.CompairString("SD-CSATRANS", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("SD-CSATRANS-RETURN", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("OnlyOutType", strscreenType) = CompairStringResult.Equal OrElse clsCommon.CompairString("JW-TO", strDocType) = CompairStringResult.Equal Then
                qry += " and TSPL_BATCH_ITEM.In_Out_Type ='O' "
            End If
            qry += " order by Line_No"
        Else
            ' ''richa agarwal 3 MAy,2019 add parent line no into queries so that we can get only one record at a time from b atch table when we found same item more than one time with different uom
            'qry = "select 1 as Code,max(Parent_Line_No) as Parent_Line_No,max(line_no) as line_no,Batch_No,Manufacture_Date,Expiry_Date,0 as MRP," & _
            '    "sum(Qty) as Qty,UOM,item_code,'O' as In_Out_Type,Document_Code,max(Document_Date) as Document_Date, " & _
            '    "'" & strDocType & "-SH' as Document_Type,Location_Code,Manual_BatchNo,max(Against_Inv_Movement_Trans_Id) as Against_Inv_Movement_Trans_Id  from ( " & _
            '    "select TSPL_BATCH_ITEM.Parent_Line_No,TSPL_BATCH_ITEM.Line_No,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.Manufacture_Date,TSPL_BATCH_ITEM.Expiry_Date, " & _
            '    "TSPL_BATCH_ITEM.MRP,TSPL_BATCH_ITEM.Qty,TSPL_BATCH_ITEM.UOM,TSPL_BATCH_ITEM.Item_Code,TSPL_BATCH_ITEM.Document_Code,TSPL_BATCH_ITEM.Document_Date, " & _
            '    "TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Location_Code,TSPL_BATCH_ITEM.Manual_BatchNo,Against_Inv_Movement_Trans_Id from TSPL_BATCH_ITEM where Document_Type='" + strDocType + "-SH" + "' And " & _
            '    "Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and TSPL_BATCH_ITEM.Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "' " & _
            '    "union all " & _
            '    "select TSPL_BATCH_ITEM.Parent_Line_No,TSPL_BATCH_ITEM.Line_No,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.Manufacture_Date,TSPL_BATCH_ITEM.Expiry_Date, " & _
            '    "TSPL_BATCH_ITEM.MRP,-1 * TSPL_BATCH_ITEM.Qty,TSPL_BATCH_ITEM.UOM,TSPL_BATCH_ITEM.Item_Code,tspl_sd_sale_invoice_head.Against_Shipment_No, " & _
            '    "TSPL_BATCH_ITEM.Document_Date,TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Location_Code,TSPL_BATCH_ITEM.Manual_BatchNo,Against_Inv_Movement_Trans_Id from " & _
            '    "TSPL_BATCH_ITEM left join tspl_sd_sale_return_head on tspl_sd_sale_return_head.document_code=TSPL_BATCH_ITEM.document_code " & _
            '    "left join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_return_head.Against_Invoice_No " & _
            '    "where TSPL_BATCH_ITEM.Document_Type='" + strDocType + "-SR" + "' and tspl_sd_sale_invoice_head.Against_Shipment_No='" + strDocNo + "' and " & _
            '    "Item_Code='" + strICode + "'  and TSPL_BATCH_ITEM.Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "' ) aa group by Document_Code,item_code,uom, Manufacture_Date,Expiry_Date, " & _
            '    "Manual_BatchNo,Batch_No,Location_Code ,Parent_Line_No having sum(Qty) > 0 order by Line_No"

            ''richa agarwal 3 MAy,2019 remove parent line no into queries and add uom 
            qry = "select 1 as Code,max(Parent_Line_No) as Parent_Line_No,max(line_no) as line_no,Batch_No,Manufacture_Date,Expiry_Date,0 as MRP," &
                "sum(Qty) as Qty,UOM,item_code,'O' as In_Out_Type,Document_Code,max(Document_Date) as Document_Date, " &
                "'" & strDocType & "-SH' as Document_Type,Location_Code,Manual_BatchNo,max(Against_Inv_Movement_Trans_Id) as Against_Inv_Movement_Trans_Id  from ( " &
                "select TSPL_BATCH_ITEM.Parent_Line_No,TSPL_BATCH_ITEM.Line_No,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.Manufacture_Date,TSPL_BATCH_ITEM.Expiry_Date, " &
                "TSPL_BATCH_ITEM.MRP,TSPL_BATCH_ITEM.Qty,TSPL_BATCH_ITEM.UOM,TSPL_BATCH_ITEM.Item_Code,TSPL_BATCH_ITEM.Document_Code,TSPL_BATCH_ITEM.Document_Date, " &
                "TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Location_Code,TSPL_BATCH_ITEM.Manual_BatchNo,Against_Inv_Movement_Trans_Id from TSPL_BATCH_ITEM where Document_Type='" + strDocType + "-SH" + "' And " &
                "Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' " & IIf(clsCommon.myLen(strUOM) > 0, "and TSPL_BATCH_ITEM.UOM='" + clsCommon.myCstr(strUOM) + "'", "and TSPL_BATCH_ITEM.Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'") & " " &
                "union all " &
                "select TSPL_BATCH_ITEM.Parent_Line_No,TSPL_BATCH_ITEM.Line_No,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.Manufacture_Date,TSPL_BATCH_ITEM.Expiry_Date, " &
                "TSPL_BATCH_ITEM.MRP,-1 * TSPL_BATCH_ITEM.Qty,TSPL_BATCH_ITEM.UOM,TSPL_BATCH_ITEM.Item_Code,tspl_sd_sale_invoice_head.Against_Shipment_No, " &
                "TSPL_BATCH_ITEM.Document_Date,TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Location_Code,TSPL_BATCH_ITEM.Manual_BatchNo,Against_Inv_Movement_Trans_Id from " &
                "TSPL_BATCH_ITEM left join tspl_sd_sale_return_head on tspl_sd_sale_return_head.document_code=TSPL_BATCH_ITEM.document_code " &
                "left join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_return_head.Against_Invoice_No " &
                "where TSPL_BATCH_ITEM.Document_Type='" + strDocType + "-SR" + "' and tspl_sd_sale_invoice_head.Against_Shipment_No='" + strDocNo + "' and " &
                "Item_Code='" + strICode + "' " & IIf(clsCommon.myLen(strUOM) > 0, "and TSPL_BATCH_ITEM.UOM='" + clsCommon.myCstr(strUOM) + "'", "and TSPL_BATCH_ITEM.Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'") & " ) aa group by Document_Code,item_code,uom, Manufacture_Date,Expiry_Date, " &
                "Manual_BatchNo,Batch_No,Location_Code ,UOM having sum(Qty) > 0 order by Line_No"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr As List(Of clsBatchInventory) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsBatchInventory)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsBatchInventory = New clsBatchInventory()
                objTr.Manual_BatchNo = clsCommon.myCstr(dr("Manual_BatchNo"))
                objTr.Code = clsCommon.myCstr(dr("Code"))
                objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                objTr.Manufacture_Date = clsCommon.myCDate(dr("Manufacture_Date"))
                objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                objTr.In_Out_Type = clsCommon.myCstr(dr("In_Out_Type"))
                objTr.Against_Inv_Movement_Trans_Id = clsCommon.myCdbl(dr("Against_Inv_Movement_Trans_Id"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_BATCH_ITEM", "Document_Code", trans)
        Dim qry As String = "Delete from TSPL_BATCH_ITEM where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction)
        Dim qry As String = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal InOutType As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction, ByVal BatchSkipOnSetting As String, ByVal isConsumptionRow As Boolean) As Boolean
        If BatchSkipOnSetting Then
            Return True ''==========[When batch item is batch type then skip]
        End If
        If clsItemMaster.IsBatchItem(strICode, trans) Then
            Dim WhrCls As String = "where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and In_Out_Type='" + InOutType + "'"
            'Sanjay, 2-July-2020 , use where clause for pick item trans_id from inventory movement 
            Dim WhrInventory As String = "where Trans_Type='" + strDocType + "' and Source_Doc_No='" + strDocNo + "' and Item_Code='" + strICode + "' and InOut='" + InOutType + "'"
            ''''''''''''''''''''''''''''
            If isConsumptionRow Then
                WhrCls += " and Parent_Line_No='" + clsCommon.myCstr(0) + "'"
            Else
                If clsCommon.CompairString(strDocType, "Disassembly") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Assembly") = CompairStringResult.Equal Then
                Else
                    WhrCls += " and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'"
                End If
            End If
            Dim qry As String = "select 1 from TSPL_BATCH_ITEM " + WhrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Item is Batch wise but no detail found." + Environment.NewLine + "Document Type='" + strDocType + "' , Document Code='" + strDocNo + "' , Item Code='" + strICode + "' , Parent Line_No='" + clsCommon.myCstr(intParentLineNo) + "' ,In Out Type=" + InOutType)
            End If
            qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=(select max(Trans_Id) from TSPL_INVENTORY_MOVEMENT " + WhrInventory + ") " + WhrCls
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return True
    End Function
    '' BHA/05/09/18-000508
    Public Shared Function GetBatchBalance(ByVal strItemCode As String, ByVal strLocationCode As String, ByVal strBatchNo As String, ByVal dblMRP As Double, ByVal strUOM As String, ByVal strCurrDocNo As String, ByVal strCurrDocType As String, ByVal trans As SqlTransaction, Optional ByVal ForNegative As Boolean = False, Optional ByVal strPhysicalStockDate As String = "") As Double
        Dim qry As String = "select  Qty from ( select Batch_No as BatchNo,Min(Manufacture_Date) as ManufactureDate,MAX(Expiry_Date) as ExpiryDate,cast( round( sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end )),2,1) as decimal(18,2)) as Qty from ("
        qry += " select * from ("
        qry += " select TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.In_Out_Type,TSPL_BATCH_ITEM.UOM as OrgUOM,TSPL_BATCH_ITEM.Qty as OrgQty,TSPL_BATCH_ITEM.MRP as OrgMRP,TSPL_BATCH_ITEM.Expiry_Date,TSPL_BATCH_ITEM.Manufacture_Date, convert(decimal(18,2),(TSPL_BATCH_ITEM.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ConvertedUOM.Conversion_Factor) as Qty, (TSPL_BATCH_ITEM.MRP /TSPL_ITEM_UOM_DETAIL.Conversion_Factor)*ConvertedUOM.Conversion_Factor as MRP"
        qry += " from TSPL_BATCH_ITEM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUOM on ConvertedUOM.Item_Code=TSPL_BATCH_ITEM.Item_Code and ConvertedUOM.UOM_Code='" + strUOM + "'"
        qry += " where TSPL_BATCH_ITEM.Item_Code='" + strItemCode + "' and TSPL_BATCH_ITEM.Location_Code='" + strLocationCode + "' and  TSPL_BATCH_ITEM.Batch_No='" + strBatchNo + "'"
        If ForNegative = False Then
            qry += " and not( TSPL_BATCH_ITEM.Document_Code = '" + strCurrDocNo + "' and TSPL_BATCH_ITEM.Document_Type = '" + strCurrDocType + "') "
        End If
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.checkStockOfItemTillTransactionDateOnly, clsFixedParameterCode.checkStockOfItemTillTransactionDateOnly, trans)) = 1) = True AndAlso clsCommon.myLen(strPhysicalStockDate) > 0 Then
            qry += " and  convert(date,TSPL_BATCH_ITEM.Document_Date,103)  <= convert(date, '" + strPhysicalStockDate + "',103) "
        End If
        qry += " and 1=(case when TSPL_BATCH_ITEM.In_Out_Type='I' and TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id is not null then 1 else case when TSPL_BATCH_ITEM.In_Out_Type='O' then 1 else 0 end end) AND TSPL_BATCH_ITEM.Document_Code NOT IN (sELECT tspl_transfer_order_head.Document_No  FROM tspl_transfer_order_head WHERE STATUS =2)"
        qry += " ) xx where 2=2 "
        If dblMRP <> 0 Then
            qry += "and MRP='" + clsCommon.myCstr(dblMRP) + "'"
        End If
        qry += " )xxx"
        qry += " group by Batch_No having cast( round( sum(Qty * (case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end )),2,1) as decimal(18,2)) <>0)xxxx "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
   
    Public Shared Function UpdateDocumentNoAndType(ByVal strCurrDocNo As String, ByVal strCurrDocType As String, ByVal strNewDocNo As String, ByVal strNewDocType As String, ByVal trans As SqlTransaction)
        Dim qry As String = "Update TSPL_BATCH_ITEM_NEW set Document_Code='" + strNewDocNo + "',Document_Type='" + strNewDocType + "'  where Document_Code='" + strCurrDocNo + "' and Document_Type='" + strCurrDocType + "' "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "Update TSPL_BATCH_ITEM set Document_Code='" + strNewDocNo + "',Document_Type='" + strNewDocType + "'  where Document_Code='" + strCurrDocNo + "' and Document_Type='" + strCurrDocType + "' "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Return True
    End Function
End Class


