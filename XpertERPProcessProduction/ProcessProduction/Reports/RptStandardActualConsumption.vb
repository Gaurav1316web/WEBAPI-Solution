Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'' Work done agist ticket no. BHA/05/09/18-000510
Public Class rptStandardActualConsumption
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Dim dt As DataTable
        Dim counting As Integer = 0
        If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
            Exit Sub
        End If
        ''richa ERO/02/07/21/21-001426 ADDED COLUMN [FAT Pers], [SNF Pers], [TS Pers],[Issued FAT KG],[Issued SNF KG],[Issued TS KG], FROM ISSUE ENTRY SCREEN
        Dim Query As String = " select [Batch No],[Main Item],[Main Item Desc],UOM,case when RowNumber=1 then [Batch Main Qty] else 0 end [Batch Main Qty],case when RowNumber=1 then Main_item_cost else 0 end [Main item cost],[BOM No],Issue_Code as [Issue Code],case when RowNumber =1 then [BOM Qty] else 0 end [BOM Qty],[BOM UOM], [BOM Item],tspl_item_master.item_desc as [BOM Item Desc],[Standard Qty],[Item UOM], [Required Qty],[Req FAT Pers] , [Req SNF Pers], [Req TS Pers],[Req FAT KG], [Req SNF KG], [Req TS KG],[Issue Qty],[Issued Total Cost],[Issued FAT Pers] , [Issued SNF Pers], [Issued TS Pers],[Issued FAT KG] ,[Issued SNF KG] ,[Issued TS KG], [Consumed Qty], Con_Fat_Per as [Con Fat Per],Con_SNF_Per as [Con SNF Per],Con_TS_Per as [Con TS Per],Con_Fat_KG as [Con Fat KG] ,Con_SNF_KG as [Con SNF KG] ,Con_TS_KG as [Con TS KG] ,[Consumed Value],AddRemoveQty as [Remove Qty],Rem_FAT_Per as [Remove Fat Pers],Rem_SNF_Per as [Remove SNF Pers], Rem_TS_Per as [Remove TS Pers],Rem_FAT_KG as [Remove Fat KG],Rem_SNF_KG as [Remove SNF KG],Rem_TS_KG as [Remove TS KG],AddRemoveValue as [Remove value],
 AddQty as [Add Qty],AddValue as [Add Value],convert(decimal(18,2),Add_FAT_Pers) as [Add FAT Pers],convert(decimal(18,2),Add_SNF_Pers) as [Add SNF Pers],convert(decimal(18,2),Add_TS_Pers) as [Add_TS_Pers],Add_FAT_KG as [Add FAT KG],Add_SNF_KG as [Add SNF KG],Add_TS_KG as [Add TS KG],Location_Code,convert(decimal(18,3),[Issue Difference Qty]) as [Issue Difference Qty],convert(decimal(18,3),[Difference Qty]) as [Difference Qty] from 
 (
 select row_number() over (partition by [Batch No] order by [Batch No]) as RowNumber, * ,[Required Qty]-[Consumed Qty] as [Difference Qty],[Issue Qty]-[Consumed Qty] as [Issue Difference Qty] from ( 
 select distinct isnull(tspl_item_uom_detail.Item_Cost,0)*isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity,0) as Main_item_cost ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code as [Batch No],TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code as [Main Item],TSPL_ITEM_MASTER.Item_Desc as [Main Item Desc],TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code as UOM,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity  as [Batch Main Qty]
 ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code as [BOM No],TSPL_PP_BOM_HEAD.PROD_QUANTITY as [BOM Qty],TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code as [BOM UOM]
 ,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE as [BOM Item],TSPL_PP_BOM_ITEM_DETAIL.QUANTITY as [Standard Qty],TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE as[Item UOM]
 ,isnull([Required Qty],0) as [Required Qty], isnull(Required_FAT_KG,0) as [Req FAT KG],isnull(Required_SNF_KG,0) as [Req SNF KG],isnull(Required_TS_KG,0) as [Req TS KG],isnull(Req_FAT_Pers,0) as [Req FAT Pers] , isnull(Req_SNF_Pers,0) as [Req SNF Pers],isnull(Req_TS_Pers,0) as [Req TS Pers] , 
 isnull(Issue_Item.Issue_Qty,0) as [Issue Qty],isnull(Issue_Item.[Total Cost],0) as [Issued Total Cost],Issue_Item.Issue_Code,convert(decimal(18,2),isnull(Issue_Item.FAT_Pers,0)) as [Issued FAT Pers],convert(decimal(18,2),isnull(Issue_Item.SNF_Pers,0)) as [Issued SNF Pers],convert(decimal(18,2),isnull(Issue_Item.TS_Pers,0)) as [Issued TS Pers],isnull(Issue_Item.Issued_FAT_KG,0) as [Issued FAT KG] , isnull(Issue_Item.Issued_SNF_KG,0) as [Issued SNF KG],isnull(Issue_Item.Issued_TS_KG,0) as [Issued TS KG] ,isnull(CONSM_QTY,0) as [Consumed Qty],isnull([Consumed Value],0) as [Consumed Value], isnull(TSPL_PP_PRODUCTION_ENTRY.Con_Fat_Per,0 ) as Con_Fat_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Con_SNF_Per,0) as Con_SNF_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Con_TS_Per,0) as Con_TS_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Con_FAT_KG ,0) as Con_FAT_KG  ,isnull(TSPL_PP_PRODUCTION_ENTRY.Con_SNF_KG,0) as Con_SNF_KG ,isnull(TSPL_PP_PRODUCTION_ENTRY.Con_TS_KG ,0) as Con_TS_KG , isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_FAT_Per,0 ) as Rem_FAT_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_SNF_Per,0) as Rem_SNF_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_TS_Per,0) as Rem_TS_Per,isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_FAT_KG ,0) as Rem_FAT_KG  ,isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_SNF_KG,0) as Rem_SNF_KG ,isnull(TSPL_PP_PRODUCTION_ENTRY.Rem_TS_KG ,0) as Rem_TS_KG ,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_FAT_Pers,0 ) as Add_FAT_Pers,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_SNF_Pers,0) as Add_SNF_Pers,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_TS_Pers,0) as Add_TS_Pers,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_FAT_KG ,0) as Add_FAT_KG  ,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_SNF_KG,0) as Add_SNF_KG ,isnull(TSPL_PP_PRODUCTION_ENTRY.Add_TS_KG ,0) as Add_TS_KG , isnull(TSPL_PP_PRODUCTION_ENTRY.AddQty,0) as AddQty,isnull(TSPL_PP_PRODUCTION_ENTRY.AddValue,0) as AddValue,isnull(TSPL_PP_PRODUCTION_ENTRY.AddRemoveQty,0) as AddRemoveQty,isnull(TSPL_PP_PRODUCTION_ENTRY.AddRemoveValue,0) as AddRemoveValue,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,BatchRawItem.Location_Code 
 from 
 (
 select CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value],0 as AddRemoveQty,0 as AddRemoveValue , 0 as AddQty,0 as AddValue, 0 as Con_FAT_KG,0 as Con_SNF_KG,0 as Con_TS_KG,0 as Con_Fat_Per,0 as Con_SNF_Per,0 as Con_TS_Per, 0 as Rem_FAT_KG,0 as Rem_SNF_KG,0 as Rem_TS_KG,0 as Rem_FAT_Per,0 as Rem_SNF_Per,0 as Rem_TS_Per ,0 as Add_FAT_KG,0 as Add_SNF_KG,0 as Add_TS_KG,0 as Add_FAT_Pers,0 as Add_SNF_Pers,0 as Add_TS_Pers
 from TSPL_PP_PRODUCTION_ENTRY
left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE 
inner join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE 

Union
select sum(CONSM_QTY * case when RI=1 then 1 else 0 end) as CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,sum([Consumed Value]* case when RI=1 then 1 else 0 end)as [Consumed Value], sum(CONSM_QTY * case when RI=2 then 1 else 0 end) as AddRemoveQty,sum([Consumed Value]* case when RI=2 then 1 else 0 end) as AddRemoveValue,
sum(CONSM_QTY * case when RI=1 and Type='Add' then 1 else 0 end) as AddQty,sum([Consumed Value]* case when RI=1 and Type='Add' then 1 else 0 end) as AddValue,
sum(Con_FAT_KG * case when RI=1 then 1 else 0 end) as Con_FAT_KG,sum(Con_SNF_KG * case when RI=1 then 1 else 0 end) as Con_SNF_KG,sum(Con_TS_KG * case when RI=1 then 1 else 0 end) as Con_TS_KG,case when sum(CONSM_QTY * case when RI=1 then 1 else 0 end)<>0 then cast ((sum(Con_FAT_KG * case when RI=1 then 1 else 0 end) *100)/sum(CONSM_QTY * case when RI=1 then 1 else 0 end) as decimal(18,3)) else 0 end as Con_Fat_Per,case when sum(CONSM_QTY * case when RI=1 then 1 else 0 end)<>0 then cast ((sum(Con_SNF_KG * case when RI=1 then 1 else 0 end) *100)/sum(CONSM_QTY * case when RI=1 then 1 else 0 end) as decimal(18,3)) else 0 end as Con_SNF_Per,
case when sum(CONSM_QTY * case when RI=1 then 1 else 0 end)<>0 then cast (((sum(Con_FAT_KG * case when RI=1 then 1 else 0 end)+sum(Con_SNF_KG * case when RI=1 then 1 else 0 end)) *100)/sum(CONSM_QTY * case when RI=1 then 1 else 0 end) as decimal(18,3)) else 0 end as Con_TS_Per,
 sum(Con_FAT_KG * case when RI=2 then 1 else 0 end)  as Rem_FAT_KG,sum(Con_SNF_KG * case when RI=2 then 1 else 0 end) as Rem_SNF_KG,sum(Con_TS_KG * case when RI=2 then 1 else 0 end) as Rem_TS_KG,sum(Con_FAT_Pers * case when RI=2 then 1 else 0 end) as Rem_FAT_Per,sum(Con_SNF_Pers * case when RI=2 then 1 else 0 end) as Rem_SNF_Per,sum(Con_TS_Pers * case when RI=2 then 1 else 0 end) as Rem_TS_Per,
 sum(Add_Fat_KG * case when RI=1 then 1 else 0 end)  as Add_Fat_KG,sum(Add_SNF_KG * case when RI=1 then 1 else 0 end) as Add_SNF_KG,sum(Add_TS_KG * case when RI=1 then 1 else 0 end) as Add_TS_KG,sum(Add_FAT_Pers * case when RI=1 then 1 else 0 end) as Add_FAT_Per,sum(Add_SNF_Pers * case when RI=1 then 1 else 0 end) as Add_SNF_Per,sum(Add_TS_Pers * case when RI=1 then 1 else 0 end) as Add_TS_Per
 from 
(
select CONSM_QTY,Main_Batch_Code as Batch_Code, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value],1 as RI ,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per,0) as Con_FAT_Pers ,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per,0) as Con_SNF_Pers, isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_Per,0)+isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Per,0) AS Con_TS_Pers,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG,0) as Con_FAT_KG,ISNULL( TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG,0) as Con_SNF_KG,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.FAT_KG,0) +ISNULL( TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG,0) as Con_TS_KG ,0 as Add_FAT_Pers,0 as Add_SNF_Pers,0 as Add_TS_Pers,0 as Add_FAT_KG,0 as Add_SNF_KG,0 as Add_TS_KG,'' as Type
from TSPL_PP_STANDARDIZATION_HEAD    
inner join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code =TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code
--where Main_Batch_Code='BOR-001/20-21/03950'
union all
select ((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY as decimal(18,2))) as CONSM_QTY,Main_Batch_Code as Batch_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.item_code as CONSM_ITEM_CODE,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*(cast( TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.fat_amt+TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.snf_amt as decimal(18,2)))) as [Consumed Value],2 as RI,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_Per,0) as Con_FAT_Pers ,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_Per,0) as Con_SNF_Pers, isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_Per,0)+isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_Per,0) AS Con_TS_Pers,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as decimal(18,2)))
as Con_FAT_KG,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as decimal(18,2)))
 as Con_SNF_KG,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as decimal(18,2)))
 +((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as decimal(18,2))) as Con_TS_KG ,
 0 as Add_FAT_Pers,0 as Add_SNF_Pers,0 as Add_TS_Pers,0 as Add_FAT_KG,0 as Add_SNF_KG,0 as Add_TS_KG,ADD_REMOVE_TYPE as Type
from TSPL_PP_STANDARDIZATION_HEAD    
left outer join TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code=TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  
where TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove'
 --and Main_Batch_Code='BOR-001/20-21/03950'
  UNION ALL
select ((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY as decimal(18,2))) as CONSM_QTY,Main_Batch_Code as Batch_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.item_code as CONSM_ITEM_CODE,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*(cast( TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.fat_amt+TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.snf_amt as decimal(18,2)))) as [Consumed Value],1 as RI,0 as Con_FAT_Pers,0 as Con_SNF_Pers,0 as Con_TS_Pers,0 as Con_FAT_KG,0 as Con_SNF_KG,0 as Con_TS_KG,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_Per,0) as Add_FAT_Pers ,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_Per,0) as Add_SNF_Pers, isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_Per,0)+isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_Per,0) AS Add_TS_Pers,
((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as decimal(18,2)))
as Add_FAT_KG,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as decimal(18,2)))
 as Add_SNF_KG,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG as decimal(18,2)))
 +((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG as decimal(18,2))) as Add_TS_KG ,ADD_REMOVE_TYPE as Type
from TSPL_PP_STANDARDIZATION_HEAD    
left outer join TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code=TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  
where TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' --and Main_Batch_Code='BOR-001/20-21/03950'
)x group by Batch_Code,CONSM_ITEM_CODE
)
TSPL_PP_PRODUCTION_ENTRY 
left outer join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_PRODUCTION_ENTRY.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code
 left outer join TSPL_PP_PRODUCTION_PLAN_HEAD on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code
 left outer join TSPL_PP_PRODUCTION_PLAN_DETAIL on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code
  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code and  TSPL_PP_PRODUCTION_ENTRY.CONSM_ITEM_CODE=TSPL_PP_BOM_ITEM_DETAIL.Item_Code
 left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE	 
 left outer join 
 (select Item_Code as RequiredItem,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,0) as [Required Qty],TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as [BatchCode],TSPL_PP_BATCH_ORDER_HEAD.Location_Code, 
 isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) as Required_FAT_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_SNF_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) + isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_TS_KG , 
 isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT,0) as Req_FAT_Pers ,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF,0) as Req_SNF_Pers, isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT,0)+isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF,0) AS Req_TS_Pers 
 from TSPL_PP_BATCH_ORDER_HEAD
 left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code)BatchRawItem 
 on BatchRawItem.BatchCode=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code And BatchRawItem.RequiredItem=TSPL_PP_BOM_ITEM_DETAIL.Item_Code " + Environment.NewLine +
        " left join (   select Issue_Code,sum(Issue_Qty) as Issue_Qty,Batch_Code,Item_Code,sum([Total Cost]) as [Total Cost],sum(FAT_Pers) as FAT_Pers,sum(SNF_Pers) as SNF_Pers,sum(TS_Pers) as TS_Pers,sum(Issued_FAT_KG) as Issued_FAT_KG,sum(Issued_SNF_KG) as Issued_SNF_KG ,sum(Issued_TS_KG) as  Issued_TS_KG from (" + Environment.NewLine +
 "select TSPL_PP_ISSUE_HEAD.Issue_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Qty,0) as Issue_Qty,TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Fat_Amt,0) +isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt,0)  as [Total Cost],  " + Environment.NewLine +
 " isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers,0) as FAT_Pers ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers,0) as SNF_Pers, isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers,0)+isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers,0) AS TS_Pers,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) as Issued_FAT_KG,ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_SNF_KG,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) +ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_TS_KG " + Environment.NewLine +
 "from    TSPL_PP_ISSUE_HEAD " + Environment.NewLine +
 "left join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code =TSPL_PP_ISSUE_HEAD.Issue_Code " + Environment.NewLine +
 ")x group by Issue_Code,Batch_Code,item_code" + Environment.NewLine +
        " ) as Issue_Item on" + Environment.NewLine +
        " Issue_Item.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE =Issue_Item.Item_Code " + Environment.NewLine +
        " left join tspl_item_master on tspl_item_master.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " + Environment.NewLine +
        " left join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code and tspl_item_uom_detail.Uom_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code " + Environment.NewLine +
        " )as xxx" + Environment.NewLine +
         " where 2=2 " + Environment.NewLine +
         "  and [Batch No] is not null and convert(date,xxx.Plan_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,xxx.Plan_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            Query += " and  xxx.[Bom item] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            Query += " and xxx.[Location_Code] in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If
        If txtSubLocation.arrValueMember IsNot Nothing AndAlso txtSubLocation.arrValueMember.Count > 0 Then
            Query += " and  xxx.[Batch No] in (" + clsCommon.GetMulcallString(txtSubLocation.arrValueMember) + ")  "
        End If
        Query += " ) as final left outer join tspl_item_master on tspl_item_master.item_code=final.[BOM Item] order by [Batch No]"
        dt = clsDBFuncationality.GetDataTable(Query)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2
            EnableDisableControls(False)
            gv1.DataSource = dt
            SetGridFormationOFGV1()
            gv1.BestFitColumns()
            ReStoreGridLayout()
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub

    '    Sub Print(ByVal IsPrint As Exporter)
    '        Dim dt As DataTable
    '        Dim counting As Integer = 0
    '        If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
    '            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
    '            Exit Sub
    '        End If
    '        ''richa ERO/02/07/21/21-001426 ADDED COLUMN [FAT Pers], [SNF Pers], [TS Pers],[Issued FAT KG],[Issued SNF KG],[Issued TS KG], FROM ISSUE ENTRY SCREEN
    '        Dim Query As String = " select [Batch No],[Main Item],[Main Item Desc],UOM,case when RowNumber=1 then [Batch Main Qty] else 0 end [Batch Main Qty],case when RowNumber=1 then Main_item_cost else 0 end [Main item cost],[BOM No],Issue_Code as [Issue Code],case when RowNumber =1 then [BOM Qty] else 0 end [BOM Qty],[BOM UOM], [BOM Item],tspl_item_master.item_desc as [BOM Item Desc],[Standard Qty],[Item UOM], [Required Qty],[Req FAT Pers] , [Req SNF Pers], [Req TS Pers],[Req FAT KG], [Req SNF KG], [Req TS KG],[Issue Qty],[Issued Total Cost],[Issued FAT Pers] , [Issued SNF Pers], [Issued TS Pers],[Issued FAT KG] ,[Issued SNF KG] ,[Issued TS KG], [Consumed Qty],[Consumed Value],AddRemoveQty,AddRemoveValue,Location_Code,convert(decimal(18,3),[Issue Difference Qty]) as [Issue Difference Qty],convert(decimal(18,3),[Difference Qty]) as [Difference Qty] from (" + Environment.NewLine +
    '         " select row_number() over (partition by [Batch No] order by [Batch No]) as RowNumber, *,[Required Qty]-[Consumed Qty] as [Difference Qty],[Issue Qty]-[Consumed Qty] as [Issue Difference Qty] from (select distinct isnull(tspl_item_uom_detail.Item_Cost,0)*isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity,0) as Main_item_cost ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code as [Batch No],TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code as [Main Item],TSPL_ITEM_MASTER.Item_Desc as [Main Item Desc],TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code as UOM,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity  as [Batch Main Qty]" + Environment.NewLine +
    '         " ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code as [BOM No],TSPL_PP_BOM_HEAD.PROD_QUANTITY as [BOM Qty],TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code as [BOM UOM]" + Environment.NewLine +
    '         " ,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE as [BOM Item],TSPL_PP_BOM_ITEM_DETAIL.QUANTITY as [Standard Qty],TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE as[Item UOM]" + Environment.NewLine +
    '         " ,isnull([Required Qty],0) as [Required Qty], isnull(Required_FAT_KG,0) as [Req FAT KG],isnull(Required_SNF_KG,0) as [Req SNF KG],isnull(Required_TS_KG,0) as [Req TS KG],isnull(Req_FAT_Pers,0) as [Req FAT Pers] , isnull(Req_SNF_Pers,0) as [Req SNF Pers],isnull(Req_TS_Pers,0) as [Req TS Pers] , " + Environment.NewLine +
    '        " isnull(Issue_Item.Issue_Qty,0) as [Issue Qty],isnull(Issue_Item.[Total Cost],0) as [Issued Total Cost],Issue_Item.Issue_Code,isnull(Issue_Item.FAT_Pers,0) as [Issued FAT Pers],isnull(Issue_Item.SNF_Pers,0) as [Issued SNF Pers],isnull(Issue_Item.TS_Pers,0) as [Issued TS Pers],isnull(Issue_Item.Issued_FAT_KG,0) as [Issued FAT KG] , isnull(Issue_Item.Issued_SNF_KG,0) as [Issued SNF KG],isnull(Issue_Item.Issued_TS_KG,0) as [Issued TS KG] " + Environment.NewLine +
    '         " ,isnull(CONSM_QTY,0) as [Consumed Qty]" + Environment.NewLine +
    '         " ,isnull([Consumed Value],0) as [Consumed Value],isnull(TSPL_PP_PRODUCTION_ENTRY.AddRemoveQty,0) as AddRemoveQty,isnull(TSPL_PP_PRODUCTION_ENTRY.AddRemoveValue,0) as AddRemoveValue,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,BatchRawItem.Location_Code " + Environment.NewLine +
    '         " from TSPL_PP_PRODUCTION_PLAN_HEAD" + Environment.NewLine +
    '         " left outer join TSPL_PP_PRODUCTION_PLAN_DETAIL on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code" + Environment.NewLine +
    '         " left outer join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code" + Environment.NewLine +
    '         " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code" + Environment.NewLine +
    '         " left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE	 " + Environment.NewLine +
    '         " left outer join (select Item_Code as RequiredItem,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,0) as [Required Qty],TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as [BatchCode],TSPL_PP_BATCH_ORDER_HEAD.Location_Code, " + Environment.NewLine +
    '        " isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) as Required_FAT_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_SNF_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) + isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_TS_KG , " + Environment.NewLine +
    '        " isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT,0) as Req_FAT_Pers ,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF,0) as Req_SNF_Pers, isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT,0)+isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF,0) AS Req_TS_Pers " + Environment.NewLine +
    '        " from TSPL_PP_BATCH_ORDER_HEAD" + Environment.NewLine +
    '         " left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code)BatchRawItem on BatchRawItem.BatchCode=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code And BatchRawItem.RequiredItem=TSPL_PP_BOM_ITEM_DETAIL.Item_Code" + Environment.NewLine +
    '         " left outer join (select CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value],0 as AddRemoveQty,0 as AddRemoveValue " + Environment.NewLine +
    '        "from TSPL_PP_PRODUCTION_ENTRY" + Environment.NewLine +
    '"left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " + Environment.NewLine +
    '"inner join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " + Environment.NewLine +
    '        "Union" + Environment.NewLine +
    '"select sum(CONSM_QTY * case when RI=1 then 1 else 0 end) as CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,sum([Consumed Value]* case when RI=1 then 1 else 0 end)as [Consumed Value], sum(CONSM_QTY * case when RI=2 then 1 else 0 end) as AddRemoveQty,sum([Consumed Value]* case when RI=2 then 1 else 0 end) as AddRemoveValue from (" + Environment.NewLine +
    '"select CONSM_QTY,Main_Batch_Code as Batch_Code, TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value],1 as RI " + Environment.NewLine +
    '"from TSPL_PP_STANDARDIZATION_HEAD    " + Environment.NewLine +
    '"inner join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Standardization_Code =TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code" + Environment.NewLine +
    '"union all" + Environment.NewLine +
    '"select ((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*cast(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY as decimal(18,2))) as CONSM_QTY,Main_Batch_Code as Batch_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.item_code as CONSM_ITEM_CODE,((case when ADD_REMOVE_TYPE='Remove' then -1 else 1 end)*(cast( TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.fat_amt+TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.snf_amt as decimal(18,2)))) as [Consumed Value],2 as RI" + Environment.NewLine +
    '"from TSPL_PP_STANDARDIZATION_HEAD    " + Environment.NewLine +
    '"left outer join TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code=TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  " + Environment.NewLine +
    '"where TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove' )x group by Batch_Code,CONSM_ITEM_CODE)TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code" + Environment.NewLine +
    '         " and TSPL_PP_PRODUCTION_ENTRY.CONSM_ITEM_CODE=TSPL_PP_BOM_ITEM_DETAIL.Item_Code" + Environment.NewLine +
    '        " left join (   select Issue_Code,sum(Issue_Qty) as Issue_Qty,Batch_Code,Item_Code,sum([Total Cost]) as [Total Cost],sum(FAT_Pers) as FAT_Pers,sum(SNF_Pers) as SNF_Pers,sum(TS_Pers) as TS_Pers,sum(Issued_FAT_KG) as Issued_FAT_KG,sum(Issued_SNF_KG) as Issued_SNF_KG ,sum(Issued_TS_KG) as  Issued_TS_KG from (" + Environment.NewLine +
    ' "select TSPL_PP_ISSUE_HEAD.Issue_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Qty,0) as Issue_Qty,TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Fat_Amt,0) +isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt,0)  as [Total Cost],  " + Environment.NewLine +
    ' " isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers,0) as FAT_Pers ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers,0) as SNF_Pers, isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers,0)+isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers,0) AS TS_Pers,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) as Issued_FAT_KG,ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_SNF_KG,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) +ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_TS_KG " + Environment.NewLine +
    ' "from    TSPL_PP_ISSUE_HEAD " + Environment.NewLine +
    ' "left join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code =TSPL_PP_ISSUE_HEAD.Issue_Code " + Environment.NewLine +
    ' ")x group by Issue_Code,Batch_Code,item_code" + Environment.NewLine +
    '        " ) as Issue_Item on" + Environment.NewLine +
    '        " Issue_Item.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE =Issue_Item.Item_Code " + Environment.NewLine +
    '        " left join tspl_item_master on tspl_item_master.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " + Environment.NewLine +
    '        " left join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code and tspl_item_uom_detail.Uom_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code " + Environment.NewLine +
    '        " )as xxx" + Environment.NewLine +
    '         " where 2=2 " + Environment.NewLine +
    '         "  and [Batch No] is not null and convert(date,xxx.Plan_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,xxx.Plan_Date ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

    '        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
    '            Query += " and  xxx.[Bom item] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
    '            End If

    '        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
    '            Query += " and xxx.[Location_Code] in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
    '            End If
    '        If txtSubLocation.arrValueMember IsNot Nothing AndAlso txtSubLocation.arrValueMember.Count > 0 Then
    '            Query += " and  xxx.[Batch No] in (" + clsCommon.GetMulcallString(txtSubLocation.arrValueMember) + ")  "
    '            End If
    '        Query += " ) as final left outer join tspl_item_master on tspl_item_master.item_code=final.[BOM Item] order by [Batch No]"
    '        dt = clsDBFuncationality.GetDataTable(Query)

    '        If dt IsNot Nothing And dt.Rows.Count > 0 Then
    '            gv1.DataSource = Nothing
    '            gv1.Columns.Clear()
    '            gv1.Rows.Clear()
    '            gv1.GroupDescriptors.Clear()
    '            gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '            gv1.ShowGroupPanel = False
    '            gv1.EnableFiltering = True
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            EnableDisableControls(False)
    '            gv1.DataSource = dt
    '            SetGridFormationOFGV1()
    '            gv1.Columns("AddRemoveQty").HeaderText = "Remove Qty"
    '            gv1.Columns("AddRemoveValue").HeaderText = "Remove Value"
    '            gv1.BestFitColumns()
    '            ReStoreGridLayout()
    '        Else
    '            clsCommon.MyMessageBoxShow("No Data Found")
    '        End If
    '    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

        gv1.Columns("Location_Code").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item7 As New GridViewSummaryItem("Consumed Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issued Total Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Main item cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issued FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issued SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issued TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Standard Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Required Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issue Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Consumed Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Remove Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Remove Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Remove Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Remove SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Remove TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Add Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Add Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Add Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Add SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Add TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Con Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Con SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Con TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Issue Difference Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Difference Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Batch Main Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("BOM Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Req FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Req SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Req TS KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)


        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
      
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControls(True)

    End Sub
    Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        TxtMultiLocation.Enabled = val
        txtItemMult.Enabled = val
        txtSubLocation.Enabled = val
    End Sub

    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub
    Private Sub txtSubLocation__My_Click(sender As Object, e As EventArgs) Handles txtSubLocation._My_Click
        Dim qry As String = "select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Description as Name from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL"
        qry += " left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code= TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code  where 2=2 "
        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If
        txtSubLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ProSub", qry, "Code", "Name", txtSubLocation.arrValueMember, txtSubLocation.arrDispalyMember)
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptAvailableQtyForProduction_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptAvailableQtyForProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtItemMult.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        txtSubLocation.arrValueMember = Nothing

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
    End Sub

   
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Standard Vs Actual Consumption Report", gv1, arrHeader, Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Standard Vs Actual Consumption Report", gv1, arrHeader, "Standard Vs Actual Consumption Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
