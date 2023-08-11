Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ItemStockReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'TxtMultiLocation.arrValueMember = Nothing
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        TxtItem.arrValueMember = Nothing
        Gv1.DataSource = Nothing
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Item_Stock_Report
    End Sub

    Private Sub Load_Item_Stock_Report()

        Dim qry As String = " "
        Dim dt As New DataTable()
        Try
            Dim whr As String = " "
            If TxtItem.arrValueMember IsNot Nothing AndAlso TxtItem.arrValueMember.Count > 0 Then
                whr += " and Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")  "
            End If


            qry = " select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,CompName,FromDate,ToDate,Trans_Id,xxxxxxx.Location_Code,FORMAT(Punching_Date, 'dd/MM/yyyy')as Punching_Date,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,[Loc Desp],Item_Type_Name,Item_Code ,Item_Desc,Stock_UOM, cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as decimal (18,2)) as OPQty, CAST( CASE WHEN (ISNULL(CLQty, 0) - ISNULL(RecQty, 0) + ISNULL(IssQty, 0)) = 0 THEN 0 ELSE (ISNULL(CLCost, 0) - ISNULL(RecCost, 0) + ISNULL(IssCost, 0)) / NULLIF((ISNULL(CLQty, 0) - ISNULL(RecQty, 0) + ISNULL(IssQty, 0)), 0) END AS DECIMAL(18, 2)) AS OPRate ,cast ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as decimal (18,2)) as OPCost, cast (RecQty as decimal (18,2)) as RecQty,cast (RecRate as decimal (18,2)) as RecRate,cast (RecCost as decimal (18,2)) as RecCost,cast (IssQty as decimal (18,2)) as IssQty ,cast (IssRate as decimal (18,2)) as IssRate ,cast (IssCost as decimal (18,2)) as IssCost ,cast (CLQty  as decimal (18,2)) as CLQty ,cast (case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty  end as decimal (18,2)) as CLRate, cast (CLCost as decimal (18,2)) as CLCost from 

                                ( select  case when '1'='1' then 'Near Tabiji Farm, Bewar Road, Ajmer.' else 'Rajashthan Cooperative Dairy Federation Limited ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "'  as ToDate,Trans_Id,Punching_Date as Punching_Date ,Location_Code,[Loc Desp],Item_Type_Name,Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate,(case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1.00*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG  from

                                (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1.00 else -1.00 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end)) end as Rate,sum(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,sum( (case when IsFromMilk=1.00 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)) as Balance_SNF  from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', 
                                case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  

                                from TSPL_ITEM_QC_PARAMETER_MASTER  

                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range 

                                from TSPL_ITEM_QC_PARAMETER_MASTER 

                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))

                                from TSPL_ITEM_QC_PARAMETER_MASTER  
                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) 

                                from TSPL_ITEM_QC_PARAMETER_MASTER
                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                                ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 

                                 union all 

                                 select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor 
                                 from TSPL_INVENTORY_MOVEMENT_NEW
                                ) InventroyMovement 
                                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                                 left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                                 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                                 left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  
                                 from (
                                 select * from ( 
                                 select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                                 ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                                 from  TSPL_ITEM_MASTER  
                                 left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                                 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                                 where 2=2 
                                 )xx
                                 Pivot 
                                 ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                                 ) Pivt
                                 Pivot 
                                 (
                                 max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                                 ) Pivt1 
                                 ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  
                                 where 2=2  " + whr + " 
                                 and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = 'AJMR') )) xxx 
                                 where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  group by xxx.Item_Code 
                                 union all 
 
                                 select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1.00 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end) as Balance_SNF   from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                                ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                                 union all 
                                 select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                                ) InventroyMovement 
                                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                                 left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                                 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                                 left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                                 select * from ( 
                                 select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                                 ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                                 from  TSPL_ITEM_MASTER  
                                 left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                                 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                                 where 2=2 
                                 )xx
                                 Pivot 
                                 ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                                 ) Pivt
                                 Pivot 
                                 (
                                 max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                                 ) Pivt1 
                                 ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  " + whr + " 
                                 and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') )) xxx 
                                 where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                                )xxxxxx  )xxxxxxx 
                                LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=xxxxxxx.Location_Code
                                where Trans_Id<>0  Order by  Punching_Date,Trans_Id "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Item Stock Report")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("Location_Code").Width = 100
        Gv1.Columns("Location_Code").IsVisible = True
        Gv1.Columns("Location_Code").HeaderText = "Location Code"
        Gv1.Columns("Location_Code").FormatString = "{0:n2}"

        Gv1.Columns("Punching_Date").Width = 100
        Gv1.Columns("Punching_Date").IsVisible = True
        Gv1.Columns("Punching_Date").HeaderText = "Punching Date"
        Gv1.Columns("Punching_Date").FormatString = "{0:n2}"

        Gv1.Columns("Item_Code").Width = 100
        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Code").HeaderText = "Item Code"
        Gv1.Columns("Item_Code").FormatString = "{0:n2}"

        Gv1.Columns("Stock_UOM").Width = 100
        Gv1.Columns("Stock_UOM").IsVisible = True
        Gv1.Columns("Stock_UOM").HeaderText = "Stock UOM"
        Gv1.Columns("Stock_UOM").FormatString = "{0:n2}"

        Gv1.Columns("Item_Desc").Width = 150
        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").HeaderText = "Item Description"
        Gv1.Columns("Item_Desc").FormatString = "{0:n2}"

        Gv1.Columns("OPQty").Width = 100
        Gv1.Columns("OPQty").IsVisible = True
        Gv1.Columns("OPQty").HeaderText = "Opening Quantity"
        Gv1.Columns("OPQty").FormatString = "{0:n2}"

        Gv1.Columns("OPRate").Width = 100
        Gv1.Columns("OPRate").IsVisible = True
        Gv1.Columns("OPRate").HeaderText = "Opening Rate"
        Gv1.Columns("OPRate").FormatString = "{0:n2}"

        Gv1.Columns("OPCost").Width = 100
        Gv1.Columns("OPCost").IsVisible = True
        Gv1.Columns("OPCost").HeaderText = "Opening Cost"
        Gv1.Columns("OPCost").FormatString = "{0:n2}"

        Gv1.Columns("RecQty").Width = 100
        Gv1.Columns("RecQty").IsVisible = True
        Gv1.Columns("RecQty").HeaderText = "Received Quantity"
        Gv1.Columns("RecQty").FormatString = "{0:n2}"

        Gv1.Columns("RecRate").Width = 100
        Gv1.Columns("RecRate").IsVisible = True
        Gv1.Columns("RecRate").HeaderText = "Received Rate"
        Gv1.Columns("RecRate").FormatString = "{0:n2}"

        Gv1.Columns("RecCost").Width = 100
        Gv1.Columns("RecCost").IsVisible = True
        Gv1.Columns("RecCost").HeaderText = "Received Cost"
        Gv1.Columns("RecCost").FormatString = "{0:n2}"

        Gv1.Columns("IssQty").Width = 100
        Gv1.Columns("IssQty").IsVisible = True
        Gv1.Columns("IssQty").HeaderText = "Issued Quantity"
        Gv1.Columns("IssQty").FormatString = "{0:n2}"

        Gv1.Columns("IssRate").Width = 100
        Gv1.Columns("IssRate").IsVisible = True
        Gv1.Columns("IssRate").HeaderText = "Issued Rate"
        Gv1.Columns("IssRate").FormatString = "{0:n2}"

        Gv1.Columns("IssCost").Width = 100
        Gv1.Columns("IssCost").IsVisible = True
        Gv1.Columns("IssCost").HeaderText = "Issued Cost"
        Gv1.Columns("IssCost").FormatString = "{0:n2}"

        Gv1.Columns("CLQty").Width = 100
        Gv1.Columns("CLQty").IsVisible = True
        Gv1.Columns("CLQty").HeaderText = "Closing Quantity"
        Gv1.Columns("CLQty").FormatString = "{0:n2}"

        Gv1.Columns("CLRate").Width = 100
        Gv1.Columns("CLRate").IsVisible = True
        Gv1.Columns("CLRate").HeaderText = "Closing Rate"
        Gv1.Columns("CLRate").FormatString = "{0:n2}"

        Gv1.Columns("CLCost").Width = 100
        Gv1.Columns("CLCost").IsVisible = True
        Gv1.Columns("CLCost").HeaderText = "Closing Cost"
        Gv1.Columns("CLCost").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("OPQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("OPRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("OPCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("RecQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("RecRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("IssQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("IssRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("IssCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Dim item0 As New GridViewSummaryItem("CLQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item0)

        Dim item As New GridViewSummaryItem("CLRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item)

        Dim items As New GridViewSummaryItem("CLCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(items)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click

        Dim qry As String = " Select Item_Code as ItemCode,Item_Desc as ItemDescription from TSPL_ITEM_MASTER order by Item_Code "
        TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "ItemCode", "ItemDescription", TxtItem.arrValueMember, TxtItem.arrDispalyMember)

    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = " Select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrcls As String = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in(" + objCommonVar.strCurrUserLocations + ") "
        End If

        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where Location_Code= '" + txtBillToLocation.Value + "'"))

    End Sub

    Private Sub ItemStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Default_Location From TSPL_USER_MASTER Where User_Code= '" + objCommonVar.CurrentUserCode + "'"))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                arrHeader.Add("Location:" + clsCommon.myCstr(lblBillToLocation.Text))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Item Stock Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Item Stock Report", Gv1, arrHeader, "Item Stock Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim qry As String = " "
        Try
            Dim whr As String = " "
            If TxtItem.arrValueMember IsNot Nothing AndAlso TxtItem.arrValueMember.Count > 0 Then
                whr += " and Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")  "
            End If

            qry = " select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,CompName,FromDate,ToDate,Trans_Id,FORMAT(Punching_Date, 'dd/MM/yyyy')as Punching_Date,xxxxxxx.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,[Loc Desp],Item_Type_Name,Item_Code ,Item_Desc,Stock_UOM, cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as decimal (18,2)) as OPQty, CAST( CASE WHEN (ISNULL(CLQty, 0) - ISNULL(RecQty, 0) + ISNULL(IssQty, 0)) = 0 THEN 0 ELSE (ISNULL(CLCost, 0) - ISNULL(RecCost, 0) + ISNULL(IssCost, 0)) / NULLIF((ISNULL(CLQty, 0) - ISNULL(RecQty, 0) + ISNULL(IssQty, 0)), 0) END AS DECIMAL(18, 2)) AS OPRate ,cast ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as decimal (18,2)) as OPCost, cast (RecQty as decimal (18,2)) as RecQty,cast (RecRate as decimal (18,2)) as RecRate,cast (RecCost as decimal (18,2)) as RecCost,cast (IssQty as decimal (18,2)) as IssQty ,cast (IssRate as decimal (18,2)) as IssRate ,cast (IssCost as decimal (18,2)) as IssCost ,cast (CLQty  as decimal (18,2)) as CLQty ,cast (case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty  end as decimal (18,2)) as CLRate, cast (CLCost as decimal (18,2)) as CLCost from 

                                ( select  case when '1'='1' then 'Near Tabiji Farm, Bewar Road, Ajmer.' else 'Rajashthan Cooperative Dairy Federation Limited ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "'  as ToDate,Trans_Id,Punching_Date as Punching_Date ,Location_Code,[Loc Desp],Item_Type_Name,Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate,(case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1.00*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG  from

                                (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1.00 else -1.00 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end)) end as Rate,sum(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,sum( (case when IsFromMilk=1.00 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)) as Balance_SNF  from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', 
                                case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  

                                from TSPL_ITEM_QC_PARAMETER_MASTER  

                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range 

                                from TSPL_ITEM_QC_PARAMETER_MASTER 

                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))

                                from TSPL_ITEM_QC_PARAMETER_MASTER  
                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) 

                                from TSPL_ITEM_QC_PARAMETER_MASTER
                                left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                                ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 

                                 union all 

                                 select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor 
                                 from TSPL_INVENTORY_MOVEMENT_NEW
                                ) InventroyMovement 
                                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                                 left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                                 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                                 left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  
                                 from (
                                 select * from ( 
                                 select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                                 ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                                 from  TSPL_ITEM_MASTER  
                                 left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                                 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                                 where 2=2 
                                 )xx
                                 Pivot 
                                 ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                                 ) Pivt
                                 Pivot 
                                 (
                                 max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                                 ) Pivt1 
                                 ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  
                                 where 2=2  " + whr + " 
                                 and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = 'AJMR') )) xxx 
                                 where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  group by xxx.Item_Code 
                                 union all 
 
                                 select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1.00 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end) as Balance_SNF   from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                                ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                                 union all 
                                 select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                                ) InventroyMovement 
                                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                                 left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                                 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                                 left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                                 select * from ( 
                                 select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                                 ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                                 ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                                 from  TSPL_ITEM_MASTER  
                                 left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                                 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                                 where 2=2 
                                 )xx
                                 Pivot 
                                 ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                                 ) Pivt
                                 Pivot 
                                 (
                                 max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                                 ) Pivt1 
                                 ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  " + whr + " 
                                 and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') )) xxx 
                                 where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                                )xxxxxx  )xxxxxxx 
                                LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=xxxxxxx.Location_Code
                                where Trans_Id<>0  Order by  Punching_Date,Trans_Id "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "rptItemStockReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class