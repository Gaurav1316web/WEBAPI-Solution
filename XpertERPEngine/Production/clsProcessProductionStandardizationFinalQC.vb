Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsPPStdFinalQCHead
#Region "Variables"
    Public QC_Code As String = Nothing
    Public QC_Date As Date = Nothing
    Public Against_STD_Code As String = Nothing
    Public Status As String = Nothing
    Public Child_Batch_Code As String = Nothing
    Public Child_Batch_Desc As String = Nothing
    Public Loaction_Code As String = Nothing
    Public Loaction_Desc As String = Nothing
    Public Main_Batch_Code As String = Nothing
    Public Main_Batch_Desc As String = Nothing
    Public Section_Stage_Map_Code As String = Nothing
    Public Posted As Integer
    Public ManualBatchNo As String = String.Empty
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public CONSM_LOCATION_CODE As String
    Public CONSM_SECTION_CODE As String
    Public Tot_Batch_Qty As Decimal
    Public Tot_Batch_FATKG As Decimal
    Public Tot_Batch_SNFKG As Decimal
    Public Tot_Produce_Qty As Decimal
    Public Tot_Produce_FATKG As Decimal
    Public Tot_Produce_SNFKG As Decimal
    Public Tot_Issue_Qty As Decimal
    Public Tot_Issue_FATKG As Decimal
    Public Tot_Issue_SNFKG As Decimal
    Public Avg_Rate_FAT As Decimal
    Public Avg_Rate_SNF As Decimal
    Public Tot_Difference_Qty As Decimal
    Public Tot_Difference_FATKG As Decimal
    Public Tot_Difference_SNFKG As Decimal
    Public Tot_AddRemove_Qty As Decimal
    Public Tot_AddRemove_FATKG As Decimal
    Public Tot_AddRemove_SNFKG As Decimal
    Public Tot_Net_Qty As Decimal
    Public Tot_Net_FATKG As Decimal
    Public Tot_Net_SNFKG As Decimal
    Public JW_Estimated_FAT_KG As Decimal
    Public JW_Estimated_FAT_Amt As Decimal
    Public JW_Estimated_SNF_KG As Decimal
    Public JW_Estimated_SNF_Amt As Decimal
    Public Is_Job_Work_Inward As Boolean = False
    Public ArrBatchItem As List(Of clsPPStdFinalQCBatchItemDetail) = Nothing
    Public ArrIssueItem As List(Of clsPPStdFinalQCIssueItemDetail) = Nothing
    Public ArrARItem As List(Of clsPPStdFinalQCAddRemoveItemDetail) = Nothing
    Public ArrQC As List(Of clsPPStdFinalQCQCDetail) = Nothing
    Public ArrStageQC As List(Of clsPPStdFinalQCStageDetail) = Nothing

    Public ArrDetail As List(Of clsPPStdFinalQCDetail) = Nothing
    Public ArrParamDetail As List(Of clsPPStdFinalQCQCParam) = Nothing
    Public ArrConsumed As List(Of clsPPStdFinalQCRM) = Nothing

#End Region
    Public Shared Function GetAutoARQty(ByVal ItemCode As String, ByVal LocationCode As String, ByVal TransDate As String, ByVal DiffFatKg As String, DiffSnfKg As String, ByVal UOMCode As String) As String
        Dim qry As String = "select case when isnull(xx.[Fat Qty],0)<=isnull(xx.[SNF Qty],0) then isnull(xx.[Fat Qty],0) else isnull(xx.[SNF Qty],0) end as Qty  from(select " & _
                            " ((((300*100)/((balance.CL_FAT_KG/(balance.CL_QTY*isnull(kgunitconv.Conversion_Factor,1))/isnull(unitconv.Conversion_Factor,1))*100))*isnull(Itemunitconv.Conversion_Factor,1))/isnull(kgunitconv.Conversion_Factor,1)) as [Fat Qty]," & _
                            " ((((400*100)/((balance.CL_SNF_KG/(balance.CL_QTY*isnull(kgunitconv.Conversion_Factor,1))/isnull(unitconv.Conversion_Factor,1))*100))*isnull(Itemunitconv.Conversion_Factor,1))/isnull(kgunitconv.Conversion_Factor,1)) as [SNF Qty]" & _
                            " from TSPL_FUN_ITEM_LOC_BALANCE('FG0000081','003','10-MAY-2017') as balance" & _
                            " left outer join TSPL_ITEM_UOM_DETAIL unitconv ON unitconv.ITEM_Code=balance.item_code and unitconv.UOM_Code=balance.Stock_UOM" & _
                            " left outer join TSPL_ITEM_UOM_DETAIL kgunitconv  ON kgunitconv.ITEM_Code=balance.item_code and kgunitconv.UOM_Code='KG'" & _
                            " left outer join TSPL_ITEM_UOM_DETAIL Itemunitconv  ON Itemunitconv.ITEM_Code=balance.item_code and Itemunitconv.UOM_Code='Pouch')xx"
        Dim str As String = Nothing
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
        Return str
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_PP_STD_FINALQC_HEAD.QC_Code as Code,TSPL_PP_STD_FINALQC_HEAD.QC_Date as [Issue Date], " & _
        " TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code as [Child Batch Code],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Batch Location],CONSM_SECTION_CODE as [Section Code],[Main Item Code],[Item Description],TSPL_PP_STD_FINALQC_HEAD.Created_By as [Created By],TSPL_PP_STD_FINALQC_HEAD.Created_Date as [Created Date],TSPL_PP_STD_FINALQC_HEAD.Modified_By as [Modified By],TSPL_PP_STD_FINALQC_HEAD.Modified_Date as [Modified Date],TSPL_PP_STD_FINALQC_HEAD.Posted as [Is Posted] " & _
        " from TSPL_PP_STD_FINALQC_HEAD left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code " & _
        " left join (select * from (   select ROW_NUMBER()  over(partition by QC_Code order by Product_Type desc, TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code) as S_no ,QC_Code as STD_Main,  TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type]  from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL left join TSPL_ITEM_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  ) as M_Inner where S_no=1 ) as Main on TSPL_PP_STD_FINALQC_HEAD.QC_Code=Main.STD_Main"
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "TSPL_PP_STD_FINALQC_HEAD.QC_Date")

        Return str

    End Function
    Public Shared Function GetFinder_PendingBatchQuantity(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.batch_code as Code,TSPL_PP_BATCH_ORDER_HEAD.batch_date as [Date], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.Description,  TSPL_PP_BATCH_ORDER_HEAD.Status,(case when TSPL_PP_BATCH_ORDER_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.location_code as [Location Code],tspl_location_master.Location_Desc as [Location],  TSPL_PP_BATCH_ORDER_HEAD.structure_code as [Production Structure], " & _
            " tspl_structure_master.structure_descq as [Structure],  TSPL_PP_BATCH_ORDER_HEAD.plan_code as [Plan Code], " & _
            " (case when len(isnull(TSPL_PP_BATCH_ORDER_HEAD.main_batch_code,''))>0 then 'Child BO' else 'Main BO' end) as [BO Type], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.main_batch_code as [Main Batch Code],  TSPL_PP_BATCH_ORDER_HEAD.sub_batch_code as [Sub Batch Code],[Main Item Code],[Item Description]," & _
            " coalesce(Prod.Unit_Code,Main.Unit_Code) as Unit_Code,coalesce(prod.Quantity,Main.Quantity) as Quantity, " & _
            " prod.Produced_Qty as [Production Quantity],[Product Type] from TSPL_PP_BATCH_ORDER_HEAD  " & _
            " left outer join tspl_location_master on TSPL_PP_BATCH_ORDER_HEAD.location_code=tspl_location_master.location_code  " & _
            " left outer join tspl_structure_master on TSPL_PP_BATCH_ORDER_HEAD.structure_code=tspl_structure_master.structure_code " & _
            " left join (select * from (   select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code) as S_no, " & _
            " Batch_Code as Batch_Code_Main,  TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type], " & _
            " TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code, Quantity from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
            " left join TSPL_ITEM_MASTER on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   where TSPL_ITEM_MASTER.Product_Type='MI'  ) as M_Inner where S_no=1 ) as Main on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Main.Batch_Code_Main" & _
            " left join (select * from ( " & _
            " select Batch_Code,UNIT_CODE,Quantity,Produced_Qty from (select ROW_NUMBER()  over(partition by Child_Batch_Code order by Product_Type desc, STD_Dtl.Item_Code) as S_no,STD.Child_Batch_Code as Batch_Code, " & _
            " STD_Dtl.Item_Code,STD_Dtl.Unit_Code,sum(STD_Dtl.Quantity) as Quantity,sum(STD_Dtl.Produced_Qty) as Produced_Qty " & _
            " from TSPL_PP_STD_FINALQC_HEAD STD left join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL STD_Dtl on STD.QC_Code=STD_Dtl.QC_Code " & _
            " left join TSPL_ITEM_MASTER Item on STD_Dtl.Item_Code= Item.Item_Code  where Item.Product_Type='MI'  " & _
            " group by STD.Child_Batch_Code,STD_Dtl.Item_Code,STD_Dtl.Unit_Code,Item.Product_Type " & _
            " ) as Prod_STD where S_no=1 " & _
            " union all " & _
            " select  Batch_Code,UNIT_CODE,sum(Quantity) as Quantity,sum(Produced_Qty) as Produced_Qty from ( " & _
            " select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, PE_Dtl.Item_Code) as S_no,PE.Batch_Code, " & _
            " PE_Dtl.Item_Code,PE_Dtl.Unit_Code,sum(PE_Dtl.BATCH_QTY) as Quantity,sum(PE_Dtl.FINAL_PRODUCTION_QTY) as Produced_Qty " & _
            " from TSPL_PP_PRODUCTION_ENTRY PE left join TSPL_PP_PRODUCTION_ENTRY_DETAIL PE_Dtl on PE.PROD_ENTRY_CODE=PE_Dtl.PROD_ENTRY_CODE " & _
            " left join TSPL_ITEM_MASTER Item on PE_Dtl.Item_Code= Item.Item_Code  where Item.Product_Type='MI'  " & _
            " group by PE.Batch_Code,PE_Dtl.Item_Code,PE_Dtl.Unit_Code,Item.Product_Type" & _
            " ) as Prod_Main group by Batch_Code,UNIT_CODE " & _
            " ) as Final_Prod) as Prod " & _
            " on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Prod.Batch_Code "
        '" ) as Final_Table where Quantity>[Production Quantity]"
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("BTCHFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetKG_AfterConversion(ByVal Item_Code As String, ByVal Unit_Code As String, ByVal Qty As Decimal, ByVal trans As SqlTransaction) As Decimal
        Dim Kg_Value As Decimal = 0
        Dim Wt_uom As String = clsCommon.myCstr(clsItemMaster.GetItemWeightUnit(Item_Code, trans))
        Dim Wt_Value As Decimal = clsCommon.myCdbl(clsItemMaster.GetItemWeightValue(Item_Code, trans))
        Dim Cnvsrn_Factr As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans))
        Dim Weight_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
        Dim KG_Cnvrsn_Value As Decimal = Nothing
        Dim qry As String = ""
        If clsCommon.CompairString(Wt_uom, Weight_KG_Unit) = CompairStringResult.Equal Then
            KG_Cnvrsn_Value = 1
        Else
            ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
            Dim ItemStructureMandatoryOnWeightConversion As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, trans)) = 1, True, False))
            qry = "select top 1 CF from (Select (case when (Container_UOM='" & Wt_uom & "' and Contained_UOM='" & Weight_KG_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_KG_Unit & "' and Contained_UOM='" & Wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(Item_Code, trans) + "')  " & IIf(ItemStructureMandatoryOnWeightConversion = True, " and isnull(Structure_Code,'') =(select Structure_Code  from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(Item_Code) & "')", "") & " )aa where isnull(cast(CF as float),0)<>0 order by Product_Type desc"
            KG_Cnvrsn_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        End If

        If KG_Cnvrsn_Value > 0 Then
            Kg_Value = ((Wt_Value * Qty * Cnvsrn_Factr) * KG_Cnvrsn_Value)
        Else
            Kg_Value = 0
        End If

        Return Kg_Value
    End Function
    Public Shared Function GetMilkAndALLItemStockBalance_With_FATSNFKG(ByVal icode As String, ByVal strLocation As String, ByVal strSubLocCode As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal Empty_Stock_Loc_Allowed As Boolean) As String
        Dim qty As Double = 0
        Dim qry As String = ""

        qry = "select ICode,Location,SUM(qty*RI) as Qty,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg from ("
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ("
        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
        qry += ",0 as fat_kg,0 as snf_kg"
        qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "') " ' and tspl_location_master.location_code='" + strLocation + "'

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " union all "

        qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
        qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
        qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
        qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + icode + "' "
        qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
        End If

        qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "')axx group by axx.Icode,axx.Location "

        If Empty_Stock_Loc_Allowed Then
            qry += " union all " + Environment.NewLine + Environment.NewLine
            qry += "select '' as ICode,axx1.Location,SUM(axx1.qty * axx1.RI) as Qty,sum(axx1.fat_kg * axx1.RI) as fat_kg,sum(axx1.snf_kg * axx1.RI) as snf_kg from ("
            qry += " select xx1.ICode,xx1.Location, xx1.Qty as OldQty,xx1.fat_kg as old_fatkg,xx1.snf_kg as old_snfkg,xx1.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM1.Conversion_Factor,0)>0 then ((xx1.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM1.Conversion_Factor) else 0 end) as snf_kg from ("
            qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
            qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
            qry += ",0 as fat_kg,0 as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
            qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code <> '" + icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
            qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "') " ' and tspl_location_master.location_code='" + strLocation + "'

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " union all "

            qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then TSPL_INVENTORY_MOVEMENT_new.Qty else 0 end) as qty  ,TSPL_INVENTORY_MOVEMENT_new.UOM as UOMNew "
            qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
            qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code <> '" + icode + "' "
            qry += " and ((case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + strLocation + "')  " 'and tspl_location_master.location_code='" + strLocation + "'

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm tt") + "'"
            End If

            qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx1 left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx1.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx1.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM1 on FinalUOM1.Item_Code=xx1.ICode and FinalUOM1.UOM_Code='" + strUOM + "')axx1 group by axx1.Location having SUM(axx1.qty * axx1.RI)=0 " + Environment.NewLine + Environment.NewLine
        End If

        Dim strr As String = "select Fnl.* from (select final.icode as [Item Code],tspl_item_master.item_desc as [Item Name],TSPL_LOCATION_MASTER.main_location_code as [Main Location],final.location as [Code],TSPL_LOCATION_MASTER.location_desc as [Location Name],(case when TSPL_LOCATION_MASTER.is_section='Y' then 'Section' when TSPL_LOCATION_MASTER.is_sub_location='Y' then 'Sub Location' else 'Main Location' end) as [Type],SUM(final.qty) as [Stock Qty],sum(final.fat_kg) as [Fat Kg],sum(final.snf_kg) as [SNF Kg] from (" + qry + ")final left outer join tspl_item_master on tspl_item_master.item_code=final.icode left outer join tspl_location_master on tspl_location_master.location_code=final.location where 1=1 " & _
            " group by final.icode,tspl_item_master.item_desc,TSPL_LOCATION_MASTER.main_location_code,final.location,TSPL_LOCATION_MASTER.location_desc,TSPL_LOCATION_MASTER.is_section,TSPL_LOCATION_MASTER.is_sub_location)Fnl "

        Return strr

    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsPPStdFinalQCHead) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(isNewEntry, obj, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsPPStdFinalQCHead, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Dim coll As New Hashtable()
        Dim qry As String = "delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & obj.QC_Code & "' and Program_Code='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'"
        clsDBFuncationality.GetDataTable(qry, trans)

        If isNewEntry Then
            obj.QC_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy"), clsDocType.PPSTDFinalQC, "", obj.Loaction_Code)
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProcessProductionStandardizationFinalQC, obj.Loaction_Code, obj.QC_Date, trans)
        clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
        clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Against_STD_Code", obj.Against_STD_Code)
        clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
        clsCommon.AddColumnsForChange(coll, "Child_Batch_Code", obj.Child_Batch_Code)
        clsCommon.AddColumnsForChange(coll, "Main_Batch_Code", obj.Main_Batch_Code)
        clsCommon.AddColumnsForChange(coll, "Loaction_Code", obj.Loaction_Code)
        clsCommon.AddColumnsForChange(coll, "Section_Stage_Map_Code", obj.Section_Stage_Map_Code, True)
        clsCommon.AddColumnsForChange(coll, "CONSM_LOCATION_CODE", obj.CONSM_LOCATION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "CONSM_SECTION_CODE", obj.CONSM_SECTION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "Is_Job_Work_Inward", IIf(obj.Is_Job_Work_Inward, 1, 0))
    
        ''richa agarwal BHA/02/07/18-000121 7 july,2018 
        clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
        ''richa agarwal againt ticket no BHA/02/07/18-000120
        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
        clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
        clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
        ''------------------
        clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)

        clsCommon.AddColumnsForChange(coll, "Tot_Batch_Qty", obj.Tot_Batch_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Batch_FATKG", obj.Tot_Batch_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Batch_SNFKG", obj.Tot_Batch_SNFKG)

        clsCommon.AddColumnsForChange(coll, "Tot_Produce_Qty", obj.Tot_Produce_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Produce_FATKG", obj.Tot_Produce_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Produce_SNFKG", obj.Tot_Produce_SNFKG)

        clsCommon.AddColumnsForChange(coll, "Tot_Issue_Qty", obj.Tot_Issue_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Issue_FATKG", obj.Tot_Issue_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Issue_SNFKG", obj.Tot_Issue_SNFKG)
        clsCommon.AddColumnsForChange(coll, "Avg_Rate_FAT", obj.Avg_Rate_FAT)
        clsCommon.AddColumnsForChange(coll, "Avg_Rate_SNF", obj.Avg_Rate_SNF)

        clsCommon.AddColumnsForChange(coll, "Tot_Difference_Qty", obj.Tot_Difference_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Difference_FATKG", obj.Tot_Difference_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Difference_SNFKG", obj.Tot_Difference_SNFKG)

        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_Qty", obj.Tot_AddRemove_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_FATKG", obj.Tot_AddRemove_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_AddRemove_SNFKG", obj.Tot_AddRemove_SNFKG)

        clsCommon.AddColumnsForChange(coll, "Tot_Net_Qty", obj.Tot_Net_Qty)
        clsCommon.AddColumnsForChange(coll, "Tot_Net_FATKG", obj.Tot_Net_FATKG)
        clsCommon.AddColumnsForChange(coll, "Tot_Net_SNFKG", obj.Tot_Net_SNFKG)

        clsCommon.AddColumnsForChange(coll, "JW_Estimated_FAT_KG", obj.JW_Estimated_FAT_KG)
        clsCommon.AddColumnsForChange(coll, "JW_Estimated_FAT_Amt", obj.JW_Estimated_FAT_Amt)
        clsCommon.AddColumnsForChange(coll, "JW_Estimated_SNF_KG", obj.JW_Estimated_SNF_KG)
        clsCommon.AddColumnsForChange(coll, "JW_Estimated_SNF_Amt", obj.JW_Estimated_SNF_Amt)

        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        'clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
        'dd/MMM/yyyy hh:mm tt
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.QC_Code), "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_QC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL", "QC_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.QC_Code), "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_QC_PARAMETER", "QC_Code", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_HEAD", OMInsertOrUpdate.Update, "TSPL_PP_STD_FINALQC_HEAD.QC_Code='" + obj.QC_Code + "'", trans)
        End If

        isSaved = isSaved AndAlso clsPPStdFinalQCBatchItemDetail.SaveData(obj.QC_Code, obj.ArrBatchItem, trans)
        isSaved = isSaved AndAlso clsPPStdFinalQCIssueItemDetail.SaveData(obj.QC_Code, obj, obj.ArrIssueItem, trans)
        isSaved = isSaved AndAlso clsPPStdFinalQCAddRemoveItemDetail.SaveData(obj, trans, obj.Is_Job_Work_Inward)
        isSaved = isSaved AndAlso clsPPStdFinalQCQCDetail.SaveData(obj.QC_Code, obj.ArrQC, trans)
        isSaved = isSaved AndAlso clsPPStdFinalQCStageDetail.SaveData(obj.QC_Code, obj.ArrStageQC, trans)

        isSaved = isSaved AndAlso clsPPStdFinalQCDetail.SaveData(obj.QC_Code, obj.ArrDetail, trans)
        isSaved = isSaved AndAlso clsPPStdFinalQCQCParam.saveData(obj.QC_Code, obj.ArrParamDetail, trans)
        If clsCommon.CompairString(obj.Status, "R") = CompairStringResult.Equal Then
            qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                "values ('" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "','" & clsUserMgtCode.ProcessProductionStandardizationFinalQC & "','" & obj.QC_Code & "','" & clsCommon.GetPrintDate(obj.QC_Date, "dd-MMM-yyyy hh:mm:ss") & "','Other',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return isSaved
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal IsCheckBalance As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UnpostData(strCode, FormId, trans, IsCheckBalance)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

 
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction, ByVal IsCheckBalance As String) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select QC_Date,Loaction_Code from TSPL_PP_STD_FINALQC_HEAD where QC_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(dt.Rows(0)("Loaction_Code")), clsCommon.myCDate(dt.Rows(0)("QC_Date")), trans)

            End If
            Dim qry As String = "select count(*) from TSPL_PP_STD_FINALQC_HEAD where Posted='0' and QC_Code='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("Current document [" + strCode + "] is not posted.")
            End If
            If IsCheckBalance Then
                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
                If Not settAllowNegativeStockInDairyProduction Then
                    qry = "select Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement_new where Trans_Type='PP_STD-FQC' and InOut='I' and Source_Doc_No='" + strCode + "'" + Environment.NewLine + _
                    " union all " + Environment.NewLine + _
                    "select Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement where Trans_Type='PP_STD-FQC' and InOut='I' and Source_Doc_No='" + strCode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Item_Code"), trans)
                            Dim BalanceQty As Decimal
                            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("Location_Code")), trans), clsCommon.myCstr(dr.Item("Location_Code")), strCode, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")))
                            Else
                                BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsCommon.myCstr(dr.Item("Location_Code")), strCode, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")), 0)
                            End If
                            BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                            If clsCommon.myCdbl(dr.Item("Qty")) > BalanceQty Then
                                If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("Qty")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                                    Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("Location_Code")) & " Available Qty: " & BalanceQty & " Transaction Qty: " & clsCommon.myCdbl(dr.Item("Qty")) & " ")
                                End If
                            End If
                        Next
                    End If
                End If
            End If


            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_QC_PARAMETER", "QC_Code", trans)
           

            clsBatchInventory.ReverseAndUnpost(FormId, strCode, trans)
            clsBatchInventoryNew.ReverseAndUnpost(FormId, strCode, trans)

            qry = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strAgainstStdNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_STD_Code from TSPL_PP_STD_FINALQC_HEAD where QC_Code='" & strCode & "'", trans))

            qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Standardization_Code in ('" + strAgainstStdNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from tspl_journal_master where Source_Doc_No='" & strCode & "' and Source_Code='PS-FQ')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete  from tspl_journal_master where Source_Doc_No='" & strCode & "'  and Source_Code='PS-FQ'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_STD_FINALQC_HEAD set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.UpdateDocumentNoAndType(strCode, clsUserMgtCode.ProcessProductionStandardizationFinalQC, strAgainstStdNo, clsUserMgtCode.frmProcessProductionStandardization, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            'comment by balwinder on 28/05/2020 Never Ever use message box in class.
            'If Not clsCommon.MyMessageBoxShow("Are you sure,want to delete Standardization Entry No. " + strCode + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
            '    Return False
            'End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select QC_Date,Loaction_Code from TSPL_PP_STD_FINALQC_HEAD where QC_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(dt.Rows(0)("Loaction_Code")), clsCommon.myCDate(dt.Rows(0)("QC_Date")), trans)

            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_QC_PARAMETER", "QC_Code", trans)
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_QC_DETAIL where   QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & strCode & "' and Screen_Name='" + clsUserMgtCode.mbtnPurchaseInvoice + "'"
            clsDBFuncationality.GetDataTable(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_QC_PARAMETER where QC_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PP_STD_FINALQC_DETAIL where QC_Code='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL where  QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where  QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where  QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_PP_STD_FINALQC_QC_LOG_SHEET where  QC_Code='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_STAGE_DETAIL where  QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            'qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where QC_Code='" & strCode & "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_HEAD where  QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_STD_FINALQC_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPPStdFinalQCHead
        Try
            Dim obj As New clsPPStdFinalQCHead()
            Dim LocCond As String = " where 1=1 "
            If clsCommon.myLen(arrloc) > 0 Then
                LocCond = LocCond & " and location_code in (" + arrloc + ")"
            End If
            Dim qry As String = "select TSPL_PP_STD_FINALQC_HEAD.*,Main_Batch.Description as Main_Batch_Desc,Child_Batch.Description as Child_Batch_Desc, TSPL_PP_STD_FINALQC_HEAD.LINE_NO,TSPL_PP_STD_FINALQC_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name], TSPL_PP_STD_FINALQC_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName from TSPL_PP_STD_FINALQC_HEAD " & _
            " left join TSPL_PP_BATCH_ORDER_HEAD Main_Batch on TSPL_PP_STD_FINALQC_HEAD.Main_Batch_Code=Main_Batch.Batch_Code" & _
            " left join TSPL_PP_BATCH_ORDER_HEAD Child_Batch  on TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code=Child_Batch.Batch_Code " & _
              " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_STD_FINALQC_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_STD_FINALQC_HEAD.CostCenterCode " & _
            " where TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code in (select batch_code from tspl_pp_batch_order_head " & LocCond & ")"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PP_STD_FINALQC_HEAD. QC_Code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PP_STD_FINALQC_HEAD. QC_Code in (select min(QC_Code) from TSPL_PP_STD_FINALQC_HEAD where  Child_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Last
                    qry += " and TSPL_PP_STD_FINALQC_HEAD. QC_Code in (select max(QC_Code) from TSPL_PP_STD_FINALQC_HEAD where  Child_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Next
                    qry += " and TSPL_PP_STD_FINALQC_HEAD. QC_Code in (select min(QC_Code) from TSPL_PP_STD_FINALQC_HEAD where  QC_Code>'" + strCode + "' and Child_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Previous
                    qry += " and TSPL_PP_STD_FINALQC_HEAD. QC_Code in (select max(QC_Code) from TSPL_PP_STD_FINALQC_HEAD where  QC_Code<'" + strCode + "' and Child_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.QC_Code = clsCommon.myCstr(dt.Rows(0)("QC_Code"))
                obj.QC_Date = clsCommon.myCDate(dt.Rows(0)("QC_Date"))
                obj.Against_STD_Code = clsCommon.myCstr(dt.Rows(0)("Against_STD_Code"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
                obj.Child_Batch_Code = clsCommon.myCstr(dt.Rows(0)("Child_Batch_Code"))
                obj.Child_Batch_Desc = clsCommon.myCstr(dt.Rows(0)("Child_Batch_Desc"))
                obj.ManualBatchNo = clsCommon.myCstr(dt.Rows(0)("ManualBatchNo"))
                obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
                obj.CostCenterCode = clsCommon.myCstr(dt.Rows(0)("CostCenterCode"))
                obj.CostCenterName = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Name"))
                obj.ProfitCenterCode = clsCommon.myCstr(dt.Rows(0)("ProfitCenterCode"))
                obj.ProfitCenterName = clsCommon.myCstr(dt.Rows(0)("ProfitCenterName"))
                obj.Loaction_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_PP_BATCH_ORDER_HEAD where  Batch_Code='" + obj.Child_Batch_Code + "'", trans))
                obj.Loaction_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_LOCATION_MASTER where location_code='" + obj.Loaction_Code + "'", trans))
                obj.Main_Batch_Code = clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code"))
                obj.Main_Batch_Desc = clsCommon.myCstr(dt.Rows(0)("Main_Batch_Desc"))
                obj.Is_Job_Work_Inward = (clsCommon.myCdbl(dt.Rows(0)("Is_Job_Work_Inward")) = 1)
                obj.Section_Stage_Map_Code = clsCommon.myCstr(dt.Rows(0)("Section_Stage_Map_Code"))
                obj.CONSM_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_LOCATION_CODE"))
                obj.CONSM_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_SECTION_CODE"))



                obj.Tot_Batch_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Batch_Qty"))
                obj.Tot_Batch_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Batch_FATKG"))
                obj.Tot_Batch_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Batch_SNFKG"))

                obj.Tot_Produce_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_Qty"))
                obj.Tot_Produce_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_FATKG"))
                obj.Tot_Produce_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Produce_SNFKG"))

                obj.Tot_Issue_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_Qty"))
                obj.Tot_Issue_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_FATKG"))
                obj.Tot_Issue_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Issue_SNFKG"))
                obj.Avg_Rate_FAT = clsCommon.myCdbl(dt.Rows(0)("Avg_Rate_FAT"))
                obj.Avg_Rate_SNF = clsCommon.myCdbl(dt.Rows(0)("Avg_Rate_SNF"))

                obj.Tot_Difference_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_Qty"))
                obj.Tot_Difference_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_FATKG"))
                obj.Tot_Difference_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Difference_SNFKG"))

                obj.Tot_AddRemove_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_Qty"))
                obj.Tot_AddRemove_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_FATKG"))
                obj.Tot_AddRemove_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_AddRemove_SNFKG"))

                obj.Tot_Net_Qty = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_Qty"))
                obj.Tot_Net_FATKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_FATKG"))
                obj.Tot_Net_SNFKG = clsCommon.myCdbl(dt.Rows(0)("Tot_Net_SNFKG"))

                obj.JW_Estimated_FAT_KG = clsCommon.myCdbl(dt.Rows(0)("JW_Estimated_FAT_KG"))
                obj.JW_Estimated_FAT_Amt = clsCommon.myCdbl(dt.Rows(0)("JW_Estimated_FAT_Amt"))
                obj.JW_Estimated_SNF_KG = clsCommon.myCdbl(dt.Rows(0)("JW_Estimated_SNF_KG"))
                obj.JW_Estimated_SNF_Amt = clsCommon.myCdbl(dt.Rows(0)("JW_Estimated_SNF_Amt"))

                obj.ArrBatchItem = clsPPStdFinalQCBatchItemDetail.GetPPSTDBatchDetail(obj.QC_Code, trans)
                obj.ArrIssueItem = clsPPStdFinalQCIssueItemDetail.GetPPSTDIssuedDetail(obj.QC_Code, trans)
                obj.ArrARItem = clsPPStdFinalQCAddRemoveItemDetail.GetPPSTDARDetail(obj.QC_Code, trans)
                obj.ArrQC = clsPPStdFinalQCQCDetail.GetPPSTDQCDetail(obj.QC_Code, trans)
                obj.ArrStageQC = clsPPStdFinalQCStageDetail.GetPPSPDetail(obj.QC_Code, trans)
                obj.ArrDetail = clsPPStdFinalQCDetail.GetData(obj.QC_Code, trans)
                obj.ArrParamDetail = clsPPStdFinalQCQCParam.getData(obj.QC_Code, trans)
                obj.ArrConsumed = clsPPStdFinalQCRM.GetConsumedRM(obj.Against_STD_Code, trans)
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = PostData(Form_Id, strCode, arrLoc, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception("Production Std Final QC No [" + strCode + "]" + ex.Message)
        End Try

    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(Form_Id, strCode, arrLoc, trans, "")
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String) As Boolean
        Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(strCode, "", NavigatorType.Current, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProcessProductionStandardizationFinalQC, obj.Loaction_Code, obj.QC_Date, trans)


        If obj.Posted = 1 Then
            Throw New Exception("already posted transaction")
        End If
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_QC_PARAMETER", "QC_Code", trans)
        Dim qry As String = "update TSPL_PP_STD_FINALQC_HEAD set Posted='1',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where  QC_Code='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        PostInventoryMovementANDJE(obj, Form_Id, strCode, arrLoc, trans, VoucherNo)
        If clsCommon.myLen(VoucherNo) <= 0 Then
            '== Notification regarding
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                CreateNotificationContentEMP(strCode, trans)
            End If
            '== Complete
        End If
        Return True
    End Function
    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "'", trans))
        ' Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select QC_Date from TSPL_PP_STD_FINALQC_HEAD where QC_code='" + StrDocNo + "'", trans))

        '' work to be done agiast ticket no. BHA/11/09/18-000535  date 13/09/2018 

        Dim qry As String = "select TSPL_PP_STD_FINALQC_HEAD.QC_Code,TSPL_PP_STD_FINALQC_HEAD.QC_Date,TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code,case when TSPL_PP_STD_FINALQC_HEAD.Status='P' then 'Process' else case when TSPL_PP_STD_FINALQC_HEAD.Status='A' then 'Accept' else case when TSPL_PP_STD_FINALQC_HEAD.Status='R' then 'Reject' else TSPL_PP_STD_FINALQC_HEAD.Status end end end as Status"
        qry += ",TSPL_PP_STD_FINALQC_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc"
        qry += ",TSPL_PP_STD_FINALQC_HEAD.Main_Batch_Code,TSPL_PP_STD_FINALQC_HEAD.ManualBatchNo from TSPL_PP_STD_FINALQC_HEAD"
        qry += " left outer join TSPL_PP_STD_FINALQC_DETAIL on TSPL_PP_STD_FINALQC_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_DETAIL.Item_Code where 2=2 and TSPL_PP_STD_FINALQC_HEAD.QC_code='" + StrDocNo + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If clsCommon.myLen(strNotifiContent) > 0 Then
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objNotification As New clsNotificationHead()
                objNotification.Notification_Text = strNotifiContent
                objNotification.Notification_Caption = strNotifiCaption
                objNotification.Notification_On = strNotificationOn
                objNotification.Notification_Detail_Text = strNotifi_DetalContent
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(dt.Rows(0)("QC_Date"))))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Item_Code, clsCommon.myCstr(dt.Rows(0)("Item_Code")))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Item_Name, clsCommon.myCstr(dt.Rows(0)("Item_Desc")))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.STD_Doc_No, clsCommon.myCstr(dt.Rows(0)("Against_STD_Code")))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.ManualBatchNo, clsCommon.myCstr(dt.Rows(0)("ManualBatchNo")))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Batch_Order_No, clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code")))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Status, clsCommon.myCstr(dt.Rows(0)("Status")))
                objNotification.SaveData(clsUserMgtCode.ProcessProductionStandardizationFinalQC, objNotification, trans)
                objNotification = Nothing
                Return True
            End If
        End If
        Return False
    End Function
    Public Shared Function PostInventoryMovementANDJE(ByVal obj As clsPPStdFinalQCHead, ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String)
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredFinalQCofstandardization, clsFixedParameterCode.RequiredFinalQCofstandardization, trans)) > 0) Then
            If clsCommon.CompairString(obj.Status, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Status, "R") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Status, "R") = CompairStringResult.Equal Then
                    Dim dttemp As DataTable = clsDBFuncationality.GetDataTable("select Approve from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.ProcessProductionStandardizationFinalQC & "' and Document_No='" & obj.QC_Code & "'", trans)
                    If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then ''BHA/17/08/18-000440 by balwinder on 16/08/2018 
                        If clsCommon.myCdbl(dttemp.Rows(0)("Approve")) = 0 Then
                            Throw New Exception("Required Transaction Approval for Document No-" + obj.QC_Code)
                        End If
                    End If
                End If
                clsBatchInventory.UpdateDocumentNoAndType(obj.Against_STD_Code, clsUserMgtCode.frmProcessProductionStandardization, obj.QC_Code, clsUserMgtCode.ProcessProductionStandardizationFinalQC, trans)
                clsPPStdFinalQCRM.SaveRM(obj.Against_STD_Code, strCode, arrLoc, trans)
                SendInventoryMovementAddedRemoved(Form_Id, strCode, arrLoc, trans)
                SendInventoryMovementMilk(Form_Id, strCode, arrLoc, trans)
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans), "1") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, trans), "1") = CompairStringResult.Equal Then
                        JournalEntrySFGProduction(strCode, trans, VoucherNo)
                    Else
                        JournalEntryWIP(trans, strCode, VoucherNo)
                    End If
                End If
            ElseIf clsCommon.CompairString(obj.Status, "P") = CompairStringResult.Equal Then
                clsProcessProductionStandardization.UnpostData(obj.Against_STD_Code, clsUserMgtCode.frmProcessProductionIssueEntry, trans)
            End If
        Else
            Throw New Exception("Final QC is not for you")
        End If
        Return True
    End Function

    Public Shared Function SendInventoryMovementAddedRemoved(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        '----------inventory movement entry for added removed items from milk--------------------------------------------------
        Dim isSaved As Boolean = True
        Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(strCode, arrLoc, NavigatorType.Current, trans)
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
        If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
            For Each objtr As clsPPStdFinalQCAddRemoveItemDetail In obj.ArrARItem
                If Not settAllowNegativeStockInDairyProduction Then
                    If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        Dim CheckStockServerDate As Boolean
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
                            CheckStockServerDate = True
                        Else
                            CheckStockServerDate = False
                        End If
                        Dim loc_type As Integer = 0
                        Dim qry As String = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + objtr.Loaction_Code + "'"
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "MAIN") = CompairStringResult.Equal Then
                            loc_type = 2
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "SUB") = CompairStringResult.Equal Then
                            loc_type = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)), "SEC") = CompairStringResult.Equal Then
                            loc_type = 0
                        End If
                        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objtr.Item_Code, obj.Loaction_Code, objtr.Loaction_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.QC_Date), trans, objtr.Unit_Code, loc_type, False)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If objtr.ADD_REMOVE_QTY > clsCommon.myCdbl(dt.Rows(0)("qty")) Then
                                If Math.Abs(objtr.ADD_REMOVE_QTY - clsCommon.myCdbl(dt.Rows(0)("qty"))) > 0.01 Then
                                    Throw New Exception("Item [" + objtr.Item_Code + "] Location [" + objtr.Loaction_Code + "] Added Qty [" + clsCommon.myCstr(objtr.ADD_REMOVE_QTY) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("qty"))) + "]")
                                End If
                            End If
                        End If
                    End If
                End If
                Dim objInventoryMovemnt As New clsInventoryMovement()
                Dim objInventoryMovemntNew As New clsInventoryMovementNew()
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.InOut = "O"
                    Else
                        objInventoryMovemntNew.InOut = "I"
                    End If

                    objInventoryMovemntNew.Location_Code = objtr.Loaction_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Desc
                    objInventoryMovemntNew.Qty = objtr.ADD_REMOVE_QTY
                    objInventoryMovemntNew.UOM = objtr.Unit_Code
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    objInventoryMovemntNew.Batch_No = obj.Main_Batch_Code
                    objInventoryMovemntNew.FAT_Per = objtr.AR_FAT_Per
                    objInventoryMovemntNew.FAT_KG = objtr.AR_FAT_KG
                    objInventoryMovemntNew.SNF_KG = objtr.AR_SNF_KG
                    objInventoryMovemntNew.SNF_Per = objtr.AR_SNF_Per
                    objInventoryMovemntNew.CalculateAvgCost = False
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = obj.Is_Job_Work_Inward
                    '' UPDATE PRODUCTION COST
                    objInventoryMovemntNew.Fat_Rate = objtr.Fat_Rate
                    objInventoryMovemntNew.SNF_Rate = objtr.SNF_Rate
                    objInventoryMovemntNew.Fat_Amt = objtr.Fat_Amt
                    objInventoryMovemntNew.SNF_Amt = objtr.SNF_Amt

                    objInventoryMovemntNew.Avg_Cost = objtr.Fat_Amt + objtr.SNF_Amt
                    objInventoryMovemntNew.FIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
                    objInventoryMovemntNew.LIFO_Cost = objtr.Fat_Amt + objtr.SNF_Amt
                    If clsCommon.CompairString(objInventoryMovemntNew.InOut, "I") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.Basic_Cost = If(objtr.ADD_REMOVE_QTY <= 0, 0, (objtr.Fat_Amt + objtr.SNF_Amt) / objtr.ADD_REMOVE_QTY)
                        objInventoryMovemntNew.Net_Cost = objtr.Fat_Amt + objtr.SNF_Amt
                    End If
                    If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.ItemType = "RM"
                    ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.ItemType = "FT"
                    Else
                        objInventoryMovemntNew.ItemType = objtr.Item_Type
                    End If
                    'objInventoryMovemntNew.Basic_Cost = Nothing
                    objInventoryMovemntNew.Batch_No = obj.Child_Batch_Code ''ERO/11/07/19-000678 by balwinder on 26/07/2019
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = True
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)


                    Dim qry As String = "select TO_LOC_CODE,TO_LOC_DESC,avail_qty  from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.QC_Code='" + obj.QC_Code + "' and TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Item_Code='" + objtr.Item_Code + "'"
                    Dim dtIssueItem As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtIssueItem IsNot Nothing AndAlso dtIssueItem.Rows.Count > 0 Then
                        Dim objInvMovNewNew As New clsInventoryMovementNew()

                        If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                            objInvMovNewNew = clsInventoryMovementNew.DeepCopyObject(objInventoryMovemntNew)
                            objInvMovNewNew.InOut = "O"
                            objInvMovNewNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                            objInvMovNewNew.DonNotCalculateAvgFATSNFCost = True
                            objInvMovNewNew.CalculateAvgCost = False
                            ArrInventoryMovementNew.Add(objInvMovNewNew)

                            If objtr.ADD_REMOVE_QTY > clsCommon.myCdbl(dtIssueItem.Rows(0)("avail_qty")) Then
                                objInvMovNewNew = New clsInventoryMovementNew()
                                objInvMovNewNew = clsInventoryMovementNew.DeepCopyObject(objInventoryMovemntNew)
                                objInvMovNewNew.InOut = "I"
                                objInvMovNewNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                                objInvMovNewNew.DonNotCalculateAvgFATSNFCost = True
                                objInvMovNewNew.CalculateAvgCost = False
                                ArrInventoryMovementNew.Add(objInvMovNewNew)
                            End If
                        Else
                            objInvMovNewNew = New clsInventoryMovementNew()
                            objInvMovNewNew = clsInventoryMovementNew.DeepCopyObject(objInventoryMovemntNew)
                            objInvMovNewNew.InOut = "I"
                            objInvMovNewNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                            objInvMovNewNew.DonNotCalculateAvgFATSNFCost = True
                            objInvMovNewNew.CalculateAvgCost = False
                            ArrInventoryMovementNew.Add(objInvMovNewNew)
                        End If
                    End If

                Else
                    If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        objInventoryMovemnt.InOut = "O"
                    Else
                        objInventoryMovemnt.InOut = "I"
                    End If
                    objInventoryMovemnt.Location_Code = objtr.Loaction_Code
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Desc
                    objInventoryMovemnt.Qty = objtr.ADD_REMOVE_QTY
                    objInventoryMovemnt.UOM = objtr.Unit_Code
                    objInventoryMovemnt.MRP = Nothing
                    objInventoryMovemnt.Add_Cost = Nothing
                    objInventoryMovemnt.Batch_No = obj.Main_Batch_Code
                    objInventoryMovemnt.FAT_Per = objtr.AR_FAT_Per
                    objInventoryMovemnt.FAT_KG = objtr.AR_FAT_KG
                    objInventoryMovemnt.SNF_KG = objtr.AR_SNF_KG
                    objInventoryMovemnt.SNF_Per = objtr.AR_SNF_Per
                    objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                    objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                    objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                    objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                    objInventoryMovemnt.CalculateAvgCost = False
                    objInventoryMovemnt.Avg_Cost = objtr.Total_Amount
                    objInventoryMovemnt.FIFO_Cost = objtr.Total_Amount
                    objInventoryMovemnt.LIFO_Cost = objtr.Total_Amount
                    If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                        objInventoryMovemnt.Basic_Cost = If(objtr.ADD_REMOVE_QTY <= 0, 0, (objtr.Total_Amount) / objtr.ADD_REMOVE_QTY)
                        objInventoryMovemnt.Net_Cost = objtr.Total_Amount
                    End If
                    If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    Else
                        objInventoryMovemnt.ItemType = objtr.Item_Type
                    End If
                    objInventoryMovemnt.Batch_No = obj.Child_Batch_Code
                    objInventoryMovemnt.MFG_Date = Nothing
                    objInventoryMovemnt.Expiry_Date = Nothing
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                    If Not clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        Dim qry As String = "select TO_LOC_CODE,TO_LOC_DESC,avail_qty  from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.QC_Code='" + obj.QC_Code + "' and TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Item_Code='" + objtr.Item_Code + "'"
                        Dim dtIssueItem As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtIssueItem IsNot Nothing AndAlso dtIssueItem.Rows.Count > 0 Then
                            Dim objInvMovNew As New clsInventoryMovement()
                            If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                                objInvMovNew = clsInventoryMovement.DeepCopyObject(objInventoryMovemnt)
                                objInvMovNew.InOut = "O"
                                objInvMovNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                                ArrInventoryMovement.Add(objInvMovNew)
                                If objtr.ADD_REMOVE_QTY > clsCommon.myCdbl(dtIssueItem.Rows(0)("avail_qty")) Then
                                    objInvMovNew = New clsInventoryMovement()
                                    objInvMovNew = clsInventoryMovement.DeepCopyObject(objInventoryMovemnt)
                                    objInvMovNew.InOut = "I"
                                    objInvMovNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                                    ArrInventoryMovement.Add(objInvMovNew)
                                End If
                            Else
                                objInvMovNew = New clsInventoryMovement()
                                objInvMovNew = clsInventoryMovement.DeepCopyObject(objInventoryMovemnt)
                                objInvMovNew.InOut = "I"
                                objInvMovNew.Location_Code = clsCommon.myCstr(dtIssueItem.Rows(0)("TO_LOC_CODE"))
                                ArrInventoryMovement.Add(objInvMovNew)
                            End If
                        End If
                    End If
                End If
            Next
            '-----------other than milk product in inventory table
            If ArrInventoryMovement.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(Form_Id, obj.QC_Code, obj.QC_Date, clsCommon.GetPrintDate(obj.QC_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If ArrInventoryMovementNew.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData(Form_Id, obj.QC_Code, obj.QC_Date, clsCommon.GetPrintDate(obj.QC_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If


        End If
        Return isSaved
        '----------------------------------------------------------------------------------------

    End Function
    Public Shared Function SendInventoryMovementMilk(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsInventoryMovement
            Dim objNew As clsInventoryMovementNew
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            'Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()

            Dim strq As String = ""
            Dim objSTD As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(strCode, arrLoc, NavigatorType.Current, trans)
            Dim objListProd As List(Of clsPPStdFinalQCBatchItemDetail) = objSTD.ArrBatchItem

            If (objListProd IsNot Nothing AndAlso objListProd.Count > 0) Then
                For Each objProd As clsPPStdFinalQCBatchItemDetail In objListProd
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim strProductType As String
                    '' in produced item
                    strProductType = clsItemMaster.GetItemProductType(objProd.Item_Code, trans)
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = "Standardization"
                        objNew.InOut = "I"
                        objNew.Location_Code = objProd.STD_Loaction_Code 'objSTD.Loaction_Code
                        objNew.main_location = objSTD.Loaction_Code
                        objNew.Item_Code = objProd.Item_Code
                        objNew.Item_Desc = clsItemMaster.GetItemName(objProd.Item_Code, trans)
                        objNew.Qty = objProd.Produced_Qty
                        objNew.UOM = objProd.Unit_Code
                        objNew.Source_Doc_No = objSTD.QC_Code
                        objNew.Source_Doc_Date = objSTD.QC_Date
                        objNew.Batch_No = objSTD.Main_Batch_Code
                        objNew.FAT_Per = objProd.Produced_FAT_per
                        objNew.SNF_Per = objProd.Produced_SNF_per
                        objNew.FAT_KG = objProd.Produced_FAT_KG
                        objNew.SNF_KG = objProd.Produced_SNF_KG

                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objProd.Fat_Rate
                        objNew.SNF_Rate = objProd.SNF_Rate
                        objNew.Fat_Amt = objProd.Fat_Amt
                        objNew.SNF_Amt = objProd.SNF_Amt
                        objNew.CalculateAvgCost = False
                        objNew.DonNotCalculateAvgFATSNFCost = objSTD.Is_Job_Work_Inward
                        objNew.Avg_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        objNew.FIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        objNew.LIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
                            objNew.Basic_Cost = If(objProd.Produced_Qty <= 0, 0, (objProd.Fat_Amt + objProd.SNF_Amt) / objProd.Produced_Qty)
                            objNew.Net_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        End If

                        strItemType = clsItemMaster.GetItemType(objProd.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        objNew.ItemType = strItemTypeToSave
                        'objNew.Basic_Cost = 0
                        objNew.MRP = 0
                        objNew.Add_Cost = 0
                        objNew.MRP = 0
                        objNew.MFG_Date = objSTD.QC_Date

                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = "STD-Production"
                        obj.InOut = "I"
                        obj.Location_Code = objProd.STD_Loaction_Code 'objSTD.Loaction_Code

                        obj.Item_Code = objProd.Item_Code
                        obj.Item_Desc = clsItemMaster.GetItemName(objProd.Item_Code, trans)
                        obj.Qty = objProd.Produced_Qty
                        obj.UOM = objProd.Unit_Code
                        obj.Source_Doc_No = objSTD.QC_Code
                        obj.Source_Doc_Date = objSTD.QC_Date
                        obj.Batch_No = objSTD.Main_Batch_Code
                        obj.FAT_Per = objProd.Produced_FAT_per
                        obj.SNF_Per = objProd.Produced_SNF_per
                        obj.FAT_KG = objProd.Produced_FAT_KG
                        obj.SNF_KG = objProd.Produced_SNF_KG

                        obj.Fat_Rate = objProd.Fat_Rate
                        obj.SNF_Rate = objProd.SNF_Rate
                        obj.Fat_Amt = objProd.Fat_Amt
                        obj.SNF_Amt = objProd.SNF_Amt
                        obj.CalculateAvgCost = False
                        obj.Avg_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        obj.FIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        obj.LIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.Basic_Cost = If(objProd.Produced_Qty <= 0, 0, (objProd.Fat_Amt + objProd.SNF_Amt) / objProd.Produced_Qty)
                            obj.Net_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        End If
                        strItemType = clsItemMaster.GetItemType(objProd.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        obj.ItemType = strItemTypeToSave
                        'obj.Basic_Cost = 0
                        obj.MRP = 0
                        obj.Add_Cost = 0
                        obj.MRP = 0
                        obj.MFG_Date = objSTD.QC_Date

                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If

            '' out consumed data
            UpdateInventoryMovementConsumption(Form_Id, ArrInventoryMovement, ArrInventoryMovementNew, objSTD, Nothing, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function

    Public Shared Function UpdateInventoryMovementConsumption(ByVal Form_Id As String, ByRef ArrInventoryMovement As List(Of clsInventoryMovement), ByRef ArrInventoryMovementNew As List(Of clsInventoryMovementNew), ByVal objSTD As clsPPStdFinalQCHead, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As clsInventoryMovement
        Dim objNew As clsInventoryMovementNew
        Dim objData As List(Of clsPPStdFinalQCRM) = objSTD.ArrConsumed
        For Each dr As clsPPStdFinalQCRM In objData
            Dim strItemTypeToSave As String = ""
            Dim strItemType As String
            Dim strProductType As String
            '' out consumed item
            strProductType = clsItemMaster.GetItemProductType(dr.CONSM_ITEM_CODE, trans)
            If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                objNew = New clsInventoryMovementNew
                objNew.Trans_Type = "STD-Consumption"
                objNew.InOut = "O"
                objNew.Location_Code = dr.LOCATION_CODE 'objSTD.Loaction_Code
                objNew.main_location = objSTD.Loaction_Code
                objNew.Item_Code = dr.CONSM_ITEM_CODE
                objNew.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                objNew.Qty = dr.CONSM_QTY
                objNew.UOM = dr.UNIT_CODE
                objNew.Source_Doc_No = dr.Standardization_Code
                objNew.Source_Doc_Date = objSTD.QC_Date
                objNew.FAT_Per = dr.FAT_Per
                objNew.SNF_Per = dr.SNF_Per
                objNew.FAT_KG = dr.FAT_KG
                objNew.SNF_KG = dr.SNF_KG
                objNew.Fat_Rate = dr.Fat_Rate
                objNew.SNF_Rate = dr.SNF_Rate
                objNew.Fat_Amt = dr.Fat_Amt
                objNew.SNF_Amt = dr.SNF_Amt
                objNew.CalculateAvgCost = False
                objNew.DonNotCalculateAvgFATSNFCost = True
                objNew.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
                objNew.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                objNew.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                objNew.CalculateAvgCost = False
                strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                End If
                objNew.ItemType = strItemTypeToSave
                objNew.Basic_Cost = 0
                objNew.Add_Cost = 0
                objNew.MRP = 0
                objNew.IS_CONSUMPTION = 1
                ArrInventoryMovementNew.Add(objNew)
            Else
                obj = New clsInventoryMovement
                obj.Trans_Type = "STD-Consumption"
                obj.InOut = "O"
                obj.Location_Code = dr.LOCATION_CODE 'objSTD.Loaction_Code
                obj.Item_Code = dr.CONSM_ITEM_CODE
                obj.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                obj.Qty = dr.CONSM_QTY
                obj.UOM = dr.UNIT_CODE
                obj.Source_Doc_No = dr.Standardization_Code
                obj.Source_Doc_Date = objSTD.QC_Date
                obj.CalculateAvgCost = False
                obj.FAT_Per = dr.FAT_Per
                obj.SNF_Per = dr.SNF_Per
                obj.FAT_KG = dr.FAT_KG
                obj.SNF_KG = dr.SNF_KG
                obj.Fat_Rate = dr.Fat_Rate
                obj.SNF_Rate = dr.SNF_Rate
                obj.Fat_Amt = dr.Fat_Amt
                obj.SNF_Amt = dr.SNF_Amt
                obj.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
                obj.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                obj.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                obj.CalculateAvgCost = False
                strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = 0
                obj.Add_Cost = 0
                obj.MRP = 0
                obj.IS_CONSUMPTION = 1
                ArrInventoryMovement.Add(obj)
            End If
        Next
        If ArrInventoryMovement.Count > 0 Then
            clsInventoryMovement.SaveData(Form_Id, objSTD.QC_Code, clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        End If
        If ArrInventoryMovementNew.Count > 0 Then
            clsInventoryMovementNew.SaveData(Form_Id, objSTD.QC_Code, clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
        End If
        Return True
    End Function

    'Public Shared Function UpdateInventoryMovementConsumptionBYIssuedItem(ByVal Form_Id As String, ByRef ArrInventoryMovement As List(Of clsInventoryMovement), ByRef ArrInventoryMovementNew As List(Of clsInventoryMovementNew), ByVal objSTD As clsPPStdFinalQCHead, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim obj As clsInventoryMovement
    '    Dim objNew As clsInventoryMovementNew
    '    Dim objData As List(Of clsPPStdFinalQCIssueItemDetail) = objSTD.ArrIssueItem ''BHA/16/04/19-000858,BHA/16/04/19-000857 by balwinder on 24/04/2019
    '    For Each dr As clsPPStdFinalQCIssueItemDetail In objData
    '        Dim strItemTypeToSave As String = ""
    '        Dim strItemType As String
    '        Dim strProductType As String
    '        '' out consumed item
    '        strProductType = clsItemMaster.GetItemProductType(dr.Item_Code, trans)
    '        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
    '            objNew = New clsInventoryMovementNew
    '            objNew.Trans_Type = "STD-Consumption"
    '            objNew.InOut = "O"
    '            objNew.Location_Code = dr.TO_LOC_CODE 'objSTD.Loaction_Code
    '            objNew.main_location = objSTD.Loaction_Code
    '            objNew.Item_Code = dr.Item_Code
    '            objNew.Item_Desc = clsItemMaster.GetItemName(dr.Item_Code, trans)
    '            objNew.Qty = dr.Avail_Qty
    '            objNew.UOM = dr.Unit_Code
    '            objNew.Source_Doc_No = objSTD.Against_STD_Code
    '            objNew.Source_Doc_Date = objSTD.QC_Date
    '            objNew.Batch_No = objSTD.Main_Batch_Code
    '            objNew.FAT_Per = dr.Avail_FAT_Per
    '            objNew.SNF_Per = dr.Avail_SNF_Per
    '            objNew.FAT_KG = dr.Avail_FAT_KG
    '            objNew.SNF_KG = dr.Avail_SNF_KG

    '            '' UPDATE PRODUCTION COST
    '            objNew.Fat_Rate = dr.Fat_Rate
    '            objNew.SNF_Rate = dr.SNF_Rate
    '            objNew.Fat_Amt = dr.Fat_Amt
    '            objNew.SNF_Amt = dr.SNF_Amt
    '            objNew.CalculateAvgCost = False
    '            objNew.DonNotCalculateAvgFATSNFCost = True
    '            objNew.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            objNew.CalculateAvgCost = False
    '            strItemType = clsItemMaster.GetItemType(dr.Item_Code, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            objNew.ItemType = strItemTypeToSave
    '            objNew.Basic_Cost = 0
    '            objNew.Add_Cost = 0
    '            objNew.MRP = 0
    '            objNew.IS_CONSUMPTION = 1
    '            ArrInventoryMovementNew.Add(objNew)

    '        Else
    '            obj = New clsInventoryMovement
    '            obj.Trans_Type = "STD-Consumption"
    '            obj.InOut = "O"
    '            obj.Location_Code = dr.TO_LOC_CODE 'objSTD.Loaction_Code
    '            obj.Item_Code = dr.Item_Code
    '            obj.Item_Desc = clsItemMaster.GetItemName(dr.Item_Code, trans)
    '            obj.Qty = dr.Avail_Qty
    '            obj.UOM = dr.Unit_Code
    '            obj.Source_Doc_No = objSTD.Against_STD_Code
    '            obj.Source_Doc_Date = objSTD.QC_Date
    '            obj.CalculateAvgCost = False
    '            obj.FAT_Per = dr.Avail_FAT_Per
    '            obj.SNF_Per = dr.Avail_SNF_Per
    '            obj.FAT_KG = dr.Avail_FAT_KG
    '            obj.SNF_KG = dr.Avail_SNF_KG
    '            obj.Batch_No = objSTD.Main_Batch_Code
    '            obj.Fat_Rate = dr.Fat_Rate
    '            obj.SNF_Rate = dr.SNF_Rate
    '            obj.Fat_Amt = dr.Fat_Amt
    '            obj.SNF_Amt = dr.SNF_Amt

    '            obj.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
    '            obj.CalculateAvgCost = False
    '            strItemType = clsItemMaster.GetItemType(dr.Item_Code, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            obj.ItemType = strItemTypeToSave
    '            obj.Basic_Cost = 0
    '            obj.Add_Cost = 0
    '            obj.MRP = 0
    '            obj.IS_CONSUMPTION = 1
    '            ArrInventoryMovement.Add(obj)
    '        End If

    '    Next
    '    If ArrInventoryMovement.Count > 0 Then
    '        clsInventoryMovement.SaveData(Form_Id, objSTD.QC_Code, clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '    End If
    '    If ArrInventoryMovementNew.Count > 0 Then
    '        clsInventoryMovementNew.SaveData(Form_Id, objSTD.QC_Code, clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objSTD.QC_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
    '    End If
    '    Return True
    'End Function

    Public Shared Function GetItemQCParameter(ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = "select TSPL_ITEM_QC_PARAMETER_MASTER.code,tspl_parameter_master.Type,TSPL_ITEM_QC_PARAMETER_MASTER.lower_range, " & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.upper_range,tspl_parameter_master.description,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range, " & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status from TSPL_ITEM_QC_PARAMETER_MASTER " & _
        " left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code " & _
        " where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetIssueAgainstBatch(ByVal Batch_Code As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        'Dim qry As String = "select Issue.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,Issue.To_Location_Code " & _
        '" from (select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_PP_ISSUE_HEAD.To_Location_Code,sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Issue_Qty, " & _
        '" sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG) as Issued_FAT_KG,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 as Issued_FAT_Pers, " & _
        '" sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG) as Issued_SNF_KG,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 as Issued_SNF_Pers, " & _
        '" (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)) as Issued_FAT_Rate,(sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)) as Issued_SNF_Rate,sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt " & _
        '" from TSPL_PP_ISSUE_ITEM_DETAIL " & _
        '" inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code " & _

        Dim qry As String = " select Issue.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,Issue.To_Location_Code  from (select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_PP_ISSUE_HEAD.To_Location_Code,sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Issue_Qty,  sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG) as Issued_FAT_KG,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 end as Issued_FAT_Pers,  sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG) as Issued_SNF_KG,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty*dbo.GetConversion(Item_Code, Unit_Code)))*100 end as Issued_SNF_Pers,  case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG)) end as Issued_FAT_Rate,case when sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)=0 then 0 else (sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt)/sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG)) end as Issued_SNF_Rate,sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt  from TSPL_PP_ISSUE_ITEM_DETAIL  inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  where TSPL_PP_ISSUE_HEAD.Batch_Code='" & Batch_Code & "' and TSPL_PP_ISSUE_HEAD.Is_post=1 and (TSPL_PP_ISSUE_HEAD.QC_Code is null or TSPL_PP_ISSUE_HEAD.QC_Code='" & Doc_Code & "') group by TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,TSPL_PP_ISSUE_HEAD.To_Location_Code) as Issue " & _
        " left join TSPL_ITEM_MASTER on Issue.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        " left join TSPL_UNIT_MASTER on Issue.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetQCParameters(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        'Dim qry As String = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code, " & _
        '" TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc," & _
        '" TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else " & _
        '" case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature," & _
        '" TSPL_PARAMETER_MASTER.Nature as Nature_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range," & _
        '" TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,'' AS QC_Status " & _
        '" from TSPL_ITEM_QC_PARAMETER_MASTER " & _
        '" left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code " & _
        '" and tspl_item_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
        '" left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code " & _
        '" and tspl_parameter_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
        '" where tspl_item_qc_parameter_master.comp_code='" + objCommonVar.CurrentCompanyCode + "' " & _
        '" and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in (select distinct Item_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
        '" inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & _
        '" where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" & Batch_Code & "')"
        Dim qry As String = GetQCParametersQry(trans)
        qry = qry & " and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in (select distinct Item_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
        " inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & _
        " where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" & Batch_Code & "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetQCParametersForItem(ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = GetQCParametersQry(trans)
        qry = qry & " and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code ='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetQCParametersQry(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code, " & _
        " TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc," & _
        " TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else " & _
        " case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature," & _
        " TSPL_PARAMETER_MASTER.Nature as Nature_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range," & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,'' AS QC_Status " & _
        " from TSPL_ITEM_QC_PARAMETER_MASTER " & _
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code " & _
        " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code " & _
        " where 2=2 "
        Return qry
    End Function
    Public Shared Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as Code union all  select  Value as Code from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function
    Public Shared Function GetFinderParameterValueList(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select Parameter_CODE as Parameter_Code,Value  from TSPL_PARAMETER_VALUE_MASTER  "
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("STD", qry, "Value", whrCls, currCode, "Value", isButtonClicked)

        Return str
    End Function
    Public Shared Function FillStageDetail(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As ClsSectionStageMapping
        Dim qry As String = ""
        Dim obj As ClsSectionStageMapping = Nothing
        qry = " select distinct top 1 TSPL_SECTION_STAGE_MAPPING.Doc_Code  from TSPL_SECTION_STAGE_MAPPING inner join TSPL_SECTION_STAGE_MAPPING_HEAD on " & _
              " TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Code = TSPL_SECTION_STAGE_MAPPING.Doc_Code " & _
              " left join TSPL_STAGE_MASTER on TSPL_SECTION_STAGE_MAPPING.Stage_Code=TSPL_STAGE_MASTER.Stage_Code " & _
              " where TSPL_SECTION_STAGE_MAPPING_HEAD.Structure_Code in " & _
              " (select Structure_Code from TSPL_PP_BATCH_ORDER_HEAD where Batch_Code='" & Batch_Code & "') " & _
              " and TSPL_SECTION_STAGE_MAPPING_HEAD.Section_Code in (select Section_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
              " where Batch_Code= '" & Batch_Code & "') "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myLen(dt.Rows(0).Item("Doc_Code")) Then
                obj = ClsSectionStageMapping.GetData(dt.Rows(0).Item("Doc_Code"), NavigatorType.Current)
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetStageQCStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Not Complete"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Complete"
        dt.Rows.Add(dr)
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) > 0) Then
            dr = dt.NewRow()
            dr("Code") = "2"
            dr("Name") = "Skip"
            dt.Rows.Add(dr)
        End If


        Return dt
    End Function
    Public Shared Function ReturnQueryForOtherItemStock(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As String
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""

        If clsCommon.myLen(strSubLocation) > 0 Then
            '    strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
            strCondition1 = "  and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            '   strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' "
            strCondition1 = " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' "
        End If
        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strSubLocation + "' "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' "
        End If


        Dim qry As String = "select SUM(qty*RI) as Qty,ICode,Location from(" + Environment.NewLine
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" + Environment.NewLine
        qry += " from (" + Environment.NewLine

        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code , TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Qty   ,TSPL_INVENTORY_MOVEMENT_NEW.UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where TSPL_INVENTORY_MOVEMENT_NEW.Qty<>0 and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code<>'" + strICode + "' " + strCondition + " "
        'If dblMRP > 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT_NEW.MRP='" + clsCommon.myCstr(dblMRP) + "'"
        'End If

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_Dispatch_BulkSale.Location_Code as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom "
        qry += " from TSPL_Dispatch_Detail_BulkSale "
        qry += " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No"
        qry += " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code<>'" + strICode + "' and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' and TSPL_Dispatch_Detail_BulkSale.Qty<>0  "
        qry += " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_PP_ISSUE_ITEM_DETAIL "
        qry += " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code "
        qry += " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code<>'" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  "
        qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Locaion,TSPL_CSA_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_CSA_TRANSFER_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_CSA_TRANSFER_DETAIL "
        qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code "
        qry += " where TSPL_CSA_TRANSFER_HEAD.status=0 and TSPL_CSA_TRANSFER_DETAIL.Item_Code<>'" + strICode + "' and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + strLocation + "' and TSPL_CSA_TRANSFER_DETAIL.Qty<>0  "
        qry += " and TSPL_CSA_TRANSFER_DETAIL.doc_code not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as Locaion,TSPL_SD_SALE_INVOICE_DETAIL.Qty,-1 as RI,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL "
        qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
        qry += " where TSPL_SD_SALE_INVOICE_HEAD.status=0 and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code<>'" + strICode + "' and TSPL_SD_SALE_INVOICE_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_DETAIL.Qty<>0  "
        qry += " and TSPL_SD_SALE_INVOICE_DETAIL.document_code not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.bill_to_location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_SD_SHIPMENT_DETAIL "
        qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code "
        qry += " where TSPL_SD_SHIPMENT_HEAD.status=0 and TSPL_SD_SHIPMENT_DETAIL.Item_Code<>'" + strICode + "' and TSPL_SD_SHIPMENT_HEAD.bill_to_location='" + strLocation + "' and TSPL_SD_SHIPMENT_DETAIL.Qty<>0  "
        qry += " and TSPL_SD_SHIPMENT_DETAIL.document_code not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code end  as Locaion,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity as Qty,-1 as RI,TSPL_ITEM_MASTER.Unit_code AS Uom "
        qry += " from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
        qry += " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code<>'" + strICode + "' " + strCondition1 + " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity<>0  "
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in ('" + strDocumentNo + "')"
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in (select LoadingTanker_No FROM TSPL_Quality_Check_BulkSale LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_Code WHERE ISNULL(TSPL_Dispatch_BulkSale.QC_Code,'')<>'')"
        'qry += " and not exists (select 1 from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left outer Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No)"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_MCC_Dispatch_Challan.Item_Code as ICode,TSPL_MCC_Dispatch_Challan.MCC_Code as Locaion,TSPL_MCC_Dispatch_Challan.Net_Qty,-1 as RI,'' AS Uom "
        qry += " from TSPL_MCC_Dispatch_Challan "
        qry += " where TSPL_MCC_Dispatch_Challan.IsPosted=0 and TSPL_MCC_Dispatch_Challan.Item_Code<>'" + strICode + "' and TSPL_MCC_Dispatch_Challan.MCC_Code='" + strLocation + "' and TSPL_MCC_Dispatch_Challan.Net_Qty<>0  "
        qry += " and TSPL_MCC_Dispatch_Challan.Chalan_No not in ('" + strDocumentNo + "')"

        '' query for add/remove items durng Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.QC_Code = TSPL_PP_STD_FINALQC_HEAD.QC_Code "
        qry += " where TSPL_PP_STD_FINALQC_HEAD.Posted=0 and TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code<>'" + strICode + "' "
        qry += " and TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.QC_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' query for  Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code,"
        qry += " TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty,"
        qry += " 1 as RI,"
        qry += " TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.UNIT_CODE from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code = TSPL_PP_STD_FINALQC_HEAD.QC_Code "
        qry += " where TSPL_PP_STD_FINALQC_HEAD.Posted=0 and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code<>'" + strICode + "' "
        qry += " and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code='" & strLocation & "' "

        '' PRODUCTION CONSUMPTION 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," & _
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY,-1 as RI," & _
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " & _
              " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
              " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE<>'" & strICode & "' " & _
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "') " & _
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE='" & strLocation & "'"

        '' query for add/remove items durng Process production STAGE PROCESS
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
        qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code<>'" + strICode + "' "
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' PRODUCTION ENTRY 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE," & _
               " TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,1 as RI," & _
               " TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE from TSPL_PP_PRODUCTION_ENTRY_DETAIL " & _
               " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
               " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE<>'" & strICode & "' " & _
               " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "')" & _
               " and TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE='" & strLocation & "'"


        qry += " )xx" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "'"
        qry += " )xxx group by ICode,Location having SUM(qty*RI)>0"
        Return qry
    End Function
    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim VoucherDesc As String = ""
        Dim SourceDocDesc As String = ""
        Dim SourceDocNo As String
        Dim Comments As String
        Dim Remarks As String

        Dim i As Integer = 0
        Try
            'Dim JRNL_DATE As Date = clsCommon.GETSERVERDATE(trans)
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(Doc_Code, "", NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            VoucherDesc = "Financial Entry for Production Standardization -" & obj.QC_Code & " "
            SourceDocDesc = "Production Standardization"
            SourceDocNo = obj.QC_Code
            Comments = "Production Standardization"
            Remarks = "Production Standardization"

            '' credit wip account of consumption items
            qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount " & _
                  " FROM TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  Consm " & _
                  " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " WHERE Consm.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If

                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next

            qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account as CreditAccount,FE.Cost as Avg_Cost from ( " & _
                              " select TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code,((case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Fat_Amt  else -TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Fat_Amt end) " & _
                              " +(case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.SNF_Amt  else -TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.SNF_Amt end)) as Cost from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL " & _
                              " inner join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code " & _
                              " where TSPL_PP_STD_FINALQC_HEAD.QC_Code='" & obj.QC_Code & "' ) as FE " & _
                              " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If

                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next

            ' '' credit wip account of overhead cost
            'qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_PP_COST_WITHOUT_BATCH Cost " & _
            '      " left join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " & _
            '      " WHERE Cost.PROD_ENTRY_CODE='" & obj.QC_Code & "'"

            'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            'For Each grow As DataRow In dtGL.Rows
            '    '' check for account setting  exist or not
            '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
            '        Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
            '    End If
            '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
            '    If clsCommon.myLen(CreditAcc) > 0 Then
            '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
            '        ArryLstGLAC.Add(Acc2)
            '    End If

            '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            'Next

            '' credit wip account of production items
            qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PED.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL PED " & _
                  " left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " WHERE PED.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
                End If
                Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(DebitAcc) > 0 Then
                    Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If

                TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next

            Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.QC_Code & " "

            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PS-FQ' and Source_Doc_No='" & obj.QC_Code & "'", trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, VoucherNo, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If

            Return isSaved
        Catch ex As Exception

            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function JournalEntryWIP(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim VoucherDesc As String = ""
        Dim SourceDocDesc As String = ""
        Dim SourceDocNo As String
        Dim Comments As String
        Dim Remarks As String

        Dim i As Integer = 0
        Try
            'Dim JRNL_DATE As Date = clsCommon.GETSERVERDATE(trans)
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(Doc_Code, "", NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            VoucherDesc = "Financial Entry for Production Standardization -" & obj.QC_Code & " "
            SourceDocDesc = "Production Standardization"
            SourceDocNo = obj.QC_Code
            Comments = "Production Standardization"
            Remarks = "Production Standardization"

            ' '' credit wip account of consumption items
            'qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount " & _
            '      " FROM TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  Consm " & _
            '      " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
            '      " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
            '      " WHERE Consm.QC_Code='" & obj.QC_Code & "'"

            'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            'For Each grow As DataRow In dtGL.Rows
            '    '' check for account setting  exist or not
            '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
            '        Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
            '    End If
            '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
            '    If clsCommon.myLen(CreditAcc) > 0 Then
            '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
            '        ArryLstGLAC.Add(Acc2)
            '    End If

            '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            'Next

            qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as CreditAccount,TSPL_PURCHASE_ACCOUNTS.WIP_Account as DebitAccount,FE.Cost as Avg_Cost from ( " & _
                              " select TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code,((case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Fat_Amt  else -TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Fat_Amt end) " & _
                              " +(case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.SNF_Amt  else -TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.SNF_Amt end)) as Cost from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL " & _
                              " inner join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code " & _
                              " where TSPL_PP_STD_FINALQC_HEAD.QC_Code='" & obj.QC_Code & "' ) as FE " & _
                              " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    Throw New Exception("Inventory Control Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If

                Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(DebitAcc) > 0 Then
                    Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If
                TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next

            ' '' credit wip account of overhead cost
            'qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_PP_COST_WITHOUT_BATCH Cost " & _
            '      " left join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " & _
            '      " WHERE Cost.PROD_ENTRY_CODE='" & obj.QC_Code & "'"

            'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            'For Each grow As DataRow In dtGL.Rows
            '    '' check for account setting  exist or not
            '    If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
            '        Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
            '    End If
            '    Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
            '    If clsCommon.myLen(CreditAcc) > 0 Then
            '        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
            '        ArryLstGLAC.Add(Acc2)
            '    End If

            '    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            'Next

            ' '' credit wip account of production items
            'qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PED.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL PED " & _
            '      " left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
            '      " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
            '      " WHERE PED.QC_Code='" & obj.QC_Code & "'"

            'dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            'For Each grow As DataRow In dtGL.Rows
            '    '' check for account setting  exist or not
            '    If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
            '        Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
            '    End If
            '    Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.Loaction_Code, trans)
            '    If clsCommon.myLen(DebitAcc) > 0 Then
            '        Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
            '        ArryLstGLAC.Add(Acc2)
            '    End If

            '    TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            'Next

            Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.QC_Code & " "

            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PS-FQ' and Source_Doc_No='" & obj.QC_Code & "'", trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, VoucherNo, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If

            Return isSaved
        Catch ex As Exception

            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    'Public Shared Function JournalEntrySFGProduction(ByVal Doc_Code As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
    '    Try
    '        JournalEntrySFGProductionTry(False, Doc_Code, trans, strVourcherNoForRecreateOnly)
    '    Catch ex As Exception
    '        Try
    '            If ex.Message.Contains("Please Check Journal Entry") Then
    '                JournalEntrySFGProductionTry(True, Doc_Code, trans, strVourcherNoForRecreateOnly)
    '            Else
    '                Throw New Exception(ex.Message)
    '            End If
    '        Catch ex1 As Exception
    '            Throw New Exception(ex1.Message)
    '        End Try
    '    End Try
    '    Return True
    'End Function

    Public Shared Function JournalEntrySFGProduction(ByVal Doc_Code As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(Doc_Code, "", NavigatorType.Current, trans)
            Dim VoucherNo As String = ""
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PS-FQ' and Source_Doc_No='" & obj.QC_Code & "'", trans))
            End If
            If obj.Is_Job_Work_Inward Then
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
                End If
                Return True  ''Journal Entry will not create is job work type.
            End If

            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim VoucherDesc As String = "Financial Entry for Production Standardization -" & obj.QC_Code & " "
            Dim SourceDocDesc As String = "Production Standardization in Bulk"
            Dim SourceDocNo As String = obj.QC_Code
            Dim Comments As String = VoucherDesc
            Dim Remarks As String = VoucherDesc
            Dim dblTotalLossAmt As Decimal = 0
            Dim i As Integer = 0
            '' credit wip account of consumption items
            qry = " SELECT  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount,TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Process_Loss_Per " & _
                  " FROM TSPL_PP_STD_FINALQC_HEAD" + Environment.NewLine + _
                  " left outer join TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL  on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code " & _
                  " left join TSPL_ITEM_MASTER on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " WHERE TSPL_PP_STD_FINALQC_HEAD.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If
                If clsCommon.myCdbl(grow.Item("Process_Loss_Per")) > 0 Then
                    If clsCommon.myLen(grow.Item("Loss_Ac")) <= 0 Then
                        Throw New Exception("Loss not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                    End If
                    CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("Loss_Ac")), obj.Loaction_Code, trans)
                    Dim amt As Decimal = Math.Round(clsCommon.myCdbl(grow("Avg_Cost")) * clsCommon.myCdbl(grow.Item("Process_Loss_Per")) / 100, 2, MidpointRounding.AwayFromZero)
                    Dim Acc2() As String = {CreditAcc, amt}
                    ArryLstGLAC.Add(Acc2)
                    dblTotalLossAmt += amt
                End If
            Next

            '' CREDIT OTHER COST(ADD REMOVE)
            qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as CreditAccount,FE.Cost as Avg_Cost,FE.IsConsumeItem,TSPL_PURCHASE_ACCOUNTS.WIP_Account from ( " & _
                  " select TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code, " & _
                  "  TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Total_Amount*(case when TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else -1 end) as Cost " + _
",(select (TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Fat_Amt+TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.SNF_Amt) from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code and TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Item_Code=TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Item_Code  ) as IsConsumeItem " + Environment.NewLine + _
"from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL  " & _
                  " inner join   TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_HEAD.QC_Code=TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.QC_Code " & _
                  " where TSPL_PP_STD_FINALQC_HEAD.QC_Code='" & obj.QC_Code & "'  "
            qry += ") as FE  " & _
                  " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

            Dim dtAddRemove As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtAddRemove.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("Inventroy Control Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.Loaction_Code, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                        ArryLstGLAC.Add(Acc2)
                    Else
                        Dim Amt As Decimal = (-1 * clsCommon.myCdbl(grow("Avg_Cost")))
                        Dim Acc2() As String = {CreditAcc, Amt, "", "", "", "", "", "", "I"}
                        ArryLstGLAC.Add(Acc2)

                        ''BHA/27/11/18-000724 by Balwinder on 18/01/2019
                        clsInventoryMovement.UpdateInvControlAccount(Doc_Code, clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(grow("CONSM_ITEM_CODE")), IIf(Amt > 0, CreditAcc, ""), IIf(Amt > 0, "", CreditAcc), "", "is_consumption=0", trans)
                    End If
                End If



                If clsCommon.myCdbl(grow("IsConsumeItem")) > 0 Then
                    Dim flag As Boolean = True
                    If clsCommon.myCdbl(grow("Avg_Cost")) < 0 Then ''For Removed Item Only
                        ''By Balwinder on 11/12/2019.check condition if issue item cost is less then removed item cost then Do not add account in JE.
                        If Math.Abs(clsCommon.myCdbl(grow("IsConsumeItem"))) < Math.Abs(clsCommon.myCdbl(grow("Avg_Cost"))) Then
                            flag = False
                        End If
                    End If
                    If flag Then
                        If clsCommon.myLen(grow.Item("WIP_Account")) <= 0 Then
                            Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                        End If
                        CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("WIP_Account")), obj.Loaction_Code, trans)
                        Dim Acc2() As String = {CreditAcc, clsCommon.myCdbl(grow("Avg_Cost"))}
                        ArryLstGLAC.Add(Acc2)
                    End If
                End If
            Next

            '' credit wip account of production items TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount
            qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount,PED.Avg_Cost " &
                  " from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL PED     left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                  " WHERE PED.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            If dtGL IsNot Nothing AndAlso dtGL.Rows.Count > 0 Then
                For ii As Integer = 0 To dtGL.Rows.Count - 1
                    '' check for account setting  exist or not
                    If clsCommon.myLen(dtGL.Rows(ii).Item("DebitAccount")) <= 0 Then
                        Throw New Exception("Inventory Control account not found for Item Code " & dtGL.Rows(ii).Item("ITEM_CODE") & "")
                    End If
                    If clsCommon.myLen(dtGL.Rows(ii).Item("Loss_Ac")) <= 0 Then
                        Throw New Exception("Gain/Loss account not found for Item Code " & dtGL.Rows(ii).Item("ITEM_CODE") & "")
                    End If
                    Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtGL.Rows(ii).Item("DebitAccount")), obj.Loaction_Code, trans)
                    Dim GainLossAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtGL.Rows(ii).Item("Loss_Ac")), obj.Loaction_Code, trans)
                    If clsCommon.myLen(DebitAcc) > 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(dtGL.Rows(ii)("Avg_Cost"))}
                            ArryLstGLAC.Add(Acc2)

                            If ii = 0 AndAlso clsCommon.myCdbl(dblTotalLossAmt) > 0 Then
                                Dim Acc3() As String = {DebitAcc, -1 * dblTotalLossAmt}
                                ArryLstGLAC.Add(Acc3)
                            End If
                        Else
                            Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(dtGL.Rows(ii)("Avg_Cost")), "", "", "", "", "", "", "I"}
                            ArryLstGLAC.Add(Acc2)
                            If ii = 0 AndAlso clsCommon.myCdbl(dblTotalLossAmt) > 0 Then
                                Dim Acc3() As String = {GainLossAcc, -1 * dblTotalLossAmt, "", "", "", "", "", "", "I"}
                                ArryLstGLAC.Add(Acc3)
                            End If

                            ''BHA/27/11/18-000724 by Balwinder on 18/01/2019
                            clsInventoryMovement.UpdateInvControlAccount(Doc_Code, clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(dtGL.Rows(ii)("ITEM_CODE")), DebitAcc, "", "", trans)
                            ''------------------
                        End If
                    End If
                Next

            End If





            Dim GLDesc As String = "Journal Entry Against Production Standardization Final QC - Doc No." & obj.QC_Code & " "
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, VoucherNo, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, trans, obj.QC_Date, GLDesc, "PS-FQ", "Production Standardization", obj.QC_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateConsumption(ByVal Doc_Code As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            UpdateConsumption(Doc_Code, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UpdateConsumption(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(Doc_Code) <= 0 Then
            Throw New Exception("Document No can not be blank")
        End If
        Dim objRec As clsPPStdFinalQCHead
        objRec = clsPPStdFinalQCHead.GetData(Doc_Code, "", NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Throw New Exception("Document not found")
        End If
        If clsCommon.myLen(objRec.QC_Code) <= 0 Then
            Throw New Exception("Document not found")
        End If
        Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            '' query for consumption on batch order bom basis
            Dim qry As String = "delete from TSPL_INVENTORY_MOVEMENT_NEW where SOURCE_DOC_NO='" & objRec.QC_Code & "' and IS_CONSUMPTION=1"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where SOURCE_DOC_NO='" & objRec.QC_Code & "' and IS_CONSUMPTION=1"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE='" & objRec.QC_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        '' created by Panch Raj against ticket No- KDI/21/05/18-000324 on date 31-05-2018
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            ''1. TSPL_PP_STD_FINALQC_HEAD
            ''2. TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL
            ''3. TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL
            ''4. TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL
            ''5. TSPL_PP_STD_FINALQC_QC_DETAIL
            ''6. TSPL_PP_STD_FINALQC_QC_LOG_SHEET
            ''7. TSPL_PP_STD_FINALQC_STAGE_DETAIL
            ''8. TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL
            ''9. TSPL_CUSTOM_FIELD_VALUES
            ''10. TSPL_INVENTORY_MOVEMENT_NEW ( no need of history)
            ''11. TSPL_INVENTORY_MOVEMENT     ( no need of history)
            ''12. TSPL_JOURNAL_DETAILS
            ''13. TSPL_JOURNAL_MASTER
            '' steps for checking the items stock and batch wise stock
            Dim obj As clsPPStdFinalQCHead = clsPPStdFinalQCHead.GetData(Doc_No, "", NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.QC_Code) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
            If Not settAllowNegativeStockInDairyProduction Then
                qry = "select Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement_new where Trans_Type='PP_STD-FQC' and InOut='I' and Source_Doc_No='" + Doc_No + "'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    "select Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement where Trans_Type='PP_STD-FQC' and InOut='I' and Source_Doc_No='" + Doc_No + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Item_Code"), trans)
                        Dim BalanceQty As Decimal
                        If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                            BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("Location_Code")), trans), clsCommon.myCstr(dr.Item("Location_Code")), Doc_No, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")))
                        Else
                            BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsCommon.myCstr(dr.Item("Location_Code")), Doc_No, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")), 0)
                        End If
                        BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                        If clsCommon.myCdbl(dr.Item("Qty")) > BalanceQty Then
                            If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("Qty")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                                Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("Location_Code")) & " Available Qty: " & BalanceQty & " Transaction Qty: " & clsCommon.myCdbl(dr.Item("Qty")) & " ")
                            End If
                        End If
                    Next
                End If
            End If

            'clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)

            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_FINALQC_HEAD", "QC_Code", "TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL", "QC_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL", "QC_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_FINALQC_QC_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_DETAIL", "QC_Code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STD_FINALQC_STAGE_DETAIL", "QC_Code", "TSPL_PP_STD_FINALQC_QC_PARAMETER", "QC_Code", trans)

            Dim strAgainstStdNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_STD_Code from TSPL_PP_STD_FINALQC_HEAD where QC_Code='" & Doc_No & "'", trans))

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, strAgainstStdNo, "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL", "Standardization_Code", trans)

            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If


            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
            '' release issue involved in standardization process
            'qry = "update TSPL_PP_ISSUE_HEAD set QC_Code=null where QC_Code='" & Doc_No & "' "
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_QC_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_PP_STD_FINALQC_QC_LOG_SHEET where QC_Code='" & Doc_No & "' "
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_QC_PARAMETER where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_STAGE_DETAIL where QC_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Standardization_Code='" & strAgainstStdNo & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STD_FINALQC_HEAD where QC_Code='" & Doc_No & "'"
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

Public Class clsPPStdFinalQCBatchItemDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public SNO As String = Nothing
    Public BOM_Code As String = Nothing
    Public BOM_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Product_Type As String


    Public Quantity As String = Nothing
    Public Shift_Code As String = Nothing
    Public Shift_Desc As String = Nothing
    Public Section_Code As String = Nothing
    Public Section_Desc As String = Nothing

    Public Requir_FAT_per As Decimal = 0
    Public Requir_SNF_Per As Decimal = 0
    Public Requir_FAT_KG As Decimal = 0
    Public Requir_SNF_KG As Decimal = 0

    Public Produced_Qty As Decimal = 0
    Public Produced_FAT_per As Decimal = 0
    Public Produced_SNF_per As Decimal = 0
    Public Produced_FAT_KG As Decimal = 0
    Public Produced_SNF_KG As Decimal = 0
    'Public NO_SAMPLE_QC As Decimal = 0
    'Public DAMAGE_Qty As Decimal = 0
    'Public FINAL_PROD_Qty As Decimal = 0
    Public STD_Loaction_Code As String = Nothing
    Public STD_Loaction_Desc As String = Nothing

    Public FIFO_Cost As Decimal
    Public LIFO_Cost As Decimal
    Public AVG_Cost As Decimal

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0

#End Region

    Public Shared Function SaveData(ByVal QC_Code As String, ByVal arr As List(Of clsPPStdFinalQCBatchItemDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL where  QC_Code='" + QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPPStdFinalQCBatchItemDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "QC_Code", QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.BOM_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Quantity", objtr.Quantity)
                    clsCommon.AddColumnsForChange(coll, "Shift_Code", objtr.Shift_Code)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", objtr.Section_Code)

                    clsCommon.AddColumnsForChange(coll, "Requir_FAT_per", objtr.Requir_FAT_per)
                    clsCommon.AddColumnsForChange(coll, "Requir_FAT_KG", objtr.Requir_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Requir_SNF_Per", objtr.Requir_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Requir_SNF_KG", objtr.Requir_SNF_KG)

                    clsCommon.AddColumnsForChange(coll, "Produced_Qty", objtr.Produced_Qty)

                    clsCommon.AddColumnsForChange(coll, "Produced_FAT_per", objtr.Produced_FAT_per)
                    clsCommon.AddColumnsForChange(coll, "Produced_SNF_per", objtr.Produced_SNF_per)

                    clsCommon.AddColumnsForChange(coll, "Produced_FAT_KG", objtr.Produced_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Produced_SNF_KG", objtr.Produced_SNF_KG)
                    '' NEW COLUMNS
                    'clsCommon.AddColumnsForChange(coll, "NO_SAMPLE_QC", objtr.NO_SAMPLE_QC)
                    'clsCommon.AddColumnsForChange(coll, "DAMAGE_Qty", objtr.DAMAGE_Qty)
                    'clsCommon.AddColumnsForChange(coll, "FINAL_PROD_Qty", objtr.FINAL_PROD_Qty)
                    ''END NEW COLUMNS
                    clsCommon.AddColumnsForChange(coll, "STD_Loaction_Code", objtr.STD_Loaction_Code, True)


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetPPSTDBatchDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCBatchItemDetail)
        Dim objIssueList As New List(Of clsPPStdFinalQCBatchItemDetail)
        Dim qry As String = "select TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE, " & _
        " TSPL_SHIFT_MASTER.SHIFT_NAME as Shift_Desc,TSPL_SECTION_MASTER.Description as Section_Desc ,TSPL_ITEM_MASTER.Product_Type, " & _
        " TSPL_UNIT_MASTER.Unit_Desc,TSPL_PP_BOM_HEAD.DESCRIPTION as BOM_Desc,TSPL_LOCATION_MASTER.Location_Desc as STD_Loaction_Desc from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_SHIFT_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE " & _
        " left join TSPL_SECTION_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Section_Code=TSPL_SECTION_MASTER.Section_Code " & _
        " left join TSPL_PP_BOM_HEAD on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.BOM_Code=TSPL_PP_BOM_HEAD.BOM_CODE " & _
        " left join TSPL_LOCATION_MASTER on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code=TSPL_LOCATION_MASTER.Location_Code " & _
        " where TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL. QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStdFinalQCBatchItemDetail()

                objtr.SNO = CInt(dr("SNO"))
                objtr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objtr.BOM_Desc = clsCommon.myCstr(dr("BOM_Desc"))

                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Produced_FAT_KG = clsCommon.myCdbl(dr("Produced_FAT_KG"))
                objtr.Produced_Qty = clsCommon.myCdbl(dr("Produced_Qty"))

                objtr.Produced_FAT_per = clsCommon.myCdbl(dr("Produced_FAT_per"))
                objtr.Produced_SNF_per = clsCommon.myCdbl(dr("Produced_SNF_per"))

                objtr.Produced_SNF_KG = clsCommon.myCdbl(dr("Produced_SNF_KG"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Quantity = dr("Quantity")
                objtr.Requir_FAT_KG = clsCommon.myCdbl(dr("Requir_FAT_KG"))
                objtr.Requir_FAT_per = clsCommon.myCdbl(dr("Requir_FAT_per"))
                objtr.Requir_SNF_KG = clsCommon.myCdbl(dr("Requir_SNF_KG"))
                objtr.Requir_SNF_Per = clsCommon.myCdbl(dr("Requir_SNF_Per"))

                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.Section_Desc = clsCommon.myCstr(dr("Section_Desc"))
                objtr.Shift_Code = clsCommon.myCstr(dr("Shift_Code"))
                objtr.Shift_Desc = clsCommon.myCstr(dr("Shift_Desc"))
                objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))
                objtr.STD_Loaction_Code = clsCommon.myCstr(dr("STD_Loaction_Code"))
                objtr.STD_Loaction_Desc = clsCommon.myCstr(dr("STD_Loaction_Desc"))

                objtr.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
                '' NEW COLUMNS
                'objtr.NO_SAMPLE_QC = clsCommon.myCdbl(dr("NO_SAMPLE_QC"))
                'objtr.DAMAGE_Qty = clsCommon.myCdbl(dr("DAMAGE_Qty"))
                'objtr.FINAL_PROD_Qty = clsCommon.myCdbl(dr("FINAL_PROD_Qty"))
                '' END NEW COLUMNS
                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function

End Class

Public Class clsPPStdFinalQCIssueItemDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Item_Type As String = Nothing
    Public Product_Type As String
    Public Unit_Desc As String = Nothing

    Public Avail_Qty As Decimal = 0
    Public Avail_FAT_Per As Decimal = 0
    Public Avail_SNF_Per As Decimal = 0
    Public Avail_FAT_KG As Decimal = 0
    Public Avail_SNF_KG As Decimal = 0

    Public Requir_FAT_Per As Decimal = 0
    Public Requir_SNF_Per As Decimal = 0

    Public Diff_Qty As Decimal = 0
    Public Diff_FAT_Per As Decimal = 0
    Public Diff_SNF_Per As Decimal = 0
    Public Diff_FAT_KG As Decimal = 0
    Public Diff_SNF_KG As Decimal = 0

    Public Remarks As String = Nothing
    Public TO_LOC_CODE As String = Nothing
    Public TO_LOC_DESC As String = Nothing
    Public Issue_Status As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal QC_Code As String, ByVal obj As clsPPStdFinalQCHead, ByVal arr As List(Of clsPPStdFinalQCIssueItemDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where  QC_Code='" + QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPPStdFinalQCIssueItemDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "QC_Code", QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)

                    clsCommon.AddColumnsForChange(coll, "Avail_Qty", objtr.Avail_Qty)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)

                    clsCommon.AddColumnsForChange(coll, "Requir_FAT_Per", objtr.Requir_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Requir_SNF_Per", objtr.Requir_SNF_Per)

                    clsCommon.AddColumnsForChange(coll, "Diff_Qty", objtr.Diff_Qty)
                    clsCommon.AddColumnsForChange(coll, "Diff_FAT_Per", objtr.Diff_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Diff_FAT_KG", objtr.Diff_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Diff_SNF_Per", objtr.Diff_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Diff_SNF_KG", objtr.Diff_SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "TO_LOC_CODE", objtr.TO_LOC_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "TO_LOC_DESC", objtr.TO_LOC_DESC, True)

                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Issue_Status", objtr.Issue_Status, True)
                    '' production costing columns
                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
                'qry = "update  TSPL_PP_ISSUE_HEAD set QC_Code='" & QC_Code & "' where (QC_Code is null or QC_Code='" & QC_Code & "') and Batch_Code='" & obj.Child_Batch_Code & "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetPPSTDIssuedDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCIssueItemDetail)
        Dim objIssueList As New List(Of clsPPStdFinalQCIssueItemDetail)
        Dim qry As String = "select TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " where TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL. QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStdFinalQCIssueItemDetail()

                objtr.SNO = CInt(dr("SNO"))
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Avail_Qty = dr("Avail_Qty")
                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")

                objtr.Requir_FAT_Per = clsCommon.myCdbl(dr("Requir_FAT_Per"))
                objtr.Requir_SNF_Per = clsCommon.myCdbl(dr("Requir_SNF_Per"))

                objtr.Diff_FAT_KG = dr("Diff_FAT_KG")
                objtr.Diff_FAT_Per = dr("Diff_FAT_Per")
                objtr.Diff_Qty = dr("Diff_Qty")
                objtr.Diff_SNF_KG = dr("Diff_SNF_KG")
                objtr.Diff_SNF_Per = dr("Diff_SNF_Per")

                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.Issue_Status = clsCommon.myCstr(dr("Issue_Status"))

                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))
                objtr.TO_LOC_CODE = clsCommon.myCstr(dr("TO_LOC_CODE"))
                objtr.TO_LOC_DESC = clsLocation.GetName(clsCommon.myCstr(dr("TO_LOC_CODE")), trans)

                '' production costing columns
                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function

End Class

Public Class clsPPStdFinalQCAddRemoveItemDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing

    Public Item_Type As String = Nothing
    Public Product_Type As String
    Public Unit_Desc As String = Nothing

    Public ADD_REMOVE_QTY As Decimal = 0
    Public ADD_REMOVE_TYPE As String = Nothing
    Public Loaction_Code As String = Nothing
    Public Location_Desc As String = Nothing

    Public Remarks As String = Nothing

    Public AR_FAT_Per As Decimal = 0
    Public AR_FAT_KG As Decimal = 0
    Public AR_SNF_Per As Decimal = 0
    Public AR_SNF_KG As Decimal = 0

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0

    Public Total_Amount As Decimal
#End Region

    Public Shared Function SaveData(ByVal objStd As clsPPStdFinalQCHead, ByVal trans As SqlTransaction, ByVal isJobWorkInward As Boolean) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where  QC_Code='" + objStd.QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            Dim ii As Integer = 0
            If objStd.ArrARItem IsNot Nothing AndAlso objStd.ArrARItem.Count > 0 Then
                For Each objtr As clsPPStdFinalQCAddRemoveItemDetail In objStd.ArrARItem
                    coll = New Hashtable()
                    ii += 1
                    clsCommon.AddColumnsForChange(coll, "QC_Code", objStd.QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", ii)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "ADD_REMOVE_QTY", objtr.ADD_REMOVE_QTY)
                    clsCommon.AddColumnsForChange(coll, "ADD_REMOVE_TYPE", objtr.ADD_REMOVE_TYPE)
                    clsCommon.AddColumnsForChange(coll, "Loaction_Code", objtr.Loaction_Code)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    clsCommon.AddColumnsForChange(coll, "AR_FAT_Per", objtr.AR_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "AR_SNF_Per", objtr.AR_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "AR_FAT_KG", objtr.AR_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "AR_SNF_KG", objtr.AR_SNF_KG)

                    If isJobWorkInward Then
                        objtr.Fat_Rate = 0
                        objtr.SNF_Rate = 0
                        objtr.Fat_Amt = 0
                        objtr.SNF_Amt = 0
                    Else
                        If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                            objtr.Fat_Rate = objStd.Avg_Rate_FAT
                            objtr.SNF_Rate = objStd.Avg_Rate_SNF
                            objtr.Fat_Amt = objStd.Avg_Rate_FAT * objtr.AR_FAT_KG
                            objtr.SNF_Amt = objStd.Avg_Rate_SNF * objtr.AR_SNF_KG
                        Else
                            Dim objCost As New MIlkComponentType
                            objCost = clsInventoryMovementNew.GetAvgCost(objtr.Product_Type, objtr.Item_Code, objtr.Loaction_Code, objtr.ADD_REMOVE_QTY, objtr.Unit_Code, objtr.AR_FAT_KG, objtr.AR_SNF_KG, objStd.QC_Date, objStd.QC_Date, False, trans)
                            objtr.Fat_Rate = If(objtr.AR_FAT_KG <= 0, 0, objCost.FAT_Cost / objtr.AR_FAT_KG)
                            objtr.SNF_Rate = If(objtr.AR_SNF_KG <= 0, 0, objCost.SNF_Cost / objtr.AR_SNF_KG)
                            objtr.Fat_Amt = objCost.FAT_Cost
                            objtr.SNF_Amt = objCost.SNF_Cost
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetBackQtyFatSNFRate(ByVal Doc_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim objCost As New MIlkComponentType
        Dim qry As String = "select AVG(Fat_Rate) as Fat_Rate,AVG(SNF_Rate) as SNF_Rate from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where QC_Code='" & Doc_Code & "' and Item_Code='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            objCost.FAT_Cost = clsCommon.myCdbl(dt.Rows(0).Item("Fat_Rate"))
            objCost.SNF_Cost = clsCommon.myCdbl(dt.Rows(0).Item("SNF_Rate"))
        End If
        Return objCost
    End Function

    Public Shared Function GetPPSTDARDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCAddRemoveItemDetail)
        Dim objARList As New List(Of clsPPStdFinalQCAddRemoveItemDetail)
        Dim qry As String = "select TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,TSPL_LOCATION_MASTER.Location_Desc from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_LOCATION_MASTER on TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL.Loaction_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
        " where TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL. QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStdFinalQCAddRemoveItemDetail()
                objtr.SNO = CInt(dr("SNO"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.ADD_REMOVE_QTY = clsCommon.myCdbl(dr("ADD_REMOVE_QTY"))
                objtr.ADD_REMOVE_TYPE = clsCommon.myCstr(dr("ADD_REMOVE_TYPE"))
                objtr.Loaction_Code = clsCommon.myCstr(dr("Loaction_Code"))
                objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))

                objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                'objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))

                objtr.AR_FAT_Per = clsCommon.myCdbl(dr("AR_FAT_Per"))
                objtr.AR_SNF_Per = clsCommon.myCdbl(dr("AR_SNF_Per"))
                objtr.AR_FAT_KG = clsCommon.myCdbl(dr("AR_FAT_KG"))
                objtr.AR_SNF_KG = clsCommon.myCdbl(dr("AR_SNF_KG"))

                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                objtr.Total_Amount = clsCommon.myCdbl(dr("Total_Amount"))
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function
End Class

Public Class clsPPStdFinalQCQCDetail
#Region "Variables"
    Public Parent_Line_No As Integer = Nothing
    Public sno As Integer = Nothing
    Public Item_Code As String = Nothing
    Public ItemDesc As String = Nothing
    Public param_code As String = Nothing
    Public param_desc As String = Nothing
    Public param_type As String = Nothing
    Public param_nature As String = Nothing
    Public param_nature_Desc As String = Nothing
    Public Standard_Range As Decimal = Nothing
    Public Standard_Status As String = Nothing
    Public Standard_Value As String = Nothing

    'Public urange As Decimal = Nothing
    Public Actual_Range As String = Nothing
    Public Actual_Value As String = Nothing
    Public Actual_Status As String = Nothing
    Public Qc_Status As String
    Public remarks As String = Nothing
    Public QC_Type As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal QC_Code As String, ByVal arr As List(Of clsPPStdFinalQCQCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_QC_DETAIL where  QC_Code='" + QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPPStdFinalQCQCDetail In arr
                    qry = "select Lower_range,Upper_range from TSPL_ITEM_QC_PARAMETER_MASTER where Item_Code='" + objtr.Item_Code + "' and Code in ('" + objtr.param_code + "') and code in ('FAT','SNF') "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If objtr.Actual_Range < clsCommon.myCdbl(dt.Rows(0)("Lower_range")) OrElse objtr.Actual_Range > clsCommon.myCdbl(dt.Rows(0)("Upper_range")) Then
                            Throw New Exception("Item Code [" + objtr.Item_Code + "] QC Paramter [" + objtr.param_code + "] Entered Value [" + clsCommon.myCstr(objtr.Actual_Range) + "] Valid Range [" + clsCommon.myCstr(dt.Rows(0)("Lower_range")) + "-" + clsCommon.myCstr(dt.Rows(0)("Upper_range")) + "]")
                        End If
                    End If

                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "QC_Code", QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Parent_Line_No", objtr.Parent_Line_No)
                    clsCommon.AddColumnsForChange(coll, "QC_Type", objtr.QC_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code)
                    clsCommon.AddColumnsForChange(coll, "Lower_range", objtr.Standard_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_range", objtr.Standard_Range)
                    clsCommon.AddColumnsForChange(coll, "Actual_Range", objtr.Actual_Range)
                    clsCommon.AddColumnsForChange(coll, "Actual_Status", objtr.Actual_Status)
                    clsCommon.AddColumnsForChange(coll, "Actual_Value", objtr.Actual_Value)
                    clsCommon.AddColumnsForChange(coll, "Qc_Status", objtr.Qc_Status)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_QC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetPPSTDQCDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCQCDetail)
        Dim objQCList As New List(Of clsPPStdFinalQCQCDetail)
        Dim qry As String = "select TSPL_PP_STD_FINALQC_QC_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,TSPL_ITEM_MASTER.Product_Type, " & _
        " TSPL_PARAMETER_MASTER.Description as Paramenter_Desc,TSPL_PARAMETER_MASTER.Nature as Parameter_Nature,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else  case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature_Desc,TSPL_PARAMETER_MASTER.Type as Parameter_Type " & _
        " from TSPL_PP_STD_FINALQC_QC_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_STD_FINALQC_QC_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_PARAMETER_MASTER on TSPL_PP_STD_FINALQC_QC_DETAIL.Parameter_Code=TSPL_PARAMETER_MASTER.Code " & _
        " where TSPL_PP_STD_FINALQC_QC_DETAIL. QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStdFinalQCQCDetail()
                objtr.sno = CInt(dr("SNO"))
                objtr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.ItemDesc = clsCommon.myCstr(dr("Item_Desc"))

                objtr.QC_Type = clsCommon.myCstr(dr("QC_Type"))
                objtr.param_code = clsCommon.myCstr(dr("Parameter_Code"))
                objtr.param_desc = clsCommon.myCstr(dr("Paramenter_Desc"))
                objtr.param_nature = clsCommon.myCstr(dr("Parameter_Nature"))
                objtr.param_nature_Desc = clsCommon.myCstr(dr("Nature_Desc"))
                objtr.param_type = clsCommon.myCstr(dr("Parameter_Type"))
                objtr.remarks = clsCommon.myCstr(dr("remarks"))

                objtr.Standard_Range = clsCommon.myCstr(dr("Lower_Range"))


                objtr.Actual_Range = clsCommon.myCstr(dr("Actual_Range"))
                objtr.Actual_Status = clsCommon.myCstr(dr("Actual_Status"))
                objtr.Actual_Value = clsCommon.myCstr(dr("Actual_Value"))
                objtr.Qc_Status = clsCommon.myCstr(dr("Qc_Status"))

                objQCList.Add(objtr)
            Next
        End If
        Return objQCList
    End Function
End Class

'Public Class clsPPStdFinalQCLogSheetDetail
'#Region "Variables"
'    Public QC_Code As String = Nothing
'    Public Log_Sheet_No As String = Nothing
'    Public Sno As Integer = Nothing
'    Public Stage_Code As String = Nothing
'    Public xtime As String = Nothing
'    Public param_code As String = Nothing
'    Public Parameter_STD_Value As String = Nothing
'    Public Parameter_ACT_Value As String = Nothing
'    Public Parameter_Desc As String = Nothing
'    Public Comp_Code As String = Nothing
'    Public Fill_Date As Date? = Nothing
'    Public QCLM_CODE As String = Nothing
'    Public Batch_Code As String = Nothing
'#End Region

'    Public Shared Function SaveData(ByVal strCode As String, ByVal StageCode As String, ByVal arr As List(Of clsPPStdFinalQCLogSheetDetail), ByVal trans As SqlTransaction) As Boolean
'        Try
'            Dim isSaved As Boolean = True
'            Dim coll As New Hashtable()

'            Dim qry As String = ""
'            qry = "delete from TSPL_PP_STD_FINALQC_QC_LOG_SHEET where QC_Code='" + strCode + "' " & _
'                    " and Stage_Code='" & StageCode & "'"
'            clsDBFuncationality.ExecuteNonQuery(qry, trans)
'            If arr IsNot Nothing AndAlso arr.Count > 0 Then
'                For Each objtr As clsPPStdFinalQCLogSheetDetail In arr
'                    coll = New Hashtable()
'                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
'                    clsCommon.AddColumnsForChange(coll, "QC_Code", strCode)
'                    clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", objtr.Log_Sheet_No)
'                    clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.Stage_Code)
'                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.Sno)
'                    clsCommon.AddColumnsForChange(coll, "Time_Value", objtr.xtime)
'                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code, True)
'                    clsCommon.AddColumnsForChange(coll, "Parameter_STD_Value", objtr.Parameter_STD_Value)
'                    clsCommon.AddColumnsForChange(coll, "Parameter_ACT_Value", objtr.Parameter_ACT_Value)
'                    clsCommon.AddColumnsForChange(coll, "Fill_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
'                    clsCommon.AddColumnsForChange(coll, "Batch_Code", objtr.Batch_Code, True)
'                    clsCommon.AddColumnsForChange(coll, "QCLM_CODE", objtr.QCLM_CODE, True)
'                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_QC_LOG_SHEET", OMInsertOrUpdate.Insert, "", trans)
'                Next
'            End If

'            Return isSaved
'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'        End Try
'    End Function
'    Public Shared Function GetPPSTDQCLogSheetDetail(ByVal QC_Code As String, ByVal Stage_Code As String, ByVal Log_Sheet_No As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCLogSheetDetail)
'        Dim objQCList As New List(Of clsPPStdFinalQCLogSheetDetail)
'        'Dim qry As String = "select TSPL_PP_LOG_SHEET_DETAIL.Doc_No as Log_Sheet_No,TSPL_PP_LOG_SHEET_HEAD.Stage_Code,TSPL_PP_LOG_SHEET_DETAIL.SNO, " & _
'        '" TSPL_PP_LOG_SHEET_DETAIL.Time_Value,TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code as QCLM_CODE,TSPL_PARAMETER_MASTER.Description as Parameter_Desc," & _
'        '" Parameter_Value as Parameter_STD_Value,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Parameter_ACT_Value ,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QC_Code,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Batch_Code,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QCLM_CODE " & _
'        '" from TSPL_PP_LOG_SHEET_DETAIL " & _
'        '" left join TSPL_PARAMETER_MASTER on TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PARAMETER_MASTER.Code " & _
'        '" left join  TSPL_PP_LOG_SHEET_HEAD on TSPL_PP_LOG_SHEET_HEAD.Doc_No=TSPL_PP_LOG_SHEET_DETAIL.Doc_No " & _
'        '" left join  (select * from TSPL_PP_STD_FINALQC_QC_LOG_SHEET where QC_Code='" + QC_Code + "') as TSPL_PP_STD_FINALQC_QC_LOG_SHEET  " & _
'        '" on TSPL_PP_LOG_SHEET_DETAIL.Doc_No=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Log_Sheet_No " & _
'        '" AND TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QCLM_CODE   " & _
'        '" AND TSPL_PP_LOG_SHEET_DETAIL.Time_Value=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Time_Value " & _
'        '" where TSPL_PP_LOG_SHEET_HEAD.stage_code='" + Stage_Code + "' and TSPL_PP_LOG_SHEET_HEAD.DOC_No='" & Log_Sheet_No & "'  order by sno"
'        Dim qry As String = "select TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Log_Sheet_No as Log_Sheet_No,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Stage_Code,TSPL_PP_LOG_SHEET_DETAIL.SNO, " & _
'        " TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Time_Value,TSPL_PARAMETER_MASTER.Description as Parameter_Desc, Parameter_Value as Parameter_STD_Value, " & _
'        " TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Parameter_ACT_Value ,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QC_Code,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Batch_Code,TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QCLM_CODE  " & _
'        " from  (select * from TSPL_PP_STD_FINALQC_QC_LOG_SHEET where QC_Code='" + QC_Code + "') as TSPL_PP_STD_FINALQC_QC_LOG_SHEET " & _
'        " left join TSPL_PP_LOG_SHEET_DETAIL      on TSPL_PP_LOG_SHEET_DETAIL.Doc_No=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Log_Sheet_No  " & _
'        " AND TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.QCLM_CODE    " & _
'        " AND TSPL_PP_LOG_SHEET_DETAIL.Time_Value=TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Time_Value " & _
'        " left join TSPL_PARAMETER_MASTER on TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PARAMETER_MASTER.Code " & _
'        " left join  TSPL_PP_LOG_SHEET_HEAD on TSPL_PP_LOG_SHEET_HEAD.Doc_No=TSPL_PP_LOG_SHEET_DETAIL.Doc_No " & _
'        " where TSPL_PP_STD_FINALQC_QC_LOG_SHEET.stage_code='" + Stage_Code + "' and TSPL_PP_STD_FINALQC_QC_LOG_SHEET.Log_Sheet_No='" & Log_Sheet_No & "'  order by sno"
'        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

'        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
'            For Each dr As DataRow In dt1.Rows
'                Dim objtr As New clsPPStdFinalQCLogSheetDetail()
'                objtr.Sno = clsCommon.myCdbl(dr("SNO"))
'                objtr.Comp_Code = objCommonVar.CurrentCompanyCode
'                objtr.Log_Sheet_No = clsCommon.myCstr(dr("Log_Sheet_No"))
'                objtr.param_code = "" ''clsCommon.myCstr(dr("Parameter_Code"))

'                objtr.Parameter_ACT_Value = clsCommon.myCstr(dr("Parameter_ACT_Value"))
'                objtr.Parameter_STD_Value = clsCommon.myCstr(dr("Parameter_STD_Value"))
'                objtr.Parameter_Desc = clsCommon.myCstr(dr("Parameter_Desc")) 'clsCommon.myCstr(dr("Parameter_Desc"))
'                objtr.Parameter_STD_Value = clsCommon.myCstr(dr("Parameter_STD_Value"))
'                objtr.Stage_Code = clsCommon.myCstr(dr("Stage_Code"))
'                objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
'                objtr.Batch_Code = clsCommon.myCstr(dr("Batch_Code"))
'                objtr.QCLM_CODE = clsCommon.myCstr(dr("QCLM_CODE"))

'                objtr.xtime = clsCommon.myCstr(dr("Time_Value"))

'                objQCList.Add(objtr)
'            Next
'        End If
'        Return objQCList
'    End Function

'    Public Shared Function GetPPSTDXTimeDetail(ByVal QC_Code As String, ByVal Stage_Code As String, ByVal Log_Sheet_No As String, ByVal trans As SqlTransaction) As List(Of String)
'        Dim objXTime As New List(Of String)
'        Dim qry As String = "select distinct SNO,Time_Value from TSPL_PP_STD_FINALQC_QC_LOG_SHEET " & _
'        " where stage_code='" + Stage_Code + "' and Log_Sheet_No='" & Log_Sheet_No & "' and QC_Code='" & QC_Code & "'  order by sno"
'        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

'        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
'            For Each dr As DataRow In dt1.Rows
'                objXTime.Add(clsCommon.myCstr(dr.Item("Time_Value")))
'            Next
'        End If
'        Return objXTime
'    End Function
'End Class

Public Class clsPPStdFinalQCStageDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public SNO As Integer = 0
    Public Stage_Code As String = Nothing
    Public Stage_Desc As String = Nothing
    Public Received_Qty As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Log_Sheet_No As String = Nothing
    Public Status As String = Nothing
    Public Remarks As String
    Public Section_Code As String
    Public Structure_Code As String
    Public Batch_Code As String = Nothing
    'Public SPQCList As New List(Of clsPPStdFinalQCLogSheetDetail)
    Public arrXtime As New List(Of String)
#End Region

    Public Shared Function SaveData(ByVal QC_Code As String, ByVal arr As List(Of clsPPStdFinalQCStageDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_STD_FINALQC_STAGE_DETAIL where  QC_Code='" + QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPPStdFinalQCStageDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "QC_Code", QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.Stage_Code)
                    clsCommon.AddColumnsForChange(coll, "Received_Qty", objtr.Received_Qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", objtr.Log_Sheet_No, True)
                    clsCommon.AddColumnsForChange(coll, "Status", objtr.Status)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", objtr.Section_Code)
                    clsCommon.AddColumnsForChange(coll, "Structure_Code", objtr.Structure_Code)
                    clsCommon.AddColumnsForChange(coll, "Batch_Code", objtr.Batch_Code, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_STAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    'isSaved = isSaved AndAlso clsPPStdFinalQCLogSheetDetail.SaveData(QC_Code, objtr.Stage_Code, objtr.SPQCList, trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetPPSPDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCStageDetail)
        Dim objARList As New List(Of clsPPStdFinalQCStageDetail)
        Dim qry As String = "select TSPL_PP_STD_FINALQC_STAGE_DETAIL.*, " & _
        " TSPL_UNIT_MASTER.Unit_Desc,TSPL_STAGE_MASTER.Description as Stage_Desc from TSPL_PP_STD_FINALQC_STAGE_DETAIL " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_STD_FINALQC_STAGE_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_STAGE_MASTER on TSPL_PP_STD_FINALQC_STAGE_DETAIL.Stage_Code=TSPL_STAGE_MASTER.Stage_Code  " & _
        " where TSPL_PP_STD_FINALQC_STAGE_DETAIL. QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStdFinalQCStageDetail()
                objtr.SNO = CInt(dr("SNO"))
                objtr.Log_Sheet_No = clsCommon.myCstr(dr("Log_Sheet_No"))
                objtr.Received_Qty = clsCommon.myCdbl(dr("Received_Qty"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.Stage_Code = clsCommon.myCstr(dr("Stage_Code"))
                objtr.Stage_Desc = clsCommon.myCstr(dr("Stage_Desc"))
                objtr.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                objtr.Status = clsCommon.myCstr(dr("Status"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                objtr.Batch_Code = clsCommon.myCstr(dr("Batch_Code"))
                'objtr.SPQCList = clsPPStdFinalQCLogSheetDetail.GetPPSTDQCLogSheetDetail(QC_Code, objtr.Stage_Code, objtr.Log_Sheet_No, trans)
                'objtr.arrXtime = clsPPStdFinalQCLogSheetDetail.GetPPSTDXTimeDetail(QC_Code, objtr.Stage_Code, objtr.Log_Sheet_No, trans)
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function



End Class

Public Class clsPPStdFinalQCDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public QC_From As String = Nothing
    Public Parent_ID As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPPStdFinalQCDetail), ByVal trans As SqlTransaction) As Boolean
        Dim sQuery As String = "Delete from TSPL_PP_STD_FINALQC_DETAIL where QC_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPPStdFinalQCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "QC_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                clsCommon.AddColumnsForChange(coll, "QC_From", obj.QC_From)
                clsCommon.AddColumnsForChange(coll, "Parent_ID", obj.Parent_ID)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPPStdFinalQCDetail)
        Dim arr As List(Of clsPPStdFinalQCDetail) = Nothing
        Dim qry As String
        qry = "select TSPL_PP_STD_FINALQC_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc from " & _
            "TSPL_PP_STD_FINALQC_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_DETAIL.Item_Code where TSPL_PP_STD_FINALQC_DETAIL.QC_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsPPStdFinalQCDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsPPStdFinalQCDetail = New clsPPStdFinalQCDetail()
                obj.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.SNF_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.FAT_Per = clsCommon.myCdbl(dr("fat_per"))
                obj.QC_From = clsCommon.myCstr(dr("QC_From"))
                obj.Parent_ID = clsCommon.myCdbl(dr("Parent_ID"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPPStdFinalQCQCParam
    Public QC_Code As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsPPStdFinalQCQCParam)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsPPStdFinalQCQCParam), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_PP_STD_FINALQC_QC_PARAMETER where QC_Code='" & strQCNo & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsPPStdFinalQCQCParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_Code", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_QC_PARAMETER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPPStdFinalQCQCParam)
        Dim arrObj As List(Of clsPPStdFinalQCQCParam) = Nothing
        Try
            Dim obj As clsPPStdFinalQCQCParam = Nothing
            Dim qry As String = "select * from TSPL_PP_STD_FINALQC_QC_PARAMETER where QC_Code='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsPPStdFinalQCQCParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsPPStdFinalQCQCParam()
                    obj.QC_Code = clsCommon.myCstr(dt.Rows(i)("QC_Code"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class


Public Class clsPPStdFinalQCRM
#Region "Variables"
    Public Standardization_Code As String
    Public CONSM_ITEM_CODE As String
    Public CONSM_QTY As Decimal
    Public LOCATION_CODE As String
    Public UNIT_CODE As String
    Public FIFO_COST As Decimal
    Public LIFO_COST As Decimal
    Public AVG_COST As Decimal

    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region
    Public Shared Function GetRM(ByVal QC_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = ""

        Dim MI_Consm_Type As String = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkStandardization, trans))
        Dim MP_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkProduct, trans)
        Dim Othr_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeOther, trans)

        qry = " select max(Consumption.ProcessLossPer) as ProcessLossPer,consumption.QC_Code,CONSM_LOCATION_CODE,CONSM_SECTION_CODE," & _
              " Consm_Item_Code,(case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when '" & MI_Consm_Type & "'='0' then TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Unit_Code else Consumption.Consm_Unit_Code end ) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Unit_Code else Consumption.Consm_Unit_Code end ) else  (case when '" & Othr_Consm_Type & "'='0' then TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Unit_Code else Consumption.Consm_Unit_Code end ) end) as Consm_Unit_Code, " & _
              " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) else  (case when '" & Othr_Consm_Type & "'='0' then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) end),3) as CONSM_QTY, " & _
              " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_FAT_KG) " & _
              " else  sum(Consumption.Consm_Fat_Kg) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' " & _
              " then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_FAT_KG) else  sum(Consumption.Consm_Fat_Kg) end) else  0 end),3) as FAT_KG, " & _
              " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_SNF_KG) " & _
              " else  sum(Consumption.Consm_SNF_Kg) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' " & _
              " then max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_SNF_KG) else  sum(Consumption.Consm_SNF_Kg) end) else  0 end),3) as SNF_KG, " & _
              " (case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when '" & MI_Consm_Type & "'='0' then (case when max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty)=0 then 0 else round((max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_FAT_KG)/max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty))*100,3) end) else max(Consumption.Consm_Fat_Per) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then (case when max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty)=0 then 0 else round((max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_FAT_KG)/max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty))*100,3) end) else max(Consumption.Consm_Fat_Per) end) else 0 end) as FAT_Pers," & _
              " (case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when '" & MI_Consm_Type & "'='0' then (case when max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty)=0 then 0 else round((max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_SNF_KG)/max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty))*100,3) end) else max(Consumption.Consm_SNF_Per) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then (case when max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty)=0 then 0 else round((max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_SNF_KG)/max(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Avail_Qty))*100,3) end) else max(Consumption.Consm_SNF_Per) end) else 0 end) as SNF_Pers, " & _
              " sum(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Fat_Amt) as Fat_Amt,sum(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.SNF_Amt) as SNF_Amt,sum(TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.QtyToCheck) as QtyToCheck from ( " & _
              " select TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer, TSPL_PP_STD_FINALQC_HEAD.QC_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.BOM_CODE,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.ITEM_CODE, " & _
              " TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as Batch_Qty,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.UNIT_CODE, " & _
              " TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty as FINAL_PRODUCTION_QTY, " & _
              " TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as LOCATION_CODE,CONSM_LOCATION_CODE, CONSM_SECTION_CODE, " & _
              " TSPL_PP_BOM_ITEM_DETAIL.Item_Code as Consm_Item_Code, " & _
              " TSPL_PP_BOM_ITEM_DETAIL.Unit_Code as Consm_Unit_Code, " & _
              " ((TSPL_PP_BOM_ITEM_DETAIL.Quantity/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty * (select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code))*((100.00+ProcessLossPer)/100.00)  as Consm_Qty, " & _
              " (TSPL_PP_BOM_ITEM_DETAIL.FAT_KG/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code)*((100.00+ProcessLossPer)/100.00) as Consm_Fat_Kg," & _
              " (TSPL_PP_BOM_ITEM_DETAIL.SNF_KG/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_Qty*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code " & _
              " and UOM_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code)*((100.00+ProcessLossPer)/100.00) as Consm_SNF_Kg,TSPL_PP_BOM_ITEM_DETAIL.Fat as Consm_Fat_Per,TSPL_PP_BOM_ITEM_DETAIL.SNF as Consm_SNF_Per  " & _
              " from TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL inner join TSPL_PP_STD_FINALQC_HEAD " & _
              " on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code " & _
              " left join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & _
              " left join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_CODE " & _
              " and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.ITEM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
              " and TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code " & _
              " left join (select 'xxx' as History_No,BOM_CODE, Item_Code,Unit_Code,Quantity,FAT_KG,SNF_KG,Fat,SNF,ProcessLossPer from TSPL_PP_BOM_ITEM_DETAIL union all select History_No,BOM_CODE, Item_Code,Unit_Code,Quantity,FAT_KG,SNF_KG,Fat,SNF,ProcessLossPer from TSPL_PP_BOM_ITEM_DETAIL_HISTORY) as TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.BOM_Code =TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE" + Environment.NewLine + _
              " inner join (select Revision_No, 'xxx' as History_No,BOM_CODE,PROD_QUANTITY,Prod_Item_Unit_Code from TSPL_PP_BOM_HEAD union all select Revision_No,History_No,BOM_CODE,PROD_QUANTITY,Prod_Item_Unit_Code from TSPL_PP_BOM_HEAD_HISTORY) as TSPL_PP_BOM_HEAD " + Environment.NewLine + _
              " on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE and isnull( TSPL_PP_BOM_HEAD.Revision_no,'')=isnull( TSPL_PP_BATCH_ORDER_BOM_DETAIL.bom_revision_no,'') and TSPL_PP_BOM_HEAD.History_No=TSPL_PP_BOM_ITEM_DETAIL.History_No" + Environment.NewLine + _
              " where TSPL_PP_STD_FINALQC_HEAD.QC_Code='" & QC_Code & "'  " & _
              " ) as Consumption " & _
              " inner join (select QC_Code,Item_Code,Unit_Code,sum(Avail_Qty) as Avail_Qty,sum(Avail_FAT_KG) as Avail_FAT_KG,sum(Avail_SNF_KG) as Avail_SNF_KG," & _
              " (sum(Avail_FAT_KG)/(case when sum(Avail_Qty)=0 then 1 else sum(Avail_Qty) end))*100 as Avail_FAT_Per, " & _
              " (sum(Avail_SNF_KG)/(case when sum(Avail_Qty)=0 then 1 else sum(Avail_Qty) end))*100  as Avail_SNF_Per,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) as SNF_Amt,sum(QtyToCheck) as QtyToCheck " & _
              " from ( " & _
              " select QC_Code,Item_Code,Unit_Code,Avail_Qty,Avail_FAT_Per,Avail_FAT_KG,Avail_SNF_Per,Avail_SNF_KG,Fat_Amt,SNF_Amt,Avail_Qty as QtyToCheck from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL " & _
              " where QC_Code='" & QC_Code & "' " & _
              " union all " & _
              " select QC_Code,Item_Code,Unit_Code,ADD_REMOVE_QTY ,AR_FAT_Per,AR_FAT_KG,AR_SNF_Per,AR_SNF_KG,Fat_Amt,SNF_Amt,0 as QtyToCheck " & _
              " from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where ADD_REMOVE_TYPE='Remove' and QC_Code='" & QC_Code & "' and Item_Code not in (select Item_Code from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL  where QC_Code='" & QC_Code & "') " & _
              " union all  " + _
              " select QC_Code,Item_Code,Unit_Code,ADD_REMOVE_QTY * case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as ADD_REMOVE_QTY,AR_FAT_Per,AR_FAT_KG  * case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as AR_FAT_KG,AR_SNF_Per,AR_SNF_KG  * case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as AR_SNF_KG,Fat_Amt  * case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as Fat_Amt,SNF_Amt  * case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as SNF_Amt,0 as QtyToCheck from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where  QC_Code='" & QC_Code & "' and Item_Code   in (select Item_Code from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL  where QC_Code='" & QC_Code & "') " + _
              " ) as TSPL_PP_PE_ISSUE_ITEM_DETAIL group by QC_Code,Item_Code,Unit_Code) TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL on Consumption.QC_Code=TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.QC_Code and Consumption.Consm_Item_Code=TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Item_Code " & _
              " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=Consumption.Item_Code " & _
              " group by consumption.QC_Code,CONSM_LOCATION_CODE, CONSM_SECTION_CODE, CONSM_ITEM_CODE, Consm_Unit_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL.Unit_Code "
        Return clsDBFuncationality.GetDataTable(qry, trans)
        ''ERO/18/02/19-000491 by balwinder on 28/02/2019 (Add Union all above)
    End Function
    Public Shared Function SaveRM(ByVal Std_Code As String, ByVal QC_Code As String, ByVal arrLoc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objRec As clsPPStdFinalQCHead
        objRec = clsPPStdFinalQCHead.GetData(QC_Code, arrLoc, NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Return False
        End If
        If clsCommon.myLen(objRec.Against_STD_Code) <= 0 Or clsCommon.myLen(objRec.Child_Batch_Code) <= 0 Then
            Return False
        End If
        Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
        Dim dtIssue As DataTable = GetRM(QC_Code, trans)
        Dim qry As String = ""
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        For Each dr As DataRow In dtIssue.Rows
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Standardization_Code", objRec.Against_STD_Code)
            clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", clsCommon.myCstr(dr.Item("Consm_Item_Code")))

            clsCommon.AddColumnsForChange(coll, "CONSM_QTY", clsCommon.myCdbl(dr.Item("Consm_Qty")))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")))
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", clsCommon.myCstr(dr.Item("Consm_Unit_Code")))

            clsCommon.AddColumnsForChange(coll, "FAT_Per", clsCommon.myCdbl(dr.Item("FAT_Pers")))
            clsCommon.AddColumnsForChange(coll, "SNF_Per", clsCommon.myCdbl(dr.Item("SNF_Pers")))
            clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCdbl(dr.Item("FAT_KG")))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(dr.Item("SNF_KG")))

            '' production costing cols
            clsCommon.AddColumnsForChange(coll, "FAT_Amt", clsCommon.myCdbl(dr.Item("FAT_Amt")))
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", clsCommon.myCdbl(dr.Item("SNF_Amt")))
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("FAT_Amt")) / clsCommon.myCdbl(dr.Item("FAT_KG"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("SNF_Amt")) / clsCommon.myCdbl(dr.Item("SNF_KG"))))

            clsCommon.AddColumnsForChange(coll, "Process_Loss_Per", clsCommon.myCdbl(dr.Item("ProcessLossPer")))

            Dim BalanceQty As Decimal = 0

            Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Consm_Item_Code"), trans)
            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), trans), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), QC_Code, objRec.QC_Date, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")))
            Else
                BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), QC_Code, objRec.QC_Date, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")), 0)
            End If
            BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero) ''By Balwinder on 29/12/2018 for EROD
            If clsCommon.myCdbl(dr.Item("QtyToCheck")) > BalanceQty Then ''change Consm_Qty to QtyToCheck becuase Consm_Qty having Add/Remove Qty.
                If Not settAllowNegativeStockInDairyProduction Then
                    If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("QtyToCheck")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                        Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")) & " Available Qty: " & BalanceQty & "  Consumed Qty: " & clsCommon.myCdbl(dr.Item("QtyToCheck")) & " ")
                    End If
                End If
            End If
            Dim objCost As New MIlkComponentType
            Dim cost As Decimal = clsCommon.myCdbl(dr.Item("FAT_Amt")) + clsCommon.myCdbl(dr.Item("SNF_Amt"))
            If cost <= 0 Then
                If Not objRec.Is_Job_Work_Inward Then
                    ''BHA/17/12/18-000754 Change by balwinder on 17/12/2018 Bharat Journal entry mismatch error coming doc no [PFQ-001/18/000723]
                    qry = "select count(1) as Cnt from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where QC_Code='" & QC_Code & "' and Item_Code='" & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & "'"
                    Dim IsAddRemoveItem As Boolean = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0)
                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal OrElse IsAddRemoveItem Then
                        objCost = GetItemIssueCost(QC_Code, clsCommon.myCstr(dr.Item("Consm_Item_Code")), trans)
                    Else
                        objCost = clsInventoryMovementNew.GetAvgCost(Product_Type, dr.Item("Consm_Item_Code"), dr.Item("CONSM_LOCATION_CODE"), clsCommon.myCdbl(dr.Item("Consm_Qty")), clsCommon.myCstr(dr.Item("Consm_Unit_Code")), clsCommon.myCdbl(dr.Item("FAT_KG")), clsCommon.myCdbl(dr.Item("SNF_KG")), objRec.QC_Date, clsCommon.GETSERVERDATE(trans), False, trans)
                    End If
                    cost = objCost.FAT_Cost + objCost.SNF_Cost
                    If cost <= 0 Then
                        Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                        Dim Qty As Decimal = clsCommon.myCdbl(dr("Consm_Qty")) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr("Consm_Item_Code")), clsCommon.myCstr(dr("Consm_Unit_Code")), trans)
                        cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, clsCommon.myCstr(dr("Consm_Item_Code")), clsCommon.myCstr(dr("CONSM_LOCATION_CODE")), Qty, objRec.QC_Date, clsCommon.GETSERVERDATE(trans), isApplyCostOnPostDate, trans)
                    End If
                    If cost <= 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                            Dim bascCost As Decimal = clsItemUOMDetails.GetItemUOMCost(objRec.QC_Date, clsCommon.myCstr(dr("Consm_Item_Code")), clsCommon.myCstr(dr("Consm_Unit_Code")), trans)
                            If bascCost <= 0 Then
                                Throw New Exception("Please provide Item Cost of item " + clsCommon.myCstr(dr("Consm_Item_Code")) + " and unit " + clsCommon.myCstr(dr("Consm_Unit_Code")))
                            End If
                            cost = clsCommon.myCdbl(dr("Consm_Qty")) * bascCost
                        End If
                    End If
                End If
            End If


            clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
            totalFifoCost = totalFifoCost + cost
            clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            totalAvgCost = totalAvgCost + cost
            clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
            totalLifoCost = totalLifoCost + cost
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
        Next
        UpdateCost(QC_Code, objRec.QC_Date, objRec.ArrBatchItem, trans, objRec.Is_Job_Work_Inward)
        Return True
    End Function
    Public Shared Function GetItemIssueCost(ByVal Doc_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Dim qry As String = "select coalesce(sum(Fat_Amt),0) as Fat_Amt," & _
                " coalesce(sum(SNF_Amt),0) as SNF_Amt from ( " & _
                " select Avail_FAT_KG,Avail_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where QC_Code='" & Doc_Code & "' and Item_Code='" & Item_Code & "' " & _
                " union all " & _
                " select AR_FAT_KG,AR_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where QC_Code='" & Doc_Code & "' and Item_Code='" & Item_Code & "' and ADD_REMOVE_TYPE='Add'" & _
                " ) as TotalIssue"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            obj.FAT_Cost = dt.Rows(0).Item("Fat_Amt")
            obj.SNF_Cost = dt.Rows(0).Item("SNF_Amt")
        End If
        Return obj
    End Function
    Public Shared Function UpdateRM(ByVal std_Code As String, ByVal QC_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objRec As clsPPStdFinalQCHead
        objRec = clsPPStdFinalQCHead.GetData(QC_Code, "", NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Return False
        End If
        If clsCommon.myLen(objRec.Against_STD_Code) <= 0 Or clsCommon.myLen(objRec.Child_Batch_Code) <= 0 Then
            Return False
        End If
        Dim dtIssue As DataTable
        Dim qry As String = ""

        dtIssue = GetRM(QC_Code, trans) 'clsDBFuncationality.GetDataTable(qry, trans)
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        For Each dr As DataRow In dtIssue.Rows
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_Code", QC_Code)
            clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", clsCommon.myCstr(dr.Item("Consm_Item_Code")))

            clsCommon.AddColumnsForChange(coll, "CONSM_QTY", clsCommon.myCdbl(dr.Item("Consm_Qty")))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")))
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", clsCommon.myCstr(dr.Item("Consm_Unit_Code")))


            clsCommon.AddColumnsForChange(coll, "FAT_Amt", clsCommon.myCdbl(dr.Item("FAT_Amt")))
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", clsCommon.myCdbl(dr.Item("SNF_Amt")))
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("FAT_Amt")) / clsCommon.myCdbl(dr.Item("FAT_KG"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("SNF_Amt")) / clsCommon.myCdbl(dr.Item("SNF_KG"))))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL", OMInsertOrUpdate.Update, "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.QC_Code='" & QC_Code & "' and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE='" & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & "'", trans)
        Next
        UpdateCost(QC_Code, objRec.QC_Date, objRec.ArrBatchItem, trans, objRec.Is_Job_Work_Inward)

        Return True
    End Function
    Public Shared Function GetIssueAvgRate1(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Dim qry As String = "select coalesce(sum(Fat_Amt)/sum(Avail_FAT_KG),0) as Fat_Rate,coalesce(sum(SNF_Amt)/sum(Avail_SNF_KG),0) as SNF_Rate from (" & _
                " select Avail_FAT_KG,Avail_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where QC_Code='" & Doc_Code & "' " & _
                " union all " & _
                " select AR_FAT_KG,AR_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where ADD_REMOVE_TYPE='Add' and QC_Code='" & Doc_Code & "' " & _
                " ) as TotalIssue"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            obj.FAT_Cost = dt.Rows(0).Item("Fat_Rate")
            obj.SNF_Cost = dt.Rows(0).Item("SNF_Rate")
        End If
        Return obj
    End Function
    Public Shared Function GetIssueCost(ByVal Std_Code As String, ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim obj As New MIlkComponentType
        Dim qry As String = "select sum(Cost) as Cost from (select (case when (Consm.Fat_Amt+Consm.SNF_Amt)<=0 then Consm.Avg_Cost else (Consm.Fat_Amt+Consm.SNF_Amt) end) as Cost from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL Consm " & _
            " left join TSPL_ITEM_MASTER Item on Consm.CONSM_ITEM_CODE=Item.Item_Code where Consm.Standardization_Code='" & Std_Code & "'" & _
            " union all select (case when ADD_REMOVE_TYPE='Add' then (Fat_Amt+SNF_Amt) else -(Fat_Amt+SNF_Amt) end) as Cost from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where  QC_Code='" & Doc_Code & "') as Final"
        Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return cost
    End Function
    Public Shared Function GetConsumedRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPPStdFinalQCRM)
        Dim qry As String = "select Standardization_Code,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_COST,LIFO_COST,AVG_COST,FAT_Per,FAT_KG,SNF_Per,SNF_KG,Fat_Rate,SNF_Rate,FAT_Amt,SNF_Amt from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where Standardization_Code ='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim obj As clsPPStdFinalQCRM
        Dim objList As New List(Of clsPPStdFinalQCRM)
        For Each dr As DataRow In dt.Rows
            obj = New clsPPStdFinalQCRM
            obj.Standardization_Code = dr.Item("Standardization_Code")
            obj.CONSM_ITEM_CODE = dr.Item("CONSM_ITEM_CODE")
            obj.CONSM_QTY = dr.Item("CONSM_QTY")
            obj.LOCATION_CODE = dr.Item("LOCATION_CODE")
            obj.UNIT_CODE = dr.Item("UNIT_CODE")
            obj.FIFO_COST = dr.Item("FIFO_COST")
            obj.LIFO_COST = dr.Item("LIFO_COST")
            obj.AVG_COST = dr.Item("AVG_COST")

            obj.FAT_Per = dr.Item("FAT_Per")
            obj.FAT_KG = dr.Item("FAT_KG")
            obj.SNF_Per = dr.Item("SNF_Per")
            obj.SNF_KG = dr.Item("SNF_KG")

            obj.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
            obj.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
            obj.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
            obj.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
            objList.Add(obj)
        Next
        Return objList
    End Function

    Private Shared Function UpdateCost(ByVal strDocCode As String, ByVal DocDate As DateTime, ArrBatchItem As List(Of clsPPStdFinalQCBatchItemDetail), ByVal tran As SqlTransaction, ByVal IsJobWorkInward As Boolean) As Boolean
        Dim qry As String = "select QC_Code,Item_Code as ICode,1 as RI ,Avail_Qty as Qty,Unit_Code as UOM,Avail_FAT_KG as FATKG,Avail_SNF_KG as SNFKG,Fat_Amt as FATAmt,SNF_Amt as SNFAmt,TO_LOC_CODE as Location,SNo,'TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL' as TableName,Fat_Rate,SNF_Rate from TSPL_PP_STD_FINALQC_ISSUE_ITEM_DETAIL where QC_Code='" + strDocCode + "'" + Environment.NewLine + _
        "union all" + Environment.NewLine + _
        "select QC_Code,Item_Code as ICode,case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end as RI,ADD_REMOVE_QTY as Qty,unit_code as UOM,AR_FAT_KG as FATKG,AR_SNF_KG as SNFKG,Fat_Amt as FATAmt,SNF_Amt as SNFAmt,Loaction_Code as Location,SNO,'TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL' as TableName,Fat_Rate,SNF_Rate from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where QC_Code='" + strDocCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim TotalAmt As Decimal = 0
        Dim FATKG As Decimal = 0
        Dim SNFKg As Decimal = 0
        Dim SettPickProductCostFromItemUOMDetail = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, tran)) > 0)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim Product_Type As String = clsItemMaster.GetItemProductType(clsCommon.myCstr(dr("ICode")), tran)
                Dim AvgCost As Decimal = 0
                If Not IsJobWorkInward Then
                    Dim IsAddRemoveItem As Boolean = False
                    If Not clsCommon.CompairString(clsCommon.myCstr(dr("TableName")), "TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL") = CompairStringResult.Equal Then
                        qry = "select count(1) as Cnt from TSPL_PP_STD_FINALQC_ADD_REMOVE_ITEM_DETAIL where QC_Code='" & clsCommon.myCstr(dr("QC_Code")) & "' and Item_Code='" & clsCommon.myCstr(dr("ICode")) & "'"
                        IsAddRemoveItem = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran)) > 0)
                    End If
                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal OrElse IsAddRemoveItem Then
                        AvgCost = clsCommon.myCdbl(dr("FATAmt")) + clsCommon.myCdbl(dr("SNFAmt"))
                        If AvgCost <= 0 Then
                            Dim objCost As MIlkComponentType = GetItemIssueCost(strDocCode, clsCommon.myCstr(dr("ICode")), tran)
                            AvgCost = objCost.FAT_Cost + objCost.SNF_Cost
                        End If
                    Else
                        AvgCost = clsCommon.myCdbl(dr("FATAmt")) + clsCommon.myCdbl(dr("SNFAmt"))
                    End If
                    If AvgCost <= 0 Then
                        Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, tran)) = 1, True, False)
                        Dim Qty As Decimal = clsCommon.myCdbl(dr("Qty")) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr("ICode")), clsCommon.myCstr(dr("UOM")), tran)
                        AvgCost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, clsCommon.myCstr(dr("ICode")), clsCommon.myCstr(dr("Location")), Qty, DocDate, clsCommon.GETSERVERDATE(tran), isApplyCostOnPostDate, tran)
                    End If
                    If AvgCost <= 0 Then
                        If SettPickProductCostFromItemUOMDetail Then
                            Dim bascCost As Decimal = clsItemUOMDetails.GetItemUOMCost(DocDate, clsCommon.myCstr(dr("ICode")), clsCommon.myCstr(dr("UOM")), tran)
                            If bascCost <= 0 Then
                                Throw New Exception("Please provide Item Cost of item " + clsCommon.myCstr(dr("ICode")) + " and unit " + clsCommon.myCstr(dr("UOM")))
                            End If
                            AvgCost = clsCommon.myCdbl(dr("Qty")) * bascCost
                        End If
                    End If
                End If
                TotalAmt += AvgCost * clsCommon.myCdbl(dr("RI"))
                FATKG += clsCommon.myCdbl(dr("FATKG"))
                SNFKg += clsCommon.myCdbl(dr("SNFKG"))
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Total_Amount", AvgCost)
                clsCommonFunctionality.UpdateDataTable(coll, clsCommon.myCstr(dr("TableName")), OMInsertOrUpdate.Update, "QC_Code='" + strDocCode + "' and SNO='" & clsCommon.myCstr(dr("SNO")) & "'", tran)
            Next
        End If
        'ERO/24/07/18-000383 by balwinder on 24/08/2018
        For Each objProd As clsPPStdFinalQCBatchItemDetail In ArrBatchItem
            Dim coll As New Hashtable()
            Dim objMCN As New MIlkComponentTypeNew
            If Not IsJobWorkInward Then
                objMCN = MIlkComponentTypeNew.CalculateFATSNFRate(TotalAmt, objProd.Produced_Qty, objProd.Produced_FAT_KG, objProd.Produced_SNF_KG)
            End If
            clsCommon.AddColumnsForChange(coll, "Fat_Rate", objMCN.FATRate)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", objMCN.SNFRate)
            If Not IsJobWorkInward Then
                If SettPickProductCostFromItemUOMDetail Then
                    Dim SettTollFATRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATRateTollerance, clsFixedParameterCode.ProductionFATRateTollerance, tran))
                    Dim SettTollSNFRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionSNFRateTollerance, clsFixedParameterCode.ProductionSNFRateTollerance, tran))
                    Dim objIMQCP As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(objProd.Item_Code, tran)
                    If Math.Round(objMCN.FATRate, 2, MidpointRounding.ToEven) > Math.Round(objIMQCP.FATRate + SettTollFATRate, 2, MidpointRounding.ToEven) OrElse Math.Round(objMCN.FATRate, 2, MidpointRounding.ToEven) < Math.Round(objIMQCP.FATRate - SettTollFATRate, 2, MidpointRounding.ToEven) Then
                        Throw New Exception("Correct Your Produce Qty or Add/Remove Item." + Environment.NewLine + "Document [" + strDocCode + "] Item [" + objProd.Item_Code + "] FAT Rate  [" + clsCommon.myCstr(Math.Round(objMCN.FATRate, 2, MidpointRounding.ToEven)) + "] and FAT Tollerance [" + clsCommon.myCstr(Math.Round(objIMQCP.FATRate - SettTollFATRate, 2, MidpointRounding.ToEven)) + "-" + clsCommon.myCstr(Math.Round(objIMQCP.FATRate + SettTollFATRate, 2, MidpointRounding.ToEven)) + "]")
                    End If
                    If Math.Round(objMCN.SNFRate, 2, MidpointRounding.ToEven) > Math.Round(objIMQCP.SNFRate + SettTollSNFRate, 2, MidpointRounding.ToEven) OrElse Math.Round(objMCN.SNFRate, 2, MidpointRounding.ToEven) < Math.Round(objIMQCP.SNFRate - SettTollSNFRate, 2, MidpointRounding.ToEven) Then
                        Throw New Exception("Correct Your Produce Qty or Add/Remove Item." + Environment.NewLine + "Document [" + strDocCode + "] Item [" + objProd.Item_Code + "] SNF Rate  [" + clsCommon.myCstr(Math.Round(objMCN.SNFRate, 2, MidpointRounding.ToEven)) + "] and SNF Tollerance [" + clsCommon.myCstr(Math.Round(objIMQCP.SNFRate - SettTollSNFRate, 2, MidpointRounding.ToEven)) + "-" + clsCommon.myCstr(Math.Round(objIMQCP.SNFRate + SettTollSNFRate, 2, MidpointRounding.ToEven)) + "]")
                    End If
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Fat_Amt", objMCN.FATRate * objProd.Produced_FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", objMCN.SNFRate * objProd.Produced_SNF_KG)
            Dim cost As Decimal = objMCN.FATRate * objProd.Produced_FAT_KG + objMCN.SNFRate * objProd.Produced_SNF_KG
            clsCommon.AddColumnsForChange(coll, "FIFO_Cost", TotalAmt)
            clsCommon.AddColumnsForChange(coll, "AVG_Cost", TotalAmt)
            clsCommon.AddColumnsForChange(coll, "LIFO_Cost", TotalAmt)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL", OMInsertOrUpdate.Update, "TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code='" + strDocCode + "' and TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.BOM_CODE='" & objProd.BOM_Code & "' AND TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.ITEM_CODE='" & objProd.Item_Code & "'", tran)
        Next
        Return True
    End Function

End Class

