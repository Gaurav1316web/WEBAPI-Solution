Imports common
Public Class rptmilkunion
    Inherits FrmMainTranScreen
#Region "Variables"
    Const sno As String = "Colsno"
    Const colunion As String = "ColUnion"
    Const colDisqty As String = "colDisqty"
    Const coldisfatkg As String = "coldisfatkg"
    Const coldissnf As String = "coldissnf"
    Const coldmdqty As String = "coldmdqty"
    Const coldmdfat As String = "coldmdfat"
    Const coldmdsnf As String = "coldmdsnf"
    Const colprocqty As String = "colprocqty"
    Const colprocfat As String = "colprocfat"
    Const colprocsnf As String = "colprocsnf"
    Const colprodqty As String = "colprodqty"
    Const colprodfat As String = "colprodfat"
    Const colprodsnf As String = "colprodsnf"
#End Region
    Private Sub rptmilkunion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",'" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,sum(final.Dis_QtyInLTR)Dis_QtyInLTR  ,sum(final.Dis_FATKG)Dis_FATKG,sum(final.Dis_SNFKG)Dis_SNFKG,sum(final.Prod_QTY)Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(final.Prod_SNFkg)Prod_SNFkg,sum(final.TotalLtr_ItemWiseDemand)TotalLtr_ItemWiseDemand,sum(final.FATKGDemand)FATKGDemand ,  sum(final.SNFKGDemand)SNFKGDemand,sum(final.Milk_WeightProc)Milk_WeightProc,sum(final.FATKGProc)FATKGProc,sum(final.SNFKGProc)SNFKGProc,final.Document_Date from (
                             select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(Dis_FATKG)Dis_FATKG,sum(Dis_SNFKG)Dis_SNFKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) as Document_Date from (
                            select 
                            CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  ELSE 0 END AS Dis_QtyInLTR,isnull((qty*std_fatPer/100),0) Dis_FATKG,isnull((qty*STD_SNFPer/100),0) Dis_SNFKG,convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)Document_Date
                             from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_SD_SHIPMENT_DETAIL 
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                            left join (select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                            left join (select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left join ( select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.IsTaxable=0 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Status=1 )xxx  group by convert(date,Document_Date,103)
"
                    'Query += ",(select COUNT(MP_Name) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL where Document_Code='" + docNo + "') As [No Of Farmer] "
                    'Query += ",(select count(Jan_Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Jan_Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Jan Aadhar Verified No]"
                    'Query += ",(select count(Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Addhar Verified]"
                    'Query += ",(select convert(varchar, From_Date,103) + ' - '+ convert(varchar, To_Date,103)  FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where Document_Code='" + docNo + "'  ) AS [Last DBT Cycle]"
                    query += "union all
                        select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG ,sum(RECEIPT_QTY)Prod_QTY,sum(fatkg)Prod_FATkg,sum(SNFkg)Prod_SNFkg, 0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,PROD_DATE,103)PROD_DATE  from (select isnull(sum(RECEIPT_QTY),0)RECEIPT_QTY,PROD_DATE ,isnull(sum(RECEIPT_QTY*[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.STD_FatPer/100),0) as fatkg,isnull(sum(RECEIPT_QTY*STD_SNFPer/100),0) as SNFkg from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                        where convert(date,PROD_DATE,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "'and convert(date,PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.POSTED=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem=1 and IsTaxable=0 group by PROD_DATE)final  group by PROD_DATE-----production
                        union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,sum(TotalLtr_ItemWise)TotalLtr_ItemWiseDemand,sum(FATKG)FATKGDemand,sum(SNFKG)SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) Document_Date from 
                         (select isnull(sum(TotalLtr_ItemWise),0)TotalLtr_ItemWise,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_FatPer,0)/100) as FATKG,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_SNFPer,0)/100) as SNFKG ,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                         where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 and Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Posted=1
                         group by Document_Date)xxx  group by convert(date,Document_Date,103)
                         union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand, sum(xxx.milk_weight) as Milk_WeightProc,SUM(XXX.fatkg)FATKGProc ,SUM(XXX.SNFKG)SNFKGProc,Document_Date
                        from (select sum(Milk_Weight)Milk_Weight,sum(fatkg)fatkg,sum(snfkg)snfkg,Document_Date from (SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date
                        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        where  convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1)final
                        group by  final.Document_Date
                        union all
                        select sum(Milk_Weight)Milk_Weight,sum([FATKg])[FATKg],sum([SNFKg])[SNFKg],document_date from(
                        select isnull(Milk_Weight,0) as Milk_Weight, cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],FAT,SNF,cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,Shift_Date,103) as document_date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No where convert(date,Shift_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Shift_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 )final group by document_date
                        union all
                        select ISNULL(sum(QTY),0)QTY,ISNULL(sum(FATKG),0)FATKG,ISNULL(sum(SNFKG),0)SNFKG,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where Status=0 and  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' group by Document_Date)xxx group by Document_Date
                        ) final
                        group by Document_Date"
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "milkunionreport", "")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormat1()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = False

        gv1.Columns("Union Name").Name = "Union Name"
        gv1.Columns("Union Name").IsVisible = True

        'gv1.Columns("Dispatch QTY").Name = "Dispatch QTY"
        'gv1.Columns("Dispatch QTY").IsVisible = True
        'gv1.Columns("Dispatch QTY").FormatString = ""

        'gv1.Columns("Dispatch FATKG").Name = "Dispatch FATKG"
        'gv1.Columns("Dispatch FATKG").IsVisible = True
        'gv1.Columns("Dispatch FATKG").FormatString = "{0:n2}"

        'gv1.Columns("Dispatch SNFKG").Name = " Dispatch SNFKG"
        'gv1.Columns("Dispatch SNFKG").IsVisible = True
        'gv1.Columns("Dispatch SNFKG").FormatString = "{0:n2}"


        'gv1.Columns("Production QTY").Name = "Production QTY"
        'gv1.Columns("Production QTY").IsVisible = True
        'gv1.Columns("Production QTY").FormatString = ""

        'gv1.Columns("Production FATKG").Name = " Production FATKG"
        'gv1.Columns("Production FATKG").IsVisible = True
        'gv1.Columns("Production FATKG").FormatString = "{0:n2}"

        'gv1.Columns("Production SNFKG").Name = "Production SNFKG"
        'gv1.Columns("Production SNFKG").IsVisible = True
        'gv1.Columns("Production SNFKG").FormatString = "{0:n2}"

        'gv1.Columns("Demand QTY").Name = "Demand QTY"
        'gv1.Columns("Demand QTY").IsVisible = True
        'gv1.Columns("Demand QTY").FormatString = ""

        'gv1.Columns("Demand FATKG").Name = "Demand FATKG"
        'gv1.Columns("Demand FATKG").IsVisible = True
        'gv1.Columns("Demand FATKG").FormatString = "{0:n2}"

        'gv1.Columns("Demand SNFKG").Name = "SNFKG"
        'gv1.Columns("Demand SNFKG").IsVisible = True
        'gv1.Columns("Demand SNFKG").FormatString = "{0:n2}"


        'gv1.Columns("Procurement QTY").Name = "Procurement QTY"
        'gv1.Columns("Procurement QTY").IsVisible = True
        'gv1.Columns("Procurement QTY").FormatString = "{0:n2}"

        'gv1.Columns("Procurement FATKG").Name = "Procurement FATKG"
        'gv1.Columns("Procurement FATKG").IsVisible = True
        'gv1.Columns("Procurement FATKG").FormatString = "{0:n2}"

        'gv1.Columns("Procurement SNFKG").Name = "Procurement SNFKG"
        'gv1.Columns("Procurement SNFKG").IsVisible = True
        'gv1.Columns("Procurement SNFKG").FormatString = "{0:n2}"



        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        'View()
        'Gv1.ShowGroupPanel = False
        'Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Sub SetGridFormat()

        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()

        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Union Name"
        repoLineNo.Name = colunion
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoEMPCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEMPCode.FormatString = ""
        repoEMPCode.HeaderText = "QTY"
        repoEMPCode.Name = colprocqty
        repoEMPCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoEMPCode.Width = 90
        gv1.MasterTemplate.Columns.Add(repoEMPCode)

        Dim repoEMPDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEMPDesc.FormatString = ""
        repoEMPDesc.HeaderText = "FATKG"
        repoEMPDesc.Name = colprocfat
        repoEMPDesc.Width = 120
        repoEMPDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEMPDesc)

        Dim repoActual As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActual.FormatString = ""
        repoActual.HeaderText = "SNFKG"
        repoActual.Name = colprocsnf
        repoActual.Width = 120
        repoActual.ReadOnly = False
        repoActual.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoActual)

        Dim repoheadVAlue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoheadVAlue.FormatString = ""
        repoheadVAlue.HeaderText = "QTY"
        repoheadVAlue.Name = colprodqty
        repoheadVAlue.Width = 120
        repoheadVAlue.ReadOnly = False
        repoheadVAlue.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoheadVAlue)

        Dim repoCEPFAC1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCEPFAC1.FormatString = ""
        repoCEPFAC1.HeaderText = "FATKG"
        repoCEPFAC1.Name = colprodfat
        repoCEPFAC1.Width = 120
        repoCEPFAC1.ReadOnly = False
        repoCEPFAC1.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCEPFAC1)

        Dim repoCEPSAC110 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCEPSAC110.FormatString = ""
        repoCEPSAC110.HeaderText = "SNFKG"
        repoCEPSAC110.Name = colprodsnf
        repoCEPSAC110.Width = 120
        repoCEPSAC110.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCEPSAC110.ReadOnly = False
        repoCEPSAC110.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCEPSAC110)

        Dim repoADCEPFAC02 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoADCEPFAC02.FormatString = ""
        repoADCEPFAC02.HeaderText = "QTY"
        repoADCEPFAC02.Name = coldmdqty
        repoADCEPFAC02.Width = 120
        repoADCEPFAC02.TextImageRelation = TextImageRelation.TextBeforeImage
        repoADCEPFAC02.ReadOnly = False
        repoADCEPFAC02.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoADCEPFAC02)

        Dim repoA As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoA.FormatString = ""
        repoA.HeaderText = "FATKG"
        repoA.Name = coldmdfat
        repoA.Width = 120
        repoA.TextImageRelation = TextImageRelation.TextBeforeImage
        repoA.ReadOnly = False
        repoA.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoA)


        Dim repoAD As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAD.FormatString = ""
        repoAD.HeaderText = "SNFKG"
        repoAD.Name = coldmdsnf
        repoAD.Width = 120
        repoAD.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAD.ReadOnly = False
        repoAD.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAD)

        Dim repodis As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodis.FormatString = ""
        repodis.HeaderText = "QTY"
        repodis.Name = colDisqty
        repodis.Width = 120
        repodis.TextImageRelation = TextImageRelation.TextBeforeImage
        repodis.ReadOnly = False
        repodis.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repodis)

        Dim repodisfat As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodisfat.FormatString = ""
        repodisfat.HeaderText = "FATKG"
        repodisfat.Name = coldisfatkg
        repodisfat.Width = 120
        repodisfat.TextImageRelation = TextImageRelation.TextBeforeImage
        repodisfat.ReadOnly = False
        repodisfat.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repodisfat)

        Dim repoDISSNF As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDISSNF.FormatString = ""
        repoDISSNF.HeaderText = "SNFKG"
        repoDISSNF.Name = coldissnf
        repoDISSNF.Width = 120
        repoDISSNF.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDISSNF.ReadOnly = False
        repoDISSNF.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDISSNF)
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True
        'ReStoreGridLayout()
        View()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(" Milk Procurement"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk_WeightProc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FATKGProc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGProc").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Prod_QTY").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Prod_FATkg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Prod_SNFkg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Demand"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("TotalLtr_ItemWiseDemand").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("FATKGDemand").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGDemand").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Dis_QtyInLTR").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Dis_FATKG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Dis_SNFKG").Name)

            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Griddata()
    End Sub
    Public Sub Griddata()
        Try
            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",sum(final.Dis_QtyInLTR) as [Dispatch QTY]  ,sum(final.Dis_FATKG) as [Dispatch FATKG],sum(final.Dis_SNFKG) AS [Dispatch SNFKG],sum(final.Prod_QTY) as [Production QTY],sum(Prod_FATkg) AS [Production FATKG],sum(final.Prod_SNFkg) AS [Production SNFKG] ,sum(final.TotalLtr_ItemWiseDemand) AS [Demand QTY],sum(final.FATKGDemand) as [Demand FATKG] ,  sum(final.SNFKGDemand) AS [Demand SNFKG],sum(final.Milk_WeightProc) as [Procurement QTY],sum(final.FATKGProc)[Procurement FATKG],sum(final.SNFKGProc)[Procurement SNFKG],final.Document_Date from (
                             select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(Dis_FATKG)Dis_FATKG,sum(Dis_SNFKG)Dis_SNFKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) as Document_Date from (
                            select 
                            CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  ELSE 0 END AS Dis_QtyInLTR,isnull((qty*std_fatPer/100),0) Dis_FATKG,isnull((qty*STD_SNFPer/100),0) Dis_SNFKG,convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)Document_Date
                             from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_SD_SHIPMENT_DETAIL 
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                            left join (select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                            left join (select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left join ( select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.IsTaxable=0 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Status=1 )xxx  group by convert(date,Document_Date,103)
"
                    'Query += ",(select COUNT(MP_Name) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL where Document_Code='" + docNo + "') As [No Of Farmer] "
                    'Query += ",(select count(Jan_Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Jan_Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Jan Aadhar Verified No]"
                    'Query += ",(select count(Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Addhar Verified]"
                    'Query += ",(select convert(varchar, From_Date,103) + ' - '+ convert(varchar, To_Date,103)  FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where Document_Code='" + docNo + "'  ) AS [Last DBT Cycle]"
                    query += "union all
                        select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG ,sum(RECEIPT_QTY)Prod_QTY,sum(fatkg)Prod_FATkg,sum(SNFkg)Prod_SNFkg, 0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,PROD_DATE,103)PROD_DATE  from (select isnull(sum(RECEIPT_QTY),0)RECEIPT_QTY,PROD_DATE ,isnull(sum(RECEIPT_QTY*[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.STD_FatPer/100),0) as fatkg,isnull(sum(RECEIPT_QTY*STD_SNFPer/100),0) as SNFkg from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                        where convert(date,PROD_DATE,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "'and convert(date,PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.POSTED=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem=1 and IsTaxable=0 group by PROD_DATE)final  group by PROD_DATE-----production
                        union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,sum(TotalLtr_ItemWise)TotalLtr_ItemWiseDemand,sum(FATKG)FATKGDemand,sum(SNFKG)SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) Document_Date from 
                         (select isnull(sum(TotalLtr_ItemWise),0)TotalLtr_ItemWise,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_FatPer,0)/100) as FATKG,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_SNFPer,0)/100) as SNFKG ,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                         where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 and Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Posted=1
                         group by Document_Date)xxx  group by convert(date,Document_Date,103)
                         union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand, sum(xxx.milk_weight) as Milk_WeightProc,SUM(XXX.fatkg)FATKGProc ,SUM(XXX.SNFKG)SNFKGProc,Document_Date
                        from (select sum(Milk_Weight)Milk_Weight,sum(fatkg)fatkg,sum(snfkg)snfkg,Document_Date from (SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date
                        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        where  convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1)final
                        group by  final.Document_Date
                        union all
                        select sum(Milk_Weight)Milk_Weight,sum([FATKg])[FATKg],sum([SNFKg])[SNFKg],document_date from(
                        select isnull(Milk_Weight,0) as Milk_Weight, cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],FAT,SNF,cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,Shift_Date,103) as document_date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No where convert(date,Shift_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Shift_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 )final group by document_date
                        union all
                        select ISNULL(sum(QTY),0)QTY,ISNULL(sum(FATKG),0)FATKG,ISNULL(sum(SNFKG),0)SNFKG,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where Status=0 and  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' group by Document_Date)xxx group by Document_Date
                        ) final
                        group by Document_Date"
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    'Gv1.Rows.Add()
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
        btngo.Enabled = True
    End Sub
End Class