Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptPurchasePlanReport
    Inherits FrmMainTranScreen
    Dim MIS_Item_Group As String

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isModifyFlag
        btnExport.Visible = MyBase.isExport
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub RptPurchasePlanReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FillFicalYear()
        GetMIS_ITem_GroupColumn()
    End Sub

    Public Sub FillFicalYear()
        Dim qry As String = " select distinct final.FiscalYear , final.Year from (  select convert (varchar ,min( datepart(year,PUNCHING_DAte)) -1) +' - '+ convert (varchar ,min( datepart(year,PUNCHING_DAte))-1 +1 ) as FiscalYear , convert (varchar ,min( datepart(year,PUNCHING_DAte)) -1) as Year from  TSPL_INVENTORY_MOVEMENT  union all  select distinct convert (varchar, datepart(year,PUNCHING_DAte) ) +' - '+  convert (varchar, datepart(year,PUNCHING_DAte) +1 ) as FiscalYear , convert (varchar, datepart(year,PUNCHING_DAte) ) as Year  from TSPL_INVENTORY_MOVEMENT  union All   select convert (varchar ,min( datepart(year,PUNCHING_DAte)) -1) +' - '+ convert (varchar ,min( datepart(year,PUNCHING_DAte))-1 +1 ) as FiscalYear , convert (varchar ,min( datepart(year,PUNCHING_DAte)) -1) as Year from  TSPL_INVENTORY_MOVEMENT_NEW  union all  select distinct convert (varchar, datepart(year,PUNCHING_DAte) ) +' - '+  convert (varchar, datepart(year,PUNCHING_DAte) +1 ) as FiscalYear , convert (varchar, datepart(year,PUNCHING_DAte) ) as Year  from TSPL_INVENTORY_MOVEMENT_NEW ) final  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            cboFiscalYear.DataSource = Nothing
            cboFiscalYear.DataSource = dt
            cboFiscalYear.ValueMember = "Year"
            cboFiscalYear.DisplayMember = "FiscalYear"
        End If
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        FillFicalYear()
        txtItem.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PageSetupReport_ID = MyBase.Form_ID
        PrintData(Nothing)
    End Sub
    Sub PrintData(ByVal exporter As EnumExportTo)
        Try
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qry As String = ""
            Dim whrcate As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim colMonth As String = ""
            Dim colMonthforSummery As String = ""
            Dim colCategoryName As String = "" ' Category 
            Dim colCategoryNameVirtualCategoryTabel As String = ""
            Dim colCategoryNameDescWithNull As String = ""
            Dim colCategoryNameWithMax As String = ""
            Dim colCategoryNameWithMaxForFinalResult As String = ""
            Dim wherItem As String = ""
            Dim prvLastDateOfMonth As String = ""
            Dim currMonthLastDate As String = ""
            Dim checkCurrentFisalYear As String = ""
            prvLastDateOfMonth = clsDBFuncationality.getSingleValue(" select CONVERT(date,dateadd(d,-(day(getdate())),getdate()),106) ")
            currMonthLastDate = clsDBFuncationality.getSingleValue(" SELECT CONVERT(date, DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,getdate())+1,0)),106) ")


            Dim colCategoryNameWithMMMM As String = ""
            colCategoryNameWithMMMM = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','  +'MMMM.['+aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '') ")
            Dim colCategoryNameWithFinalMax As String = ""
            colCategoryNameWithFinalMax = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(final.['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '') ")

            Dim strToDateFiscalYear As Integer = 0
            strToDateFiscalYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromdate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            Todate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(strToDateFiscalYear) + "", "dd/MM/yyyy")
            colMonth = "[January], [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December]"
            colMonthforSummery = "January,February,March,April,May,June,July,August,September,October,November,December"

            colCategoryName = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','  +'['+aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameVirtualCategoryTabel = clsDBFuncationality.getSingleValue("  Select  STUFF((SELECT ','  +'VirtualCategoryTabel.['+aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameDescWithNull = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(VirtualCategoryTabel.['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameWithMax = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameWithMaxForFinalResult = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(FinalResult.['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                wherItem = " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
            End If
            'qry = " select dddd.Item_Code,dddd.Item_Desc,dddd.UOM, " + colCategoryNameVirtualCategoryTabel + " , " & _
            '      " isnull (dddd.[April],0) as [April],isnull(dddd.[May],0) as [May],isnull( dddd.[June],0) as [June],isnull (dddd.[July],0) as [July],isnull(dddd.[August],0) as [August] ,isnull(dddd.[September],0) as [September],isnull(dddd.[October],0) as [October],isnull(dddd.[November],0) as [November] ,isnull(dddd.[December],0) as [December] ,isnull(dddd.[January],0) as [January] ,isnull( dddd.[February],0) as [February] ,isnull(dddd.[March],0) as [March] , (isnull (dddd.[April],0) +isnull(dddd.[May],0) + isnull( dddd.[June],0) + isnull (dddd.[July],0) +isnull(dddd.[August],0) +isnull(dddd.[September],0) +isnull(dddd.[October],0) + isnull(dddd.[November],0) + isnull(dddd.[December],0) + isnull(dddd.[January],0) + isnull( dddd.[February],0) + isnull(dddd.[March],0)) as Total  from (  select * from ( " & _
            '      " select pppp.Item_Code,max(pppp.Item_Desc) as Item_Desc,pppp.UOM,sum(pppp.Issued_Qty) as Issued_Qty,pppp.MONTHS from ( " & _
            '      " Select TSPL_INVENTORY_MOVEMENT.Item_Code,max(TSPL_INVENTORY_MOVEMENT.Item_Desc) as Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM ,  Sum(STOCK_QTY * (CASE WHEN convert (date, PUNCHING_DAte,103) >= convert (date,'" + fromdate + "' ,103) AND CONVERT(date, PUNCHING_DAte,103) <= convert (date,'" + Todate + "',103)   THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_Qty ,datename(MONTH,PUNCHING_DAte ) as  MONTHS  from TSPL_INVENTORY_MOVEMENT " & _
            '      " where convert (date, PUNCHING_DAte,103) >= convert (date,'" + fromdate + "',103) and convert (date,PUNCHING_DAte,103) <= convert (date,'" + Todate + "',103)   group by TSPL_INVENTORY_MOVEMENT.Item_Code, datename(MONTH,TSPL_INVENTORY_MOVEMENT.PUNCHING_DAte ),TSPL_INVENTORY_MOVEMENT.UOM  " & _
            '      " union all " & _
            '      " Select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,max(TSPL_INVENTORY_MOVEMENT_NEW.Item_Desc) as Item_Desc,TSPL_INVENTORY_MOVEMENT_NEW.UOM ,  Sum(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= convert(date,'" + fromdate + "',103)   AND PUNCHING_DAte <= convert (date,'" + Todate + "',103)   THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_Qty ,datename(MONTH,PUNCHING_DAte )  as  MONTHS  from TSPL_INVENTORY_MOVEMENT_NEW  where convert (date, PUNCHING_DAte,103) >= convert (date,'" + fromdate + "',103) and convert (date,PUNCHING_DAte,103) <= convert (date,'" + Todate + "',103)   group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code, datename (MONTH,TSPL_INVENTORY_MOVEMENT_NEW.PUNCHING_DAte ),TSPL_INVENTORY_MOVEMENT_NEW.UOM " & _
            '      " )pppp group by pppp.Item_Code,pppp.MONTHS ,pppp.UOM ) xxx " & _
            '      " pivot (sum(Issued_Qty)   for MONTHS in ( [January] , [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December]) ) piv ) dddd   left outer join  " & _
            '      " (select Item_Code, " + colCategoryNameWithMax + " from ( " & _
            '      " select * from (  " & _
            '      " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code  ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc  ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER   " & _
            '      " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
            '      " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " & _
            '      " TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values where 2=2 )xx " & _
            '      " Pivot  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryName + " )) Pivt" & _
            '      " ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=dddd.Item_Code order by dddd.Item_Code "
            checkCurrentFisalYear = clsDBFuncationality.getSingleValue(" select case when convert (date, GETDATE(),103) <= convert (date, '" + Todate + "', 103)  then 1 else 0 end ")

            qry = " select MMMM.Item_Code, MMMM.Item_Desc , " + colCategoryNameWithMMMM + " , Stock_UOM , isnull([April],0) as [April] ,isnull([May],0) as [May] ,isnull([June],0) as [June] ,isnull ([July],0) as [July] ,isnull([August],0) as [August] ,isnull([September],0) as [September],isnull([October],0) as [October],isnull([November],0) as [November] ,isnull([December],0) as [December],isnull([January],0) as [January] , isnull([February],0) as [February],isnull([March],0) as [March] , isnull (aaa.PurchaseOrder_Qty,0) as [Pending_PO_Qty]  from (  " & _
                  " select * from ( select final.Item_Code,max(final.Item_Desc) as Item_Desc, " + colCategoryNameWithFinalMax + " ,max(final.Stock_UOM) as Stock_UOM ,  sum(final.IssQty)  - sum(final.RecQty)  as Consumption   ,datename(MONTH,final.Punching_Date  )  as Punching_Date   from ( " & _
                  " select CompName,FromDate,ToDate,Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " + colCategoryName + " , Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty   " & _
                  "  , case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate, (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNF, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecQty,RecRate,RecFAT,RecFATPER,RecSNF,RecSNFPER,RecCost , IssQty, " & _
                  " IssRate,IssFAT,IssFATPER,IssSNF,IssSNFPER,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost from ( select  case when '0'='1' then '' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(fromdate, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(Todate, "dd/MMM/yyyy") + "' as ToDate , Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description , " + colCategoryName + " , " & _
                  " Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Balance_FAT else 0 end) as RecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as RecFATPER, (case when InOut='I' then Balance_SNF else 0 end) as RecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as RecSNFPER, (case when InOut='I' then Cost else 0 end) as RecCost, " & _
                  " (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty ,  " & _
                  " (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as IssFATPER, (case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as IssSNFPER, (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  " & _
                  " ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG ,SUM(Balance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_FAT,SUM(Balance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_SNF  from (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name, " & _
                  " 'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description, " + colCategoryNameWithMax + " " & _
                  " ,xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code , " & _
                  " sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF  from (select * from ( select InventroyMovement.Trans_Id,InventroyMovement.Trans_Type,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code ," & _
                  " " + colCategoryNameVirtualCategoryTabel + " ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  " & _
                  " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType  from TSPL_INVENTORY_MOVEMENT " & _
                  " union all " & _
                  " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType from TSPL_INVENTORY_MOVEMENT_NEW " & _
                  " ) InventroyMovement  " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code  left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code  left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type left outer join  " & _
                  "  (select Item_Code, " + colCategoryNameWithMax + "  from (  select * from ( " & _
                  "  select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
                  " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " & _
                  " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  " & _
                  " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  " & _
                  "  from TSPL_ITEM_MASTER " & _
                  " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code  " & _
                  " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                  "  where 2 = 2 " & _
                  " )xx " & _
                  " Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryName + ")  ) Pivt  " & _
                  "  ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'   and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join (select 'R' as Code,'Raw Material' as Name union  select 'F' as Code,'Finished Good' as Name  union  select 'S' as Code,'Semi Finished Good' as Name  union   select 'A' as Code,'Asset' as Name  union  select 'O' as Code,'Other' as Name union  select 'J' as Code,'Job Work' as Name) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  and (MilkFatPer=0 and FatPer=0  and  MilkSNFPer=0 and SNFPer=0)  " + wherItem + "  ) xxx  " & _
                  " where convert (date,Punching_Date,103) < convert (date,'" + fromdate + "',103) group by xxx.Item_Code  union all  select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " + colCategoryName + " " & _
                  " ,Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty " & _
                  " ,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF   from (select * from ( select InventroyMovement.Trans_Id,InventroyMovement.Trans_Type,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code   " & _
                  " ," + colCategoryNameVirtualCategoryTabel + " ,  " & _
                  " TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType  from TSPL_INVENTORY_MOVEMENT " & _
                  " union all   " & _
                  " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType from TSPL_INVENTORY_MOVEMENT_NEW ) InventroyMovement  " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code " & _
                  " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code  " & _
                  " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as   " & _
                  " FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type left outer join (select Item_Code,  " + colCategoryNameWithMax + " from ( select * from (  " & _
                  " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code  ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values   ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc from TSPL_ITEM_MASTER  left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code   left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  where 2 = 2  )xx  Pivot " & _
                  " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryName + " )  ) Pivt " & _
                  " ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'   and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join (select 'R' as Code,'Raw Material' as Name union  select 'F' as Code,'Finished Good' as Name  union  select 'S' as Code,'Semi Finished Good' as Name  union   select 'A' as Code,'Asset' as Name  union  select 'O' as Code,'Other' as Name union  select 'J' as Code,'Job Work' as Name) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2  and (MilkFatPer=0 and FatPer=0  and  MilkSNFPer=0 and SNFPer=0)  " + wherItem + " " & _
                  " ) xxx   where convert (date,Punching_Date,103)>=convert (date,'" + fromdate + "',103) and convert(date, Punching_Date,103)<= convert (date,'" + Todate + "',103)  " & _
                  " )xxxxxx  )xxxxxxx where Trans_Id<>0 " & _
                  " ) final  where Trans_Type = 'ISSTRAN' group by final.Item_Code,  " & _
                  " datename(MONTH,final.Punching_Date ),datename(YEAR,final.Punching_Date  )   ) final2  " & _
                  "  pivot (sum(Consumption)   for Punching_Date in ( [January] , [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December]) ) piv    ) MMMM " & _
                  "  left outer join ( select final.Item_Code ,sum (final.[PENDING/ EXTRA QUANTITY]) as PurchaseOrder_Qty  from (select   ddd.Item_Code as Item_Code  , case when  (convert(decimal(10,2), max(ddd.PurchaseOrder_Qty))-convert(decimal(10,2), sum(isnull(TSPL_SRN_DETAIL.SRN_Qty,0))) )  < 0 then 0 else  (convert(decimal(10,2), max(ddd.PurchaseOrder_Qty))-convert(decimal(10,2), sum(isnull(TSPL_SRN_DETAIL.SRN_Qty,0))) ) end as 'PENDING/ EXTRA QUANTITY'   from (    select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,   convert(Date,PurchaseOrder_Date,103) as PurchaseOrder_Date,Vendor_Code,Vendor_Name,  Description,Bill_To_Location , (SELECT TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_LOCATION_MASTER  WHERE Location_Code = Bill_To_Location) as [Location Detail],Item_Code,Item_Desc,PurchaseOrder_Qty from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  where TSPL_PURCHASE_ORDER_HEAD.Status=1 ) ddd left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.PO_ID = ddd.PurchaseOrder_No and TSPL_SRN_DETAIL.Item_Code=ddd.Item_Code  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO = TSPL_SRN_DETAIL.SRN_NO    group by ddd.PurchaseOrder_No ,ddd.Item_Code  ) final group by final.Item_Code ) aaa on MMMM.Item_Code = aaa.Item_Code "

            If checkCurrentFisalYear = "1" Then
                qry = " select kkkk.* , OPQtyTab.OPBal_Current_Month  as Opening_Stock_Current_Month, OPQtyTab.OPBal_Prv_Month as Opening_Stock_Previous_Month  from (  " + qry + " )kkkk  left outer join ( select Item_Code, SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte < convert (date,getdate(),103) THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBal_Current_Month] ,  case when   datename(MONTH,convert (date, getdate(),103) ) = 'April' then 0 else   (SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte < convert (date, '" + prvLastDateOfMonth + "',103) THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))) end AS [OPBal_Prv_Month]   from TSPL_INVENTORY_MOVEMENT group by  Item_Code ) OPQtyTab  on kkkk.Item_Code = OPQtyTab.Item_Code  "
            End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                FooterSummery(colMonthforSummery)
                gv.BestFitColumns()
                gv.Columns("Item_Code").IsPinned = True
                gv.Columns("Item_Desc").IsPinned = True
                gv.Columns("Pending_PO_Qty").IsPinned = True
                gv.Columns("Pending_PO_Qty").PinPosition = PinnedColumnPosition.Right
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub FooterSummery(ByVal strColumnName As String)
        strColumnName = strColumnName + ",Total,Pending_PO_Qty,Opening_Stock_Current_Month,Opening_Stock_Previous_Month"
        Dim words As String() = strColumnName.Split(New Char() {","c})
        If gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim word As String
            For Each word In words
                Dim item1 As New GridViewSummaryItem(word, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptPurchasePlanReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Fiscal Year : " + cboFiscalYear.Text)
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyExportToPDF("Purchase Plan Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
