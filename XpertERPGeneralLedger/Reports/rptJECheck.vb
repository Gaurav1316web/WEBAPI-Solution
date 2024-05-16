Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Imports System.IO

Public Class rptJECheck
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim strqry As String
    Dim dt As DataTable = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag

    End Sub

    Private Sub GLTransReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        GC.Collect()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gv1.EnableFiltering = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        LoadDataNew(False)
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        RadPanel1.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
    End Sub

    Private Sub LoadDataNew(ByVal IsPrint As Boolean)
        Try
            strqry = "select TrancationType,DocCode, GLSourceCode,convert(varchar, DocDate,103) as DocDate,Voucher_No,Source_Code,TSPL_GL_SOURCECODE.SourceDescription,Amount from (" & Environment.NewLine & _
            " select max(TrancationType) as TrancationType,max(DocCode) as DocCode, GLSourceCode,max(DocDate) as DocDate,max(Voucher_No) as Voucher_No,Source_Code,sum(case when Chk=1 then Amount else 0 end) as Amount from (" & Environment.NewLine & _
            " select * from (" & Environment.NewLine & _
            " select Reverse_Code as DocCode,Reverse_Code as GLSourceCode,convert(datetime, Reversal_Date) as DocDate,'' as Voucher_No,'RV-TA' as Source_Code,Amount,1 as Chk,'Bank Reverse Entry' as TrancationType " & Environment.NewLine & _
            " from TSPL_BANK_REVERSE where Post='P'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Transfer_No as DocCode,Transfer_No as GLSourceCode,convert(datetime, Transfer_Date) as DocDate,'' as Voucher_No,'BK-TF' as Source_Code,Transfer_Amount as  Amount,1 as Chk,'Contra Vouchers' as TrancationType  from TSPL_BANK_TRANSFER where Post='P'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Receipt_No as DocCode,Receipt_No as GLSourceCode,convert(datetime, Receipt_Date) as DocDate,'' as Voucher_No," & Environment.NewLine & _
            " case when Receipt_Type='O' then 'AR-OA' else  " & Environment.NewLine & _
            " case when Receipt_Type='A' then 'AR-DC' else   " & Environment.NewLine & _
            " case when Receipt_Type='F' then 'AR-RF' else  " & Environment.NewLine & _
            " case when Receipt_Type='P' then 'AR-PI' else  " & Environment.NewLine & _
            " case when Receipt_Type='R' then 'AR-PY' else  " & Environment.NewLine & _
            " case when Receipt_Type='U' then '' else  " & Environment.NewLine & _
            " case when Receipt_Type='M' then 'AR-MI' else  " & Environment.NewLine & _
            " case when Receipt_Type='S' then 'AR-MR' else  '' end end end end end end end end as Source_Code,Receipt_Amount as  Amount,1 as Chk,'Receipt' as TrancationType from TSPL_RECEIPT_HEADER where Posted='Y'" & Environment.NewLine & _
            " and Receipt_Type<>'A' and  2= case when Receipt_Type<>'A' then 2 else case when exists (select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.ApplyBrachAccounting + "' and type='" + clsFixedParameterType.ApplyBrachAccounting + "' and Description=1) then 2 else 3 end end " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Adjustment_No as DocCode,Adjustment_No as GLSourceCode,convert(datetime, Adjustment_Date) as DocDate,'' as Voucher_No,'AR-AD' as Source_Code,Adjustment_Amount as  Amount,1 as Chk,'Receipt adjustment entry' as TrancationType from TSPL_Receipt_Adjustment_Header where Is_Post='Y'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Document_No as DocCode,Document_No as GLSourceCode,convert(datetime, Document_Date) as DocDate,'' as Voucher_No,case when Document_Type='C' then 'AR-CR' else case when Document_Type='D' then 'AR-DN' else case when Document_Type='I' then 'AR-IN' else '' end end end as Source_Code,Document_Total as Amount,1 as Chk,'AR Invoice' as TrancationType from TSPL_Customer_Invoice_Head where len(ISNULL(Against_MCC_Material_Sale_Return,''))<=0  and len(ISNULL(Against_Sale_No,''))<=0  and len(ISNULL(Against_Sale_Return_No,''))<=0  and len(ISNULL(Against_Service_Visit_Code,''))<=0  and len(ISNULL(Against_VCGL,''))<=0  and len(ISNULL(AgainstScrap,''))<=0  and len(ISNULL(RefDocNo,''))<=0  and Status=1 " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Document_No as DocCode,Document_No as GLSourceCode,convert(datetime, Posting_Date) as DocDate,'' as Voucher_No,case when Document_Type='C' then 'AP-CN' else case when Document_Type='D' then 'AP-DN' else case when Document_Type='I' then 'AP-IN' else '' end end end as Source_Code,Document_Total as Amount,1 as Chk,case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 'Ap invoice TDS' else case when Invoice_Type = 'VS' then  'Vendor service charge' else 'AP Invoice' end end as TrancationType from TSPL_VENDOR_INVOICE_HEAD " & Environment.NewLine & _
            " where" & Environment.NewLine & _
            " len(ISNULL(RefDocNo,''))<=0 and len(ISNULL(Against_Acquisition,''))<=0 and len(ISNULL(Against_Asset_Work,''))<=0 and len(ISNULL(Against_BulkMillkPurchaseInvoice_No,''))<=0 and len(ISNULL(Against_MillkPurchaseInvoice_No,''))<=0 and len(ISNULL(Against_POInvoice_No,''))<=0 and len(ISNULL(Against_POInvoice_No,''))<=0 and len(ISNULL(Against_PurchaseReturn_No,''))<=0 and len(ISNULL(Against_VCGL,''))<=0  and len(ISNULL(Against_VSPItemIssue_No,''))<=0 and Posting_Date is not null and  Invoice_Type<>'VC'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Adjustment_No as DocCode,Adjustment_No as GLSourceCode,convert(datetime, Adjustment_Date) as DocDate,'' as Voucher_No,'AP-AD' as Source_Code,Adjustment_Amount as Amount,1 as Chk,'Payment adjustment entry' as TrancationType from TSPL_PAYMENT_ADJUSTMENT_HEADER where Is_Post='Y'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select  TSPL_VCGL_Head.Document_No as DocCode, " & Environment.NewLine & _
            " case when TSPL_VCGL_Head.Document_Type='V' then TSPL_VENDOR_INVOICE_HEAD.Document_No else case when TSPL_VCGL_Head.Document_Type='C'  then TSPL_Customer_Invoice_Head.Document_No " & Environment.NewLine & _
            " else ''  end end as GLSourceCode," & Environment.NewLine & _
            " convert(datetime, TSPL_VCGL_Head.Document_Date) as DocDate,'' as Voucher_No," & Environment.NewLine & _
            " case when TSPL_VCGL_Head.Document_Type='V'  then " & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' else" & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN'  else " & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'AP-IN' " & Environment.NewLine & _
            " 	else '' end end end" & Environment.NewLine & _
            " else case when TSPL_VCGL_Head.Document_Type='C' then " & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'AR-DN' else" & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='C' then 'AR-CR' else" & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='I' then 'AR-IN' " & Environment.NewLine & _
            " 	else '' end end end  end end " & Environment.NewLine & _
            " as Source_Code" & Environment.NewLine & _
            " ,Amount,1 as Chk,'Vendor/Customer Journal Entry(H)' as TrancationType  from TSPL_VCGL_Head" & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='VC' and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code= TSPL_VCGL_Head.VC_Code and TSPL_VCGL_Head.Document_Type='V'" & Environment.NewLine & _
            " left outer join  TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_VCGL=TSPL_VCGL_Head.Document_No and TSPL_Customer_Invoice_Head.Customer_Code= TSPL_VCGL_Head.VC_Code and TSPL_VCGL_Head.Document_Type='C'" & Environment.NewLine & _
            "                                                     where TSPL_VCGL_Head.Status = 1" & Environment.NewLine & _
            "                                                     union all" & Environment.NewLine & _
            " select  TSPL_VCGL_Detail.Document_No as DocCode, " & Environment.NewLine & _
            " case when TSPL_VCGL_Detail.Row_Type='Vendor' then TSPL_VENDOR_INVOICE_HEAD.Document_No else case when TSPL_VCGL_Detail.Row_Type='Customer'  then TSPL_Customer_Invoice_Head.Document_No " & Environment.NewLine & _
            " else ''  end end as GLSourceCode," & Environment.NewLine & _
            " convert(datetime, TSPL_VCGL_Head.Document_Date) as DocDate,'' as Voucher_No," & Environment.NewLine & _
            " case when TSPL_VCGL_Detail.Row_Type='Vendor'  then " & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' else" & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN'  else " & Environment.NewLine & _
            " 	case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'AP-IN' " & Environment.NewLine & _
            " 	else '' end end end" & Environment.NewLine & _
            " else case when TSPL_VCGL_Detail.Row_Type='Customer' then " & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'AR-DN' else" & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='C' then 'AR-CR' else" & Environment.NewLine & _
            " 	case when TSPL_Customer_Invoice_Head.Document_Type='I' then 'AR-IN' " & Environment.NewLine & _
            " 	else '' end end end  end end " & Environment.NewLine & _
            " as Source_Code" & Environment.NewLine & _
            " ,(Dr_Amount+Cr_Amount) as Amount,1 as Chk,'Vendor/Customer Journal Entry(D)' as TrancationType  from " & Environment.NewLine & _
            " TSPL_VCGL_Detail " & Environment.NewLine & _
            " left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No" & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Detail.Document_No and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='VC' and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code= TSPL_VCGL_Detail.VCGL_Code and TSPL_VCGL_Detail.Row_Type='Vendor'" & Environment.NewLine & _
            " left outer join  TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_VCGL=TSPL_VCGL_Detail.Document_No and TSPL_Customer_Invoice_Head.Customer_Code= TSPL_VCGL_Detail.VCGL_Code and TSPL_VCGL_Detail.Row_Type='Customer'" & Environment.NewLine & _
            " where TSPL_VCGL_Head.Status = 1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Document_No as DocCode,Document_No as GLSourceCode,convert(datetime, Document_Date) as DocDate,'' as Voucher_No,'MM-TF' as Source_Code,DOC_Total_Amt as Amount,1 as Chk,'Bank Reverse Entry' as TrancationType from TSPL_TRANSFER_ORDER_HEAD where Status='1'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as GLSourceCode,convert(datetime,max( TSPL_ADJUSTMENT_HEADER.Adjustment_Date)) as DocDate,'' as Voucher_No,'IC-AD' as Source_Code,sum( TSPL_ADJUSTMENT_DETAIL.Item_Cost) as Amount,1 as Chk,'Store Adjustment' as TrancationType " & Environment.NewLine & _
            " from TSPL_ADJUSTMENT_DETAIL" & Environment.NewLine & _
            " left outer join TSPL_ADJUSTMENT_HEADER  on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No" & Environment.NewLine & _
            " where  TSPL_ADJUSTMENT_HEADER.Posted='Y' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('CD','CI','BD','BI') and TSPL_ADJUSTMENT_DETAIL.Item_Cost>0 " & Environment.NewLine & _
            " and exists (select 1 from TSPL_INV_PARAMETERS where IsCreateJEForStoreAdj=1)" & Environment.NewLine & _
            " group by TSPL_ADJUSTMENT_HEADER.Adjustment_No" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_Transfer_RETURN.Document_No as DocCode,TSPL_Transfer_RETURN.Document_No as GLSourceCode,convert(datetime, TSPL_Transfer_RETURN.Document_Date) as DocDate,'' as Voucher_No,'SN-RT' as Source_Code,TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as Amount,1 as Chk,'Transfer Return' as TrancationType" & Environment.NewLine & _
            " from  TSPL_Transfer_RETURN" & Environment.NewLine & _
            " left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_RETURN.Transfer_No" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SRN_HEAD.SRN_No as DocCode,TSPL_SRN_HEAD.SRN_No as GLSourceCode,convert(datetime, TSPL_SRN_HEAD.SRN_Date) as DocDate,'' as Voucher_No,'PO-RC' as Source_Code,TSPL_SRN_HEAD.SRN_Total_Amt as Amount,1 as Chk,case when Document_Type='MT' then 'Merchant SRN' else  'SRN' end as TrancationType " & _
            " from TSPL_SRN_HEAD where Status=1 " & _
            " and exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0') " & _
            " and len(isnull( TSPL_SRN_HEAD.Against_RGP,''))<=0 and is_RGP_Non_Inventory=0" & _
            " union all " & _
            " select TSPL_SRN_HEAD.SRN_No as DocCode,TSPL_SRN_HEAD.SRN_No as GLSourceCode,convert(datetime, TSPL_SRN_HEAD.SRN_Date) as DocDate,'' as Voucher_No,'SR-RG' as Source_Code,TSPL_SRN_HEAD.SRN_Total_Amt as Amount,1 as Chk,'Job work' as TrancationType " & _
            " from TSPL_SRN_HEAD " & _
            " inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_SRN_HEAD.Against_RGP" & _
            " where TSPL_SRN_HEAD.Status=1  and len(isnull( TSPL_SRN_HEAD.Against_RGP,''))>0  " & _
            " and exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0') " & _
            " union all" & Environment.NewLine & _
            " select TSPL_SRN_RETURN.Document_No as DocCode,TSPL_SRN_RETURN.Document_No as GLSourceCode,convert(datetime, TSPL_SRN_RETURN.Document_Date) as DocDate,'' as Voucher_No,'SN-RT' as Source_Code,TSPL_SRN_HEAD.SRN_Total_Amt as Amount,1 as Chk,'SRN Return' as TrancationType " & Environment.NewLine & _
            " from TSPL_SRN_RETURN " & Environment.NewLine & _
            " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_RETURN.SRN_No where exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0')" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_PI_HEAD.PI_No as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_PI_HEAD.PI_Date) as DocDate,'' as Voucher_No,'AP-IN' as Source_Code,TSPL_PI_HEAD.PI_Total_Amt as Amount,1 as Chk,'Purchase invoice' as TrancationType " & Environment.NewLine & _
            " from TSPL_PI_HEAD " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No= TSPL_PI_HEAD.PI_No" & Environment.NewLine & _
            " where TSPL_PI_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_PR_HEAD.PR_No as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_PR_HEAD.PR_Date) as DocDate,'' as Voucher_No,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then  'AP-DN' else '' end end as Source_Code,TSPL_PR_HEAD.PR_Total_Amt as Amount,1 as Chk,'Rebate/Discount' as TrancationType " & Environment.NewLine & _
            " from TSPL_PR_HEAD " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No= TSPL_PR_HEAD.PR_No" & Environment.NewLine & _
            " where TSPL_PR_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SCRAPINVOICE_HEAD.invoice_No as DocCode,TSPL_Customer_Invoice_Head.Document_No as GLSourceCode,convert(datetime,TSPL_SCRAPINVOICE_HEAD.shipment_Date) as DocDate,'' as Voucher_No,'AR-IN' as Source_Code,TSPL_SCRAPINVOICE_HEAD.Doc_Amt,1 as Chk,'Misc sale' as TrancationType  " & Environment.NewLine & _
            " from TSPL_SCRAPINVOICE_HEAD " & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.invoice_No" & Environment.NewLine & _
            " where TSPL_SCRAPINVOICE_HEAD.ispost=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_ACQUISITION_HEAD.Acquisition_Code as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_ACQUISITION_HEAD.Acquisition_Date) as DocDate,'' as Voucher_No,'AP-IN' as Source_Code,TSPL_ACQUISITION_HEAD.Net_Amt as Amount,1 as Chk,'Acquisition entry' as TrancationType " & Environment.NewLine & _
            " from TSPL_ACQUISITION_HEAD " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition= TSPL_ACQUISITION_HEAD.Acquisition_Code" & Environment.NewLine & _
            " where TSPL_ACQUISITION_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Document_Code as DocCode,Document_Code as GLSourceCode,convert(datetime, Document_Date) as DocDate,'' as Voucher_No,'AM-DP' as Source_Code,Dep_Amount_Tax as Amount,1 as Chk,'Asset Depreciation' as TrancationType from TSPL_ASSET_DEPRECIATION where Is_Permanent='YES'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Document_No as DocCode,Document_No as GLSourceCode,convert(datetime, Document_Date) as DocDate,'' as Voucher_No,'AM-DE' as Source_Code,Doc_Amt as Amount,1 as Chk,'Asset Depreciation' as TrancationType from TSPL_ASSET_SCRAP_HEAD where Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_ASSET_WORK_HEAD.Document_Code as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_ASSET_WORK_HEAD.Document_Date) as DocDate,'' as Voucher_No,'AP-IN' as Source_Code,TSPL_ASSET_WORK_HEAD.Net_Amt as Amount,1 as Chk,'Asset work expense' as TrancationType " & Environment.NewLine & _
            " from TSPL_ASSET_WORK_HEAD " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work=TSPL_ASSET_WORK_HEAD.Document_Code" & Environment.NewLine & _
            " where TSPL_ASSET_WORK_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select SALARY_GENERATION_CODE as DocCode,SALARY_GENERATION_CODE as GLSourceCode,convert(datetime, GENERATE_DATE) as DocDate,'' as Voucher_No,'PL-JE' as Source_Code,0 as Amount,1 as Chk,'Salary Generation' as TrancationType " & Environment.NewLine & _
            " from TSPL_GENERATE_SALARY where POSTED=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Chalan_NO as DocCode,Chalan_NO as GLSourceCode,convert(datetime, Dispatch_Date) as DocDate,'' as Voucher_No,'DI-CH' as Source_Code,Amount,1 as Chk,'Tanker Dispatch' as TrancationType from tspl_mcc_dispatch_challan where isPosted=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE) as DocDate,'' as Voucher_No,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then  'AP-IN' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else ''  end end as Source_Code,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT as Amount,1 as Chk,'Milk Purchase Invoice' as TrancationType from TSPL_MILK_PURCHASE_INVOICE_HEAD " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE" & Environment.NewLine & _
            " where TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as DocCode,TSPL_Customer_Invoice_Head.Document_No as GLSourceCode,convert(datetime, TSPL_SD_SALE_INVOICE_HEAD.Document_Date) as DocDate,'' as Voucher_No,'AR-IN' as Source_Code,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Amount,1 as Chk," & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='CSA' then 'CSA Sale Invoice/Sale Patti' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Material Sale' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS' then  'Fresh Invoice' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' then 'Product Invoice' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type='EX'  then 'Export Sales Invoice' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type='MT' then 'Merchant Sales Invoice'  else  " & Environment.NewLine & _
            " '' end end end end end end  as TrancationType from TSPL_SD_SALE_INVOICE_HEAD " & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & Environment.NewLine & _
            " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type in ('CSA','MCC','FS','PS','EXP') " & Environment.NewLine & _
            " and TSPL_SD_SALE_INVOICE_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SD_SALE_RETURN_HEAD.Document_Code as DocCode,TSPL_Customer_Invoice_Head.Document_No as GLSourceCode,convert(datetime, TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,'' as Voucher_No,'AR-CR' as Source_Code,TSPL_SD_SALE_RETURN_HEAD.Total_Amt as Amount,1 as Chk," & Environment.NewLine & _
            " case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'MCC' then  'MCC Material Sale Return' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'FS' then 'Fresh Sale Return' else " & Environment.NewLine & _
            " case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'PS' then 'Product Sales Return' else " & Environment.NewLine & _
            " case when  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_RETURN_HEAD.Document_Type='EX' then 'Export Sales Return' else  " & Environment.NewLine & _
            " case when  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_RETURN_HEAD.Document_Type='MT' then 'Merchant Sales Return' else  " & Environment.NewLine & _
            " '' end end end end end as TrancationType " & Environment.NewLine & _
            " from TSPL_SD_SALE_RETURN_HEAD" & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on " & Environment.NewLine & _
            " (case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'MCC' then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return else case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type in('FS','PS','EXP') then Against_Sale_Return_No else '' end end)=TSPL_SD_SALE_RETURN_HEAD.Document_Code" & Environment.NewLine & _
            " where  TSPL_SD_SALE_RETURN_HEAD.Trans_Type in ('MCC','FS','PS','EXP') and TSPL_SD_SALE_RETURN_HEAD.Status=1 " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_Bulk_MILK_SRN.SRN_NO as DocCode,TSPL_Bulk_MILK_SRN.SRN_NO as GLSourceCode,convert(datetime, TSPL_Bulk_MILK_SRN.SRN_Date) as DocDate,'' as Voucher_No,'BM-SR' as Source_Code,TSPL_Bulk_MILK_SRN.Actual_Amount as Amount,1 as Chk,'Bulk Milk SRN' as TrancationType from TSPL_Bulk_MILK_SRN where TSPL_Bulk_MILK_SRN.isPosted=1  " & Environment.NewLine & _
            " and exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0') " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO as DocCode,TSPL_VENDOR_INVOICE_HEAD.Document_No as GLSourceCode,convert(datetime, TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE) as DocDate,'' as Voucher_No,'AP-IN' as Source_Code,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_AMT as Amount,1 as Chk,'Bulk Milk Purchase Invoice' as TrancationType " & Environment.NewLine & _
            " from TSPL_BULK_MILK_PURCHASE_INVOICE_head " & Environment.NewLine & _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No=TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO" & Environment.NewLine & _
            " where TSPL_BULK_MILK_PURCHASE_INVOICE_head.isPosted=1" & Environment.NewLine
            '" union all" & Environment.NewLine
            '" select TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as DocCode,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as GLSourceCode,convert(datetime, TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date) as DocDate,'' as Voucher_No,'MT-IN' as Source_Code,0 as Amount,1 as Chk,'Bulk Milk Purchase Invoice' as TrancationType " & Environment.NewLine & _
            '" from TSPL_MILK_TRANSFER_IN " & Environment.NewLine & _
            '" where TSPL_MILK_TRANSFER_IN.isPosted=1" & Environment.NewLine & _
            strqry += " union all" & Environment.NewLine & _
            " select TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO as DocCode,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO as GLSourceCode,convert(datetime, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date) as DocDate,'' as Voucher_No,'SR-RT' as Source_Code,TSPL_Bulk_MILK_SRN.Actual_Amount as Amount,1 as Chk,'Bulk Milk SRN Return' as TrancationType " & Environment.NewLine & _
            " from TSPL_Bulk_Milk_SRN_Return " & Environment.NewLine & _
            " left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_Bulk_Milk_SRN_Return.SRN_NO where exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0') " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_Dispatch_BulkSale.Document_No as DocCode,TSPL_Dispatch_BulkSale.Document_No as GLSourceCode,convert(datetime, TSPL_Dispatch_BulkSale.Document_Date) as DocDate,'' as Voucher_No,'DS-BS' as Source_Code,Total_Amt as Amount,1 as Chk,'Bulk Dispatch' as TrancationType " & Environment.NewLine & _
            " from TSPL_Dispatch_BulkSale " & Environment.NewLine & _
            " where TSPL_Dispatch_BulkSale.Posted=1 and exists( select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SkipCogsEntry + "' and type='" + clsFixedParameterType.SkipCogsEntry + "' and Description='0' ) " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_INVOICE_MASTER_BULKSALE.Document_No as DocCode,TSPL_Customer_Invoice_Head.Document_No as GLSourceCode,convert(datetime, TSPL_INVOICE_MASTER_BULKSALE.Document_Date) as DocDate,'' as Voucher_No,'AR-IN' as Source_Code,TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as Amount,1 as Chk,'Bulk Invoice' as TrancationType " & Environment.NewLine & _
            " from TSPL_INVOICE_MASTER_BULKSALE" & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No" & Environment.NewLine & _
            " where TSPL_INVOICE_MASTER_BULKSALE.Posted=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_Dispatch_BulkSale_Trade.Document_No as DocCode,TSPL_Dispatch_BulkSale_Trade.Document_No as GLSourceCode,convert(datetime, TSPL_Dispatch_BulkSale_Trade.Document_Date) as DocDate,'' as Voucher_No,'DS-BT' as Source_Code,Total_Amt as Amount,1 as Chk,'Bulk Dispatch Trade' as TrancationType " & Environment.NewLine & _
            " from TSPL_Dispatch_BulkSale_Trade " & Environment.NewLine & _
            " where TSPL_Dispatch_BulkSale_Trade.Posted=1 and  exists( select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0' ) " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as DocCode,TSPL_Customer_Invoice_Head.Document_No as GLSourceCode" & Environment.NewLine & _
            " ,convert(datetime, TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date) as DocDate" & Environment.NewLine & _
            " ,'' as Voucher_No,'AR-CR' as Source_Code,Total_Amt as Amount,1 as Chk,'Bulk Sales Retrun(Bulk Invoice)' as TrancationType  from TSPL_SALE_RETURN_MASTER_BULKSALE " & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_Return_No=TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No" & Environment.NewLine & _
            " where Posted=1 and Against='Bulk Invoice'" & Environment.NewLine & _
            "  union all" & Environment.NewLine & _
            " select TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as DocCode,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as GLSourceCode" & Environment.NewLine & _
            " ,convert(datetime, TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date) as DocDate" & Environment.NewLine & _
            " ,'' as Voucher_No,'DS-BR' as Source_Code,Total_Amt as Amount,1 as Chk,'Bulk Sales Retrun(Dispatch)' as TrancationType  from TSPL_SALE_RETURN_MASTER_BULKSALE " & Environment.NewLine & _
            " where Posted=1 and Against<>'Bulk Invoice'" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as DocCode,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as GLSourceCode" & Environment.NewLine & _
            " ,convert(datetime, TSPL_CSA_TRANSFER_HEAD.Transfer_Date) as DocDate" & Environment.NewLine & _
            " ,'' as Voucher_No,'CS-TR' as Source_Code,Document_Amount as Amount,1 as Chk,'CSA Transfer' as TrancationType  from TSPL_CSA_TRANSFER_HEAD " & Environment.NewLine & _
            " where Status=1  " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SD_SALE_RETURN_HEAD.Document_Code as DocCode,TSPL_SD_SALE_RETURN_HEAD.Document_Code as GLSourceCode,convert(datetime, TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,'' as Voucher_No,'CS-RC' as Source_Code,TSPL_SD_SALE_RETURN_HEAD.Total_Amt as Amount,1 as Chk,'MCC Material Sale' as TrancationType from TSPL_SD_SALE_RETURN_HEAD " & Environment.NewLine & _
            " where  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='CSA' and TSPL_SD_SALE_RETURN_HEAD.Status=1" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select TSPL_SD_SHIPMENT_HEAD.Document_Code as DocCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as GLSourceCode,convert(datetime, TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,'' as Voucher_No,'DS-FS' as Source_Code,TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount,1 as Chk,case when TSPL_SD_SHIPMENT_HEAD.Trans_Type ='FS' then 'Fresh Dispatch' else case when TSPL_SD_SHIPMENT_HEAD.Trans_Type ='PS' then 'Product Dispatch' else '' end end as TrancationType " & Environment.NewLine & _
            " from TSPL_SD_SHIPMENT_HEAD " & Environment.NewLine & _
            " where  TSPL_SD_SHIPMENT_HEAD.Trans_Type in ('FS','PS') and TSPL_SD_SHIPMENT_HEAD.Status=1 and not exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.SkipCogsEntry + "' and type='" + clsFixedParameterType.SkipCogsEntry + "' and Description=1)" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select RGP_No as DocCode,RGP_No as GLSourceCode,convert(datetime, RGP_Date,103) as DocDate,'' as Voucher_No,'MR-JW' as Source_Code,Document_Amount as  Amount,1 as Chk,'Milk RGP' as TrancationType " & Environment.NewLine & _
            " from TSPL_Milk_RGP_Head where Status=1 and exists ( select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0' ) " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select SRN_NO as DocCode,SRN_NO as GLSourceCode,convert(datetime, SRN_Date,103) as DocDate,'' as Voucher_No,'BM-SR' as Source_Code,Amount as  Amount,1 as Chk,'Milk JobWork SRN' as TrancationType  from tspl_Job_milk_srn where isPosted=1 and exists(select 1 from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.AllowPurchaseAccounting + "' and type='" + clsFixedParameterType.AllowPurchaseAccounting + "' and Description='0')" & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " select Payment_No as DocCode,Payment_No as GLSourceCode,convert(datetime, Payment_Date) as DocDate,'' as Voucher_No," & Environment.NewLine & _
            " case when Payment_Type='MI' then 'AP-MI' else  'AP-PY' end as Source_Code,Payment_Amount as  Amount,1 as Chk,'Payment' as TrancationType " & Environment.NewLine & _
            " from TSPL_PAYMENT_HEADER where Posted='1' and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' and " & Environment.NewLine & _
            " 2 = case when TSPL_PAYMENT_HEADER.Payment_Type<>'AD' then 2 else case when  exists(select 1 from TSPL_FIXED_PARAMETER where Type='" + clsFixedParameterType.ApplyBrachAccounting + "' and code='" + clsFixedParameterCode.ApplyBrachAccounting + "' and Description='1') then 2 else 3 end end " & Environment.NewLine & _
            " ) xx where DocDate>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and DocDate<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" & Environment.NewLine
            If txtLocationSeg.arrValueMember IsNot Nothing AndAlso txtLocationSeg.arrValueMember.Count > 0 Then
                strqry += " and Source_Code in (" + clsCommon.GetMulcallString(txtLocationSeg.arrValueMember) + ")"
                End If
            strqry += "Union all" & Environment.NewLine & _
            " select '' as DocCode, Source_Doc_No as GLSourceCode,Voucher_Date as DocDate,Voucher_No,Source_Code,Total_Debit_Amt as Amount,0 as Chk,'' as TrancationType  from TSPL_JOURNAL_MASTER  where 2=2" & Environment.NewLine & _
             " )xxxx group by GLSourceCode,Source_Code having sum(Chk)>0 " & Environment.NewLine & _
             " )xxxxx left outer join TSPL_GL_SOURCECODE on TSPL_GL_SOURCECODE.SourceCode=xxxxx.Source_Code where len(isnull( Voucher_No,'') )<=0 order by xxxxx.DocDate   "
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing AndAlso dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
                End If
            gv1.DataSource = dt
            SetGridFormation()
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
        dt = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Sub SetGridFormation()
        gv1.GroupDescriptors.Clear()
        For jj As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(jj).ReadOnly = True
            gv1.Columns(jj).IsVisible = False
        Next
        gv1.Columns("TrancationType").IsVisible = True
        gv1.Columns("TrancationType").Width = 150
        gv1.Columns("TrancationType").HeaderText = "Trancation Type"

        gv1.Columns("DocCode").IsVisible = True
        gv1.Columns("DocCode").Width = 150
        gv1.Columns("DocCode").HeaderText = "Trancation Code"

        gv1.Columns("GLSourceCode").IsVisible = True
        gv1.Columns("GLSourceCode").Width = 150
        gv1.Columns("GLSourceCode").HeaderText = "GL Source Code"

        gv1.Columns("DocDate").IsVisible = True
        gv1.Columns("DocDate").Width = 100
        gv1.Columns("DocDate").HeaderText = "Trancation Date"

        gv1.Columns("Voucher_No").IsVisible = True
        gv1.Columns("Voucher_No").Width = 150
        gv1.Columns("Voucher_No").HeaderText = "Voucher No"

        gv1.Columns("Source_Code").IsVisible = True
        gv1.Columns("Source_Code").Width = 80
        gv1.Columns("Source_Code").HeaderText = "Source Code"

        gv1.Columns("SourceDescription").IsVisible = True
        gv1.Columns("SourceDescription").Width = 80
        gv1.Columns("SourceDescription").HeaderText = "Source Description"

        gv1.Columns("Amount").IsVisible = True
        gv1.Columns("Amount").Width = 200
        gv1.Columns("Amount").HeaderText = "Amount"

         
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim total As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(total)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
        EnableDisableControls(False)
    End Sub

    Private Sub GLTransReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.JECheckSystem & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If txtLocationSeg.arrDispalyMember IsNot Nothing AndAlso txtLocationSeg.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Source Code : " + clsCommon.GetMulcallStringWithComma(txtLocationSeg.arrDispalyMember))
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("JE Check System", gv1, arrHeader, "JECheck", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocationSeg__My_Click(sender As Object, e As EventArgs) Handles txtLocationSeg._My_Click
        strqry = "select SourceCode as Code ,SourceDescription as Name  from TSPL_GL_SOURCECODE"
        txtLocationSeg.arrValueMember = clsCommon.ShowMultipleSelectForm("SC@JECheck", strqry, "Code", "Name", txtLocationSeg.arrValueMember, txtLocationSeg.arrDispalyMember)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
