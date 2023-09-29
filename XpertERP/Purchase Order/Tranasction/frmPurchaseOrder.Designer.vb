<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseOrder
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition8 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition9 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel59 = New common.Controls.MyLabel()
        Me.txtTenderNo = New common.UserControls.txtFinder()
        Me.lblDept = New common.Controls.MyLabel()
        Me.txtRefTendorNo = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.MyLabel55 = New common.Controls.MyLabel()
        Me.chkTender = New common.Controls.MyCheckBox()
        Me.MyLabel50 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblProject = New common.Controls.MyLabel()
        Me.lblVendorQuotationNo = New common.Controls.MyLabel()
        Me.chkRepair = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkISPO = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblGstinNo = New common.Controls.MyLabel()
        Me.MyLabel49 = New common.Controls.MyLabel()
        Me.MyLabel45 = New common.Controls.MyLabel()
        Me.lblRegisterOrUnregister = New common.Controls.MyLabel()
        Me.txt_deliverydays = New common.MyNumBox()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.chkJobWorkOutward = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddl_category = New common.Controls.MyComboBox()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.chkGSTRegistered = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.lblBillDate = New common.Controls.MyLabel()
        Me.dtBillDate = New common.Controls.MyDateTimePicker()
        Me.lblBillNo = New common.Controls.MyLabel()
        Me.txtBillNo = New common.Controls.MyTextBox()
        Me.chk_emergency = New common.Controls.MyCheckBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pnl_capex = New System.Windows.Forms.Panel()
        Me.chkReceiveControl = New common.Controls.MyCheckBox()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.lbl_rebudgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_rebudgetamt = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lbl_budgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_budgetamt = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.fndcapexsubcode = New common.UserControls.txtFinder()
        Me.lbl_capexsubcode = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.fndcapexcode = New common.UserControls.txtFinder()
        Me.lbl_capexcode = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.chkAutoCalculate = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtAgainstPO_No = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.dtpRenewal = New common.Controls.MyDateTimePicker()
        Me.chkOpenPO = New common.Controls.MyCheckBox()
        Me.chkIsMerchantTrade = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblState = New common.Controls.MyLabel()
        Me.fndState = New common.UserControls.txtFinder()
        Me.lblStateName = New common.Controls.MyLabel()
        Me.chkMCCPurchase = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.chkBlanket = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblAmt = New common.Controls.MyLabel()
        Me.txtAmount = New common.Controls.MyTextBox()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.cboModeOfTransport = New common.Controls.MyComboBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpExpiryDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblTotRAmtCopy = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.txtReferencePO = New common.Controls.MyTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtDeliveryDuration = New common.Controls.MyTextBox()
        Me.lblDeliveryDuration = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtQuotationNo = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtQuotationDate = New common.Controls.MyDateTimePicker()
        Me.txtRGPNo = New common.UserControls.txtFinder()
        Me.lblAmbendmentNoCaption = New common.Controls.MyLabel()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblDeliveryDate = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkAgainst_RGP = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDeliveryDate = New common.Controls.MyDateTimePicker()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnForm_Update = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_c_form = New common.UserControls.MyRadGridView()
        Me.chk_c_form = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_roadpermit = New common.UserControls.MyRadGridView()
        Me.Chkroadpermit = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkTDSApplied = New common.Controls.MyCheckBox()
        Me.chkExciseOnQty = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel54 = New common.Controls.MyLabel()
        Me.lblConfirmatory_PO_SRN_No = New common.Controls.MyLabel()
        Me.txtFreight = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel52 = New common.Controls.MyLabel()
        Me.MyLabel51 = New common.Controls.MyLabel()
        Me.MyLabel48 = New common.Controls.MyLabel()
        Me.txtPackingForward = New common.Controls.MyTextBox()
        Me.txtInsurance = New common.Controls.MyTextBox()
        Me.txtDeliveryDesc = New common.Controls.MyLabel()
        Me.txtDelivery_Code = New common.UserControls.txtFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtInsuranceTerms = New common.Controls.MyTextBox()
        Me.txtPaymentTerm = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtTermRemark = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RdPaymentterms = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblBankDesc = New common.Controls.MyLabel()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.TxtBuyerPODate = New common.Controls.MyDateTimePicker()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.TxtBuyerPONo = New common.Controls.MyTextBox()
        Me.ChkPartPayment = New common.Controls.MyCheckBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.cmbAdvanceType = New common.Controls.MyComboBox()
        Me.TxtCIF = New common.MyNumBox()
        Me.lblCIF = New common.Controls.MyLabel()
        Me.chkTransshipment = New common.Controls.MyCheckBox()
        Me.chkPartshipment = New common.Controls.MyCheckBox()
        Me.dtpAcceptance = New common.Controls.MyDateTimePicker()
        Me.chkAcceptance = New common.Controls.MyCheckBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtPre_Carriage_By = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtAdvance_Pers = New common.MyNumBox()
        Me.cmbTerms_Payment = New common.Controls.MyComboBox()
        Me.cmbTerms = New common.Controls.MyComboBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtPIDueDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.FndCreditTerms = New common.UserControls.txtFinder()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.TxtCreditTermsName = New common.Controls.MyLabel()
        Me.fndCountry_Final_Destination = New common.UserControls.txtFinder()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txtFinal_Destination = New common.Controls.MyTextBox()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.fndCountry_Origin = New common.UserControls.txtFinder()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.txtPort_Discharge = New common.Controls.MyTextBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.cboStuffing = New common.Controls.MyComboBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.txtCarrier = New common.Controls.MyTextBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.TxtHSClassificationNo = New common.Controls.MyTextBox()
        Me.txtPINo = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblBeneficiary = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAmountinpercentage = New System.Windows.Forms.RadioButton()
        Me.rdbAmountinrupees = New System.Windows.Forms.RadioButton()
        Me.lblAdvance = New common.Controls.MyLabel()
        Me.TxtOnAccount = New common.MyNumBox()
        Me.lblonAccount = New common.Controls.MyLabel()
        Me.txtRetained = New common.MyNumBox()
        Me.lblretained = New common.Controls.MyLabel()
        Me.TxtBalancePayment = New common.MyNumBox()
        Me.lblBalancePayment = New common.Controls.MyLabel()
        Me.TxtLC = New common.MyNumBox()
        Me.lblLC = New common.Controls.MyLabel()
        Me.TxtCAD = New common.MyNumBox()
        Me.lblCad = New common.Controls.MyLabel()
        Me.txtAdvance = New common.MyNumBox()
        Me.lblpaymenttermsgroup = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.fndPaymenttermsGroup = New common.UserControls.txtFinder()
        Me.TxtBeneficiary = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxtINCOTERMS = New common.Controls.MyTextBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cboPIStatus = New common.Controls.MyComboBox()
        Me.btnMTUpdate = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtPIDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gvACInsurance = New common.UserControls.MyRadGridView()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.MyLabel56 = New common.Controls.MyLabel()
        Me.lblAddChargesForInsurance = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.lblTotalInsuranceAmt = New common.Controls.MyLabel()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.lblAddChargesForInsurance1 = New common.Controls.MyLabel()
        Me.txtHeaderDiscountAmount = New common.MyNumBox()
        Me.MyLabel53 = New common.Controls.MyLabel()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.lblTaxableAmount = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.lblAmtAfterTax = New common.Controls.MyLabel()
        Me.chkIsContent = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtKindAttentation = New System.Windows.Forms.TextBox()
        Me.lblKindAttentation = New common.Controls.MyLabel()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.txtContentSubject = New System.Windows.Forms.TextBox()
        Me.lblContentSubject = New common.Controls.MyLabel()
        Me.lblSubject = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
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
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.lblWAddress = New common.Controls.MyTextBox()
        Me.lblWPhone = New common.Controls.MyTextBox()
        Me.lblWVendorName = New common.Controls.MyTextBox()
        Me.MyLabel47 = New common.Controls.MyLabel()
        Me.lblAddress = New common.Controls.MyLabel()
        Me.MyLabel46 = New common.Controls.MyLabel()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.gvCategoryValue = New common.UserControls.MyRadGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.gvTermsCdtion = New common.UserControls.MyRadGridView()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.RadPageViewPage7 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSchedule = New common.UserControls.MyRadGridView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtScheduleStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel60 = New common.Controls.MyLabel()
        Me.MyLabel61 = New common.Controls.MyLabel()
        Me.btnNewHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.btn_cancel = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.chkpoclose = New System.Windows.Forms.CheckBox()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintNew = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.ReportFooter = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefTendorNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel55, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorQuotationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRepair, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.lblGstinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegisterOrUnregister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_deliverydays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWorkOutward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGSTRegistered, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chk_emergency.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.pnl_capex.SuspendLayout()
        CType(Me.chkReceiveControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoCalculate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAgainstPO_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpRenewal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsMerchantTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMCCPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBlanket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmtCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.txtReferencePO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtDeliveryDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveryDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuotationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuotationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainst_RGP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.btnForm_Update, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv_c_form, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_c_form.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_c_form, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gv_roadpermit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_roadpermit.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chkroadpermit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.chkTDSApplied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExciseOnQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel54, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConfirmatory_PO_SRN_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel52, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPackingForward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsuranceTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTermRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RdPaymentterms.SuspendLayout()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBuyerPODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBuyerPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkPartPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAdvanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPartshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPre_Carriage_By, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPIDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCreditTermsName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFinal_Destination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPort_Discharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStuffing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtHSClassificationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBeneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.lblAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtOnAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblonAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRetained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblretained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBalancePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalancePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCAD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymenttermsgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtINCOTERMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.cboPIStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMTUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPIDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gvACInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvACInsurance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.MyLabel56, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddChargesForInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalInsuranceAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddChargesForInsurance1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeaderDiscountAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxableAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKindAttentation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblContentSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPageViewPage6.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.lblWAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gvCategoryValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategoryValue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.gvTermsCdtion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTermsCdtion.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage7.SuspendLayout()
        CType(Me.gvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSchedule.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNewHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_cancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNewHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_cancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCopy)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkpoclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintNew)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1105, 473)
        Me.SplitContainer1.SplitterDistance = 441
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RdPaymentterms)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage7)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(2, 2)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage7
        Me.RadPageView1.Size = New System.Drawing.Size(1101, 437)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel59)
        Me.RadPageViewPage1.Controls.Add(Me.txtTenderNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefTendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel55)
        Me.RadPageViewPage1.Controls.Add(Me.chkTender)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel50)
        Me.RadPageViewPage1.Controls.Add(Me.fndProject)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorQuotationNo)
        Me.RadPageViewPage1.Controls.Add(Me.chkRepair)
        Me.RadPageViewPage1.Controls.Add(Me.ChkISPO)
        Me.RadPageViewPage1.Controls.Add(Me.Panel4)
        Me.RadPageViewPage1.Controls.Add(Me.txt_deliverydays)
        Me.RadPageViewPage1.Controls.Add(Me.chkJobWorkOutward)
        Me.RadPageViewPage1.Controls.Add(Me.ddl_category)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage1.Controls.Add(Me.chkGSTRegistered)
        Me.RadPageViewPage1.Controls.Add(Me.Panel3)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtBillDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage1.Controls.Add(Me.chk_emergency)
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Controls.Add(Me.chkAutoCalculate)
        Me.RadPageViewPage1.Controls.Add(Me.txtAgainstPO_No)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.dtpRenewal)
        Me.RadPageViewPage1.Controls.Add(Me.chkOpenPO)
        Me.RadPageViewPage1.Controls.Add(Me.chkIsMerchantTrade)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.Panel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.chkBlanket)
        Me.RadPageViewPage1.Controls.Add(Me.lblAmt)
        Me.RadPageViewPage1.Controls.Add(Me.txtAmount)
        Me.RadPageViewPage1.Controls.Add(Me.cboPOType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.cboModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.dtpExpiryDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmtCopy)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtQuotationNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtQuotationDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRGPNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblAmbendmentNoCaption)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.lblDept)
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.lblDeliveryDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkAgainst_RGP)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDept)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDeliveryDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(96.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage1.Text = "Purchase Order"
        '
        'MyLabel59
        '
        Me.MyLabel59.FieldName = Nothing
        Me.MyLabel59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel59.Location = New System.Drawing.Point(868, 41)
        Me.MyLabel59.Name = "MyLabel59"
        Me.MyLabel59.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel59.TabIndex = 611
        Me.MyLabel59.Text = "Tender"
        '
        'txtTenderNo
        '
        Me.txtTenderNo.CalculationExpression = Nothing
        Me.txtTenderNo.FieldCode = Nothing
        Me.txtTenderNo.FieldDesc = Nothing
        Me.txtTenderNo.FieldMaxLength = 0
        Me.txtTenderNo.FieldName = Nothing
        Me.txtTenderNo.isCalculatedField = False
        Me.txtTenderNo.IsSourceFromTable = False
        Me.txtTenderNo.IsSourceFromValueList = False
        Me.txtTenderNo.IsUnique = False
        Me.txtTenderNo.Location = New System.Drawing.Point(914, 40)
        Me.txtTenderNo.MendatroryField = False
        Me.txtTenderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenderNo.MyLinkLable1 = Me.MyLabel59
        Me.txtTenderNo.MyLinkLable2 = Me.lblDept
        Me.txtTenderNo.MyReadOnly = False
        Me.txtTenderNo.MyShowMasterFormButton = False
        Me.txtTenderNo.Name = "txtTenderNo"
        Me.txtTenderNo.ReferenceFieldDesc = Nothing
        Me.txtTenderNo.ReferenceFieldName = Nothing
        Me.txtTenderNo.ReferenceTableName = Nothing
        Me.txtTenderNo.Size = New System.Drawing.Size(118, 19)
        Me.txtTenderNo.TabIndex = 610
        Me.txtTenderNo.Value = ""
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(241, 97)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(264, 18)
        Me.lblDept.TabIndex = 37
        Me.lblDept.TextWrap = False
        '
        'txtRefTendorNo
        '
        Me.txtRefTendorNo.CalculationExpression = Nothing
        Me.txtRefTendorNo.FieldCode = Nothing
        Me.txtRefTendorNo.FieldDesc = Nothing
        Me.txtRefTendorNo.FieldMaxLength = 0
        Me.txtRefTendorNo.FieldName = Nothing
        Me.txtRefTendorNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefTendorNo.isCalculatedField = False
        Me.txtRefTendorNo.IsSourceFromTable = False
        Me.txtRefTendorNo.IsSourceFromValueList = False
        Me.txtRefTendorNo.IsUnique = False
        Me.txtRefTendorNo.Location = New System.Drawing.Point(956, 21)
        Me.txtRefTendorNo.MaxLength = 50
        Me.txtRefTendorNo.MendatroryField = False
        Me.txtRefTendorNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefTendorNo.MyLinkLable2 = Nothing
        Me.txtRefTendorNo.Name = "txtRefTendorNo"
        Me.txtRefTendorNo.ReferenceFieldDesc = Nothing
        Me.txtRefTendorNo.ReferenceFieldName = Nothing
        Me.txtRefTendorNo.ReferenceTableName = Nothing
        Me.txtRefTendorNo.Size = New System.Drawing.Size(124, 18)
        Me.txtRefTendorNo.TabIndex = 609
        Me.txtRefTendorNo.Visible = False
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(505, 60)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel7.TabIndex = 42
        Me.RadLabel7.Text = "Delivered To"
        '
        'MyLabel55
        '
        Me.MyLabel55.FieldName = Nothing
        Me.MyLabel55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel55.Location = New System.Drawing.Point(868, 21)
        Me.MyLabel55.Name = "MyLabel55"
        Me.MyLabel55.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel55.TabIndex = 608
        Me.MyLabel55.Text = "Ref Tendor No"
        Me.MyLabel55.Visible = False
        '
        'chkTender
        '
        Me.chkTender.Location = New System.Drawing.Point(959, 144)
        Me.chkTender.MyLinkLable1 = Nothing
        Me.chkTender.MyLinkLable2 = Nothing
        Me.chkTender.Name = "chkTender"
        Me.chkTender.Size = New System.Drawing.Size(55, 18)
        Me.chkTender.TabIndex = 607
        Me.chkTender.Tag1 = Nothing
        Me.chkTender.Text = "Tender"
        '
        'MyLabel50
        '
        Me.MyLabel50.FieldName = Nothing
        Me.MyLabel50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel50.Location = New System.Drawing.Point(784, 202)
        Me.MyLabel50.Name = "MyLabel50"
        Me.MyLabel50.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel50.TabIndex = 606
        Me.MyLabel50.Text = "Vendor Quotation"
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
        Me.fndProject.Location = New System.Drawing.Point(979, 108)
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
        Me.fndProject.Size = New System.Drawing.Size(38, 20)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        Me.fndProject.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 2)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel4.TabIndex = 1
        Me.MyLabel4.Text = "Reference"
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(90, 4)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(42, 20)
        Me.lblProject.TabIndex = 2
        Me.lblProject.TextWrap = False
        Me.lblProject.Visible = False
        '
        'lblVendorQuotationNo
        '
        Me.lblVendorQuotationNo.AutoSize = False
        Me.lblVendorQuotationNo.BorderVisible = True
        Me.lblVendorQuotationNo.FieldName = Nothing
        Me.lblVendorQuotationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorQuotationNo.Location = New System.Drawing.Point(885, 201)
        Me.lblVendorQuotationNo.Name = "lblVendorQuotationNo"
        Me.lblVendorQuotationNo.Size = New System.Drawing.Size(129, 19)
        Me.lblVendorQuotationNo.TabIndex = 605
        Me.lblVendorQuotationNo.TextWrap = False
        '
        'chkRepair
        '
        Me.chkRepair.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRepair.Location = New System.Drawing.Point(454, 117)
        Me.chkRepair.Name = "chkRepair"
        Me.chkRepair.Size = New System.Drawing.Size(54, 16)
        Me.chkRepair.TabIndex = 604
        Me.chkRepair.Text = "Repair"
        Me.chkRepair.Visible = False
        '
        'ChkISPO
        '
        Me.ChkISPO.AccessibleDescription = ""
        Me.ChkISPO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkISPO.Location = New System.Drawing.Point(889, 147)
        Me.ChkISPO.Name = "ChkISPO"
        Me.ChkISPO.Size = New System.Drawing.Size(48, 16)
        Me.ChkISPO.TabIndex = 69
        Me.ChkISPO.Text = "Is PO"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lblGstinNo)
        Me.Panel4.Controls.Add(Me.MyLabel49)
        Me.Panel4.Controls.Add(Me.MyLabel45)
        Me.Panel4.Controls.Add(Me.lblRegisterOrUnregister)
        Me.Panel4.Location = New System.Drawing.Point(507, 160)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(445, 21)
        Me.Panel4.TabIndex = 603
        '
        'lblGstinNo
        '
        Me.lblGstinNo.AutoSize = False
        Me.lblGstinNo.BorderVisible = True
        Me.lblGstinNo.FieldName = Nothing
        Me.lblGstinNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGstinNo.Location = New System.Drawing.Point(260, 2)
        Me.lblGstinNo.Name = "lblGstinNo"
        Me.lblGstinNo.Size = New System.Drawing.Size(183, 19)
        Me.lblGstinNo.TabIndex = 0
        Me.lblGstinNo.TextWrap = False
        '
        'MyLabel49
        '
        Me.MyLabel49.FieldName = Nothing
        Me.MyLabel49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel49.Location = New System.Drawing.Point(215, 3)
        Me.MyLabel49.Name = "MyLabel49"
        Me.MyLabel49.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel49.TabIndex = 2
        Me.MyLabel49.Text = "GSTIN"
        '
        'MyLabel45
        '
        Me.MyLabel45.FieldName = Nothing
        Me.MyLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel45.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel45.Name = "MyLabel45"
        Me.MyLabel45.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel45.TabIndex = 1
        Me.MyLabel45.Text = "Vendor "
        '
        'lblRegisterOrUnregister
        '
        Me.lblRegisterOrUnregister.AutoSize = False
        Me.lblRegisterOrUnregister.BorderVisible = True
        Me.lblRegisterOrUnregister.FieldName = Nothing
        Me.lblRegisterOrUnregister.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegisterOrUnregister.Location = New System.Drawing.Point(66, 2)
        Me.lblRegisterOrUnregister.Name = "lblRegisterOrUnregister"
        Me.lblRegisterOrUnregister.Size = New System.Drawing.Size(147, 19)
        Me.lblRegisterOrUnregister.TabIndex = 3
        Me.lblRegisterOrUnregister.TextWrap = False
        '
        'txt_deliverydays
        '
        Me.txt_deliverydays.BackColor = System.Drawing.Color.White
        Me.txt_deliverydays.CalculationExpression = Nothing
        Me.txt_deliverydays.DecimalPlaces = 0
        Me.txt_deliverydays.FieldCode = Nothing
        Me.txt_deliverydays.FieldDesc = Nothing
        Me.txt_deliverydays.FieldMaxLength = 5
        Me.txt_deliverydays.FieldName = Nothing
        Me.txt_deliverydays.isCalculatedField = False
        Me.txt_deliverydays.IsSourceFromTable = False
        Me.txt_deliverydays.IsSourceFromValueList = False
        Me.txt_deliverydays.IsUnique = False
        Me.txt_deliverydays.Location = New System.Drawing.Point(914, 58)
        Me.txt_deliverydays.MendatroryField = False
        Me.txt_deliverydays.MyLinkLable1 = Me.MyLabel42
        Me.txt_deliverydays.MyLinkLable2 = Nothing
        Me.txt_deliverydays.Name = "txt_deliverydays"
        Me.txt_deliverydays.ReferenceFieldDesc = Nothing
        Me.txt_deliverydays.ReferenceFieldName = Nothing
        Me.txt_deliverydays.ReferenceTableName = Nothing
        Me.txt_deliverydays.Size = New System.Drawing.Size(46, 20)
        Me.txt_deliverydays.TabIndex = 13
        Me.txt_deliverydays.Text = "0"
        Me.txt_deliverydays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_deliverydays.Value = 0R
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(868, 60)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel42.TabIndex = 75
        Me.MyLabel42.Text = "Days"
        '
        'chkJobWorkOutward
        '
        Me.chkJobWorkOutward.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWorkOutward.Location = New System.Drawing.Point(345, 117)
        Me.chkJobWorkOutward.Name = "chkJobWorkOutward"
        Me.chkJobWorkOutward.Size = New System.Drawing.Size(108, 16)
        Me.chkJobWorkOutward.TabIndex = 23
        Me.chkJobWorkOutward.Text = "JobWorkOutward"
        '
        'ddl_category
        '
        Me.ddl_category.AutoCompleteDisplayMember = Nothing
        Me.ddl_category.AutoCompleteValueMember = Nothing
        Me.ddl_category.CalculationExpression = Nothing
        Me.ddl_category.DropDownAnimationEnabled = True
        Me.ddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_category.FieldCode = Nothing
        Me.ddl_category.FieldDesc = Nothing
        Me.ddl_category.FieldMaxLength = 0
        Me.ddl_category.FieldName = Nothing
        Me.ddl_category.isCalculatedField = False
        Me.ddl_category.IsSourceFromTable = False
        Me.ddl_category.IsSourceFromValueList = False
        Me.ddl_category.IsUnique = False
        Me.ddl_category.Location = New System.Drawing.Point(573, 136)
        Me.ddl_category.MendatroryField = True
        Me.ddl_category.MyLinkLable1 = Me.MyLabel39
        Me.ddl_category.MyLinkLable2 = Nothing
        Me.ddl_category.Name = "ddl_category"
        Me.ddl_category.ReferenceFieldDesc = Nothing
        Me.ddl_category.ReferenceFieldName = Nothing
        Me.ddl_category.ReferenceTableName = Nothing
        Me.ddl_category.Size = New System.Drawing.Size(217, 20)
        Me.ddl_category.TabIndex = 25
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(505, 138)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel39.TabIndex = 73
        Me.MyLabel39.Text = "Category"
        '
        'chkGSTRegistered
        '
        Me.chkGSTRegistered.AccessibleDescription = ""
        Me.chkGSTRegistered.Enabled = False
        Me.chkGSTRegistered.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTRegistered.Location = New System.Drawing.Point(789, 130)
        Me.chkGSTRegistered.Name = "chkGSTRegistered"
        Me.chkGSTRegistered.Size = New System.Drawing.Size(95, 16)
        Me.chkGSTRegistered.TabIndex = 26
        Me.chkGSTRegistered.Text = "GST Registred"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.MyLabel43)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(3, 135)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(503, 23)
        Me.Panel3.TabIndex = 24
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(238, 2)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(264, 19)
        Me.lblSubLocation.TabIndex = 276
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(5, 4)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel43.TabIndex = 0
        Me.MyLabel43.Text = "Sub Location"
        '
        'txtSubLocation
        '
        Me.txtSubLocation.CalculationExpression = Nothing
        Me.txtSubLocation.FieldCode = Nothing
        Me.txtSubLocation.FieldDesc = Nothing
        Me.txtSubLocation.FieldMaxLength = 0
        Me.txtSubLocation.FieldName = Nothing
        Me.txtSubLocation.isCalculatedField = False
        Me.txtSubLocation.IsSourceFromTable = False
        Me.txtSubLocation.IsSourceFromValueList = False
        Me.txtSubLocation.IsUnique = False
        Me.txtSubLocation.Location = New System.Drawing.Point(105, 2)
        Me.txtSubLocation.MendatroryField = True
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Nothing
        Me.txtSubLocation.MyLinkLable2 = Nothing
        Me.txtSubLocation.MyReadOnly = False
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(132, 20)
        Me.txtSubLocation.TabIndex = 1
        Me.txtSubLocation.Value = ""
        '
        'lblBillDate
        '
        Me.lblBillDate.FieldName = Nothing
        Me.lblBillDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDate.Location = New System.Drawing.Point(765, 184)
        Me.lblBillDate.Name = "lblBillDate"
        Me.lblBillDate.Size = New System.Drawing.Size(48, 16)
        Me.lblBillDate.TabIndex = 191
        Me.lblBillDate.Text = "Bill Date"
        Me.lblBillDate.Visible = False
        '
        'dtBillDate
        '
        Me.dtBillDate.CalculationExpression = Nothing
        Me.dtBillDate.CustomFormat = "dd/MM/yyyy"
        Me.dtBillDate.FieldCode = Nothing
        Me.dtBillDate.FieldDesc = Nothing
        Me.dtBillDate.FieldMaxLength = 0
        Me.dtBillDate.FieldName = Nothing
        Me.dtBillDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtBillDate.isCalculatedField = False
        Me.dtBillDate.IsSourceFromTable = False
        Me.dtBillDate.IsSourceFromValueList = False
        Me.dtBillDate.IsUnique = False
        Me.dtBillDate.Location = New System.Drawing.Point(813, 183)
        Me.dtBillDate.MendatroryField = False
        Me.dtBillDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtBillDate.MyLinkLable1 = Me.lblBillDate
        Me.dtBillDate.MyLinkLable2 = Nothing
        Me.dtBillDate.Name = "dtBillDate"
        Me.dtBillDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtBillDate.ReferenceFieldDesc = Nothing
        Me.dtBillDate.ReferenceFieldName = Nothing
        Me.dtBillDate.ReferenceTableName = Nothing
        Me.dtBillDate.Size = New System.Drawing.Size(83, 18)
        Me.dtBillDate.TabIndex = 30
        Me.dtBillDate.TabStop = False
        Me.dtBillDate.Text = "13/06/2011"
        Me.dtBillDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.dtBillDate.Visible = False
        '
        'lblBillNo
        '
        Me.lblBillNo.FieldName = Nothing
        Me.lblBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.Location = New System.Drawing.Point(614, 182)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.Size = New System.Drawing.Size(39, 16)
        Me.lblBillNo.TabIndex = 189
        Me.lblBillNo.Text = "Bill No"
        Me.lblBillNo.Visible = False
        '
        'txtBillNo
        '
        Me.txtBillNo.CalculationExpression = Nothing
        Me.txtBillNo.FieldCode = Nothing
        Me.txtBillNo.FieldDesc = Nothing
        Me.txtBillNo.FieldMaxLength = 0
        Me.txtBillNo.FieldName = Nothing
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.isCalculatedField = False
        Me.txtBillNo.IsSourceFromTable = False
        Me.txtBillNo.IsSourceFromValueList = False
        Me.txtBillNo.IsUnique = False
        Me.txtBillNo.Location = New System.Drawing.Point(653, 183)
        Me.txtBillNo.MaxLength = 200
        Me.txtBillNo.MendatroryField = False
        Me.txtBillNo.MyLinkLable1 = Me.lblBillNo
        Me.txtBillNo.MyLinkLable2 = Nothing
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.ReferenceFieldDesc = Nothing
        Me.txtBillNo.ReferenceFieldName = Nothing
        Me.txtBillNo.ReferenceTableName = Nothing
        Me.txtBillNo.Size = New System.Drawing.Size(110, 18)
        Me.txtBillNo.TabIndex = 29
        Me.txtBillNo.Visible = False
        '
        'chk_emergency
        '
        Me.chk_emergency.Controls.Add(Me.lblProject)
        Me.chk_emergency.Location = New System.Drawing.Point(889, 130)
        Me.chk_emergency.MyLinkLable1 = Nothing
        Me.chk_emergency.MyLinkLable2 = Nothing
        Me.chk_emergency.Name = "chk_emergency"
        Me.chk_emergency.Size = New System.Drawing.Size(75, 18)
        Me.chk_emergency.TabIndex = 27
        Me.chk_emergency.Tag1 = Nothing
        Me.chk_emergency.Text = "Emergency"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 217)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnl_capex)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1077, 102)
        Me.SplitContainer2.SplitterDistance = 49
        Me.SplitContainer2.TabIndex = 71
        '
        'pnl_capex
        '
        Me.pnl_capex.Controls.Add(Me.chkReceiveControl)
        Me.pnl_capex.Controls.Add(Me.MyLabel37)
        Me.pnl_capex.Controls.Add(Me.MyLabel38)
        Me.pnl_capex.Controls.Add(Me.MyLabel40)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel35)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel36)
        Me.pnl_capex.Controls.Add(Me.fndcapexsubcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexsubcode)
        Me.pnl_capex.Controls.Add(Me.MyLabel34)
        Me.pnl_capex.Controls.Add(Me.fndcapexcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexcode)
        Me.pnl_capex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_capex.Location = New System.Drawing.Point(2, 2)
        Me.pnl_capex.Name = "pnl_capex"
        Me.pnl_capex.Size = New System.Drawing.Size(1073, 45)
        Me.pnl_capex.TabIndex = 0
        '
        'chkReceiveControl
        '
        Me.chkReceiveControl.Location = New System.Drawing.Point(867, 25)
        Me.chkReceiveControl.MyLinkLable1 = Nothing
        Me.chkReceiveControl.MyLinkLable2 = Nothing
        Me.chkReceiveControl.Name = "chkReceiveControl"
        Me.chkReceiveControl.Size = New System.Drawing.Size(115, 18)
        Me.chkReceiveControl.TabIndex = 75
        Me.chkReceiveControl.Tag1 = Nothing
        Me.chkReceiveControl.Text = "Control on Receive"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(623, 26)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(143, 16)
        Me.MyLabel37.TabIndex = 15
        Me.MyLabel37.Text = "Tolerence Balance Amount"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(416, 26)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel38.TabIndex = 11
        Me.MyLabel38.Text = "Tolerence Amount"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(239, 26)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel40.TabIndex = 13
        Me.MyLabel40.Text = "Bal. Amount"
        '
        'lbl_rebudgetamtwithtolerence
        '
        Me.lbl_rebudgetamtwithtolerence.AutoSize = False
        Me.lbl_rebudgetamtwithtolerence.BorderVisible = True
        Me.lbl_rebudgetamtwithtolerence.FieldName = Nothing
        Me.lbl_rebudgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamtwithtolerence.Location = New System.Drawing.Point(768, 25)
        Me.lbl_rebudgetamtwithtolerence.Name = "lbl_rebudgetamtwithtolerence"
        Me.lbl_rebudgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamtwithtolerence.TabIndex = 16
        Me.lbl_rebudgetamtwithtolerence.TextWrap = False
        '
        'lbl_rebudgetamt
        '
        Me.lbl_rebudgetamt.AutoSize = False
        Me.lbl_rebudgetamt.BorderVisible = True
        Me.lbl_rebudgetamt.FieldName = Nothing
        Me.lbl_rebudgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamt.Location = New System.Drawing.Point(313, 25)
        Me.lbl_rebudgetamt.Name = "lbl_rebudgetamt"
        Me.lbl_rebudgetamt.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamt.TabIndex = 14
        Me.lbl_rebudgetamt.TextWrap = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(6, 26)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel35.TabIndex = 9
        Me.MyLabel35.Text = "Amount"
        '
        'lbl_budgetamtwithtolerence
        '
        Me.lbl_budgetamtwithtolerence.AutoSize = False
        Me.lbl_budgetamtwithtolerence.BorderVisible = True
        Me.lbl_budgetamtwithtolerence.FieldName = Nothing
        Me.lbl_budgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamtwithtolerence.Location = New System.Drawing.Point(521, 25)
        Me.lbl_budgetamtwithtolerence.Name = "lbl_budgetamtwithtolerence"
        Me.lbl_budgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_budgetamtwithtolerence.TabIndex = 12
        Me.lbl_budgetamtwithtolerence.TextWrap = False
        '
        'lbl_budgetamt
        '
        Me.lbl_budgetamt.AutoSize = False
        Me.lbl_budgetamt.BorderVisible = True
        Me.lbl_budgetamt.FieldName = Nothing
        Me.lbl_budgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamt.Location = New System.Drawing.Point(106, 25)
        Me.lbl_budgetamt.Name = "lbl_budgetamt"
        Me.lbl_budgetamt.Size = New System.Drawing.Size(131, 19)
        Me.lbl_budgetamt.TabIndex = 10
        Me.lbl_budgetamt.TextWrap = False
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(5, 5)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel36.TabIndex = 7
        Me.MyLabel36.Text = "Capex Sub-Code"
        '
        'fndcapexsubcode
        '
        Me.fndcapexsubcode.CalculationExpression = Nothing
        Me.fndcapexsubcode.FieldCode = Nothing
        Me.fndcapexsubcode.FieldDesc = Nothing
        Me.fndcapexsubcode.FieldMaxLength = 0
        Me.fndcapexsubcode.FieldName = Nothing
        Me.fndcapexsubcode.isCalculatedField = False
        Me.fndcapexsubcode.IsSourceFromTable = False
        Me.fndcapexsubcode.IsSourceFromValueList = False
        Me.fndcapexsubcode.IsUnique = False
        Me.fndcapexsubcode.Location = New System.Drawing.Point(106, 3)
        Me.fndcapexsubcode.MendatroryField = True
        Me.fndcapexsubcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexsubcode.MyLinkLable1 = Me.MyLabel36
        Me.fndcapexsubcode.MyLinkLable2 = Me.lbl_capexsubcode
        Me.fndcapexsubcode.MyReadOnly = False
        Me.fndcapexsubcode.MyShowMasterFormButton = False
        Me.fndcapexsubcode.Name = "fndcapexsubcode"
        Me.fndcapexsubcode.ReferenceFieldDesc = Nothing
        Me.fndcapexsubcode.ReferenceFieldName = Nothing
        Me.fndcapexsubcode.ReferenceTableName = Nothing
        Me.fndcapexsubcode.Size = New System.Drawing.Size(131, 20)
        Me.fndcapexsubcode.TabIndex = 6
        Me.fndcapexsubcode.Value = ""
        '
        'lbl_capexsubcode
        '
        Me.lbl_capexsubcode.AutoSize = False
        Me.lbl_capexsubcode.BorderVisible = True
        Me.lbl_capexsubcode.FieldName = Nothing
        Me.lbl_capexsubcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexsubcode.Location = New System.Drawing.Point(239, 4)
        Me.lbl_capexsubcode.Name = "lbl_capexsubcode"
        Me.lbl_capexsubcode.Size = New System.Drawing.Size(264, 19)
        Me.lbl_capexsubcode.TabIndex = 8
        Me.lbl_capexsubcode.TextWrap = False
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(505, 6)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel34.TabIndex = 4
        Me.MyLabel34.Text = "Capex Code"
        '
        'fndcapexcode
        '
        Me.fndcapexcode.CalculationExpression = Nothing
        Me.fndcapexcode.Enabled = False
        Me.fndcapexcode.FieldCode = Nothing
        Me.fndcapexcode.FieldDesc = Nothing
        Me.fndcapexcode.FieldMaxLength = 0
        Me.fndcapexcode.FieldName = Nothing
        Me.fndcapexcode.isCalculatedField = False
        Me.fndcapexcode.IsSourceFromTable = False
        Me.fndcapexcode.IsSourceFromValueList = False
        Me.fndcapexcode.IsUnique = False
        Me.fndcapexcode.Location = New System.Drawing.Point(588, 5)
        Me.fndcapexcode.MendatroryField = False
        Me.fndcapexcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexcode.MyLinkLable1 = Me.MyLabel34
        Me.fndcapexcode.MyLinkLable2 = Me.lbl_capexcode
        Me.fndcapexcode.MyReadOnly = False
        Me.fndcapexcode.MyShowMasterFormButton = False
        Me.fndcapexcode.Name = "fndcapexcode"
        Me.fndcapexcode.ReferenceFieldDesc = Nothing
        Me.fndcapexcode.ReferenceFieldName = Nothing
        Me.fndcapexcode.ReferenceTableName = Nothing
        Me.fndcapexcode.Size = New System.Drawing.Size(131, 19)
        Me.fndcapexcode.TabIndex = 3
        Me.fndcapexcode.Value = ""
        '
        'lbl_capexcode
        '
        Me.lbl_capexcode.AutoSize = False
        Me.lbl_capexcode.BorderVisible = True
        Me.lbl_capexcode.FieldName = Nothing
        Me.lbl_capexcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexcode.Location = New System.Drawing.Point(721, 5)
        Me.lbl_capexcode.Name = "lbl_capexcode"
        Me.lbl_capexcode.Size = New System.Drawing.Size(262, 19)
        Me.lbl_capexcode.TabIndex = 5
        Me.lbl_capexcode.TextWrap = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 2)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1073, 45)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
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
        Me.gv1.Size = New System.Drawing.Size(1053, 15)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'chkAutoCalculate
        '
        Me.chkAutoCalculate.AccessibleDescription = ""
        Me.chkAutoCalculate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoCalculate.Location = New System.Drawing.Point(789, 145)
        Me.chkAutoCalculate.Name = "chkAutoCalculate"
        Me.chkAutoCalculate.Size = New System.Drawing.Size(94, 16)
        Me.chkAutoCalculate.TabIndex = 68
        Me.chkAutoCalculate.Text = "Auto Calculate"
        '
        'txtAgainstPO_No
        '
        Me.txtAgainstPO_No.AutoSize = False
        Me.txtAgainstPO_No.BorderVisible = True
        Me.txtAgainstPO_No.FieldName = Nothing
        Me.txtAgainstPO_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgainstPO_No.Location = New System.Drawing.Point(674, 200)
        Me.txtAgainstPO_No.Name = "txtAgainstPO_No"
        Me.txtAgainstPO_No.Size = New System.Drawing.Size(108, 18)
        Me.txtAgainstPO_No.TabIndex = 32
        Me.txtAgainstPO_No.TextWrap = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(504, 202)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel13.TabIndex = 66
        Me.MyLabel13.Text = "Renewal Date"
        '
        'dtpRenewal
        '
        Me.dtpRenewal.CalculationExpression = Nothing
        Me.dtpRenewal.CustomFormat = "dd/MM/yyyy"
        Me.dtpRenewal.FieldCode = Nothing
        Me.dtpRenewal.FieldDesc = Nothing
        Me.dtpRenewal.FieldMaxLength = 0
        Me.dtpRenewal.FieldName = Nothing
        Me.dtpRenewal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpRenewal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRenewal.isCalculatedField = False
        Me.dtpRenewal.IsSourceFromTable = False
        Me.dtpRenewal.IsSourceFromValueList = False
        Me.dtpRenewal.IsUnique = False
        Me.dtpRenewal.Location = New System.Drawing.Point(581, 201)
        Me.dtpRenewal.MendatroryField = False
        Me.dtpRenewal.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRenewal.MyLinkLable1 = Me.MyLabel13
        Me.dtpRenewal.MyLinkLable2 = Me.txtAgainstPO_No
        Me.dtpRenewal.Name = "dtpRenewal"
        Me.dtpRenewal.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRenewal.ReferenceFieldDesc = Nothing
        Me.dtpRenewal.ReferenceFieldName = Nothing
        Me.dtpRenewal.ReferenceTableName = Nothing
        Me.dtpRenewal.ShowCheckBox = True
        Me.dtpRenewal.Size = New System.Drawing.Size(91, 18)
        Me.dtpRenewal.TabIndex = 31
        Me.dtpRenewal.TabStop = False
        Me.dtpRenewal.Text = "13/06/2011"
        Me.dtpRenewal.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkOpenPO
        '
        Me.chkOpenPO.Location = New System.Drawing.Point(889, 113)
        Me.chkOpenPO.MyLinkLable1 = Nothing
        Me.chkOpenPO.MyLinkLable2 = Nothing
        Me.chkOpenPO.Name = "chkOpenPO"
        Me.chkOpenPO.Size = New System.Drawing.Size(66, 18)
        Me.chkOpenPO.TabIndex = 24
        Me.chkOpenPO.Tag1 = Nothing
        Me.chkOpenPO.Text = "Open PO"
        '
        'chkIsMerchantTrade
        '
        Me.chkIsMerchantTrade.AccessibleDescription = ""
        Me.chkIsMerchantTrade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsMerchantTrade.Location = New System.Drawing.Point(789, 114)
        Me.chkIsMerchantTrade.Name = "chkIsMerchantTrade"
        Me.chkIsMerchantTrade.Size = New System.Drawing.Size(100, 16)
        Me.chkIsMerchantTrade.TabIndex = 23
        Me.chkIsMerchantTrade.Text = "Merchant Trade"
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(979, 190)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblState)
        Me.Panel2.Controls.Add(Me.fndState)
        Me.Panel2.Controls.Add(Me.lblStateName)
        Me.Panel2.Controls.Add(Me.chkMCCPurchase)
        Me.Panel2.Location = New System.Drawing.Point(3, 183)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(609, 22)
        Me.Panel2.TabIndex = 28
        '
        'lblState
        '
        Me.lblState.FieldName = Nothing
        Me.lblState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(5, 3)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(33, 16)
        Me.lblState.TabIndex = 52
        Me.lblState.Text = "State"
        '
        'fndState
        '
        Me.fndState.CalculationExpression = Nothing
        Me.fndState.Enabled = False
        Me.fndState.FieldCode = Nothing
        Me.fndState.FieldDesc = Nothing
        Me.fndState.FieldMaxLength = 0
        Me.fndState.FieldName = Nothing
        Me.fndState.isCalculatedField = False
        Me.fndState.IsSourceFromTable = False
        Me.fndState.IsSourceFromValueList = False
        Me.fndState.IsUnique = False
        Me.fndState.Location = New System.Drawing.Point(105, 2)
        Me.fndState.MendatroryField = False
        Me.fndState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndState.MyLinkLable1 = Me.lblState
        Me.fndState.MyLinkLable2 = Me.lblStateName
        Me.fndState.MyReadOnly = False
        Me.fndState.MyShowMasterFormButton = False
        Me.fndState.Name = "fndState"
        Me.fndState.ReferenceFieldDesc = Nothing
        Me.fndState.ReferenceFieldName = Nothing
        Me.fndState.ReferenceTableName = Nothing
        Me.fndState.Size = New System.Drawing.Size(132, 19)
        Me.fndState.TabIndex = 0
        Me.fndState.Value = ""
        '
        'lblStateName
        '
        Me.lblStateName.AutoSize = False
        Me.lblStateName.BorderVisible = True
        Me.lblStateName.FieldName = Nothing
        Me.lblStateName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateName.Location = New System.Drawing.Point(238, 3)
        Me.lblStateName.Name = "lblStateName"
        Me.lblStateName.Size = New System.Drawing.Size(264, 18)
        Me.lblStateName.TabIndex = 53
        Me.lblStateName.TextWrap = False
        '
        'chkMCCPurchase
        '
        Me.chkMCCPurchase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMCCPurchase.Location = New System.Drawing.Point(502, 4)
        Me.chkMCCPurchase.Name = "chkMCCPurchase"
        Me.chkMCCPurchase.Size = New System.Drawing.Size(97, 16)
        Me.chkMCCPurchase.TabIndex = 1
        Me.chkMCCPurchase.Text = "MCC Purchase"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(901, 1)
        Me.MyLabel7.MaximumSize = New System.Drawing.Size(177, 16)
        Me.MyLabel7.MinimumSize = New System.Drawing.Size(177, 16)
        Me.MyLabel7.Name = "MyLabel7"
        '
        '
        '
        Me.MyLabel7.RootElement.MaxSize = New System.Drawing.Size(177, 16)
        Me.MyLabel7.RootElement.MinSize = New System.Drawing.Size(177, 16)
        Me.MyLabel7.Size = New System.Drawing.Size(177, 16)
        Me.MyLabel7.TabIndex = 61
        Me.MyLabel7.Text = "Entered PO"
        Me.MyLabel7.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'chkBlanket
        '
        Me.chkBlanket.AccessibleDescription = ""
        Me.chkBlanket.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBlanket.Location = New System.Drawing.Point(505, 98)
        Me.chkBlanket.Name = "chkBlanket"
        Me.chkBlanket.Size = New System.Drawing.Size(58, 16)
        Me.chkBlanket.TabIndex = 18
        Me.chkBlanket.Text = "Blanket"
        '
        'lblAmt
        '
        Me.lblAmt.FieldName = Nothing
        Me.lblAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmt.Location = New System.Drawing.Point(726, 98)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(63, 16)
        Me.lblAmt.TabIndex = 59
        Me.lblAmt.Text = "NetAmount"
        Me.lblAmt.Visible = False
        '
        'txtAmount
        '
        Me.txtAmount.CalculationExpression = Nothing
        Me.txtAmount.FieldCode = Nothing
        Me.txtAmount.FieldDesc = Nothing
        Me.txtAmount.FieldMaxLength = 0
        Me.txtAmount.FieldName = Nothing
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.isCalculatedField = False
        Me.txtAmount.IsSourceFromTable = False
        Me.txtAmount.IsSourceFromValueList = False
        Me.txtAmount.IsUnique = False
        Me.txtAmount.Location = New System.Drawing.Point(790, 97)
        Me.txtAmount.MaxLength = 200
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Me.lblAmt
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReferenceFieldDesc = Nothing
        Me.txtAmount.ReferenceFieldName = Nothing
        Me.txtAmount.ReferenceTableName = Nothing
        Me.txtAmount.Size = New System.Drawing.Size(162, 18)
        Me.txtAmount.TabIndex = 20
        Me.txtAmount.Visible = False
        '
        'cboPOType
        '
        Me.cboPOType.AutoCompleteDisplayMember = Nothing
        Me.cboPOType.AutoCompleteValueMember = Nothing
        Me.cboPOType.CalculationExpression = Nothing
        Me.cboPOType.DropDownAnimationEnabled = True
        Me.cboPOType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPOType.FieldCode = Nothing
        Me.cboPOType.FieldDesc = Nothing
        Me.cboPOType.FieldMaxLength = 0
        Me.cboPOType.FieldName = Nothing
        Me.cboPOType.isCalculatedField = False
        Me.cboPOType.IsSourceFromTable = False
        Me.cboPOType.IsSourceFromValueList = False
        Me.cboPOType.IsUnique = False
        Me.cboPOType.Location = New System.Drawing.Point(300, 19)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.RadLabel8
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(205, 20)
        Me.cboPOType.TabIndex = 6
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(243, 21)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 57
        Me.RadLabel8.Text = "PO Type"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(5, 21)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel5.TabIndex = 56
        Me.RadLabel5.Text = "Mode of Transport"
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.AutoCompleteDisplayMember = Nothing
        Me.cboModeOfTransport.AutoCompleteValueMember = Nothing
        Me.cboModeOfTransport.CalculationExpression = Nothing
        Me.cboModeOfTransport.DropDownAnimationEnabled = True
        Me.cboModeOfTransport.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModeOfTransport.FieldCode = Nothing
        Me.cboModeOfTransport.FieldDesc = Nothing
        Me.cboModeOfTransport.FieldMaxLength = 0
        Me.cboModeOfTransport.FieldName = Nothing
        Me.cboModeOfTransport.isCalculatedField = False
        Me.cboModeOfTransport.IsSourceFromTable = False
        Me.cboModeOfTransport.IsSourceFromValueList = False
        Me.cboModeOfTransport.IsUnique = False
        Me.cboModeOfTransport.Location = New System.Drawing.Point(108, 19)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.RadLabel5
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.ReferenceFieldDesc = Nothing
        Me.cboModeOfTransport.ReferenceFieldName = Nothing
        Me.cboModeOfTransport.ReferenceTableName = Nothing
        Me.cboModeOfTransport.Size = New System.Drawing.Size(132, 20)
        Me.cboModeOfTransport.TabIndex = 5
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(576, 98)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel6.TabIndex = 49
        Me.MyLabel6.Text = "ExpireOn"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CalculationExpression = Nothing
        Me.dtpExpiryDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpExpiryDate.FieldCode = Nothing
        Me.dtpExpiryDate.FieldDesc = Nothing
        Me.dtpExpiryDate.FieldMaxLength = 0
        Me.dtpExpiryDate.FieldName = Nothing
        Me.dtpExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.isCalculatedField = False
        Me.dtpExpiryDate.IsSourceFromTable = False
        Me.dtpExpiryDate.IsSourceFromValueList = False
        Me.dtpExpiryDate.IsUnique = False
        Me.dtpExpiryDate.Location = New System.Drawing.Point(632, 97)
        Me.dtpExpiryDate.MendatroryField = False
        Me.dtpExpiryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpExpiryDate.MyLinkLable1 = Me.MyLabel6
        Me.dtpExpiryDate.MyLinkLable2 = Nothing
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpExpiryDate.ReferenceFieldDesc = Nothing
        Me.dtpExpiryDate.ReferenceFieldName = Nothing
        Me.dtpExpiryDate.ReferenceTableName = Nothing
        Me.dtpExpiryDate.ShowCheckBox = True
        Me.dtpExpiryDate.Size = New System.Drawing.Size(91, 18)
        Me.dtpExpiryDate.TabIndex = 19
        Me.dtpExpiryDate.TabStop = False
        Me.dtpExpiryDate.Text = "13/06/2011"
        Me.dtpExpiryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(858, 325)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel5.TabIndex = 27
        Me.MyLabel5.Text = "Document Amount"
        '
        'lblTotRAmtCopy
        '
        Me.lblTotRAmtCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmtCopy.AutoSize = False
        Me.lblTotRAmtCopy.BorderVisible = True
        Me.lblTotRAmtCopy.FieldName = Nothing
        Me.lblTotRAmtCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmtCopy.Location = New System.Drawing.Point(960, 324)
        Me.lblTotRAmtCopy.Name = "lblTotRAmtCopy"
        Me.lblTotRAmtCopy.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmtCopy.TabIndex = 26
        Me.lblTotRAmtCopy.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(819, 378)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(261, 13)
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
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 321)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 25
        Me.UcItemBalance1.TabStop = False
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.txtReferencePO)
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Location = New System.Drawing.Point(3, 159)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(503, 23)
        Me.pnlPCJ.TabIndex = 23
        '
        'txtReferencePO
        '
        Me.txtReferencePO.CalculationExpression = Nothing
        Me.txtReferencePO.FieldCode = Nothing
        Me.txtReferencePO.FieldDesc = Nothing
        Me.txtReferencePO.FieldMaxLength = 0
        Me.txtReferencePO.FieldName = Nothing
        Me.txtReferencePO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReferencePO.isCalculatedField = False
        Me.txtReferencePO.IsSourceFromTable = False
        Me.txtReferencePO.IsSourceFromValueList = False
        Me.txtReferencePO.IsUnique = False
        Me.txtReferencePO.Location = New System.Drawing.Point(105, 3)
        Me.txtReferencePO.MaxLength = 50
        Me.txtReferencePO.MendatroryField = False
        Me.txtReferencePO.MyLinkLable1 = Me.RadLabel7
        Me.txtReferencePO.MyLinkLable2 = Nothing
        Me.txtReferencePO.Name = "txtReferencePO"
        Me.txtReferencePO.ReferenceFieldDesc = Nothing
        Me.txtReferencePO.ReferenceFieldName = Nothing
        Me.txtReferencePO.ReferenceTableName = Nothing
        Me.txtReferencePO.Size = New System.Drawing.Size(260, 18)
        Me.txtReferencePO.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtDeliveryDuration)
        Me.Panel1.Controls.Add(Me.lblDeliveryDuration)
        Me.Panel1.Location = New System.Drawing.Point(505, 116)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 18)
        Me.Panel1.TabIndex = 22
        '
        'txtDeliveryDuration
        '
        Me.txtDeliveryDuration.CalculationExpression = Nothing
        Me.txtDeliveryDuration.FieldCode = Nothing
        Me.txtDeliveryDuration.FieldDesc = Nothing
        Me.txtDeliveryDuration.FieldMaxLength = 0
        Me.txtDeliveryDuration.FieldName = Nothing
        Me.txtDeliveryDuration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryDuration.isCalculatedField = False
        Me.txtDeliveryDuration.IsSourceFromTable = False
        Me.txtDeliveryDuration.IsSourceFromValueList = False
        Me.txtDeliveryDuration.IsUnique = False
        Me.txtDeliveryDuration.Location = New System.Drawing.Point(105, 0)
        Me.txtDeliveryDuration.MaxLength = 50
        Me.txtDeliveryDuration.MendatroryField = False
        Me.txtDeliveryDuration.MyLinkLable1 = Me.RadLabel7
        Me.txtDeliveryDuration.MyLinkLable2 = Nothing
        Me.txtDeliveryDuration.Name = "txtDeliveryDuration"
        Me.txtDeliveryDuration.ReferenceFieldDesc = Nothing
        Me.txtDeliveryDuration.ReferenceFieldName = Nothing
        Me.txtDeliveryDuration.ReferenceTableName = Nothing
        Me.txtDeliveryDuration.Size = New System.Drawing.Size(176, 18)
        Me.txtDeliveryDuration.TabIndex = 0
        '
        'lblDeliveryDuration
        '
        Me.lblDeliveryDuration.FieldName = Nothing
        Me.lblDeliveryDuration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveryDuration.Location = New System.Drawing.Point(2, 1)
        Me.lblDeliveryDuration.Name = "lblDeliveryDuration"
        Me.lblDeliveryDuration.Size = New System.Drawing.Size(93, 16)
        Me.lblDeliveryDuration.TabIndex = 1
        Me.lblDeliveryDuration.Text = "Delivery Duration"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 117)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel2.TabIndex = 28
        Me.MyLabel2.Text = "Quotation No"
        '
        'txtQuotationNo
        '
        Me.txtQuotationNo.CalculationExpression = Nothing
        Me.txtQuotationNo.FieldCode = Nothing
        Me.txtQuotationNo.FieldDesc = Nothing
        Me.txtQuotationNo.FieldMaxLength = 0
        Me.txtQuotationNo.FieldName = Nothing
        Me.txtQuotationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuotationNo.isCalculatedField = False
        Me.txtQuotationNo.IsSourceFromTable = False
        Me.txtQuotationNo.IsSourceFromValueList = False
        Me.txtQuotationNo.IsUnique = False
        Me.txtQuotationNo.Location = New System.Drawing.Point(108, 116)
        Me.txtQuotationNo.MaxLength = 200
        Me.txtQuotationNo.MendatroryField = False
        Me.txtQuotationNo.MyLinkLable1 = Me.MyLabel2
        Me.txtQuotationNo.MyLinkLable2 = Nothing
        Me.txtQuotationNo.Name = "txtQuotationNo"
        Me.txtQuotationNo.ReferenceFieldDesc = Nothing
        Me.txtQuotationNo.ReferenceFieldName = Nothing
        Me.txtQuotationNo.ReferenceTableName = Nothing
        Me.txtQuotationNo.Size = New System.Drawing.Size(132, 18)
        Me.txtQuotationNo.TabIndex = 21
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(241, 117)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel1.TabIndex = 35
        Me.MyLabel1.Text = "Date"
        '
        'txtQuotationDate
        '
        Me.txtQuotationDate.CalculationExpression = Nothing
        Me.txtQuotationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtQuotationDate.FieldCode = Nothing
        Me.txtQuotationDate.FieldDesc = Nothing
        Me.txtQuotationDate.FieldMaxLength = 0
        Me.txtQuotationDate.FieldName = Nothing
        Me.txtQuotationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuotationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtQuotationDate.isCalculatedField = False
        Me.txtQuotationDate.IsSourceFromTable = False
        Me.txtQuotationDate.IsSourceFromValueList = False
        Me.txtQuotationDate.IsUnique = False
        Me.txtQuotationDate.Location = New System.Drawing.Point(271, 116)
        Me.txtQuotationDate.MendatroryField = False
        Me.txtQuotationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuotationDate.MyLinkLable1 = Me.MyLabel1
        Me.txtQuotationDate.MyLinkLable2 = Nothing
        Me.txtQuotationDate.Name = "txtQuotationDate"
        Me.txtQuotationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuotationDate.ReferenceFieldDesc = Nothing
        Me.txtQuotationDate.ReferenceFieldName = Nothing
        Me.txtQuotationDate.ReferenceTableName = Nothing
        Me.txtQuotationDate.Size = New System.Drawing.Size(75, 18)
        Me.txtQuotationDate.TabIndex = 22
        Me.txtQuotationDate.TabStop = False
        Me.txtQuotationDate.Text = "13/06/2011"
        Me.txtQuotationDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRGPNo
        '
        Me.txtRGPNo.CalculationExpression = Nothing
        Me.txtRGPNo.FieldCode = Nothing
        Me.txtRGPNo.FieldDesc = Nothing
        Me.txtRGPNo.FieldMaxLength = 0
        Me.txtRGPNo.FieldName = Nothing
        Me.txtRGPNo.isCalculatedField = False
        Me.txtRGPNo.IsSourceFromTable = False
        Me.txtRGPNo.IsSourceFromValueList = False
        Me.txtRGPNo.IsUnique = False
        Me.txtRGPNo.Location = New System.Drawing.Point(790, 78)
        Me.txtRGPNo.MendatroryField = False
        Me.txtRGPNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRGPNo.MyLinkLable1 = Nothing
        Me.txtRGPNo.MyLinkLable2 = Nothing
        Me.txtRGPNo.MyReadOnly = True
        Me.txtRGPNo.MyShowMasterFormButton = False
        Me.txtRGPNo.Name = "txtRGPNo"
        Me.txtRGPNo.ReferenceFieldDesc = Nothing
        Me.txtRGPNo.ReferenceFieldName = Nothing
        Me.txtRGPNo.ReferenceTableName = Nothing
        Me.txtRGPNo.Size = New System.Drawing.Size(162, 18)
        Me.txtRGPNo.TabIndex = 16
        Me.txtRGPNo.Value = ""
        Me.txtRGPNo.Visible = False
        '
        'lblAmbendmentNoCaption
        '
        Me.lblAmbendmentNoCaption.FieldName = Nothing
        Me.lblAmbendmentNoCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmbendmentNoCaption.Location = New System.Drawing.Point(899, 184)
        Me.lblAmbendmentNoCaption.Name = "lblAmbendmentNoCaption"
        Me.lblAmbendmentNoCaption.Size = New System.Drawing.Size(88, 16)
        Me.lblAmbendmentNoCaption.TabIndex = 18
        Me.lblAmbendmentNoCaption.Text = "Amendment No:"
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(505, 1)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel23.TabIndex = 45
        Me.RadLabel23.Text = "Indent No"
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(505, 79)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel21.TabIndex = 41
        Me.RadLabel21.Text = "Type"
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(241, 78)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(264, 18)
        Me.lblShipToLocation.TabIndex = 38
        Me.lblShipToLocation.TextWrap = False
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(241, 59)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(264, 18)
        Me.lblBillToLocation.TabIndex = 39
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(505, 41)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 43
        Me.RadLabel6.Text = "Remarks"
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(5, 98)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel20.TabIndex = 30
        Me.RadLabel20.Text = "Department"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(505, 21)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 44
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(5, 79)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel18.TabIndex = 31
        Me.RadLabel18.Text = "Ship To Location"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(5, 60)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(84, 16)
        Me.RadLabel15.TabIndex = 32
        Me.RadLabel15.Text = "Bill To Location"
        '
        'lblDeliveryDate
        '
        Me.lblDeliveryDate.FieldName = Nothing
        Me.lblDeliveryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveryDate.Location = New System.Drawing.Point(757, 60)
        Me.lblDeliveryDate.Name = "lblDeliveryDate"
        Me.lblDeliveryDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDeliveryDate.TabIndex = 13
        Me.lblDeliveryDate.Text = "Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(388, 1)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 48
        Me.RadLabel4.Text = "Date"
        '
        'chkAgainst_RGP
        '
        Me.chkAgainst_RGP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainst_RGP.Location = New System.Drawing.Point(726, 79)
        Me.chkAgainst_RGP.Name = "chkAgainst_RGP"
        Me.chkAgainst_RGP.Size = New System.Drawing.Size(62, 16)
        Me.chkAgainst_RGP.TabIndex = 16
        Me.chkAgainst_RGP.Text = "RGP No"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(730, 1)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 4
        Me.chkOnHold.Text = "On Hold"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(241, 40)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(264, 18)
        Me.lblVendorName.TabIndex = 40
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 41)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 33
        Me.RadLabel2.Text = "Vendor No"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(5, 1)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(40, 16)
        Me.RadLabel1.TabIndex = 34
        Me.RadLabel1.Text = "PO No"
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
        Me.cboItemType.Location = New System.Drawing.Point(576, 77)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel21
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(147, 20)
        Me.cboItemType.TabIndex = 15
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
        Me.txtRemarks.Location = New System.Drawing.Point(576, 40)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(291, 18)
        Me.txtRemarks.TabIndex = 9
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
        Me.txtReqNo.Location = New System.Drawing.Point(576, 0)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel23
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(148, 18)
        Me.txtReqNo.TabIndex = 3
        Me.txtReqNo.Value = ""
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
        Me.txtDept.Location = New System.Drawing.Point(108, 97)
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
        Me.txtDept.Size = New System.Drawing.Size(132, 18)
        Me.txtDept.TabIndex = 17
        Me.txtDept.Value = ""
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
        Me.txtVendorNo.Location = New System.Drawing.Point(108, 40)
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
        Me.txtVendorNo.Size = New System.Drawing.Size(132, 18)
        Me.txtVendorNo.TabIndex = 8
        Me.txtVendorNo.Value = ""
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
        Me.txtShipToLocation.Location = New System.Drawing.Point(108, 78)
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
        Me.txtShipToLocation.Size = New System.Drawing.Size(132, 18)
        Me.txtShipToLocation.TabIndex = 14
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
        Me.txtBillToLocation.Location = New System.Drawing.Point(108, 59)
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
        Me.txtBillToLocation.Size = New System.Drawing.Size(132, 18)
        Me.txtBillToLocation.TabIndex = 10
        Me.txtBillToLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(799, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(99, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 47
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(108, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(260, 18)
        Me.txtDocNo.TabIndex = 0
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
        Me.txtDeliveryDate.Location = New System.Drawing.Point(789, 59)
        Me.txtDeliveryDate.MendatroryField = False
        Me.txtDeliveryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.MyLinkLable1 = Me.lblDeliveryDate
        Me.txtDeliveryDate.MyLinkLable2 = Nothing
        Me.txtDeliveryDate.Name = "txtDeliveryDate"
        Me.txtDeliveryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.ReferenceFieldDesc = Nothing
        Me.txtDeliveryDate.ReferenceFieldName = Nothing
        Me.txtDeliveryDate.ReferenceTableName = Nothing
        Me.txtDeliveryDate.Size = New System.Drawing.Size(78, 18)
        Me.txtDeliveryDate.TabIndex = 12
        Me.txtDeliveryDate.TabStop = False
        Me.txtDeliveryDate.Text = "13/06/2011"
        Me.txtDeliveryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.Enabled = False
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(576, 59)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(179, 18)
        Me.txtRefNo.TabIndex = 11
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(422, 0)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtDesc.Location = New System.Drawing.Point(576, 20)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(291, 18)
        Me.txtDesc.TabIndex = 7
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(368, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.btnForm_Update)
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage5.Controls.Add(Me.chk_c_form)
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage5.Controls.Add(Me.Chkroadpermit)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(88.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage5.Text = "Form(s) Detail"
        '
        'btnForm_Update
        '
        Me.btnForm_Update.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnForm_Update.Location = New System.Drawing.Point(581, 173)
        Me.btnForm_Update.Name = "btnForm_Update"
        Me.btnForm_Update.Size = New System.Drawing.Size(69, 22)
        Me.btnForm_Update.TabIndex = 21
        Me.btnForm_Update.Text = "&Update"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv_c_form)
        Me.RadGroupBox4.HeaderText = "C-Forms Detail"
        Me.RadGroupBox4.Location = New System.Drawing.Point(147, 201)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(430, 187)
        Me.RadGroupBox4.TabIndex = 20
        Me.RadGroupBox4.Text = "C-Forms Detail"
        Me.RadGroupBox4.Visible = False
        '
        'gv_c_form
        '
        Me.gv_c_form.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_c_form.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv_c_form.MasterTemplate.AllowDragToGroup = False
        Me.gv_c_form.MasterTemplate.EnableFiltering = True
        Me.gv_c_form.MasterTemplate.EnableGrouping = False
        Me.gv_c_form.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_c_form.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_c_form.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv_c_form.Name = "gv_c_form"
        Me.gv_c_form.ShowGroupPanel = False
        Me.gv_c_form.ShowHeaderCellButtons = True
        Me.gv_c_form.Size = New System.Drawing.Size(426, 167)
        Me.gv_c_form.TabIndex = 0
        '
        'chk_c_form
        '
        Me.chk_c_form.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_c_form.Location = New System.Drawing.Point(15, 211)
        Me.chk_c_form.Name = "chk_c_form"
        Me.chk_c_form.Size = New System.Drawing.Size(99, 16)
        Me.chk_c_form.TabIndex = 19
        Me.chk_c_form.Text = "Against C-Form"
        Me.chk_c_form.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gv_roadpermit)
        Me.RadGroupBox3.HeaderText = "Forms Detail"
        Me.RadGroupBox3.Location = New System.Drawing.Point(147, 8)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(430, 187)
        Me.RadGroupBox3.TabIndex = 18
        Me.RadGroupBox3.Text = "Forms Detail"
        '
        'gv_roadpermit
        '
        Me.gv_roadpermit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_roadpermit.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv_roadpermit.MasterTemplate.AllowDragToGroup = False
        Me.gv_roadpermit.MasterTemplate.EnableFiltering = True
        Me.gv_roadpermit.MasterTemplate.EnableGrouping = False
        Me.gv_roadpermit.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_roadpermit.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_roadpermit.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv_roadpermit.Name = "gv_roadpermit"
        Me.gv_roadpermit.ShowGroupPanel = False
        Me.gv_roadpermit.ShowHeaderCellButtons = True
        Me.gv_roadpermit.Size = New System.Drawing.Size(426, 167)
        Me.gv_roadpermit.TabIndex = 0
        '
        'Chkroadpermit
        '
        Me.Chkroadpermit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chkroadpermit.Location = New System.Drawing.Point(15, 18)
        Me.Chkroadpermit.Name = "Chkroadpermit"
        Me.Chkroadpermit.Size = New System.Drawing.Size(124, 16)
        Me.Chkroadpermit.TabIndex = 17
        Me.Chkroadpermit.Text = "Against Road Permit"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.chkTDSApplied)
        Me.RadPageViewPage2.Controls.Add(Me.chkExciseOnQty)
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(82.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage2.Text = "Taxes/Terms"
        '
        'chkTDSApplied
        '
        Me.chkTDSApplied.Enabled = False
        Me.chkTDSApplied.Location = New System.Drawing.Point(835, 12)
        Me.chkTDSApplied.MyLinkLable1 = Nothing
        Me.chkTDSApplied.MyLinkLable2 = Nothing
        Me.chkTDSApplied.Name = "chkTDSApplied"
        Me.chkTDSApplied.Size = New System.Drawing.Size(81, 18)
        Me.chkTDSApplied.TabIndex = 25
        Me.chkTDSApplied.Tag1 = Nothing
        Me.chkTDSApplied.Text = "TCS Applied"
        '
        'chkExciseOnQty
        '
        Me.chkExciseOnQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExciseOnQty.Location = New System.Drawing.Point(714, 13)
        Me.chkExciseOnQty.Name = "chkExciseOnQty"
        Me.chkExciseOnQty.Size = New System.Drawing.Size(115, 16)
        Me.chkExciseOnQty.TabIndex = 2
        Me.chkExciseOnQty.Text = "Excise on Quantity"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(547, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 1
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
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 6
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(917, 232)
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
        Me.RadGroupBox1.Controls.Add(Me.MyLabel54)
        Me.RadGroupBox1.Controls.Add(Me.lblConfirmatory_PO_SRN_No)
        Me.RadGroupBox1.Controls.Add(Me.txtFreight)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel52)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel51)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel48)
        Me.RadGroupBox1.Controls.Add(Me.txtPackingForward)
        Me.RadGroupBox1.Controls.Add(Me.txtInsurance)
        Me.RadGroupBox1.Controls.Add(Me.txtDeliveryDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtDelivery_Code)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtInsuranceTerms)
        Me.RadGroupBox1.Controls.Add(Me.txtPaymentTerm)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.txtTermRemark)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtTermCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtDueDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox1.Controls.Add(Me.lblTermName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Terms"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 247)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1079, 144)
        Me.RadGroupBox1.TabIndex = 3
        Me.RadGroupBox1.Text = "Terms"
        '
        'MyLabel54
        '
        Me.MyLabel54.FieldName = Nothing
        Me.MyLabel54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel54.Location = New System.Drawing.Point(177, 45)
        Me.MyLabel54.Name = "MyLabel54"
        Me.MyLabel54.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel54.TabIndex = 35
        Me.MyLabel54.Text = "Confirmatory SRN No"
        '
        'lblConfirmatory_PO_SRN_No
        '
        Me.lblConfirmatory_PO_SRN_No.AutoSize = False
        Me.lblConfirmatory_PO_SRN_No.BorderVisible = True
        Me.lblConfirmatory_PO_SRN_No.FieldName = Nothing
        Me.lblConfirmatory_PO_SRN_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmatory_PO_SRN_No.Location = New System.Drawing.Point(299, 44)
        Me.lblConfirmatory_PO_SRN_No.Name = "lblConfirmatory_PO_SRN_No"
        Me.lblConfirmatory_PO_SRN_No.Size = New System.Drawing.Size(183, 19)
        Me.lblConfirmatory_PO_SRN_No.TabIndex = 36
        Me.lblConfirmatory_PO_SRN_No.TextWrap = False
        '
        'txtFreight
        '
        Me.txtFreight.AutoSize = False
        Me.txtFreight.CalculationExpression = Nothing
        Me.txtFreight.FieldCode = Nothing
        Me.txtFreight.FieldDesc = Nothing
        Me.txtFreight.FieldMaxLength = 0
        Me.txtFreight.FieldName = Nothing
        Me.txtFreight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreight.isCalculatedField = False
        Me.txtFreight.IsSourceFromTable = False
        Me.txtFreight.IsSourceFromValueList = False
        Me.txtFreight.IsUnique = False
        Me.txtFreight.Location = New System.Drawing.Point(596, 102)
        Me.txtFreight.MaxLength = 500
        Me.txtFreight.MendatroryField = False
        Me.txtFreight.Multiline = True
        Me.txtFreight.MyLinkLable1 = Me.MyLabel15
        Me.txtFreight.MyLinkLable2 = Nothing
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.ReferenceFieldDesc = Nothing
        Me.txtFreight.ReferenceFieldName = Nothing
        Me.txtFreight.ReferenceTableName = Nothing
        Me.txtFreight.Size = New System.Drawing.Size(415, 37)
        Me.txtFreight.TabIndex = 10
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(484, 63)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel15.TabIndex = 5
        Me.MyLabel15.Text = "Insurance Terms"
        '
        'MyLabel52
        '
        Me.MyLabel52.FieldName = Nothing
        Me.MyLabel52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel52.Location = New System.Drawing.Point(489, 106)
        Me.MyLabel52.Name = "MyLabel52"
        Me.MyLabel52.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel52.TabIndex = 34
        Me.MyLabel52.Text = "Freight"
        '
        'MyLabel51
        '
        Me.MyLabel51.AutoSize = False
        Me.MyLabel51.FieldName = Nothing
        Me.MyLabel51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel51.Location = New System.Drawing.Point(239, 112)
        Me.MyLabel51.Name = "MyLabel51"
        Me.MyLabel51.Size = New System.Drawing.Size(91, 29)
        Me.MyLabel51.TabIndex = 32
        Me.MyLabel51.Text = "Packing Forward"
        '
        'MyLabel48
        '
        Me.MyLabel48.AutoSize = False
        Me.MyLabel48.FieldName = Nothing
        Me.MyLabel48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel48.Location = New System.Drawing.Point(6, 115)
        Me.MyLabel48.Name = "MyLabel48"
        Me.MyLabel48.Size = New System.Drawing.Size(80, 26)
        Me.MyLabel48.TabIndex = 30
        Me.MyLabel48.Text = "Insurance"
        '
        'txtPackingForward
        '
        Me.txtPackingForward.CalculationExpression = Nothing
        Me.txtPackingForward.FieldCode = Nothing
        Me.txtPackingForward.FieldDesc = Nothing
        Me.txtPackingForward.FieldMaxLength = 0
        Me.txtPackingForward.FieldName = Nothing
        Me.txtPackingForward.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackingForward.isCalculatedField = False
        Me.txtPackingForward.IsSourceFromTable = False
        Me.txtPackingForward.IsSourceFromValueList = False
        Me.txtPackingForward.IsUnique = False
        Me.txtPackingForward.Location = New System.Drawing.Point(339, 115)
        Me.txtPackingForward.MaxLength = 200
        Me.txtPackingForward.MendatroryField = False
        Me.txtPackingForward.MyLinkLable1 = Me.MyLabel51
        Me.txtPackingForward.MyLinkLable2 = Nothing
        Me.txtPackingForward.Name = "txtPackingForward"
        Me.txtPackingForward.ReferenceFieldDesc = Nothing
        Me.txtPackingForward.ReferenceFieldName = Nothing
        Me.txtPackingForward.ReferenceTableName = Nothing
        Me.txtPackingForward.Size = New System.Drawing.Size(143, 18)
        Me.txtPackingForward.TabIndex = 31
        '
        'txtInsurance
        '
        Me.txtInsurance.CalculationExpression = Nothing
        Me.txtInsurance.FieldCode = Nothing
        Me.txtInsurance.FieldDesc = Nothing
        Me.txtInsurance.FieldMaxLength = 0
        Me.txtInsurance.FieldName = Nothing
        Me.txtInsurance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance.isCalculatedField = False
        Me.txtInsurance.IsSourceFromTable = False
        Me.txtInsurance.IsSourceFromValueList = False
        Me.txtInsurance.IsUnique = False
        Me.txtInsurance.Location = New System.Drawing.Point(92, 114)
        Me.txtInsurance.MaxLength = 200
        Me.txtInsurance.MendatroryField = False
        Me.txtInsurance.MyLinkLable1 = Me.MyLabel48
        Me.txtInsurance.MyLinkLable2 = Nothing
        Me.txtInsurance.Name = "txtInsurance"
        Me.txtInsurance.ReferenceFieldDesc = Nothing
        Me.txtInsurance.ReferenceFieldName = Nothing
        Me.txtInsurance.ReferenceTableName = Nothing
        Me.txtInsurance.Size = New System.Drawing.Size(143, 18)
        Me.txtInsurance.TabIndex = 29
        '
        'txtDeliveryDesc
        '
        Me.txtDeliveryDesc.AutoSize = False
        Me.txtDeliveryDesc.BorderVisible = True
        Me.txtDeliveryDesc.FieldName = Nothing
        Me.txtDeliveryDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryDesc.Location = New System.Drawing.Point(234, 86)
        Me.txtDeliveryDesc.Name = "txtDeliveryDesc"
        Me.txtDeliveryDesc.Size = New System.Drawing.Size(248, 20)
        Me.txtDeliveryDesc.TabIndex = 12
        Me.txtDeliveryDesc.TextWrap = False
        '
        'txtDelivery_Code
        '
        Me.txtDelivery_Code.CalculationExpression = Nothing
        Me.txtDelivery_Code.FieldCode = Nothing
        Me.txtDelivery_Code.FieldDesc = Nothing
        Me.txtDelivery_Code.FieldMaxLength = 0
        Me.txtDelivery_Code.FieldName = Nothing
        Me.txtDelivery_Code.isCalculatedField = False
        Me.txtDelivery_Code.IsSourceFromTable = False
        Me.txtDelivery_Code.IsSourceFromValueList = False
        Me.txtDelivery_Code.IsUnique = False
        Me.txtDelivery_Code.Location = New System.Drawing.Point(92, 86)
        Me.txtDelivery_Code.MendatroryField = False
        Me.txtDelivery_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelivery_Code.MyLinkLable1 = Me.MyLabel16
        Me.txtDelivery_Code.MyLinkLable2 = Me.txtDeliveryDesc
        Me.txtDelivery_Code.MyReadOnly = False
        Me.txtDelivery_Code.MyShowMasterFormButton = False
        Me.txtDelivery_Code.Name = "txtDelivery_Code"
        Me.txtDelivery_Code.ReferenceFieldDesc = Nothing
        Me.txtDelivery_Code.ReferenceFieldName = Nothing
        Me.txtDelivery_Code.ReferenceTableName = Nothing
        Me.txtDelivery_Code.Size = New System.Drawing.Size(143, 20)
        Me.txtDelivery_Code.TabIndex = 10
        Me.txtDelivery_Code.Value = ""
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(6, 87)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel16.TabIndex = 11
        Me.MyLabel16.Text = "Delivery Terms"
        '
        'txtInsuranceTerms
        '
        Me.txtInsuranceTerms.AutoSize = False
        Me.txtInsuranceTerms.CalculationExpression = Nothing
        Me.txtInsuranceTerms.FieldCode = Nothing
        Me.txtInsuranceTerms.FieldDesc = Nothing
        Me.txtInsuranceTerms.FieldMaxLength = 0
        Me.txtInsuranceTerms.FieldName = Nothing
        Me.txtInsuranceTerms.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsuranceTerms.isCalculatedField = False
        Me.txtInsuranceTerms.IsSourceFromTable = False
        Me.txtInsuranceTerms.IsSourceFromValueList = False
        Me.txtInsuranceTerms.IsUnique = False
        Me.txtInsuranceTerms.Location = New System.Drawing.Point(596, 61)
        Me.txtInsuranceTerms.MaxLength = 500
        Me.txtInsuranceTerms.MendatroryField = False
        Me.txtInsuranceTerms.Multiline = True
        Me.txtInsuranceTerms.MyLinkLable1 = Me.MyLabel15
        Me.txtInsuranceTerms.MyLinkLable2 = Nothing
        Me.txtInsuranceTerms.Name = "txtInsuranceTerms"
        Me.txtInsuranceTerms.ReferenceFieldDesc = Nothing
        Me.txtInsuranceTerms.ReferenceFieldName = Nothing
        Me.txtInsuranceTerms.ReferenceTableName = Nothing
        Me.txtInsuranceTerms.Size = New System.Drawing.Size(415, 37)
        Me.txtInsuranceTerms.TabIndex = 9
        '
        'txtPaymentTerm
        '
        Me.txtPaymentTerm.AutoSize = False
        Me.txtPaymentTerm.CalculationExpression = Nothing
        Me.txtPaymentTerm.FieldCode = Nothing
        Me.txtPaymentTerm.FieldDesc = Nothing
        Me.txtPaymentTerm.FieldMaxLength = 0
        Me.txtPaymentTerm.FieldName = Nothing
        Me.txtPaymentTerm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentTerm.isCalculatedField = False
        Me.txtPaymentTerm.IsSourceFromTable = False
        Me.txtPaymentTerm.IsSourceFromValueList = False
        Me.txtPaymentTerm.IsUnique = False
        Me.txtPaymentTerm.Location = New System.Drawing.Point(596, 22)
        Me.txtPaymentTerm.MaxLength = 500
        Me.txtPaymentTerm.MendatroryField = False
        Me.txtPaymentTerm.Multiline = True
        Me.txtPaymentTerm.MyLinkLable1 = Me.MyLabel14
        Me.txtPaymentTerm.MyLinkLable2 = Nothing
        Me.txtPaymentTerm.Name = "txtPaymentTerm"
        Me.txtPaymentTerm.ReferenceFieldDesc = Nothing
        Me.txtPaymentTerm.ReferenceFieldName = Nothing
        Me.txtPaymentTerm.ReferenceTableName = Nothing
        Me.txtPaymentTerm.Size = New System.Drawing.Size(415, 35)
        Me.txtPaymentTerm.TabIndex = 8
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(484, 22)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel14.TabIndex = 7
        Me.MyLabel14.Text = "Payment Terms"
        '
        'txtTermRemark
        '
        Me.txtTermRemark.CalculationExpression = Nothing
        Me.txtTermRemark.FieldCode = Nothing
        Me.txtTermRemark.FieldDesc = Nothing
        Me.txtTermRemark.FieldMaxLength = 0
        Me.txtTermRemark.FieldName = Nothing
        Me.txtTermRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermRemark.isCalculatedField = False
        Me.txtTermRemark.IsSourceFromTable = False
        Me.txtTermRemark.IsSourceFromValueList = False
        Me.txtTermRemark.IsUnique = False
        Me.txtTermRemark.Location = New System.Drawing.Point(92, 64)
        Me.txtTermRemark.MaxLength = 200
        Me.txtTermRemark.MendatroryField = False
        Me.txtTermRemark.MyLinkLable1 = Me.MyLabel3
        Me.txtTermRemark.MyLinkLable2 = Nothing
        Me.txtTermRemark.Name = "txtTermRemark"
        Me.txtTermRemark.ReferenceFieldDesc = Nothing
        Me.txtTermRemark.ReferenceFieldName = Nothing
        Me.txtTermRemark.ReferenceTableName = Nothing
        Me.txtTermRemark.Size = New System.Drawing.Size(390, 18)
        Me.txtTermRemark.TabIndex = 1
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 66)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 4
        Me.MyLabel3.Text = "Remark"
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
        Me.txtTermCode.Location = New System.Drawing.Point(92, 22)
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
        Me.RadLabel16.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel16.TabIndex = 5
        Me.RadLabel16.Text = "Credit Days"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(234, 21)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(248, 20)
        Me.lblTermName.TabIndex = 6
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
        Me.txtDueDate.Location = New System.Drawing.Point(92, 43)
        Me.txtDueDate.MendatroryField = False
        Me.txtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.MyLinkLable1 = Me.RadLabel17
        Me.txtDueDate.MyLinkLable2 = Nothing
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.ReferenceFieldDesc = Nothing
        Me.txtDueDate.ReferenceFieldName = Nothing
        Me.txtDueDate.ReferenceTableName = Nothing
        Me.txtDueDate.Size = New System.Drawing.Size(82, 18)
        Me.txtDueDate.TabIndex = 2
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(6, 45)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 3
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
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1075, 190)
        Me.gv2.TabIndex = 4
        Me.gv2.TabStop = False
        '
        'RdPaymentterms
        '
        Me.RdPaymentterms.Controls.Add(Me.lblBankDesc)
        Me.RdPaymentterms.Controls.Add(Me.txtPaymentMode)
        Me.RdPaymentterms.Controls.Add(Me.txtBankCode)
        Me.RdPaymentterms.Controls.Add(Me.lblpaymentcode)
        Me.RdPaymentterms.Controls.Add(Me.lblbankcode)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel33)
        Me.RdPaymentterms.Controls.Add(Me.TxtBuyerPODate)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel23)
        Me.RdPaymentterms.Controls.Add(Me.TxtBuyerPONo)
        Me.RdPaymentterms.Controls.Add(Me.ChkPartPayment)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel26)
        Me.RdPaymentterms.Controls.Add(Me.cmbAdvanceType)
        Me.RdPaymentterms.Controls.Add(Me.TxtCIF)
        Me.RdPaymentterms.Controls.Add(Me.lblCIF)
        Me.RdPaymentterms.Controls.Add(Me.chkTransshipment)
        Me.RdPaymentterms.Controls.Add(Me.chkPartshipment)
        Me.RdPaymentterms.Controls.Add(Me.dtpAcceptance)
        Me.RdPaymentterms.Controls.Add(Me.chkAcceptance)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel21)
        Me.RdPaymentterms.Controls.Add(Me.txtPre_Carriage_By)
        Me.RdPaymentterms.Controls.Add(Me.txtAdvance_Pers)
        Me.RdPaymentterms.Controls.Add(Me.cmbTerms_Payment)
        Me.RdPaymentterms.Controls.Add(Me.cmbTerms)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel19)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel20)
        Me.RdPaymentterms.Controls.Add(Me.txtPIDueDate)
        Me.RdPaymentterms.Controls.Add(Me.FndCreditTerms)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel24)
        Me.RdPaymentterms.Controls.Add(Me.fndCountry_Final_Destination)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel25)
        Me.RdPaymentterms.Controls.Add(Me.TxtCreditTermsName)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel27)
        Me.RdPaymentterms.Controls.Add(Me.txtFinal_Destination)
        Me.RdPaymentterms.Controls.Add(Me.fndCountry_Origin)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel18)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel28)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel30)
        Me.RdPaymentterms.Controls.Add(Me.txtPort_Discharge)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel31)
        Me.RdPaymentterms.Controls.Add(Me.cboStuffing)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel32)
        Me.RdPaymentterms.Controls.Add(Me.txtCarrier)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel17)
        Me.RdPaymentterms.Controls.Add(Me.TxtHSClassificationNo)
        Me.RdPaymentterms.Controls.Add(Me.txtPINo)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel10)
        Me.RdPaymentterms.Controls.Add(Me.RadGroupBox6)
        Me.RdPaymentterms.Controls.Add(Me.lblAdvance)
        Me.RdPaymentterms.Controls.Add(Me.TxtOnAccount)
        Me.RdPaymentterms.Controls.Add(Me.lblonAccount)
        Me.RdPaymentterms.Controls.Add(Me.txtRetained)
        Me.RdPaymentterms.Controls.Add(Me.lblretained)
        Me.RdPaymentterms.Controls.Add(Me.TxtBalancePayment)
        Me.RdPaymentterms.Controls.Add(Me.lblBalancePayment)
        Me.RdPaymentterms.Controls.Add(Me.TxtLC)
        Me.RdPaymentterms.Controls.Add(Me.lblLC)
        Me.RdPaymentterms.Controls.Add(Me.TxtCAD)
        Me.RdPaymentterms.Controls.Add(Me.lblCad)
        Me.RdPaymentterms.Controls.Add(Me.txtAdvance)
        Me.RdPaymentterms.Controls.Add(Me.lblpaymenttermsgroup)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel12)
        Me.RdPaymentterms.Controls.Add(Me.fndPaymenttermsGroup)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel11)
        Me.RdPaymentterms.Controls.Add(Me.lblBeneficiary)
        Me.RdPaymentterms.Controls.Add(Me.TxtBeneficiary)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel9)
        Me.RdPaymentterms.Controls.Add(Me.TxtINCOTERMS)
        Me.RdPaymentterms.Controls.Add(Me.RadGroupBox5)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel8)
        Me.RdPaymentterms.ItemSize = New System.Drawing.SizeF(96.0!, 26.0!)
        Me.RdPaymentterms.Location = New System.Drawing.Point(10, 35)
        Me.RdPaymentterms.Name = "RdPaymentterms"
        Me.RdPaymentterms.Size = New System.Drawing.Size(1080, 391)
        Me.RdPaymentterms.Text = "Payment Terms"
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.FieldName = Nothing
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(283, 359)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(238, 19)
        Me.lblBankDesc.TabIndex = 1486
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.CalculationExpression = Nothing
        Me.txtPaymentMode.FieldCode = Nothing
        Me.txtPaymentMode.FieldDesc = Nothing
        Me.txtPaymentMode.FieldMaxLength = 0
        Me.txtPaymentMode.FieldName = Nothing
        Me.txtPaymentMode.isCalculatedField = False
        Me.txtPaymentMode.IsSourceFromTable = False
        Me.txtPaymentMode.IsSourceFromValueList = False
        Me.txtPaymentMode.IsUnique = False
        Me.txtPaymentMode.Location = New System.Drawing.Point(138, 383)
        Me.txtPaymentMode.MendatroryField = False
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(143, 19)
        Me.txtPaymentMode.TabIndex = 1488
        Me.txtPaymentMode.Value = ""
        '
        'txtBankCode
        '
        Me.txtBankCode.CalculationExpression = Nothing
        Me.txtBankCode.FieldCode = Nothing
        Me.txtBankCode.FieldDesc = Nothing
        Me.txtBankCode.FieldMaxLength = 0
        Me.txtBankCode.FieldName = Nothing
        Me.txtBankCode.isCalculatedField = False
        Me.txtBankCode.IsSourceFromTable = False
        Me.txtBankCode.IsSourceFromValueList = False
        Me.txtBankCode.IsUnique = False
        Me.txtBankCode.Location = New System.Drawing.Point(138, 358)
        Me.txtBankCode.MendatroryField = False
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(143, 20)
        Me.txtBankCode.TabIndex = 1485
        Me.txtBankCode.Value = ""
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(15, 381)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 1487
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(15, 359)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 1484
        Me.lblbankcode.Text = "Bank Code"
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(529, 292)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel33.TabIndex = 1482
        Me.MyLabel33.Text = "Buyer PO Date"
        '
        'TxtBuyerPODate
        '
        Me.TxtBuyerPODate.CalculationExpression = Nothing
        Me.TxtBuyerPODate.CustomFormat = "dd/MM/yyyy"
        Me.TxtBuyerPODate.FieldCode = Nothing
        Me.TxtBuyerPODate.FieldDesc = Nothing
        Me.TxtBuyerPODate.FieldMaxLength = 0
        Me.TxtBuyerPODate.FieldName = Nothing
        Me.TxtBuyerPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtBuyerPODate.isCalculatedField = False
        Me.TxtBuyerPODate.IsSourceFromTable = False
        Me.TxtBuyerPODate.IsSourceFromValueList = False
        Me.TxtBuyerPODate.IsUnique = False
        Me.TxtBuyerPODate.Location = New System.Drawing.Point(630, 288)
        Me.TxtBuyerPODate.MendatroryField = False
        Me.TxtBuyerPODate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.TxtBuyerPODate.MyLinkLable1 = Nothing
        Me.TxtBuyerPODate.MyLinkLable2 = Nothing
        Me.TxtBuyerPODate.Name = "TxtBuyerPODate"
        Me.TxtBuyerPODate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.TxtBuyerPODate.ReferenceFieldDesc = Nothing
        Me.TxtBuyerPODate.ReferenceFieldName = Nothing
        Me.TxtBuyerPODate.ReferenceTableName = Nothing
        Me.TxtBuyerPODate.ShowCheckBox = True
        Me.TxtBuyerPODate.Size = New System.Drawing.Size(112, 20)
        Me.TxtBuyerPODate.TabIndex = 1483
        Me.TxtBuyerPODate.TabStop = False
        Me.TxtBuyerPODate.Text = "18/06/2014"
        Me.TxtBuyerPODate.Value = New Date(2014, 6, 18, 12, 42, 51, 794)
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(529, 266)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel23.TabIndex = 1481
        Me.MyLabel23.Text = "Buyer PO No."
        '
        'TxtBuyerPONo
        '
        Me.TxtBuyerPONo.CalculationExpression = Nothing
        Me.TxtBuyerPONo.FieldCode = Nothing
        Me.TxtBuyerPONo.FieldDesc = Nothing
        Me.TxtBuyerPONo.FieldMaxLength = 0
        Me.TxtBuyerPONo.FieldName = Nothing
        Me.TxtBuyerPONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBuyerPONo.isCalculatedField = False
        Me.TxtBuyerPONo.IsSourceFromTable = False
        Me.TxtBuyerPONo.IsSourceFromValueList = False
        Me.TxtBuyerPONo.IsUnique = False
        Me.TxtBuyerPONo.Location = New System.Drawing.Point(630, 266)
        Me.TxtBuyerPONo.MaxLength = 30
        Me.TxtBuyerPONo.MendatroryField = False
        Me.TxtBuyerPONo.MyLinkLable1 = Me.MyLabel23
        Me.TxtBuyerPONo.MyLinkLable2 = Nothing
        Me.TxtBuyerPONo.Name = "TxtBuyerPONo"
        Me.TxtBuyerPONo.ReferenceFieldDesc = Nothing
        Me.TxtBuyerPONo.ReferenceFieldName = Nothing
        Me.TxtBuyerPONo.ReferenceTableName = Nothing
        Me.TxtBuyerPONo.Size = New System.Drawing.Size(175, 18)
        Me.TxtBuyerPONo.TabIndex = 1480
        '
        'ChkPartPayment
        '
        Me.ChkPartPayment.Location = New System.Drawing.Point(878, 195)
        Me.ChkPartPayment.MyLinkLable1 = Nothing
        Me.ChkPartPayment.MyLinkLable2 = Nothing
        Me.ChkPartPayment.Name = "ChkPartPayment"
        Me.ChkPartPayment.Size = New System.Drawing.Size(98, 18)
        Me.ChkPartPayment.TabIndex = 1471
        Me.ChkPartPayment.Tag1 = Nothing
        Me.ChkPartPayment.Text = "Is Part Payment"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(531, 196)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel26.TabIndex = 1470
        Me.MyLabel26.Text = "Advance Type"
        '
        'cmbAdvanceType
        '
        Me.cmbAdvanceType.AutoCompleteDisplayMember = Nothing
        Me.cmbAdvanceType.AutoCompleteValueMember = Nothing
        Me.cmbAdvanceType.CalculationExpression = Nothing
        Me.cmbAdvanceType.DropDownAnimationEnabled = True
        Me.cmbAdvanceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbAdvanceType.FieldCode = Nothing
        Me.cmbAdvanceType.FieldDesc = Nothing
        Me.cmbAdvanceType.FieldMaxLength = 0
        Me.cmbAdvanceType.FieldName = Nothing
        Me.cmbAdvanceType.isCalculatedField = False
        Me.cmbAdvanceType.IsSourceFromTable = False
        Me.cmbAdvanceType.IsSourceFromValueList = False
        Me.cmbAdvanceType.IsUnique = False
        Me.cmbAdvanceType.Location = New System.Drawing.Point(630, 194)
        Me.cmbAdvanceType.MendatroryField = False
        Me.cmbAdvanceType.MyLinkLable1 = Me.MyLabel26
        Me.cmbAdvanceType.MyLinkLable2 = Nothing
        Me.cmbAdvanceType.Name = "cmbAdvanceType"
        Me.cmbAdvanceType.ReferenceFieldDesc = Nothing
        Me.cmbAdvanceType.ReferenceFieldName = Nothing
        Me.cmbAdvanceType.ReferenceTableName = Nothing
        Me.cmbAdvanceType.Size = New System.Drawing.Size(127, 20)
        Me.cmbAdvanceType.TabIndex = 1469
        '
        'TxtCIF
        '
        Me.TxtCIF.BackColor = System.Drawing.Color.White
        Me.TxtCIF.CalculationExpression = Nothing
        Me.TxtCIF.DecimalPlaces = 2
        Me.TxtCIF.FieldCode = Nothing
        Me.TxtCIF.FieldDesc = Nothing
        Me.TxtCIF.FieldMaxLength = 0
        Me.TxtCIF.FieldName = Nothing
        Me.TxtCIF.isCalculatedField = False
        Me.TxtCIF.IsSourceFromTable = False
        Me.TxtCIF.IsSourceFromValueList = False
        Me.TxtCIF.IsUnique = False
        Me.TxtCIF.Location = New System.Drawing.Point(138, 333)
        Me.TxtCIF.MendatroryField = False
        Me.TxtCIF.MyLinkLable1 = Nothing
        Me.TxtCIF.MyLinkLable2 = Nothing
        Me.TxtCIF.Name = "TxtCIF"
        Me.TxtCIF.ReferenceFieldDesc = Nothing
        Me.TxtCIF.ReferenceFieldName = Nothing
        Me.TxtCIF.ReferenceTableName = Nothing
        Me.TxtCIF.Size = New System.Drawing.Size(143, 20)
        Me.TxtCIF.TabIndex = 1467
        Me.TxtCIF.Text = "0"
        Me.TxtCIF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCIF.Value = 0R
        '
        'lblCIF
        '
        Me.lblCIF.FieldName = Nothing
        Me.lblCIF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCIF.Location = New System.Drawing.Point(16, 337)
        Me.lblCIF.Name = "lblCIF"
        Me.lblCIF.Size = New System.Drawing.Size(24, 16)
        Me.lblCIF.TabIndex = 1468
        Me.lblCIF.Text = "CIF"
        '
        'chkTransshipment
        '
        Me.chkTransshipment.Location = New System.Drawing.Point(637, 239)
        Me.chkTransshipment.MyLinkLable1 = Nothing
        Me.chkTransshipment.MyLinkLable2 = Nothing
        Me.chkTransshipment.Name = "chkTransshipment"
        Me.chkTransshipment.Size = New System.Drawing.Size(105, 18)
        Me.chkTransshipment.TabIndex = 1466
        Me.chkTransshipment.Tag1 = Nothing
        Me.chkTransshipment.Text = "Is Transshipment"
        '
        'chkPartshipment
        '
        Me.chkPartshipment.Location = New System.Drawing.Point(529, 239)
        Me.chkPartshipment.MyLinkLable1 = Nothing
        Me.chkPartshipment.MyLinkLable2 = Nothing
        Me.chkPartshipment.Name = "chkPartshipment"
        Me.chkPartshipment.Size = New System.Drawing.Size(102, 18)
        Me.chkPartshipment.TabIndex = 1465
        Me.chkPartshipment.Tag1 = Nothing
        Me.chkPartshipment.Text = "Is Part Shipment"
        '
        'dtpAcceptance
        '
        Me.dtpAcceptance.CalculationExpression = Nothing
        Me.dtpAcceptance.CustomFormat = "dd/MM/yyyy"
        Me.dtpAcceptance.FieldCode = Nothing
        Me.dtpAcceptance.FieldDesc = Nothing
        Me.dtpAcceptance.FieldMaxLength = 0
        Me.dtpAcceptance.FieldName = Nothing
        Me.dtpAcceptance.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAcceptance.isCalculatedField = False
        Me.dtpAcceptance.IsSourceFromTable = False
        Me.dtpAcceptance.IsSourceFromValueList = False
        Me.dtpAcceptance.IsUnique = False
        Me.dtpAcceptance.Location = New System.Drawing.Point(698, 219)
        Me.dtpAcceptance.MendatroryField = False
        Me.dtpAcceptance.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpAcceptance.MyLinkLable1 = Nothing
        Me.dtpAcceptance.MyLinkLable2 = Nothing
        Me.dtpAcceptance.Name = "dtpAcceptance"
        Me.dtpAcceptance.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpAcceptance.ReferenceFieldDesc = Nothing
        Me.dtpAcceptance.ReferenceFieldName = Nothing
        Me.dtpAcceptance.ReferenceTableName = Nothing
        Me.dtpAcceptance.Size = New System.Drawing.Size(80, 20)
        Me.dtpAcceptance.TabIndex = 1464
        Me.dtpAcceptance.TabStop = False
        Me.dtpAcceptance.Text = "18/06/2014"
        Me.dtpAcceptance.Value = New Date(2014, 6, 18, 12, 42, 51, 794)
        '
        'chkAcceptance
        '
        Me.chkAcceptance.Location = New System.Drawing.Point(529, 219)
        Me.chkAcceptance.MyLinkLable1 = Nothing
        Me.chkAcceptance.MyLinkLable2 = Nothing
        Me.chkAcceptance.Name = "chkAcceptance"
        Me.chkAcceptance.Size = New System.Drawing.Size(167, 18)
        Me.chkAcceptance.TabIndex = 1463
        Me.chkAcceptance.Tag1 = Nothing
        Me.chkAcceptance.Text = "PI Accepted Acceptance Date"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(529, 170)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel21.TabIndex = 1447
        Me.MyLabel21.Text = "Terms of Payment"
        '
        'txtPre_Carriage_By
        '
        Me.txtPre_Carriage_By.AutoCompleteDisplayMember = Nothing
        Me.txtPre_Carriage_By.AutoCompleteValueMember = Nothing
        Me.txtPre_Carriage_By.CalculationExpression = Nothing
        Me.txtPre_Carriage_By.DropDownAnimationEnabled = True
        Me.txtPre_Carriage_By.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtPre_Carriage_By.FieldCode = Nothing
        Me.txtPre_Carriage_By.FieldDesc = Nothing
        Me.txtPre_Carriage_By.FieldMaxLength = 0
        Me.txtPre_Carriage_By.FieldName = Nothing
        Me.txtPre_Carriage_By.isCalculatedField = False
        Me.txtPre_Carriage_By.IsSourceFromTable = False
        Me.txtPre_Carriage_By.IsSourceFromValueList = False
        Me.txtPre_Carriage_By.IsUnique = False
        Me.txtPre_Carriage_By.Location = New System.Drawing.Point(630, 12)
        Me.txtPre_Carriage_By.MendatroryField = False
        Me.txtPre_Carriage_By.MyLinkLable1 = Me.MyLabel18
        Me.txtPre_Carriage_By.MyLinkLable2 = Nothing
        Me.txtPre_Carriage_By.Name = "txtPre_Carriage_By"
        Me.txtPre_Carriage_By.ReferenceFieldDesc = Nothing
        Me.txtPre_Carriage_By.ReferenceFieldName = Nothing
        Me.txtPre_Carriage_By.ReferenceTableName = Nothing
        Me.txtPre_Carriage_By.Size = New System.Drawing.Size(138, 20)
        Me.txtPre_Carriage_By.TabIndex = 1448
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(529, 14)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel18.TabIndex = 1441
        Me.MyLabel18.Text = "Pre-Carriage By"
        '
        'txtAdvance_Pers
        '
        Me.txtAdvance_Pers.CalculationExpression = Nothing
        Me.txtAdvance_Pers.DecimalPlaces = 0
        Me.txtAdvance_Pers.Enabled = False
        Me.txtAdvance_Pers.FieldCode = Nothing
        Me.txtAdvance_Pers.FieldDesc = Nothing
        Me.txtAdvance_Pers.FieldMaxLength = 0
        Me.txtAdvance_Pers.FieldName = Nothing
        Me.txtAdvance_Pers.isCalculatedField = False
        Me.txtAdvance_Pers.IsSourceFromTable = False
        Me.txtAdvance_Pers.IsSourceFromValueList = False
        Me.txtAdvance_Pers.IsUnique = False
        Me.txtAdvance_Pers.Location = New System.Drawing.Point(762, 194)
        Me.txtAdvance_Pers.MaxLength = 3
        Me.txtAdvance_Pers.MendatroryField = False
        Me.txtAdvance_Pers.MyLinkLable1 = Nothing
        Me.txtAdvance_Pers.MyLinkLable2 = Nothing
        Me.txtAdvance_Pers.Name = "txtAdvance_Pers"
        Me.txtAdvance_Pers.ReferenceFieldDesc = Nothing
        Me.txtAdvance_Pers.ReferenceFieldName = Nothing
        Me.txtAdvance_Pers.ReferenceTableName = Nothing
        Me.txtAdvance_Pers.Size = New System.Drawing.Size(110, 20)
        Me.txtAdvance_Pers.TabIndex = 1460
        Me.txtAdvance_Pers.Text = "0"
        Me.txtAdvance_Pers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance_Pers.Value = 0R
        '
        'cmbTerms_Payment
        '
        Me.cmbTerms_Payment.AutoCompleteDisplayMember = Nothing
        Me.cmbTerms_Payment.AutoCompleteValueMember = Nothing
        Me.cmbTerms_Payment.CalculationExpression = Nothing
        Me.cmbTerms_Payment.DropDownAnimationEnabled = True
        Me.cmbTerms_Payment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTerms_Payment.FieldCode = Nothing
        Me.cmbTerms_Payment.FieldDesc = Nothing
        Me.cmbTerms_Payment.FieldMaxLength = 0
        Me.cmbTerms_Payment.FieldName = Nothing
        Me.cmbTerms_Payment.isCalculatedField = False
        Me.cmbTerms_Payment.IsSourceFromTable = False
        Me.cmbTerms_Payment.IsSourceFromValueList = False
        Me.cmbTerms_Payment.IsUnique = False
        Me.cmbTerms_Payment.Location = New System.Drawing.Point(630, 171)
        Me.cmbTerms_Payment.MendatroryField = False
        Me.cmbTerms_Payment.MyLinkLable1 = Me.MyLabel21
        Me.cmbTerms_Payment.MyLinkLable2 = Nothing
        Me.cmbTerms_Payment.Name = "cmbTerms_Payment"
        Me.cmbTerms_Payment.ReferenceFieldDesc = Nothing
        Me.cmbTerms_Payment.ReferenceFieldName = Nothing
        Me.cmbTerms_Payment.ReferenceTableName = Nothing
        Me.cmbTerms_Payment.Size = New System.Drawing.Size(362, 20)
        Me.cmbTerms_Payment.TabIndex = 1459
        '
        'cmbTerms
        '
        Me.cmbTerms.AutoCompleteDisplayMember = Nothing
        Me.cmbTerms.AutoCompleteValueMember = Nothing
        Me.cmbTerms.CalculationExpression = Nothing
        Me.cmbTerms.DropDownAnimationEnabled = True
        Me.cmbTerms.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTerms.FieldCode = Nothing
        Me.cmbTerms.FieldDesc = Nothing
        Me.cmbTerms.FieldMaxLength = 0
        Me.cmbTerms.FieldName = Nothing
        Me.cmbTerms.isCalculatedField = False
        Me.cmbTerms.IsSourceFromTable = False
        Me.cmbTerms.IsSourceFromValueList = False
        Me.cmbTerms.IsUnique = False
        Me.cmbTerms.Location = New System.Drawing.Point(630, 146)
        Me.cmbTerms.MendatroryField = False
        Me.cmbTerms.MyLinkLable1 = Me.MyLabel19
        Me.cmbTerms.MyLinkLable2 = Nothing
        Me.cmbTerms.Name = "cmbTerms"
        Me.cmbTerms.ReferenceFieldDesc = Nothing
        Me.cmbTerms.ReferenceFieldName = Nothing
        Me.cmbTerms.ReferenceTableName = Nothing
        Me.cmbTerms.Size = New System.Drawing.Size(365, 20)
        Me.cmbTerms.TabIndex = 1458
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(529, 148)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel19.TabIndex = 1446
        Me.MyLabel19.Text = "Term Code"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(773, 126)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel20.TabIndex = 1462
        Me.MyLabel20.Text = "Stuffing Status"
        '
        'txtPIDueDate
        '
        Me.txtPIDueDate.CalculationExpression = Nothing
        Me.txtPIDueDate.CustomFormat = "dd-MM-yyyy"
        Me.txtPIDueDate.FieldCode = Nothing
        Me.txtPIDueDate.FieldDesc = Nothing
        Me.txtPIDueDate.FieldMaxLength = 0
        Me.txtPIDueDate.FieldName = Nothing
        Me.txtPIDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPIDueDate.isCalculatedField = False
        Me.txtPIDueDate.IsSourceFromTable = False
        Me.txtPIDueDate.IsSourceFromValueList = False
        Me.txtPIDueDate.IsUnique = False
        Me.txtPIDueDate.Location = New System.Drawing.Point(630, 125)
        Me.txtPIDueDate.MendatroryField = False
        Me.txtPIDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPIDueDate.MyLinkLable1 = Me.MyLabel24
        Me.txtPIDueDate.MyLinkLable2 = Nothing
        Me.txtPIDueDate.Name = "txtPIDueDate"
        Me.txtPIDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPIDueDate.ReferenceFieldDesc = Nothing
        Me.txtPIDueDate.ReferenceFieldName = Nothing
        Me.txtPIDueDate.ReferenceTableName = Nothing
        Me.txtPIDueDate.Size = New System.Drawing.Size(81, 18)
        Me.txtPIDueDate.TabIndex = 1456
        Me.txtPIDueDate.TabStop = False
        Me.txtPIDueDate.Text = "13-06-2011"
        Me.txtPIDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(529, 126)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel24.TabIndex = 1440
        Me.MyLabel24.Text = "Due Date"
        '
        'FndCreditTerms
        '
        Me.FndCreditTerms.CalculationExpression = Nothing
        Me.FndCreditTerms.FieldCode = Nothing
        Me.FndCreditTerms.FieldDesc = Nothing
        Me.FndCreditTerms.FieldMaxLength = 0
        Me.FndCreditTerms.FieldName = Nothing
        Me.FndCreditTerms.isCalculatedField = False
        Me.FndCreditTerms.IsSourceFromTable = False
        Me.FndCreditTerms.IsSourceFromValueList = False
        Me.FndCreditTerms.IsUnique = False
        Me.FndCreditTerms.Location = New System.Drawing.Point(630, 103)
        Me.FndCreditTerms.MendatroryField = False
        Me.FndCreditTerms.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndCreditTerms.MyLinkLable1 = Me.MyLabel25
        Me.FndCreditTerms.MyLinkLable2 = Me.TxtCreditTermsName
        Me.FndCreditTerms.MyReadOnly = False
        Me.FndCreditTerms.MyShowMasterFormButton = False
        Me.FndCreditTerms.Name = "FndCreditTerms"
        Me.FndCreditTerms.ReferenceFieldDesc = Nothing
        Me.FndCreditTerms.ReferenceFieldName = Nothing
        Me.FndCreditTerms.ReferenceTableName = Nothing
        Me.FndCreditTerms.Size = New System.Drawing.Size(138, 20)
        Me.FndCreditTerms.TabIndex = 1455
        Me.FndCreditTerms.Value = ""
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(529, 106)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel25.TabIndex = 1439
        Me.MyLabel25.Text = "Credit Terms"
        '
        'TxtCreditTermsName
        '
        Me.TxtCreditTermsName.AutoSize = False
        Me.TxtCreditTermsName.BorderVisible = True
        Me.TxtCreditTermsName.FieldName = Nothing
        Me.TxtCreditTermsName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditTermsName.Location = New System.Drawing.Point(771, 104)
        Me.TxtCreditTermsName.Name = "TxtCreditTermsName"
        Me.TxtCreditTermsName.Size = New System.Drawing.Size(224, 19)
        Me.TxtCreditTermsName.TabIndex = 1438
        Me.TxtCreditTermsName.TextWrap = False
        '
        'fndCountry_Final_Destination
        '
        Me.fndCountry_Final_Destination.CalculationExpression = Nothing
        Me.fndCountry_Final_Destination.FieldCode = Nothing
        Me.fndCountry_Final_Destination.FieldDesc = Nothing
        Me.fndCountry_Final_Destination.FieldMaxLength = 0
        Me.fndCountry_Final_Destination.FieldName = Nothing
        Me.fndCountry_Final_Destination.isCalculatedField = False
        Me.fndCountry_Final_Destination.IsSourceFromTable = False
        Me.fndCountry_Final_Destination.IsSourceFromValueList = False
        Me.fndCountry_Final_Destination.IsUnique = False
        Me.fndCountry_Final_Destination.Location = New System.Drawing.Point(864, 80)
        Me.fndCountry_Final_Destination.MendatroryField = False
        Me.fndCountry_Final_Destination.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry_Final_Destination.MyLinkLable1 = Me.MyLabel27
        Me.fndCountry_Final_Destination.MyLinkLable2 = Nothing
        Me.fndCountry_Final_Destination.MyReadOnly = False
        Me.fndCountry_Final_Destination.MyShowMasterFormButton = False
        Me.fndCountry_Final_Destination.Name = "fndCountry_Final_Destination"
        Me.fndCountry_Final_Destination.ReferenceFieldDesc = Nothing
        Me.fndCountry_Final_Destination.ReferenceFieldName = Nothing
        Me.fndCountry_Final_Destination.ReferenceTableName = Nothing
        Me.fndCountry_Final_Destination.Size = New System.Drawing.Size(130, 22)
        Me.fndCountry_Final_Destination.TabIndex = 1453
        Me.fndCountry_Final_Destination.Value = ""
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(773, 76)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(86, 27)
        Me.MyLabel27.TabIndex = 1445
        Me.MyLabel27.Text = "<html><p>Country of </p><p>Final Destination</p></html>"
        '
        'txtFinal_Destination
        '
        Me.txtFinal_Destination.CalculationExpression = Nothing
        Me.txtFinal_Destination.FieldCode = Nothing
        Me.txtFinal_Destination.FieldDesc = Nothing
        Me.txtFinal_Destination.FieldMaxLength = 0
        Me.txtFinal_Destination.FieldName = Nothing
        Me.txtFinal_Destination.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinal_Destination.isCalculatedField = False
        Me.txtFinal_Destination.IsSourceFromTable = False
        Me.txtFinal_Destination.IsSourceFromValueList = False
        Me.txtFinal_Destination.IsUnique = False
        Me.txtFinal_Destination.Location = New System.Drawing.Point(630, 56)
        Me.txtFinal_Destination.MaxLength = 100
        Me.txtFinal_Destination.MendatroryField = False
        Me.txtFinal_Destination.MyLinkLable1 = Me.MyLabel28
        Me.txtFinal_Destination.MyLinkLable2 = Nothing
        Me.txtFinal_Destination.Name = "txtFinal_Destination"
        Me.txtFinal_Destination.ReferenceFieldDesc = Nothing
        Me.txtFinal_Destination.ReferenceFieldName = Nothing
        Me.txtFinal_Destination.ReferenceTableName = Nothing
        Me.txtFinal_Destination.Size = New System.Drawing.Size(362, 18)
        Me.txtFinal_Destination.TabIndex = 1451
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(529, 57)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel28.TabIndex = 1443
        Me.MyLabel28.Text = "Final Destination"
        '
        'fndCountry_Origin
        '
        Me.fndCountry_Origin.CalculationExpression = Nothing
        Me.fndCountry_Origin.FieldCode = Nothing
        Me.fndCountry_Origin.FieldDesc = Nothing
        Me.fndCountry_Origin.FieldMaxLength = 0
        Me.fndCountry_Origin.FieldName = Nothing
        Me.fndCountry_Origin.isCalculatedField = False
        Me.fndCountry_Origin.IsSourceFromTable = False
        Me.fndCountry_Origin.IsSourceFromValueList = False
        Me.fndCountry_Origin.IsUnique = False
        Me.fndCountry_Origin.Location = New System.Drawing.Point(630, 80)
        Me.fndCountry_Origin.MendatroryField = False
        Me.fndCountry_Origin.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry_Origin.MyLinkLable1 = Me.MyLabel30
        Me.fndCountry_Origin.MyLinkLable2 = Nothing
        Me.fndCountry_Origin.MyReadOnly = False
        Me.fndCountry_Origin.MyShowMasterFormButton = False
        Me.fndCountry_Origin.Name = "fndCountry_Origin"
        Me.fndCountry_Origin.ReferenceFieldDesc = Nothing
        Me.fndCountry_Origin.ReferenceFieldName = Nothing
        Me.fndCountry_Origin.ReferenceTableName = Nothing
        Me.fndCountry_Origin.Size = New System.Drawing.Size(138, 20)
        Me.fndCountry_Origin.TabIndex = 1452
        Me.fndCountry_Origin.Value = ""
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(529, 76)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(95, 27)
        Me.MyLabel30.TabIndex = 1444
        Me.MyLabel30.Text = "<html><p> Country of Origin </p><p> Goods</p></html>"
        '
        'txtPort_Discharge
        '
        Me.txtPort_Discharge.CalculationExpression = Nothing
        Me.txtPort_Discharge.FieldCode = Nothing
        Me.txtPort_Discharge.FieldDesc = Nothing
        Me.txtPort_Discharge.FieldMaxLength = 0
        Me.txtPort_Discharge.FieldName = Nothing
        Me.txtPort_Discharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort_Discharge.isCalculatedField = False
        Me.txtPort_Discharge.IsSourceFromTable = False
        Me.txtPort_Discharge.IsSourceFromValueList = False
        Me.txtPort_Discharge.IsUnique = False
        Me.txtPort_Discharge.Location = New System.Drawing.Point(630, 36)
        Me.txtPort_Discharge.MaxLength = 100
        Me.txtPort_Discharge.MendatroryField = False
        Me.txtPort_Discharge.MyLinkLable1 = Me.MyLabel31
        Me.txtPort_Discharge.MyLinkLable2 = Nothing
        Me.txtPort_Discharge.Name = "txtPort_Discharge"
        Me.txtPort_Discharge.ReferenceFieldDesc = Nothing
        Me.txtPort_Discharge.ReferenceFieldName = Nothing
        Me.txtPort_Discharge.ReferenceTableName = Nothing
        Me.txtPort_Discharge.Size = New System.Drawing.Size(362, 18)
        Me.txtPort_Discharge.TabIndex = 1450
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(529, 36)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel31.TabIndex = 1442
        Me.MyLabel31.Text = "Port of Discharge"
        '
        'cboStuffing
        '
        Me.cboStuffing.AutoCompleteDisplayMember = Nothing
        Me.cboStuffing.AutoCompleteValueMember = Nothing
        Me.cboStuffing.CalculationExpression = Nothing
        Me.cboStuffing.DropDownAnimationEnabled = True
        Me.cboStuffing.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboStuffing.FieldCode = Nothing
        Me.cboStuffing.FieldDesc = Nothing
        Me.cboStuffing.FieldMaxLength = 0
        Me.cboStuffing.FieldName = Nothing
        Me.cboStuffing.isCalculatedField = False
        Me.cboStuffing.IsSourceFromTable = False
        Me.cboStuffing.IsSourceFromValueList = False
        Me.cboStuffing.IsUnique = False
        Me.cboStuffing.Location = New System.Drawing.Point(865, 124)
        Me.cboStuffing.MendatroryField = False
        Me.cboStuffing.MyLinkLable1 = Me.MyLabel20
        Me.cboStuffing.MyLinkLable2 = Nothing
        Me.cboStuffing.Name = "cboStuffing"
        Me.cboStuffing.ReferenceFieldDesc = Nothing
        Me.cboStuffing.ReferenceFieldName = Nothing
        Me.cboStuffing.ReferenceTableName = Nothing
        Me.cboStuffing.Size = New System.Drawing.Size(127, 20)
        Me.cboStuffing.TabIndex = 1457
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(773, 9)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(89, 27)
        Me.MyLabel32.TabIndex = 1461
        Me.MyLabel32.Text = "<html><p>Place of Receipt </p><p>of Pre-Carrier</p></html>"
        '
        'txtCarrier
        '
        Me.txtCarrier.CalculationExpression = Nothing
        Me.txtCarrier.FieldCode = Nothing
        Me.txtCarrier.FieldDesc = Nothing
        Me.txtCarrier.FieldMaxLength = 0
        Me.txtCarrier.FieldName = Nothing
        Me.txtCarrier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarrier.isCalculatedField = False
        Me.txtCarrier.IsSourceFromTable = False
        Me.txtCarrier.IsSourceFromValueList = False
        Me.txtCarrier.IsUnique = False
        Me.txtCarrier.Location = New System.Drawing.Point(865, 12)
        Me.txtCarrier.MaxLength = 50
        Me.txtCarrier.MendatroryField = False
        Me.txtCarrier.MyLinkLable1 = Me.MyLabel32
        Me.txtCarrier.MyLinkLable2 = Nothing
        Me.txtCarrier.Name = "txtCarrier"
        Me.txtCarrier.ReferenceFieldDesc = Nothing
        Me.txtCarrier.ReferenceFieldName = Nothing
        Me.txtCarrier.ReferenceTableName = Nothing
        Me.txtCarrier.Size = New System.Drawing.Size(127, 18)
        Me.txtCarrier.TabIndex = 1449
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(15, 116)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(113, 16)
        Me.MyLabel17.TabIndex = 1437
        Me.MyLabel17.Text = "HS Classification No."
        '
        'TxtHSClassificationNo
        '
        Me.TxtHSClassificationNo.CalculationExpression = Nothing
        Me.TxtHSClassificationNo.FieldCode = Nothing
        Me.TxtHSClassificationNo.FieldDesc = Nothing
        Me.TxtHSClassificationNo.FieldMaxLength = 0
        Me.TxtHSClassificationNo.FieldName = Nothing
        Me.TxtHSClassificationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHSClassificationNo.isCalculatedField = False
        Me.TxtHSClassificationNo.IsSourceFromTable = False
        Me.TxtHSClassificationNo.IsSourceFromValueList = False
        Me.TxtHSClassificationNo.IsUnique = False
        Me.TxtHSClassificationNo.Location = New System.Drawing.Point(138, 116)
        Me.TxtHSClassificationNo.MaxLength = 30
        Me.TxtHSClassificationNo.MendatroryField = False
        Me.TxtHSClassificationNo.MyLinkLable1 = Me.MyLabel17
        Me.TxtHSClassificationNo.MyLinkLable2 = Nothing
        Me.TxtHSClassificationNo.Name = "TxtHSClassificationNo"
        Me.TxtHSClassificationNo.ReferenceFieldDesc = Nothing
        Me.TxtHSClassificationNo.ReferenceFieldName = Nothing
        Me.TxtHSClassificationNo.ReferenceTableName = Nothing
        Me.TxtHSClassificationNo.Size = New System.Drawing.Size(175, 18)
        Me.TxtHSClassificationNo.TabIndex = 1436
        '
        'txtPINo
        '
        Me.txtPINo.CalculationExpression = Nothing
        Me.txtPINo.FieldCode = Nothing
        Me.txtPINo.FieldDesc = Nothing
        Me.txtPINo.FieldMaxLength = 0
        Me.txtPINo.FieldName = Nothing
        Me.txtPINo.isCalculatedField = False
        Me.txtPINo.IsSourceFromTable = False
        Me.txtPINo.IsSourceFromValueList = False
        Me.txtPINo.IsUnique = False
        Me.txtPINo.Location = New System.Drawing.Point(138, 8)
        Me.txtPINo.MendatroryField = False
        Me.txtPINo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPINo.MyLinkLable1 = Me.MyLabel11
        Me.txtPINo.MyLinkLable2 = Me.lblBeneficiary
        Me.txtPINo.MyReadOnly = False
        Me.txtPINo.MyShowMasterFormButton = False
        Me.txtPINo.Name = "txtPINo"
        Me.txtPINo.ReferenceFieldDesc = Nothing
        Me.txtPINo.ReferenceFieldName = Nothing
        Me.txtPINo.ReferenceTableName = Nothing
        Me.txtPINo.Size = New System.Drawing.Size(143, 22)
        Me.txtPINo.TabIndex = 1435
        Me.txtPINo.Value = ""
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(16, 72)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel11.TabIndex = 1416
        Me.MyLabel11.Text = "Beneficiary"
        '
        'lblBeneficiary
        '
        Me.lblBeneficiary.AutoSize = False
        Me.lblBeneficiary.BorderVisible = True
        Me.lblBeneficiary.FieldName = Nothing
        Me.lblBeneficiary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeneficiary.Location = New System.Drawing.Point(283, 72)
        Me.lblBeneficiary.Name = "lblBeneficiary"
        Me.lblBeneficiary.Size = New System.Drawing.Size(236, 19)
        Me.lblBeneficiary.TabIndex = 1417
        Me.lblBeneficiary.TextWrap = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(17, 168)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel10.TabIndex = 1434
        Me.MyLabel10.Text = "Apply on Amount"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbAmountinpercentage)
        Me.RadGroupBox6.Controls.Add(Me.rdbAmountinrupees)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(138, 163)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(143, 21)
        Me.RadGroupBox6.TabIndex = 1433
        '
        'rdbAmountinpercentage
        '
        Me.rdbAmountinpercentage.AutoSize = True
        Me.rdbAmountinpercentage.Location = New System.Drawing.Point(72, 2)
        Me.rdbAmountinpercentage.Name = "rdbAmountinpercentage"
        Me.rdbAmountinpercentage.Size = New System.Drawing.Size(62, 18)
        Me.rdbAmountinpercentage.TabIndex = 1
        Me.rdbAmountinpercentage.TabStop = True
        Me.rdbAmountinpercentage.Text = "Percent"
        Me.rdbAmountinpercentage.UseVisualStyleBackColor = True
        '
        'rdbAmountinrupees
        '
        Me.rdbAmountinrupees.AutoSize = True
        Me.rdbAmountinrupees.Checked = True
        Me.rdbAmountinrupees.Location = New System.Drawing.Point(2, 3)
        Me.rdbAmountinrupees.Name = "rdbAmountinrupees"
        Me.rdbAmountinrupees.Size = New System.Drawing.Size(62, 18)
        Me.rdbAmountinrupees.TabIndex = 0
        Me.rdbAmountinrupees.TabStop = True
        Me.rdbAmountinrupees.Text = "Amount"
        Me.rdbAmountinrupees.UseVisualStyleBackColor = True
        '
        'lblAdvance
        '
        Me.lblAdvance.FieldName = Nothing
        Me.lblAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvance.Location = New System.Drawing.Point(16, 194)
        Me.lblAdvance.Name = "lblAdvance"
        Me.lblAdvance.Size = New System.Drawing.Size(50, 16)
        Me.lblAdvance.TabIndex = 1422
        Me.lblAdvance.Text = "Advance"
        '
        'TxtOnAccount
        '
        Me.TxtOnAccount.BackColor = System.Drawing.Color.White
        Me.TxtOnAccount.CalculationExpression = Nothing
        Me.TxtOnAccount.DecimalPlaces = 2
        Me.TxtOnAccount.FieldCode = Nothing
        Me.TxtOnAccount.FieldDesc = Nothing
        Me.TxtOnAccount.FieldMaxLength = 0
        Me.TxtOnAccount.FieldName = Nothing
        Me.TxtOnAccount.isCalculatedField = False
        Me.TxtOnAccount.IsSourceFromTable = False
        Me.TxtOnAccount.IsSourceFromValueList = False
        Me.TxtOnAccount.IsUnique = False
        Me.TxtOnAccount.Location = New System.Drawing.Point(138, 286)
        Me.TxtOnAccount.MendatroryField = False
        Me.TxtOnAccount.MyLinkLable1 = Nothing
        Me.TxtOnAccount.MyLinkLable2 = Nothing
        Me.TxtOnAccount.Name = "TxtOnAccount"
        Me.TxtOnAccount.ReferenceFieldDesc = Nothing
        Me.TxtOnAccount.ReferenceFieldName = Nothing
        Me.TxtOnAccount.ReferenceTableName = Nothing
        Me.TxtOnAccount.Size = New System.Drawing.Size(143, 20)
        Me.TxtOnAccount.TabIndex = 1431
        Me.TxtOnAccount.Text = "0"
        Me.TxtOnAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOnAccount.Value = 0R
        '
        'lblonAccount
        '
        Me.lblonAccount.FieldName = Nothing
        Me.lblonAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblonAccount.Location = New System.Drawing.Point(16, 290)
        Me.lblonAccount.Name = "lblonAccount"
        Me.lblonAccount.Size = New System.Drawing.Size(65, 16)
        Me.lblonAccount.TabIndex = 1432
        Me.lblonAccount.Text = "On Account"
        '
        'txtRetained
        '
        Me.txtRetained.BackColor = System.Drawing.Color.White
        Me.txtRetained.CalculationExpression = Nothing
        Me.txtRetained.DecimalPlaces = 2
        Me.txtRetained.FieldCode = Nothing
        Me.txtRetained.FieldDesc = Nothing
        Me.txtRetained.FieldMaxLength = 0
        Me.txtRetained.FieldName = Nothing
        Me.txtRetained.isCalculatedField = False
        Me.txtRetained.IsSourceFromTable = False
        Me.txtRetained.IsSourceFromValueList = False
        Me.txtRetained.IsUnique = False
        Me.txtRetained.Location = New System.Drawing.Point(138, 310)
        Me.txtRetained.MendatroryField = False
        Me.txtRetained.MyLinkLable1 = Nothing
        Me.txtRetained.MyLinkLable2 = Nothing
        Me.txtRetained.Name = "txtRetained"
        Me.txtRetained.ReferenceFieldDesc = Nothing
        Me.txtRetained.ReferenceFieldName = Nothing
        Me.txtRetained.ReferenceTableName = Nothing
        Me.txtRetained.Size = New System.Drawing.Size(143, 20)
        Me.txtRetained.TabIndex = 1429
        Me.txtRetained.Text = "0"
        Me.txtRetained.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRetained.Value = 0R
        '
        'lblretained
        '
        Me.lblretained.FieldName = Nothing
        Me.lblretained.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblretained.Location = New System.Drawing.Point(16, 314)
        Me.lblretained.Name = "lblretained"
        Me.lblretained.Size = New System.Drawing.Size(52, 16)
        Me.lblretained.TabIndex = 1430
        Me.lblretained.Text = "Retained"
        '
        'TxtBalancePayment
        '
        Me.TxtBalancePayment.BackColor = System.Drawing.Color.White
        Me.TxtBalancePayment.CalculationExpression = Nothing
        Me.TxtBalancePayment.DecimalPlaces = 2
        Me.TxtBalancePayment.FieldCode = Nothing
        Me.TxtBalancePayment.FieldDesc = Nothing
        Me.TxtBalancePayment.FieldMaxLength = 0
        Me.TxtBalancePayment.FieldName = Nothing
        Me.TxtBalancePayment.isCalculatedField = False
        Me.TxtBalancePayment.IsSourceFromTable = False
        Me.TxtBalancePayment.IsSourceFromValueList = False
        Me.TxtBalancePayment.IsUnique = False
        Me.TxtBalancePayment.Location = New System.Drawing.Point(138, 238)
        Me.TxtBalancePayment.MendatroryField = False
        Me.TxtBalancePayment.MyLinkLable1 = Nothing
        Me.TxtBalancePayment.MyLinkLable2 = Nothing
        Me.TxtBalancePayment.Name = "TxtBalancePayment"
        Me.TxtBalancePayment.ReferenceFieldDesc = Nothing
        Me.TxtBalancePayment.ReferenceFieldName = Nothing
        Me.TxtBalancePayment.ReferenceTableName = Nothing
        Me.TxtBalancePayment.Size = New System.Drawing.Size(143, 20)
        Me.TxtBalancePayment.TabIndex = 1427
        Me.TxtBalancePayment.Text = "0"
        Me.TxtBalancePayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBalancePayment.Value = 0R
        '
        'lblBalancePayment
        '
        Me.lblBalancePayment.FieldName = Nothing
        Me.lblBalancePayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalancePayment.Location = New System.Drawing.Point(16, 242)
        Me.lblBalancePayment.Name = "lblBalancePayment"
        Me.lblBalancePayment.Size = New System.Drawing.Size(95, 16)
        Me.lblBalancePayment.TabIndex = 1428
        Me.lblBalancePayment.Text = "Balance Payment"
        '
        'TxtLC
        '
        Me.TxtLC.BackColor = System.Drawing.Color.White
        Me.TxtLC.CalculationExpression = Nothing
        Me.TxtLC.DecimalPlaces = 2
        Me.TxtLC.FieldCode = Nothing
        Me.TxtLC.FieldDesc = Nothing
        Me.TxtLC.FieldMaxLength = 0
        Me.TxtLC.FieldName = Nothing
        Me.TxtLC.isCalculatedField = False
        Me.TxtLC.IsSourceFromTable = False
        Me.TxtLC.IsSourceFromValueList = False
        Me.TxtLC.IsUnique = False
        Me.TxtLC.Location = New System.Drawing.Point(138, 214)
        Me.TxtLC.MendatroryField = False
        Me.TxtLC.MyLinkLable1 = Nothing
        Me.TxtLC.MyLinkLable2 = Nothing
        Me.TxtLC.Name = "TxtLC"
        Me.TxtLC.ReferenceFieldDesc = Nothing
        Me.TxtLC.ReferenceFieldName = Nothing
        Me.TxtLC.ReferenceTableName = Nothing
        Me.TxtLC.Size = New System.Drawing.Size(143, 20)
        Me.TxtLC.TabIndex = 1425
        Me.TxtLC.Text = "0"
        Me.TxtLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLC.Value = 0R
        '
        'lblLC
        '
        Me.lblLC.FieldName = Nothing
        Me.lblLC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLC.Location = New System.Drawing.Point(16, 219)
        Me.lblLC.Name = "lblLC"
        Me.lblLC.Size = New System.Drawing.Size(21, 16)
        Me.lblLC.TabIndex = 1426
        Me.lblLC.Text = "LC"
        '
        'TxtCAD
        '
        Me.TxtCAD.BackColor = System.Drawing.Color.White
        Me.TxtCAD.CalculationExpression = Nothing
        Me.TxtCAD.DecimalPlaces = 2
        Me.TxtCAD.FieldCode = Nothing
        Me.TxtCAD.FieldDesc = Nothing
        Me.TxtCAD.FieldMaxLength = 0
        Me.TxtCAD.FieldName = Nothing
        Me.TxtCAD.isCalculatedField = False
        Me.TxtCAD.IsSourceFromTable = False
        Me.TxtCAD.IsSourceFromValueList = False
        Me.TxtCAD.IsUnique = False
        Me.TxtCAD.Location = New System.Drawing.Point(138, 262)
        Me.TxtCAD.MendatroryField = False
        Me.TxtCAD.MyLinkLable1 = Nothing
        Me.TxtCAD.MyLinkLable2 = Nothing
        Me.TxtCAD.Name = "TxtCAD"
        Me.TxtCAD.ReferenceFieldDesc = Nothing
        Me.TxtCAD.ReferenceFieldName = Nothing
        Me.TxtCAD.ReferenceTableName = Nothing
        Me.TxtCAD.Size = New System.Drawing.Size(143, 20)
        Me.TxtCAD.TabIndex = 1423
        Me.TxtCAD.Text = "0"
        Me.TxtCAD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCAD.Value = 0R
        '
        'lblCad
        '
        Me.lblCad.FieldName = Nothing
        Me.lblCad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCad.Location = New System.Drawing.Point(16, 266)
        Me.lblCad.Name = "lblCad"
        Me.lblCad.Size = New System.Drawing.Size(30, 16)
        Me.lblCad.TabIndex = 1424
        Me.lblCad.Text = "CAD"
        '
        'txtAdvance
        '
        Me.txtAdvance.BackColor = System.Drawing.Color.White
        Me.txtAdvance.CalculationExpression = Nothing
        Me.txtAdvance.DecimalPlaces = 2
        Me.txtAdvance.FieldCode = Nothing
        Me.txtAdvance.FieldDesc = Nothing
        Me.txtAdvance.FieldMaxLength = 0
        Me.txtAdvance.FieldName = Nothing
        Me.txtAdvance.isCalculatedField = False
        Me.txtAdvance.IsSourceFromTable = False
        Me.txtAdvance.IsSourceFromValueList = False
        Me.txtAdvance.IsUnique = False
        Me.txtAdvance.Location = New System.Drawing.Point(138, 190)
        Me.txtAdvance.MendatroryField = False
        Me.txtAdvance.MyLinkLable1 = Nothing
        Me.txtAdvance.MyLinkLable2 = Nothing
        Me.txtAdvance.Name = "txtAdvance"
        Me.txtAdvance.ReferenceFieldDesc = Nothing
        Me.txtAdvance.ReferenceFieldName = Nothing
        Me.txtAdvance.ReferenceTableName = Nothing
        Me.txtAdvance.Size = New System.Drawing.Size(143, 20)
        Me.txtAdvance.TabIndex = 1421
        Me.txtAdvance.Text = "0"
        Me.txtAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance.Value = 0R
        '
        'lblpaymenttermsgroup
        '
        Me.lblpaymenttermsgroup.AutoSize = False
        Me.lblpaymenttermsgroup.BorderVisible = True
        Me.lblpaymenttermsgroup.FieldName = Nothing
        Me.lblpaymenttermsgroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymenttermsgroup.Location = New System.Drawing.Point(285, 138)
        Me.lblpaymenttermsgroup.Name = "lblpaymenttermsgroup"
        Me.lblpaymenttermsgroup.Size = New System.Drawing.Size(236, 19)
        Me.lblpaymenttermsgroup.TabIndex = 1420
        Me.lblpaymenttermsgroup.TextWrap = False
        '
        'MyLabel12
        '
        Me.MyLabel12.AutoSize = False
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(16, 132)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(87, 32)
        Me.MyLabel12.TabIndex = 1419
        Me.MyLabel12.Text = "Payment Terms Group"
        '
        'fndPaymenttermsGroup
        '
        Me.fndPaymenttermsGroup.CalculationExpression = Nothing
        Me.fndPaymenttermsGroup.FieldCode = Nothing
        Me.fndPaymenttermsGroup.FieldDesc = Nothing
        Me.fndPaymenttermsGroup.FieldMaxLength = 0
        Me.fndPaymenttermsGroup.FieldName = Nothing
        Me.fndPaymenttermsGroup.isCalculatedField = False
        Me.fndPaymenttermsGroup.IsSourceFromTable = False
        Me.fndPaymenttermsGroup.IsSourceFromValueList = False
        Me.fndPaymenttermsGroup.IsUnique = False
        Me.fndPaymenttermsGroup.Location = New System.Drawing.Point(138, 137)
        Me.fndPaymenttermsGroup.MendatroryField = True
        Me.fndPaymenttermsGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPaymenttermsGroup.MyLinkLable1 = Me.MyLabel12
        Me.fndPaymenttermsGroup.MyLinkLable2 = Me.lblpaymenttermsgroup
        Me.fndPaymenttermsGroup.MyReadOnly = False
        Me.fndPaymenttermsGroup.MyShowMasterFormButton = False
        Me.fndPaymenttermsGroup.Name = "fndPaymenttermsGroup"
        Me.fndPaymenttermsGroup.ReferenceFieldDesc = Nothing
        Me.fndPaymenttermsGroup.ReferenceFieldName = Nothing
        Me.fndPaymenttermsGroup.ReferenceTableName = Nothing
        Me.fndPaymenttermsGroup.Size = New System.Drawing.Size(143, 20)
        Me.fndPaymenttermsGroup.TabIndex = 1418
        Me.fndPaymenttermsGroup.Value = ""
        '
        'TxtBeneficiary
        '
        Me.TxtBeneficiary.CalculationExpression = Nothing
        Me.TxtBeneficiary.FieldCode = Nothing
        Me.TxtBeneficiary.FieldDesc = Nothing
        Me.TxtBeneficiary.FieldMaxLength = 0
        Me.TxtBeneficiary.FieldName = Nothing
        Me.TxtBeneficiary.isCalculatedField = False
        Me.TxtBeneficiary.IsSourceFromTable = False
        Me.TxtBeneficiary.IsSourceFromValueList = False
        Me.TxtBeneficiary.IsUnique = False
        Me.TxtBeneficiary.Location = New System.Drawing.Point(138, 71)
        Me.TxtBeneficiary.MendatroryField = True
        Me.TxtBeneficiary.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBeneficiary.MyLinkLable1 = Me.MyLabel11
        Me.TxtBeneficiary.MyLinkLable2 = Me.lblBeneficiary
        Me.TxtBeneficiary.MyReadOnly = False
        Me.TxtBeneficiary.MyShowMasterFormButton = False
        Me.TxtBeneficiary.Name = "TxtBeneficiary"
        Me.TxtBeneficiary.ReferenceFieldDesc = Nothing
        Me.TxtBeneficiary.ReferenceFieldName = Nothing
        Me.TxtBeneficiary.ReferenceTableName = Nothing
        Me.TxtBeneficiary.Size = New System.Drawing.Size(143, 20)
        Me.TxtBeneficiary.TabIndex = 1415
        Me.TxtBeneficiary.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(16, 95)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel9.TabIndex = 1414
        Me.MyLabel9.Text = "INCOTERMS"
        '
        'TxtINCOTERMS
        '
        Me.TxtINCOTERMS.CalculationExpression = Nothing
        Me.TxtINCOTERMS.FieldCode = Nothing
        Me.TxtINCOTERMS.FieldDesc = Nothing
        Me.TxtINCOTERMS.FieldMaxLength = 0
        Me.TxtINCOTERMS.FieldName = Nothing
        Me.TxtINCOTERMS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtINCOTERMS.isCalculatedField = False
        Me.TxtINCOTERMS.IsSourceFromTable = False
        Me.TxtINCOTERMS.IsSourceFromValueList = False
        Me.TxtINCOTERMS.IsUnique = False
        Me.TxtINCOTERMS.Location = New System.Drawing.Point(138, 95)
        Me.TxtINCOTERMS.MaxLength = 200
        Me.TxtINCOTERMS.MendatroryField = False
        Me.TxtINCOTERMS.MyLinkLable1 = Me.MyLabel9
        Me.TxtINCOTERMS.MyLinkLable2 = Nothing
        Me.TxtINCOTERMS.Name = "TxtINCOTERMS"
        Me.TxtINCOTERMS.ReferenceFieldDesc = Nothing
        Me.TxtINCOTERMS.ReferenceFieldName = Nothing
        Me.TxtINCOTERMS.ReferenceTableName = Nothing
        Me.TxtINCOTERMS.Size = New System.Drawing.Size(383, 18)
        Me.TxtINCOTERMS.TabIndex = 1413
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cboPIStatus)
        Me.RadGroupBox5.Controls.Add(Me.btnMTUpdate)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox5.Controls.Add(Me.txtPIDate)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel29)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(16, 33)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(503, 33)
        Me.RadGroupBox5.TabIndex = 1412
        '
        'cboPIStatus
        '
        Me.cboPIStatus.AutoCompleteDisplayMember = Nothing
        Me.cboPIStatus.AutoCompleteValueMember = Nothing
        Me.cboPIStatus.CalculationExpression = Nothing
        Me.cboPIStatus.DropDownAnimationEnabled = True
        Me.cboPIStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPIStatus.FieldCode = Nothing
        Me.cboPIStatus.FieldDesc = Nothing
        Me.cboPIStatus.FieldMaxLength = 0
        Me.cboPIStatus.FieldName = Nothing
        Me.cboPIStatus.isCalculatedField = False
        Me.cboPIStatus.IsSourceFromTable = False
        Me.cboPIStatus.IsSourceFromValueList = False
        Me.cboPIStatus.IsUnique = False
        Me.cboPIStatus.Location = New System.Drawing.Point(76, 5)
        Me.cboPIStatus.MendatroryField = False
        Me.cboPIStatus.MyLinkLable1 = Me.RadLabel5
        Me.cboPIStatus.MyLinkLable2 = Nothing
        Me.cboPIStatus.Name = "cboPIStatus"
        Me.cboPIStatus.ReferenceFieldDesc = Nothing
        Me.cboPIStatus.ReferenceFieldName = Nothing
        Me.cboPIStatus.ReferenceTableName = Nothing
        Me.cboPIStatus.Size = New System.Drawing.Size(143, 20)
        Me.cboPIStatus.TabIndex = 1403
        '
        'btnMTUpdate
        '
        Me.btnMTUpdate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMTUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMTUpdate.Location = New System.Drawing.Point(402, 6)
        Me.btnMTUpdate.Name = "btnMTUpdate"
        Me.btnMTUpdate.Size = New System.Drawing.Size(69, 20)
        Me.btnMTUpdate.TabIndex = 1402
        Me.btnMTUpdate.Text = "Update"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(3, 8)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel22.TabIndex = 169
        Me.MyLabel22.Text = "PI Status"
        '
        'txtPIDate
        '
        Me.txtPIDate.CalculationExpression = Nothing
        Me.txtPIDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPIDate.FieldCode = Nothing
        Me.txtPIDate.FieldDesc = Nothing
        Me.txtPIDate.FieldMaxLength = 0
        Me.txtPIDate.FieldName = Nothing
        Me.txtPIDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPIDate.isCalculatedField = False
        Me.txtPIDate.IsSourceFromTable = False
        Me.txtPIDate.IsSourceFromValueList = False
        Me.txtPIDate.IsUnique = False
        Me.txtPIDate.Location = New System.Drawing.Point(285, 7)
        Me.txtPIDate.MendatroryField = False
        Me.txtPIDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPIDate.MyLinkLable1 = Me.MyLabel29
        Me.txtPIDate.MyLinkLable2 = Nothing
        Me.txtPIDate.Name = "txtPIDate"
        Me.txtPIDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPIDate.ReferenceFieldDesc = Nothing
        Me.txtPIDate.ReferenceFieldName = Nothing
        Me.txtPIDate.ReferenceTableName = Nothing
        Me.txtPIDate.ShowCheckBox = True
        Me.txtPIDate.Size = New System.Drawing.Size(108, 18)
        Me.txtPIDate.TabIndex = 1400
        Me.txtPIDate.TabStop = False
        Me.txtPIDate.Text = "13/06/2011"
        Me.txtPIDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(225, 8)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel29.TabIndex = 1401
        Me.MyLabel29.Text = "PI Date"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(16, 10)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel8.TabIndex = 30
        Me.MyLabel8.Text = " PI No"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer5)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage3.Text = "Additional Charges"
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer5.Size = New System.Drawing.Size(1080, 391)
        Me.SplitContainer5.SplitterDistance = 621
        Me.SplitContainer5.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.gvAC)
        Me.GroupBox3.Controls.Add(Me.RadPanel1)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(621, 391)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Additional Charges"
        '
        'gvAC
        '
        Me.gvAC.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvAC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvAC.ForeColor = System.Drawing.Color.Black
        Me.gvAC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAC.Location = New System.Drawing.Point(3, 16)
        '
        '
        '
        Me.gvAC.MasterTemplate.AllowDeleteRow = False
        Me.gvAC.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(615, 345)
        Me.gvAC.TabIndex = 0
        Me.gvAC.TabStop = False
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadLabel31)
        Me.RadPanel1.Controls.Add(Me.lblAddCharges)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(3, 361)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(615, 27)
        Me.RadPanel1.TabIndex = 1
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(370, 4)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 1
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(501, 4)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 0
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.gvACInsurance)
        Me.GroupBox4.Controls.Add(Me.RadPanel2)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(455, 391)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Additional Charges For Insurance"
        '
        'gvACInsurance
        '
        Me.gvACInsurance.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvACInsurance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvACInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvACInsurance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvACInsurance.ForeColor = System.Drawing.Color.Black
        Me.gvACInsurance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvACInsurance.Location = New System.Drawing.Point(3, 16)
        '
        '
        '
        Me.gvACInsurance.MasterTemplate.AllowDeleteRow = False
        Me.gvACInsurance.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvACInsurance.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvACInsurance.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvACInsurance.Name = "gvACInsurance"
        Me.gvACInsurance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvACInsurance.ShowGroupPanel = False
        Me.gvACInsurance.ShowHeaderCellButtons = True
        Me.gvACInsurance.Size = New System.Drawing.Size(449, 345)
        Me.gvACInsurance.TabIndex = 3
        Me.gvACInsurance.TabStop = False
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.MyLabel56)
        Me.RadPanel2.Controls.Add(Me.lblAddChargesForInsurance)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel2.Location = New System.Drawing.Point(3, 361)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(449, 27)
        Me.RadPanel2.TabIndex = 2
        '
        'MyLabel56
        '
        Me.MyLabel56.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel56.FieldName = Nothing
        Me.MyLabel56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel56.Location = New System.Drawing.Point(130, 5)
        Me.MyLabel56.Name = "MyLabel56"
        Me.MyLabel56.Size = New System.Drawing.Size(203, 16)
        Me.MyLabel56.TabIndex = 1
        Me.MyLabel56.Text = "Total Additional Charges For Insurance"
        '
        'lblAddChargesForInsurance
        '
        Me.lblAddChargesForInsurance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddChargesForInsurance.AutoSize = False
        Me.lblAddChargesForInsurance.BorderVisible = True
        Me.lblAddChargesForInsurance.FieldName = Nothing
        Me.lblAddChargesForInsurance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddChargesForInsurance.Location = New System.Drawing.Point(335, 4)
        Me.lblAddChargesForInsurance.Name = "lblAddChargesForInsurance"
        Me.lblAddChargesForInsurance.Size = New System.Drawing.Size(110, 18)
        Me.lblAddChargesForInsurance.TabIndex = 0
        Me.lblAddChargesForInsurance.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1020, 391)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1020, 391)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1080, 391)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1080, 391)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel58)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotalInsuranceAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel57)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddChargesForInsurance1)
        Me.RadPageViewPage4.Controls.Add(Me.txtHeaderDiscountAmount)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel53)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxableAmount)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterTax)
        Me.RadPageViewPage4.Controls.Add(Me.chkIsContent)
        Me.RadPageViewPage4.Controls.Add(Me.txtKindAttentation)
        Me.RadPageViewPage4.Controls.Add(Me.lblKindAttentation)
        Me.RadPageViewPage4.Controls.Add(Me.txtSubject)
        Me.RadPageViewPage4.Controls.Add(Me.txtContentSubject)
        Me.RadPageViewPage4.Controls.Add(Me.lblContentSubject)
        Me.RadPageViewPage4.Controls.Add(Me.lblSubject)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
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
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage4.Text = "Total"
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(82, 168)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(136, 16)
        Me.MyLabel58.TabIndex = 80
        Me.MyLabel58.Text = "+ Total Insurance Amount"
        '
        'lblTotalInsuranceAmt
        '
        Me.lblTotalInsuranceAmt.AutoSize = False
        Me.lblTotalInsuranceAmt.BorderVisible = True
        Me.lblTotalInsuranceAmt.FieldName = Nothing
        Me.lblTotalInsuranceAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalInsuranceAmt.Location = New System.Drawing.Point(221, 167)
        Me.lblTotalInsuranceAmt.Name = "lblTotalInsuranceAmt"
        Me.lblTotalInsuranceAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotalInsuranceAmt.TabIndex = 79
        Me.lblTotalInsuranceAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel57
        '
        Me.MyLabel57.FieldName = Nothing
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(5, 148)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(213, 16)
        Me.MyLabel57.TabIndex = 78
        Me.MyLabel57.Text = "+ Total Additional Charges For Insurance"
        '
        'lblAddChargesForInsurance1
        '
        Me.lblAddChargesForInsurance1.AutoSize = False
        Me.lblAddChargesForInsurance1.BorderVisible = True
        Me.lblAddChargesForInsurance1.FieldName = Nothing
        Me.lblAddChargesForInsurance1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddChargesForInsurance1.Location = New System.Drawing.Point(221, 147)
        Me.lblAddChargesForInsurance1.Name = "lblAddChargesForInsurance1"
        Me.lblAddChargesForInsurance1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddChargesForInsurance1.TabIndex = 77
        Me.lblAddChargesForInsurance1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeaderDiscountAmount
        '
        Me.txtHeaderDiscountAmount.BackColor = System.Drawing.Color.White
        Me.txtHeaderDiscountAmount.CalculationExpression = Nothing
        Me.txtHeaderDiscountAmount.DecimalPlaces = 2
        Me.txtHeaderDiscountAmount.FieldCode = Nothing
        Me.txtHeaderDiscountAmount.FieldDesc = Nothing
        Me.txtHeaderDiscountAmount.FieldMaxLength = 0
        Me.txtHeaderDiscountAmount.FieldName = Nothing
        Me.txtHeaderDiscountAmount.isCalculatedField = False
        Me.txtHeaderDiscountAmount.IsSourceFromTable = False
        Me.txtHeaderDiscountAmount.IsSourceFromValueList = False
        Me.txtHeaderDiscountAmount.IsUnique = False
        Me.txtHeaderDiscountAmount.Location = New System.Drawing.Point(221, 85)
        Me.txtHeaderDiscountAmount.MendatroryField = False
        Me.txtHeaderDiscountAmount.MyLinkLable1 = Nothing
        Me.txtHeaderDiscountAmount.MyLinkLable2 = Nothing
        Me.txtHeaderDiscountAmount.Name = "txtHeaderDiscountAmount"
        Me.txtHeaderDiscountAmount.ReferenceFieldDesc = Nothing
        Me.txtHeaderDiscountAmount.ReferenceFieldName = Nothing
        Me.txtHeaderDiscountAmount.ReferenceTableName = Nothing
        Me.txtHeaderDiscountAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtHeaderDiscountAmount.TabIndex = 75
        Me.txtHeaderDiscountAmount.Text = "0"
        Me.txtHeaderDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtHeaderDiscountAmount.Value = 0R
        '
        'MyLabel53
        '
        Me.MyLabel53.FieldName = Nothing
        Me.MyLabel53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel53.Location = New System.Drawing.Point(85, 87)
        Me.MyLabel53.Name = "MyLabel53"
        Me.MyLabel53.Size = New System.Drawing.Size(133, 16)
        Me.MyLabel53.TabIndex = 76
        Me.MyLabel53.Text = "Header Discount Amount"
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(129, 188)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel44.TabIndex = 74
        Me.MyLabel44.Text = "Taxable Amount"
        '
        'lblTaxableAmount
        '
        Me.lblTaxableAmount.AutoSize = False
        Me.lblTaxableAmount.BorderVisible = True
        Me.lblTaxableAmount.FieldName = Nothing
        Me.lblTaxableAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxableAmount.Location = New System.Drawing.Point(220, 187)
        Me.lblTaxableAmount.Name = "lblTaxableAmount"
        Me.lblTaxableAmount.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxableAmount.TabIndex = 73
        Me.lblTaxableAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(124, 228)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel41.TabIndex = 72
        Me.MyLabel41.Text = "Amount After Tax"
        '
        'lblAmtAfterTax
        '
        Me.lblAmtAfterTax.AutoSize = False
        Me.lblAmtAfterTax.BorderVisible = True
        Me.lblAmtAfterTax.FieldName = Nothing
        Me.lblAmtAfterTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterTax.Location = New System.Drawing.Point(221, 227)
        Me.lblAmtAfterTax.Name = "lblAmtAfterTax"
        Me.lblAmtAfterTax.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterTax.TabIndex = 71
        Me.lblAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkIsContent
        '
        Me.chkIsContent.AccessibleDescription = ""
        Me.chkIsContent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsContent.Location = New System.Drawing.Point(19, 406)
        Me.chkIsContent.Name = "chkIsContent"
        Me.chkIsContent.Size = New System.Drawing.Size(72, 16)
        Me.chkIsContent.TabIndex = 70
        Me.chkIsContent.Text = "Is Content"
        '
        'txtKindAttentation
        '
        Me.txtKindAttentation.Location = New System.Drawing.Point(123, 402)
        Me.txtKindAttentation.Multiline = True
        Me.txtKindAttentation.Name = "txtKindAttentation"
        Me.txtKindAttentation.Size = New System.Drawing.Size(298, 52)
        Me.txtKindAttentation.TabIndex = 20
        '
        'lblKindAttentation
        '
        Me.lblKindAttentation.FieldName = Nothing
        Me.lblKindAttentation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKindAttentation.Location = New System.Drawing.Point(30, 408)
        Me.lblKindAttentation.Name = "lblKindAttentation"
        Me.lblKindAttentation.Size = New System.Drawing.Size(77, 16)
        Me.lblKindAttentation.TabIndex = 19
        Me.lblKindAttentation.Text = "Kind Attention"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(123, 292)
        Me.txtSubject.Multiline = True
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(298, 48)
        Me.txtSubject.TabIndex = 18
        '
        'txtContentSubject
        '
        Me.txtContentSubject.Location = New System.Drawing.Point(123, 344)
        Me.txtContentSubject.Multiline = True
        Me.txtContentSubject.Name = "txtContentSubject"
        Me.txtContentSubject.Size = New System.Drawing.Size(298, 52)
        Me.txtContentSubject.TabIndex = 17
        '
        'lblContentSubject
        '
        Me.lblContentSubject.FieldName = Nothing
        Me.lblContentSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContentSubject.Location = New System.Drawing.Point(30, 360)
        Me.lblContentSubject.Name = "lblContentSubject"
        Me.lblContentSubject.Size = New System.Drawing.Size(87, 16)
        Me.lblContentSubject.TabIndex = 16
        Me.lblContentSubject.Text = "Content Subject"
        '
        'lblSubject
        '
        Me.lblSubject.FieldName = Nothing
        Me.lblSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubject.Location = New System.Drawing.Point(69, 296)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(44, 16)
        Me.lblSubject.TabIndex = 15
        Me.lblSubject.Text = "Subject"
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(30, 13)
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
        Me.txtConversionRate.Location = New System.Drawing.Point(353, 9)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 19)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(483, 11)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 4
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
        Me.txtApplicableFrom.Size = New System.Drawing.Size(152, 18)
        Me.txtApplicableFrom.TabIndex = 5
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(23, 10)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 2
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(256, 11)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 3
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(221, 247)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 6
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(78, 248)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 5
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(427, 61)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(475, 347)
        Me.txtComment.TabIndex = 1
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(370, 64)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 1
        Me.RadLabel14.Text = "Comment"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(98, 128)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 10
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(118, 268)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 4
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(221, 267)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 3
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(141, 208)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 8
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(221, 207)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 7
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(221, 127)
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
        Me.lblDiscountAmt.Location = New System.Drawing.Point(221, 107)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 12
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(221, 65)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 13
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(90, 108)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(128, 16)
        Me.RadLabel22.TabIndex = 11
        Me.RadLabel22.Text = "- Total Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(32, 66)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 14
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage6.Enabled = False
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(75.0!, 26.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage6.Text = "Work Order"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblWAddress)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblWPhone)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblWVendorName)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel47)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblAddress)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel46)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer4.Size = New System.Drawing.Size(1080, 391)
        Me.SplitContainer4.SplitterDistance = 49
        Me.SplitContainer4.TabIndex = 1
        '
        'lblWAddress
        '
        Me.lblWAddress.AutoSize = False
        Me.lblWAddress.CalculationExpression = Nothing
        Me.lblWAddress.FieldCode = Nothing
        Me.lblWAddress.FieldDesc = Nothing
        Me.lblWAddress.FieldMaxLength = 0
        Me.lblWAddress.FieldName = Nothing
        Me.lblWAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWAddress.isCalculatedField = False
        Me.lblWAddress.IsSourceFromTable = False
        Me.lblWAddress.IsSourceFromValueList = False
        Me.lblWAddress.IsUnique = False
        Me.lblWAddress.Location = New System.Drawing.Point(495, 7)
        Me.lblWAddress.MaxLength = 200
        Me.lblWAddress.MendatroryField = False
        Me.lblWAddress.MyLinkLable1 = Me.MyLabel2
        Me.lblWAddress.MyLinkLable2 = Nothing
        Me.lblWAddress.Name = "lblWAddress"
        Me.lblWAddress.ReferenceFieldDesc = Nothing
        Me.lblWAddress.ReferenceFieldName = Nothing
        Me.lblWAddress.ReferenceTableName = Nothing
        Me.lblWAddress.Size = New System.Drawing.Size(536, 38)
        Me.lblWAddress.TabIndex = 47
        '
        'lblWPhone
        '
        Me.lblWPhone.CalculationExpression = Nothing
        Me.lblWPhone.FieldCode = Nothing
        Me.lblWPhone.FieldDesc = Nothing
        Me.lblWPhone.FieldMaxLength = 0
        Me.lblWPhone.FieldName = Nothing
        Me.lblWPhone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWPhone.isCalculatedField = False
        Me.lblWPhone.IsSourceFromTable = False
        Me.lblWPhone.IsSourceFromValueList = False
        Me.lblWPhone.IsUnique = False
        Me.lblWPhone.Location = New System.Drawing.Point(91, 29)
        Me.lblWPhone.MaxLength = 200
        Me.lblWPhone.MendatroryField = False
        Me.lblWPhone.MyLinkLable1 = Me.MyLabel2
        Me.lblWPhone.MyLinkLable2 = Nothing
        Me.lblWPhone.Name = "lblWPhone"
        Me.lblWPhone.ReferenceFieldDesc = Nothing
        Me.lblWPhone.ReferenceFieldName = Nothing
        Me.lblWPhone.ReferenceTableName = Nothing
        Me.lblWPhone.Size = New System.Drawing.Size(267, 18)
        Me.lblWPhone.TabIndex = 46
        '
        'lblWVendorName
        '
        Me.lblWVendorName.CalculationExpression = Nothing
        Me.lblWVendorName.FieldCode = Nothing
        Me.lblWVendorName.FieldDesc = Nothing
        Me.lblWVendorName.FieldMaxLength = 0
        Me.lblWVendorName.FieldName = Nothing
        Me.lblWVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWVendorName.isCalculatedField = False
        Me.lblWVendorName.IsSourceFromTable = False
        Me.lblWVendorName.IsSourceFromValueList = False
        Me.lblWVendorName.IsUnique = False
        Me.lblWVendorName.Location = New System.Drawing.Point(91, 9)
        Me.lblWVendorName.MaxLength = 200
        Me.lblWVendorName.MendatroryField = False
        Me.lblWVendorName.MyLinkLable1 = Me.MyLabel2
        Me.lblWVendorName.MyLinkLable2 = Nothing
        Me.lblWVendorName.Name = "lblWVendorName"
        Me.lblWVendorName.ReferenceFieldDesc = Nothing
        Me.lblWVendorName.ReferenceFieldName = Nothing
        Me.lblWVendorName.ReferenceTableName = Nothing
        Me.lblWVendorName.Size = New System.Drawing.Size(267, 18)
        Me.lblWVendorName.TabIndex = 45
        '
        'MyLabel47
        '
        Me.MyLabel47.FieldName = Nothing
        Me.MyLabel47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel47.Location = New System.Drawing.Point(9, 31)
        Me.MyLabel47.Name = "MyLabel47"
        Me.MyLabel47.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel47.TabIndex = 44
        Me.MyLabel47.Text = "Phone No"
        '
        'lblAddress
        '
        Me.lblAddress.FieldName = Nothing
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(418, 9)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 42
        Me.lblAddress.Text = "Address"
        '
        'MyLabel46
        '
        Me.MyLabel46.FieldName = Nothing
        Me.MyLabel46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel46.Location = New System.Drawing.Point(9, 8)
        Me.MyLabel46.Name = "MyLabel46"
        Me.MyLabel46.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel46.TabIndex = 40
        Me.MyLabel46.Text = "Vendor Name"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.gvCategoryValue)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(1080, 338)
        Me.SplitContainer3.SplitterDistance = 219
        Me.SplitContainer3.TabIndex = 0
        '
        'gvCategoryValue
        '
        Me.gvCategoryValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCategoryValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCategoryValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategoryValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCategoryValue.ForeColor = System.Drawing.Color.Black
        Me.gvCategoryValue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCategoryValue.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCategoryValue.MasterTemplate.AllowDeleteRow = False
        Me.gvCategoryValue.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCategoryValue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategoryValue.MasterTemplate.ViewDefinition = TableViewDefinition8
        Me.gvCategoryValue.Name = "gvCategoryValue"
        Me.gvCategoryValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCategoryValue.ShowGroupPanel = False
        Me.gvCategoryValue.ShowHeaderCellButtons = True
        Me.gvCategoryValue.Size = New System.Drawing.Size(1080, 219)
        Me.gvCategoryValue.TabIndex = 1
        Me.gvCategoryValue.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadSplitContainer1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1080, 115)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Terms & Conditions"
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(3, 16)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(1074, 96)
        Me.RadSplitContainer1.TabIndex = 2
        Me.RadSplitContainer1.TabStop = False
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.gvTermsCdtion)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(1074, 92)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, 0.5!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 84)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'gvTermsCdtion
        '
        Me.gvTermsCdtion.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvTermsCdtion.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTermsCdtion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTermsCdtion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTermsCdtion.ForeColor = System.Drawing.Color.Black
        Me.gvTermsCdtion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTermsCdtion.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvTermsCdtion.MasterTemplate.AllowDeleteRow = False
        Me.gvTermsCdtion.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvTermsCdtion.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTermsCdtion.MasterTemplate.ViewDefinition = TableViewDefinition9
        Me.gvTermsCdtion.Name = "gvTermsCdtion"
        Me.gvTermsCdtion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTermsCdtion.ShowGroupPanel = False
        Me.gvTermsCdtion.ShowHeaderCellButtons = True
        Me.gvTermsCdtion.Size = New System.Drawing.Size(1074, 92)
        Me.gvTermsCdtion.TabIndex = 1
        Me.gvTermsCdtion.TabStop = False
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 96)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(1074, 0)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, -0.5!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -84)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'RadPageViewPage7
        '
        Me.RadPageViewPage7.Controls.Add(Me.gvSchedule)
        Me.RadPageViewPage7.Controls.Add(Me.Panel5)
        Me.RadPageViewPage7.Controls.Add(Me.MyLabel61)
        Me.RadPageViewPage7.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage7.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage7.Name = "RadPageViewPage7"
        Me.RadPageViewPage7.Size = New System.Drawing.Size(1080, 391)
        Me.RadPageViewPage7.Text = "Set Schedule"
        '
        'gvSchedule
        '
        Me.gvSchedule.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvSchedule.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSchedule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvSchedule.ForeColor = System.Drawing.Color.Black
        Me.gvSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSchedule.Location = New System.Drawing.Point(0, 28)
        '
        '
        '
        Me.gvSchedule.MasterTemplate.AllowDeleteRow = False
        Me.gvSchedule.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSchedule.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSchedule.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvSchedule.Name = "gvSchedule"
        Me.gvSchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSchedule.ShowGroupPanel = False
        Me.gvSchedule.ShowHeaderCellButtons = True
        Me.gvSchedule.Size = New System.Drawing.Size(1080, 350)
        Me.gvSchedule.TabIndex = 20
        Me.gvSchedule.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.RadButton1)
        Me.Panel5.Controls.Add(Me.txtScheduleStartDate)
        Me.Panel5.Controls.Add(Me.MyLabel60)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1080, 28)
        Me.Panel5.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(202, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(55, 18)
        Me.RadButton1.TabIndex = 1520
        Me.RadButton1.Text = ">>"
        '
        'txtScheduleStartDate
        '
        Me.txtScheduleStartDate.CalculationExpression = Nothing
        Me.txtScheduleStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtScheduleStartDate.FieldCode = Nothing
        Me.txtScheduleStartDate.FieldDesc = Nothing
        Me.txtScheduleStartDate.FieldMaxLength = 0
        Me.txtScheduleStartDate.FieldName = Nothing
        Me.txtScheduleStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleStartDate.isCalculatedField = False
        Me.txtScheduleStartDate.IsSourceFromTable = False
        Me.txtScheduleStartDate.IsSourceFromValueList = False
        Me.txtScheduleStartDate.IsUnique = False
        Me.txtScheduleStartDate.Location = New System.Drawing.Point(120, 5)
        Me.txtScheduleStartDate.MendatroryField = False
        Me.txtScheduleStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleStartDate.MyLinkLable1 = Me.MyLabel60
        Me.txtScheduleStartDate.MyLinkLable2 = Nothing
        Me.txtScheduleStartDate.Name = "txtScheduleStartDate"
        Me.txtScheduleStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleStartDate.ReferenceFieldDesc = Nothing
        Me.txtScheduleStartDate.ReferenceFieldName = Nothing
        Me.txtScheduleStartDate.ReferenceTableName = Nothing
        Me.txtScheduleStartDate.Size = New System.Drawing.Size(79, 18)
        Me.txtScheduleStartDate.TabIndex = 1518
        Me.txtScheduleStartDate.TabStop = False
        Me.txtScheduleStartDate.Text = "13/06/2011"
        Me.txtScheduleStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel60
        '
        Me.MyLabel60.FieldName = Nothing
        Me.MyLabel60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel60.Location = New System.Drawing.Point(5, 6)
        Me.MyLabel60.Name = "MyLabel60"
        Me.MyLabel60.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel60.TabIndex = 1519
        Me.MyLabel60.Text = "Schedule Start Date"
        '
        'MyLabel61
        '
        Me.MyLabel61.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyLabel61.FieldName = Nothing
        Me.MyLabel61.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel61.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel61.Location = New System.Drawing.Point(0, 378)
        Me.MyLabel61.Name = "MyLabel61"
        Me.MyLabel61.Size = New System.Drawing.Size(1080, 13)
        Me.MyLabel61.TabIndex = 1522
        Me.MyLabel61.Text = "Press F5 To View Penelty Details"
        '
        'btnNewHistory
        '
        Me.btnNewHistory.Location = New System.Drawing.Point(817, 2)
        Me.btnNewHistory.Name = "btnNewHistory"
        Me.btnNewHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnNewHistory.TabIndex = 41
        Me.btnNewHistory.Text = "&History"
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewTDSDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(212, 5)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(69, 22)
        Me.btnViewTDSDetails.TabIndex = 13
        Me.btnViewTDSDetails.Text = "View TDS"
        '
        'btn_cancel
        '
        Me.btn_cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_cancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cancel.Location = New System.Drawing.Point(891, 2)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(69, 22)
        Me.btn_cancel.TabIndex = 12
        Me.btn_cancel.Text = "Cancel"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(960, 2)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(69, 22)
        Me.btnHistory.TabIndex = 11
        Me.btnHistory.Text = "History"
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(419, 5)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(69, 22)
        Me.btnCopy.TabIndex = 5
        Me.btnCopy.Text = "Copy PO"
        '
        'chkpoclose
        '
        Me.chkpoclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkpoclose.AutoSize = True
        Me.chkpoclose.Location = New System.Drawing.Point(639, 7)
        Me.chkpoclose.Name = "chkpoclose"
        Me.chkpoclose.Size = New System.Drawing.Size(133, 18)
        Me.chkpoclose.TabIndex = 9
        Me.chkpoclose.Text = "Close Purchase Order"
        Me.chkpoclose.UseVisualStyleBackColor = True
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(281, 5)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 22)
        Me.btnUnpost.TabIndex = 3
        Me.btnUnpost.Text = "Reverse"
        '
        'btnPrintNew
        '
        Me.btnPrintNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintNew.Location = New System.Drawing.Point(350, 5)
        Me.btnPrintNew.Name = "btnPrintNew"
        Me.btnPrintNew.Size = New System.Drawing.Size(69, 22)
        Me.btnPrintNew.TabIndex = 4
        Me.btnPrintNew.Text = "Print"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Location = New System.Drawing.Point(488, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 22)
        Me.RadSplitButton1.TabIndex = 6
        Me.RadSplitButton1.Text = "Print"
        Me.RadSplitButton1.Visible = False
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(568, 5)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 7
        Me.btnAmendment.Text = "Amendment"
        Me.btnAmendment.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(143, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(74, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1029, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.ReportFooter, Me.SaveLayoutbtn, Me.DeleteLayout, Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'ReportFooter
        '
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.Text = "Report Footer"
        '
        'SaveLayoutbtn
        '
        Me.SaveLayoutbtn.Name = "SaveLayoutbtn"
        Me.SaveLayoutbtn.Text = "Save Layout"
        '
        'DeleteLayout
        '
        Me.DeleteLayout.Name = "DeleteLayout"
        Me.DeleteLayout.Text = "Delete Layout "
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1105, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'frmPurchaseOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 493)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmPurchaseOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Order"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefTendorNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel55, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorQuotationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRepair, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.lblGstinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegisterOrUnregister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_deliverydays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWorkOutward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGSTRegistered, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chk_emergency.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.pnl_capex.ResumeLayout(False)
        Me.pnl_capex.PerformLayout()
        CType(Me.chkReceiveControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoCalculate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAgainstPO_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpRenewal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsMerchantTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMCCPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBlanket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmtCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.txtReferencePO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtDeliveryDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveryDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuotationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuotationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainst_RGP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.RadPageViewPage5.PerformLayout()
        CType(Me.btnForm_Update, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv_c_form.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_c_form, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_c_form, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gv_roadpermit.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_roadpermit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chkroadpermit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.chkTDSApplied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExciseOnQty, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel54, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConfirmatory_PO_SRN_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel52, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPackingForward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsuranceTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTermRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RdPaymentterms.ResumeLayout(False)
        Me.RdPaymentterms.PerformLayout()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBuyerPODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBuyerPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkPartPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAdvanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPartshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPre_Carriage_By, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPIDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCreditTermsName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFinal_Destination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPort_Discharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStuffing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtHSClassificationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBeneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.lblAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtOnAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblonAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRetained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblretained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBalancePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalancePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCAD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymenttermsgroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtINCOTERMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.cboPIStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMTUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPIDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.gvACInsurance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvACInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.MyLabel56, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddChargesForInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalInsuranceAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddChargesForInsurance1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeaderDiscountAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxableAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKindAttentation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblContentSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.RadPageViewPage6.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.lblWAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gvCategoryValue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategoryValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        CType(Me.gvTermsCdtion.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTermsCdtion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage7.ResumeLayout(False)
        Me.RadPageViewPage7.PerformLayout()
        CType(Me.gvSchedule.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNewHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_cancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
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
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblAmbendmentNoCaption As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents chkAgainst_RGP As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents txtRGPNo As common.UserControls.txtFinder
    Friend WithEvents btnPrintNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtQuotationNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtQuotationDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkExciseOnQty As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtTermRemark As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblDeliveryDate As common.Controls.MyLabel
    Friend WithEvents txtDeliveryDate As common.Controls.MyDateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDeliveryDuration As common.Controls.MyLabel
    Friend WithEvents txtDeliveryDuration As common.Controls.MyTextBox
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmtCopy As common.Controls.MyLabel
    Friend WithEvents chkpoclose As System.Windows.Forms.CheckBox
    Friend WithEvents btnCopy As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ReportFooter As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpExpiryDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblStateName As common.Controls.MyLabel
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents fndState As common.UserControls.txtFinder
    Friend WithEvents chkMCCPurchase As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents lblAmt As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.Controls.MyTextBox
    Friend WithEvents chkBlanket As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Chkroadpermit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_roadpermit As common.UserControls.MyRadGridView
    Friend WithEvents chk_c_form As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_c_form As common.UserControls.MyRadGridView
    Friend WithEvents btnForm_Update As Telerik.WinControls.UI.RadButton
    Friend WithEvents RdPaymentterms As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnMTUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtPIDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents lblBeneficiary As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtBeneficiary As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents TxtINCOTERMS As common.Controls.MyTextBox
    Friend WithEvents lblpaymenttermsgroup As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents fndPaymenttermsGroup As common.UserControls.txtFinder
    Friend WithEvents txtAdvance As common.MyNumBox
    Friend WithEvents lblAdvance As common.Controls.MyLabel
    Friend WithEvents TxtBalancePayment As common.MyNumBox
    Friend WithEvents lblBalancePayment As common.Controls.MyLabel
    Friend WithEvents TxtLC As common.MyNumBox
    Friend WithEvents lblLC As common.Controls.MyLabel
    Friend WithEvents TxtCAD As common.MyNumBox
    Friend WithEvents lblCad As common.Controls.MyLabel
    Friend WithEvents chkIsMerchantTrade As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtOnAccount As common.MyNumBox
    Friend WithEvents lblonAccount As common.Controls.MyLabel
    Friend WithEvents txtRetained As common.MyNumBox
    Friend WithEvents lblretained As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAmountinpercentage As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAmountinrupees As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents cboPIStatus As common.Controls.MyComboBox
    Friend WithEvents chkOpenPO As common.Controls.MyCheckBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpRenewal As common.Controls.MyDateTimePicker
    Friend WithEvents txtAgainstPO_No As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtInsuranceTerms As common.Controls.MyTextBox
    Friend WithEvents txtPaymentTerm As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtDelivery_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtDeliveryDesc As common.Controls.MyLabel
    Friend WithEvents chkAutoCalculate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblSubject As common.Controls.MyLabel
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents txtContentSubject As System.Windows.Forms.TextBox
    Friend WithEvents lblContentSubject As common.Controls.MyLabel
    Friend WithEvents txtKindAttentation As System.Windows.Forms.TextBox
    Friend WithEvents lblKindAttentation As common.Controls.MyLabel
    Friend WithEvents txtPINo As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents TxtHSClassificationNo As common.Controls.MyTextBox
    Friend WithEvents ChkISPO As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIsContent As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtPre_Carriage_By As common.Controls.MyComboBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtAdvance_Pers As common.MyNumBox
    Friend WithEvents cmbTerms_Payment As common.Controls.MyComboBox
    Friend WithEvents cmbTerms As common.Controls.MyComboBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtPIDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents FndCreditTerms As common.UserControls.txtFinder
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents TxtCreditTermsName As common.Controls.MyLabel
    Friend WithEvents fndCountry_Final_Destination As common.UserControls.txtFinder
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtFinal_Destination As common.Controls.MyTextBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents fndCountry_Origin As common.UserControls.txtFinder
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents txtPort_Discharge As common.Controls.MyTextBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents cboStuffing As common.Controls.MyComboBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents txtCarrier As common.Controls.MyTextBox
    Friend WithEvents chkTransshipment As common.Controls.MyCheckBox
    Friend WithEvents chkPartshipment As common.Controls.MyCheckBox
    Friend WithEvents dtpAcceptance As common.Controls.MyDateTimePicker
    Friend WithEvents chkAcceptance As common.Controls.MyCheckBox
    Friend WithEvents TxtCIF As common.MyNumBox
    Friend WithEvents lblCIF As common.Controls.MyLabel
    Friend WithEvents ChkPartPayment As common.Controls.MyCheckBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents cmbAdvanceType As common.Controls.MyComboBox
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents TxtBuyerPODate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents TxtBuyerPONo As common.Controls.MyTextBox
    Friend WithEvents btn_cancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnl_capex As System.Windows.Forms.Panel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents fndcapexsubcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexsubcode As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents fndcapexcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexcode As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamt As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamt As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddl_category As common.Controls.MyComboBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterTax As common.Controls.MyLabel
    Friend WithEvents chk_emergency As common.Controls.MyCheckBox
    Friend WithEvents chkReceiveControl As common.Controls.MyCheckBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents txt_deliverydays As common.MyNumBox
    Friend WithEvents lblBillNo As common.Controls.MyLabel
    Friend WithEvents txtBillNo As common.Controls.MyTextBox
    Friend WithEvents lblBillDate As common.Controls.MyLabel
    Friend WithEvents dtBillDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents gvTermsCdtion As common.UserControls.MyRadGridView
    Friend WithEvents gvCategoryValue As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents chkGSTRegistered As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents lblTaxableAmount As common.Controls.MyLabel
    Friend WithEvents chkJobWorkOutward As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel46 As common.Controls.MyLabel
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents MyLabel47 As common.Controls.MyLabel
    Friend WithEvents lblWPhone As common.Controls.MyTextBox
    Friend WithEvents lblWVendorName As common.Controls.MyTextBox
    Friend WithEvents lblWAddress As common.Controls.MyTextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblGstinNo As common.Controls.MyLabel
    Friend WithEvents MyLabel49 As common.Controls.MyLabel
    Friend WithEvents MyLabel45 As common.Controls.MyLabel
    Friend WithEvents lblRegisterOrUnregister As common.Controls.MyLabel
    Friend WithEvents chkRepair As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents lblVendorQuotationNo As common.Controls.MyLabel
    Friend WithEvents MyLabel50 As common.Controls.MyLabel
    Friend WithEvents MyLabel52 As common.Controls.MyLabel
    Friend WithEvents MyLabel51 As common.Controls.MyLabel
    Friend WithEvents MyLabel48 As common.Controls.MyLabel
    Friend WithEvents txtPackingForward As common.Controls.MyTextBox
    Friend WithEvents txtInsurance As common.Controls.MyTextBox
    Friend WithEvents txtFreight As common.Controls.MyTextBox
    Friend WithEvents txtHeaderDiscountAmount As common.MyNumBox
    Friend WithEvents MyLabel53 As common.Controls.MyLabel
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtReferencePO As common.Controls.MyTextBox
    Friend WithEvents chkTender As common.Controls.MyCheckBox
    Friend WithEvents MyLabel54 As common.Controls.MyLabel
    Friend WithEvents lblConfirmatory_PO_SRN_No As common.Controls.MyLabel
    Friend WithEvents txtRefTendorNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel55 As common.Controls.MyLabel
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents gvACInsurance As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel2 As RadPanel
    Friend WithEvents MyLabel56 As common.Controls.MyLabel
    Friend WithEvents lblAddChargesForInsurance As common.Controls.MyLabel
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents lblAddChargesForInsurance1 As common.Controls.MyLabel
    Friend WithEvents MyLabel58 As common.Controls.MyLabel
    Friend WithEvents lblTotalInsuranceAmt As common.Controls.MyLabel
    Friend WithEvents chkTDSApplied As common.Controls.MyCheckBox
    Friend WithEvents btnViewTDSDetails As RadButton
    Friend WithEvents btnNewHistory As RadButton
    Friend WithEvents MyLabel59 As common.Controls.MyLabel
    Friend WithEvents txtTenderNo As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage7 As RadPageViewPage
    Friend WithEvents gvSchedule As common.UserControls.MyRadGridView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtScheduleStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel60 As common.Controls.MyLabel
    Friend WithEvents MyLabel61 As common.Controls.MyLabel
End Class

