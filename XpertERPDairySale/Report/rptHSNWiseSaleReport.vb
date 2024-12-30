
Imports common
Imports System.IO

Public Class rptHSNWiseSaleReport
    Inherits FrmMainTranScreen
#Region "Variables"
#End Region
    Private Sub rptHSNWiseSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtTaxCode__My_Click(sender As Object, e As EventArgs) Handles txtTaxCode._My_Click
        Try
            Dim qry As String = "select Tax_Code as [Tax Code],Tax_Code_Desc as [Tax Name] from TSPL_TAX_MASTER  "
            txtTaxCode.arrValueMember = clsCommon.ShowMultipleSelectForm("HSNTax", qry, "Tax Code", "Tax Name", txtTaxCode.arrValueMember, txtTaxCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select  Item_Code as [Item Code] ,Item_Desc as  [Item Desc] ,Short_Description as [Short Description] from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("HSNItem", qry, "Item Code", "Item Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtTaxCode.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try

            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim BaseQuery As String = ""
            Dim FinalQuery As String = ""
            If txtTaxCode.arrValueMember IsNot Nothing Then
                whrcls = " and tax in (" & clsCommon.GetMulcallString(txtTaxCode.arrValueMember) & ") "
            End If

            If txtItem.arrValueMember IsNot Nothing Then
                whrcls = " and Item_Code in (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If
            qry = "---------------- VCGL---------------------------- 
            select TSPL_VCGL_Detail.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Detail.Row_Type ='Customer' then TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount else 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  where TSPL_VCGL_Detail.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''     and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_VCGL_Head.Status =1  
            union all 
            select  TSPL_VCGL_Head.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Head.Document_Type  ='C' then TSPL_VCGL_Head.Tot_Cr_Amount-TSPL_VCGL_Head.Tot_Dr_Amount else 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, '' as Tax, 0 as Tax_Amt ,0 as Tax_Rate from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  
            where TSPL_VCGL_Head.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''     and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_VCGL_Head.Status =1  
            Union all 
            ---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- 
            select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM, (TSPL_SD_SALE_INVOICE_DETAIL.Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
            left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_INVOICE_HEAD.Status =1  
            union all 
            select TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,TSPL_CANSALE_INVOICE_detail.Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,TSPL_CANSALE_INVOICE_detail.Item_Net_Amt ,TSPL_CANSALE_INVOICE_detail.Total_Tax_Amt,'' as Tax,  0 as Tax_Amt ,0 as Tax_Rate  from TSPL_CANSALE_INVOICE_detail left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            select TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount ,TSPL_INVOICE_DETAIL_BULKSALE.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_INVOICE_DETAIL_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No 
            where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all  
            ---------------- Sale RETURN ,CAN SALE RETURN----------------------- 
            select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  
            where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 
            union all 
            select TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_SALE_RETURN_DETAIL_BULKSALE left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice'  and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            ---------------- Scrap Sale Return---------------------- 
            select TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty AS Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,TSPL_SCRAPSALE_DETAIL_RETURN.TotalAmt as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_SCRAPSALE_DETAIL_RETURN left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1  
            union all  
            ---------------- Scrap Sale ----------------- 
            select TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty AS Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,TSPL_SCRAPINVOICE_DETAIL.TotalAmt as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_SCRAPINVOICE_DETAIL left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPINVOICE_HEAD.ispost =1  
            union all 
            ---------------- mcc Material Sale RETURN-------------------------------- 
            select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_SD_SALE_RETURN_DETAIL  left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' 
            union all 
            ---------------- Security Receipt--------------------------
            select TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0)) as Total_Tax_Amt,'' as Tax,  0 as Tax_Amt ,0 as Tax_Rate from TSPL_Customer_Invoice_Detail left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Dec/2024',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'27/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 "
            qry += " " & Environment.NewLine & " Union all " & Environment.NewLine & ""
            For ii As Integer = 1 To 5
                If ii > 1 Then
                    BaseQuery += " Union all " & Environment.NewLine & ""
                End If

                BaseQuery += "---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- 
            select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt,   isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & ",'') AS Tax,  TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Amt as TAX_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Rate as TAX_Rate    from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
            left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_INVOICE_HEAD.Status =1  and TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Amt > 0
            union all 
            select TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,0 as Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,0 as Item_Net_Amt,TSPL_CANSALE_INVOICE_detail.Total_Tax_Amt, isnull(TSPL_CANSALE_INVOICE_detail.TAX" & ii & ",'') AS Tax,  TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Amt as TAX_Amt,TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Rate as TAX_Rate  from TSPL_CANSALE_INVOICE_detail left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Amt>0
            union all 
            select TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,0 as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_INVOICE_DETAIL_BULKSALE.Total_Tax_Amt, isnull(TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & ",'') AS Tax,  TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Amt as TAX_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Rate as TAX_Rate from TSPL_INVOICE_DETAIL_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No 
            where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Amt>0
            union all  
            ---------------- Sale RETURN ,CAN SALE RETURN----------------------- 
            select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & ",'') AS Tax,  TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt as Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Rate as Tax_Rate from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  
            where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 and TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt>0
            union all 
            select TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,0 as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Total_Tax_Amt, isnull(TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & ",'') AS Tax,  TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Amt as TAX_Amt,TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Rate as TAX_Rate  from TSPL_SALE_RETURN_DETAIL_BULKSALE left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' and TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Amt>0 and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            ---------------- Scrap Sale Return---------------------- 
            select TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,0 as Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & ",'') as Tax, TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Amt as Tax_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Rate as TAX_Rate from TSPL_SCRAPSALE_DETAIL_RETURN left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1   and TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Amt>0
            union all  
            ---------------- Scrap Sale ----------------- 
            select TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,0 as Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, isnull(TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & ",'') as Tax, TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Amt as TAX_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Rate as TAX_Rate from TSPL_SCRAPINVOICE_DETAIL left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPINVOICE_HEAD.ispost =1  and TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Amt>0
            union all 
            ---------------- mcc Material Sale RETURN-------------------------------- 
            select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,  isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & ",'') as Tax,TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt as Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Rate as TAX_Rate from TSPL_SD_SALE_RETURN_DETAIL  left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt > 0
            union all 
            ---------------- Security Receipt--------------------------
            select TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,0 as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0)) as Total_Tax_Amt,TSPL_Customer_Invoice_Detail.TAX" & ii & " as Tax,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  isnull(TSPL_Customer_Invoice_Detail.TAX" & ii & "_Amt,0) as Tax_Amt ,TSPL_Customer_Invoice_Detail.TAX" & ii & "_Rate as Tax_Rate from TSPL_Customer_Invoice_Detail left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0   and TSPL_Customer_Invoice_Detail.TAX" & ii & "_Amt >0"
            Next

            BaseQuery = " select Document_No,xx.Item_Code,UOM,(Qty)Qty, (Item_Net_Amt)Item_Net_Amt,Tax,Tax_Rate,Tax_Amt from ( " & qry & " " & BaseQuery & " )xx  where 2= 2 " & whrcls & " ) xxx  left outer join TSPL_TAX_MASTER ON  TSPL_TAX_MASTER.Tax_Code = XXX.TAX "
            Dim TaxDesc As String = ""
            qry = ""
            Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(" select Tax,max(Tax_Code_Desc)Tax_Code_Desc from ( " & BaseQuery & " where Tax<> '' group by Tax")
            For Each dr As DataRow In dtTax.Rows
                TaxDesc += "[" & dr("Tax_Code_Desc") & " Amount] ,"
                qry += " ,sum(case when xxx.Tax ='" & dr("Tax") & "' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as [" & dr("Tax_Code_Desc") & " Amount] "
            Next
            qry = " select TSPL_ITEM_MASTER.HSN_Code,FINAL.Item_Code,TSPL_ITEM_MASTER.Short_Description,I.UOM_Code + '-' + I.UOM_Description AS UOM, isnull((Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Total_Qty,Item_Net_Amt,convert(decimal(18,2),((Total_Tax_Amt * 100)/(Item_Net_Amt - Total_Tax_Amt) )) as Tax_Rate,(Item_Net_Amt - Total_Tax_Amt) AS Taxable_Amt, " & TaxDesc & " Total_Tax_Amt from (  select xxx.Item_Code,UOM,SUM(Qty)Qty, sum(Item_Net_Amt)Item_Net_Amt, sum(Tax_Amt)Total_Tax_Amt " & qry & " from ( "

            BaseQuery = " " & qry & " " & BaseQuery & " GROUP BY xxx.Item_Code,UOM ) FINAL left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = FINAL.Item_Code  LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = FINAL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = FINAL.UOM	LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON FINAL.Item_Code = I.item_code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)


            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.ShowGroupPanel = False
        gv1.Columns("HSN_Code").HeaderText = "HSN"
        gv1.Columns("Short_Description").HeaderText = "Description"
        gv1.Columns("UOM").HeaderText = "UQC"
        gv1.Columns("Total_Qty").HeaderText = "Total Quantity"
        gv1.Columns("Item_Net_Amt").FormatString = "{0:n2}"
        gv1.Columns("Item_Net_Amt").HeaderText = "Total Value"
        gv1.Columns("Tax_Rate").HeaderText = "Tax Rate"
        gv1.Columns("Taxable_Amt").HeaderText = "Taxable Amount"
        gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amount"
        gv1.Columns("Item_Code").HeaderText = "Item Code"
        gv1.Columns("Item_Code").IsVisible = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 4 To gv1.Columns.Count - 1
            If ii = 6 Then
            Else
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                If txtTaxCode.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Tax Code : " & clsCommon.GetMulcallString(txtTaxCode.arrValueMember) & "   Tax Name :" & clsCommon.GetMulcallString(txtTaxCode.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item Code : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Tax Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptHSNWiseSaleReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                If txtTaxCode.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Tax Code : " & clsCommon.GetMulcallString(txtTaxCode.arrValueMember) & "   Tax Name :" & clsCommon.GetMulcallString(txtTaxCode.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item Code : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Tax Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcel(Me.Text, gv1, arrHeader, Me.Text)
                clsCommon.MyMessageBoxShow(Me, "Export Successfully", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

