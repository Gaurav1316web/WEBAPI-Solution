Imports common
Imports XpertERPHRandPayroll

Public Enum EnumTransType
    PurchaseIndent
    ScarSale
    JournalEntry
    RcptEntry
    PymntEntry
    BankTransfer
    LoadOut
    SaleInvoice
    PurchaseInvoice
    SRN
    ICAdj
    MMTrans
    BankRvrs
    RcptADJ
    APInvoice
    SaleOrder
    SaleReturn
    SaleReturnInter
    ARInvoice
    PurchaseReturn
    ScrapInvoice
    ScrapShipment
    IssueReturnTransfer
    NRGP
    RGP
    SDShipment
    SDSaleReturn
    SaleInvoiceDemo
    Assemblies
    WareHouseBreakage
    VCGL
    APInvoiceTDS
    SaleQuotation
    GLAccount
    frmSalesmanTarget
    PuchaseOrder
    ExpiredItemEntry
    MilkSRN
    MilkPI
    productionEntry
    transfer
    SDCSATrans
    SDCSASale
    SDCSADO
    Bank_Guarantee_Master
    RICE_PROC
    RICE_MIX
    PP_ISSUE
    MCC_Material
    EXPORT_SO
    EXPORT_QUOTATION
    EXPORT_PROFORMA
    DispChallan
    MilkTransferIn
    Fresh_Sale
    Sale_Return
    Product_Invoice
    Product_Return_Sale
    CSA_Invoice
    EXPORT_Invoice
    Bulk_Invoice
    Bulk_Return
    CrateReceived
    InvoiceFreshSale
    MCCMaterialFrm
    SD_CSATRANS_RETURN
    MCCMaterialSaleReturn
    Export_Commercial_Inv
    PP_STDN
    BulkSRNTrade
    DispatchBSTrade
    DispatchBSTradeReturn
    DispatchBS
    BulkSRN
    PRD_STG_PROC
    VSPTRAN
    PROD_ENTRY
    Export_Sale_Return
    FS_SH
    PS_SH
    MCC_MSRN
    MT_Sale_Order
    MT_Proforma
    MT_Comm_Inv
    MT_Sale_Inv
    MT_Sale_Ret
    MT_Sale_Qu
    MilkReceipt
    Mcc_transfer
    Bulk_Purchase
    Bulk_Purchase_Return
    GRN
    MRN
    VendorServiceCharge
    ComplaintDetailEntry
    JOBWorkRGP
    JOBWorkSRN
    VSPASSETISSUE
    ProductDelivery
    DairyBooking
    FreshDelivery
    LCCreation
    SalaryGeneration
    CSA_Transfer
    STIR_TransferKDILReturn
    productBookingEntry
    ProductDeliveryOrder
    FreshBookingEntry
    ContraVoucher
    ProductionPlanning
    ProductionBatchOrder
    productionIssueEntry
    ProcessProductionStandardization
    ProcessProductionStageProcess
    MCCMilkSample
    MilkTruckSheet
    tankerLocationChange
    MerchantPurchaseOrder
    SaleOrderMT
    PerformaInvoiceMT
    LCRequsetMT
    DocAcceptanceMT
    CommercialInvoiceMT
    FixedDepositeMT
    SaleInvoiceMT
    saleReturnMT
    JobWorkMilkWeighment
    JWMilkGateEntry
    JobMilkQualityCheck
    JobMilkUnloading
    serviceAssetDispatch
    CartMaintenanceEntry
    FAAcquisitionEntry
    FADisposalEntry
    EmployeeSalary_HR
    LeaveApplication_HR
    LeaveAdjustment_HR
    AllowanceDetail_HR
    Deductiondetail_HR
    ReimbursementDetails_HR
    GenerateBonus_HR
    LoanGeneration_HR
    LoanAdjustment_HR
    DailyAttendance_HR
    HourlyAttendance_HR
    MonthlyAttendance_HR
    AdjustmentVoucher_HR
    LTAClaim_HR
    MediclaimEntry_HR
    FullAndFinalSettlement_HR
    EmployeeShiftChange_HR
    EmployeeTransfer_HR
    EmpIncrement_HR
    FA_IssueItemsToAsset
    FAAssetWork
    MCCMaterialSaleFarmer
    MCCMaterialSaleFarmerReturn
    MCCFarmerAdjustment
    frmMCCMaterialFarmer
    frmMCCMaterialSaleReturnFarmer
    frmFarmerPaymentAdjustment
    MCCDispatchReturn
    TransferReturn
    Jobwork_Transfer_Milk
    Jobwork_Transfer_Other
    JobWork_SRN
    Jobwork_Transfer_Milk_Return
    Jobwork_Transfer_Other_Return
    JobWork_SRN_RETURN
    MilkTransferInReturn
    RGPR
    Bulk_MILK_SRN_Return
    Bulk_MILK_TRADE_Return
    Purchase_Return
    SRNReturn
    ScrapSaleRetrun
    frmDeliveryOrderDairy
    DispatchDairy
    ItemMasterForOther
End Enum

Public Class clsOpenTransactionForm
    Public Shared Sub OpenTransacionForm(ByVal TransType As EnumTransType, ByVal DocumnetNo As String)
        OpenTransacionForm(TransType, DocumnetNo, True)
    End Sub
    Public Shared Sub OpenTransacionForm(ByVal TransType As EnumTransType, ByVal DocumnetNo As String, ByVal IsOpenInMDI As Boolean, Optional ByVal IFTrueShowFormElseShowDialog As Boolean = True)
        'clsCommon.ProgressBarShow()
        'clsCommon.ProgressBarUpdate("Opening Transacion.Please wait...")
        'Dim frm
        Try
            Select Case TransType
                Case EnumTransType.JournalEntry
                    'frm = New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    'frm.strVoucherNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.journalEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)

                Case EnumTransType.RcptEntry
                    'frm = New FrmReceipttNew
                    'frm.strRcptNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.ReceiptEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DispChallan
                    'frm = New FrmMccDispatch
                    'frm.strDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmMCCDispatch, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkTransferIn
                    'frm = New FrmMilkTransferIn
                    'frm.strDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmMilkTransferIn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PymntEntry
                    'frm = New FrmPaymentNew()
                    'frm.strPaymentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.PaymentEntryNew, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LoadOut
                    'frm = New frmShipmentInvoice()
                    'frm.strLoadOutNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.LoadOut, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.APInvoice
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_VENDOR_INVOICE_HEAD WHERE Document_No='" & DocumnetNo & "'")), "VS") = CompairStringResult.Equal Then
                        'frm = New FrmVendorService()
                        'frm.text = "Vendor Service Charge"

                        MDI.ShowForm(clsUserMgtCode.FrmVendorService, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    Else
                        MDI.ShowForm(clsUserMgtCode.mbtnAPInvoiceEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)

                        'frm = New FrmAPInvoiceEntry()
                    End If
                    'frm.strAPInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                Case EnumTransType.BankTransfer
                    'frm = New FrmBankTransfer(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    'frm.strbankTrans = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.bankTransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SRN
                    'frm = New frmSRN()
                    'frm.strSRNno = DocumnetNo
                    'frm.FORMTYPE = clsUserMgtCode.mbtnSRN
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Document_Type,'')  from TSPL_SRN_HEAD where SRN_No ='" & clsCommon.myCstr(DocumnetNo) & "'")), "MT") = CompairStringResult.Equal Then
                        MDI.ShowForm(clsUserMgtCode.FrmSRNMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    Else
                        MDI.ShowForm(clsUserMgtCode.mbtnSRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    End If

                Case EnumTransType.InvoiceFreshSale
                    'frm = New frmInvoiceFreshSale()
                    'frm.strSRNno = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmInvoiceFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.CrateReceived
                    'frm = New frmCreateReceived()
                    'frm.strCrateReceived = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmCreateReceived, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PuchaseOrder
                    'frm = New frmPurchaseOrder(clsUserMgtCode.mbtnPurchaseOrder)
                    'frm.PurchaseOrderNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.mbtnPurchaseOrder, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PurchaseIndent ''BM00000008148
                    'frm = New frmPurchaseRequistion()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.mbtnPurchaseRequistion, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)

                Case EnumTransType.ScarSale ''BM00000008148
                    'frm = New frmPurchaseRequistion()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.ScrapSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ExpiredItemEntry
                    'frm = New FrmExpiryDateEntry()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.FrmExpiryDateEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MMTrans
                    'frm = New frmTransferNew(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    'frm.strTrnasfer = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.Transfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.BankRvrs
                    'frm = New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    'frm.strBankRvrse = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.reverseTransaction, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ICAdj
                    Dim strAdjustmentType As String = ClsAdjustments.GetTransactionType(DocumnetNo, Nothing)
                    If common.clsCommon.CompairString(frmAdjustmentEmpty.strCostTransaction, strAdjustmentType) = common.CompairStringResult.Equal Then
                        'frm = New frmAdjustmentEmpty()
                        'frm.strDocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()

                        MDI.ShowForm(clsUserMgtCode.mbtnEmptyTrans, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    ElseIf common.clsCommon.CompairString(frmAdjustmentProduction.strCostTransaction, strAdjustmentType) = common.CompairStringResult.Equal Then
                        'frm = New frmAdjustmentProduction()
                        'frm.strDocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()

                        MDI.ShowForm(clsUserMgtCode.mbtnProductionEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    ElseIf common.clsCommon.CompairString(AdjustmentEnum.strCostTransaction, strAdjustmentType) = common.CompairStringResult.Equal Then
                        'frm = New frmAdjustmentStore()
                        'frm.strDocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()

                        MDI.ShowForm(clsUserMgtCode.mbtnStoreAdjustment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    End If
                Case EnumTransType.SaleInvoice
                    'frm = New frmShipmentInvoice()
                    'frm.strLoadOutNo = common.clsCommon.myCstr(common.clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + DocumnetNo + "'"))
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    'frm = New frmSNSaleInvoice
                    'frm.DocumentNo = DocumnetNo 'common.clsCommon.myCstr(common.clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + DocumnetNo + "'"))
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.MdiParent = MDI
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmSNSaleInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleInvoiceDemo
                    Dim strInvType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & DocumnetNo & "' "))
                    If clsCommon.CompairString(strInvType, "S") = CompairStringResult.Equal Then
                        'frm = New frmSNServiceInvoice()
                        'frm.strSaleInvoice = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        MDI.ShowForm(clsUserMgtCode.frmSNServiceInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    Else
                        'frm = New frmSNSaleInvoice()
                        'frm.strSaleInvoice = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        MDI.ShowForm(clsUserMgtCode.frmSNSaleInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    End If


                Case EnumTransType.RcptADJ
                    'frm = New frmAdj()
                    'frm.strAdjNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.ReceiptAdjustmentEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PurchaseInvoice
                    'frm = New frmPurchaseInvoice()
                    'frm.strPOInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.mbtnPurchaseInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.frmSalesmanTarget
                    'frm = New FrmSalesmanTarget()
                    'frm.Code = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmSalesmanTarget, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PurchaseReturn
                    'frm = New frmPurchaseReturn()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.mbtnPurchaseReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleReturn, EnumTransType.SaleReturnInter
                    Dim qry As String = "select 1 from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocumnetNo + "'"
                    Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        'frm = New frmSNSaleReturn()
                        'frm.DocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()

                        MDI.ShowForm(clsUserMgtCode.frmSNSaleReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    Else
                        'frm = New frmSalesReturnNew()
                        'frm.strPOInvoice = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()

                        MDI.ShowForm(clsUserMgtCode.saleReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    End If
                Case EnumTransType.ARInvoice
                    'frm = New FrmARInvoiceEntry()
                    'frm.strAPInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.mbtnARInvoiceEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ScrapInvoice
                    Dim qry As String = "SELECT shipment_No,TSPL_SCRAPINVOICE_HEAD.Doc_Type from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + DocumnetNo + "'"
                    Dim dt As DataTable = common.clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        'frm = New frmScrapSale()
                        'frm.strShipmentno = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        DocumnetNo = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Doc_Type")), "J") = CompairStringResult.Equal Then
                            MDI.ShowForm(clsUserMgtCode.JobWorkDispatchProduction, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                        Else
                            MDI.ShowForm(clsUserMgtCode.ScrapSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                        End If

                    End If


                Case EnumTransType.ScrapShipment
                    'frm = New frmScrapSale()
                    'frm.strShipmentno = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.ScrapSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.IssueReturnTransfer
                    'frm = New frmIssueReturn()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.mbtnIssueReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.RGP
                    'frm = New frmRGP()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.mbtnGatePass, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.NRGP
                    'frm = New frmRGP()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.mbtnGatePass, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SDShipment
                    Dim strDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_SD_SHIPMENT_HEAD where Document_Code='" + DocumnetNo + "'"))
                    If clsCommon.CompairString(strDocType, "PS") = CompairStringResult.Equal Then
                        'frm = New frmShipmentProductSale()
                        'frm.DocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        MDI.ShowForm(clsUserMgtCode.frmShipmentProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    ElseIf clsCommon.CompairString(strDocType, "FS") = CompairStringResult.Equal Then
                        'frm = New frmDispatchNoteFreshSale()
                        'frm.DocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        MDI.ShowForm(clsUserMgtCode.FrmDispatchFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    Else
                        'frm = New frmSNShipment()
                        'frm.DocumentNo = DocumnetNo
                        'frm.WindowState = FormWindowState.Maximized
                        'frm.Show()
                        MDI.ShowForm(clsUserMgtCode.frmSNShipment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    End If


                Case EnumTransType.SDSaleReturn
                    'frm = New frmSNSaleReturn()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmSNSaleReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Assemblies
                    'frm = New frmAssemblies()
                    'frm.Document_No = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmAssemblies, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.WareHouseBreakage
                    'frm = New FrmWarehouseBreakage()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmWarehouseBreakage, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.VCGL
                    'frm = New frmVCGLEntry()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.mbtnVCGLEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.APInvoiceTDS
                    'frm = New FrmAPInvoiceEntryTDS()
                    'frm.strAPInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleQuotation
                    'frm = New frmSaleQuotation()
                    'frm.DocCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmSaleQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleOrder
                    'frm = New frmSNSalesOrder()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmSNSalesOrder, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.GLAccount
                    'frm = New frmGLAccount(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    'frm.strAccountCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.glAccount, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.productionEntry
                    'frm = New frmAdjustmentProduction()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.mbtnProductionEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.transfer
                    'frm = New frmTransferDCC()
                    'frm.strTransferno = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                    '    frm = New FrmTransferKDIL()
                    '    frm.strTransferno = DocumnetNo
                    '    frm.WindowState = FormWindowState.Maximized
                    '    frm.Show()
                    '    'formShow(frm, strProgramName)
                    'Else
                    '    frm = New frmTransferDCC()
                    '    frm.strTransferno = DocumnetNo
                    '    frm.WindowState = FormWindowState.Maximized
                    '    frm.Show()
                    '    'formShow(frm, strProgramName)
                    'End If
                    MDI.ShowForm(clsUserMgtCode.Transfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SDCSATrans
                    'frm = New frmCSATransfer()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmCSATransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SDCSASale
                    'frm = New FrmCSASaleInvoice()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmCSASaleInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)

                Case EnumTransType.SDCSADO
                    'frm = New FrmCSADeliveryOrder()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmCSADeliveryOrder, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bank_Guarantee_Master
                    'frm = New FrmBankGuaranteeMaster1()
                    'frm.strPaymentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.FrmBankGuaranteeMaster1, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkSRN
                    'frm = New frmMilkSRNMCC()
                    'frmMilkSRNMCC.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmMilkSRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkPI
                    'frm = New frmMilkPurchaseInvoiceMCC()
                    'frmMilkPurchaseInvoiceMCC.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmMilkPurchaseInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.RICE_MIX
                    'frm = New FrmRiceMixingEntry()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmRiceMixingEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.RICE_PROC
                    'frm = New FrmRiceProcessingEntry()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmRiceProcessingEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PP_ISSUE
                    'frm = New FrmProcessProductionIssueEntry()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmProcessProductionIssueEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCC_Material
                    DocumnetNo = clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_shipment_HEAD where sale_Invoice_No='" & DocumnetNo & "'")
                    'frm = New frmMCCMaterialSale()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterial, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCMaterialFrm
                    'frm = New frmMCCMaterialSale()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterial, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCMaterialSaleReturn
                    'frm = New frmMccMaterialSaleReturn()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterialSaleReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EXPORT_SO
                    'frm = New frmEXSalesOrder(clsUserMgtCode.frmEXSalesOrder)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesOrder, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EXPORT_QUOTATION
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmEXSalesQuotation)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EXPORT_PROFORMA
                    'frm = New frmEXPorformaInvoice(clsUserMgtCode.frmEXPorformaInvoice)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmEXPorformaInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Export_Commercial_Inv
                    'frm = New frmEXCommercialInvoice(clsUserMgtCode.frmEXCommercialInvoice)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmEXCommercialInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Fresh_Sale
                    'frm = New frmInvoiceFreshSale()
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmInvoiceFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Sale_Return
                    'frm = New frmSaleReturnFreshSale()
                    'frm.strSRNno = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmSaleReturnFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ProductDelivery
                    'frm = New frmDeliveryOrderProductSale()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()




                    MDI.ShowForm(clsUserMgtCode.frmSalesOrderProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Product_Invoice
                    'frm = New frmSaleInvoiceProductSale()
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()




                    MDI.ShowForm(clsUserMgtCode.frmSaleInvoiceProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Product_Return_Sale
                    'frm = New frmSaleReturnProductSale()
                    'frm.strSRNno = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmSaleReturnProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.CSA_Invoice
                    'frm = New FrmCSASaleInvoice()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmCSASaleInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EXPORT_Invoice
                    'frm = New frmEXSalesInvoice(clsUserMgtCode.frmEXSalesInvoice)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmSalesInvoiceMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_Return
                    'frm = New FrmBulkSaleReturn()
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.FrmBulkSaleReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_Invoice
                    'frm = New FrmInvoiceBulkSale()
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.FrmInvoiceBulkSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)

                Case EnumTransType.SD_CSATRANS_RETURN
                    'frm = New FrmCSATransferReturn()
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmCSATransferReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PP_STDN
                    'frm = New frmProcessProductionStandardization()
                    'frm.strDocumentCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmProcessProductionStandardization, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.BulkSRNTrade
                    'frm = New FrmBulkTradeSRN
                    'frm.strDocumentCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    'MDI.ShowForm(clsUserMgtCode.frmVSPAssetIssue,"", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DispatchBSTrade
                    'frm = New FrmDispatchBulkSaleTrade
                    'frm.strDocumentCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DispatchBSTradeReturn
                    MDI.ShowForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DispatchBS
                    'frm = New FrmDispatchBulkSale
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.FrmDispatchBulkSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.BulkSRN
                    'frm = New FrmBulkMilkSRN
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmBulkMilkSRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PRD_STG_PROC
                    'frm = New frmProcessProductionStageProcess
                    'frm.strDocumentCode = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmProcessProductionStageProcess, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.VSPTRAN
                    'frm = New frmVSPItemIssue
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmVSPItemIssue, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PROD_ENTRY
                    'frm = New frmProductionEntry
                    'frm.strDocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmProductionEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Export_Sale_Return
                    'frm = New frmEXSalesReturn(clsUserMgtCode.frmEXSalesReturn)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmSalesReturnMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FS_SH
                    'frm = New frmDispatchMultipleFreshSale
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()




                    MDI.ShowForm(clsUserMgtCode.frmDispatchMultipleFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PS_SH
                    'frm = New frmShipmentProductSale()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmShipmentProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Sale_Order
                    'frm = New frmEXSalesOrder(clsUserMgtCode.frmSalesOrderMT)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmSalesOrderMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Proforma
                    'frm = New frmEXPorformaInvoice(clsUserMgtCode.frmProformaInvoiceMT)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXPorformaInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Comm_Inv
                    'frm = New frmEXCommercialInvoice(clsUserMgtCode.frmCommercialInvoiceMT)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXCommercialInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Sale_Inv
                    'frm = New frmEXSalesInvoice(clsUserMgtCode.frmSalesInvoiceMT)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmEXSalesInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Sale_Ret
                    'frm = New frmEXSalesReturn(clsUserMgtCode.frmSalesReturnMT)
                    'frm.strSaleInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()



                    MDI.ShowForm(clsUserMgtCode.frmEXSalesReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MT_Sale_Qu
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmEXSalesQuotation)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()


                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PurchaseInvoice
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PurchaseInvoice
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkReceipt
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.frmMilkPurchaseInvoice)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkTransferIn
                    'frm = New FrmEXSalesQuotation(clsUserMgtCode.mbtnPurchaseInvoice)
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmEXSalesQuotation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_Purchase
                    'frm = New FrmMilkPurchaseInvoice
                    'frm.tag = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_Purchase_Return
                    'frm = New FrmMilkPurchaseReturn()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.FrmMilkPurchaseReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Mcc_transfer
                    'frm = New FrmMilkTransferIn
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmMilkTransferIn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.GRN
                    'frm = New frmGRN()
                    'frm.strGRN = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.mbtnGRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MRN
                    'frm = New frmMRN()
                    'frm.strGRN = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.mbtnMRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.VendorServiceCharge
                    'frm = New FrmVendorService()
                    'frm.strAPInvoice = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.FrmVendorService, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ComplaintDetailEntry
                    'frm = New FrmComplaintDetailEntry()
                    'frm.strComplaint = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.frmComplaintDetailEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JOBWorkRGP
                    'frm = New frmMilkRGP()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.FrmMilkJobWork, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JOBWorkSRN
                    'frm = New FrmJobMilkSRN()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()

                    MDI.ShowForm(clsUserMgtCode.FrmJobMilkSRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.VSPASSETISSUE
                    'frm = New frmVSPAssetIssue()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmVSPAssetIssue, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DairyBooking
                    'frm = New frmBookingDairyMultipleCustomer()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmbookingdairy, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FreshDelivery
                    'frm = New frmDeliveryNoteFreshSale()
                    'frm.StrDocNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    MDI.ShowForm(clsUserMgtCode.frmDeliveryNoteFreshSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LCCreation
                    'frm = New FrmLCCreation()
                    'frm.DocumentNo = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.Show()
                    'NEW
                    MDI.ShowForm(clsUserMgtCode.FrmLCCreation, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SalaryGeneration
                    'frm = New frmSalaryGeneration()
                    'frm.OpenTransDocument = DocumnetNo
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.show()
                    ''NEW
                    MDI.ShowForm(clsUserMgtCode.frmSalaryGeneration, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.CSA_Transfer
                    'frm = New frmCSATransfer()
                    'If DocumnetNo IsNot Nothing AndAlso DocumnetNo.Length > 0 Then
                    '    frm.csaTransferFinderValue = DocumnetNo
                    '    frm.WindowState = FormWindowState.Maximized
                    '    frm.show()
                    'End If
                    MDI.ShowForm(clsUserMgtCode.frmCSATransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    'NEW
                Case EnumTransType.STIR_TransferKDILReturn
                    'frm = New frmTransferKDILReturn()
                    'If DocumnetNo IsNot Nothing AndAlso DocumnetNo.Length > 0 Then
                    '    frm.STIRDocOpenTrans = DocumnetNo
                    '    frm.WindowState = FormWindowState.Maximized
                    '    frm.show()
                    'End If
                    MDI.ShowForm(clsUserMgtCode.TransferReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                    '===========================Added by preeti gupta==============
                Case EnumTransType.productBookingEntry
                    MDI.ShowForm(clsUserMgtCode.frmBookingProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ProductDeliveryOrder
                    MDI.ShowForm(clsUserMgtCode.frmSalesOrderProductSale, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FreshBookingEntry
                    MDI.ShowForm(clsUserMgtCode.FrmBookingEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ContraVoucher
                    MDI.ShowForm(clsUserMgtCode.bankTransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ProductionPlanning
                    MDI.ShowForm(clsUserMgtCode.frmProductionPlanningDairy, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ProductionBatchOrder
                    MDI.ShowForm(clsUserMgtCode.frmBatchOrderDairy, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCMilkSample
                    MDI.ShowForm(clsUserMgtCode.frmMilkSample, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkTruckSheet
                    MDI.ShowForm(clsUserMgtCode.MilkTruckSheet, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.tankerLocationChange
                    MDI.ShowForm(clsUserMgtCode.frmDispatchTransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MerchantPurchaseOrder
                    MDI.ShowForm(clsUserMgtCode.FrmPurchaseOrderMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleOrderMT
                    MDI.ShowForm(clsUserMgtCode.frmSalesOrderMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleOrderMT
                    MDI.ShowForm(clsUserMgtCode.frmSalesOrderMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.PerformaInvoiceMT
                    MDI.ShowForm(clsUserMgtCode.frmProformaInvoiceMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LCRequsetMT
                    MDI.ShowForm(clsUserMgtCode.FrmLCRequest, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DocAcceptanceMT
                    MDI.ShowForm(clsUserMgtCode.FrmDocumentAcceptance, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.CommercialInvoiceMT
                    MDI.ShowForm(clsUserMgtCode.frmCommercialInvoiceMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FixedDepositeMT
                    MDI.ShowForm(clsUserMgtCode.FrmFixedDeposit, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SaleInvoiceMT
                    MDI.ShowForm(clsUserMgtCode.frmSalesInvoiceMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.saleReturnMT
                    MDI.ShowForm(clsUserMgtCode.frmSalesInvoiceMT, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JobWorkMilkWeighment
                    MDI.ShowForm(clsUserMgtCode.FrmMilkWeighment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JWMilkGateEntry
                    MDI.ShowForm(clsUserMgtCode.FrmMilkGateEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JWMilkGateEntry
                    MDI.ShowForm(clsUserMgtCode.FrmMilkGateEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JobMilkQualityCheck
                    MDI.ShowForm(clsUserMgtCode.FrmJobMilkQualityCheck, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JobMilkUnloading
                    MDI.ShowForm(clsUserMgtCode.FrmMilkUnloading, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.serviceAssetDispatch
                    MDI.ShowForm(clsUserMgtCode.frmAssetDistatch, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.CartMaintenanceEntry
                    MDI.ShowForm(clsUserMgtCode.frmCartMaintenanceEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FAAcquisitionEntry
                    MDI.ShowForm(clsUserMgtCode.FAAcquisitionEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FADisposalEntry
                    MDI.ShowForm(clsUserMgtCode.FADisposalEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EmployeeSalary_HR
                    MDI.ShowForm(clsUserMgtCode.frmEmpSalary, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LeaveApplication_HR
                    MDI.ShowForm(clsUserMgtCode.frmLeaveApplication, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LeaveAdjustment_HR
                    MDI.ShowForm(clsUserMgtCode.frmLeaveAdjustment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.AllowanceDetail_HR
                    MDI.ShowForm(clsUserMgtCode.frmAllowanceDetails, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Deductiondetail_HR
                    MDI.ShowForm(clsUserMgtCode.frmDeductionDetails, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ReimbursementDetails_HR
                    MDI.ShowForm(clsUserMgtCode.frmReimbursementDetails, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.GenerateBonus_HR
                    MDI.ShowForm(clsUserMgtCode.frmGenerateBonus, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LoanGeneration_HR
                    MDI.ShowForm(clsUserMgtCode.frmLoanGeneration, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LoanAdjustment_HR
                    MDI.ShowForm(clsUserMgtCode.frmLoanAdjustment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DailyAttendance_HR
                    MDI.ShowForm(clsUserMgtCode.frmDailyAttendance, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.HourlyAttendance_HR
                    MDI.ShowForm(clsUserMgtCode.frmHourlyAttendance, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MonthlyAttendance_HR
                    MDI.ShowForm(clsUserMgtCode.frmMonthlyAttendance, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.AdjustmentVoucher_HR
                    MDI.ShowForm(clsUserMgtCode.frmAdjustmentVoucher, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.LTAClaim_HR
                    MDI.ShowForm(clsUserMgtCode.frmLTAClaim, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MediclaimEntry_HR
                    MDI.ShowForm(clsUserMgtCode.frmMediclaimEntry, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FullAndFinalSettlement_HR
                    MDI.ShowForm(clsUserMgtCode.frmFullAndFinalSettlement, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EmployeeShiftChange_HR
                    MDI.ShowForm(clsUserMgtCode.frmEmployeeShiftChange, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EmployeeTransfer_HR
                    MDI.ShowForm(clsUserMgtCode.FrmEmployeeTransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.EmpIncrement_HR
                    MDI.ShowForm(clsUserMgtCode.FrmEmpIncrement, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FA_IssueItemsToAsset
                    MDI.ShowForm(clsUserMgtCode.frmIssueItemsToAsset, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.FAAssetWork
                    MDI.ShowForm(clsUserMgtCode.FAAssetWork, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCMaterialSaleFarmer
                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterialFarmer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCMaterialSaleFarmerReturn
                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCFarmerAdjustment
                    MDI.ShowForm(clsUserMgtCode.frmFarmerPaymentAdjustment, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MCCDispatchReturn
                    MDI.ShowForm(clsUserMgtCode.MCCDispatchReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.TransferReturn
                    MDI.ShowForm(clsUserMgtCode.TransferReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Jobwork_Transfer_Milk
                    MDI.ShowForm(clsUserMgtCode.frmMilkJobWorkTransfer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Jobwork_Transfer_Milk_Return
                    MDI.ShowForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Jobwork_Transfer_Other
                    MDI.ShowForm(clsUserMgtCode.frmMilkJobWorkTransferOther, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Jobwork_Transfer_Other_Return
                    MDI.ShowForm(clsUserMgtCode.frmMilkJobWorkTransferOtherReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JobWork_SRN
                    MDI.ShowForm(clsUserMgtCode.JWO_SRN, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.frmMCCMaterialFarmer
                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterialFarmer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.frmMCCMaterialSaleReturnFarmer
                    MDI.ShowForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.JobWork_SRN_RETURN
                    MDI.ShowForm(clsUserMgtCode.JWO_SRN_Return, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.MilkTransferInReturn
                    MDI.ShowForm(clsUserMgtCode.frmMilkTransferInReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.RGPR
                    MDI.ShowForm(clsUserMgtCode.mbtnGatePass, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_MILK_SRN_Return
                    MDI.ShowForm(clsUserMgtCode.frmBulkMilkSRNReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Bulk_MILK_TRADE_Return
                    MDI.ShowForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.Purchase_Return
                    MDI.ShowForm(clsUserMgtCode.mbtnPurchaseReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.SRNReturn
                    MDI.ShowForm(clsUserMgtCode.SRNReturn, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ScrapSaleRetrun
                    MDI.ShowForm(clsUserMgtCode.ScrapSaleRetrun, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.frmDeliveryOrderDairy
                    MDI.ShowForm(clsUserMgtCode.frmDeliveryOrderDairy, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.DispatchDairy
                    MDI.ShowForm(clsUserMgtCode.frmSaleDispatchDairy, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
                Case EnumTransType.ItemMasterForOther
                    MDI.ShowForm(clsUserMgtCode.FrmItemMasterRMOther, "", IsOpenInMDI, DocumnetNo, IFTrueShowFormElseShowDialog)
            End Select
        Catch ex As Exception

        Finally
            'clsCommon.ProgressBarHide()
            'frm.focus()
        End Try
    End Sub

End Class
