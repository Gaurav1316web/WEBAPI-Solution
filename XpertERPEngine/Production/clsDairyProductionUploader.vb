Imports common
Imports System.Data.SqlClient

Public Class clsDairyProductionUploader
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Location_FG As String
    Public Location_FG_Name As String
    Public Location_RM As String
    Public Location_RM_Name As String
    Public Location_PK As String
    Public Location_PK_Name As String
    Public Description As String
    Public Batch_No As String
    Public Batch_Date As Date
    Public Arr As List(Of clsDairyProductionUploaderDetail) = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDairyProductionUploader, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_PRODUCTION_UPLOADER_QC where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PRODUCTION_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.Location_FG) <= 0 Then
                Throw New Exception("Please provide FG location")
            End If
            If clsCommon.myLen(obj.Location_RM) <= 0 Then
                Throw New Exception("Please provide location to pick raw milk")
            End If
            If clsCommon.myLen(obj.Location_PK) <= 0 Then
                Throw New Exception("Please provide location to pick packing item")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_FG, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_FG", obj.Location_FG)
            clsCommon.AddColumnsForChange(coll, "Location_RM", obj.Location_RM)
            clsCommon.AddColumnsForChange(coll, "Location_PK", obj.Location_PK)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
            clsCommon.AddColumnsForChange(coll, "Batch_Date", clsCommon.GetPrintDate(obj.Batch_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DairyProductionUploader, "", obj.Location_FG)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_HEAD", OMInsertOrUpdate.Update, "TSPL_PRODUCTION_UPLOADER_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsDairyProductionUploaderDetail.SaveData(obj, trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isOrderByDate As Boolean) As clsDairyProductionUploader
        Dim obj As clsDairyProductionUploader = Nothing
        Dim qry As String = "SELECT TSPL_PRODUCTION_UPLOADER_HEAD.*,TSPL_LOCATION_MASTER_FG.Location_Desc as Location_FG_Name,TSPL_LOCATION_MASTER_RM.Location_Desc as Location_RM_Name,TSPL_LOCATION_MASTER_PK.Location_Desc as Location_PK_Name 
FROM TSPL_PRODUCTION_UPLOADER_HEAD 
left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_FG on TSPL_LOCATION_MASTER_FG.Location_Code=TSPL_PRODUCTION_UPLOADER_HEAD.Location_FG 
left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_RM on TSPL_LOCATION_MASTER_RM.Location_Code=TSPL_PRODUCTION_UPLOADER_HEAD.Location_RM 
left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PK on TSPL_LOCATION_MASTER_PK.Location_Code=TSPL_PRODUCTION_UPLOADER_HEAD.Location_PK 
where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = (select MIN(Document_No) from TSPL_PRODUCTION_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_PRODUCTION_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = (select Min(Document_No) from TSPL_PRODUCTION_UPLOADER_HEAD where Document_No>'" + strDocNo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_PRODUCTION_UPLOADER_HEAD where Document_No<'" + strDocNo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_PRODUCTION_UPLOADER_HEAD.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDairyProductionUploader()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_FG = clsCommon.myCstr(dt.Rows(0)("Location_FG"))
            obj.Location_FG_Name = clsCommon.myCstr(dt.Rows(0)("Location_FG_Name"))
            obj.Location_RM = clsCommon.myCstr(dt.Rows(0)("Location_RM"))
            obj.Location_RM_Name = clsCommon.myCstr(dt.Rows(0)("Location_RM_Name"))
            obj.Location_PK = clsCommon.myCstr(dt.Rows(0)("Location_PK"))
            obj.Location_PK_Name = clsCommon.myCstr(dt.Rows(0)("Location_PK_Name"))
            obj.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
            obj.Batch_Date = clsCommon.myCDate(dt.Rows(0)("Batch_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.Arr = clsDairyProductionUploaderDetail.GetData(isOrderByDate, obj.Document_No, "", trans)
        End If
        Return obj
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PRODUCTION_UPLOADER_HEAD", "Document_No", "TSPL_PRODUCTION_UPLOADER_DETAIL", "Document_No", "TSPL_PRODUCTION_UPLOADER_QC", "Document_No", trans)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsDairyProductionUploader = clsDairyProductionUploader.GetData(strCode, NavigatorType.Current, trans, False)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_FG, obj.Document_Date, trans)


            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PRODUCTION_UPLOADER_QC where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PRODUCTION_UPLOADER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PRODUCTION_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
            Dim qry As String = "update TSPL_PRODUCTION_UPLOADER_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
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

            Dim obj As clsDairyProductionUploader = clsDairyProductionUploader.GetData(strCode, NavigatorType.Current, trans, True)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_FG, obj.Document_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            MilkProductionUploader(obj, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function MilkProductionUploader(ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            For Each objtr As clsDairyProductionUploaderDetail In obj.Arr
                qry = "select top 1 BOM_CODE from TSPL_PP_BOM_HEAD 
where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + objtr.Item_Code + "'  and '" + clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") + "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date) order by BOM_CODE desc"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                    Throw New Exception("BOM Not Found for Item [" + objtr.Item_Code + "] and Date [" + clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") + "]")
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "BOM_Code", clsCommon.myCstr(dt.Rows(0)("BOM_CODE")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_DETAIL", OMInsertOrUpdate.Update, "PK_ID=" + clsCommon.myCstr(objtr.PK_ID), trans)
            Next

            qry = "insert into TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL (Document_No,Against_PKID,Cost_Code,Amount)  
select '" + obj.Document_No + "' as Document_No,PK_ID,xx.COST_CODE,(xx.prod_qty * (xx.OverHead_Cost/xx.build_qty)) as Amount from (
select TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID,(TSPL_PRODUCTION_UPLOADER_DETAIL.Qty * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty
,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.OverHead_Cost
 from TSPL_PRODUCTION_UPLOADER_DETAIL
left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_Code
inner join TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS on TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code=TSPL_PRODUCTION_UPLOADER_DETAIL.UOM 
where TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No='" + obj.Document_No + "' and TSPL_PRODUCTION_UPLOADER_DETAIL.QC_Status=1
) xx  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "select PK_ID,xx.ITEM_CODE,xx.Item_Desc,xx.Item_Type,xx.UNIT_CODE,xx.Product_Type,(xx.prod_qty * (xx.quantity/xx.build_qty)) as Qty,xx.fat,xx.snf,xx.fat_kg,xx.snf_kg,Batch_No,Is_Batch_Item,Batch_Date from (
select TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID,(TSPL_PRODUCTION_UPLOADER_DETAIL.Qty * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date
,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type
,(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY
,(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf
,(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg
,(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg 
,TSPL_ITEM_MASTER.Is_Batch_Item,TSPL_PRODUCTION_UPLOADER_DETAIL.Batch_No,TSPL_PRODUCTION_UPLOADER_DETAIL.Batch_Date
from TSPL_PRODUCTION_UPLOADER_DETAIL
left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_Code
left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code=TSPL_PRODUCTION_UPLOADER_DETAIL.UOM 
where TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No='" + obj.Document_No + "' and TSPL_PRODUCTION_UPLOADER_DETAIL.QC_Status=1
)xx order by Batch_Date,PK_ID "
            Dim dtRM = clsDBFuncationality.GetDataTable(qry, trans)
            If dtRM IsNot Nothing AndAlso dtRM.Rows.Count > 0 Then
                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                Dim ArrInvetoryMovementNew As New List(Of clsInventoryMovementNew)
                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
                Dim PKID As Decimal = clsCommon.myCDecimal(dtRM.Rows(0)("PK_ID"))
                Dim PKDate As Date = clsCommon.myCDate(dtRM.Rows(0)("Batch_Date"))
                ''out the Raw material and Packing Item
                For Each drRM As DataRow In dtRM.Rows
                    If PKID <> clsCommon.myCDecimal(drRM("PK_ID")) Then
                        If ArrInvetoryMovementNew.Count > 0 Then
                            clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                        End If
                        If ArrInventoryMovement.Count > 0 Then
                            clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                        End If
                        ArrInventoryMovement = New List(Of clsInventoryMovement)
                        ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                        PKID = clsCommon.myCDecimal(drRM("PK_ID"))
                    End If

                    If Not settAllowNegativeStockInDairyProduction Then
                        Dim CheckStockServerDate As Boolean
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
                            CheckStockServerDate = True
                        Else
                            CheckStockServerDate = False
                        End If
                        Dim strLocation As String = ""
                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Product_Type")), "MI") = CompairStringResult.Equal Then
                            strLocation = obj.Location_RM
                            Dim strMainLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select main_location_code from tspl_location_master where Location_Code='" + obj.Location_RM + "'", trans))
                            dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(drRM("ITEM_CODE")), strMainLocation, obj.Location_RM, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), clsCommon.myCDate(drRM("Batch_Date"))), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 1)
                        Else
                            strLocation = obj.Location_PK
                            dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(drRM("ITEM_CODE")), obj.Location_PK, "", IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), clsCommon.myCDate(drRM("Batch_Date"))), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 2)
                        End If


                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.myCDecimal(drRM("Qty")) > clsCommon.myCdbl(dt.Rows(0)("qty")) Then
                                If Math.Abs(clsCommon.myCDecimal(drRM("Qty")) - clsCommon.myCdbl(dt.Rows(0)("qty"))) > 0.01 Then
                                    Throw New Exception("Item [" + clsCommon.myCstr(drRM("ITEM_CODE")) + "] Location [" + strLocation + "] Issue Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(drRM("Qty"))) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("qty"))) + "]")
                                End If
                            End If
                        End If
                        'If isCheckFutureBalance Then
                        '    Dim Product_Type As String = clsItemMaster.GetItemProductType(clsCommon.myCstr(drRM("ITEM_CODE")), trans)
                        '    Dim FutureBalanceQty As Decimal = 0
                        '    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                        '        FutureBalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(drRM("ITEM_CODE")), clsLocation.GetMainLocationMilk(objtr.frm_loc_code, trans), objtr.frm_loc_code, "", clsCommon.myCDate(drRM("Batch_Date")), trans, clsCommon.myCstr(drRM("UNIT_CODE")))
                        '    Else
                        '        FutureBalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(drRM("ITEM_CODE")), objtr.frm_loc_code, "", clsCommon.myCDate(drRM("Batch_Date")), trans, clsCommon.myCstr(drRM("UNIT_CODE")), 0)
                        '    End If
                        '    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                        '    If clsCommon.myCDecimal(drRM("Qty")) > FutureBalanceQty Then
                        '        If Math.Abs(clsCommon.myCDecimal(drRM("Qty")) - FutureBalanceQty) > 0.01 Then
                        '            Throw New Exception("Item [" + clsCommon.myCstr(drRM("ITEM_CODE")) + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(drRM("Qty"))) + "] is more than Future Mininium Balance Qty [" + clsCommon.myCstr(FutureBalanceQty) + "]")
                        '        End If
                        '    End If
                        'End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(drRM("Product_Type")), "MI") = CompairStringResult.Equal Then
                        Dim objInventoryMovemnt As New clsInventoryMovementNew()
                        objInventoryMovemnt.Source_Doc_Date = clsCommon.myCDate(drRM("Batch_Date"))
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.main_location = ""
                        objInventoryMovemnt.Location_Code = obj.Location_RM
                        objInventoryMovemnt.Other_Location_Code = ""
                        objInventoryMovemnt.Other_Location_Desc = ""
                        objInventoryMovemnt.Item_Code = clsCommon.myCstr(drRM("ITEM_CODE"))
                        objInventoryMovemnt.Item_Desc = clsCommon.myCstr(drRM("Item_Desc"))
                        objInventoryMovemnt.Qty = clsCommon.myCDecimal(drRM("Qty"))
                        objInventoryMovemnt.UOM = clsCommon.myCstr(drRM("UNIT_CODE"))
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = clsCommon.myCstr(drRM("Item_Type"))
                        End If
                        objInventoryMovemnt.Basic_Cost = Nothing
                        objInventoryMovemnt.Batch_No = clsCommon.myCstr(drRM("Batch_No"))
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_Per = clsCommon.myCDecimal(drRM("fat"))
                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(drRM("fat_kg"))
                        objInventoryMovemnt.SNF_Per = clsCommon.myCDecimal(drRM("snf"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(drRM("snf_kg"))


                        Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", clsCommon.myCstr(drRM("Product_Type")), clsCommon.myCstr(drRM("ITEM_CODE")), obj.Location_RM, clsCommon.myCDecimal(drRM("Qty")), clsCommon.myCstr(drRM("UNIT_CODE")), clsCommon.myCDecimal(drRM("fat_kg")), clsCommon.myCDecimal(drRM("snf_kg")), clsCommon.myCDate(drRM("Batch_Date")), clsCommon.myCDate(drRM("Batch_Date")), False, trans)
                        objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                        objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                        objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                        objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                        Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = clsCommon.myCDecimal(drRM("PK_ID"))
                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                    Else
                        Dim objInventoryMovemnt As New clsInventoryMovement()
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = obj.Location_PK
                        objInventoryMovemnt.Other_Location_Code = ""
                        objInventoryMovemnt.Other_Location_Desc = ""
                        objInventoryMovemnt.Item_Code = clsCommon.myCstr(drRM("ITEM_CODE"))
                        objInventoryMovemnt.Item_Desc = clsCommon.myCstr(drRM("Item_Desc"))
                        objInventoryMovemnt.Qty = clsCommon.myCDecimal(drRM("Qty"))
                        objInventoryMovemnt.UOM = clsCommon.myCstr(drRM("UNIT_CODE"))
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(drRM("Item_Type")), "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = clsCommon.myCstr(drRM("Item_Type"))
                        End If
                        objInventoryMovemnt.Batch_No = clsCommon.myCstr(drRM("Batch_No"))
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_Per = clsCommon.myCDecimal(drRM("fat"))
                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(drRM("fat_kg"))
                        objInventoryMovemnt.SNF_Per = clsCommon.myCDecimal(drRM("snf"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(drRM("snf_kg"))

                        Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", clsCommon.myCstr(drRM("Product_Type")), clsCommon.myCstr(drRM("ITEM_CODE")), obj.Location_PK, clsCommon.myCDecimal(drRM("Qty")), clsCommon.myCstr(drRM("UNIT_CODE")), clsCommon.myCDecimal(drRM("fat_kg")), clsCommon.myCDecimal(drRM("snf_kg")), clsCommon.myCDate(drRM("Batch_Date")), clsCommon.myCDate(drRM("Batch_Date")), False, trans)
                        objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                        objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                        objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                        objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                        Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        'objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = clsCommon.myCDecimal(drRM("PK_ID"))
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    End If
                Next
                If ArrInvetoryMovementNew.Count > 0 Then
                    clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
                If ArrInventoryMovement.Count > 0 Then
                    clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If

                ''In the Finish Goods Item
                For Each objtr As clsDairyProductionUploaderDetail In obj.Arr
                    ArrInventoryMovement = New List(Of clsInventoryMovement)
                    ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)

                    qry = "select sum(Fat_KG)as Fat_KG,sum(SNF_KG)as SNF_KG,sum(Fat_Amt)as Fat_Amt,sum(SNF_Amt)as SNF_Amt,sum(Avg_Cost) as Avg_Cost  from(
select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
union all
select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
union all
select 0 as Fat_KG,0 as SNF_KG,0 as Fat_Amt,0 as SNF_Amt,Amount as Avg_Cost from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL where Against_PKID='" + clsCommon.myCstr(objtr.PK_ID) + "'
)xx"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim strProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                    Dim strItemType As String
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        Dim objInventoryMovemnt = New clsInventoryMovementNew
                        objInventoryMovemnt.Trans_Type = "Production"
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = obj.Location_FG
                        objInventoryMovemnt.Item_Code = objtr.Item_Code
                        objInventoryMovemnt.Item_Desc = objtr.Item_Name
                        objInventoryMovemnt.Qty = objtr.Qty
                        objInventoryMovemnt.UOM = objtr.UOM
                        objInventoryMovemnt.Source_Doc_No = obj.Document_No
                        objInventoryMovemnt.Source_Doc_Date = objtr.Batch_Date
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Batch_No = objtr.Batch_No

                        'objInventoryMovemnt.FAT_Per = objProd.FAT_Per
                        'objInventoryMovemnt.SNF_Per = objProd.SNF_Per
                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Avg_Cost = AvgCost
                        objInventoryMovemnt.FIFO_Cost = AvgCost
                        objInventoryMovemnt.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objtr.Qty)
                            objInventoryMovemnt.Net_Cost = AvgCost
                        End If

                        strItemType = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemType = "FT"

                        End If
                        objInventoryMovemnt.ItemType = strItemType
                        objInventoryMovemnt.MFG_Date = objtr.Batch_Date
                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)

                    Else
                        Dim objInventoryMovemnt As New clsInventoryMovement
                        objInventoryMovemnt.Trans_Type = "Production"
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = obj.Location_FG
                        objInventoryMovemnt.Item_Code = objtr.Item_Code
                        objInventoryMovemnt.Item_Desc = objtr.Item_Name
                        objInventoryMovemnt.Qty = objtr.Qty
                        objInventoryMovemnt.UOM = objtr.UOM
                        objInventoryMovemnt.Source_Doc_No = objtr.PK_ID
                        objInventoryMovemnt.Source_Doc_Date = objtr.Batch_Date
                        objInventoryMovemnt.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemType = "FT"
                        End If
                        objInventoryMovemnt.ItemType = strItemType
                        objInventoryMovemnt.Batch_No = objtr.Batch_No

                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Avg_Cost = AvgCost
                        objInventoryMovemnt.FIFO_Cost = AvgCost
                        objInventoryMovemnt.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objtr.Qty)
                            objInventoryMovemnt.Net_Cost = AvgCost
                        End If
                        objInventoryMovemnt.MFG_Date = objtr.Batch_Date
                        ArrInventoryMovement.Add(objInventoryMovemnt)

                        If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) Then
                            Dim arrBatchItem As New List(Of clsBatchInventory)
                            Dim objBatchItem As clsBatchInventory = New clsBatchInventory()
                            objBatchItem.Batch_No = objtr.Batch_No
                            objBatchItem.Manufacture_Date = objtr.Batch_Date
                            objBatchItem.Expiry_Date = objtr.Batch_Date.AddDays(clsItemMaster.GetSelfLife(objtr.Item_Code, trans))
                            objBatchItem.Qty = objtr.Qty
                            objBatchItem.Manual_BatchNo = objtr.Batch_No
                            If clsCommon.myLen(objBatchItem.Batch_No) > 0 AndAlso objBatchItem.Qty <> 0 Then
                                arrBatchItem.Add(objBatchItem)
                            End If
                            clsBatchInventory.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, "I", objtr.Item_Code, obj.Location_FG, 1, 0, objtr.UOM, arrBatchItem, trans)
                        End If
                    End If

                    If ArrInvetoryMovementNew.Count > 0 Then
                        clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                    End If
                    If ArrInventoryMovement.Count > 0 Then
                        clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), objtr.Batch_Date, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                    End If
                Next
            End If



            Dim ArryLstGLAC As ArrayList = New ArrayList()
            qry = "select xxx.InOut,xxx.Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,xxx.Avg_Cost from (
select InOut,Item_Code,sum(Avg_Cost) as Avg_Cost  from(
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in (select cast( PK_ID as varchar) from TSPL_PRODUCTION_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "') 
and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
union all
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in (select cast( PK_ID as varchar) from TSPL_PRODUCTION_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "') 
and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
) xx group by Item_Code,InOut 
) xxx
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xxx.Item_Code
left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code
order by InOut desc"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory control Account not found for Item " & clsCommon.myCstr(dr("Item_Code")) & "")
                    End If
                    Dim InvCtrlAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("Inv_Control_Account")), obj.Location_FG, trans)
                    Dim RI As Integer = -1
                    If clsCommon.CompairString(clsCommon.myCstr(dr("InOut")), "I") = CompairStringResult.Equal Then
                        RI = 1
                    End If
                    If clsCommon.myLen(InvCtrlAcc) > 0 Then
                        Dim Acc1() As String = {InvCtrlAcc, RI * clsCommon.myCDecimal(dr("Avg_Cost"))}
                        ArryLstGLAC.Add(Acc1)
                    End If
                Next
            End If

            qry = "select Cost_Code,max(GL_Acc) as GL_Acc,sum(Amount) as Amount from (
select TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Cost_Code,TSPL_OVERHEAD_COST.GL_Acc,Amount from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL
left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Cost_Code
where Document_No='" + obj.Document_No + "' 
)x group by Cost_Code"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr("GL_Acc")) <= 0 Then
                        Throw New Exception("GL Account not found for cost code " & clsCommon.myCstr(dr("Item_Code")) & "")
                    End If
                    Dim GLAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("GL_Acc")), obj.Location_FG, trans)
                    If clsCommon.myLen(GLAcc) > 0 Then
                        Dim Acc2() As String = {GLAcc, -1 * clsCommon.myCDecimal(dr("Amount"))}
                        ArryLstGLAC.Add(Acc2)
                    End If
                Next
            End If
            If ArryLstGLAC IsNot Nothing AndAlso ArryLstGLAC.Count > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_FG, False, trans, obj.Document_Date, "Production Entry", "PR-UP", "Production Entry", obj.Document_No, obj.Description, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "Journal Entry Against Production Uploader Entry- Doc No." & obj.Document_No & "", "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        Try
            Dim Qry As String = "select Status from TSPL_PRODUCTION_UPLOADER_HEAD where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
            End If

            If Not clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If


            Qry = "delete from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-UP' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim arr As List(Of clsDairyProductionUploaderDetail) = clsDairyProductionUploaderDetail.GetData(False, strCode, "", trans)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsDairyProductionUploaderDetail In arr
                    clsBatchInventory.ReverseAndUnpost(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(objtr.PK_ID), trans)

                    Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(objtr.PK_ID) + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Next
            End If

            Qry = "Update TSPL_PRODUCTION_UPLOADER_HEAD set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDairyProductionUploaderDetail
#Region "Variables"
    Public Document_No As String
    Public PK_ID As Integer
    Public Batch_Date As Date
    Public Shift_Code As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public Batch_No As String
    Public QC_Status As Boolean
    Public ArrQC As List(Of clsDairyProductionUploaderQC) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As Boolean
        If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
            For Each objTR As clsDairyProductionUploaderDetail In obj.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Batch_No", objTR.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Batch_Date", clsCommon.GetPrintDate(objTR.Batch_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Shift_Code", objTR.Shift_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "QC_Status ", IIf(objTR.QC_Status, 1, 0))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)


                clsDairyProductionUploaderQC.SaveData(obj.Document_No, clsERPFuncationality.GetScopeIdentityValue(trans), objTR.ArrQC, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal isOrderByDate As Boolean, ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsDairyProductionUploaderDetail)
        Dim arr As List(Of clsDairyProductionUploaderDetail) = Nothing
        Dim qry As String = "SELECT TSPL_PRODUCTION_UPLOADER_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_PRODUCTION_UPLOADER_DETAIL " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code " + Environment.NewLine +
            " where  TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        If isOrderByDate Then
            qry += " ORDER BY TSPL_PRODUCTION_UPLOADER_DETAIL.Batch_Date,TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID"
        Else
            qry += " ORDER BY TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsDairyProductionUploaderDetail)
            Dim objTr As clsDairyProductionUploaderDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsDairyProductionUploaderDetail
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Batch_Date = clsCommon.myCDate(dr("Batch_Date"))
                objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.Shift_Code = clsCommon.myCstr(dr("Shift_Code"))
                objTr.QC_Status = (clsCommon.myCdbl(dt.Rows(0)("QC_Status")) = 1)
                objTr.ArrQC = clsDairyProductionUploaderQC.GetData(objTr.PK_ID, trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsDairyProductionUploaderQC
#Region "Variables"
    Public Document_No As String
    Public PK_ID As Integer
    Public Against_PK_ID As Integer
    Public QC_Code As String
    Public Value As String

#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal PKID As Integer, ByVal Arr As List(Of clsDairyProductionUploaderQC), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsDairyProductionUploaderQC In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Against_PKID", PKID)
                clsCommon.AddColumnsForChange(coll, "QC_Code", objTR.QC_Code)
                clsCommon.AddColumnsForChange(coll, "Value", objTR.Value)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_QC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstPKID As Integer, ByVal trans As SqlTransaction) As List(Of clsDairyProductionUploaderQC)
        Dim arr As List(Of clsDairyProductionUploaderQC) = Nothing
        Dim qry As String = "SELECT TSPL_PRODUCTION_UPLOADER_QC.*  FROM TSPL_PRODUCTION_UPLOADER_QC where TSPL_PRODUCTION_UPLOADER_QC.Against_PKID=" + clsCommon.myCstr(AgainstPKID) + " "
        qry += " ORDER BY TSPL_PRODUCTION_UPLOADER_QC.PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsDairyProductionUploaderQC)
            Dim objTr As clsDairyProductionUploaderQC
            For Each dr As DataRow In dt.Rows
                objTr = New clsDairyProductionUploaderQC
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_PK_ID = clsCommon.myCDecimal(dr("Against_PKID"))
                objTr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                objTr.Value = clsCommon.myCstr(dr("Value"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

