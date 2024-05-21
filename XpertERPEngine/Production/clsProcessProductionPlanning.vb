'--------Monika----------BM00000003192
Imports common
Imports System.Data.SqlClient

Public Class clsProcessProductionPlanning
#Region "Variables"
    Public plancode As String = Nothing
    Public plandate As Date = Nothing
    Public plandesc As String = Nothing
    Public status As String = Nothing
    Public Is_Post As String = Nothing
    Public locationcode As String = Nothing
    Public locationname As String = Nothing
    Public Dispatch_Days As Integer = Nothing
    Public Section_Code As String = Nothing
    Public Section_Name As String = Nothing
    Public Structure_Code As String = Nothing
    Public Structure_Name As String = Nothing
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public Uploader_TR_No As String = String.Empty
    Public Arr As New List(Of clsProcessProductionPlanningDetail)
    '===========Added by preeti Gupta
    Public CheckStockServerDate As Boolean = True
    '========================================
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls
        Else
            whrCls = " " 'tspl_pp_production_plan_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        Dim qry As String = "select TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code as Code,TSPL_PP_PRODUCTION_PLAN_HEAD.plan_date as [Date],TSPL_PP_PRODUCTION_PLAN_HEAD.Description,TSPL_PP_PRODUCTION_PLAN_HEAD.Status,(case when TSPL_PP_PRODUCTION_PLAN_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status],TSPL_STRUCTURE_MASTER.Structure_Descq as [Production Category],TSPL_SECTION_MASTER.description as [Section],TSPL_PP_PRODUCTION_PLAN_HEAD.location_code as [Location Code],tspl_location_master.location_desc as [Location Description],TSPL_PP_PRODUCTION_PLAN_HEAD.Line_No as [Line No],TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name] from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PP_PRODUCTION_PLAN_HEAD.location_code "
        qry += " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.section_code=TSPL_PP_PRODUCTION_PLAN_HEAD.section_code " & _
            " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode"

        str = clsCommon.ShowSelectForm("PLNFND", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "TSPL_PP_PRODUCTION_PLAN_HEAD.plan_date")

        Return str
    End Function

    Public Shared Function UnpostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Plan_Date,Location_Code from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionPlanningDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Plan_Date")), trans)

            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("update TSPL_PP_PRODUCTION_PLAN_HEAD set Is_Post='0',status='Approved',Modified_By='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where plan_code='" + strCode + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessProductionPlanning, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessProductionPlanning, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim isSaved As Boolean = True


            If isNewEntry Then 'clsCommon.myLen(obj.plancode) <= 0
                obj.plancode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.plandate, "dd/MMM/yyyy"), clsDocType.ProductionPlanning, "", obj.locationcode)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionPlanningDairy, obj.locationcode, obj.plandate, trans)
            'Dim qry As String = "select count(*) from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + obj.plancode + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Plan_Code", obj.plancode)
            clsCommon.AddColumnsForChange(coll, "Plan_Date", clsCommon.GetPrintDate(obj.plandate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.plandesc)
            clsCommon.AddColumnsForChange(coll, "Status", obj.status)
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.Is_Post)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.locationcode)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code, True)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Days", obj.Dispatch_Days)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
            clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
            ''------------------
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
            clsCommon.AddColumnsForChange(coll, "Uploader_TR_No", obj.Uploader_TR_No, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_PLAN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.plancode, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_PLAN_HEAD", OMInsertOrUpdate.Update, " plan_code='" + obj.plancode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProcessProductionPlanningDetail.SaveDetailData(obj.plancode, obj.Arr, trans)


            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_PRODUCTION_PLAN_HEAD", "Plan_Code", "TSPL_PP_PRODUCTION_PLAN_DETAIL", "Plan_Code", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "select count(*) from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check <= 0 Then
                Throw New Exception("Plan code does not exist")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Plan_Date,Location_Code from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionPlanningDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Plan_Date")), trans)

            End If

            qry = "select Batch_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL where Plan_Code='" + strCode + "'"
            Dim strFutureDoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strFutureDoc) > 0 Then
                Throw New Exception("Batch Order No [" + strFutureDoc + "] is created.Can't Delete It")
            End If

            HistoryUpdate(strCode, trans)

            qry = "delete from TSPL_PP_PRODUCTION_PLAN_DETAIL where plan_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_PP_PRODUCTION_PLAN_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where plan_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMilkAndALLItemStockBalance(ByVal icode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction, ByVal strUOM As String) As Double
        Dim qty As Double = 0
        Dim qry As String = ""

        'If clsCommon.CompairString(clsItemMaster.GetItemProductType(icode, Nothing), "MI") = CompairStringResult.Equal Then
        '    qty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(icode, strLocation, strSubLocation, strDocumentNo, dtDocumentDate, trans, strUOM))
        'Else
        '    qty = clsCommon.myCdbl(clsItemLocationDetails.getBalance(icode, strLocation, strDocumentNo, dtDocumentDate, trans, strUOM, 0))

        qry = "select SUM(qty*RI) as Qty from ("
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty from ("
        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strSubLocation + "') and tspl_location_master.location_code='" + strLocation + "' "

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " union all "

        qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
        qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + icode + "' "
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strSubLocation + "') and tspl_location_master.location_code='" + strLocation + "' "

        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "')axx"
        qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        'End If

        Dim dblConvFac As Double = 1 'clsItemMaster.GetConvertionFactor(icode, strUOM, trans)

        Return qty / IIf(dblConvFac <= 0, 1, dblConvFac)

    End Function

    Public Shared Function GetMilkAndALLItemStockBalance_With_FATSNFKG(ByVal icode As String, ByVal strLocation As String, ByVal Sec_Sub_Loc_Code As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal _IsSub_Location As Integer, Optional ByVal Empty_Loc_Stock_Allowed As Boolean = False) As DataTable
        Return GetMilkAndALLItemStockBalance_With_FATSNFKG("", icode, strLocation, Sec_Sub_Loc_Code, dtDocumentDate, trans, strUOM, _IsSub_Location, Empty_Loc_Stock_Allowed)
    End Function
    Public Shared Function GetMilkAndALLItemStockBalance_With_FATSNFKG(ByVal ExtraWhrClauseInvNew As String, ByVal icode As String, ByVal strLocation As String, ByVal Sec_Sub_Loc_Code As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal _IsSub_Location As Integer, Optional ByVal Empty_Loc_Stock_Allowed As Boolean = False) As DataTable
        Dim qty As Double = 0
        Dim qry As String = ""
        qry = "select ICode,Location,SUM(qty*RI) as Qty,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg from ("
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty"
        'qry += " ,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg"
        'qry += " ,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg "
        qry += " ,xx.fat_kg,xx.snf_kg "
        qry += " from ( select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Stock_Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew "
        qry += ",0 as fat_kg,0 as snf_kg"
        qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
        qry += " where  TSPL_INVENTORY_MOVEMENT.Item_Code='" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'


        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " union all "

        qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Stock_Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew "
        qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
        qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
        qry += " where TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + icode + "' "
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        If clsCommon.myLen(ExtraWhrClauseInvNew) > 0 Then
            qry += " and " + ExtraWhrClauseInvNew
        End If

        qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "')axx group by axx.Icode,axx.Location "

        If Empty_Loc_Stock_Allowed Then
            qry += " union all " + Environment.NewLine + Environment.NewLine
            qry += "select '' as ICode,axx1.Location,SUM(axx1.qty * axx1.RI) as Qty,sum(axx1.fat_kg * axx1.RI) as fat_kg,sum(axx1.snf_kg * axx1.RI) as snf_kg from ("
            qry += " select xx1.ICode,xx1.Location, xx1.Qty as OldQty,xx1.fat_kg as old_fatkg,xx1.snf_kg as old_snfkg,xx1.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as snf_kg from ("
            qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
            qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
            qry += ",0 as fat_kg,0 as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
            qry += " where  TSPL_INVENTORY_MOVEMENT.Item_Code <> '" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
            qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

            intSettingType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " union all "

            qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
            qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
            qry += " where   TSPL_INVENTORY_MOVEMENT_new.Item_Code <> '" + icode + "' "
            qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If

            qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx1 left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx1.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx1.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM1 on FinalUOM1.Item_Code=xx1.ICode and FinalUOM1.UOM_Code='" + strUOM + "')axx1 group by axx1.Location having SUM(axx1.qty * axx1.RI)=0 " + Environment.NewLine + Environment.NewLine
        End If



        Dim whrcls As String = ""
        If _IsSub_Location = 0 Then 'for section
            whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + strLocation + "' and location_code='" + Sec_Sub_Loc_Code + "' and isnull(Is_Section,'N')='Y')"

        ElseIf _IsSub_Location = 1 Then 'for sub-location
            whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + strLocation + "' and location_code='" + Sec_Sub_Loc_Code + "' and isnull(Is_Sub_Location,'N')='Y')"

        ElseIf _IsSub_Location = 2 Then 'for main plant
            whrcls = " and location ='" + strLocation + "' " ' in (select location_code from tspl_location_master where location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y')
        End If

        Dim strr As String = "select SUM(finall.qty) as Qty,sum(finall.fat_kg) as fat_kg,sum(finall.snf_kg) as snf_kg from (" + qry + ")finall where 1=1 " + whrcls + ""

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strr, trans)
        'End If

        Return dt

    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "select count(*) from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check <= 0 Then
                Throw New Exception("Plan code does not exist")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Plan_Date,Location_Code from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionPlanningDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Plan_Date")), trans)

            End If
            '== Notification regarding
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionPlanningDairy + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                CreateNotificationContentEMP(strCode, trans)
            End If
            '== Complete
            HistoryUpdate(strCode, trans)
            qry = "update TSPL_PP_PRODUCTION_PLAN_HEAD set Is_Post='1',status='Approved',Modified_By='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where plan_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionPlanningDairy + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionPlanningDairy + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionPlanningDairy + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionPlanningDairy + "'", trans))
        Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(strDocumentDate)))
            objNotification.SaveData(clsUserMgtCode.frmProductionPlanningDairy, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsProcessProductionPlanning
        Dim obj As New clsProcessProductionPlanning()
        Dim objtr As New clsProcessProductionPlanningDetail()
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Try
            'Dim qry As String = "select TSPL_STRUCTURE_MASTER.Structure_Descq as Structure_Name,TSPL_SECTION_MASTER.description as Section_Name,TSPL_PP_PRODUCTION_PLAN_HEAD.Section_Code,TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code,TSPL_PP_PRODUCTION_PLAN_HEAD.Dispatch_Days,TSPL_PP_PRODUCTION_PLAN_HEAD.Description,TSPL_PP_PRODUCTION_PLAN_HEAD.is_post,TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code,TSPL_PP_PRODUCTION_PLAN_HEAD.plan_date,TSPL_PP_PRODUCTION_PLAN_HEAD.Status,TSPL_PP_PRODUCTION_PLAN_HEAD.location_code,tspl_location_master.location_desc from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PP_PRODUCTION_PLAN_HEAD.location_code"
            'qry += " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.section_code=TSPL_PP_PRODUCTION_PLAN_HEAD.section_code where 2=2 " & If(clsCommon.myLen(arrLoc) > 0, " and TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ")", "") & " "

            ''richa agarwal againt ticket no BHA/02/07/18-000120
            Dim qry As String = "select TSPL_STRUCTURE_MASTER.Structure_Descq as Structure_Name,TSPL_SECTION_MASTER.description as Section_Name,TSPL_PP_PRODUCTION_PLAN_HEAD.Section_Code,TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code,TSPL_PP_PRODUCTION_PLAN_HEAD.Dispatch_Days,TSPL_PP_PRODUCTION_PLAN_HEAD.Description,TSPL_PP_PRODUCTION_PLAN_HEAD.is_post,TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code,TSPL_PP_PRODUCTION_PLAN_HEAD.plan_date,TSPL_PP_PRODUCTION_PLAN_HEAD.Status,TSPL_PP_PRODUCTION_PLAN_HEAD.location_code,tspl_location_master.location_desc, " & _
            " TSPL_PP_PRODUCTION_PLAN_HEAD.LINE_NO,TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name], TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName " & _
            " from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PP_PRODUCTION_PLAN_HEAD.location_code " & _
            " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.section_code=TSPL_PP_PRODUCTION_PLAN_HEAD.section_code " & _
            " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode " & _
            " where 2=2 " & If(clsCommon.myLen(arrLoc) > 0, " and TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ")", "") & " "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code in (select min(plan_code) from TSPL_PP_PRODUCTION_PLAN_HEAD where TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + "))"
                Case NavigatorType.Last
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code in (select max(plan_code) from TSPL_PP_PRODUCTION_PLAN_HEAD where TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + "))"
                Case NavigatorType.Next
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code in (select min(plan_code) from TSPL_PP_PRODUCTION_PLAN_HEAD where TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ") and plan_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code in (select max(plan_code) from TSPL_PP_PRODUCTION_PLAN_HEAD where TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ") and plan_code<'" + strCode + "')"
            End Select
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj = New clsProcessProductionPlanning()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.plancode = clsCommon.myCstr(dt.Rows(0)("plan_code"))
                obj.plandate = clsCommon.myCDate(dt.Rows(0)("plan_date"))
                obj.plandesc = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                obj.status = clsCommon.myCstr(dt.Rows(0)("status"))
                obj.locationcode = clsCommon.myCstr(dt.Rows(0)("location_code"))
                obj.locationname = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
                obj.Section_Name = clsCommon.myCstr(dt.Rows(0)("Section_Name"))
                obj.Structure_Name = clsCommon.myCstr(dt.Rows(0)("Structure_Name"))
                obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_post"))
                obj.Dispatch_Days = CInt(clsCommon.myCdbl(dt.Rows(0)("Dispatch_Days")))
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
                obj.CostCenterCode = clsCommon.myCstr(dt.Rows(0)("CostCenterCode"))
                obj.CostCenterName = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Name"))
                obj.ProfitCenterCode = clsCommon.myCstr(dt.Rows(0)("ProfitCenterCode"))
                obj.ProfitCenterName = clsCommon.myCstr(dt.Rows(0)("ProfitCenterName"))
                ''--------------------------
                obj.Arr = New List(Of clsProcessProductionPlanningDetail)

                qry = "select TSPL_PP_PRODUCTION_PLAN_DETAIL.* from TSPL_PP_PRODUCTION_PLAN_DETAIL where TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_code='" + obj.plancode + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr = New clsProcessProductionPlanningDetail()
                        objtr.sno = CInt(dr("sno"))
                        objtr.icode = clsCommon.myCstr(dr("item_code"))
                        objtr.iname = clsItemMaster.GetItemName(objtr.icode, trans)
                        objtr.itype = clsItemMaster.GetItemType(objtr.icode, trans)
                        objtr.uom = clsCommon.myCstr(dr("unit_code"))
                        objtr.dlvryqty = clsCommon.myCdbl(dr("Delivery_Qty"))
                        objtr.saleqty = clsCommon.myCdbl(dr("Sale_Qty"))
                        objtr.planqty = clsCommon.myCdbl(dr("Plan_Qty"))
                        objtr.finalqty = clsCommon.myCdbl(dr("Final_Qty"))
                        objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.Avg_Sale_Qty = clsCommon.myCdbl(dr("Avg_Sale_Qty"))

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If


            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objtr = Nothing
            dt = Nothing
            dt1 = Nothing
        End Try
    End Function
    Public Shared Function GetSectionStock(ByVal Section_Loc As String, Optional ByVal Item_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim Cond1 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond1 = Cond1 & " and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' "
            End If
            Dim Cond2 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond2 = Cond2 & " and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' "
            End If
            Dim qry As String = " select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where " & Cond1 & "  group by TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.Stock_UOM " & _
                           " union all " & _
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where " & Cond2 & " group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetSectionStockHistory(ByVal Section_Loc As String, ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim qry As String = "select * from ( select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' " & _
                           " union all " & _
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' ) as SectionHist order by [Item Code],convert(date,[Doc Date],103) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        '' added by Panch raj against ticket no - KDI/21/05/18-000326 on 31-05-2018
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            '' 1. TSPL_PP_PRODUCTION_PLAN_DETAIL
            '' 2. TSPL_PP_PRODUCTION_PLAN_HEAD
            '' 3. TSPL_CUSTOM_FIELD_VALUES
            '' 4. TSPL_INVENTORY_MOVEMENT_NEW
            '' 5. TSPL_INVENTORY_MOVEMENT
            '' 6. TSPL_JOURNAL_DETAILS
            '' 7. TSPL_JOURNAL_MASTER
            '' steps for checking the items stcock and batch wise stock
            Dim obj As clsProcessProductionPlanning = clsProcessProductionPlanning.GetData(Doc_No, "", NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.plancode) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            '' check whether plan code is used in Batch Order
            'qry = "select count(*) as total from TSPL_PP_BATCH_ORDER_HEAD where Plan_Code='" & Doc_No & "'"
            'Dim count As Decimal = clsDBFuncationality.getSingleValue(qry, trans)
            'If count > 0 Then
            '    Throw New Exception("Plan Code - " & Doc_No & " is used in Batch Order. It can not be Cancelled.")
            'End If
            qry = "select batch_code from TSPL_PP_BATCH_ORDER_HEAD where Plan_Code='" & Doc_No & "'"
            Dim Str_batch_code As String = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(Str_batch_code) > 0 Then
                Throw New Exception("Plan Code is used in Batch Order - [" & Str_batch_code & "]. It can not be Cancelled.")
            End If

            '' no inventory movement on production planning
            'clsItemLocationDetails.CheckCancelInventoryBalance(obj, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_PRODUCTION_PLAN_HEAD", "Plan_Code", "TSPL_PP_PRODUCTION_PLAN_DETAIL", "Plan_Code", trans)
            '' no je creation on Prod Planning

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
            '' delete data from original table
            qry = "delete from TSPL_PP_PRODUCTION_PLAN_DETAIL where Plan_Code='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_PRODUCTION_PLAN_HEAD where Plan_Code='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsProcessProductionPlanningDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public itype As String = Nothing
    Public uom As String = Nothing
    Public dlvryqty As String = Nothing
    Public saleqty As String = Nothing
    Public planqty As String = Nothing
    Public finalqty As String = Nothing
    Public Remarks As String = Nothing
    Public Avg_Sale_Qty As Decimal = Nothing
#End Region

    Public Shared Function SaveDetailData(ByVal strCode As String, ByVal arr As List(Of clsProcessProductionPlanningDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_PRODUCTION_PLAN_DETAIL where plan_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProcessProductionPlanningDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Plan_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.uom)
                    clsCommon.AddColumnsForChange(coll, "Delivery_Qty", objtr.dlvryqty)
                    clsCommon.AddColumnsForChange(coll, "Sale_Qty", objtr.saleqty)
                    clsCommon.AddColumnsForChange(coll, "Plan_Qty", objtr.planqty)
                    clsCommon.AddColumnsForChange(coll, "Final_Qty", objtr.finalqty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Avg_Sale_Qty", objtr.Avg_Sale_Qty)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_PLAN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class