Imports common
Public Class rptmilkunion
    Inherits FrmMainTranScreen
    Private Sub rptmilkunion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
        rdbPosted.Checked = True
        rdbUnposted.Checked = False
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
                    Dim status As String
                    If rdbPosted.Checked Then
                        status = 1
                    Else
                        status = 0
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",'" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,isnull(sum(final.Dis_QtyInLTR),0)Dis_QtyInLTR  ,sum(final.Dis_FATKG)Dis_FATKG,sum(final.Dis_SNFKG)Dis_SNFKG,sum(final.Prod_QTY)Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(final.Prod_SNFkg)Prod_SNFkg,sum(final.TotalLtr_ItemWiseDemand)TotalLtr_ItemWiseDemand,sum(final.FATKGDemand)FATKGDemand ,  sum(final.SNFKGDemand)SNFKGDemand,sum(final.Milk_WeightProc)Milk_WeightProc,sum(final.FATKGProc)FATKGProc,sum(final.SNFKGProc)SNFKGProc from (
                             select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(Dis_FATKG)Dis_FATKG,sum(Dis_SNFKG)Dis_SNFKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc from (select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(isnull((Dis_QtyInLTR*std_fatPer/100),0)) Dis_FATKG,sum(isnull((Dis_QtyInLTR*STD_SNFPer/100),0))Dis_snfKG  from   (select 
                            CASE WHEN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor WHEN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  ELSE 0 END AS Dis_QtyInLTR,isnull((qty*std_fatPer/100),0) Dis_FATKG,isnull((qty*STD_SNFPer/100),0) Dis_SNFKG,TSPL_SD_SHIPMENT_DETAIL.Item_Code,STD_FatPer,STD_SNFPer,qty
                             from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_SD_SHIPMENT_DETAIL 
                            left outer join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Item_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left outer join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                            left join (select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                            left join (select Conversion_factor,  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left join ( select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.IsTaxable=0 and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Is_FreshItem=1 and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Status=" + status + " )yyy)xxxx
                         union all
                        select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG ,sum(RECEIPT_QTY)Prod_QTY,sum(fatkg)Prod_FATkg,sum(SNFkg)Prod_SNFkg, 0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc  from (select isnull(sum(RECEIPT_QTY),0)RECEIPT_QTY,isnull(sum(RECEIPT_QTY*[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.STD_FatPer/100),0) as fatkg,isnull(sum(RECEIPT_QTY*STD_SNFPer/100),0) as SNFkg from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                        where convert(date,PROD_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "'and convert(date,PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.POSTED=" + status + " and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem=1 and IsTaxable=0)final  -----production
                        union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,sum(TotalLtr_ItemWise)TotalLtr_ItemWiseDemand,sum(FATKG)FATKGDemand,sum(SNFKG)SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc from 
                         (select isnull(sum(TotalLtr_ItemWise),0)TotalLtr_ItemWise,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_FatPer,0)/100) as FATKG,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_SNFPer,0)/100) as SNFKG  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                         where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 and Is_FreshItem=1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Posted=" + status + "
                        )xxx 
                         union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand, sum(xxx.milk_weight) as Milk_WeightProc,SUM(XXX.fatkg)FATKGProc ,SUM(XXX.SNFKG)SNFKGProc
                        from 
						(select sum(Milk_Weight)Milk_Weight,sum(fatkg)fatkg,sum(snfkg)snfkg from (SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date
                        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        where  convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=" + status + ")final
                        
                        union all
                        select sum(Milk_Weight)Milk_Weight,sum([FATKg])[FATKg],sum([SNFKg])[SNFKg] from(
                        select isnull(Milk_Weight,0) as Milk_Weight, cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],FAT,SNF,cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No where convert(date,Shift_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Shift_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=" + status + " )final 
                        union all
                        select ISNULL(sum(QTY),0)QTY,ISNULL(sum(FATKG),0)FATKG,ISNULL(sum(SNFKG),0)SNFKG from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where Status=0 and  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' )xxx 
                        ) final
                   "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptmilkunionreport", "") ''report for both (RCDF And RCDFCF)
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

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 500
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Dis_QtyInLTR").HeaderText = "QTY"
        gv1.Columns("Dis_QtyInLTR").Width = 200
        gv1.Columns("Dis_QtyInLTR").FormatString = "{0:n3}"

        gv1.Columns("Dis_FATKG").HeaderText = "FATKG"
        gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"

        gv1.Columns("Dis_SNFKG").HeaderText = "SNFKG"
        gv1.Columns("Dis_SNFKG").IsVisible = True
        gv1.Columns("Dis_SNFKG").FormatString = "{0:n3}"


        gv1.Columns("Prod_QTY").HeaderText = "QTY"
        gv1.Columns("Prod_QTY").IsVisible = True
        gv1.Columns("Prod_QTY").FormatString = ""

        gv1.Columns("Prod_FATkg").HeaderText = "FATKG"
        gv1.Columns("Prod_FATkg").IsVisible = True
        gv1.Columns("Prod_FATkg").FormatString = "{0:n3}"

        gv1.Columns("Prod_SNFkg").HeaderText = "SNFKG"
        gv1.Columns("Prod_SNFkg").IsVisible = True
        gv1.Columns("Prod_SNFkg").FormatString = "{0:n3}"

        gv1.Columns("TotalLtr_ItemWiseDemand").HeaderText = "QTY"
        gv1.Columns("TotalLtr_ItemWiseDemand").IsVisible = True
        gv1.Columns("TotalLtr_ItemWiseDemand").FormatString = ""

        gv1.Columns("FATKGDemand").HeaderText = "FATKG"
        gv1.Columns("FATKGDemand").IsVisible = True
        gv1.Columns("FATKGDemand").FormatString = "{0:n3}"

        gv1.Columns("SNFKGDemand").HeaderText = "SNFKG"
        gv1.Columns("SNFKGDemand").IsVisible = True
        gv1.Columns("SNFKGDemand").FormatString = "{0:n3}"


        gv1.Columns("Milk_WeightProc").HeaderText = "QTY"
        gv1.Columns("Milk_WeightProc").IsVisible = True
        gv1.Columns("Milk_WeightProc").FormatString = "{0:n3}"

        gv1.Columns("FATKGProc").HeaderText = "FATKG"
        gv1.Columns("FATKGProc").IsVisible = True
        gv1.Columns("FATKGProc").FormatString = "{0:n3}"

        gv1.Columns("SNFKGProc").HeaderText = "SNFKG"
        gv1.Columns("SNFKGProc").IsVisible = True
        gv1.Columns("SNFKGProc").FormatString = "{0:n3}"



        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

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
            query = "select * from (
SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') 
) xx  ORDER BY Location_Name"
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    Dim status1 As String
                    Dim status2 As String
                    Dim status3 As String
                    Dim status4 As String
                    Dim status5 As String
                    If rdbPosted.Checked Then
                        status1 = " and  TSPL_SD_SHIPMENT_HEAD.Status=1 "
                        status2 = " and TSPL_PP_PRODUCTION_ENTRY.POSTED= 1 "
                        status3 = " and TSPL_DEMAND_BOOKING_master.Posted= 1 "
                        status4 = " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status= 1 "
                        status5 = "and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status= 1"
                    ElseIf rdbUnposted.Checked Then
                        status1 = " and  TSPL_SD_SHIPMENT_HEAD.Status=0 "
                        status2 = " and TSPL_PP_PRODUCTION_ENTRY.POSTED= 0 "
                        status3 = " and TSPL_DEMAND_BOOKING_master.Posted= 0 "
                        status4 = " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status= 0 "
                        status5 = "and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status= 0 "
                    Else
                        status1 = " "
                        status2 = " "
                        status3 = " "
                        status4 = " "
                        status5 = " "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",isnull(sum(final.Dis_QtyInLTR),0)Dis_QtyInLTR  ,sum(final.Dis_FATKG)Dis_FATKG,sum(final.Dis_SNFKG)Dis_SNFKG,sum(final.Prod_QTY)Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(final.Prod_SNFkg)Prod_SNFkg,sum(final.TotalLtr_ItemWiseDemand)TotalLtr_ItemWiseDemand,sum(final.FATKGDemand)FATKGDemand ,  sum(final.SNFKGDemand)SNFKGDemand,sum(final.Milk_WeightProc)Milk_WeightProc,sum(final.FATKGProc)FATKGProc,sum(final.SNFKGProc)SNFKGProc from (
                             select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(Dis_FATKG)Dis_FATKG,sum(Dis_SNFKG)Dis_SNFKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc from (select sum(Dis_QtyInLTR)Dis_QtyInLTR,sum(isnull((Dis_QtyInLTR*std_fatPer/100),0)) Dis_FATKG,sum(isnull((Dis_QtyInLTR*STD_SNFPer/100),0))Dis_snfKG  from   (select 
                            CASE WHEN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor WHEN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  ELSE 0 END AS Dis_QtyInLTR,isnull((qty*std_fatPer/100),0) Dis_FATKG,isnull((qty*STD_SNFPer/100),0) Dis_SNFKG,TSPL_SD_SHIPMENT_DETAIL.Item_Code,STD_FatPer,STD_SNFPer,qty
                             from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_SD_SHIPMENT_DETAIL 
                            left outer join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Item_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left outer join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                            left join (select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                            left join (select Conversion_factor,  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            left join ( select Conversion_factor, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.IsTaxable=0 and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master.Is_FreshItem=1 " + status1 + " )yyy)xxxx
                         union all
                        select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG ,sum(RECEIPT_QTY)Prod_QTY,sum(fatkg)Prod_FATkg,sum(SNFkg)Prod_SNFkg, 0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc  from (select isnull(sum(RECEIPT_QTY),0)RECEIPT_QTY,isnull(sum(RECEIPT_QTY*[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.STD_FatPer/100),0) as fatkg,isnull(sum(RECEIPT_QTY*STD_SNFPer/100),0) as SNFkg from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                        where convert(date,PROD_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "'and convert(date,PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status2 + " and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem=1 and IsTaxable=0)final  -----production
                        union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,sum(TotalLtr_ItemWise)TotalLtr_ItemWiseDemand,sum(FATKG)FATKGDemand,sum(SNFKG)SNFKGDemand,0 as Milk_WeightProc,0 as FATKGProc,0 as SNFKGProc from 
                         (select isnull(sum(TotalLtr_ItemWise),0)TotalLtr_ItemWise,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_FatPer,0)/100) as FATKG,sum(isnull(TotalLtr_ItemWise,0)*isnull(STD_SNFPer,0)/100) as SNFKG  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
                         left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                         where convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 and Is_FreshItem=1 " + status3 + "
                        )xxx 
                         union all
                         select 0 as Dis_QtyInLTR, 0 as Dis_FATKG,0 as Dis_SnfKG,0 as Prod_QTY,0 as Prod_FATkg,0 as Prod_SNFkg,0 as TotalLtr_ItemWiseDemand,0 as FATKGDemand , 0 as SNFKGDemand, sum(xxx.milk_weight) as Milk_WeightProc,SUM(XXX.fatkg)FATKGProc ,SUM(XXX.SNFKG)SNFKGProc
                        from 
						(select sum(Milk_Weight)Milk_Weight,sum(fatkg)fatkg,sum(snfkg)snfkg from (SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg],convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date
                        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                        where  convert(date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + status4 + ")final
                        
                        union all
                        select sum(Milk_Weight)Milk_Weight,sum([FATKg])[FATKg],sum([SNFKg])[SNFKg] from(
                        select isnull(Milk_Weight,0) as Milk_Weight, cast(Milk_Weight*FAT/100 as decimal(18,3))  as [FATKg],FAT,SNF,cast(Milk_Weight*SNF/100 as decimal(18,3)) as [SNFKg] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No where convert(date,Shift_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Shift_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status5 + " )final 
                        union all
                        select ISNULL(sum(QTY),0)QTY,ISNULL(sum(FATKG),0)FATKG,ISNULL(sum(SNFKG),0)SNFKG from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
                        left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                        where Status=0 and  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' )xxx 
                        ) final
                   "
                Next
            End If
            query="select * from ("+query+")xx "
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
        rdbPosted.Checked = True
        rdbUnposted.Checked = False
    End Sub
End Class