Imports common
Public Class rptmilkunion
    Inherits FrmMainTranScreen
    Private Sub rptmilkunion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtdate.Value = previousDayString

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
            Query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        Query += " UNION ALL "
                    End If
                    Query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",sum(final.Dis_QtyInLTR)Dis_QtyInLTR  ,sum(final.Dis_FATKG)Dis_FATKG,sum(final.Dis_SNFKG)Dis_SNFKG,sum(final.Prod_QTY)Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(final.Prod_SNFkg)Prod_SNFkg,sum(final.TotalLtr_ItemWiseDemand)TotalLtr_ItemWiseDemand,sum(final.FATKGDemand)FATKGDemand ,  sum(final.SNFKGDemand)SNFKGDemand,sum(final.Milk_WeightProc)Milk_WeightProc,sum(final.FATKGProc)FATKGProc,sum(final.SNFKGProc)SNFKGProc,final.Document_Date from (
                             select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(Dis_FATKG)Dis_FATKG,sum(Dis_SNFKG)Dis_SNFKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) as Document_Date from (
                            select 
                            CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  ELSE 0 END AS Dis_QtyInLTR,isnull((qty*std_fatPer/100),0) Dis_FATKG,isnull((qty*STD_SNFPer/100),0) Dis_SNFKG,convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)Document_Date
                             from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_SD_SHIPMENT_DETAIL 
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                            left join (select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                            left join (select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left join ( select Conversion_factor,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.IsTaxable=0 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Status=1 )xxx  group by convert(date,Document_Date,103)
"
                    'Query += ",(select COUNT(MP_Name) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL where Document_Code='" + docNo + "') As [No Of Farmer] "
                    'Query += ",(select count(Jan_Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Jan_Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Jan Aadhar Verified No]"
                    'Query += ",(select count(Aadhar_No_Verified) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code_VLC_Uploader where Aadhar_No_Verified=1 and Document_Code='" + docNo + "') As [Addhar Verified]"
                    'Query += ",(select convert(varchar, From_Date,103) + ' - '+ convert(varchar, To_Date,103)  FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where Document_Code='" + docNo + "'  ) AS [Last DBT Cycle]"
                    query += "union all
                        select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG ,sum(RECEIPT_QTY)Prod_QTY,sum(fatkg)Prod_FATkg,sum(SNFkg)Prod_SNFkg, 0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,PROD_DATE,103)PROD_DATE  from (select isnull(sum(RECEIPT_QTY),0)RECEIPT_QTY,PROD_DATE ,isnull(sum(RECEIPT_QTY*[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.STD_FatPer/100),0) as fatkg,isnull(sum(RECEIPT_QTY*STD_SNFPer/100),0) as SNFkg from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                        where convert(date,PROD_DATE,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.POSTED=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem=1 and IsTaxable=0 group by PROD_DATE)final  group by PROD_DATE-----production
                        union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,sum(TotalLtr_ItemWise)TotalLtr_ItemWiseDemand,sum(FATKG)FATKGDemand,sum(SNFKG)SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc,convert(date,Document_Date,103) Document_Date from 
                         (select isnull(sum(TotalLtr_ItemWise),0)TotalLtr_ItemWise,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_FatPer,0)/100) as FATKG,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_SNFPer,0)/100) as SNFKG ,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                         where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_Date,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 and Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Posted=1
                         group by Document_Date)xxx  group by convert(date,Document_Date,103)
                         union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand, sum(xxx.milk_weight) as Milk_WeightProc,SUM(XXX.fatkg)FATKGProc ,SUM(XXX.SNFKG)SNFKGProc,Document_Date
                        from (select sum(Milk_Weight)Milk_Weight,sum(fatkg)fatkg,sum(snfkg)snfkg,Document_Date from (SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date
                        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        where  convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1)final
                        group by  final.Document_Date
                        union all
                        select sum(Milk_Weight)Milk_Weight,sum([FATKg])[FATKg],sum([SNFKg])[SNFKg],document_date from(
                        select isnull(Milk_Weight,0) as Milk_Weight, cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],FAT,SNF,cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,Shift_Date,103) as document_date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No where convert(date,Shift_Date,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 )final group by document_date
                        union all
                        select ISNULL(sum(QTY),0)QTY,ISNULL(sum(FATKG),0)FATKG,ISNULL(sum(SNFKG),0)SNFKG,Document_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where Status=0 and  convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(txtdate.Value) + "' group by Document_Date)xxx group by Document_Date
                        ) final
                        group by Document_Date"
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Query)
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

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.close()
    End Sub
End Class