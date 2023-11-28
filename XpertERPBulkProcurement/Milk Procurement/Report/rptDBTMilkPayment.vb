Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptDBTMilkPayment
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim StrPermission As String
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
        'btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@TSDSR1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name from TSPL_VLC_MASTER_HEAD "
            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VLC_Code_VLC_Uploader", "VLC_Name", txtDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv.DataSource = Nothing
        txtMCC.arrValueMember = Nothing
        txtDCS.arrValueMember = Nothing
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = MyBase.Form_ID
        Return ReportID
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        Load_DBT_Report()
    End Sub

    Private Sub Load_DBT_Report()
        Try
            Dim qry As String = ""
            Dim dt As New DataTable()
            Dim MCCName As String = Nothing
            Dim whr As String = ""
            Dim whrclsRecpt As String = Nothing
            Dim whrclsRjt As String = Nothing
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                whrclsRjt += " and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                whrclsRecpt = " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                whrclsRjt += " and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader   IN (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")"
                whrclsRecpt = " and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader  IN (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")"
            End If

            qry = " select aa.[MCC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[VLC Name],
                        aa.[SRN Qty],(aa.[SRN Qty]/aa.Conversion_Factor) as[SRN QtyLtr],
                        ((aa.[SRN Qty]/aa.Conversion_Factor)* ISNULL(aa.Incetive_Rate, 0)) as [DBT Amount] ,aa.Incetive_Rate,aa.Conversion_Factor from ( 
                         select xxx.*  from (
                         select xx.*  from ( 
                        select max(pp.[MCC Name] )  as [MCC Name],max([VLC Name]) as [VLC Name],max(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],
                         sum([SRN Qty]) as [SRN Qty],sum([SRN Amount]) as [SRN Amount], max(Incetive_Rate) as Incetive_Rate,max(Conversion_Factor) as Conversion_Factor  from (
 
                        Select final.[Milk Receipt Code]  ,final.[MCC Name] ,final.[Vlc Uploader Code]  ,final.[VLC Name] ,final.Item_Code,final.Item_Desc,final.UOM_Code as [UOM],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)],final.[SRN Amount], final.[SRN Qty],final.Incetive_Rate,final.Conversion_Factor From

                        ( Select  TSPL_MP_INCENTIVE1.Incetive_Rate,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,'' as RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                         ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount   ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] 
                         From TSPL_MILK_RECEIPT_DETAIL 
                         Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE 
                         Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE
                         Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO 
                         Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
                         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code
                         LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='LTR'
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                         Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE
                         Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE
                         left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                         Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE
                         left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                         And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) 
                         And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT 
                         Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE 
                         And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code 
                         left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code
                         left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per
                         left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code
                         left outer join (select  VLC_Code,max (Incetive_Rate) as Incetive_Rate from TSPL_MP_INCENTIVE_ENTRY_HEAD
                         left outer join  TSPL_MP_INCENTIVE_ENTRY_DETAIL on TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code= TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code group by VLC_Code) as TSPL_MP_INCENTIVE1 on TSPL_MP_INCENTIVE1.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE 
                         where 2 = 2  and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "'  
                         and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'   
                         " + whrclsRecpt + "  
                          Union All 
                          Select TSPL_MP_INCENTIVE1.Incetive_Rate,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)],  Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status],  TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate],  TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date ,  '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.Code as RejectType,  case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as RejectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  
                         ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                         ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount 
                         ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,t1.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] 
                         From   TSPL_MILK_REJECT_DETAIL 
                         Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE 
                         left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO 
                         Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code
                         LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='LTR'
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
                         Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE 
                         Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE 
                         Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE 
                         left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                         Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE 
                         Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT 
                         Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code 
                         left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per 
                         left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type 
                         left outer join (select VLC_Code,max(Incetive_Rate) as Incetive_Rate  from TSPL_MP_INCENTIVE_ENTRY_HEAD
                         left outer join  TSPL_MP_INCENTIVE_ENTRY_DETAIL on TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code= TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code  group by VLC_Code) as TSPL_MP_INCENTIVE1 on TSPL_MP_INCENTIVE1.VLC_Code=TSPL_MILK_REJECT_DETAIL.VLC_CODE
                         where 2=2   and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' 
                         and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "' 
                         " + whrclsRjt + " ) As final where 2=2 
                         ) as  pp group by pp.[Vlc Uploader Code])as xx ) as xxx ) as aa
                         order by convert(int, aa.[VLC_Code_VLC_Uploader]) asc  "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.GroupDescriptors.Clear()
                gv.SummaryRowsBottom.Clear()
                gv.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv.BestFitColumns()
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
                ReStoreGridLayout()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Report")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'Dim ReportID As String = GetReportID()
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("MCC Name").Width = 100
        gv.Columns("MCC Name").IsVisible = True
        gv.Columns("MCC Name").HeaderText = "MCC Name"

        gv.Columns("VLC Name").Width = 100
        gv.Columns("VLC Name").IsVisible = True
        gv.Columns("VLC Name").HeaderText = "VLC Name"

        gv.Columns("VLC Uploader Code").Width = 100
        gv.Columns("VLC Uploader Code").IsVisible = True
        gv.Columns("VLC Uploader Code").HeaderText = "VLC Uploader Code"

        gv.Columns("SRN Qty").Width = 100
        gv.Columns("SRN Qty").IsVisible = True
        gv.Columns("SRN Qty").HeaderText = "SRN Qty"
        gv.Columns("SRN Qty").FormatString = "{0:n2}"

        gv.Columns("SRN QtyLtr").Width = 100
        gv.Columns("SRN QtyLtr").IsVisible = True
        gv.Columns("SRN QtyLtr").HeaderText = "SRN QtyLtr"
        gv.Columns("SRN QtyLtr").FormatString = "{0:n2}"

        gv.Columns("DBT Amount").Width = 100
        gv.Columns("DBT Amount").IsVisible = True
        gv.Columns("DBT Amount").HeaderText = "DBT Amount"
        gv.Columns("DBT Amount").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("SRN QtyLtr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("DBT Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub rptDBTMilkReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = ""
            Dim dt As New DataTable()
            Dim MCCName As String = Nothing
            Dim whr As String = ""
            Dim whrclsRecpt As String = Nothing
            Dim whrclsRjt As String = Nothing

            Try
                If txtMCC.arrValueMember.Count > 1 Then
                    MCCName = ",'' AS MCCName"
                    whrclsRjt = "  and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    whrclsRecpt = " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                ElseIf txtMCC.arrValueMember.Count <= 0 Then
                    MCCName = ",'' AS MCCName"
                    whrclsRjt = "  and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    whrclsRecpt = " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                Else
                    MCCName = ",aa.[MCC Name] as MCCName"
                    whrclsRjt = "  and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    whrclsRecpt = " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                End If
            Catch
                MCCName = ",'' AS MCCName"
            End Try

            qry = " select ROW_NUMBER() OVER (ORDER BY CONVERT(INT, aa.[VLC_Code_VLC_Uploader]) ASC) AS Sno " + MCCName + ",aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[VLC Name] as VLCName,
                        aa.[SRN Qty],(aa.[SRN Qty]/aa.Conversion_Factor) as[SRN QtyLtr],
                        ((aa.[SRN Qty]/aa.Conversion_Factor)* ISNULL(aa.Incetive_Rate, 0)) as [DBT Amount] ,
                        aa.Incetive_Rate,aa.Conversion_Factor,aa.Comp_Name,'" + txtFromDate.Value + "' As FromDate,'" + txtToDate.Value + "' As ToDate
                        from ( 
                         select xxx.*  from (
                         select xx.*  from ( 
                        select max(pp.[MCC Name] )  as [MCC Name],max([VLC Name]) as [VLC Name],max(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],
                         sum([SRN Qty]) as [SRN Qty],sum([SRN Amount]) as [SRN Amount], max(Incetive_Rate) as Incetive_Rate,
                         max(Conversion_Factor) as Conversion_Factor,MAX(Comp_Name) AS Comp_Name  from (
 
                        Select final.[Milk Receipt Code],final.[MCC Name] ,final.[Vlc Uploader Code],
                        final.[VLC Name] ,final.Item_Code,final.Item_Desc,final.UOM_Code as [UOM],final.[Milk Weight(KG)],
                        final.[Milk Weight(LTR)]  as [Milk Weight(LTR)],final.[SRN Amount], final.[SRN Qty],final.Incetive_Rate,
                        final.Conversion_Factor,FINAL.Comp_Name From

                        ( Select  TSPL_MP_INCENTIVE1.Incetive_Rate,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,'' as RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                         ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code],
                         [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,
                         (isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,
                         (isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,
                         TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,
                         TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code],TSPL_COMPANY_MASTER.Comp_Name  
                         From TSPL_MILK_RECEIPT_DETAIL 
                         Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE 
                         LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_RECEIPT_HEAD.Comp_Code
                         Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE
                         Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO 
                         Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
                         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code
                         LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='LTR'
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                         Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE
                         Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE
                         left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                         Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE
                         left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                         And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) 
                         And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT 
                         Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE 
                         And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code 
                         left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code
                         left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per
                         left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code
                         left outer join (select  VLC_Code,max (Incetive_Rate) as Incetive_Rate from TSPL_MP_INCENTIVE_ENTRY_HEAD
                         left outer join  TSPL_MP_INCENTIVE_ENTRY_DETAIL on TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code= TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code group by VLC_Code) as TSPL_MP_INCENTIVE1 on TSPL_MP_INCENTIVE1.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE 
                         where 2 = 2  and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "'  
                         and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'   
                         " + whrclsRecpt + "  
                          Union All 
                          Select TSPL_MP_INCENTIVE1.Incetive_Rate,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)],  Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status],  TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate],  TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date ,  '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.Code as RejectType,  case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as RejectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  
                         ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                         ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount 
                         ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,
                          (isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,
                         TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,
                         t1.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code],TSPL_COMPANY_MASTER.Comp_Name 
                         From   TSPL_MILK_REJECT_DETAIL 
                         Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE 
                         LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_REJECT_HEAD.Comp_Code
                         left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO 
                         Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code
                         LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='LTR'
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
                         Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE 
                         Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE 
                         Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE 
                         left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                         Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE 
                         Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                         Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT 
                         Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code 
                         left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per 
                         left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type 
                         left outer join (select VLC_Code,max(Incetive_Rate) as Incetive_Rate  from TSPL_MP_INCENTIVE_ENTRY_HEAD
                         left outer join  TSPL_MP_INCENTIVE_ENTRY_DETAIL on TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code= TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code  group by VLC_Code) as TSPL_MP_INCENTIVE1 on TSPL_MP_INCENTIVE1.VLC_Code=TSPL_MILK_REJECT_DETAIL.VLC_CODE
                         where 2=2   and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' 
                         and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "' 
                         " + whrclsRjt + " ) As final where 2=2 
                         ) as  pp group by pp.[Vlc Uploader Code])as xx ) as xxx ) as aa
                         order by convert(int, aa.[VLC_Code_VLC_Uploader]) asc     "

            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptDBTPayment", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDBTMilkPayment & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrReportName, gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                ' clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = gv

                doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER"))
                'Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.arrValueMember + "'"))
                doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER")) + Environment.NewLine
                'doc.MiddleHeader += "Daily Summary Of :" + clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Value + "'")) + " "
                'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                '    doc.MiddleHeader += "Daily Summary Of :" + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember)
                'Else
                '    doc.MiddleHeader += "Daily Summary Of : GANGANAGAR "
                'End If
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)

                'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                '    doc.LeftMiddleText = "Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember)
                '    doc.LeftLowerFont = New Font("Segoe UI", 8, FontStyle.Regular)
                'End If

                doc.AssociatedObject = gv
                'doc.Print()
                doc.RightFooter = "Page [Page #] of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub


End Class