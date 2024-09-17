Imports System.Data.SqlClient
Public Class clsProductionShiftMgmt
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Location_Code As String
    Public Location_Name As String
    Public Shift_Code As String
    Public Remarks As String
    Public Comment As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
    Public ArrOP As List(Of clsProductionShiftMgmtOpen) = Nothing
    Public ArrRecPlant As List(Of clsProductionShiftMgmtReceiptPlantMilk) = Nothing
    Public ArrRecBulk As List(Of clsProductionShiftMgmtReceiptBulkMilk) = Nothing
    Public ArrPro As List(Of clsProductionShiftMgmtProduction) = Nothing
    Public ArrProRMSummary As List(Of clsProductionShiftMgmtProductionRMSummary) = Nothing
    Public ArrCL As List(Of clsProductionShiftMgmtClose) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsProductionShiftMgmt, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_SHIFT_MGMT_CLOSE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_OPEN where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                Throw New Exception("Please provide location")
            End If
            If clsCommon.myLen(obj.Shift_Code) <= 0 Then
                Throw New Exception("Please provide location to pick raw milk")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_Code, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ShiftMgmt, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Update, "TSPL_SHIFT_MGMT.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsProductionShiftMgmtOpen.SaveData(obj.Document_No, obj.ArrOP, trans)
            clsProductionShiftMgmtReceiptPlantMilk.SaveData(obj.Document_No, obj.ArrRecPlant, trans)
            clsProductionShiftMgmtReceiptBulkMilk.SaveData(obj.Document_No, obj.ArrRecBulk, trans)
            clsProductionShiftMgmtProduction.SaveData(obj.Document_No, obj.ArrPro, trans)
            clsProductionShiftMgmtProductionRMSummary.SaveData(obj.Document_No, obj.ArrProRMSummary, trans)
            clsProductionShiftMgmtClose.SaveData(obj.Document_No, obj.ArrCL, trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionShiftMgmt
        Dim obj As clsProductionShiftMgmt = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT.*,TSPL_LOCATION_MASTER.Location_Desc as Location_Name 
FROM TSPL_SHIFT_MGMT 
left outer join TSPL_LOCATION_MASTER   on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT.Location_Code
where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select MIN(Document_No) from TSPL_SHIFT_MGMT where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Min(Document_No) from TSPL_SHIFT_MGMT where Document_No>'" + strDocNo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT where Document_No<'" + strDocNo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_SHIFT_MGMT.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProductionShiftMgmt()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.ArrOP = clsProductionShiftMgmtOpen.GetData(obj.Document_No, "", trans)
            obj.ArrRecPlant = clsProductionShiftMgmtReceiptPlantMilk.GetData(obj.Document_No, "", trans)
            obj.ArrRecBulk = clsProductionShiftMgmtReceiptBulkMilk.GetData(obj.Document_No, "", trans)
            obj.ArrPro = clsProductionShiftMgmtProduction.GetData(obj.Document_No, "", trans)
            obj.ArrProRMSummary = clsProductionShiftMgmtProductionRMSummary.GetData(obj.Document_No, "", trans)
            obj.ArrCL = clsProductionShiftMgmtClose.GetData(obj.Document_No, "", trans)
        End If
        Return obj
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SHIFT_MGMT", "Document_No", "TSPL_SHIFT_MGMT_PRODUCTION", "Document_No", "TSPL_SHIFT_MGMT_OPEN", "Document_No", trans)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsProductionShiftMgmt = clsProductionShiftMgmt.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProductionShiftMgmt, obj.Location_Code, obj.Document_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_CLOSE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_OPEN where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim obj As clsProductionShiftMgmt = clsProductionShiftMgmt.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_Code, obj.Document_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            MilkProductionUploader(obj, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function MilkProductionUploader(ByVal obj As clsProductionShiftMgmt, ByVal trans As SqlTransaction) As Boolean
        '        Try
        '            Dim qry As String = ""
        '            Dim dt As DataTable = Nothing
        '            For Each objtr As clsProductionShiftMgmtProduction In obj.Arr
        '                qry = "select top 1 BOM_CODE from TSPL_PP_BOM_HEAD 
        'where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + objtr.Item_Code + "'  and '" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date) order by BOM_CODE desc"
        '                dt = clsDBFuncationality.GetDataTable(qry, trans)
        '                If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
        '                    Throw New Exception("BOM Not Found for Item [" + objtr.Item_Code + "] and Date [" + clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") + "]")
        '                End If
        '                Dim coll As New Hashtable()
        '                clsCommon.AddColumnsForChange(coll, "BOM_Code", clsCommon.myCstr(dt.Rows(0)("BOM_CODE")))
        '                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION", OMInsertOrUpdate.Update, "PK_ID=" + clsCommon.myCstr(objtr.PK_ID), trans)
        '            Next

        '            qry = "insert into TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL (Document_No,Against_PKID,Cost_Code,Amount)  
        'select '" + obj.Document_No + "' as Document_No,PK_ID,xx.COST_CODE,(xx.prod_qty * (xx.OverHead_Cost/xx.build_qty)) as Amount from (
        'select TSPL_SHIFT_MGMT_PRODUCTION.PK_ID,(TSPL_SHIFT_MGMT_PRODUCTION.Qty * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty
        ',TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.OverHead_Cost
        ' from TSPL_SHIFT_MGMT_PRODUCTION
        'left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_SHIFT_MGMT_PRODUCTION.BOM_Code
        'inner join TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS on TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code=TSPL_PP_BOM_HEAD.BOM_CODE
        'left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
        'left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code=TSPL_SHIFT_MGMT_PRODUCTION.UOM 
        'where TSPL_SHIFT_MGMT_PRODUCTION.Document_No='" + obj.Document_No + "' and TSPL_SHIFT_MGMT_PRODUCTION.Temp=1
        ') xx  "
        '            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        '            qry = "select PK_ID,xx.ITEM_CODE,xx.Item_Desc,xx.Item_Type,xx.UNIT_CODE,xx.Product_Type,(xx.prod_qty * (xx.quantity/xx.build_qty)) as Qty,xx.fat,xx.snf,xx.fat_kg,xx.snf_kg,Comment,Is_Batch_Item,Batch_Date from (
        'select TSPL_SHIFT_MGMT_PRODUCTION.PK_ID,(TSPL_SHIFT_MGMT_PRODUCTION.Qty * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date
        ',TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type
        ',(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY
        ',(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf
        ',(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg
        ',(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg 
        ',TSPL_ITEM_MASTER.Is_Batch_Item,TSPL_SHIFT_MGMT_PRODUCTION.Comment,TSPL_SHIFT_MGMT_PRODUCTION.Batch_Date
        'from TSPL_SHIFT_MGMT_PRODUCTION
        'left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_SHIFT_MGMT_PRODUCTION.BOM_Code
        'left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE
        'left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE 
        'left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
        'left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code=TSPL_SHIFT_MGMT_PRODUCTION.UOM 
        'where TSPL_SHIFT_MGMT_PRODUCTION.Document_No='" + obj.Document_No + "' and TSPL_SHIFT_MGMT_PRODUCTION.Temp=1
        ')xx order by Batch_Date,PK_ID "
        '            Dim dtRM = clsDBFuncationality.GetDataTable(qry, trans)
        '            If dtRM IsNot Nothing AndAlso dtRM.Rows.Count > 0 Then
        '                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
        '                Dim ArrInvetoryMovementNew As New List(Of clsInventoryMovementNew)
        '                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
        '                Dim PKID As Decimal = clsCommon.myCDecimal(dtRM.Rows(0)("PK_ID"))
        '                Dim PKDate As Date = clsCommon.myCDate(dtRM.Rows(0)("Batch_Date"))
        '                ''out the Raw material and Packing Item
        '                For Each drRM As DataRow In dtRM.Rows
        '                    If PKID <> clsCommon.myCDecimal(drRM("PK_ID")) Then
        '                        If ArrInvetoryMovementNew.Count > 0 Then
        '                            clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
        '                        End If
        '                        If ArrInventoryMovement.Count > 0 Then
        '                            clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        '                        End If
        '                        ArrInventoryMovement = New List(Of clsInventoryMovement)
        '                        ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
        '                        PKID = clsCommon.myCDecimal(drRM("PK_ID"))
        '                    End If

        '                    If Not settAllowNegativeStockInDairyProduction Then
        '                        Dim CheckStockServerDate As Boolean
        '                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
        '                            CheckStockServerDate = True
        '                        Else
        '                            CheckStockServerDate = False
        '                        End If
        '                        Dim strLocation As String = ""
        '                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Product_Type")), "MI") = CompairStringResult.Equal Then
        '                            strLocation = obj.Shift_Code
        '                            Dim strMainLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select main_location_code from tspl_location_master where Location_Code='" + obj.Shift_Code + "'", trans))
        '                            dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(drRM("ITEM_CODE")), strMainLocation, obj.Shift_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), clsCommon.myCDate(drRM("Batch_Date"))), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 1)
        '                        Else
        '                            strLocation = obj.Location_PK
        '                            dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(drRM("ITEM_CODE")), obj.Location_PK, "", IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), clsCommon.myCDate(drRM("Batch_Date"))), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 2)
        '                        End If


        '                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                            If clsCommon.myCDecimal(drRM("Qty")) > clsCommon.myCDecimal(dt.Rows(0)("qty")) Then
        '                                If Math.Abs(clsCommon.myCDecimal(drRM("Qty")) - clsCommon.myCDecimal(dt.Rows(0)("qty"))) > 0.01 Then
        '                                    Throw New Exception("Item [" + clsCommon.myCstr(drRM("ITEM_CODE")) + "] Location [" + strLocation + "] Issue Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(drRM("Qty"))) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(dt.Rows(0)("qty"))) + "]")
        '                                End If
        '                            End If
        '                        End If
        '                        'If isCheckFutureBalance Then
        '                        '    Dim Product_Type As String = clsItemMaster.GetItemProductType(clsCommon.myCstr(drRM("ITEM_CODE")), trans)
        '                        '    Dim FutureBalanceQty As Decimal = 0
        '                        '    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
        '                        '        FutureBalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(drRM("ITEM_CODE")), clsLocation.GetMainLocationMilk(objtr.frm_loc_code, trans), objtr.frm_loc_code, "", clsCommon.myCDate(drRM("Batch_Date")), trans, clsCommon.myCstr(drRM("UNIT_CODE")))
        '                        '    Else
        '                        '        FutureBalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(drRM("ITEM_CODE")), objtr.frm_loc_code, "", clsCommon.myCDate(drRM("Batch_Date")), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 0)
        '                        '    End If
        '                        '    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
        '                        '    If clsCommon.myCDecimal(drRM("Qty")) > FutureBalanceQty Then
        '                        '        If Math.Abs(clsCommon.myCDecimal(drRM("Qty")) - FutureBalanceQty) > 0.01 Then
        '                        '            Throw New Exception("Item [" + clsCommon.myCstr(drRM("ITEM_CODE")) + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(drRM("Qty"))) + "] is more than Future Mininium Balance Qty [" + clsCommon.myCstr(FutureBalanceQty) + "]")
        '                        '        End If
        '                        '    End If
        '                        'End If
        '                    End If

        '                    If clsCommon.CompairString(clsCommon.myCstr(drRM("Product_Type")), "MI") = CompairStringResult.Equal Then
        '                        Dim objInventoryMovemnt As New clsInventoryMovementNew()
        '                        objInventoryMovemnt.Source_Doc_Date = clsCommon.myCDate(drRM("Batch_Date"))
        '                        objInventoryMovemnt.InOut = "O"
        '                        objInventoryMovemnt.main_location = ""
        '                        objInventoryMovemnt.Location_Code = obj.Shift_Code
        '                        objInventoryMovemnt.Other_Location_Code = ""
        '                        objInventoryMovemnt.Other_Location_Desc = ""
        '                        objInventoryMovemnt.Item_Code = clsCommon.myCstr(drRM("ITEM_CODE"))
        '                        objInventoryMovemnt.Item_Desc = clsCommon.myCstr(drRM("Item_Desc"))
        '                        objInventoryMovemnt.Qty = clsCommon.myCDecimal(drRM("Qty"))
        '                        objInventoryMovemnt.UOM = clsCommon.myCstr(drRM("UNIT_CODE"))
        '                        objInventoryMovemnt.MRP = Nothing
        '                        objInventoryMovemnt.Add_Cost = Nothing
        '                        objInventoryMovemnt.Net_Cost = Nothing
        '                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "R") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.ItemType = "RM"
        '                        ElseIf clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "F") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.ItemType = "FT"
        '                        Else
        '                            objInventoryMovemnt.ItemType = clsCommon.myCstr(drRM("Item_Type"))
        '                        End If
        '                        objInventoryMovemnt.Basic_Cost = Nothing
        '                        objInventoryMovemnt.Comment = clsCommon.myCstr(drRM("Comment"))
        '                        objInventoryMovemnt.MFG_Date = Nothing
        '                        objInventoryMovemnt.Expiry_Date = Nothing
        '                        objInventoryMovemnt.FAT_Per = clsCommon.myCDecimal(drRM("fat"))
        '                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(drRM("fat_kg"))
        '                        objInventoryMovemnt.SNF_Per = clsCommon.myCDecimal(drRM("snf"))
        '                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(drRM("snf_kg"))


        '                        Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", clsCommon.myCstr(drRM("Product_Type")), clsCommon.myCstr(drRM("ITEM_CODE")), obj.Shift_Code, clsCommon.myCDecimal(drRM("Qty")), clsCommon.myCstr(drRM("UNIT_CODE")), clsCommon.myCDecimal(drRM("fat_kg")), clsCommon.myCDecimal(drRM("snf_kg")), clsCommon.myCDate(drRM("Batch_Date")), clsCommon.myCDate(drRM("Batch_Date")), False, trans)
        '                        objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
        '                        objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
        '                        objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
        '                        objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
        '                        Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
        '                        objInventoryMovemnt.FIFO_Cost = cost
        '                        objInventoryMovemnt.Avg_Cost = cost
        '                        objInventoryMovemnt.LIFO_Cost = cost
        '                        objInventoryMovemnt.CalculateAvgCost = False
        '                        objInventoryMovemnt.Ref_Line_No = clsCommon.myCDecimal(drRM("PK_ID"))
        '                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)
        '                    Else
        '                        Dim objInventoryMovemnt As New clsInventoryMovement()
        '                        objInventoryMovemnt.InOut = "O"
        '                        objInventoryMovemnt.Location_Code = obj.Location_PK
        '                        objInventoryMovemnt.Other_Location_Code = ""
        '                        objInventoryMovemnt.Other_Location_Desc = ""
        '                        objInventoryMovemnt.Item_Code = clsCommon.myCstr(drRM("ITEM_CODE"))
        '                        objInventoryMovemnt.Item_Desc = clsCommon.myCstr(drRM("Item_Desc"))
        '                        objInventoryMovemnt.Qty = clsCommon.myCDecimal(drRM("Qty"))
        '                        objInventoryMovemnt.UOM = clsCommon.myCstr(drRM("UNIT_CODE"))
        '                        objInventoryMovemnt.MRP = Nothing
        '                        objInventoryMovemnt.Add_Cost = Nothing
        '                        objInventoryMovemnt.Net_Cost = Nothing
        '                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "R") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.ItemType = "RM"
        '                        ElseIf clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "F") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.ItemType = "FT"
        '                        Else
        '                            objInventoryMovemnt.ItemType = clsCommon.myCstr(drRM("Item_Type"))
        '                        End If
        '                        objInventoryMovemnt.Comment = clsCommon.myCstr(drRM("Comment"))
        '                        objInventoryMovemnt.MFG_Date = Nothing
        '                        objInventoryMovemnt.Expiry_Date = Nothing
        '                        objInventoryMovemnt.FAT_Per = clsCommon.myCDecimal(drRM("fat"))
        '                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(drRM("fat_kg"))
        '                        objInventoryMovemnt.SNF_Per = clsCommon.myCDecimal(drRM("snf"))
        '                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(drRM("snf_kg"))

        '                        Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", clsCommon.myCstr(drRM("Product_Type")), clsCommon.myCstr(drRM("ITEM_CODE")), obj.Location_PK, clsCommon.myCDecimal(drRM("Qty")), clsCommon.myCstr(drRM("UNIT_CODE")), clsCommon.myCDecimal(drRM("fat_kg")), clsCommon.myCDecimal(drRM("snf_kg")), clsCommon.myCDate(drRM("Batch_Date")), clsCommon.myCDate(drRM("Batch_Date")), False, trans)
        '                        objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
        '                        objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
        '                        objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
        '                        objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
        '                        Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
        '                        objInventoryMovemnt.FIFO_Cost = cost
        '                        objInventoryMovemnt.Avg_Cost = cost
        '                        objInventoryMovemnt.LIFO_Cost = cost
        '                        'objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
        '                        objInventoryMovemnt.CalculateAvgCost = False
        '                        objInventoryMovemnt.Ref_Line_No = clsCommon.myCDecimal(drRM("PK_ID"))
        '                        ArrInventoryMovement.Add(objInventoryMovemnt)
        '                    End If
        '                Next
        '                If ArrInvetoryMovementNew.Count > 0 Then
        '                    clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
        '                End If
        '                If ArrInventoryMovement.Count > 0 Then
        '                    clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        '                End If

        '                ''In the Finish Goods Item
        '                For Each objtr As clsProductionShiftMgmtProduction In obj.Arr
        '                    ArrInventoryMovement = New List(Of clsInventoryMovement)
        '                    ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)

        '                    qry = "select sum(Fat_KG)as Fat_KG,sum(SNF_KG)as SNF_KG,sum(Fat_Amt)as Fat_Amt,sum(SNF_Amt)as SNF_Amt,sum(Avg_Cost) as Avg_Cost  from(
        'select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
        'union all
        'select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
        'union all
        'select 0 as Fat_KG,0 as SNF_KG,0 as Fat_Amt,0 as SNF_Amt,Amount as Avg_Cost from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL where Against_PKID='" + clsCommon.myCstr(objtr.PK_ID) + "'
        ')xx"
        '                    dt = clsDBFuncationality.GetDataTable(qry, trans)
        '                    Dim strProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
        '                    Dim strItemType As String
        '                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
        '                        Dim objInventoryMovemnt = New clsInventoryMovementNew
        '                        objInventoryMovemnt.Trans_Type = "Production"
        '                        objInventoryMovemnt.InOut = "I"
        '                        objInventoryMovemnt.Location_Code = obj.Location
        '                        objInventoryMovemnt.Item_Code = objtr.Item_Code
        '                        objInventoryMovemnt.Item_Desc = objtr.Item_Name
        '                        objInventoryMovemnt.Qty = objtr.Qty
        '                        objInventoryMovemnt.UOM = objtr.UOM
        '                        objInventoryMovemnt.Source_Doc_No = obj.Document_No
        '                        objInventoryMovemnt.Source_Doc_Date = objtr.Batch_Date
        '                        objInventoryMovemnt.CalculateAvgCost = False
        '                        objInventoryMovemnt.Comment = objtr.Comment

        '                        'objInventoryMovemnt.FAT_Per = objProd.FAT_Per
        '                        'objInventoryMovemnt.SNF_Per = objProd.SNF_Per
        '                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
        '                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
        '                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
        '                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
        '                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
        '                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
        '                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
        '                        objInventoryMovemnt.Avg_Cost = AvgCost
        '                        objInventoryMovemnt.FIFO_Cost = AvgCost
        '                        objInventoryMovemnt.LIFO_Cost = AvgCost
        '                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objtr.Qty)
        '                            objInventoryMovemnt.Net_Cost = AvgCost
        '                        End If

        '                        strItemType = clsItemMaster.GetItemType(objtr.Item_Code, trans)
        '                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
        '                            strItemType = "RM"
        '                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
        '                            strItemType = "OT"
        '                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
        '                            strItemType = "FT"

        '                        End If
        '                        objInventoryMovemnt.ItemType = strItemType
        '                        objInventoryMovemnt.MFG_Date = objtr.Batch_Date
        '                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)

        '                    Else
        '                        Dim objInventoryMovemnt As New clsInventoryMovement
        '                        objInventoryMovemnt.Trans_Type = "Production"
        '                        objInventoryMovemnt.InOut = "I"
        '                        objInventoryMovemnt.Location_Code = obj.Location
        '                        objInventoryMovemnt.Item_Code = objtr.Item_Code
        '                        objInventoryMovemnt.Item_Desc = objtr.Item_Name
        '                        objInventoryMovemnt.Qty = objtr.Qty
        '                        objInventoryMovemnt.UOM = objtr.UOM
        '                        objInventoryMovemnt.Source_Doc_No = objtr.PK_ID
        '                        objInventoryMovemnt.Source_Doc_Date = objtr.Batch_Date
        '                        objInventoryMovemnt.CalculateAvgCost = False
        '                        strItemType = clsItemMaster.GetItemType(objtr.Item_Code, trans)
        '                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
        '                            strItemType = "RM"
        '                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
        '                            strItemType = "OT"
        '                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
        '                            strItemType = "FT"
        '                        End If
        '                        objInventoryMovemnt.ItemType = strItemType
        '                        objInventoryMovemnt.Comment = objtr.Comment

        '                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
        '                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
        '                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
        '                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
        '                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
        '                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
        '                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
        '                        objInventoryMovemnt.Avg_Cost = AvgCost
        '                        objInventoryMovemnt.FIFO_Cost = AvgCost
        '                        objInventoryMovemnt.LIFO_Cost = AvgCost
        '                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
        '                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objtr.Qty)
        '                            objInventoryMovemnt.Net_Cost = AvgCost
        '                        End If
        '                        objInventoryMovemnt.MFG_Date = objtr.Batch_Date
        '                        ArrInventoryMovement.Add(objInventoryMovemnt)

        '                        If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) Then
        '                            Dim arrBatchItem As New List(Of clsBatchInventory)
        '                            Dim objBatchItem As clsBatchInventory = New clsBatchInventory()
        '                            objBatchItem.Comment = objtr.Comment
        '                            objBatchItem.Manufacture_Date = objtr.Batch_Date
        '                            objBatchItem.Expiry_Date = objtr.Batch_Date.AddDays(clsItemMaster.GetSelfLife(objtr.Item_Code, trans))
        '                            objBatchItem.Qty = objtr.Qty
        '                            objBatchItem.Manual_BatchNo = objtr.Comment
        '                            If clsCommon.myLen(objBatchItem.Comment) > 0 AndAlso objBatchItem.Qty <> 0 Then
        '                                arrBatchItem.Add(objBatchItem)
        '                            End If
        '                            clsBatchInventory.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, "I", objtr.Item_Code, obj.Location, 1, 0, objtr.UOM, arrBatchItem, trans)
        '                        End If
        '                    End If

        '                    If ArrInvetoryMovementNew.Count > 0 Then
        '                        clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
        '                    End If
        '                    If ArrInventoryMovement.Count > 0 Then
        '                        clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        '                    End If
        '                Next
        '            End If



        '            Dim ArryLstGLAC As ArrayList = New ArrayList()
        '            qry = "select xxx.InOut,xxx.Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,xxx.Avg_Cost from (
        'select InOut,Item_Code,sum(Avg_Cost) as Avg_Cost  from(
        'select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in (select cast( PK_ID as varchar) from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "') 
        'and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
        'union all
        'select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in (select cast( PK_ID as varchar) from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "') 
        'and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
        ') xx group by Item_Code,InOut 
        ') xxx
        'left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xxx.Item_Code
        'left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code
        'order by InOut desc"
        '            dt = clsDBFuncationality.GetDataTable(qry, trans)
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For Each dr As DataRow In dt.Rows
        '                    If clsCommon.myLen(dr("Inv_Control_Account")) <= 0 Then
        '                        Throw New Exception("Inventory control Account not found for Item " & clsCommon.myCstr(dr("Item_Code")) & "")
        '                    End If
        '                    Dim InvCtrlAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("Inv_Control_Account")), obj.Location, trans)
        '                    Dim RI As Integer = -1
        '                    If clsCommon.CompairString(clsCommon.myCstr(dr("InOut")), "I") = CompairStringResult.Equal Then
        '                        RI = 1
        '                    End If
        '                    If clsCommon.myLen(InvCtrlAcc) > 0 Then
        '                        Dim Acc1() As String = {InvCtrlAcc, RI * clsCommon.myCDecimal(dr("Avg_Cost"))}
        '                        ArryLstGLAC.Add(Acc1)
        '                    End If
        '                Next
        '            End If

        '            qry = "select Cost_Code,max(GL_Acc) as GL_Acc,sum(Amount) as Amount from (
        'select TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Cost_Code,TSPL_OVERHEAD_COST.GL_Acc,Amount from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL
        'left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Cost_Code
        'where Document_No='" + obj.Document_No + "' 
        ')x group by Cost_Code"
        '            dt = clsDBFuncationality.GetDataTable(qry, trans)
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For Each dr As DataRow In dt.Rows
        '                    If clsCommon.myLen(dr("GL_Acc")) <= 0 Then
        '                        Throw New Exception("GL Account not found for cost code " & clsCommon.myCstr(dr("Item_Code")) & "")
        '                    End If
        '                    Dim GLAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("GL_Acc")), obj.Location, trans)
        '                    If clsCommon.myLen(GLAcc) > 0 Then
        '                        Dim Acc2() As String = {GLAcc, -1 * clsCommon.myCDecimal(dr("Amount"))}
        '                        ArryLstGLAC.Add(Acc2)
        '                    End If
        '                Next
        '            End If
        '            If ArryLstGLAC IsNot Nothing AndAlso ArryLstGLAC.Count > 0 Then
        '                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location, False, trans, obj.Document_Date, "Production Entry", "PR-UP", "Production Entry", obj.Document_No, obj.Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "Journal Entry Against Production Uploader Entry- Doc No." & obj.Document_No & "", "")
        '            End If
        '        Catch ex As Exception
        '            Throw New Exception(ex.Message)
        '        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        'Try
        '    Dim Qry As String = "select Status from TSPL_SHIFT_MGMT where Document_No='" + strCode + "'"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '        Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
        '    End If

        '    If Not clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
        '        Throw New Exception("Transaction status should be posted for reverse and unpost")
        '    End If


        '    Qry = "delete from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL where Document_No='" + strCode + "'"
        '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        '    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-UP' and Source_Doc_No='" + strCode + "'", trans)
        '    If clsCommon.myLen(VoucherNo) > 0 Then
        '        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
        '        Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
        '        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        '        Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
        '        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        '    End If

        '    Dim arr As List(Of clsProductionShiftMgmtProduction) = clsProductionShiftMgmtProduction.GetData(strCode, "", trans)
        '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
        '        For Each objtr As clsProductionShiftMgmtProduction In arr
        '            clsBatchInventory.ReverseAndUnpost(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), trans)

        '            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'"
        '            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        '            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'"
        '            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        '        Next
        '    End If

        '    Qry = "Update TSPL_SHIFT_MGMT set Status = 0 where Document_No='" + strCode + "'"
        '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
        Return True
    End Function
End Class
Public Class clsProductionShiftMgmtOpen
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtOpen), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtOpen In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_OPEN", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtOpen)
        Dim arr As List(Of clsProductionShiftMgmtOpen) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_OPEN.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc  FROM TSPL_SHIFT_MGMT_OPEN 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_OPEN.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_OPEN.Location_Code 
where  TSPL_SHIFT_MGMT_OPEN.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_OPEN.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtOpen)
            Dim objTr As clsProductionShiftMgmtOpen
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtOpen
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtReceiptPlantMilk
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Shift As String
    Public Reject_Type As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Remarks As String

#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtReceiptPlantMilk), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtReceiptPlantMilk In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Shift", objTR.Shift)
                clsCommon.AddColumnsForChange(coll, "Reject_Type", objTR.Reject_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtReceiptPlantMilk)
        Dim arr As List(Of clsProductionShiftMgmtReceiptPlantMilk) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.Item_Code 
where  TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtReceiptPlantMilk)
            Dim objTr As clsProductionShiftMgmtReceiptPlantMilk
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtReceiptPlantMilk
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Shift = clsCommon.myCstr(dr("Shift"))
                objTr.Reject_Type = clsCommon.myCstr(dr("Reject_Type"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtReceiptBulkMilk
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Trans_Type As String
    Public Against_MilkTransferIn As String
    Public Against_BulkMilkSRN As String
    Public Against_Adjustment As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtReceiptBulkMilk), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtReceiptBulkMilk In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Trans_Type", objTR.Trans_Type)
                clsCommon.AddColumnsForChange(coll, "Against_MilkTransferIn", objTR.Against_MilkTransferIn, True)
                clsCommon.AddColumnsForChange(coll, "Against_BulkMilkSRN", objTR.Against_BulkMilkSRN, True)
                clsCommon.AddColumnsForChange(coll, "Against_Adjustment", objTR.Against_Adjustment, True)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtReceiptBulkMilk)
        Dim arr As List(Of clsProductionShiftMgmtReceiptBulkMilk) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Item_Code 
where  TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtReceiptBulkMilk)
            Dim objTr As clsProductionShiftMgmtReceiptBulkMilk
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtReceiptBulkMilk
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Against_MilkTransferIn = clsCommon.myCstr(dr("Against_MilkTransferIn"))
                objTr.Against_BulkMilkSRN = clsCommon.myCstr(dr("Against_BulkMilkSRN"))
                objTr.Against_Adjustment = clsCommon.myCstr(dr("Against_Adjustment"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProduction
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String
    Public BOM_Code As String
    Public Entered_UOM As Integer ''1 LTR 2'KG
    Public ArrRM As List(Of clsProductionShiftMgmtProductionRM)

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtProduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProduction In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", objTR.BOM_Code)
                clsCommon.AddColumnsForChange(coll, "Entered_UOM", objTR.Entered_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsProductionShiftMgmtProductionRM.SaveData(DocumentNo, objTR.PK_ID, objTR.ArrRM, trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProduction)
        Dim arr As List(Of clsProductionShiftMgmtProduction) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProduction)
            Dim objTr As clsProductionShiftMgmtProduction
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProduction
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objTr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objTr.Entered_UOM = clsCommon.myCDecimal(dr("Entered_UOM"))
                objTr.ArrRM = clsProductionShiftMgmtProductionRM.GetData(objTr.Document_No, objTr.PK_ID, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProductionRM
#Region "Variables"
    Public PK_ID As Integer
    Public Against_PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstPKID As Integer, ByVal Arr As List(Of clsProductionShiftMgmtProductionRM), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRM In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_PK_ID", AgainstPKID)
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRM)
        Dim arr As List(Of clsProductionShiftMgmtProductionRM) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION_RM " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_PRODUCTION_RM.Against_PK_ID=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRM)
            Dim objTr As clsProductionShiftMgmtProductionRM
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRM
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_PK_ID = clsCommon.myCstr(dr("Against_PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProductionShiftMgmtProductionRMSummary
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Arr As List(Of clsProductionShiftMgmtProductionRMIssue)
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtProductionRMSummary), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRMSummary In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsProductionShiftMgmtProductionRMIssue.SaveData(DocumentNo, objTR.PK_ID, objTR.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRMSummary)
        Dim arr As List(Of clsProductionShiftMgmtProductionRMSummary) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRMSummary)
            Dim objTr As clsProductionShiftMgmtProductionRMSummary
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRMSummary
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Arr = clsProductionShiftMgmtProductionRMIssue.GetData(strPONo, objTr.PK_ID, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProductionShiftMgmtProductionRMIssue
#Region "Variables"
    Public PK_ID As Integer
    Public Against_RM_Summary As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstRMSummary As Integer, ByVal Arr As List(Of clsProductionShiftMgmtProductionRMIssue), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRMIssue In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_RM_Summary", AgainstRMSummary)
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRMIssue)
        Dim arr As List(Of clsProductionShiftMgmtProductionRMIssue) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc 
FROM TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Item_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Location_Code
where TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Against_RM_Summary=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRMIssue)
            Dim objTr As clsProductionShiftMgmtProductionRMIssue
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRMIssue
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_RM_Summary = clsCommon.myCstr(dr("Against_RM_Summary"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtClose
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtClose), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtClose In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_CLOSE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtClose)
        Dim arr As List(Of clsProductionShiftMgmtClose) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_CLOSE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc   FROM TSPL_SHIFT_MGMT_CLOSE 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_CLOSE.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_CLOSE.Location_Code 
where  TSPL_SHIFT_MGMT_CLOSE.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_CLOSE.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtClose)
            Dim objTr As clsProductionShiftMgmtClose
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtClose
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class



