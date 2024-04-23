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
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'TxtMultiLocation.arrValueMember = Nothing
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        TxtItem.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        LoadLocation()
        LoadUnit()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Item_Stock_Report()
    End Sub

    Private Sub Load_Item_Stock_Report()

        Dim qry As String = " "
        Dim Item_Type As String = Nothing
        Dim dt As New DataTable()
        Try
            Dim whr As String = " "
            If TxtItem.arrValueMember IsNot Nothing AndAlso TxtItem.arrValueMember.Count > 0 Then
                whr += " and Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")  "
            End If

            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                whr += " and Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")  "
                Item_Type = " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")  "
            End If


            qry = "select * from (select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,xxxxxxx.Location_Code,[Loc Desp],Add1,Add4,convert(varchar, Punching_Date,103) as Punching_Date  ,Structure_Desc,Item_Type_Name,RACK_NO,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty,
                    case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate ,case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost, RecPurQty,RecPurRate,RecPurCost ,RecProQty, RecProRate ,RecProCost,RecAdjQty,RecAdjRate ,RecAdjCost ,RecOthQty,RecOthRate ,RecOthCost,RecQty,RecRate,RecCost  ,IssTransferQty ,IssTransferRate ,IssTransferCost ,IssSaleQty ,IssSaleRate  ,IssSaleCost , IssIssAdjQty , IssIssAdjRate ,IssIssAdjCost , IssOthQty , IssOthRate ,IssOthCost ,IssQty,IssRate,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty,
                    case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 ) then 0 else CLCost/CLQty end as CLRate, CLCost 
                     
                    from ( select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date,max(Structure_Desc)Structure_Desc,max(Item_Type_Name)Item_Type_Name,max(RACK_NO)RACK_NO, Item_Code ,max(Item_Desc) as Item_Desc, (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost
                    ,sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount 
                    ,sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount 
                      ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,sum(case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty  ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost 
                    ,sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount
                    ,sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount
                     ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF 
                    ,SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount 
                    ,SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount 
                      from (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,max(Item_Type_Name) as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF 
                    ,( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount
                    ,(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount 
                     ,   '' as In_Category,'' as Out_Category,max(Structure_Desc)Structure_Desc,max(RACK_NO)RACK_NO from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
            If clsCommon.CompairString(cmbUnit.Text, "Select") = CompairStringResult.Equal Then
                qry += "isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
            Else
                qry += "isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,'" + clsCommon.myCstr(cmbUnit.Text) + "' as Stock_UOM,cast((InventroyMovement.Stock_Qty /  (case when InventroyMovement.Custom_UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code and InventroyMovement.Custom_Coversion_Factor>0 then  InventroyMovement.Custom_Coversion_Factor else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)) as decimal(18,10)) as Stock_Qty,"
            End If
            qry += "isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))
                    from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                   ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation, Structure_Desc,TSPL_ITEM_MASTER.RACK_NO  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type 
                     left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No  "
            If clsCommon.CompairString(cmbUnit.Text, "Select") <> CompairStringResult.Equal Then
                qry += "  inner Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=(select Unit_Code  from TSPL_UNIT_MASTER where Unit_Desc= '" + clsCommon.myCstr(cmbUnit.Text) + "') "
            End If
            qry += "  left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
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
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  " + whr + "  "

            ' qry += " and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') ) "

            qry += " ) xxx "
            qry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code 
                     union all  select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,  convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost, ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,  ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF
                    ,(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount
                    ,(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount
                     ,In_Category,Out_Category,Structure_Desc,RACK_NO   from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
            If clsCommon.CompairString(cmbUnit.Text, "Select") = CompairStringResult.Equal Then
                qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, "
            Else
                qry += "isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,'" + clsCommon.myCstr(cmbUnit.Text) + "' as Stock_UOM,cast((InventroyMovement.Stock_Qty /  (case when InventroyMovement.Custom_UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code and InventroyMovement.Custom_Coversion_Factor>0 then  InventroyMovement.Custom_Coversion_Factor else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)) as decimal(18,10)) as Stock_Qty, "
            End If
            qry += "isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
               
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation,Structure_Desc,TSPL_ITEM_MASTER.RACK_NO  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No"
            If clsCommon.CompairString(cmbUnit.Text, "Select") <> CompairStringResult.Equal Then
                qry += "  inner Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=(select Unit_Code  from TSPL_UNIT_MASTER where Unit_Desc= '" + clsCommon.myCstr(cmbUnit.Text) + "') "
            End If
            qry += " left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
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
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  " + whr + "  "

            ' qry += " and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') )"

            qry += ") xxx 
                     where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     union  all select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],Items.Item_Code,Item_Desc, Item_Category_Struct_Code,  Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF 
                    ,0 as FATAmount
                    ,0 as SNFAmount
                     ,In_Category,Out_Category,Structure_Desc,RACK_NO  from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type,Structure_Desc,TSPL_ITEM_MASTER.RACK_NO   FROM ExplodeDates( '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2 " + Item_Type + "  and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ") " '  and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') ) "
            qry += " and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code  left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2  )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x "
            If clsCommon.CompairString(cmbUnit.Text, "Select") <> CompairStringResult.Equal Then
                qry += " where Stock_UOM='" + clsCommon.myCstr(cmbUnit.Text) + "'"
            End If
            If clsCommon.CompairString(cmbUnit.Text, "Select") <> CompairStringResult.Equal AndAlso clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += " and Location_Code='" + txtBillToLocation.Value + "' "
            ElseIf clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += " where Location_Code='" + txtBillToLocation.Value + "' "
            End If
            qry += " Order by  convert(date,  Punching_Date,103),Location_Code"

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

        Gv1.Columns("Structure_Desc").Width = 100
        Gv1.Columns("Structure_Desc").IsVisible = True
        Gv1.Columns("Structure_Desc").HeaderText = "Structure Name"
        Gv1.Columns("Structure_Desc").FormatString = "{0:n2}"

        Gv1.Columns("Item_Type_Name").Width = 100
        Gv1.Columns("Item_Type_Name").IsVisible = True
        Gv1.Columns("Item_Type_Name").HeaderText = "Category"
        Gv1.Columns("Item_Type_Name").FormatString = "{0:n2}"

        Gv1.Columns("RACK_NO").Width = 100
        Gv1.Columns("RACK_NO").IsVisible = True
        Gv1.Columns("RACK_NO").HeaderText = "Rack No"
        Gv1.Columns("RACK_NO").FormatString = "{0:n2}"

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

        Gv1.Columns("RecPurQty").Width = 100
        Gv1.Columns("RecPurQty").IsVisible = True
        Gv1.Columns("RecPurQty").HeaderText = "Received Purchased Quantity"
        Gv1.Columns("RecPurQty").FormatString = "{0:n2}"

        Gv1.Columns("RecPurRate").Width = 100
        Gv1.Columns("RecPurRate").IsVisible = True
        Gv1.Columns("RecPurRate").HeaderText = "Received Purchased Rate"
        Gv1.Columns("RecPurRate").FormatString = "{0:n2}"

        Gv1.Columns("RecPurCost").Width = 100
        Gv1.Columns("RecPurCost").IsVisible = True
        Gv1.Columns("RecPurCost").HeaderText = "Received Purchased Cost"
        Gv1.Columns("RecPurCost").FormatString = "{0:n2}"

        Gv1.Columns("RecProQty").Width = 100
        Gv1.Columns("RecProQty").IsVisible = True
        Gv1.Columns("RecProQty").HeaderText = "Received Production Quantity"
        Gv1.Columns("RecProQty").FormatString = "{0:n2}"

        Gv1.Columns("RecProRate").Width = 100
        Gv1.Columns("RecProRate").IsVisible = True
        Gv1.Columns("RecProRate").HeaderText = "Received Production Rate"
        Gv1.Columns("RecProRate").FormatString = "{0:n2}"

        Gv1.Columns("RecProCost").Width = 100
        Gv1.Columns("RecProCost").IsVisible = True
        Gv1.Columns("RecProCost").HeaderText = "Received Production Cost"
        Gv1.Columns("RecProCost").FormatString = "{0:n2}"

        Gv1.Columns("RecAdjQty").Width = 100
        Gv1.Columns("RecAdjQty").IsVisible = True
        Gv1.Columns("RecAdjQty").HeaderText = "Received Adjust Quantity"
        Gv1.Columns("RecAdjQty").FormatString = "{0:n2}"

        Gv1.Columns("RecAdjRate").Width = 100
        Gv1.Columns("RecAdjRate").IsVisible = True
        Gv1.Columns("RecAdjRate").HeaderText = "Received Adjust Rate"
        Gv1.Columns("RecAdjRate").FormatString = "{0:n2}"

        Gv1.Columns("RecAdjCost").Width = 100
        Gv1.Columns("RecAdjCost").IsVisible = True
        Gv1.Columns("RecAdjCost").HeaderText = "Received Adjust Cost"
        Gv1.Columns("RecAdjCost").FormatString = "{0:n2}"

        Gv1.Columns("RecOthQty").Width = 100
        Gv1.Columns("RecOthQty").IsVisible = True
        Gv1.Columns("RecOthQty").HeaderText = "Received Other Quantity"
        Gv1.Columns("RecOthQty").FormatString = "{0:n2}"

        Gv1.Columns("RecOthRate").Width = 100
        Gv1.Columns("RecOthRate").IsVisible = True
        Gv1.Columns("RecOthRate").HeaderText = "Received Other Rate"
        Gv1.Columns("RecOthRate").FormatString = "{0:n2}"

        Gv1.Columns("RecOthCost").Width = 100
        Gv1.Columns("RecOthCost").IsVisible = True
        Gv1.Columns("RecOthCost").HeaderText = "Received Other Cost"
        Gv1.Columns("RecOthCost").FormatString = "{0:n2}"

        Gv1.Columns("IssTransferQty").Width = 100
        Gv1.Columns("IssTransferQty").IsVisible = True
        Gv1.Columns("IssTransferQty").HeaderText = "Issued Transfer Quantity"
        Gv1.Columns("IssTransferQty").FormatString = "{0:n2}"

        Gv1.Columns("IssTransferRate").Width = 100
        Gv1.Columns("IssTransferRate").IsVisible = True
        Gv1.Columns("IssTransferRate").HeaderText = "Issued Transfer Rate"
        Gv1.Columns("IssTransferRate").FormatString = "{0:n2}"

        Gv1.Columns("IssTransferCost").Width = 100
        Gv1.Columns("IssTransferCost").IsVisible = True
        Gv1.Columns("IssTransferCost").HeaderText = "Issued Transfer Cost"
        Gv1.Columns("IssTransferCost").FormatString = "{0:n2}"

        Gv1.Columns("IssSaleQty").Width = 100
        Gv1.Columns("IssSaleQty").IsVisible = True
        Gv1.Columns("IssSaleQty").HeaderText = "Issued Sale Quantity"
        Gv1.Columns("IssSaleQty").FormatString = "{0:n2}"

        Gv1.Columns("IssSaleRate").Width = 100
        Gv1.Columns("IssSaleRate").IsVisible = True
        Gv1.Columns("IssSaleRate").HeaderText = "Issued Sale Rate"
        Gv1.Columns("IssSaleRate").FormatString = "{0:n2}"

        Gv1.Columns("IssSaleCost").Width = 100
        Gv1.Columns("IssSaleCost").IsVisible = True
        Gv1.Columns("IssSaleCost").HeaderText = "Issued Sale Cost"
        Gv1.Columns("IssSaleCost").FormatString = "{0:n2}"

        Gv1.Columns("IssIssAdjQty").Width = 100
        Gv1.Columns("IssIssAdjQty").IsVisible = True
        Gv1.Columns("IssIssAdjQty").HeaderText = "Issued Adjust Quantity"
        Gv1.Columns("IssIssAdjQty").FormatString = "{0:n2}"

        Gv1.Columns("IssIssAdjRate").Width = 100
        Gv1.Columns("IssIssAdjRate").IsVisible = True
        Gv1.Columns("IssIssAdjRate").HeaderText = "Issued Adjust Rate"
        Gv1.Columns("IssIssAdjRate").FormatString = "{0:n2}"

        Gv1.Columns("IssIssAdjCost").Width = 100
        Gv1.Columns("IssIssAdjCost").IsVisible = True
        Gv1.Columns("IssIssAdjCost").HeaderText = "Issued Adjust Cost"
        Gv1.Columns("IssIssAdjCost").FormatString = "{0:n2}"

        Gv1.Columns("IssOthQty").Width = 100
        Gv1.Columns("IssOthQty").IsVisible = True
        Gv1.Columns("IssOthQty").HeaderText = "Issued Other Quantity"
        Gv1.Columns("IssOthQty").FormatString = "{0:n2}"

        Gv1.Columns("IssOthRate").Width = 100
        Gv1.Columns("IssOthRate").IsVisible = True
        Gv1.Columns("IssOthRate").HeaderText = "Issued Other Rate"
        Gv1.Columns("IssOthRate").FormatString = "{0:n2}"

        Gv1.Columns("IssOthCost").Width = 100
        Gv1.Columns("IssOthCost").IsVisible = True
        Gv1.Columns("IssOthCost").HeaderText = "Issued Other Cost"
        Gv1.Columns("IssOthCost").FormatString = "{0:n2}"

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

        'Dim item01 As New GridViewSummaryItem("RecPurQty", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item01)

        'Dim item02 As New GridViewSummaryItem("RecPurRate", "{0:n2}", GridAggregateFunction.Sum), summaryRowItem.Add(item02)

        'Dim item03 As New GridViewSummaryItem("RecPurCost", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item03)

        'Dim item04 As New GridViewSummaryItem("RecProQty", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item04)

        'Dim item05 As New GridViewSummaryItem("RecProRate", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item05)

        'Dim item06 As New GridViewSummaryItem("RecProCost", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item06)

        'Dim item07 As New GridViewSummaryItem("RecAdjQty", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item07)

        'Dim item08 As New GridViewSummaryItem("RecAdjRate", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item08)

        'Dim item09 As New GridViewSummaryItem("RecAdjCost", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item09)

        'Dim item11 As New GridViewSummaryItem("RecOthQty", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)

        'Dim item12 As New GridViewSummaryItem("RecOthRate", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item12)

        'Dim item13 As New GridViewSummaryItem("RecOthCost", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item13)

        'Dim item14 As New GridViewSummaryItem("IssTransferQty", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item14)
        'RecQty, RecRate, RecCost, IssTransferQty, IssTransferRate, IssTransferCost, IssSaleQty, IssSaleRate, IssSaleCost, IssIssAdjQty, IssIssAdjRate, IssIssAdjCost, IssOthQty, IssOthRate, IssOthCost, IssQty, IssRate, IssCost


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
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
    End Sub

    Sub LoadLocation()
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Default_Location From TSPL_USER_MASTER Where User_Code= '" + objCommonVar.CurrentUserCode + "'"))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
    End Sub
    Sub LoadUnit()
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Description") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbUnit.DataSource = Nothing
        cmbUnit.DataSource = dt
        cmbUnit.DisplayMember = "Description"
        cmbUnit.ValueMember = "Code"
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

            qry = " select * from (select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,xxxxxxx.Location_Code,[Loc Desp],Add1,Add4,convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty,
                    case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate ,case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost, RecPurQty,RecPurRate,RecPurCost ,RecProQty, RecProRate ,RecProCost,RecAdjQty,RecAdjRate ,RecAdjCost ,RecOthQty,RecOthRate ,RecOthCost,RecQty,RecRate,RecCost  ,IssTransferQty ,IssTransferRate ,IssTransferCost ,IssSaleQty ,IssSaleRate  ,IssSaleCost , IssIssAdjQty , IssIssAdjRate ,IssIssAdjCost , IssOthQty , IssOthRate ,IssOthCost ,IssQty,IssRate,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty,
                    case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 ) then 0 else CLCost/CLQty end as CLRate, CLCost 
                     
                    from ( select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost
                    ,sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount 
                    ,sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount 
                      ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,sum(case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty  ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost 
                    ,sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount
                    ,sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount
                     ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF 
                    ,SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount 
                    ,SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount 
                      from (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF 
                    ,( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount
                    ,(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount 
                     ,   '' as In_Category,'' as Out_Category,max(Structure_Desc)Structure_Desc,max(RACK_NO)RACK_NO from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation,Structure_Desc,TSPL_ITEM_MASTER.RACK_NO  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No    left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
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
                     where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code 
                     union all  select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,  convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost, ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,  ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF
                    ,(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount
                    ,(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount
                     ,In_Category,Out_Category ,Structure_Desc,RACK_NO    from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation,Structure_Desc,TSPL_ITEM_MASTER.RACK_NO   from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
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
                     where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     union  all select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],Items.Item_Code,Item_Desc, Item_Category_Struct_Code,  Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF 
                    ,0 as FATAmount
                    ,0 as SNFAmount
                     ,In_Category,Out_Category,Structure_Desc,RACK_NO  from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type,Structure_Desc,TSPL_ITEM_MASTER.RACK_NO   FROM ExplodeDates( '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2  and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")   and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtBillToLocation.Value) + "') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code  left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2  )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x Order by  convert(date,  Punching_Date,103),Location_Code"

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

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Try
            txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class