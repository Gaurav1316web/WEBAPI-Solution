'--------Created By Richa 18/11/2016 Against Ticket No BM00000010292,ERO/07/05/18-000295,UDL/22/05/18-000171
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmCustomerSetOff
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ApplyCardSaleInvoiceOnlyWithCardSaleAdvance As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public isInsideLoadData As Boolean = False
    Dim dtAllData As DataTable = Nothing
    Dim dtAllDataDetail As DataTable = Nothing
    Dim dtmain As DataTable = Nothing
    Dim strQuery As String = String.Empty
    Public ValidatedCount As Integer = 0
    '----------grid Varibales-----------
    Const colApply As String = "colApply"
    Const colDocType As String = "colDocType"
    Const colSINo As String = "colSINo"
    Const colDocNo As String = "colDocNo"
    Const colDocDate As String = "colDocDate"
    Const colVendorInvNo As String = "colVendorInvNo"
    Const colFilledTotal As String = "colFilledTotal"
    Const colEmptyTotal As String = "colEmptyTotal"
    Const colOrgnlAmt As String = "colOrgnlAmt"
    Const colBalAmt As String = "colBalAmt"
    Const colTemp As String = "colTemp"
    Const colTemp1 As String = "colTemp1"
    Const colAppliedAmt As String = "colAppliedAmt"
    Const colTDSAmt As String = "colTDSAmt"
    Const colAdjNo As String = "colAdjNo"
    Const colAdjAmt As String = "colAdjAmt"
    Const colComment As String = "colComment"
    Const colInvisibleTag As String = "colInvisibleTag"
    Const colChild_Cust_Code As String = "colChild_Cust_Code"
    Const colChild_Cust_Name As String = "colChild_Cust_Name"
    Const colConvRateOld As String = "colConvRateOld"

    Const colLineNo As String = "colLineNo"
    Const colGLAccount As String = "colGLAccount"
    Const colAccDesc As String = "colAccDesc"
    Const colAmount As String = "colAmount"
    Const colRemark As String = "colRemark"

    '' grid objects
    Dim apply As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim SiNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim docDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim FilledTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim EmptyTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim BalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim appliedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim temp As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim tdsAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim adjNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim adjAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim comment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim InvisibleTag As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim Child_Cust_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim Child_Cust_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '-----------------------------------
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
   

    Private Sub FrmCustomerSetOff_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave_Click(btnsave, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                         "========Table Name=========" + Environment.NewLine + _
                         "TSPL_RECEIPT_HEADER,TSPL_RECEIPT_DETAIL ,TSPL_RECEIPT_DETAIL_GST ,TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine + _
                         "tspl_BankReco_Head & tspl_BankReco_Detail (For Set Outstanding Entry) " + Environment.NewLine + _
                         "tspl_bank_transfer(For bank transfer)" + Environment.NewLine + _
                         "Journal Entry" + Environment.NewLine + _
                         "=========Setting Name======" + Environment.NewLine + _
                         "AllowSetOffUntilTransactionsnotend" + Environment.NewLine + _
                         "AllowtoSetoffDocDateWise " + Environment.NewLine + _
                         "AllowtoSkipJournalEntryofPaymentandReceiptforAD" + Environment.NewLine + _
                         "AllowToUseSubAccount" + Environment.NewLine + _
                         "AllowUseApplyDocSeriesForReceipt" + Environment.NewLine + _
                         "AllowDefaultBankCodeforCreditNote" + Environment.NewLine + _
                         "SecurityDocumentKnockOffonReceipt" + Environment.NewLine + _
                         "AllowtoSetNoOfTransactionsforSetOff")
        End If
    End Sub
  
    Private Sub FrmCustomerSetOff_Load(sender As Object, e As EventArgs) Handles Me.Load
        ApplyCardSaleInvoiceOnlyWithCardSaleAdvance = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCardSaleInvoiceOnlyWithCardSaleAdvance, clsFixedParameterCode.ApplyCardSaleInvoiceOnlyWithCardSaleAdvance, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
            chkForCardSale.Visible = True
        Else
            chkForCardSale.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCustomersSetOff)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
  
    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Public Sub LoadCustomer()
        'Dim qry As String = " Select * from ( Select distinct Cust_Code as Code, Customer_Name as Name from  (select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date,  (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo, (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt,   EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code  from ( select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," & _
        '" (TSPL_Customer_Invoice_Head.Document_Total  " & _
        '"  -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & _
        '" -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " & _
        '" left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & _
        '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
        '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & _
        '" -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " & _
        '" left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & _
        '"  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " & _
        '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & _
        '" -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " & _
        '" , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1   order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and Balance_Amt>0  and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & _
        '" ) XXX WHERE [Balance Amount]>0 )Final order by Final.Code "


        'Dim qry As String = " Select * from ( Select distinct Cust_Code as Code, Customer_Name as Name from  (select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date,  (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo, (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt,   EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code  from ( select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," & _
        '" (TSPL_Customer_Invoice_Head.Document_Total  " & _
        '"  -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & _
        '" -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " & _
        '" left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & _
        '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
        '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & _
        '" -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " & _
        '" left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & _
        '"  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " & _
        '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & _
        '" -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " & _
        '" , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1   order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and Balance_Amt>0  and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & _
        '" and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' " & _
        '" ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & _
        '" ) XXX WHERE [Balance Amount]>0 )Final order by Final.Code "

        'Dim qry As String = " Select distinct Final.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name  from ( Select TSPL_RECEIPT_HEADER.Cust_Code , Receipt_No as [Code], " & _
        '" Entry_Desc as [Description], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance'  When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], " & _
        '" Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],   TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) " & _
        '" from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND   RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>''),0)" & _
        '" as [Bal Amt]   from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND  Receipt_Type IN ('P','O','U') AND Receipt_No <> ''   and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N' " & _
        '" and (convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )   ) Final" & _
        '" left outer join TSPL_CUSTOMER_MASTER on Final.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        '" where [Bal Amt]>0 "

        Dim strSecurityDocumentKnockOffonReceiptSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SecurityDocumentKnockOffonReceipt, clsFixedParameterCode.SecurityDocumentKnockOffonReceipt, Nothing))
        Dim strwhrsecurity As String = ""
        If clsCommon.CompairString(strSecurityDocumentKnockOffonReceiptSetting, "0") = CompairStringResult.Equal Then
            strwhrsecurity = " and isnull(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "
        End If

        Dim qry As String = " Select distinct Final.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name  from ( Select TSPL_RECEIPT_HEADER.Cust_Code , Receipt_No as [Code], " &
        " Entry_Desc as [Description], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance'  When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], " &
        " Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],   TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) " &
        " from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND   RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>''),0)" &
        " as [Bal Amt]   from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND  Receipt_Type IN ('P','O','U') AND Receipt_No <> ''   and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N' " & strwhrsecurity & " " &
        " and (convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' ) " & Environment.NewLine &
        " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN (sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N') " & Environment.NewLine &
        " union all  " & Environment.NewLine &
        " Select * from  (select xxx.Cust_Code ,[Invoice No] as [Code],Description ,[Invoice Date] as [Receipt Date],Type as [Receipt Type],[Doc Total] as [Receipt Amt],'' As [Bank Code],'' AS [Payment Code], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Bal Amt] from ( select " & Environment.NewLine &
        " TSPL_Customer_Invoice_Head.Description , 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] , " & Environment.NewLine &
        " (TSPL_Customer_Invoice_Head.Document_Total -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & Environment.NewLine &
        " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
        " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & Environment.NewLine &
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
        " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & Environment.NewLine &
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & Environment.NewLine &
 " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
 " -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " & Environment.NewLine &
        " , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & Environment.NewLine &
        " ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code ) XXX WHERE [Bal Amt]>0 " & Environment.NewLine &
        " and [Receipt Type]  ='Credit Note' " & Environment.NewLine &
        " and ([Receipt Date] >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and [Receipt Date]<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  ) " & Environment.NewLine &
        "  ) Final" &
        " left outer join TSPL_CUSTOMER_MASTER on Final.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_Customer_Invoice_Head on Final.Code =TSPL_Customer_Invoice_Head.Document_No" &
        " where [Bal Amt]>0 and " & IIf(chkVSPonly.Checked = True, " isnull(customer_class,'')='VSP'", " isnull(customer_class,'')<>'VSP'") & " and TSPL_CUSTOMER_MASTER.CSA_Type IN ('N','') AND TSPL_CUSTOMER_MASTER.Status ='N' AND TSPL_CUSTOMER_MASTER.OnHold='N' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')=''"

        If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
        End If

        If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and isnull(TSPL_CUSTOMER_MASTER.Cust_Category_Code,'') in (" + clsCommon.GetMulcallString(txtCustomerCategory.arrValueMember) + ")  "
        End If

        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
            If chkForCardSale.Checked = True Then
                qry += " and  Code in (Select distinct against_receipt_no from TSPL_BOOKING_PAYMENT_MODE_DETAIL )"
            Else
                qry += " and  Code not in (Select distinct against_receipt_no from TSPL_BOOKING_PAYMENT_MODE_DETAIL )"
            End If
        End If

        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            If clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + objCommonVar.strCurrUserCustomers + ") "
            End If
        End If

        ' cbgCustomer.DataSource = clsDBFuncationality.GetDataTable("Select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER ")
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Sub Reset()
        btnsave.Enabled = True
        isNewEntry = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
        dgvReceipt.DataSource = Nothing
        chkForCardSale.Checked = False
        chkVSPonly.Checked = False
    End Sub
    Sub ResetonDate()
        btnsave.Enabled = True
        isNewEntry = True
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
    End Sub
    Sub ResetonDateforContinuousTransaction()
        btnsave.Enabled = True
        isNewEntry = True
        'LoadCustomer()
        'chkCustomerAll.IsChecked = True
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
    End Sub
    Sub LoadBlankGrid()

        dgvReceipt.DataSource = Nothing
        dgvReceipt.Rows.Clear()
        dgvReceipt.Columns.Clear()

        dgvReceipt.AllowDeleteRow = True
        dgvReceipt.AllowAddNewRow = False

        apply = New GridViewTextBoxColumn()
        apply.FormatString = ""
        apply.HeaderText = colApply
        apply.Name = colApply
        apply.Width = 50
        apply.ReadOnly = True
        apply.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvReceipt.MasterTemplate.Columns.Add(apply)

        docType = New GridViewTextBoxColumn()
        docType.FormatString = ""
        docType.HeaderText = "Document Type"
        docType.Name = colDocType
        docType.Width = 100
        docType.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(docType)

        SiNo = New GridViewTextBoxColumn()
        SiNo.FormatString = ""
        SiNo.HeaderText = "Document Sale Invoice No"
        SiNo.Name = colSINo
        SiNo.Width = 100
        SiNo.ReadOnly = True
        SiNo.IsVisible = False
        dgvReceipt.MasterTemplate.Columns.Add(SiNo)

        docNo = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "Document No"
        docNo.Name = colDocNo
        docNo.Width = 150
        docNo.ReadOnly = True
        docNo.IsVisible = True
        dgvReceipt.MasterTemplate.Columns.Add(docNo)

        docDate = New GridViewTextBoxColumn()
        docDate.FormatString = ""
        docDate.HeaderText = "Document Date"
        docDate.Name = colDocDate
        docDate.Width = 150
        docDate.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(docDate)

        FilledTotal = New GridViewDecimalColumn()
        FilledTotal.FormatString = ""
        FilledTotal.HeaderText = "Filled"
        FilledTotal.Name = colFilledTotal
        FilledTotal.Width = 70
        FilledTotal.ReadOnly = True
        FilledTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(FilledTotal)

        EmptyTotal = New GridViewDecimalColumn()
        EmptyTotal.FormatString = ""
        EmptyTotal.HeaderText = "Empty"
        EmptyTotal.Name = colEmptyTotal
        EmptyTotal.Width = 70
        EmptyTotal.ReadOnly = True
        EmptyTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(EmptyTotal)

        originalInvAmt = New GridViewDecimalColumn()
        originalInvAmt.FormatString = ""
        originalInvAmt.HeaderText = "Original Amount"
        originalInvAmt.Name = colOrgnlAmt
        originalInvAmt.Width = 100
        originalInvAmt.ReadOnly = True
        originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(originalInvAmt)

        BalAmt = New GridViewDecimalColumn()
        BalAmt.FormatString = ""
        BalAmt.DecimalPlaces = 2
        BalAmt.HeaderText = "Balance Amt"
        BalAmt.Name = colBalAmt
        BalAmt.Width = 100
        BalAmt.ReadOnly = True
        BalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(BalAmt)

        appliedAmt = New GridViewDecimalColumn()
        appliedAmt.FormatString = ""
        appliedAmt.DecimalPlaces = 2
        appliedAmt.HeaderText = "Applied Amount"
        appliedAmt.Name = colAppliedAmt
        appliedAmt.Width = 100
        appliedAmt.ReadOnly = False
        appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(appliedAmt)

        appliedAmt = New GridViewDecimalColumn()
        appliedAmt.FormatString = ""
        appliedAmt.DecimalPlaces = 4
        appliedAmt.HeaderText = "Conv Rate Old"
        appliedAmt.Name = colConvRateOld
        appliedAmt.Width = 100
        appliedAmt.ReadOnly = True
        appliedAmt.IsVisible = False
        appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(appliedAmt)

        temp = New GridViewDecimalColumn()
        temp.FormatString = ""
        temp.Name = colTemp
        temp.ReadOnly = True
        temp.IsVisible = False
        temp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(temp)

        temp = New GridViewDecimalColumn()
        temp.FormatString = ""
        temp.Name = colTemp1
        temp.ReadOnly = True
        temp.IsVisible = False
        temp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(temp)

        tdsAmt = New GridViewDecimalColumn()
        tdsAmt.FormatString = ""
        tdsAmt.HeaderText = "TDS Amount"
        tdsAmt.Name = colTDSAmt
        tdsAmt.Width = 100
        tdsAmt.ReadOnly = True
        tdsAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(tdsAmt)

        adjNo = New GridViewTextBoxColumn()
        adjNo.FormatString = ""
        adjNo.HeaderText = "Adjustment No"
        adjNo.Width = 100
        adjNo.Name = colAdjNo
        adjNo.ReadOnly = True
        adjNo.IsVisible = True
        dgvReceipt.MasterTemplate.Columns.Add(adjNo)

        adjAmt = New GridViewDecimalColumn()
        adjAmt.FormatString = ""
        adjAmt.HeaderText = "Adjustment Amt"
        adjAmt.Name = colAdjAmt
        adjAmt.Width = 100
        adjAmt.ReadOnly = True
        adjAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(adjAmt)

        comment = New GridViewTextBoxColumn()
        comment.FormatString = ""
        comment.HeaderText = "Comment"
        comment.Name = colComment
        comment.Width = 150
        comment.ReadOnly = False
        dgvReceipt.MasterTemplate.Columns.Add(comment)

        InvisibleTag = New GridViewTextBoxColumn()
        InvisibleTag.FormatString = ""
        InvisibleTag.HeaderText = "InvTag"
        InvisibleTag.Name = colInvisibleTag
        InvisibleTag.ReadOnly = True
        InvisibleTag.IsVisible = False
        dgvReceipt.MasterTemplate.Columns.Add(InvisibleTag)

        Child_Cust_Code = New GridViewTextBoxColumn()
        Child_Cust_Code.FormatString = ""
        Child_Cust_Code.HeaderText = "Child Customer Code"
        Child_Cust_Code.Name = colChild_Cust_Code
        Child_Cust_Code.Width = 100
        Child_Cust_Code.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(Child_Cust_Code)

        Child_Cust_Name = New GridViewTextBoxColumn()
        Child_Cust_Name.FormatString = ""
        Child_Cust_Name.HeaderText = "Child Customer Name"
        Child_Cust_Name.Name = colChild_Cust_Name
        Child_Cust_Name.Width = 200
        Child_Cust_Name.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(Child_Cust_Name)

        '   clsCustomFieldGrid.LoadBlankGrid(dgvReceipt, MyBase.ArrDetailFields)

        dgvReceipt.ShowGroupPanel = False
        dgvReceipt.AllowColumnReorder = False
        dgvReceipt.AllowRowReorder = False
        dgvReceipt.EnableSorting = False
        dgvReceipt.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvReceipt.MasterTemplate.ShowRowHeaderColumn = False
        dgvReceipt.AllowAddNewRow = False

    End Sub
    
    
    

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim trans As SqlTransaction = Nothing
        Dim strcustomer As String = String.Empty
        Try
            If txtFromDate.Value > txtToDate.Value Then
                Throw New Exception("From date cannot be greater than to Date")
            End If
            Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, trans))
            If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                While cbgCustomer.CheckedValue.Count > 0
                    If cbgCustomer.CheckedValue.Count > 0 Then
                        Dim list As New ArrayList
                        list = cbgCustomer.CheckedValue

                        Dim j As Integer = 0
                        ' trans = clsDBFuncationality.GetTransactin()
                        ValidatedCount = list.Count
                        clsCommon.ProgressBarPercentShow()
                        For i As Integer = 0 To list.Count - 1
                            j = j + 1
                            trans = Nothing
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & i + 1 & " of Total customers " & ValidatedCount)
                            'EnableDisableTrigger(False, trans)
                            If i > list.Count - 1 Then
                                strcustomer = TryCast(list.Item(list.Count - 1), String)
                            Else
                                strcustomer = TryCast(list.Item(i), String)
                            End If
                            AutoInvoice(strcustomer, "A")
                            'EnableDisableTrigger(True, trans)
                            'trans.Commit()
                            list = cbgCustomer.CheckedValue
                          
                        Next
                        clsCommon.ProgressBarPercentHide()
                        ResetonDateforContinuousTransaction()
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please select atleast one customer to set off.", Me.Text)
                    End If
                End While
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
            Else
                If cbgCustomer.CheckedValue.Count > 0 Then
                    Dim list As New ArrayList
                    list = cbgCustomer.CheckedValue

                    Dim j As Integer = 0
                    ValidatedCount = list.Count
                    clsCommon.ProgressBarPercentShow()
                    For i As Integer = 0 To list.Count - 1
                        j = j + 1
                        'trans = clsDBFuncationality.GetTransactin()
                        clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & i + 1 & " of Total customers " & ValidatedCount)
                        'EnableDisableTrigger(False, trans)
                        strcustomer = TryCast(list.Item(i), String)
                        AutoInvoice(strcustomer, "A")
                        'EnableDisableTrigger(True, trans)
                        'trans.Commit()
                    Next
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                    ResetonDate()
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select atleast one customer to set off.", Me.Text)
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            ResetonDate()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message + " for Customer " + strcustomer, Me.Text)
        End Try
    End Sub

    '' By balwinder on 08/07/2019 it update Journal entry if receipt is of Fiscal Year which is closed.
    'Function EnableDisableTrigger(ByVal isEnable As Boolean, ByVal tran As SqlTransaction) As Boolean
    '    Dim strPre As String = "disable"
    '    If isEnable Then
    '        strPre = "enable"
    '    End If

    '    Try
    '        'clsDBFuncationality.ExecuteNonQuery(strPre + " TRIGGER [dbo].[TrgReceiptTransType] ON [dbo].[TSPL_RECEIPT_HEADER]", tran)
    '    Catch ex As Exception
    '    End Try
    '    Return True
    'End Function


    Sub AutoInvoice(ByVal Customer As String, ByVal ReceiptType As String)
        Dim DocNo As String = Nothing
        Try

            Dim WhrCls As String = String.Empty


            Dim strSecurityDocumentKnockOffonReceiptSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SecurityDocumentKnockOffonReceipt, clsFixedParameterCode.SecurityDocumentKnockOffonReceipt, Nothing))
            Dim strwhrsecurity As String = ""
            If clsCommon.CompairString(strSecurityDocumentKnockOffonReceiptSetting, "0") = CompairStringResult.Equal Then
                strwhrsecurity = " and isnull(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "
            End If


            'strQuery = " Select Final.Code as ReceiptNo,Final.[Receipt Type] ,final.[Receipt Date] from ( Select Receipt_No as [Code], Entry_Desc as [Description], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance' " & _
            '" When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],  " & _
            '" TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND  " & _
            '" RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>''),0) as [Bal Amt]  " & _
            '" from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Cust_Code='" + Customer + "' AND Receipt_Type IN ('P','O','U') AND Receipt_No <> ''  " & _
            '" and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N' " & strwhrsecurity & " and (convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )   " & Environment.NewLine & _
            '" AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN (sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N') " & Environment.NewLine & _
            '" union all  " & Environment.NewLine & _
            '" Select * from  (select [Invoice No] as [Code],Description ,[Invoice Date] as [Receipt Date],Type as [Receipt Type],[Doc Total] as [Receipt Amt],'' As [Bank Code],'' AS [Payment Code], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Bal Amt] from ( select " & Environment.NewLine & _
            '" TSPL_Customer_Invoice_Head.Description , 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] , " & Environment.NewLine & _
            '" (TSPL_Customer_Invoice_Head.Document_Total -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & Environment.NewLine & _
            '" -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine & _
            '" -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine & _
            '" -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & Environment.NewLine & _
            '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & Environment.NewLine & _
            '" -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " & Environment.NewLine & _
            '" , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & Environment.NewLine & _
            '" ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code where ( xxx.cust_Code ='" + Customer + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + Customer + "' ) ) XXX WHERE [Bal Amt]>0 " & Environment.NewLine & _
            '" and [Receipt Type]  ='Credit Note' " & Environment.NewLine & _
            '" and ([Receipt Date] >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and [Receipt Date]<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  ) " & Environment.NewLine & _
            '") Final where [Bal Amt]>0 order by [Receipt Date] "
            Dim NOOFTRANSACTIONS As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetNoOfTransactionsforSetOff, clsFixedParameterCode.AllowtoSetNoOfTransactionsforSetOff, Nothing))
            If clsCommon.myLen(NOOFTRANSACTIONS) > 0 Then
                NOOFTRANSACTIONS = " TOP " & NOOFTRANSACTIONS & ""
            Else
                NOOFTRANSACTIONS = ""
            End If
            strQuery = " Select " & NOOFTRANSACTIONS & " Final.Code as ReceiptNo,Final.[Receipt Type] ,final.[Receipt Date] from ( Select Receipt_No as [Code], Entry_Desc as [Description], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance' " &
          " When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],  " &
          " TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code], Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND  " &
          " RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>''),0) as [Bal Amt],  " &
          " case when Receipt_Type='O' then 1 when Receipt_Type ='P' then 2 else 3 end AS DocOrderType " &
          " from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Cust_Code='" + Customer + "' AND Receipt_Type IN ('P','O','U') AND Receipt_No <> ''  " &
          " and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N' " & strwhrsecurity & " and (convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )   " & Environment.NewLine &
          " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN (sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N') " & Environment.NewLine &
          " union all  " & Environment.NewLine &
          " Select *,0 AS DocOrderType from  (select [Invoice No] as [Code],Description ,[Invoice Date] as [Receipt Date],Type as [Receipt Type],[Doc Total] as [Receipt Amt],'' As [Bank Code],'' AS [Payment Code], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Bal Amt] from ( select " & Environment.NewLine &
          " TSPL_Customer_Invoice_Head.Description , 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] , " & Environment.NewLine &
          " (TSPL_Customer_Invoice_Head.Document_Total -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & Environment.NewLine &
          " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
          " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & Environment.NewLine &
          " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
          " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & Environment.NewLine &
          " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & Environment.NewLine &
 " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
" -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " & Environment.NewLine &
          " , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & Environment.NewLine &
          " ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code where ( xxx.cust_Code ='" + Customer + "' ) ) XXX WHERE [Bal Amt]>0 " & Environment.NewLine &
          " and [Receipt Type]  ='Credit Note' " & Environment.NewLine &
          " and ([Receipt Date] >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and [Receipt Date]<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  ) " & Environment.NewLine &
          ") Final left outer join TSPL_Customer_Invoice_Head on Final.Code  =TSPL_Customer_Invoice_Head.Document_No where [Bal Amt]>0 and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' order by [Receipt Date] ,DocOrderType "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery, Nothing)

            Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, Nothing))

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ''richa for progress bar
                'Dim list1 As New ArrayList
                'list1 = dt.Rows.Count
                Dim j1 As Integer = 0

                Dim ValidatedCount1 As Integer = dt.Rows.Count
                '  clsCommon.ProgressBarPercentShow()
                'For i1 As Integer = 0 To dt.Rows.Count - 1
                '    j1 = j1 + 1
                '    clsCommon.ProgressBarPercentUpdate(j1 / ValidatedCount1 * 100, " Saving and posting Record(s) " & i1 & " of Total documents " & ValidatedCount1)
                'Next

                ''------



                For i As Integer = 0 To dt.Rows.Count - 1
                    j1 = j1 + 1
                    clsCommon.ProgressBarPercentUpdate(j1 / ValidatedCount1 * 100, " Saving and posting Record(s) " & i + 1 & " of Total documents " & ValidatedCount1 & " for customer " & Customer)

                    txtDocumentNo.Value = clsCommon.myCstr(dt.Rows(i)("ReceiptNo"))
                    lblDocumentType.Text = clsCommon.myCstr(dt.Rows(i)("Receipt Type"))
                    lbldocumentdate.Text = clsCommon.myCDate(dt.Rows(i)("Receipt Date"))
                    lblBalAmt.Text = clsRcptEntryHeader.GetBalance(txtDocumentNo.Value, "", Nothing)
                    ''richa 20/12/2017
                    Dim strAllowtoSetoffDocDateWise As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetoffDocDateWise, clsFixedParameterCode.AllowtoSetoffDocDateWise, Nothing))
                    If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then


                        Dim strQueryforinvoiceno As String = " Select top 1 XXX.DocNo from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
                        " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
                        " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
                        " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
                        "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" &
                        " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
                        " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
                        " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
                        " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
                        " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
                        " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
                        " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
                        " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
                        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
                        " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
                        " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
                        " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
                        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
                        " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
                        " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
                        " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
                        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
                        " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
                        " -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " + Environment.NewLine +
                        " , '0.00' as [Apply_Amt], " &
                        " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine &
                        " UNION All " &
                        " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
                        " (Receipt_Amount " &
                        " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
                        " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' and isnull(SecurityDeposit,'')='N'  AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
                        " ) as xxx " &
                        " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + Customer + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + Customer + "' )" &
                        ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and Type<>'Credit Note' ORDER By [Invoice Date],DocOrderType"
                        Dim strInvoiceno As String = clsDBFuncationality.getSingleValue(strQueryforinvoiceno, Nothing)
                        If clsCommon.myCdbl(lblBalAmt.Text) > 0 And clsCommon.myLen(strInvoiceno) > 0 Then
                            While clsCommon.myCdbl(lblBalAmt.Text) > 0 And clsCommon.myLen(strInvoiceno) > 0
                                'LoadBlankGrid()
                                funFillGrid(Customer, Nothing)
                                Dim strcountofcreditnote As Integer = 0
                                If dgvReceipt.Rows.Count > 0 Then
                                    For Each grow As GridViewRowInfo In dgvReceipt.Rows
                                        If clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                            strcountofcreditnote = strcountofcreditnote + 1
                                        End If
                                    Next
                                    If strcountofcreditnote <> dgvReceipt.Rows.Count Then
                                        AutoApplyAmt(clsCommon.myCdbl(lblBalAmt.Text))
                                        savedata(Customer)
                                    End If

                                    If strcountofcreditnote = dgvReceipt.Rows.Count AndAlso clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                                        Dim list As New ArrayList
                                        list = cbgCustomer.CheckedValue
                                        list.Remove(Customer)
                                        cbgCustomer.CheckedValue = list
                                    End If
                                End If
                                lblBalAmt.Text = clsRcptEntryHeader.GetBalance(txtDocumentNo.Value, "", Nothing)
                                strInvoiceno = clsDBFuncationality.getSingleValue(strQueryforinvoiceno, Nothing)
                            End While
                        Else
                            If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                                Dim list As New ArrayList
                                list = cbgCustomer.CheckedValue
                                list.Remove(Customer)
                                cbgCustomer.CheckedValue = list

                            End If
                        End If

                    Else
                        If clsCommon.myCdbl(lblBalAmt.Text) > 0 Then
                            'LoadBlankGrid()
                            funFillGrid(Customer, Nothing)
                            Dim strcountofcreditnote As Integer = 0
                            If dgvReceipt.Rows.Count > 0 Then
                                For Each grow As GridViewRowInfo In dgvReceipt.Rows
                                    If clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                        strcountofcreditnote = strcountofcreditnote + 1
                                    End If
                                Next
                                If strcountofcreditnote <> dgvReceipt.Rows.Count Then
                                    AutoApplyAmt(clsCommon.myCdbl(lblBalAmt.Text))
                                    savedata(Customer)
                                End If

                                If strcountofcreditnote = dgvReceipt.Rows.Count AndAlso clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                                    Dim list As New ArrayList
                                    list = cbgCustomer.CheckedValue
                                    list.Remove(Customer)
                                    cbgCustomer.CheckedValue = list
                                End If
                            End If
                        End If
                    End If
                    ''-----------------

                Next

                'clsCommon.ProgressBarPercentHide()
            Else
                If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                    Dim list As New ArrayList
                    list = cbgCustomer.CheckedValue
                    list.Remove(Customer)
                    cbgCustomer.CheckedValue = list

                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'inSideLoadData = False
        End Try


    End Sub
    Public Sub funFillGrid(ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            'LoadBlankGrid()
            'dgvReceipt.Rows.Clear()
            'dgvReceipt.Columns.Clear()
            dgvReceipt.DataSource = Nothing
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = ""
            Else
                WhrCls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            Dim strAllowtoSetoffDocDateWise As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetoffDocDateWise, clsFixedParameterCode.AllowtoSetoffDocDateWise, trans))
            Dim strInvoiceDate As Date?
            Dim srcreditnote As String = String.Empty
            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
                Dim strQueryforinvoicedate As String = " Select top 1 XXX.[Invoice Date] from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
           " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
           " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
           " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
           "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" &
           " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
           " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
           " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
           " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
           " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
           " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
           " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
           " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
           " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
           " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
           " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
           " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
           " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
           " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
" -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
" -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " + Environment.NewLine +
           " , '0.00' as [Apply_Amt], " &
           " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine
                If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
                    If chkForCardSale.Checked Then
                        strQueryforinvoicedate += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=1 "
                    Else
                        strQueryforinvoicedate += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=0 "
                    End If
                End If

                strQueryforinvoicedate += " UNION All " &
           " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
           " (Receipt_Amount " &
           " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
           " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' and isnull(SecurityDeposit,'')='N'  AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
           " ) as xxx " &
           " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" &
           ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and type<>'Credit Note' ORDER By [Invoice Date],DocOrderType"
                strInvoiceDate = clsDBFuncationality.getSingleValue(strQueryforinvoicedate, trans)
                strQueryforinvoicedate = String.Empty
            End If

            strQuery = ""
            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
                strQuery = " Select final.*,sum(final.Cum_balance ) over (order by final.RowNo)  as Cumulaive_Bal,RowNo from ( "
            End If
            'If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
            '    strQuery = " Select  final.Apply as colApply ,final.Type as colDocType,final.DocNo as colSINo,final.[Invoice No] as colDocNo ,final.[Invoice Date] as colDocDate ,final.[Balance Amount] as colFilledTotal,final.EmptyTotal as colEmptyTotal,final.[Doc Total] as colOrgnlAmt, final.[Balance Amount] as colBalAmt,final.Apply_Amt as colAppliedAmt,final.ConvRate as colConvRateOld,final.[Balance Amount] as colTemp,isnull(final.[Balance Amount],0)+isnull(final.AdjAmt,0)  as colTemp1, isnull(final.AdjNo,'') as colAdjNo,isnull(final.AdjAmt,0) as  colAdjAmt,final.Tag as colInvisibleTag, final.Child_Customer_Code as colChild_Cust_Code, sum(final.Cum_balance ) over (order by final.RowNo)   as Cumulaive_Bal,RowNo from ( "
            'End If

            'strQuery += " Select XXX.*, case when Type ='Credit Note' then [Balance Amount] *-1 else [Balance Amount] end as Cum_balance,ROW_NUMBER() OVER ( ORDER BY [Invoice Date],DocOrderType) as RowNo from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," & _
            strQuery += " Select  XXX.Apply as colApply ,XXX.Type as colDocType,XXX.DocNo as colSINo,XXX.[Invoice No] as colDocNo ,XXX.[Invoice Date] as colDocDate ,XXX.[Balance Amount] as colFilledTotal,XXX.EmptyTotal as colEmptyTotal,XXX.[Doc Total] as colOrgnlAmt, XXX.[Balance Amount] as colBalAmt,XXX.Apply_Amt as colAppliedAmt,XXX.ConvRate as colConvRateOld,XXX.[Balance Amount] as colTemp,isnull(XXX.[Balance Amount],0)+isnull(XXX.AdjAmt,0)  as colTemp1, isnull(XXX.AdjNo,'') as colAdjNo,isnull(XXX.AdjAmt,0) as  colAdjAmt,XXX.Tag as colInvisibleTag, XXX.Child_Customer_Code as colChild_Cust_Code, case when Type ='Credit Note' then [Balance Amount] *-1 else [Balance Amount] end as Cum_balance,ROW_NUMBER() OVER ( ORDER BY [Invoice Date],DocOrderType) as RowNo from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
       " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
       " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
       " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
       "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" &
       " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
       " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
       " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
       " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
       " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
       " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
       " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
       " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
       " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
       " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
       " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
       " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
       " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
       " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
" -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
" -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " + Environment.NewLine +
       " , '0.00' as [Apply_Amt], " &
       " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine
            If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
                If chkForCardSale.Checked Then
                    strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=1 "
                Else
                    strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=0 "
                End If
            End If

            strQuery += " UNION All " &
       " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
       " (Receipt_Amount " &
       " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
       " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' and isnull(SecurityDeposit,'')='N'  AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
       " ) as xxx " &
       " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" &
       ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 "


            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
                If clsCommon.myLen(strInvoiceDate) > 0 Then
                    strQuery += "and convert(date,[Invoice Date] ,103) ='" & clsCommon.GetPrintDate(strInvoiceDate, "dd/MMM/yyyy") & "' "
                End If
            Else
                strQuery += "and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
            End If

            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
                strQuery += " and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' ORDER By [Invoice Date],DocOrderType "
            Else
                strQuery += " and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' )final "
            End If
            Dim dt As DataTable = Nothing
            'If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
            '    dt = clsDBFuncationality.GetDataTable(strQuery, trans)
            'Else
            '    'dt = clsDBFuncationality.GetDataTable(strQuery + " where final.Type<>'Credit Note' ", trans)
            '    'Dim strRwNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  Select case when isnull(max(RowNo),0)=0 then 1 else max(RowNo) end RowNo from (" + strQuery + " )fullFinal where Cumulaive_Bal<=" & clsCommon.myCdbl(lblBalAmt.Text) & " ", trans))

            '    'dt = clsDBFuncationality.GetDataTable(strQuery + " where RowNo <= " & clsCommon.myCdbl(strRwNo) & " ", trans)
            '    dt = clsDBFuncationality.GetDataTable(strQuery, trans)
            'End If
            dt = clsDBFuncationality.GetDataTable(strQuery, trans)
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery + " where final.Type<>'Credit Note' ", trans)
            If dt.Rows.Count > 0 Then
                'If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
                '    Dim strRwNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  Select case when isnull(max(RowNo),0)=0 then 1 else max(RowNo) end RowNo from (" + strQuery + " )fullFinal where Cumulaive_Bal<=" & clsCommon.myCdbl(lblBalAmt.Text) & " ", trans))

                '    dt = clsDBFuncationality.GetDataTable(strQuery + " where RowNo <= " & clsCommon.myCdbl(strRwNo) & " ", trans)
                'End If
                If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
                    'If dt.Rows.Count = 1 AndAlso clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Credit Note") = CompairStringResult.Equal Then
                    '    srcreditnote = clsCommon.myCstr(dt.Rows(0)("DocNo"))
                    If dt.Rows.Count = 1 AndAlso clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("colDocType")), "Credit Note") = CompairStringResult.Equal Then
                        srcreditnote = clsCommon.myCstr(dt.Rows(0)("colDocNo"))
                        strQuery = " Select XXX.Apply as colApply ,XXX.Type as colDocType,XXX.DocNo as colSINo,XXX.[Invoice No] as colDocNo ,XXX.[Invoice Date] as colDocDate ,XXX.[Balance Amount] as colFilledTotal,XXX.EmptyTotal as colEmptyTotal,XXX.[Doc Total] as colOrgnlAmt, XXX.[Balance Amount] as colBalAmt,XXX.Apply_Amt as colAppliedAmt,XXX.ConvRate as colConvRateOld,XXX.[Balance Amount] as colTemp,isnull(XXX.[Balance Amount],0)+isnull(XXX.AdjAmt,0)  as colTemp1, isnull(XXX.AdjNo,'') as colAdjNo,isnull(XXX.AdjAmt,0) as  colAdjAmt,XXX.Tag as colInvisibleTag, XXX.Child_Customer_Code as colChild_Cust_Code from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
                " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
                " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
                " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
                "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" &
                " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
                " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
                " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
                " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
                " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
                " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine &
                " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
                " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
                " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
                " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
                 " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
                " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
                 " -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " + Environment.NewLine +
                " , '0.00' as [Apply_Amt], " &
                " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine
                        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
                            If chkForCardSale.Checked Then
                                strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=1 "
                            Else
                                strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=0 "
                            End If
                        End If
                        strQuery += " UNION All " &
                " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
                " (Receipt_Amount " &
                " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
                " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' and isnull(SecurityDeposit,'')='N'  AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
                " ) as xxx " &
                " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" &
                ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 "

                        If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strInvoiceDate) > 0 Then
                                strQuery += "and convert(date,[Invoice Date] ,103) ='" & clsCommon.GetPrintDate(strInvoiceDate, "dd/MMM/yyyy") & "' "
                            End If
                        Else
                            strQuery += "and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
                        End If

                        strQuery += " and [Invoice No] not in ('" & txtDocumentNo.Value & "','" & srcreditnote & "') and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' ORDER By [Invoice Date],DocOrderType "

                        dt = clsDBFuncationality.GetDataTable(strQuery, trans)
                        strQuery = String.Empty
                    End If
                End If

                If dt.Rows.Count > 0 Then
                    'If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then

                    '    For ii As Integer = 0 To dt.Rows.Count - 1
                    '        If clsCommon.myCdbl(dt.Rows(ii)("Balance Amount")) > 0 Then
                    '            dgvReceipt.Rows.AddNew()
                    '            dgvReceipt.CurrentRow.Cells(colApply).Value = clsCommon.myCstr(dt.Rows(ii)("Apply"))
                    '            dgvReceipt.CurrentRow.Cells(colDocType).Value = clsCommon.myCstr(dt.Rows(ii)("Type"))
                    '            dgvReceipt.CurrentRow.Cells(colSINo).Value = clsCommon.myCstr(dt.Rows(ii)("DocNo"))
                    '            dgvReceipt.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice No"))
                    '            dgvReceipt.CurrentRow.Cells(colConvRateOld).Value = clsCommon.myCdbl(dt.Rows(ii)("ConvRate"))
                    '            dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice Date"))
                    '            dgvReceipt.CurrentRow.Cells(colFilledTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                    '            dgvReceipt.CurrentRow.Cells(colEmptyTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("EmptyTotal"))
                    '            dgvReceipt.CurrentRow.Cells(colOrgnlAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Doc Total"))
                    '            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                    '            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
                    '            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
                    '            dgvReceipt.CurrentRow.Cells(colTemp).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                    '            dgvReceipt.CurrentRow.Cells(colTemp1).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt")) + clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                    '            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Apply_Amt"))
                    '            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
                    '            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
                    '            dgvReceipt.CurrentRow.Cells(colInvisibleTag).Value = clsCommon.myCstr(dt.Rows(ii)("Tag"))
                    '            dgvReceipt.CurrentRow.Cells(colChild_Cust_Code).Value = clsCommon.myCstr(dt.Rows(ii)("Child_Customer_Code"))
                    '        End If
                    '        If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
                    '            If clsCommon.myCdbl(dt.Rows(ii)("Cumulaive_Bal")) >= clsCommon.myCdbl(lblBalAmt.Text) Then ''And clsCommon.myCdbl(dt.Rows(ii)("Cumulaive_Bal")) >= 0.0 Then
                    '                Exit For
                    '            End If
                    '        End If
                    '    Next
                    '    dgvReceipt.Rows.Clear()
                    '    dgvReceipt.Columns.Clear()
                    '    dgvReceipt.CurrentRow = dgvReceipt.Rows(0)
                    'Else
                    dgvReceipt.DataSource = dt
                    dgvReceipt.CurrentRow = dgvReceipt.Rows(0)
                    'End If
                Else
                    Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, trans))
                    If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                        Dim list As New ArrayList
                        list = cbgCustomer.CheckedValue
                        list.Remove(strCustCode)
                        cbgCustomer.CheckedValue = list

                    End If

                End If
            Else
                Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, trans))
                If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
                    Dim list As New ArrayList
                    list = cbgCustomer.CheckedValue
                    list.Remove(strCustCode)
                    cbgCustomer.CheckedValue = list
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '    Public Sub funFillGrid(ByVal strCustCode As String, ByVal trans As SqlTransaction)
    '        Try
    '            LoadBlankGrid()
    '            Dim WhrCls As String = ""
    '            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
    '                WhrCls = ""
    '            Else
    '                WhrCls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
    '            End If

    '            Dim strAllowtoSetoffDocDateWise As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetoffDocDateWise, clsFixedParameterCode.AllowtoSetoffDocDateWise, trans))
    '            Dim strInvoiceDate As Date?
    '            Dim srcreditnote As String = String.Empty
    '            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                Dim strQueryforinvoicedate As String = " Select top 1 XXX.[Invoice Date] from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," & _
    '           " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " & _
    '           " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," & _
    '           " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " & _
    '           "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" & _
    '           " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " & _
    '           " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," & _
    '           " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine + _
    '           " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine + _
    '           " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine + _
    '           " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine & _
    '           " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine + _
    '           " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine + _
    '           " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine + _
    '           " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '           " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine + _
    '           " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine + _
    '           " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine + _
    '           " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine + _
    '" -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine + _
    '" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine + _
    '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine + _
    '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '" -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " + Environment.NewLine + _
    '           " , '0.00' as [Apply_Amt], " & _
    '           " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine & _
    '           " UNION All " & _
    '           " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," & _
    '           " (Receipt_Amount " & _
    '           " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " & _
    '           " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " & _
    '           " ) as xxx " & _
    '           " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" & _
    '           ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' and type<>'Credit Note' ORDER By [Invoice Date],DocOrderType"
    '                strInvoiceDate = clsDBFuncationality.getSingleValue(strQueryforinvoicedate, trans)
    '                strQueryforinvoicedate = String.Empty
    '            End If

    '            '            strQuery = " Select XXX.* from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," & _
    '            '            " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " & _
    '            '            " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," & _
    '            '            " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " & _
    '            '            "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" & _
    '            '            " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " & _
    '            '            " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," & _
    '            '            " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine + _
    '            '            " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine + _
    '            '            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine + _
    '            '            " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine & _
    '            '            " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine + _
    '            '            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine + _
    '            '            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine + _
    '            '            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '            '            " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine + _
    '            '            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine + _
    '            '            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine + _
    '            '            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine + _
    '            ' " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine + _
    '            '" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine + _
    '            '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine + _
    '            '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '            ' " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " + Environment.NewLine + _
    '            '            " , '0.00' as [Apply_Amt], " & _
    '            '            " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine & _
    '            '            " UNION All " & _
    '            '            " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," & _
    '            '            " (Receipt_Amount " & _
    '            '            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " & _
    '            '            " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " & _
    '            '            " ) as xxx " & _
    '            '            " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" & _
    '            '            ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 "
    '            strQuery = ""
    '            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
    '                strQuery = " Select final.*,sum(final.Cum_balance ) over (order by final.RowNo)  as Cumulaive_Bal from ( "
    '            End If
    '            strQuery += " Select XXX.*, case when Type ='Credit Note' then [Balance Amount] *-1 else [Balance Amount] end as Cum_balance,ROW_NUMBER() OVER ( ORDER BY [Invoice Date],DocOrderType) as RowNo from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," & _
    '            " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " & _
    '            " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," & _
    '            " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " & _
    '            "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" & _
    '            " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " & _
    '            " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," & _
    '            " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine + _
    '            " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine + _
    '            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine + _
    '            " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine & _
    '            " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine + _
    '            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine + _
    '            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine + _
    '            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '            " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine + _
    '            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine + _
    '            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine + _
    '            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine + _
    ' " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine + _
    '" left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine + _
    '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine + _
    '" where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    ' " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " + Environment.NewLine + _
    '            " , '0.00' as [Apply_Amt], " & _
    '            " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine & _
    '            " UNION All " & _
    '            " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," & _
    '            " (Receipt_Amount " & _
    '            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " & _
    '            " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " & _
    '            " ) as xxx " & _
    '            " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" & _
    '            ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 "


    '            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                If clsCommon.myLen(strInvoiceDate) > 0 Then
    '                    strQuery += "and convert(date,[Invoice Date] ,103) ='" & clsCommon.GetPrintDate(strInvoiceDate, "dd/MMM/yyyy") & "' "
    '                End If
    '            Else
    '                strQuery += "and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
    '            End If

    '            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                strQuery += " and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' ORDER By [Invoice Date],DocOrderType "
    '            Else
    '                strQuery += " and [Invoice No] <>'" & txtDocumentNo.Value & "' and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' )final "
    '            End If
    '            Dim dt As DataTable = Nothing
    '            If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                dt = clsDBFuncationality.GetDataTable(strQuery, trans)
    '            Else
    '                dt = clsDBFuncationality.GetDataTable(strQuery + " where final.Type<>'Credit Note' ", trans)
    '            End If
    '            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery + " where final.Type<>'Credit Note' ", trans)
    '            If dt.Rows.Count > 0 Then
    '                If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
    '                    dt = clsDBFuncationality.GetDataTable(strQuery, trans)
    '                End If
    '                If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                    If dt.Rows.Count = 1 AndAlso clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Credit Note") = CompairStringResult.Equal Then
    '                        srcreditnote = clsCommon.myCstr(dt.Rows(0)("DocNo"))

    '                        strQuery = " Select XXX.* from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," & _
    '                " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " & _
    '                " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," & _
    '                " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " & _
    '                "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code,DocOrderType  from (" & _
    '                " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " & _
    '                " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," & _
    '                " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine + _
    '                " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine + _
    '                " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine + _
    '                " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> ''),0)  " & Environment.NewLine & _
    '                " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine + _
    '                " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine + _
    '                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine + _
    '                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '                " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine + _
    '                " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine + _
    '                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine + _
    '                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine + _
    '                 " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine + _
    '                " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine + _
    '                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine + _
    '                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine + _
    '                 " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " + Environment.NewLine + _
    '                " , '0.00' as [Apply_Amt], " & _
    '                " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation,case when Document_Type ='I' then 0 when Document_Type ='D' then 1 else 3 end AS DocOrderType from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " & Environment.NewLine & _
    '                " UNION All " & _
    '                " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," & _
    '                " (Receipt_Amount " & _
    '                " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " & _
    '                " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation,2 AS DocOrderType  from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " & _
    '                " ) as xxx " & _
    '                " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" & _
    '                ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]   =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Balance Amount]>0 "

    '                        If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") = CompairStringResult.Equal Then
    '                            If clsCommon.myLen(strInvoiceDate) > 0 Then
    '                                strQuery += "and convert(date,[Invoice Date] ,103) ='" & clsCommon.GetPrintDate(strInvoiceDate, "dd/MMM/yyyy") & "' "
    '                            End If
    '                        Else
    '                            strQuery += "and convert(date,[Invoice Date] ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
    '                        End If

    '                        strQuery += " and [Invoice No] not in ('" & txtDocumentNo.Value & "','" & srcreditnote & "') and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' ORDER By [Invoice Date],DocOrderType "

    '                        dt = clsDBFuncationality.GetDataTable(strQuery, trans)
    '                        strQuery = String.Empty
    '                    End If
    '                End If

    '                If dt.Rows.Count > 0 Then
    '                    For ii As Integer = 0 To dt.Rows.Count - 1
    '                        If clsCommon.myCdbl(dt.Rows(ii)("Balance Amount")) > 0 Then
    '                            dgvReceipt.Rows.AddNew()
    '                            dgvReceipt.CurrentRow.Cells(colApply).Value = clsCommon.myCstr(dt.Rows(ii)("Apply"))
    '                            dgvReceipt.CurrentRow.Cells(colDocType).Value = clsCommon.myCstr(dt.Rows(ii)("Type"))
    '                            dgvReceipt.CurrentRow.Cells(colSINo).Value = clsCommon.myCstr(dt.Rows(ii)("DocNo"))
    '                            dgvReceipt.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice No"))
    '                            dgvReceipt.CurrentRow.Cells(colConvRateOld).Value = clsCommon.myCdbl(dt.Rows(ii)("ConvRate"))
    '                            dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice Date"))
    '                            dgvReceipt.CurrentRow.Cells(colFilledTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
    '                            dgvReceipt.CurrentRow.Cells(colEmptyTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("EmptyTotal"))
    '                            dgvReceipt.CurrentRow.Cells(colOrgnlAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Doc Total"))
    '                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
    '                            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
    '                            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
    '                            dgvReceipt.CurrentRow.Cells(colTemp).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
    '                            dgvReceipt.CurrentRow.Cells(colTemp1).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt")) + clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
    '                            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Apply_Amt"))
    '                            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
    '                            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
    '                            dgvReceipt.CurrentRow.Cells(colInvisibleTag).Value = clsCommon.myCstr(dt.Rows(ii)("Tag"))
    '                            dgvReceipt.CurrentRow.Cells(colChild_Cust_Code).Value = clsCommon.myCstr(dt.Rows(ii)("Child_Customer_Code"))
    '                        End If
    '                        If clsCommon.CompairString(strAllowtoSetoffDocDateWise, "1") <> CompairStringResult.Equal Then
    '                            If clsCommon.myCdbl(dt.Rows(ii)("Cumulaive_Bal")) >= clsCommon.myCdbl(lblBalAmt.Text) Then ''And clsCommon.myCdbl(dt.Rows(ii)("Cumulaive_Bal")) >= 0.0 Then
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                    dgvReceipt.CurrentRow = dgvReceipt.Rows(0)
    '                Else
    '                    Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, trans))
    '                    If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
    '                        Dim list As New ArrayList
    '                        list = cbgCustomer.CheckedValue
    '                        list.Remove(strCustCode)
    '                        cbgCustomer.CheckedValue = list

    '                    End If

    '                End If
    '            Else
    '                Dim strAllowSetOffUntilTransactionsnotend As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, trans))
    '                If clsCommon.CompairString(strAllowSetOffUntilTransactionsnotend, "1") = CompairStringResult.Equal Then
    '                    Dim list As New ArrayList
    '                    list = cbgCustomer.CheckedValue
    '                    list.Remove(strCustCode)
    '                    cbgCustomer.CheckedValue = list
    '                End If
    '            End If

    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub
    Public Sub AutoApplyAmt(ByVal tempAmt As Decimal)
        Try
            Dim ReceiptAmt As Decimal = 0
            For Each grow As GridViewRowInfo In dgvReceipt.Rows
                If tempAmt > 0 Then
                    If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                        grow.Cells(colApply).Value = "Yes"
                        If clsCommon.myCdbl(grow.Cells(colBalAmt).Value) <= tempAmt Then
                            grow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(grow.Cells(colBalAmt).Value)
                            grow.Cells(colBalAmt).Value = 0.0
                        ElseIf clsCommon.myCdbl(grow.Cells(colBalAmt).Value) > tempAmt Then
                            grow.Cells(colAppliedAmt).Value = tempAmt
                            grow.Cells(colBalAmt).Value = clsCommon.myCdbl(grow.Cells(colBalAmt).Value) - tempAmt
                        End If
                        If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                            tempAmt = tempAmt - clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            ReceiptAmt = ReceiptAmt + clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                        End If
                        lblreceiptamount.Text = clsCommon.myCdbl(ReceiptAmt)
                    End If

                    ''richa 
                    'grow.Cells(colApply).Value = "Yes"
                    'If clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) <= tempAmt Then
                    '    grow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(grow.Cells(colPendingAmt).Value)
                    '    grow.Cells(colPendingAmt).Value = 0.0
                    'ElseIf clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) > tempAmt Then
                    '    grow.Cells(colAppliedAmt).Value = tempAmt
                    '    grow.Cells(colPendingAmt).Value = clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) - tempAmt
                    'End If
                    'If clsCommon.CompairString(grow.Cells(colDocType).Value, "Debit Note") <> CompairStringResult.Equal Then
                    '    tempAmt = tempAmt - clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                    '    ReceiptAmt = ReceiptAmt + clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                    '    'Else
                    '    '    tempAmt = tempAmt + clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                    '    '    ReceiptAmt = ReceiptAmt - clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                    'End If
                    'lblreceiptamount.Text = clsCommon.myCdbl(ReceiptAmt)

                    ''-----------------


                Else
                    '' to apply credit note amount with debit note and invoices
                    Dim strinvoicecount As Integer = 0
                    For Each grow1 As GridViewRowInfo In dgvReceipt.Rows
                        If clsCommon.CompairString(grow1.Cells(colApply).Value, "No") = CompairStringResult.Equal OrElse (clsCommon.CompairString(grow1.Cells(colApply).Value, "Yes") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow1.Cells(colBalAmt).Value) > 0) Then
                            'If clsCommon.CompairString(grow1.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow1.Cells(colBalAmt).Value) > 0 Then
                            '    strinvoicecount = strinvoicecount + 1
                            'End If
                            If (clsCommon.CompairString(grow1.Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Or clsCommon.CompairString(grow1.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(grow1.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow1.Cells(colBalAmt).Value) > 0 Then
                                strinvoicecount = strinvoicecount + 1
                            End If
                            If strinvoicecount > 0 Then
                                For Each gvexternal As GridViewRowInfo In dgvReceipt.Rows
                                    If clsCommon.CompairString(gvexternal.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvexternal.Cells(colBalAmt).Value) > 0 Then
                                        tempAmt = clsCommon.myCdbl(gvexternal.Cells(colBalAmt).Value)
                                        gvexternal.Cells(colApply).Value = "Yes"

                                        For Each gvinternal As GridViewRowInfo In dgvReceipt.Rows
                                            If clsCommon.CompairString(gvinternal.Cells(colDocType).Value, "Credit Note") <> CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value) > 0 Then
                                                gvinternal.Cells(colApply).Value = "Yes"
                                                If clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value) <= tempAmt Then
                                                    Dim dblinternalremaingbal As Double = clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value)
                                                    'gvinternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvinternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value)
                                                    'gvinternal.Cells(colBalAmt).Value = 0.0

                                                    'gvexternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(gvinternal.Cells(colAppliedAmt).Value)
                                                    'gvexternal.Cells(colBalAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colBalAmt).Value) - clsCommon.myCdbl(gvinternal.Cells(colAppliedAmt).Value)

                                                    gvinternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvinternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value)
                                                    gvinternal.Cells(colBalAmt).Value = 0.0

                                                    gvexternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(dblinternalremaingbal)
                                                    gvexternal.Cells(colBalAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colBalAmt).Value) - clsCommon.myCdbl(dblinternalremaingbal)
                                                    tempAmt = 0
                                                    'tempAmt =
                                                ElseIf clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value) > tempAmt Then
                                                    gvinternal.Cells(colAppliedAmt).Value = gvinternal.Cells(colAppliedAmt).Value + tempAmt
                                                    gvinternal.Cells(colBalAmt).Value = clsCommon.myCdbl(gvinternal.Cells(colBalAmt).Value) - tempAmt

                                                    gvexternal.Cells(colAppliedAmt).Value = gvexternal.Cells(colAppliedAmt).Value + tempAmt
                                                    gvexternal.Cells(colBalAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colBalAmt).Value) - tempAmt
                                                    tempAmt = 0
                                                End If

                                                If tempAmt = 0 Then
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                        If tempAmt = 0 Then
                                            Exit For
                                        End If
                                    End If
                                Next
                                strinvoicecount = 0
                            End If

                        End If

                    Next
                    Exit For
                    ''---------------------------------- to apply credit note amount with debit note and invoices
                End If
                '---------------
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function savedata(ByVal strcustomer As String)
        Try
            Dim obj As New clsRcptEntryHeader()
            obj.memorndmamt = "0"
            obj.Entry_Desc = "Against set off for " + lblDocumentType.Text + " document No. " + txtDocumentNo.Value
            For Each grow As GridViewRowInfo In dgvReceipt.Rows
                If grow.Cells(colApply).Value = "Yes" Then
                    If clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value) >= clsCommon.myCDate(grow.Cells(colDocDate).Value) Then
                        lbldocinvoicedate.Text = clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value)
                    Else
                        lbldocinvoicedate.Text = clsCommon.myCDate(grow.Cells(colDocDate).Value)
                    End If
                End If
            Next
            If clsCommon.myCDate(lbldocumentdate.Text) >= clsCommon.myCDate(lbldocinvoicedate.Text) Then
                obj.Receipt_Date = clsCommon.myCDate(lbldocumentdate.Text)
                obj.Receipt_Post_Date = clsCommon.myCDate(lbldocumentdate.Text)
            Else
                obj.Receipt_Date = clsCommon.myCDate(lbldocinvoicedate.Text)
                obj.Receipt_Post_Date = clsCommon.myCDate(lbldocinvoicedate.Text)
            End If

            Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No='" & txtDocumentNo.Value & "'", Nothing))
            If clsCommon.myLen(strdocumentType) > 0 AndAlso clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal Then
                Dim strbancode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, Nothing))
                If clsCommon.myLen(strbancode) > 0 Then
                    Dim bankcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE as [Code] from TSPL_BANK_MASTEr where TSPL_bank_master.INACTIVE ='Active' and TSPL_BANK_MASTER.bank_type<>'S' and Bank_Code='" & clsCommon.myCstr(strbancode) & "' ", Nothing))
                    If clsCommon.myLen(bankcode) <= 0 Then
                        Throw New Exception("Please enter Bank Code into fixed parameter.")
                    End If
                Else
                    Throw New Exception("Please enter Bank Code into fixed parameter.")
                End If
                obj.Bank_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, Nothing))
                obj.Payment_Code = "NEFT"
                obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_Customer_Invoice_Head.ConvRate,1) as ConvRate, isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Receipt_Date), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Document_No ='" & txtDocumentNo.Value & "' )xx", Nothing))
                If clsCommon.myCdbl(obj.ConvRate) = 0 Then
                    obj.ConvRate = 1
                End If
                obj.ConvRateOld = obj.ConvRate
                obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select CURRENCY_CODE,ApplicableFrom ,Customer_Name from TSPL_Customer_Invoice_Head where Document_No ='" & txtDocumentNo.Value & "'", Nothing)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                    If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
                        obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                End If
            Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_RECEIPT_HEADER.ConvRate,TSPL_RECEIPT_HEADER.ConvRateOld ,TSPL_RECEIPT_HEADER.ApplicableFrom,TSPL_RECEIPT_HEADER.Bank_Code,TSPL_RECEIPT_HEADER.Payment_Code,TSPL_RECEIPT_HEADER.Customer_Name,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE,TSPL_RECEIPT_HEADER.CURRENCY_CODE FROM TSPL_RECEIPT_HEADER  WHERE TSPL_RECEIPT_HEADER.Receipt_No ='" & txtDocumentNo.Value & "'", Nothing)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                    obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
                    obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                    obj.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("BASE_CURRENCY_CODE"))
                    'obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                    obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_RECEIPT_HEADER.ConvRate,1) as ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.receipt_no=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Receipt_Date), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_RECEIPT_HEADER where Receipt_No ='" & txtDocumentNo.Value & "' )xx", Nothing))
                    obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
                    If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
                        obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
                    Else
                        obj.ApplicableFrom = Nothing
                    End If

                End If

            End If
            obj.Receipt_Type = "A"
            obj.Cust_Code = clsCommon.myCstr(strcustomer)
            Dim OutstandingAmt As Decimal = 0
            If clsCommon.myCdbl(lblBalAmt.Text) > clsCommon.myCdbl(lblreceiptamount.Text) Then
                obj.Receipt_Amount = clsCommon.myCdbl(lblreceiptamount.Text)
            Else
                obj.Receipt_Amount = clsCommon.myCdbl(lblBalAmt.Text)
            End If
            obj.UnApply_Amt = obj.Receipt_Amount
            obj.Balance_Amt = obj.Receipt_Amount
            obj.IsSalesmanType = "N"
            obj.SecurityDeposit = "N"
            obj.CFormRecd = "N"
            obj.CHECK_PRINT = 0
            obj.Applied_Receipt = clsCommon.myCstr(txtDocumentNo.Value)
            obj.IsApplyDocAuto = 1
            '' setof columns that are used in Receipt triggers to skip them
            obj.Set_Off_Date = clsCommon.GETSERVERDATE(Nothing)
            If chkForCardSale.Checked Then
                obj.isCardSale = 1
            Else
                obj.isCardSale = 0
            End If
            obj.ArrTr = New List(Of clsReceiptDettail)
            '============================Detail Section==============================

            For i As Integer = 0 To dgvReceipt.Rows.Count - 1
                Dim objTr As New clsReceiptDettail()
                If dgvReceipt.Rows(i).Cells(colApply).Value = "Yes" Then
                    objTr.Apply = "Y"
                    If clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Then
                        objTr.Receipt_Type = "D"
                    ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                        objTr.Receipt_Type = "C"
                    ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                        objTr.Receipt_Type = "F"
                    Else
                        objTr.Receipt_Type = "I"
                    End If
                    If dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "N" Then
                        objTr.TagType = "N"
                    ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "S" Then
                        objTr.TagType = "S"
                    ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "C" Then
                        objTr.TagType = "C"
                    End If
                    objTr.Document_No = dgvReceipt.Rows(i).Cells(colDocNo).Value
                    objTr.Document_Date = clsCommon.GetPrintDate(dgvReceipt.Rows(i).Cells(colDocDate).Value, "yyyy-MM-dd")
                    objTr.Original_Amt = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colOrgnlAmt).Value)
                    objTr.Pending_Balance = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colBalAmt).Value)
                    objTr.Applied_Amount = dgvReceipt.Rows(i).Cells(colAppliedAmt).Value
                    objTr.Adjustment_No = dgvReceipt.Rows(i).Cells(colAdjNo).Value
                    objTr.Adjustment_Cost = dgvReceipt.Rows(i).Cells(colAdjAmt).Value
                    'objTr.Comment = dgvReceipt.Rows(i).Cells(colComment).Value

                    If clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value) = 0 Then
                        objTr.ConvRateOld = 1
                    Else
                        objTr.ConvRateOld = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value)
                    End If
                    '' child cust code
                    objTr.Child_Cust_Code = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colChild_Cust_Code).Value)
                    obj.ArrTr.Add(objTr)
                End If
            Next


            'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
            '    clsCustomFieldGrid.GetData(obj.arrCustomFields, dgvReceipt, MyBase.ArrDetailFields, colDocNo)
            'End If



            '==================Detail Section Ends Here=======================


            obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(obj.Receipt_Amount * obj.ConvRate)


            If clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) <> CompairStringResult.Equal Then
                    Dim dt1 As DataTable
                    dt1 = clsRcptEntryHeader.GetExchangeDetailDt(strcustomer, Nothing)
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        obj.EXCHANGE_GAIN_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_GAIN_ACCOUNT"))
                        obj.EXCHANGE_LOSS_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_LOSS_ACCOUNT"))
                    Else
                        obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                        obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                    End If
                    Dim dtLastRate As DataTable
                    '' gather conv rate and amount of transaction to calculate exchange loss and gain
                    Dim strInvoiceNo As String = String.Empty
                    Dim lossorgainamount As Double = 0
                    Dim Totallossorgainamount As Double = 0

                    Dim InvoiceType As String = ""
                    For i As Integer = 0 To dgvReceipt.Rows.Count - 1
                        strInvoiceNo = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocNo).Value)
                        InvoiceType = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocType).Value)
                        dtLastRate = clsRcptEntryHeader.GetExchangeRateAmount(strInvoiceNo, obj.Receipt_Date, Nothing)
                        If clsCommon.CompairString(InvoiceType, "Credit Note") = CompairStringResult.Equal Then
                            lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate") * -1
                        Else
                            lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate")
                        End If

                        Totallossorgainamount = Totallossorgainamount + lossorgainamount
                    Next



                    Dim diff As Double = 0.0
                    If Totallossorgainamount <> 0 Then
                        diff = obj.RECEIVED_AMOUNT_BASE_CURRENCY - Totallossorgainamount
                    End If

                    If diff = 0 Then
                        obj.EXCHANGE_LOSS_AMT = 0
                        obj.EXCHANGE_GAIN_AMT = 0
                    ElseIf diff < 0 Then
                        If clsCommon.myLen(obj.EXCHANGE_LOSS_ACCOUNT) = 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Exchange Loss Account not defined.", Me.Text)
                            Return False
                        End If
                        obj.EXCHANGE_LOSS_AMT = -diff
                        obj.EXCHANGE_GAIN_AMT = 0
                    Else
                        If clsCommon.myLen(obj.EXCHANGE_GAIN_ACCOUNT) = 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Exchange Gain Account not defined.", Me.Text)
                            Return False
                        End If
                        obj.EXCHANGE_LOSS_AMT = 0
                        obj.EXCHANGE_GAIN_AMT = diff
                    End If
                End If
            Else

                obj.EXCHANGE_LOSS_AMT = 0
                obj.EXCHANGE_GAIN_AMT = 0
                obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                obj.EXCHANGE_LOSS_ACCOUNT = Nothing
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                obj.SaveData(obj, True, trans)
                clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
            obj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        ResetonDate()
    End Sub

    Private Sub txtToDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtToDate.Validating
        ResetonDate()
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        ''Dim strQry As String = "select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER "
        Dim strQry As String = "select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code in (select distinct Cust_Group_Code from tspl_customer_master where " & IIf(chkVSPonly.Checked = True, " isnull(customer_class,'')='VSP'", " isnull(customer_class,'')<>'VSP'") & "   and CSA_Type IN ('N','') ) order by Cust_Group_Code "
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("cUSTOMERGrpSelector@SETOFF", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
        LoadCustomer()
    End Sub

    Private Sub chkVSPonly_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVSPonly.ToggleStateChanged

        LoadCustomer()
    End Sub

    Private Sub txtCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles txtCustomerCategory._My_Click
        Dim strQry As String = "select CUST_CATEGORY_CODE AS Code,CUST_CATEGORY_DESC as Name from TSPL_CUSTOMER_CATEGORY_MASTER where CUST_CATEGORY_CODE in (select distinct CUST_CATEGORY_CODE from tspl_customer_master where " & IIf(chkVSPonly.Checked = True, " isnull(customer_class,'')='VSP'", " isnull(customer_class,'')<>'VSP'") & " and CSA_Type IN ('N','') ) order by CUST_CATEGORY_CODE "
        txtCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCategorySelector@SETOFF", strQry, "Code", "Name", txtCustomerCategory.arrValueMember, txtCustomerCategory.arrDispalyMember)
        LoadCustomer()
    End Sub

End Class