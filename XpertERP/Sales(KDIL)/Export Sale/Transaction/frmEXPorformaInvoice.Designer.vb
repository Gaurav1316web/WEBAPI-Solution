<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEXPorformaInvoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEXPorformaInvoice))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim WindowsSettings1 As Telerik.WinControls.WindowsSettings = New Telerik.WinControls.WindowsSettings()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtPort_of_Loading = New common.Controls.MyTextBox()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.chkIsTaxable = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtairwayline = New common.Controls.MyTextBox()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.TxtFinalGrossWeight = New common.MyNumBox()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.TxtMTPODate = New common.Controls.MyDateTimePicker()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.TxtMTPONo = New common.Controls.MyTextBox()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.txtPre_Carriage_By = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.cmbDocType = New common.Controls.MyComboBox()
        Me.chkTransshipment = New common.Controls.MyCheckBox()
        Me.chkPartshipment = New common.Controls.MyCheckBox()
        Me.dtpAcceptance = New common.Controls.MyDateTimePicker()
        Me.chkAcceptance = New common.Controls.MyCheckBox()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.txtAdvance_Pers = New common.MyNumBox()
        Me.txtOthr_Instructn = New common.Controls.MyTextBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.cmbTerms_Payment = New common.Controls.MyComboBox()
        Me.txtComm_Pay_name = New common.Controls.MyLabel()
        Me.cmbTerms = New common.Controls.MyComboBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.fndCountry_Final_Destination = New common.UserControls.txtFinder()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtFinal_Destination = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.fndComm_Pay_Code = New common.UserControls.txtFinder()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.fndCountry_Origin = New common.UserControls.txtFinder()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtPort_Discharge = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.cboStuffing = New common.Controls.MyComboBox()
        Me.cmbComm_Amount = New common.Controls.MyComboBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtAmt_comm = New common.MyNumBox()
        Me.txtExporter_Ref_No = New common.Controls.MyTextBox()
        Me.txtpodate = New common.Controls.MyDateTimePicker()
        Me.txtInvNo = New common.Controls.MyTextBox()
        Me.chkInternal = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.cmbComm_Payable = New common.Controls.MyComboBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtPONo = New common.Controls.MyTextBox()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.dtpChallan = New common.Controls.MyDateTimePicker()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblDept = New common.Controls.MyLabel()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.RadLabel28 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtIm_Ex_No = New common.Controls.MyTextBox()
        Me.txtCarrier = New common.Controls.MyTextBox()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_Notify_Party = New common.UserControls.MyRadGridView()
        Me.Grpjoint = New System.Windows.Forms.GroupBox()
        Me.txtAcc_No = New common.Controls.MyLabel()
        Me.txtIFSCCode = New common.Controls.MyLabel()
        Me.txtBankBranchName = New common.Controls.MyLabel()
        Me.txtbankCity = New common.Controls.MyLabel()
        Me.txtbankState = New common.Controls.MyLabel()
        Me.txtbankName = New common.Controls.MyLabel()
        Me.lblBankCity = New common.Controls.MyLabel()
        Me.lblBankName = New common.Controls.MyLabel()
        Me.lblBankBranch = New common.Controls.MyLabel()
        Me.lblIFCICode = New common.Controls.MyLabel()
        Me.lblAccountNo = New common.Controls.MyLabel()
        Me.txtBankBranchCode = New common.UserControls.txtFinder()
        Me.fndBankState = New common.UserControls.txtFinder()
        Me.lblBankState = New common.Controls.MyLabel()
        Me.fndBankCity = New common.UserControls.txtFinder()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtForm38 = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.chkAgainstCForm = New Telerik.WinControls.UI.RadCheckBox()
        Me.dtpInvoice = New common.Controls.MyDateTimePicker()
        Me.ddlInvoiceType = New common.Controls.MyComboBox()
        Me.chkCreateAutoReceipt = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtVehcileCode = New common.Controls.MyLabel()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.fndProject = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblProject = New common.Controls.MyLabel()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.txtPriceCode = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.txtPriceGroupCode = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RdPaymentterms = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtCIF = New common.MyNumBox()
        Me.lblCIF = New common.Controls.MyLabel()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.TxtHSClassificationNo = New common.Controls.MyTextBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAmountinpercentage = New System.Windows.Forms.RadioButton()
        Me.rdbAmountinrupees = New System.Windows.Forms.RadioButton()
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
        Me.lblAdvance = New common.Controls.MyLabel()
        Me.lblpaymenttermsgroup = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.fndPaymenttermsGroup = New common.UserControls.txtFinder()
        Me.lblBeneficiary = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.TxtBeneficiary = New common.UserControls.txtFinder()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.TxtINCOTERMS = New common.Controls.MyTextBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.btnReverseAndUnpost = New Telerik.WinControls.UI.RadButton()
        Me.lblInvoiceDiscAmt = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkDiscountOnAmt = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDiscountOnRate = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDiscAmt = New common.MyNumBox()
        Me.txtDiscPer = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmFormat1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmFormat2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnSendEmailSMS = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.txtPIDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtPort_of_Loading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtairwayline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFinalGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMTPODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMTPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPre_Carriage_By, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPartshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOthr_Instructn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComm_Pay_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFinal_Destination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPort_Discharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStuffing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbComm_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmt_comm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExporter_Ref_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtInvNo.SuspendLayout()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbComm_Payable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIm_Ex_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv_Notify_Party, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Notify_Party.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grpjoint.SuspendLayout()
        CType(Me.txtAcc_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIFCICode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankState, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtForm38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainstCForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateAutoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehcileCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.fndProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RdPaymentterms.SuspendLayout()
        CType(Me.TxtCIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtHSClassificationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
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
        CType(Me.lblAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymenttermsgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBeneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtINCOTERMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPIDate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1016, 488)
        Me.SplitContainer1.SplitterDistance = 456
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RdPaymentterms)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1016, 456)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtPort_of_Loading)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage1.Controls.Add(Me.chkIsTaxable)
        Me.RadPageViewPage1.Controls.Add(Me.txtairwayline)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel37)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFinalGrossWeight)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel43)
        Me.RadPageViewPage1.Controls.Add(Me.txtGrossWeight)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMTPODate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel35)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel36)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMTPONo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.txtPre_Carriage_By)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel28)
        Me.RadPageViewPage1.Controls.Add(Me.cmbDocType)
        Me.RadPageViewPage1.Controls.Add(Me.chkTransshipment)
        Me.RadPageViewPage1.Controls.Add(Me.chkPartshipment)
        Me.RadPageViewPage1.Controls.Add(Me.dtpAcceptance)
        Me.RadPageViewPage1.Controls.Add(Me.chkAcceptance)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdvance_Pers)
        Me.RadPageViewPage1.Controls.Add(Me.txtOthr_Instructn)
        Me.RadPageViewPage1.Controls.Add(Me.cmbTerms_Payment)
        Me.RadPageViewPage1.Controls.Add(Me.txtComm_Pay_name)
        Me.RadPageViewPage1.Controls.Add(Me.cmbTerms)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtDueDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtTermCode)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.fndCountry_Final_Destination)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.txtFinal_Destination)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.lblTermName)
        Me.RadPageViewPage1.Controls.Add(Me.fndComm_Pay_Code)
        Me.RadPageViewPage1.Controls.Add(Me.fndCountry_Origin)
        Me.RadPageViewPage1.Controls.Add(Me.txtPort_Discharge)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.cboStuffing)
        Me.RadPageViewPage1.Controls.Add(Me.cmbComm_Amount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.txtAmt_comm)
        Me.RadPageViewPage1.Controls.Add(Me.txtExporter_Ref_No)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.txtpodate)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.cmbComm_Payable)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.txtPONo)
        Me.RadPageViewPage1.Controls.Add(Me.btnDrillDown)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.dtpChallan)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblDept)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel28)
        Me.RadPageViewPage1.Controls.Add(Me.txtDept)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtIm_Ex_No)
        Me.RadPageViewPage1.Controls.Add(Me.txtCarrier)
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(102.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(995, 410)
        Me.RadPageViewPage1.Text = "Proforma Invoice"
        '
        'txtPort_of_Loading
        '
        Me.txtPort_of_Loading.CalculationExpression = Nothing
        Me.txtPort_of_Loading.FieldCode = Nothing
        Me.txtPort_of_Loading.FieldDesc = Nothing
        Me.txtPort_of_Loading.FieldMaxLength = 0
        Me.txtPort_of_Loading.FieldName = Nothing
        Me.txtPort_of_Loading.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort_of_Loading.isCalculatedField = False
        Me.txtPort_of_Loading.IsSourceFromTable = False
        Me.txtPort_of_Loading.IsSourceFromValueList = False
        Me.txtPort_of_Loading.IsUnique = False
        Me.txtPort_of_Loading.Location = New System.Drawing.Point(600, 107)
        Me.txtPort_of_Loading.MaxLength = 100
        Me.txtPort_of_Loading.MendatroryField = False
        Me.txtPort_of_Loading.MyLinkLable1 = Me.MyLabel38
        Me.txtPort_of_Loading.MyLinkLable2 = Nothing
        Me.txtPort_of_Loading.Name = "txtPort_of_Loading"
        Me.txtPort_of_Loading.ReferenceFieldDesc = Nothing
        Me.txtPort_of_Loading.ReferenceFieldName = Nothing
        Me.txtPort_of_Loading.ReferenceTableName = Nothing
        Me.txtPort_of_Loading.Size = New System.Drawing.Size(138, 18)
        Me.txtPort_of_Loading.TabIndex = 166
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(499, 109)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel38.TabIndex = 167
        Me.MyLabel38.Text = "Port of Loading"
        '
        'chkIsTaxable
        '
        Me.chkIsTaxable.Location = New System.Drawing.Point(851, 262)
        Me.chkIsTaxable.Name = "chkIsTaxable"
        Me.chkIsTaxable.Size = New System.Drawing.Size(69, 18)
        Me.chkIsTaxable.TabIndex = 165
        Me.chkIsTaxable.Text = "Is Taxable"
        '
        'txtairwayline
        '
        Me.txtairwayline.CalculationExpression = Nothing
        Me.txtairwayline.FieldCode = Nothing
        Me.txtairwayline.FieldDesc = Nothing
        Me.txtairwayline.FieldMaxLength = 0
        Me.txtairwayline.FieldName = Nothing
        Me.txtairwayline.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtairwayline.isCalculatedField = False
        Me.txtairwayline.IsSourceFromTable = False
        Me.txtairwayline.IsSourceFromValueList = False
        Me.txtairwayline.IsUnique = False
        Me.txtairwayline.Location = New System.Drawing.Point(835, 108)
        Me.txtairwayline.MaxLength = 50
        Me.txtairwayline.MendatroryField = False
        Me.txtairwayline.MyLinkLable1 = Me.MyLabel37
        Me.txtairwayline.MyLinkLable2 = Nothing
        Me.txtairwayline.Name = "txtairwayline"
        Me.txtairwayline.ReferenceFieldDesc = Nothing
        Me.txtairwayline.ReferenceFieldName = Nothing
        Me.txtairwayline.ReferenceTableName = Nothing
        Me.txtairwayline.Size = New System.Drawing.Size(160, 18)
        Me.txtairwayline.TabIndex = 163
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(740, 109)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel37.TabIndex = 164
        Me.MyLabel37.Text = "Ship/Airway Line"
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
        Me.txtRefNo.Location = New System.Drawing.Point(835, 221)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(160, 18)
        Me.txtRefNo.TabIndex = 22
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(744, 222)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel7.TabIndex = 37
        Me.RadLabel7.Text = "Ref No"
        '
        'TxtFinalGrossWeight
        '
        Me.TxtFinalGrossWeight.CalculationExpression = Nothing
        Me.TxtFinalGrossWeight.DecimalPlaces = 2
        Me.TxtFinalGrossWeight.FieldCode = Nothing
        Me.TxtFinalGrossWeight.FieldDesc = Nothing
        Me.TxtFinalGrossWeight.FieldMaxLength = 0
        Me.TxtFinalGrossWeight.FieldName = Nothing
        Me.TxtFinalGrossWeight.isCalculatedField = False
        Me.TxtFinalGrossWeight.IsSourceFromTable = False
        Me.TxtFinalGrossWeight.IsSourceFromValueList = False
        Me.TxtFinalGrossWeight.IsUnique = False
        Me.TxtFinalGrossWeight.Location = New System.Drawing.Point(359, 258)
        Me.TxtFinalGrossWeight.MendatroryField = False
        Me.TxtFinalGrossWeight.MyLinkLable1 = Me.MyLabel43
        Me.TxtFinalGrossWeight.MyLinkLable2 = Nothing
        Me.TxtFinalGrossWeight.Name = "TxtFinalGrossWeight"
        Me.TxtFinalGrossWeight.ReferenceFieldDesc = Nothing
        Me.TxtFinalGrossWeight.ReferenceFieldName = Nothing
        Me.TxtFinalGrossWeight.ReferenceTableName = Nothing
        Me.TxtFinalGrossWeight.Size = New System.Drawing.Size(139, 20)
        Me.TxtFinalGrossWeight.TabIndex = 162
        Me.TxtFinalGrossWeight.Text = "0"
        Me.TxtFinalGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFinalGrossWeight.Value = 0R
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(254, 260)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel43.TabIndex = 161
        Me.MyLabel43.Text = "Final Gross Weight"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 2
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(91, 258)
        Me.txtGrossWeight.MendatroryField = False
        Me.txtGrossWeight.MyLinkLable1 = Me.MyLabel42
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(157, 20)
        Me.txtGrossWeight.TabIndex = 160
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0R
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(2, 260)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel42.TabIndex = 159
        Me.MyLabel42.Text = "Gross Weight"
        '
        'TxtMTPODate
        '
        Me.TxtMTPODate.CalculationExpression = Nothing
        Me.TxtMTPODate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.TxtMTPODate.FieldCode = Nothing
        Me.TxtMTPODate.FieldDesc = Nothing
        Me.TxtMTPODate.FieldMaxLength = 0
        Me.TxtMTPODate.FieldName = Nothing
        Me.TxtMTPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtMTPODate.isCalculatedField = False
        Me.TxtMTPODate.IsSourceFromTable = False
        Me.TxtMTPODate.IsSourceFromValueList = False
        Me.TxtMTPODate.IsUnique = False
        Me.TxtMTPODate.Location = New System.Drawing.Point(835, 42)
        Me.TxtMTPODate.MendatroryField = False
        Me.TxtMTPODate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.TxtMTPODate.MyLinkLable1 = Nothing
        Me.TxtMTPODate.MyLinkLable2 = Nothing
        Me.TxtMTPODate.Name = "TxtMTPODate"
        Me.TxtMTPODate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.TxtMTPODate.ReferenceFieldDesc = Nothing
        Me.TxtMTPODate.ReferenceFieldName = Nothing
        Me.TxtMTPODate.ReferenceTableName = Nothing
        Me.TxtMTPODate.ShowCheckBox = True
        Me.TxtMTPODate.Size = New System.Drawing.Size(128, 20)
        Me.TxtMTPODate.TabIndex = 152
        Me.TxtMTPODate.TabStop = False
        Me.TxtMTPODate.Text = "18/06/2014 12:42 PM"
        Me.TxtMTPODate.Value = New Date(2014, 6, 18, 12, 42, 51, 794)
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(741, 45)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel35.TabIndex = 154
        Me.MyLabel35.Text = "PO Date"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(499, 45)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel36.TabIndex = 153
        Me.MyLabel36.Text = "PO No"
        '
        'TxtMTPONo
        '
        Me.TxtMTPONo.CalculationExpression = Nothing
        Me.TxtMTPONo.FieldCode = Nothing
        Me.TxtMTPONo.FieldDesc = Nothing
        Me.TxtMTPONo.FieldMaxLength = 0
        Me.TxtMTPONo.FieldName = Nothing
        Me.TxtMTPONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMTPONo.isCalculatedField = False
        Me.TxtMTPONo.IsSourceFromTable = False
        Me.TxtMTPONo.IsSourceFromValueList = False
        Me.TxtMTPONo.IsUnique = False
        Me.TxtMTPONo.Location = New System.Drawing.Point(600, 44)
        Me.TxtMTPONo.MaxLength = 200
        Me.TxtMTPONo.MendatroryField = False
        Me.TxtMTPONo.MyLinkLable1 = Me.MyLabel36
        Me.TxtMTPONo.MyLinkLable2 = Nothing
        Me.TxtMTPONo.Name = "TxtMTPONo"
        Me.TxtMTPONo.ReferenceFieldDesc = Nothing
        Me.TxtMTPONo.ReferenceFieldName = Nothing
        Me.TxtMTPONo.ReferenceTableName = Nothing
        Me.TxtMTPONo.Size = New System.Drawing.Size(138, 18)
        Me.TxtMTPONo.TabIndex = 151
        '
        'MyLabel29
        '
        Me.MyLabel29.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(500, 261)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(330, 16)
        Me.MyLabel29.TabIndex = 150
        Me.MyLabel29.Text = "Double click for selecting Pre-Carriage By on Pre-Carriage label."
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
        Me.cboItemType.Location = New System.Drawing.Point(349, 234)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel29
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(149, 20)
        Me.cboItemType.TabIndex = 19
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(254, 235)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel29.TabIndex = 0
        Me.RadLabel29.Text = "Item Type"
        '
        'txtPre_Carriage_By
        '
        Me.txtPre_Carriage_By.AutoSize = False
        Me.txtPre_Carriage_By.BorderVisible = True
        Me.txtPre_Carriage_By.FieldName = Nothing
        Me.txtPre_Carriage_By.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPre_Carriage_By.Location = New System.Drawing.Point(600, 86)
        Me.txtPre_Carriage_By.Name = "txtPre_Carriage_By"
        Me.txtPre_Carriage_By.Size = New System.Drawing.Size(138, 19)
        Me.txtPre_Carriage_By.TabIndex = 149
        Me.txtPre_Carriage_By.TextWrap = False
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(254, 214)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel28.TabIndex = 148
        Me.MyLabel28.Text = "Document Type"
        '
        'cmbDocType
        '
        Me.cmbDocType.AutoCompleteDisplayMember = Nothing
        Me.cmbDocType.AutoCompleteValueMember = Nothing
        Me.cmbDocType.CalculationExpression = Nothing
        Me.cmbDocType.DropDownAnimationEnabled = True
        Me.cmbDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbDocType.FieldCode = Nothing
        Me.cmbDocType.FieldDesc = Nothing
        Me.cmbDocType.FieldMaxLength = 0
        Me.cmbDocType.FieldName = Nothing
        Me.cmbDocType.isCalculatedField = False
        Me.cmbDocType.IsSourceFromTable = False
        Me.cmbDocType.IsSourceFromValueList = False
        Me.cmbDocType.IsUnique = False
        Me.cmbDocType.Location = New System.Drawing.Point(349, 213)
        Me.cmbDocType.MendatroryField = True
        Me.cmbDocType.MyLinkLable1 = Me.MyLabel28
        Me.cmbDocType.MyLinkLable2 = Nothing
        Me.cmbDocType.Name = "cmbDocType"
        Me.cmbDocType.ReferenceFieldDesc = Nothing
        Me.cmbDocType.ReferenceFieldName = Nothing
        Me.cmbDocType.ReferenceTableName = Nothing
        Me.cmbDocType.Size = New System.Drawing.Size(149, 20)
        Me.cmbDocType.TabIndex = 16
        '
        'chkTransshipment
        '
        Me.chkTransshipment.Location = New System.Drawing.Point(108, 232)
        Me.chkTransshipment.MyLinkLable1 = Nothing
        Me.chkTransshipment.MyLinkLable2 = Nothing
        Me.chkTransshipment.Name = "chkTransshipment"
        Me.chkTransshipment.Size = New System.Drawing.Size(105, 18)
        Me.chkTransshipment.TabIndex = 18
        Me.chkTransshipment.Tag1 = Nothing
        Me.chkTransshipment.Text = "Is Transshipment"
        '
        'chkPartshipment
        '
        Me.chkPartshipment.Location = New System.Drawing.Point(0, 232)
        Me.chkPartshipment.MyLinkLable1 = Nothing
        Me.chkPartshipment.MyLinkLable2 = Nothing
        Me.chkPartshipment.Name = "chkPartshipment"
        Me.chkPartshipment.Size = New System.Drawing.Size(102, 18)
        Me.chkPartshipment.TabIndex = 17
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
        Me.dtpAcceptance.Location = New System.Drawing.Point(169, 212)
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
        Me.dtpAcceptance.TabIndex = 15
        Me.dtpAcceptance.TabStop = False
        Me.dtpAcceptance.Text = "18/06/2014"
        Me.dtpAcceptance.Value = New Date(2014, 6, 18, 12, 42, 51, 794)
        '
        'chkAcceptance
        '
        Me.chkAcceptance.Location = New System.Drawing.Point(0, 212)
        Me.chkAcceptance.MyLinkLable1 = Nothing
        Me.chkAcceptance.MyLinkLable2 = Nothing
        Me.chkAcceptance.Name = "chkAcceptance"
        Me.chkAcceptance.Size = New System.Drawing.Size(167, 18)
        Me.chkAcceptance.TabIndex = 14
        Me.chkAcceptance.Tag1 = Nothing
        Me.chkAcceptance.Text = "PI Accepted Acceptance Date"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(0, 335)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.TabIndex = 146
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(499, 243)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel21.TabIndex = 20
        Me.MyLabel21.Text = "Terms of Payment"
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Location = New System.Drawing.Point(980, 241)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel23.TabIndex = 34
        Me.MyLabel23.Text = "%"
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
        Me.txtAdvance_Pers.Location = New System.Drawing.Point(949, 241)
        Me.txtAdvance_Pers.MaxLength = 3
        Me.txtAdvance_Pers.MendatroryField = True
        Me.txtAdvance_Pers.MyLinkLable1 = Nothing
        Me.txtAdvance_Pers.MyLinkLable2 = Nothing
        Me.txtAdvance_Pers.Name = "txtAdvance_Pers"
        Me.txtAdvance_Pers.ReferenceFieldDesc = Nothing
        Me.txtAdvance_Pers.ReferenceFieldName = Nothing
        Me.txtAdvance_Pers.ReferenceTableName = Nothing
        Me.txtAdvance_Pers.Size = New System.Drawing.Size(31, 20)
        Me.txtAdvance_Pers.TabIndex = 33
        Me.txtAdvance_Pers.Text = "0"
        Me.txtAdvance_Pers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance_Pers.Value = 0R
        '
        'txtOthr_Instructn
        '
        Me.txtOthr_Instructn.CalculationExpression = Nothing
        Me.txtOthr_Instructn.Enabled = False
        Me.txtOthr_Instructn.FieldCode = Nothing
        Me.txtOthr_Instructn.FieldDesc = Nothing
        Me.txtOthr_Instructn.FieldMaxLength = 0
        Me.txtOthr_Instructn.FieldName = Nothing
        Me.txtOthr_Instructn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthr_Instructn.isCalculatedField = False
        Me.txtOthr_Instructn.IsSourceFromTable = False
        Me.txtOthr_Instructn.IsSourceFromValueList = False
        Me.txtOthr_Instructn.IsUnique = False
        Me.txtOthr_Instructn.Location = New System.Drawing.Point(98, 192)
        Me.txtOthr_Instructn.MaxLength = 1000
        Me.txtOthr_Instructn.MendatroryField = False
        Me.txtOthr_Instructn.MyLinkLable1 = Me.MyLabel26
        Me.txtOthr_Instructn.MyLinkLable2 = Nothing
        Me.txtOthr_Instructn.Name = "txtOthr_Instructn"
        Me.txtOthr_Instructn.ReferenceFieldDesc = Nothing
        Me.txtOthr_Instructn.ReferenceFieldName = Nothing
        Me.txtOthr_Instructn.ReferenceTableName = Nothing
        Me.txtOthr_Instructn.Size = New System.Drawing.Size(400, 18)
        Me.txtOthr_Instructn.TabIndex = 13
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(-2, 193)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel26.TabIndex = 24
        Me.MyLabel26.Text = "Other Instructions"
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
        Me.cmbTerms_Payment.Location = New System.Drawing.Point(600, 241)
        Me.cmbTerms_Payment.MendatroryField = False
        Me.cmbTerms_Payment.MyLinkLable1 = Me.MyLabel21
        Me.cmbTerms_Payment.MyLinkLable2 = Nothing
        Me.cmbTerms_Payment.Name = "cmbTerms_Payment"
        Me.cmbTerms_Payment.ReferenceFieldDesc = Nothing
        Me.cmbTerms_Payment.ReferenceFieldName = Nothing
        Me.cmbTerms_Payment.ReferenceTableName = Nothing
        Me.cmbTerms_Payment.Size = New System.Drawing.Size(348, 20)
        Me.cmbTerms_Payment.TabIndex = 33
        '
        'txtComm_Pay_name
        '
        Me.txtComm_Pay_name.AutoSize = False
        Me.txtComm_Pay_name.BorderVisible = True
        Me.txtComm_Pay_name.FieldName = Nothing
        Me.txtComm_Pay_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComm_Pay_name.Location = New System.Drawing.Point(242, 167)
        Me.txtComm_Pay_name.Name = "txtComm_Pay_name"
        Me.txtComm_Pay_name.Size = New System.Drawing.Size(256, 19)
        Me.txtComm_Pay_name.TabIndex = 29
        Me.txtComm_Pay_name.TextWrap = False
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
        Me.cmbTerms.Location = New System.Drawing.Point(600, 219)
        Me.cmbTerms.MendatroryField = False
        Me.cmbTerms.MyLinkLable1 = Me.MyLabel22
        Me.cmbTerms.MyLinkLable2 = Nothing
        Me.cmbTerms.Name = "cmbTerms"
        Me.cmbTerms.ReferenceFieldDesc = Nothing
        Me.cmbTerms.ReferenceFieldName = Nothing
        Me.cmbTerms.ReferenceTableName = Nothing
        Me.cmbTerms.Size = New System.Drawing.Size(138, 20)
        Me.cmbTerms.TabIndex = 32
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(499, 221)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel22.TabIndex = 18
        Me.MyLabel22.Text = "Term Code"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(743, 199)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel20.TabIndex = 145
        Me.MyLabel20.Text = "Stuffing Status"
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
        Me.txtDueDate.Location = New System.Drawing.Point(600, 198)
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
        Me.txtDueDate.TabIndex = 30
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(499, 199)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 4
        Me.RadLabel17.Text = "Due Date"
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
        Me.txtTermCode.Location = New System.Drawing.Point(600, 177)
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
        Me.txtTermCode.Size = New System.Drawing.Size(138, 19)
        Me.txtTermCode.TabIndex = 29
        Me.txtTermCode.Value = ""
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(499, 179)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel16.TabIndex = 2
        Me.RadLabel16.Text = "Credit Terms"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(741, 177)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(254, 19)
        Me.lblTermName.TabIndex = 1
        Me.lblTermName.TextWrap = False
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
        Me.fndCountry_Final_Destination.Location = New System.Drawing.Point(835, 149)
        Me.fndCountry_Final_Destination.MendatroryField = False
        Me.fndCountry_Final_Destination.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry_Final_Destination.MyLinkLable1 = Me.MyLabel18
        Me.fndCountry_Final_Destination.MyLinkLable2 = Nothing
        Me.fndCountry_Final_Destination.MyReadOnly = False
        Me.fndCountry_Final_Destination.MyShowMasterFormButton = False
        Me.fndCountry_Final_Destination.Name = "fndCountry_Final_Destination"
        Me.fndCountry_Final_Destination.ReferenceFieldDesc = Nothing
        Me.fndCountry_Final_Destination.ReferenceFieldName = Nothing
        Me.fndCountry_Final_Destination.ReferenceTableName = Nothing
        Me.fndCountry_Final_Destination.Size = New System.Drawing.Size(160, 20)
        Me.fndCountry_Final_Destination.TabIndex = 28
        Me.fndCountry_Final_Destination.Value = ""
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(743, 149)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(86, 27)
        Me.MyLabel18.TabIndex = 12
        Me.MyLabel18.Text = "<html><p>Country of </p><p>Final Destination</p></html>"
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
        Me.txtFinal_Destination.Location = New System.Drawing.Point(835, 129)
        Me.txtFinal_Destination.MaxLength = 100
        Me.txtFinal_Destination.MendatroryField = False
        Me.txtFinal_Destination.MyLinkLable1 = Me.MyLabel16
        Me.txtFinal_Destination.MyLinkLable2 = Nothing
        Me.txtFinal_Destination.Name = "txtFinal_Destination"
        Me.txtFinal_Destination.ReferenceFieldDesc = Nothing
        Me.txtFinal_Destination.ReferenceFieldName = Nothing
        Me.txtFinal_Destination.ReferenceTableName = Nothing
        Me.txtFinal_Destination.Size = New System.Drawing.Size(160, 18)
        Me.txtFinal_Destination.TabIndex = 26
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(740, 129)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel16.TabIndex = 7
        Me.MyLabel16.Text = "Final Destination"
        '
        'fndComm_Pay_Code
        '
        Me.fndComm_Pay_Code.CalculationExpression = Nothing
        Me.fndComm_Pay_Code.FieldCode = Nothing
        Me.fndComm_Pay_Code.FieldDesc = Nothing
        Me.fndComm_Pay_Code.FieldMaxLength = 0
        Me.fndComm_Pay_Code.FieldName = Nothing
        Me.fndComm_Pay_Code.isCalculatedField = False
        Me.fndComm_Pay_Code.IsSourceFromTable = False
        Me.fndComm_Pay_Code.IsSourceFromValueList = False
        Me.fndComm_Pay_Code.IsUnique = False
        Me.fndComm_Pay_Code.Location = New System.Drawing.Point(98, 167)
        Me.fndComm_Pay_Code.MendatroryField = False
        Me.fndComm_Pay_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndComm_Pay_Code.MyLinkLable1 = Me.MyLabel24
        Me.fndComm_Pay_Code.MyLinkLable2 = Me.txtComm_Pay_name
        Me.fndComm_Pay_Code.MyReadOnly = False
        Me.fndComm_Pay_Code.MyShowMasterFormButton = False
        Me.fndComm_Pay_Code.Name = "fndComm_Pay_Code"
        Me.fndComm_Pay_Code.ReferenceFieldDesc = Nothing
        Me.fndComm_Pay_Code.ReferenceFieldName = Nothing
        Me.fndComm_Pay_Code.ReferenceTableName = Nothing
        Me.fndComm_Pay_Code.Size = New System.Drawing.Size(143, 19)
        Me.fndComm_Pay_Code.TabIndex = 12
        Me.fndComm_Pay_Code.Value = ""
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(-2, 167)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(70, 27)
        Me.MyLabel24.TabIndex = 18
        Me.MyLabel24.Text = "<html><p> Commission </p><p> Payable To</p></html>"
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
        Me.fndCountry_Origin.Location = New System.Drawing.Point(600, 149)
        Me.fndCountry_Origin.MendatroryField = False
        Me.fndCountry_Origin.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry_Origin.MyLinkLable1 = Me.MyLabel17
        Me.fndCountry_Origin.MyLinkLable2 = Nothing
        Me.fndCountry_Origin.MyReadOnly = False
        Me.fndCountry_Origin.MyShowMasterFormButton = False
        Me.fndCountry_Origin.Name = "fndCountry_Origin"
        Me.fndCountry_Origin.ReferenceFieldDesc = Nothing
        Me.fndCountry_Origin.ReferenceFieldName = Nothing
        Me.fndCountry_Origin.ReferenceTableName = Nothing
        Me.fndCountry_Origin.Size = New System.Drawing.Size(138, 19)
        Me.fndCountry_Origin.TabIndex = 27
        Me.fndCountry_Origin.Value = ""
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(499, 149)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(95, 27)
        Me.MyLabel17.TabIndex = 10
        Me.MyLabel17.Text = "<html><p> Country of Origin </p><p> Goods</p></html>"
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
        Me.txtPort_Discharge.Location = New System.Drawing.Point(600, 127)
        Me.txtPort_Discharge.MaxLength = 100
        Me.txtPort_Discharge.MendatroryField = False
        Me.txtPort_Discharge.MyLinkLable1 = Me.MyLabel15
        Me.txtPort_Discharge.MyLinkLable2 = Nothing
        Me.txtPort_Discharge.Name = "txtPort_Discharge"
        Me.txtPort_Discharge.ReferenceFieldDesc = Nothing
        Me.txtPort_Discharge.ReferenceFieldName = Nothing
        Me.txtPort_Discharge.ReferenceTableName = Nothing
        Me.txtPort_Discharge.Size = New System.Drawing.Size(138, 18)
        Me.txtPort_Discharge.TabIndex = 25
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(499, 127)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel15.TabIndex = 6
        Me.MyLabel15.Text = "Port of Discharge"
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
        Me.cboStuffing.Location = New System.Drawing.Point(835, 197)
        Me.cboStuffing.MendatroryField = True
        Me.cboStuffing.MyLinkLable1 = Me.MyLabel20
        Me.cboStuffing.MyLinkLable2 = Nothing
        Me.cboStuffing.Name = "cboStuffing"
        Me.cboStuffing.ReferenceFieldDesc = Nothing
        Me.cboStuffing.ReferenceFieldName = Nothing
        Me.cboStuffing.ReferenceTableName = Nothing
        Me.cboStuffing.Size = New System.Drawing.Size(160, 20)
        Me.cboStuffing.TabIndex = 31
        '
        'cmbComm_Amount
        '
        Me.cmbComm_Amount.AutoCompleteDisplayMember = Nothing
        Me.cmbComm_Amount.AutoCompleteValueMember = Nothing
        Me.cmbComm_Amount.CalculationExpression = Nothing
        Me.cmbComm_Amount.DropDownAnimationEnabled = True
        Me.cmbComm_Amount.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbComm_Amount.Enabled = False
        Me.cmbComm_Amount.FieldCode = Nothing
        Me.cmbComm_Amount.FieldDesc = Nothing
        Me.cmbComm_Amount.FieldMaxLength = 0
        Me.cmbComm_Amount.FieldName = Nothing
        Me.cmbComm_Amount.isCalculatedField = False
        Me.cmbComm_Amount.IsSourceFromTable = False
        Me.cmbComm_Amount.IsSourceFromValueList = False
        Me.cmbComm_Amount.IsUnique = False
        Me.cmbComm_Amount.Location = New System.Drawing.Point(398, 145)
        Me.cmbComm_Amount.MendatroryField = False
        Me.cmbComm_Amount.MyLinkLable1 = Me.MyLabel25
        Me.cmbComm_Amount.MyLinkLable2 = Nothing
        Me.cmbComm_Amount.Name = "cmbComm_Amount"
        Me.cmbComm_Amount.ReferenceFieldDesc = Nothing
        Me.cmbComm_Amount.ReferenceFieldName = Nothing
        Me.cmbComm_Amount.ReferenceTableName = Nothing
        Me.cmbComm_Amount.Size = New System.Drawing.Size(100, 20)
        Me.cmbComm_Amount.TabIndex = 11
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(153, 147)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel25.TabIndex = 22
        Me.MyLabel25.Text = "Amount of Commission"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(741, 66)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel19.TabIndex = 143
        Me.MyLabel19.Text = "Exporter Ref. No"
        '
        'txtAmt_comm
        '
        Me.txtAmt_comm.CalculationExpression = Nothing
        Me.txtAmt_comm.DecimalPlaces = 2
        Me.txtAmt_comm.Enabled = False
        Me.txtAmt_comm.FieldCode = Nothing
        Me.txtAmt_comm.FieldDesc = Nothing
        Me.txtAmt_comm.FieldMaxLength = 0
        Me.txtAmt_comm.FieldName = Nothing
        Me.txtAmt_comm.isCalculatedField = False
        Me.txtAmt_comm.IsSourceFromTable = False
        Me.txtAmt_comm.IsSourceFromValueList = False
        Me.txtAmt_comm.IsUnique = False
        Me.txtAmt_comm.Location = New System.Drawing.Point(283, 145)
        Me.txtAmt_comm.MendatroryField = False
        Me.txtAmt_comm.MyLinkLable1 = Me.MyLabel25
        Me.txtAmt_comm.MyLinkLable2 = Nothing
        Me.txtAmt_comm.Name = "txtAmt_comm"
        Me.txtAmt_comm.ReferenceFieldDesc = Nothing
        Me.txtAmt_comm.ReferenceFieldName = Nothing
        Me.txtAmt_comm.ReferenceTableName = Nothing
        Me.txtAmt_comm.Size = New System.Drawing.Size(111, 20)
        Me.txtAmt_comm.TabIndex = 10
        Me.txtAmt_comm.Text = "0"
        Me.txtAmt_comm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmt_comm.Value = 0R
        '
        'txtExporter_Ref_No
        '
        Me.txtExporter_Ref_No.CalculationExpression = Nothing
        Me.txtExporter_Ref_No.FieldCode = Nothing
        Me.txtExporter_Ref_No.FieldDesc = Nothing
        Me.txtExporter_Ref_No.FieldMaxLength = 0
        Me.txtExporter_Ref_No.FieldName = Nothing
        Me.txtExporter_Ref_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExporter_Ref_No.isCalculatedField = False
        Me.txtExporter_Ref_No.IsSourceFromTable = False
        Me.txtExporter_Ref_No.IsSourceFromValueList = False
        Me.txtExporter_Ref_No.IsUnique = False
        Me.txtExporter_Ref_No.Location = New System.Drawing.Point(835, 65)
        Me.txtExporter_Ref_No.MaxLength = 50
        Me.txtExporter_Ref_No.MendatroryField = False
        Me.txtExporter_Ref_No.MyLinkLable1 = Me.MyLabel19
        Me.txtExporter_Ref_No.MyLinkLable2 = Nothing
        Me.txtExporter_Ref_No.Name = "txtExporter_Ref_No"
        Me.txtExporter_Ref_No.ReferenceFieldDesc = Nothing
        Me.txtExporter_Ref_No.ReferenceFieldName = Nothing
        Me.txtExporter_Ref_No.ReferenceTableName = Nothing
        Me.txtExporter_Ref_No.Size = New System.Drawing.Size(160, 18)
        Me.txtExporter_Ref_No.TabIndex = 23
        '
        'txtpodate
        '
        Me.txtpodate.CalculationExpression = Nothing
        Me.txtpodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtpodate.FieldCode = Nothing
        Me.txtpodate.FieldDesc = Nothing
        Me.txtpodate.FieldMaxLength = 0
        Me.txtpodate.FieldName = Nothing
        Me.txtpodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtpodate.isCalculatedField = False
        Me.txtpodate.IsSourceFromTable = False
        Me.txtpodate.IsSourceFromValueList = False
        Me.txtpodate.IsUnique = False
        Me.txtpodate.Location = New System.Drawing.Point(835, 21)
        Me.txtpodate.MendatroryField = False
        Me.txtpodate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtpodate.MyLinkLable1 = Nothing
        Me.txtpodate.MyLinkLable2 = Nothing
        Me.txtpodate.Name = "txtpodate"
        Me.txtpodate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtpodate.ReferenceFieldDesc = Nothing
        Me.txtpodate.ReferenceFieldName = Nothing
        Me.txtpodate.ReferenceTableName = Nothing
        Me.txtpodate.Size = New System.Drawing.Size(128, 20)
        Me.txtpodate.TabIndex = 21
        Me.txtpodate.TabStop = False
        Me.txtpodate.Text = "18/06/2014 12:42 PM"
        Me.txtpodate.Value = New Date(2014, 6, 18, 12, 42, 51, 794)
        '
        'txtInvNo
        '
        Me.txtInvNo.CalculationExpression = Nothing
        Me.txtInvNo.Controls.Add(Me.chkInternal)
        Me.txtInvNo.FieldCode = Nothing
        Me.txtInvNo.FieldDesc = Nothing
        Me.txtInvNo.FieldMaxLength = 0
        Me.txtInvNo.FieldName = Nothing
        Me.txtInvNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNo.isCalculatedField = False
        Me.txtInvNo.IsSourceFromTable = False
        Me.txtInvNo.IsSourceFromValueList = False
        Me.txtInvNo.IsUnique = False
        Me.txtInvNo.Location = New System.Drawing.Point(1013, 202)
        Me.txtInvNo.MaxLength = 200
        Me.txtInvNo.MendatroryField = False
        Me.txtInvNo.MyLinkLable1 = Me.RadLabel6
        Me.txtInvNo.MyLinkLable2 = Nothing
        Me.txtInvNo.Name = "txtInvNo"
        Me.txtInvNo.ReferenceFieldDesc = Nothing
        Me.txtInvNo.ReferenceFieldName = Nothing
        Me.txtInvNo.ReferenceTableName = Nothing
        Me.txtInvNo.Size = New System.Drawing.Size(166, 18)
        Me.txtInvNo.TabIndex = 7
        Me.txtInvNo.Visible = False
        '
        'chkInternal
        '
        Me.chkInternal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInternal.Location = New System.Drawing.Point(0, 0)
        Me.chkInternal.Name = "chkInternal"
        Me.chkInternal.Size = New System.Drawing.Size(58, 16)
        Me.chkInternal.TabIndex = 18
        Me.chkInternal.Text = "Internal"
        Me.chkInternal.Visible = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(583, 300)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel6.TabIndex = 30
        Me.RadLabel6.Text = " Invoice No"
        Me.RadLabel6.Visible = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(499, 87)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel14.TabIndex = 5
        Me.MyLabel14.Text = "Pre-Carriage By"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(741, 24)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel13.TabIndex = 141
        Me.MyLabel13.Text = "Buyer PO Date"
        '
        'cmbComm_Payable
        '
        Me.cmbComm_Payable.AutoCompleteDisplayMember = Nothing
        Me.cmbComm_Payable.AutoCompleteValueMember = Nothing
        Me.cmbComm_Payable.CalculationExpression = Nothing
        Me.cmbComm_Payable.DropDownAnimationEnabled = True
        Me.cmbComm_Payable.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbComm_Payable.FieldCode = Nothing
        Me.cmbComm_Payable.FieldDesc = Nothing
        Me.cmbComm_Payable.FieldMaxLength = 0
        Me.cmbComm_Payable.FieldName = Nothing
        Me.cmbComm_Payable.isCalculatedField = False
        Me.cmbComm_Payable.IsSourceFromTable = False
        Me.cmbComm_Payable.IsSourceFromValueList = False
        Me.cmbComm_Payable.IsUnique = False
        Me.cmbComm_Payable.Location = New System.Drawing.Point(98, 145)
        Me.cmbComm_Payable.MendatroryField = False
        Me.cmbComm_Payable.MyLinkLable1 = Me.MyLabel27
        Me.cmbComm_Payable.MyLinkLable2 = Nothing
        Me.cmbComm_Payable.Name = "cmbComm_Payable"
        Me.cmbComm_Payable.ReferenceFieldDesc = Nothing
        Me.cmbComm_Payable.ReferenceFieldName = Nothing
        Me.cmbComm_Payable.ReferenceTableName = Nothing
        Me.cmbComm_Payable.Size = New System.Drawing.Size(50, 20)
        Me.cmbComm_Payable.TabIndex = 9
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(-2, 141)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(67, 27)
        Me.MyLabel27.TabIndex = 16
        Me.MyLabel27.Text = "<html><p> Commission</p><p> Payable</p></html>"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(499, 24)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel10.TabIndex = 140
        Me.MyLabel10.Text = "Buyer PO No"
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
        Me.txtPONo.Location = New System.Drawing.Point(600, 23)
        Me.txtPONo.MaxLength = 200
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyLinkLable1 = Me.MyLabel10
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(138, 18)
        Me.txtPONo.TabIndex = 20
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(743, 1)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDrillDown.TabIndex = 4
        '
        'MyLabel9
        '
        Me.MyLabel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(741, 340)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel9.TabIndex = 134
        Me.MyLabel9.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(844, 339)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(142, 18)
        Me.lblTotRAmt1.TabIndex = 34
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(-2, 64)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel2.TabIndex = 27
        Me.RadLabel2.Text = "Buyer Name"
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
        Me.txtVendorNo.Location = New System.Drawing.Point(98, 63)
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
        Me.txtVendorNo.Size = New System.Drawing.Size(143, 19)
        Me.txtVendorNo.TabIndex = 5
        Me.txtVendorNo.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(242, 63)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(256, 18)
        Me.lblVendorName.TabIndex = 10
        Me.lblVendorName.TextWrap = False
        '
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(241, 125)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(257, 18)
        Me.lblSalesman.TabIndex = 21
        Me.lblSalesman.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(-2, 126)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 54
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
        Me.txtSalesman.Location = New System.Drawing.Point(98, 125)
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
        Me.txtSalesman.Size = New System.Drawing.Size(143, 18)
        Me.txtSalesman.TabIndex = 8
        Me.txtSalesman.Value = ""
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
        Me.dtpChallan.Location = New System.Drawing.Point(1032, 125)
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
        Me.dtpChallan.TabIndex = 18
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
        Me.txtDate.Location = New System.Drawing.Point(376, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(122, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(345, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 23
        Me.RadLabel4.Text = "Date"
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(241, 105)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(257, 18)
        Me.lblDept.TabIndex = 18
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
        Me.txtReqNo.Location = New System.Drawing.Point(600, 1)
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
        Me.txtReqNo.Size = New System.Drawing.Size(138, 19)
        Me.txtReqNo.TabIndex = 2
        Me.txtReqNo.Value = ""
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(499, 2)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel24.TabIndex = 24
        Me.RadLabel24.Text = "SO No."
        '
        'RadLabel28
        '
        Me.RadLabel28.FieldName = Nothing
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(-2, 106)
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
        Me.txtDept.Location = New System.Drawing.Point(98, 105)
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
        Me.txtDept.Size = New System.Drawing.Size(143, 19)
        Me.txtDept.TabIndex = 7
        Me.txtDept.Value = ""
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(499, 66)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(95, 16)
        Me.RadLabel13.TabIndex = 34
        Me.RadLabel13.Text = "Import/ Export No"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(743, 82)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(89, 27)
        Me.RadLabel8.TabIndex = 36
        Me.RadLabel8.Text = "<html><p>Place of Receipt </p><p>of Pre-Carrier</p></html>"
        '
        'txtIm_Ex_No
        '
        Me.txtIm_Ex_No.CalculationExpression = Nothing
        Me.txtIm_Ex_No.FieldCode = Nothing
        Me.txtIm_Ex_No.FieldDesc = Nothing
        Me.txtIm_Ex_No.FieldMaxLength = 0
        Me.txtIm_Ex_No.FieldName = Nothing
        Me.txtIm_Ex_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIm_Ex_No.isCalculatedField = False
        Me.txtIm_Ex_No.IsSourceFromTable = False
        Me.txtIm_Ex_No.IsSourceFromValueList = False
        Me.txtIm_Ex_No.IsUnique = False
        Me.txtIm_Ex_No.Location = New System.Drawing.Point(600, 65)
        Me.txtIm_Ex_No.MaxLength = 50
        Me.txtIm_Ex_No.MendatroryField = False
        Me.txtIm_Ex_No.MyLinkLable1 = Me.RadLabel13
        Me.txtIm_Ex_No.MyLinkLable2 = Nothing
        Me.txtIm_Ex_No.Name = "txtIm_Ex_No"
        Me.txtIm_Ex_No.ReferenceFieldDesc = Nothing
        Me.txtIm_Ex_No.ReferenceFieldName = Nothing
        Me.txtIm_Ex_No.ReferenceTableName = Nothing
        Me.txtIm_Ex_No.Size = New System.Drawing.Size(138, 18)
        Me.txtIm_Ex_No.TabIndex = 22
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
        Me.txtCarrier.Location = New System.Drawing.Point(835, 85)
        Me.txtCarrier.MaxLength = 50
        Me.txtCarrier.MendatroryField = False
        Me.txtCarrier.MyLinkLable1 = Me.RadLabel8
        Me.txtCarrier.MyLinkLable2 = Nothing
        Me.txtCarrier.Name = "txtCarrier"
        Me.txtCarrier.ReferenceFieldDesc = Nothing
        Me.txtCarrier.ReferenceFieldName = Nothing
        Me.txtCarrier.ReferenceTableName = Nothing
        Me.txtCarrier.Size = New System.Drawing.Size(160, 18)
        Me.txtCarrier.TabIndex = 24
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(241, 84)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(257, 18)
        Me.lblShipToLocation.TabIndex = 14
        Me.lblShipToLocation.TextWrap = False
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(243, 42)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(255, 18)
        Me.lblBillToLocation.TabIndex = 7
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(-2, 22)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 28
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(-2, 85)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel18.TabIndex = 31
        Me.RadLabel18.Text = "Consignee"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(-2, 43)
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 280)
        Me.RadGroupBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(1, 15, 1, 1)
        Me.RadGroupBox2.Size = New System.Drawing.Size(993, 54)
        Me.RadGroupBox2.TabIndex = 35
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
        Me.gv1.Location = New System.Drawing.Point(1, 15)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(991, 38)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-2, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel1.TabIndex = 45
        Me.RadLabel1.Text = "PI No."
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(325, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(16, 20)
        Me.btnAddNew.TabIndex = 0
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
        Me.txtShipToLocation.Location = New System.Drawing.Point(98, 84)
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
        Me.txtShipToLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtShipToLocation.TabIndex = 6
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
        Me.txtBillToLocation.Location = New System.Drawing.Point(98, 42)
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
        Me.txtBillToLocation.Size = New System.Drawing.Size(144, 19)
        Me.txtBillToLocation.TabIndex = 4
        Me.txtBillToLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(896, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 26
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
        Me.txtDocNo.Size = New System.Drawing.Size(226, 20)
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
        Me.txtDesc.Location = New System.Drawing.Point(98, 22)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(400, 18)
        Me.txtDesc.TabIndex = 3
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage5.Controls.Add(Me.Grpjoint)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(136.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(995, 410)
        Me.RadPageViewPage5.Text = "Notify Party/Bank Detail"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv_Notify_Party)
        Me.RadGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = "Notified Party"
        Me.RadGroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(995, 277)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Notified Party"
        '
        'gv_Notify_Party
        '
        Me.gv_Notify_Party.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_Notify_Party.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_Notify_Party.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Notify_Party.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_Notify_Party.ForeColor = System.Drawing.Color.Black
        Me.gv_Notify_Party.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_Notify_Party.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv_Notify_Party.MasterTemplate.AllowDeleteRow = False
        Me.gv_Notify_Party.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_Notify_Party.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_Notify_Party.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv_Notify_Party.MyStopExport = False
        Me.gv_Notify_Party.Name = "gv_Notify_Party"
        Me.gv_Notify_Party.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_Notify_Party.ShowGroupPanel = False
        Me.gv_Notify_Party.ShowHeaderCellButtons = True
        Me.gv_Notify_Party.Size = New System.Drawing.Size(975, 247)
        Me.gv_Notify_Party.TabIndex = 0
        Me.gv_Notify_Party.TabStop = False
        '
        'Grpjoint
        '
        Me.Grpjoint.BackColor = System.Drawing.Color.Transparent
        Me.Grpjoint.Controls.Add(Me.txtAcc_No)
        Me.Grpjoint.Controls.Add(Me.txtIFSCCode)
        Me.Grpjoint.Controls.Add(Me.txtBankBranchName)
        Me.Grpjoint.Controls.Add(Me.txtbankCity)
        Me.Grpjoint.Controls.Add(Me.txtbankState)
        Me.Grpjoint.Controls.Add(Me.txtbankName)
        Me.Grpjoint.Controls.Add(Me.lblBankCity)
        Me.Grpjoint.Controls.Add(Me.lblBankName)
        Me.Grpjoint.Controls.Add(Me.lblBankBranch)
        Me.Grpjoint.Controls.Add(Me.lblIFCICode)
        Me.Grpjoint.Controls.Add(Me.lblAccountNo)
        Me.Grpjoint.Controls.Add(Me.txtBankBranchCode)
        Me.Grpjoint.Controls.Add(Me.fndBankState)
        Me.Grpjoint.Controls.Add(Me.lblBankState)
        Me.Grpjoint.Controls.Add(Me.fndBankCity)
        Me.Grpjoint.Controls.Add(Me.txtBankCode)
        Me.Grpjoint.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Grpjoint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grpjoint.ForeColor = System.Drawing.Color.Black
        Me.Grpjoint.Location = New System.Drawing.Point(0, 277)
        Me.Grpjoint.Name = "Grpjoint"
        Me.Grpjoint.Size = New System.Drawing.Size(995, 133)
        Me.Grpjoint.TabIndex = 112
        Me.Grpjoint.TabStop = False
        Me.Grpjoint.Text = "Bank Detail"
        '
        'txtAcc_No
        '
        Me.txtAcc_No.AutoSize = False
        Me.txtAcc_No.BorderVisible = True
        Me.txtAcc_No.FieldName = Nothing
        Me.txtAcc_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc_No.Location = New System.Drawing.Point(458, 108)
        Me.txtAcc_No.Name = "txtAcc_No"
        Me.txtAcc_No.Size = New System.Drawing.Size(212, 20)
        Me.txtAcc_No.TabIndex = 87
        Me.txtAcc_No.TextWrap = False
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.AutoSize = False
        Me.txtIFSCCode.BorderVisible = True
        Me.txtIFSCCode.FieldName = Nothing
        Me.txtIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIFSCCode.Location = New System.Drawing.Point(167, 108)
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.Size = New System.Drawing.Size(214, 20)
        Me.txtIFSCCode.TabIndex = 86
        Me.txtIFSCCode.TextWrap = False
        '
        'txtBankBranchName
        '
        Me.txtBankBranchName.AutoSize = False
        Me.txtBankBranchName.BorderVisible = True
        Me.txtBankBranchName.FieldName = Nothing
        Me.txtBankBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranchName.Location = New System.Drawing.Point(312, 86)
        Me.txtBankBranchName.Name = "txtBankBranchName"
        Me.txtBankBranchName.Size = New System.Drawing.Size(358, 20)
        Me.txtBankBranchName.TabIndex = 85
        Me.txtBankBranchName.TextWrap = False
        '
        'txtbankCity
        '
        Me.txtbankCity.AutoSize = False
        Me.txtbankCity.BorderVisible = True
        Me.txtbankCity.FieldName = Nothing
        Me.txtbankCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankCity.Location = New System.Drawing.Point(312, 64)
        Me.txtbankCity.Name = "txtbankCity"
        Me.txtbankCity.Size = New System.Drawing.Size(358, 20)
        Me.txtbankCity.TabIndex = 84
        Me.txtbankCity.TextWrap = False
        '
        'txtbankState
        '
        Me.txtbankState.AutoSize = False
        Me.txtbankState.BorderVisible = True
        Me.txtbankState.FieldName = Nothing
        Me.txtbankState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankState.Location = New System.Drawing.Point(312, 42)
        Me.txtbankState.Name = "txtbankState"
        Me.txtbankState.Size = New System.Drawing.Size(358, 20)
        Me.txtbankState.TabIndex = 83
        Me.txtbankState.TextWrap = False
        '
        'txtbankName
        '
        Me.txtbankName.AutoSize = False
        Me.txtbankName.BorderVisible = True
        Me.txtbankName.FieldName = Nothing
        Me.txtbankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankName.Location = New System.Drawing.Point(312, 20)
        Me.txtbankName.Name = "txtbankName"
        Me.txtbankName.Size = New System.Drawing.Size(358, 20)
        Me.txtbankName.TabIndex = 82
        Me.txtbankName.TextWrap = False
        '
        'lblBankCity
        '
        Me.lblBankCity.FieldName = Nothing
        Me.lblBankCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCity.Location = New System.Drawing.Point(57, 64)
        Me.lblBankCity.Name = "lblBankCity"
        Me.lblBankCity.Size = New System.Drawing.Size(26, 16)
        Me.lblBankCity.TabIndex = 81
        Me.lblBankCity.Text = "City"
        '
        'lblBankName
        '
        Me.lblBankName.FieldName = Nothing
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(57, 21)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(80, 16)
        Me.lblBankName.TabIndex = 70
        Me.lblBankName.Text = "Name Of Bank"
        '
        'lblBankBranch
        '
        Me.lblBankBranch.FieldName = Nothing
        Me.lblBankBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankBranch.Location = New System.Drawing.Point(57, 91)
        Me.lblBankBranch.Name = "lblBankBranch"
        Me.lblBankBranch.Size = New System.Drawing.Size(71, 16)
        Me.lblBankBranch.TabIndex = 72
        Me.lblBankBranch.Text = "Bank Branch"
        '
        'lblIFCICode
        '
        Me.lblIFCICode.FieldName = Nothing
        Me.lblIFCICode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIFCICode.Location = New System.Drawing.Point(57, 112)
        Me.lblIFCICode.Name = "lblIFCICode"
        Me.lblIFCICode.Size = New System.Drawing.Size(62, 16)
        Me.lblIFCICode.TabIndex = 74
        Me.lblIFCICode.Text = "IFSC Code"
        '
        'lblAccountNo
        '
        Me.lblAccountNo.FieldName = Nothing
        Me.lblAccountNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountNo.Location = New System.Drawing.Point(385, 110)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.Size = New System.Drawing.Size(68, 16)
        Me.lblAccountNo.TabIndex = 76
        Me.lblAccountNo.Text = "Account No."
        '
        'txtBankBranchCode
        '
        Me.txtBankBranchCode.CalculationExpression = Nothing
        Me.txtBankBranchCode.Enabled = False
        Me.txtBankBranchCode.FieldCode = Nothing
        Me.txtBankBranchCode.FieldDesc = Nothing
        Me.txtBankBranchCode.FieldMaxLength = 0
        Me.txtBankBranchCode.FieldName = Nothing
        Me.txtBankBranchCode.isCalculatedField = False
        Me.txtBankBranchCode.IsSourceFromTable = False
        Me.txtBankBranchCode.IsSourceFromValueList = False
        Me.txtBankBranchCode.IsUnique = False
        Me.txtBankBranchCode.Location = New System.Drawing.Point(167, 87)
        Me.txtBankBranchCode.MendatroryField = False
        Me.txtBankBranchCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranchCode.MyLinkLable1 = Me.lblBankBranch
        Me.txtBankBranchCode.MyLinkLable2 = Me.txtBankBranchName
        Me.txtBankBranchCode.MyReadOnly = False
        Me.txtBankBranchCode.MyShowMasterFormButton = False
        Me.txtBankBranchCode.Name = "txtBankBranchCode"
        Me.txtBankBranchCode.ReferenceFieldDesc = Nothing
        Me.txtBankBranchCode.ReferenceFieldName = Nothing
        Me.txtBankBranchCode.ReferenceTableName = Nothing
        Me.txtBankBranchCode.Size = New System.Drawing.Size(141, 19)
        Me.txtBankBranchCode.TabIndex = 1
        Me.txtBankBranchCode.Value = ""
        '
        'fndBankState
        '
        Me.fndBankState.CalculationExpression = Nothing
        Me.fndBankState.Enabled = False
        Me.fndBankState.FieldCode = Nothing
        Me.fndBankState.FieldDesc = Nothing
        Me.fndBankState.FieldMaxLength = 0
        Me.fndBankState.FieldName = Nothing
        Me.fndBankState.isCalculatedField = False
        Me.fndBankState.IsSourceFromTable = False
        Me.fndBankState.IsSourceFromValueList = False
        Me.fndBankState.IsUnique = False
        Me.fndBankState.Location = New System.Drawing.Point(167, 42)
        Me.fndBankState.MendatroryField = False
        Me.fndBankState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankState.MyLinkLable1 = Me.lblBankState
        Me.fndBankState.MyLinkLable2 = Me.txtbankState
        Me.fndBankState.MyReadOnly = False
        Me.fndBankState.MyShowMasterFormButton = False
        Me.fndBankState.Name = "fndBankState"
        Me.fndBankState.ReferenceFieldDesc = Nothing
        Me.fndBankState.ReferenceFieldName = Nothing
        Me.fndBankState.ReferenceTableName = Nothing
        Me.fndBankState.Size = New System.Drawing.Size(141, 19)
        Me.fndBankState.TabIndex = 2
        Me.fndBankState.Value = ""
        '
        'lblBankState
        '
        Me.lblBankState.FieldName = Nothing
        Me.lblBankState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankState.Location = New System.Drawing.Point(57, 43)
        Me.lblBankState.Name = "lblBankState"
        Me.lblBankState.Size = New System.Drawing.Size(33, 16)
        Me.lblBankState.TabIndex = 78
        Me.lblBankState.Text = "State"
        '
        'fndBankCity
        '
        Me.fndBankCity.CalculationExpression = Nothing
        Me.fndBankCity.Enabled = False
        Me.fndBankCity.FieldCode = Nothing
        Me.fndBankCity.FieldDesc = Nothing
        Me.fndBankCity.FieldMaxLength = 0
        Me.fndBankCity.FieldName = Nothing
        Me.fndBankCity.isCalculatedField = False
        Me.fndBankCity.IsSourceFromTable = False
        Me.fndBankCity.IsSourceFromValueList = False
        Me.fndBankCity.IsUnique = False
        Me.fndBankCity.Location = New System.Drawing.Point(167, 64)
        Me.fndBankCity.MendatroryField = False
        Me.fndBankCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCity.MyLinkLable1 = Me.lblBankCity
        Me.fndBankCity.MyLinkLable2 = Me.txtbankCity
        Me.fndBankCity.MyReadOnly = False
        Me.fndBankCity.MyShowMasterFormButton = False
        Me.fndBankCity.Name = "fndBankCity"
        Me.fndBankCity.ReferenceFieldDesc = Nothing
        Me.fndBankCity.ReferenceFieldName = Nothing
        Me.fndBankCity.ReferenceTableName = Nothing
        Me.fndBankCity.Size = New System.Drawing.Size(141, 19)
        Me.fndBankCity.TabIndex = 3
        Me.fndBankCity.Value = ""
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
        Me.txtBankCode.Location = New System.Drawing.Point(167, 20)
        Me.txtBankCode.MendatroryField = False
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Me.lblBankName
        Me.txtBankCode.MyLinkLable2 = Me.txtbankName
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(141, 19)
        Me.txtBankCode.TabIndex = 0
        Me.txtBankCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage2.Controls.Add(Me.txtForm38)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage2.Controls.Add(Me.chkAgainstCForm)
        Me.RadPageViewPage2.Controls.Add(Me.dtpInvoice)
        Me.RadPageViewPage2.Controls.Add(Me.ddlInvoiceType)
        Me.RadPageViewPage2.Controls.Add(Me.chkCreateAutoReceipt)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage2.Controls.Add(Me.txtVehicleNo)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage2.Controls.Add(Me.txtVehcileCode)
        Me.RadPageViewPage2.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage2.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage2.Controls.Add(Me.lblRouteDesc)
        Me.RadPageViewPage2.Controls.Add(Me.lblPriceCode)
        Me.RadPageViewPage2.Controls.Add(Me.txtRouteNo)
        Me.RadPageViewPage2.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.txtPriceGroupCode)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(995, 410)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(596, 323)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 5
        Me.chkOnHold.Text = "On Hold"
        Me.chkOnHold.Visible = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(2, 321)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel12.TabIndex = 33
        Me.MyLabel12.Text = "Form 38 No"
        Me.MyLabel12.Visible = False
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
        Me.txtForm38.Location = New System.Drawing.Point(101, 320)
        Me.txtForm38.MaxLength = 200
        Me.txtForm38.MendatroryField = False
        Me.txtForm38.MyLinkLable1 = Nothing
        Me.txtForm38.MyLinkLable2 = Nothing
        Me.txtForm38.Name = "txtForm38"
        Me.txtForm38.ReferenceFieldDesc = Nothing
        Me.txtForm38.ReferenceFieldName = Nothing
        Me.txtForm38.ReferenceTableName = Nothing
        Me.txtForm38.Size = New System.Drawing.Size(104, 18)
        Me.txtForm38.TabIndex = 17
        Me.txtForm38.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(232, 299)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel7.TabIndex = 42
        Me.MyLabel7.Text = "Invoice Type"
        Me.MyLabel7.Visible = False
        '
        'chkAgainstCForm
        '
        Me.chkAgainstCForm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainstCForm.Location = New System.Drawing.Point(131, 299)
        Me.chkAgainstCForm.Name = "chkAgainstCForm"
        '
        '
        '
        Me.chkAgainstCForm.RootElement.StretchHorizontally = True
        Me.chkAgainstCForm.RootElement.StretchVertically = True
        Me.chkAgainstCForm.Size = New System.Drawing.Size(98, 16)
        Me.chkAgainstCForm.TabIndex = 16
        Me.chkAgainstCForm.Text = "Against C Form"
        Me.chkAgainstCForm.Visible = False
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
        Me.dtpInvoice.Location = New System.Drawing.Point(477, 299)
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
        Me.dtpInvoice.TabIndex = 0
        Me.dtpInvoice.TabStop = False
        Me.dtpInvoice.Text = "17/05/2011"
        Me.dtpInvoice.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        Me.dtpInvoice.Visible = False
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
        Me.ddlInvoiceType.Location = New System.Drawing.Point(307, 297)
        Me.ddlInvoiceType.MendatroryField = True
        Me.ddlInvoiceType.MyLinkLable1 = Me.MyLabel7
        Me.ddlInvoiceType.MyLinkLable2 = Nothing
        Me.ddlInvoiceType.Name = "ddlInvoiceType"
        Me.ddlInvoiceType.ReferenceFieldDesc = Nothing
        Me.ddlInvoiceType.ReferenceFieldName = Nothing
        Me.ddlInvoiceType.ReferenceTableName = Nothing
        Me.ddlInvoiceType.Size = New System.Drawing.Size(164, 20)
        Me.ddlInvoiceType.TabIndex = 14
        Me.ddlInvoiceType.Visible = False
        '
        'chkCreateAutoReceipt
        '
        Me.chkCreateAutoReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateAutoReceipt.Location = New System.Drawing.Point(3, 299)
        Me.chkCreateAutoReceipt.Name = "chkCreateAutoReceipt"
        Me.chkCreateAutoReceipt.Size = New System.Drawing.Size(122, 16)
        Me.chkCreateAutoReceipt.TabIndex = 13
        Me.chkCreateAutoReceipt.Text = "Create Auto Receipt"
        Me.chkCreateAutoReceipt.Visible = False
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(3, 279)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel5.TabIndex = 35
        Me.RadLabel5.Text = "Vehicle No"
        Me.RadLabel5.Visible = False
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AutoSize = False
        Me.txtVehicleNo.BorderVisible = True
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.Location = New System.Drawing.Point(246, 277)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(287, 18)
        Me.txtVehicleNo.TabIndex = 24
        Me.txtVehicleNo.TextWrap = False
        Me.txtVehicleNo.Visible = False
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(671, 279)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 36
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        '
        'txtVehcileCode
        '
        Me.txtVehcileCode.AutoSize = False
        Me.txtVehcileCode.BorderVisible = True
        Me.txtVehcileCode.FieldName = Nothing
        Me.txtVehcileCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehcileCode.Location = New System.Drawing.Point(103, 277)
        Me.txtVehcileCode.Name = "txtVehcileCode"
        Me.txtVehcileCode.Size = New System.Drawing.Size(141, 20)
        Me.txtVehcileCode.TabIndex = 23
        Me.txtVehcileCode.TextWrap = False
        Me.txtVehcileCode.Visible = False
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(384, 345)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(457, 22)
        Me.pnlPCJ.TabIndex = 56
        Me.pnlPCJ.Visible = False
        '
        'fndProject
        '
        Me.fndProject.AutoSize = False
        Me.fndProject.BorderVisible = True
        Me.fndProject.FieldName = Nothing
        Me.fndProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.Location = New System.Drawing.Point(97, 1)
        Me.fndProject.Name = "fndProject"
        Me.fndProject.Size = New System.Drawing.Size(104, 20)
        Me.fndProject.TabIndex = 0
        Me.fndProject.TextWrap = False
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
        Me.lblProject.Location = New System.Drawing.Point(213, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(236, 20)
        Me.lblProject.TabIndex = 1
        Me.lblProject.TextWrap = False
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(3, 257)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 124
        Me.lblRouteNo.Text = "Route No"
        Me.lblRouteNo.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel2.Location = New System.Drawing.Point(551, 261)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(438, 16)
        Me.MyLabel2.TabIndex = 25
        Me.MyLabel2.Text = "Press Ctrl+F7 on Current Row For Manully Insert Rate and Amount or vise versa"
        Me.MyLabel2.Visible = False
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(247, 255)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(286, 17)
        Me.lblRouteDesc.TabIndex = 28
        Me.lblRouteDesc.Visible = False
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Location = New System.Drawing.Point(2, 233)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 121
        Me.lblPriceCode.Text = "Price Code"
        Me.lblPriceCode.Visible = False
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
        Me.txtRouteNo.Location = New System.Drawing.Point(103, 255)
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
        Me.txtRouteNo.Size = New System.Drawing.Size(142, 20)
        Me.txtRouteNo.TabIndex = 8
        Me.txtRouteNo.Value = ""
        Me.txtRouteNo.Visible = False
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(102, 233)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(141, 19)
        Me.txtPriceCode.TabIndex = 29
        Me.txtPriceCode.TextWrap = False
        Me.txtPriceCode.Visible = False
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
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(285, 233)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel8.TabIndex = 30
        Me.MyLabel8.Text = "Price Group Code"
        Me.MyLabel8.Visible = False
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
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextWrap = False
        '
        'txtPriceGroupCode
        '
        Me.txtPriceGroupCode.AutoSize = False
        Me.txtPriceGroupCode.BorderVisible = True
        Me.txtPriceGroupCode.FieldName = Nothing
        Me.txtPriceGroupCode.Location = New System.Drawing.Point(391, 233)
        Me.txtPriceGroupCode.Name = "txtPriceGroupCode"
        Me.txtPriceGroupCode.Size = New System.Drawing.Size(141, 19)
        Me.txtPriceGroupCode.TabIndex = 31
        Me.txtPriceGroupCode.TextWrap = False
        Me.txtPriceGroupCode.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(837, 251)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
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
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(990, 193)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        '
        'RdPaymentterms
        '
        Me.RdPaymentterms.Controls.Add(Me.TxtCIF)
        Me.RdPaymentterms.Controls.Add(Me.lblCIF)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel30)
        Me.RdPaymentterms.Controls.Add(Me.TxtHSClassificationNo)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel32)
        Me.RdPaymentterms.Controls.Add(Me.RadGroupBox6)
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
        Me.RdPaymentterms.Controls.Add(Me.lblAdvance)
        Me.RdPaymentterms.Controls.Add(Me.lblpaymenttermsgroup)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel33)
        Me.RdPaymentterms.Controls.Add(Me.fndPaymenttermsGroup)
        Me.RdPaymentterms.Controls.Add(Me.lblBeneficiary)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel31)
        Me.RdPaymentterms.Controls.Add(Me.TxtBeneficiary)
        Me.RdPaymentterms.Controls.Add(Me.MyLabel34)
        Me.RdPaymentterms.Controls.Add(Me.TxtINCOTERMS)
        Me.RdPaymentterms.ItemSize = New System.Drawing.SizeF(96.0!, 26.0!)
        Me.RdPaymentterms.Location = New System.Drawing.Point(10, 35)
        Me.RdPaymentterms.Name = "RdPaymentterms"
        Me.RdPaymentterms.Size = New System.Drawing.Size(995, 410)
        Me.RdPaymentterms.Text = "Payment Terms"
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
        Me.TxtCIF.Location = New System.Drawing.Point(134, 278)
        Me.TxtCIF.MendatroryField = False
        Me.TxtCIF.MyLinkLable1 = Nothing
        Me.TxtCIF.MyLinkLable2 = Nothing
        Me.TxtCIF.Name = "TxtCIF"
        Me.TxtCIF.ReferenceFieldDesc = Nothing
        Me.TxtCIF.ReferenceFieldName = Nothing
        Me.TxtCIF.ReferenceTableName = Nothing
        Me.TxtCIF.Size = New System.Drawing.Size(143, 20)
        Me.TxtCIF.TabIndex = 1496
        Me.TxtCIF.Text = "0"
        Me.TxtCIF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCIF.Value = 0R
        '
        'lblCIF
        '
        Me.lblCIF.FieldName = Nothing
        Me.lblCIF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCIF.Location = New System.Drawing.Point(12, 282)
        Me.lblCIF.Name = "lblCIF"
        Me.lblCIF.Size = New System.Drawing.Size(24, 16)
        Me.lblCIF.TabIndex = 1497
        Me.lblCIF.Text = "CIF"
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(11, 61)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(113, 16)
        Me.MyLabel30.TabIndex = 1495
        Me.MyLabel30.Text = "HS Classification No."
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
        Me.TxtHSClassificationNo.Location = New System.Drawing.Point(134, 61)
        Me.TxtHSClassificationNo.MaxLength = 30
        Me.TxtHSClassificationNo.MendatroryField = False
        Me.TxtHSClassificationNo.MyLinkLable1 = Me.MyLabel30
        Me.TxtHSClassificationNo.MyLinkLable2 = Nothing
        Me.TxtHSClassificationNo.Name = "TxtHSClassificationNo"
        Me.TxtHSClassificationNo.ReferenceFieldDesc = Nothing
        Me.TxtHSClassificationNo.ReferenceFieldName = Nothing
        Me.TxtHSClassificationNo.ReferenceTableName = Nothing
        Me.TxtHSClassificationNo.Size = New System.Drawing.Size(175, 18)
        Me.TxtHSClassificationNo.TabIndex = 1494
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(13, 113)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel32.TabIndex = 1492
        Me.MyLabel32.Text = "Apply on Amount"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbAmountinpercentage)
        Me.RadGroupBox6.Controls.Add(Me.rdbAmountinrupees)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(134, 108)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(143, 21)
        Me.RadGroupBox6.TabIndex = 1491
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
        Me.TxtOnAccount.Location = New System.Drawing.Point(134, 231)
        Me.TxtOnAccount.MendatroryField = False
        Me.TxtOnAccount.MyLinkLable1 = Nothing
        Me.TxtOnAccount.MyLinkLable2 = Nothing
        Me.TxtOnAccount.Name = "TxtOnAccount"
        Me.TxtOnAccount.ReferenceFieldDesc = Nothing
        Me.TxtOnAccount.ReferenceFieldName = Nothing
        Me.TxtOnAccount.ReferenceTableName = Nothing
        Me.TxtOnAccount.Size = New System.Drawing.Size(143, 20)
        Me.TxtOnAccount.TabIndex = 1489
        Me.TxtOnAccount.Text = "0"
        Me.TxtOnAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOnAccount.Value = 0R
        '
        'lblonAccount
        '
        Me.lblonAccount.FieldName = Nothing
        Me.lblonAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblonAccount.Location = New System.Drawing.Point(12, 235)
        Me.lblonAccount.Name = "lblonAccount"
        Me.lblonAccount.Size = New System.Drawing.Size(65, 16)
        Me.lblonAccount.TabIndex = 1490
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
        Me.txtRetained.Location = New System.Drawing.Point(134, 255)
        Me.txtRetained.MendatroryField = False
        Me.txtRetained.MyLinkLable1 = Nothing
        Me.txtRetained.MyLinkLable2 = Nothing
        Me.txtRetained.Name = "txtRetained"
        Me.txtRetained.ReferenceFieldDesc = Nothing
        Me.txtRetained.ReferenceFieldName = Nothing
        Me.txtRetained.ReferenceTableName = Nothing
        Me.txtRetained.Size = New System.Drawing.Size(143, 20)
        Me.txtRetained.TabIndex = 1487
        Me.txtRetained.Text = "0"
        Me.txtRetained.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRetained.Value = 0R
        '
        'lblretained
        '
        Me.lblretained.FieldName = Nothing
        Me.lblretained.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblretained.Location = New System.Drawing.Point(12, 259)
        Me.lblretained.Name = "lblretained"
        Me.lblretained.Size = New System.Drawing.Size(52, 16)
        Me.lblretained.TabIndex = 1488
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
        Me.TxtBalancePayment.Location = New System.Drawing.Point(134, 183)
        Me.TxtBalancePayment.MendatroryField = False
        Me.TxtBalancePayment.MyLinkLable1 = Nothing
        Me.TxtBalancePayment.MyLinkLable2 = Nothing
        Me.TxtBalancePayment.Name = "TxtBalancePayment"
        Me.TxtBalancePayment.ReferenceFieldDesc = Nothing
        Me.TxtBalancePayment.ReferenceFieldName = Nothing
        Me.TxtBalancePayment.ReferenceTableName = Nothing
        Me.TxtBalancePayment.Size = New System.Drawing.Size(143, 20)
        Me.TxtBalancePayment.TabIndex = 1485
        Me.TxtBalancePayment.Text = "0"
        Me.TxtBalancePayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBalancePayment.Value = 0R
        '
        'lblBalancePayment
        '
        Me.lblBalancePayment.FieldName = Nothing
        Me.lblBalancePayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalancePayment.Location = New System.Drawing.Point(12, 187)
        Me.lblBalancePayment.Name = "lblBalancePayment"
        Me.lblBalancePayment.Size = New System.Drawing.Size(95, 16)
        Me.lblBalancePayment.TabIndex = 1486
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
        Me.TxtLC.Location = New System.Drawing.Point(134, 159)
        Me.TxtLC.MendatroryField = False
        Me.TxtLC.MyLinkLable1 = Nothing
        Me.TxtLC.MyLinkLable2 = Nothing
        Me.TxtLC.Name = "TxtLC"
        Me.TxtLC.ReferenceFieldDesc = Nothing
        Me.TxtLC.ReferenceFieldName = Nothing
        Me.TxtLC.ReferenceTableName = Nothing
        Me.TxtLC.Size = New System.Drawing.Size(143, 20)
        Me.TxtLC.TabIndex = 1483
        Me.TxtLC.Text = "0"
        Me.TxtLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLC.Value = 0R
        '
        'lblLC
        '
        Me.lblLC.FieldName = Nothing
        Me.lblLC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLC.Location = New System.Drawing.Point(12, 164)
        Me.lblLC.Name = "lblLC"
        Me.lblLC.Size = New System.Drawing.Size(21, 16)
        Me.lblLC.TabIndex = 1484
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
        Me.TxtCAD.Location = New System.Drawing.Point(134, 207)
        Me.TxtCAD.MendatroryField = False
        Me.TxtCAD.MyLinkLable1 = Nothing
        Me.TxtCAD.MyLinkLable2 = Nothing
        Me.TxtCAD.Name = "TxtCAD"
        Me.TxtCAD.ReferenceFieldDesc = Nothing
        Me.TxtCAD.ReferenceFieldName = Nothing
        Me.TxtCAD.ReferenceTableName = Nothing
        Me.TxtCAD.Size = New System.Drawing.Size(143, 20)
        Me.TxtCAD.TabIndex = 1481
        Me.TxtCAD.Text = "0"
        Me.TxtCAD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCAD.Value = 0R
        '
        'lblCad
        '
        Me.lblCad.FieldName = Nothing
        Me.lblCad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCad.Location = New System.Drawing.Point(12, 211)
        Me.lblCad.Name = "lblCad"
        Me.lblCad.Size = New System.Drawing.Size(30, 16)
        Me.lblCad.TabIndex = 1482
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
        Me.txtAdvance.Location = New System.Drawing.Point(134, 135)
        Me.txtAdvance.MendatroryField = False
        Me.txtAdvance.MyLinkLable1 = Nothing
        Me.txtAdvance.MyLinkLable2 = Nothing
        Me.txtAdvance.Name = "txtAdvance"
        Me.txtAdvance.ReferenceFieldDesc = Nothing
        Me.txtAdvance.ReferenceFieldName = Nothing
        Me.txtAdvance.ReferenceTableName = Nothing
        Me.txtAdvance.Size = New System.Drawing.Size(143, 20)
        Me.txtAdvance.TabIndex = 1479
        Me.txtAdvance.Text = "0"
        Me.txtAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance.Value = 0R
        '
        'lblAdvance
        '
        Me.lblAdvance.FieldName = Nothing
        Me.lblAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvance.Location = New System.Drawing.Point(12, 139)
        Me.lblAdvance.Name = "lblAdvance"
        Me.lblAdvance.Size = New System.Drawing.Size(50, 16)
        Me.lblAdvance.TabIndex = 1480
        Me.lblAdvance.Text = "Advance"
        '
        'lblpaymenttermsgroup
        '
        Me.lblpaymenttermsgroup.AutoSize = False
        Me.lblpaymenttermsgroup.BorderVisible = True
        Me.lblpaymenttermsgroup.FieldName = Nothing
        Me.lblpaymenttermsgroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymenttermsgroup.Location = New System.Drawing.Point(281, 84)
        Me.lblpaymenttermsgroup.Name = "lblpaymenttermsgroup"
        Me.lblpaymenttermsgroup.Size = New System.Drawing.Size(236, 19)
        Me.lblpaymenttermsgroup.TabIndex = 1478
        Me.lblpaymenttermsgroup.TextWrap = False
        '
        'MyLabel33
        '
        Me.MyLabel33.AutoSize = False
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(12, 77)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(87, 32)
        Me.MyLabel33.TabIndex = 1477
        Me.MyLabel33.Text = "Payment Terms Group"
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
        Me.fndPaymenttermsGroup.Location = New System.Drawing.Point(134, 82)
        Me.fndPaymenttermsGroup.MendatroryField = True
        Me.fndPaymenttermsGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPaymenttermsGroup.MyLinkLable1 = Me.MyLabel33
        Me.fndPaymenttermsGroup.MyLinkLable2 = Me.lblpaymenttermsgroup
        Me.fndPaymenttermsGroup.MyReadOnly = False
        Me.fndPaymenttermsGroup.MyShowMasterFormButton = False
        Me.fndPaymenttermsGroup.Name = "fndPaymenttermsGroup"
        Me.fndPaymenttermsGroup.ReferenceFieldDesc = Nothing
        Me.fndPaymenttermsGroup.ReferenceFieldName = Nothing
        Me.fndPaymenttermsGroup.ReferenceTableName = Nothing
        Me.fndPaymenttermsGroup.Size = New System.Drawing.Size(143, 22)
        Me.fndPaymenttermsGroup.TabIndex = 1476
        Me.fndPaymenttermsGroup.Value = ""
        '
        'lblBeneficiary
        '
        Me.lblBeneficiary.AutoSize = False
        Me.lblBeneficiary.BorderVisible = True
        Me.lblBeneficiary.FieldName = Nothing
        Me.lblBeneficiary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeneficiary.Location = New System.Drawing.Point(279, 16)
        Me.lblBeneficiary.Name = "lblBeneficiary"
        Me.lblBeneficiary.Size = New System.Drawing.Size(236, 19)
        Me.lblBeneficiary.TabIndex = 1475
        Me.lblBeneficiary.TextWrap = False
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(12, 17)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel31.TabIndex = 1474
        Me.MyLabel31.Text = "Beneficiary"
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
        Me.TxtBeneficiary.Location = New System.Drawing.Point(134, 14)
        Me.TxtBeneficiary.MendatroryField = True
        Me.TxtBeneficiary.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBeneficiary.MyLinkLable1 = Me.MyLabel31
        Me.TxtBeneficiary.MyLinkLable2 = Me.lblBeneficiary
        Me.TxtBeneficiary.MyReadOnly = False
        Me.TxtBeneficiary.MyShowMasterFormButton = False
        Me.TxtBeneficiary.Name = "TxtBeneficiary"
        Me.TxtBeneficiary.ReferenceFieldDesc = Nothing
        Me.TxtBeneficiary.ReferenceFieldName = Nothing
        Me.TxtBeneficiary.ReferenceTableName = Nothing
        Me.TxtBeneficiary.Size = New System.Drawing.Size(143, 22)
        Me.TxtBeneficiary.TabIndex = 1473
        Me.TxtBeneficiary.Value = ""
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(12, 40)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel34.TabIndex = 1472
        Me.MyLabel34.Text = "INCOTERMS"
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
        Me.TxtINCOTERMS.Location = New System.Drawing.Point(134, 40)
        Me.TxtINCOTERMS.MaxLength = 200
        Me.TxtINCOTERMS.MendatroryField = False
        Me.TxtINCOTERMS.MyLinkLable1 = Me.MyLabel34
        Me.TxtINCOTERMS.MyLinkLable2 = Nothing
        Me.TxtINCOTERMS.Name = "TxtINCOTERMS"
        Me.TxtINCOTERMS.ReferenceFieldDesc = Nothing
        Me.TxtINCOTERMS.ReferenceFieldName = Nothing
        Me.TxtINCOTERMS.ReferenceTableName = Nothing
        Me.TxtINCOTERMS.Size = New System.Drawing.Size(383, 18)
        Me.TxtINCOTERMS.TabIndex = 1471
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(995, 410)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(995, 410)
        Me.SplitContainer2.SplitterDistance = 371
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
        Me.gvAC.MyStopExport = False
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(995, 371)
        Me.gvAC.TabIndex = 1
        Me.gvAC.TabStop = False
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(750, 3)
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
        Me.lblAddCharges.Location = New System.Drawing.Point(882, 3)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 127
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(995, 410)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(995, 410)
        Me.UcCustomFields1.TabIndex = 1
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtComment)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage4.Controls.Add(Me.btnReverseAndUnpost)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscPer)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(995, 410)
        Me.RadPageViewPage4.Text = "Total"
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(125, 194)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 161
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(212, 193)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(109, 18)
        Me.lblTaxAmt.TabIndex = 160
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(466, 53)
        Me.txtComment.MaxLength = 5000
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComment.Size = New System.Drawing.Size(355, 197)
        Me.txtComment.TabIndex = 158
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(343, 57)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(117, 16)
        Me.MyLabel11.TabIndex = 159
        Me.MyLabel11.Text = "Terms and Conditions"
        '
        'btnReverseAndUnpost
        '
        Me.btnReverseAndUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnpost.Location = New System.Drawing.Point(6, 276)
        Me.btnReverseAndUnpost.Name = "btnReverseAndUnpost"
        Me.btnReverseAndUnpost.Size = New System.Drawing.Size(268, 18)
        Me.btnReverseAndUnpost.TabIndex = 9
        Me.btnReverseAndUnpost.Text = "Reverse and Unpost"
        Me.btnReverseAndUnpost.Visible = False
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(211, 127)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 7
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(65, 127)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 157
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(135, 79)
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
        Me.RadGroupBox3.Location = New System.Drawing.Point(210, 79)
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
        Me.txtDiscAmt.Location = New System.Drawing.Point(271, 103)
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
        Me.txtDiscPer.Location = New System.Drawing.Point(210, 103)
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
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(252, 105)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel5.TabIndex = 155
        Me.MyLabel5.Text = "%"
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(85, 9)
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
        Me.txtConversionRate.Location = New System.Drawing.Point(330, 11)
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
        Me.txtCurrencyCode.Location = New System.Drawing.Point(62, 9)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 20)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(456, 12)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 2
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(550, 12)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(176, 18)
        Me.txtApplicableFrom.TabIndex = 3
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(6, 11)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 137
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(238, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 139
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(63, 216)
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
        Me.lblAddCharges1.Location = New System.Drawing.Point(211, 215)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 13
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(83, 172)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(103, 238)
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
        Me.lblTotRAmt.Location = New System.Drawing.Point(211, 237)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 14
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(211, 171)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 11
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(211, 149)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 10
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(211, 56)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 2
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(104, 150)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(17, 57)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(995, 410)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(995, 410)
        Me.UcAttachment1.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(554, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 22)
        Me.btnCancel.TabIndex = 44
        Me.btnCancel.Text = "Cancel"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmFormat1, Me.rmFormat2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(223, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(76, 20)
        Me.RadSplitButton1.TabIndex = 43
        Me.RadSplitButton1.Text = "Print"
        '
        'rmFormat1
        '
        Me.rmFormat1.AccessibleDescription = "Export PI Format1"
        Me.rmFormat1.AccessibleName = "Export PI Format1"
        Me.rmFormat1.Name = "rmFormat1"
        Me.rmFormat1.Text = "Performa Invoice Format1"
        '
        'rmFormat2
        '
        Me.rmFormat2.AccessibleDescription = "Export PI Format2"
        Me.rmFormat2.AccessibleName = "Export PI Format2"
        Me.rmFormat2.Name = "rmFormat2"
        Me.rmFormat2.Text = "Performa Invoice Format2"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(302, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(87, 21)
        Me.btnHistory.TabIndex = 4
        Me.btnHistory.Text = "Amendment"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSendEmailSMS})
        Me.btnsetting.Location = New System.Drawing.Point(394, 5)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(85, 21)
        Me.btnsetting.TabIndex = 5
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnSendEmailSMS
        '
        Me.btnSendEmailSMS.Name = "btnSendEmailSMS"
        Me.btnSendEmailSMS.Text = "SendEmailSMS"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(481, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(78, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(151, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(943, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1016, 20)
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
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "E-Mail/SMS Setting"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1016, 488)
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
        Me.txtPIDate.MyLinkLable1 = Nothing
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
        'frmEXPorformaInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 508)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmEXPorformaInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Export Proforma Invoice"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtPort_of_Loading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtairwayline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFinalGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMTPODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMTPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPre_Carriage_By, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPartshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOthr_Instructn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComm_Pay_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFinal_Destination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPort_Discharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStuffing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbComm_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmt_comm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExporter_Ref_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtInvNo.ResumeLayout(False)
        Me.txtInvNo.PerformLayout()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbComm_Payable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIm_Ex_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv_Notify_Party.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Notify_Party, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grpjoint.ResumeLayout(False)
        Me.Grpjoint.PerformLayout()
        CType(Me.txtAcc_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIFCICode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankState, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtForm38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainstCForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateAutoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehcileCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.fndProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RdPaymentterms.ResumeLayout(False)
        Me.RdPaymentterms.PerformLayout()
        CType(Me.TxtCIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtHSClassificationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
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
        CType(Me.lblAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymenttermsgroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBeneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtINCOTERMS, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPIDate, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtShipToLocation As common.UserControls.txtFinder
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents txtInvNo As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtIm_Ex_No As common.Controls.MyTextBox
    Friend WithEvents txtCarrier As common.Controls.MyTextBox
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents cboItemType As common.Controls.MyComboBox
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
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
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
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
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
    Friend WithEvents chkCreateAutoReceipt As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents chkAgainstCForm As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents fndProject As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblProject As common.Controls.MyLabel
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
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents ddlInvoiceType As common.Controls.MyComboBox
    Friend WithEvents txtPriceGroupCode As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtVehcileCode As common.Controls.MyLabel
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverseAndUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtForm38 As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtpodate As common.Controls.MyDateTimePicker
    Friend WithEvents btnSendEmailSMS As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents fndCountry_Origin As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents fndCountry_Final_Destination As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtFinal_Destination As common.Controls.MyTextBox
    Friend WithEvents txtPort_Discharge As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtExporter_Ref_No As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents cboStuffing As common.Controls.MyComboBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents cmbTerms_Payment As common.Controls.MyComboBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents cmbTerms As common.Controls.MyComboBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents txtAdvance_Pers As common.MyNumBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Notify_Party As common.UserControls.MyRadGridView
    Friend WithEvents txtComm_Pay_name As common.Controls.MyLabel
    Friend WithEvents fndComm_Pay_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents cmbComm_Amount As common.Controls.MyComboBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents txtAmt_comm As common.MyNumBox
    Friend WithEvents txtOthr_Instructn As common.Controls.MyTextBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents cmbComm_Payable As common.Controls.MyComboBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents chkAcceptance As common.Controls.MyCheckBox
    Friend WithEvents dtpAcceptance As common.Controls.MyDateTimePicker
    Friend WithEvents chkPartshipment As common.Controls.MyCheckBox
    Friend WithEvents chkTransshipment As common.Controls.MyCheckBox
    Friend WithEvents Grpjoint As System.Windows.Forms.GroupBox
    Friend WithEvents lblBankCity As common.Controls.MyLabel
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents lblBankBranch As common.Controls.MyLabel
    Friend WithEvents lblIFCICode As common.Controls.MyLabel
    Friend WithEvents lblAccountNo As common.Controls.MyLabel
    Friend WithEvents txtBankBranchCode As common.UserControls.txtFinder
    Friend WithEvents lblBankState As common.Controls.MyLabel
    Friend WithEvents fndBankState As common.UserControls.txtFinder
    Friend WithEvents fndBankCity As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents txtbankState As common.Controls.MyLabel
    Friend WithEvents txtbankName As common.Controls.MyLabel
    Friend WithEvents txtBankBranchName As common.Controls.MyLabel
    Friend WithEvents txtbankCity As common.Controls.MyLabel
    Friend WithEvents txtAcc_No As common.Controls.MyLabel
    Friend WithEvents txtIFSCCode As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents cmbDocType As common.Controls.MyComboBox
    Friend WithEvents txtPre_Carriage_By As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents RdPaymentterms As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtCIF As common.MyNumBox
    Friend WithEvents lblCIF As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents TxtHSClassificationNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAmountinpercentage As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAmountinrupees As System.Windows.Forms.RadioButton
    Friend WithEvents TxtOnAccount As common.MyNumBox
    Friend WithEvents lblonAccount As common.Controls.MyLabel
    Friend WithEvents txtRetained As common.MyNumBox
    Friend WithEvents lblretained As common.Controls.MyLabel
    Friend WithEvents TxtBalancePayment As common.MyNumBox
    Friend WithEvents lblBalancePayment As common.Controls.MyLabel
    Friend WithEvents TxtLC As common.MyNumBox
    Friend WithEvents lblLC As common.Controls.MyLabel
    Friend WithEvents TxtCAD As common.MyNumBox
    Friend WithEvents lblCad As common.Controls.MyLabel
    Friend WithEvents txtAdvance As common.MyNumBox
    Friend WithEvents lblAdvance As common.Controls.MyLabel
    Friend WithEvents lblpaymenttermsgroup As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents fndPaymenttermsGroup As common.UserControls.txtFinder
    Friend WithEvents lblBeneficiary As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents TxtBeneficiary As common.UserControls.txtFinder
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents TxtINCOTERMS As common.Controls.MyTextBox
    Friend WithEvents txtPIDate As common.Controls.MyDateTimePicker
    Friend WithEvents TxtMTPODate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents TxtMTPONo As common.Controls.MyTextBox
    Friend WithEvents TxtFinalGrossWeight As common.MyNumBox
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmFormat1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmFormat2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtairwayline As common.Controls.MyTextBox
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents chkIsTaxable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents txtPort_of_Loading As common.Controls.MyTextBox
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
End Class

