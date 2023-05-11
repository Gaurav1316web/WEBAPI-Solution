<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeliveryOrderProductSale
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeliveryOrderProductSale))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkSameBillShip = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.lblARSecurity = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.lblReverseAdvanceSec = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.lblReverseRefund = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lblTotalOutstansing = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.lblRefund = New common.Controls.MyLabel()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.lblLedgerOutstanding = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblShortcloseDO = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.lblPendingDO = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblAdvanceSecurity = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblCreditLimit = New common.Controls.MyLabel()
        Me.chkIsTaxable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkInsuranceInclude = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.txtPriceCode = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.txtTransporterCode = New common.UserControls.txtFinder()
        Me.chkBookingwise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkItemwise = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.fndBookingNo = New common.UserControls.txtFinder()
        Me.txtVehicleCapacity = New common.MyNumBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtCustPODate = New common.Controls.MyDateTimePicker()
        Me.txtrate = New common.MyNumBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtRoadPermitNo = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.txtAdvance = New common.MyNumBox()
        Me.chkCommApply = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.ddlPaymentTerms = New common.Controls.MyComboBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblInvoiceType = New common.Controls.MyLabel()
        Me.ddlDispatchTerms = New common.Controls.MyComboBox()
        Me.lblCloseRemarksdesc = New common.Controls.MyLabel()
        Me.txtCloseRemarks = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblProject = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtDate = New System.Windows.Forms.DateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtPONo = New common.Controls.MyTextBox()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.txtPriceGroupCode = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.lblSONo = New common.Controls.MyLabel()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.txtDispatchPeriod = New common.MyNumBox()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.txtVehicleCode = New common.UserControls.txtFinder()
        Me.lblVhicleNo = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.cboModeOfTransport = New common.Controls.MyComboBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkclose = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDispatchDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.lblDept = New common.Controls.MyLabel()
        Me.lblKMReading = New common.Controls.MyLabel()
        Me.txtSOvalidity = New common.MyNumBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDeliveryDate = New common.Controls.MyDateTimePicker()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.lblAmountAfterComm = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblCommAmt = New common.Controls.MyLabel()
        Me.lblInvoiceDiscAmt = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkDiscountOnAmt = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDiscountOnRate = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDiscAmt = New common.MyNumBox()
        Me.txtDiscPer = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnpreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnsend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.chkRateUserCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRateDefaultSetting = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.lblAmbendmentNoCaption = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkSameBillShip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblARSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLedgerOutstanding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShortcloseDO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPendingDO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInsuranceInclude, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBookingwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustPODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoadPermitNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCommApply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlPaymentTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDispatchTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCloseRemarksdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispatchPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSOvalidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmountAfterComm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCopy)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateUserCustomer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateDefaultSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblAmbendmentNoCaption)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1189, 523)
        Me.SplitContainer1.SplitterDistance = 491
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1189, 491)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkSameBillShip)
        Me.RadPageViewPage1.Controls.Add(Me.Panel3)
        Me.RadPageViewPage1.Controls.Add(Me.chkIsTaxable)
        Me.RadPageViewPage1.Controls.Add(Me.chkInsuranceInclude)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblTransporterName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporterCode)
        Me.RadPageViewPage1.Controls.Add(Me.chkBookingwise)
        Me.RadPageViewPage1.Controls.Add(Me.chkItemwise)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.fndBookingNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleCapacity)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustPODate)
        Me.RadPageViewPage1.Controls.Add(Me.txtrate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoadPermitNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdvance)
        Me.RadPageViewPage1.Controls.Add(Me.chkCommApply)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.ddlPaymentTerms)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceType)
        Me.RadPageViewPage1.Controls.Add(Me.ddlDispatchTerms)
        Me.RadPageViewPage1.Controls.Add(Me.lblCloseRemarksdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtCloseRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtPONo)
        Me.RadPageViewPage1.Controls.Add(Me.btnDrillDown)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceGroupCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblSONo)
        Me.RadPageViewPage1.Controls.Add(Me.cboPOType)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDeliveryDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1168, 445)
        Me.RadPageViewPage1.Text = "Delivery Order"
        '
        'chkSameBillShip
        '
        Me.chkSameBillShip.Enabled = False
        Me.chkSameBillShip.Location = New System.Drawing.Point(718, 174)
        Me.chkSameBillShip.Name = "chkSameBillShip"
        Me.chkSameBillShip.Size = New System.Drawing.Size(157, 18)
        Me.chkSameBillShip.TabIndex = 1415
        Me.chkSameBillShip.Text = "Same Bill and Ship To Party"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.MyLabel25)
        Me.Panel3.Controls.Add(Me.lblARSecurity)
        Me.Panel3.Controls.Add(Me.MyLabel24)
        Me.Panel3.Controls.Add(Me.lblReverseAdvanceSec)
        Me.Panel3.Controls.Add(Me.MyLabel23)
        Me.Panel3.Controls.Add(Me.lblReverseRefund)
        Me.Panel3.Controls.Add(Me.MyLabel35)
        Me.Panel3.Controls.Add(Me.lblTotalOutstansing)
        Me.Panel3.Controls.Add(Me.MyLabel33)
        Me.Panel3.Controls.Add(Me.lblRefund)
        Me.Panel3.Controls.Add(Me.MyLabel30)
        Me.Panel3.Controls.Add(Me.lblLedgerOutstanding)
        Me.Panel3.Controls.Add(Me.MyLabel28)
        Me.Panel3.Controls.Add(Me.lblShortcloseDO)
        Me.Panel3.Controls.Add(Me.MyLabel26)
        Me.Panel3.Controls.Add(Me.lblPendingDO)
        Me.Panel3.Controls.Add(Me.MyLabel22)
        Me.Panel3.Controls.Add(Me.lblAdvanceSecurity)
        Me.Panel3.Controls.Add(Me.MyLabel21)
        Me.Panel3.Controls.Add(Me.lblCreditLimit)
        Me.Panel3.Location = New System.Drawing.Point(940, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 201)
        Me.Panel3.TabIndex = 1414
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Location = New System.Drawing.Point(3, 63)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(108, 18)
        Me.MyLabel25.TabIndex = 76
        Me.MyLabel25.Text = "AR Credit Security(-)"
        '
        'lblARSecurity
        '
        Me.lblARSecurity.AutoSize = False
        Me.lblARSecurity.BorderVisible = True
        Me.lblARSecurity.FieldName = Nothing
        Me.lblARSecurity.Location = New System.Drawing.Point(130, 62)
        Me.lblARSecurity.Name = "lblARSecurity"
        Me.lblARSecurity.Size = New System.Drawing.Size(87, 19)
        Me.lblARSecurity.TabIndex = 75
        Me.lblARSecurity.TextWrap = False
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(3, 42)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(118, 18)
        Me.MyLabel24.TabIndex = 74
        Me.MyLabel24.Text = "Revese Adv Security(-)"
        '
        'lblReverseAdvanceSec
        '
        Me.lblReverseAdvanceSec.AutoSize = False
        Me.lblReverseAdvanceSec.BorderVisible = True
        Me.lblReverseAdvanceSec.FieldName = Nothing
        Me.lblReverseAdvanceSec.Location = New System.Drawing.Point(130, 41)
        Me.lblReverseAdvanceSec.Name = "lblReverseAdvanceSec"
        Me.lblReverseAdvanceSec.Size = New System.Drawing.Size(87, 19)
        Me.lblReverseAdvanceSec.TabIndex = 73
        Me.lblReverseAdvanceSec.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Location = New System.Drawing.Point(3, 159)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(119, 18)
        Me.MyLabel23.TabIndex = 72
        Me.MyLabel23.Text = "Reverse Refund Sec(+)"
        '
        'lblReverseRefund
        '
        Me.lblReverseRefund.AutoSize = False
        Me.lblReverseRefund.BorderVisible = True
        Me.lblReverseRefund.FieldName = Nothing
        Me.lblReverseRefund.Location = New System.Drawing.Point(130, 158)
        Me.lblReverseRefund.Name = "lblReverseRefund"
        Me.lblReverseRefund.Size = New System.Drawing.Size(87, 19)
        Me.lblReverseRefund.TabIndex = 71
        Me.lblReverseRefund.TextWrap = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Location = New System.Drawing.Point(4, 179)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel35.TabIndex = 70
        Me.MyLabel35.Text = "Total Outstanding"
        '
        'lblTotalOutstansing
        '
        Me.lblTotalOutstansing.AutoSize = False
        Me.lblTotalOutstansing.BorderVisible = True
        Me.lblTotalOutstansing.FieldName = Nothing
        Me.lblTotalOutstansing.Location = New System.Drawing.Point(131, 178)
        Me.lblTotalOutstansing.Name = "lblTotalOutstansing"
        Me.lblTotalOutstansing.Size = New System.Drawing.Size(87, 19)
        Me.lblTotalOutstansing.TabIndex = 69
        Me.lblTotalOutstansing.TextWrap = False
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Location = New System.Drawing.Point(3, 139)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel33.TabIndex = 68
        Me.MyLabel33.Text = "Refund Security (-)"
        '
        'lblRefund
        '
        Me.lblRefund.AutoSize = False
        Me.lblRefund.BorderVisible = True
        Me.lblRefund.FieldName = Nothing
        Me.lblRefund.Location = New System.Drawing.Point(130, 138)
        Me.lblRefund.Name = "lblRefund"
        Me.lblRefund.Size = New System.Drawing.Size(87, 19)
        Me.lblRefund.TabIndex = 67
        Me.lblRefund.TextWrap = False
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Location = New System.Drawing.Point(3, 121)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(115, 18)
        Me.MyLabel30.TabIndex = 66
        Me.MyLabel30.Text = "Ledger Outstansing(-)"
        '
        'lblLedgerOutstanding
        '
        Me.lblLedgerOutstanding.AutoSize = False
        Me.lblLedgerOutstanding.BorderVisible = True
        Me.lblLedgerOutstanding.FieldName = Nothing
        Me.lblLedgerOutstanding.Location = New System.Drawing.Point(130, 120)
        Me.lblLedgerOutstanding.Name = "lblLedgerOutstanding"
        Me.lblLedgerOutstanding.Size = New System.Drawing.Size(87, 19)
        Me.lblLedgerOutstanding.TabIndex = 65
        Me.lblLedgerOutstanding.TextWrap = False
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(4, 103)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(109, 18)
        Me.MyLabel28.TabIndex = 64
        Me.MyLabel28.Text = "UnPostedDispatch(-)"
        '
        'lblShortcloseDO
        '
        Me.lblShortcloseDO.AutoSize = False
        Me.lblShortcloseDO.BorderVisible = True
        Me.lblShortcloseDO.FieldName = Nothing
        Me.lblShortcloseDO.Location = New System.Drawing.Point(130, 102)
        Me.lblShortcloseDO.Name = "lblShortcloseDO"
        Me.lblShortcloseDO.Size = New System.Drawing.Size(87, 19)
        Me.lblShortcloseDO.TabIndex = 63
        Me.lblShortcloseDO.TextWrap = False
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Location = New System.Drawing.Point(4, 84)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(116, 18)
        Me.MyLabel26.TabIndex = 62
        Me.MyLabel26.Text = "Pending Posted DO(-)"
        '
        'lblPendingDO
        '
        Me.lblPendingDO.AutoSize = False
        Me.lblPendingDO.BorderVisible = True
        Me.lblPendingDO.FieldName = Nothing
        Me.lblPendingDO.Location = New System.Drawing.Point(130, 83)
        Me.lblPendingDO.Name = "lblPendingDO"
        Me.lblPendingDO.Size = New System.Drawing.Size(87, 19)
        Me.lblPendingDO.TabIndex = 61
        Me.lblPendingDO.TextWrap = False
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Location = New System.Drawing.Point(3, 21)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(106, 18)
        Me.MyLabel22.TabIndex = 58
        Me.MyLabel22.Text = "Advance Security(+)"
        '
        'lblAdvanceSecurity
        '
        Me.lblAdvanceSecurity.AutoSize = False
        Me.lblAdvanceSecurity.BorderVisible = True
        Me.lblAdvanceSecurity.FieldName = Nothing
        Me.lblAdvanceSecurity.Location = New System.Drawing.Point(130, 20)
        Me.lblAdvanceSecurity.Name = "lblAdvanceSecurity"
        Me.lblAdvanceSecurity.Size = New System.Drawing.Size(87, 19)
        Me.lblAdvanceSecurity.TabIndex = 57
        Me.lblAdvanceSecurity.TextWrap = False
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel21.TabIndex = 56
        Me.MyLabel21.Text = "Credit Limit(+)"
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.AutoSize = False
        Me.lblCreditLimit.BorderVisible = True
        Me.lblCreditLimit.FieldName = Nothing
        Me.lblCreditLimit.Location = New System.Drawing.Point(130, 2)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(87, 19)
        Me.lblCreditLimit.TabIndex = 25
        Me.lblCreditLimit.TextWrap = False
        '
        'chkIsTaxable
        '
        Me.chkIsTaxable.Enabled = False
        Me.chkIsTaxable.Location = New System.Drawing.Point(711, 150)
        Me.chkIsTaxable.Name = "chkIsTaxable"
        Me.chkIsTaxable.Size = New System.Drawing.Size(69, 18)
        Me.chkIsTaxable.TabIndex = 1413
        Me.chkIsTaxable.Text = "Is Taxable"
        '
        'chkInsuranceInclude
        '
        Me.chkInsuranceInclude.Location = New System.Drawing.Point(711, 129)
        Me.chkInsuranceInclude.Name = "chkInsuranceInclude"
        Me.chkInsuranceInclude.Size = New System.Drawing.Size(128, 18)
        Me.chkInsuranceInclude.TabIndex = 1412
        Me.chkInsuranceInclude.Text = "Is Insurance Including"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPriceCode)
        Me.Panel1.Controls.Add(Me.txtPriceCode)
        Me.Panel1.Location = New System.Drawing.Point(485, 174)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(227, 23)
        Me.Panel1.TabIndex = 1411
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Location = New System.Drawing.Point(3, 3)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 56
        Me.lblPriceCode.Text = "Price Code"
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(104, 2)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(116, 19)
        Me.txtPriceCode.TabIndex = 25
        Me.txtPriceCode.TextWrap = False
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.Location = New System.Drawing.Point(240, 89)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(242, 18)
        Me.lblTransporterName.TabIndex = 1410
        Me.lblTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransporterName.TextWrap = False
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(1, 90)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel31.TabIndex = 1408
        Me.MyLabel31.Text = "Transporter"
        '
        'txtTransporterCode
        '
        Me.txtTransporterCode.CalculationExpression = Nothing
        Me.txtTransporterCode.FieldCode = Nothing
        Me.txtTransporterCode.FieldDesc = Nothing
        Me.txtTransporterCode.FieldMaxLength = 0
        Me.txtTransporterCode.FieldName = Nothing
        Me.txtTransporterCode.isCalculatedField = False
        Me.txtTransporterCode.IsSourceFromTable = False
        Me.txtTransporterCode.IsSourceFromValueList = False
        Me.txtTransporterCode.IsUnique = False
        Me.txtTransporterCode.Location = New System.Drawing.Point(96, 87)
        Me.txtTransporterCode.MendatroryField = False
        Me.txtTransporterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterCode.MyLinkLable1 = Me.MyLabel31
        Me.txtTransporterCode.MyLinkLable2 = Nothing
        Me.txtTransporterCode.MyReadOnly = False
        Me.txtTransporterCode.MyShowMasterFormButton = False
        Me.txtTransporterCode.Name = "txtTransporterCode"
        Me.txtTransporterCode.ReferenceFieldDesc = Nothing
        Me.txtTransporterCode.ReferenceFieldName = Nothing
        Me.txtTransporterCode.ReferenceTableName = Nothing
        Me.txtTransporterCode.Size = New System.Drawing.Size(143, 20)
        Me.txtTransporterCode.TabIndex = 1407
        Me.txtTransporterCode.Value = ""
        '
        'chkBookingwise
        '
        Me.chkBookingwise.Location = New System.Drawing.Point(772, 20)
        Me.chkBookingwise.Name = "chkBookingwise"
        Me.chkBookingwise.Size = New System.Drawing.Size(86, 18)
        Me.chkBookingwise.TabIndex = 1395
        Me.chkBookingwise.Text = "Booking wise"
        '
        'chkItemwise
        '
        Me.chkItemwise.Location = New System.Drawing.Point(872, 21)
        Me.chkItemwise.Name = "chkItemwise"
        Me.chkItemwise.Size = New System.Drawing.Size(70, 18)
        Me.chkItemwise.TabIndex = 1394
        Me.chkItemwise.Text = "Item Wise"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(490, 24)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel19.TabIndex = 1393
        Me.MyLabel19.Text = "Booking No"
        '
        'fndBookingNo
        '
        Me.fndBookingNo.CalculationExpression = Nothing
        Me.fndBookingNo.FieldCode = Nothing
        Me.fndBookingNo.FieldDesc = Nothing
        Me.fndBookingNo.FieldMaxLength = 0
        Me.fndBookingNo.FieldName = Nothing
        Me.fndBookingNo.isCalculatedField = False
        Me.fndBookingNo.IsSourceFromTable = False
        Me.fndBookingNo.IsSourceFromValueList = False
        Me.fndBookingNo.IsUnique = False
        Me.fndBookingNo.Location = New System.Drawing.Point(589, 22)
        Me.fndBookingNo.MendatroryField = False
        Me.fndBookingNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBookingNo.MyLinkLable1 = Me.MyLabel19
        Me.fndBookingNo.MyLinkLable2 = Nothing
        Me.fndBookingNo.MyReadOnly = True
        Me.fndBookingNo.MyShowMasterFormButton = False
        Me.fndBookingNo.Name = "fndBookingNo"
        Me.fndBookingNo.ReferenceFieldDesc = Nothing
        Me.fndBookingNo.ReferenceFieldName = Nothing
        Me.fndBookingNo.ReferenceTableName = Nothing
        Me.fndBookingNo.Size = New System.Drawing.Size(144, 19)
        Me.fndBookingNo.TabIndex = 1392
        Me.fndBookingNo.Value = ""
        '
        'txtVehicleCapacity
        '
        Me.txtVehicleCapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtVehicleCapacity.CalculationExpression = Nothing
        Me.txtVehicleCapacity.DecimalPlaces = 0
        Me.txtVehicleCapacity.FieldCode = Nothing
        Me.txtVehicleCapacity.FieldDesc = Nothing
        Me.txtVehicleCapacity.FieldMaxLength = 0
        Me.txtVehicleCapacity.FieldName = Nothing
        Me.txtVehicleCapacity.isCalculatedField = False
        Me.txtVehicleCapacity.IsSourceFromTable = False
        Me.txtVehicleCapacity.IsSourceFromValueList = False
        Me.txtVehicleCapacity.IsUnique = False
        Me.txtVehicleCapacity.Location = New System.Drawing.Point(857, 221)
        Me.txtVehicleCapacity.MendatroryField = False
        Me.txtVehicleCapacity.MyLinkLable1 = Nothing
        Me.txtVehicleCapacity.MyLinkLable2 = Nothing
        Me.txtVehicleCapacity.Name = "txtVehicleCapacity"
        Me.txtVehicleCapacity.ReferenceFieldDesc = Nothing
        Me.txtVehicleCapacity.ReferenceFieldName = Nothing
        Me.txtVehicleCapacity.ReferenceTableName = Nothing
        Me.txtVehicleCapacity.Size = New System.Drawing.Size(66, 20)
        Me.txtVehicleCapacity.TabIndex = 147
        Me.txtVehicleCapacity.Text = "0"
        Me.txtVehicleCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicleCapacity.Value = 0R
        Me.txtVehicleCapacity.Visible = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(711, 44)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel18.TabIndex = 1391
        Me.MyLabel18.Text = "Cust Po Date"
        '
        'txtCustPODate
        '
        Me.txtCustPODate.CalculationExpression = Nothing
        Me.txtCustPODate.CustomFormat = "dd/MM/yyyy"
        Me.txtCustPODate.FieldCode = Nothing
        Me.txtCustPODate.FieldDesc = Nothing
        Me.txtCustPODate.FieldMaxLength = 0
        Me.txtCustPODate.FieldName = Nothing
        Me.txtCustPODate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCustPODate.isCalculatedField = False
        Me.txtCustPODate.IsSourceFromTable = False
        Me.txtCustPODate.IsSourceFromValueList = False
        Me.txtCustPODate.IsUnique = False
        Me.txtCustPODate.Location = New System.Drawing.Point(811, 43)
        Me.txtCustPODate.MendatroryField = False
        Me.txtCustPODate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustPODate.MyLinkLable1 = Me.MyLabel18
        Me.txtCustPODate.MyLinkLable2 = Nothing
        Me.txtCustPODate.Name = "txtCustPODate"
        Me.txtCustPODate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustPODate.ReferenceFieldDesc = Nothing
        Me.txtCustPODate.ReferenceFieldName = Nothing
        Me.txtCustPODate.ReferenceTableName = Nothing
        Me.txtCustPODate.ShowCheckBox = True
        Me.txtCustPODate.Size = New System.Drawing.Size(108, 18)
        Me.txtCustPODate.TabIndex = 1390
        Me.txtCustPODate.TabStop = False
        Me.txtCustPODate.Text = "13/06/2011"
        Me.txtCustPODate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtrate
        '
        Me.txtrate.CalculationExpression = Nothing
        Me.txtrate.DecimalPlaces = 2
        Me.txtrate.FieldCode = Nothing
        Me.txtrate.FieldDesc = Nothing
        Me.txtrate.FieldMaxLength = 0
        Me.txtrate.FieldName = Nothing
        Me.txtrate.isCalculatedField = False
        Me.txtrate.IsSourceFromTable = False
        Me.txtrate.IsSourceFromValueList = False
        Me.txtrate.IsUnique = False
        Me.txtrate.Location = New System.Drawing.Point(811, 85)
        Me.txtrate.MendatroryField = True
        Me.txtrate.MyLinkLable1 = Me.MyLabel16
        Me.txtrate.MyLinkLable2 = Nothing
        Me.txtrate.Name = "txtrate"
        Me.txtrate.ReferenceFieldDesc = Nothing
        Me.txtrate.ReferenceFieldName = Nothing
        Me.txtrate.ReferenceTableName = Nothing
        Me.txtrate.Size = New System.Drawing.Size(109, 20)
        Me.txtrate.TabIndex = 1388
        Me.txtrate.Text = "0"
        Me.txtrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtrate.Value = 0R
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(711, 87)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel16.TabIndex = 1389
        Me.MyLabel16.Text = "Rate for RT"
        '
        'txtRoadPermitNo
        '
        Me.txtRoadPermitNo.CalculationExpression = Nothing
        Me.txtRoadPermitNo.FieldCode = Nothing
        Me.txtRoadPermitNo.FieldDesc = Nothing
        Me.txtRoadPermitNo.FieldMaxLength = 0
        Me.txtRoadPermitNo.FieldName = Nothing
        Me.txtRoadPermitNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoadPermitNo.isCalculatedField = False
        Me.txtRoadPermitNo.IsSourceFromTable = False
        Me.txtRoadPermitNo.IsSourceFromValueList = False
        Me.txtRoadPermitNo.IsUnique = False
        Me.txtRoadPermitNo.Location = New System.Drawing.Point(1025, 200)
        Me.txtRoadPermitNo.MaxLength = 50
        Me.txtRoadPermitNo.MendatroryField = False
        Me.txtRoadPermitNo.MyLinkLable1 = Me.MyLabel20
        Me.txtRoadPermitNo.MyLinkLable2 = Nothing
        Me.txtRoadPermitNo.Name = "txtRoadPermitNo"
        Me.txtRoadPermitNo.ReferenceFieldDesc = Nothing
        Me.txtRoadPermitNo.ReferenceFieldName = Nothing
        Me.txtRoadPermitNo.ReferenceTableName = Nothing
        Me.txtRoadPermitNo.Size = New System.Drawing.Size(106, 18)
        Me.txtRoadPermitNo.TabIndex = 158
        Me.txtRoadPermitNo.Visible = False
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(929, 202)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel20.TabIndex = 159
        Me.MyLabel20.Text = "Road Permit No"
        Me.MyLabel20.Visible = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(711, 224)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(140, 16)
        Me.MyLabel13.TabIndex = 150
        Me.MyLabel13.Text = "Vehicle Capacity Required"
        Me.MyLabel13.Visible = False
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(1, 68)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel18.TabIndex = 19
        Me.RadLabel18.Text = "Ship To Location"
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(240, 67)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(242, 18)
        Me.lblShipToLocation.TabIndex = 13
        Me.lblShipToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipToLocation.TextWrap = False
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(491, 89)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel17.TabIndex = 1387
        Me.MyLabel17.Text = "Advance %"
        '
        'txtShipToLocation
        '
        Me.txtShipToLocation.CalculationExpression = Nothing
        Me.txtShipToLocation.FieldCode = Nothing
        Me.txtShipToLocation.FieldDesc = Nothing
        Me.txtShipToLocation.FieldMaxLength = 0
        Me.txtShipToLocation.FieldName = Nothing
        Me.txtShipToLocation.isCalculatedField = False
        Me.txtShipToLocation.IsSourceFromTable = False
        Me.txtShipToLocation.IsSourceFromValueList = False
        Me.txtShipToLocation.IsUnique = False
        Me.txtShipToLocation.Location = New System.Drawing.Point(96, 67)
        Me.txtShipToLocation.MendatroryField = False
        Me.txtShipToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipToLocation.MyLinkLable1 = Me.RadLabel18
        Me.txtShipToLocation.MyLinkLable2 = Me.lblShipToLocation
        Me.txtShipToLocation.MyReadOnly = False
        Me.txtShipToLocation.MyShowMasterFormButton = False
        Me.txtShipToLocation.Name = "txtShipToLocation"
        Me.txtShipToLocation.ReferenceFieldDesc = Nothing
        Me.txtShipToLocation.ReferenceFieldName = Nothing
        Me.txtShipToLocation.ReferenceTableName = Nothing
        Me.txtShipToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtShipToLocation.TabIndex = 12
        Me.txtShipToLocation.Value = ""
        '
        'txtAdvance
        '
        Me.txtAdvance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAdvance.CalculationExpression = Nothing
        Me.txtAdvance.DecimalPlaces = 0
        Me.txtAdvance.FieldCode = Nothing
        Me.txtAdvance.FieldDesc = Nothing
        Me.txtAdvance.FieldMaxLength = 0
        Me.txtAdvance.FieldName = Nothing
        Me.txtAdvance.isCalculatedField = False
        Me.txtAdvance.IsSourceFromTable = False
        Me.txtAdvance.IsSourceFromValueList = False
        Me.txtAdvance.IsUnique = False
        Me.txtAdvance.Location = New System.Drawing.Point(589, 85)
        Me.txtAdvance.MendatroryField = False
        Me.txtAdvance.MyLinkLable1 = Nothing
        Me.txtAdvance.MyLinkLable2 = Nothing
        Me.txtAdvance.Name = "txtAdvance"
        Me.txtAdvance.ReferenceFieldDesc = Nothing
        Me.txtAdvance.ReferenceFieldName = Nothing
        Me.txtAdvance.ReferenceTableName = Nothing
        Me.txtAdvance.Size = New System.Drawing.Size(115, 20)
        Me.txtAdvance.TabIndex = 1386
        Me.txtAdvance.Text = "0"
        Me.txtAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance.Value = 0R
        '
        'chkCommApply
        '
        Me.chkCommApply.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCommApply.Location = New System.Drawing.Point(772, 3)
        Me.chkCommApply.Name = "chkCommApply"
        Me.chkCommApply.Size = New System.Drawing.Size(114, 16)
        Me.chkCommApply.TabIndex = 157
        Me.chkCommApply.Text = "Commission Apply"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(710, 66)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel12.TabIndex = 153
        Me.MyLabel12.Text = "Payment Terms"
        '
        'ddlPaymentTerms
        '
        Me.ddlPaymentTerms.AutoCompleteDisplayMember = Nothing
        Me.ddlPaymentTerms.AutoCompleteValueMember = Nothing
        Me.ddlPaymentTerms.CalculationExpression = Nothing
        Me.ddlPaymentTerms.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlPaymentTerms.FieldCode = Nothing
        Me.ddlPaymentTerms.FieldDesc = Nothing
        Me.ddlPaymentTerms.FieldMaxLength = 0
        Me.ddlPaymentTerms.FieldName = Nothing
        Me.ddlPaymentTerms.isCalculatedField = False
        Me.ddlPaymentTerms.IsSourceFromTable = False
        Me.ddlPaymentTerms.IsSourceFromValueList = False
        Me.ddlPaymentTerms.IsUnique = False
        Me.ddlPaymentTerms.Location = New System.Drawing.Point(811, 62)
        Me.ddlPaymentTerms.MendatroryField = False
        Me.ddlPaymentTerms.MyLinkLable1 = Me.MyLabel7
        Me.ddlPaymentTerms.MyLinkLable2 = Nothing
        Me.ddlPaymentTerms.Name = "ddlPaymentTerms"
        Me.ddlPaymentTerms.ReferenceFieldDesc = Nothing
        Me.ddlPaymentTerms.ReferenceFieldName = Nothing
        Me.ddlPaymentTerms.ReferenceTableName = Nothing
        Me.ddlPaymentTerms.Size = New System.Drawing.Size(109, 20)
        Me.ddlPaymentTerms.TabIndex = 154
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(964, 220)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel7.TabIndex = 121
        Me.MyLabel7.Text = "Price Group Code"
        Me.MyLabel7.Visible = False
        '
        'lblInvoiceType
        '
        Me.lblInvoiceType.FieldName = Nothing
        Me.lblInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceType.Location = New System.Drawing.Point(489, 68)
        Me.lblInvoiceType.Name = "lblInvoiceType"
        Me.lblInvoiceType.Size = New System.Drawing.Size(85, 16)
        Me.lblInvoiceType.TabIndex = 151
        Me.lblInvoiceType.Text = "Dispatch Terms"
        '
        'ddlDispatchTerms
        '
        Me.ddlDispatchTerms.AutoCompleteDisplayMember = Nothing
        Me.ddlDispatchTerms.AutoCompleteValueMember = Nothing
        Me.ddlDispatchTerms.CalculationExpression = Nothing
        Me.ddlDispatchTerms.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlDispatchTerms.FieldCode = Nothing
        Me.ddlDispatchTerms.FieldDesc = Nothing
        Me.ddlDispatchTerms.FieldMaxLength = 0
        Me.ddlDispatchTerms.FieldName = Nothing
        Me.ddlDispatchTerms.isCalculatedField = False
        Me.ddlDispatchTerms.IsSourceFromTable = False
        Me.ddlDispatchTerms.IsSourceFromValueList = False
        Me.ddlDispatchTerms.IsUnique = False
        Me.ddlDispatchTerms.Location = New System.Drawing.Point(589, 63)
        Me.ddlDispatchTerms.MendatroryField = False
        Me.ddlDispatchTerms.MyLinkLable1 = Me.MyLabel7
        Me.ddlDispatchTerms.MyLinkLable2 = Nothing
        Me.ddlDispatchTerms.Name = "ddlDispatchTerms"
        Me.ddlDispatchTerms.ReferenceFieldDesc = Nothing
        Me.ddlDispatchTerms.ReferenceFieldName = Nothing
        Me.ddlDispatchTerms.ReferenceTableName = Nothing
        Me.ddlDispatchTerms.Size = New System.Drawing.Size(115, 20)
        Me.ddlDispatchTerms.TabIndex = 152
        '
        'lblCloseRemarksdesc
        '
        Me.lblCloseRemarksdesc.AutoSize = False
        Me.lblCloseRemarksdesc.BorderVisible = True
        Me.lblCloseRemarksdesc.FieldName = Nothing
        Me.lblCloseRemarksdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloseRemarksdesc.Location = New System.Drawing.Point(709, 106)
        Me.lblCloseRemarksdesc.Name = "lblCloseRemarksdesc"
        Me.lblCloseRemarksdesc.Size = New System.Drawing.Size(213, 20)
        Me.lblCloseRemarksdesc.TabIndex = 133
        Me.lblCloseRemarksdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCloseRemarksdesc.TextWrap = False
        '
        'txtCloseRemarks
        '
        Me.txtCloseRemarks.CalculationExpression = Nothing
        Me.txtCloseRemarks.FieldCode = Nothing
        Me.txtCloseRemarks.FieldDesc = Nothing
        Me.txtCloseRemarks.FieldMaxLength = 0
        Me.txtCloseRemarks.FieldName = Nothing
        Me.txtCloseRemarks.isCalculatedField = False
        Me.txtCloseRemarks.IsSourceFromTable = False
        Me.txtCloseRemarks.IsSourceFromValueList = False
        Me.txtCloseRemarks.IsUnique = False
        Me.txtCloseRemarks.Location = New System.Drawing.Point(589, 107)
        Me.txtCloseRemarks.MendatroryField = False
        Me.txtCloseRemarks.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCloseRemarks.MyLinkLable1 = Me.MyLabel4
        Me.txtCloseRemarks.MyLinkLable2 = Me.lblProject
        Me.txtCloseRemarks.MyReadOnly = False
        Me.txtCloseRemarks.MyShowMasterFormButton = False
        Me.txtCloseRemarks.Name = "txtCloseRemarks"
        Me.txtCloseRemarks.ReferenceFieldDesc = Nothing
        Me.txtCloseRemarks.ReferenceFieldName = Nothing
        Me.txtCloseRemarks.ReferenceTableName = Nothing
        Me.txtCloseRemarks.Size = New System.Drawing.Size(115, 19)
        Me.txtCloseRemarks.TabIndex = 132
        Me.txtCloseRemarks.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 1)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 34
        Me.MyLabel4.Text = "Project"
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(219, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(185, 20)
        Me.lblProject.TabIndex = 1
        Me.lblProject.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProject.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(489, 112)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel9.TabIndex = 130
        Me.MyLabel9.Text = "CLose Remarks"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(407, 2)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(79, 20)
        Me.txtDate.TabIndex = 129
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(489, 48)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel8.TabIndex = 15
        Me.MyLabel8.Text = "Customer PO No"
        '
        'txtPONo
        '
        Me.txtPONo.CalculationExpression = Nothing
        Me.txtPONo.FieldCode = Nothing
        Me.txtPONo.FieldDesc = Nothing
        Me.txtPONo.FieldMaxLength = 0
        Me.txtPONo.FieldName = Nothing
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.isCalculatedField = False
        Me.txtPONo.IsSourceFromTable = False
        Me.txtPONo.IsSourceFromValueList = False
        Me.txtPONo.IsUnique = False
        Me.txtPONo.Location = New System.Drawing.Point(589, 43)
        Me.txtPONo.MaxLength = 200
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyLinkLable1 = Me.MyLabel8
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(115, 18)
        Me.txtPONo.TabIndex = 11
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(746, 2)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 18)
        Me.btnDrillDown.TabIndex = 4
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(488, 157)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel5.TabIndex = 126
        Me.MyLabel5.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(589, 155)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(116, 18)
        Me.lblTotRAmt1.TabIndex = 27
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPriceGroupCode
        '
        Me.txtPriceGroupCode.AutoSize = False
        Me.txtPriceGroupCode.BorderVisible = True
        Me.txtPriceGroupCode.FieldName = Nothing
        Me.txtPriceGroupCode.Location = New System.Drawing.Point(1065, 221)
        Me.txtPriceGroupCode.Name = "txtPriceGroupCode"
        Me.txtPriceGroupCode.Size = New System.Drawing.Size(91, 19)
        Me.txtPriceGroupCode.TabIndex = 26
        Me.txtPriceGroupCode.TextWrap = False
        Me.txtPriceGroupCode.Visible = False
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(847, 432)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 373)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 29
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(241, 110)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(242, 18)
        Me.lblSalesman.TabIndex = 17
        Me.lblSalesman.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalesman.TextWrap = False
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(1174, 183)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel21
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(135, 20)
        Me.cboItemType.TabIndex = 19
        Me.cboItemType.Visible = False
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(1083, 98)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel21.TabIndex = 20
        Me.RadLabel21.Text = "Item Type"
        Me.RadLabel21.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(1, 111)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel2.TabIndex = 31
        Me.MyLabel2.Text = "Salesman"
        '
        'txtSalesman
        '
        Me.txtSalesman.CalculationExpression = Nothing
        Me.txtSalesman.FieldCode = Nothing
        Me.txtSalesman.FieldDesc = Nothing
        Me.txtSalesman.FieldMaxLength = 0
        Me.txtSalesman.FieldName = Nothing
        Me.txtSalesman.isCalculatedField = False
        Me.txtSalesman.IsSourceFromTable = False
        Me.txtSalesman.IsSourceFromValueList = False
        Me.txtSalesman.IsUnique = False
        Me.txtSalesman.Location = New System.Drawing.Point(95, 110)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.MyLabel2
        Me.txtSalesman.MyLinkLable2 = Me.lblSalesman
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.MyShowMasterFormButton = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReferenceFieldDesc = Nothing
        Me.txtSalesman.ReferenceFieldName = Nothing
        Me.txtSalesman.ReferenceTableName = Nothing
        Me.txtSalesman.Size = New System.Drawing.Size(145, 18)
        Me.txtSalesman.TabIndex = 16
        Me.txtSalesman.Value = ""
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(821, 90)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'lblSONo
        '
        Me.lblSONo.FieldName = Nothing
        Me.lblSONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSONo.Location = New System.Drawing.Point(489, 3)
        Me.lblSONo.Name = "lblSONo"
        Me.lblSONo.Size = New System.Drawing.Size(40, 16)
        Me.lblSONo.TabIndex = 15
        Me.lblSONo.Text = "SO No"
        '
        'cboPOType
        '
        Me.cboPOType.AutoCompleteDisplayMember = Nothing
        Me.cboPOType.AutoCompleteValueMember = Nothing
        Me.cboPOType.CalculationExpression = Nothing
        Me.cboPOType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPOType.FieldCode = Nothing
        Me.cboPOType.FieldDesc = Nothing
        Me.cboPOType.FieldMaxLength = 0
        Me.cboPOType.FieldName = Nothing
        Me.cboPOType.isCalculatedField = False
        Me.cboPOType.IsSourceFromTable = False
        Me.cboPOType.IsSourceFromValueList = False
        Me.cboPOType.IsUnique = False
        Me.cboPOType.Location = New System.Drawing.Point(994, 228)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.RadLabel8
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(77, 20)
        Me.cboPOType.TabIndex = 18
        Me.cboPOType.Visible = False
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(871, 96)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 24
        Me.RadLabel8.Text = "SO Type"
        Me.RadLabel8.Visible = False
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(242, 24)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(242, 18)
        Me.lblBillToLocation.TabIndex = 7
        Me.lblBillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(1, 153)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "Remarks"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(1, 174)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(1, 25)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 20
        Me.RadLabel15.Text = "Location"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel20)
        Me.RadGroupBox2.Controls.Add(Me.txtDept)
        Me.RadGroupBox2.Controls.Add(Me.lblDept)
        Me.RadGroupBox2.Controls.Add(Me.lblKMReading)
        Me.RadGroupBox2.Controls.Add(Me.txtSOvalidity)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 203)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1163, 167)
        Me.RadGroupBox2.TabIndex = 28
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Controls.Add(Me.MyLabel11)
        Me.gv1.Controls.Add(Me.pnlPCJ)
        Me.gv1.Controls.Add(Me.lblRouteNo)
        Me.gv1.Controls.Add(Me.txtRouteNo)
        Me.gv1.Controls.Add(Me.lblRouteDesc)
        Me.gv1.Controls.Add(Me.txtDispatchPeriod)
        Me.gv1.Controls.Add(Me.lblVehicleCode)
        Me.gv1.Controls.Add(Me.txtVehicleCode)
        Me.gv1.Controls.Add(Me.lblVhicleNo)
        Me.gv1.Controls.Add(Me.MyLabel10)
        Me.gv1.Controls.Add(Me.cboModeOfTransport)
        Me.gv1.Controls.Add(Me.chkOnHold)
        Me.gv1.Controls.Add(Me.RadLabel5)
        Me.gv1.Controls.Add(Me.RadLabel8)
        Me.gv1.Controls.Add(Me.RadLabel21)
        Me.gv1.Controls.Add(Me.chkclose)
        Me.gv1.Controls.Add(Me.txtDispatchDate)
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1143, 137)
        Me.gv1.TabIndex = 17
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(752, 25)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel11.TabIndex = 160
        Me.MyLabel11.Text = "Dispatch Period"
        Me.MyLabel11.Visible = False
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(1061, 12)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(413, 22)
        Me.pnlPCJ.TabIndex = 21
        Me.pnlPCJ.Visible = False
        '
        'fndProject
        '
        Me.fndProject.CalculationExpression = Nothing
        Me.fndProject.FieldCode = Nothing
        Me.fndProject.FieldDesc = Nothing
        Me.fndProject.FieldMaxLength = 0
        Me.fndProject.FieldName = Nothing
        Me.fndProject.isCalculatedField = False
        Me.fndProject.IsSourceFromTable = False
        Me.fndProject.IsSourceFromValueList = False
        Me.fndProject.IsUnique = False
        Me.fndProject.Location = New System.Drawing.Point(96, 2)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.MyLabel4
        Me.fndProject.MyLinkLable2 = Me.lblProject
        Me.fndProject.MyReadOnly = False
        Me.fndProject.MyShowMasterFormButton = False
        Me.fndProject.Name = "fndProject"
        Me.fndProject.ReferenceFieldDesc = Nothing
        Me.fndProject.ReferenceFieldName = Nothing
        Me.fndProject.ReferenceTableName = Nothing
        Me.fndProject.Size = New System.Drawing.Size(119, 20)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(247, 20)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 119
        Me.lblRouteNo.Text = "Route No"
        Me.lblRouteNo.Visible = False
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(344, 20)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(142, 19)
        Me.txtRouteNo.TabIndex = 22
        Me.txtRouteNo.Value = ""
        Me.txtRouteNo.Visible = False
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(489, 25)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(244, 17)
        Me.lblRouteDesc.TabIndex = 23
        Me.lblRouteDesc.Visible = False
        '
        'txtDispatchPeriod
        '
        Me.txtDispatchPeriod.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDispatchPeriod.CalculationExpression = Nothing
        Me.txtDispatchPeriod.DecimalPlaces = 0
        Me.txtDispatchPeriod.FieldCode = Nothing
        Me.txtDispatchPeriod.FieldDesc = Nothing
        Me.txtDispatchPeriod.FieldMaxLength = 0
        Me.txtDispatchPeriod.FieldName = Nothing
        Me.txtDispatchPeriod.isCalculatedField = False
        Me.txtDispatchPeriod.IsSourceFromTable = False
        Me.txtDispatchPeriod.IsSourceFromValueList = False
        Me.txtDispatchPeriod.IsUnique = False
        Me.txtDispatchPeriod.Location = New System.Drawing.Point(845, 20)
        Me.txtDispatchPeriod.MendatroryField = False
        Me.txtDispatchPeriod.MyLinkLable1 = Nothing
        Me.txtDispatchPeriod.MyLinkLable2 = Nothing
        Me.txtDispatchPeriod.Name = "txtDispatchPeriod"
        Me.txtDispatchPeriod.ReferenceFieldDesc = Nothing
        Me.txtDispatchPeriod.ReferenceFieldName = Nothing
        Me.txtDispatchPeriod.ReferenceTableName = Nothing
        Me.txtDispatchPeriod.Size = New System.Drawing.Size(118, 20)
        Me.txtDispatchPeriod.TabIndex = 155
        Me.txtDispatchPeriod.Text = "0"
        Me.txtDispatchPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDispatchPeriod.Value = 0R
        Me.txtDispatchPeriod.Visible = False
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Location = New System.Drawing.Point(38, 40)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(72, 18)
        Me.lblVehicleCode.TabIndex = 141
        Me.lblVehicleCode.Text = "Vehicle Code"
        Me.lblVehicleCode.Visible = False
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.CalculationExpression = Nothing
        Me.txtVehicleCode.FieldCode = Nothing
        Me.txtVehicleCode.FieldDesc = Nothing
        Me.txtVehicleCode.FieldMaxLength = 0
        Me.txtVehicleCode.FieldName = Nothing
        Me.txtVehicleCode.isCalculatedField = False
        Me.txtVehicleCode.IsSourceFromTable = False
        Me.txtVehicleCode.IsSourceFromValueList = False
        Me.txtVehicleCode.IsUnique = False
        Me.txtVehicleCode.Location = New System.Drawing.Point(133, 40)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.lblVehicleCode
        Me.txtVehicleCode.MyLinkLable2 = Nothing
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.MyShowMasterFormButton = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.ReferenceFieldDesc = Nothing
        Me.txtVehicleCode.ReferenceFieldName = Nothing
        Me.txtVehicleCode.ReferenceTableName = Nothing
        Me.txtVehicleCode.Size = New System.Drawing.Size(142, 20)
        Me.txtVehicleCode.TabIndex = 140
        Me.txtVehicleCode.Value = ""
        Me.txtVehicleCode.Visible = False
        '
        'lblVhicleNo
        '
        Me.lblVhicleNo.CalculationExpression = Nothing
        Me.lblVhicleNo.FieldCode = Nothing
        Me.lblVhicleNo.FieldDesc = Nothing
        Me.lblVhicleNo.FieldMaxLength = 0
        Me.lblVhicleNo.FieldName = Nothing
        Me.lblVhicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVhicleNo.isCalculatedField = False
        Me.lblVhicleNo.IsSourceFromTable = False
        Me.lblVhicleNo.IsSourceFromValueList = False
        Me.lblVhicleNo.IsUnique = False
        Me.lblVhicleNo.Location = New System.Drawing.Point(280, 42)
        Me.lblVhicleNo.MaxLength = 200
        Me.lblVhicleNo.MendatroryField = False
        Me.lblVhicleNo.MyLinkLable1 = Me.RadLabel6
        Me.lblVhicleNo.MyLinkLable2 = Nothing
        Me.lblVhicleNo.Name = "lblVhicleNo"
        Me.lblVhicleNo.ReferenceFieldDesc = Nothing
        Me.lblVhicleNo.ReferenceFieldName = Nothing
        Me.lblVhicleNo.ReferenceTableName = Nothing
        Me.lblVhicleNo.Size = New System.Drawing.Size(242, 18)
        Me.lblVhicleNo.TabIndex = 148
        Me.lblVhicleNo.Visible = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(845, 68)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel10.TabIndex = 144
        Me.MyLabel10.Text = "Dispatch Date"
        Me.MyLabel10.Visible = False
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.AutoCompleteDisplayMember = Nothing
        Me.cboModeOfTransport.AutoCompleteValueMember = Nothing
        Me.cboModeOfTransport.CalculationExpression = Nothing
        Me.cboModeOfTransport.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModeOfTransport.FieldCode = Nothing
        Me.cboModeOfTransport.FieldDesc = Nothing
        Me.cboModeOfTransport.FieldMaxLength = 0
        Me.cboModeOfTransport.FieldName = Nothing
        Me.cboModeOfTransport.isCalculatedField = False
        Me.cboModeOfTransport.IsSourceFromTable = False
        Me.cboModeOfTransport.IsSourceFromValueList = False
        Me.cboModeOfTransport.IsUnique = False
        Me.cboModeOfTransport.Location = New System.Drawing.Point(1068, 63)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.RadLabel5
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.ReferenceFieldDesc = Nothing
        Me.cboModeOfTransport.ReferenceFieldName = Nothing
        Me.cboModeOfTransport.ReferenceTableName = Nothing
        Me.cboModeOfTransport.Size = New System.Drawing.Size(68, 20)
        Me.cboModeOfTransport.TabIndex = 14
        Me.cboModeOfTransport.Visible = False
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(1005, 66)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel5.TabIndex = 20
        Me.RadLabel5.Text = "Tpt. Mode"
        Me.RadLabel5.Visible = False
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(1074, 68)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 5
        Me.chkOnHold.Text = "On Hold"
        Me.chkOnHold.Visible = False
        '
        'chkclose
        '
        Me.chkclose.Location = New System.Drawing.Point(1005, 89)
        Me.chkclose.Name = "chkclose"
        Me.chkclose.Size = New System.Drawing.Size(108, 18)
        Me.chkclose.TabIndex = 128
        Me.chkclose.Text = "Close Sales Order"
        Me.chkclose.Visible = False
        '
        'txtDispatchDate
        '
        Me.txtDispatchDate.CalculationExpression = Nothing
        Me.txtDispatchDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDispatchDate.FieldCode = Nothing
        Me.txtDispatchDate.FieldDesc = Nothing
        Me.txtDispatchDate.FieldMaxLength = 0
        Me.txtDispatchDate.FieldName = Nothing
        Me.txtDispatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDispatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDispatchDate.isCalculatedField = False
        Me.txtDispatchDate.IsSourceFromTable = False
        Me.txtDispatchDate.IsSourceFromValueList = False
        Me.txtDispatchDate.IsUnique = False
        Me.txtDispatchDate.Location = New System.Drawing.Point(928, 66)
        Me.txtDispatchDate.MendatroryField = False
        Me.txtDispatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDispatchDate.MyLinkLable1 = Me.MyLabel10
        Me.txtDispatchDate.MyLinkLable2 = Nothing
        Me.txtDispatchDate.Name = "txtDispatchDate"
        Me.txtDispatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDispatchDate.ReferenceFieldDesc = Nothing
        Me.txtDispatchDate.ReferenceFieldName = Nothing
        Me.txtDispatchDate.ReferenceTableName = Nothing
        Me.txtDispatchDate.Size = New System.Drawing.Size(74, 18)
        Me.txtDispatchDate.TabIndex = 143
        Me.txtDispatchDate.TabStop = False
        Me.txtDispatchDate.Text = "13/06/2011"
        Me.txtDispatchDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtDispatchDate.Visible = False
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(69, 38)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel20.TabIndex = 0
        Me.RadLabel20.Text = "Department"
        Me.RadLabel20.Visible = False
        '
        'txtDept
        '
        Me.txtDept.CalculationExpression = Nothing
        Me.txtDept.FieldCode = Nothing
        Me.txtDept.FieldDesc = Nothing
        Me.txtDept.FieldMaxLength = 0
        Me.txtDept.FieldName = Nothing
        Me.txtDept.isCalculatedField = False
        Me.txtDept.IsSourceFromTable = False
        Me.txtDept.IsSourceFromValueList = False
        Me.txtDept.IsUnique = False
        Me.txtDept.Location = New System.Drawing.Point(168, 37)
        Me.txtDept.MendatroryField = False
        Me.txtDept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.MyLinkLable1 = Me.RadLabel20
        Me.txtDept.MyLinkLable2 = Me.lblDept
        Me.txtDept.MyReadOnly = False
        Me.txtDept.MyShowMasterFormButton = False
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReferenceFieldDesc = Nothing
        Me.txtDept.ReferenceFieldName = Nothing
        Me.txtDept.ReferenceTableName = Nothing
        Me.txtDept.Size = New System.Drawing.Size(143, 18)
        Me.txtDept.TabIndex = 10
        Me.txtDept.Value = ""
        Me.txtDept.Visible = False
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(312, 37)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(242, 18)
        Me.lblDept.TabIndex = 16
        Me.lblDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDept.TextWrap = False
        Me.lblDept.Visible = False
        '
        'lblKMReading
        '
        Me.lblKMReading.FieldName = Nothing
        Me.lblKMReading.Location = New System.Drawing.Point(257, 20)
        Me.lblKMReading.Name = "lblKMReading"
        Me.lblKMReading.Size = New System.Drawing.Size(61, 18)
        Me.lblKMReading.TabIndex = 146
        Me.lblKMReading.Text = "SO Validity"
        Me.lblKMReading.Visible = False
        '
        'txtSOvalidity
        '
        Me.txtSOvalidity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSOvalidity.CalculationExpression = Nothing
        Me.txtSOvalidity.DecimalPlaces = 0
        Me.txtSOvalidity.FieldCode = Nothing
        Me.txtSOvalidity.FieldDesc = Nothing
        Me.txtSOvalidity.FieldMaxLength = 0
        Me.txtSOvalidity.FieldName = Nothing
        Me.txtSOvalidity.isCalculatedField = False
        Me.txtSOvalidity.IsSourceFromTable = False
        Me.txtSOvalidity.IsSourceFromValueList = False
        Me.txtSOvalidity.IsUnique = False
        Me.txtSOvalidity.Location = New System.Drawing.Point(352, 20)
        Me.txtSOvalidity.MendatroryField = False
        Me.txtSOvalidity.MyLinkLable1 = Me.lblKMReading
        Me.txtSOvalidity.MyLinkLable2 = Nothing
        Me.txtSOvalidity.Name = "txtSOvalidity"
        Me.txtSOvalidity.ReferenceFieldDesc = Nothing
        Me.txtSOvalidity.ReferenceFieldName = Nothing
        Me.txtSOvalidity.ReferenceTableName = Nothing
        Me.txtSOvalidity.Size = New System.Drawing.Size(68, 20)
        Me.txtSOvalidity.TabIndex = 145
        Me.txtSOvalidity.Text = "0"
        Me.txtSOvalidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSOvalidity.Value = 0R
        Me.txtSOvalidity.Visible = False
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(1, 131)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel7.TabIndex = 15
        Me.RadLabel7.Text = "Reference No"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(489, 134)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel13.TabIndex = 18
        Me.RadLabel13.Text = "Delivery Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(242, 46)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(241, 18)
        Me.lblVendorName.TabIndex = 10
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(1, 47)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Customer"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(349, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(96, 152)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(387, 18)
        Me.txtRemarks.TabIndex = 24
        '
        'txtReqNo
        '
        Me.txtReqNo.CalculationExpression = Nothing
        Me.txtReqNo.FieldCode = Nothing
        Me.txtReqNo.FieldDesc = Nothing
        Me.txtReqNo.FieldMaxLength = 0
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.isCalculatedField = False
        Me.txtReqNo.IsSourceFromTable = False
        Me.txtReqNo.IsSourceFromValueList = False
        Me.txtReqNo.IsUnique = False
        Me.txtReqNo.Location = New System.Drawing.Point(589, 2)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.lblSONo
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(144, 18)
        Me.txtReqNo.TabIndex = 3
        Me.txtReqNo.Value = ""
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(97, 46)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(143, 18)
        Me.txtVendorNo.TabIndex = 9
        Me.txtVendorNo.Value = ""
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.CalculationExpression = Nothing
        Me.txtBillToLocation.FieldCode = Nothing
        Me.txtBillToLocation.FieldDesc = Nothing
        Me.txtBillToLocation.FieldMaxLength = 0
        Me.txtBillToLocation.FieldName = Nothing
        Me.txtBillToLocation.isCalculatedField = False
        Me.txtBillToLocation.IsSourceFromTable = False
        Me.txtBillToLocation.IsSourceFromValueList = False
        Me.txtBillToLocation.IsUnique = False
        Me.txtBillToLocation.Location = New System.Drawing.Point(98, 24)
        Me.txtBillToLocation.MendatroryField = True
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtBillToLocation.TabIndex = 6
        Me.txtBillToLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(893, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'txtDeliveryDate
        '
        Me.txtDeliveryDate.CalculationExpression = Nothing
        Me.txtDeliveryDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDeliveryDate.FieldCode = Nothing
        Me.txtDeliveryDate.FieldDesc = Nothing
        Me.txtDeliveryDate.FieldMaxLength = 0
        Me.txtDeliveryDate.FieldName = Nothing
        Me.txtDeliveryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDeliveryDate.isCalculatedField = False
        Me.txtDeliveryDate.IsSourceFromTable = False
        Me.txtDeliveryDate.IsSourceFromValueList = False
        Me.txtDeliveryDate.IsUnique = False
        Me.txtDeliveryDate.Location = New System.Drawing.Point(589, 131)
        Me.txtDeliveryDate.MendatroryField = False
        Me.txtDeliveryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.MyLinkLable1 = Me.RadLabel13
        Me.txtDeliveryDate.MyLinkLable2 = Nothing
        Me.txtDeliveryDate.Name = "txtDeliveryDate"
        Me.txtDeliveryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.ReferenceFieldDesc = Nothing
        Me.txtDeliveryDate.ReferenceFieldName = Nothing
        Me.txtDeliveryDate.ReferenceTableName = Nothing
        Me.txtDeliveryDate.Size = New System.Drawing.Size(114, 18)
        Me.txtDeliveryDate.TabIndex = 15
        Me.txtDeliveryDate.TabStop = False
        Me.txtDeliveryDate.Text = "13/06/2011"
        Me.txtDeliveryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(95, 131)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(389, 18)
        Me.txtRefNo.TabIndex = 20
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(96, 174)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(388, 18)
        Me.txtDesc.TabIndex = 8
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1168, 445)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 3)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyReadOnly = False
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 19)
        Me.txtTaxGroup.TabIndex = 0
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 4)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 2
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 3)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(1010, 341)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.txtTermCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtDueDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox1.Controls.Add(Me.lblTermName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Terms"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 354)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1168, 87)
        Me.RadGroupBox1.TabIndex = 14
        Me.RadGroupBox1.Text = "Terms"
        '
        'txtTermCode
        '
        Me.txtTermCode.CalculationExpression = Nothing
        Me.txtTermCode.FieldCode = Nothing
        Me.txtTermCode.FieldDesc = Nothing
        Me.txtTermCode.FieldMaxLength = 0
        Me.txtTermCode.FieldName = Nothing
        Me.txtTermCode.isCalculatedField = False
        Me.txtTermCode.IsSourceFromTable = False
        Me.txtTermCode.IsSourceFromValueList = False
        Me.txtTermCode.IsUnique = False
        Me.txtTermCode.Location = New System.Drawing.Point(68, 23)
        Me.txtTermCode.MendatroryField = False
        Me.txtTermCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermCode.MyLinkLable1 = Me.RadLabel16
        Me.txtTermCode.MyLinkLable2 = Me.lblTermName
        Me.txtTermCode.MyReadOnly = False
        Me.txtTermCode.MyShowMasterFormButton = False
        Me.txtTermCode.Name = "txtTermCode"
        Me.txtTermCode.ReferenceFieldDesc = Nothing
        Me.txtTermCode.ReferenceFieldName = Nothing
        Me.txtTermCode.ReferenceTableName = Nothing
        Me.txtTermCode.Size = New System.Drawing.Size(143, 19)
        Me.txtTermCode.TabIndex = 0
        Me.txtTermCode.Value = ""
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(6, 24)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 13
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(220, 22)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 1
        Me.lblTermName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTermName.TextWrap = False
        '
        'txtDueDate
        '
        Me.txtDueDate.CalculationExpression = Nothing
        Me.txtDueDate.CustomFormat = "dd-MM-yyyy"
        Me.txtDueDate.FieldCode = Nothing
        Me.txtDueDate.FieldDesc = Nothing
        Me.txtDueDate.FieldMaxLength = 0
        Me.txtDueDate.FieldName = Nothing
        Me.txtDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDueDate.isCalculatedField = False
        Me.txtDueDate.IsSourceFromTable = False
        Me.txtDueDate.IsSourceFromValueList = False
        Me.txtDueDate.IsUnique = False
        Me.txtDueDate.Location = New System.Drawing.Point(70, 57)
        Me.txtDueDate.MendatroryField = False
        Me.txtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.MyLinkLable1 = Me.RadLabel17
        Me.txtDueDate.MyLinkLable2 = Nothing
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.ReferenceFieldDesc = Nothing
        Me.txtDueDate.ReferenceFieldName = Nothing
        Me.txtDueDate.ReferenceTableName = Nothing
        Me.txtDueDate.Size = New System.Drawing.Size(81, 18)
        Me.txtDueDate.TabIndex = 3
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(6, 58)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 12
        Me.RadLabel17.Text = "Due Date"
        '
        'gv2
        '
        Me.gv2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(2, 34)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1163, 303)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        Me.gv2.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvAC)
        Me.RadPageViewPage3.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(902, 445)
        Me.RadPageViewPage3.Text = "Additional Charges"
        '
        'gvAC
        '
        Me.gvAC.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvAC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvAC.ForeColor = System.Drawing.Color.Black
        Me.gvAC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAC.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAC.MasterTemplate.AllowDeleteRow = False
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(902, 396)
        Me.gvAC.TabIndex = 3
        Me.gvAC.TabStop = False
        Me.gvAC.Text = "RadGridView1"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadLabel31)
        Me.RadPanel1.Controls.Add(Me.lblAddCharges)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 396)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(902, 49)
        Me.RadPanel1.TabIndex = 0
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(657, 16)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 130
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(788, 16)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 131
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(902, 445)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(902, 445)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(902, 445)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(902, 445)
        Me.UcAttachment1.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage4.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmountAfterComm)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.lblCommAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscPer)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.txtComment)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1168, 445)
        Me.RadPageViewPage4.Text = "Total"
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(80, 325)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel29.TabIndex = 1391
        Me.MyLabel29.Text = "Actual TCS Tax Base Amt"
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(224, 323)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1390
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(92, 349)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel27.TabIndex = 1389
        Me.MyLabel27.Text = "TCS Tax Base Amount"
        '
        'txttcstaxbaseamount
        '
        Me.txttcstaxbaseamount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txttcstaxbaseamount.CalculationExpression = Nothing
        Me.txttcstaxbaseamount.DecimalPlaces = 0
        Me.txttcstaxbaseamount.FieldCode = Nothing
        Me.txttcstaxbaseamount.FieldDesc = Nothing
        Me.txttcstaxbaseamount.FieldMaxLength = 0
        Me.txttcstaxbaseamount.FieldName = Nothing
        Me.txttcstaxbaseamount.isCalculatedField = False
        Me.txttcstaxbaseamount.IsSourceFromTable = False
        Me.txttcstaxbaseamount.IsSourceFromValueList = False
        Me.txttcstaxbaseamount.IsUnique = False
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(225, 345)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(115, 20)
        Me.txttcstaxbaseamount.TabIndex = 1388
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(829, 28)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel15.TabIndex = 154
        Me.MyLabel15.Text = "Amount After Commission"
        Me.MyLabel15.Visible = False
        '
        'lblAmountAfterComm
        '
        Me.lblAmountAfterComm.AutoSize = False
        Me.lblAmountAfterComm.BorderVisible = True
        Me.lblAmountAfterComm.FieldName = Nothing
        Me.lblAmountAfterComm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountAfterComm.Location = New System.Drawing.Point(974, 28)
        Me.lblAmountAfterComm.Name = "lblAmountAfterComm"
        Me.lblAmountAfterComm.Size = New System.Drawing.Size(110, 18)
        Me.lblAmountAfterComm.TabIndex = 153
        Me.lblAmountAfterComm.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmountAfterComm.Visible = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(103, 225)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel14.TabIndex = 152
        Me.MyLabel14.Text = "Commission Amount"
        '
        'lblCommAmt
        '
        Me.lblCommAmt.AutoSize = False
        Me.lblCommAmt.BorderVisible = True
        Me.lblCommAmt.FieldName = Nothing
        Me.lblCommAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommAmt.Location = New System.Drawing.Point(222, 225)
        Me.lblCommAmt.Name = "lblCommAmt"
        Me.lblCommAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblCommAmt.TabIndex = 151
        Me.lblCommAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(223, 147)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 7
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(78, 147)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 150
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(147, 99)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Discount On"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnAmt)
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnRate)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(222, 99)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(141, 21)
        Me.RadGroupBox3.TabIndex = 4
        '
        'chkDiscountOnAmt
        '
        Me.chkDiscountOnAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiscountOnAmt.Location = New System.Drawing.Point(66, 2)
        Me.chkDiscountOnAmt.Name = "chkDiscountOnAmt"
        Me.chkDiscountOnAmt.Size = New System.Drawing.Size(59, 16)
        Me.chkDiscountOnAmt.TabIndex = 1
        Me.chkDiscountOnAmt.Text = "Amount"
        '
        'chkDiscountOnRate
        '
        Me.chkDiscountOnRate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDiscountOnRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiscountOnRate.Location = New System.Drawing.Point(15, 2)
        Me.chkDiscountOnRate.Name = "chkDiscountOnRate"
        Me.chkDiscountOnRate.Size = New System.Drawing.Size(44, 16)
        Me.chkDiscountOnRate.TabIndex = 0
        Me.chkDiscountOnRate.Text = "Rate"
        Me.chkDiscountOnRate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtDiscAmt
        '
        Me.txtDiscAmt.BackColor = System.Drawing.Color.White
        Me.txtDiscAmt.CalculationExpression = Nothing
        Me.txtDiscAmt.DecimalPlaces = 5
        Me.txtDiscAmt.FieldCode = Nothing
        Me.txtDiscAmt.FieldDesc = Nothing
        Me.txtDiscAmt.FieldMaxLength = 0
        Me.txtDiscAmt.FieldName = Nothing
        Me.txtDiscAmt.isCalculatedField = False
        Me.txtDiscAmt.IsSourceFromTable = False
        Me.txtDiscAmt.IsSourceFromValueList = False
        Me.txtDiscAmt.IsUnique = False
        Me.txtDiscAmt.Location = New System.Drawing.Point(283, 123)
        Me.txtDiscAmt.MendatroryField = False
        Me.txtDiscAmt.MyLinkLable1 = Nothing
        Me.txtDiscAmt.MyLinkLable2 = Nothing
        Me.txtDiscAmt.Name = "txtDiscAmt"
        Me.txtDiscAmt.ReferenceFieldDesc = Nothing
        Me.txtDiscAmt.ReferenceFieldName = Nothing
        Me.txtDiscAmt.ReferenceTableName = Nothing
        Me.txtDiscAmt.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscAmt.TabIndex = 6
        Me.txtDiscAmt.Text = "0"
        Me.txtDiscAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscAmt.Value = 0R
        '
        'txtDiscPer
        '
        Me.txtDiscPer.BackColor = System.Drawing.Color.White
        Me.txtDiscPer.CalculationExpression = Nothing
        Me.txtDiscPer.DecimalPlaces = 5
        Me.txtDiscPer.FieldCode = Nothing
        Me.txtDiscPer.FieldDesc = Nothing
        Me.txtDiscPer.FieldMaxLength = 0
        Me.txtDiscPer.FieldName = Nothing
        Me.txtDiscPer.isCalculatedField = False
        Me.txtDiscPer.IsSourceFromTable = False
        Me.txtDiscPer.IsSourceFromValueList = False
        Me.txtDiscPer.IsUnique = False
        Me.txtDiscPer.Location = New System.Drawing.Point(222, 123)
        Me.txtDiscPer.MendatroryField = False
        Me.txtDiscPer.MyLinkLable1 = Nothing
        Me.txtDiscPer.MyLinkLable2 = Nothing
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.ReferenceFieldDesc = Nothing
        Me.txtDiscPer.ReferenceFieldName = Nothing
        Me.txtDiscPer.ReferenceTableName = Nothing
        Me.txtDiscPer.Size = New System.Drawing.Size(39, 20)
        Me.txtDiscPer.TabIndex = 5
        Me.txtDiscPer.Text = "0"
        Me.txtDiscPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscPer.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(264, 125)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel1.TabIndex = 148
        Me.MyLabel1.Text = "%"
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.lblEffectiveFrom)
        Me.pnlCurrConv.Controls.Add(Me.txtApplicableFrom)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(96, 28)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(688, 38)
        Me.pnlCurrConv.TabIndex = 0
        '
        'txtConversionRate
        '
        Me.txtConversionRate.BackColor = System.Drawing.Color.White
        Me.txtConversionRate.CalculationExpression = Nothing
        Me.txtConversionRate.DecimalPlaces = 2
        Me.txtConversionRate.FieldCode = Nothing
        Me.txtConversionRate.FieldDesc = Nothing
        Me.txtConversionRate.FieldMaxLength = 0
        Me.txtConversionRate.FieldName = Nothing
        Me.txtConversionRate.isCalculatedField = False
        Me.txtConversionRate.IsSourceFromTable = False
        Me.txtConversionRate.IsSourceFromValueList = False
        Me.txtConversionRate.IsUnique = False
        Me.txtConversionRate.Location = New System.Drawing.Point(290, 8)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.ReferenceFieldDesc = Nothing
        Me.txtConversionRate.ReferenceFieldName = Nothing
        Me.txtConversionRate.ReferenceTableName = Nothing
        Me.txtConversionRate.Size = New System.Drawing.Size(124, 20)
        Me.txtConversionRate.TabIndex = 1
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.Enabled = False
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(59, 10)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(136, 22)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(420, 12)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 141
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(516, 10)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(119, 18)
        Me.txtApplicableFrom.TabIndex = 2
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(4, 12)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 137
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(193, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 139
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(80, 276)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 135
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(223, 276)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 11
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(429, 68)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(355, 164)
        Me.txtComment.TabIndex = 2
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(369, 72)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 127
        Me.RadLabel14.Text = "Comment"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(96, 203)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(117, 303)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 123
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(223, 301)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 12
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(140, 249)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 122
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(223, 249)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 10
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(223, 203)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 9
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(223, 173)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 8
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(223, 72)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 1
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(116, 173)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(32, 72)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(380, 2)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(69, 22)
        Me.btnHistory.TabIndex = 5
        Me.btnHistory.Text = "History"
        Me.btnHistory.Visible = False
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnpreview, Me.btnsend, Me.btnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(1020, 2)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 9
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnpreview
        '
        Me.btnpreview.AccessibleDescription = "Preview"
        Me.btnpreview.AccessibleName = "Preview"
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Text = "Preview"
        '
        'btnsend
        '
        Me.btnsend.AccessibleDescription = "Send E-Mail/SMS"
        Me.btnsend.AccessibleName = "Send E-Mail/SMS"
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Text = "Send E-Mail/SMS"
        '
        'btnSendForApproval
        '
        Me.btnSendForApproval.AccessibleDescription = "Send For Approval"
        Me.btnSendForApproval.AccessibleName = "Send For Approval"
        Me.btnSendForApproval.Name = "btnSendForApproval"
        Me.btnSendForApproval.Text = "Send For Approval"
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(309, 2)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(69, 22)
        Me.btnCopy.TabIndex = 4
        Me.btnCopy.Text = "Copy SO"
        Me.btnCopy.Visible = False
        '
        'chkRateUserCustomer
        '
        Me.chkRateUserCustomer.Enabled = False
        Me.chkRateUserCustomer.IsThreeState = True
        Me.chkRateUserCustomer.Location = New System.Drawing.Point(856, 2)
        Me.chkRateUserCustomer.Name = "chkRateUserCustomer"
        Me.chkRateUserCustomer.Size = New System.Drawing.Size(158, 18)
        Me.chkRateUserCustomer.TabIndex = 6
        Me.chkRateUserCustomer.Text = "User Customer Rate Setting"
        Me.chkRateUserCustomer.Visible = False
        '
        'chkRateDefaultSetting
        '
        Me.chkRateDefaultSetting.Enabled = False
        Me.chkRateDefaultSetting.IsThreeState = True
        Me.chkRateDefaultSetting.Location = New System.Drawing.Point(853, 10)
        Me.chkRateDefaultSetting.Name = "chkRateDefaultSetting"
        Me.chkRateDefaultSetting.Size = New System.Drawing.Size(120, 18)
        Me.chkRateDefaultSetting.TabIndex = 5
        Me.chkRateDefaultSetting.Text = "Rate Default Setting"
        Me.chkRateDefaultSetting.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(235, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(452, 2)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 7
        Me.btnAmendment.Text = "Amendment"
        '
        'lblAmbendmentNoCaption
        '
        Me.lblAmbendmentNoCaption.FieldName = Nothing
        Me.lblAmbendmentNoCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmbendmentNoCaption.Location = New System.Drawing.Point(743, 5)
        Me.lblAmbendmentNoCaption.Name = "lblAmbendmentNoCaption"
        Me.lblAmbendmentNoCaption.Size = New System.Drawing.Size(85, 16)
        Me.lblAmbendmentNoCaption.TabIndex = 8
        Me.lblAmbendmentNoCaption.Text = "Amendment No"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(157, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(81, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1112, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1189, 25)
        Me.Panel2.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1189, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "E-Mail/SMS Setting"
        Me.RadMenuItem3.AccessibleName = "E-Mail/SMS Setting"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "E-Mail/SMS Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem4.AccessibleName = "RadMenuItem4"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "RadMenuItem5"
        Me.RadMenuItem5.AccessibleName = "RadMenuItem5"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Footer Setting"
        '
        'txtTCSTaxRate
        '
        Me.txtTCSTaxRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTCSTaxRate.CalculationExpression = Nothing
        Me.txtTCSTaxRate.DecimalPlaces = 0
        Me.txtTCSTaxRate.FieldCode = Nothing
        Me.txtTCSTaxRate.FieldDesc = Nothing
        Me.txtTCSTaxRate.FieldMaxLength = 0
        Me.txtTCSTaxRate.FieldName = Nothing
        Me.txtTCSTaxRate.isCalculatedField = False
        Me.txtTCSTaxRate.IsSourceFromTable = False
        Me.txtTCSTaxRate.IsSourceFromValueList = False
        Me.txtTCSTaxRate.IsUnique = False
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(225, 370)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(115, 20)
        Me.txtTCSTaxRate.TabIndex = 1402
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
        '
        'frmDeliveryOrderProductSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmDeliveryOrderProductSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Delivery Order"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkSameBillShip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblARSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLedgerOutstanding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShortcloseDO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPendingDO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInsuranceInclude, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBookingwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustPODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoadPermitNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCommApply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlPaymentTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDispatchTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCloseRemarksdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispatchPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSOvalidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmountAfterComm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtShipToLocation As common.UserControls.txtFinder
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents txtDeliveryDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents btnAmendment As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblShipToLocation As common.Controls.MyLabel
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents lblSONo As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblAmbendmentNoCaption As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents chkRateUserCustomer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRateDefaultSetting As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents txtPriceCode As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblInvoiceDiscAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPriceGroupCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents btnCopy As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.Controls.MyTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnpreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkclose As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblCloseRemarksdesc As common.Controls.MyLabel
    Friend WithEvents txtCloseRemarks As common.UserControls.txtFinder
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtDispatchDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtSOvalidity As common.MyNumBox
    Friend WithEvents lblKMReading As common.Controls.MyLabel
    Friend WithEvents txtDispatchPeriod As common.MyNumBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents ddlPaymentTerms As common.Controls.MyComboBox
    Friend WithEvents lblInvoiceType As common.Controls.MyLabel
    Friend WithEvents ddlDispatchTerms As common.Controls.MyComboBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents chkCommApply As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents lblAmountAfterComm As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents lblCommAmt As common.Controls.MyLabel
    Friend WithEvents txtVehicleCapacity As common.MyNumBox
    Friend WithEvents lblVhicleNo As common.Controls.MyTextBox
    Friend WithEvents txtRoadPermitNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtrate As common.MyNumBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtAdvance As common.MyNumBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtCustPODate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents fndBookingNo As common.UserControls.txtFinder
    Friend WithEvents chkItemwise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkBookingwise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtTransporterCode As common.UserControls.txtFinder
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkInsuranceInclude As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIsTaxable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblCreditLimit As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblAdvanceSecurity As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lblTotalOutstansing As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents lblRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents lblLedgerOutstanding As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents lblShortcloseDO As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents lblPendingDO As common.Controls.MyLabel
    Friend WithEvents chkSameBillShip As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents lblReverseAdvanceSec As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents lblReverseRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents lblARSecurity As common.Controls.MyLabel
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
End Class

