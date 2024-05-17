Imports System.IO
Imports common

'by Sanjay - Create new report 
Public Class rptTruckSheetDailySummaryReport
    Inherits FrmMainTranScreen

    Dim StrPermission As String
    Dim SetCowFatPer As Decimal = 0
    Dim AreaWiseBilling As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load

        SetCowFatPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Visible = AreaWiseBilling
        lblArea.Visible = AreaWiseBilling
        Reset()
    End Sub




    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        txtMCC.arrValueMember = Nothing
        fndArea.Value = Nothing
        EnableDisableControl(True)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        txtMCC.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        btnGo.Enabled = val
        fndArea.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim MCCName As String = Nothing
            Dim AreaName As String = Nothing
            Dim whrclsRecpt As String = Nothing
            Dim whrclsRjt As String = Nothing
            Dim whre As String = Nothing
            Try
                If AreaWiseBilling Then
                    If clsCommon.myLen(fndArea.Value) > 0 Then
                        AreaName = ",max ([MCC Name]) as AreaName"
                        whre += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    End If
                Else
                    If txtMCC.arrValueMember.Count > 1 Then
                        MCCName = ",'Ganganagar' AS MCCName"
                        whrclsRecpt = " and TSPL_milk_SRN_head.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    ElseIf txtMCC.arrValueMember.Count <= 0 Then
                        MCCName = ",'Ganganagar' AS MCCName"
                        whrclsRecpt = " and TSPL_milk_SRN_head.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    Else
                        MCCName = ",max ([MCC Name]) as MCCName"
                        whrclsRecpt = " and TSPL_milk_SRN_head.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    End If
                End If
            Catch
                MCCName = ",'Ganganagar' AS MCCName"
            End Try

            Dim qry As String = "select  A.Comp_Name, [Doc Date],a.[Milk type],a.regn_no,a.phone1 " + MCCName + AreaName + "
,sum([Milk Weight]* case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet Qty]
,sum([FAT] * case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet FAT]
,sum([SNF] * case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet SNF] 
,sum([Milk Weight]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR Qty]
,sum([FAT]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR FAT]
,sum([SNF]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR SNF]
,sum([Milk Weight]* case when len(isnull(RejectType,''))>0 and RejectType='CURD' then 1 else 0 end) as [CURD Qty]
, CASE  WHEN SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) = 0 THEN 0 ELSE CAST(SUM([FAT] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) / NULLIF(SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END), 0) * 100 AS DECIMAL(18, 2)) END AS [Sweet FAT(%)]			
,CASE  WHEN SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) = 0 THEN 0 ELSE CAST(SUM([SNF] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) / NULLIF(SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END), 0) * 100 AS DECIMAL(18, 2)) END AS [Sweet SNF(%)]
,sum([Milk Weight]) as [Total Qty],sum([FAT]) as [Total FAT Kg],sum([SNF]) AS [Total SNF Kg] ,
'" + fromDate.Value + "' As FromDate,'" + ToDate.Value + "' As ToDate, '" & objCommonVar.CurrentUser & "' As User_Name 
from(select AA.Comp_Name,aa.regn_no,aa.phone1, aa.[Milk Type],aa.[Milk Weight],[FAT], [SNF],aa.RejectType ,case When isnull(RejectType,'')='SOUR' then [Milk Weight] else 0 end as [SOUR] ,case When isnull(RejectType,'')='CURD' then [Milk Weight] else 0 end as [CURD] ,[Doc Date],[MCC Name]
from (
select PP.Comp_Name,pp.regn_no,pp.phone1, PP.[Milk Type] AS [Milk Type],sum([Milk Weight(KG)] ) as [Milk Weight] 
,sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF],RejectType,[Doc Date],[Shift] ,MAX([MCC Name] )as [MCC Name]
from (  Select FINAL.Comp_Name,final.regn_no,final.phone1, final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift , final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)], final.[Mix Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Mix CLR],final.[Mix SNF(%)],final.[Mix FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Mix FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Mix SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter,  final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,
Case When TSPL_milk_SRN_detail.FAT_PER <= 5 Then TSPL_milk_SRN_detail.FAT_PER Else 0 End [Cow FAT(%)], 
Case When TSPL_milk_SRN_detail.FAT_PER <= 5 Then TSPL_milk_SRN_detail.SNF_PER Else 0 End [Cow SNF(%)], 
Case When TSPL_milk_SRN_detail.FAT_PER > 5 Then TSPL_milk_SRN_detail.FAT_PER Else 0 End [Mix FAT(%)], 
Case When TSPL_milk_SRN_detail.FAT_PER > 5 Then TSPL_milk_SRN_detail.SNF_PER Else 0 End [Mix SNF(%)], 
Case When TSPL_milk_SRN_detail.FAT_PER <= 5 Then TSPL_milk_SRN_detail.ACC_Qty Else 0 End [Cow Milk Qty (KG)],
Case When TSPL_milk_SRN_detail.FAT_PER > 5 Then TSPL_milk_SRN_detail.ACC_Qty Else 0 End [Mix Milk Qty (KG)]
, Case When Coalesce(TSPL_milk_SRN_detail.fat_per, 0) <= 0 Then 'M' When Coalesce(TSPL_milk_SRN_detail.FAT_PER, 0) <= " + clsCommon.myCstr(SetCowFatPer) + "  Then 'C' Else 'M' End As [Milk Type], 
TSPL_milk_SRN_HEAD.DOC_CODE As [Milk Receipt Code],TSPL_milk_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_milk_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_milk_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_milk_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_milk_SRN_HEAD.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_milk_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_milk_SRN_HEAD.SAMPLE_NO As [Sample No],  case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS else TSPL_MILK_SHIFT_UPLOADER_DETAIL.NO_OF_CANS end As [No Of Cans], TSPL_milk_SRN_detail.Qty As [Milk Weight],TSPL_milk_SRN_detail.UOM_Code, TSPL_milk_SRN_detail.ACC_Qty As [Milk Weight(KG)], TSPL_milk_SRN_detail.Acc_qty_ltr As [Milk Weight(LTR)], TSPL_milk_SRN_detail.FAT_PER As [FAT(%)], TSPL_milk_SRN_detail.SNF_PER As [SNF(%)], TSPL_milk_SRN_detail.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When isnull((case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample else 1 end),0)  = 0 Then 'Auto' Else 'Manual' end   As [Sample Status],
TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date ,
(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Weight else 1 end) AS IS_MANUAL , '' AS MACHINE_NO,                                
Case When isnull((case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample else 1 end),0)  = 0 Then 'N' Else 'Y' end   As IS_MILK_SAMPLE_MANUAL,
TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type else TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type end) As RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount   ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.regn_no,TSPL_COMPANY_MASTER.Phone1
From TSPL_milk_SRN_detail 
Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_milk_SRN_detail.DOC_CODE 
LEFT OUTER JOIN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_milk_SRN_HEAD.Against_Uploader_TR_No  
LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_milk_SRN_head.Comp_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_milk_SRN_HEAD.MCC_CODE 
Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_milk_SRN_HEAD.VLC_CODE
Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_milk_SRN_HEAD.VSP_CODE
left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_milk_SRN_HEAD.ROUTE_CODE
left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code
left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per
left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  and Cast(TSPL_milk_SRN_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy") + "'  and Cast(TSPL_milk_SRN_HEAD.DOC_DATE as date) <= '" + clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy") + "'" + whrclsRecpt + whre + "  
) As final where 2=2   ) as  pp group by PP.Comp_Name,pp.regn_no,pp.phone1, [Milk Type],  [Doc Date],[Shift],pp.RejectType ) as aa )a where 1=1
group by Comp_Name, [Milk Type], [Doc Date],regn_no,phone1
order by [Doc Date] "

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "SKR") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt2, "rptTruckSheetDailySummaryReportSKR", "")
                Else
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt2, "rptTruckSheetDailySummaryReport", "")
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            Dim FinalQuery As String = Nothing
            Dim strRejection As String = Nothing
            Dim strSRNQuery As String = Nothing
            strRejection = ",(case when len(isnull(TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No,''))>0 then TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type else (case when len(isnull(TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No,''))>0 then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type else null end) end)   as RejectType,'' as RejectReason,'' as Defaulter"
            Dim ShowVLCUploaderData As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
            Dim SetCowFatPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
            If AreaWiseBilling Then
                strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, Nothing, Nothing, Nothing, "", strRejection, ShowVLCUploaderData, SetCowFatPer, fndArea.Value)
            Else
                strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "", strRejection, ShowVLCUploaderData, SetCowFatPer, fndArea.Value)
            End If

            qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Mix CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Mix FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Mix SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & ") As final where 2=2 "
            FinalQuery = "select [Doc Date],a.[Milk type], 
                              sum([Milk Weight]* case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet Qty]
                            ,sum([FAT] * case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet FAT]		
                            ,sum([SNF] * case when len(isnull(RejectType,''))>0 then 0 else 1 end) as [Sweet SNF] 
                            ,CASE  WHEN SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) = 0 THEN 0
                             ELSE CAST(SUM([FAT] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) / NULLIF(SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END), 0) * 100 AS DECIMAL(18, 2)) END AS [Sweet FAT(%)]			
                             ,CASE  WHEN SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) = 0 THEN 0
                              ELSE CAST(SUM([SNF] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END) / NULLIF(SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 THEN 0 ELSE 1 END), 0) * 100 AS DECIMAL(18, 2)) END AS [Sweet SNF(%)]
                            ,sum([Milk Weight]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR Qty]
                             ,CASE WHEN (SUM([FAT] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 AND RejectType = 'SOUR' THEN 1 ELSE 0 END)) = 0 THEN 0
                              ELSE CAST(ISNULL(SUM([FAT] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 AND RejectType = 'SOUR' THEN 1 ELSE 0 END), 0)
                               / NULLIF(ISNULL(SUM([Milk Weight] * CASE WHEN LEN(ISNULL(RejectType, '')) > 0 AND RejectType = 'SOUR' THEN 1 ELSE 0 END), 0), 0) * 100 AS DECIMAL(18, 2))
                              END AS [SOUR FAT(%)] 
                             ,CASE WHEN (sum([SNF]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) )=0  THEN 0 ELSE cast (
                             ISNULL(sum([SNF]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end),0) / NULLIF(ISNULL(sum([Milk Weight]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end),0),0)* 100 as decimal (18, 2) ) END AS[SOUR SNF(%)]
                            ,sum([FAT]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR FAT] 
                            ,sum([SNF]* case when len(isnull(RejectType,''))>0 and RejectType='SOUR' then 1 else 0 end) as [SOUR SNF]
                            ,sum([Milk Weight]* case when len(isnull(RejectType,''))>0 and RejectType='CURD' then 1 else 0 end) as [CURD Qty]
                            ,sum([Milk Weight]) as [Total Qty]
                            ,sum([FAT]) as [Total FAT Kg]
                            ,sum([SNF]) AS [Total SNF Kg]
                            ,sum(NetAmount) as Amount
                            from(select aa.[Milk Type],aa.[Milk Weight],[FAT], [SNF],aa.RejectType
                            ,case When isnull(RejectType,'')='SOUR' then [Milk Weight] else 0 end as [SOUR]
                            ,case When isnull(RejectType,'')='CURD' then [Milk Weight] else 0 end as [CURD]
                            ,NetAmount,[Doc Date],[Shift]
                             from (
                             select PP.[Milk Type] AS [Milk Type],sum([Milk Weight(KG)] ) as [Milk Weight]
                            ,sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF],RejectType,[Doc Date],[Shift],sum(NetAmount) as NetAmount
                            from (" + qry +
                            "  ) as  pp group by [Milk Type],  [Doc Date],[Shift],pp.RejectType ) as aa )a where 1=1
                             group by [Milk Type], [Doc Date] order by [Doc Date]"

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(FinalQuery)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                btnGo.Enabled = False
                SetGridFormat()
                ReStoreGridLayout()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.Columns("Sweet FAT").IsVisible = True
        Gv1.Columns("Sweet SNF").IsVisible = True
        Gv1.Columns("SOUR FAT").IsVisible = True
        Gv1.Columns("SOUR SNF").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv1.Columns("Sweet Qty").FormatString = "{0:f2}"
        Dim item1 As New GridViewSummaryItem("Sweet Qty", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Gv1.Columns("Sweet FAT").FormatString = "{0:n2}"
        Dim item9 As New GridViewSummaryItem("Sweet FAT", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Gv1.Columns("Sweet SNF").FormatString = "{0:n2}"
        Dim item10 As New GridViewSummaryItem("Sweet SNF", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Gv1.Columns("SOUR Qty").FormatString = "{0:n2}"
        Dim item2 As New GridViewSummaryItem("SOUR Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Gv1.Columns("SOUR FAT").FormatString = "{0:n2}"
        Dim item11 As New GridViewSummaryItem("SOUR FAT", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Gv1.Columns("SOUR SNF").FormatString = "{0:n2}"
        Dim item12 As New GridViewSummaryItem("SOUR SNF", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Gv1.Columns("CURD Qty").FormatString = "{0:n2}"
        Dim item3 As New GridViewSummaryItem("CURD Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Gv1.Columns("Total Qty").FormatString = "{0:n2}"
        Dim item4 As New GridViewSummaryItem("Total Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Gv1.Columns("Total FAT Kg").FormatString = "{0:n2}"
        Dim item5 As New GridViewSummaryItem("Total FAT Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Gv1.Columns("Total SNF Kg").FormatString = "{0:n2}"
        Dim item6 As New GridViewSummaryItem("Total SNF Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Gv1.Columns("Amount").FormatString = "{0:n2}"
        Dim item7 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim summaryItem1 As New GridViewSummaryItem()
        summaryItem1.FormatString = "{0:n2}"
        summaryItem1.Name = "Sweet FAT(%)"
        summaryItem1.AggregateExpression = "sum([Sweet FAT])*100/sum([Sweet Qty])"
        summaryRowItem.Add(summaryItem1)

        Dim summaryItem2 As New GridViewSummaryItem()
        summaryItem2.FormatString = "{0:n2}"
        summaryItem2.Name = "Sweet SNF(%)"
        summaryItem2.AggregateExpression = "sum([Sweet SNF])*100/sum([Sweet Qty])"
        summaryRowItem.Add(summaryItem2)

        Dim summaryItem3 As New GridViewSummaryItem()
        summaryItem3.FormatString = "{0:n2}"
        summaryItem3.Name = "SOUR FAT(%)"
        summaryItem3.AggregateExpression = "sum([SOUR FAT])*100/sum([SOUR Qty])"
        summaryRowItem.Add(summaryItem3)

        Dim summaryItem4 As New GridViewSummaryItem()
        summaryItem4.FormatString = "{0:n2}"
        summaryItem4.Name = "SOUR SNF(%)"
        summaryItem4.AggregateExpression = "sum([SOUR SNF])*100/sum([SOUR Qty])"
        summaryRowItem.Add(summaryItem4)


        'If Gv1.Rows.Count > 0 Then
        '    Dim summaryRowItem As New GridViewSummaryRowItem()
        '    For i As Integer = 3 To Gv1.Columns.Count - 1
        '        Dim aa = Gv1.Columns(i).HeaderText()
        '        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item1)
        '    Next
        '    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'End If
        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'Gv1.AutoSizeRows = True
        'Gv1.BestFitColumns()
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'Gv1.ShowGroupPanel = False
        'Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetDailySummaryReport & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrReportName, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                ' clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1

                doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER"))
                'Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.arrValueMember + "'"))
                doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER")) + Environment.NewLine
                'doc.MiddleHeader += "Daily Summary Of :" + clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Value + "'")) + " "
                If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                    doc.MiddleHeader += "Daily Summary Of :" + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember)
                Else
                    doc.MiddleHeader += "Daily Summary Of : GANGANAGAR "
                End If
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)

                'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                '    doc.LeftMiddleText = "Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember)
                '    doc.LeftLowerFont = New Font("Segoe UI", 8, FontStyle.Regular)
                'End If

                doc.AssociatedObject = Gv1
                'doc.Print()
                doc.RightFooter = "Page [Page #] of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@TSDSR1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)

            Dim arrMCCMapped As New ArrayList
            Dim dt As New DataTable
            Dim query As String = "select MCC_NAME from TSPL_MCC_MASTER  WHERE Area_Location_Code='" + fndArea.Value + "'"
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(query)

            For i As Integer = 0 To dt.Rows.Count - 1
                arrMCCMapped.Add(dt.Rows(i)("MCC_NAME"))
                'arrMCCMapped.Add(dt.Rows(i)("MCC_NAME").ToString())
            Next
            txtMCC.arrValueMember = arrMCCMapped
            'txtMCC.arrValueMember

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub


End Class
