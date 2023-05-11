<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWorkEstimationEng
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.txtRequisition = New common.UserControls.txtFinder()
        Me.chkTender = New common.Controls.MyCheckBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.pnl_capex = New System.Windows.Forms.Panel()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.lblEmail = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.lbl_rebudgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_rebudgetamt = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lbl_budgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_budgetamt = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblApprovalDate = New common.Controls.MyLabel()
        Me.chk_emergency = New common.Controls.MyCheckBox()
        Me.ddl_category = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.fndcapexsubcode = New common.UserControls.txtFinder()
        Me.lbl_capexsubcode = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.fndcapexcode = New common.UserControls.txtFinder()
        Me.lbl_capexcode = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.chkOpenPO = New common.Controls.MyCheckBox()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtRequestBy = New common.Controls.MyTextBox()
        Me.lblDept = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.txtRequiredDate = New common.Controls.MyDateTimePicker()
        Me.cboModeOfTransport = New common.Controls.MyComboBox()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkInternal = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtReqNo = New common.UserControls.txtNavigator()
        Me.txtExpireDate = New common.Controls.MyDateTimePicker()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.txtCustOrderNo = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New XpertERPEngine.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.WorkOrder = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblSubmittedto = New common.Controls.MyLabel()
        Me.lblContent = New common.Controls.MyLabel()
        Me.lblSubject = New common.Controls.MyLabel()
        Me.txtCopySubmit = New common.Controls.MyTextBox()
        Me.txtContent = New common.Controls.MyTextBox()
        Me.txtSubject = New common.Controls.MyTextBox()
        Me.txtTo = New common.Controls.MyTextBox()
        Me.lblTo = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkExciseOnQty = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
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
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTotalComment = New System.Windows.Forms.TextBox()
        Me.txtTotalSubject = New System.Windows.Forms.TextBox()
        Me.txtHeaderDiscountAmount = New common.MyNumBox()
        Me.MyLabel53 = New common.Controls.MyLabel()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.lblTaxableAmount = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.lblAmtAfterTax = New common.Controls.MyLabel()
        Me.txtKindAttentation = New System.Windows.Forms.TextBox()
        Me.lblKindAttentation = New common.Controls.MyLabel()
        Me.txtContentSubject = New System.Windows.Forms.TextBox()
        Me.lblContentSubject = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.btnEmailsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.BtnMailPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnMailSendemail = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkprclose = New System.Windows.Forms.CheckBox()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RdEmailAndSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.pnl_capex.SuspendLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.WorkOrder.SuspendLayout()
        CType(Me.lblSubmittedto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCopySubmit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.chkExciseOnQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.txtHeaderDiscountAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxableAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKindAttentation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblContentSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEmailsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnEmailsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkprclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(935, 509)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.WorkOrder)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(935, 465)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "Custom Fields"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequisition)
        Me.RadPageViewPage1.Controls.Add(Me.chkTender)
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.Controls.Add(Me.chkOpenPO)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.cboPOType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.lblDept)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtDept)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequiredDate)
        Me.RadPageViewPage1.Controls.Add(Me.cboModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkInternal)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtExpireDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRmks)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustOrderNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(99.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(914, 419)
        Me.RadPageViewPage1.Text = "Work Estimation"
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(454, 4)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel23.TabIndex = 75
        Me.RadLabel23.Text = "Req No"
        '
        'txtRequisition
        '
        Me.txtRequisition.CalculationExpression = Nothing
        Me.txtRequisition.FieldCode = Nothing
        Me.txtRequisition.FieldDesc = Nothing
        Me.txtRequisition.FieldMaxLength = 0
        Me.txtRequisition.FieldName = Nothing
        Me.txtRequisition.isCalculatedField = False
        Me.txtRequisition.IsSourceFromTable = False
        Me.txtRequisition.IsSourceFromValueList = False
        Me.txtRequisition.IsUnique = False
        Me.txtRequisition.Location = New System.Drawing.Point(504, 1)
        Me.txtRequisition.MendatroryField = False
        Me.txtRequisition.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequisition.MyLinkLable1 = Me.RadLabel23
        Me.txtRequisition.MyLinkLable2 = Nothing
        Me.txtRequisition.MyReadOnly = True
        Me.txtRequisition.MyShowMasterFormButton = False
        Me.txtRequisition.Name = "txtRequisition"
        Me.txtRequisition.ReferenceFieldDesc = Nothing
        Me.txtRequisition.ReferenceFieldName = Nothing
        Me.txtRequisition.ReferenceTableName = Nothing
        Me.txtRequisition.Size = New System.Drawing.Size(145, 19)
        Me.txtRequisition.TabIndex = 74
        Me.txtRequisition.Value = ""
        '
        'chkTender
        '
        Me.chkTender.Location = New System.Drawing.Point(311, 108)
        Me.chkTender.MyLinkLable1 = Nothing
        Me.chkTender.MyLinkLable2 = Nothing
        Me.chkTender.Name = "chkTender"
        Me.chkTender.Size = New System.Drawing.Size(55, 18)
        Me.chkTender.TabIndex = 73
        Me.chkTender.Tag1 = Nothing
        Me.chkTender.Text = "Tender"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 129)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.pnl_capex)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(908, 218)
        Me.SplitContainer3.SplitterDistance = 92
        Me.SplitContainer3.TabIndex = 72
        '
        'pnl_capex
        '
        Me.pnl_capex.Controls.Add(Me.txtEmail)
        Me.pnl_capex.Controls.Add(Me.lblEmail)
        Me.pnl_capex.Controls.Add(Me.MyLabel37)
        Me.pnl_capex.Controls.Add(Me.MyLabel38)
        Me.pnl_capex.Controls.Add(Me.MyLabel40)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel35)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel3)
        Me.pnl_capex.Controls.Add(Me.lblApprovalDate)
        Me.pnl_capex.Controls.Add(Me.chk_emergency)
        Me.pnl_capex.Controls.Add(Me.ddl_category)
        Me.pnl_capex.Controls.Add(Me.MyLabel2)
        Me.pnl_capex.Controls.Add(Me.MyLabel36)
        Me.pnl_capex.Controls.Add(Me.fndcapexsubcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexsubcode)
        Me.pnl_capex.Controls.Add(Me.MyLabel34)
        Me.pnl_capex.Controls.Add(Me.fndcapexcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexcode)
        Me.pnl_capex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_capex.Location = New System.Drawing.Point(0, 0)
        Me.pnl_capex.Name = "pnl_capex"
        Me.pnl_capex.Size = New System.Drawing.Size(908, 92)
        Me.pnl_capex.TabIndex = 71
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(569, 51)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.RadLabel7
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(336, 18)
        Me.txtEmail.TabIndex = 55
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(463, 25)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(105, 16)
        Me.RadLabel7.TabIndex = 27
        Me.RadLabel7.Text = "Customer Order No"
        '
        'lblEmail
        '
        Me.lblEmail.FieldName = Nothing
        Me.lblEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(468, 51)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(47, 16)
        Me.lblEmail.TabIndex = 54
        Me.lblEmail.Text = "Email Id"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(211, 72)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(147, 16)
        Me.MyLabel37.TabIndex = 52
        Me.MyLabel37.Text = "Bal. Amount With Tolerence"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(211, 50)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(125, 16)
        Me.MyLabel38.TabIndex = 48
        Me.MyLabel38.Text = "Amount With Tolerence"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(5, 72)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel40.TabIndex = 50
        Me.MyLabel40.Text = "Bal. Amount"
        '
        'lbl_rebudgetamtwithtolerence
        '
        Me.lbl_rebudgetamtwithtolerence.AutoSize = False
        Me.lbl_rebudgetamtwithtolerence.BorderVisible = True
        Me.lbl_rebudgetamtwithtolerence.FieldName = Nothing
        Me.lbl_rebudgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamtwithtolerence.Location = New System.Drawing.Point(363, 71)
        Me.lbl_rebudgetamtwithtolerence.Name = "lbl_rebudgetamtwithtolerence"
        Me.lbl_rebudgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamtwithtolerence.TabIndex = 53
        Me.lbl_rebudgetamtwithtolerence.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_rebudgetamtwithtolerence.TextWrap = False
        '
        'lbl_rebudgetamt
        '
        Me.lbl_rebudgetamt.AutoSize = False
        Me.lbl_rebudgetamt.BorderVisible = True
        Me.lbl_rebudgetamt.FieldName = Nothing
        Me.lbl_rebudgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamt.Location = New System.Drawing.Point(109, 71)
        Me.lbl_rebudgetamt.Name = "lbl_rebudgetamt"
        Me.lbl_rebudgetamt.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamt.TabIndex = 51
        Me.lbl_rebudgetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_rebudgetamt.TextWrap = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(5, 50)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel35.TabIndex = 46
        Me.MyLabel35.Text = "Amount"
        '
        'lbl_budgetamtwithtolerence
        '
        Me.lbl_budgetamtwithtolerence.AutoSize = False
        Me.lbl_budgetamtwithtolerence.BorderVisible = True
        Me.lbl_budgetamtwithtolerence.FieldName = Nothing
        Me.lbl_budgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamtwithtolerence.Location = New System.Drawing.Point(363, 49)
        Me.lbl_budgetamtwithtolerence.Name = "lbl_budgetamtwithtolerence"
        Me.lbl_budgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_budgetamtwithtolerence.TabIndex = 49
        Me.lbl_budgetamtwithtolerence.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_budgetamtwithtolerence.TextWrap = False
        '
        'lbl_budgetamt
        '
        Me.lbl_budgetamt.AutoSize = False
        Me.lbl_budgetamt.BorderVisible = True
        Me.lbl_budgetamt.FieldName = Nothing
        Me.lbl_budgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamt.Location = New System.Drawing.Point(109, 49)
        Me.lbl_budgetamt.Name = "lbl_budgetamt"
        Me.lbl_budgetamt.Size = New System.Drawing.Size(100, 19)
        Me.lbl_budgetamt.TabIndex = 47
        Me.lbl_budgetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_budgetamt.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(468, 6)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Approval Date"
        '
        'lblApprovalDate
        '
        Me.lblApprovalDate.AutoSize = False
        Me.lblApprovalDate.BorderVisible = True
        Me.lblApprovalDate.FieldName = Nothing
        Me.lblApprovalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovalDate.Location = New System.Drawing.Point(569, 4)
        Me.lblApprovalDate.Name = "lblApprovalDate"
        Me.lblApprovalDate.Size = New System.Drawing.Size(193, 19)
        Me.lblApprovalDate.TabIndex = 45
        Me.lblApprovalDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblApprovalDate.TextWrap = False
        '
        'chk_emergency
        '
        Me.chk_emergency.Location = New System.Drawing.Point(258, 6)
        Me.chk_emergency.MyLinkLable1 = Nothing
        Me.chk_emergency.MyLinkLable2 = Nothing
        Me.chk_emergency.Name = "chk_emergency"
        Me.chk_emergency.Size = New System.Drawing.Size(75, 18)
        Me.chk_emergency.TabIndex = 43
        Me.chk_emergency.Tag1 = Nothing
        Me.chk_emergency.Text = "Emergency"
        '
        'ddl_category
        '
        Me.ddl_category.AutoCompleteDisplayMember = Nothing
        Me.ddl_category.AutoCompleteValueMember = Nothing
        Me.ddl_category.CalculationExpression = Nothing
        Me.ddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_category.FieldCode = Nothing
        Me.ddl_category.FieldDesc = Nothing
        Me.ddl_category.FieldMaxLength = 0
        Me.ddl_category.FieldName = Nothing
        Me.ddl_category.isCalculatedField = False
        Me.ddl_category.IsSourceFromTable = False
        Me.ddl_category.IsSourceFromValueList = False
        Me.ddl_category.IsUnique = False
        Me.ddl_category.Location = New System.Drawing.Point(109, 4)
        Me.ddl_category.MendatroryField = True
        Me.ddl_category.MyLinkLable1 = Me.MyLabel2
        Me.ddl_category.MyLinkLable2 = Nothing
        Me.ddl_category.Name = "ddl_category"
        Me.ddl_category.ReferenceFieldDesc = Nothing
        Me.ddl_category.ReferenceFieldName = Nothing
        Me.ddl_category.ReferenceTableName = Nothing
        Me.ddl_category.Size = New System.Drawing.Size(136, 20)
        Me.ddl_category.TabIndex = 41
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel2.TabIndex = 42
        Me.MyLabel2.Text = "Category"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(5, 27)
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
        Me.fndcapexsubcode.Location = New System.Drawing.Point(109, 27)
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
        Me.fndcapexsubcode.Size = New System.Drawing.Size(143, 20)
        Me.fndcapexsubcode.TabIndex = 6
        Me.fndcapexsubcode.Value = ""
        '
        'lbl_capexsubcode
        '
        Me.lbl_capexsubcode.AutoSize = False
        Me.lbl_capexsubcode.BorderVisible = True
        Me.lbl_capexsubcode.FieldName = Nothing
        Me.lbl_capexsubcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexsubcode.Location = New System.Drawing.Point(258, 27)
        Me.lbl_capexsubcode.Name = "lbl_capexsubcode"
        Me.lbl_capexsubcode.Size = New System.Drawing.Size(205, 19)
        Me.lbl_capexsubcode.TabIndex = 8
        Me.lbl_capexsubcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_capexsubcode.TextWrap = False
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(467, 30)
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
        Me.fndcapexcode.Location = New System.Drawing.Point(569, 27)
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
        Me.fndcapexcode.Size = New System.Drawing.Size(143, 19)
        Me.fndcapexcode.TabIndex = 3
        Me.fndcapexcode.Value = ""
        '
        'lbl_capexcode
        '
        Me.lbl_capexcode.AutoSize = False
        Me.lbl_capexcode.BorderVisible = True
        Me.lbl_capexcode.FieldName = Nothing
        Me.lbl_capexcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexcode.Location = New System.Drawing.Point(715, 28)
        Me.lbl_capexcode.Name = "lbl_capexcode"
        Me.lbl_capexcode.Size = New System.Drawing.Size(192, 19)
        Me.lbl_capexcode.TabIndex = 5
        Me.lbl_capexcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_capexcode.TextWrap = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(908, 122)
        Me.RadGroupBox2.TabIndex = 18
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
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(888, 92)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'chkOpenPO
        '
        Me.chkOpenPO.Location = New System.Drawing.Point(228, 107)
        Me.chkOpenPO.MyLinkLable1 = Nothing
        Me.chkOpenPO.MyLinkLable2 = Nothing
        Me.chkOpenPO.Name = "chkOpenPO"
        Me.chkOpenPO.Size = New System.Drawing.Size(77, 18)
        Me.chkOpenPO.TabIndex = 37
        Me.chkOpenPO.Tag1 = Nothing
        Me.chkOpenPO.Text = "Is Open PO"
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
        Me.cboItemType.Location = New System.Drawing.Point(86, 85)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.MyLabel1
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(136, 20)
        Me.cboItemType.TabIndex = 38
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(1, 108)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 31
        Me.MyLabel1.Text = "Type"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = True
        Me.UcItemBalance1.CommitedQtyLbl = True
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 353)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.TabIndex = 20
        Me.UcItemBalance1.TabStop = False
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.RadLabel15)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(457, 106)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(455, 22)
        Me.pnlPCJ.TabIndex = 17
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(6, 1)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel15.TabIndex = 1
        Me.RadLabel15.Text = "Project"
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
        Me.fndProject.Location = New System.Drawing.Point(115, 1)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.RadLabel15
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
        Me.lblProject.Location = New System.Drawing.Point(261, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(193, 20)
        Me.lblProject.TabIndex = 2
        Me.lblProject.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProject.TextWrap = False
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
        Me.cboPOType.Location = New System.Drawing.Point(86, 107)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.MyLabel1
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(136, 20)
        Me.cboPOType.TabIndex = 16
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(227, 86)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel12.TabIndex = 30
        Me.RadLabel12.Text = "Requested By"
        '
        'txtRequestBy
        '
        Me.txtRequestBy.CalculationExpression = Nothing
        Me.txtRequestBy.FieldCode = Nothing
        Me.txtRequestBy.FieldDesc = Nothing
        Me.txtRequestBy.FieldMaxLength = 0
        Me.txtRequestBy.FieldName = Nothing
        Me.txtRequestBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestBy.isCalculatedField = False
        Me.txtRequestBy.IsSourceFromTable = False
        Me.txtRequestBy.IsSourceFromValueList = False
        Me.txtRequestBy.IsUnique = False
        Me.txtRequestBy.Location = New System.Drawing.Point(310, 85)
        Me.txtRequestBy.MaxLength = 100
        Me.txtRequestBy.MendatroryField = False
        Me.txtRequestBy.MyLinkLable1 = Me.RadLabel12
        Me.txtRequestBy.MyLinkLable2 = Nothing
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.ReferenceFieldDesc = Nothing
        Me.txtRequestBy.ReferenceFieldName = Nothing
        Me.txtRequestBy.ReferenceTableName = Nothing
        Me.txtRequestBy.Size = New System.Drawing.Size(148, 18)
        Me.txtRequestBy.TabIndex = 14
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(718, 83)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(193, 20)
        Me.lblDept.TabIndex = 19
        Me.lblDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(463, 86)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel14.TabIndex = 24
        Me.RadLabel14.Text = "Department"
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
        Me.txtDept.Location = New System.Drawing.Point(572, 84)
        Me.txtDept.MendatroryField = True
        Me.txtDept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.MyLinkLable1 = Me.RadLabel14
        Me.txtDept.MyLinkLable2 = Me.lblDept
        Me.txtDept.MyReadOnly = False
        Me.txtDept.MyShowMasterFormButton = False
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReferenceFieldDesc = Nothing
        Me.txtDept.ReferenceFieldName = Nothing
        Me.txtDept.ReferenceTableName = Nothing
        Me.txtDept.Size = New System.Drawing.Size(143, 19)
        Me.txtDept.TabIndex = 15
        Me.txtDept.Value = ""
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(1, 86)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel10.TabIndex = 32
        Me.RadLabel10.Text = "Indent Type"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(718, 45)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel9.TabIndex = 21
        Me.RadLabel9.Text = "Date Required"
        '
        'txtRequiredDate
        '
        Me.txtRequiredDate.CalculationExpression = Nothing
        Me.txtRequiredDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRequiredDate.FieldCode = Nothing
        Me.txtRequiredDate.FieldDesc = Nothing
        Me.txtRequiredDate.FieldMaxLength = 0
        Me.txtRequiredDate.FieldName = Nothing
        Me.txtRequiredDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRequiredDate.isCalculatedField = False
        Me.txtRequiredDate.IsSourceFromTable = False
        Me.txtRequiredDate.IsSourceFromValueList = False
        Me.txtRequiredDate.IsUnique = False
        Me.txtRequiredDate.Location = New System.Drawing.Point(797, 43)
        Me.txtRequiredDate.MendatroryField = False
        Me.txtRequiredDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRequiredDate.MyLinkLable1 = Me.RadLabel9
        Me.txtRequiredDate.MyLinkLable2 = Nothing
        Me.txtRequiredDate.Name = "txtRequiredDate"
        Me.txtRequiredDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRequiredDate.ReferenceFieldDesc = Nothing
        Me.txtRequiredDate.ReferenceFieldName = Nothing
        Me.txtRequiredDate.ReferenceTableName = Nothing
        Me.txtRequiredDate.ShowCheckBox = True
        Me.txtRequiredDate.Size = New System.Drawing.Size(114, 18)
        Me.txtRequiredDate.TabIndex = 10
        Me.txtRequiredDate.TabStop = False
        Me.txtRequiredDate.Text = "13/06/2011"
        Me.txtRequiredDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.cboModeOfTransport.Location = New System.Drawing.Point(572, 42)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.RadLabel11
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.ReferenceFieldDesc = Nothing
        Me.cboModeOfTransport.ReferenceFieldName = Nothing
        Me.cboModeOfTransport.ReferenceTableName = Nothing
        Me.cboModeOfTransport.Size = New System.Drawing.Size(143, 20)
        Me.cboModeOfTransport.TabIndex = 9
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(463, 45)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel11.TabIndex = 26
        Me.RadLabel11.Text = "Mode Of Transport"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(653, 3)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel13.TabIndex = 28
        Me.RadLabel13.Text = "Expiry Date"
        '
        'RadLabel27
        '
        Me.RadLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(724, 402)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 20
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(803, 400)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 19
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(718, 63)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(193, 20)
        Me.lblLocation.TabIndex = 20
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(1, 25)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 35
        Me.RadLabel5.Text = "Description"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(718, 25)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel3.TabIndex = 22
        Me.RadLabel3.Text = "Reference No"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(1, 65)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 33
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(1, 45)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 34
        Me.RadLabel8.Text = "Remarks"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(463, 65)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 25
        Me.RadLabel6.Text = "Location"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(340, 4)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 29
        Me.RadLabel4.Text = "Date"
        '
        'chkInternal
        '
        Me.chkInternal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInternal.Location = New System.Drawing.Point(372, 110)
        Me.chkInternal.Name = "chkInternal"
        Me.chkInternal.Size = New System.Drawing.Size(58, 16)
        Me.chkInternal.TabIndex = 4
        Me.chkInternal.Text = "Internal"
        Me.chkInternal.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel1.TabIndex = 36
        Me.RadLabel1.Text = "Estimation No"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(572, 64)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtLocation.TabIndex = 12
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(817, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(94, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 23
        '
        'txtReqNo
        '
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.Location = New System.Drawing.Point(86, 1)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtReqNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel1
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyMaxLength = 32767
        Me.txtReqNo.MyReadOnly = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.Size = New System.Drawing.Size(229, 20)
        Me.txtReqNo.TabIndex = 0
        Me.txtReqNo.Value = ""
        '
        'txtExpireDate
        '
        Me.txtExpireDate.CalculationExpression = Nothing
        Me.txtExpireDate.CustomFormat = "dd/MM/yyyy"
        Me.txtExpireDate.FieldCode = Nothing
        Me.txtExpireDate.FieldDesc = Nothing
        Me.txtExpireDate.FieldMaxLength = 0
        Me.txtExpireDate.FieldName = Nothing
        Me.txtExpireDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpireDate.isCalculatedField = False
        Me.txtExpireDate.IsSourceFromTable = False
        Me.txtExpireDate.IsSourceFromValueList = False
        Me.txtExpireDate.IsUnique = False
        Me.txtExpireDate.Location = New System.Drawing.Point(722, 2)
        Me.txtExpireDate.MendatroryField = False
        Me.txtExpireDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.MyLinkLable1 = Me.RadLabel13
        Me.txtExpireDate.MyLinkLable2 = Nothing
        Me.txtExpireDate.Name = "txtExpireDate"
        Me.txtExpireDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.ReferenceFieldDesc = Nothing
        Me.txtExpireDate.ReferenceFieldName = Nothing
        Me.txtExpireDate.ReferenceTableName = Nothing
        Me.txtExpireDate.ShowCheckBox = True
        Me.txtExpireDate.Size = New System.Drawing.Size(92, 18)
        Me.txtExpireDate.TabIndex = 3
        Me.txtExpireDate.TabStop = False
        Me.txtExpireDate.Text = "13/06/2011"
        Me.txtExpireDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtComment.Location = New System.Drawing.Point(86, 64)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(372, 18)
        Me.txtComment.TabIndex = 11
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
        Me.txtRefNo.Location = New System.Drawing.Point(797, 23)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel3
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(114, 18)
        Me.txtRefNo.TabIndex = 7
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(86, 44)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(372, 18)
        Me.txtRmks.TabIndex = 8
        '
        'txtCustOrderNo
        '
        Me.txtCustOrderNo.CalculationExpression = Nothing
        Me.txtCustOrderNo.FieldCode = Nothing
        Me.txtCustOrderNo.FieldDesc = Nothing
        Me.txtCustOrderNo.FieldMaxLength = 0
        Me.txtCustOrderNo.FieldName = Nothing
        Me.txtCustOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustOrderNo.isCalculatedField = False
        Me.txtCustOrderNo.IsSourceFromTable = False
        Me.txtCustOrderNo.IsSourceFromValueList = False
        Me.txtCustOrderNo.IsUnique = False
        Me.txtCustOrderNo.Location = New System.Drawing.Point(572, 22)
        Me.txtCustOrderNo.MaxLength = 50
        Me.txtCustOrderNo.MendatroryField = False
        Me.txtCustOrderNo.MyLinkLable1 = Me.RadLabel7
        Me.txtCustOrderNo.MyLinkLable2 = Nothing
        Me.txtCustOrderNo.Name = "txtCustOrderNo"
        Me.txtCustOrderNo.ReferenceFieldDesc = Nothing
        Me.txtCustOrderNo.ReferenceFieldName = Nothing
        Me.txtCustOrderNo.ReferenceTableName = Nothing
        Me.txtCustOrderNo.Size = New System.Drawing.Size(143, 18)
        Me.txtCustOrderNo.TabIndex = 6
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
        Me.txtDate.Location = New System.Drawing.Point(373, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
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
        Me.txtDesc.Location = New System.Drawing.Point(86, 24)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(372, 18)
        Me.txtDesc.TabIndex = 5
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPEngineeringAndPlantManagement.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(316, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(914, 419)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(914, 419)
        Me.UcCustomFields1.TabIndex = 0
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(914, 419)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(914, 419)
        Me.UcAttachment1.TabIndex = 0
        '
        'WorkOrder
        '
        Me.WorkOrder.Controls.Add(Me.lblSubmittedto)
        Me.WorkOrder.Controls.Add(Me.lblContent)
        Me.WorkOrder.Controls.Add(Me.lblSubject)
        Me.WorkOrder.Controls.Add(Me.txtCopySubmit)
        Me.WorkOrder.Controls.Add(Me.txtContent)
        Me.WorkOrder.Controls.Add(Me.txtSubject)
        Me.WorkOrder.Controls.Add(Me.txtTo)
        Me.WorkOrder.Controls.Add(Me.lblTo)
        Me.WorkOrder.ItemSize = New System.Drawing.SizeF(75.0!, 26.0!)
        Me.WorkOrder.Location = New System.Drawing.Point(10, 35)
        Me.WorkOrder.Name = "WorkOrder"
        Me.WorkOrder.Size = New System.Drawing.Size(914, 419)
        Me.WorkOrder.Text = "Work Order"
        '
        'lblSubmittedto
        '
        Me.lblSubmittedto.FieldName = Nothing
        Me.lblSubmittedto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubmittedto.Location = New System.Drawing.Point(16, 160)
        Me.lblSubmittedto.Name = "lblSubmittedto"
        Me.lblSubmittedto.Size = New System.Drawing.Size(99, 16)
        Me.lblSubmittedto.TabIndex = 54
        Me.lblSubmittedto.Text = "Copy Submitted to"
        '
        'lblContent
        '
        Me.lblContent.FieldName = Nothing
        Me.lblContent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContent.Location = New System.Drawing.Point(16, 114)
        Me.lblContent.Name = "lblContent"
        Me.lblContent.Size = New System.Drawing.Size(46, 16)
        Me.lblContent.TabIndex = 53
        Me.lblContent.Text = "Content"
        '
        'lblSubject
        '
        Me.lblSubject.FieldName = Nothing
        Me.lblSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubject.Location = New System.Drawing.Point(16, 66)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(44, 16)
        Me.lblSubject.TabIndex = 52
        Me.lblSubject.Text = "Subject"
        '
        'txtCopySubmit
        '
        Me.txtCopySubmit.AutoSize = False
        Me.txtCopySubmit.CalculationExpression = Nothing
        Me.txtCopySubmit.FieldCode = Nothing
        Me.txtCopySubmit.FieldDesc = Nothing
        Me.txtCopySubmit.FieldMaxLength = 0
        Me.txtCopySubmit.FieldName = Nothing
        Me.txtCopySubmit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopySubmit.isCalculatedField = False
        Me.txtCopySubmit.IsSourceFromTable = False
        Me.txtCopySubmit.IsSourceFromValueList = False
        Me.txtCopySubmit.IsUnique = False
        Me.txtCopySubmit.Location = New System.Drawing.Point(118, 160)
        Me.txtCopySubmit.MaxLength = 200
        Me.txtCopySubmit.MendatroryField = False
        Me.txtCopySubmit.Multiline = True
        Me.txtCopySubmit.MyLinkLable1 = Me.MyLabel2
        Me.txtCopySubmit.MyLinkLable2 = Nothing
        Me.txtCopySubmit.Name = "txtCopySubmit"
        Me.txtCopySubmit.ReferenceFieldDesc = Nothing
        Me.txtCopySubmit.ReferenceFieldName = Nothing
        Me.txtCopySubmit.ReferenceTableName = Nothing
        Me.txtCopySubmit.Size = New System.Drawing.Size(536, 38)
        Me.txtCopySubmit.TabIndex = 51
        '
        'txtContent
        '
        Me.txtContent.AutoSize = False
        Me.txtContent.CalculationExpression = Nothing
        Me.txtContent.FieldCode = Nothing
        Me.txtContent.FieldDesc = Nothing
        Me.txtContent.FieldMaxLength = 0
        Me.txtContent.FieldName = Nothing
        Me.txtContent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContent.isCalculatedField = False
        Me.txtContent.IsSourceFromTable = False
        Me.txtContent.IsSourceFromValueList = False
        Me.txtContent.IsUnique = False
        Me.txtContent.Location = New System.Drawing.Point(118, 114)
        Me.txtContent.MaxLength = 200
        Me.txtContent.MendatroryField = False
        Me.txtContent.Multiline = True
        Me.txtContent.MyLinkLable1 = Me.MyLabel2
        Me.txtContent.MyLinkLable2 = Nothing
        Me.txtContent.Name = "txtContent"
        Me.txtContent.ReferenceFieldDesc = Nothing
        Me.txtContent.ReferenceFieldName = Nothing
        Me.txtContent.ReferenceTableName = Nothing
        Me.txtContent.Size = New System.Drawing.Size(536, 38)
        Me.txtContent.TabIndex = 50
        '
        'txtSubject
        '
        Me.txtSubject.AutoSize = False
        Me.txtSubject.CalculationExpression = Nothing
        Me.txtSubject.FieldCode = Nothing
        Me.txtSubject.FieldDesc = Nothing
        Me.txtSubject.FieldMaxLength = 0
        Me.txtSubject.FieldName = Nothing
        Me.txtSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.isCalculatedField = False
        Me.txtSubject.IsSourceFromTable = False
        Me.txtSubject.IsSourceFromValueList = False
        Me.txtSubject.IsUnique = False
        Me.txtSubject.Location = New System.Drawing.Point(118, 66)
        Me.txtSubject.MaxLength = 200
        Me.txtSubject.MendatroryField = False
        Me.txtSubject.Multiline = True
        Me.txtSubject.MyLinkLable1 = Me.MyLabel2
        Me.txtSubject.MyLinkLable2 = Nothing
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.ReferenceFieldDesc = Nothing
        Me.txtSubject.ReferenceFieldName = Nothing
        Me.txtSubject.ReferenceTableName = Nothing
        Me.txtSubject.Size = New System.Drawing.Size(536, 38)
        Me.txtSubject.TabIndex = 49
        '
        'txtTo
        '
        Me.txtTo.AutoSize = False
        Me.txtTo.CalculationExpression = Nothing
        Me.txtTo.FieldCode = Nothing
        Me.txtTo.FieldDesc = Nothing
        Me.txtTo.FieldMaxLength = 0
        Me.txtTo.FieldName = Nothing
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.isCalculatedField = False
        Me.txtTo.IsSourceFromTable = False
        Me.txtTo.IsSourceFromValueList = False
        Me.txtTo.IsUnique = False
        Me.txtTo.Location = New System.Drawing.Point(118, 19)
        Me.txtTo.MaxLength = 200
        Me.txtTo.MendatroryField = False
        Me.txtTo.Multiline = True
        Me.txtTo.MyLinkLable1 = Me.MyLabel2
        Me.txtTo.MyLinkLable2 = Nothing
        Me.txtTo.Name = "txtTo"
        Me.txtTo.ReferenceFieldDesc = Nothing
        Me.txtTo.ReferenceFieldName = Nothing
        Me.txtTo.ReferenceTableName = Nothing
        Me.txtTo.Size = New System.Drawing.Size(536, 38)
        Me.txtTo.TabIndex = 48
        '
        'lblTo
        '
        Me.lblTo.FieldName = Nothing
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(16, 19)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(19, 16)
        Me.lblTo.TabIndex = 43
        Me.lblTo.Text = "To"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.chkExciseOnQty)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(82.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(914, 419)
        Me.RadPageViewPage2.Text = "Taxes/Terms"
        '
        'chkExciseOnQty
        '
        Me.chkExciseOnQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExciseOnQty.Location = New System.Drawing.Point(723, 10)
        Me.chkExciseOnQty.Name = "chkExciseOnQty"
        Me.chkExciseOnQty.Size = New System.Drawing.Size(115, 16)
        Me.chkExciseOnQty.TabIndex = 12
        Me.chkExciseOnQty.Text = "Excise on Quantity"
        '
        'MyLabel6
        '
        Me.MyLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel6.Location = New System.Drawing.Point(759, 250)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(155, 16)
        Me.MyLabel6.TabIndex = 11
        Me.MyLabel6.Text = "Double click To Chage Rate"
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
        Me.gv2.Location = New System.Drawing.Point(4, 38)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(907, 206)
        Me.gv2.TabIndex = 10
        Me.gv2.TabStop = False
        Me.gv2.Text = "RadGridView1"
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(70, 6)
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
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 20)
        Me.txtTaxGroup.TabIndex = 9
        Me.txtTaxGroup.Value = ""
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 6)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 7
        Me.lblTaxGrpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTaxGrpName.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(4, 6)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel5.TabIndex = 8
        Me.MyLabel5.Text = "Tax Group"
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
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtInsuranceTerms)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.txtPaymentTerm)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.txtTermRemark)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtTermCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtDueDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox1.Controls.Add(Me.lblTermName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Terms"
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 272)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(907, 144)
        Me.RadGroupBox1.TabIndex = 4
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
        Me.lblConfirmatory_PO_SRN_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtDeliveryDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtTermRemark.MyLinkLable1 = Me.MyLabel4
        Me.txtTermRemark.MyLinkLable2 = Nothing
        Me.txtTermRemark.Name = "txtTermRemark"
        Me.txtTermRemark.ReferenceFieldDesc = Nothing
        Me.txtTermRemark.ReferenceFieldName = Nothing
        Me.txtTermRemark.ReferenceTableName = Nothing
        Me.txtTermRemark.Size = New System.Drawing.Size(390, 18)
        Me.txtTermRemark.TabIndex = 1
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 66)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel4.TabIndex = 4
        Me.MyLabel4.Text = "Remark"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(547, 0)
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
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalComment)
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalSubject)
        Me.RadPageViewPage3.Controls.Add(Me.txtHeaderDiscountAmount)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel53)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage3.Controls.Add(Me.lblTaxableAmount)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage3.Controls.Add(Me.lblAmtAfterTax)
        Me.RadPageViewPage3.Controls.Add(Me.txtKindAttentation)
        Me.RadPageViewPage3.Controls.Add(Me.lblKindAttentation)
        Me.RadPageViewPage3.Controls.Add(Me.txtContentSubject)
        Me.RadPageViewPage3.Controls.Add(Me.lblContentSubject)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage3.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage3.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage3.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage3.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(914, 419)
        Me.RadPageViewPage3.Text = "Total"
        '
        'txtTotalComment
        '
        Me.txtTotalComment.Location = New System.Drawing.Point(422, 54)
        Me.txtTotalComment.Multiline = True
        Me.txtTotalComment.Name = "txtTotalComment"
        Me.txtTotalComment.Size = New System.Drawing.Size(466, 354)
        Me.txtTotalComment.TabIndex = 104
        '
        'txtTotalSubject
        '
        Me.txtTotalSubject.Location = New System.Drawing.Point(114, 245)
        Me.txtTotalSubject.Multiline = True
        Me.txtTotalSubject.Name = "txtTotalSubject"
        Me.txtTotalSubject.Size = New System.Drawing.Size(298, 48)
        Me.txtTotalSubject.TabIndex = 103
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
        Me.txtHeaderDiscountAmount.Location = New System.Drawing.Point(212, 74)
        Me.txtHeaderDiscountAmount.MendatroryField = False
        Me.txtHeaderDiscountAmount.MyLinkLable1 = Nothing
        Me.txtHeaderDiscountAmount.MyLinkLable2 = Nothing
        Me.txtHeaderDiscountAmount.Name = "txtHeaderDiscountAmount"
        Me.txtHeaderDiscountAmount.ReferenceFieldDesc = Nothing
        Me.txtHeaderDiscountAmount.ReferenceFieldName = Nothing
        Me.txtHeaderDiscountAmount.ReferenceTableName = Nothing
        Me.txtHeaderDiscountAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtHeaderDiscountAmount.TabIndex = 101
        Me.txtHeaderDiscountAmount.Text = "0"
        Me.txtHeaderDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtHeaderDiscountAmount.Value = 0R
        '
        'MyLabel53
        '
        Me.MyLabel53.FieldName = Nothing
        Me.MyLabel53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel53.Location = New System.Drawing.Point(73, 75)
        Me.MyLabel53.Name = "MyLabel53"
        Me.MyLabel53.Size = New System.Drawing.Size(133, 16)
        Me.MyLabel53.TabIndex = 102
        Me.MyLabel53.Text = "Header Discount Amount"
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(117, 138)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel44.TabIndex = 100
        Me.MyLabel44.Text = "Taxable Amount"
        '
        'lblTaxableAmount
        '
        Me.lblTaxableAmount.AutoSize = False
        Me.lblTaxableAmount.BorderVisible = True
        Me.lblTaxableAmount.FieldName = Nothing
        Me.lblTaxableAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxableAmount.Location = New System.Drawing.Point(211, 139)
        Me.lblTaxableAmount.Name = "lblTaxableAmount"
        Me.lblTaxableAmount.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxableAmount.TabIndex = 99
        Me.lblTaxableAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(112, 180)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel41.TabIndex = 98
        Me.MyLabel41.Text = "Amount After Tax"
        '
        'lblAmtAfterTax
        '
        Me.lblAmtAfterTax.AutoSize = False
        Me.lblAmtAfterTax.BorderVisible = True
        Me.lblAmtAfterTax.FieldName = Nothing
        Me.lblAmtAfterTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterTax.Location = New System.Drawing.Point(212, 181)
        Me.lblAmtAfterTax.Name = "lblAmtAfterTax"
        Me.lblAmtAfterTax.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterTax.TabIndex = 97
        Me.lblAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtKindAttentation
        '
        Me.txtKindAttentation.Location = New System.Drawing.Point(114, 356)
        Me.txtKindAttentation.Multiline = True
        Me.txtKindAttentation.Name = "txtKindAttentation"
        Me.txtKindAttentation.Size = New System.Drawing.Size(298, 52)
        Me.txtKindAttentation.TabIndex = 96
        '
        'lblKindAttentation
        '
        Me.lblKindAttentation.FieldName = Nothing
        Me.lblKindAttentation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKindAttentation.Location = New System.Drawing.Point(21, 362)
        Me.lblKindAttentation.Name = "lblKindAttentation"
        Me.lblKindAttentation.Size = New System.Drawing.Size(77, 16)
        Me.lblKindAttentation.TabIndex = 95
        Me.lblKindAttentation.Text = "Kind Attention"
        '
        'txtContentSubject
        '
        Me.txtContentSubject.Location = New System.Drawing.Point(114, 299)
        Me.txtContentSubject.Multiline = True
        Me.txtContentSubject.Name = "txtContentSubject"
        Me.txtContentSubject.Size = New System.Drawing.Size(298, 51)
        Me.txtContentSubject.TabIndex = 93
        '
        'lblContentSubject
        '
        Me.lblContentSubject.FieldName = Nothing
        Me.lblContentSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContentSubject.Location = New System.Drawing.Point(21, 314)
        Me.lblContentSubject.Name = "lblContentSubject"
        Me.lblContentSubject.Size = New System.Drawing.Size(87, 16)
        Me.lblContentSubject.TabIndex = 92
        Me.lblContentSubject.Text = "Content Subject"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(60, 250)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel7.TabIndex = 91
        Me.MyLabel7.Text = "Subject"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(212, 202)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 82
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(66, 201)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 81
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(361, 52)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel8.TabIndex = 77
        Me.MyLabel8.Text = "Comment"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(86, 117)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel9.TabIndex = 86
        Me.MyLabel9.Text = "Amount After Discount"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(106, 222)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel10.TabIndex = 80
        Me.MyLabel10.Text = "Document Amount"
        '
        'MyLabel11
        '
        Me.MyLabel11.AutoSize = False
        Me.MyLabel11.BorderVisible = True
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(212, 223)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(110, 18)
        Me.MyLabel11.TabIndex = 79
        Me.MyLabel11.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(129, 159)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 84
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(212, 160)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 83
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(212, 118)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 85
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(212, 97)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 88
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(212, 53)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 89
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(78, 96)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(128, 16)
        Me.RadLabel22.TabIndex = 87
        Me.RadLabel22.Text = "- Total Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(20, 54)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 90
        Me.RadLabel19.Text = "Document Amount without Discount"
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(23, 3)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(736, 38)
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
        'btnEmailsetting
        '
        Me.btnEmailsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEmailsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnMailPreview, Me.btnMailSendemail})
        Me.btnEmailsetting.Location = New System.Drawing.Point(305, 6)
        Me.btnEmailsetting.Name = "btnEmailsetting"
        Me.btnEmailsetting.Size = New System.Drawing.Size(83, 22)
        Me.btnEmailsetting.TabIndex = 9
        Me.btnEmailsetting.Text = "E-Mail/SMS"
        '
        'BtnMailPreview
        '
        Me.BtnMailPreview.AccessibleDescription = "Preview"
        Me.BtnMailPreview.AccessibleName = "Preview"
        Me.BtnMailPreview.Name = "BtnMailPreview"
        Me.BtnMailPreview.Text = "Preview"
        '
        'btnMailSendemail
        '
        Me.btnMailSendemail.AccessibleDescription = "Send E-Mail/SMS"
        Me.btnMailSendemail.AccessibleName = "Send E-Mail/SMS"
        Me.btnMailSendemail.Name = "btnMailSendemail"
        Me.btnMailSendemail.Text = "Send E-Mail/SMS"
        '
        'chkprclose
        '
        Me.chkprclose.AutoSize = True
        Me.chkprclose.BackColor = System.Drawing.Color.Transparent
        Me.chkprclose.Location = New System.Drawing.Point(691, 8)
        Me.chkprclose.Name = "chkprclose"
        Me.chkprclose.Size = New System.Drawing.Size(136, 18)
        Me.chkprclose.TabIndex = 5
        Me.chkprclose.Text = "Close Work Requisition"
        Me.chkprclose.UseVisualStyleBackColor = False
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(394, 6)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 22)
        Me.btnUnpost.TabIndex = 4
        Me.btnUnpost.Text = "Reverse"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(861, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(935, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Layout"
        Me.RadMenuItem1.AccessibleName = "Layout"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.SaveLayoutbtn, Me.DeleteLayout, Me.RdEmailAndSmsSetting})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'SaveLayoutbtn
        '
        Me.SaveLayoutbtn.AccessibleDescription = "Save Layout"
        Me.SaveLayoutbtn.AccessibleName = "Save Layout"
        Me.SaveLayoutbtn.Name = "SaveLayoutbtn"
        Me.SaveLayoutbtn.Text = "Save Layout"
        '
        'DeleteLayout
        '
        Me.DeleteLayout.AccessibleDescription = "Delete Layout"
        Me.DeleteLayout.AccessibleName = "Delete Layout"
        Me.DeleteLayout.Name = "DeleteLayout"
        Me.DeleteLayout.Text = "Delete Layout"
        '
        'RdEmailAndSmsSetting
        '
        Me.RdEmailAndSmsSetting.AccessibleDescription = "Email And SMS Setting"
        Me.RdEmailAndSmsSetting.AccessibleName = "Email And SMS Setting"
        Me.RdEmailAndSmsSetting.Name = "RdEmailAndSmsSetting"
        Me.RdEmailAndSmsSetting.Text = "Email And SMS Setting"
        '
        'frmWorkEstimationEng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 529)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmWorkEstimationEng"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Work Estimation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.pnl_capex.ResumeLayout(False)
        Me.pnl_capex.PerformLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.WorkOrder.ResumeLayout(False)
        Me.WorkOrder.PerformLayout()
        CType(Me.lblSubmittedto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCopySubmit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.chkExciseOnQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.txtHeaderDiscountAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxableAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKindAttentation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblContentSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEmailsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkInternal As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents txtCustOrderNo As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtExpireDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtReqNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents txtRequiredDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents txtRequestBy As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents chkprclose As System.Windows.Forms.CheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RdEmailAndSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnEmailsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnMailPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnMailSendemail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkOpenPO As common.Controls.MyCheckBox
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents ddl_category As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents pnl_capex As System.Windows.Forms.Panel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents fndcapexsubcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexsubcode As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents fndcapexcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexcode As common.Controls.MyLabel
    Friend WithEvents chk_emergency As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblApprovalDate As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamt As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamt As common.Controls.MyLabel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkTender As common.Controls.MyCheckBox
    Friend WithEvents lblEmail As common.Controls.MyLabel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents WorkOrder As RadPageViewPage
    Friend WithEvents lblTo As common.Controls.MyLabel
    Friend WithEvents txtTo As common.Controls.MyTextBox
    Friend WithEvents lblSubmittedto As common.Controls.MyLabel
    Friend WithEvents lblContent As common.Controls.MyLabel
    Friend WithEvents lblSubject As common.Controls.MyLabel
    Friend WithEvents txtCopySubmit As common.Controls.MyTextBox
    Friend WithEvents txtContent As common.Controls.MyTextBox
    Friend WithEvents txtSubject As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents MyLabel54 As common.Controls.MyLabel
    Friend WithEvents lblConfirmatory_PO_SRN_No As common.Controls.MyLabel
    Friend WithEvents txtFreight As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel52 As common.Controls.MyLabel
    Friend WithEvents MyLabel51 As common.Controls.MyLabel
    Friend WithEvents MyLabel48 As common.Controls.MyLabel
    Friend WithEvents txtPackingForward As common.Controls.MyTextBox
    Friend WithEvents txtInsurance As common.Controls.MyTextBox
    Friend WithEvents txtDeliveryDesc As common.Controls.MyLabel
    Friend WithEvents txtDelivery_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtInsuranceTerms As common.Controls.MyTextBox
    Friend WithEvents txtPaymentTerm As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtTermRemark As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkExciseOnQty As RadCheckBox
    Friend WithEvents pnlCurrConv As Panel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtHeaderDiscountAmount As common.MyNumBox
    Friend WithEvents MyLabel53 As common.Controls.MyLabel
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents lblTaxableAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterTax As common.Controls.MyLabel
    Friend WithEvents txtKindAttentation As TextBox
    Friend WithEvents lblKindAttentation As common.Controls.MyLabel
    Friend WithEvents txtContentSubject As TextBox
    Friend WithEvents lblContentSubject As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents txtTotalSubject As TextBox
    Friend WithEvents txtTotalComment As TextBox
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents txtRequisition As common.UserControls.txtFinder
End Class

