Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


' Ticket No : BHA/05/09/18-000515 By prabhakar - Create New Report 
Public Class rptProductionWiseStockReco
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Dim isInsideLoadData As Boolean = False
    Dim FORMTYPE As String = Nothing
#End Region
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        ' LoadType()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableCtrl(True)
        LoadUnit()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.Checked = True, "S", "D")
            TemplateGridview = gv1
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try

            clsCommon.ProgressBarShow()
            EnableDisableCtrl(False)
            btnGo.Enabled = False
            Dim fromdate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim Wher As String = " where 2= 2 "
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                Wher += " and Final.Item_Code In (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Wher += " and Final.Location_Code In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            Dim strUnitCode As String = ""
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 AndAlso clsCommon.CompairString(cmbUnit.SelectedValue, "Select") <> CompairStringResult.Equal Then
                strUnitCode = cmbUnit.SelectedValue
            End If

            ''**********************************************************
            'Dim Qry As String = "  select trans_Date as [Trans Date] ,Location_Code as [Location Code],Location_Desc as [Location Name], "
            'If rdbDetail.Checked = True Then
            '    Qry = Qry + " Source_Doc_No ,  "
            'End If
            'Qry = Qry + "  Outermost.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Desc] ,round((coalesce([Opening Qty],RunningBalanceQty-BalanceQty)),2) as [Opening Qty], " & _
            '              "  round([PRODUTION Entry Qty],2) as [PRODUTION Entry Qty], " & _
            '              "  round([ADJUSTMENT Qty],2) as [ADJUSTMENT Qty], " & _
            '              "  round([Sale Qty],2)  as [Sale Qty], " & _
            '              "  round([Disassembly Qty],2)  as [Disassembly Qty], " & _
            '              "  /* round([Other In Qty],2) as [Other In Qty], */ " & _
            '              "  /* round([Other Out Qty],2) as [Other Out Qty], */ " & _
            '              "  round(RunningBalanceQty,2) as [Closing Qty] " & _
            '              "  from (  " & _
            '              "  SELECT * " & _
            '              "  ,(coalesce([Opening Qty],0)+coalesce([PRODUTION Entry Qty],0)+coalesce([ADJUSTMENT Qty],0) /* + coalesce([Other In Qty],0) +coalesce([Other Out Qty],0) */ +coalesce([Sale Qty],0)+coalesce([Disassembly Qty],0)) as BalanceQty, " & _
            '              "  sum(coalesce([Opening Qty],0)+coalesce([PRODUTION Entry Qty],0)+coalesce([ADJUSTMENT Qty],0) /* + coalesce([Other In Qty],0) +coalesce([Other Out Qty],0) */ +coalesce([Sale Qty],0)+coalesce([Disassembly Qty],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceQty " & _
            '              "  FROM (  " & _
            '              "  select Item_Code,Location_Code,Location_Desc, "
            'If rdbDetail.Checked = True Then
            '    Qry = Qry + " Source_Doc_No,  "
            'End If
            'Qry = Qry + "  convert(varchar,Trans_date,103) as Trans_Date, " & _
            '              "  SUM([Opening Qty]) as [Opening Qty], " & _
            '              "  SUM([PRODUTION Entry Qty]) as [PRODUTION Entry Qty], " & _
            '              "  SUM([Sale Qty]) AS [Sale Qty], " & _
            '              "  SUM([ADJUSTMENT Qty]) AS [ADJUSTMENT Qty], " & _
            '              "  SUM([Disassembly Qty]) AS [Disassembly Qty], " & _
            '              "  SUM([Other Out Qty]) AS [Other Out Qty], " & _
            '              "  SUM([Other In Qty]) AS [Other In Qty], " & _
            '              "  row_number() over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,Trans_date) as Tr_id from (  " & _
            '              "  select Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type+' Qty' as Trans_Type_Qty,max(FatStockFinal.Qty) as Qty , FatStockFinal.Source_Doc_No " & _
            '              "  from ( " & _
            '              "  select Loc.Location_Code,Loc.Location_Desc,(case when Seq_No=0 then '" + fromdate + "' else AllDate.thedate end) as Trans_date,Seq_No,Trans_Type from ( select 0 as Seq_No,'Opening' as Trans_Type  " & _
            '              "  union all  " & _
            '              "  select 1 as Seq_No,'PRODUTION Entry' as Trans_Type  " & _
            '              "  union all " & _
            '              "  select 2 as Seq_No,'Sale' as Trans_Type " & _
            '              "  union all " & _
            '              "  select 3 as Seq_No,'ADJUSTMENT' as Trans_Type  " & _
            '              "  union all  " & _
            '              "  select 4 as Seq_No,'Disassembly' as Trans_Type " & _
            '              "  union all " & _
            '              "  select 5 as Seq_No,'Other In' as Trans_Type " & _
            '              "  union all " & _
            '              "  select 6 as Seq_No,'Other Out' as Trans_Type  " & _
            '              "  ) as TransType,TSPL_LOCATION_MASTER as Loc,dbo.ExplodeDates('" + fromdate + "','" + todate + "') as AllDate  " & _
            '              "  where ((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y')) ) as Loc_Trans  " & _
            '              "  inner join ( " & _
            '              "  select Item_Code,Stock_UOM,Location_Code,Report_Type,cast('" + fromdate + "' as date) as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty , '' as Source_Doc_No " & _
            '              "  from (  " & _
            '              "  select Final.Inv_Type,Final.Trans_Type,'Opening' as Report_Type, " & _
            '              "  Final.InOut,Final.Location_Code,Final.Item_Code,  " & _
            '              "  Final.Stock_Qty,Final.Stock_UOM,  " & _
            '              "  Punching_Date " & _
            '              "  from (  " & _
            '              "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code   " & _
            '              "  from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) < '" + fromdate + "' and 2=2 and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0   " & _
            '              "  Union All  " & _
            '              "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code  " & _
            '              "  from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) < '" + fromdate + "' and 2=2 and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0  " & _
            '              "  ) as Final  " + Wher + " " & _
            '              "  ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code  " & _
            '              "  Union All " & _
            '              "  select Item_Code,Stock_UOM,Location_Code,Report_Type,Punching_Date  as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty,Source_Doc_No  " & _
            '              "  from (  " & _
            '              "  select Final.Inv_Type,Final.Trans_Type,(Case  " & _
            '              "  when Final.Trans_Type in ('PROD_ENTRY') then 'PRODUTION Entry'  " & _
            '              "  when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS') then 'Sale' " & _
            '              "  when Final.Trans_Type in ('IC-AD')  Then 'ADJUSTMENT'  " & _
            '              "  When final.Trans_Type in ('Disassembly') Then 'Disassembly' " & _
            '              "  When Final.Inout='I' then 'Other In' when Final.Inout='O' then 'Other Out' end) as Report_Type, " & _
            '              "  Final.InOut, Final.Location_Code, Final.Item_Code, " & _
            '              "  Final.Stock_Qty, Final.Stock_UOM, Punching_Date, Final.Source_Doc_No " & _
            '              "  from ( " & _
            '              "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code " & _
            '              "  from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) between '" + fromdate + "' and '" + todate + "' and 2=2  and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0   " & _
            '              "   and  TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('PROD_ENTRY','CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS', 'IC-AD','Disassembly') " & _
            '              "  Union All " & _
            '              "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code  " & _
            '              "  from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) between '" + fromdate + "' and '" + todate + "' and 2=2  and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0  " & _
            '              "   and  TSPL_INVENTORY_MOVEMENT.Trans_Type in ('PROD_ENTRY','CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS', 'IC-AD','Disassembly') " & _
            '              "  ) as Final  " + Wher + " " & _
            '              "  ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code,Punching_Date ,Source_Doc_No  " & _
            '              "  ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date  " & _
            '              "  group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No , Source_Doc_No " & _
            '              "  ) AS FatStockOuter  " & _
            '              "   Pivot " & _
            '              "  (   " & _
            '              "  max(Qty) " & _
            '              "  FOR Trans_Type_Qty  " & _
            '              "  IN ([Opening Qty],  " & _
            '              "  [PRODUTION Entry Qty],  " & _
            '              "  [ADJUSTMENT Qty],  " & _
            '              "  [Sale Qty],  " & _
            '              "  [Disassembly Qty], " & _
            '              "  [Other In Qty],  " & _
            '              "  [Other Out Qty] " & _
            '              "  )  " & _
            '              "  ) AS PIVQty  " & _
            '              "  GROUP BY Item_Code,Location_Code,Location_Desc,Trans_date  "
            'If rdbDetail.Checked = True Then
            '    Qry = Qry + " ,Source_Doc_No  "
            'End If
            'Qry = Qry + "  )AS FATSNFtockFinal) as Outermost  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code = Outermost.Item_Code  "
            '' **********************************************************************************************************************************************

            '**********************************************************
            Dim Qry As String = "  select trans_Date as [Trans Date] ,Location_Code as [Location Code],Location_Desc as [Location Name], "
            If rdbDetail.Checked = True Then
                Qry = Qry + " Source_Doc_No as [Document Code] ,  "
            End If
            Qry = Qry + "  Outermost.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Desc], "
            If clsCommon.myLen(strUnitCode) > 0 Then
                Qry = Qry + " '" + strUnitCode + "' as UOM, "
            Else
                Qry = Qry + " Stock_UOM as UOM, "
            End If
            If rdbDetail.Checked = True Then
                Qry = Qry + " Stocking_Rate as [Stocking Rate] ,  "
            End If
            Qry = Qry + "  round((coalesce([Opening Qty],RunningBalanceQty-BalanceQty)),2) as [Opening Qty], " & _
                          "  round([PRODUTION Entry Qty],2) as [PRODUTION Entry Qty], " & _
                          "  round([ADJUSTMENT Qty],2) as [ADJUSTMENT Qty], " & _
                          "  round([Sale Qty],2)  as [Sale Qty], " & _
                          "  round([Disassembly Qty],2)  as [Disassembly Qty], " & _
                          "  /* round([Other In Qty],2) as [Other In Qty], */ " & _
                          "  /* round([Other Out Qty],2) as [Other Out Qty], */ " & _
                          "  round(RunningBalanceQty,2) as [Closing Qty] " & _
                          "  from (  " & _
                          "  SELECT * " & _
                          "  ,(coalesce([Opening Qty],0)+coalesce([PRODUTION Entry Qty],0)+coalesce([ADJUSTMENT Qty],0) /* + coalesce([Other In Qty],0) +coalesce([Other Out Qty],0) */ +coalesce([Sale Qty],0)+coalesce([Disassembly Qty],0)) as BalanceQty, " & _
                          "  sum(coalesce([Opening Qty],0)+coalesce([PRODUTION Entry Qty],0)+coalesce([ADJUSTMENT Qty],0) /* + coalesce([Other In Qty],0) +coalesce([Other Out Qty],0) */ +coalesce([Sale Qty],0)+coalesce([Disassembly Qty],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceQty " & _
                          "  FROM (  " & _
                          "  select Item_Code,Location_Code,Location_Desc, "
            If rdbDetail.Checked = True Then
                Qry = Qry + " Source_Doc_No,  max(Stocking_Rate) as Stocking_Rate,  " ' max(Avg_Cost) as Avg_Cost  ,max(Stock_Qty_Item_Wise)  as Stock_Qty_Item_Wise ,
            End If
            Qry = Qry + "  convert(varchar,Trans_date,103) as Trans_Date, " & _
                          "  SUM([Opening Qty]) as [Opening Qty], " & _
                          "  SUM([PRODUTION Entry Qty]) as [PRODUTION Entry Qty], " & _
                          "  SUM([Sale Qty]) AS [Sale Qty], " & _
                          "  SUM([ADJUSTMENT Qty]) AS [ADJUSTMENT Qty], " & _
                          "  SUM([Disassembly Qty]) AS [Disassembly Qty], " & _
                          "  SUM([Other Out Qty]) AS [Other Out Qty], " & _
                          "  SUM([Other In Qty]) AS [Other In Qty], " & _
                          "  row_number() over (partition by Item_Code,Location_Code, Stock_UOM order by Item_Code,Location_Code,Trans_date) as Tr_id , Stock_UOM   from (  " & _
                          "  select Loc_Trans.Location_Code,FatStockFinal.Item_Code, FatStockFinal.Stock_UOM,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type+' Qty' as Trans_Type_Qty,max(FatStockFinal.Qty) as Qty , FatStockFinal.Source_Doc_No  ,max(FatStockFinal.Avg_Cost) as Avg_Cost ,max( FatStockFinal.Stock_Qty_Item_Wise) as Stock_Qty_Item_Wise ,max( FatStockFinal.Stocking_Rate ) as Stocking_Rate " & _
                          "  from ( " & _
                          "  select Loc.Location_Code,Loc.Location_Desc,(case when Seq_No=0 then '" + fromdate + "' else AllDate.thedate end) as Trans_date,Seq_No,Trans_Type from ( select 0 as Seq_No,'Opening' as Trans_Type  " & _
                          "  union all  " & _
                          "  select 1 as Seq_No,'PRODUTION Entry' as Trans_Type  " & _
                          "  union all " & _
                          "  select 2 as Seq_No,'Sale' as Trans_Type " & _
                          "  union all " & _
                          "  select 3 as Seq_No,'ADJUSTMENT' as Trans_Type  " & _
                          "  union all  " & _
                          "  select 4 as Seq_No,'Disassembly' as Trans_Type " & _
                          "  union all " & _
                          "  select 5 as Seq_No,'Other In' as Trans_Type " & _
                          "  union all " & _
                          "  select 6 as Seq_No,'Other Out' as Trans_Type  " & _
                          "  ) as TransType,TSPL_LOCATION_MASTER as Loc,dbo.ExplodeDates('" + fromdate + "','" + todate + "') as AllDate  " & _
                          "  where ((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y')) ) as Loc_Trans  " & _
                          "  inner join ( " & _
                          "  select Item_Code,Stock_UOM,Location_Code,Report_Type,cast('" + fromdate + "' as date) as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty , '' as Source_Doc_No  , 0 as Avg_Cost , 0 as Stock_Qty_Item_Wise , 0 as Stocking_Rate " & _
                          "  from (  " & _
                          "  select Final.Inv_Type,Final.Trans_Type,'Opening' as Report_Type, " & _
                          "  Final.InOut,Final.Location_Code,Final.Item_Code,  "
            If clsCommon.myLen(strUnitCode) > 0 Then
                Qry = Qry + " ( case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as Float) end) as Stock_Qty,  "
            Else

                Qry = Qry + "  Final.Stock_Qty, "
            End If
            Qry = Qry + "  Final.Stock_UOM,  " & _
                          "  Punching_Date " & _
                          "  from (  " & _
                          "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code   " & _
                          "  from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) < '" + fromdate + "' and 2=2 and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0   " & _
                          "  Union All  " & _
                          "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code  " & _
                          "  from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) < '" + fromdate + "' and 2=2 and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0  " & _
                          "  ) as Final "
            If clsCommon.myLen(strUnitCode) > 0 Then
                Qry = Qry + "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code "
                Qry = Qry + "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" + strUnitCode + "') as StockLtr on Final.Item_Code=StockLtr.Item_Code "
            End If
            Qry = Qry + " " + Wher + " " & _
                          "  ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code  " & _
                          "  Union All " & _
                          "  select Item_Code,Stock_UOM,Location_Code,Report_Type,Punching_Date  as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty,Source_Doc_No  , max( Avg_Cost) as  Avg_Cost , max(Stock_Qty_Item_Wise) as Stock_Qty_Item_Wise ,max( Stocking_Rate) as Stocking_Rate  " & _
                          "  from (  " & _
                          "  select Final.Inv_Type,Final.Trans_Type,(Case  " & _
                          "  when Final.Trans_Type in ('PROD_ENTRY') then 'PRODUTION Entry'  " & _
                          "  when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS','FS-SH','FS-SR') then 'Sale' " & _
                          "  when Final.Trans_Type in ('IC-AD')  Then 'ADJUSTMENT'  " & _
                          "  When final.Trans_Type in ('Disassembly') Then 'Disassembly' " & _
                          "  When Final.Inout='I' then 'Other In' when Final.Inout='O' then 'Other Out' end) as Report_Type, " & _
                          "  Final.InOut, Final.Location_Code, Final.Item_Code, "
            If clsCommon.myLen(strUnitCode) > 0 Then
                Qry = Qry + " ( case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as Float) end) as Stock_Qty,  "
            Else

                Qry = Qry + "  Final.Stock_Qty, "
            End If

            Qry = Qry + "  Final.Stock_UOM, Punching_Date, Final.Source_Doc_No , Final.Avg_Cost,Final.Stock_Qty_Item_Wise , Final.Stocking_Rate " & _
                   "  from ( " & _
                   "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code, TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost, TSPL_INVENTORY_MOVEMENT_NEW. Stock_Qty as Stock_Qty_Item_Wise , CAST ( (TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost / (nullif(TSPL_INVENTORY_MOVEMENT_NEW. Stock_Qty,0)) ) as Decimal(18,2)) as Stocking_Rate " & _
                   "  from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) between '" + fromdate + "' and '" + todate + "' and 2=2  and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0   " & _
                   "   and  TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('PROD_ENTRY','CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS', 'IC-AD','Disassembly','FS-SH','FS-SR') " & _
                   "  Union All " & _
                   "  select  1 as Inv_Type,TSPL_INVENTORY_MOVEMENT.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,cast(Punching_Date as date) as Punching_Date,Other_Location_Code , TSPL_INVENTORY_MOVEMENT.Avg_Cost, TSPL_INVENTORY_MOVEMENT. Stock_Qty as  Stock_Qty_Item_Wise ,cast ((TSPL_INVENTORY_MOVEMENT.Avg_Cost / (nullif( TSPL_INVENTORY_MOVEMENT.Stock_Qty,0))) as Decimal (18,2) ) as  Stocking_Rate " & _
                   "  from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   where 2=2 and cast(Punching_Date as date) between '" + fromdate + "' and '" + todate + "' and 2=2  and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0  " & _
                   "   and  TSPL_INVENTORY_MOVEMENT.Trans_Type in ('PROD_ENTRY','CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS', 'IC-AD','Disassembly','FS-SH','FS-SR') " & _
                   "  ) as Final "

            If clsCommon.myLen(strUnitCode) > 0 Then
                Qry = Qry + "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code  "
                Qry = Qry + "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" + strUnitCode + "') as StockLtr on Final.Item_Code=StockLtr.Item_Code   "
            End If
            Qry = Qry + " " + Wher + " " & _
                          "  ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code,Punching_Date ,Source_Doc_No  " & _
                          "  ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date  " & _
                          "  group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No , Source_Doc_No , FatStockFinal.Stock_UOM " & _
                          "  ) AS FatStockOuter  " & _
                          "   Pivot " & _
                          "  (   " & _
                          "  max(Qty) " & _
                          "  FOR Trans_Type_Qty  " & _
                          "  IN ([Opening Qty],  " & _
                          "  [PRODUTION Entry Qty],  " & _
                          "  [ADJUSTMENT Qty],  " & _
                          "  [Sale Qty],  " & _
                          "  [Disassembly Qty], " & _
                          "  [Other In Qty],  " & _
                          "  [Other Out Qty] " & _
                          "  )  " & _
                          "  ) AS PIVQty  " & _
                          "  GROUP BY Item_Code,Location_Code,Location_Desc,Trans_date  ,Stock_UOM "
            If rdbDetail.Checked = True Then
                Qry = Qry + " ,Source_Doc_No  "
            End If
            Qry = Qry + "  )AS FATSNFtockFinal) as Outermost  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code = Outermost.Item_Code where TSPL_ITEM_MASTER.Product_Type in ('MI','MP') "
            ' **********************************************************************************************************************************************

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            gv1.DataSource = dt
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.BestFitColumns()
            gv1.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2
            btnExport.Enabled = True
            'btnQuickExport.Enabled = True
            gv1.ReadOnly = True
            SetGridFormationOFGV1()
            btnGo.Enabled = True
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGo.Enabled = True
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For Each col As GridViewColumn In gv1.Columns
            If (col.Name.Contains("PRODUTION Entry Qty") = True OrElse col.Name.Contains("Sale Qty") = True OrElse col.Name.Contains("Disassembly Qty") = True OrElse col.Name.Contains("Other Out Qty") = True OrElse col.Name.Contains("Other In Qty") = True) Then
                If (col.Name.Contains("Opening") = True OrElse col.Name.Contains("Closing") = True) Then
                    Continue For
                End If
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            End If
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arrHeader As List(Of String) = New List(Of String)()
        'Dim strTemp As String = ""
        'arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        'clsCommon.MyExportToExcelGrid("Production Wise stock Reco:", gv1, arrHeader, Me.Text)

        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionWiseStockReco & "'"))
                If txtItemCode.arrDispalyMember IsNot Nothing AndAlso txtItemCode.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
                End If
                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    
    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        rdbSummary.Enabled = val
        rdbDetail.Enabled = val
        txtItemCode.Enabled = val
        txtLocation.Enabled = val
        cmbUnit.Enabled = val
    End Sub

    'Private Function GetReportID() As String
    '    Dim ReportID As String = "ProductionWiseStockRecoReport"
    '    Return ReportID
    'End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function

    

   

    
    'Sub LoadType()
    '    isInsideLoadData = True
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    Dim dr As DataRow = dt.NewRow()

    '    dr("Code") = "Summary"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Detail"
    '    dt.Rows.Add(dr)

    '    cboType.DataSource = dt
    '    cboType.ValueMember = "Code"
    '    cboType.DisplayMember = "Code"

    '    isInsideLoadData = False
    'End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        btnExport.Enabled = False
        'btnQuickExport.Enabled = False
        btnGo.Enabled = True
        gv1.DataSource = Nothing
        LoadUnit()
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemCodeProdWiseStockRecoMulSel", qry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select tspl_location_Master.Location_Code as Code,tspl_location_Master.Location_Desc as Name from tspl_location_Master "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocProWiseStorcRecoMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
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

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionWiseStockReco & "'"))
                If txtItemCode.arrDispalyMember IsNot Nothing AndAlso txtItemCode.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
                End If
                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

'Public Class ClsProductionwiseStockReco
'    Public Shared Function GetQtyFatSNFStockGKDT(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As DataTable
'        Dim qry As String = GetQtyFatSNFStockQryGK(objFilter, ShowTankerNo)
'        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
'        Return dt
'    End Function

'    Public Shared Function GetQtyFatSNFStockQryGK(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As String
'        Dim Qry As String = ""
'        Dim LocTransQry As String = ""
'        Dim StockUnionQry As String = ""
'        If clsCommon.CompairString(objFilter.ReportType, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.ReportType, "None") = CompairStringResult.Equal Then
'            objFilter.ReportType = "Summary"
'        End If

'        LocTransQry = " select Loc.Location_Code,Loc.Location_Desc,(case when Seq_No=0 then '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' else AllDate.thedate end) as Trans_date,Seq_No,Trans_Type from (" & _
'                      " select 0 as Seq_No,'Opening' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 1 as Seq_No,'Purchase' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 2 as Seq_No,'MCC Transfer Received' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 3 as Seq_No,'Other In' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 4 as Seq_No,'Sale' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 5 as Seq_No,'MCC Transfer Out' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 6 as Seq_No,'Plant Transfer Out' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 7 as Seq_No,'Inhouse Consumption' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 8 as Seq_No,'Other Out' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 9 as Seq_No,'In Transit' as Trans_Type " & Environment.NewLine &
'                      " union all " & Environment.NewLine &
'                      " select 10 as Seq_No,'Physical Stock' as Trans_Type " & Environment.NewLine &
'                      " ) as TransType,TSPL_LOCATION_MASTER as Loc,dbo.ExplodeDates('" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "','" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "') as AllDate " & Environment.NewLine &
'                      " where ((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y'))"
'        StockUnionQry = GetQtyFatSNFBaseQryGK(objFilter, True, ShowTankerNo) & Environment.NewLine &
'                        " Union All " & GetQtyFatSNFBaseQryGK(objFilter, False, ShowTankerNo)
'        Dim transDateSel As String = "convert(varchar,Trans_date,103) as Trans_Date"
'        Dim transDateGroup As String = "Trans_date"
'        Dim transDateOrder As String = "Trans_date"
'        If clsCommon.CompairString(objFilter.ReportType, "Daily") = CompairStringResult.Equal Then
'            transDateSel = "convert(varchar,Trans_date,103) as Trans_Date"
'            transDateGroup = "Trans_date"
'            transDateOrder = "Trans_date"
'        ElseIf clsCommon.CompairString(objFilter.ReportType, "Monthly") = CompairStringResult.Equal Then
'            transDateSel = " dateName(month,Trans_date)+ '-' + dateName(year,Trans_date) as Trans_Date"
'            transDateGroup = "dateName(month,Trans_date),dateName(year,Trans_date)"
'            transDateOrder = "(dateName(month,Trans_date) + '-' + dateName(year,Trans_date))"
'        End If

'        Qry = " select trans_Date as [Trans Date] ,Location_Code as [Location Code],Location_Desc as [Location Name],Item_Code as [Item Code]," & Environment.NewLine &
'              " round((coalesce([Opening Qty],RunningBalanceQty-BalanceQty)),2) as [Opening Qty]," & Environment.NewLine &
'              " round((coalesce([Opening FAT],RunningBalanceFAT-BalanceFat)),3) as [Opening FAT]," & Environment.NewLine &
'              " round((coalesce([Opening SNF],RunningBalanceSNF-BalanceSNF)),3) as [Opening SNF]," & Environment.NewLine &
'              " round([Purchase Qty Ltr],2) as [Purchase Qty Ltr]," & Environment.NewLine &
'              " round([Purchase FAT Ltr],3) as [Purchase FAT Ltr]," & Environment.NewLine &
'              " round([Purchase SNF Ltr],3) as [Purchase SNF Ltr]," & Environment.NewLine &
'              " round([Purchase Qty],2) as [Purchase Qty]," & Environment.NewLine &
'              " round([Purchase FAT],3) as [Purchase FAT]," & Environment.NewLine &
'              " round([Purchase SNF],3) as [Purchase SNF]," & Environment.NewLine &
'              " round([MCC Transfer Received Qty],2) as [MCC Transfer Received Qty]," & Environment.NewLine &
'              " round([MCC Transfer Received FAT],3) as [MCC Transfer Received FAT]," & Environment.NewLine &
'              " round([MCC Transfer Received SNF],3) as [MCC Transfer Received SNF]," & Environment.NewLine &
'              " round([Other In Qty],2) as [Other In Qty]," & Environment.NewLine &
'              " round([Other In FAT],3) as [Other In FAT]," & Environment.NewLine &
'              " round([Other In SNF],3) as [Other In SNF]," & Environment.NewLine &
'              " (round(coalesce([Purchase Qty],0),2)+round(coalesce([MCC Transfer Received Qty],0),2)+round(coalesce([Other In Qty],0),2)) as [Total In Qty]," & Environment.NewLine &
'              " (round(coalesce([Purchase FAT],0),3)+round(coalesce([MCC Transfer Received FAT],0),3)+round(coalesce([Other In FAT],0),3)) as [Total In FAT]," & Environment.NewLine &
'              " (round(coalesce([Purchase SNF],0),3)+round(coalesce([MCC Transfer Received SNF],0),3)+round(coalesce([Other In SNF],0),3)) as [Total In SNF]," & Environment.NewLine &
'              " round([Sale Qty],2) as [Sale Qty]," & Environment.NewLine &
'              " round([Sale FAT],3) as [Sale FAT]," & Environment.NewLine &
'              " round([Sale SNF],3) as [Sale SNF]," & Environment.NewLine &
'              " round([MCC Transfer Out Qty],2) as [MCC Transfer Out Qty]," & Environment.NewLine &
'              " round([MCC Transfer Out FAT],3) as [MCC Transfer Out FAT]," & Environment.NewLine &
'              " round([MCC Transfer Out SNF],3) as [MCC Transfer Out SNF]," & Environment.NewLine &
'              " round([Plant Transfer Out Qty],2) as [Plant Transfer Out Qty]," & Environment.NewLine &
'              " round([Plant Transfer Out FAT],3) as [Plant Transfer Out FAT]," & Environment.NewLine &
'              " round([Plant Transfer Out SNF],3) as [Plant Transfer Out SNF]," & Environment.NewLine &
'              " round([Inhouse Consumption Qty],2) as [Inhouse Consumption Qty]," & Environment.NewLine &
'              " round([Inhouse Consumption FAT],3) as [Inhouse Consumption FAT]," & Environment.NewLine &
'              " round([Inhouse Consumption SNF],3) as [Inhouse Consumption SNF]," & Environment.NewLine &
'              " round([Other Out Qty],2) as [Other Out Qty]," & Environment.NewLine &
'              " round([Other Out FAT],3) as [Other Out FAT]," & Environment.NewLine &
'              " round([Other Out SNF],3) as [Other Out SNF]," & Environment.NewLine &
'              " (round(coalesce([Sale Qty],0),2)+round(coalesce([MCC Transfer Out Qty],0),2)+round(coalesce([Plant Transfer Out Qty],0),2)+round(coalesce([Inhouse Consumption Qty],0),2)+round(coalesce([Other Out Qty],0),2)) as [Total Out Qty]," & Environment.NewLine &
'              " (round(coalesce([Sale FAT],0),3)+round(coalesce([MCC Transfer Out FAT],0),3)+round(coalesce([Plant Transfer Out FAT],0),3)+round(coalesce([Inhouse Consumption FAT],0),3)+round(coalesce([Other Out FAT],0),3)) as [Total Out FAT]," & Environment.NewLine &
'              " (round(coalesce([Sale SNF],0),3)+round(coalesce([MCC Transfer Out SNF],0),3)+round(coalesce([Plant Transfer Out SNF],0),3)+round(coalesce([Inhouse Consumption SNF],0),3)+round(coalesce([Other Out SNF],0),3)) as [Total Out SNF]," & Environment.NewLine &
'              " round([In Transit Qty],2) as [In Transit Qty]," & Environment.NewLine &
'              " round([In Transit FAT],3) as [In Transit FAT]," & Environment.NewLine &
'              " round([In Transit SNF],3) as [In Transit SNF]," & Environment.NewLine &
'              " round(RunningBalanceQty,2) as [Closing Qty],round(RunningBalanceFat,3) as [Closing FAT],round(RunningBalanceSNF,3) as [Closing SNF], " & _
'              " round([Physical Stock Qty],2) as [Physical Stock Qty]," & Environment.NewLine &
'              " round([Physical Stock FAT],3) as [Physical Stock FAT]," & Environment.NewLine &
'              " round([Physical Stock SNF],3) as [Physical Stock SNF]," & Environment.NewLine &
'              " (round(RunningBalanceQty,2)-round([Physical Stock Qty],2)) as [Loss/Gain Qty]," & Environment.NewLine &
'              " (round(RunningBalanceFat,3)-round([Physical Stock FAT],3)) as [Loss/Gain FAT]," & Environment.NewLine &
'              " (round(RunningBalanceSNF,3)-round([Physical Stock SNF],3)) as [Loss/Gain SNF] " & Environment.NewLine &
'              " from ( " & Environment.NewLine &
'              " SELECT *,(coalesce([Opening Qty],0)+coalesce([Purchase Qty],0)+coalesce([MCC Transfer Received Qty],0)+coalesce([Other In Qty],0)+coalesce([Sale Qty],0)+coalesce([Plant Transfer Out Qty],0)+coalesce([MCC Transfer Out Qty],0)+coalesce([Inhouse Consumption Qty],0)+coalesce([Other Out Qty],0)) as BalanceQty," & Environment.NewLine &
'              " (coalesce([Opening FAT],0)+coalesce([Purchase FAT],0)+coalesce([MCC Transfer Received FAT],0)+coalesce([Other In FAT],0)+coalesce([Sale FAT],0)+coalesce([Plant Transfer Out FAT],0)+coalesce([MCC Transfer Out FAT],0)+coalesce([Inhouse Consumption FAT],0)+coalesce([Other Out FAT],0)) as BalanceFat," & Environment.NewLine &
'              " (coalesce([Opening SNF],0)+coalesce([Purchase SNF],0)+coalesce([MCC Transfer Received SNF],0)+coalesce([Other In SNF],0)+coalesce([Sale SNF],0)+coalesce([Plant Transfer Out SNF],0)+coalesce([MCC Transfer Out SNF],0)+coalesce([Inhouse Consumption SNF],0)+coalesce([Other Out SNF],0)) as BalanceSNF," & Environment.NewLine &
'              " sum(coalesce([Opening Qty],0)+coalesce([Purchase Qty],0)+coalesce([MCC Transfer Received Qty],0)+coalesce([Other In Qty],0)+coalesce([Sale Qty],0)+coalesce([MCC Transfer Out Qty],0)+coalesce([Plant Transfer Out Qty],0)+coalesce([Inhouse Consumption Qty],0)+coalesce([Other Out Qty],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceQty," & Environment.NewLine &
'              " sum(coalesce([Opening FAT],0)+coalesce([Purchase FAT],0)+coalesce([MCC Transfer Received FAT],0)+coalesce([Other In FAT],0)+coalesce([Sale FAT],0)+coalesce([MCC Transfer Out FAT],0)+coalesce([Plant Transfer Out FAT],0)+coalesce([Inhouse Consumption FAT],0)+coalesce([Other Out FAT],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceFat," & Environment.NewLine &
'              " sum(coalesce([Opening SNF],0)+coalesce([Purchase SNF],0)+coalesce([MCC Transfer Received SNF],0)+coalesce([Other In SNF],0)+coalesce([Sale SNF],0)+coalesce([MCC Transfer Out SNF],0)+coalesce([Plant Transfer Out SNF],0)+coalesce([Inhouse Consumption SNF],0)+coalesce([Other Out SNF],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceSNF " & Environment.NewLine &
'              " FROM ( " & Environment.NewLine &
'              " select Item_Code,Location_Code,Location_Desc," & transDateSel & ", " & Environment.NewLine &
'              " SUM([Opening Qty]) as [Opening Qty]," & Environment.NewLine &
'              " SUM([Opening FAT]) AS [Opening FAT]," & Environment.NewLine &
'              " SUM([Opening SNF]) AS [Opening SNF], " & Environment.NewLine &
'              " SUM([Purchase Qty Ltr]) as [Purchase Qty Ltr]," & Environment.NewLine &
'              " SUM([Purchase FAT Ltr]) AS [Purchase FAT Ltr], " & Environment.NewLine &
'              " SUM([Purchase SNF Ltr]) AS [Purchase SNF Ltr], " & Environment.NewLine &
'              " SUM([Purchase Qty]) as [Purchase Qty]," & Environment.NewLine &
'              " SUM([Purchase FAT]) AS [Purchase FAT], " & Environment.NewLine &
'              " SUM([Purchase SNF]) AS [Purchase SNF], " & Environment.NewLine &
'              " SUM([MCC Transfer Received Qty]) AS [MCC Transfer Received Qty], " & Environment.NewLine &
'              " SUM([MCC Transfer Received FAT]) AS [MCC Transfer Received FAT], " & Environment.NewLine &
'              " SUM([MCC Transfer Received SNF]) AS [MCC Transfer Received SNF], " & Environment.NewLine &
'              " SUM([Other In Qty]) AS [Other In Qty], " & Environment.NewLine &
'              " SUM([Other In FAT]) AS [Other In FAT], " & Environment.NewLine &
'              " SUM([Other In SNF]) AS [Other In SNF], " & Environment.NewLine &
'              " SUM([Sale Qty]) AS [Sale Qty], " & Environment.NewLine &
'              " SUM([Sale FAT]) AS [Sale FAT], " & Environment.NewLine &
'              " SUM([Sale SNF]) AS [Sale SNF], " & Environment.NewLine &
'              " SUM([MCC Transfer Out Qty]) AS [MCC Transfer Out Qty], " & Environment.NewLine &
'              " SUM([MCC Transfer Out FAT]) AS [MCC Transfer Out FAT], " & Environment.NewLine &
'              " SUM([MCC Transfer Out SNF]) AS [MCC Transfer Out SNF], " & Environment.NewLine &
'              " SUM([Plant Transfer Out Qty]) AS [Plant Transfer Out Qty], " & Environment.NewLine &
'              " SUM([Plant Transfer Out FAT]) AS [Plant Transfer Out FAT], " & Environment.NewLine &
'              " SUM([Plant Transfer Out SNF]) AS [Plant Transfer Out SNF], " & Environment.NewLine &
'              " SUM([Inhouse Consumption Qty]) AS [Inhouse Consumption Qty], " & Environment.NewLine &
'              " SUM([Inhouse Consumption FAT]) AS [Inhouse Consumption FAT], " & Environment.NewLine &
'              " SUM([Inhouse Consumption SNF]) AS [Inhouse Consumption SNF]," & Environment.NewLine &
'              " SUM([Other Out Qty]) AS [Other Out Qty], " & Environment.NewLine &
'              " SUM([Other Out FAT]) AS [Other Out FAT], " & Environment.NewLine &
'              " SUM([Other Out SNF]) AS [Other Out SNF], " & Environment.NewLine &
'              " SUM([In Transit Qty]) AS [In Transit Qty], " & Environment.NewLine &
'              " SUM([In Transit FAT]) AS [In Transit FAT], " & Environment.NewLine &
'              " SUM([In Transit SNF]) AS [In Transit SNF], " & Environment.NewLine &
'              " SUM([Physical Stock Qty]) AS [Physical Stock Qty], " & Environment.NewLine &
'              " SUM([Physical Stock FAT]) AS [Physical Stock FAT], " & Environment.NewLine &
'              " SUM([Physical Stock SNF]) AS [Physical Stock SNF], " & Environment.NewLine &
'              " row_number() over (partition by Item_Code,Location_Code order by Item_Code,Location_Code," & transDateOrder & ") as Tr_id from ( " & Environment.NewLine &
'              " select Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type+' FAT' as Trans_Type_Fat,Loc_Trans.Trans_Type+' FAT Ltr' as Trans_Type_Fat_Ltr,Loc_Trans.Trans_Type+' Qty' as Trans_Type_Qty,Loc_Trans.Trans_Type+' Qty Ltr' as Trans_Type_Qty_Ltr, " & Environment.NewLine &
'              " Loc_Trans.Trans_Type+' SNF' as Trans_Type_SNF,Loc_Trans.Trans_Type+' SNF Ltr' as Trans_Type_SNF_Ltr,max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcQty  else FatStockFinal.Qty end) as Qty,max(FatStockFinal.QtyLtr) as QtyLtr, " & Environment.NewLine &
'              " max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcFAT_KG else FatStockFinal.FAT_KG end) as FAT_KG,max(FatStockFinal.FAT_Ltr) as FAT_Ltr,max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcSNF_KG else FatStockFinal.SNF_KG end) as SNF_KG,max(FatStockFinal.SNF_Ltr) as SNF_Ltr from (" & Environment.NewLine &
'              " " & LocTransQry & " ) as Loc_Trans " & Environment.NewLine &
'              " inner join ( " & Environment.NewLine &
'              " " & StockUnionQry & " ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date " & Environment.NewLine &
'              " group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No " & Environment.NewLine &
'              " ) AS FatStockOuter " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(Qty) " & Environment.NewLine &
'              " FOR Trans_Type_Qty " & Environment.NewLine &
'              " IN ([Opening Qty], " & Environment.NewLine &
'              " [Purchase Qty], " & Environment.NewLine &
'              " [MCC Transfer Received Qty], " & Environment.NewLine &
'              " [Other In Qty], " & Environment.NewLine &
'              " [Sale Qty], " & Environment.NewLine &
'              " [MCC Transfer Out Qty], " & Environment.NewLine &
'              " [Plant Transfer Out Qty], " & Environment.NewLine &
'              " [Inhouse Consumption Qty], " & Environment.NewLine &
'              " [Other Out Qty], " & Environment.NewLine &
'              " [In Transit Qty], " & Environment.NewLine &
'              " [Physical Stock Qty]) " & Environment.NewLine &
'              " ) AS PIVQty " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(QtyLtr) " & Environment.NewLine &
'              " FOR Trans_Type_Qty_Ltr " & Environment.NewLine &
'              " IN ([Purchase Qty Ltr]) " & Environment.NewLine &
'              " ) AS PIVQtyLtr " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(Fat_Ltr) " & Environment.NewLine &
'              " FOR Trans_Type_Fat_Ltr " & Environment.NewLine &
'              " IN ( [Purchase Fat Ltr] ) " & Environment.NewLine &
'              " ) AS PIVFatLtr " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(SNF_Ltr) " & Environment.NewLine &
'              " FOR Trans_Type_SNF_Ltr " & Environment.NewLine &
'              " IN ( [Purchase SNF Ltr] ) " & Environment.NewLine &
'              " ) AS PIVSNFLtr " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(FAT_KG) " & Environment.NewLine &
'              " FOR Trans_Type_Fat " & Environment.NewLine &
'              " IN ([Opening FAT], " & Environment.NewLine &
'              " [Purchase FAT], " & Environment.NewLine &
'              " [MCC Transfer Received FAT], " & Environment.NewLine &
'              " [Other In FAT], " & Environment.NewLine &
'              " [Sale FAT], " & Environment.NewLine &
'              " [MCC Transfer Out FAT], " & Environment.NewLine &
'              " [Plant Transfer Out FAT], " & Environment.NewLine &
'              " [Inhouse Consumption FAT], " & Environment.NewLine &
'              " [Other Out FAT], " & Environment.NewLine &
'              " [In Transit FAT], " & Environment.NewLine &
'              " [Physical Stock FAT]) " & Environment.NewLine &
'              " ) AS PIVF " & Environment.NewLine &
'              " PIVOT " & Environment.NewLine &
'              " (  " & Environment.NewLine &
'              " max(SNF_KG) " & Environment.NewLine &
'              " FOR Trans_Type_SNF " & Environment.NewLine &
'              " IN ([Opening SNF], " & Environment.NewLine &
'              " [Purchase SNF], " & Environment.NewLine &
'              " [MCC Transfer Received SNF], " & Environment.NewLine &
'              " [Other In SNF], " & Environment.NewLine &
'              " [Sale SNF], " & Environment.NewLine &
'              " [MCC Transfer Out SNF], " & Environment.NewLine &
'              " [Plant Transfer Out SNF], " & Environment.NewLine &
'              " [Inhouse Consumption SNF], " & Environment.NewLine &
'              " [Other Out SNF], " & Environment.NewLine &
'              " [In Transit SNF], " & Environment.NewLine &
'              " [Physical Stock SNF]) " & Environment.NewLine &
'              " ) AS PIVS " & Environment.NewLine &
'              " GROUP BY Item_Code,Location_Code,Location_Desc," & transDateGroup & " " & Environment.NewLine &
'              " )AS FATSNFtockFinal) as Outermost "
'        If clsCommon.CompairString(objFilter.ReportType, "Summary") = CompairStringResult.Equal Then
'            Qry = "select Seq_No as [Seq No],Report_Type as [Report Type],round(sum(Qty),2) as [Quantity],round(sum(Fat_KG),3) as [FAT Kg],round(sum(SNF_KG),3) as [SNF KG] from (" & Environment.NewLine &
'              " select Loc_Trans.Location_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Seq_No,Loc_Trans.Trans_Type as Report_Type,FatStockFinal.Item_Code,FatStockFinal.Stock_UOM,FatStockFinal.Qty,FatStockFinal.FAT_KG,FatStockFinal.SNF_KG from (" & LocTransQry & " ) as Loc_Trans " & Environment.NewLine &
'              " left join ( " & Environment.NewLine &
'              " " & StockUnionQry & " ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date " & Environment.NewLine &
'              " ) AS FatStockOuter where Report_Type not in ('In Transit','Physical Stock') group by Seq_No,Report_Type order by Seq_No  "
'        End If
'        '' " group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No " & Environment.NewLine &

'        Return Qry
'    End Function

'    Public Shared Function GetQtyFatSNFBaseQryGK(ByVal objFilter As clsStockRecoFilters, ByVal is_Opening As Boolean, Optional ByVal ShowTankerNo As Boolean = False) As String
'        Dim Qry As String = ""
'        Dim QryCond As String = " where 2=2 "
'        Dim QryCondMilk As String = " where 2=2 "
'        Dim ReportTypeCol As String = ""
'        Dim Punching_DateCol As String = ""

'        Dim LocationFirstTime As Integer = 0
'        Dim LocationAddress As String = String.Empty
'        Dim strWhrCatg As String = ""
'        Dim strWhrCatgOldInv As String = ""
'        Dim strCondDisp As String = ""
'        Dim strCondPS As String = ""
'        If objFilter.SelectLocation = True Then
'            Dim IsApplicable As Boolean = False
'            For ii As Integer = 0 To objFilter.arrLocation.Count - 1
'                If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
'                    LocationFirstTime += 1
'                    If LocationFirstTime = 1 Then
'                        LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(objFilter.arrLocation(ii).Code) & "'")
'                    End If
'                    If IsApplicable Then
'                        strWhrCatg += " Or "
'                        strWhrCatgOldInv += " Or "
'                        strCondDisp += " Or "
'                        strCondPS += " Or "
'                    End If

'                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_INVENTORY_MOVEMENT_NEW.Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
'                    strWhrCatgOldInv += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_INVENTORY_MOVEMENT.Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
'                    strCondDisp += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_MCC_Dispatch_Challan.MCC_CODE else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
'                    strCondPS += " ((case when Is_Section='N' and Is_Sub_Location='N' then coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,TSPL_PHYSICAL_STOCK.Location) else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
'                    IsApplicable = True
'                    Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
'                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
'                        strWhrCatg += " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in ("
'                        strWhrCatgOldInv += " and TSPL_INVENTORY_MOVEMENT.Location_Code in ("
'                        strCondDisp += " and TSPL_MCC_Dispatch_Challan.MCC_CODE in ("
'                        strCondPS += " and (case when len(coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,''))>0 then TSPL_LOCATION_MASTER.Main_Location_Code else TSPL_PHYSICAL_STOCK.Location end) in ("
'                        Dim isFirstTime As Boolean = True
'                        For Each strInn As String In arr.Keys
'                            If Not isFirstTime Then
'                                strWhrCatg += ","
'                                strWhrCatgOldInv += ","
'                                strCondDisp += ","
'                                strCondPS += ","
'                            End If
'                            strWhrCatg += "'" + strInn + "'"
'                            strWhrCatgOldInv += "'" + strInn + "'"
'                            strCondDisp += "'" + strInn + "'"
'                            strCondPS += "'" + strInn + "'"
'                            isFirstTime = False
'                        Next
'                        strWhrCatg += ")"
'                        strWhrCatgOldInv += ")"
'                        strCondDisp += ")"
'                        strCondPS += ")"
'                    End If
'                End If
'            Next
'            If Not IsApplicable Then
'                Throw New Exception("Please select at least one location")
'            End If

'            'Else
'            '    If clsCommon.CompairString(objFilter.FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
'            '        strWhrCatg += "  (Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'))"
'            '    End If
'        End If
'        If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
'            If clsCommon.myLen(strWhrCatg) > 0 Then
'                strWhrCatg = strWhrCatg & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strWhrCatgOldInv = strWhrCatgOldInv & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strCondDisp = strCondDisp & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strCondPS = strCondPS & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'            Else
'                strWhrCatg = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strWhrCatgOldInv = " strWhrCatgOldInv in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strCondDisp = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'                strCondPS = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
'            End If
'        End If
'        If clsCommon.myLen(strWhrCatg) <= 0 Then
'            strWhrCatg = "2=2 "
'            strWhrCatgOldInv = "2=2 "
'            strCondDisp = "2=2 "
'            strCondPS = "2=2 "
'        End If

'        If is_Opening Then
'            'If IncludeSubLocation Then
'            '    QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"               
'            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
'            'Else
'            '    QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(arrItemCode) & ") and  TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"                
'            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and  TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(arrItemCode) & ") and  TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
'            'End If
'            QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and " & strWhrCatgOldInv & ""
'            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and " & strWhrCatg & ""

'            ReportTypeCol = "'Opening'"
'            Punching_DateCol = "cast('" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' as date)"
'        Else
'            'If IncludeSubLocation Then
'            '    QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"                
'            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
'            'Else
'            '    QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")  and TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
'            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
'            'End If
'            QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and  " & strWhrCatgOldInv & ""
'            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and " & strWhrCatg & " "
'            '' other in : ('IC-AD','MJ-SR', 'M-PURRETURN','NRGP','Purchase Return','RGP','SRN')
'            ReportTypeCol = "(Case when Final.Trans_Type in ('BulkSRN','BulkSRNTrade','MCC-MSRN','BulkSRNRet','M-PURRETURN') " & Environment.NewLine &
'              " then 'Purchase' " & Environment.NewLine &
'              " when Final.Trans_Type in ('DispChallan-RET','MilkTransferIn')  then 'MCC Transfer Received' " & Environment.NewLine &
'              " when Final.Trans_Type in ('Transfer','TRN-RET') and Final.Inout='I' then 'Other In' " & Environment.NewLine &
'              " when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS') then 'Sale' " & Environment.NewLine &
'              " when Final.Trans_Type in ('DispChallan')  then (case when LocO.Location_Category='MCC' then 'MCC Transfer Out' else 'Plant Transfer Out' end)  " & Environment.NewLine &
'              " when Final.Trans_Type in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTR_WB','PROD_ENTRY') and Final.Inout='O' then 'Inhouse Consumption' " & _
'              " when Final.Trans_Type in ('In Transit')  then 'In Transit' " & _
'              " when Final.Trans_Type in ('Physical Stock')  then 'Physical Stock' " & _
'              " when Final.Inout='I' then 'Other In' when Final.Inout='O' then 'Other Out' end)"
'            Punching_DateCol = "Punching_Date "
'        End If
'        QryCond = QryCond & " and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0 "
'        QryCondMilk = QryCondMilk & "and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0 "

'        '" select 1 as Inv_Type,'MP' as Product_Type,Trans_Type,InOut,TSPL_INVENTORY_MOVEMENT.Location_Code as Location_Code,Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM,TSPL_INVENTORY_MOVEMENT.Net_Cost,Avg_Cost, " & Environment.NewLine &
'        '      " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date,Other_Location_Code " & If(ShowTankerNo = True, ",null as Tanker_No", "") & "  " & Environment.NewLine &
'        '      " from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   " & QryCond & " " & Environment.NewLine &
'        '      " union all " & Environment.NewLine &
'        Qry = " select Item_Code,Stock_UOM,Location_Code,Report_Type," & Punching_DateCol & " as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty,sum(case when InOut='I' then Stock_Qty_Ltr*Inv_Type else -Stock_Qty_Ltr*Inv_Type end) as QtyLtr,sum(case when InOut='I' then coalesce(FAT_Kg,0)*Inv_Type else -coalesce(FAT_Kg,0)*Inv_Type end) as FAT_KG,sum(case when InOut='I' then coalesce(FAT_Ltr,0)*Inv_Type else -coalesce(FAT_Ltr,0)*Inv_Type end) as FAT_Ltr," & Environment.NewLine &
'              " sum(case when InOut='I' then coalesce(SNF_Kg,0)*Inv_Type else -coalesce(SNF_Kg,0)*Inv_Type end) as SNF_KG,sum(case when InOut='I' then coalesce(SNF_Ltr,0)*Inv_Type else -coalesce(SNF_Ltr,0)*Inv_Type end) as SNF_Ltr,sum(case when InOut='I' then (case when Inv_Type=0 then Stock_Qty else 0 end) else -(case when Inv_Type=0 then Stock_Qty else 0 end) end) as NotInCalcQty,sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(FAT_Kg,0) else 0 end) else -(case when Inv_Type=0 then coalesce(FAT_Kg,0) else 0 end) end) as NotInCalcFAT_KG," & Environment.NewLine &
'              " sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(SNF_KG,0) else 0 end) else -(case when Inv_Type=0 then coalesce(SNF_KG,0) else 0 end) end) as NotInCalcSNF_KG,sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(Net_Cost,0) else 0 end) else -(case when Inv_Type=0 then coalesce(Net_Cost,0) else 0 end) end) as NotInCalcNetCost " & If(ShowTankerNo = True, ",sum(case when InOut='I' then Net_Cost*Inv_Type else -Net_Cost*Inv_Type end) as Net_Cost," & If(is_Opening = True, "null as Tanker_No", "Tanker_No"), "") & " " & Environment.NewLine &
'              " from ( " & Environment.NewLine &
'              " select Final.Inv_Type,Final.Product_Type,Final.Trans_Type," & ReportTypeCol & " as Report_Type, " & Environment.NewLine &
'              " Final.InOut,Final.Location_Code,Loc.Location_Category,Final.Item_Code, " & Environment.NewLine &
'              " Final.Stock_Qty,(case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as Float) end) as Stock_Qty_Ltr,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, " & Environment.NewLine &
'              " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
'              " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
'              " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as FAT_Kg," & Environment.NewLine &
'              " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as SNF_Kg," & Environment.NewLine &
'              " (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Final.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)*100) as Float) end) as FAT_Ltr," & Environment.NewLine &
'              " (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Final.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)*100) as Float) end) as SNF_Ltr," & Environment.NewLine &
'              " Punching_Date " & If(ShowTankerNo = True, ",Tanker_No", "") & " " & Environment.NewLine &
'              " from ( " & Environment.NewLine &
'              " select  1 as Inv_Type,'MI' as Product_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
'              " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date,Other_Location_Code " & If(ShowTankerNo = True, ",Doc.Tanker_No", "") & " " & Environment.NewLine &
'              " from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code "
'        If ShowTankerNo Then
'            Qry = Qry & " left join (select Chalan_NO as Document_No,Tanker_No,'DispChallan' as Trans_Type from TSPL_MCC_Dispatch_Challan " & _
'                        " union all " & _
'                        " select DocR.Document_No,doc.Tanker_No,'DispChallanRet' as Trans_Type " & _
'                        " from TSPL_MCC_DISPATCH_CHALLAN_RETURN DocR " & _
'                        " inner join TSPL_MCC_Dispatch_Challan Doc on DocR.Challan_No=Doc.Chalan_NO " & _
'                        " union all " + _
'                        " select SRN_NO as Document_No,Tanker_No,'BulkSRN' as Trans_Type from TSPL_Bulk_MILK_SRN" + _
'                        " union all " + _
'                        " select DocR.SRN_Return_NO as Document_No,doc.Tanker_No,'BulkSRNRet' as Trans_Type from TSPL_Bulk_Milk_SRN_Return DocR " + _
'                        " inner join TSPL_Bulk_MILK_SRN Doc on DocR.SRN_NO=doc.SRN_NO " + _
'                        " union all" + _
'                        " select Doc.Receipt_Challan_No as Document_No,Docc.Tanker_No,'MilkTransferIn' as Trans_Type from TSPL_MILK_TRANSFER_IN Doc " + _
'                        " inner join TSPL_MCC_Dispatch_Challan DocC on Doc.Dispatch_Challan_No=DocC.Chalan_NO" + _
'                        " union all " + _
'                        " select doc.Receipt_Challan_Return_No as Document_No,DocC.Tanker_No,'MilkTransferInReturn' as Trans_Type from TSPL_MILK_TRANSFER_IN_RETURN Doc " + _
'                        " inner join TSPL_MCC_Dispatch_Challan DocC on Doc.Dispatch_Challan_No=DocC.Chalan_NO" + _
'                        " union all " + _
'                        " select TSPL_CAN_SALE_HEAD.Document_No as Document_No,'' as Tanker_No,'DisCanSale' as Trans_Type from TSPL_CAN_SALE_DETAIL " + _
'                        " inner join TSPL_CAN_SALE_HEAD on TSPL_CAN_SALE_DETAIL.Document_No=TSPL_CAN_SALE_HEAD.Document_No " + _
'                        " group by TSPL_CAN_SALE_HEAD.Document_No " + _
'                        " union all " + _
'                        " Select Document_No as Document_No,Tanker_Code,'DispatchBS' as Trans_Type from TSPL_Dispatch_BulkSale " + _
'                        " union all " + _
'                        " Select Document_No as Document_No,Tanker_No,'SALERETURNBS' as Trans_Type from TSPL_SALE_RETURN_MASTER_BULKSALE " + _
'                        " ) as Doc on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=Doc.Document_No and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type=Doc.Trans_Type "
'        End If
'        Qry = Qry & " " & QryCondMilk & " " & Environment.NewLine
'        Qry = Qry & " Union All " & Environment.NewLine &
'              " select 0 as Inv_Type,'MI' as Product_Type,'In Transit' as Trans_Type,'O' as Inout, (case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_MCC_Dispatch_Challan.MCC_CODE end)  as Location_Code,Chalan_NO as Source_Doc_No,Item_Code," & _
'              " Net_Qty as Stock_Qty,UOM_Code as Stock_Uom,Amount as Net_Cost,Avg_Amount as Avg_Cost,FAT_R as Fat_Per,SNF_R as SNF_Per,FAT_KG,SNF_KG ,cast(Dispatch_Date as Date) as Punching_Date,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as Other_Location_Code" & If(ShowTankerNo = True, ",null as Tanker_No", "") & " " & _
'              " from TSPL_MCC_Dispatch_Challan left join TSPL_LOCATION_MASTER  on TSPL_MCC_Dispatch_Challan.MCC_Code=TSPL_LOCATION_MASTER.Location_Code  where 2=2 and cast(Dispatch_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "'  and " & strCondDisp & " " & Environment.NewLine &
'              " union all " & Environment.NewLine &
'              " select 0 as Inv_Type,'MI' as Product_Type,'Physical Stock' as Trans_Type,'I' as Inout, " & _
'              " (case when len(coalesce(Silo_Location,''))>0 then TSPL_LOCATION_MASTER.Main_Location_Code else Location end) as Location_Code,Physical_No as Source_Doc_No,Item_Code," & _
'              " Physical_Qty as Stock_Qty,Stock_Unit as Stock_Uom,0 as Net_Cost,0 as Avg_Cost,FAT_Pers as Fat_Per,SNF_Pers as SNF_Per,FAT_Kg,SNF_Kg ,cast(Stock_Date as Date) as Punching_Date,null as Other_Location_Code" & If(ShowTankerNo = True, ",null as Tanker_No", "") & " " & _
'              " from TSPL_PHYSICAL_STOCK  left join TSPL_LOCATION_MASTER  on coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,TSPL_PHYSICAL_STOCK.Location)=TSPL_LOCATION_MASTER.Location_Code where TSPL_PHYSICAL_STOCK.Is_Milk=1 and cast(TSPL_PHYSICAL_STOCK.Stock_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and " & strCondPS & " " & Environment.NewLine &
'              " ) as Final " & Environment.NewLine &
'              " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
'              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
'              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code  " & Environment.NewLine &
'              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on Final.Item_Code=StockLtr.Item_Code  " & Environment.NewLine &
'              " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
'              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
'              " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
'              " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
'              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
'              " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
'              " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code " & Environment.NewLine &
'              " left join TSPL_LOCATION_MASTER LocO on Final.Other_Location_Code=LocO.Location_Code where 2=2 " & Environment.NewLine &
'              " AND ( COALESCE(Item_Fat.Fat_Per,0)<>0 OR COALESCE(Item_SNF.SNF_Per,0) <>0) " & Environment.NewLine &
'              " /*and Trans_Type not in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTRY','PROD_WR','Prod-Scrap') */" & Environment.NewLine &
'              " ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code" & IIf(is_Opening = True, "", ",Punching_Date") & " " & If(ShowTankerNo = True And is_Opening = False, ",Tanker_No", "") & ""
'        Return Qry
'    End Function

'End Class
