
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmAdvanceRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "AdvanceRegister"
    Dim arrBack As New List(Of String)
    Dim arrVSP As New ArrayList()
    Dim arrFarmer As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            Dim qryAdv As String = " select TSPL_RECEIPT_HEADER.Receipt_No,TSPL_RECEIPT_HEADER.Receipt_Date as Receipt_Date,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name," & _
                                " TSPL_CUSTOMER_MASTER.GSTNO as GSTIN_NO_Cust,TSPL_RECEIPT_HEADER.Delivery_Code_PS,convert(date,coalesce(TSPL_SD_SALES_ORDER_HEAD.Document_Date,DO.Document_Date),103) as Document_Date,TSPL_RECEIPT_HEADER.Receipt_Amount," & _
                                " (CASE WHEN TAX1.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX1_Rate WHEN TAX2.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX2_Rate " & _
                                " WHEN TAX3.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX3_Rate WHEN TAX4.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX4_Rate " & _
                                " WHEN TAX5.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX5_Rate ELSE 0 END) AS SGST_RATE, " & _
                                " (CASE WHEN TAX1.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX1_Amt WHEN TAX2.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX2_Amt " & _
                                " WHEN TAX3.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX3_Amt WHEN TAX4.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX4_Amt " & _
                                " WHEN TAX5.Type='SGST' THEN TSPL_RECEIPT_HEADER.TAX5_Amt ELSE 0 END) AS SGST_AMT, " & _
                                " (CASE WHEN TAX1.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX1_Rate WHEN TAX2.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX2_Rate " & _
                                " WHEN TAX3.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX3_Rate WHEN TAX4.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX4_Rate " & _
                                " WHEN TAX5.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX5_Rate ELSE 0 END) AS CGST_RATE, " & _
                                " (CASE WHEN TAX1.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX1_Amt WHEN TAX2.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX2_Amt " & _
                                " WHEN TAX3.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX3_Amt WHEN TAX4.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX4_Amt " & _
                                " WHEN TAX5.Type='CGST' THEN TSPL_RECEIPT_HEADER.TAX5_Amt ELSE 0 END) AS CGST_AMT, " & _
                                " (CASE WHEN TAX1.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX1_Rate WHEN TAX2.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX2_Rate " & _
                                " WHEN TAX3.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX3_Rate WHEN TAX4.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX4_Rate " & _
                                " WHEN TAX5.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX5_Rate ELSE 0 END) AS IGST_RATE, " & _
                                " (CASE WHEN TAX1.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX1_Amt WHEN TAX2.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX2_Amt " & _
                                " WHEN TAX3.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX3_Amt WHEN TAX4.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX4_Amt " & _
                                " WHEN TAX5.Type='IGST' THEN TSPL_RECEIPT_HEADER.TAX5_Amt ELSE 0 END) AS IGST_AMT, " & _
                                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX1_Rate WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX2_Rate " & _
                                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX3_Rate WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX4_Rate " & _
                                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX5_Rate ELSE 0 END) AS Other_Tax_RATE, " & _
                                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX1_Amt WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX2_Amt " & _
                                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX3_Amt WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX4_Amt " & _
                                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_RECEIPT_HEADER.TAX5_Amt ELSE 0 END) AS Other_Tax_AMT, " & _
                                " (coalesce(TSPL_RECEIPT_HEADER.TAX1_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX2_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX3_Amt,0)" & _
                                " +coalesce(TSPL_RECEIPT_HEADER.TAX4_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX5_Amt,0)) as Total_Tax_Amt,coalesce(TSPL_LOCATION_MASTER.City_Code,coalesce(Loc_DO.City_Code,TSPL_CITY_MASTER.City_Name)) as [Place of Supply] " & _
                                " from TSPL_RECEIPT_HEADER " & _
                                " left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                                " left join TSPL_CITY_MASTER on TSPL_CUSTOMER_MASTER.CITY_CODE=TSPL_CITY_MASTER.City_Code " & _
                                " left join TSPL_SD_SALES_ORDER_HEAD on TSPL_RECEIPT_HEADER.Delivery_Code_PS=TSPL_SD_SALES_ORDER_HEAD.Document_Code " & _
                                " left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE DO on TSPL_RECEIPT_HEADER.Delivery_Code_PS=DO.Document_Code " & _
                                " left join TSPL_LOCATION_MASTER on TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                                " left join TSPL_LOCATION_MASTER Loc_DO on DO.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAX1 ON TSPL_RECEIPT_HEADER.TAX1=TAX1.Tax_Code" & _
                                " LEFT JOIN TSPL_TAX_MASTER TAX2 ON TSPL_RECEIPT_HEADER.TAX2=TAX2.Tax_Code " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAX3 ON TSPL_RECEIPT_HEADER.TAX3=TAX3.Tax_Code " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAX4 ON TSPL_RECEIPT_HEADER.TAX4=TAX4.Tax_Code " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAX5 ON TSPL_RECEIPT_HEADER.TAX5=TAX5.Tax_Code " & _
                                " where Receipt_Type in ('P','F')  "
            '' tax>0 validation was removed by Panch Raj on telephonic conversation with ranjana mam on 09-11-2017
            '" and ((coalesce(TSPL_RECEIPT_HEADER.TAX1_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX2_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX3_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX4_Amt,0)+coalesce(TSPL_RECEIPT_HEADER.TAX5_Amt,0)))>0"
            If chkReceipt.Checked Then
                qryAdv = qryAdv & " and cast(TSPL_RECEIPT_HEADER.Receipt_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"           
            End If
            If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                qryAdv = qryAdv & " and TSPL_RECEIPT_HEADER.Cust_Code in (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & ")"
            End If
            Dim InvoiceQry1 As String = ""
            InvoiceQry1 = "select Receipt.Receipt_No,SD.Document_Code as Invoice_No,convert(date,SD.Document_Date,103) as Document_Date,SD.Customer_Code," & _
                " SD.SaleOrderNo as Mapping_Doc_No,SD.Amount_Less_Discount,(CASE WHEN TAX1.Type='SGST' THEN SD.TAX1_Rate WHEN TAX2.Type='SGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='SGST' THEN SD.TAX3_Rate " & _
                " WHEN TAX4.Type='SGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='SGST' THEN SD.TAX5_Rate ELSE 0 END) AS SGST_RATE, " & _
                " (CASE WHEN TAX1.Type='SGST' THEN SD.TAX1_Amt WHEN TAX2.Type='SGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='SGST' THEN SD.TAX3_Amt " & _
                " WHEN TAX4.Type='SGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='SGST' THEN SD.TAX5_Amt ELSE 0 END) AS SGST_AMT, " & _
                " (CASE WHEN TAX1.Type='CGST' THEN SD.TAX1_Rate WHEN TAX2.Type='CGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='CGST' THEN SD.TAX3_Rate " & _
                " WHEN TAX4.Type='CGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='CGST' THEN SD.TAX5_Rate ELSE 0 END) AS CGST_RATE," & _
                " (CASE WHEN TAX1.Type='CGST' THEN SD.TAX1_Amt WHEN TAX2.Type='CGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='CGST' THEN SD.TAX3_Amt " & _
                " WHEN TAX4.Type='CGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='CGST' THEN SD.TAX5_Amt ELSE 0 END) AS CGST_AMT, " & _
                " (CASE WHEN TAX1.Type='IGST' THEN SD.TAX1_Rate WHEN TAX2.Type='IGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='IGST' THEN SD.TAX3_Rate " & _
                " WHEN TAX4.Type='IGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='IGST' THEN SD.TAX5_Rate ELSE 0 END) AS IGST_RATE, " & _
                " (CASE WHEN TAX1.Type='IGST' THEN SD.TAX1_Amt WHEN TAX2.Type='IGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='IGST' THEN SD.TAX3_Amt " & _
                " WHEN TAX4.Type='IGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='IGST' THEN SD.TAX5_Amt ELSE 0 END) AS IGST_AMT," & _
                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX1_Rate WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX2_Rate " & _
                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX3_Rate WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX4_Rate " & _
                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX5_Rate ELSE 0 END) AS Other_Tax_RATE, " & _
                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX1_Amt WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX2_Amt " & _
                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX3_Amt WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX4_Amt " & _
                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX5_Amt ELSE 0 END) AS Other_Tax_AMT, " & _
                " (coalesce(SD.TAX1_Amt,0)+coalesce(SD.TAX2_Amt,0)+coalesce(SD.TAX3_Amt,0) +coalesce(SD.TAX4_Amt,0)+coalesce(SD.TAX5_Amt,0)) as Total_Tax_Amt " & _
                " from (select Receipt_No,Delivery_Code_PS from TSPL_RECEIPT_HEADER where len(coalesce(TSPL_RECEIPT_HEADER.Delivery_Code_PS,''))>0) as Receipt " & _
                " left join ( " & _
                " select TSPL_SD_SALES_ORDER_HEAD.Document_Code as SaleOrderNo,TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date," & _
                " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.TAX1,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate,TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.TAX1_Amt," & _
                " TSPL_SD_SALE_INVOICE_HEAD.TAX2,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate,TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.TAX2_Amt," & _
                " TSPL_SD_SALE_INVOICE_HEAD.TAX3,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.TAX3_Amt," & _
                " TSPL_SD_SALE_INVOICE_HEAD.TAX4,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.TAX4_Amt, " & _
                " TSPL_SD_SALE_INVOICE_HEAD.TAX5,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.TAX5_Amt from TSPL_SD_SALE_INVOICE_HEAD " & _
                " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                " left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " & _
                " left join TSPL_SD_SALES_ORDER_HEAD on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Against_Sales_Order=TSPL_SD_SALES_ORDER_HEAD.Document_Code " & _
                " LEFT join TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.SODO_No and TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SO_ADVANCE_ADJUSTMENT_KNOCKOFF.SI_No " & _
                " ) SD ON Receipt.Delivery_Code_PS=SD.SaleOrderNo or  Receipt.Delivery_Code_PS=SD.Delivery_Code_PS " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX1 ON SD.TAX1=TAX1.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX2 ON SD.TAX2=TAX2.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX3 ON SD.TAX3=TAX3.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX4 ON SD.TAX4=TAX4.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX5 ON SD.TAX5=TAX5.Tax_Code where 2=2 "

            If chkInvoice.Checked Then
                InvoiceQry1 = InvoiceQry1 & " and cast(SD.Document_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
            End If
            If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                InvoiceQry1 = InvoiceQry1 & " and SD.Customer_Code in (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & ")"
            End If
            Dim InvoiceQry2 As String = ""
            InvoiceQry2 = " select TSPL_Receipt_InvoiceMapping_Head.Receipt_No,SD.Document_Code as Invoice_No," & _
                          " convert(date,SD.Document_Date,103) as Document_Date,SD.Customer_Code,SD.Document_Code AS Mapping_Doc_No,SD.Amount_Less_Discount, " & _
                          " (CASE WHEN TAX1.Type='SGST' THEN SD.TAX1_Rate WHEN TAX2.Type='SGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='SGST' THEN SD.TAX3_Rate " & _
                          " WHEN TAX4.Type='SGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='SGST' THEN SD.TAX5_Rate ELSE 0 END) AS SGST_RATE, " & _
                          " (CASE WHEN TAX1.Type='SGST' THEN SD.TAX1_Amt WHEN TAX2.Type='SGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='SGST' THEN SD.TAX3_Amt " & _
                          " WHEN TAX4.Type='SGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='SGST' THEN SD.TAX5_Amt ELSE 0 END) AS SGST_AMT, " & _
                          " (CASE WHEN TAX1.Type='CGST' THEN SD.TAX1_Rate WHEN TAX2.Type='CGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='CGST' THEN SD.TAX3_Rate " & _
                          " WHEN TAX4.Type='CGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='CGST' THEN SD.TAX5_Rate ELSE 0 END) AS CGST_RATE," & _
                          " (CASE WHEN TAX1.Type='CGST' THEN SD.TAX1_Amt WHEN TAX2.Type='CGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='CGST' THEN SD.TAX3_Amt " & _
                          " WHEN TAX4.Type='CGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='CGST' THEN SD.TAX5_Amt ELSE 0 END) AS CGST_AMT, " & _
                          " (CASE WHEN TAX1.Type='IGST' THEN SD.TAX1_Rate WHEN TAX2.Type='IGST' THEN SD.TAX2_Rate  WHEN TAX3.Type='IGST' THEN SD.TAX3_Rate " & _
                          " WHEN TAX4.Type='IGST' THEN SD.TAX4_Rate  WHEN TAX5.Type='IGST' THEN SD.TAX5_Rate ELSE 0 END) AS IGST_RATE," & _
                          " (CASE WHEN TAX1.Type='IGST' THEN SD.TAX1_Amt WHEN TAX2.Type='IGST' THEN SD.TAX2_Amt  WHEN TAX3.Type='IGST' THEN SD.TAX3_Amt " & _
                          " WHEN TAX4.Type='IGST' THEN SD.TAX4_Amt  WHEN TAX5.Type='IGST' THEN SD.TAX5_Amt ELSE 0 END) AS IGST_AMT," & _
                          " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX1_Rate WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX2_Rate " & _
                          " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX3_Rate WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX4_Rate " & _
                          " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX5_Rate ELSE 0 END) AS Other_Tax_RATE, " & _
                          " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX1_Amt WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX2_Amt " & _
                          " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX3_Amt WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX4_Amt " & _
                          " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN SD.TAX5_Amt ELSE 0 END) AS Other_Tax_AMT, " & _
                          " (coalesce(SD.TAX1_Amt,0)+coalesce(SD.TAX2_Amt,0)+coalesce(SD.TAX3_Amt,0) +coalesce(SD.TAX4_Amt,0)+coalesce(SD.TAX5_Amt,0)) as Total_Tax_Amt from TSPL_Receipt_InvoiceMapping_Detail " & _
                          " inner join TSPL_Receipt_InvoiceMapping_Head on TSPL_Receipt_InvoiceMapping_Detail.Doc_CODE=TSPL_Receipt_InvoiceMapping_Head.Doc_Code " & _
                          " left join TSPL_SD_SALE_INVOICE_HEAD SD on SD.Document_Code=TSPL_Receipt_InvoiceMapping_Detail.InvoiceNo " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAX1 ON SD.TAX1=TAX1.Tax_Code " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAX2 ON SD.TAX2=TAX2.Tax_Code " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAX3 ON SD.TAX3=TAX3.Tax_Code " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAX4 ON SD.TAX4=TAX4.Tax_Code " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAX5 ON SD.TAX5=TAX5.Tax_Code where 2=2 "

            If chkInvoice.Checked Then
                InvoiceQry2 = InvoiceQry2 & " and cast(SD.Document_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
            End If
            If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                InvoiceQry2 = InvoiceQry2 & " and SD.Customer_Code in (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & ")"
            End If
            Dim SDFinal As String = " SELECT Final.Invoice_No,Final.Receipt_No,max(Final.Document_Date) as Document_Date,MAX(Final.Customer_Code) as Customer_Code,sum(Final.Amount_Less_Discount) as Amount_Less_Discount," & _
                                    " MAX(Final.SGST_RATE) AS SGST_RATE,SUM(Final.SGST_AMT) AS SGST_AMT,MAX(Final.CGST_RATE) as CGST_RATE,sum(Final.CGST_AMT) as CGST_AMT," & _
                                    " MAX(Final.IGST_RATE) as IGST_RATE,sum(Final.IGST_AMT) as IGST_AMT,MAX(Final.Other_Tax_Rate) as Other_Tax_Rate,sum(Final.Other_Tax_AMT) as Other_Tax_AMT," & _
                                    " sum(Final.Total_Tax_Amt) as Total_Tax_Amt from ( " & InvoiceQry1 & " union all " & InvoiceQry2 & " ) as Final group by Final.Invoice_No,Final.Receipt_No"
            Dim qry As String
            qry = " select view_adv_receipt.Receipt_No,convert(varchar,view_adv_receipt.Receipt_Date,103) as Receipt_Date,view_adv_receipt.Cust_Code,view_adv_receipt.Customer_Name,view_adv_receipt.GSTIN_No_Cust," & _
                  " view_adv_receipt.Delivery_Code_PS,convert(varchar,view_adv_receipt.Document_Date) as Document_Date,view_adv_receipt.Receipt_Amount," & _
                  " (view_adv_receipt.Receipt_Amount-view_adv_receipt.Total_Tax_Amt-sum(view_sd_adv_reg.Amount_Less_Discount) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No)+view_sd_adv_reg.Amount_Less_Discount) as Taxable_Advance," & _
                  " view_adv_receipt.SGST_RATE,(coalesce(view_adv_receipt.SGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.SGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.SGST_AMT,0)) AS SGST_AMT, " & _
                  " view_adv_receipt.CGST_RATE,(coalesce(view_adv_receipt.CGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.CGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.CGST_AMT,0)) AS CGST_AMT,view_adv_receipt.IGST_RATE,(coalesce(view_adv_receipt.IGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.IGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.IGST_AMT,0)) as IGST_AMT,view_adv_receipt.Other_Tax_rate,(coalesce(view_adv_receipt.Other_Tax_Amt,0)-COALESCE(sum(view_sd_adv_reg.Other_Tax_Amt) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.Other_Tax_Amt,0)) as Other_Tax_Amt, " & _
                  " (view_adv_receipt.Total_Tax_Amt-COALESCE(sum(view_sd_adv_reg.Total_Tax_Amt) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+view_sd_adv_reg.Total_Tax_Amt) AS Total_Tax_Amt, " & _
                  " view_sd_adv_reg.Invoice_No,convert(varchar,view_sd_adv_reg.Document_Date) as Document_Invoice_Date,view_sd_adv_reg.Amount_Less_Discount as Invoice_Adjusted_Amt," & _
                  " view_sd_adv_reg.SGST_RATE AS INV_SGST_RATE,view_sd_adv_reg.SGST_AMT AS INV_SGST_AMT,view_sd_adv_reg.CGST_RATE AS INV_CGST_RATE,view_sd_adv_reg.CGST_AMT AS INV_CGST_AMT," & _
                  " view_sd_adv_reg.IGST_RATE AS INV_IGST_RATE,view_sd_adv_reg.IGST_AMT AS INV_IGST_AMT,view_sd_adv_reg.Other_Tax_Rate as Inv_Other_Tax_Rate, " & _
                  " view_sd_adv_reg.Other_Tax_Amt as Inv_Other_Tax_Amt,view_sd_adv_reg.Total_Tax_Amt as INV_Total_Tax_Amt, " & _
                  " ((view_adv_receipt.Receipt_Amount-view_adv_receipt.Total_Tax_Amt)-sum(view_sd_adv_reg.Amount_Less_Discount) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No)) as Balance," & _
                  " ((coalesce(view_adv_receipt.SGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.SGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.SGST_AMT,0))-view_sd_adv_reg.SGST_AMT) as Diff_SGST_AMT,((coalesce(view_adv_receipt.CGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.CGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.CGST_AMT,0))-view_sd_adv_reg.CGST_AMT) as Diff_CGST_AMT," & _
                  " ((coalesce(view_adv_receipt.IGST_AMT,0)-COALESCE(sum(view_sd_adv_reg.IGST_AMT) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.IGST_AMT,0))-view_sd_adv_reg.IGST_AMT) as Diff_IGST_AMT,((coalesce(view_adv_receipt.Other_Tax_Amt,0)-COALESCE(sum(view_sd_adv_reg.Other_Tax_Amt) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.Other_Tax_Amt,0))-view_sd_adv_reg.Other_Tax_Amt) as Diff_Other_Tax_Amt," & _
                  " ((coalesce(view_adv_receipt.Total_Tax_Amt,0)-COALESCE(sum(view_sd_adv_reg.Total_Tax_Amt) over (Partition by view_adv_receipt.Receipt_No order by view_sd_adv_reg.Invoice_No),0)+coalesce(view_sd_adv_reg.Total_Tax_Amt,0))-view_sd_adv_reg.Total_Tax_Amt) as Diff_Total_Tax_Amt,view_adv_receipt.[Place of Supply] " & _
                  " from (" & qryAdv & ") view_adv_receipt " & _
                  " left join (" & SDFinal & ") view_sd_adv_reg on view_adv_receipt.Receipt_No=view_sd_adv_reg.Receipt_No where 2=2 "

            If chkReceipt.Checked Then
                qry = qry & " and cast(view_adv_receipt.Receipt_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
            End If
            If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                qry = qry & " and view_adv_receipt.Cust_Code in (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & ")"
            End If
            If chkInvoice.Checked Then
                qry = qry & " and cast(view_sd_adv_reg.Document_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
            End If
           

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.DataSource = dt
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.BestFitColumns()
            gv3.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2

            gv3.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub
    Sub SetGridLayout()
        
        gv3.Columns("Receipt_No").Width = 100
        gv3.Columns("Receipt_No").HeaderText = "Receipt No"

        gv3.Columns("Receipt_Date").Width = 100
        gv3.Columns("Receipt_Date").HeaderText = "Receipt Date"

        gv3.Columns("Cust_Code").Width = 100
        gv3.Columns("Cust_Code").HeaderText = "Customer CODE"

        gv3.Columns("Customer_Name").Width = 100
        gv3.Columns("Customer_Name").HeaderText = "Customer Name"

        gv3.Columns("GSTIN_NO_Cust").Width = 100
        gv3.Columns("GSTIN_NO_Cust").HeaderText = "GSTIN No"

        gv3.Columns("Delivery_Code_PS").Width = 100
        gv3.Columns("Delivery_Code_PS").HeaderText = "Delivery Code/Invoice"

        gv3.Columns("Document_Date").Width = 100
        gv3.Columns("Document_Date").HeaderText = "Document Date"

        gv3.Columns("Receipt_Amount").Width = 100
        gv3.Columns("Receipt_Amount").HeaderText = "Advance Amount"

        gv3.Columns("Taxable_Advance").Width = 100
        gv3.Columns("Taxable_Advance").HeaderText = "Taxable Advance"

        gv3.Columns("SGST_RATE").Width = 100
        gv3.Columns("SGST_RATE").HeaderText = "SGST Rate"

        gv3.Columns("SGST_RATE").Width = 100
        gv3.Columns("SGST_RATE").HeaderText = "SGST Rate"

        gv3.Columns("SGST_AMT").Width = 100
        gv3.Columns("SGST_AMT").HeaderText = "SGST Amt"

        gv3.Columns("CGST_RATE").Width = 100
        gv3.Columns("CGST_RATE").HeaderText = "CGST Rate"

        gv3.Columns("CGST_AMT").Width = 100
        gv3.Columns("CGST_AMT").HeaderText = "CGST Amt"

        gv3.Columns("IGST_RATE").Width = 100
        gv3.Columns("IGST_RATE").HeaderText = "IGST Rate"

        gv3.Columns("IGST_AMT").Width = 100
        gv3.Columns("IGST_AMT").HeaderText = "IGST Amt"

        gv3.Columns("Other_Tax_RATE").Width = 100
        gv3.Columns("Other_Tax_RATE").HeaderText = "Other Tax Rate"

        gv3.Columns("Other_Tax_AMT").Width = 100
        gv3.Columns("Other_Tax_AMT").HeaderText = "Other Tax Amt"

        gv3.Columns("Invoice_No").Width = 100
        gv3.Columns("Invoice_No").HeaderText = "Invoice No"

        gv3.Columns("Document_Invoice_Date").Width = 100
        gv3.Columns("Document_Invoice_Date").HeaderText = "Document Invoice Date"

        gv3.Columns("Taxable_Advance").Width = 100
        gv3.Columns("Taxable_Advance").HeaderText = "Invoice Adjusted Amount"

        gv3.Columns("INV_SGST_RATE").Width = 100
        gv3.Columns("INV_SGST_RATE").HeaderText = "Invoice SGST Rate"

        gv3.Columns("INV_SGST_AMT").Width = 100
        gv3.Columns("INV_SGST_AMT").HeaderText = "Invoice SGST Amount"

        gv3.Columns("INV_CGST_RATE").Width = 100
        gv3.Columns("INV_CGST_RATE").HeaderText = "Invoice CGST Rate"

        gv3.Columns("INV_CGST_AMT").Width = 100
        gv3.Columns("INV_CGST_AMT").HeaderText = "Invoice CGST Amount"

        gv3.Columns("INV_IGST_RATE").Width = 100
        gv3.Columns("INV_IGST_RATE").HeaderText = "Invoice IGST Rate"

        gv3.Columns("INV_IGST_AMT").Width = 100
        gv3.Columns("INV_IGST_AMT").HeaderText = "Invoice IGST Amount"

        gv3.Columns("Inv_Other_Tax_Rate").Width = 100
        gv3.Columns("Inv_Other_Tax_Rate").HeaderText = "Invoice Other Tax Rate"

        gv3.Columns("Inv_Other_Tax_Amt").Width = 100
        gv3.Columns("Inv_Other_Tax_Amt").HeaderText = "Invoice Other Tax Amount"

        gv3.Columns("INV_Total_Tax_Amt").Width = 100
        gv3.Columns("INV_Total_Tax_Amt").HeaderText = "Invoice Total Tax Amount"

        gv3.Columns("Balance").Width = 100
        gv3.Columns("Balance").HeaderText = "Balance"

        gv3.Columns("Diff_SGST_AMT").Width = 100
        gv3.Columns("Diff_SGST_AMT").HeaderText = "Diff SGST AMT"

        gv3.Columns("Diff_CGST_AMT").Width = 100
        gv3.Columns("Diff_CGST_AMT").HeaderText = "Diff CGST AMT"

        gv3.Columns("Diff_IGST_AMT").Width = 100
        gv3.Columns("Diff_IGST_AMT").HeaderText = "Diff IGST AMT"

        gv3.Columns("Diff_Other_Tax_Amt").Width = 100
        gv3.Columns("Diff_Other_Tax_Amt").HeaderText = "Diff Other Tax AMT"
        gv3.Columns("Diff_Total_Tax_Amt").Width = 100
        gv3.Columns("Diff_Total_Tax_Amt").HeaderText = "Diff Total Tax AMT"

        gv3.Columns("Invoice_Adjusted_Amt").Width = 100
        gv3.Columns("Invoice_Adjusted_Amt").HeaderText = "Invoice Adjusted Amount"

        gv3.Columns("Place of Supply").Width = 100
        gv3.Columns("Place of Supply").HeaderText = "Place of Supply"

        gv3.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For Each gcol As GridViewColumn In gv3.Columns
            If gcol.Name.Contains("Amt") OrElse gcol.Name.Contains("AMT") OrElse gcol.Name.Contains("Amount") OrElse gcol.Name.Contains("Taxable_Advance") OrElse gcol.Name.Contains("Balance") Then
                Dim item1 As New GridViewSummaryItem(gcol.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            End If
        Next
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem("Debit", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'Dim item2 As New GridViewSummaryItem("Credit", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)

        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub frmAdvanceRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        'dtpInvFromDate.Value = txtFromDate.Value
        'dtpInvToDate.Value = txtToDate.Value

        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAdvanceRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        btnGenrate.Enabled = True
        'rbtnSummary.IsChecked = True
        gv3.DataSource = Nothing

        'txtLocation.arrValueMember = Nothing

        TxtCustCode.arrValueMember = Nothing
        'txtMultDistr.arrValueMember = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmAdvanceRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Customer Advance Register")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Customer Advance Register", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Customer Advance Register", gv3, arr, "Customer Advance Register", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Customer Advance Register")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Customer Advance Register", gv3, arr, "Customer Advance Register", False)
    End Sub

#Region "grid operations"


#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    ' ============= Addded by Preeti gupta============
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count <= 0 Then
                gv3.Focus()
                clsCommon.MyMessageBoxShow("Data not found.")
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.rptCustomerAdvanceReg & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")
            If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
            End If
        
            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer Advance Register", gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    
    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtCustCode._My_Click
        Dim qry As String = " select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER where 2=2 "        
        TxtCustCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivCustMulSel", qry, "Code", "Name", TxtCustCode.arrValueMember, TxtCustCode.arrDispalyMember)
    End Sub

    
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        TxtCustCode.arrValueMember = Nothing
    End Sub

    Private Sub chkReceipt_CheckedChanged(sender As Object, e As EventArgs) Handles chkReceipt.CheckedChanged
        If chkReceipt.Checked Then
            chkInvoice.Checked = False
        End If
    End Sub

    Private Sub chkInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoice.CheckedChanged
        If chkInvoice.Checked Then
            chkReceipt.Checked = False
        End If
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
