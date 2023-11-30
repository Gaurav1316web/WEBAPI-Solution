<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSNShipment
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSNShipment))
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim WindowsSettings1 As Telerik.WinControls.WindowsSettings = New Telerik.WinControls.WindowsSettings()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lbltransporter = New common.Controls.MyLabel()
        Me.fndtransporter = New common.UserControls.txtFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtOrderQty = New common.MyNumBox()
        Me.lblOrderQty = New common.Controls.MyLabel()
        Me.txtWeightmentNo = New common.UserControls.txtMultiSelectFinder()
        Me.txtBarCode = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtShipToDeliveryAt = New common.Controls.MyTextBox()
        Me.dtpLR_GR_Date = New common.Controls.MyDateTimePicker()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtBeejakNo = New common.Controls.MyTextBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lblTotalOutstansing = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtLR_GR_No = New common.Controls.MyTextBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtEWayBillDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtElecttefNo = New common.Controls.MyTextBox()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtEWayBillNo = New common.Controls.MyTextBox()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.chkIsTaxable = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtpodate = New common.Controls.MyDateTimePicker()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtForm38 = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtPONo = New common.Controls.MyTextBox()
        Me.lblInvoiceNo = New common.Controls.MyLabel()
        Me.txtInvoiceNo = New common.Controls.MyLabel()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtVehicleCode = New common.UserControls.txtFinder()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.txtPriceGroupCode = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.chkAutoTransfer = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblFromLoc = New common.Controls.MyLabel()
        Me.txtFromLoc = New common.UserControls.txtFinder()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtFinder()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.lblInvoiceType = New common.Controls.MyLabel()
        Me.ddlInvoiceType = New common.Controls.MyComboBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.txtPriceCode = New common.Controls.MyLabel()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.txtInvNo = New common.Controls.MyTextBox()
        Me.dtpInvoice = New common.Controls.MyDateTimePicker()
        Me.chkCreateAutoInvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkInternal = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpChallan = New common.Controls.MyDateTimePicker()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblDept = New common.Controls.MyLabel()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.RadLabel28 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.txtGRNo = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.chkCreateAutoReceipt = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.chkFillDCSDetail = New Telerik.WinControls.UI.RadCheckBox()
        Me.gvDCS = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.lblARSecurity = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.lblReverseAdvanceSec = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.lblReverseRefund = New common.Controls.MyLabel()
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
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.lblCreditLimit = New common.Controls.MyLabel()
        Me.btnReverseAndUnpost = New Telerik.WinControls.UI.RadButton()
        Me.pnlMannualInvoiceNo = New System.Windows.Forms.Panel()
        Me.TxtInvoiceManualNoWithPrefix = New common.Controls.MyTextBox()
        Me.txtMannaulInvoiceNo = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblInvoiceDiscAmt = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkDiscountOnAmt = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDiscountOnRate = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDiscAmt = New common.MyNumBox()
        Me.txtDiscPer = New common.MyNumBox()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.chkVendorGrossReceipt = New common.Controls.MyCheckBox()
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
        Me.btnPrnt = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnPrintA4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrintA5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDCSPrint = New Telerik.WinControls.UI.RadSplitButton()
        Me.rbtnDCSPrint = New Telerik.WinControls.UI.RadMenuItem()
        Me.rbtnDCSSummary = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnAddCost = New Telerik.WinControls.UI.RadButton()
        Me.btnInvoiceJE = New Telerik.WinControls.UI.RadButton()
        Me.btnDeliveredTo = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnpreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnsend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrintInvoice = New Telerik.WinControls.UI.RadButton()
        Me.chkRateUserCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRateDefaultSetting = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtGEDate = New common.Controls.MyDateTimePicker()
        Me.txtGENo = New common.Controls.MyTextBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Head = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_details = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lbltransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipToDeliveryAt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLR_GR_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBeejakNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLR_GR_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtEWayBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtElecttefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtForm38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateAutoInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateAutoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.chkFillDCSDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblARSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMannualInvoiceNo.SuspendLayout()
        CType(Me.TxtInvoiceManualNoWithPrefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMannaulInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorGrossReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.btnPrnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDCSPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeliveredTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chkRateUserCustomer.SuspendLayout()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGEDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtGEDate.SuspendLayout()
        CType(Me.txtGENo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrnt)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDCSPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAddCost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnInvoiceJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeliveredTo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintInvoice)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateUserCustomer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtGEDate)
        Me.SplitContainer1.Size = New System.Drawing.Size(1255, 569)
        Me.SplitContainer1.SplitterDistance = 537
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
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
        Me.RadPageView1.Size = New System.Drawing.Size(1255, 537)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lbltransporter)
        Me.RadPageViewPage1.Controls.Add(Me.fndtransporter)
        Me.RadPageViewPage1.Controls.Add(Me.txtOrderQty)
        Me.RadPageViewPage1.Controls.Add(Me.lblOrderQty)
        Me.RadPageViewPage1.Controls.Add(Me.txtWeightmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtBarCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToDeliveryAt)
        Me.RadPageViewPage1.Controls.Add(Me.dtpLR_GR_Date)
        Me.RadPageViewPage1.Controls.Add(Me.txtBeejakNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel35)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotalOutstansing)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.txtLR_GR_No)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.chkIsTaxable)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.txtpodate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtForm38)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtPONo)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceGroupCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.chkAutoTransfer)
        Me.RadPageViewPage1.Controls.Add(Me.lblFromLoc)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLoc)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceType)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.ddlInvoiceType)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteDesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvNo)
        Me.RadPageViewPage1.Controls.Add(Me.dtpInvoice)
        Me.RadPageViewPage1.Controls.Add(Me.chkCreateAutoInvoice)
        Me.RadPageViewPage1.Controls.Add(Me.chkInternal)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.dtpChallan)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblDept)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel28)
        Me.RadPageViewPage1.Controls.Add(Me.txtDept)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.chkCreateAutoReceipt)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.btnDrillDown)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(111.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1234, 491)
        Me.RadPageViewPage1.Text = "Shipment/Dispatch"
        '
        'lbltransporter
        '
        Me.lbltransporter.AutoSize = False
        Me.lbltransporter.BorderVisible = True
        Me.lbltransporter.FieldName = Nothing
        Me.lbltransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltransporter.Location = New System.Drawing.Point(770, 112)
        Me.lbltransporter.Name = "lbltransporter"
        Me.lbltransporter.Size = New System.Drawing.Size(228, 18)
        Me.lbltransporter.TabIndex = 38
        Me.lbltransporter.TextWrap = False
        '
        'fndtransporter
        '
        Me.fndtransporter.CalculationExpression = Nothing
        Me.fndtransporter.FieldCode = Nothing
        Me.fndtransporter.FieldDesc = Nothing
        Me.fndtransporter.FieldMaxLength = 0
        Me.fndtransporter.FieldName = Nothing
        Me.fndtransporter.isCalculatedField = False
        Me.fndtransporter.IsSourceFromTable = False
        Me.fndtransporter.IsSourceFromValueList = False
        Me.fndtransporter.IsUnique = False
        Me.fndtransporter.Location = New System.Drawing.Point(634, 112)
        Me.fndtransporter.MendatroryField = False
        Me.fndtransporter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtransporter.MyLinkLable1 = Me.lblRouteNo
        Me.fndtransporter.MyLinkLable2 = Nothing
        Me.fndtransporter.MyReadOnly = False
        Me.fndtransporter.MyShowMasterFormButton = False
        Me.fndtransporter.Name = "fndtransporter"
        Me.fndtransporter.ReferenceFieldDesc = Nothing
        Me.fndtransporter.ReferenceFieldName = Nothing
        Me.fndtransporter.ReferenceTableName = Nothing
        Me.fndtransporter.Size = New System.Drawing.Size(130, 19)
        Me.fndtransporter.TabIndex = 1433
        Me.fndtransporter.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(0, 157)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 124
        Me.lblRouteNo.Text = "Route No"
        '
        'txtOrderQty
        '
        Me.txtOrderQty.CalculationExpression = Nothing
        Me.txtOrderQty.DecimalPlaces = 0
        Me.txtOrderQty.FieldCode = Nothing
        Me.txtOrderQty.FieldDesc = Nothing
        Me.txtOrderQty.FieldMaxLength = 0
        Me.txtOrderQty.FieldName = Nothing
        Me.txtOrderQty.isCalculatedField = False
        Me.txtOrderQty.IsSourceFromTable = False
        Me.txtOrderQty.IsSourceFromValueList = False
        Me.txtOrderQty.IsUnique = False
        Me.txtOrderQty.Location = New System.Drawing.Point(1099, 177)
        Me.txtOrderQty.MendatroryField = True
        Me.txtOrderQty.MyLinkLable1 = Me.lblOrderQty
        Me.txtOrderQty.MyLinkLable2 = Nothing
        Me.txtOrderQty.Name = "txtOrderQty"
        Me.txtOrderQty.ReferenceFieldDesc = Nothing
        Me.txtOrderQty.ReferenceFieldName = Nothing
        Me.txtOrderQty.ReferenceTableName = Nothing
        Me.txtOrderQty.Size = New System.Drawing.Size(130, 20)
        Me.txtOrderQty.TabIndex = 1432
        Me.txtOrderQty.Text = "0"
        Me.txtOrderQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOrderQty.Value = 0R
        '
        'lblOrderQty
        '
        Me.lblOrderQty.FieldName = Nothing
        Me.lblOrderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderQty.Location = New System.Drawing.Point(1005, 179)
        Me.lblOrderQty.Name = "lblOrderQty"
        Me.lblOrderQty.Size = New System.Drawing.Size(83, 16)
        Me.lblOrderQty.TabIndex = 1431
        Me.lblOrderQty.Text = "Order Qty (MT)"
        '
        'txtWeightmentNo
        '
        Me.txtWeightmentNo.arrDispalyMember = Nothing
        Me.txtWeightmentNo.arrValueMember = Nothing
        Me.txtWeightmentNo.Location = New System.Drawing.Point(98, 177)
        Me.txtWeightmentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightmentNo.MyLinkLable1 = Nothing
        Me.txtWeightmentNo.MyLinkLable2 = Nothing
        Me.txtWeightmentNo.MyNullText = ""
        Me.txtWeightmentNo.Name = "txtWeightmentNo"
        Me.txtWeightmentNo.Size = New System.Drawing.Size(143, 20)
        Me.txtWeightmentNo.TabIndex = 1430
        '
        'txtBarCode
        '
        Me.txtBarCode.CalculationExpression = Nothing
        Me.txtBarCode.FieldCode = Nothing
        Me.txtBarCode.FieldDesc = Nothing
        Me.txtBarCode.FieldMaxLength = 0
        Me.txtBarCode.FieldName = Nothing
        Me.txtBarCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCode.isCalculatedField = False
        Me.txtBarCode.IsSourceFromTable = False
        Me.txtBarCode.IsSourceFromValueList = False
        Me.txtBarCode.IsUnique = False
        Me.txtBarCode.Location = New System.Drawing.Point(634, 92)
        Me.txtBarCode.MaxLength = 200
        Me.txtBarCode.MendatroryField = False
        Me.txtBarCode.MyLinkLable1 = Me.MyLabel16
        Me.txtBarCode.MyLinkLable2 = Nothing
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.ReferenceFieldDesc = Nothing
        Me.txtBarCode.ReferenceFieldName = Nothing
        Me.txtBarCode.ReferenceTableName = Nothing
        Me.txtBarCode.Size = New System.Drawing.Size(261, 18)
        Me.txtBarCode.TabIndex = 18
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(537, 90)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel16.TabIndex = 59
        Me.MyLabel16.Text = "Bar Code"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(779, 156)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel21.TabIndex = 1429
        Me.MyLabel21.Text = "Ship To/Delivery At"
        '
        'txtShipToDeliveryAt
        '
        Me.txtShipToDeliveryAt.CalculationExpression = Nothing
        Me.txtShipToDeliveryAt.FieldCode = Nothing
        Me.txtShipToDeliveryAt.FieldDesc = Nothing
        Me.txtShipToDeliveryAt.FieldMaxLength = 0
        Me.txtShipToDeliveryAt.FieldName = Nothing
        Me.txtShipToDeliveryAt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipToDeliveryAt.isCalculatedField = False
        Me.txtShipToDeliveryAt.IsSourceFromTable = False
        Me.txtShipToDeliveryAt.IsSourceFromValueList = False
        Me.txtShipToDeliveryAt.IsUnique = False
        Me.txtShipToDeliveryAt.Location = New System.Drawing.Point(888, 155)
        Me.txtShipToDeliveryAt.MaxLength = 50
        Me.txtShipToDeliveryAt.MendatroryField = False
        Me.txtShipToDeliveryAt.MyLinkLable1 = Me.MyLabel21
        Me.txtShipToDeliveryAt.MyLinkLable2 = Nothing
        Me.txtShipToDeliveryAt.Name = "txtShipToDeliveryAt"
        Me.txtShipToDeliveryAt.ReferenceFieldDesc = Nothing
        Me.txtShipToDeliveryAt.ReferenceFieldName = Nothing
        Me.txtShipToDeliveryAt.ReferenceTableName = Nothing
        Me.txtShipToDeliveryAt.Size = New System.Drawing.Size(341, 18)
        Me.txtShipToDeliveryAt.TabIndex = 1428
        '
        'dtpLR_GR_Date
        '
        Me.dtpLR_GR_Date.CalculationExpression = Nothing
        Me.dtpLR_GR_Date.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpLR_GR_Date.FieldCode = Nothing
        Me.dtpLR_GR_Date.FieldDesc = Nothing
        Me.dtpLR_GR_Date.FieldMaxLength = 0
        Me.dtpLR_GR_Date.FieldName = Nothing
        Me.dtpLR_GR_Date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLR_GR_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLR_GR_Date.isCalculatedField = False
        Me.dtpLR_GR_Date.IsSourceFromTable = False
        Me.dtpLR_GR_Date.IsSourceFromValueList = False
        Me.dtpLR_GR_Date.IsUnique = False
        Me.dtpLR_GR_Date.Location = New System.Drawing.Point(857, 134)
        Me.dtpLR_GR_Date.MendatroryField = False
        Me.dtpLR_GR_Date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLR_GR_Date.MyLinkLable1 = Me.MyLabel20
        Me.dtpLR_GR_Date.MyLinkLable2 = Nothing
        Me.dtpLR_GR_Date.Name = "dtpLR_GR_Date"
        Me.dtpLR_GR_Date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLR_GR_Date.ReferenceFieldDesc = Nothing
        Me.dtpLR_GR_Date.ReferenceFieldName = Nothing
        Me.dtpLR_GR_Date.ReferenceTableName = Nothing
        Me.dtpLR_GR_Date.Size = New System.Drawing.Size(126, 18)
        Me.dtpLR_GR_Date.TabIndex = 1426
        Me.dtpLR_GR_Date.TabStop = False
        Me.dtpLR_GR_Date.Text = "13/06/2011 11:29 AM"
        Me.dtpLR_GR_Date.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(824, 135)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel20.TabIndex = 1427
        Me.MyLabel20.Text = "Date"
        '
        'txtBeejakNo
        '
        Me.txtBeejakNo.CalculationExpression = Nothing
        Me.txtBeejakNo.FieldCode = Nothing
        Me.txtBeejakNo.FieldDesc = Nothing
        Me.txtBeejakNo.FieldMaxLength = 0
        Me.txtBeejakNo.FieldName = Nothing
        Me.txtBeejakNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBeejakNo.isCalculatedField = False
        Me.txtBeejakNo.IsSourceFromTable = False
        Me.txtBeejakNo.IsSourceFromValueList = False
        Me.txtBeejakNo.IsUnique = False
        Me.txtBeejakNo.Location = New System.Drawing.Point(311, 177)
        Me.txtBeejakNo.MaxLength = 200
        Me.txtBeejakNo.MendatroryField = False
        Me.txtBeejakNo.MyLinkLable1 = Me.MyLabel16
        Me.txtBeejakNo.MyLinkLable2 = Nothing
        Me.txtBeejakNo.Name = "txtBeejakNo"
        Me.txtBeejakNo.ReferenceFieldDesc = Nothing
        Me.txtBeejakNo.ReferenceFieldName = Nothing
        Me.txtBeejakNo.ReferenceTableName = Nothing
        Me.txtBeejakNo.Size = New System.Drawing.Size(218, 18)
        Me.txtBeejakNo.TabIndex = 1422
        Me.txtBeejakNo.TabStop = False
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(246, 177)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel17.TabIndex = 1423
        Me.MyLabel17.Text = "Beejak No"
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Location = New System.Drawing.Point(779, 201)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel35.TabIndex = 1418
        Me.MyLabel35.Text = "Credit Limit"
        '
        'lblTotalOutstansing
        '
        Me.lblTotalOutstansing.AutoSize = False
        Me.lblTotalOutstansing.BorderVisible = True
        Me.lblTotalOutstansing.FieldName = Nothing
        Me.lblTotalOutstansing.Location = New System.Drawing.Point(888, 201)
        Me.lblTotalOutstansing.Name = "lblTotalOutstansing"
        Me.lblTotalOutstansing.Size = New System.Drawing.Size(109, 19)
        Me.lblTotalOutstansing.TabIndex = 1417
        Me.lblTotalOutstansing.TextWrap = False
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(537, 135)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel19.TabIndex = 1425
        Me.MyLabel19.Text = "LR/GR No"
        '
        'txtLR_GR_No
        '
        Me.txtLR_GR_No.CalculationExpression = Nothing
        Me.txtLR_GR_No.FieldCode = Nothing
        Me.txtLR_GR_No.FieldDesc = Nothing
        Me.txtLR_GR_No.FieldMaxLength = 0
        Me.txtLR_GR_No.FieldName = Nothing
        Me.txtLR_GR_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLR_GR_No.isCalculatedField = False
        Me.txtLR_GR_No.IsSourceFromTable = False
        Me.txtLR_GR_No.IsSourceFromValueList = False
        Me.txtLR_GR_No.IsUnique = False
        Me.txtLR_GR_No.Location = New System.Drawing.Point(634, 134)
        Me.txtLR_GR_No.MaxLength = 50
        Me.txtLR_GR_No.MendatroryField = False
        Me.txtLR_GR_No.MyLinkLable1 = Me.MyLabel19
        Me.txtLR_GR_No.MyLinkLable2 = Nothing
        Me.txtLR_GR_No.Name = "txtLR_GR_No"
        Me.txtLR_GR_No.ReferenceFieldDesc = Nothing
        Me.txtLR_GR_No.ReferenceFieldName = Nothing
        Me.txtLR_GR_No.ReferenceTableName = Nothing
        Me.txtLR_GR_No.Size = New System.Drawing.Size(181, 18)
        Me.txtLR_GR_No.TabIndex = 1424
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtEWayBillDate)
        Me.RadGroupBox5.Controls.Add(Me.txtElecttefNo)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel38)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel43)
        Me.RadGroupBox5.Controls.Add(Me.txtEWayBillNo)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel42)
        Me.RadGroupBox5.Controls.Add(Me.btnUpdate)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(1000, 38)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(229, 114)
        Me.RadGroupBox5.TabIndex = 1416
        '
        'txtEWayBillDate
        '
        Me.txtEWayBillDate.CalculationExpression = Nothing
        Me.txtEWayBillDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEWayBillDate.FieldCode = Nothing
        Me.txtEWayBillDate.FieldDesc = Nothing
        Me.txtEWayBillDate.FieldMaxLength = 0
        Me.txtEWayBillDate.FieldName = Nothing
        Me.txtEWayBillDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEWayBillDate.isCalculatedField = False
        Me.txtEWayBillDate.IsSourceFromTable = False
        Me.txtEWayBillDate.IsSourceFromValueList = False
        Me.txtEWayBillDate.IsUnique = False
        Me.txtEWayBillDate.Location = New System.Drawing.Point(107, 31)
        Me.txtEWayBillDate.MendatroryField = False
        Me.txtEWayBillDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEWayBillDate.MyLinkLable1 = Me.MyLabel43
        Me.txtEWayBillDate.MyLinkLable2 = Nothing
        Me.txtEWayBillDate.Name = "txtEWayBillDate"
        Me.txtEWayBillDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEWayBillDate.ReferenceFieldDesc = Nothing
        Me.txtEWayBillDate.ReferenceFieldName = Nothing
        Me.txtEWayBillDate.ReferenceTableName = Nothing
        Me.txtEWayBillDate.ShowCheckBox = True
        Me.txtEWayBillDate.Size = New System.Drawing.Size(93, 18)
        Me.txtEWayBillDate.TabIndex = 1404
        Me.txtEWayBillDate.TabStop = False
        Me.txtEWayBillDate.Text = "13/06/2011"
        Me.txtEWayBillDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(5, 31)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel43.TabIndex = 1405
        Me.MyLabel43.Text = "E Waybill Date"
        '
        'txtElecttefNo
        '
        Me.txtElecttefNo.CalculationExpression = Nothing
        Me.txtElecttefNo.FieldCode = Nothing
        Me.txtElecttefNo.FieldDesc = Nothing
        Me.txtElecttefNo.FieldMaxLength = 0
        Me.txtElecttefNo.FieldName = Nothing
        Me.txtElecttefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtElecttefNo.isCalculatedField = False
        Me.txtElecttefNo.IsSourceFromTable = False
        Me.txtElecttefNo.IsSourceFromValueList = False
        Me.txtElecttefNo.IsUnique = False
        Me.txtElecttefNo.Location = New System.Drawing.Point(107, 54)
        Me.txtElecttefNo.MaxLength = 10
        Me.txtElecttefNo.MendatroryField = False
        Me.txtElecttefNo.MyLinkLable1 = Me.MyLabel38
        Me.txtElecttefNo.MyLinkLable2 = Nothing
        Me.txtElecttefNo.Name = "txtElecttefNo"
        Me.txtElecttefNo.ReferenceFieldDesc = Nothing
        Me.txtElecttefNo.ReferenceFieldName = Nothing
        Me.txtElecttefNo.ReferenceTableName = Nothing
        Me.txtElecttefNo.Size = New System.Drawing.Size(111, 18)
        Me.txtElecttefNo.TabIndex = 1406
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(5, 56)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel38.TabIndex = 1407
        Me.MyLabel38.Text = "Electronic Ref No"
        '
        'txtEWayBillNo
        '
        Me.txtEWayBillNo.CalculationExpression = Nothing
        Me.txtEWayBillNo.FieldCode = Nothing
        Me.txtEWayBillNo.FieldDesc = Nothing
        Me.txtEWayBillNo.FieldMaxLength = 0
        Me.txtEWayBillNo.FieldName = Nothing
        Me.txtEWayBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillNo.isCalculatedField = False
        Me.txtEWayBillNo.IsSourceFromTable = False
        Me.txtEWayBillNo.IsSourceFromValueList = False
        Me.txtEWayBillNo.IsUnique = False
        Me.txtEWayBillNo.Location = New System.Drawing.Point(106, 8)
        Me.txtEWayBillNo.MaxLength = 30
        Me.txtEWayBillNo.MendatroryField = False
        Me.txtEWayBillNo.MyLinkLable1 = Me.MyLabel42
        Me.txtEWayBillNo.MyLinkLable2 = Nothing
        Me.txtEWayBillNo.Name = "txtEWayBillNo"
        Me.txtEWayBillNo.ReferenceFieldDesc = Nothing
        Me.txtEWayBillNo.ReferenceFieldName = Nothing
        Me.txtEWayBillNo.ReferenceTableName = Nothing
        Me.txtEWayBillNo.Size = New System.Drawing.Size(112, 18)
        Me.txtEWayBillNo.TabIndex = 1402
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(5, 8)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel42.TabIndex = 1403
        Me.MyLabel42.Text = "E Waybill No"
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(131, 80)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(93, 20)
        Me.btnUpdate.TabIndex = 41
        Me.btnUpdate.Text = "Update"
        '
        'chkIsTaxable
        '
        Me.chkIsTaxable.Location = New System.Drawing.Point(1000, 21)
        Me.chkIsTaxable.Name = "chkIsTaxable"
        Me.chkIsTaxable.Size = New System.Drawing.Size(69, 18)
        Me.chkIsTaxable.TabIndex = 1415
        Me.chkIsTaxable.Text = "Is Taxable"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(0, 177)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel18.TabIndex = 1421
        Me.MyLabel18.Text = "Weighment No"
        '
        'txtpodate
        '
        Me.txtpodate.CalculationExpression = Nothing
        Me.txtpodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtpodate.FieldCode = Nothing
        Me.txtpodate.FieldDesc = Nothing
        Me.txtpodate.FieldMaxLength = 0
        Me.txtpodate.FieldName = Nothing
        Me.txtpodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtpodate.isCalculatedField = False
        Me.txtpodate.IsSourceFromTable = False
        Me.txtpodate.IsSourceFromValueList = False
        Me.txtpodate.IsUnique = False
        Me.txtpodate.Location = New System.Drawing.Point(865, 46)
        Me.txtpodate.MendatroryField = False
        Me.txtpodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtpodate.MyLinkLable1 = Me.MyLabel14
        Me.txtpodate.MyLinkLable2 = Nothing
        Me.txtpodate.Name = "txtpodate"
        Me.txtpodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtpodate.ReferenceFieldDesc = Nothing
        Me.txtpodate.ReferenceFieldName = Nothing
        Me.txtpodate.ReferenceTableName = Nothing
        Me.txtpodate.Size = New System.Drawing.Size(126, 18)
        Me.txtpodate.TabIndex = 10
        Me.txtpodate.TabStop = False
        Me.txtpodate.Text = "13/06/2011 11:29 AM"
        Me.txtpodate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(832, 48)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel14.TabIndex = 139
        Me.MyLabel14.Text = "Date"
        '
        'txtForm38
        '
        Me.txtForm38.CalculationExpression = Nothing
        Me.txtForm38.FieldCode = Nothing
        Me.txtForm38.FieldDesc = Nothing
        Me.txtForm38.FieldMaxLength = 0
        Me.txtForm38.FieldName = Nothing
        Me.txtForm38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm38.isCalculatedField = False
        Me.txtForm38.IsSourceFromTable = False
        Me.txtForm38.IsSourceFromValueList = False
        Me.txtForm38.IsUnique = False
        Me.txtForm38.Location = New System.Drawing.Point(634, 155)
        Me.txtForm38.MaxLength = 200
        Me.txtForm38.MendatroryField = False
        Me.txtForm38.MyLinkLable1 = Me.MyLabel16
        Me.txtForm38.MyLinkLable2 = Nothing
        Me.txtForm38.Name = "txtForm38"
        Me.txtForm38.ReferenceFieldDesc = Nothing
        Me.txtForm38.ReferenceFieldName = Nothing
        Me.txtForm38.ReferenceTableName = Nothing
        Me.txtForm38.Size = New System.Drawing.Size(134, 18)
        Me.txtForm38.TabIndex = 22
        Me.txtForm38.TabStop = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(537, 155)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel13.TabIndex = 122
        Me.MyLabel13.Text = "Form38 No"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(537, 46)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel12.TabIndex = 137
        Me.MyLabel12.Text = "Customer PO No"
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
        Me.txtPONo.Location = New System.Drawing.Point(634, 46)
        Me.txtPONo.MaxLength = 200
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyLinkLable1 = Me.MyLabel12
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(183, 18)
        Me.txtPONo.TabIndex = 9
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.FieldName = Nothing
        Me.lblInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNo.Location = New System.Drawing.Point(833, 69)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(60, 16)
        Me.lblInvoiceNo.TabIndex = 134
        Me.lblInvoiceNo.Text = "Invoice No"
        Me.lblInvoiceNo.Visible = False
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.AutoSize = False
        Me.txtInvoiceNo.BorderVisible = True
        Me.txtInvoiceNo.FieldName = Nothing
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(899, 68)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(92, 18)
        Me.txtInvoiceNo.TabIndex = 41
        Me.txtInvoiceNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtInvoiceNo.Visible = False
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(244, 134)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(285, 18)
        Me.lblVehicleNo.TabIndex = 29
        Me.lblVehicleNo.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(779, 178)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel9.TabIndex = 132
        Me.MyLabel9.Text = "Document Amount"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(0, 134)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel10.TabIndex = 22
        Me.MyLabel10.Text = "Vehicle Code"
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
        Me.txtVehicleCode.Location = New System.Drawing.Point(98, 134)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.MyLabel10
        Me.txtVehicleCode.MyLinkLable2 = Me.lblVehicleNo
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.MyShowMasterFormButton = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.ReferenceFieldDesc = Nothing
        Me.txtVehicleCode.ReferenceFieldName = Nothing
        Me.txtVehicleCode.ReferenceTableName = Nothing
        Me.txtVehicleCode.Size = New System.Drawing.Size(143, 19)
        Me.txtVehicleCode.TabIndex = 19
        Me.txtVehicleCode.Value = ""
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(888, 177)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt1.TabIndex = 42
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPriceGroupCode
        '
        Me.txtPriceGroupCode.AutoSize = False
        Me.txtPriceGroupCode.BorderVisible = True
        Me.txtPriceGroupCode.FieldName = Nothing
        Me.txtPriceGroupCode.Location = New System.Drawing.Point(634, 201)
        Me.txtPriceGroupCode.Name = "txtPriceGroupCode"
        Me.txtPriceGroupCode.Size = New System.Drawing.Size(134, 19)
        Me.txtPriceGroupCode.TabIndex = 40
        Me.txtPriceGroupCode.TextWrap = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(537, 201)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel8.TabIndex = 39
        Me.MyLabel8.Text = "Price Group Code"
        '
        'chkAutoTransfer
        '
        Me.chkAutoTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoTransfer.Location = New System.Drawing.Point(887, 225)
        Me.chkAutoTransfer.Name = "chkAutoTransfer"
        Me.chkAutoTransfer.Size = New System.Drawing.Size(106, 16)
        Me.chkAutoTransfer.TabIndex = 19
        Me.chkAutoTransfer.Text = "Create Auto STN"
        Me.chkAutoTransfer.Visible = False
        '
        'lblFromLoc
        '
        Me.lblFromLoc.FieldName = Nothing
        Me.lblFromLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLoc.Location = New System.Drawing.Point(868, 247)
        Me.lblFromLoc.Name = "lblFromLoc"
        Me.lblFromLoc.Size = New System.Drawing.Size(79, 16)
        Me.lblFromLoc.TabIndex = 18
        Me.lblFromLoc.Text = "From Location"
        Me.lblFromLoc.Visible = False
        '
        'txtFromLoc
        '
        Me.txtFromLoc.CalculationExpression = Nothing
        Me.txtFromLoc.FieldCode = Nothing
        Me.txtFromLoc.FieldDesc = Nothing
        Me.txtFromLoc.FieldMaxLength = 0
        Me.txtFromLoc.FieldName = Nothing
        Me.txtFromLoc.isCalculatedField = False
        Me.txtFromLoc.IsSourceFromTable = False
        Me.txtFromLoc.IsSourceFromValueList = False
        Me.txtFromLoc.IsUnique = False
        Me.txtFromLoc.Location = New System.Drawing.Point(965, 243)
        Me.txtFromLoc.MendatroryField = False
        Me.txtFromLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLoc.MyLinkLable1 = Me.lblFromLoc
        Me.txtFromLoc.MyLinkLable2 = Me.lblShipToLocation
        Me.txtFromLoc.MyReadOnly = False
        Me.txtFromLoc.MyShowMasterFormButton = False
        Me.txtFromLoc.Name = "txtFromLoc"
        Me.txtFromLoc.ReferenceFieldDesc = Nothing
        Me.txtFromLoc.ReferenceFieldName = Nothing
        Me.txtFromLoc.ReferenceTableName = Nothing
        Me.txtFromLoc.Size = New System.Drawing.Size(143, 22)
        Me.txtFromLoc.TabIndex = 24
        Me.txtFromLoc.Value = ""
        Me.txtFromLoc.Visible = False
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(242, 67)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(287, 18)
        Me.lblShipToLocation.TabIndex = 17
        Me.lblShipToLocation.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(0, 47)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 27
        Me.RadLabel2.Text = "Customer No"
        '
        'txtCustomer
        '
        Me.txtCustomer.CalculationExpression = Nothing
        Me.txtCustomer.FieldCode = Nothing
        Me.txtCustomer.FieldDesc = Nothing
        Me.txtCustomer.FieldMaxLength = 0
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.isCalculatedField = False
        Me.txtCustomer.IsSourceFromTable = False
        Me.txtCustomer.IsSourceFromValueList = False
        Me.txtCustomer.IsUnique = False
        Me.txtCustomer.Location = New System.Drawing.Point(98, 45)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomer.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(143, 20)
        Me.txtCustomer.TabIndex = 8
        Me.txtCustomer.Value = ""
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(242, 46)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(286, 18)
        Me.lblCustomerName.TabIndex = 13
        Me.lblCustomerName.TextWrap = False
        '
        'lblInvoiceType
        '
        Me.lblInvoiceType.FieldName = Nothing
        Me.lblInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceType.Location = New System.Drawing.Point(665, 67)
        Me.lblInvoiceType.Name = "lblInvoiceType"
        Me.lblInvoiceType.Size = New System.Drawing.Size(70, 16)
        Me.lblInvoiceType.TabIndex = 12
        Me.lblInvoiceType.Text = "Invoice Type"
        Me.lblInvoiceType.Visible = False
        '
        'ddlInvoiceType
        '
        Me.ddlInvoiceType.AutoCompleteDisplayMember = Nothing
        Me.ddlInvoiceType.AutoCompleteValueMember = Nothing
        Me.ddlInvoiceType.CalculationExpression = Nothing
        Me.ddlInvoiceType.DropDownAnimationEnabled = True
        Me.ddlInvoiceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlInvoiceType.FieldCode = Nothing
        Me.ddlInvoiceType.FieldDesc = Nothing
        Me.ddlInvoiceType.FieldMaxLength = 0
        Me.ddlInvoiceType.FieldName = Nothing
        Me.ddlInvoiceType.isCalculatedField = False
        Me.ddlInvoiceType.IsSourceFromTable = False
        Me.ddlInvoiceType.IsSourceFromValueList = False
        Me.ddlInvoiceType.IsUnique = False
        Me.ddlInvoiceType.Location = New System.Drawing.Point(741, 66)
        Me.ddlInvoiceType.MendatroryField = True
        Me.ddlInvoiceType.MyLinkLable1 = Me.MyLabel7
        Me.ddlInvoiceType.MyLinkLable2 = Nothing
        Me.ddlInvoiceType.Name = "ddlInvoiceType"
        Me.ddlInvoiceType.ReferenceFieldDesc = Nothing
        Me.ddlInvoiceType.ReferenceFieldName = Nothing
        Me.ddlInvoiceType.ReferenceTableName = Nothing
        Me.ddlInvoiceType.Size = New System.Drawing.Size(86, 20)
        Me.ddlInvoiceType.TabIndex = 13
        Me.ddlInvoiceType.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(259, 96)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel7.TabIndex = 7
        Me.MyLabel7.Text = "%"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(244, 156)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(284, 17)
        Me.lblRouteDesc.TabIndex = 36
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
        Me.txtRouteNo.Location = New System.Drawing.Point(98, 155)
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
        Me.txtRouteNo.Size = New System.Drawing.Size(143, 20)
        Me.txtRouteNo.TabIndex = 21
        Me.txtRouteNo.Value = ""
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(634, 177)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(133, 19)
        Me.txtPriceCode.TabIndex = 38
        Me.txtPriceCode.TextWrap = False
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Location = New System.Drawing.Point(537, 177)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 121
        Me.lblPriceCode.Text = "Price Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel5.Location = New System.Drawing.Point(1034, 461)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(197, 16)
        Me.MyLabel5.TabIndex = 56
        Me.MyLabel5.Text = "Press F4 to open serial item Details"
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(0, 199)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(533, 22)
        Me.pnlPCJ.TabIndex = 27
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 34
        Me.MyLabel4.Text = "Project"
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
        Me.fndProject.Location = New System.Drawing.Point(98, 1)
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
        Me.fndProject.Size = New System.Drawing.Size(143, 19)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(244, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(285, 20)
        Me.lblProject.TabIndex = 1
        Me.lblProject.TextWrap = False
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(915, 422)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 24
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
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 422)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 44
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
        Me.lblSalesman.Location = New System.Drawing.Point(242, 111)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(287, 18)
        Me.lblSalesman.TabIndex = 26
        Me.lblSalesman.TextWrap = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(1005, 198)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel6.TabIndex = 30
        Me.RadLabel6.Text = " Invoice No"
        Me.RadLabel6.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(0, 112)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 51
        Me.MyLabel1.Text = "Salesman"
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
        Me.txtSalesman.Location = New System.Drawing.Point(98, 110)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.MyLabel1
        Me.txtSalesman.MyLinkLable2 = Me.lblSalesman
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.MyShowMasterFormButton = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReferenceFieldDesc = Nothing
        Me.txtSalesman.ReferenceFieldName = Nothing
        Me.txtSalesman.ReferenceTableName = Nothing
        Me.txtSalesman.Size = New System.Drawing.Size(143, 20)
        Me.txtSalesman.TabIndex = 17
        Me.txtSalesman.Value = ""
        '
        'txtInvNo
        '
        Me.txtInvNo.CalculationExpression = Nothing
        Me.txtInvNo.FieldCode = Nothing
        Me.txtInvNo.FieldDesc = Nothing
        Me.txtInvNo.FieldMaxLength = 0
        Me.txtInvNo.FieldName = Nothing
        Me.txtInvNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNo.isCalculatedField = False
        Me.txtInvNo.IsSourceFromTable = False
        Me.txtInvNo.IsSourceFromValueList = False
        Me.txtInvNo.IsUnique = False
        Me.txtInvNo.Location = New System.Drawing.Point(1099, 197)
        Me.txtInvNo.MaxLength = 200
        Me.txtInvNo.MendatroryField = False
        Me.txtInvNo.MyLinkLable1 = Me.RadLabel6
        Me.txtInvNo.MyLinkLable2 = Nothing
        Me.txtInvNo.Name = "txtInvNo"
        Me.txtInvNo.ReferenceFieldDesc = Nothing
        Me.txtInvNo.ReferenceFieldName = Nothing
        Me.txtInvNo.ReferenceTableName = Nothing
        Me.txtInvNo.Size = New System.Drawing.Size(245, 18)
        Me.txtInvNo.TabIndex = 33
        Me.txtInvNo.Visible = False
        '
        'dtpInvoice
        '
        Me.dtpInvoice.CalculationExpression = Nothing
        Me.dtpInvoice.CustomFormat = "dd/MM/yyyy"
        Me.dtpInvoice.FieldCode = Nothing
        Me.dtpInvoice.FieldDesc = Nothing
        Me.dtpInvoice.FieldMaxLength = 0
        Me.dtpInvoice.FieldName = Nothing
        Me.dtpInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInvoice.isCalculatedField = False
        Me.dtpInvoice.IsSourceFromTable = False
        Me.dtpInvoice.IsSourceFromValueList = False
        Me.dtpInvoice.IsUnique = False
        Me.dtpInvoice.Location = New System.Drawing.Point(1041, 219)
        Me.dtpInvoice.MendatroryField = False
        Me.dtpInvoice.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInvoice.MyLinkLable1 = Nothing
        Me.dtpInvoice.MyLinkLable2 = Nothing
        Me.dtpInvoice.Name = "dtpInvoice"
        Me.dtpInvoice.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInvoice.ReferenceFieldDesc = Nothing
        Me.dtpInvoice.ReferenceFieldName = Nothing
        Me.dtpInvoice.ReferenceTableName = Nothing
        Me.dtpInvoice.ShowCheckBox = True
        Me.dtpInvoice.Size = New System.Drawing.Size(97, 18)
        Me.dtpInvoice.TabIndex = 34
        Me.dtpInvoice.TabStop = False
        Me.dtpInvoice.Text = "17/05/2011"
        Me.dtpInvoice.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        Me.dtpInvoice.Visible = False
        '
        'chkCreateAutoInvoice
        '
        Me.chkCreateAutoInvoice.Enabled = False
        Me.chkCreateAutoInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateAutoInvoice.Location = New System.Drawing.Point(537, 68)
        Me.chkCreateAutoInvoice.Name = "chkCreateAutoInvoice"
        Me.chkCreateAutoInvoice.Size = New System.Drawing.Size(120, 16)
        Me.chkCreateAutoInvoice.TabIndex = 12
        Me.chkCreateAutoInvoice.Text = "Create Auto Invoice"
        '
        'chkInternal
        '
        Me.chkInternal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInternal.Location = New System.Drawing.Point(1080, 70)
        Me.chkInternal.Name = "chkInternal"
        Me.chkInternal.Size = New System.Drawing.Size(58, 16)
        Me.chkInternal.TabIndex = 20
        Me.chkInternal.Text = "Internal"
        Me.chkInternal.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel2.Location = New System.Drawing.Point(-1, 478)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(438, 16)
        Me.MyLabel2.TabIndex = 25
        Me.MyLabel2.Text = "Press Ctrl+F7 on Current Row For Manully Insert Rate and Amount or vise versa"
        Me.MyLabel2.Visible = False
        '
        'dtpChallan
        '
        Me.dtpChallan.CalculationExpression = Nothing
        Me.dtpChallan.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallan.FieldCode = Nothing
        Me.dtpChallan.FieldDesc = Nothing
        Me.dtpChallan.FieldMaxLength = 0
        Me.dtpChallan.FieldName = Nothing
        Me.dtpChallan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpChallan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallan.isCalculatedField = False
        Me.dtpChallan.IsSourceFromTable = False
        Me.dtpChallan.IsSourceFromValueList = False
        Me.dtpChallan.IsUnique = False
        Me.dtpChallan.Location = New System.Drawing.Point(901, 91)
        Me.dtpChallan.MendatroryField = False
        Me.dtpChallan.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallan.MyLinkLable1 = Nothing
        Me.dtpChallan.MyLinkLable2 = Nothing
        Me.dtpChallan.Name = "dtpChallan"
        Me.dtpChallan.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallan.ReferenceFieldDesc = Nothing
        Me.dtpChallan.ReferenceFieldName = Nothing
        Me.dtpChallan.ReferenceTableName = Nothing
        Me.dtpChallan.ShowCheckBox = True
        Me.dtpChallan.Size = New System.Drawing.Size(96, 18)
        Me.dtpChallan.TabIndex = 16
        Me.dtpChallan.TabStop = False
        Me.dtpChallan.Text = "17/05/2011"
        Me.dtpChallan.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        Me.dtpChallan.Visible = False
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(406, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 3
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Date"
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(242, 89)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(287, 18)
        Me.lblDept.TabIndex = 22
        Me.lblDept.TextWrap = False
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
        Me.txtReqNo.Location = New System.Drawing.Point(634, 1)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel24
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(145, 20)
        Me.txtReqNo.TabIndex = 4
        Me.txtReqNo.Value = ""
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(537, 2)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(53, 16)
        Me.RadLabel24.TabIndex = 24
        Me.RadLabel24.Text = "Order No"
        '
        'RadLabel28
        '
        Me.RadLabel28.FieldName = Nothing
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(0, 90)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel28.TabIndex = 33
        Me.RadLabel28.Text = "Department"
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
        Me.txtDept.Location = New System.Drawing.Point(98, 88)
        Me.txtDept.MendatroryField = False
        Me.txtDept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.MyLinkLable1 = Me.RadLabel28
        Me.txtDept.MyLinkLable2 = Me.lblDept
        Me.txtDept.MyReadOnly = False
        Me.txtDept.MyShowMasterFormButton = False
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReferenceFieldDesc = Nothing
        Me.txtDept.ReferenceFieldName = Nothing
        Me.txtDept.ReferenceTableName = Nothing
        Me.txtDept.Size = New System.Drawing.Size(143, 20)
        Me.txtDept.TabIndex = 14
        Me.txtDept.Value = ""
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(537, 110)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel8.TabIndex = 36
        Me.RadLabel8.Text = "Carrier"
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(242, 24)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(287, 18)
        Me.lblBillToLocation.TabIndex = 9
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(537, 24)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 10
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(0, 68)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel18.TabIndex = 31
        Me.RadLabel18.Text = "Ship To Location"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(0, 25)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(84, 16)
        Me.RadLabel15.TabIndex = 29
        Me.RadLabel15.Text = "Bill To Location"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 219)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1229, 201)
        Me.RadGroupBox2.TabIndex = 40
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Controls.Add(Me.RadLabel13)
        Me.gv1.Controls.Add(Me.txtGRNo)
        Me.gv1.Controls.Add(Me.RadLabel7)
        Me.gv1.Controls.Add(Me.txtRefNo)
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1209, 171)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(535, 3)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel13.TabIndex = 34
        Me.RadLabel13.Text = "GR No"
        Me.RadLabel13.Visible = False
        '
        'txtGRNo
        '
        Me.txtGRNo.CalculationExpression = Nothing
        Me.txtGRNo.FieldCode = Nothing
        Me.txtGRNo.FieldDesc = Nothing
        Me.txtGRNo.FieldMaxLength = 0
        Me.txtGRNo.FieldName = Nothing
        Me.txtGRNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRNo.isCalculatedField = False
        Me.txtGRNo.IsSourceFromTable = False
        Me.txtGRNo.IsSourceFromValueList = False
        Me.txtGRNo.IsUnique = False
        Me.txtGRNo.Location = New System.Drawing.Point(601, 2)
        Me.txtGRNo.MaxLength = 50
        Me.txtGRNo.MendatroryField = False
        Me.txtGRNo.MyLinkLable1 = Me.RadLabel13
        Me.txtGRNo.MyLinkLable2 = Nothing
        Me.txtGRNo.Name = "txtGRNo"
        Me.txtGRNo.ReferenceFieldDesc = Nothing
        Me.txtGRNo.ReferenceFieldName = Nothing
        Me.txtGRNo.ReferenceTableName = Nothing
        Me.txtGRNo.Size = New System.Drawing.Size(114, 18)
        Me.txtGRNo.TabIndex = 0
        Me.txtGRNo.Visible = False
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(589, 26)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel7.TabIndex = 37
        Me.RadLabel7.Text = "Challan No"
        Me.RadLabel7.Visible = False
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
        Me.txtRefNo.Location = New System.Drawing.Point(655, 25)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(245, 18)
        Me.txtRefNo.TabIndex = 1
        Me.txtRefNo.Visible = False
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(784, 2)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 5
        Me.chkOnHold.Text = "On Hold"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-1, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 45
        Me.RadLabel1.Text = "Code"
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
        Me.txtShipToLocation.Location = New System.Drawing.Point(98, 66)
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
        Me.txtShipToLocation.Size = New System.Drawing.Size(143, 20)
        Me.txtShipToLocation.TabIndex = 11
        Me.txtShipToLocation.Value = ""
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
        Me.txtBillToLocation.Location = New System.Drawing.Point(98, 23)
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
        Me.txtBillToLocation.Size = New System.Drawing.Size(143, 20)
        Me.txtBillToLocation.TabIndex = 6
        Me.txtBillToLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(851, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 7
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 0)
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
        Me.txtDesc.Location = New System.Drawing.Point(634, 23)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(361, 18)
        Me.txtDesc.TabIndex = 7
        '
        'chkCreateAutoReceipt
        '
        Me.chkCreateAutoReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateAutoReceipt.Location = New System.Drawing.Point(585, 390)
        Me.chkCreateAutoReceipt.Name = "chkCreateAutoReceipt"
        Me.chkCreateAutoReceipt.Size = New System.Drawing.Size(122, 16)
        Me.chkCreateAutoReceipt.TabIndex = 48
        Me.chkCreateAutoReceipt.Text = "Create Auto Receipt"
        Me.chkCreateAutoReceipt.Visible = False
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(406, 385)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel29.TabIndex = 40
        Me.RadLabel29.Text = "Item Type"
        Me.RadLabel29.Visible = False
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownAnimationEnabled = True
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(471, 383)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel29
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(128, 20)
        Me.cboItemType.TabIndex = 21
        Me.cboItemType.Visible = False
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(752, 1)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDrillDown.TabIndex = 5
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(350, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(71.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1234, 491)
        Me.RadPageViewPage5.Text = "DCS Items"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkFillDCSDetail)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvDCS)
        Me.SplitContainer3.Size = New System.Drawing.Size(1234, 491)
        Me.SplitContainer3.SplitterDistance = 28
        Me.SplitContainer3.TabIndex = 0
        '
        'chkFillDCSDetail
        '
        Me.chkFillDCSDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFillDCSDetail.Location = New System.Drawing.Point(4, 6)
        Me.chkFillDCSDetail.Name = "chkFillDCSDetail"
        Me.chkFillDCSDetail.Size = New System.Drawing.Size(94, 16)
        Me.chkFillDCSDetail.TabIndex = 13
        Me.chkFillDCSDetail.Text = "Fill DCS Detail"
        '
        'gvDCS
        '
        Me.gvDCS.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDCS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDCS.ForeColor = System.Drawing.Color.Black
        Me.gvDCS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDCS.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDCS.MasterTemplate.AllowDeleteRow = False
        Me.gvDCS.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDCS.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDCS.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvDCS.Name = "gvDCS"
        Me.gvDCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDCS.ShowGroupPanel = False
        Me.gvDCS.ShowHeaderCellButtons = True
        Me.gvDCS.Size = New System.Drawing.Size(1234, 459)
        Me.gvDCS.TabIndex = 2
        Me.gvDCS.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 31)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1234, 495)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(547, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(89, 13)
        Me.rbtnTaxCalManual.MyLinkLable1 = Nothing
        Me.rbtnTaxCalManual.MyLinkLable2 = Nothing
        Me.rbtnTaxCalManual.Name = "rbtnTaxCalManual"
        Me.rbtnTaxCalManual.Size = New System.Drawing.Size(57, 18)
        Me.rbtnTaxCalManual.TabIndex = 1
        Me.rbtnTaxCalManual.Text = "Manual"
        '
        'rbtnTaxCalAutomatic
        '
        Me.rbtnTaxCalAutomatic.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTaxCalAutomatic.MyLinkLable1 = Nothing
        Me.rbtnTaxCalAutomatic.MyLinkLable2 = Nothing
        Me.rbtnTaxCalAutomatic.Name = "rbtnTaxCalAutomatic"
        Me.rbtnTaxCalAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnTaxCalAutomatic.TabIndex = 0
        Me.rbtnTaxCalAutomatic.Text = "Automatic"
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
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 18)
        Me.txtTaxGroup.TabIndex = 0
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 6)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 5
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
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(1076, 391)
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
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 404)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1234, 87)
        Me.RadGroupBox1.TabIndex = 3
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
        Me.RadLabel16.Location = New System.Drawing.Point(6, 26)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 2
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(220, 23)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 1
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
        Me.txtDueDate.TabIndex = 2
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
        Me.RadLabel17.TabIndex = 4
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
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1229, 353)
        Me.gv2.TabIndex = 3
        Me.gv2.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 31)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1234, 495)
        Me.RadPageViewPage3.Text = "Additional Charges"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvAC)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel31)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblAddCharges)
        Me.SplitContainer2.Size = New System.Drawing.Size(1234, 495)
        Me.SplitContainer2.SplitterDistance = 447
        Me.SplitContainer2.TabIndex = 0
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
        Me.gvAC.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(1234, 447)
        Me.gvAC.TabIndex = 1
        Me.gvAC.TabStop = False
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(989, 3)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 126
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(1121, 3)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 127
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 31)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1234, 495)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1234, 495)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1234, 491)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1234, 491)
        Me.UcAttachment1.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage4.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage4.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel58)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel57)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.txtComment)
        Me.RadPageViewPage4.Controls.Add(Me.Panel3)
        Me.RadPageViewPage4.Controls.Add(Me.btnReverseAndUnpost)
        Me.RadPageViewPage4.Controls.Add(Me.pnlMannualInvoiceNo)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscPer)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.chkVendorGrossReceipt)
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
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1234, 491)
        Me.RadPageViewPage4.Text = "Total"
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
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(217, 316)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReadOnly = True
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(110, 20)
        Me.txtTCSTaxRate.TabIndex = 1478
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
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
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(217, 290)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReadOnly = True
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(110, 20)
        Me.txttcstaxbaseamount.TabIndex = 1477
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(217, 268)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1476
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(93, 290)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel58.TabIndex = 1475
        Me.MyLabel58.Text = "TCS Tax Base Amount"
        '
        'MyLabel57
        '
        Me.MyLabel57.FieldName = Nothing
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(77, 268)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel57.TabIndex = 1474
        Me.MyLabel57.Text = "Actual TCS Tax Base Amt"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(375, 77)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 1473
        Me.RadLabel14.Text = "Comment"
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(372, 94)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Me.RadLabel14
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtComment.RootElement.StretchVertically = True
        Me.txtComment.Size = New System.Drawing.Size(859, 339)
        Me.txtComment.TabIndex = 1472
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.MyLabel25)
        Me.Panel3.Controls.Add(Me.lblARSecurity)
        Me.Panel3.Controls.Add(Me.MyLabel24)
        Me.Panel3.Controls.Add(Me.lblReverseAdvanceSec)
        Me.Panel3.Controls.Add(Me.MyLabel23)
        Me.Panel3.Controls.Add(Me.lblReverseRefund)
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
        Me.Panel3.Controls.Add(Me.MyLabel15)
        Me.Panel3.Controls.Add(Me.lblCreditLimit)
        Me.Panel3.Location = New System.Drawing.Point(41, 406)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(210, 27)
        Me.Panel3.TabIndex = 1471
        Me.Panel3.Visible = False
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Location = New System.Drawing.Point(3, 59)
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
        Me.lblARSecurity.Location = New System.Drawing.Point(121, 58)
        Me.lblARSecurity.Name = "lblARSecurity"
        Me.lblARSecurity.Size = New System.Drawing.Size(87, 19)
        Me.lblARSecurity.TabIndex = 75
        Me.lblARSecurity.TextWrap = False
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(3, 40)
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
        Me.lblReverseAdvanceSec.Location = New System.Drawing.Point(121, 39)
        Me.lblReverseAdvanceSec.Name = "lblReverseAdvanceSec"
        Me.lblReverseAdvanceSec.Size = New System.Drawing.Size(87, 19)
        Me.lblReverseAdvanceSec.TabIndex = 73
        Me.lblReverseAdvanceSec.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Location = New System.Drawing.Point(3, 154)
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
        Me.lblReverseRefund.Location = New System.Drawing.Point(121, 153)
        Me.lblReverseRefund.Name = "lblReverseRefund"
        Me.lblReverseRefund.Size = New System.Drawing.Size(87, 19)
        Me.lblReverseRefund.TabIndex = 71
        Me.lblReverseRefund.TextWrap = False
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Location = New System.Drawing.Point(3, 135)
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
        Me.lblRefund.Location = New System.Drawing.Point(121, 134)
        Me.lblRefund.Name = "lblRefund"
        Me.lblRefund.Size = New System.Drawing.Size(87, 19)
        Me.lblRefund.TabIndex = 67
        Me.lblRefund.TextWrap = False
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Location = New System.Drawing.Point(3, 116)
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
        Me.lblLedgerOutstanding.Location = New System.Drawing.Point(121, 115)
        Me.lblLedgerOutstanding.Name = "lblLedgerOutstanding"
        Me.lblLedgerOutstanding.Size = New System.Drawing.Size(87, 19)
        Me.lblLedgerOutstanding.TabIndex = 65
        Me.lblLedgerOutstanding.TextWrap = False
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(4, 97)
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
        Me.lblShortcloseDO.Location = New System.Drawing.Point(121, 96)
        Me.lblShortcloseDO.Name = "lblShortcloseDO"
        Me.lblShortcloseDO.Size = New System.Drawing.Size(87, 19)
        Me.lblShortcloseDO.TabIndex = 63
        Me.lblShortcloseDO.TextWrap = False
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Location = New System.Drawing.Point(4, 78)
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
        Me.lblPendingDO.Location = New System.Drawing.Point(121, 77)
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
        Me.lblAdvanceSecurity.Location = New System.Drawing.Point(121, 20)
        Me.lblAdvanceSecurity.Name = "lblAdvanceSecurity"
        Me.lblAdvanceSecurity.Size = New System.Drawing.Size(87, 19)
        Me.lblAdvanceSecurity.TabIndex = 57
        Me.lblAdvanceSecurity.TextWrap = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel15.TabIndex = 56
        Me.MyLabel15.Text = "Credit Limit(+)"
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.AutoSize = False
        Me.lblCreditLimit.BorderVisible = True
        Me.lblCreditLimit.FieldName = Nothing
        Me.lblCreditLimit.Location = New System.Drawing.Point(121, 2)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(87, 19)
        Me.lblCreditLimit.TabIndex = 25
        Me.lblCreditLimit.TextWrap = False
        '
        'btnReverseAndUnpost
        '
        Me.btnReverseAndUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnpost.Location = New System.Drawing.Point(963, 473)
        Me.btnReverseAndUnpost.Name = "btnReverseAndUnpost"
        Me.btnReverseAndUnpost.Size = New System.Drawing.Size(268, 18)
        Me.btnReverseAndUnpost.TabIndex = 9
        Me.btnReverseAndUnpost.Text = "Reverse and Unpost"
        Me.btnReverseAndUnpost.Visible = False
        '
        'pnlMannualInvoiceNo
        '
        Me.pnlMannualInvoiceNo.Controls.Add(Me.TxtInvoiceManualNoWithPrefix)
        Me.pnlMannualInvoiceNo.Controls.Add(Me.txtMannaulInvoiceNo)
        Me.pnlMannualInvoiceNo.Controls.Add(Me.MyLabel11)
        Me.pnlMannualInvoiceNo.Location = New System.Drawing.Point(404, 43)
        Me.pnlMannualInvoiceNo.Name = "pnlMannualInvoiceNo"
        Me.pnlMannualInvoiceNo.Size = New System.Drawing.Size(222, 28)
        Me.pnlMannualInvoiceNo.TabIndex = 3
        Me.pnlMannualInvoiceNo.Visible = False
        '
        'TxtInvoiceManualNoWithPrefix
        '
        Me.TxtInvoiceManualNoWithPrefix.CalculationExpression = Nothing
        Me.TxtInvoiceManualNoWithPrefix.FieldCode = Nothing
        Me.TxtInvoiceManualNoWithPrefix.FieldDesc = Nothing
        Me.TxtInvoiceManualNoWithPrefix.FieldMaxLength = 0
        Me.TxtInvoiceManualNoWithPrefix.FieldName = Nothing
        Me.TxtInvoiceManualNoWithPrefix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceManualNoWithPrefix.isCalculatedField = False
        Me.TxtInvoiceManualNoWithPrefix.IsSourceFromTable = False
        Me.TxtInvoiceManualNoWithPrefix.IsSourceFromValueList = False
        Me.TxtInvoiceManualNoWithPrefix.IsUnique = False
        Me.TxtInvoiceManualNoWithPrefix.Location = New System.Drawing.Point(117, 5)
        Me.TxtInvoiceManualNoWithPrefix.MaxLength = 200
        Me.TxtInvoiceManualNoWithPrefix.MendatroryField = False
        Me.TxtInvoiceManualNoWithPrefix.MyLinkLable1 = Me.RadLabel3
        Me.TxtInvoiceManualNoWithPrefix.MyLinkLable2 = Nothing
        Me.TxtInvoiceManualNoWithPrefix.Name = "TxtInvoiceManualNoWithPrefix"
        Me.TxtInvoiceManualNoWithPrefix.ReferenceFieldDesc = Nothing
        Me.TxtInvoiceManualNoWithPrefix.ReferenceFieldName = Nothing
        Me.TxtInvoiceManualNoWithPrefix.ReferenceTableName = Nothing
        Me.TxtInvoiceManualNoWithPrefix.Size = New System.Drawing.Size(99, 18)
        Me.TxtInvoiceManualNoWithPrefix.TabIndex = 10
        '
        'txtMannaulInvoiceNo
        '
        Me.txtMannaulInvoiceNo.BackColor = System.Drawing.Color.White
        Me.txtMannaulInvoiceNo.CalculationExpression = Nothing
        Me.txtMannaulInvoiceNo.DecimalPlaces = 0
        Me.txtMannaulInvoiceNo.FieldCode = Nothing
        Me.txtMannaulInvoiceNo.FieldDesc = Nothing
        Me.txtMannaulInvoiceNo.FieldMaxLength = 0
        Me.txtMannaulInvoiceNo.FieldName = Nothing
        Me.txtMannaulInvoiceNo.isCalculatedField = False
        Me.txtMannaulInvoiceNo.IsSourceFromTable = False
        Me.txtMannaulInvoiceNo.IsSourceFromValueList = False
        Me.txtMannaulInvoiceNo.IsUnique = False
        Me.txtMannaulInvoiceNo.Location = New System.Drawing.Point(116, 3)
        Me.txtMannaulInvoiceNo.MendatroryField = False
        Me.txtMannaulInvoiceNo.MyLinkLable1 = Nothing
        Me.txtMannaulInvoiceNo.MyLinkLable2 = Nothing
        Me.txtMannaulInvoiceNo.Name = "txtMannaulInvoiceNo"
        Me.txtMannaulInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtMannaulInvoiceNo.ReferenceFieldName = Nothing
        Me.txtMannaulInvoiceNo.ReferenceTableName = Nothing
        Me.txtMannaulInvoiceNo.Size = New System.Drawing.Size(100, 20)
        Me.txtMannaulInvoiceNo.TabIndex = 0
        Me.txtMannaulInvoiceNo.Text = "0"
        Me.txtMannaulInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMannaulInvoiceNo.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(107, 18)
        Me.MyLabel11.TabIndex = 0
        Me.MyLabel11.Text = "Mannual Invoice No"
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(217, 118)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 10
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(73, 118)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 157
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(142, 70)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel3.TabIndex = 4
        Me.MyLabel3.Text = "Discount On"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnAmt)
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnRate)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(217, 70)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(141, 21)
        Me.RadGroupBox3.TabIndex = 5
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
        Me.txtDiscAmt.Location = New System.Drawing.Point(278, 94)
        Me.txtDiscAmt.MendatroryField = False
        Me.txtDiscAmt.MyLinkLable1 = Nothing
        Me.txtDiscAmt.MyLinkLable2 = Nothing
        Me.txtDiscAmt.Name = "txtDiscAmt"
        Me.txtDiscAmt.ReferenceFieldDesc = Nothing
        Me.txtDiscAmt.ReferenceFieldName = Nothing
        Me.txtDiscAmt.ReferenceTableName = Nothing
        Me.txtDiscAmt.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscAmt.TabIndex = 8
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
        Me.txtDiscPer.Location = New System.Drawing.Point(217, 94)
        Me.txtDiscPer.MendatroryField = False
        Me.txtDiscPer.MyLinkLable1 = Nothing
        Me.txtDiscPer.MyLinkLable2 = Nothing
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.ReferenceFieldDesc = Nothing
        Me.txtDiscPer.ReferenceFieldName = Nothing
        Me.txtDiscPer.ReferenceTableName = Nothing
        Me.txtDiscPer.Size = New System.Drawing.Size(39, 20)
        Me.txtDiscPer.TabIndex = 6
        Me.txtDiscPer.Text = "0"
        Me.txtDiscPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscPer.Value = 0R
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(41, 3)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(736, 38)
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
        Me.txtConversionRate.Location = New System.Drawing.Point(353, 8)
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
        Me.txtCurrencyCode.Location = New System.Drawing.Point(80, 9)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 24)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(483, 12)
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
        Me.txtApplicableFrom.Location = New System.Drawing.Point(579, 10)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(119, 18)
        Me.txtApplicableFrom.TabIndex = 2
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(22, 12)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 137
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(256, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 139
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(75, 221)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 131
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(217, 220)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 14
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkVendorGrossReceipt
        '
        Me.chkVendorGrossReceipt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVendorGrossReceipt.Location = New System.Drawing.Point(1103, 3)
        Me.chkVendorGrossReceipt.MyLinkLable1 = Nothing
        Me.chkVendorGrossReceipt.MyLinkLable2 = Nothing
        Me.chkVendorGrossReceipt.Name = "chkVendorGrossReceipt"
        Me.chkVendorGrossReceipt.Size = New System.Drawing.Size(128, 18)
        Me.chkVendorGrossReceipt.TabIndex = 1
        Me.chkVendorGrossReceipt.Tag1 = Nothing
        Me.chkVendorGrossReceipt.Text = "Vendor Gross Receipt"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(95, 167)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(115, 245)
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
        Me.lblTotRAmt.Location = New System.Drawing.Point(217, 244)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 15
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(138, 194)
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
        Me.lblTaxAmt.Location = New System.Drawing.Point(217, 193)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 13
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(217, 166)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 12
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(217, 139)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 11
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(217, 47)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 2
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(116, 140)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(29, 48)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'btnPrnt
        '
        Me.btnPrnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrnt.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnPrintA4, Me.btnPrintA5})
        Me.btnPrnt.Location = New System.Drawing.Point(218, 4)
        Me.btnPrnt.Name = "btnPrnt"
        Me.btnPrnt.Size = New System.Drawing.Size(86, 20)
        Me.btnPrnt.TabIndex = 47
        Me.btnPrnt.Text = "Print"
        '
        'btnPrintA4
        '
        Me.btnPrintA4.Name = "btnPrintA4"
        Me.btnPrintA4.Text = "Print A4 Size"
        Me.btnPrintA4.UseCompatibleTextRendering = False
        '
        'btnPrintA5
        '
        Me.btnPrintA5.Name = "btnPrintA5"
        Me.btnPrintA5.Text = "Print A5 Size"
        Me.btnPrintA5.UseCompatibleTextRendering = False
        '
        'btnDCSPrint
        '
        Me.btnDCSPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDCSPrint.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rbtnDCSPrint, Me.rbtnDCSSummary})
        Me.btnDCSPrint.Location = New System.Drawing.Point(306, 4)
        Me.btnDCSPrint.Name = "btnDCSPrint"
        Me.btnDCSPrint.Size = New System.Drawing.Size(86, 20)
        Me.btnDCSPrint.TabIndex = 46
        Me.btnDCSPrint.Text = "DCS Print"
        '
        'rbtnDCSPrint
        '
        Me.rbtnDCSPrint.Name = "rbtnDCSPrint"
        Me.rbtnDCSPrint.Text = "Print Challan"
        Me.rbtnDCSPrint.UseCompatibleTextRendering = False
        '
        'rbtnDCSSummary
        '
        Me.rbtnDCSSummary.Name = "rbtnDCSSummary"
        Me.rbtnDCSSummary.Text = "Print Summary"
        Me.rbtnDCSSummary.UseCompatibleTextRendering = False
        '
        'btnAddCost
        '
        Me.btnAddCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCost.Location = New System.Drawing.Point(987, 5)
        Me.btnAddCost.Name = "btnAddCost"
        Me.btnAddCost.Size = New System.Drawing.Size(87, 20)
        Me.btnAddCost.TabIndex = 4
        Me.btnAddCost.Text = "Additional Cost"
        Me.btnAddCost.Visible = False
        '
        'btnInvoiceJE
        '
        Me.btnInvoiceJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInvoiceJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvoiceJE.Location = New System.Drawing.Point(1080, 4)
        Me.btnInvoiceJE.Name = "btnInvoiceJE"
        Me.btnInvoiceJE.Size = New System.Drawing.Size(96, 20)
        Me.btnInvoiceJE.TabIndex = 45
        Me.btnInvoiceJE.Text = "Show Invoice JE"
        '
        'btnDeliveredTo
        '
        Me.btnDeliveredTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeliveredTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeliveredTo.Location = New System.Drawing.Point(650, 4)
        Me.btnDeliveredTo.Name = "btnDeliveredTo"
        Me.btnDeliveredTo.Size = New System.Drawing.Size(87, 20)
        Me.btnDeliveredTo.TabIndex = 40
        Me.btnDeliveredTo.Text = "Delivered To"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(561, 4)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(87, 20)
        Me.btnHistory.TabIndex = 39
        Me.btnHistory.Text = "History"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnpreview, Me.btnsend, Me.btnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(473, 4)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(86, 20)
        Me.btnsetting.TabIndex = 6
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnpreview
        '
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Text = "Preview"
        '
        'btnsend
        '
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Text = "Send E-Mail/SMS"
        '
        'btnSendForApproval
        '
        Me.btnSendForApproval.Name = "btnSendForApproval"
        Me.btnSendForApproval.Text = "Send For Approval"
        '
        'btnPrintInvoice
        '
        Me.btnPrintInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintInvoice.Location = New System.Drawing.Point(394, 4)
        Me.btnPrintInvoice.Name = "btnPrintInvoice"
        Me.btnPrintInvoice.Size = New System.Drawing.Size(77, 20)
        Me.btnPrintInvoice.TabIndex = 5
        Me.btnPrintInvoice.Text = "Print Invoice"
        Me.btnPrintInvoice.Visible = False
        '
        'chkRateUserCustomer
        '
        Me.chkRateUserCustomer.Controls.Add(Me.chkRateDefaultSetting)
        Me.chkRateUserCustomer.Controls.Add(Me.RadLabel20)
        Me.chkRateUserCustomer.Controls.Add(Me.RadLabel21)
        Me.chkRateUserCustomer.Enabled = False
        Me.chkRateUserCustomer.IsThreeState = True
        Me.chkRateUserCustomer.Location = New System.Drawing.Point(800, 5)
        Me.chkRateUserCustomer.Name = "chkRateUserCustomer"
        Me.chkRateUserCustomer.Size = New System.Drawing.Size(158, 18)
        Me.chkRateUserCustomer.TabIndex = 9
        Me.chkRateUserCustomer.Text = "User Customer Rate Setting"
        Me.chkRateUserCustomer.Visible = False
        '
        'chkRateDefaultSetting
        '
        Me.chkRateDefaultSetting.Enabled = False
        Me.chkRateDefaultSetting.IsThreeState = True
        Me.chkRateDefaultSetting.Location = New System.Drawing.Point(83, 0)
        Me.chkRateDefaultSetting.Name = "chkRateDefaultSetting"
        Me.chkRateDefaultSetting.Size = New System.Drawing.Size(120, 18)
        Me.chkRateDefaultSetting.TabIndex = 7
        Me.chkRateDefaultSetting.Text = "Rate Default Setting"
        Me.chkRateDefaultSetting.Visible = False
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(31, 4)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel20.TabIndex = 8
        Me.RadLabel20.Text = "Gate Entry  Date"
        Me.RadLabel20.Visible = False
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(9, 1)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel21.TabIndex = 38
        Me.RadLabel21.Text = "Gate Entry No"
        Me.RadLabel21.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(147, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(76, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1182, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtGEDate
        '
        Me.txtGEDate.CalculationExpression = Nothing
        Me.txtGEDate.Controls.Add(Me.txtGENo)
        Me.txtGEDate.CustomFormat = "dd/MM/yyyy"
        Me.txtGEDate.FieldCode = Nothing
        Me.txtGEDate.FieldDesc = Nothing
        Me.txtGEDate.FieldMaxLength = 0
        Me.txtGEDate.FieldName = Nothing
        Me.txtGEDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGEDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGEDate.isCalculatedField = False
        Me.txtGEDate.IsSourceFromTable = False
        Me.txtGEDate.IsSourceFromValueList = False
        Me.txtGEDate.IsUnique = False
        Me.txtGEDate.Location = New System.Drawing.Point(795, 6)
        Me.txtGEDate.MendatroryField = False
        Me.txtGEDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGEDate.MyLinkLable1 = Me.RadLabel20
        Me.txtGEDate.MyLinkLable2 = Nothing
        Me.txtGEDate.Name = "txtGEDate"
        Me.txtGEDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGEDate.ReferenceFieldDesc = Nothing
        Me.txtGEDate.ReferenceFieldName = Nothing
        Me.txtGEDate.ReferenceTableName = Nothing
        Me.txtGEDate.ShowCheckBox = True
        Me.txtGEDate.Size = New System.Drawing.Size(95, 18)
        Me.txtGEDate.TabIndex = 10
        Me.txtGEDate.TabStop = False
        Me.txtGEDate.Text = "13/06/2011"
        Me.txtGEDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtGEDate.Visible = False
        '
        'txtGENo
        '
        Me.txtGENo.CalculationExpression = Nothing
        Me.txtGENo.FieldCode = Nothing
        Me.txtGENo.FieldDesc = Nothing
        Me.txtGENo.FieldMaxLength = 0
        Me.txtGENo.FieldName = Nothing
        Me.txtGENo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGENo.isCalculatedField = False
        Me.txtGENo.IsSourceFromTable = False
        Me.txtGENo.IsSourceFromValueList = False
        Me.txtGENo.IsUnique = False
        Me.txtGENo.Location = New System.Drawing.Point(9, 0)
        Me.txtGENo.MaxLength = 50
        Me.txtGENo.MendatroryField = False
        Me.txtGENo.MyLinkLable1 = Me.RadLabel21
        Me.txtGENo.MyLinkLable2 = Nothing
        Me.txtGENo.Name = "txtGENo"
        Me.txtGENo.ReferenceFieldDesc = Nothing
        Me.txtGENo.ReferenceFieldName = Nothing
        Me.txtGENo.ReferenceTableName = Nothing
        Me.txtGENo.Size = New System.Drawing.Size(143, 18)
        Me.txtGENo.TabIndex = 0
        Me.txtGENo.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem6})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1255, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "E-Mail/SMS Setting"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "File"
        '
        'Export
        '
        Me.Export.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export_Head, Me.Export_details})
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'Export_Head
        '
        Me.Export_Head.Name = "Export_Head"
        Me.Export_Head.Text = "Export Shipment Head"
        '
        'Export_details
        '
        Me.Export_details.Name = "Export_details"
        Me.Export_details.Text = "Export Shipment Details"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        Me.Import.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1255, 569)
        Me.Panel1.TabIndex = 4
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.LastShowDpiScaleFactor = New System.Drawing.SizeF(1.0!, 1.0!)
        Me.RadMenuItem2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.TabIndex = 0
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        WindowsSettings1.EnableRoundedCorners = Nothing
        WindowsSettings1.RoundedCornersStyle = Telerik.WinControls.RoundedCornersStyle.Round
        Me.RadMenuItem2.WindowsSettings = WindowsSettings1
        '
        'frmSNShipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 589)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmSNShipment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Shipment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lbltransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipToDeliveryAt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLR_GR_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBeejakNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLR_GR_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtEWayBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtElecttefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtForm38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateAutoInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateAutoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.chkFillDCSDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDCS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblARSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMannualInvoiceNo.ResumeLayout(False)
        Me.pnlMannualInvoiceNo.PerformLayout()
        CType(Me.TxtInvoiceManualNoWithPrefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMannaulInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorGrossReceipt, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.btnPrnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDCSPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeliveredTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chkRateUserCustomer.ResumeLayout(False)
        Me.chkRateUserCustomer.PerformLayout()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGEDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtGEDate.ResumeLayout(False)
        Me.txtGEDate.PerformLayout()
        CType(Me.txtGENo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents txtShipToLocation As common.UserControls.txtFinder
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents txtInvNo As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents txtGENo As common.Controls.MyTextBox
    Friend WithEvents txtGRNo As common.Controls.MyTextBox
    Friend WithEvents txtGEDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnAddCost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents chkVendorGrossReceipt As common.Controls.MyCheckBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
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
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel28 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents dtpChallan As common.Controls.MyDateTimePicker
    Friend WithEvents dtpInvoice As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkInternal As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCreateAutoInvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCreateAutoReceipt As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents chkRateUserCustomer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRateDefaultSetting As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtBarCode As common.Controls.MyTextBox
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents txtPriceCode As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblInvoiceDiscAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents ddlInvoiceType As common.Controls.MyComboBox
    Friend WithEvents lblInvoiceType As common.Controls.MyLabel
    Friend WithEvents chkAutoTransfer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblFromLoc As common.Controls.MyLabel
    Friend WithEvents txtFromLoc As common.UserControls.txtFinder
    Friend WithEvents txtPriceGroupCode As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlMannualInvoiceNo As System.Windows.Forms.Panel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtMannaulInvoiceNo As common.MyNumBox
    Friend WithEvents btnPrintInvoice As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblInvoiceNo As common.Controls.MyLabel
    Friend WithEvents txtInvoiceNo As common.Controls.MyLabel
    Friend WithEvents btnReverseAndUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtForm38 As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnpreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtpodate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_Head As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TxtInvoiceManualNoWithPrefix As common.Controls.MyTextBox
    Friend WithEvents btnDeliveredTo As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIsTaxable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtEWayBillDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtElecttefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents btnUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnInvoiceJE As RadButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents lblARSecurity As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents lblReverseAdvanceSec As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents lblReverseRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents lblRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents lblLedgerOutstanding As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents lblShortcloseDO As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents lblPendingDO As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblAdvanceSecurity As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents lblCreditLimit As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lblTotalOutstansing As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents txtBeejakNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtShipToDeliveryAt As common.Controls.MyTextBox
    Friend WithEvents dtpLR_GR_Date As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtLR_GR_No As common.Controls.MyTextBox
    Friend WithEvents txtWeightmentNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents MyLabel58 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
    Friend WithEvents lblOrderQty As common.Controls.MyLabel
    Friend WithEvents txtOrderQty As common.MyNumBox
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents gvDCS As common.UserControls.MyRadGridView
    Friend WithEvents chkFillDCSDetail As RadCheckBox
    Friend WithEvents btnDCSPrint As RadSplitButton
    Friend WithEvents rbtnDCSPrint As RadMenuItem
    Friend WithEvents rbtnDCSSummary As RadMenuItem
    Friend WithEvents lbltransporter As common.Controls.MyLabel
    Friend WithEvents fndtransporter As common.UserControls.txtFinder
    Friend WithEvents btnPrnt As RadSplitButton
    Friend WithEvents btnPrintA4 As RadMenuItem
    Friend WithEvents btnPrintA5 As RadMenuItem
End Class

