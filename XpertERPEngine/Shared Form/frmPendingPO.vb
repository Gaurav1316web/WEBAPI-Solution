Imports common

Public Class frmPendingPO

#Region "Variables"
    Public IsItemInsuranceColumn As Boolean = False
    Public ItemForDocumentFilter As String = Nothing
    Dim ShowCapexCodeandSubCode As Boolean = False
    Public strFormType As String = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public IsMerchantTrade As String = Nothing
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public Is_Load_MRN As String = False
    Public Is_From_RGP As Boolean = False
    Public Bill_To_Location As String = Nothing
    Public ArrReturn As List(Of clsPurchaseOrderDetail) = Nothing
    Public ArrReturn_EX As List(Of clsEXSalesOrderDetail) = Nothing
    Public dtGRNDate? As Date = Nothing
    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False

    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colItemTol As String = "tolerencePer"
    Const colItemTolAmt As String = "ItemTolerenceQty"
    Const colDIType As String = "IType"
    Const colRowType As String = "COLTYPE"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDDamageQty As String = "DAMAGEQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDTaxRate1 As String = "TaxRate1"
    Const colDTaxRate2 As String = "TaxRate2"
    Const colDTaxRate3 As String = "TaxRate3"
    Const colDTaxRate4 As String = "TaxRate4"
    Const colDTaxRate5 As String = "TaxRate5"
    Const colDTaxRate6 As String = "TaxRate6"
    Const colDTaxRate7 As String = "TaxRate7"
    Const colDTaxRate8 As String = "TaxRate8"
    Const colDTaxRate9 As String = "TaxRate9"
    Const colDTaxRate10 As String = "TaxRate10"
    Const colDTaxAmt1 As String = "TaxAmt1"
    Const colDTaxAmt2 As String = "TaxAmt2"
    Const colDTaxAmt3 As String = "TaxAmt3"
    Const colDTaxAmt4 As String = "TaxAmt4"
    Const colDTaxAmt5 As String = "TaxAmt5"
    Const colDTaxAmt6 As String = "TaxAmt6"
    Const colDTaxAmt7 As String = "TaxAmt7"
    Const colDTaxAmt8 As String = "TaxAmt8"
    Const colDTaxAmt9 As String = "TaxAmt9"
    Const colDTaxAmt10 As String = "TaxAmt10"
    Const colDHeaderDisPer As String = "colDHeaderDisPer"
    Const colDBinNo As String = "BinNo"
    Const colDAgainstItemWiseTaxCode As String = "colDAgainstItemWiseTaxCode"
    Const colDTaxableAmountPer As String = "colDTaxableAmountPer"
    Const colDIsBlanket As String = "colDIsBlanket"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "Assessable"
    Const colAbatementRate As String = "colAbatementRate"

    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHMRN_Date As String = "colHMRN_Date"
    Const colHGRN_DATE As String = "GRN_DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHMRNCode As String = "colHMRNCode"
    Const colHGRNCode As String = "colHGRNCode"
    Const colHSaleInvoiceNo As String = "colHSaleInvoiceNo"
    Private blnShowInvoiceNo As String = "blnShowInvoiceNo"
    Const colHPotype As String = "PoType"
    Const colHBill_To_Location As String = "colHBill_To_Location"
    Const colHReferencePo As String = "colHReferencePo"
    Const colHTenderNo As String = "colHTenderNo"
    Dim OpenPOForShorateLeakageQty As Boolean = False
    Dim AutoClosePOBasedOnSRNQtyWithTolerance As Boolean = False

#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AutoClosePOBasedOnSRNQtyWithTolerance = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePOBasedOnSRNQtyWithTolerance, clsFixedParameterCode.AutoClosePOBasedOnSRNQtyWithTolerance, Nothing)) = 1, True, False)
        OpenPOForShorateLeakageQty = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenPOforRejectShortageQty, clsFixedParameterCode.OpenPOforRejectShortageQty, Nothing)) = 1, True, False)
        Dim qry As String = ""
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        If strFormType IsNot Nothing AndAlso clsCommon.myLen(strFormType) > 0 AndAlso clsCommon.CompairString(strFormType, "EXPORT SO") = CompairStringResult.Equal Then
            blnShowInvoiceNo = "0"
            qry = "select '' as Against_Item_Wise_Tax_Rate, '' as Vendor,'' as VendorName,CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,'' as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," &
                        " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," &
                        " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," &
                        " SUM(Unapproved) as UnapprovedQty," &
                        " SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty ,MAX(Rate) as Rate," &
                        " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(mrp) as MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate,max(Bin_No) as Bin_No,max(SaleInvoiceNo) as SaleInvoiceNo,max(Taxable_Amount_Per) as Taxable_Amount_Per from ( " + Environment.NewLine &
                        " select TSPL_PURCHASE_ORDER_DETAIL.Taxable_Amount_Per,'' as SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate from TSPL_PURCHASE_ORDER_DETAIL  left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.Ref_No left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 AND TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='" + PurchaseOrder_Type + "' and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' and TSPL_PURCHASE_ORDER_HEAD.item_type<>'N' and TSPL_TENDER_HEADER.close_yn='N' "
            qry += " union all" + Environment.NewLine &
            " select 0 as Taxable_Amount_Per,'' as SaleInvoiceNo,TSPL_SD_SALES_ORDER_DETAIL.Bin_No,TSPL_SD_SALES_ORDER_DETAIL.cust_po_no as Code,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_SD_SALES_ORDER_DETAIL.Qty,0 as Unapproved,TSPL_SD_SALES_ORDER_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SD_SALES_ORDER_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SD_SALES_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_SD_SALES_ORDER_DETAIL  left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_DETAIL.document_code=TSPL_SD_SALES_ORDER_HEAD.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALES_ORDER_DETAIL.item_code where TSPL_SD_SALES_ORDER_HEAD.Status=1  and TSPL_SD_SALES_ORDER_HEAD.cust_po_type='" + PurchaseOrder_Type + "' and len(isnull(TSPL_SD_SALES_ORDER_DETAIL.cust_po_no,''))>0  " + Environment.NewLine &
            " union all  " + Environment.NewLine &
            " select 0 as Taxable_Amount_Per,'' as SaleInvoiceNo,TSPL_SD_SALES_ORDER_DETAIL.Bin_No,TSPL_SD_SALES_ORDER_DETAIL.cust_po_no as Code,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_SD_SALES_ORDER_DETAIL.qty as Unapproved,TSPL_SD_SALES_ORDER_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SD_SALES_ORDER_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SD_SALES_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_SD_SALES_ORDER_DETAIL  left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_DETAIL.document_code=TSPL_SD_SALES_ORDER_HEAD.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALES_ORDER_DETAIL.item_code where TSPL_SD_SALES_ORDER_HEAD.Status=0 and TSPL_SD_SALES_ORDER_HEAD.cust_po_type='" + PurchaseOrder_Type + "' and len(isnull(TSPL_SD_SALES_ORDER_DETAIL.cust_po_no,''))>0 and TSPL_SD_SALES_ORDER_DETAIL.document_code not in ('" + strCurrCode + "')  " + Environment.NewLine &
            " )Final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += "  where TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            qry += " group by Code,ICode,Unit having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 order by Code,ICode "
        Else
            blnShowInvoiceNo = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSaleInvoiceNoInPOfinderInSRN, clsFixedParameterCode.ShowSaleInvoiceNoInPOfinderInSRN, Nothing)) = 1, True, False)

            If clsCommon.myLen(VendorCode) > 0 Then
                Me.Text = Me.Text + " For " + VendorName
            End If

            ''richa 28/08/2014 Against Ticket no.BM00000003667
            Dim desc As String = ""
            Dim strCondition As String = ""
            desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InvoiceBasedPO, clsFixedParameterCode.InvoiceBasedPO, Nothing))
            If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
                strCondition = " and ISNULL(TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,'')<>'' "
            End If
            ''------------------------------------------------------
            If Is_Load_MRN Then
                'UDL/14/03/19-000280 by balwinder on 14/03/2019
                qry = "select xx.*, TSPL_PURCHASE_ORDER_HEAD.ReferencePO  from ( select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," &
                      " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," &
                      " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," &
                      " SUM(Unapproved) as UnapprovedQty," &
                      " SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty ,MAX(Rate) as Rate," &
                      " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate,max(Bin_No) as Bin_No,MRN_No as MRN_No,MAX(GRN_No) as GRN_No,max(MRN_Date) as MRN_Date,Max(Grn_date) as GRN_date,max(Category) as category,max(Emergency) as emergency,max(Capex_Code) as capex_code,max(Capex_SubCode) as capex_subcode,max(Against_Item_Wise_Tax_Rate) as Against_Item_Wise_Tax_Rate,max(Taxable_Amount_Per) as Taxable_Amount_Per,max(Header_Discount_Per) as Header_Discount_Per,Max(Final.Item_Insurance_Apply_On) as Item_Insurance_Apply_On,Max(Final.Item_Insurance_Rate) as Item_Insurance_Rate,Max(Final.Item_Insurance_Amt) as Item_Insurance_Amt from ( " + Environment.NewLine &
                      " select TSPL_MRN_DETAIL.Header_Discount_Per,TSPL_MRN_DETAIL.Taxable_Amount_Per, TSPL_MRN_DETAIL.Against_Item_Wise_Tax_Rate, TSPL_MRN_DETAIL.Category,TSPL_MRN_DETAIL.Emergency,TSPL_MRN_DETAIL.Capex_Code,TSPL_MRN_DETAIL.Capex_SubCode,pod.Bin_No,coalesce(TSPL_MRN_DETAIL.PO_ID,TSPL_MRN_DETAIL.RGP_No) as Code,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1.0 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,TSPL_MRN_DETAIL.GRN_Id as GRN_No,MRN_Date,TSPL_GRN_HEAD.GRN_Date,TSPL_MRN_DETAIL.Item_Insurance_Apply_On,TSPL_MRN_DETAIL.Item_Insurance_Rate,TSPL_MRN_DETAIL.Item_Insurance_Amt " + Environment.NewLine +
                      " from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRNo=TSPL_MRN_DETAIL.GRN_Id  " +
                      " left outer join (select PurchaseOrder_No,Item_Code,max(Bin_No) as Bin_No,max(Abatementrate) as Abatementrate from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No,Item_Code) as  pod on TSPL_MRN_DETAIL.PO_ID=pod.PurchaseOrder_No and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code  " + Environment.NewLine +
                      " where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1 and TSPL_MRN_HEAD.IsCancel=0   "
                If clsCommon.myLen(VendorCode) > 0 Then
                    qry += " and TSPL_MRN_Head.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
                End If
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qry += "  and TSPL_MRN_Head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                qry += " union all" + Environment.NewLine &
                " select 0 as Header_Discount_Per,0 as Taxable_Amount_Per, '' as Against_Item_Wise_Tax_Rate,TSPL_SRN_DETAIL.Category,TSPL_SRN_DETAIL.Emergency,TSPL_SRN_DETAIL.Capex_Code,TSPL_SRN_DETAIL.Capex_SubCode,TSPL_SRN_DETAIL.Bin_No,coalesce(TSPL_SRN_DETAIL.PO_ID,TSPL_SRN_DETAIL.RGP_ID) as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,TSPL_SRN_DETAIL.GRN_ID as GRN_No,null,null,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0   and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No) " + Environment.NewLine &
                " union all  " + Environment.NewLine &
                " select 0 as Header_Discount_Per,0 as Taxable_Amount_Per, '' as Against_Item_Wise_Tax_Rate,TSPL_SRN_DETAIL.Category,TSPL_SRN_DETAIL.Emergency,TSPL_SRN_DETAIL.Capex_Code,TSPL_SRN_DETAIL.Capex_SubCode,TSPL_SRN_DETAIL.Bin_No,coalesce(TSPL_SRN_DETAIL.PO_ID,TSPL_SRN_DETAIL.RGP_ID) as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,TSPL_SRN_DETAIL.GRN_ID as GRN_No,null,null,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrCode + "')   and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No) " + Environment.NewLine &
                " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'  
                   where 1=1 "
                ''item which are mpped for qc ,exclude that from pendign queries
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowQualityModuleInERP, clsFixedParameterCode.AllowQualityModuleInERP, Nothing)), "1") = CompairStringResult.Equal Then
                    qry += " and final.icode not in (select distinct TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Item_Code from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.Document_Code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Document_Code where TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code=final.vendor) "
                End If

                ''==============end here===============================
                qry += " group by Code,MRN_No,GRN_No,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) >0  "
                qry += " )xx left join tspl_item_master on tspl_item_master.item_code =xx.ICode  left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = xx.Code "
                qry += " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=  xx.GRN_No "
                qry += " where (isnull(tspl_item_master.Is_AllowQC_ON_Purchase,0)=0 and not exists(select item_code from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code=xx.ICode))
                        or TSPL_GRN_HEAD.IsSkipPurchaseQC=1 "
                qry += " order by Code,MRN_No,GRN_No,ICode"

            Else
                Dim strSchcondition As String = ""
                Dim strGRNcondition As String = ""
                Dim strPOType As String = ""
                Dim strRGPType As String = ""
                Dim strSRNCondition As String = ""
                If clsCommon.myLen(PurchaseOrder_Type) > 0 Then
                    strPOType = " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='" + PurchaseOrder_Type + "'"
                    strSchcondition = " and TSPL_PO_SCH_HEAD.po_type='" + PurchaseOrder_Type + "'"
                    strGRNcondition = " and tspl_grn_head.PurchaseOrder_Type='" + PurchaseOrder_Type + "'"
                    strSRNCondition = " and tspl_srn_head.PurchaseOrder_Type='" + PurchaseOrder_Type + "'"
                End If

                qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,max(tolerencePer) as tolerencePer,
	  max(ItemTolerenceQty) as ItemTolerenceQty ,MAX(IType) as IType,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," &
                        " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," &
                        " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," &
                        " SUM(Unapproved) as UnapprovedQty," &
                        " SUM((Qty *RI)- Unapproved " + IIf(OpenPOForShorateLeakageQty, "+", "-") + " DamageQty) as PedningQty ,MAX(Rate) as Rate," &
                        " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate,max(Bin_No) as Bin_No,max(SaleInvoiceNo) as SaleInvoiceNo,max(Against_Item_Wise_Tax_Rate) as Against_Item_Wise_Tax_Rate,max(Taxable_Amount_Per) as Taxable_Amount_Per,sum(isBlanket) as isBlanket,max(Header_Discount_Per) as Header_Discount_Per,max(ReferencePo) as ReferencePo,max(Item_Insurance_Apply_On) as Item_Insurance_Apply_On ,max(Item_Insurance_Rate) as Item_Insurance_Rate ,max(Item_Insurance_Amt) as Item_Insurance_Amt, max(RefTendorNo) as RefTendorNo from ( " + Environment.NewLine &
                        " select TSPL_PURCHASE_ORDER_HEAD.ReferencePo,TSPL_PURCHASE_ORDER_DETAIL.Header_Discount_Per,TSPL_PURCHASE_ORDER_HEAD.isBlanket,TSPL_PURCHASE_ORDER_DETAIL.Taxable_Amount_Per, TSPL_PURCHASE_ORDER_DETAIL.Against_Item_Wise_Tax_Rate, TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName, tspl_item_master.tolerence as tolerencePer,
	 (
          TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty * ISNULL(TSPL_ITEM_MASTER.tolerence, 0)
        )/ 100 as ItemTolerenceQty,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType"
                If AutoClosePOBasedOnSRNQtyWithTolerance Then
                    qry += ",TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty+((TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty * ISNULL(TSPL_ITEM_MASTER.tolerence ,0))/100) "
                Else
                    qry += ",TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty "
                End If

                qry += " as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate,TSPL_PURCHASE_ORDER_DETAIL.Item_Insurance_Apply_On,TSPL_PURCHASE_ORDER_DETAIL.Item_Insurance_Rate,TSPL_PURCHASE_ORDER_DETAIL.Item_Insurance_Amt,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo  from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' and  TSPL_PURCHASE_ORDER_HEAD.IsCancel=0 and TSPL_PURCHASE_ORDER_HEAD.item_type<>'N' " + strPOType + " " + strCondition + " "
                If Not Is_From_RGP Then
                    qry += " AND TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type<>'J' " + Environment.NewLine
                End If
                If clsCommon.myLen(VendorCode) > 0 Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
                End If
                If clsCommon.CompairString(IsMerchantTrade, "MT") = CompairStringResult.Equal Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1 " + Environment.NewLine
                Else
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =0 " + Environment.NewLine
                End If
                If dtGRNDate IsNot Nothing Then
                    qry += " and 2=(case when TSPL_PURCHASE_ORDER_HEAD.isBlanket=1 then (case when TSPL_PURCHASE_ORDER_HEAD.Expiry_Date>='" + clsCommon.GetPrintDate(dtGRNDate, "dd/MMM/yyyy") + "' then 2 else 3 end)  else 2 end) " ''BHA/27/06/18-000101. By Balwinder on 03/07/2018
                End If


                Dim UDLPurchaseOrderthroughAP As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UDLPurchaseOrderthroughAP, clsFixedParameterCode.UDLPurchaseOrderthroughAP, Nothing)) = 1, True, False)
                If UDLPurchaseOrderthroughAP = True Then
                    qry += "  and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type <> 'S'" + Environment.NewLine
                End If
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qry += "  and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                If clsCommon.myLen(ItemForDocumentFilter) > 0 Then
                    qry += "  and TSPL_PURCHASE_ORDER_DETAIL.Item_Code = '" + ItemForDocumentFilter + "'"
                End If
                qry += " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per, 0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,
                '' as SaleInvoiceNo,'' as Bin_No,TSPL_SRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,
                '' as IType, CASE 
  WHEN ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0) > 0 
    THEN TSPL_SRN_DETAIL.SRN_Qty 
  ELSE (
    CASE 
      WHEN ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0) > 0 
        THEN 
          CASE 
            WHEN TSPL_NIR_QC.QC_Status = 0 
              THEN 0 
            ELSE TSPL_MRN_DETAIL.MRN_Qty 
          END
      ELSE (
        CASE 
          WHEN (
            tspl_grn_head.VisualQCStatusSecond = 3 
            OR tspl_grn_head.VisualQCStatus = 3
          ) 
            THEN 0 
          ELSE TSPL_GRN_DETAIL.GRN_Qty 
        END
      )
    END
  )
END AS Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,
                0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,
                0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,
                null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,
                (isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate,
                '' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo
                from TSPL_SRN_DETAIL 
                left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN
                left outer join tspl_grn_detail on tspl_grn_detail.GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_SRN_HEAD.Against_MRN
                left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No
                left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SRN_DETAIL.item_code
                where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 and len(isnull(tspl_srn_detail.MRN_Id,''))<=0 " + strSRNCondition + " and len(isnull(tspl_srn_detail.GRN_Id,''))<=0  and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No)  " + Environment.NewLine &
                " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,
                '' as SaleInvoiceNo,''as BinNo, TSPL_SRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,
                '' as IType,0  as Qty,  case when isnull(TSPL_SRN_DETAIL.SRN_Qty,0)>0 THEN TSPL_SRN_DETAIL.SRN_Qty ELSE (
                case when isnull(TSPL_MRN_DETAIL.MRN_Qty,0)>0 then TSPL_MRN_DETAIL.MRN_Qty  ELSE (
                CASE when (tspl_grn_head.VisualQCStatusSecond=3 or tspl_grn_head.VisualQCStatus=3) then  (0)
                else (TSPL_GRN_DETAIL.GRN_Qty) end)End)end as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,
                '' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,
                0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,
                0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,
                (isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate,
                '' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo 
                from TSPL_SRN_DETAIL 
                left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN 
                left outer join tspl_grn_detail on tspl_grn_detail.GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_SRN_HEAD.Against_MRN
                left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SRN_DETAIL.item_code 
                where TSPL_SRN_HEAD.Status=0 and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrCode + "') and len(isnull(TSPL_SRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 and len(isnull(tspl_srn_detail.MRN_Id,''))<=0 and len(isnull(tspl_srn_detail.GRN_Id,''))<=0  and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No) " + strSRNCondition + "  " + Environment.NewLine
                qry += " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,
                '' as Bin_No,TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,
                '' as IType,  CASE 
  WHEN ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0) > 0 
    THEN TSPL_SRN_DETAIL.SRN_Qty 
  ELSE (
    CASE 
      WHEN ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0) > 0 
        THEN 
          CASE 
            WHEN TSPL_NIR_QC.QC_Status = 0 
              THEN 0 
            ELSE TSPL_MRN_DETAIL.MRN_Qty 
          END
      ELSE (
        CASE 
          WHEN (
            tspl_grn_head.VisualQCStatusSecond = 3 
            OR tspl_grn_head.VisualQCStatus = 3
          ) 
            THEN 0 
          ELSE TSPL_GRN_DETAIL.GRN_Qty 
        END
      )
    END
  )
END AS Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,
                -1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,
                0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,
                0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,
                0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,
                (isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,
                0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo  
                from TSPL_GRN_DETAIL  
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No 
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No
                left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No
                left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left outer join tspl_srn_head on tspl_srn_head.Against_GRN = TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No=tspl_srn_head.SRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code 
                where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 and TSPL_GRN_HEAD.IsCancel=0  " + strGRNcondition + "  " + Environment.NewLine
                ''and not exists (select TSPL_GRN_DETAIL.* from TSPL_QC_CHECK_SRN_DETAIL  left join  TSPL_MRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No=TSPL_MRN_DETAIL.MRN_No  where TSPL_MRN_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_NO and TSPL_QC_CHECK_SRN_DETAIL.Ok_Qty<=0)

                qry += " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,
                '' as SaleInvoiceNo,''as BinNo, TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,
                tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,0  as Qty,  case when isnull(TSPL_SRN_DETAIL.SRN_Qty,0)>0 THEN TSPL_SRN_DETAIL.SRN_Qty ELSE (
                case when isnull(TSPL_MRN_DETAIL.MRN_Qty,0)>0 then TSPL_MRN_DETAIL.MRN_Qty  ELSE (
                CASE when (tspl_grn_head.VisualQCStatusSecond=3 or tspl_grn_head.VisualQCStatus=3) then  (0)
                else (TSPL_GRN_DETAIL.GRN_Qty) end)end ) END as Unapproved,
                TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,
                0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,
                0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,
                0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,
                (isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate,
                '' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo 
                from TSPL_GRN_DETAIL 
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No
                left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left outer join tspl_srn_head on tspl_srn_head.Against_GRN = TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No=tspl_srn_head.SRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code
                where TSPL_GRN_HEAD.Status=0 and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrCode + "') and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 and TSPL_GRN_HEAD.IsCancel=0 " + strGRNcondition + "  " + Environment.NewLine

                '==============add by Monika===if schedule setting is on then po that used in schedule should also not seen in pending PO list
                qry += " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_PO_SCH_DETAIL.PO_code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code where TSPL_PO_SCH_HEAD.is_post=1 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + strSchcondition + "  " + Environment.NewLine &
                " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,''as BinNo, TSPL_PO_SCH_DETAIL.PO_Code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,0  as Qty,TSPL_PO_SCH_DETAIL.schedule_qty as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code  where TSPL_PO_SCH_HEAD.is_post=0 and TSPL_PO_SCH_DETAIL.document_code not in ('" + strCurrCode + "') and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + strSchcondition + "  " + Environment.NewLine &
                " union all  " + Environment.NewLine
                If clsCommon.CompairString(PurchaseOrder_Type, "J") = CompairStringResult.Equal OrElse Is_From_RGP Then
                    qry += " select  '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine &
                " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,''as BinNo, TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=0 and TSPL_RGP_JOB_WORK_DETAIL.rgp_no not in ('" + strCurrCode + "') and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
                Else
                    qry += " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=1 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine &
                " union all  " + Environment.NewLine &
                " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,''as BinNo, TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=0 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and TSPL_RGP_DETAIL.rgp_no not in ('" + strCurrCode + "') and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
                End If

                If OpenPOForShorateLeakageQty Then
                    qry += " union all " + Environment.NewLine +
                    " select '' as ReferencePo,0 as Header_Discount_Per, 0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_SRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,tspl_item_master.tolerence as tolerencePer,
	  0 as ItemTolerenceQty,'' as IType,0 as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0)) as DamageQty,0 as AbatementRate ,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo " + Environment.NewLine +
                    " from TSPL_SRN_DETAIL" + Environment.NewLine +
                    " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No " + Environment.NewLine
                    Dim is_Srn_rejQty_goes_in_Rejstore As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SRN_Rejected_Store from TSPL_PURCHASE_SETTINGS", Nothing)) = 0, False, True)
                    If is_Srn_rejQty_goes_in_Rejstore Then  ''In SPMMD If Qty rejected Then PO will Open by rejected qty. no need to check Return Qty
                        qry += " inner join TSPL_PR_HEAD on TSPL_PR_HEAD.Against_SRN=TSPL_SRN_HEAD.SRN_NO  " + Environment.NewLine
                    End If
                    qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SRN_DETAIL.item_code " + Environment.NewLine +
                    " where " + Environment.NewLine +
                    " TSPL_SRN_HEAD.Status=1 and" + Environment.NewLine +
                    " len(isnull(TSPL_SRN_DETAIL.PO_ID,''))>0 " + Environment.NewLine +
                    " and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No) "

                    If is_Srn_rejQty_goes_in_Rejstore Then
                        ''Added by balwinder on 08/05/2020
                        qry += " union all" + Environment.NewLine +
                        " select  '' as ReferencePo,0 as Header_Discount_Per, 0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_SRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_PR_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_PR_DETAIL.PR_Qty as Qty,0 as Unapproved,TSPL_PR_DETAIL.Unit_code as Unit,'' as Location,1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,isnull(TSPL_PR_DETAIL.MRP,0) as MRP, 0 as DamageQty,0 as AbatementRate ,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo " + Environment.NewLine +
                        "from TSPL_PR_DETAIL" + Environment.NewLine +
                        "left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No  " + Environment.NewLine +
                        "left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No=TSPL_PR_HEAD.Against_SRN and TSPL_SRN_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code and TSPL_SRN_DETAIL.Row_Type='Item'" + Environment.NewLine +
                        "left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PR_DETAIL.item_code " + Environment.NewLine +
                        "where TSPL_PR_HEAD.Status=1 and" + Environment.NewLine +
                        "len(isnull(TSPL_SRN_DETAIL.PO_ID,''))>0 "
                    End If

                End If
                '' add rejected qty for further grn
                'qry += " union all " + Environment.NewLine +
                '       " select '' as ReferencePo,0 as Header_Discount_Per,0 as isBlanket,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,'' as SaleInvoiceNo,'' as Bin_No,TSPL_QC_CHECK_SRN_DETAIL.PO_No as Code,null as Vendor,TSPL_QC_CHECK_SRN_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,0 as Qty,0 as Unapproved,TSPL_QC_CHECK_SRN_DETAIL.Unit_code as Unit,'' as Location,1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty as DamageQty,0 as AbatementRate,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt, null as RefTendorNo  from TSPL_QC_CHECK_SRN_DETAIL  " &
                '       " left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_SRN_DETAIL.document_code " &
                '       " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_CHECK_SRN_DETAIL.item_code where TSPL_QC_CHECK_HEAD.Posted=1 " &
                '       " and len(isnull(TSPL_QC_CHECK_SRN_DETAIL.PO_No,''))>0  and TSPL_QC_CHECK_SRN_DETAIL.Ok_Qty<=0 and not exists (select * from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL.MRN_No=TSPL_QC_CHECK_SRN_DETAIL.MRN_No )"
                qry += " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' "

                qry += " group by Code,ICode,Unit,MRP having SUM(Chk)>0 and  (SUM((Qty *RI)-Unapproved" + IIf(OpenPOForShorateLeakageQty, "+", "-") + "DamageQty) <>0 or sum(isBlanket)>0) order by Code,ICode "

            End If
        End If

        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for vendor " + VendorName + "")
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
    End Sub

    Public Shared Function Load_discount_for_GRN(ByVal po_No As String, ByVal item_code As String)
        Dim disc_per As Double = 0
        Try
            'Dim sQuery As String = "select disc_per from TSPL_PURCHASE_ORDER_detail where purchaseorder_No='" & po_No & "' and item_code='" & item_code & "'"
            Dim sQuery As String = "select disc_per from TSPL_MRN_DETAIL where MRN_No='" & po_No & "' and item_code='" & item_code & "'"
            disc_per = clsDBFuncationality.getSingleValue(sQuery)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
        Return disc_per
    End Function

    Public Shared Function Load_discount_per_unit_for_GRN(ByVal po_No As String, ByVal item_code As String)
        Dim Disc_Per_Unit As Double = 0
        Try
            'Dim sQuery As String = "select Disc_Per_Unit from TSPL_PURCHASE_ORDER_detail where purchaseorder_No='" & po_No & "' and item_code='" & item_code & "'"
            Dim sQuery As String = "select Disc_Per_Unit from TSPL_MRN_DETAIL where MRN_No='" & po_No & "' and item_code='" & item_code & "'"
            ' Disc_Per_Unit = clsDBFuncationality.getSingleValue(sQuery)
            Disc_Per_Unit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sQuery))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
        Return Disc_Per_Unit
    End Function

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = ""
            If Is_Load_MRN Then
                strCode = clsCommon.myCstr(dr("MRN_No"))
            Else
                strCode = clsCommon.myCstr(dr("Code"))
            End If
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = clsCommon.myCstr(dr("Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHPotype).Value = clsPurchaseOrderHead.GetPurchaseTypeName(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PurchaseOrder_Type from TSPL_PURCHASE_ORDER_HEAD where purchaseorder_no='" + clsCommon.myCstr(dr("code")) + "'")))

                gvHead.Rows(gvHead.RowCount - 1).Cells(colHBill_To_Location).Value = clsCommon.myCstr(dr("Location"))
                If Is_Load_MRN Then
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHMRNCode).Value = clsCommon.myCstr(dr("MRN_No"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHGRNCode).Value = clsCommon.myCstr(dr("GRN_No"))

                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHMRN_Date).Value = clsCommon.myCstr(dr("MRN_Date"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHGRN_DATE).Value = clsCommon.myCstr(dr("GRN_Date"))
                End If
                If blnShowInvoiceNo Then
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHSaleInvoiceNo).Value = clsCommon.myCstr(dr("SaleInvoiceNo"))
                End If
                'If Is_Load_MRN = False Then
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHReferencePo).Value = clsCommon.myCstr(dr("ReferencePo"))
                'End If
                If dtAllData.Columns.Contains("RefTendorNo") Then
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHTenderNo).Value = clsCommon.myCstr(dr("RefTendorNo"))
                End If
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


        If Is_Load_MRN Then
            Dim repoMRNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoMRNCode.FormatString = ""
            repoMRNCode.HeaderText = "MRN No"
            repoMRNCode.Name = colHMRNCode
            repoMRNCode.Width = 170
            repoMRNCode.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoMRNCode)

            Dim repoMRNDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoMRNDate.FormatString = ""
            repoMRNDate.HeaderText = "MRN Date"
            repoMRNDate.Name = colHMRN_Date
            repoMRNDate.Width = 70
            repoMRNDate.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoMRNDate)


            Dim repoGRNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoGRNCode.FormatString = ""
            repoGRNCode.HeaderText = "GRN No"
            repoGRNCode.Name = colHGRNCode
            repoGRNCode.Width = 170
            repoGRNCode.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoGRNCode)

            Dim repoGRNDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoGRNDate.FormatString = ""
            repoGRNDate.HeaderText = "GRN Date"
            repoGRNDate.Name = colHGRN_DATE
            repoGRNDate.Width = 70
            repoGRNDate.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoGRNDate)


        End If
        If blnShowInvoiceNo Then
            Dim repoSaleInvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSaleInvoiceNo.FormatString = ""
            repoSaleInvoiceNo.HeaderText = "Sale Invoice No"
            repoSaleInvoiceNo.Name = colHSaleInvoiceNo
            repoSaleInvoiceNo.Width = 170
            repoSaleInvoiceNo.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoSaleInvoiceNo)
        End If

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "PO No"
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


        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Vendor"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        repoVendorName = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "PO Type"
        repoVendorName.Name = colHPotype
        repoVendorName.Width = 100
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        Dim repoBill_To_Location As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBill_To_Location.FormatString = ""
        repoBill_To_Location.HeaderText = "Bill To Location"
        repoBill_To_Location.Name = colHBill_To_Location
        repoBill_To_Location.Width = 170
        repoBill_To_Location.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoBill_To_Location)

        'If Is_Load_MRN = False Then
        Dim repoReferencePo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReferencePo.FormatString = ""
        repoReferencePo.HeaderText = "Reference PO"
        repoReferencePo.Name = colHReferencePo
        repoReferencePo.Width = 170
        repoReferencePo.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoReferencePo)
        'End If

        repoReferencePo = New GridViewTextBoxColumn()
        repoReferencePo.FormatString = ""
        repoReferencePo.HeaderText = "Tender No"
        repoReferencePo.Name = colHTenderNo
        repoReferencePo.Width = 170
        repoReferencePo.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoReferencePo)

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


        If Is_Load_MRN Then
            Dim repoMRNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoMRNCode.FormatString = ""
            repoMRNCode.HeaderText = "MRN No"
            repoMRNCode.Name = colHMRNCode
            repoMRNCode.Width = 170
            repoMRNCode.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoMRNCode)


            Dim repoGRNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoGRNCode.FormatString = ""
            repoGRNCode.HeaderText = "GRN No"
            repoGRNCode.Name = colHGRNCode
            repoGRNCode.Width = 170
            repoGRNCode.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoGRNCode)



        End If

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "PO No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)





        Dim repoIType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIType.FormatString = ""
        repoIType.HeaderText = "Row Type"
        repoIType.Name = colDIType
        repoIType.Width = 180
        repoIType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIType)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoUnit.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        repoMRP.WrapText = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.WrapText = True
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)

        Dim repoItemTol As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTol.FormatString = ""
        repoItemTol.HeaderText = IIf(Is_Load_MRN, "tolerencePer", "Tolerence %")
        repoItemTol.Name = colItemTol
        repoItemTol.Width = 70
        repoItemTol.WrapText = True
        repoItemTol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTol)

        Dim repoItemTolAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTolAmt.FormatString = ""
        repoItemTolAmt.HeaderText = IIf(Is_Load_MRN, "ItemTolerenceQty", "Tolerence Amt")
        repoItemTolAmt.Name = colItemTolAmt
        repoItemTolAmt.Width = 70
        repoItemTolAmt.WrapText = True
        repoItemTolAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTolAmt)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = IIf(Is_Load_MRN, "Qty", "PO Allowed Qty")
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = IIf(strFormType Is Nothing, "Used in SRN", "Used in SO")
        repoAppQty.Name = colDApprovedQty
        repoAppQty.ReadOnly = True
        repoAppQty.Width = 100
        repoAppQty.WrapText = True
        repoAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAppQty)

        Dim repoUnAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "Unapproved Qty"
        repoUnAppQty.Name = colDUnApprovedQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)

        Dim repoDamageQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDamageQty = New GridViewDecimalColumn()
        repoDamageQty.FormatString = ""
        repoDamageQty.HeaderText = "Damage Quantity"
        repoDamageQty.Name = colDDamageQty
        repoDamageQty.ReadOnly = True
        repoDamageQty.Width = 80
        repoDamageQty.WrapText = True
        repoDamageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDamageQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)


        Dim repoTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxCode.FormatString = ""
        repoTaxCode.HeaderText = "Tax Group Code"
        repoTaxCode.Name = colDTaxGroup
        repoTaxCode.Width = 100
        repoTaxCode.ReadOnly = True
        repoTaxCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxCode)

        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colDBinNo
        repoBinNo.Width = 100
        repoBinNo.ReadOnly = True
        repoBinNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        repoBinNo = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Against Item Wise Tax Code"
        repoBinNo.Name = colDAgainstItemWiseTaxCode
        repoBinNo.ReadOnly = True
        repoBinNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Taxable Amount Per"
        repoPendingQty.Name = colDTaxableAmountPer
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Is Blanket"
        repoPendingQty.Name = colDIsBlanket
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoTaxName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxName.FormatString = ""
        repoTaxName.HeaderText = "Tax Group"
        repoTaxName.Name = colDTaxGroupName
        repoTaxName.Width = 100
        repoTaxName.ReadOnly = True
        repoTaxName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxName)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colDTaxRate1
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.IsVisible = False
        repoTaxRate1.WrapText = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colDTaxRate2
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.IsVisible = False
        repoTaxRate2.WrapText = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colDTaxRate3
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.IsVisible = False
        repoTaxRate3.WrapText = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colDTaxRate4
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.IsVisible = False
        repoTaxRate4.WrapText = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colDTaxRate5
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.IsVisible = False
        repoTaxRate5.WrapText = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colDTaxRate6
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.IsVisible = False
        repoTaxRate6.WrapText = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colDTaxRate7
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.IsVisible = False
        repoTaxRate7.WrapText = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colDTaxRate8
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.IsVisible = False
        repoTaxRate8.WrapText = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colDTaxRate9
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.IsVisible = False
        repoTaxRate9.WrapText = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colDTaxRate10
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.IsVisible = False
        repoTaxRate10.WrapText = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colDTaxAmt1
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.WrapText = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colDTaxAmt2
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.WrapText = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colDTaxAmt3
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.WrapText = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colDTaxAmt4
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.WrapText = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colDTaxAmt5
        repoTaxAmt5.ReadOnly = True
        repoTaxAmt5.IsVisible = False
        repoTaxAmt5.WrapText = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colDTaxAmt6
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.WrapText = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colDTaxAmt7
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.WrapText = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colDTaxAmt8
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.WrapText = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colDTaxAmt9
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.WrapText = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colDTaxAmt10
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.WrapText = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim AbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        AbatementRate = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Abatement Rate"
        AbatementRate.Name = colAbatementRate
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

        Dim repoCategoryType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCategoryType)

        Dim repoEmergency As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoEmergency.Checked = ToggleState.Off
        repoEmergency.HeaderText = "Emergency"
        repoEmergency.Name = colEmergency
        repoEmergency.Width = 50
        repoEmergency.IsVisible = ShowCapexCodeandSubCode
        repoEmergency.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoEmergency)

        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        repoCapexSubCode.HeaderImage = My.Resources.search4
        repoCapexSubCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        repoCapexCode.HeaderImage = My.Resources.search4
        repoCapexCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexCode)

        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Header Discount %"
        repoTaxAmt9.Name = colDHeaderDisPer
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.WrapText = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        repoCapexSubCode = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Item Insurance Apply On"
        repoCapexSubCode.Name = colItemInsuranceApplyOn
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = False
        repoCapexSubCode.ReadOnly = True
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        AbatementRate = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Item Insurance %"
        AbatementRate.Name = colItemInsurancePer
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

        AbatementRate = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Item Insurance Amt"
        AbatementRate.Name = colItemInsuranceAmt
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

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
        Me.Close()
    End Sub

    Sub btnOKPressed()
        btnOk.Focus()
        If Not isAllowed() Then
            Exit Sub
        End If

        If strFormType IsNot Nothing AndAlso clsCommon.myLen(strFormType) > 0 AndAlso clsCommon.CompairString(strFormType, "EXPORT SO") = CompairStringResult.Equal Then

            ArrReturn_EX = New List(Of clsEXSalesOrderDetail)
            Dim obj As clsEXSalesOrderDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsEXSalesOrderDetail()
                    obj.PurchaseOrder_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                    obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                    ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.Bin_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDBinNo).Value)
                    obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                    obj.Assessable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAssessable).Value)

                    If (obj.Balance_Qty > 0) Then
                        ArrReturn_EX.Add(obj)
                    End If
                End If
            Next

            If ArrReturn_EX.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending PO item", Me.Text)
            Else
                Me.Close()
            End If

        Else

            ArrReturn = New List(Of clsPurchaseOrderDetail)
            Dim obj As clsPurchaseOrderDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsPurchaseOrderDetail()
                    obj.PurchaseOrder_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                    obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    'obj.MRN_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDmrn).Value)
                    ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                    ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.PurchaseOrder_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                    obj.POTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
                    obj.Bin_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDBinNo).Value)
                    obj.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAgainstItemWiseTaxCode).Value)
                    obj.Taxable_Amount_Per = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxableAmountPer).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate1).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate2).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate3).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate4).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate5).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate6).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate7).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate8).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate9).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate10).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt1).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt2).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt3).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt4).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt5).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt6).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt7).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt8).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt9).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt10).Value)
                    obj.Requisition_Id = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 isnull(Requisition_Id,'')  from tspl_purchase_order_detail where purchaseOrder_No='" & obj.PurchaseOrder_No & "'  and Item_code='" & obj.Item_Code & "'"))
                    obj.Disc_Per = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select disc_per  from tspl_purchase_order_detail where purchaseOrder_No='" & obj.PurchaseOrder_No & "' and Item_code='" & obj.Item_Code & "' "))
                    obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                    obj.Assessable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAssessable).Value)
                    obj.AbatementRate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAbatementRate).Value)
                    obj.Header_Discount_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDHeaderDisPer).Value)
                    '' done by rohit
                    If Is_Load_MRN Then
                        obj.MRN_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colHMRNCode).Value)
                        obj.GRN_No_Temp = clsCommon.myCstr(gv1.Rows(ii).Cells(colHGRNCode).Value)
                        If ShowCapexCodeandSubCode Then
                            obj.Category = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                            obj.Emergency = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
                            obj.Capex_SubCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)
                            obj.Capex_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)
                        End If
                    End If
                    ''---------------------
                    'obj.AssessableMRP = gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value
                    'obj.TotalAssessableMRP = gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value

                    obj.Item_Insurance_Apply_On = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemInsuranceApplyOn).Value)
                    obj.Item_Insurance_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsurancePer).Value)
                    obj.Item_Insurance_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsuranceAmt).Value)

                    If (obj.Balance_Qty > 0 OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colDIsBlanket).Value) > 0) Then
                        ArrReturn.Add(obj)
                    End If
                End If
            Next

            If ArrReturn.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending PO item", Me.Text)
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrPOType As New List(Of String)
        Dim arrLoc As New List(Of String)

        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                Dim potype As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHPotype).Value)
                Dim Loc As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHBill_To_Location).Value)
                VendorCode = strCode
                VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)
                Bill_To_Location = Loc

                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value))
                        End If
                        If clsCommon.CompairString(potype, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPotype).Value)) <> CompairStringResult.Equal Then
                            arrPOType.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPotype).Value))
                        End If

                        If clsCommon.CompairString(Loc, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHBill_To_Location).Value)) <> CompairStringResult.Equal Then
                            arrLoc.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHBill_To_Location).Value))
                        End If
                    End If ''boolean check

                Next ''jj

                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one vendor are not allowed.", Me.Text)
                    Return False
                End If
                If arrPOType.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one type are not allowed.", Me.Text)
                    Return False
                End If
                If arrLoc.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one location are not allowed.", Me.Text)
                    Return False
                End If
                Return True 'only one time loop check
            End If
        Next ''ii
        Return True
    End Function

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentCell Is gv1.Columns(colDCode) Then
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
        Try
            If Not IsInsideLoadData Then
                Dim strCode As String
                If Is_Load_MRN Then
                    strCode = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHMRNCode).Value)
                Else
                    strCode = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                End If

                If clsCommon.myLen(strCode) > 0 Then
                    LoadDetailData(e.NewValue, strCode)
                End If
            End If

            'If Not IsInsideLoadData Then
            '    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
            '        Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
            '        Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
            '        If clsCommon.myLen(VendorCode) <= 0 Then
            '            VendorCode = strVendorCode
            '            VendorName = strVendorName
            '        End If

            '        If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
            '            Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
            '            If clsCommon.myLen(strCode) > 0 Then
            '                LoadDetailData(e.NewValue, strCode)
            '            End If
            '        Else
            '            common.clsCommon.MyMessageBoxShow("PO's Vendor should be `" + VendorName)
            '            e.Cancel = True
            '        End If
            '    End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If Is_Load_MRN Then
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("MRN_No"))) = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTol).Value = clsCommon.myCdbl(dr("tolerencePer"))
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTolAmt).Value = clsCommon.myCdbl(dr("ItemTolerenceQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDBinNo).Value = clsCommon.myCstr(dr("Bin_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("TaxGroupName"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate1).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate2).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate3).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate4).Value = clsCommon.myCdbl(dr("TAX4_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate5).Value = clsCommon.myCdbl(dr("TAX5_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate6).Value = clsCommon.myCdbl(dr("TAX6_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate7).Value = clsCommon.myCdbl(dr("TAX7_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate8).Value = clsCommon.myCdbl(dr("TAX8_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate9).Value = clsCommon.myCdbl(dr("TAX9_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate10).Value = clsCommon.myCdbl(dr("TAX10_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDDamageQty).Value = clsCommon.myCdbl(dr("DamageQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt1).Value = clsCommon.myCdbl(dr("TAX1_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt2).Value = clsCommon.myCdbl(dr("TAX2_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt3).Value = clsCommon.myCdbl(dr("TAX3_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt4).Value = clsCommon.myCdbl(dr("TAX4_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt5).Value = clsCommon.myCdbl(dr("TAX5_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt6).Value = clsCommon.myCdbl(dr("TAX6_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt7).Value = clsCommon.myCdbl(dr("TAX7_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt8).Value = clsCommon.myCdbl(dr("TAX8_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt9).Value = clsCommon.myCdbl(dr("TAX9_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt10).Value = clsCommon.myCdbl(dr("TAX10_Amt"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = clsCommon.myCdbl(dr("AbatementRate"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHMRNCode).Value = clsCommon.myCstr(dr("MRN_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHGRNCode).Value = clsCommon.myCstr(dr("GRN_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = clsCommon.myCstr(dr("category"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = clsCommon.myCdbl(dr("emergency"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = clsCommon.myCstr(dr("capex_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = clsCommon.myCstr(dr("capex_subcode"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDAgainstItemWiseTaxCode).Value = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxableAmountPer).Value = clsCommon.myCstr(dr("Taxable_Amount_Per"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDHeaderDisPer).Value = clsCommon.myCdbl(dr("Header_Discount_Per"))
                        If IsItemInsuranceColumn Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                        End If
                    End If
                Else
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTol).Value = clsCommon.myCdbl(dr("tolerencePer"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTolAmt).Value = clsCommon.myCdbl(dr("ItemTolerenceQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDBinNo).Value = clsCommon.myCstr(dr("Bin_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("TaxGroupName"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate1).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate2).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate3).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate4).Value = clsCommon.myCdbl(dr("TAX4_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate5).Value = clsCommon.myCdbl(dr("TAX5_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate6).Value = clsCommon.myCdbl(dr("TAX6_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate7).Value = clsCommon.myCdbl(dr("TAX7_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate8).Value = clsCommon.myCdbl(dr("TAX8_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate9).Value = clsCommon.myCdbl(dr("TAX9_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate10).Value = clsCommon.myCdbl(dr("TAX10_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDDamageQty).Value = clsCommon.myCdbl(dr("DamageQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt1).Value = clsCommon.myCdbl(dr("TAX1_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt2).Value = clsCommon.myCdbl(dr("TAX2_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt3).Value = clsCommon.myCdbl(dr("TAX3_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt4).Value = clsCommon.myCdbl(dr("TAX4_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt5).Value = clsCommon.myCdbl(dr("TAX5_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt6).Value = clsCommon.myCdbl(dr("TAX6_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt7).Value = clsCommon.myCdbl(dr("TAX7_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt8).Value = clsCommon.myCdbl(dr("TAX8_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt9).Value = clsCommon.myCdbl(dr("TAX9_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt10).Value = clsCommon.myCdbl(dr("TAX10_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDIsBlanket).Value = clsCommon.myCdbl(dr("isBlanket"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDHeaderDisPer).Value = clsCommon.myCdbl(dr("Header_Discount_Per"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = clsCommon.myCdbl(dr("AbatementRate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDAgainstItemWiseTaxCode).Value = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxableAmountPer).Value = clsCommon.myCstr(dr("Taxable_Amount_Per"))
                        If IsItemInsuranceColumn Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                        End If
                    End If
                End If

            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If Is_Load_MRN Then
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colHMRNCode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(ii)
                    End If
                Else
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(ii)
                    End If
                End If

            Next
        End If

    End Sub

End Class

