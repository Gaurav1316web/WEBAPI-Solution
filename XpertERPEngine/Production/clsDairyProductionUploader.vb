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
            Dim qry As String = "delete from TSPL_PRODUCTION_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "'"
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
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PRODUCTION_UPLOADER_HEAD", "Document_No", "TSPL_PRODUCTION_UPLOADER_DETAIL", "Document_No", trans)
        Return True
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
            If dtRM IsNot Nothing AndAlso dtRM.Rows.Count <= 0 Then
                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                Dim ArrInvetoryMovementNew As New List(Of clsInventoryMovementNew)
                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
                Dim PKID As Decimal = clsCommon.myCDecimal(dtRM.Rows(0)("PK_ID"))
                Dim PKDate As Date = clsCommon.myCDate(dtRM.Rows(0)("Batch_Date"))
                For Each drRM As DataRow In dtRM.Rows
                    If PKID <> clsCommon.myCDecimal(dtRM.Rows(0)("PK_ID")) Then
                        If ArrInvetoryMovementNew.Count > 0 Then
                            clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                        End If
                        If ArrInventoryMovement.Count > 0 Then
                            clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                        End If
                        ArrInventoryMovement = New List(Of clsInventoryMovement)
                        ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
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

                '//Check for -ve balance



                For Each objtr As clsDairyProductionUploaderDetail In obj.Arr
                    ArrInventoryMovement = New List(Of clsInventoryMovement)
                    ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)

                    qry = "select sum(Fat_KG)as Fat_KG,sum(SNF_KG)as SNF_KG,sum(Fat_Amt)as Fat_Amt,sum(SNF_Amt)as SNF_Amt,sum(Avg_Cost) as Avg_Cost  from(
select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + objtr.PK_ID + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
union all
select Fat_KG,SNF_KG,Fat_Amt,SNF_Amt,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + objtr.PK_ID + "' and Trans_Type='" + clsUserMgtCode.DariyProductionUploader + "'
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
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")) + clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
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
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")) + clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
                        objInventoryMovemnt.Avg_Cost = AvgCost
                        objInventoryMovemnt.FIFO_Cost = AvgCost
                        objInventoryMovemnt.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objtr.Qty)
                            objInventoryMovemnt.Net_Cost = AvgCost
                        End If
                        objInventoryMovemnt.MFG_Date = objtr.Batch_Date
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    End If

                    If ArrInvetoryMovementNew.Count > 0 Then
                        clsInventoryMovementNew.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                    End If
                    If ArrInventoryMovement.Count > 0 Then
                        clsInventoryMovement.SaveData(clsUserMgtCode.DariyProductionUploader, clsCommon.myCstr(PKID), PKDate, clsCommon.GetPrintDate(PKDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function MilkProductionUploaderOLD(ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As Boolean
        Try
            For Each objtr As clsDairyProductionUploaderDetail In obj.Arr
                Try
                    Dim objPlan As clsProcessProductionPlanning = CreatePlanning(objtr, obj, trans)
                    Dim docNo As String = objPlan.plancode
                    objPlan = clsProcessProductionPlanning.GetData(docNo, "", NavigatorType.Current, trans)

                    Dim objBatch As clsProcessBatchOrder = CreateBatchOrder(objPlan, objtr, obj, trans)
                    docNo = objBatch.Batchcode
                    objBatch = clsProcessBatchOrder.GetData(docNo, "", NavigatorType.Current, trans)

                    Dim objIssue As clsProcessProductionIssueEntry = CreateIssue(objBatch, objPlan, objtr, obj, trans)
                    docNo = objIssue.issuecode
                    objIssue = clsProcessProductionIssueEntry.GetData(docNo, "", NavigatorType.Current, trans)

                    Dim objSP As clsProcessProductionStageProcess = CreateStageProcess(objIssue, objBatch, objtr, obj, trans)
                    docNo = objSP.STAGE_PROCESS_CODE
                    objSP = clsProcessProductionStageProcess.GetData(docNo, NavigatorType.Current, "", trans)

                    Dim objPE As clsProductionEntry = CreateProductionEntry(objSP, objIssue, objBatch, objtr, obj, trans)
                Catch ex As Exception
                    Throw New Exception("Error Row No [" + clsCommon.myCstr(objtr.PK_ID) + "]" + Environment.NewLine + ex.Message)
                End Try
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function CreatePlanning(ByVal objtr As clsDairyProductionUploaderDetail, ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As clsProcessProductionPlanning
        Dim objPlan As New clsProcessProductionPlanning
        'Try
        '    objPlan.plandate = objtr.Batch_Date
        '    objPlan.plandesc = obj.Description + " For " + objtr.Item_Code + "[" + objtr.Item_Name + "]"
        '    objPlan.status = "Approved"
        '    objPlan.Dispatch_Days = 1
        '    objPlan.Batch_No = objtr.Batch_No
        '    objPlan.Structure_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Code  from TSPL_ITEM_MASTER where item_code='" + objtr.Item_Code + "'", trans))
        '    'objPlan.LINE_NO = clsCommon.myCstr(FndLineNo.Value)
        '    'objPlan.CostCenterCode = clsCommon.myCstr(TxtCostCenterCode.Value)
        '    'objPlan.ProfitCenterCode = clsCommon.myCstr(TxtProfitCenterCode.Value)
        '    objPlan.Is_Post = "0"
        '    objPlan.locationcode = obj.Location_FG
        '    objPlan.Uploader_TR_No = objtr.PK_ID
        '    objPlan.Arr = New List(Of clsProcessProductionPlanningDetail)

        '    Dim objPlanTr As New clsProcessProductionPlanningDetail()
        '    objPlanTr.sno = 1
        '    objPlanTr.icode = objtr.Item_Code
        '    objPlanTr.uom = objtr.UOM
        '    objPlanTr.dlvryqty = objtr.Qty
        '    objPlanTr.saleqty = objtr.Qty
        '    objPlanTr.planqty = objtr.Qty
        '    objPlanTr.finalqty = objtr.Qty
        '    'objPlanTr.Remarks = clsCommon.myCstr(grow.Cells(colRem).Value).Replace("'", "`")
        '    objPlanTr.Avg_Sale_Qty = objtr.Qty
        '    objPlan.Arr.Add(objPlanTr)
        '    clsProcessProductionPlanning.SaveData(objPlan, True, trans)
        '    clsProcessProductionPlanning.PostData(objPlan.plancode, trans)
        'Catch ex As Exception
        '    Throw New Exception("Error At Planning " + Environment.NewLine + ex.Message)
        'End Try
        Return objPlan
    End Function

    Private Shared Function CreateBatchOrder(ByVal objPlan As clsProcessProductionPlanning, ByVal objtr As clsDairyProductionUploaderDetail, ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As clsProcessBatchOrder
        Dim objBatch As New clsProcessBatchOrder()
        Try
            'Dim DecimalPoint As Integer = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, trans)))
            'If DecimalPoint <= 0 Then
            '    DecimalPoint = 3
            'End If


            'objBatch.Batchdate = objtr.Batch_Date
            'objBatch.batchdesc = obj.Description + " For " + objtr.Item_Code + "[" + objtr.Item_Name + "]"
            'objBatch.status = "Approved"
            'objBatch.IsPost = "0"

            ''objBatch.LINE_NO = clsCommon.myCstr(FndLineNo.Value)
            ''objBatch.CostCenterCode = clsCommon.myCstr(TxtCostCenterCode.Value)
            ''objBatch.ProfitCenterCode = clsCommon.myCstr(TxtProfitCenterCode.Value)
            ''''----------------
            'objBatch.locationcode = obj.Location_FG
            'objBatch.Is_Job_Work_Inward = False
            'objBatch.itemcatcode = objPlan.Structure_Code
            'objBatch.Plancode = objPlan.plancode
            ''objBatch.Main_batchcode = clsCommon.myCstr(txtmain_batch.Text)
            ''objBatch.Sub_batchcode = "" 'clsCommon.myCstr(txtsub_batch.Text)
            'objBatch.Batch_No = objPlan.Batch_No
            ''objBatch.ManualBatchNo = clsCommon.myCstr(txtManualBatchNo.Text)
            'objBatch.closeyn = "0"
            ''If chkclose.Checked Then
            ''    objBatch.closeyn = "1"
            ''End If
            ''--------------------------main item grid

            'objBatch.ArrMainItem = New List(Of clsProcessBatchOrderMainDetail)

            'Dim objBatchTr As New clsProcessBatchOrderMainDetail()
            'objBatchTr.SNO = 1
            'Dim qry As String = "select top 1 BOM_CODE,Revision_No from TSPL_PP_BOM_HEAD where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + objtr.Item_Code + "' " + Environment.NewLine +
            '"and '" + clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") + "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date) order by BOM_CODE desc"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
            '    Throw New Exception("BOM Not Found for Item [" + objtr.Item_Code + "] and Date [" + clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") + "]")
            'End If
            'objBatchTr.bomcode = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))
            'objBatchTr.BOM_Revision_No = clsCommon.myCstr(dt.Rows(0)("Revision_No"))
            'objBatchTr.icode = objtr.Item_Code
            'objBatchTr.qty = objtr.Qty
            'objBatchTr.UOM = objtr.UOM
            'objBatchTr.Plan_Code = objPlan.plancode
            'objBatchTr.shiftcode = objtr.Shift_Code
            'objBatchTr.sectioncode = objtr.Batch_No
            'objBatchTr.bom_fat_pers = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + objtr.Item_Code + "' and TSPL_PARAMETER_MASTER.Type='FAT'", trans)), 2)
            'objBatchTr.bom_fat_kg = clsBOM.GetFatSNFKG_AfterConversion(objtr.Item_Code, objtr.UOM, objtr.Qty, objBatchTr.bom_fat_pers, trans)
            'objBatchTr.bom_snf_pers = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + objtr.Item_Code + "' and TSPL_PARAMETER_MASTER.Type='SNF'", trans)), 2)
            'objBatchTr.bom_snf_kg = clsBOM.GetFatSNFKG_AfterConversion(objtr.Item_Code, objtr.UOM, objtr.Qty, objBatchTr.bom_snf_pers, trans)
            'objBatch.ArrMainItem.Add(objBatchTr)


            'qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BO_TEMP'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            'If check <= 0 Then
            '    qry = "create table BO_TEMP (BOM_Code varchar(30),item_code varchar(50),UOM varchar(12) null,Qty float)"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'ElseIf check > 0 Then
            '    qry = "drop table BO_TEMP"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '    qry = "create table BO_TEMP (BOM_Code varchar(30),item_code varchar(50),UOM varchar(12) null,Qty float)"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If


            'Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "BOM_Code", objBatchTr.bomcode)
            'clsCommon.AddColumnsForChange(coll, "item_code", objtr.Item_Code)
            'clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
            'clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
            'clsCommonFunctionality.UpdateDataTable(coll, "BO_TEMP", OMInsertOrUpdate.Insert, "", trans)

            'qry = "select axx.deactive,axx.effective_date,axx.ITEM_CODE,axx.item_desc,axx.Item_Type,axx.unit_code,axx.Product_Type,sum(axx.Final_Qty) as QUANTITY,round(sum(axx.fat)/count(axx.ITEM_CODE),2) as fat,round(sum(axx.SNF)/count(axx.ITEM_CODE),2) as snf,sum(axx.fat_kg) as fat_kg,sum(axx.snf_kg) as snf_kg from ("
            'qry += "select ax.bom_code,ax.prod_item_code,ax.deactive,ax.effective_date,ax.ITEM_CODE,ax.item_desc,ax.Item_Type,ax.unit_code,ax.Product_Type,ax.quantity,ax.fat,ax.snf,ax.fat_kg,ax.snf_kg,ax.prod_qty,(ax.prod_qty * (ax.quantity/ax.build_qty)) as Final_Qty from ("
            'qry += "select (BO_TEMP.qty *finalcnvrsn.Conversion_Factor/ tspl_item_uom_detail.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type,(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY,(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf,(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg,(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE left outer join tspl_pp_bom_head on tspl_pp_bom_head.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code "
            'qry += "left outer join BO_TEMP on BO_TEMP.bom_code=tspl_pp_bom_head.bom_code and BO_TEMP.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code and BO_TEMP.item_code=tspl_pp_bom_head.prod_item_code "
            'qry += "left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_pp_bom_head.prod_item_code and tspl_item_uom_detail.uom_code=tspl_pp_bom_head.prod_item_unit_code "
            'qry += "left outer join tspl_item_uom_detail finalcnvrsn on finalcnvrsn.item_code=tspl_pp_bom_head.prod_item_code and finalcnvrsn.uom_code=bo_temp.uom "
            'qry += " where TSPL_PP_BOM_ITEM_DETAIL.bom_code in ('" + objBatchTr.bomcode + "') "
            'qry += ")ax)axx group by axx.deactive,axx.effective_date,axx.ITEM_CODE,axx.item_desc,axx.Item_Type,axx.unit_code,axx.Product_Type"
            'dt = New DataTable()
            'dt = clsDBFuncationality.GetDataTable(qry, trans)



            'Dim deactive As Integer = 0
            'Dim effectivedate As Date = Nothing
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    objBatch.ArrRawItem = New List(Of clsProcessBatchOrderRawDetail)
            '    For ii As Integer = 0 To dt.Rows.Count - 1
            '        deactive = CInt(dt.Rows(ii)("deactive"))
            '        effectivedate = clsCommon.myCDate(dt.Rows(ii)("effective_date"))
            '        If deactive >= 1 AndAlso clsCommon.GetPrintDate(objtr.Batch_Date, "dd/MMM/yyyy") > clsCommon.GetPrintDate(effectivedate, "dd/MMM/yyyy") Then
            '            Continue For
            '        End If
            '        Dim objBatchRawTr As New clsProcessBatchOrderRawDetail()

            '        objBatchRawTr.Rawsno = ii + 1
            '        objBatchRawTr.Rawomcode = objBatchTr.bomcode
            '        objBatchRawTr.Proditem = objtr.Item_Code
            '        objBatchRawTr.rawicode = clsCommon.myCstr(dt.Rows(ii)("ITEM_CODE"))
            '        objBatchRawTr.rawunit = clsCommon.myCstr(dt.Rows(ii)("UNIT_CODE"))
            '        objBatchRawTr.rawqty = Math.Round(clsCommon.myCdbl(dt.Rows(ii)("QUANTITY")), DecimalPoint)
            '        objBatchRawTr.fat = Math.Round(clsCommon.myCdbl(dt.Rows(ii)("FAT")), 2)
            '        objBatchRawTr.snf = Math.Round(clsCommon.myCdbl(dt.Rows(ii)("SNF")), 2)
            '        objBatchRawTr.fat_kg = clsBOM.GetFatSNFKG_AfterConversion(objBatchRawTr.rawicode, objBatchRawTr.rawunit, objBatchRawTr.rawqty, objBatchRawTr.fat, trans)
            '        objBatchRawTr.snf_kg = clsBOM.GetFatSNFKG_AfterConversion(objBatchRawTr.rawicode, objBatchRawTr.rawunit, objBatchRawTr.rawqty, objBatchRawTr.snf, trans)
            '        objBatchRawTr.rawbatchcode = ""
            '        objBatchRawTr.remarks = ""
            '        objBatch.ArrRawItem.Add(objBatchRawTr)
            '    Next
            'End If

            'qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BO_TEMP'"
            'check = clsDBFuncationality.getSingleValue(qry, trans)
            'If check > 0 Then
            '    qry = "drop table BO_TEMP"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If

            'dt = Nothing
            'clsProcessBatchOrder.SaveData(objBatch, True, trans)
            'clsProcessBatchOrder.PostData(objBatch.Batchcode, trans)
        Catch ex As Exception
            Throw New Exception("Error At Batch order " + Environment.NewLine + ex.Message)
        End Try
        Return objBatch
    End Function

    Private Shared Function CreateIssue(ByVal objBatch As clsProcessBatchOrder, ByVal objPlan As clsProcessProductionPlanning, ByVal objtr As clsDairyProductionUploaderDetail, ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As clsProcessProductionIssueEntry
        Dim objIssue As New clsProcessProductionIssueEntry()
        Try
            'Dim CheckStockServerDate As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)) = 1)
            'Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, trans))
            'Dim DecimalPoint As Integer = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, trans)))
            'If DecimalPoint <= 0 Then
            '    DecimalPoint = 3
            'End If
            'Dim settTankerDispatchAvgFATSNFPer As Integer = settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, trans)) = 1)
            'Dim From_SubLocation_YN As Integer

            'Dim qry As String
            'objIssue.issue_date = objtr.Batch_Date
            'objIssue.issuedesc = obj.Description + " For " + objtr.Item_Code + "[" + objtr.Item_Name + "]"
            'objIssue.status = "Approved"
            'objIssue.batch_code = objBatch.Batchcode
            'objIssue.Main_Loc_Code = obj.Location_FG
            'objIssue.Against_BO = 1
            'objIssue.Batch_No = objtr.Batch_No
            ''objIssue.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
            ''objIssue.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
            ''objIssue.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
            ''objIssue.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
            'objIssue.Is_Job_Work_Inward = False
            'objIssue.is_post = "0"
            'objIssue.ArrItem = New List(Of clsProcessProductionIssueItemDetail)
            'objIssue.ArrQC = New List(Of clsProcessProductionIssueQCDetail)

            ''objIssue.frm_loc_code = clsCommon.myCstr(txtfrmsub.Value)
            ''objIssue.to_loc_code = clsCommon.myCstr(txttosub.Value)
            ''objIssue.Rbtn_Frm_Sub = IIf(btnFrm_Sectn.IsChecked = True, 0, 1)
            ''objIssue.Rbtn_To_Sub = IIf(btnTo_Sectn.IsChecked = True, 0, 1)
            'Dim ii As Integer = 0
            'Dim dtLocType As DataTable = clsProcessProductionIssueEntry.GetLocationType(trans, True)
            'For Each objBatchTR As clsProcessBatchOrderRawDetail In objBatch.ArrRawItem
            '    Dim BalanceQty As Decimal = objBatchTR.rawqty
            '    For Each drLocType As DataRow In dtLocType.Rows
            '        Dim strItemLoc As String = ""
            '        If ShowLocationItemLocationwise = 1 Then
            '            strItemLoc = " and location_code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & objBatchTR.rawicode & "')"
            '        End If
            '        qry = " Select location_code from tspl_location_master where 2=2 and Is_Consumption_Location=0  "
            '        If clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "MAIN") = CompairStringResult.Equal Then
            '            From_SubLocation_YN = 2
            '            qry += " and location_code ='" + obj.Location_FG + "' "
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "SUB") = CompairStringResult.Equal Then
            '            From_SubLocation_YN = 1
            '            qry += " and location_code in (Select location_code from tspl_location_master where main_location_code='" + obj.Location_FG + "' and isnull(Is_Sub_Location,'N')='Y')" & strItemLoc
            '        Else
            '            From_SubLocation_YN = 0
            '            qry += " and location_code in (Select location_code from tspl_location_master where main_location_code='" + obj.Location_FG + "' and isnull(Is_Section,'N')='Y')" & strItemLoc
            '        End If
            '        Dim dtSubLocation As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '        For Each drSubLocation As DataRow In dtSubLocation.Rows
            '            Dim dtStock As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objBatchTR.rawicode, obj.Location_FG, clsCommon.myCstr(drSubLocation("location_code")), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), objtr.Batch_Date), trans, objBatchTR.rawunit, From_SubLocation_YN)
            '            If dtStock IsNot Nothing AndAlso dtStock.Rows.Count > 0 Then
            '                If clsCommon.myCdbl(dtStock.Rows(0)("qty")) > 0 Then
            '                    Dim Product_Type As String = clsItemMaster.GetItemProductType(objBatchTR.rawicode, trans)
            '                    Dim FutureBalanceQty As Decimal = 0
            '                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
            '                        FutureBalanceQty = clsInventoryMovementNew.getBalance(objBatchTR.rawicode, clsLocation.GetMainLocationMilk(clsCommon.myCstr(drSubLocation("location_code")), trans), clsCommon.myCstr(drSubLocation("location_code")), "", objtr.Batch_Date, trans, objBatchTR.rawunit)
            '                    Else
            '                        FutureBalanceQty = clsItemLocationDetails.getBalance(objBatchTR.rawicode, clsCommon.myCstr(drSubLocation("location_code")), "", objtr.Batch_Date, trans, objBatchTR.rawunit, 0)
            '                    End If

            '                    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
            '                    If FutureBalanceQty < clsCommon.myCdbl(dtStock.Rows(0)("qty")) Then
            '                        Dim decimalRatio As Decimal = FutureBalanceQty / clsCommon.myCdbl(dtStock.Rows(0)("qty"))
            '                        dtStock.Rows(0)("qty") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("qty")) * decimalRatio, DecimalPoint)
            '                        dtStock.Rows(0)("fat_kg") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("fat_kg")) * decimalRatio, DecimalPoint)
            '                        dtStock.Rows(0)("snf_kg") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("snf_kg")) * decimalRatio, DecimalPoint)
            '                    End If
            '                    If clsCommon.myCdbl(dtStock.Rows(0)("qty")) > 1 AndAlso BalanceQty > 0 Then
            '                        Dim objIssueTr As New clsProcessProductionIssueItemDetail()
            '                        ii += 1
            '                        objIssueTr.sno = ii
            '                        objIssueTr.itemcode = objBatchTR.rawicode
            '                        objIssueTr.uom_code = objBatchTR.rawunit
            '                        Dim sec_loc As String = clsProcessProductionIssueEntry.GetBOSectionLocationCode(objBatch.Batchcode, obj.Location_FG, objIssueTr.itemcode, trans)
            '                        If clsCommon.myLen(sec_loc) > 0 Then
            '                            sec_loc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where location_code in (" + sec_loc + ")", trans))
            '                        End If
            '                        objIssueTr.to_loc_code = sec_loc
            '                        objIssueTr.From_SubLocation_YN = From_SubLocation_YN
            '                        objIssueTr.frm_loc_code = clsCommon.myCstr(drSubLocation("location_code"))
            '                        If clsCommon.myLen(objIssue.frm_loc_code) <= 0 Then
            '                            If clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "MAIN") <> CompairStringResult.Equal Then
            '                                objIssue.frm_loc_code = objIssueTr.frm_loc_code
            '                                If clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "SEC") = CompairStringResult.Equal Then
            '                                    objIssue.Rbtn_Frm_Sub = 0
            '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "SUB") = CompairStringResult.Equal Then
            '                                    objIssue.Rbtn_Frm_Sub = 1
            '                                End If
            '                            End If
            '                        End If
            '                        If clsCommon.myLen(objIssue.to_loc_code) <= 0 Then
            '                            If clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "MAIN") <> CompairStringResult.Equal Then
            '                                objIssue.to_loc_code = objIssueTr.to_loc_code
            '                                If clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "SEC") = CompairStringResult.Equal Then
            '                                    objIssue.Rbtn_To_Sub = 0
            '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(drLocType("Code")), "SUB") = CompairStringResult.Equal Then
            '                                    objIssue.Rbtn_To_Sub = 1
            '                                End If
            '                            End If
            '                        End If
            '                        objIssueTr.avail_qty = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("qty")), DecimalPoint)
            '                        objIssueTr.avail_fat_kg = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("fat_kg")), DecimalPoint)
            '                        objIssueTr.avail_snf_kg = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("snf_kg")), DecimalPoint)
            '                        objIssueTr.avail_fat_pers = clsBOM.GetFatSNFPercentage_AfterConversion(objIssueTr.itemcode, objIssueTr.uom_code, objIssueTr.avail_qty, objIssueTr.avail_fat_kg, trans, settTankerDispatchAvgFATSNFPer)
            '                        objIssueTr.avail_snf_pers = clsBOM.GetFatSNFPercentage_AfterConversion(objIssueTr.itemcode, objIssueTr.uom_code, objIssueTr.avail_qty, objIssueTr.avail_snf_kg, trans, settTankerDispatchAvgFATSNFPer)
            '                        objIssueTr.req_qty = clsCommon.myCdbl(objBatchTR.rawqty)
            '                        If BalanceQty > objIssueTr.avail_qty Then
            '                            objIssueTr.issue_qty = objIssueTr.avail_qty
            '                            objIssueTr.fat_kg = objIssueTr.avail_fat_kg
            '                            objIssueTr.fat_pers = objIssueTr.avail_fat_pers
            '                            objIssueTr.snf_kg = objIssueTr.avail_snf_kg
            '                            objIssueTr.snf_pers = objIssueTr.avail_snf_pers
            '                            BalanceQty -= objIssueTr.issue_qty
            '                        Else
            '                            objIssueTr.issue_qty = BalanceQty
            '                            objIssueTr.fat_kg = clsBOM.GetFatSNFKG_AfterConversion(objIssueTr.itemcode, objIssueTr.uom_code, BalanceQty, objIssueTr.avail_fat_pers, trans)
            '                            objIssueTr.fat_pers = objIssueTr.avail_fat_pers
            '                            objIssueTr.snf_kg = clsBOM.GetFatSNFKG_AfterConversion(objIssueTr.itemcode, objIssueTr.uom_code, BalanceQty, objIssueTr.avail_snf_pers, trans)
            '                            objIssueTr.snf_pers = objIssueTr.avail_snf_pers
            '                            BalanceQty = 0
            '                        End If
            '                        'objIssueTr.remarks = clsCommon.myCstr(grow.Cells(colrem).Value).Replace("'", "`")
            '                        'objIssueTr.arrBatchItem = TryCast(grow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
            '                        'objIssueTr.arrBatchItemNew = TryCast(grow.Cells(colitemcode).Tag, List(Of clsBatchInventoryNew))
            '                        objIssue.ArrItem.Add(objIssueTr)
            '                        If BalanceQty <= 0 Then
            '                            Exit For
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        Next
            '        If BalanceQty <= 0 Then
            '            Exit For
            '        End If
            '    Next
            '    If BalanceQty > 0 Then
            '        Throw New Exception("Item [" + objBatchTR.rawicode + "].Qty [" + clsCommon.myCstr(objBatchTR.rawqty) + "] Not available")
            '    Else
            '        Continue For
            '    End If
            'Next
            'qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            'Try
            '    If check > 0 Then
            '        clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM", trans)
            '        clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)", trans)
            '    Else
            '        clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)", trans)
            '    End If
            '    Dim allicode As String = ""
            '    For Each objIssueTr As clsProcessProductionIssueItemDetail In objIssue.ArrItem
            '        allicode = allicode + "','" + objIssueTr.itemcode
            '        clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + objIssueTr.itemcode + "','" + objIssueTr.frm_loc_code + "','" + objIssueTr.to_loc_code + "'", trans)
            '    Next
            '    If clsCommon.myLen(allicode) > 0 AndAlso allicode.Substring(0, 3) = "','" Then
            '        allicode = allicode.Substring(3, allicode.Length - 3)
            '    End If

            '    qry = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc,TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then 'Alphanumeric' else case when TSPL_PARAMETER_MASTER.Nature='B' then 'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then 'Range' end end end) as Nature,sum(TSPL_ITEM_QC_PARAMETER_MASTER.actual_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Lower_range,sum(TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Upper_range,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_value) as Value1,max(TSPL_ITEM_QC_PARAMETER_MASTER.Value2) as Value2,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_status) as Status from TSPL_ITEM_QC_PARAMETER_MASTER "
            '    qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code "
            '    qry += " left outer join TEMP_LOC_QC_PARAM on TEMP_LOC_QC_PARAM.item_code=TSPL_ITEM_QC_PARAMETER_MASTER.item_code "
            '    qry += " where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in ('" + allicode + "') group by TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description,TSPL_PARAMETER_MASTER.Type,TSPL_PARAMETER_MASTER.Nature"
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        For Each dr As DataRow In dt.Rows
            '            Dim objIssueQCTr As New clsProcessProductionIssueQCDetail()
            '            objIssueQCTr.sno = CInt(dr("sno"))
            '            objIssueQCTr.frm_loc_code = clsCommon.myCstr(dr("frm_loc"))
            '            objIssueQCTr.to_loc_code = clsCommon.myCstr(dr("to_loc"))
            '            objIssueQCTr.itemcode = clsCommon.myCstr(dr("Item_Code"))
            '            objIssueQCTr.param_code = clsCommon.myCstr(dr("Code"))
            '            objIssueQCTr.lrange = clsCommon.myCdbl(dr("Lower_range"))
            '            objIssueQCTr.urange = clsCommon.myCdbl(dr("Upper_range"))
            '            objIssueQCTr.value1 = clsCommon.myCstr(dr("Value1"))
            '            objIssueQCTr.value2 = clsCommon.myCstr(dr("Value2"))
            '            objIssueQCTr.status_grid = clsCommon.myCstr(dr("Status"))
            '            If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
            '                For Each objIssueTr As clsProcessProductionIssueItemDetail In objIssue.ArrItem
            '                    If clsCommon.CompairString(objIssueQCTr.frm_loc_code, objIssueTr.frm_loc_code) = CompairStringResult.Equal Then
            '                        If clsCommon.CompairString(objIssueQCTr.to_loc_code, objIssueTr.to_loc_code) = CompairStringResult.Equal Then
            '                            If clsCommon.CompairString(objIssueQCTr.itemcode, objIssueTr.itemcode) = CompairStringResult.Equal Then
            '                                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal Then
            '                                    objIssueQCTr.QCRange = objIssueTr.fat_pers
            '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
            '                                    objIssueQCTr.QCRange = objIssueTr.snf_pers
            '                                End If
            '                            End If
            '                        End If
            '                    End If
            '                Next
            '            End If
            '            If objIssueQCTr.status_grid = "None" Then
            '                objIssueQCTr.status_grid = ""
            '            End If
            '            If objIssueQCTr.QCStatus = "None" Then
            '                objIssueQCTr.QCStatus = ""
            '            End If
            '            objIssueQCTr.remarks = ""
            '            If clsCommon.myLen(objIssueQCTr.param_code) > 0 Then
            '                objIssue.ArrQC.Add(objIssueQCTr)
            '            End If
            '        Next
            '    Else
            '        'Throw New Exception("Mapped first QC parameter with items in Item Master screen")
            '    End If
            '    dt = Nothing
            'Catch ex As Exception
            '    qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            '    check = clsDBFuncationality.getSingleValue(qry, trans)
            '    If check > 0 Then
            '        clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM", trans)
            '    End If
            '    clsCommon.MyMessageBoxShow(ex.Message)
            'Finally
            '    qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            '    check = clsDBFuncationality.getSingleValue(qry, trans)
            '    If check > 0 Then
            '        clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM", trans)
            '    End If

            'End Try

            'clsProcessProductionIssueEntry.SaveData(True, objIssue, trans)
            'clsProcessProductionIssueEntry.PostData(clsUserMgtCode.frmProcessProductionIssueEntry, True, "", objIssue.issuecode, trans)
        Catch ex As Exception
            Throw New Exception("Error At Production Issue " + Environment.NewLine + ex.Message)
        End Try
        Return objIssue
    End Function

    Private Shared Function CreateStageProcess(ByVal objIssue As clsProcessProductionIssueEntry, ByVal objBatch As clsProcessBatchOrder, ByVal objtr As clsDairyProductionUploaderDetail, ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As clsProcessProductionStageProcess
        Dim objSP As New clsProcessProductionStageProcess
        'Try
        '    objSP.STAGE_PROCESS_DATE = objtr.Batch_Date
        '    objSP.Issue_Code = objIssue.issuecode
        '    objSP.Main_Batch_Code = objIssue.batch_code
        '    objSP.Is_Job_Work_Inward = False
        '    objSP.Loaction_Code = obj.Location_FG
        '    objSP.Posted = 0
        '    'objSP.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
        '    'objSP.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
        '    'objSP.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
        '    'objSP.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
        '    objSP.ArrBatchItem = New List(Of clsProcessProductionSPBatchItemDetail)
        '    objSP.ArrIssueItem = New List(Of clsProcessProductionSPIssueItemDetail)
        '    objSP.ArrStage = New List(Of clsProcessProductionSPDetail)
        '    objSP.ArrARItem = New List(Of clsProcessProductionSPARDetail)


        '    For Each dr As clsProcessBatchOrderMainDetail In objBatch.ArrMainItem
        '        Dim objSPBatchTR As New clsProcessProductionSPBatchItemDetail()
        '        objSPBatchTR.SNO = dr.SNO
        '        objSPBatchTR.BOM_Code = dr.bomcode
        '        objSPBatchTR.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
        '        objSPBatchTR.Item_Code = dr.icode
        '        objSPBatchTR.Item_Type = dr.itype
        '        objSPBatchTR.Product_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select coalesce(Product_Type,'') as Product_Type from TSPL_ITEM_MASTER where item_code='" & dr.icode & "'", trans))
        '        objSPBatchTR.Quantity = dr.qty
        '        objSPBatchTR.Batch_No = dr.sectioncode
        '        objSPBatchTR.Shift_Code = dr.shiftcode
        '        'objSPBatchTR.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
        '        objSPBatchTR.Unit_Code = dr.UOM
        '        objSPBatchTR.Unit_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" & dr.UOM & "' ", trans))

        '        'objSPBatchTR.NO_SAMPLE_QC = clsCommon.myCdbl(grow.Cells(colNO_SAMPLE_QC).Value)
        '        'objSPBatchTR.DAMAGE_Qty = clsCommon.myCdbl(grow.Cells(colDAMAGE_Qty).Value)
        '        objSPBatchTR.FINAL_PROD_Qty = objtr.Qty
        '        'objSPBatchTR.SP_Loaction_Code = clsCommon.myCstr(grow.Cells(colSP_Loaction_Code).Value)
        '        If clsCommon.myLen(objSPBatchTR.Item_Code) > 0 Then
        '            objSP.ArrBatchItem.Add(objSPBatchTR)
        '        End If
        '    Next
        '    Dim TotalRec As Double = 0
        '    Dim Unit_Code As String = String.Empty
        '    Dim Unit_Desc As String = String.Empty

        '    Dim ii As Integer = 0
        '    For Each obIssueTr As clsProcessProductionIssueItemDetail In objIssue.ArrItem
        '        Dim objSPIssuetr As New clsProcessProductionSPIssueItemDetail()
        '        ii += 1
        '        objSPIssuetr.SNO = ii
        '        objSPIssuetr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
        '        objSPIssuetr.Issue_Code = objIssue.issuecode

        '        objSPIssuetr.From_Loaction_Code = obIssueTr.frm_loc_code
        '        objSPIssuetr.To_Location_Code = obIssueTr.to_loc_code
        '        objSPIssuetr.Item_Code = obIssueTr.itemcode
        '        If clsCommon.CompairString(obIssueTr.product_type, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(obIssueTr.product_type, "MP") = CompairStringResult.Equal Then
        '            TotalRec = TotalRec + obIssueTr.issue_qty
        '            Unit_Code = obIssueTr.uom_code
        '            Unit_Desc = obIssueTr.uom_desc
        '        End If

        '        objSPIssuetr.Item_Type = obIssueTr.item_type
        '        objSPIssuetr.Product_Type = obIssueTr.product_type
        '        objSPIssuetr.Avail_FAT_KG = obIssueTr.fat_kg
        '        objSPIssuetr.Avail_FAT_Per = obIssueTr.fat_pers
        '        objSPIssuetr.Avail_Qty = obIssueTr.issue_qty
        '        objSPIssuetr.Avail_SNF_KG = obIssueTr.snf_kg
        '        objSPIssuetr.Avail_SNF_Per = obIssueTr.snf_pers
        '        'objSPIssuetr.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemarks).Value)
        '        'objSPIssuetr.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
        '        objSPIssuetr.Unit_Code = obIssueTr.uom_code
        '        objSPIssuetr.Unit_Desc = obIssueTr.uom_desc
        '        objSPIssuetr.Issue_Status = "Accept"
        '        objSPIssuetr.Fat_Rate = obIssueTr.Fat_Rate
        '        objSPIssuetr.SNF_Rate = obIssueTr.SNF_Rate
        '        objSPIssuetr.Fat_Amt = obIssueTr.Fat_Amt
        '        objSPIssuetr.SNF_Amt = obIssueTr.SNF_Amt
        '        objSP.ArrIssueItem.Add(objSPIssuetr)
        '    Next
        '    Dim objStageDetail As ClsSectionStageMapping = clsProcessProductionStageProcess.FillStageDetail(objBatch.Batchcode, trans)
        '    objSP.Section_Stage_Map_Code = clsProcessProductionStageProcess.FillStageDetail(objIssue.batch_code, trans).doc_code
        '    For Each objStage As clsSectionStageMappingDetail In objStageDetail.Arr
        '        If clsCommon.CompairString(objStage.Stage_Type, "SP") = CompairStringResult.Equal Then
        '            Dim objSPTR As New clsProcessProductionSPDetail()
        '            objSPTR.SNO = objStage.sequnceno
        '            objSPTR.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
        '            objSPTR.Stage_Code = objStage.stagecode
        '            objSPTR.Unit_Code = Unit_Code
        '            objSPTR.Log_Sheet_No = objStage.logsheetno
        '            objSPTR.Status = "2"
        '            objSPTR.Received_Qty = TotalRec
        '            objSPTR.Batch_No = objStageDetail.Batch_No
        '            objSPTR.Structure_Code = objStageDetail.Cate_Code

        '            'objSPTR.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
        '            objSPTR.Batch_Code = objBatch.Batchcode
        '            'objSPTR.SPQCList = grow.Tag
        '            If clsCommon.myLen(objSPTR.Stage_Code) > 0 Then
        '                objSP.ArrStage.Add(objSPTR)
        '            End If
        '        End If
        '    Next
        '    clsProcessProductionStageProcess.SaveData(True, objSP, trans)
        '    clsProcessProductionStageProcess.PostData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, "", trans)
        'Catch ex As Exception
        '    Throw New Exception("Error At Stage Process" + Environment.NewLine + ex.Message)
        'End Try
        Return objSP
    End Function

    Private Shared Function CreateProductionEntry(ByVal objSP As clsProcessProductionStageProcess, ByVal objIssue As clsProcessProductionIssueEntry, ByVal objBatch As clsProcessBatchOrder, ByVal objtr As clsDairyProductionUploaderDetail, ByVal obj As clsDairyProductionUploader, ByVal trans As SqlTransaction) As clsProductionEntry
        Dim objPE As New clsProductionEntry
        Try
            'objPE.DESCRIPTION = obj.Description + " For " + objtr.Item_Code + "[" + objtr.Item_Name + "]"
            'objPE.PROD_DATE = objtr.Batch_Date
            'objPE.Batch_Code = objBatch.Batchcode
            'objPE.BATCH_DATE = objBatch.Batchdate
            ''objPE.RECEIVED_BY = clsCommon.myCstr(Me.txtReceivedBy.Value)
            'objPE.LOCATION_CODE = obj.Location_FG
            'objPE.COMMENTS = obj.Description + " For " + objtr.Item_Code + "[" + objtr.Item_Name + "]"
            ''If gvStage.Tag Is Nothing Then
            ''    objPE.Section_Stage_Map_Code = ""
            ''Else
            ''    objPE.Section_Stage_Map_Code = gvStage.Tag
            ''End If
            'objPE.CONSM_LOCATION_CODE = clsProductionEntry.GetBatchConsumptionSection(obj.Location_FG, objBatch.Batchcode, trans)
            'If clsCommon.myLen(objPE.CONSM_LOCATION_CODE) <= 0 Then
            '    Throw New Exception("Consumption Location not found for batch " & objBatch.Batchcode & "")
            'Else
            '    objPE.CONSM_SECTION_CODE = clsLocation.GetSectionCode(objPE.CONSM_LOCATION_CODE, trans)
            'End If


            ''objPE.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
            ''objPE.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
            ''objPE.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
            ''objPE.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
            ''''----------------
            'Dim objPETr As clsProductionEntryDetail
            ''objList = New List(Of clsProductionEntryDetail)
            'objPE.Is_Job_Work_Inward = False
            'objPE.ArrBatchItem = New List(Of clsProductionEntryDetail)
            'objPE.ArrIssueItem = New List(Of clsProcessProductionPEIssueItemDetail)
            'objPE.ArrQC = New List(Of clsProcessProductionPEQCDetail)
            'objPE.ArrStage = New List(Of clsProcessProductionPEStageDetail)
            'objPE.ArrWF = New List(Of clsPPPEWFItemDetail)
            'objPE.ArrScrap = New List(Of clsPPScrapItemDetail)
            'For Each objBatchTR As clsProcessBatchOrderMainDetail In objBatch.ArrMainItem
            '    objPETr = New clsProductionEntryDetail()
            '    objPETr.Shift_Code = objBatchTR.shiftcode
            '    objPETr.Batch_No = objBatchTR.sectioncode
            '    objPETr.BOM_CODE = objBatchTR.bomcode

            '    objPETr.ITEM_CODE = objBatchTR.icode
            '    objPETr.ITEM_DESCRIPTION = objBatchTR.iname
            '    objPETr.BATCH_QTY = objBatchTR.qty
            '    objPETr.RECEIPT_QTY = objBatchTR.qty
            '    'objPETr.REJ_HEAD = clsCommon.myCstr(grow.Cells(colRejHead).Value)
            '    'objPETr.REJ_QTY = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
            '    'objPETr.BREAKAGE_HEAD = clsCommon.myCstr(grow.Cells(colBreakageHead).Value)
            '    'objPETr.BREAKAGE_QTY = clsCommon.myCdbl(grow.Cells(colBreakageQty).Value)
            '    objPETr.UNIT_CODE = objBatchTR.UOM
            '    'objPETr.LAB_TESTING = clsCommon.myCstr(grow.Cells(colLabTesting).Value)
            '    objPETr.FINAL_PRODUCTION_QTY = objBatchTR.qty
            '    'objPETr.FAT_Per = clsCommon.myCdbl(grow.Cells(colFAT_Per).Value)
            '    objPETr.FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(objPETr.ITEM_CODE, objPETr.UNIT_CODE, objPETr.FINAL_PRODUCTION_QTY, objPETr.FAT_Per, trans)
            '    'objPETr.SNF_Per = clsCommon.myCdbl(grow.Cells(colSNF_Per).Value)
            '    objPETr.SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(objPETr.ITEM_CODE, objPETr.UNIT_CODE, objPETr.FINAL_PRODUCTION_QTY, objPETr.SNF_Per, trans)
            '    objPETr.LOCATION_CODE = obj.Location_FG
            '    objPE.ArrBatchItem.Add(objPETr)
            'Next


            'Dim TotalRec As Double = 0
            'Dim Unit_Code As String = ""
            'Dim Unit_Desc As String = ""
            'Dim dt As DataTable = clsProductionEntry.GetIssueAgainstBatch(objBatch.Batchcode, "", trans)
            'Dim ii As Integer = 0
            'For Each dr As DataRow In dt.Rows
            '    ii += 1
            '    Dim objPEIssue As New clsProcessProductionPEIssueItemDetail()
            '    objPEIssue.SNO = ii
            '    objPEIssue.Issue_Code = objIssue.issuecode
            '    objPEIssue.Comp_Code = objCommonVar.CurrentCompanyCode
            '    objPEIssue.Item_Code = dr.Item("Item_Code")
            '    objPEIssue.Item_Type = clsItemMaster.ItemType(dr.Item("Item_Type"))
            '    objPEIssue.Product_Type = clsItemMaster.ProductType(dr.Item("Product_Type"))
            '    objPEIssue.Avail_FAT_KG = dr.Item("Avail_FAT_KG")
            '    objPEIssue.Avail_FAT_Per = dr.Item("Avail_FAT_Pers")
            '    objPEIssue.Avail_Qty = dr.Item("Issue_Qty")
            '    objPEIssue.Avail_SNF_KG = clsCommon.myCdbl(dr.Item("Avail_SNF_KG"))
            '    objPEIssue.Avail_SNF_Per = clsCommon.myCdbl(dr.Item("Avail_SNF_Pers"))

            '    'objPEIssue.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemarks).Value)
            '    'objPEIssue.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(txtCode.Value)
            '    objPEIssue.Unit_Code = clsCommon.myCstr(dr.Item("Unit_Code"))
            '    objPEIssue.Unit_Desc = clsCommon.myCstr(dr.Item("Unit_Desc"))

            '    objPEIssue.From_Loaction_Code = clsCommon.myCstr(dr.Item("From_Loaction_Code"))
            '    objPEIssue.To_Location_Code = clsCommon.myCstr(dr.Item("To_Location_Code"))

            '    objPEIssue.Fat_Rate = clsCommon.myCdbl(dr.Item("Issued_FAT_Rate"))
            '    objPEIssue.SNF_Rate = clsCommon.myCdbl(dr.Item("Issued_SNF_Rate"))
            '    objPEIssue.Fat_Amt = clsCommon.myCdbl(dr.Item("Issued_FAT_Amt"))
            '    objPEIssue.SNF_Amt = clsCommon.myCdbl(dr.Item("Issued_SNF_Amt"))
            '    Dim strProductType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objPEIssue.Item_Code + "'", trans))
            '    If clsCommon.myLen(objPEIssue.Item_Code) > 0 Then
            '        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(strProductType, "MP") = CompairStringResult.Equal Then
            '            TotalRec = TotalRec + objPEIssue.Avail_Qty
            '            Unit_Code = objPEIssue.Unit_Code
            '            Unit_Desc = objPEIssue.Unit_Desc
            '        End If
            '        objPE.ArrIssueItem.Add(objPEIssue)
            '    End If
            'Next

            'ii = 0
            'dt = clsProcessProductionStandardization.GetQCParameters(objBatch.Batchcode, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        ii += 1
            '        Dim objPEQC As New clsProcessProductionPEQCDetail()
            '        objPEQC.sno = ii
            '        objPEQC.QC_Type = "Batch Order"
            '        objPEQC.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            '        objPEQC.param_code = clsCommon.myCstr(dr("Code"))
            '        objPEQC.Standard_Range = clsCommon.myCdbl(dr("Actual_Range"))
            '        objPEQC.Standard_Status = clsCommon.myCstr(dr("Actual_Status"))
            '        objPEQC.Standard_Value = clsCommon.myCstr(dr("Actual_Value"))
            '        If clsCommon.CompairString(clsCommon.myCstr(dr("Nature_Code")), "R") = CompairStringResult.Equal Then
            '            'objPEQC.Actual_Range = clsCommon.myCstr(grow.Cells(colActual_Range).Value)
            '            objPEQC.Actual_Range = objPEQC.Standard_Range
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Nature_Code")), "A") = CompairStringResult.Equal Then
            '            'objPEQC.Actual_Value = clsCommon.myCstr(grow.Cells(colActual_Value).Value)
            '            objPEQC.Actual_Value = objPEQC.Standard_Status
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Nature_Code")), "B") = CompairStringResult.Equal Then
            '            'objPEQC.Actual_Status = clsCommon.myCstr(grow.Cells(colActual_Status).Value)
            '            objPEQC.Actual_Status = objPEQC.Standard_Value
            '        End If
            '        objPEQC.Qc_Status = "Ok"
            '        objPEQC.remarks = ""
            '        If clsCommon.CompairString(objPE.ArrBatchItem(0).ITEM_CODE, objPEQC.Item_Code) = CompairStringResult.Equal Then
            '            If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal Then
            '                objPE.ArrBatchItem(0).FAT_Per = clsCommon.myCdbl(objPEQC.Standard_Range)
            '                objPE.ArrBatchItem(0).FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(objPE.ArrBatchItem(0).ITEM_CODE, objPE.ArrBatchItem(0).UNIT_CODE, objPE.ArrBatchItem(0).FINAL_PRODUCTION_QTY, objPE.ArrBatchItem(0).FAT_Per, trans)
            '            ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
            '                objPE.ArrBatchItem(0).SNF_Per = clsCommon.myCdbl(objPEQC.Standard_Range)
            '                objPE.ArrBatchItem(0).SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(objPE.ArrBatchItem(0).ITEM_CODE, objPE.ArrBatchItem(0).UNIT_CODE, objPE.ArrBatchItem(0).FINAL_PRODUCTION_QTY, objPE.ArrBatchItem(0).SNF_Per, trans)
            '            End If
            '        End If
            '        If clsCommon.myLen(objPEQC.param_code) > 0 Then
            '            objPE.ArrQC.Add(objPEQC)
            '        End If
            '    Next
            'End If


            'ii = 0
            'Dim objSM As ClsSectionStageMapping = clsProcessProductionStageProcess.FillStageDetail(objBatch.Batchcode, trans)
            'If objSM IsNot Nothing Then
            '    For Each objStage As clsSectionStageMappingDetail In objSM.Arr
            '        If clsCommon.CompairString(objStage.Stage_Type, "PE") = CompairStringResult.Equal Then
            '            ii += 1
            '            Dim objPEStage As New clsProcessProductionPEStageDetail()
            '            objPEStage.SNO = ii
            '            objPEStage.Comp_Code = objCommonVar.CurrentCompanyCode
            '            objPEStage.Stage_Code = objStage.stagecode
            '            objPEStage.Unit_Code = Unit_Code
            '            objPEStage.Log_Sheet_No = objStage.logsheetno
            '            objPEStage.Status = ""
            '            objPEStage.Received_Qty = TotalRec
            '            objPEStage.Remarks = ""
            '            objPEStage.Batch_No = objSM.Batch_No
            '            objPEStage.Structure_Code = objSM.Cate_Code
            '            'objPEStage.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(txtCode.Value)
            '            objPEStage.Batch_Code = objBatch.Batchcode
            '            objPEStage.SPQCList = Nothing
            '            If clsCommon.myLen(objPEStage.Stage_Code) > 0 Then
            '                objPE.ArrStage.Add(objPEStage)
            '            End If
            '        End If
            '    Next
            'End If

            'objPE.SaveData(trans, objPE, objPE.ArrBatchItem, True, "")
            'clsProductionEntry.PostData(clsUserMgtCode.frmProductionEntry, objPE.PROD_ENTRY_CODE, "", True, trans)
        Catch ex As Exception
            Throw New Exception("Error At Production Entry " + Environment.NewLine + ex.Message)
        End Try


        Return objPE
    End Function
End Class

Public Class clsDairyProductionUploaderDetail
#Region "Variables"
    Public Document_No As String
    Public PK_ID As String
    Public Batch_Date As Date
    Public Shift_Code As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public Batch_No As String
    Public QC_Status As Integer
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
                clsCommon.AddColumnsForChange(coll, "QC_Status ", objTR.QC_Status)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCTION_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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
                objTr.QC_Status = clsCommon.myCdbl(dt.Rows(0)("QC_Status"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

