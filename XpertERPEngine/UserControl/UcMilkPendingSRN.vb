Imports common
Imports System.Data.SqlClient
Public Class UcMilkPendingSRN
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public MpCode As String = Nothing
    Public fORMCode As String = Nothing
    Public VendorName As String = Nothing
    Public stran As SqlTransaction
    Public strCurrCode As String = Nothing
    Public Frm_date As Date = Nothing
    Public To_date As Date = Nothing
    Public isForMP As Boolean = False
    Public ArrReturn As List(Of clsMilkSRNMCCDetail) = Nothing
    Dim dtAllData As DataTable = Nothing

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDInvoiceCode As String = "Invoice_CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const ColFATPer As String = "ColFATPer"
    Const ColSNFPer As String = "ColSNFPer"
    Const ColCLR As String = "ColCLR"
    Const ColEMPAmt As String = "ColEMPAMT"
    Const ColCANS As String = "ColCANS"
    Const ColService_Charge As String = "ColService_Charge"
    Const ColCOMMISSION As String = "ColCOMMISSION"
    Const ColPAYMENTCOMMISSION As String = "ColPAYMENTCOMMISSION"
    Const ColOwn_Asset_AMount As String = "ColOwn_Asset_AMount"
    Const ColHead_Load_Amount As String = "ColHead_Load_Amount"

    Const ColFATKG As String = "ColFATKG"
    Const ColSNFKG As String = "ColSNFKG"
    Const ColCorrection_Factor As String = "ColCorrection_Factor"

    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDQty As String = "QTY"
    Const colDAcc_Qty As String = "ACC_QTY"

    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDAmount As String = "COLAMOUNT"
    Const colDServiceChargeAmount As String = "COLSERVICECHARGEAMT"

    Public Const colHSelect As String = "SELECT"
    Public Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHVLCCode As String = "colHVLCCode"
    Const colHVLCName As String = "colHVLCName"
    Const colHVEHICLECode As String = "colHVEHICLECode"
    Const colHVEHICLENAME As String = "colHVEHICLENAME"
    Const colHRouteCode As String = "colHRouteCode"
    Const colHRouteName As String = "colHRouteName"
    Const colHMCC As String = "colHMCC"
    Const colHShift As String = "colHShift"
    Dim MultipleFinderFillAuto As Boolean = False
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim qry As String
            If isForMP = False Then
                qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
                    & " ,Unit ,Qty as POQty,ACC_Qty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
                    & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
                    & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,Service_Charge_Type,Case when Nature='C' then Actual_charges end as  Commission," _
                      & " Case when Nature='E' then Actual_charges end as Payment_Commission,Head_Load_Amount as Head_Load_Amount,Own_Asset_Amount as Own_Asset_Amount from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_SRN_DETAIL.Own_Asset_Amount,TSPL_MILK_SRN_HEAD.DOC_DATE" _
                    & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
                    & " ,Item_Desc as IName,TSPL_MILK_SRN_DETAIL.Qty  as Qty,TSPL_MILK_SRN_DETAIL.ACC_Qty  as ACC_Qty,0 as Unapproved,tspl_milk_receipt_Detail.Uom_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
                    & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
                    & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
                    & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
                    & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
                    & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
                    & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
                    & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE where  TSPL_MILK_SRN_HEAD.Posted=1  and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code is null and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)  and coalesce(TSPL_MILK_SRN_DETAIL.AMOUNT,0)>0 "
                If clsCommon.myLen(VendorCode) > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VendorCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0       
                End If
                qry &= " --union all " _
                    & " -- select TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Qty,0 as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable, TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty  as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code where TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted=1 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code,''))>0" _
                    & " --             union all  " _
                    & " --             select TSPL_MILK_PURCHASE_INVOICE_DETAIL.Bin_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_ID as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,0  as Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate  from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.PI_No=TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_No where TSPL_MILK_PURCHASE_INVOICE_HEAD.Status=0 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Id,''))>0 " & vbNewLine _
                    & " )Final inner join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,Service_Charge_Type,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers," _
                  & " ,Nature,Actual_charges,payment_commision_pers,Qty,ACC_Qty,Head_Load_Amount,Own_Asset_Amount  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "

            Else
                qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
            & " ,Unit ,Qty as POQty,ACC_Qty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
            & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
            & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,Service_Charge_Type,Case when Nature='C' then Actual_charges end as  Commission," _
              & " Case when Nature='E' then Actual_charges end as Payment_Commission,Head_Load_Amount as Head_Load_Amount,Own_Asset_Amount as Own_Asset_Amount from (select distinct tspl_Vlc_Data_Uploader.doc_No as Code,0 as Head_Load_Amount,0 as Own_Asset_Amount,tspl_Vlc_Data_Uploader.DOC_DATE ," _
              & " tspl_Vlc_Data_Uploader.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,tspl_Vendor_master.Vendor_CODE as Vendor,Vendor_name,TSPL_ITEM_MASTER.Item_Code as " _
              & " ICode ,Item_Desc as IName,tspl_Vlc_Data_Uploader.Qty  as Qty,tspl_Vlc_Data_Uploader.Qty  as ACC_Qty,0 as Unapproved,tspl_Vlc_Data_Uploader.Uom_Code as Unit," _
              & " 1 as RI,tspl_Vlc_Data_Uploader.RATE as Rate,1 as Chk  ,tspl_Vlc_Data_Uploader.Amount,tspl_Vlc_Data_Uploader.fat as FAT_PER,tspl_Vlc_Data_Uploader.snf " _
              & " as  SNF_PER,tspl_Vlc_Data_Uploader.qty/40   as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,tspl_Vlc_Data_Uploader.Route_no,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE," _
              & " TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift " _
              & " from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on " _
              & " tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE" _
              & " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE" _
              & " =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =" _
              & " TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on " _
              & " TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join  TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=" _
              & " TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=" _
              & " TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on " _
              & " TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=" _
              & " TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE left join tspl_Vlc_Data_Uploader" _
              & " on tspl_Vlc_Data_Uploader.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and convert(date,tspl_Vlc_Data_Uploader.doc_date,103)=convert(date,TSPL_MILK_SRN_HEAD.MCC_CODE,103) " _
              & " and Route_No=TSPL_MILK_Receipt_DETAIL.ROUTE_CODE and tspl_Vlc_Data_Uploader.VLC_Code=VLC_Code_VLC_Uploader where  TSPL_MILK_SRN_HEAD.Posted=1 " _
              & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code is null and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between " _
              & " convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)  " _
              & " and coalesce(TSPL_MILK_SRN_DETAIL.AMOUNT,0)>0 "
                If clsCommon.myLen(VendorCode) > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VendorCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0       
                End If
                qry &= " --union all " _
                & " -- select TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Qty,0 as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable, TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty  as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code where TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted=1 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code,''))>0" _
                & " --             union all  " _
                & " --             select TSPL_MILK_PURCHASE_INVOICE_DETAIL.Bin_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_ID as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,0  as Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate  from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.PI_No=TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_No where TSPL_MILK_PURCHASE_INVOICE_HEAD.Status=0 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Id,''))>0 " & vbNewLine _
                & " )Final inner join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,Service_Charge_Type,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers," _
                  & " ,Nature,Actual_charges,payment_commision_pers,Qty,ACC_Qty,Head_Load_Amount,Own_Asset_Amount  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "

            End If
            'Dim qry As String = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,Unit ,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
            '" SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
            '" SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
            '" SUM(Unapproved) as UnapprovedQty,SUM(FREEQty) as FreeQty," & _
            '" SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," & _
            '" MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,Final.MRP as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,0 as Assessable,max(Leak_Qty) as Leak_Qty,max(Burst_Qty) as  Burst_Qty, max(Short_Qty) as Short_Qty,max(Is_Mannual_Amt) as Is_Mannual_Amt,max(Amount) as Amount ,MAX(Line_No) as Line_No,max(AbatementRate) as AbatementRate,max(Bin_No) as Bin_No from ( " & _
            '" select TSPL_SRN_DETAIL.Bin_No,TSPL_SRN_DETAIL.SRN_No as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL .free_qty as FREEQty,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_HEAD.Tax_Group,TSPL_SRN_DETAIL.TAX1_Rate,TSPL_SRN_DETAIL.TAX2_Rate,TSPL_SRN_DETAIL.TAX3_Rate,TSPL_SRN_DETAIL.TAX4_Rate,TSPL_SRN_DETAIL.TAX5_Rate,TSPL_SRN_DETAIL.TAX6_Rate,TSPL_SRN_DETAIL.TAX7_Rate,TSPL_SRN_DETAIL.TAX8_Rate,TSPL_SRN_DETAIL.TAX9_Rate,TSPL_SRN_DETAIL.TAX10_Rate,TSPL_SRN_DETAIL.TAX1_Amt,TSPL_SRN_DETAIL.TAX2_Amt,TSPL_SRN_DETAIL.TAX3_Amt,TSPL_SRN_DETAIL.TAX4_Amt,TSPL_SRN_DETAIL.TAX5_Amt,TSPL_SRN_DETAIL.TAX6_Amt,TSPL_SRN_DETAIL.TAX7_Amt,TSPL_SRN_DETAIL.TAX8_Amt,TSPL_SRN_DETAIL.TAX9_Amt,TSPL_SRN_DETAIL.TAX10_Amt,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Assessable,TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty,TSPL_SRN_DETAIL.Is_Mannual_Amt,TSPL_SRN_DETAIL.Amount,TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.AbatementRate from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1  and TSPL_SRN_DETAIL.Row_Type='" + clsItemRowType.RowTypeItem + "'"
            'If clsCommon.myLen(VendorCode) > 0 Then
            '    qry += " and TSPL_SRN_HEAD.Vendor_Code='" + VendorCode + "'"
            'End If
            'qry += " union all " & _
            '" select TSPL_PI_DETAIL.Bin_No,TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty,0 as Unapproved,TSPL_PI_DETAIL.free_qty as FREEQty,TSPL_PI_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_PI_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_PI_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_PI_DETAIL.Assessable, TSPL_PI_DETAIL.Leak_Qty as Leak_Qty,TSPL_PI_DETAIL.Burst_Qty as  Burst_Qty,TSPL_PI_DETAIL.Short_Qty  as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_PI_DETAIL.AbatementRate from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 " & _
            '" union all  " & _
            '" select TSPL_PI_DETAIL.Bin_No,TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved,TSPL_PI_DETAIL.free_qty as FREEQty,TSPL_PI_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_PI_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_PI_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_PI_DETAIL.Assessable,TSPL_PI_DETAIL.Leak_Qty as Leak_Qty,TSPL_PI_DETAIL.Burst_Qty as  Burst_Qty,TSPL_PI_DETAIL.Short_Qty as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_PI_DETAIL.AbatementRate  from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('" + strCurrCode + "') " & _
            'qry += ")Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' group by Code,ICode,Unit,MRP  having SUM(Chk)>0 " _
            '& " and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0 and (   sum(Leak_Qty)<>0 or sum(Burst_Qty)<>0  or sum(Short_Qty)<>0 ))) " _
            '& " and (SUM((Qty *RI)- Unapproved)<>0 or (SUM(Qty*RI)=0 and (sum(Leak_Qty*RI)<>0 or sum(Burst_Qty*RI)<>0  or sum(Short_Qty*RI)<>0)))" _
            '& " order by Code, Line_No "
            dtAllData = clsDBFuncationality.GetDataTable(qry)
            If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
                Throw New Exception("No item found for vendor " + VendorName + "")
            End If
            LoadHeadData()
            LoadBlankGridDetail()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function LoaDHeadDataQuery(ByVal trans As SqlTransaction) '==========This is For Create Bill and Incentive Both.
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, trans)) = 1)
        Dim qry As String = String.Empty
        If isForMP = False Then
            qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
                & " ,Unit ,Qty as POQty,Acc_Qty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
                & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
                & " ,0 as Assessable,max(Amount) as Amount,max(Service_Charge_Amount) as Service_Charge_Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,Service_Charge_Type,Case when Nature='C' then Actual_charges end as  Commission," _
                  & " Case when Nature='E' then Actual_charges end as Payment_Commission,Head_Load_Amount as Head_Load_Amount,Own_Asset_Amount as Own_Asset_Amount,max( EMP_Amount) as EMP_Amount from (" +
                  "select  distinct  TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_SRN_DETAIL.Own_Asset_Amount,TSPL_MILK_SRN_HEAD.DOC_DATE" _
                & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
                & " ,Item_Desc as IName,TSPL_MILK_SRN_DETAIL.Qty  as Qty,TSPL_MILK_SRN_DETAIL.ACC_Qty  as ACC_Qty,0 as Unapproved,tspl_milk_receipt_Detail.Uom_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
                & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift,TSPL_MILK_SRN_DETAIL.emp_amount from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
                & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
                & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
                & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
                & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.Sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
                & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
                & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.mcc_code  where  TSPL_MILK_SRN_HEAD.Posted=1  and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code is null and tspl_Milk_Srn_Head.is_incentive_Created='N' "
            Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, trans)) = 1
            If isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103) "
            Else
                qry += " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103) "
            End If

            qry += " and 2= (case when isnull(TSPL_MCC_MASTER.Failed_Sample_Apply,0)=1 and (TSPL_MILK_SRN_DETAIL.FAT_PER<TSPL_MCC_MASTER.Failed_Sample_FAT or TSPL_MILK_SRN_DETAIL.SNF_PER<TSPL_MCC_MASTER.Failed_Sample_SNF) and isnull(TSPL_MILK_SRN_head.Failed_Sample_Status,0)=0 and isnull( TSPL_MILK_SRN_head.Against_Reject_No,'')='' then 3 else 2 end) "

            ''Comment by balwinder on 24/Jan/2017 at gajraula becuase pick Zero amount SRN in Milk Purchase Invoice
            ''  qry += " and coalesce(TSPL_MILK_SRN_DETAIL.AMOUNT,0)>0 "
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VendorCode + "'"
            End If
            qry += " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers," _
                  & " payment_commision_pers,Nature,Actual_charges,Qty,Service_Charge_Type,Acc_Qty,Head_Load_Amount,Own_Asset_Amount  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "
        Else
            qry = "select distinct CAST(0 as bit) as Sel,code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
            & " ,Unit ,Qty as POQty,ACC_Qty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
            & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
            & " ,0 as Assessable,max(Amount) as Amount,0 as Service_Charge_Amount,FAT_PER,SNF_PER,CLR,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,Service_Charge_Type,Case when Nature='C' then Actual_charges end as  Commission," _
            & " Case when Nature='E' then Actual_charges end as Payment_Commission,Head_Load_Amount as Head_Load_Amount,Own_Asset_Amount as Own_Asset_Amount,max( EMP_Amount) as EMP_Amount from (select distinct TSPL_MILK_SRN_HEAD.DOC_cODE as Code,0 as Head_Load_Amount,0 as Own_Asset_Amount,TSPL_MILK_SRN_HEAD.DOC_DATE , tspl_Vlc_Data_Uploader.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,tspl_Vlc_Data_Uploader.mpc  Vendor,tspl_Vlc_Data_Uploader.MP_Name as Vendor_name,TSPL_ITEM_MASTER.Item_Code as  ICode ,Item_Desc as IName,tspl_Vlc_Data_Uploader.Qty  as Qty,tspl_Vlc_Data_Uploader.Qty  as ACC_Qty,0 as Unapproved,tspl_Vlc_Data_Uploader.Uom_Code as Unit, 1 as RI,tspl_Vlc_Data_Uploader.RATE as Rate,1 as Chk  ,tspl_Vlc_Data_Uploader.Amount,tspl_Vlc_Data_Uploader.fat as FAT_PER,tspl_Vlc_Data_Uploader.snf  as  SNF_PER,tspl_Vlc_Data_Uploader.qty/40   as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,tspl_Vlc_Data_Uploader.Route_no as route_code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE, TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift,TSPL_MILK_SRN_DETAIL.emp_amount " _
            & "  from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on  tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on  TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join  TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE= TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE= TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on  TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code= TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE " _
            & "  left join ( select Parent_Vendor_Code,tspl_Mp_master.mp_Code as mpc,tspl_Mp_master.mp_name,mp_code_vlc_Uploader ,TSPL_MCC_MASTER.MCC_Code ,tspl_Vlc_Data_Uploader.shift,tspl_Vlc_Data_Uploader.Doc_Date,tspl_Vlc_Data_Uploader.VLC_CODE,tspl_Vlc_Data_Uploader.Route_No,tspl_Vlc_Data_Uploader.qty,tspl_Vlc_Data_Uploader.Uom_Code,tspl_Vlc_Data_Uploader.Rate,tspl_Vlc_Data_Uploader.Amount,tspl_Vlc_Data_Uploader.fat,tspl_Vlc_Data_Uploader.snf   from tspl_Mp_master inner join tspl_Vendor_master on tspl_Vendor_master.Vendor_Code=tspl_Mp_master.MP_Code inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=tspl_Mp_master.VLC_Code inner join tspl_Vlc_Data_Uploader on tspl_Vlc_Data_Uploader.MP_CODE=tspl_Mp_master.MP_Code_VLC_Uploader and TSPL_VLC_MASTER_HEAD.Route_Code=tspl_Vlc_Data_Uploader.Route_No  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader=tspl_Vlc_Data_Uploader.MCC_Code where Parent_Vendor_Code='" + VendorCode + "'  and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between  convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103) ) tspl_Vlc_Data_Uploader on  tspl_Vlc_Data_Uploader.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and convert(date,tspl_Vlc_Data_Uploader.doc_date,103)=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) and tspl_Vlc_Data_Uploader.shift=TSPL_MILK_SRN_HEAD.SHIFT  and Route_No=TSPL_MILK_Receipt_DETAIL.ROUTE_CODE and tspl_Vlc_Data_Uploader.VLC_Code=VLC_Code_VLC_Uploader where  TSPL_MILK_SRN_HEAD.Posted=1 "
            '& " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code is null  "

            qry &= "and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)  "
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and tspl_Vlc_Data_Uploader.Parent_Vendor_Code='" + VendorCode + "'  and mpc='" & MpCode & "' " '--and TSPL_MILK_SRN_DETAIL.Status=0       
            End If

            qry &= " )Final inner join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,Service_Charge_Type,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers," _
              & " Nature,Actual_charges,payment_commision_pers,Qty,ACC_Qty,Head_Load_Amount,Own_Asset_Amount  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "


        End If
        dtAllData = clsDBFuncationality.GetDataTable(qry, trans)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            Return False
            Exit Function
        End If
        LoadHeadData()
        LoadBlankGridDetail()
        Return True
    End Function

    Public Function LoaDHeadDataQueryVsp(ByVal trans As SqlTransaction) '==========This is For Whose bill is Created and Want to Create only Incentive Of Those.
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, trans)) = 1)
        Dim qry As String = "select distinct CAST(0 as bit) as Sel,code,Invoice_Code,Final.DOC_DATE,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
            & " ,Unit ,Qty as POQty,Acc_Qty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
            & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
            & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,cans,'' as Route_Code,'' as route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,Case when Nature='C' then Actual_charges end as  Commission," _
              & " Case when Nature='E' then Actual_charges end as Payment_Commission,Head_Load_Amount as Head_Load_Amount,Own_Asset_Amount as Own_Asset_Amount from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_SRN_DETAIL.Own_Asset_Amount,TSPL_MILK_Purchase_Invoice_DETAIL.DOC_CODE as Invoice_Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
            & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
            & " ,Item_Desc as IName,TSPL_MILK_SRN_DETAIL.Qty  as Qty,TSPL_MILK_SRN_DETAIL.ACC_Qty  as ACC_Qty,0 as Unapproved,tspl_milk_receipt_Detail.Uom_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
            & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE" _
            & " =TSPL_MILK_SRN_DETAIL.DOC_CODE  Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code left join TSPL_VLC_MASTER_HEAD " _
            & " on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " _
            & " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code" _
            & " =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No  Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE left join " _
            & " TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code " _
            & " and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code=TSPL_MILK_SRN_HEAD.Doc_Code left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE where  TSPL_MILK_SRN_HEAD.Posted=1  and TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code is not null and tspl_Milk_Srn_Head.is_incentive_Created='N' and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" & clsCommon.GetPrintDate(Frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(To_date, "dd-MMM-yyyy") & "',103)  and coalesce(TSPL_MILK_SRN_DETAIL.AMOUNT,0)>0 "
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VendorCode + "'" '--and TSPL_MILK_SRN_DETAIL.Status=0       
        End If
        qry &= " --union all " _
        & " -- select TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Qty,0 as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable, TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty  as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Doc_Code where TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted=1 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Code,''))>0" _
        & " --             union all  " _
        & " --             select TSPL_MILK_PURCHASE_INVOICE_DETAIL.Bin_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_ID as Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.Vendor_Code as Vendor,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code as ICode,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Desc as IName,0  as Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_Qty as Unapproved,TSPL_MILK_PURCHASE_INVOICE_DETAIL.free_qty as FREEQty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_MILK_PURCHASE_INVOICE_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Assessable,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Leak_Qty as Leak_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Burst_Qty as  Burst_Qty,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Short_Qty as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AbatementRate  from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.PI_No=TSPL_MILK_PURCHASE_INVOICE_DETAIL.PI_No where TSPL_MILK_PURCHASE_INVOICE_HEAD.Status=0 and len(isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Id,''))>0 " & vbNewLine _
        & " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,cans,Correction_factor,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers," _
              & " ,Nature,Actual_charges,payment_commision_pers,Qty,Invoice_Code,Acc_Qty,Head_Load_Amount,Own_Asset_Amount  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "
        'Dim qry As String = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,Unit ,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
        '" SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
        '" SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
        '" SUM(Unapproved) as UnapprovedQty,SUM(FREEQty) as FreeQty," & _
        '" SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," & _
        '" MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,Final.MRP as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,0 as Assessable,max(Leak_Qty) as Leak_Qty,max(Burst_Qty) as  Burst_Qty, max(Short_Qty) as Short_Qty,max(Is_Mannual_Amt) as Is_Mannual_Amt,max(Amount) as Amount ,MAX(Line_No) as Line_No,max(AbatementRate) as AbatementRate,max(Bin_No) as Bin_No from ( " & _
        '" select TSPL_SRN_DETAIL.Bin_No,TSPL_SRN_DETAIL.SRN_No as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL .free_qty as FREEQty,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_HEAD.Tax_Group,TSPL_SRN_DETAIL.TAX1_Rate,TSPL_SRN_DETAIL.TAX2_Rate,TSPL_SRN_DETAIL.TAX3_Rate,TSPL_SRN_DETAIL.TAX4_Rate,TSPL_SRN_DETAIL.TAX5_Rate,TSPL_SRN_DETAIL.TAX6_Rate,TSPL_SRN_DETAIL.TAX7_Rate,TSPL_SRN_DETAIL.TAX8_Rate,TSPL_SRN_DETAIL.TAX9_Rate,TSPL_SRN_DETAIL.TAX10_Rate,TSPL_SRN_DETAIL.TAX1_Amt,TSPL_SRN_DETAIL.TAX2_Amt,TSPL_SRN_DETAIL.TAX3_Amt,TSPL_SRN_DETAIL.TAX4_Amt,TSPL_SRN_DETAIL.TAX5_Amt,TSPL_SRN_DETAIL.TAX6_Amt,TSPL_SRN_DETAIL.TAX7_Amt,TSPL_SRN_DETAIL.TAX8_Amt,TSPL_SRN_DETAIL.TAX9_Amt,TSPL_SRN_DETAIL.TAX10_Amt,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Assessable,TSPL_SRN_DETAIL.Leak_Qty,TSPL_SRN_DETAIL.Burst_Qty,TSPL_SRN_DETAIL.Short_Qty,TSPL_SRN_DETAIL.Is_Mannual_Amt,TSPL_SRN_DETAIL.Amount,TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.AbatementRate from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1  and TSPL_SRN_DETAIL.Row_Type='" + clsItemRowType.RowTypeItem + "'"
        'If clsCommon.myLen(VendorCode) > 0 Then
        '    qry += " and TSPL_SRN_HEAD.Vendor_Code='" + VendorCode + "'"
        'End If
        'qry += " union all " & _
        '" select TSPL_PI_DETAIL.Bin_No,TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty,0 as Unapproved,TSPL_PI_DETAIL.free_qty as FREEQty,TSPL_PI_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_PI_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_PI_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_PI_DETAIL.Assessable, TSPL_PI_DETAIL.Leak_Qty as Leak_Qty,TSPL_PI_DETAIL.Burst_Qty as  Burst_Qty,TSPL_PI_DETAIL.Short_Qty  as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_PI_DETAIL.AbatementRate from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 " & _
        '" union all  " & _
        '" select TSPL_PI_DETAIL.Bin_No,TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved,TSPL_PI_DETAIL.free_qty as FREEQty,TSPL_PI_DETAIL.Unit_Code as Unit,'' as Location,-1 as RI,TSPL_PI_DETAIL.Item_Cost as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,TSPL_PI_DETAIL.MRP as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,TSPL_PI_DETAIL.Assessable,TSPL_PI_DETAIL.Leak_Qty as Leak_Qty,TSPL_PI_DETAIL.Burst_Qty as  Burst_Qty,TSPL_PI_DETAIL.Short_Qty as Short_Qty,0 as Is_Mannual_Amt,0 as Amount,0 as  Line_No,TSPL_PI_DETAIL.AbatementRate  from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('" + strCurrCode + "') " & _
        'qry += ")Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' group by Code,ICode,Unit,MRP  having SUM(Chk)>0 " _
        '& " and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0 and (   sum(Leak_Qty)<>0 or sum(Burst_Qty)<>0  or sum(Short_Qty)<>0 ))) " _
        '& " and (SUM((Qty *RI)- Unapproved)<>0 or (SUM(Qty*RI)=0 and (sum(Leak_Qty*RI)<>0 or sum(Burst_Qty*RI)<>0  or sum(Short_Qty*RI)<>0)))" _
        '& " order by Code, Line_No "
        dtAllData = clsDBFuncationality.GetDataTable(qry, trans)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            ' common.clsCommon.MyMessageBoxShow("No item found for vendor " + VendorName + "")
            Return False
            Exit Function
        End If
        LoadHeadData()
        LoadBlankGridDetail()
        Return True
    End Function

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("DOC_DATE"))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHMCC).Value = clsCommon.myCstr(dr("MCC_Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHShift).Value = clsCommon.myCstr(dr("Shift"))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVLCCode).Value = clsCommon.myCstr(dr("VLC_Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVLCName).Value = clsCommon.myCstr(dr("VLC_NAME"))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHRouteCode).Value = clsCommon.myCstr(dr("Route_Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHRouteName).Value = clsCommon.myCstr(dr("Route_NAME"))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVEHICLECode).Value = clsCommon.myCstr(dr("Vehicle_Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVEHICLENAME).Value = clsCommon.myCstr(dr("Vehicle_NAME"))



            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "SRN No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)


        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "MCC Code"
        repoLocation.Name = colHMCC
        repoLocation.Width = 170
        repoLocation.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLocation)

        Dim repoShift As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShift.FormatString = ""
        repoShift.HeaderText = "Shift"
        repoShift.Name = colHShift
        repoShift.Width = 170
        repoShift.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoShift)

        Dim repoVLcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLcCode.FormatString = ""
        repoVLcCode.HeaderText = "VLC Code"
        repoVLcCode.Name = colHVLCCode
        repoVLcCode.Width = 170
        repoVLcCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVLcCode)

        Dim repoVLCname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCname.FormatString = ""
        repoVLCname.HeaderText = "VLC Name"
        repoVLCname.Name = colHVLCName
        repoVLCname.Width = 170
        repoVLCname.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVLCname)


        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "VSP Code"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "VSP Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        Dim repoRouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteCode.FormatString = ""
        repoRouteCode.HeaderText = "Route Code"
        repoRouteCode.Name = colHRouteCode
        repoRouteCode.Width = 170
        repoRouteCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoRouteCode)

        Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteName.FormatString = ""
        repoRouteName.HeaderText = "Route Name"
        repoRouteName.Name = colHRouteName
        repoRouteName.Width = 170
        repoRouteName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoRouteName)

        Dim repoVehicleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicleCode.FormatString = ""
        repoVehicleCode.HeaderText = "Vehicle Code"
        repoVehicleCode.Name = colHVEHICLECode
        repoVehicleCode.Width = 170
        repoVehicleCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVehicleCode)

        Dim repoVehicleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicleName.FormatString = ""
        repoVehicleName.HeaderText = "Vehicle Name"
        repoVehicleName.Name = colHVEHICLENAME
        repoVehicleName.Width = 170
        repoVehicleName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVehicleName)


        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "SRN No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoInvoiceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceCode.FormatString = ""
        repoInvoiceCode.HeaderText = "Invoice No"
        repoInvoiceCode.Name = colDInvoiceCode
        repoInvoiceCode.Width = 0
        repoInvoiceCode.IsVisible = False
        repoInvoiceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoInvoiceCode)

        Dim repoVLCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVLCCode.FormatString = ""
        repoVLCCode.HeaderText = "VLC Code"
        repoVLCCode.Name = colHVLCCode
        repoVLCCode.Width = 180
        repoVLCCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVLCCode)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "SRN Qty"
        repoOrderQty.Name = colDQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAccQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAccQty.FormatString = ""
        repoAccQty.HeaderText = "ACC Qty"
        repoAccQty.Name = colDAcc_Qty
        repoAccQty.ReadOnly = True
        repoAccQty.Width = 80
        repoAccQty.WrapText = True
        repoAccQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAccQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repocans As GridViewDecimalColumn = New GridViewDecimalColumn()
        repocans.FormatString = ""
        repocans.HeaderText = "Cans"
        repocans.Name = ColCANS
        repocans.ReadOnly = True
        repocans.Width = 80
        repocans.WrapText = True
        repocans.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repocans)

        Dim repoService_Charge As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoService_Charge.FormatString = ""
        repoService_Charge.HeaderText = "Service Charge Type"
        repoService_Charge.Name = ColService_Charge
        repoService_Charge.Width = 0
        repoService_Charge.IsVisible = False
        repoService_Charge.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoService_Charge)

        Dim repocommission As GridViewDecimalColumn = New GridViewDecimalColumn()
        repocommission.FormatString = ""
        repocommission.HeaderText = "Commission"
        repocommission.Name = ColCOMMISSION
        repocommission.ReadOnly = True
        repocommission.Width = 80
        repocommission.WrapText = True
        repocommission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repocommission)


        Dim repoPaymentcommission As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentcommission.FormatString = ""
        repoPaymentcommission.HeaderText = "Payment Commission"
        repoPaymentcommission.Name = ColPAYMENTCOMMISSION
        repoPaymentcommission.ReadOnly = True
        repoPaymentcommission.IsVisible = False
        repoPaymentcommission.Width = 0
        repoPaymentcommission.WrapText = True
        repoPaymentcommission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPaymentcommission)

        Dim repoHeadLoad As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadLoad.FormatString = ""
        repoHeadLoad.HeaderText = "Head Load Amount"
        repoHeadLoad.Name = ColHead_Load_Amount
        repoHeadLoad.IsVisible = True
        repoHeadLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHeadLoad.ReadOnly = True
        repoHeadLoad.WrapText = True
        repoHeadLoad.Width = 100
        gv1.MasterTemplate.Columns.Add(repoHeadLoad)

        Dim repoOwnAsset As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOwnAsset.FormatString = ""
        repoOwnAsset.HeaderText = "Own Asset Amount"
        repoOwnAsset.Name = ColOwn_Asset_AMount
        repoOwnAsset.IsVisible = True
        repoOwnAsset.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOwnAsset.ReadOnly = True
        repoOwnAsset.WrapText = True
        repoOwnAsset.Width = 100
        gv1.MasterTemplate.Columns.Add(repoOwnAsset)



        Dim repoFATPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATPer.FormatString = ""
        repoFATPer.HeaderText = "FAT(%)"
        repoFATPer.Name = ColFATPer
        repoFATPer.ReadOnly = True
        repoFATPer.Width = 80
        repoFATPer.WrapText = True
        repoFATPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATPer)


        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF(%)"
        repoSNFPer.Name = ColSNFPer
        repoSNFPer.ReadOnly = True
        repoSNFPer.Width = 80
        repoSNFPer.WrapText = True
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPer)


        Dim repoCLR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCLR.FormatString = ""
        repoCLR.HeaderText = "CLR"
        repoCLR.Name = ColCLR
        repoCLR.ReadOnly = True
        repoCLR.Width = 80
        repoCLR.WrapText = True
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCLR)


        repoCLR = New GridViewDecimalColumn()
        repoCLR.FormatString = ""
        repoCLR.HeaderText = "EMP Amount"
        repoCLR.Name = ColEMPAmt
        repoCLR.ReadOnly = True
        repoCLR.Width = 80
        repoCLR.WrapText = True
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCLR)


        Dim repoCorrection_Factor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCorrection_Factor.FormatString = ""
        repoCorrection_Factor.HeaderText = "Correction Factor"
        repoCorrection_Factor.Name = ColCorrection_Factor
        repoCorrection_Factor.ReadOnly = True
        repoCorrection_Factor.Width = 100
        repoCorrection_Factor.WrapText = True
        repoCorrection_Factor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCorrection_Factor)

        Dim repoFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG.)"
        repoFATKG.Name = ColFATKG
        repoFATKG.ReadOnly = True
        repoFATKG.Width = 80
        repoFATKG.WrapText = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATKG)


        Dim repoSNFKg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKg.FormatString = ""
        repoSNFKg.HeaderText = "SNF(KG.)"
        repoSNFKg.Name = ColSNFKG
        repoSNFKg.ReadOnly = True
        repoSNFKg.Width = 80
        repoSNFKg.WrapText = True
        repoSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKg)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        'repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colDAmount
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        'repoAmt.IsVisible = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Service charge Amount"
        repoAmt.Name = colDServiceChargeAmount
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        'repoAmt.IsVisible = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    ''Sub setGridPropery()
    ''    gv1.AllowAddNewRow = False
    ''    gv1.ShowGroupPanel = False
    ''    gv1.AllowColumnReorder = True
    ''    gv1.AllowRowReorder = True
    ''    gv1.EnableSorting = False
    ''    gv1.MasterTemplate.ShowRowHeaderColumn = False
    ''    gv1.MasterTemplate.ShowColumnHeaders = True
    ''    ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
    ''    gv1.EnableAlternatingRowColor = True
    ''    gv1.EnableFiltering = True
    ''    gv1.ShowFilteringRow = True
    ''End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()

    End Sub

    Sub btnCancelPressed()
    End Sub

    Public Sub btnOKPressed()
        If Not isAllowed() Then
            Exit Sub
        End If
        Dim doccode As String = ""
        ArrReturn = New List(Of clsMilkSRNMCCDetail)
        Dim obj As clsMilkSRNMCCDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsMilkSRNMCCDetail()
                obj.DOC_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                doccode = IIf(doccode = "", "'" & obj.DOC_CODE & "'", doccode & ",'" & obj.DOC_CODE & "'")
                obj.Item_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.RATE = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                obj.MILK_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDQty).Value)
                obj.ACC_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAcc_Qty).Value)
                obj.AMOUNT = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAmount).Value)

                obj.Service_Charge_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDServiceChargeAmount).Value)


                obj.SNF = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColSNFPer).Value)
                obj.FAT = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFATPer).Value)
                obj.CLR = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCLR).Value)
                obj.Cans = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCANS).Value)
                Try
                    obj.Emp_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColEMPAmt).Value)
                Catch ex As Exception
                End Try

                obj.Service_Charge_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(ColService_Charge).Value)
                obj.Commission = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCOMMISSION).Value)
                obj.Payment_Commission = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColPAYMENTCOMMISSION).Value)
                obj.Own_Asset_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColOwn_Asset_AMount).Value)
                obj.Head_Load_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColHead_Load_Amount).Value)
                obj.Correction_Factor = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCorrection_Factor).Value)
                obj.VlC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colHVLCCode).Value)
                If gv1.Columns.Contains(colDInvoiceCode) Then
                    obj.Invoice_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDInvoiceCode).Value)
                Else
                    obj.Invoice_Code = ""
                End If

                'obj.VlC_Doc_Code = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDCode).Value)
                ArrReturn.Add(obj)
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending SRN item")
        Else
        End If
    End Sub

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDCode) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        'If Not IsInsideLoadData Then
        '    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
        '    If clsCommon.myLen(strCode) > 0 Then
        '        LoadDetailData(e.NewValue, strCode)
        '    End If
        'End If
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                If clsCommon.myLen(VendorCode) <= 0 Then
                    VendorCode = strVendorCode
                    VendorName = strVendorName
                End If
                If fORMCode Then
                    'Dim sQuery As String = "select count(*) from tspl_mp_master inner join tspl_vlc_master_Head on tspl_vlc_master_Head.vlc_Code=tspl_mp_master.vlc_Code and vsp_Code='" & VendorCode & "' and mp_Code='" & strVendorCode & "'"
                    'Dim cc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sQuery, stran))
                    'If cc = 1 Then
                    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                    If clsCommon.myLen(strCode) > 0 Then
                        LoadDetailData(e.NewValue, strCode)
                        VendorCode = strVendorCode
                        VendorName = strVendorName
                        'Else
                        '    common.clsCommon.MyMessageBoxShow("SRN's Vendor should be `" + VendorName)
                        '    e.Cancel = True
                    End If
                Else
                    If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
                        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                        If clsCommon.myLen(strCode) > 0 Then
                            LoadDetailData(e.NewValue, strCode)
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("SRN's Vendor should be `" + VendorName)
                        e.Cancel = True
                    End If

                End If
            End If
        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAcc_Qty).Value = clsCommon.myCdbl(dr("ACC_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColEMPAmt).Value = clsCommon.myCdbl(dr("EMP_Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCorrection_Factor).Value = clsCommon.myCstr(dr("Correction_Factor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATPer).Value = clsCommon.myCstr(dr("FAT_per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFPer).Value = clsCommon.myCstr(dr("SNF_per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCLR).Value = clsCommon.myCdbl(dr("CLR"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCANS).Value = clsCommon.myCdbl(dr("Cans"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHVLCCode).Value = clsCommon.myCstr(dr("VLC_Code"))
                    If dtAllData.Columns.Contains("Invoice_Code") Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDInvoiceCode).Value = clsCommon.myCstr(dr("Invoice_Code"))
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColCOMMISSION).Value = clsCommon.myCdbl(dr("Commission"))
                    If dtAllData.Columns.Contains("Service_Charge_Type") Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColService_Charge).Value = clsCommon.myCstr(dr("Service_Charge_Type"))
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColPAYMENTCOMMISSION).Value = clsCommon.myCdbl(dr("Payment_Commission"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColHead_Load_Amount).Value = clsCommon.myCdbl(dr("Head_Load_Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColOwn_Asset_AMount).Value = clsCommon.myCdbl(dr("Own_Asset_Amount"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATKG).Value = (clsCommon.myCdbl(dr("FAT_per")) * clsCommon.myCdbl(dr("POQty"))) / 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFKG).Value = (clsCommon.myCdbl(dr("SNF_per")) * clsCommon.myCdbl(dr("POQty"))) / 100
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDServiceChargeAmount).Value = clsCommon.myCdbl(dr("Service_Charge_Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAmount).Value = clsCommon.myCdbl(dr("Amount")) ' + (clsCommon.myCdbl(dr("Commission")) * clsCommon.myCdbl(dr("Amount")) / 100)

                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If

    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrLoc As New List(Of String)
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value)
                For jj As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myCBool(gv1.Rows(jj).Cells(colDSelect).Value) AndAlso clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(jj).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                        Dim strVendorCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                        If Not arrVendor.Contains(strVendorCode) Then
                            arrVendor.Add(strVendorCode)
                            If Not MultipleFinderFillAuto Then
                                If arrVendor.Count > 1 Then
                                    clsCommon.MyMessageBoxShow("Items of more than one VSP not acceptable ")
                                    Return False
                                End If
                            End If
                            VendorCode = strVendorCode
                            VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)
                        End If
                        Dim StrLoc As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHMCC).Value)
                        If Not arrLoc.Contains(StrLoc) Then
                            arrLoc.Add(StrLoc)
                            If Not MultipleFinderFillAuto Then
                                If arrLoc.Count > 1 Then
                                    clsCommon.MyMessageBoxShow("Items of more than one MCC not acceptable ")
                                    Return False
                                End If
                            End If
                            VendorCode = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                            VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)
                        End If
                    End If
                Next
            End If
        Next
        Return True
    End Function
End Class
