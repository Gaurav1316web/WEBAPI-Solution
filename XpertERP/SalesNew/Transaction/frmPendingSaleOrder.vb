'-changes By--[Pankaj Kumar Chaudhary]--Against Ticket No -[BM00000002083]
Imports common
Public Class frmPendingSaleOrder
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strDocType As String = Nothing
    Public strExport_Merchant As String = Nothing
    Public strDocumentAcceptanceNo As String = Nothing
    Public ArrReturn As List(Of clsSNSalesOrderDetail) = Nothing
    Public ArrReturn_EX As List(Of clsEXSalesOrderDetail) = Nothing
    Public ArrReturn_EX_PI As List(Of clsEXPorformaInvoiceDetail) = Nothing
    Public ArrReturn_EX_Comm As List(Of clsEXCommercialInvoiceDetail) = Nothing
    Public arrReturn_MT_Purc As List(Of clsPurchaseOrderDetail) = Nothing
    Dim dtAllData As DataTable = Nothing

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDIType As String = "IType"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
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
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDDisPer As String = "DISCOUNTPER"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setGridPropery()
        'Dim qry As String = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
        '" SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
        '" SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
        '" SUM(Unapproved) as UnapprovedQty," & _
        '" SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," & _
        '" MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,0 as Assessable,max(Leak_Qty) as Leak_Qty,max(Burst_Qty) as Burst_Qty,max(Short_Qty ) as Short_Qty  from ( " & _
        '" select TSPL_SD_SALES_ORDER_DETAIL.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Vendor,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_DETAIL.Item_Desc as IName,TSPL_SD_SALES_ORDER_DETAIL.Row_Type as IType,(TSPL_SD_SALES_ORDER_DETAIL.MRN_Qty +isnull(TSPL_SD_SALES_ORDER_DETAIL.Leak_Qty,0)+isnull(TSPL_SD_SALES_ORDER_DETAIL.Burst_Qty,0)+isnull(TSPL_SD_SALES_ORDER_DETAIL.Short_Qty,0)) as Qty,0 as Unapproved,TSPL_SD_SALES_ORDER_DETAIL.Unit_Code as Unit,TSPL_SD_SALES_ORDER_DETAIL.Location as Location,1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SD_SALES_ORDER_HEAD.Tax_Group,TSPL_SD_SALES_ORDER_DETAIL.TAX1_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX2_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX3_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX4_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX5_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX6_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX7_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX8_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX9_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX10_Rate,TSPL_SD_SALES_ORDER_DETAIL.MRP,TSPL_SD_SALES_ORDER_DETAIL.Batch_No,TSPL_SD_SALES_ORDER_DETAIL.MFG_Date,TSPL_SD_SALES_ORDER_DETAIL.Expiry_Date,TSPL_SD_SALES_ORDER_DETAIL.Disc_Per,MRN_Date as TransDate,TSPL_SD_SALES_ORDER_DETAIL.Assessable,TSPL_SD_SALES_ORDER_DETAIL.Leak_Qty,TSPL_SD_SALES_ORDER_DETAIL.Burst_Qty,TSPL_SD_SALES_ORDER_DETAIL.Short_Qty  from TSPL_SD_SALES_ORDER_DETAIL left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code where TSPL_SD_SALES_ORDER_DETAIL.Status=0 and TSPL_SD_SALES_ORDER_HEAD.Status=1 "
        'If clsCommon.myLen(VendorCode) > 0 Then
        '    qry += " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code='" + VendorCode + "'"
        'End If
        'qry += " union all" & _
        '" select TSPL_SD_SHIPMENT_DETAIL.Order_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_DETAIL.Item_Desc as IName,'' as IType,(isnull(TSPL_SD_SHIPMENT_DETAIL.SRN_Qty,0) +isnull(TSPL_SD_SHIPMENT_DETAIL.Leak_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Burst_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Short_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Rejected_Qty,0) ) as Qty,0 as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,isnull(TSPL_SD_SHIPMENT_DETAIL.MRP,0) as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)as Assessable,0 as Leak_Qty,0 as Burst_Qty,0 as Short_Qty from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.SRN_No=TSPL_SD_SHIPMENT_DETAIL.SRN_No where TSPL_SD_SHIPMENT_Head.Status=1 and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Order_Code,''))>0  " & _
        '" union all  " & _
        '" select TSPL_SD_SHIPMENT_DETAIL.Order_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_DETAIL.Item_Desc as IName,'' as IType,0  as Qty,(isnull(TSPL_SD_SHIPMENT_DETAIL.SRN_Qty,0) +isnull(TSPL_SD_SHIPMENT_DETAIL.Leak_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Burst_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Short_Qty,0)+isnull(TSPL_SD_SHIPMENT_DETAIL.Rejected_Qty,0)) as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,isnull(TSPL_SD_SHIPMENT_DETAIL.MRP,0) as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0) as Assessable,0 as Leak_Qty,0 as Burst_Qty,0 as Short_Qty from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.SRN_No=TSPL_SD_SHIPMENT_DETAIL.SRN_No  where TSPL_SD_SHIPMENT_Head.Status=0 and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Order_Code,''))>0 and TSPL_SD_SHIPMENT_DETAIL.SRN_No not in ('" + strCurrCode + "')   " & _
        '" )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Customer_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM(Qty *RI) <>0 order by Code,ICode "

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim StrCondition As String = ""
        Dim StrCondition1 As String = ""
        strwherecls = Xtra.CustomerPermission()
        If clsCommon.myLen(strwherecls) > 0 Then
            StrCondition = "   AND Final.Vendor IN (" + strwherecls + ")"
            StrCondition1 = "   WHERE Final.Vendor IN (" + strwherecls + ")"
        Else
            StrCondition = ""
            StrCondition1 = ""
        End If
        '-------------------------------------
        Dim qry As String = ""
        If strDocType IsNot Nothing AndAlso clsCommon.CompairString(strDocType, "Export PI") = CompairStringResult.Equal Then
            qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " + Environment.NewLine
            qry += " select TSPL_SD_SALES_ORDER_DETAIL.Line_No,TSPL_SD_SALES_ORDER_DETAIL.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Vendor,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_SD_SALES_ORDER_DETAIL.Row_Type as IType, TSPL_SD_SALES_ORDER_DETAIL.Qty  as Qty,0 as Unapproved,TSPL_SD_SALES_ORDER_DETAIL.Unit_Code as Unit,TSPL_SD_SALES_ORDER_DETAIL.Location as Location,1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SD_SALES_ORDER_HEAD.Tax_Group,TSPL_SD_SALES_ORDER_DETAIL.TAX1_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX2_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX3_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX4_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX5_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX6_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX7_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX8_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX9_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX10_Rate,0 as MRP ,TSPL_SD_SALES_ORDER_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_SD_SALES_ORDER_DETAIL.Assessable " + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SALES_ORDER_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code  " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code" + Environment.NewLine
            qry += " where Scheme_Item='N'  and TSPL_SD_SALES_ORDER_HEAD.Status=1 and TSPL_SD_SALES_ORDER_HEAD.trans_type='EXP' and TSPL_SD_SALES_ORDER_HEAD.close_yn='N'  " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code='" + VendorCode + "'"
            End If
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD.salesorder_type='" + strExport_Merchant + "' "
            End If
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_EX_PI_DETAIL.Line_No,TSPL_EX_PI_DETAIL.Sales_Order_Code as Code,TSPL_EX_PI_HEAD.Customer_Code as Vendor,TSPL_EX_PI_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_PI_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_PI_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_PI_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_EX_PI_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_EX_PI_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_PI_DETAIL.Sales_Order_Code,''))>0   " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_PI_HEAD.document_type='" + strExport_Merchant + "' "
            End If
            qry += " union all   " + Environment.NewLine
            qry += " select TSPL_EX_PI_DETAIL.Line_No,TSPL_EX_PI_DETAIL.Sales_Order_Code as Code,TSPL_EX_PI_HEAD.Customer_Code as Vendor,TSPL_EX_PI_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_EX_PI_DETAIL.Qty,0) as Unapproved,TSPL_EX_PI_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_PI_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_EX_PI_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_EX_PI_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_PI_DETAIL.Sales_Order_Code,''))>0 and TSPL_EX_PI_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_PI_HEAD.document_type='" + strExport_Merchant +"' "
            End If
            qry += " )Final " + Environment.NewLine
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
            qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " + Environment.NewLine
            If IsAllowSingleSI4SingleSO = True Then
                qry += " WHERE Code Not in (Select Distinct Sales_Order_Code from TSPL_EX_PI_DETAIL WHERE ISNULL(Sales_Order_Code,'')<>'') " + StrCondition + " "
            Else
                qry += "" + StrCondition1 + "  "
            End If
            qry += " group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"

            '=================export proforma----------------------------------------
        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Export COMMERCIAL") = CompairStringResult.Equal Then
            qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " + Environment.NewLine
            qry += " select TSPL_EX_PI_DETAIL.Line_No,TSPL_EX_PI_DETAIL.Document_Code as Code,TSPL_EX_PI_HEAD.Customer_Code as Vendor,TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Row_Type as IType, TSPL_EX_PI_DETAIL.Qty  as Qty,0 as Unapproved,TSPL_EX_PI_DETAIL.Unit_Code as Unit,TSPL_EX_PI_DETAIL.Location as Location,1 as RI,TSPL_EX_PI_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_EX_PI_HEAD.Tax_Group,TSPL_EX_PI_DETAIL.TAX1_Rate,TSPL_EX_PI_DETAIL.TAX2_Rate,TSPL_EX_PI_DETAIL.TAX3_Rate,TSPL_EX_PI_DETAIL.TAX4_Rate,TSPL_EX_PI_DETAIL.TAX5_Rate,TSPL_EX_PI_DETAIL.TAX6_Rate,TSPL_EX_PI_DETAIL.TAX7_Rate,TSPL_EX_PI_DETAIL.TAX8_Rate,TSPL_EX_PI_DETAIL.TAX9_Rate,TSPL_EX_PI_DETAIL.TAX10_Rate,TSPL_EX_PI_DETAIL.MRP ,TSPL_EX_PI_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_EX_PI_DETAIL.Assessable " + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_EX_PI_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code  " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_PI_DETAIL.Item_Code" + Environment.NewLine
            qry += " where Scheme_Item='N'  and TSPL_EX_PI_HEAD.Status=1 and isnull(TSPL_EX_PI_HEAD.stuffing_status,'0')='1' " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_EX_PI_HEAD.Customer_Code='" + VendorCode + "'"
            End If
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_PI_HEAD.document_type='" + strExport_Merchant + "' AND isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,'') NOT IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')"
            End If
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0   " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + strExport_Merchant + "' "
            End If
            qry += " union all   " + Environment.NewLine
            qry += " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0 and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + strExport_Merchant + "' "
            End If
            qry += " )Final " + Environment.NewLine
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
            qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " + Environment.NewLine
            If IsAllowSingleSI4SingleSO = True Then
                qry += " WHERE Code Not in (Select Distinct PI_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL WHERE ISNULL(PI_Code,'')<>'') " + StrCondition + " "
            Else
                qry += "" + StrCondition1 + "  "
            End If
            qry += " group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"


        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Export Sale Invoice") = CompairStringResult.Equal Then
            qry = "select CAST(0 as bit) as Sel,code,'' as Tax_Group,'' as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,Final.MRP as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " + Environment.NewLine
            qry += " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Row_Type as IType, TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty  as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_Code as Unit,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Location as Location,1 as  RI,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MRP ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Disc_Per,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date as TransDate,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable  " + Environment.NewLine
            qry += "  ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code  " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code " + Environment.NewLine
            qry += " where Scheme_Item='N'  and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code='" + VendorCode + "'"
            End If
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='" + strExport_Merchant + "' "
            End If

            qry += " union all " + Environment.NewLine

            qry += " select TSPL_SD_SALE_INVOICE_DETAIL.Line_No,TSPL_SD_SALE_INVOICE_DETAIL.Commercial_Inv_No as Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Vendor,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SALE_INVOICE_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and TSPL_SD_SALE_INVOICE_HEAD.trans_type='EXP' and Scheme_Item='N'  and len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Commercial_Inv_No,''))>0   " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_type='" + strExport_Merchant + "' "
            End If
            qry += " union all   " + Environment.NewLine
            qry += " select TSPL_SD_SALE_INVOICE_DETAIL.Line_No,TSPL_SD_SALE_INVOICE_DETAIL.Commercial_Inv_No as Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Vendor,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SALE_INVOICE_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status=0 and TSPL_SD_SALE_INVOICE_HEAD.trans_type='EXP' and Scheme_Item='N'  and len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Commercial_Inv_No,''))>0 and TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine
            If clsCommon.myLen(strExport_Merchant) > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_type='" + strExport_Merchant + "' "
            End If
            qry += " )Final " + Environment.NewLine
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
            If IsAllowSingleSI4SingleSO = True Then
                qry += " WHERE Code Not in (Select Distinct Commercial_Inv_No from TSPL_SD_SALE_INVOICE_DETAIL WHERE ISNULL(Commercial_Inv_No,'')<>'') " + StrCondition + " "
            Else
                qry += "" + StrCondition1 + "  "
            End If
            qry += " group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"

            ''richa agarwal 25/04/2015
        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Merchant PI Without LC") = CompairStringResult.Equal Then

            qry = " select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Max(Unit) as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Max(Final.MRP) as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " & _
            " select TSPL_EX_PI_DETAIL.Line_No,TSPL_EX_PI_DETAIL.Document_Code as Code,TSPL_EX_PI_HEAD.Customer_Code as Vendor,TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Row_Type as IType, TSPL_EX_PI_DETAIL.Qty  as Qty,0 as Unapproved,TSPL_EX_PI_DETAIL.Unit_Code as Unit,TSPL_EX_PI_DETAIL.Location as Location,1 as RI,TSPL_EX_PI_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_EX_PI_HEAD.Tax_Group,TSPL_EX_PI_DETAIL.TAX1_Rate,TSPL_EX_PI_DETAIL.TAX2_Rate,TSPL_EX_PI_DETAIL.TAX3_Rate,TSPL_EX_PI_DETAIL.TAX4_Rate,TSPL_EX_PI_DETAIL.TAX5_Rate,TSPL_EX_PI_DETAIL.TAX6_Rate,TSPL_EX_PI_DETAIL.TAX7_Rate,TSPL_EX_PI_DETAIL.TAX8_Rate,TSPL_EX_PI_DETAIL.TAX9_Rate,TSPL_EX_PI_DETAIL.TAX10_Rate,TSPL_EX_PI_DETAIL.MRP ,TSPL_EX_PI_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_EX_PI_DETAIL.Assessable " & _
            " ,Scheme_Code from TSPL_EX_PI_DETAIL " & _
            " left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.Document_Code  " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_PI_DETAIL.Item_Code" & _
            " where Scheme_Item='N'  and TSPL_EX_PI_HEAD.Status=1 " & _
            "  and (isnull(TSPL_EX_PI_HEAD.MT_Against_PO,'')<>'' or isnull(TSPL_EX_PI_HEAD.MT_Against_PO,'')='')" & _
            "  and TSPL_EX_PI_HEAD.document_type='MT' AND isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,'') NOT IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L') " & _
            "           union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable" & _
            " ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL " & _
            " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code" & _
            " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0   " & _
            " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT' " & _
            "          union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable" & _
            " ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL " & _
            " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code" & _
            " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0 and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " & _
            " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT' )Final " & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " & _
            " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " & _
            "    group by Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0  order by Code,Icode"

        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Merchant PI With LC") = CompairStringResult.Equal Then

            qry = " select CAST(0 as bit) as Sel,code ,[PI Code] ,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Max(Unit) as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Max(Final.MRP) as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from (  " & _
            " select TSPL_EX_PI_DETAIL.Line_No,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo as Code,TSPL_EX_PI_HEAD.Document_Code as [PI Code],TSPL_EX_PI_HEAD.Customer_Code as Vendor,TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Row_Type as IType, TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty  as Qty,0 as Unapproved,TSPL_EX_PI_DETAIL.Unit_Code as Unit,TSPL_EX_PI_DETAIL.Location as Location,1 as RI,TSPL_EX_PI_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_EX_PI_HEAD.Tax_Group,TSPL_EX_PI_DETAIL.TAX1_Rate,TSPL_EX_PI_DETAIL.TAX2_Rate,TSPL_EX_PI_DETAIL.TAX3_Rate,TSPL_EX_PI_DETAIL.TAX4_Rate,TSPL_EX_PI_DETAIL.TAX5_Rate,TSPL_EX_PI_DETAIL.TAX6_Rate,TSPL_EX_PI_DETAIL.TAX7_Rate,TSPL_EX_PI_DETAIL.TAX8_Rate,TSPL_EX_PI_DETAIL.TAX9_Rate,TSPL_EX_PI_DETAIL.TAX10_Rate,TSPL_EX_PI_DETAIL.MRP ,TSPL_EX_PI_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_EX_PI_DETAIL.Assessable  ,Scheme_Code" & _
            " from TSPL_EX_PI_DETAIL  left outer join TSPL_EX_PI_HEAD on TSPL_EX_PI_HEAD.Document_Code=TSPL_EX_PI_DETAIL.DOCUMENT_CODE " & _
            " lEFT oUTER Join TSPL_DOCUMENT_ACCEPTANCE_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No =TSPL_EX_PI_HEAD.Document_Code  Left Outer Join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo " & _
            " and TSPL_DOCUMENT_ACCEPTANCE_MT .PurchaseInvoice_No=TSPL_EX_PI_HEAD.Document_Code and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code=TSPL_EX_PI_DETAIL.Item_Code " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_EX_PI_DETAIL.Item_Code where Scheme_Item='N'  and TSPL_EX_PI_HEAD.Status=1 and TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1   and TSPL_EX_PI_HEAD.document_type='MT' and isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No ,'')=''" & _
            " AND isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,'') IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')  and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty is not null          " & _
            "         union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo as Code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as [PI Code],TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo,''))>0   and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT'      " & _
            " union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo as Code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code as [PI Code],TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.PI_Code,''))>0 and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo,''))>0  and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')   and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT'" & _
            " )Final  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location  left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' group by Code,ICode,[PI Code]  having (SUM(Qty *RI)-SUM(Unapproved)) >0  order by Code,Icode"

        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Merchant PO Without LC") = CompairStringResult.Equal Then

            qry = " select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Max(Unit) as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty," & _
            " SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Max(Final.MRP) as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor," & _
            "   MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " & _
            " select TSPL_PURCHASE_ORDER_DETAIL.Line_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code  as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location," & _
            "  1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate," & _
            " TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.MRP ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per,PurchaseOrder_Date as TransDate,TSPL_PURCHASE_ORDER_DETAIL.Assessable  ,'' as Scheme_Code from " & _
            " TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code where TSPL_PURCHASE_ORDER_HEAD.Status=1  and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1  and" & _
            " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No not in(Select distinct MT_Against_PO from TSPL_EX_PI_HEAD where MT_Against_PO is not null ) AND isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,'') NOT IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code" & _
            " LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')           " & _
            "  union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate," & _
            " 0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  " & _
            " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO,''))>0    and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT' " & _
            " union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO  as Code,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit," & _
            " '' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  " & _
            " left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO,''))>0 and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')   and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT'" & _
            " )Final  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location  left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'     group by Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0  order by Code,Icode"

        ElseIf strDocType IsNot Nothing AndAlso clsCommon.myLen(strDocType) > 0 AndAlso clsCommon.CompairString(strDocType, "Merchant PO With LC") = CompairStringResult.Equal Then

            qry = " select CAST(0 as bit) as Sel,code ,[PO Code] ,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Max(Unit) as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, " & _
            " SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate," & _
            " MAX(Final.TAX10_Rate) as TAX10_Rate,Max(Final.MRP) as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from (   " & _
            " select TSPL_PURCHASE_ORDER_DETAIL.Line_No,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo as Code,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO Code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Code  as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType," & _
            " TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty  as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate," & _
            " TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.MRP ," & _
            " TSPL_PURCHASE_ORDER_DETAIL.Disc_Per,PurchaseOrder_Date as TransDate,TSPL_PURCHASE_ORDER_DETAIL.Assessable  ,'' as Scheme_Code from " & _
            " TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No lEFT oUTER Join TSPL_DOCUMENT_ACCEPTANCE_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
            " Left Outer Join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  and TSPL_DOCUMENT_ACCEPTANCE_MT .PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code  " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code where TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1   and TSPL_PURCHASE_ORDER_HEAD .MT_Is_Merchant_Trade=1 and isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No ,'')<>''" & _
            " AND isnull(TSPL_PURCHASE_ORDER_HEAD .MT_Payment_Terms_Group_Code,'') IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code " & _
            " LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')  and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty is not null " & _
            " union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo as Code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO as [PO Code],TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group," & _
            " 0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code " & _
            " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO,''))>0  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo,''))>0   and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT'   " & _
            " union all" & _
            " select TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Line_No,TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo as Code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO as [PO Code],TSPL_EX_COMMERCIAL_INVOICE_HEAD.Customer_Code as Vendor,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Item_Code as ICode,'' as IName," & _
            " '' as IType,0 as Qty,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Qty,0) as Unapproved,TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate," & _
            " 0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Assessable,0)as Assessable ,Scheme_Code from TSPL_EX_COMMERCIAL_INVOICE_DETAIL  left outer join TSPL_EX_COMMERCIAL_INVOICE_HEAD on TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code=TSPL_EX_COMMERCIAL_INVOICE_DETAIL.Document_Code" & _
            " where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_DETAIL.MT_Against_PO ,''))>0 and len(isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD.DocumentAcceptanceNo,''))>0  and TSPL_EX_COMMERCIAL_INVOICE_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')   and TSPL_EX_COMMERCIAL_INVOICE_HEAD.document_type='MT'" & _
            " )Final  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location  left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' group by Code,ICode,[PO Code]  having (SUM(Qty *RI)-SUM(Unapproved)) >0  order by Code,Icode"

            '======================DCC sale order===================================================
        Else
            qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " + Environment.NewLine
            qry += " select TSPL_SD_SALES_ORDER_DETAIL.Line_No,TSPL_SD_SALES_ORDER_DETAIL.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Vendor,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_SD_SALES_ORDER_DETAIL.Row_Type as IType, TSPL_SD_SALES_ORDER_DETAIL.Qty  as Qty,0 as Unapproved,TSPL_SD_SALES_ORDER_DETAIL.Unit_Code as Unit,TSPL_SD_SALES_ORDER_DETAIL.Location as Location,1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SD_SALES_ORDER_HEAD.Tax_Group,TSPL_SD_SALES_ORDER_DETAIL.TAX1_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX2_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX3_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX4_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX5_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX6_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX7_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX8_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX9_Rate,TSPL_SD_SALES_ORDER_DETAIL.TAX10_Rate,TSPL_SD_SALES_ORDER_DETAIL.MRP ,TSPL_SD_SALES_ORDER_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_SD_SALES_ORDER_DETAIL.Assessable " + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SALES_ORDER_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code  " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code" + Environment.NewLine
            qry += " where TSPL_SD_SALES_ORDER_DETAIL.Status=0 and Scheme_Item='N'  and TSPL_SD_SALES_ORDER_HEAD.Status=1 and TSPL_SD_SALES_ORDER_HEAD.close_yn='N'  " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code='" + VendorCode + "'"
            End If
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Order_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_SD_SHIPMENT_Head.Status=1 and Scheme_Item='N'  and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Order_Code,''))>0   " + Environment.NewLine
            qry += " union all   " + Environment.NewLine
            qry += " select TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Order_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
            qry += " ,Scheme_Code from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine
            qry += " where TSPL_SD_SHIPMENT_Head.Status=0 and Scheme_Item='N'  and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Order_Code,''))>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine
            qry += " )Final " + Environment.NewLine
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
            qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " + Environment.NewLine
            If IsAllowSingleSI4SingleSO = True Then
                qry += " WHERE Code Not in (Select Distinct Order_Code from TSPL_SD_SHIPMENT_DETAIL WHERE ISNULL(Order_Code,'')<>'') " + StrCondition + " "
            Else
                qry += "" + StrCondition1 + "  "
            End If
            qry += " group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"
        End If

        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            If clsCommon.myLen(VendorName) > 0 Then
                common.clsCommon.MyMessageBoxShow("No record found for vendor " + VendorName + "")
            Else
                common.clsCommon.MyMessageBoxShow("No record found.")
            End If
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
    End Sub

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
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
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
        repoCode.HeaderText = "Order No"
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
        repoVendor.HeaderText = "Customer"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

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
        repoCode.HeaderText = "Order No"
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

        Dim repoScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme.FormatString = ""
        repoScheme.HeaderText = "Scheme Code"
        repoScheme.Name = colSchemCode
        repoScheme.Width = 180
        repoScheme.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoScheme)

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
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used Qty"
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

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)




        Dim repoDiscPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscPer.FormatString = ""
        repoDiscPer.HeaderText = "Discount Per"
        repoDiscPer.Name = colDDisPer
        repoDiscPer.ReadOnly = True
        repoDiscPer.IsVisible = False
        repoDiscPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscPer)

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

    Sub setGridPropery()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

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
        Try
            If strDocType IsNot Nothing AndAlso clsCommon.CompairString(strDocType, "Export PI") = CompairStringResult.Equal Then
                ArrReturn_EX = New List(Of clsEXSalesOrderDetail)
                Dim obj As clsEXSalesOrderDetail = Nothing
                For ii As Integer = 0 To gv1.RowCount - 1
                    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                        obj = New clsEXSalesOrderDetail()
                        obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                        obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                        ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                        'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
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
                        obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                        obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                        If (obj.Balance_Qty > 0) Then
                            ArrReturn_EX.Add(obj)
                        End If
                    End If
                Next

                If ArrReturn_EX.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending SO item")
                Else
                    Me.Close()
                End If

            ElseIf strDocType IsNot Nothing AndAlso clsCommon.CompairString(strDocType, "Export Sale Invoice") = CompairStringResult.Equal Then
                ArrReturn_EX_Comm = New List(Of clsEXCommercialInvoiceDetail)
                Dim obj As clsEXCommercialInvoiceDetail = Nothing
                For ii As Integer = 0 To gv1.RowCount - 1
                    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                        obj = New clsEXCommercialInvoiceDetail()
                        obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                        obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                        ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                        'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
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
                        obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                        obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                        If (obj.Balance_Qty > 0) Then
                            ArrReturn_EX_Comm.Add(obj)
                        End If
                    End If
                Next

                If ArrReturn_EX_Comm.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending PI item")
                Else
                    clsERPFuncationality.closeForm(Me)
                End If


            ElseIf strDocType IsNot Nothing AndAlso (clsCommon.CompairString(strDocType, "Export COMMERCIAL") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Merchant PI Without LC") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Merchant PI With LC") = CompairStringResult.Equal) Then
                ArrReturn_EX_PI = New List(Of clsEXPorformaInvoiceDetail)
                Dim obj As clsEXPorformaInvoiceDetail = Nothing
                For ii As Integer = 0 To gv1.RowCount - 1
                    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                        obj = New clsEXPorformaInvoiceDetail()
                        If clsCommon.CompairString(strDocType, "Merchant PI With LC") = CompairStringResult.Equal Then
                            obj.Document_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select PurchaseInvoice_No from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value) & "'"))
                            strDocumentAcceptanceNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        Else
                            obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        End If

                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                        obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                        ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                        'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
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
                        obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                        obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                        If (obj.Balance_Qty > 0) Then
                            ArrReturn_EX_PI.Add(obj)
                        End If
                    End If
                Next

                If ArrReturn_EX_PI.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending PI item")
                Else
                    Me.Close()
                End If
                ''richa agarwal 26/04/2015
            ElseIf strDocType IsNot Nothing AndAlso (clsCommon.CompairString(strDocType, "Merchant PO Without LC") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Merchant PO With LC") = CompairStringResult.Equal) Then
                arrReturn_MT_Purc = New List(Of clsPurchaseOrderDetail)
                Dim obj As clsPurchaseOrderDetail = Nothing
                For ii As Integer = 0 To gv1.RowCount - 1
                    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                        obj = New clsPurchaseOrderDetail()
                        If clsCommon.CompairString(strDocType, "Merchant PO With LC") = CompairStringResult.Equal Then
                            obj.PurchaseOrder_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select PurchaseOrder_No from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo ='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value) & "'"))
                            strDocumentAcceptanceNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        Else
                            obj.PurchaseOrder_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        End If

                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                        obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                        ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                        'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
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
                        obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                        '  obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                        If (obj.Balance_Qty > 0) Then
                            arrReturn_MT_Purc.Add(obj)
                        End If
                    End If
                Next

                If arrReturn_MT_Purc.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending PI item")
                Else
                    Me.Close()
                End If

                ''--------------------------------------------

            Else
                ArrReturn = New List(Of clsSNSalesOrderDetail)
                Dim obj As clsSNSalesOrderDetail = Nothing
                For ii As Integer = 0 To gv1.RowCount - 1
                    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                        obj = New clsSNSalesOrderDetail()
                        obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                        obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                        ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                        'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
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
                        obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                        obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                        If (obj.Balance_Qty > 0) Then
                            ArrReturn.Add(obj)
                        End If
                    End If
                Next

                If ArrReturn.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending MRN item")
                Else
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Dim IsAllowSingleSI4SingleSO As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData("AllowSingleInvoiceAgainstSingleOrder", "AllowSingleInvoiceAgainstSingleOrder", Nothing)) = 1, True, False)
    Function isanotherrowchecked(ByVal Currentrowindex As Integer) As Boolean
        Dim r As Boolean = False
        Try
            If gvHead IsNot Nothing AndAlso gvHead.Rows.Count >= 0 AndAlso Currentrowindex >= 0 Then
                For i As Integer = 0 To gvHead.Rows.Count - 1
                    If i <> Currentrowindex AndAlso gvHead.Rows(i).Cells(colHSelect).Value = True Then
                        Return True
                    Else
                        r = False
                    End If
                Next

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return r
    End Function
    Private Sub gvHead_CellValidating(sender As Object, e As CellValidatingEventArgs) Handles gvHead.CellValidating
      
      

    End Sub
    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If Not IsAllowSingleSI4SingleSO Then
                If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                    If clsCommon.CompairString(strDocType, "Export COMMERCIAL") <> CompairStringResult.Equal Then
                        If isanotherrowchecked(gvHead.CurrentRow.Index) Then
                            e.Cancel = True
                            common.clsCommon.MyMessageBoxShow("You can select only one order at a time")
                            Exit Sub
                        End If
                    End If
                    Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                    Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)

                    If clsCommon.myLen(VendorCode) <= 0 Then
                        VendorCode = strVendorCode
                        VendorName = strVendorName
                    End If
                    If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
                        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                        If clsCommon.myLen(strCode) > 0 Then
                            LoadDetailData(e.NewValue, strCode)
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Order's Customer should be `" + VendorName)
                        e.Cancel = True
                    End If

                End If
            Else
                gv1.Rows.Clear()
                Dim strCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If e.NewValue = True Then
                    For Each grow As GridViewRowInfo In gvHead.Rows
                        grow.Cells(colHSelect).Value = False
                    Next
                    LoadDetailData(e.NewValue, strCode1)
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
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

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))



                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDDisPer).Value = clsCommon.myCdbl(dr("Disc_Per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemCode).Value = clsCommon.myCstr(dr("Scheme_Code"))

                End If
            Next
        Else

            If clsCommon.CompairString(strDocType, "Export COMMERCIAL") <> CompairStringResult.Equal Then
                For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    gv1.Rows.RemoveAt(ii)
                Next
            Else
                For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(ii)
                    End If
                Next
               
            End If


        End If
    End Sub
End Class

