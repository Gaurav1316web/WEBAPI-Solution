<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaymentNew
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.ChkRetention = New Telerik.WinControls.UI.RadCheckBox()
        Me.grpVendorBankDetails = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtVendor_Bank_ACNo = New common.Controls.MyTextBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtVendor_bankcode = New common.Controls.MyTextBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.TxtVendorBank_IFSCCode = New common.Controls.MyTextBox()
        Me.txtVendorBank_branchname = New common.Controls.MyTextBox()
        Me.TxtVendor_BankName = New common.Controls.MyTextBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.LblLocDesp = New common.Controls.MyLabel()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.RadLabel18 = New Telerik.WinControls.UI.RadLabel()
        Me.pnlmemorndm = New System.Windows.Forms.Panel()
        Me.chkmemorndm = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtmemoamt = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtTotalPaymentBaseCurr = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtBaseCurrency = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkTDSProvision = New common.Controls.MyCheckBox()
        Me.chkSaving = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFarmerLoanPayment = New Telerik.WinControls.UI.RadCheckBox()
        Me.pnlMiscPayment = New System.Windows.Forms.Panel()
        Me.txtRemitTo = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtTotalAppliedAmt = New common.Controls.MyTextBox()
        Me.chkIsReceipt = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnImportPosted = New Telerik.WinControls.UI.RadButton()
        Me.ddlEmployeeAdvanceType = New common.Controls.MyComboBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.rbtnExportPosted = New Telerik.WinControls.UI.RadButton()
        Me.lblMPAdv = New common.Controls.MyLabel()
        Me.txtMPAdv = New common.UserControls.txtFinder()
        Me.ddlEmployeeType = New common.Controls.MyComboBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.btnOk = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtTapalNo = New common.Controls.MyTextBox()
        Me.chkOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.txtDataAndTimeSelection = New common.Controls.MyDateTimePicker()
        Me.chkBankChargesWaveOff = New Telerik.WinControls.UI.RadCheckBox()
        Me.pnlEMI = New System.Windows.Forms.Panel()
        Me.txtNoOfEMI = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtInterestRate = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndloanNo = New common.UserControls.txtFinder()
        Me.lblApplyLoanNo = New common.Controls.MyLabel()
        Me.ChkAdvSalary = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblCustomerOutStanding = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.ChkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.LblPONo = New common.Controls.MyLabel()
        Me.txtPONo = New common.UserControls.txtFinder()
        Me.pnlPJC = New System.Windows.Forms.Panel()
        Me.lblProjDesc = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblEmpDesc = New common.Controls.MyLabel()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.lblProjCode = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.pnlCform = New System.Windows.Forms.Panel()
        Me.chkCForm = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtCFormInvNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtBankCharges = New common.MyNumBox()
        Me.lblLoadOutNo = New common.Controls.MyLabel()
        Me.txtLoadOutno = New common.UserControls.txtFinder()
        Me.txtPaymentAmt = New common.MyNumBox()
        Me.pnlVendor = New System.Windows.Forms.Panel()
        Me.LblAccPayee = New common.Controls.MyLabel()
        Me.lblBalAmt = New common.Controls.MyLabel()
        Me.lblDocumentNo = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtFinder()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.txtVendorCode = New common.UserControls.txtFinder()
        Me.lblvendorcode = New common.Controls.MyLabel()
        Me.pnlCheque = New System.Windows.Forms.Panel()
        Me.ChkAccPayee = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCheckPrint = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkPDC = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblchequeno = New common.Controls.MyLabel()
        Me.txtChequeNo = New common.Controls.MyTextBox()
        Me.lblchequedate = New common.Controls.MyLabel()
        Me.dtpChequeDate = New common.Controls.MyDateTimePicker()
        Me.lblBankDesc = New common.Controls.MyLabel()
        Me.pnlAdvance = New System.Windows.Forms.Panel()
        Me.lblNetPayableAmt = New common.Controls.MyLabel()
        Me.txtNetPayableAmt = New common.Controls.MyTextBox()
        Me.lblTDSAmt = New common.Controls.MyLabel()
        Me.txtTDSAmt = New common.Controls.MyTextBox()
        Me.dtpPayment = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblTotPayment = New common.Controls.MyLabel()
        Me.lbldesc = New common.Controls.MyLabel()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtPaymentNo = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.ddlPaymentType = New common.Controls.MyComboBox()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.lblpaymenttype = New common.Controls.MyLabel()
        Me.gvDetails = New common.UserControls.MyRadGridView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.TabForGST = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.LBLPO_Location_GST = New common.Controls.MyLabel()
        Me.TxtPO_Location_GST = New common.UserControls.txtFinder()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.lblPOTotalAdditionalCharge = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lblPOTotalTaxAmt = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.LblPOTotalAmount = New common.Controls.MyLabel()
        Me.lblDONo = New common.Controls.MyLabel()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.txtPONo_GST = New common.UserControls.txtFinder()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.gvTaxDetail = New common.UserControls.MyRadGridView()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.TabBankChargesTax = New Telerik.WinControls.UI.RadPageViewPage()
        Me.butCostCenterAndHirerachy_Update_AfterPost = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroupBankCharges = New common.UserControls.txtFinder()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblBankChargTaxGroup = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnOpenBankCashBook = New Telerik.WinControls.UI.RadButton()
        Me.btnPaymentAdvice = New Telerik.WinControls.UI.RadButton()
        Me.btnRtgs = New Telerik.WinControls.UI.RadButton()
        Me.btnVoidCheck = New Telerik.WinControls.UI.RadButton()
        Me.btnChqUpdate = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintCheck = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.BtnBank = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.ChkRetention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpVendorBankDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVendorBankDetails.SuspendLayout()
        CType(Me.txtVendor_Bank_ACNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendor_bankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtVendorBank_IFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorBank_branchname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtVendor_BankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlmemorndm.SuspendLayout()
        CType(Me.chkmemorndm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmemoamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPaymentBaseCurr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkTDSProvision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFarmerLoanPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMiscPayment.SuspendLayout()
        CType(Me.txtRemitTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAppliedAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnImportPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlEmployeeAdvanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnExportPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMPAdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlEmployeeType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTapalNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDataAndTimeSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBankChargesWaveOff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEMI.SuspendLayout()
        CType(Me.txtNoOfEMI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplyLoanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAdvSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerOutStanding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPJC.SuspendLayout()
        CType(Me.lblProjDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProjCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCform.SuspendLayout()
        CType(Me.chkCForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoadOutNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVendor.SuspendLayout()
        CType(Me.LblAccPayee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCheque.SuspendLayout()
        CType(Me.ChkAccPayee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChequeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdvance.SuspendLayout()
        CType(Me.lblNetPayableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetPayableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTDSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTDSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymenttype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.TabForGST.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.LBLPO_Location_GST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPOTotalAdditionalCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPOTotalTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblPOTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gvTaxDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabBankChargesTax.SuspendLayout()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankChargTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPaymentAdvice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRtgs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnVoidCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnChqUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOpenBankCashBook)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPaymentAdvice)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRtgs)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnVoidCheck)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnChqUpdate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintCheck)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnBank)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1134, 538)
        Me.SplitContainer1.SplitterDistance = 509
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.TabForGST)
        Me.RadPageView1.Controls.Add(Me.TabBankChargesTax)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1134, 489)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(56.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1113, 445)
        Me.RadPageViewPage1.Text = "Payment"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkRetention)
        Me.SplitContainer2.Panel1.Controls.Add(Me.grpVendorBankDetails)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblLocDesp)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel18)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlmemorndm)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlCurrConv)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvDetails)
        Me.SplitContainer2.Size = New System.Drawing.Size(1113, 445)
        Me.SplitContainer2.SplitterDistance = 290
        Me.SplitContainer2.TabIndex = 0
        '
        'ChkRetention
        '
        Me.ChkRetention.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkRetention.Location = New System.Drawing.Point(595, 235)
        Me.ChkRetention.Name = "ChkRetention"
        Me.ChkRetention.Size = New System.Drawing.Size(69, 18)
        Me.ChkRetention.TabIndex = 12138
        Me.ChkRetention.Text = "Retention"
        '
        'grpVendorBankDetails
        '
        Me.grpVendorBankDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpVendorBankDetails.Controls.Add(Me.txtVendor_Bank_ACNo)
        Me.grpVendorBankDetails.Controls.Add(Me.MyLabel17)
        Me.grpVendorBankDetails.Controls.Add(Me.txtVendor_bankcode)
        Me.grpVendorBankDetails.Controls.Add(Me.TxtVendorBank_IFSCCode)
        Me.grpVendorBankDetails.Controls.Add(Me.MyLabel22)
        Me.grpVendorBankDetails.Controls.Add(Me.txtVendorBank_branchname)
        Me.grpVendorBankDetails.Controls.Add(Me.TxtVendor_BankName)
        Me.grpVendorBankDetails.Controls.Add(Me.MyLabel23)
        Me.grpVendorBankDetails.HeaderText = "Vendor Bank Details"
        Me.grpVendorBankDetails.Location = New System.Drawing.Point(711, 208)
        Me.grpVendorBankDetails.Name = "grpVendorBankDetails"
        Me.grpVendorBankDetails.Size = New System.Drawing.Size(385, 80)
        Me.grpVendorBankDetails.TabIndex = 612
        Me.grpVendorBankDetails.Text = "Vendor Bank Details"
        '
        'txtVendor_Bank_ACNo
        '
        Me.txtVendor_Bank_ACNo.CalculationExpression = Nothing
        Me.txtVendor_Bank_ACNo.FieldCode = Nothing
        Me.txtVendor_Bank_ACNo.FieldDesc = Nothing
        Me.txtVendor_Bank_ACNo.FieldMaxLength = 0
        Me.txtVendor_Bank_ACNo.FieldName = Nothing
        Me.txtVendor_Bank_ACNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor_Bank_ACNo.isCalculatedField = False
        Me.txtVendor_Bank_ACNo.IsSourceFromTable = False
        Me.txtVendor_Bank_ACNo.IsSourceFromValueList = False
        Me.txtVendor_Bank_ACNo.IsUnique = False
        Me.txtVendor_Bank_ACNo.Location = New System.Drawing.Point(69, 58)
        Me.txtVendor_Bank_ACNo.MaxLength = 50
        Me.txtVendor_Bank_ACNo.MendatroryField = False
        Me.txtVendor_Bank_ACNo.MyLinkLable1 = Me.MyLabel17
        Me.txtVendor_Bank_ACNo.MyLinkLable2 = Nothing
        Me.txtVendor_Bank_ACNo.Name = "txtVendor_Bank_ACNo"
        Me.txtVendor_Bank_ACNo.ReferenceFieldDesc = Nothing
        Me.txtVendor_Bank_ACNo.ReferenceFieldName = Nothing
        Me.txtVendor_Bank_ACNo.ReferenceTableName = Nothing
        Me.txtVendor_Bank_ACNo.Size = New System.Drawing.Size(311, 18)
        Me.txtVendor_Bank_ACNo.TabIndex = 123
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(5, 60)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel17.TabIndex = 124
        Me.MyLabel17.Text = "Account No"
        '
        'txtVendor_bankcode
        '
        Me.txtVendor_bankcode.CalculationExpression = Nothing
        Me.txtVendor_bankcode.FieldCode = Nothing
        Me.txtVendor_bankcode.FieldDesc = Nothing
        Me.txtVendor_bankcode.FieldMaxLength = 0
        Me.txtVendor_bankcode.FieldName = Nothing
        Me.txtVendor_bankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor_bankcode.isCalculatedField = False
        Me.txtVendor_bankcode.IsSourceFromTable = False
        Me.txtVendor_bankcode.IsSourceFromValueList = False
        Me.txtVendor_bankcode.IsUnique = False
        Me.txtVendor_bankcode.Location = New System.Drawing.Point(68, 17)
        Me.txtVendor_bankcode.MaxLength = 50
        Me.txtVendor_bankcode.MendatroryField = False
        Me.txtVendor_bankcode.MyLinkLable1 = Me.MyLabel22
        Me.txtVendor_bankcode.MyLinkLable2 = Nothing
        Me.txtVendor_bankcode.Name = "txtVendor_bankcode"
        Me.txtVendor_bankcode.ReferenceFieldDesc = Nothing
        Me.txtVendor_bankcode.ReferenceFieldName = Nothing
        Me.txtVendor_bankcode.ReferenceTableName = Nothing
        Me.txtVendor_bankcode.Size = New System.Drawing.Size(130, 18)
        Me.txtVendor_bankcode.TabIndex = 122
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(4, 39)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel22.TabIndex = 29
        Me.MyLabel22.Text = "IFSC Code"
        '
        'TxtVendorBank_IFSCCode
        '
        Me.TxtVendorBank_IFSCCode.CalculationExpression = Nothing
        Me.TxtVendorBank_IFSCCode.FieldCode = Nothing
        Me.TxtVendorBank_IFSCCode.FieldDesc = Nothing
        Me.TxtVendorBank_IFSCCode.FieldMaxLength = 0
        Me.TxtVendorBank_IFSCCode.FieldName = Nothing
        Me.TxtVendorBank_IFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorBank_IFSCCode.isCalculatedField = False
        Me.TxtVendorBank_IFSCCode.IsSourceFromTable = False
        Me.TxtVendorBank_IFSCCode.IsSourceFromValueList = False
        Me.TxtVendorBank_IFSCCode.IsUnique = False
        Me.TxtVendorBank_IFSCCode.Location = New System.Drawing.Point(68, 37)
        Me.TxtVendorBank_IFSCCode.MaxLength = 50
        Me.TxtVendorBank_IFSCCode.MendatroryField = False
        Me.TxtVendorBank_IFSCCode.MyLinkLable1 = Me.MyLabel22
        Me.TxtVendorBank_IFSCCode.MyLinkLable2 = Nothing
        Me.TxtVendorBank_IFSCCode.Name = "TxtVendorBank_IFSCCode"
        Me.TxtVendorBank_IFSCCode.ReferenceFieldDesc = Nothing
        Me.TxtVendorBank_IFSCCode.ReferenceFieldName = Nothing
        Me.TxtVendorBank_IFSCCode.ReferenceTableName = Nothing
        Me.TxtVendorBank_IFSCCode.Size = New System.Drawing.Size(130, 18)
        Me.TxtVendorBank_IFSCCode.TabIndex = 5
        '
        'txtVendorBank_branchname
        '
        Me.txtVendorBank_branchname.CalculationExpression = Nothing
        Me.txtVendorBank_branchname.FieldCode = Nothing
        Me.txtVendorBank_branchname.FieldDesc = Nothing
        Me.txtVendorBank_branchname.FieldMaxLength = 0
        Me.txtVendorBank_branchname.FieldName = Nothing
        Me.txtVendorBank_branchname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorBank_branchname.isCalculatedField = False
        Me.txtVendorBank_branchname.IsSourceFromTable = False
        Me.txtVendorBank_branchname.IsSourceFromValueList = False
        Me.txtVendorBank_branchname.IsUnique = False
        Me.txtVendorBank_branchname.Location = New System.Drawing.Point(201, 37)
        Me.txtVendorBank_branchname.MaxLength = 150
        Me.txtVendorBank_branchname.MendatroryField = False
        Me.txtVendorBank_branchname.MyLinkLable1 = Nothing
        Me.txtVendorBank_branchname.MyLinkLable2 = Nothing
        Me.txtVendorBank_branchname.Name = "txtVendorBank_branchname"
        Me.txtVendorBank_branchname.ReferenceFieldDesc = Nothing
        Me.txtVendorBank_branchname.ReferenceFieldName = Nothing
        Me.txtVendorBank_branchname.ReferenceTableName = Nothing
        Me.txtVendorBank_branchname.Size = New System.Drawing.Size(179, 18)
        Me.txtVendorBank_branchname.TabIndex = 4
        '
        'TxtVendor_BankName
        '
        Me.TxtVendor_BankName.CalculationExpression = Nothing
        Me.TxtVendor_BankName.FieldCode = Nothing
        Me.TxtVendor_BankName.FieldDesc = Nothing
        Me.TxtVendor_BankName.FieldMaxLength = 0
        Me.TxtVendor_BankName.FieldName = Nothing
        Me.TxtVendor_BankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor_BankName.isCalculatedField = False
        Me.TxtVendor_BankName.IsSourceFromTable = False
        Me.TxtVendor_BankName.IsSourceFromValueList = False
        Me.TxtVendor_BankName.IsUnique = False
        Me.TxtVendor_BankName.Location = New System.Drawing.Point(201, 17)
        Me.TxtVendor_BankName.MaxLength = 50
        Me.TxtVendor_BankName.MendatroryField = False
        Me.TxtVendor_BankName.MyLinkLable1 = Me.MyLabel23
        Me.TxtVendor_BankName.MyLinkLable2 = Nothing
        Me.TxtVendor_BankName.Name = "TxtVendor_BankName"
        Me.TxtVendor_BankName.ReferenceFieldDesc = Nothing
        Me.TxtVendor_BankName.ReferenceFieldName = Nothing
        Me.TxtVendor_BankName.ReferenceTableName = Nothing
        Me.TxtVendor_BankName.Size = New System.Drawing.Size(178, 18)
        Me.TxtVendor_BankName.TabIndex = 1
        Me.TxtVendor_BankName.TabStop = False
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(4, 17)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel23.TabIndex = 26
        Me.MyLabel23.Text = "Bank Code"
        '
        'LblLocDesp
        '
        Me.LblLocDesp.AutoSize = False
        Me.LblLocDesp.BorderVisible = True
        Me.LblLocDesp.FieldName = Nothing
        Me.LblLocDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocDesp.Location = New System.Drawing.Point(467, 213)
        Me.LblLocDesp.Name = "LblLocDesp"
        Me.LblLocDesp.Size = New System.Drawing.Size(187, 18)
        Me.LblLocDesp.TabIndex = 47
        Me.LblLocDesp.TextWrap = False
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(366, 214)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Nothing
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(95, 17)
        Me.txtlocation.TabIndex = 45
        Me.txtlocation.Value = ""
        '
        'RadLabel18
        '
        Me.RadLabel18.Location = New System.Drawing.Point(315, 213)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel18.TabIndex = 46
        Me.RadLabel18.Text = "Location"
        '
        'pnlmemorndm
        '
        Me.pnlmemorndm.Controls.Add(Me.chkmemorndm)
        Me.pnlmemorndm.Controls.Add(Me.txtmemoamt)
        Me.pnlmemorndm.Controls.Add(Me.MyLabel6)
        Me.pnlmemorndm.Location = New System.Drawing.Point(6, 211)
        Me.pnlmemorndm.Name = "pnlmemorndm"
        Me.pnlmemorndm.Size = New System.Drawing.Size(309, 23)
        Me.pnlmemorndm.TabIndex = 1
        Me.pnlmemorndm.Visible = False
        '
        'chkmemorndm
        '
        Me.chkmemorndm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkmemorndm.Location = New System.Drawing.Point(4, 3)
        Me.chkmemorndm.Name = "chkmemorndm"
        Me.chkmemorndm.Size = New System.Drawing.Size(120, 16)
        Me.chkmemorndm.TabIndex = 0
        Me.chkmemorndm.Text = "Memorandum Entry"
        '
        'txtmemoamt
        '
        Me.txtmemoamt.AutoSize = False
        Me.txtmemoamt.CalculationExpression = Nothing
        Me.txtmemoamt.Enabled = False
        Me.txtmemoamt.FieldCode = Nothing
        Me.txtmemoamt.FieldDesc = Nothing
        Me.txtmemoamt.FieldMaxLength = 0
        Me.txtmemoamt.FieldName = Nothing
        Me.txtmemoamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmemoamt.isCalculatedField = False
        Me.txtmemoamt.IsSourceFromTable = False
        Me.txtmemoamt.IsSourceFromValueList = False
        Me.txtmemoamt.IsUnique = False
        Me.txtmemoamt.Location = New System.Drawing.Point(227, 2)
        Me.txtmemoamt.MaxLength = 18
        Me.txtmemoamt.MendatroryField = False
        Me.txtmemoamt.Multiline = True
        Me.txtmemoamt.MyLinkLable1 = Me.MyLabel6
        Me.txtmemoamt.MyLinkLable2 = Nothing
        Me.txtmemoamt.Name = "txtmemoamt"
        Me.txtmemoamt.ReferenceFieldDesc = Nothing
        Me.txtmemoamt.ReferenceFieldName = Nothing
        Me.txtmemoamt.ReferenceTableName = Nothing
        Me.txtmemoamt.Size = New System.Drawing.Size(77, 20)
        Me.txtmemoamt.TabIndex = 2
        Me.txtmemoamt.Text = "0"
        Me.txtmemoamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(125, 3)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel6.TabIndex = 1
        Me.MyLabel6.Text = "MemorandumAmt."
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.txtTotalPaymentBaseCurr)
        Me.pnlCurrConv.Controls.Add(Me.MyLabel5)
        Me.pnlCurrConv.Controls.Add(Me.txtBaseCurrency)
        Me.pnlCurrConv.Controls.Add(Me.MyLabel4)
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.lblEffectiveFrom)
        Me.pnlCurrConv.Controls.Add(Me.txtApplicableFrom)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(8, 235)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(581, 53)
        Me.pnlCurrConv.TabIndex = 1
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
        Me.txtConversionRate.Location = New System.Drawing.Point(273, 5)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.ReferenceFieldDesc = Nothing
        Me.txtConversionRate.ReferenceFieldName = Nothing
        Me.txtConversionRate.ReferenceTableName = Nothing
        Me.txtConversionRate.Size = New System.Drawing.Size(95, 20)
        Me.txtConversionRate.TabIndex = 3
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'txtTotalPaymentBaseCurr
        '
        Me.txtTotalPaymentBaseCurr.BackColor = System.Drawing.Color.White
        Me.txtTotalPaymentBaseCurr.CalculationExpression = Nothing
        Me.txtTotalPaymentBaseCurr.DecimalPlaces = 2
        Me.txtTotalPaymentBaseCurr.FieldCode = Nothing
        Me.txtTotalPaymentBaseCurr.FieldDesc = Nothing
        Me.txtTotalPaymentBaseCurr.FieldMaxLength = 0
        Me.txtTotalPaymentBaseCurr.FieldName = Nothing
        Me.txtTotalPaymentBaseCurr.isCalculatedField = False
        Me.txtTotalPaymentBaseCurr.IsSourceFromTable = False
        Me.txtTotalPaymentBaseCurr.IsSourceFromValueList = False
        Me.txtTotalPaymentBaseCurr.IsUnique = False
        Me.txtTotalPaymentBaseCurr.Location = New System.Drawing.Point(273, 26)
        Me.txtTotalPaymentBaseCurr.MendatroryField = False
        Me.txtTotalPaymentBaseCurr.MyLinkLable1 = Nothing
        Me.txtTotalPaymentBaseCurr.MyLinkLable2 = Nothing
        Me.txtTotalPaymentBaseCurr.Name = "txtTotalPaymentBaseCurr"
        Me.txtTotalPaymentBaseCurr.ReferenceFieldDesc = Nothing
        Me.txtTotalPaymentBaseCurr.ReferenceFieldName = Nothing
        Me.txtTotalPaymentBaseCurr.ReferenceTableName = Nothing
        Me.txtTotalPaymentBaseCurr.Size = New System.Drawing.Size(95, 20)
        Me.txtTotalPaymentBaseCurr.TabIndex = 9
        Me.txtTotalPaymentBaseCurr.Text = "0"
        Me.txtTotalPaymentBaseCurr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPaymentBaseCurr.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(178, 28)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel5.TabIndex = 8
        Me.MyLabel5.Text = "Total Payment"
        '
        'txtBaseCurrency
        '
        Me.txtBaseCurrency.CalculationExpression = Nothing
        Me.txtBaseCurrency.Enabled = False
        Me.txtBaseCurrency.FieldCode = Nothing
        Me.txtBaseCurrency.FieldDesc = Nothing
        Me.txtBaseCurrency.FieldMaxLength = 0
        Me.txtBaseCurrency.FieldName = Nothing
        Me.txtBaseCurrency.isCalculatedField = False
        Me.txtBaseCurrency.IsSourceFromTable = False
        Me.txtBaseCurrency.IsSourceFromValueList = False
        Me.txtBaseCurrency.IsUnique = False
        Me.txtBaseCurrency.Location = New System.Drawing.Point(89, 27)
        Me.txtBaseCurrency.MendatroryField = False
        Me.txtBaseCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaseCurrency.MyLinkLable1 = Nothing
        Me.txtBaseCurrency.MyLinkLable2 = Nothing
        Me.txtBaseCurrency.MyReadOnly = False
        Me.txtBaseCurrency.MyShowMasterFormButton = False
        Me.txtBaseCurrency.Name = "txtBaseCurrency"
        Me.txtBaseCurrency.ReferenceFieldDesc = Nothing
        Me.txtBaseCurrency.ReferenceFieldName = Nothing
        Me.txtBaseCurrency.ReferenceTableName = Nothing
        Me.txtBaseCurrency.Size = New System.Drawing.Size(83, 20)
        Me.txtBaseCurrency.TabIndex = 7
        Me.txtBaseCurrency.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel4.TabIndex = 6
        Me.MyLabel4.Text = "BaseCurrency"
        '
        'txtCurrencyCode
        '
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
        Me.txtCurrencyCode.Location = New System.Drawing.Point(89, 5)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(83, 20)
        Me.txtCurrencyCode.TabIndex = 1
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(374, 9)
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
        Me.txtApplicableFrom.Location = New System.Drawing.Point(467, 6)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(106, 18)
        Me.txtApplicableFrom.TabIndex = 5
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(7, 8)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 0
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(178, 8)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 2
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkTDSProvision)
        Me.RadGroupBox1.Controls.Add(Me.chkSaving)
        Me.RadGroupBox1.Controls.Add(Me.chkFarmerLoanPayment)
        Me.RadGroupBox1.Controls.Add(Me.pnlMiscPayment)
        Me.RadGroupBox1.Controls.Add(Me.chkIsReceipt)
        Me.RadGroupBox1.Controls.Add(Me.rbtnImportPosted)
        Me.RadGroupBox1.Controls.Add(Me.ddlEmployeeAdvanceType)
        Me.RadGroupBox1.Controls.Add(Me.rbtnExportPosted)
        Me.RadGroupBox1.Controls.Add(Me.lblMPAdv)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.txtMPAdv)
        Me.RadGroupBox1.Controls.Add(Me.ddlEmployeeType)
        Me.RadGroupBox1.Controls.Add(Me.btnOk)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel38)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.txtTapalNo)
        Me.RadGroupBox1.Controls.Add(Me.chkOpening)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel37)
        Me.RadGroupBox1.Controls.Add(Me.txtDataAndTimeSelection)
        Me.RadGroupBox1.Controls.Add(Me.chkBankChargesWaveOff)
        Me.RadGroupBox1.Controls.Add(Me.pnlEMI)
        Me.RadGroupBox1.Controls.Add(Me.fndloanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblApplyLoanNo)
        Me.RadGroupBox1.Controls.Add(Me.ChkAdvSalary)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerOutStanding)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.ChkSecurity)
        Me.RadGroupBox1.Controls.Add(Me.LblPONo)
        Me.RadGroupBox1.Controls.Add(Me.txtPONo)
        Me.RadGroupBox1.Controls.Add(Me.pnlPJC)
        Me.RadGroupBox1.Controls.Add(Me.pnlCform)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtBankCharges)
        Me.RadGroupBox1.Controls.Add(Me.lblLoadOutNo)
        Me.RadGroupBox1.Controls.Add(Me.txtLoadOutno)
        Me.RadGroupBox1.Controls.Add(Me.txtPaymentAmt)
        Me.RadGroupBox1.Controls.Add(Me.pnlVendor)
        Me.RadGroupBox1.Controls.Add(Me.pnlCheque)
        Me.RadGroupBox1.Controls.Add(Me.lblBankDesc)
        Me.RadGroupBox1.Controls.Add(Me.pnlAdvance)
        Me.RadGroupBox1.Controls.Add(Me.dtpPayment)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblTotPayment)
        Me.RadGroupBox1.Controls.Add(Me.lbldesc)
        Me.RadGroupBox1.Controls.Add(Me.txtPaymentMode)
        Me.RadGroupBox1.Controls.Add(Me.txtBankCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.txtPaymentNo)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymentcode)
        Me.RadGroupBox1.Controls.Add(Me.ddlPaymentType)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymentno)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymentdate)
        Me.RadGroupBox1.Controls.Add(Me.lblbankcode)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymenttype)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1110, 205)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkTDSProvision
        '
        Me.chkTDSProvision.Location = New System.Drawing.Point(752, 159)
        Me.chkTDSProvision.MyLinkLable1 = Nothing
        Me.chkTDSProvision.MyLinkLable2 = Nothing
        Me.chkTDSProvision.Name = "chkTDSProvision"
        Me.chkTDSProvision.ReadOnly = True
        Me.chkTDSProvision.Size = New System.Drawing.Size(89, 18)
        Me.chkTDSProvision.TabIndex = 12137
        Me.chkTDSProvision.Tag1 = Nothing
        Me.chkTDSProvision.Text = "TDS Provision"
        '
        'chkSaving
        '
        Me.chkSaving.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSaving.Location = New System.Drawing.Point(524, 66)
        Me.chkSaving.Name = "chkSaving"
        Me.chkSaving.Size = New System.Drawing.Size(53, 18)
        Me.chkSaving.TabIndex = 12116
        Me.chkSaving.Text = "Saving"
        '
        'chkFarmerLoanPayment
        '
        Me.chkFarmerLoanPayment.Location = New System.Drawing.Point(685, 137)
        Me.chkFarmerLoanPayment.Name = "chkFarmerLoanPayment"
        Me.chkFarmerLoanPayment.Size = New System.Drawing.Size(129, 18)
        Me.chkFarmerLoanPayment.TabIndex = 12136
        Me.chkFarmerLoanPayment.Text = "Farmer Loan Payment"
        '
        'pnlMiscPayment
        '
        Me.pnlMiscPayment.Controls.Add(Me.txtRemitTo)
        Me.pnlMiscPayment.Controls.Add(Me.MyLabel1)
        Me.pnlMiscPayment.Controls.Add(Me.MyLabel2)
        Me.pnlMiscPayment.Controls.Add(Me.txtTotalAppliedAmt)
        Me.pnlMiscPayment.Location = New System.Drawing.Point(207, 155)
        Me.pnlMiscPayment.Name = "pnlMiscPayment"
        Me.pnlMiscPayment.Size = New System.Drawing.Size(443, 24)
        Me.pnlMiscPayment.TabIndex = 26
        '
        'txtRemitTo
        '
        Me.txtRemitTo.CalculationExpression = Nothing
        Me.txtRemitTo.FieldCode = Nothing
        Me.txtRemitTo.FieldDesc = Nothing
        Me.txtRemitTo.FieldMaxLength = 0
        Me.txtRemitTo.FieldName = Nothing
        Me.txtRemitTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemitTo.isCalculatedField = False
        Me.txtRemitTo.IsSourceFromTable = False
        Me.txtRemitTo.IsSourceFromValueList = False
        Me.txtRemitTo.IsUnique = False
        Me.txtRemitTo.Location = New System.Drawing.Point(280, 3)
        Me.txtRemitTo.MaxLength = 6
        Me.txtRemitTo.MendatroryField = False
        Me.txtRemitTo.MyLinkLable1 = Nothing
        Me.txtRemitTo.MyLinkLable2 = Nothing
        Me.txtRemitTo.Name = "txtRemitTo"
        Me.txtRemitTo.ReferenceFieldDesc = Nothing
        Me.txtRemitTo.ReferenceFieldName = Nothing
        Me.txtRemitTo.ReferenceTableName = Nothing
        Me.txtRemitTo.Size = New System.Drawing.Size(159, 18)
        Me.txtRemitTo.TabIndex = 3
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(227, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel1.TabIndex = 2
        Me.MyLabel1.Text = "Remit To"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(114, 16)
        Me.MyLabel2.TabIndex = 0
        Me.MyLabel2.Text = "Total Applied Amount"
        '
        'txtTotalAppliedAmt
        '
        Me.txtTotalAppliedAmt.AutoSize = False
        Me.txtTotalAppliedAmt.CalculationExpression = Nothing
        Me.txtTotalAppliedAmt.FieldCode = Nothing
        Me.txtTotalAppliedAmt.FieldDesc = Nothing
        Me.txtTotalAppliedAmt.FieldMaxLength = 0
        Me.txtTotalAppliedAmt.FieldName = Nothing
        Me.txtTotalAppliedAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAppliedAmt.isCalculatedField = False
        Me.txtTotalAppliedAmt.IsSourceFromTable = False
        Me.txtTotalAppliedAmt.IsSourceFromValueList = False
        Me.txtTotalAppliedAmt.IsUnique = False
        Me.txtTotalAppliedAmt.Location = New System.Drawing.Point(124, 2)
        Me.txtTotalAppliedAmt.MaxLength = 18
        Me.txtTotalAppliedAmt.MendatroryField = False
        Me.txtTotalAppliedAmt.Multiline = True
        Me.txtTotalAppliedAmt.MyLinkLable1 = Nothing
        Me.txtTotalAppliedAmt.MyLinkLable2 = Nothing
        Me.txtTotalAppliedAmt.Name = "txtTotalAppliedAmt"
        Me.txtTotalAppliedAmt.ReadOnly = True
        Me.txtTotalAppliedAmt.ReferenceFieldDesc = Nothing
        Me.txtTotalAppliedAmt.ReferenceFieldName = Nothing
        Me.txtTotalAppliedAmt.ReferenceTableName = Nothing
        Me.txtTotalAppliedAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalAppliedAmt.TabIndex = 1
        Me.txtTotalAppliedAmt.Text = "0"
        Me.txtTotalAppliedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkIsReceipt
        '
        Me.chkIsReceipt.Location = New System.Drawing.Point(826, 26)
        Me.chkIsReceipt.Name = "chkIsReceipt"
        Me.chkIsReceipt.Size = New System.Drawing.Size(57, 18)
        Me.chkIsReceipt.TabIndex = 12135
        Me.chkIsReceipt.Text = "Receipt"
        '
        'rbtnImportPosted
        '
        Me.rbtnImportPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnImportPosted.Location = New System.Drawing.Point(983, 181)
        Me.rbtnImportPosted.Name = "rbtnImportPosted"
        Me.rbtnImportPosted.Size = New System.Drawing.Size(121, 19)
        Me.rbtnImportPosted.TabIndex = 12122
        Me.rbtnImportPosted.Text = "Import Posted Data"
        Me.rbtnImportPosted.Visible = False
        '
        'ddlEmployeeAdvanceType
        '
        Me.ddlEmployeeAdvanceType.AutoCompleteDisplayMember = Nothing
        Me.ddlEmployeeAdvanceType.AutoCompleteValueMember = Nothing
        Me.ddlEmployeeAdvanceType.CalculationExpression = Nothing
        Me.ddlEmployeeAdvanceType.DropDownAnimationEnabled = True
        Me.ddlEmployeeAdvanceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlEmployeeAdvanceType.FieldCode = Nothing
        Me.ddlEmployeeAdvanceType.FieldDesc = Nothing
        Me.ddlEmployeeAdvanceType.FieldMaxLength = 0
        Me.ddlEmployeeAdvanceType.FieldName = Nothing
        Me.ddlEmployeeAdvanceType.isCalculatedField = False
        Me.ddlEmployeeAdvanceType.IsSourceFromTable = False
        Me.ddlEmployeeAdvanceType.IsSourceFromValueList = False
        Me.ddlEmployeeAdvanceType.IsUnique = False
        RadListDataItem1.Text = "Yes"
        RadListDataItem2.Text = "No"
        Me.ddlEmployeeAdvanceType.Items.Add(RadListDataItem1)
        Me.ddlEmployeeAdvanceType.Items.Add(RadListDataItem2)
        Me.ddlEmployeeAdvanceType.Location = New System.Drawing.Point(980, 157)
        Me.ddlEmployeeAdvanceType.MendatroryField = True
        Me.ddlEmployeeAdvanceType.MyLinkLable1 = Me.MyLabel14
        Me.ddlEmployeeAdvanceType.MyLinkLable2 = Nothing
        Me.ddlEmployeeAdvanceType.Name = "ddlEmployeeAdvanceType"
        Me.ddlEmployeeAdvanceType.ReferenceFieldDesc = Nothing
        Me.ddlEmployeeAdvanceType.ReferenceFieldName = Nothing
        Me.ddlEmployeeAdvanceType.ReferenceTableName = Nothing
        Me.ddlEmployeeAdvanceType.Size = New System.Drawing.Size(132, 20)
        Me.ddlEmployeeAdvanceType.TabIndex = 12125
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(842, 159)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(132, 16)
        Me.MyLabel14.TabIndex = 12126
        Me.MyLabel14.Text = "Employee Advance Type"
        '
        'rbtnExportPosted
        '
        Me.rbtnExportPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnExportPosted.Location = New System.Drawing.Point(870, 181)
        Me.rbtnExportPosted.Name = "rbtnExportPosted"
        Me.rbtnExportPosted.Size = New System.Drawing.Size(108, 19)
        Me.rbtnExportPosted.TabIndex = 12121
        Me.rbtnExportPosted.Text = "Export Posted Data"
        Me.rbtnExportPosted.Visible = False
        '
        'lblMPAdv
        '
        Me.lblMPAdv.FieldName = Nothing
        Me.lblMPAdv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMPAdv.Location = New System.Drawing.Point(580, 27)
        Me.lblMPAdv.Name = "lblMPAdv"
        Me.lblMPAdv.Size = New System.Drawing.Size(130, 16)
        Me.lblMPAdv.TabIndex = 12133
        Me.lblMPAdv.Text = "Milk Producer (Advance)"
        Me.lblMPAdv.Visible = False
        '
        'txtMPAdv
        '
        Me.txtMPAdv.CalculationExpression = Nothing
        Me.txtMPAdv.FieldCode = Nothing
        Me.txtMPAdv.FieldDesc = Nothing
        Me.txtMPAdv.FieldMaxLength = 0
        Me.txtMPAdv.FieldName = Nothing
        Me.txtMPAdv.isCalculatedField = False
        Me.txtMPAdv.IsSourceFromTable = False
        Me.txtMPAdv.IsSourceFromValueList = False
        Me.txtMPAdv.IsUnique = False
        Me.txtMPAdv.Location = New System.Drawing.Point(711, 26)
        Me.txtMPAdv.MendatroryField = False
        Me.txtMPAdv.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMPAdv.MyLinkLable1 = Nothing
        Me.txtMPAdv.MyLinkLable2 = Nothing
        Me.txtMPAdv.MyReadOnly = False
        Me.txtMPAdv.MyShowMasterFormButton = False
        Me.txtMPAdv.Name = "txtMPAdv"
        Me.txtMPAdv.ReferenceFieldDesc = Nothing
        Me.txtMPAdv.ReferenceFieldName = Nothing
        Me.txtMPAdv.ReferenceTableName = Nothing
        Me.txtMPAdv.Size = New System.Drawing.Size(113, 19)
        Me.txtMPAdv.TabIndex = 12134
        Me.txtMPAdv.Value = ""
        Me.txtMPAdv.Visible = False
        '
        'ddlEmployeeType
        '
        Me.ddlEmployeeType.AutoCompleteDisplayMember = Nothing
        Me.ddlEmployeeType.AutoCompleteValueMember = Nothing
        Me.ddlEmployeeType.CalculationExpression = Nothing
        Me.ddlEmployeeType.DropDownAnimationEnabled = True
        Me.ddlEmployeeType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlEmployeeType.FieldCode = Nothing
        Me.ddlEmployeeType.FieldDesc = Nothing
        Me.ddlEmployeeType.FieldMaxLength = 0
        Me.ddlEmployeeType.FieldName = Nothing
        Me.ddlEmployeeType.isCalculatedField = False
        Me.ddlEmployeeType.IsSourceFromTable = False
        Me.ddlEmployeeType.IsSourceFromValueList = False
        Me.ddlEmployeeType.IsUnique = False
        RadListDataItem3.Text = "Yes"
        RadListDataItem4.Text = "No"
        Me.ddlEmployeeType.Items.Add(RadListDataItem3)
        Me.ddlEmployeeType.Items.Add(RadListDataItem4)
        Me.ddlEmployeeType.Location = New System.Drawing.Point(982, 135)
        Me.ddlEmployeeType.MendatroryField = True
        Me.ddlEmployeeType.MyLinkLable1 = Me.MyLabel13
        Me.ddlEmployeeType.MyLinkLable2 = Nothing
        Me.ddlEmployeeType.Name = "ddlEmployeeType"
        Me.ddlEmployeeType.ReferenceFieldDesc = Nothing
        Me.ddlEmployeeType.ReferenceFieldName = Nothing
        Me.ddlEmployeeType.ReferenceTableName = Nothing
        Me.ddlEmployeeType.Size = New System.Drawing.Size(132, 20)
        Me.ddlEmployeeType.TabIndex = 12123
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(844, 137)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(132, 16)
        Me.MyLabel13.TabIndex = 12124
        Me.MyLabel13.Text = "Employee Expense Type"
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(813, 182)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(52, 18)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = ">>"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(682, 114)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel38.TabIndex = 12132
        Me.MyLabel38.Text = "Tapal No"
        '
        'txtTapalNo
        '
        Me.txtTapalNo.CalculationExpression = Nothing
        Me.txtTapalNo.FieldCode = Nothing
        Me.txtTapalNo.FieldDesc = Nothing
        Me.txtTapalNo.FieldMaxLength = 0
        Me.txtTapalNo.FieldName = Nothing
        Me.txtTapalNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTapalNo.isCalculatedField = False
        Me.txtTapalNo.IsSourceFromTable = False
        Me.txtTapalNo.IsSourceFromValueList = False
        Me.txtTapalNo.IsUnique = False
        Me.txtTapalNo.Location = New System.Drawing.Point(737, 113)
        Me.txtTapalNo.MendatroryField = False
        Me.txtTapalNo.MyLinkLable1 = Nothing
        Me.txtTapalNo.MyLinkLable2 = Nothing
        Me.txtTapalNo.Name = "txtTapalNo"
        Me.txtTapalNo.ReferenceFieldDesc = Nothing
        Me.txtTapalNo.ReferenceFieldName = Nothing
        Me.txtTapalNo.ReferenceTableName = Nothing
        Me.txtTapalNo.Size = New System.Drawing.Size(142, 18)
        Me.txtTapalNo.TabIndex = 12131
        '
        'chkOpening
        '
        Me.chkOpening.Enabled = False
        Me.chkOpening.Location = New System.Drawing.Point(685, 161)
        Me.chkOpening.Name = "chkOpening"
        Me.chkOpening.Size = New System.Drawing.Size(64, 18)
        Me.chkOpening.TabIndex = 12120
        Me.chkOpening.Text = "Opening"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(881, 113)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel37.TabIndex = 12130
        Me.MyLabel37.Text = "Date And Time"
        '
        'txtDataAndTimeSelection
        '
        Me.txtDataAndTimeSelection.CalculationExpression = Nothing
        Me.txtDataAndTimeSelection.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDataAndTimeSelection.FieldCode = Nothing
        Me.txtDataAndTimeSelection.FieldDesc = Nothing
        Me.txtDataAndTimeSelection.FieldMaxLength = 0
        Me.txtDataAndTimeSelection.FieldName = Nothing
        Me.txtDataAndTimeSelection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataAndTimeSelection.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDataAndTimeSelection.isCalculatedField = False
        Me.txtDataAndTimeSelection.IsSourceFromTable = False
        Me.txtDataAndTimeSelection.IsSourceFromValueList = False
        Me.txtDataAndTimeSelection.IsUnique = False
        Me.txtDataAndTimeSelection.Location = New System.Drawing.Point(963, 112)
        Me.txtDataAndTimeSelection.MendatroryField = False
        Me.txtDataAndTimeSelection.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDataAndTimeSelection.MyLinkLable1 = Nothing
        Me.txtDataAndTimeSelection.MyLinkLable2 = Nothing
        Me.txtDataAndTimeSelection.Name = "txtDataAndTimeSelection"
        Me.txtDataAndTimeSelection.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDataAndTimeSelection.ReferenceFieldDesc = Nothing
        Me.txtDataAndTimeSelection.ReferenceFieldName = Nothing
        Me.txtDataAndTimeSelection.ReferenceTableName = Nothing
        Me.txtDataAndTimeSelection.ShowCheckBox = True
        Me.txtDataAndTimeSelection.Size = New System.Drawing.Size(140, 18)
        Me.txtDataAndTimeSelection.TabIndex = 12129
        Me.txtDataAndTimeSelection.TabStop = False
        Me.txtDataAndTimeSelection.Text = "13/06/2011 11:29 AM"
        Me.txtDataAndTimeSelection.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkBankChargesWaveOff
        '
        Me.chkBankChargesWaveOff.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBankChargesWaveOff.Location = New System.Drawing.Point(881, 45)
        Me.chkBankChargesWaveOff.Name = "chkBankChargesWaveOff"
        Me.chkBankChargesWaveOff.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkBankChargesWaveOff.Size = New System.Drawing.Size(138, 18)
        Me.chkBankChargesWaveOff.TabIndex = 12119
        Me.chkBankChargesWaveOff.Text = "Wave off Bank Charges "
        '
        'pnlEMI
        '
        Me.pnlEMI.Controls.Add(Me.txtNoOfEMI)
        Me.pnlEMI.Controls.Add(Me.MyLabel11)
        Me.pnlEMI.Controls.Add(Me.txtInterestRate)
        Me.pnlEMI.Controls.Add(Me.MyLabel8)
        Me.pnlEMI.Location = New System.Drawing.Point(883, 65)
        Me.pnlEMI.Name = "pnlEMI"
        Me.pnlEMI.Size = New System.Drawing.Size(137, 43)
        Me.pnlEMI.TabIndex = 12118
        Me.pnlEMI.Visible = False
        '
        'txtNoOfEMI
        '
        Me.txtNoOfEMI.BackColor = System.Drawing.Color.White
        Me.txtNoOfEMI.CalculationExpression = Nothing
        Me.txtNoOfEMI.DecimalPlaces = 0
        Me.txtNoOfEMI.FieldCode = Nothing
        Me.txtNoOfEMI.FieldDesc = Nothing
        Me.txtNoOfEMI.FieldMaxLength = 0
        Me.txtNoOfEMI.FieldName = Nothing
        Me.txtNoOfEMI.isCalculatedField = False
        Me.txtNoOfEMI.IsSourceFromTable = False
        Me.txtNoOfEMI.IsSourceFromValueList = False
        Me.txtNoOfEMI.IsUnique = False
        Me.txtNoOfEMI.Location = New System.Drawing.Point(75, 21)
        Me.txtNoOfEMI.MendatroryField = False
        Me.txtNoOfEMI.MyLinkLable1 = Nothing
        Me.txtNoOfEMI.MyLinkLable2 = Nothing
        Me.txtNoOfEMI.Name = "txtNoOfEMI"
        Me.txtNoOfEMI.ReferenceFieldDesc = Nothing
        Me.txtNoOfEMI.ReferenceFieldName = Nothing
        Me.txtNoOfEMI.ReferenceTableName = Nothing
        Me.txtNoOfEMI.Size = New System.Drawing.Size(57, 20)
        Me.txtNoOfEMI.TabIndex = 29
        Me.txtNoOfEMI.Text = "0"
        Me.txtNoOfEMI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfEMI.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(3, 23)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel11.TabIndex = 28
        Me.MyLabel11.Text = "No Of EMI"
        '
        'txtInterestRate
        '
        Me.txtInterestRate.BackColor = System.Drawing.Color.White
        Me.txtInterestRate.CalculationExpression = Nothing
        Me.txtInterestRate.DecimalPlaces = 2
        Me.txtInterestRate.FieldCode = Nothing
        Me.txtInterestRate.FieldDesc = Nothing
        Me.txtInterestRate.FieldMaxLength = 0
        Me.txtInterestRate.FieldName = Nothing
        Me.txtInterestRate.isCalculatedField = False
        Me.txtInterestRate.IsSourceFromTable = False
        Me.txtInterestRate.IsSourceFromValueList = False
        Me.txtInterestRate.IsUnique = False
        Me.txtInterestRate.Location = New System.Drawing.Point(75, 0)
        Me.txtInterestRate.MendatroryField = False
        Me.txtInterestRate.MyLinkLable1 = Nothing
        Me.txtInterestRate.MyLinkLable2 = Nothing
        Me.txtInterestRate.Name = "txtInterestRate"
        Me.txtInterestRate.ReferenceFieldDesc = Nothing
        Me.txtInterestRate.ReferenceFieldName = Nothing
        Me.txtInterestRate.ReferenceTableName = Nothing
        Me.txtInterestRate.Size = New System.Drawing.Size(57, 20)
        Me.txtInterestRate.TabIndex = 27
        Me.txtInterestRate.Text = "0"
        Me.txtInterestRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInterestRate.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel8.TabIndex = 26
        Me.MyLabel8.Text = "Interest Rate"
        '
        'fndloanNo
        '
        Me.fndloanNo.CalculationExpression = Nothing
        Me.fndloanNo.FieldCode = Nothing
        Me.fndloanNo.FieldDesc = Nothing
        Me.fndloanNo.FieldMaxLength = 0
        Me.fndloanNo.FieldName = Nothing
        Me.fndloanNo.isCalculatedField = False
        Me.fndloanNo.IsSourceFromTable = False
        Me.fndloanNo.IsSourceFromValueList = False
        Me.fndloanNo.IsUnique = False
        Me.fndloanNo.Location = New System.Drawing.Point(887, 3)
        Me.fndloanNo.MendatroryField = False
        Me.fndloanNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndloanNo.MyLinkLable1 = Nothing
        Me.fndloanNo.MyLinkLable2 = Nothing
        Me.fndloanNo.MyReadOnly = False
        Me.fndloanNo.MyShowMasterFormButton = False
        Me.fndloanNo.Name = "fndloanNo"
        Me.fndloanNo.ReferenceFieldDesc = Nothing
        Me.fndloanNo.ReferenceFieldName = Nothing
        Me.fndloanNo.ReferenceTableName = Nothing
        Me.fndloanNo.Size = New System.Drawing.Size(117, 19)
        Me.fndloanNo.TabIndex = 12117
        Me.fndloanNo.Value = ""
        Me.fndloanNo.Visible = False
        '
        'lblApplyLoanNo
        '
        Me.lblApplyLoanNo.FieldName = Nothing
        Me.lblApplyLoanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplyLoanNo.Location = New System.Drawing.Point(827, 5)
        Me.lblApplyLoanNo.Name = "lblApplyLoanNo"
        Me.lblApplyLoanNo.Size = New System.Drawing.Size(52, 16)
        Me.lblApplyLoanNo.TabIndex = 12116
        Me.lblApplyLoanNo.Text = "Loan No."
        Me.lblApplyLoanNo.Visible = False
        '
        'ChkAdvSalary
        '
        Me.ChkAdvSalary.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkAdvSalary.Location = New System.Drawing.Point(415, 66)
        Me.ChkAdvSalary.Name = "ChkAdvSalary"
        Me.ChkAdvSalary.Size = New System.Drawing.Size(96, 18)
        Me.ChkAdvSalary.TabIndex = 12115
        Me.ChkAdvSalary.Text = "Advance Salary"
        '
        'lblCustomerOutStanding
        '
        Me.lblCustomerOutStanding.AutoSize = False
        Me.lblCustomerOutStanding.BorderVisible = True
        Me.lblCustomerOutStanding.FieldName = Nothing
        Me.lblCustomerOutStanding.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerOutStanding.Location = New System.Drawing.Point(961, 26)
        Me.lblCustomerOutStanding.Name = "lblCustomerOutStanding"
        Me.lblCustomerOutStanding.Size = New System.Drawing.Size(146, 18)
        Me.lblCustomerOutStanding.TabIndex = 12114
        Me.lblCustomerOutStanding.Visible = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(891, 27)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel10.TabIndex = 12113
        Me.MyLabel10.Text = "Outstanding"
        Me.MyLabel10.Visible = False
        '
        'ChkSecurity
        '
        Me.ChkSecurity.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkSecurity.Location = New System.Drawing.Point(590, 46)
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(60, 18)
        Me.ChkSecurity.TabIndex = 13
        Me.ChkSecurity.Text = "Security"
        '
        'LblPONo
        '
        Me.LblPONo.FieldName = Nothing
        Me.LblPONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPONo.Location = New System.Drawing.Point(656, 47)
        Me.LblPONo.Name = "LblPONo"
        Me.LblPONo.Size = New System.Drawing.Size(43, 16)
        Me.LblPONo.TabIndex = 12112
        Me.LblPONo.Text = "PO No."
        '
        'txtPONo
        '
        Me.txtPONo.CalculationExpression = Nothing
        Me.txtPONo.FieldCode = Nothing
        Me.txtPONo.FieldDesc = Nothing
        Me.txtPONo.FieldMaxLength = 0
        Me.txtPONo.FieldName = Nothing
        Me.txtPONo.isCalculatedField = False
        Me.txtPONo.IsSourceFromTable = False
        Me.txtPONo.IsSourceFromValueList = False
        Me.txtPONo.IsUnique = False
        Me.txtPONo.Location = New System.Drawing.Point(706, 46)
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.MyLinkLable1 = Nothing
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.MyReadOnly = False
        Me.txtPONo.MyShowMasterFormButton = False
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(172, 19)
        Me.txtPONo.TabIndex = 14
        Me.txtPONo.Value = ""
        '
        'pnlPJC
        '
        Me.pnlPJC.Controls.Add(Me.lblProjDesc)
        Me.pnlPJC.Controls.Add(Me.MyLabel7)
        Me.pnlPJC.Controls.Add(Me.lblEmpDesc)
        Me.pnlPJC.Controls.Add(Me.lblEmpCode)
        Me.pnlPJC.Controls.Add(Me.lblProjCode)
        Me.pnlPJC.Controls.Add(Me.MyLabel9)
        Me.pnlPJC.Location = New System.Drawing.Point(3, 181)
        Me.pnlPJC.Name = "pnlPJC"
        Me.pnlPJC.Size = New System.Drawing.Size(782, 19)
        Me.pnlPJC.TabIndex = 27
        '
        'lblProjDesc
        '
        Me.lblProjDesc.AutoSize = False
        Me.lblProjDesc.BorderVisible = True
        Me.lblProjDesc.FieldName = Nothing
        Me.lblProjDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjDesc.Location = New System.Drawing.Point(571, 0)
        Me.lblProjDesc.Name = "lblProjDesc"
        Me.lblProjDesc.Size = New System.Drawing.Size(210, 18)
        Me.lblProjDesc.TabIndex = 5
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 0)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel7.TabIndex = 0
        Me.MyLabel7.Text = "Employee Code"
        '
        'lblEmpDesc
        '
        Me.lblEmpDesc.AutoSize = False
        Me.lblEmpDesc.BorderVisible = True
        Me.lblEmpDesc.FieldName = Nothing
        Me.lblEmpDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpDesc.Location = New System.Drawing.Point(204, 0)
        Me.lblEmpDesc.Name = "lblEmpDesc"
        Me.lblEmpDesc.Size = New System.Drawing.Size(182, 18)
        Me.lblEmpDesc.TabIndex = 2
        '
        'lblEmpCode
        '
        Me.lblEmpCode.AutoSize = False
        Me.lblEmpCode.BorderVisible = True
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(90, 0)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(113, 18)
        Me.lblEmpCode.TabIndex = 1
        '
        'lblProjCode
        '
        Me.lblProjCode.AutoSize = False
        Me.lblProjCode.BorderVisible = True
        Me.lblProjCode.FieldName = Nothing
        Me.lblProjCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjCode.Location = New System.Drawing.Point(464, 0)
        Me.lblProjCode.Name = "lblProjCode"
        Me.lblProjCode.Size = New System.Drawing.Size(105, 18)
        Me.lblProjCode.TabIndex = 4
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(388, 0)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel9.TabIndex = 3
        Me.MyLabel9.Text = "Project Code"
        '
        'pnlCform
        '
        Me.pnlCform.Controls.Add(Me.chkCForm)
        Me.pnlCform.Controls.Add(Me.MyLabel18)
        Me.pnlCform.Controls.Add(Me.txtCFormInvNo)
        Me.pnlCform.Location = New System.Drawing.Point(589, 65)
        Me.pnlCform.Name = "pnlCform"
        Me.pnlCform.Size = New System.Drawing.Size(291, 22)
        Me.pnlCform.TabIndex = 19
        '
        'chkCForm
        '
        Me.chkCForm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCForm.Location = New System.Drawing.Point(3, 4)
        Me.chkCForm.Name = "chkCForm"
        Me.chkCForm.Size = New System.Drawing.Size(96, 16)
        Me.chkCForm.TabIndex = 0
        Me.chkCForm.Text = "Against CForm"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(111, 3)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel18.TabIndex = 1
        Me.MyLabel18.Text = "Inv. No"
        Me.MyLabel18.Visible = False
        '
        'txtCFormInvNo
        '
        Me.txtCFormInvNo.CalculationExpression = Nothing
        Me.txtCFormInvNo.FieldCode = Nothing
        Me.txtCFormInvNo.FieldDesc = Nothing
        Me.txtCFormInvNo.FieldMaxLength = 0
        Me.txtCFormInvNo.FieldName = Nothing
        Me.txtCFormInvNo.isCalculatedField = False
        Me.txtCFormInvNo.IsSourceFromTable = False
        Me.txtCFormInvNo.IsSourceFromValueList = False
        Me.txtCFormInvNo.IsUnique = False
        Me.txtCFormInvNo.Location = New System.Drawing.Point(155, 3)
        Me.txtCFormInvNo.MendatroryField = True
        Me.txtCFormInvNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCFormInvNo.MyLinkLable1 = Nothing
        Me.txtCFormInvNo.MyLinkLable2 = Nothing
        Me.txtCFormInvNo.MyReadOnly = False
        Me.txtCFormInvNo.MyShowMasterFormButton = False
        Me.txtCFormInvNo.Name = "txtCFormInvNo"
        Me.txtCFormInvNo.ReferenceFieldDesc = Nothing
        Me.txtCFormInvNo.ReferenceFieldName = Nothing
        Me.txtCFormInvNo.ReferenceTableName = Nothing
        Me.txtCFormInvNo.Size = New System.Drawing.Size(134, 16)
        Me.txtCFormInvNo.TabIndex = 2
        Me.txtCFormInvNo.Value = ""
        Me.txtCFormInvNo.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(257, 67)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "Bank Charges"
        '
        'txtBankCharges
        '
        Me.txtBankCharges.BackColor = System.Drawing.Color.White
        Me.txtBankCharges.CalculationExpression = Nothing
        Me.txtBankCharges.DecimalPlaces = 2
        Me.txtBankCharges.FieldCode = Nothing
        Me.txtBankCharges.FieldDesc = Nothing
        Me.txtBankCharges.FieldMaxLength = 0
        Me.txtBankCharges.FieldName = Nothing
        Me.txtBankCharges.isCalculatedField = False
        Me.txtBankCharges.IsSourceFromTable = False
        Me.txtBankCharges.IsSourceFromValueList = False
        Me.txtBankCharges.IsUnique = False
        Me.txtBankCharges.Location = New System.Drawing.Point(337, 65)
        Me.txtBankCharges.MendatroryField = False
        Me.txtBankCharges.MyLinkLable1 = Nothing
        Me.txtBankCharges.MyLinkLable2 = Nothing
        Me.txtBankCharges.Name = "txtBankCharges"
        Me.txtBankCharges.ReferenceFieldDesc = Nothing
        Me.txtBankCharges.ReferenceFieldName = Nothing
        Me.txtBankCharges.ReferenceTableName = Nothing
        Me.txtBankCharges.Size = New System.Drawing.Size(74, 20)
        Me.txtBankCharges.TabIndex = 18
        Me.txtBankCharges.Text = "0"
        Me.txtBankCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBankCharges.Value = 0R
        '
        'lblLoadOutNo
        '
        Me.lblLoadOutNo.FieldName = Nothing
        Me.lblLoadOutNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadOutNo.Location = New System.Drawing.Point(681, 5)
        Me.lblLoadOutNo.Name = "lblLoadOutNo"
        Me.lblLoadOutNo.Size = New System.Drawing.Size(68, 16)
        Me.lblLoadOutNo.TabIndex = 5
        Me.lblLoadOutNo.Text = "Loadout No."
        Me.lblLoadOutNo.Visible = False
        '
        'txtLoadOutno
        '
        Me.txtLoadOutno.CalculationExpression = Nothing
        Me.txtLoadOutno.FieldCode = Nothing
        Me.txtLoadOutno.FieldDesc = Nothing
        Me.txtLoadOutno.FieldMaxLength = 0
        Me.txtLoadOutno.FieldName = Nothing
        Me.txtLoadOutno.isCalculatedField = False
        Me.txtLoadOutno.IsSourceFromTable = False
        Me.txtLoadOutno.IsSourceFromValueList = False
        Me.txtLoadOutno.IsUnique = False
        Me.txtLoadOutno.Location = New System.Drawing.Point(755, 4)
        Me.txtLoadOutno.MendatroryField = True
        Me.txtLoadOutno.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadOutno.MyLinkLable1 = Nothing
        Me.txtLoadOutno.MyLinkLable2 = Nothing
        Me.txtLoadOutno.MyReadOnly = False
        Me.txtLoadOutno.MyShowMasterFormButton = False
        Me.txtLoadOutno.Name = "txtLoadOutno"
        Me.txtLoadOutno.ReferenceFieldDesc = Nothing
        Me.txtLoadOutno.ReferenceFieldName = Nothing
        Me.txtLoadOutno.ReferenceTableName = Nothing
        Me.txtLoadOutno.Size = New System.Drawing.Size(66, 19)
        Me.txtLoadOutno.TabIndex = 6
        Me.txtLoadOutno.Value = ""
        Me.txtLoadOutno.Visible = False
        '
        'txtPaymentAmt
        '
        Me.txtPaymentAmt.BackColor = System.Drawing.Color.White
        Me.txtPaymentAmt.CalculationExpression = Nothing
        Me.txtPaymentAmt.DecimalPlaces = 2
        Me.txtPaymentAmt.FieldCode = Nothing
        Me.txtPaymentAmt.FieldDesc = Nothing
        Me.txtPaymentAmt.FieldMaxLength = 0
        Me.txtPaymentAmt.FieldName = Nothing
        Me.txtPaymentAmt.isCalculatedField = False
        Me.txtPaymentAmt.IsSourceFromTable = False
        Me.txtPaymentAmt.IsSourceFromValueList = False
        Me.txtPaymentAmt.IsUnique = False
        Me.txtPaymentAmt.Location = New System.Drawing.Point(95, 158)
        Me.txtPaymentAmt.MendatroryField = False
        Me.txtPaymentAmt.MyLinkLable1 = Nothing
        Me.txtPaymentAmt.MyLinkLable2 = Nothing
        Me.txtPaymentAmt.Name = "txtPaymentAmt"
        Me.txtPaymentAmt.ReferenceFieldDesc = Nothing
        Me.txtPaymentAmt.ReferenceFieldName = Nothing
        Me.txtPaymentAmt.ReferenceTableName = Nothing
        Me.txtPaymentAmt.Size = New System.Drawing.Size(111, 20)
        Me.txtPaymentAmt.TabIndex = 25
        Me.txtPaymentAmt.Text = "0"
        Me.txtPaymentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPaymentAmt.Value = 0R
        '
        'pnlVendor
        '
        Me.pnlVendor.Controls.Add(Me.LblAccPayee)
        Me.pnlVendor.Controls.Add(Me.lblBalAmt)
        Me.pnlVendor.Controls.Add(Me.lblDocumentNo)
        Me.pnlVendor.Controls.Add(Me.txtDocumentNo)
        Me.pnlVendor.Controls.Add(Me.lblVendorName)
        Me.pnlVendor.Controls.Add(Me.txtVendorCode)
        Me.pnlVendor.Controls.Add(Me.lblvendorcode)
        Me.pnlVendor.Location = New System.Drawing.Point(3, 110)
        Me.pnlVendor.Name = "pnlVendor"
        Me.pnlVendor.Size = New System.Drawing.Size(676, 44)
        Me.pnlVendor.TabIndex = 23
        '
        'LblAccPayee
        '
        Me.LblAccPayee.AutoSize = False
        Me.LblAccPayee.BorderVisible = True
        Me.LblAccPayee.FieldName = Nothing
        Me.LblAccPayee.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAccPayee.Location = New System.Drawing.Point(577, 2)
        Me.LblAccPayee.Name = "LblAccPayee"
        Me.LblAccPayee.Size = New System.Drawing.Size(99, 18)
        Me.LblAccPayee.TabIndex = 6
        '
        'lblBalAmt
        '
        Me.lblBalAmt.AutoSize = False
        Me.lblBalAmt.BorderVisible = True
        Me.lblBalAmt.FieldName = Nothing
        Me.lblBalAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalAmt.Location = New System.Drawing.Point(257, 22)
        Me.lblBalAmt.Name = "lblBalAmt"
        Me.lblBalAmt.Size = New System.Drawing.Size(171, 18)
        Me.lblBalAmt.TabIndex = 0
        Me.lblBalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.FieldName = Nothing
        Me.lblDocumentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(3, 24)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(75, 16)
        Me.lblDocumentNo.TabIndex = 4
        Me.lblDocumentNo.Text = "Document No"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.CalculationExpression = Nothing
        Me.txtDocumentNo.FieldCode = Nothing
        Me.txtDocumentNo.FieldDesc = Nothing
        Me.txtDocumentNo.FieldMaxLength = 0
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.isCalculatedField = False
        Me.txtDocumentNo.IsSourceFromTable = False
        Me.txtDocumentNo.IsSourceFromValueList = False
        Me.txtDocumentNo.IsUnique = False
        Me.txtDocumentNo.Location = New System.Drawing.Point(94, 22)
        Me.txtDocumentNo.MendatroryField = True
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentNo.MyLinkLable1 = Nothing
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.MyShowMasterFormButton = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReferenceFieldDesc = Nothing
        Me.txtDocumentNo.ReferenceFieldName = Nothing
        Me.txtDocumentNo.ReferenceTableName = Nothing
        Me.txtDocumentNo.Size = New System.Drawing.Size(161, 19)
        Me.txtDocumentNo.TabIndex = 5
        Me.txtDocumentNo.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(257, 2)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(320, 19)
        Me.lblVendorName.TabIndex = 3
        '
        'txtVendorCode
        '
        Me.txtVendorCode.CalculationExpression = Nothing
        Me.txtVendorCode.FieldCode = Nothing
        Me.txtVendorCode.FieldDesc = Nothing
        Me.txtVendorCode.FieldMaxLength = 0
        Me.txtVendorCode.FieldName = Nothing
        Me.txtVendorCode.isCalculatedField = False
        Me.txtVendorCode.IsSourceFromTable = False
        Me.txtVendorCode.IsSourceFromValueList = False
        Me.txtVendorCode.IsUnique = False
        Me.txtVendorCode.Location = New System.Drawing.Point(94, 2)
        Me.txtVendorCode.MendatroryField = True
        Me.txtVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.MyLinkLable1 = Nothing
        Me.txtVendorCode.MyLinkLable2 = Nothing
        Me.txtVendorCode.MyReadOnly = False
        Me.txtVendorCode.MyShowMasterFormButton = False
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.ReferenceFieldDesc = Nothing
        Me.txtVendorCode.ReferenceFieldName = Nothing
        Me.txtVendorCode.ReferenceTableName = Nothing
        Me.txtVendorCode.Size = New System.Drawing.Size(159, 19)
        Me.txtVendorCode.TabIndex = 2
        Me.txtVendorCode.Value = ""
        '
        'lblvendorcode
        '
        Me.lblvendorcode.FieldName = Nothing
        Me.lblvendorcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvendorcode.Location = New System.Drawing.Point(3, 1)
        Me.lblvendorcode.Name = "lblvendorcode"
        Me.lblvendorcode.Size = New System.Drawing.Size(73, 16)
        Me.lblvendorcode.TabIndex = 1
        Me.lblvendorcode.Text = "Vendor Code"
        '
        'pnlCheque
        '
        Me.pnlCheque.Controls.Add(Me.ChkAccPayee)
        Me.pnlCheque.Controls.Add(Me.chkCheckPrint)
        Me.pnlCheque.Controls.Add(Me.chkPDC)
        Me.pnlCheque.Controls.Add(Me.lblchequeno)
        Me.pnlCheque.Controls.Add(Me.txtChequeNo)
        Me.pnlCheque.Controls.Add(Me.lblchequedate)
        Me.pnlCheque.Controls.Add(Me.dtpChequeDate)
        Me.pnlCheque.Location = New System.Drawing.Point(255, 87)
        Me.pnlCheque.Name = "pnlCheque"
        Me.pnlCheque.Size = New System.Drawing.Size(625, 22)
        Me.pnlCheque.TabIndex = 22
        '
        'ChkAccPayee
        '
        Me.ChkAccPayee.Location = New System.Drawing.Point(548, 2)
        Me.ChkAccPayee.Name = "ChkAccPayee"
        Me.ChkAccPayee.Size = New System.Drawing.Size(71, 18)
        Me.ChkAccPayee.TabIndex = 6
        Me.ChkAccPayee.Text = "A/C Payee"
        '
        'chkCheckPrint
        '
        Me.chkCheckPrint.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCheckPrint.Location = New System.Drawing.Point(187, 2)
        Me.chkCheckPrint.Name = "chkCheckPrint"
        Me.chkCheckPrint.Size = New System.Drawing.Size(77, 18)
        Me.chkCheckPrint.TabIndex = 2
        Me.chkCheckPrint.Text = "Check Print"
        '
        'chkPDC
        '
        Me.chkPDC.Location = New System.Drawing.Point(462, 2)
        Me.chkPDC.Name = "chkPDC"
        Me.chkPDC.Size = New System.Drawing.Size(83, 18)
        Me.chkPDC.TabIndex = 5
        Me.chkPDC.Text = "PDC Cheque"
        '
        'lblchequeno
        '
        Me.lblchequeno.FieldName = Nothing
        Me.lblchequeno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequeno.Location = New System.Drawing.Point(1, 3)
        Me.lblchequeno.Name = "lblchequeno"
        Me.lblchequeno.Size = New System.Drawing.Size(83, 16)
        Me.lblchequeno.TabIndex = 0
        Me.lblchequeno.Text = "Cheque/DD No"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CalculationExpression = Nothing
        Me.txtChequeNo.FieldCode = Nothing
        Me.txtChequeNo.FieldDesc = Nothing
        Me.txtChequeNo.FieldMaxLength = 0
        Me.txtChequeNo.FieldName = Nothing
        Me.txtChequeNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeNo.isCalculatedField = False
        Me.txtChequeNo.IsSourceFromTable = False
        Me.txtChequeNo.IsSourceFromValueList = False
        Me.txtChequeNo.IsUnique = False
        Me.txtChequeNo.Location = New System.Drawing.Point(84, 2)
        Me.txtChequeNo.MaxLength = 6
        Me.txtChequeNo.MendatroryField = False
        Me.txtChequeNo.MyLinkLable1 = Nothing
        Me.txtChequeNo.MyLinkLable2 = Nothing
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.ReferenceFieldDesc = Nothing
        Me.txtChequeNo.ReferenceFieldName = Nothing
        Me.txtChequeNo.ReferenceTableName = Nothing
        Me.txtChequeNo.Size = New System.Drawing.Size(102, 18)
        Me.txtChequeNo.TabIndex = 1
        '
        'lblchequedate
        '
        Me.lblchequedate.FieldName = Nothing
        Me.lblchequedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequedate.Location = New System.Drawing.Point(267, 3)
        Me.lblchequedate.Name = "lblchequedate"
        Me.lblchequedate.Size = New System.Drawing.Size(92, 16)
        Me.lblchequedate.TabIndex = 3
        Me.lblchequedate.Text = "Cheque/DD Date"
        '
        'dtpChequeDate
        '
        Me.dtpChequeDate.CalculationExpression = Nothing
        Me.dtpChequeDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpChequeDate.FieldCode = Nothing
        Me.dtpChequeDate.FieldDesc = Nothing
        Me.dtpChequeDate.FieldMaxLength = 0
        Me.dtpChequeDate.FieldName = Nothing
        Me.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDate.isCalculatedField = False
        Me.dtpChequeDate.IsSourceFromTable = False
        Me.dtpChequeDate.IsSourceFromValueList = False
        Me.dtpChequeDate.IsUnique = False
        Me.dtpChequeDate.Location = New System.Drawing.Point(356, 1)
        Me.dtpChequeDate.MendatroryField = False
        Me.dtpChequeDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDate.MyLinkLable1 = Nothing
        Me.dtpChequeDate.MyLinkLable2 = Nothing
        Me.dtpChequeDate.Name = "dtpChequeDate"
        Me.dtpChequeDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDate.ReferenceFieldDesc = Nothing
        Me.dtpChequeDate.ReferenceFieldName = Nothing
        Me.dtpChequeDate.ReferenceTableName = Nothing
        Me.dtpChequeDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpChequeDate.TabIndex = 4
        Me.dtpChequeDate.TabStop = False
        Me.dtpChequeDate.Text = "10/06/2011 12:34 PM"
        Me.dtpChequeDate.Value = New Date(2011, 6, 10, 12, 34, 10, 140)
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.FieldName = Nothing
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(256, 47)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(324, 18)
        Me.lblBankDesc.TabIndex = 12
        '
        'pnlAdvance
        '
        Me.pnlAdvance.Controls.Add(Me.lblNetPayableAmt)
        Me.pnlAdvance.Controls.Add(Me.txtNetPayableAmt)
        Me.pnlAdvance.Controls.Add(Me.lblTDSAmt)
        Me.pnlAdvance.Controls.Add(Me.txtTDSAmt)
        Me.pnlAdvance.Location = New System.Drawing.Point(207, 156)
        Me.pnlAdvance.Name = "pnlAdvance"
        Me.pnlAdvance.Size = New System.Drawing.Size(445, 24)
        Me.pnlAdvance.TabIndex = 117
        '
        'lblNetPayableAmt
        '
        Me.lblNetPayableAmt.FieldName = Nothing
        Me.lblNetPayableAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetPayableAmt.Location = New System.Drawing.Point(184, 6)
        Me.lblNetPayableAmt.Name = "lblNetPayableAmt"
        Me.lblNetPayableAmt.Size = New System.Drawing.Size(91, 16)
        Me.lblNetPayableAmt.TabIndex = 2
        Me.lblNetPayableAmt.Text = "Net Payable Amt"
        '
        'txtNetPayableAmt
        '
        Me.txtNetPayableAmt.AutoSize = False
        Me.txtNetPayableAmt.CalculationExpression = Nothing
        Me.txtNetPayableAmt.FieldCode = Nothing
        Me.txtNetPayableAmt.FieldDesc = Nothing
        Me.txtNetPayableAmt.FieldMaxLength = 0
        Me.txtNetPayableAmt.FieldName = Nothing
        Me.txtNetPayableAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetPayableAmt.isCalculatedField = False
        Me.txtNetPayableAmt.IsSourceFromTable = False
        Me.txtNetPayableAmt.IsSourceFromValueList = False
        Me.txtNetPayableAmt.IsUnique = False
        Me.txtNetPayableAmt.Location = New System.Drawing.Point(277, 2)
        Me.txtNetPayableAmt.MaxLength = 18
        Me.txtNetPayableAmt.MendatroryField = False
        Me.txtNetPayableAmt.Multiline = True
        Me.txtNetPayableAmt.MyLinkLable1 = Nothing
        Me.txtNetPayableAmt.MyLinkLable2 = Nothing
        Me.txtNetPayableAmt.Name = "txtNetPayableAmt"
        Me.txtNetPayableAmt.ReadOnly = True
        Me.txtNetPayableAmt.ReferenceFieldDesc = Nothing
        Me.txtNetPayableAmt.ReferenceFieldName = Nothing
        Me.txtNetPayableAmt.ReferenceTableName = Nothing
        Me.txtNetPayableAmt.Size = New System.Drawing.Size(142, 20)
        Me.txtNetPayableAmt.TabIndex = 3
        Me.txtNetPayableAmt.Text = "0"
        Me.txtNetPayableAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTDSAmt
        '
        Me.lblTDSAmt.FieldName = Nothing
        Me.lblTDSAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTDSAmt.Location = New System.Drawing.Point(4, 3)
        Me.lblTDSAmt.Name = "lblTDSAmt"
        Me.lblTDSAmt.Size = New System.Drawing.Size(71, 16)
        Me.lblTDSAmt.TabIndex = 0
        Me.lblTDSAmt.Text = "TDS Amount"
        '
        'txtTDSAmt
        '
        Me.txtTDSAmt.AutoSize = False
        Me.txtTDSAmt.CalculationExpression = Nothing
        Me.txtTDSAmt.FieldCode = Nothing
        Me.txtTDSAmt.FieldDesc = Nothing
        Me.txtTDSAmt.FieldMaxLength = 0
        Me.txtTDSAmt.FieldName = Nothing
        Me.txtTDSAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAmt.isCalculatedField = False
        Me.txtTDSAmt.IsSourceFromTable = False
        Me.txtTDSAmt.IsSourceFromValueList = False
        Me.txtTDSAmt.IsUnique = False
        Me.txtTDSAmt.Location = New System.Drawing.Point(79, 2)
        Me.txtTDSAmt.MaxLength = 18
        Me.txtTDSAmt.MendatroryField = False
        Me.txtTDSAmt.Multiline = True
        Me.txtTDSAmt.MyLinkLable1 = Nothing
        Me.txtTDSAmt.MyLinkLable2 = Nothing
        Me.txtTDSAmt.Name = "txtTDSAmt"
        Me.txtTDSAmt.ReadOnly = True
        Me.txtTDSAmt.ReferenceFieldDesc = Nothing
        Me.txtTDSAmt.ReferenceFieldName = Nothing
        Me.txtTDSAmt.ReferenceTableName = Nothing
        Me.txtTDSAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtTDSAmt.TabIndex = 1
        Me.txtTDSAmt.Text = "0"
        Me.txtTDSAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dtpPayment
        '
        Me.dtpPayment.CalculationExpression = Nothing
        Me.dtpPayment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpPayment.FieldCode = Nothing
        Me.dtpPayment.FieldDesc = Nothing
        Me.dtpPayment.FieldMaxLength = 0
        Me.dtpPayment.FieldName = Nothing
        Me.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPayment.isCalculatedField = False
        Me.dtpPayment.IsSourceFromTable = False
        Me.dtpPayment.IsSourceFromValueList = False
        Me.dtpPayment.IsUnique = False
        Me.dtpPayment.Location = New System.Drawing.Point(543, 3)
        Me.dtpPayment.MendatroryField = False
        Me.dtpPayment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.MyLinkLable1 = Nothing
        Me.dtpPayment.MyLinkLable2 = Nothing
        Me.dtpPayment.Name = "dtpPayment"
        Me.dtpPayment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.ReferenceFieldDesc = Nothing
        Me.dtpPayment.ReferenceFieldName = Nothing
        Me.dtpPayment.ReferenceTableName = Nothing
        Me.dtpPayment.Size = New System.Drawing.Size(133, 20)
        Me.dtpPayment.TabIndex = 4
        Me.dtpPayment.TabStop = False
        Me.dtpPayment.Text = "10/06/2011 11:51 AM"
        Me.dtpPayment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(94, 25)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(486, 20)
        Me.txtDescription.TabIndex = 9
        '
        'lblTotPayment
        '
        Me.lblTotPayment.FieldName = Nothing
        Me.lblTotPayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPayment.Location = New System.Drawing.Point(5, 160)
        Me.lblTotPayment.Name = "lblTotPayment"
        Me.lblTotPayment.Size = New System.Drawing.Size(79, 16)
        Me.lblTotPayment.TabIndex = 24
        Me.lblTotPayment.Text = "Total Payment"
        '
        'lbldesc
        '
        Me.lbldesc.FieldName = Nothing
        Me.lbldesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.Location = New System.Drawing.Point(5, 27)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(63, 16)
        Me.lbldesc.TabIndex = 8
        Me.lbldesc.Text = "Description"
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
        Me.txtPaymentMode.Location = New System.Drawing.Point(94, 88)
        Me.txtPaymentMode.MendatroryField = True
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(159, 18)
        Me.txtPaymentMode.TabIndex = 21
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
        Me.txtBankCode.Location = New System.Drawing.Point(94, 47)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(159, 19)
        Me.txtBankCode.TabIndex = 11
        Me.txtBankCode.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(1010, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(70, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 7
        '
        'txtPaymentNo
        '
        Me.txtPaymentNo.FieldName = Nothing
        Me.txtPaymentNo.Location = New System.Drawing.Point(94, 3)
        Me.txtPaymentNo.MendatroryField = False
        Me.txtPaymentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtPaymentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPaymentNo.MyLinkLable1 = Nothing
        Me.txtPaymentNo.MyLinkLable2 = Nothing
        Me.txtPaymentNo.MyMaxLength = 32767
        Me.txtPaymentNo.MyReadOnly = False
        Me.txtPaymentNo.Name = "txtPaymentNo"
        Me.txtPaymentNo.Size = New System.Drawing.Size(343, 20)
        Me.txtPaymentNo.TabIndex = 1
        Me.txtPaymentNo.Value = ""
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(437, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 20)
        Me.btnNew.TabIndex = 2
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(6, 86)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 20
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'ddlPaymentType
        '
        Me.ddlPaymentType.AutoCompleteDisplayMember = Nothing
        Me.ddlPaymentType.AutoCompleteValueMember = Nothing
        Me.ddlPaymentType.CalculationExpression = Nothing
        Me.ddlPaymentType.DropDownAnimationEnabled = True
        Me.ddlPaymentType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlPaymentType.FieldCode = Nothing
        Me.ddlPaymentType.FieldDesc = Nothing
        Me.ddlPaymentType.FieldMaxLength = 0
        Me.ddlPaymentType.FieldName = Nothing
        Me.ddlPaymentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlPaymentType.isCalculatedField = False
        Me.ddlPaymentType.IsSourceFromTable = False
        Me.ddlPaymentType.IsSourceFromValueList = False
        Me.ddlPaymentType.IsUnique = False
        Me.ddlPaymentType.Location = New System.Drawing.Point(94, 68)
        Me.ddlPaymentType.MendatroryField = False
        Me.ddlPaymentType.MyLinkLable1 = Nothing
        Me.ddlPaymentType.MyLinkLable2 = Nothing
        Me.ddlPaymentType.Name = "ddlPaymentType"
        Me.ddlPaymentType.ReferenceFieldDesc = Nothing
        Me.ddlPaymentType.ReferenceFieldName = Nothing
        Me.ddlPaymentType.ReferenceTableName = Nothing
        Me.ddlPaymentType.Size = New System.Drawing.Size(159, 18)
        Me.ddlPaymentType.TabIndex = 16
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(6, 5)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(69, 16)
        Me.lblpaymentno.TabIndex = 0
        Me.lblpaymentno.Text = "Payment No"
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(459, 5)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(78, 16)
        Me.lblpaymentdate.TabIndex = 3
        Me.lblpaymentdate.Text = "Payment Date"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(6, 47)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 10
        Me.lblbankcode.Text = "Bank Code"
        '
        'lblpaymenttype
        '
        Me.lblpaymenttype.FieldName = Nothing
        Me.lblpaymenttype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymenttype.Location = New System.Drawing.Point(6, 67)
        Me.lblpaymenttype.Name = "lblpaymenttype"
        Me.lblpaymenttype.Size = New System.Drawing.Size(79, 16)
        Me.lblpaymenttype.TabIndex = 15
        Me.lblpaymenttype.Text = "Payment Type"
        '
        'gvDetails
        '
        Me.gvDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetails.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDetails.ForeColor = System.Drawing.Color.Black
        Me.gvDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetails.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowDeleteRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetails.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.ShowHeaderCellButtons = True
        Me.gvDetails.Size = New System.Drawing.Size(1113, 151)
        Me.gvDetails.TabIndex = 0
        Me.gvDetails.TabStop = False
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(82.0!, 24.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1115, 441)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(75.0!, 24.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1115, 441)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1115, 441)
        Me.UcAttachment1.TabIndex = 0
        '
        'TabForGST
        '
        Me.TabForGST.Controls.Add(Me.SplitContainer3)
        Me.TabForGST.ItemSize = New System.Drawing.SizeF(32.0!, 24.0!)
        Me.TabForGST.Location = New System.Drawing.Point(10, 37)
        Me.TabForGST.Name = "TabForGST"
        Me.TabForGST.Size = New System.Drawing.Size(1115, 441)
        Me.TabForGST.Text = "GST"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.LBLPO_Location_GST)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TxtPO_Location_GST)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel36)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPOTotalAdditionalCharge)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel35)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPOTotalTaxAmt)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel34)
        Me.SplitContainer3.Panel1.Controls.Add(Me.LblPOTotalAmount)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDONo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel11)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtTaxGroup)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPONo_GST)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTaxGrpName)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(1115, 441)
        Me.SplitContainer3.SplitterDistance = 78
        Me.SplitContainer3.TabIndex = 0
        '
        'LBLPO_Location_GST
        '
        Me.LBLPO_Location_GST.AutoSize = False
        Me.LBLPO_Location_GST.BorderVisible = True
        Me.LBLPO_Location_GST.FieldName = Nothing
        Me.LBLPO_Location_GST.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPO_Location_GST.Location = New System.Drawing.Point(226, 51)
        Me.LBLPO_Location_GST.Name = "LBLPO_Location_GST"
        Me.LBLPO_Location_GST.Size = New System.Drawing.Size(321, 18)
        Me.LBLPO_Location_GST.TabIndex = 50
        Me.LBLPO_Location_GST.TextWrap = False
        '
        'TxtPO_Location_GST
        '
        Me.TxtPO_Location_GST.CalculationExpression = Nothing
        Me.TxtPO_Location_GST.Enabled = False
        Me.TxtPO_Location_GST.FieldCode = Nothing
        Me.TxtPO_Location_GST.FieldDesc = Nothing
        Me.TxtPO_Location_GST.FieldMaxLength = 0
        Me.TxtPO_Location_GST.FieldName = Nothing
        Me.TxtPO_Location_GST.isCalculatedField = False
        Me.TxtPO_Location_GST.IsSourceFromTable = False
        Me.TxtPO_Location_GST.IsSourceFromValueList = False
        Me.TxtPO_Location_GST.IsUnique = False
        Me.TxtPO_Location_GST.Location = New System.Drawing.Point(75, 50)
        Me.TxtPO_Location_GST.MendatroryField = False
        Me.TxtPO_Location_GST.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPO_Location_GST.MyLinkLable1 = Nothing
        Me.TxtPO_Location_GST.MyLinkLable2 = Nothing
        Me.TxtPO_Location_GST.MyReadOnly = False
        Me.TxtPO_Location_GST.MyShowMasterFormButton = False
        Me.TxtPO_Location_GST.Name = "TxtPO_Location_GST"
        Me.TxtPO_Location_GST.ReferenceFieldDesc = Nothing
        Me.TxtPO_Location_GST.ReferenceFieldName = Nothing
        Me.TxtPO_Location_GST.ReferenceTableName = Nothing
        Me.TxtPO_Location_GST.Size = New System.Drawing.Size(144, 18)
        Me.TxtPO_Location_GST.TabIndex = 48
        Me.TxtPO_Location_GST.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 51)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel1.TabIndex = 49
        Me.RadLabel1.Text = "PO Location"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(553, 29)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(144, 16)
        Me.MyLabel36.TabIndex = 30
        Me.MyLabel36.Text = "PO Total Additional Charge"
        '
        'lblPOTotalAdditionalCharge
        '
        Me.lblPOTotalAdditionalCharge.AutoSize = False
        Me.lblPOTotalAdditionalCharge.BorderVisible = True
        Me.lblPOTotalAdditionalCharge.FieldName = Nothing
        Me.lblPOTotalAdditionalCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOTotalAdditionalCharge.Location = New System.Drawing.Point(706, 28)
        Me.lblPOTotalAdditionalCharge.Name = "lblPOTotalAdditionalCharge"
        Me.lblPOTotalAdditionalCharge.Size = New System.Drawing.Size(110, 18)
        Me.lblPOTotalAdditionalCharge.TabIndex = 31
        Me.lblPOTotalAdditionalCharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(555, 9)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(143, 16)
        Me.MyLabel35.TabIndex = 28
        Me.MyLabel35.Text = "Payment Total Tax Amount"
        '
        'lblPOTotalTaxAmt
        '
        Me.lblPOTotalTaxAmt.AutoSize = False
        Me.lblPOTotalTaxAmt.BorderVisible = True
        Me.lblPOTotalTaxAmt.FieldName = Nothing
        Me.lblPOTotalTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOTotalTaxAmt.Location = New System.Drawing.Point(706, 8)
        Me.lblPOTotalTaxAmt.Name = "lblPOTotalTaxAmt"
        Me.lblPOTotalTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblPOTotalTaxAmt.TabIndex = 29
        Me.lblPOTotalTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(226, 9)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel34.TabIndex = 24
        Me.MyLabel34.Text = "PO Total Amount"
        '
        'LblPOTotalAmount
        '
        Me.LblPOTotalAmount.AutoSize = False
        Me.LblPOTotalAmount.BorderVisible = True
        Me.LblPOTotalAmount.FieldName = Nothing
        Me.LblPOTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPOTotalAmount.Location = New System.Drawing.Point(334, 8)
        Me.LblPOTotalAmount.Name = "LblPOTotalAmount"
        Me.LblPOTotalAmount.Size = New System.Drawing.Size(160, 18)
        Me.LblPOTotalAmount.TabIndex = 27
        Me.LblPOTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDONo
        '
        Me.lblDONo.FieldName = Nothing
        Me.lblDONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDONo.Location = New System.Drawing.Point(9, 9)
        Me.lblDONo.Name = "lblDONo"
        Me.lblDONo.Size = New System.Drawing.Size(40, 16)
        Me.lblDONo.TabIndex = 26
        Me.lblDONo.Text = "PO No"
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(9, 29)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 23
        Me.RadLabel11.Text = "Tax Group"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.Enabled = False
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(75, 28)
        Me.txtTaxGroup.MendatroryField = False
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyReadOnly = True
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(144, 18)
        Me.txtTaxGroup.TabIndex = 21
        Me.txtTaxGroup.Value = ""
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(226, 28)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 22
        Me.lblTaxGrpName.TextWrap = False
        '
        'txtPONo_GST
        '
        Me.txtPONo_GST.CalculationExpression = Nothing
        Me.txtPONo_GST.FieldCode = Nothing
        Me.txtPONo_GST.FieldDesc = Nothing
        Me.txtPONo_GST.FieldMaxLength = 0
        Me.txtPONo_GST.FieldName = Nothing
        Me.txtPONo_GST.isCalculatedField = False
        Me.txtPONo_GST.IsSourceFromTable = False
        Me.txtPONo_GST.IsSourceFromValueList = False
        Me.txtPONo_GST.IsUnique = False
        Me.txtPONo_GST.Location = New System.Drawing.Point(75, 7)
        Me.txtPONo_GST.MendatroryField = False
        Me.txtPONo_GST.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo_GST.MyLinkLable1 = Me.lblDONo
        Me.txtPONo_GST.MyLinkLable2 = Nothing
        Me.txtPONo_GST.MyReadOnly = True
        Me.txtPONo_GST.MyShowMasterFormButton = False
        Me.txtPONo_GST.Name = "txtPONo_GST"
        Me.txtPONo_GST.ReferenceFieldDesc = Nothing
        Me.txtPONo_GST.ReferenceFieldName = Nothing
        Me.txtPONo_GST.ReferenceTableName = Nothing
        Me.txtPONo_GST.Size = New System.Drawing.Size(144, 18)
        Me.txtPONo_GST.TabIndex = 25
        Me.txtPONo_GST.Value = ""
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gvTaxDetail)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer4.Size = New System.Drawing.Size(1115, 359)
        Me.SplitContainer4.SplitterDistance = 125
        Me.SplitContainer4.TabIndex = 0
        '
        'gvTaxDetail
        '
        Me.gvTaxDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvTaxDetail.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gvTaxDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTaxDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTaxDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTaxDetail.ForeColor = System.Drawing.Color.Black
        Me.gvTaxDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTaxDetail.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvTaxDetail.MasterTemplate.AllowAddNewRow = False
        Me.gvTaxDetail.MasterTemplate.AllowDeleteRow = False
        Me.gvTaxDetail.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvTaxDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTaxDetail.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvTaxDetail.Name = "gvTaxDetail"
        Me.gvTaxDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTaxDetail.ShowHeaderCellButtons = True
        Me.gvTaxDetail.Size = New System.Drawing.Size(1115, 125)
        Me.gvTaxDetail.TabIndex = 7
        Me.gvTaxDetail.TabStop = False
        '
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvItem.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvItem.ForeColor = System.Drawing.Color.Black
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItem.MasterTemplate.AllowAddNewRow = False
        Me.gvItem.MasterTemplate.AllowDeleteRow = False
        Me.gvItem.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(1115, 230)
        Me.gvItem.TabIndex = 8
        Me.gvItem.TabStop = False
        '
        'TabBankChargesTax
        '
        Me.TabBankChargesTax.Controls.Add(Me.butCostCenterAndHirerachy_Update_AfterPost)
        Me.TabBankChargesTax.Controls.Add(Me.GroupBox1)
        Me.TabBankChargesTax.Controls.Add(Me.txtTaxGroupBankCharges)
        Me.TabBankChargesTax.Controls.Add(Me.lblBankChargTaxGroup)
        Me.TabBankChargesTax.Controls.Add(Me.RadLabel10)
        Me.TabBankChargesTax.Controls.Add(Me.gv2)
        Me.TabBankChargesTax.Controls.Add(Me.MyLabel12)
        Me.TabBankChargesTax.ItemSize = New System.Drawing.SizeF(116.0!, 24.0!)
        Me.TabBankChargesTax.Location = New System.Drawing.Point(10, 37)
        Me.TabBankChargesTax.Name = "TabBankChargesTax"
        Me.TabBankChargesTax.Size = New System.Drawing.Size(1113, 441)
        Me.TabBankChargesTax.Text = "Tax on Bank Charges"
        '
        'butCostCenterAndHirerachy_Update_AfterPost
        '
        Me.butCostCenterAndHirerachy_Update_AfterPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCostCenterAndHirerachy_Update_AfterPost.Location = New System.Drawing.Point(924, 419)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Name = "butCostCenterAndHirerachy_Update_AfterPost"
        Me.butCostCenterAndHirerachy_Update_AfterPost.Size = New System.Drawing.Size(186, 22)
        Me.butCostCenterAndHirerachy_Update_AfterPost.TabIndex = 11
        Me.butCostCenterAndHirerachy_Update_AfterPost.Text = "Cost Center And Hirerachy Update"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(571, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        Me.GroupBox1.Visible = False
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(89, 13)
        Me.rbtnTaxCalManual.MyLinkLable1 = Nothing
        Me.rbtnTaxCalManual.MyLinkLable2 = Nothing
        Me.rbtnTaxCalManual.Name = "rbtnTaxCalManual"
        Me.rbtnTaxCalManual.Size = New System.Drawing.Size(57, 18)
        Me.rbtnTaxCalManual.TabIndex = 1
        Me.rbtnTaxCalManual.TabStop = False
        Me.rbtnTaxCalManual.Text = "Manual"
        '
        'rbtnTaxCalAutomatic
        '
        Me.rbtnTaxCalAutomatic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnTaxCalAutomatic.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTaxCalAutomatic.MyLinkLable1 = Nothing
        Me.rbtnTaxCalAutomatic.MyLinkLable2 = Nothing
        Me.rbtnTaxCalAutomatic.Name = "rbtnTaxCalAutomatic"
        Me.rbtnTaxCalAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnTaxCalAutomatic.TabIndex = 0
        Me.rbtnTaxCalAutomatic.Text = "Automatic"
        Me.rbtnTaxCalAutomatic.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtTaxGroupBankCharges
        '
        Me.txtTaxGroupBankCharges.CalculationExpression = Nothing
        Me.txtTaxGroupBankCharges.FieldCode = Nothing
        Me.txtTaxGroupBankCharges.FieldDesc = Nothing
        Me.txtTaxGroupBankCharges.FieldMaxLength = 0
        Me.txtTaxGroupBankCharges.FieldName = Nothing
        Me.txtTaxGroupBankCharges.isCalculatedField = False
        Me.txtTaxGroupBankCharges.IsSourceFromTable = False
        Me.txtTaxGroupBankCharges.IsSourceFromValueList = False
        Me.txtTaxGroupBankCharges.IsUnique = False
        Me.txtTaxGroupBankCharges.Location = New System.Drawing.Point(87, 12)
        Me.txtTaxGroupBankCharges.MendatroryField = True
        Me.txtTaxGroupBankCharges.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroupBankCharges.MyLinkLable1 = Me.MyLabel12
        Me.txtTaxGroupBankCharges.MyLinkLable2 = Me.lblBankChargTaxGroup
        Me.txtTaxGroupBankCharges.MyReadOnly = False
        Me.txtTaxGroupBankCharges.MyShowMasterFormButton = False
        Me.txtTaxGroupBankCharges.Name = "txtTaxGroupBankCharges"
        Me.txtTaxGroupBankCharges.ReferenceFieldDesc = Nothing
        Me.txtTaxGroupBankCharges.ReferenceFieldName = Nothing
        Me.txtTaxGroupBankCharges.ReferenceTableName = Nothing
        Me.txtTaxGroupBankCharges.Size = New System.Drawing.Size(143, 19)
        Me.txtTaxGroupBankCharges.TabIndex = 5
        Me.txtTaxGroupBankCharges.Value = ""
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(21, 14)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel12.TabIndex = 8
        Me.MyLabel12.Text = "Tax Group"
        '
        'lblBankChargTaxGroup
        '
        Me.lblBankChargTaxGroup.AutoSize = False
        Me.lblBankChargTaxGroup.BorderVisible = True
        Me.lblBankChargTaxGroup.FieldName = Nothing
        Me.lblBankChargTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankChargTaxGroup.Location = New System.Drawing.Point(243, 12)
        Me.lblBankChargTaxGroup.Name = "lblBankChargTaxGroup"
        Me.lblBankChargTaxGroup.Size = New System.Drawing.Size(321, 20)
        Me.lblBankChargTaxGroup.TabIndex = 10
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(862, 323)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(162, 16)
        Me.RadLabel10.TabIndex = 9
        Me.RadLabel10.Text = "Double click To Change Rate"
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
        Me.gv2.Location = New System.Drawing.Point(20, 46)
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
        Me.gv2.Size = New System.Drawing.Size(1005, 271)
        Me.gv2.TabIndex = 7
        Me.gv2.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1134, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export Blank Sheet"
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "Import-Save Only"
        Me.rmiImport.AccessibleName = "Import-Save Only"
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'btnOpenBankCashBook
        '
        Me.btnOpenBankCashBook.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenBankCashBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenBankCashBook.Location = New System.Drawing.Point(877, 2)
        Me.btnOpenBankCashBook.Name = "btnOpenBankCashBook"
        Me.btnOpenBankCashBook.Size = New System.Drawing.Size(124, 18)
        Me.btnOpenBankCashBook.TabIndex = 14
        Me.btnOpenBankCashBook.Text = "Open Bank Cash Book"
        '
        'btnPaymentAdvice
        '
        Me.btnPaymentAdvice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPaymentAdvice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaymentAdvice.Location = New System.Drawing.Point(387, 2)
        Me.btnPaymentAdvice.Name = "btnPaymentAdvice"
        Me.btnPaymentAdvice.Size = New System.Drawing.Size(91, 18)
        Me.btnPaymentAdvice.TabIndex = 12
        Me.btnPaymentAdvice.Text = "Payment Advice"
        '
        'btnRtgs
        '
        Me.btnRtgs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRtgs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRtgs.Location = New System.Drawing.Point(330, 2)
        Me.btnRtgs.Name = "btnRtgs"
        Me.btnRtgs.Size = New System.Drawing.Size(57, 18)
        Me.btnRtgs.TabIndex = 11
        Me.btnRtgs.Text = "RTGS "
        '
        'btnVoidCheck
        '
        Me.btnVoidCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVoidCheck.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVoidCheck.Location = New System.Drawing.Point(804, 2)
        Me.btnVoidCheck.Name = "btnVoidCheck"
        Me.btnVoidCheck.Size = New System.Drawing.Size(73, 18)
        Me.btnVoidCheck.TabIndex = 10
        Me.btnVoidCheck.Text = "Void Check"
        '
        'btnChqUpdate
        '
        Me.btnChqUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChqUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChqUpdate.Location = New System.Drawing.Point(478, 2)
        Me.btnChqUpdate.Name = "btnChqUpdate"
        Me.btnChqUpdate.Size = New System.Drawing.Size(138, 18)
        Me.btnChqUpdate.TabIndex = 9
        Me.btnChqUpdate.Text = "Cheque No/Date Update"
        Me.btnChqUpdate.Visible = False
        '
        'btnPrintCheck
        '
        Me.btnPrintCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintCheck.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintCheck.Location = New System.Drawing.Point(1002, 2)
        Me.btnPrintCheck.Name = "btnPrintCheck"
        Me.btnPrintCheck.Size = New System.Drawing.Size(79, 18)
        Me.btnPrintCheck.TabIndex = 7
        Me.btnPrintCheck.Text = "Print Check"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(691, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(113, 18)
        Me.btnReverse.TabIndex = 6
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewTDSDetails.Enabled = False
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(200, 2)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(68, 18)
        Me.btnViewTDSDetails.TabIndex = 3
        Me.btnViewTDSDetails.Text = "View TDS"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(268, 2)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(62, 18)
        Me.btnprint.TabIndex = 4
        Me.btnprint.Text = "Print"
        '
        'BtnBank
        '
        Me.BtnBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBank.Location = New System.Drawing.Point(614, 2)
        Me.BtnBank.Name = "BtnBank"
        Me.BtnBank.Size = New System.Drawing.Size(77, 18)
        Me.BtnBank.TabIndex = 5
        Me.BtnBank.Text = "Bank Transfer"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(63, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(68, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.Location = New System.Drawing.Point(134, 2)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(66, 18)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1082, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(47, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1115, 441)
        Me.UcCustomFields1.TabIndex = 1
        '
        'FrmPaymentNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1134, 538)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPaymentNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Payment Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.ChkRetention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpVendorBankDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVendorBankDetails.ResumeLayout(False)
        Me.grpVendorBankDetails.PerformLayout()
        CType(Me.txtVendor_Bank_ACNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendor_bankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtVendorBank_IFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorBank_branchname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtVendor_BankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlmemorndm.ResumeLayout(False)
        Me.pnlmemorndm.PerformLayout()
        CType(Me.chkmemorndm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmemoamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPaymentBaseCurr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkTDSProvision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFarmerLoanPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMiscPayment.ResumeLayout(False)
        Me.pnlMiscPayment.PerformLayout()
        CType(Me.txtRemitTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAppliedAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnImportPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlEmployeeAdvanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnExportPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMPAdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlEmployeeType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTapalNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDataAndTimeSelection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBankChargesWaveOff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEMI.ResumeLayout(False)
        Me.pnlEMI.PerformLayout()
        CType(Me.txtNoOfEMI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplyLoanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAdvSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerOutStanding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblPONo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPJC.ResumeLayout(False)
        Me.pnlPJC.PerformLayout()
        CType(Me.lblProjDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProjCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCform.ResumeLayout(False)
        Me.pnlCform.PerformLayout()
        CType(Me.chkCForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoadOutNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVendor.ResumeLayout(False)
        Me.pnlVendor.PerformLayout()
        CType(Me.LblAccPayee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCheque.ResumeLayout(False)
        Me.pnlCheque.PerformLayout()
        CType(Me.ChkAccPayee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChequeDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdvance.ResumeLayout(False)
        Me.pnlAdvance.PerformLayout()
        CType(Me.lblNetPayableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetPayableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTDSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTDSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymenttype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.TabForGST.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.LBLPO_Location_GST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPOTotalAdditionalCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPOTotalTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblPOTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gvTaxDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabBankChargesTax.ResumeLayout(False)
        Me.TabBankChargesTax.PerformLayout()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankChargTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPaymentAdvice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRtgs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnVoidCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnChqUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblTotPayment As common.Controls.MyLabel
    Friend WithEvents lbldesc As common.Controls.MyLabel
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtPaymentNo As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpChequeDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtChequeNo As common.Controls.MyTextBox
    Friend WithEvents lblchequedate As common.Controls.MyLabel
    Friend WithEvents lblchequeno As common.Controls.MyLabel
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents ddlPaymentType As common.Controls.MyComboBox
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents lblpaymenttype As common.Controls.MyLabel
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpPayment As common.Controls.MyDateTimePicker
    Friend WithEvents btnViewTDSDetails As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnBank As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlMiscPayment As System.Windows.Forms.Panel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTotalAppliedAmt As common.Controls.MyTextBox
    Friend WithEvents pnlAdvance As System.Windows.Forms.Panel
    Friend WithEvents lblNetPayableAmt As common.Controls.MyLabel
    Friend WithEvents txtNetPayableAmt As common.Controls.MyTextBox
    Friend WithEvents lblTDSAmt As common.Controls.MyLabel
    Friend WithEvents txtTDSAmt As common.Controls.MyTextBox
    Friend WithEvents txtRemitTo As common.Controls.MyTextBox
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
    Friend WithEvents pnlCheque As System.Windows.Forms.Panel
    Friend WithEvents pnlVendor As System.Windows.Forms.Panel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents txtVendorCode As common.UserControls.txtFinder
    Friend WithEvents lblvendorcode As common.Controls.MyLabel
    Friend WithEvents txtPaymentAmt As common.MyNumBox
    Friend WithEvents lblLoadOutNo As common.Controls.MyLabel
    Friend WithEvents txtLoadOutno As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtBankCharges As common.MyNumBox
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtBaseCurrency As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtTotalPaymentBaseCurr As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents pnlCform As System.Windows.Forms.Panel
    Friend WithEvents chkCForm As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtCFormInvNo As common.UserControls.txtFinder
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblProjDesc As common.Controls.MyLabel
    Friend WithEvents lblEmpDesc As common.Controls.MyLabel
    Friend WithEvents lblProjCode As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents pnlPJC As System.Windows.Forms.Panel
    Friend WithEvents chkPDC As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCheckPrint As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnPrintCheck As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlmemorndm As System.Windows.Forms.Panel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtmemoamt As common.Controls.MyTextBox
    Friend WithEvents chkmemorndm As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblDocumentNo As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtFinder
    Friend WithEvents lblBalAmt As common.Controls.MyLabel
    Friend WithEvents ChkAccPayee As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents LblPONo As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.UserControls.txtFinder
    Friend WithEvents ChkSecurity As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents LblAccPayee As common.Controls.MyLabel
    Friend WithEvents lblCustomerOutStanding As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblLocDesp As common.Controls.MyLabel
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel18 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnChqUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnVoidCheck As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkAdvSalary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnRtgs As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPaymentAdvice As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndloanNo As common.UserControls.txtFinder
    Friend WithEvents lblApplyLoanNo As common.Controls.MyLabel
    Friend WithEvents rbtnImportPosted As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnExportPosted As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlEMI As System.Windows.Forms.Panel
    Friend WithEvents txtNoOfEMI As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtInterestRate As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TabForGST As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lblPOTotalTaxAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents LblPOTotalAmount As common.Controls.MyLabel
    Friend WithEvents lblDONo As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents txtPONo_GST As common.UserControls.txtFinder
    Friend WithEvents gvTaxDetail As common.UserControls.MyRadGridView
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents lblPOTotalAdditionalCharge As common.Controls.MyLabel
    Friend WithEvents LBLPO_Location_GST As common.Controls.MyLabel
    Friend WithEvents TxtPO_Location_GST As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents TabBankChargesTax As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroupBankCharges As common.UserControls.txtFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblBankChargTaxGroup As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents ddlEmployeeType As common.Controls.MyComboBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents ddlEmployeeAdvanceType As common.Controls.MyComboBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents chkBankChargesWaveOff As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnOpenBankCashBook As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtTapalNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents txtDataAndTimeSelection As common.Controls.MyDateTimePicker
    Friend WithEvents lblMPAdv As common.Controls.MyLabel
    Friend WithEvents txtMPAdv As common.UserControls.txtFinder
    Friend WithEvents chkIsReceipt As RadCheckBox
    Friend WithEvents chkFarmerLoanPayment As RadCheckBox
    Friend WithEvents grpVendorBankDetails As RadGroupBox
    Friend WithEvents txtVendor_Bank_ACNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtVendor_bankcode As common.Controls.MyTextBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents TxtVendorBank_IFSCCode As common.Controls.MyTextBox
    Friend WithEvents txtVendorBank_branchname As common.Controls.MyTextBox
    Friend WithEvents TxtVendor_BankName As common.Controls.MyTextBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents butCostCenterAndHirerachy_Update_AfterPost As RadButton
    Friend WithEvents chkSaving As RadCheckBox
    Friend WithEvents chkTDSProvision As common.Controls.MyCheckBox
    Friend WithEvents ChkRetention As RadCheckBox
End Class

