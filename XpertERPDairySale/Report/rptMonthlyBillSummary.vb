Imports common
Public Class rptMonthlyBillSummary
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub
    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1  "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub
    Sub reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDocumentdate.IsChecked = True
        rbtnSupplydate.IsChecked = False
        chkExcludeShift.Checked = False
    End Sub
    Private Sub rptMonthlyBillSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub Print()
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Dim whrcls As String = ""
            Dim forCustomerWise As String = ""
            Dim Route As String = ""
            Dim maxroute As String = ""
            Dim Group As String = ""
            Dim fromdate As Date = txtfDate.Value.AddDays(1)
            Dim todate As Date = txtToDate.Value.AddDays(-1)
            If rbtnSupplydate.IsChecked Then
                If chkExcludeShift.Checked Then
                    whr = " (convert(date,Final.Supply_Date,103)='" + clsCommon.GetPrintDate(txtfDate.Value) + "'  and Shift_Type='EVENING') OR  (convert(date,Final.Supply_Date,103)>='" + clsCommon.GetPrintDate(fromdate) + "' AND convert(date,Final.Supply_Date,103)<='" + clsCommon.GetPrintDate(todate) + "')
                        OR (convert(date,Final.Supply_Date,103)='" + clsCommon.GetPrintDate(txtToDate.Value) + "'  and Shift_Type='MORNING') "
                Else
                    whr = "  convert(date,Final.Supply_Date,103)>='" + clsCommon.GetPrintDate(txtfDate.Value) + "' AND convert(date,Final.Supply_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' "
                End If
            ElseIf rbtnDocumentdate.IsChecked Then
                whr += "(convert(date,Final.Shipment_Date,103)>='" + clsCommon.GetPrintDate(txtfDate.Value) + "' AND convert(date,Final.Shipment_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "')"
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                whrcls += " and Final.Cust_Code in  (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                whrcls += " and Route_No in (" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")"
            End If
            If rbtnrouteWise.IsChecked Then
                maxroute = " Route_No, "
                Group += " GROUP BY 
                        Item_Code,Route_No,cust_Code,ReportRate ORDER BY Supply_Date,Shift_Type "
            ElseIf rbtnCustomerWise.IsChecked Then
                maxroute = " Max(Route_No)Route_No, "
                Group += "GROUP BY  Item_Code,cust_Code,ReportRate ORDER BY Supply_Date,Shift_Type"
            End If
            If rbtnCustomerWise.IsChecked Then
                Route = ",max(Routes)Routes"
                forCustomerWise = "left join (SELECT 
    Customer_Code,
    STUFF((
        SELECT DISTINCT ', ' + Route_No
        FROM TSPL_SD_SALE_INVOICE_HEAD AS InnerTable
        WHERE InnerTable.Customer_Code = OuterTable.Customer_Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Routes
FROM 
    TSPL_SD_SALE_INVOICE_HEAD AS OuterTable
GROUP BY 
    Customer_Code)RR on RR.Customer_Code=final.cust_Code "
            End If
            qry = "select '" + txtfDate.Value + "' as FromDate,'" + txtToDate.Value + "' as todate " + Route + "
,0 as valueInRs,
sum(Base_Amt)Base_Amt,sum(TotalAmt)TotalAmt,ReportRate,
  max(Payment_Terms) AS Payment_Terms, 
  MAX(Is_Distributor) AS Is_Distributor, 
  MAX(Is_BPL) Is_BPL, 
  MAX(Is_CashSale) Is_CashSale, 
  MAX(Is_DCS) Is_DCS, 
  MAX(Booking_Type) Booking_Type, 
  MAX(CST_LST) CST_LST, 
  MAX(Manual_VehicleNo) Manual_VehicleNo, 
  MAX(PaymentTerms) PaymentTerms, 
  MAX(ReceiverName) ReceiverName, 
  SUM(Security_TotalAmt) Security_TotalAmt, 
  MAX(Supply_Date) Supply_Date, 
  MAX(Shift_Type) Shift_Type, 
   
  MAX(ITAX1) ITAX1, 
  SUM(ITAX1_RATE) ITAX1_RATE, 
  SUM(ITAX1_Amt) ITAX1_Amt, 
  MAX(ITAX2) ITAX2, 
  SUM(ITAX2_RATE) ITAX2_RATE, 
  SUM(ITAX2_Amt) ITAX2_Amt, 
  MAX(ITAX3) ITAX3, 
  SUM(ITAX3_RATE) ITAX3_RATE, 
  SUM(ITAX3_Amt) ITAX3_Amt, 
  MAX(ITAX4) ITAX4, 
  SUM(ITAX4_RATE) ITAX4_RATE, 
  SUM(ITAX4_Amt) ITAX4_Amt, 
  MAX(ITAX5) ITAX5, 
  SUM(ITAX5_RATE) ITAX5_RATE, 
  SUM(ITAX5_Amt) ITAX5_Amt, 
  MAX(ITAX6) ITAX6, 
  SUM(ITAX6_RATE) ITAX6_RATE, 
  SUM(ITAX6_Amt) ITAX6_Amt, 
  MAX(ITAX7) ITAX7, 
  SUM(ITAX7_RATE) ITAX7_RATE, 
  SUM(ITAX7_Amt) ITAX7_Amt, 
  MAX(ITAX8) ITAX8, 
  SUM(ITAX8_RATE) ITAX8_RATE, 
  SUM(ITAX8_Amt) ITAX8_Amt, 
  MAX(ITAX9) ITAX9, 
  SUM(ITAX9_RATE) ITAX9_RATE, 
  SUM(ITAX9_Amt) ITAX9_Amt, 
  MAX(ITAX10) ITAX10, 
  SUM(ITAX10_RATE) ITAX10_RATE, 
  SUM(ITAX10_Amt) ITAX10_Amt, 
  MAX(Zone_Code) Zone_Code, 
   
  SUM(ConversionFactor) ConversionFactor, 
  MAX(EInvoice_Type) EInvoice_Type, 
  SUM(LeakageDeduction_Freshsale) LeakageDeduction_Freshsale, 
  SUM(LeakageDeduction) LeakageDeduction, 
  MAX(Location_Desc) Location_Desc, 
  MAX(Loc_Short_Name) Loc_Short_Name, 
  MAX(Loc_Pin) Loc_Pin, 
  MAX(Loc_Phone) Loc_Phone, 
  MAX(Loc_Eamil) Loc_Eamil, 
  MAX(Loc_Website) Loc_Website, 
  MAX(ISO_No) ISO_No, 
  MAX(Invoice_No) Invoice_No, 
  MAX(Invoice_Date) Invoice_Date, 
  MAX(Cust_City) Cust_City, 
  MAX(Against_Shipment_No) Against_Shipment_No, 
  MAX(Cust_Gst_StateCode) Cust_Gst_StateCode, 
  MAX(Electronic_Ref_No) Electronic_Ref_No, 
  MAX(CustGSTNo) CustGSTNo, 
  MAX(GST_STATE_Code) GST_STATE_Code, 
  MAX(LocGstNo) LocGstNo, 
  MAX(EWayBillDate) EWayBillDate, 
  MAX(EWayBillNo) EWayBillNo, 
  MAX(HSN_Code) HSN_Code, 
  MAX(InvRemarks) InvRemarks, 
  MAX(Delivery_Code) Delivery_Code, 
  MAX(Sale_Invoice_No) Sale_Invoice_No, 
  MAX(vehicleNo) vehicleNo, 
  MAX(Sale_Invoice_Date) Sale_Invoice_Date, 
  SUM(RoundOffAmount) RoundOffAmount, 
  MAX(Loc_ADd1) Loc_ADd1, 
  MAX(LOC_ADD2) LOC_ADD2, 
  MAX(LOC_ADD3) LOC_ADD3, 
  MAX(LocationState) LocationState, max(STATE_CODE)LOCSTATE_CODE,max(City_Code)LOCCity_Code, 
  MAX(LOCPhone) LOCPhone, 
  MAX(Loc_TIN_NO) Loc_TIN_NO, 
  MAX(Document_Code) Document_Code, 
  MAX(Document_Date) Document_Date, 
  MAX(Description) Description, 
  MAX(Lorry_No) Lorry_No, 
  MAX(Sku_Seq) Sku_Seq, 
  Item_Code, 
  MAX(Item_Desc) Item_Desc, 
  SUM(QtyCrates) QtyCrates, 

  MAx(Unit_code) Unit_code, 
  MAX(UOM_Code) UOM_Code, 
  SUM(Qty_Default) Qty_Default, 
  SUM(QtyAccToReportUOM) QtyAccToReportUOM, 
  MAX(GrandTotalCrates) GrandTotalCrates, 
  MAX(Comp_Code) Comp_Code, 
  MAX(Comp_Name) Comp_Name, 
  MAX(comp_add1) comp_add1, 
  MAX(comp_Fax) comp_Fax, 
  MAX(comp_Email) comp_Email, 
  MAX(comp_tinNo) comp_tinNo, 
  (cust_Code) cust_Code, 
  MAX(Customer_Name) Customer_Name, 
  MAX(cust_add1) cust_add1, 
  MAX(cust_add2) cust_add2, 
  MAX(cust_add3) cust_add3, 
  MAX(CustPhone) CustPhone, 
  MAX(cust_fax) cust_fax, 
  MAX(cust_Statename) cust_Statename, 
  MAX(cust_Email) cust_Email, 
  MAX(cust_website) cust_website, 
  MAX(Customer_Pan) Customer_Pan, 
  MAX(Ack_No) Ack_No, 
  MAX(Ack_Date) Ack_Date, 
  MAX(TaxableNonTaxable) TaxableNonTaxable, 
  MAX(TAX1) TAX1, 
  MAX(TaxType1) TaxType1, 
  SUM(TAX1_Rate) TAX1_Rate, 
  SUM(TAX1_Amt) TAX1_Amt, 
  MAX(TAX2) TAX2, 
  MAX(TaxType2) TaxType2, 
  SUM(TAX2_Rate) TAX2_Rate, 
  SUM(TAX2_Amt) TAX2_Amt, 
  MAX(TAX3) TAX3, 
  MAX(TaxType3) TaxType3, 
  SUM(TAX3_Rate) TAX3_Rate, 
  SUM(TAX3_Amt) TAX3_Amt, 
  MAX(TAX4) TAX4, 
  MAX(TaxType4) TaxType4, 
  SUM(TAX4_Rate) TAX4_Rate, 
  SUM(TAX4_Amt) TAX4_Amt, 
  MAX(TAX5) TAX5, 
  MAX(TaxType5) TaxType5, 
  SUM(TAX5_Amt) TAX5_Amt, 
  MAX(TAX6) TAX6, 
  MAX(TaxType6) TaxType6, 
  SUM(TAX6_Amt) TAX6_Amt, 
  " + maxroute + "
  MAX(Route_Desc) Route_Desc, 
  SUM(
    Distributor_Commission_TotalAmt
  ) Distributor_Commission_TotalAmt, 
  SUM(
    Transporter_Commission_TotalAmt
  ) Transporter_Commission_TotalAmt, 
  MAX(Against_Delivery_Code) Against_Delivery_Code, 
  MAX(Credit_Customer) Credit_Customer, 
  SUM(Booth_Security_Amt) Booth_Security_Amt, 
  MAX(Particulars) Particulars, 
  MAX(CopyType) CopyType, 
  MAX(SellerGST) SellerGST, 
  MAX(Pan_No) Pan_No 
from 
  (
    select 
      Main_Final.*, 
      TSPL_COMPANY_MASTER.Logo_Img, 
      1 As CopyType, 
      TSPL_COMPANY_MASTER.GSTReg_No As SellerGST, 
      TSPL_COMPANY_MASTER.Pan_No 
    from 
      (
        select 
          final.*, 
          Item_Desc as Particulars
          
        from 
          (
            Select 
              case when TSPL_BOOKING_MATSER.Is_CashSale = 'Y' then 'CASH' else 'CREDIT' END AS PaymentTerms, 
              TSPL_BOOKING_MATSER.Is_Distributor, 
              TSPL_BOOKING_MATSER.Is_BPL, 
              TSPL_BOOKING_MATSER.Is_CashSale, 
              TSPL_BOOKING_MATSER.Is_DCS, 
              TSPL_BOOKING_MATSER.Booking_Type, 
              TSPL_COMPANY_MASTER.CST_LST, 
             
              TSPL_SD_SHIPMENT_HEAD.ManualVehicle as Manual_VehicleNo, 
              TSPL_SD_SHIPMENT_HEAD.Payment_Terms, 
              TSPL_SD_SHIPMENT_HEAD.ReceiverName, 
              TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt, 
              convert(
                varchar(12), 
                TSPL_SD_SHIPMENT_HEAD.Supply_Date, 
                103
              ) Supply_Date, TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt,
              case when TSPL_SD_SHIPMENT_HEAD.Shift_Type = 'AM' then 'Morning' else 'Evening' end as Shift_Type, 
              
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as ITAX1, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_RATE AS ITAX1_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as ITAX1_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as ITAX2, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_RATE AS ITAX2_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as ITAX2_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3 AS ITAX3, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate AS ITAX3_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as ITAX3_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4 AS ITAX4, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_RATE AS ITAX4_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as ITAX4_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as ITAX5, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5_RATE AS ITAX5_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt as ITAX5_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as ITAX6, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6_RATE AS ITAX6_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt as ITAX6_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7 AS ITAX7, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate AS ITAX7_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt as ITAX7_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8 AS ITAX8, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8_RATE AS ITAX8_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt as ITAX8_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9 AS ITAX9, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate AS ITAX9_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt as ITAX9_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10 AS ITAX10, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10_RATE AS ITAX10_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt as ITAX10_Amt, 
               
              Zone_Code, 
             
              TSPL_ITEM_UOM_DETAIL.Conversion_Factor As ConversionFactor, 
              TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type, 
              0 as LeakageDeduction_Freshsale, 
              0 as LeakageDeduction, 
              TSPL_LOCATION_MASTER.Location_Desc, 
              TSPL_LOCATION_MASTER.Loc_Short_Name, 
              TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin, 
              (
                case when isnull(TSPL_LOCATION_MASTER.Phone1, '')<> '' then TSPL_LOCATION_MASTER.Phone1 when isnull(TSPL_LOCATION_MASTER.Phone2, '')<> '' then + ', ' + TSPL_LOCATION_MASTER.Phone2 end
              ) as Loc_Phone, 
              TSPL_LOCATION_MASTER.Email as Loc_Eamil, 
              '' as Loc_Website, 
              TSPL_COMPANY_MASTER.ISO_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No, 
              convert(
                varchar(12), 
                TSPL_SD_SALE_INVOICE_HEAD.Document_date, 
                103
              ) as Invoice_Date, 
			   convert(
                varchar(12), 
                TSPL_SD_SHIPMENT_HEAD.Document_date, 
                103
              ) as Shipment_Date,
              customer_city_master.city_name as Cust_City, 
              TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No, 
              CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode, 
              TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No, 
              Tspl_customer_master.gstno as CustGSTNo, 
              TSPL_STATE_MASTER.gst_state_code, 
              tspl_location_master.gstno as LocGstNo, 
              TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo, 
              TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate, 
              TSPL_ITEM_MASTER.HSN_Code, 
              TSPL_SD_SALE_INVOICE_HEAD.Remarks as InvRemarks, 
              TSPL_SD_sale_invoice_DETAIL.Delivery_Code, 
              
             
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Sale_Invoice_No, 
              Case When ISNULL(
                TSPL_SD_SHIPMENT_HEAD.ManualVehicle, 
                ''
              )<> '' Then TSPL_SD_SHIPMENT_HEAD.ManualVehicle WHEN ISNULL(
                TSPL_SD_SHIPMENT_HEAD.AlternateVehicle, 
                ''
              )<> '' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SALE_INVOICE_HEAD.vehicleNo End as vehicleNo, 
              Convert(
                varchar, TSPL_SD_SALE_INVOICE_HEAD.Sale_Invoice_Date, 
                103
              ) as Sale_Invoice_Date, 
              TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount, 
              TSPL_LOCATION_MASTER.Add1 as Loc_ADd1, 
              TSPL_LOCATION_MASTER.Add2 as LOC_ADD2, 
              TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, 
              TSPL_STATE_MASTER.State_Name as LocationState, TSPL_STATE_MASTER.STATE_CODE,TSPL_LOCATION_MASTER.City_Code,
              case when ISNULL(TSPL_LOCATION_MASTER.Phone1, '')= '(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end + Case When ISNULL(TSPL_LOCATION_MASTER.Phone2, '')<> '(+__)__________' Then ', ' + TSPL_LOCATION_MASTER.Phone2 Else '' End as LOCPhone, 
              TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO, 
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code, 
              convert(
                varchar, TSPL_SD_SHIPMENT_HEAD.Document_Date, 
                103
              ) as Document_Date, 
              TSPL_SD_SALE_INVOICE_HEAD.Description, 
              TSPL_SD_SALE_INVOICE_HEAD.Lorry_No, 
              TSPL_ITEM_MASTER.Sku_Seq, 
              CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then TSPL_SD_sale_invoice_DETAIL.Item_Code + ' -Scheme' else TSPL_SD_sale_invoice_DETAIL.Item_Code end as Item_Code, 
              (
                CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then 0 else (
                  TSPL_SD_sale_invoice_DETAIL.Line_No
                ) end
              ) as Line_No, 
              CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then TSPL_ITEM_MASTER.Item_Desc + ' -Scheme' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc, 
              TSPL_SD_sale_invoice_DETAIL.Crate as QtyCrates, 
              TSPL_SD_sale_invoice_DETAIL.Unit_code, 
              ItemConvReportUOM.UOM_Code, 
              convert(
                Decimal(18, 2), 
                TSPL_SD_sale_invoice_DETAIL.Qty
              ) as Qty_Default, 
              cast(
                (
                  TSPL_SD_sale_invoice_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
                ) as Decimal(18, 2)
              ) as QtyAccToReportUOM, Convert(decimal(18,2),(TSPL_SD_sale_invoice_DETAIL.Amt_Less_Discount/((
                TSPL_SD_sale_invoice_DETAIL.Qty
              *ItemConvinUOM.Conversion_Factor )/ItemConvReportUOM.Conversion_Factor))) as ReportRate,
              '' GrandTotalCrates, 
              TSPL_COMPANY_MASTER.Comp_Code, 
              TSPL_COMPANY_MASTER.Comp_Name, 
              TSPL_COMPANY_MASTER.Add1 as comp_add1, 
              TSPL_COMPANY_MASTER.Fax as comp_Fax, 
              TSPL_COMPANY_MASTER.Email as comp_Email, 
              TSPL_COMPANY_MASTER.Tin_No as comp_tinNo, 
              TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as cust_Code, 
              TSPL_CUSTOMER_MASTER.Customer_Name, 
              TSPL_CUSTOMER_MASTER.Add1 as cust_add1, 
              TSPL_CUSTOMER_MASTER.Add2 as cust_add2, 
              TSPL_CUSTOMER_MASTER.Add3 cust_add3, 
              case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1, '')= '(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end + Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2, '')<> '(+__)__________' Then ', ' + TSPL_CUSTOMER_MASTER.Phone2 Else '' End as CustPhone, 
              TSPL_CUSTOMER_MASTER.Fax as cust_fax, 
              TSPL_CUSTOMER_MASTER.State as Cust_state, 
              CUSTOMER_STATE_MASTER.STATE_NAME as cust_Statename, 
              TSPL_CUSTOMER_MASTER.Email as cust_Email, 
              TSPL_CUSTOMER_MASTER.WebSite as cust_website, 
              TSPL_CUSTOMER_MASTER.pan as Customer_Pan, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.Ack_No, 
                'NA'
              ) AS Ack_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Ack_Date, 
              TSPL_SD_SHIPMENT_HEAD.DO_Item_Type As TaxableNonTaxable, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX1, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX1
              ) as TaxType1, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt, 
                0.00
              ) As TAX1_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as TAX1Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX2
              ) as TaxType2, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX2, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt, 
                0.00
              ) As TAX2_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as TAX2Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX3
              ) as TaxType3, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX3, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt, 
                0.00
              ) As TAX3_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as TAX3Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX4
              ) as TaxType4, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX4, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt, 
                0.00
              ) As TAX4_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as TAX4Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX5
              ) as TaxType5, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX5, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt, 
                0.00
              ) As TAX5_Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX6
              ) as TaxType6, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX6, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt, 
                0.00
              ) As TAX6_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.Route_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Route_Desc, 
              TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt, 
              TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt, 
              isnull(
                TSPL_SD_SHIPMENT_HEAD.Against_booking_no, 
                ''
              ) as Against_Delivery_Code, 
              Case when TSPL_CUSTOMER_MASTER.Credit_Customer = 'Y' THEN 'CREDIT' else '' end as Credit_Customer, 
              
              IsNull(
                TSPL_SD_SHIPMENT_DETAIL.Booth_Security_Amt, 
                0
              ) Booth_Security_Amt 
            from 
              TSPL_SD_SALE_INVOICE_DETAIL 
              LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE 
              left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No 
              left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
              and TSPL_SD_SHIPMENT_DETAIL.Line_No = TSPL_SD_sale_invoice_DETAIL.Line_No 
              left outer join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No 
              left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
              And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SD_sale_invoice_DETAIL.Unit_code 
              LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
              left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
              and ItemConvReportUOM.Report_UOM = 1 
            
              left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_sale_invoice_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
              and TSPL_SD_sale_invoice_DETAIL.Unit_code = ItemConvinUOM.UOM_Code 
           
                                       left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code 
              left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
              left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location 
              LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code = TSPL_LOCATION_MASTER.State 
              left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State = CUSTOMER_STATE_MASTER.STATE_CODE 
              left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code = customer_city_master.City_Code 
               
              LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SHIPMENT_HEAD.AlternateVehicle 
              left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code = TSPL_SD_sale_invoice_DETAIL.Price_code 
              and TSPL_ITEM_PRICE_MASTER.Location_Code = TSPL_SD_sale_invoice_DETAIL.Location 
              and TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
             
         ) as final
       where 2=2 " + whrcls + " ) AS Main_Final 
      left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.comp_code = Main_Final.comp_code  
  ) Final  " + forCustomerWise + "where " + whr + " " + Group + "

  "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnrouteWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMonthlyBillSummary", "")
                ElseIf rbtnCustomerWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMonthlyBillSummaryCustomerWise", "")
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub btnprintDetail_Click(sender As Object, e As EventArgs) Handles btnprintDetail.Click
        PrintDetail()
    End Sub
    Sub PrintDetail()
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim whrcls As String = ""
            Dim whr As String = ""
            Dim fromdate As Date = txtfDate.Value.AddDays(1)
            Dim todate As Date = txtToDate.Value.AddDays(-1)
            If rbtnSupplydate.IsChecked Then
                If chkExcludeShift.Checked Then
                    whr = " (convert(date,Final.supply_date,103)='" + clsCommon.GetPrintDate(txtfDate.Value) + "'  and Shift_Type='EVENING') OR  (convert(date,Final.supply_date,103)>='" + clsCommon.GetPrintDate(fromdate) + "' AND convert(date,Final.supply_date,103)<='" + clsCommon.GetPrintDate(todate) + "')
                        OR (convert(date,Final.supply_date,103)='" + clsCommon.GetPrintDate(txtToDate.Value) + "'  and Shift_Type='MORNING') "
                Else
                    whr = "  convert(date,Final.supply_date,103)>='" + clsCommon.GetPrintDate(txtfDate.Value) + "' AND convert(date,Final.supply_date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' "
                End If
            ElseIf rbtnDocumentdate.IsChecked Then
                whr += "(convert(date,Final.Shipment_Date,103)>='" + clsCommon.GetPrintDate(txtfDate.Value) + "' AND convert(date,Final.Shipment_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "')"
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                whr += " and Final.Cust_Code in  (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                whr += " and Route_No in (" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")"
            End If
            qry = "select *
from 
  (
    select 
      Main_Final.*, 
      TSPL_COMPANY_MASTER.Logo_Img, 
      1 As CopyType, 
      TSPL_COMPANY_MASTER.GSTReg_No As SellerGST, 
      TSPL_COMPANY_MASTER.Pan_No 
    from 
      (
        select 
          final.*, 
          Item_Desc as Particulars
          
        from 
          (
            Select 
              case when TSPL_BOOKING_MATSER.Is_CashSale = 'Y' then 'CASH' else 'CREDIT' END AS PaymentTerms, 
              TSPL_BOOKING_MATSER.Is_Distributor, 
              TSPL_BOOKING_MATSER.Is_BPL, 
              TSPL_BOOKING_MATSER.Is_CashSale, 
			  tspl_item_master.Short_Description
			  ,
              TSPL_BOOKING_MATSER.Is_DCS, 
              TSPL_BOOKING_MATSER.Booking_Type, 
              TSPL_COMPANY_MASTER.CST_LST, 
             
              TSPL_SD_SHIPMENT_HEAD.ManualVehicle as Manual_VehicleNo, 
              TSPL_SD_SHIPMENT_HEAD.Payment_Terms, 
              TSPL_SD_SHIPMENT_HEAD.ReceiverName, 
              TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt, 
              convert(
                varchar(12), 
                TSPL_SD_SHIPMENT_HEAD.Supply_Date, 
                103
              ) Supply_Date, TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as Base_Amt,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt,
              case when TSPL_SD_SHIPMENT_HEAD.Shift_Type = 'AM' then 'M' else 'E' end as Shift_Type, 
              
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as ITAX1, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_RATE AS ITAX1_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as ITAX1_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as ITAX2, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_RATE AS ITAX2_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as ITAX2_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3 AS ITAX3, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate AS ITAX3_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as ITAX3_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4 AS ITAX4, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_RATE AS ITAX4_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as ITAX4_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as ITAX5, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5_RATE AS ITAX5_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt as ITAX5_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as ITAX6, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6_RATE AS ITAX6_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt as ITAX6_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7 AS ITAX7, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate AS ITAX7_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt as ITAX7_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8 AS ITAX8, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8_RATE AS ITAX8_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt as ITAX8_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9 AS ITAX9, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate AS ITAX9_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt as ITAX9_Amt, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10 AS ITAX10, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10_RATE AS ITAX10_RATE, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt as ITAX10_Amt, 
               
              Zone_Code, 
             
              TSPL_ITEM_UOM_DETAIL.Conversion_Factor As ConversionFactor, 
              TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type, 
              0 as LeakageDeduction_Freshsale, 
              0 as LeakageDeduction, 
              TSPL_LOCATION_MASTER.Location_Desc, 
              TSPL_LOCATION_MASTER.Loc_Short_Name, 
              TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin, 
              (
                case when isnull(TSPL_LOCATION_MASTER.Phone1, '')<> '' then TSPL_LOCATION_MASTER.Phone1 when isnull(TSPL_LOCATION_MASTER.Phone2, '')<> '' then + ', ' + TSPL_LOCATION_MASTER.Phone2 end
              ) as Loc_Phone, 
              TSPL_LOCATION_MASTER.Email as Loc_Eamil, 
              '' as Loc_Website, 
              TSPL_COMPANY_MASTER.ISO_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No, 
              convert(
                varchar(12), 
                TSPL_SD_SALE_INVOICE_HEAD.Document_date, 
                103
              ) as Invoice_Date, 
			   convert(
                varchar(12), 
                TSPL_SD_SHIPMENT_HEAD.Document_date, 
                103
              ) as Shipment_Date,
              customer_city_master.city_name as Cust_City, 
              TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No, 
              CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode, 
              TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No, 
              Tspl_customer_master.gstno as CustGSTNo, 
              TSPL_STATE_MASTER.gst_state_code, 
              tspl_location_master.gstno as LocGstNo, 
              TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo, 
              TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate, 
              TSPL_ITEM_MASTER.HSN_Code, 
              TSPL_SD_SALE_INVOICE_HEAD.Remarks as InvRemarks, 
              TSPL_SD_sale_invoice_DETAIL.Delivery_Code, 
              
             
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Sale_Invoice_No, 
              Case When ISNULL(
                TSPL_SD_SHIPMENT_HEAD.ManualVehicle, 
                ''
              )<> '' Then TSPL_SD_SHIPMENT_HEAD.ManualVehicle WHEN ISNULL(
                TSPL_SD_SHIPMENT_HEAD.AlternateVehicle, 
                ''
              )<> '' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SALE_INVOICE_HEAD.vehicleNo End as vehicleNo, 
              Convert(
                varchar, TSPL_SD_SALE_INVOICE_HEAD.Sale_Invoice_Date, 
                103
              ) as Sale_Invoice_Date, 
              TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount, 
              TSPL_LOCATION_MASTER.Add1 as Loc_ADd1, 
              TSPL_LOCATION_MASTER.Add2 as LOC_ADD2, 
              TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, 
              TSPL_STATE_MASTER.State_Name as LocationState, TSPL_STATE_MASTER.STATE_CODE,TSPL_LOCATION_MASTER.City_Code,
              case when ISNULL(TSPL_LOCATION_MASTER.Phone1, '')= '(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end + Case When ISNULL(TSPL_LOCATION_MASTER.Phone2, '')<> '(+__)__________' Then ', ' + TSPL_LOCATION_MASTER.Phone2 Else '' End as LOCPhone, 
              TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO, 
              TSPL_SD_SALE_INVOICE_HEAD.Document_Code, 
              convert(
                varchar, TSPL_SD_SHIPMENT_HEAD.Document_Date, 
                103
              ) as Document_Date, 
              TSPL_SD_SALE_INVOICE_HEAD.Description, 
              TSPL_SD_SALE_INVOICE_HEAD.Lorry_No, 
              TSPL_ITEM_MASTER.Sku_Seq, 
              CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then TSPL_SD_sale_invoice_DETAIL.Item_Code + ' -Scheme' else TSPL_SD_sale_invoice_DETAIL.Item_Code end as Item_Code, 
              (
                CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then 0 else (
                  TSPL_SD_sale_invoice_DETAIL.Line_No
                ) end
              ) as Line_No, 
              CASE when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' then TSPL_ITEM_MASTER.Item_Desc + ' -Scheme' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc, 
              TSPL_SD_sale_invoice_DETAIL.Crate as QtyCrates, 
              TSPL_SD_sale_invoice_DETAIL.Unit_code, 
              ItemConvReportUOM.UOM_Code, 
              convert(
                Decimal(18, 2), 
                TSPL_SD_sale_invoice_DETAIL.Qty
              ) as Qty_Default, 
              cast(
                (
                  TSPL_SD_sale_invoice_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
                ) as Decimal(18, 2)
              ) as QtyAccToReportUOM, Convert(decimal(18,2),(TSPL_SD_sale_invoice_DETAIL.Amt_Less_Discount/((
                TSPL_SD_sale_invoice_DETAIL.Qty
              *ItemConvinUOM.Conversion_Factor )/ItemConvReportUOM.Conversion_Factor))) as ReportRate,
              '' GrandTotalCrates, 
              TSPL_COMPANY_MASTER.Comp_Code, 
              TSPL_COMPANY_MASTER.Comp_Name, 
              TSPL_COMPANY_MASTER.Add1 as comp_add1, 
              TSPL_COMPANY_MASTER.Fax as comp_Fax, 
              TSPL_COMPANY_MASTER.Email as comp_Email, 
              TSPL_COMPANY_MASTER.Tin_No as comp_tinNo, 
              TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as cust_Code, 
              TSPL_CUSTOMER_MASTER.Customer_Name, 
              TSPL_CUSTOMER_MASTER.Add1 as cust_add1, 
              TSPL_CUSTOMER_MASTER.Add2 as cust_add2, 
              TSPL_CUSTOMER_MASTER.Add3 cust_add3, 
              case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1, '')= '(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end + Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2, '')<> '(+__)__________' Then ', ' + TSPL_CUSTOMER_MASTER.Phone2 Else '' End as CustPhone, 
              TSPL_CUSTOMER_MASTER.Fax as cust_fax, 
              TSPL_CUSTOMER_MASTER.State as Cust_state, 
              CUSTOMER_STATE_MASTER.STATE_NAME as cust_Statename, 
              TSPL_CUSTOMER_MASTER.Email as cust_Email, 
              TSPL_CUSTOMER_MASTER.WebSite as cust_website, 
              TSPL_CUSTOMER_MASTER.pan as Customer_Pan, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.Ack_No, 
                'NA'
              ) AS Ack_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Ack_Date, 
              TSPL_SD_SHIPMENT_HEAD.DO_Item_Type As TaxableNonTaxable, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX1, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX1
              ) as TaxType1, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt, 
                0.00
              ) As TAX1_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as TAX1Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX2
              ) as TaxType2, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX2, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt, 
                0.00
              ) As TAX2_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as TAX2Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX3
              ) as TaxType3, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX3, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt, 
                0.00
              ) As TAX3_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as TAX3Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX4
              ) as TaxType4, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX4, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt, 
                0.00
              ) As TAX4_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate, 
              TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as TAX4Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX5
              ) as TaxType5, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX5, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt, 
                0.00
              ) As TAX5_Amt, 
              (
                select 
                  type 
                from 
                  TSPL_TAX_MASTER 
                where 
                  Tax_Code = TSPL_SD_SALE_INVOICE_HEAD.TAX6
              ) as TaxType6, 
              TSPL_SD_SALE_INVOICE_HEAD.TAX6, 
              IsNull(
                TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt, 
                0.00
              ) As TAX6_Amt, 
              TSPL_SD_SALE_INVOICE_HEAD.Route_No, 
              TSPL_SD_SALE_INVOICE_HEAD.Route_Desc, 
              TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt, 
              TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt, 
              isnull(
                TSPL_SD_SHIPMENT_HEAD.Against_booking_no, 
                ''
              ) as Against_Delivery_Code, 
              Case when TSPL_CUSTOMER_MASTER.Credit_Customer = 'Y' THEN 'CREDIT' else '' end as Credit_Customer, 
              
              IsNull(
                TSPL_SD_SHIPMENT_DETAIL.Booth_Security_Amt, 
                0
              ) Booth_Security_Amt 
            from 
              TSPL_SD_SALE_INVOICE_DETAIL 
              LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE 
              left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No 
              left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
              and TSPL_SD_SHIPMENT_DETAIL.Line_No = TSPL_SD_sale_invoice_DETAIL.Line_No 
              left outer join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No 
              left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
              And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SD_sale_invoice_DETAIL.Unit_code 
              LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
              left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
              and ItemConvReportUOM.Report_UOM = 1 
            
              left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_sale_invoice_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
              and TSPL_SD_sale_invoice_DETAIL.Unit_code = ItemConvinUOM.UOM_Code 
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code 
              left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
              left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location 
              LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code = TSPL_LOCATION_MASTER.State 
              left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State = CUSTOMER_STATE_MASTER.STATE_CODE 
              left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code = customer_city_master.City_Code 
               
              LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SHIPMENT_HEAD.AlternateVehicle 
              left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code = TSPL_SD_sale_invoice_DETAIL.Price_code 
              and TSPL_ITEM_PRICE_MASTER.Location_Code = TSPL_SD_sale_invoice_DETAIL.Location 
              and TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code 
             
         ) as final
       where 2=2  ) AS Main_Final 
      left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.comp_code = Main_Final.comp_code  
  ) Final  left join (SELECT 
    Customer_Code,
    STUFF((
        SELECT DISTINCT ', ' + Route_No
        FROM TSPL_SD_SALE_INVOICE_HEAD AS InnerTable
        WHERE InnerTable.Customer_Code = OuterTable.Customer_Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Routes
FROM 
    TSPL_SD_SALE_INVOICE_HEAD AS OuterTable
GROUP BY 
    Customer_Code)RR on RR.Customer_Code=final.cust_Code where " + whr + "  ORDER BY Supply_Date,Shift_Type "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnrouteWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMonthlyRouteWiseDetail", "")
                ElseIf rbtnCustomerWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMonthlyCustomerWiseDetail", "")
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class