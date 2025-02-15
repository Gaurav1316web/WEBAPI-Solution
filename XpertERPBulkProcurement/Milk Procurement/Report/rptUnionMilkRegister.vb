Imports common
Public Class rptUnionMilkRegister
    Inherits FrmMainTranScreen

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        GridData(True)
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub rptUnionMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
    End Sub
    Sub GridData(ByVal print As Boolean)
        Try
            Dim query As String = ""
            Dim dtr As DataTable = Nothing
            Dim startDate As Date = txtFromDate.Value
            Dim endDate As Date = txtToDate.Value
            Dim daysDifference As Integer = DateDiff(DateInterval.Day, startDate, endDate) + 1
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RCDF") = CompairStringResult.Equal Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gv1.DataSource = Nothing
                    Exit Sub
                End If


                Dim docNo As String = ""


                dtr = clsMilkUnion.UnionDBName()
                query = ""
                If dtr IsNot Nothing AndAlso dtr.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtr.Rows.Count - 1
                        If ii > 0 Then
                            query += " UNION ALL "
                        End If
                        query += "select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name] ,
                            COUNT(DISTINCT final.[Route Code]) AS [Route Code] , '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                            COUNT(DISTINCT final.[MCC Code]) AS [MCC Code] , 
                            COUNT(DISTINCT final.[BMC Code]) AS [BMC Code],  
                            COUNT(DISTINCT final.[Vlc Uploader Code]) AS [Vlc Uploader Code],  
                            SUM(final.[Milk Weight(KG)]) AS Quantity, 
                            round(SUM(final.[Milk Weight(KG)])/'" + clsCommon.myCstr(daysDifference) + "',0) AS [Average Quantity]
	                        From 
                        (Select case when isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Capping_Apply,0)=1 then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,case when isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Capping_Apply,0)=1 then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Capping_SNF else null end as Capping_SNF,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.MCC_Type as [MCC Type],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.Short_Description as Short_Description_MCC,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_ROUTE_MASTER.Short_Description as Short_Description_Route,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER.Zone_Code as [Zone Code],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD.Short_Description as Short_Description_VLC,case when [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center],
	                        case when [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.Is_MCC=1 then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.MCC_Code  end as [MCC Code], case when [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.Is_MCC=0 then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.MCC_Code end as [BMC Code],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Item_Code,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_ITEM_MASTER.Item_Desc  , Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'C' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'C' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'B' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'C' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY Else 0 End [Cow Milk Qty (KG)],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.Mcc_Code_VLC_Uploader,Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type= 'C' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Cow Milk Qty (Ltr)],  Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'B' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY Else 0 End [Buffalo Milk Qty (KG)],Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Buffalo Milk Qty (Ltr)] , TPCP.Dock_Collection_Milk_Type As [Milk Type],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.MCC_CODE As MCC, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.MCC_NAME As [MCC Name],isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.plant_code,'') As [Plant Code], isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_location_master.location_desc,'') As [Plant Name], Convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO as [Route Code],  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_BULK_ROUTE_MASTER.Route_Name As [Route Name], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.VSP_CODE As [VSP Code], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER.Vendor_Name As [VSP Name], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_GROUP.Group_Desc as [Vendor Group Desc],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD.VLC_Name As [VLC Name], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No IS Null Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL.No_Of_Cans Else 
                                          Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Against_Uploader_TR_No Is Null Then [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans Else 0 End End As [No Of Cans], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Qty As [Milk Weight],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.UOM_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY As [Milk Weight(KG)], convert(decimal(18,2),[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR) As [Milk Weight(LTR)],Convert(decimal(18,2),[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR)*5 as [DBT Amount] , [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.CLR, Convert(decimal(18,3), [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_KG) As [FAT(KG)], Convert(decimal(18,3),[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_KG) As [SNF(KG)], Convert(decimal(18,3), ROUND([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_PER * [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [FAT(LTR)], Convert(decimal(18,3),ROUND([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_PER * [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [SNF(LTR)], cast( round(([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount +isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0) )  * isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount , cast((([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount ,  Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = 0 Then 0 Else 1 End As [Sample Status], 
										  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.RATE As [SRN Rate], [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , CASE WHEN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample=0 THEN 'N' ELSE 'Y' END AS IS_MANUAL ,'' As MACHINE_NO,[Transporter Code],[Transporter Name],isnull( [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.TIP_Amount,isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,Tabtspl_FAT_SNF_UPLOADER_MASTER.Planning_Code,FORMAT ( [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_PRICE_CHART_PLANNING.Posted_Date , 'dd/MM/yyyy') as Planning_Posted_Date, FORMAT ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_PRICE_CHART_PLANNING. Posted_Date , 'hh:mm:ss tt') as Planning_Posted_Time,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_MASTER.Declared_Rate,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Price_Code as [Price Code],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Purchase_Order_No,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Head_Load_Amount 
                        ,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast(([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION.Amount+[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast(([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION.Amount+[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.RATE)*[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                         ,(isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive 
                         ,case when tspl_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code is null then isnull( tspl_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.Incentive_Amount,0) else isnull(tspl_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount,0) end as  IncetiveAmt,case when isnull( [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.Vehicle,case when isnull( [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Sub_Standard,0)=1 then  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.ACC_QTY_LTR else 0 end as [SubStandardQty],Convert(varchar,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE,103) + ' ' + CONVERT(varchar,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE,8) as [Doc Date Time],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.QAT_Rate,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.QAT_Amt 
                         From [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.DOC_CODE  
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SHIFT_UPLOADER_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SHIFT_UPLOADER_DETAIL.TR_No=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_NO=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.Against_Uploader_TR_No 
                         left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_ITEM_MASTER on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_ITEM_MASTER.item_code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.item_code 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_HEAD On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.MCC_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.MCC_CODE 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VLC_MASTER_HEAD.VLC_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.VLC_CODE
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER.Vendor_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.VSP_CODE
                         left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_GROUP on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_MASTER.Vendor_Group_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_VENDOR_GROUP.Ven_Group_Code 
                         left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_BULK_ROUTE_MASTER On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_BULK_ROUTE_MASTER.ROUTE_NO=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.ROUTE_CODE 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_ROUTE_MASTER On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_ROUTE_MASTER.Route_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.ROUTE_CODE
                         Left join (select [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.vendor_code as [Transporter Code],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_vendor_master.vendor_name as [Transporter Name],[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.mcc_code,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.vehicle_code from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_vendor_master on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_vendor_master.vendor_code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.vendor_code and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_vendor_master.form_type='PTM' left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_mcc_master on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_mcc_master.mcc_code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master On [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_Primary_Vehicle_Master.Vehicle_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_ROUTE_MASTER.Vehicle_Code 
                         left outer join (select code,max(Price_code) as Price_code,max(Planning_Code) as Planning_Code from  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_FAT_SNF_UPLOADER_MASTER group by code) as Tabtspl_FAT_SNF_UPLOADER_MASTER on Tabtspl_FAT_SNF_UPLOADER_MASTER.code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Price_Code 
                         left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_MASTER on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_MASTER.Price_Code=Tabtspl_FAT_SNF_UPLOADER_MASTER.Price_code
                         left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION.Price_code=Tabtspl_FAT_SNF_UPLOADER_MASTER.Price_code and cast([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PRICE_SNF_DEDUCTION.Per left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_PRICE_CHART_PLANNING on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_PRICE_CHART_PLANNING.Planning_Code =  Tabtspl_FAT_SNF_UPLOADER_MASTER.Planning_Code 
                         left join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_location_master on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_location_master.location_code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MCC_MASTER.Plant_Code left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL group by MILK_SRN_Code) as tspl_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on tspl_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL group by MILK_SRN_Code) as tspl_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL on tspl_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.MILK_SRN_Code=[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_CODE  left outer join  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_PRICE_CHART_PLANNING TPCP on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_DETAIL.Price_Code=TPCP.Planning_Code  where 2 = 2  and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type is null and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type is null  and Cast([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and Cast([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_MILK_SRN_HEAD.DOC_DATE as Date) <='" + txtToDate.Value + "') As final where 2=2 "
                    Next
                End If

            Else
                query = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                        COUNT(DISTINCT final.[Route Code]) AS [Route Code] , 
                        COUNT(DISTINCT final.[MCC Code]) AS [MCC Code] , 
                        COUNT(DISTINCT final.[BMC Code]) AS [BMC Code],  
                        COUNT(DISTINCT final.[Vlc Uploader Code]) AS [Vlc Uploader Code],  
                        SUM(final.[Milk Weight(KG)]) AS Quantity, 
                        round(SUM(final.[Milk Weight(KG)])/'" + clsCommon.myCstr(daysDifference) + "',0) AS [Average Quantity]
	                    From 
                    (Select case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_SNF else null end as Capping_SNF,TSPL_MCC_MASTER.MCC_Type as [MCC Type],TSPL_MCC_MASTER.Short_Description as Short_Description_MCC,TSPL_MCC_ROUTE_MASTER.Short_Description as Short_Description_Route,TSPL_VENDOR_MASTER.Zone_Code as [Zone Code],TSPL_VLC_MASTER_HEAD.Short_Description as Short_Description_VLC,case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center],
	                    case when TSPL_MCC_MASTER.Is_MCC=1 then TSPL_MCC_MASTER.MCC_Code  end as [MCC Code], case when TSPL_MCC_MASTER.Is_MCC=0 then TSPL_MCC_MASTER.MCC_Code end as [BMC Code],TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'C' Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'C' Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'B' Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'C' Then TSPL_MILK_SRN_DETAIL.ACC_QTY Else 0 End [Cow Milk Qty (KG)],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type= 'C' Then TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Cow Milk Qty (Ltr)],  Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type = 'B' Then TSPL_MILK_SRN_DETAIL.ACC_QTY Else 0 End [Buffalo Milk Qty (KG)],Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Buffalo Milk Qty (Ltr)] , TPCP.Dock_Collection_Milk_Type As [Milk Type], TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO as [Route Code],  TSPL_BULK_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  Case When TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No IS Null Then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.No_Of_Cans Else 
                                      Case When TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No Is Null Then TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans Else 0 End End As [No Of Cans], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_QTY As [Milk Weight(KG)], convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR) As [Milk Weight(LTR)],Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR)*5 as [DBT Amount] , TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR, Convert(decimal(18,3), TSPL_MILK_SRN_DETAIL.FAT_KG) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SRN_DETAIL.SNF_KG) As [SNF(KG)], Convert(decimal(18,3), ROUND(TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [FAT(LTR)], Convert(decimal(18,3),ROUND(TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [SNF(LTR)], cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount +isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0) )  * isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount , cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount ,  Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = 0 Then 0 Else 1 End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample=0 THEN 'N' ELSE 'Y' END AS IS_MANUAL ,'' As MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code,FORMAT ( TSPL_PRICE_CHART_PLANNING.Posted_Date , 'dd/MM/yyyy') as Planning_Posted_Date, FORMAT (TSPL_PRICE_CHART_PLANNING. Posted_Date , 'hh:mm:ss tt') as Planning_Posted_Time,TSPL_MILK_PRICE_MASTER.Declared_Rate,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount 
                    ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                     ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive 
                     ,case when TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code is null then isnull( TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.Incentive_Amount,0) else isnull(TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount,0) end as  IncetiveAmt,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then  TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR else 0 end as [SubStandardQty],Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) + ' ' + CONVERT(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,8) as [Doc Date Time],TSPL_MILK_SRN_DETAIL.QAT_Rate,TSPL_MILK_SRN_DETAIL.QAT_Amt 
                     From TSPL_MILK_SRN_DETAIL 
                     Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                     Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                     Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_NO=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
                     Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                     Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                     Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                     left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                     left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
                     Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                     Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                     Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                     left outer join (select code,max(Price_code) as Price_code,max(Planning_Code) as Planning_Code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code 
                     left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code
                     left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code =  TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code 
                     left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE  left outer join  TSPL_PRICE_CHART_PLANNING TPCP on TSPL_MILK_SRN_DETAIL.Price_Code=TPCP.Planning_Code  where 2 = 2  and TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type is null and TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type is null  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(txtToDate.Value) + "' ) As final where 2=2 "

            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()
                    gv1.DataSource = dt2
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.EnableFiltering = True
                    If dtr IsNot Nothing AndAlso dtr.Rows.Count > 0 Then
                        SetGridFormat()
                    Else
                        SetGridFormat1()
                    End If
                    gv1.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RCDF") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "rptMilkSRNALLUnion", "")
                    Else
                        frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "rptMilkSRNUnion", "") ''report for both (RCDF And RCDFCF)
                    End If
                End If
                    Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
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
        gv1.Columns("SNo").IsVisible = True '

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 200
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Route Code").HeaderText = "Route Code"
        gv1.Columns("Route Code").Width = 150
        gv1.Columns("Route Code").IsVisible = True

        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False

        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False

        gv1.Columns("MCC Code").HeaderText = "MCC Code"
        gv1.Columns("MCC Code").Width = 150
        gv1.Columns("MCC Code").IsVisible = True

        gv1.Columns("BMC Code").HeaderText = "BMC Code"
        gv1.Columns("BMC Code").Width = 150
        gv1.Columns("BMC Code").IsVisible = True

        gv1.Columns("Vlc Uploader Code").HeaderText = "Vlc Uploader Code"
        gv1.Columns("Vlc Uploader Code").Width = 150
        gv1.Columns("Vlc Uploader Code").IsVisible = False

        gv1.Columns("Quantity").HeaderText = "Quantity"
        gv1.Columns("Quantity").Width = 170
        gv1.Columns("Quantity").FormatString = "{0:n0}"
        gv1.Columns("Quantity").IsVisible = True

        gv1.Columns("Average Quantity").HeaderText = "Average Quantity"
        gv1.Columns("Average Quantity").Width = 170
        gv1.Columns("Average Quantity").FormatString = "{0:n0}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        ' View()
    End Sub
    Sub SetGridFormat1()
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
        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False

        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False

        gv1.Columns("Route Code").HeaderText = "Route Code"
        gv1.Columns("Route Code").Width = 150
        gv1.Columns("Route Code").IsVisible = True

        gv1.Columns("MCC Code").HeaderText = "MCC Code"
        gv1.Columns("MCC Code").Width = 150
        gv1.Columns("MCC Code").IsVisible = True

        gv1.Columns("BMC Code").HeaderText = "BMC Code"
        gv1.Columns("BMC Code").Width = 150
        gv1.Columns("BMC Code").IsVisible = True

        gv1.Columns("Vlc Uploader Code").HeaderText = "Vlc Uploader Code"
        gv1.Columns("Vlc Uploader Code").Width = 150
        gv1.Columns("Vlc Uploader Code").IsVisible = False

        gv1.Columns("Quantity").HeaderText = "Quantity"
        gv1.Columns("Quantity").Width = 170
        gv1.Columns("Quantity").FormatString = "{0:n0}"
        gv1.Columns("Quantity").IsVisible = True

        gv1.Columns("Average Quantity").HeaderText = "Average Quantity"
        gv1.Columns("Average Quantity").Width = 170
        gv1.Columns("Average Quantity").FormatString = "{0:n0}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        ' View()
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        GridData(False)
    End Sub
End Class