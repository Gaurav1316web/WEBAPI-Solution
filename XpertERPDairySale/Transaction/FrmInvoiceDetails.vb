Imports common
Public Class FrmInvoiceDetails
#Region "Variables"
    Public strInvoiceNo As String = ""
    Public strInvoiceDate As String = ""
    Public strCustCode As String = ""
    Public PricePlanRoundOffDigit As String = "0"
#End Region
    Private Sub FrmInvoiceDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtInvoiceNo.Text = strInvoiceNo
        txtInvoiceDate.Text = strInvoiceDate
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            PricePlanRoundOffDigit = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PricePlanRoundOffDigit, clsFixedParameterCode.PricePlanRoundOffDigit, Nothing))

            Dim qry As String = "select --xx.Document_Code,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,sum(xx.QtyInLtr) as QtyInLtr,sum(xx.QtyInPouch) as QtyInPouch,max(xx.Rate_Per_Pouch) as Rate_Per_Pouch,sum(xx.Item_Net_Amt) as Item_Net_Amt
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,FORMAT((TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)),'N' + CAST('" + PricePlanRoundOffDigit + "' AS VARCHAR(10))) as Rate_Per_Pouch,CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR
from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_DETAIL.unit_code  
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + txtInvoiceNo.Text + "'
)xx 
group by xx.Document_Code,xx.Item_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()

            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintInvoice.Click
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim objMultPrintInvoice As New FrmPrintFreshInvoice
            Dim fromdate As String = ""
            Dim todate As String = ""

            Dim qry As String = ""
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtInvoiceNo.Text) > 0 Then



                    Dim dtDocdate As Date?
                    dtDocdate = Nothing
                    Dim StrSql As String = Nothing

                    StrSql = "Select Document_Code,Document_Date,Customer_Code,Bill_To_Location,is_taxable,Tax_Group from  TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + txtInvoiceNo.Text + "'"

                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
                    If dt1.Rows.Count > 0 Then
                        'IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
                        dtDocdate = clsCommon.myCDate(dt1.Rows(0)("Document_Date"))
                    End If
                    qry = objMultPrintInvoice.PrintInvoiceForAll(clsCommon.myCstr("'" & txtInvoiceNo.Text & "'"), clsCommon.myCDate(txtInvoiceDate.Text), strCustCode)
                    qry = "select max(XXFinal.Report_Status) as Report_Status, max(XXFinal.PaymentTerms) as PaymentTerms,max(XXFinal.Is_Distributor) as Is_Distributor, max(XXFinal.Is_BPL) as Is_BPL, max(XXFinal.Is_CashSale) as Is_CashSale, max(XXFinal.BPL_Coupon_Code) as BPL_Coupon_Code, max(XXFinal.BPL_Name) as BPL_Name, max(XXFinal.BPL_Remark) as BPL_Remark, max(XXFinal.BPL_Coupon_Date) as BPL_Coupon_Date, max(XXFinal.BPL_Category) as BPL_Category, max(XXFinal.PO_Indent_No) as PO_Indent_No, max(XXFinal.PO_Indent_Date) as PO_Indent_Date, max(XXFinal.Booking_OpeningBal) as Booking_OpeningBal, max(XXFinal.Booking_DrAmt) as Booking_DrAmt, max(XXFinal.Booking_CrAmt) as Booking_CrAmt, max(XXFinal.Booking_ClosingBal) as Booking_ClosingBal, max(XXFinal.Booking_ChequeNo) as Booking_ChequeNo, max(XXFinal.Is_DCS) as Is_DCS, max(XXFinal.Booking_Type) as Booking_Type, max(XXFinal.CST_LST) as CST_LST, max(cast(BarCode_Img as varbinary(max))) as BarCode_Img, max(XXFinal.DocumentTime) as DocumentTime, max(XXFinal.Manual_VehicleNo) as Manual_VehicleNo, max(XXFinal.Payment_Terms) as Payment_Terms, max(XXFinal.ReceiverName) as ReceiverName, sum(XXFinal.Amt_Less_Discount) as Amt_Less_Discount, max(XXFinal.Dispatch_OpeningBal) as Dispatch_OpeningBal, max(XXFinal.Dispatch_DrAmt) as Dispatch_DrAmt, max(XXFinal.Dispatch_CrAmt) as Dispatch_CrAmt, max(XXFinal.Dispatch_ClosingBal) as Dispatch_ClosingBal, max(XXFinal.Security_TotalAmt) as Security_TotalAmt, max(XXFinal.Supply_Date) as Supply_Date, max(XXFinal.Shift_Type) as Shift_Type, sum(XXFinal.QTY_LTRKG) as QTY_LTRKG, max(XXFinal.ITAX1) as ITAX1, max(XXFinal.ITAX1_RATE) as ITAX1_RATE, max(XXFinal.ITAX1_Amt) as ITAX1_Amt, max(XXFinal.ITAX1_Base_Amt) as ITAX1_Base_Amt, max(XXFinal.ITAX2) as ITAX2, max(XXFinal.ITAX2_RATE) as ITAX2_RATE, sum(XXFinal.ITAX2_Amt) as ITAX2_Amt, sum(XXFinal.ITAX2_Base_Amt) as ITAX2_Base_Amt, max(XXFinal.ITAX3) as ITAX3, max(XXFinal.ITAX3_Rate) as ITAX3_Rate, sum(XXFinal.ITAX3_Amt) as ITAX3_Amt, sum(XXFinal.ITAX3_Base_Amt) as ITAX3_Base_Amt, max(XXFinal.ITAX4) as ITAX4, max(XXFinal.ITAX4_RATE) as ITAX4_RATE, sum(XXFinal.ITAX4_Amt) as ITAX4_Amt, sum(XXFinal.ITAX4_Base_Amt) as ITAX4_Base_Amt, max(XXFinal.ITAX5) as ITAX5, max(XXFinal.ITAX5_RATE) as ITAX5_RATE, sum(XXFinal.ITAX5_Amt) as ITAX5_Amt, sum(XXFinal.ITAX5_Base_Amt) as ITAX5_Base_Amt, max(XXFinal.ITAX6) as ITAX6, max(XXFinal.ITAX6_RATE) as ITAX6_RATE, sum(XXFinal.ITAX6_Amt) as ITAX6_Amt, sum(XXFinal.ITAX6_Base_Amt) as ITAX6_Base_Amt, max(XXFinal.ITAX7) as ITAX7, max(XXFinal.ITAX7_Rate) as ITAX7_Rate, sum(XXFinal.ITAX7_Amt) as ITAX7_Amt, sum(XXFinal.ITAX7_Base_Amt) as ITAX7_Base_Amt, max(XXFinal.ITAX8) as ITAX8, max(XXFinal.ITAX8_RATE) as ITAX8_RATE, sum(XXFinal.ITAX8_Amt) as ITAX8_Amt, sum(XXFinal.ITAX8_Base_Amt) as ITAX8_Base_Amt, max(XXFinal.ITAX9) as ITAX9, max(XXFinal.ITAX9_Rate) as ITAX9_Rate, sum(XXFinal.ITAX9_Amt) as ITAX9_Amt, sum(XXFinal.ITAX9_Base_Amt) as ITAX9_Base_Amt, max(XXFinal.ITAX10) as ITAX10, max(XXFinal.ITAX10_RATE) as ITAX10_RATE, sum(XXFinal.ITAX10_Amt) as ITAX10_Amt, sum(XXFinal.ITAX10_Base_Amt) as ITAX10_Base_Amt, max(XXFinal.IRN_No) as IRN_No, max(XXFinal.Zone_Code) as Zone_Code, max(XXFinal.CF) as CF, max(XXFinal.ConversionFactor) as ConversionFactor, max(XXFinal.EInvoice_Type) as EInvoice_Type, max(XXFinal.LeakageDeduction_Freshsale) as LeakageDeduction_Freshsale, max(XXFinal.LeakageDeduction) as LeakageDeduction, max(XXFinal.SCM) as SCM, max(XXFinal.DIS_MARGIN) as DIS_MARGIN, max(XXFinal.NearestCity) as NearestCity, max(XXFinal.Location_Desc) as Location_Desc, max(XXFinal.Loc_Short_Name) as Loc_Short_Name, max(XXFinal.Loc_Pin) as Loc_Pin, max(XXFinal.Loc_Phone) as Loc_Phone, max(XXFinal.Loc_Eamil) as Loc_Eamil, max(XXFinal.Loc_Website) as Loc_Website, max(XXFinal.ISO_No) as ISO_No, max(XXFinal.Invoice_No) as Invoice_No, max(XXFinal.Invoice_Date) as Invoice_Date, max(XXFinal.Cust_City) as Cust_City, max(XXFinal.Against_Shipment_No) as Against_Shipment_No, max(XXFinal.Cust_Gst_StateCode) as Cust_Gst_StateCode, max(XXFinal.Electronic_Ref_No) as Electronic_Ref_No, max(XXFinal.CustGSTNo) as CustGSTNo, max(XXFinal.Area_Code) as Area_Code, max(XXFinal.gst_state_code) as gst_state_code, max(XXFinal.LocGstNo) as LocGstNo, max(XXFinal.EWayBillNo) as EWayBillNo, max(XXFinal.EWayBillDate) as EWayBillDate, max(XXFinal.HSN_Code) as HSN_Code, max(XXFinal.InvRemarks) as InvRemarks, max(XXFinal.Delivery_Code) as Delivery_Code, max(XXFinal.Conversion_factor) as Conversion_factor, sum(XXFinal.QTY_Box) as QTY_Box, max(XXFinal.Sale_Invoice_No) as Sale_Invoice_No, max(XXFinal.vehicleNo) as vehicleNo, max(XXFinal.Sale_Invoice_Date) as Sale_Invoice_Date, sum(XXFinal.RoundOffAmount) as RoundOffAmount, sum(XXFinal.Total_Amt) as Total_Amt, max(XXFinal.Loc_ADd1) as Loc_ADd1, max(XXFinal.LOC_ADD2) as LOC_ADD2, max(XXFinal.LOC_ADD3) as LOC_ADD3, max(XXFinal.LocationState) as LocationState, max(XXFinal.LOCPhone) as LOCPhone, max(XXFinal.Loc_TIN_NO) as Loc_TIN_NO, (XXFinal.Document_Code) as Document_Code, max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Description) as Description, max(XXFinal.Lorry_No) as Lorry_No, max(XXFinal.Sku_Seq) as Sku_Seq, XXFinal.Item_Code as Item_Code, max(XXFinal.Line_No) as Line_No, max(XXFinal.Item_Desc) as Item_Desc, sum(XXFinal.QtyCrates) as QtyCrates, max(XXFinal.ConvFactInCrate) as ConvFactInCrate, max(XXFinal.ConvQtyInCrate) as ConvQtyInCrate, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Qty_Default) as Qty_Default, max(XXFinal.Rate_Default) as Rate_Default, sum(XXFinal.QtyPCS) as QtyPCS, sum(XXFinal.free_qty) as free_qty, max(XXFinal.FreeSchemeInLitres) as FreeSchemeInLitres, max(XXFinal.RatePerPcs) as RatePerPcs, sum(XXFinal.valueInRs) as valueInRs, max(XXFinal.comp_add2) as comp_add2, max(XXFinal.comp_add3) as comp_add3, max(XXFinal.CompPhone) as CompPhone, max(XXFinal.Cash_Scheme_Amount) as Cash_Scheme_Amount, sum(XXFinal.schemeInCrates) as schemeInCrates, max(XXFinal.GrandTotalCrates) as GrandTotalCrates, max(XXFinal.Comp_Code) as Comp_Code, max(XXFinal.Comp_Name) as Comp_Name, max(XXFinal.comp_add1) as comp_add1, max(XXFinal.comp_Fax) as comp_Fax, max(XXFinal.comp_Email) as comp_Email, max(XXFinal.comp_tinNo) as comp_tinNo, max(XXFinal.cust_Code) as cust_Code, max(XXFinal.Customer_Name) as Customer_Name, max(XXFinal.cust_add1) as cust_add1, max(XXFinal.cust_add2) as cust_add2, max(XXFinal.cust_add3) as cust_add3, max(XXFinal.CustPhone) as CustPhone, max(XXFinal.cust_fax) as cust_fax, max(XXFinal.Cust_state) as Cust_state, max(XXFinal.cust_Statename) as cust_Statename, max(XXFinal.cust_Email) as cust_Email, max(XXFinal.cust_website) as cust_website, max(XXFinal.Customer_Pan) as Customer_Pan, max(XXFinal.Ack_No) as Ack_No, max(XXFinal.Ack_Date) as Ack_Date, max(XXFinal.TaxableNonTaxable) as TaxableNonTaxable, max(XXFinal.TAX1) as TAX1, max(XXFinal.TaxType1) as TaxType1, sum(XXFinal.TAX1_Amt) as TAX1_Amt, max(XXFinal.TAX1_Rate) as TAX1_Rate, sum(XXFinal.TAX1Amt) as TAX1Amt, max(XXFinal.TaxType2) as TaxType2, max(XXFinal.TAX2) as TAX2, max(XXFinal.TAX2_Amt) as TAX2_Amt, max(XXFinal.TAX2_Rate) as TAX2_Rate, max(XXFinal.TAX2Amt) as TAX2Amt, max(XXFinal.TaxType3) as TaxType3, max(XXFinal.TAX3) as TAX3, max(XXFinal.TAX3_Amt) as TAX3_Amt, max(XXFinal.TAX3_Rate) as TAX3_Rate, max(XXFinal.TAX3Amt) as TAX3Amt, max(XXFinal.TaxType4) as TaxType4, max(XXFinal.TAX4) as TAX4, max(XXFinal.TAX4_Amt) as TAX4_Amt, max(XXFinal.TAX4_Rate) as TAX4_Rate, max(XXFinal.TAX4Amt) as TAX4Amt, max(XXFinal.TaxType5) as TaxType5, max(XXFinal.TAX5) as TAX5, max(XXFinal.TAX5_Amt) as TAX5_Amt, max(XXFinal.TaxType6) as TaxType6, max(XXFinal.TAX6) as TAX6, max(XXFinal.TAX6_Amt) as TAX6_Amt, max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc, sum(XXFinal.Distributor_Commission_TotalAmt) as Distributor_Commission_TotalAmt, sum(XXFinal.Transporter_Commission_TotalAmt) as Transporter_Commission_TotalAmt, max(XXFinal.Transport_Id) as Transport_Id, max(XXFinal.Transporter_Name) as Transporter_Name, max(XXFinal.Against_Delivery_Code) as Against_Delivery_Code, max(XXFinal.batchNO) as batchNO, max(XXFinal.Credit_Customer) as Credit_Customer, max(XXFinal.Ship_To_Code) as Ship_To_Code, max(XXFinal.Ship_To_Desc) as Ship_To_Desc, max(XXFinal.Ship_Address) as Ship_Address, max(XXFinal.Ship_City) as Ship_City, max(XXFinal.Ship_State) as Ship_State, max(XXFinal.Ship_Pin_Code) as Ship_Pin_Code, max(XXFinal.Ship_PAN) as Ship_PAN, max(XXFinal.Ship_GSTNO) as Ship_GSTNO, sum(XXFinal.Booth_Security_Amt) as Booth_Security_Amt, max(XXFinal.Billing_Unit_code) as Billing_Unit_code, sum(XXFinal.Billing_Qty) as Billing_Qty, max(XXFinal.BulkCF) as BulkCF, sum(XXFinal.Total_Basic_Amt) as Total_Basic_Amt, max(XXFinal.Brand) as Brand, max(XXFinal.BRANDDESC) as BRANDDESC, max(XXFinal.Particulars) as Particulars, max(XXFinal.Crate_No) as Crate_No, max(cast(Logo_Img as varbinary(max))) as Logo_Img, max(XXFinal.CopyType) as CopyType, max(XXFinal.SellerGST) as SellerGST, max(XXFinal.Pan_No) as Pan_No, max(XXFinal.Bank_Name) as Bank_Name, max(XXFinal.BankAccountNo) as BankAccountNo, max(XXFinal.BankBranchAddress) as BankBranchAddress, max(XXFinal.BankIFSCCode) as BankIFSCCode, max(XXFinal.Tcan_No) as Tcan_No, max(XXFinal.RateLtr) as RateLtr, max(XXFinal.Company_Name) as Company_Name, max(XXFinal.Address2) as Address2, max(XXFinal.Regn_No) as Regn_No, max(XXFinal.FSSAI_NO) as FSSAI_NO, max(XXFinal.Receipt_No) as Receipt_No, max(XXFinal.Receipt_Date) as Receipt_Date, sum(XXFinal.Receipt_Amount) as Receipt_Amount, max(XXFinal.Payment_Code) as Payment_Code, max(XXFinal.cheque_No) as cheque_No, max(XXFinal.Cheque_Date) as Cheque_Date, max(XXFinal.OpeningBal) as OpeningBal, max(XXFinal.ClosingBal) as ClosingBal from( " + qry + "   ) XXFinal   group by XXFinal.Item_Code,XXFinal.Document_Code "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        'If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX7")) = "IGST"  Then
                        If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" Then
                            frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceIGST", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                        Else
                            frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceGNG1", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                        End If                    '


                        'Else
                        '    filePath = frmCRV.funsubreportWithdt(MyBase.Form_ID, isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoice", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                    End If
                    'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoice", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                    frmCRV = Nothing
                Else
                    Throw New Exception("Invoice not found")
                End If
            Else
                If clsCommon.myLen(txtInvoiceNo.Text) > 0 Then
                    If common.clsCommon.MyMessageBoxShow(" Do you want to print Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        qry = "select  MIN(Supply_Date) AS fromdate,
    MAX(Supply_Date) AS todate
	from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode in(select distinct TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode from TSPL_SD_SHIPMENT_DETAIL
left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
where TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE in(select distinct Shipment_Code from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" + txtInvoiceNo.Text + "'))"
                        Dim ddt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If ddt IsNot Nothing AndAlso ddt.Rows.Count > 0 Then
                            If ddt.Rows(0)("fromdate") IsNot DBNull.Value Then
                                fromdate = clsCommon.myCstr(clsCommon.GetPrintDate(ddt.Rows(0)("fromdate"), "dd/MMM/yyyy"))
                            End If
                            If ddt.Rows(0)("todate") IsNot DBNull.Value Then
                                todate = clsCommon.myCstr(clsCommon.GetPrintDate(ddt.Rows(0)("todate"), "dd/MMM/yyyy"))
                            End If
                        End If
                        qry = "select  TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.Access_Officer as FassiLicNo,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3 as CompAddress,TSPL_COMPANY_MASTER.Phone1,'" + txtInvoiceDate.Text + "' as BillDate,xxx.*,'" + fromdate + "' as GPFromDate,'" + todate + "' as GPTodate,(SELECT STUFF(( SELECT DISTINCT ', ' + Right(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode,6) FROM TSPL_SD_SHIPMENT_DETAIL LEFT JOIN TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL ON TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID = TSPL_SD_SHIPMENT_DETAIL.PK_ID WHERE TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE IN ( SELECT DISTINCT Shipment_Code FROM TSPL_SD_SALE_INVOICE_DETAIL WHERE DOCUMENT_CODE = '" + txtInvoiceNo.Text + "' ) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS GPCodeList) as GPCode
from(
select xx.Document_Code,max(xx.Route_No) as Route_No,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,sum(xx.QtyInLtr) as QtyInLtr,sum(xx.QtyInPouch) as QtyInPouch,max(xx.Rate_Per_Pouch) as Rate_Per_Pouch,sum(xx.Item_Net_Amt) as Item_Net_Amt,max(xx.Customer_Name) as Customer_Name,max(xx.Add1) as Add1,max(xx.Add2)as Add2,max(xx.add3) as Add3,
max(xx.GST_STATE_Code) as GST_STATE_Code,max(xx.STATE_NAME) as STATE_NAME,max(xx.GSTNO)as GSTNO,sum(xx.Transporter_Commission_TotalAmt)as TCAmt
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)) as Rate_Per_Pouch,CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR,
TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3 ,TSPL_STATE_MASTER.GST_STATE_Code, TSPL_STATE_MASTER.STATE_NAME ,TSPL_CUSTOMER_MASTER.GSTNO,isnull(TSPL_SD_SALE_INVOICE_HEAD.Transporter_Commission_TotalAmt,0) as Transporter_Commission_TotalAmt

from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State
left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_DETAIL.unit_code  
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + txtInvoiceNo.Text + "'
)xx 
group by xx.Document_Code,xx.Item_Code
)XXX
left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='UDP'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                            '    'If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX7")) = "IGST"  Then
                            '    If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" Then
                            '        frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceIGST", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            '    Else
                            '        frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceGNG1", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            '    End If
                            'Else

                            frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptMultipleInvoice", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())

                            'End If
                        Else

                            frmCRV = Nothing
                        End If

                    End If
                End If
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class