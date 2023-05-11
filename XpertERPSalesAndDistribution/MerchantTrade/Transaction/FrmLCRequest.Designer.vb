<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLCRequest
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
        Me.TxtDraweeBank = New common.Controls.MyLabel()
        Me.FndDraweeBankCode = New common.UserControls.txtFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtFDPeriod = New common.MyNumBox()
        Me.TxtLCPeriod = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtLCAmount = New common.MyNumBox()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.FndVendor = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.FndPurchaseOrderNo = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.LblBankName = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtFDPer = New common.MyNumBox()
        Me.fndBankCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLCRequestdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblRequest = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndLCRequestcode = New common.UserControls.txtNavigator()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.cboLCExtend = New common.Controls.MyComboBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.TxtLCExtend = New common.MyNumBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.FndPurchaseInvoiceNo = New common.UserControls.txtFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAgainstPI = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstPO = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.cmbLCType = New common.Controls.MyComboBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.TxtNoofDays = New common.MyNumBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.cboFrom = New common.Controls.MyComboBox()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.cboAvailableBy = New common.Controls.MyComboBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.TxtPlace = New common.Controls.MyTextBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.cboTransshipment = New common.Controls.MyComboBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.cboPartialShipment = New common.Controls.MyComboBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.cboDeferredPD = New common.Controls.MyComboBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.cboMixedPD = New common.Controls.MyComboBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.TxtLocationCode = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.cboFDPeriod = New common.Controls.MyComboBox()
        Me.cboLCPeriod = New common.Controls.MyComboBox()
        Me.lblLCExpiryDate = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RDSGSWaiver = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtSGSWaiverContext = New System.Windows.Forms.RichTextBox()
        Me.TxtSGSWaiverRefNo = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RDMerchantdeclaration = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtMerchantDecContext = New System.Windows.Forms.RichTextBox()
        Me.TxtMerchantDecrefNo = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.RDFormA2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtPurposeCode = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.TxtPurposeGroupName = New common.Controls.MyTextBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.TxtSerailNo = New common.Controls.MyTextBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.TxtFormNo = New common.Controls.MyTextBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.TxtAdCodeNo = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.RDLCIssuingApplication = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLCIssuing = New common.UserControls.MyRadGridView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RDTotal = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.txtConversionRate = New common.MyNumBox()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCopyofLC = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.RDPSGSWaiverLetter = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDPMerchantDeclaration = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnMDFormat1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnMDFormat2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDPFormA2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDPLCIssuingApp = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.TxtDraweeBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFDPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFDPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLCRequestdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.cboLCExtend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCExtend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbAgainstPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLCType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNoofDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAvailableBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPlace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPartialShipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDeferredPD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMixedPD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFDPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLCPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLCExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDSGSWaiver.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSGSWaiverRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDMerchantdeclaration.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMerchantDecrefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDFormA2.SuspendLayout()
        CType(Me.TxtPurposeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPurposeGroupName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSerailNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFormNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdCodeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDLCIssuingApplication.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvLCIssuing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLCIssuing.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.RDTotal.SuspendLayout()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopyofLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtDraweeBank
        '
        Me.TxtDraweeBank.AutoSize = False
        Me.TxtDraweeBank.BorderVisible = True
        Me.TxtDraweeBank.Location = New System.Drawing.Point(301, 274)
        Me.TxtDraweeBank.Name = "TxtDraweeBank"
        Me.TxtDraweeBank.Size = New System.Drawing.Size(291, 19)
        Me.TxtDraweeBank.TabIndex = 352
        Me.TxtDraweeBank.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FndDraweeBankCode
        '
        Me.FndDraweeBankCode.Location = New System.Drawing.Point(112, 274)
        Me.FndDraweeBankCode.MendatroryField = True
        Me.FndDraweeBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndDraweeBankCode.MyLinkLable1 = Nothing
        Me.FndDraweeBankCode.MyLinkLable2 = Nothing
        Me.FndDraweeBankCode.MyReadOnly = False
        Me.FndDraweeBankCode.MyShowMasterFormButton = False
        Me.FndDraweeBankCode.Name = "FndDraweeBankCode"
        Me.FndDraweeBankCode.Size = New System.Drawing.Size(184, 19)
        Me.FndDraweeBankCode.TabIndex = 351
        Me.FndDraweeBankCode.Value = ""
        '
        'MyLabel15
        '
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(3, 275)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel15.TabIndex = 350
        Me.MyLabel15.Text = "Drawee Bank"
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(759, 3)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel11.TabIndex = 343
        Me.MyLabel11.Text = "LC Expiry"
        Me.MyLabel11.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(3, 227)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel4.TabIndex = 336
        Me.MyLabel4.Text = "FD Period"
        '
        'TxtFDPeriod
        '
        Me.TxtFDPeriod.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFDPeriod.DecimalPlaces = 0
        Me.TxtFDPeriod.Location = New System.Drawing.Point(112, 225)
        Me.TxtFDPeriod.MaxLength = 6
        Me.TxtFDPeriod.MendatroryField = False
        Me.TxtFDPeriod.MyLinkLable1 = Nothing
        Me.TxtFDPeriod.MyLinkLable2 = Nothing
        Me.TxtFDPeriod.Name = "TxtFDPeriod"
        Me.TxtFDPeriod.Size = New System.Drawing.Size(184, 20)
        Me.TxtFDPeriod.TabIndex = 337
        Me.TxtFDPeriod.Text = "0"
        Me.TxtFDPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFDPeriod.Value = 0.0R
        '
        'TxtLCPeriod
        '
        Me.TxtLCPeriod.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCPeriod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLCPeriod.DecimalPlaces = 0
        Me.TxtLCPeriod.Location = New System.Drawing.Point(112, 202)
        Me.TxtLCPeriod.MaxLength = 6
        Me.TxtLCPeriod.MendatroryField = False
        Me.TxtLCPeriod.MyLinkLable1 = Nothing
        Me.TxtLCPeriod.MyLinkLable2 = Nothing
        Me.TxtLCPeriod.Name = "TxtLCPeriod"
        Me.TxtLCPeriod.Size = New System.Drawing.Size(184, 20)
        Me.TxtLCPeriod.TabIndex = 335
        Me.TxtLCPeriod.Text = "0"
        Me.TxtLCPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCPeriod.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(3, 204)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 334
        Me.MyLabel3.Text = "LC Period"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(454, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 17
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(3, 158)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "LC Amount"
        '
        'TxtLCAmount
        '
        Me.TxtLCAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCAmount.DecimalPlaces = 2
        Me.TxtLCAmount.Enabled = False
        Me.TxtLCAmount.Location = New System.Drawing.Point(112, 156)
        Me.TxtLCAmount.MendatroryField = False
        Me.TxtLCAmount.MyLinkLable1 = Nothing
        Me.TxtLCAmount.MyLinkLable2 = Nothing
        Me.TxtLCAmount.Name = "TxtLCAmount"
        Me.TxtLCAmount.Size = New System.Drawing.Size(184, 20)
        Me.TxtLCAmount.TabIndex = 14
        Me.TxtLCAmount.Text = "0"
        Me.TxtLCAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCAmount.Value = 0.0R
        '
        'lblvendor
        '
        Me.lblvendor.AutoSize = False
        Me.lblvendor.BorderVisible = True
        Me.lblvendor.Location = New System.Drawing.Point(301, 112)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(291, 19)
        Me.lblvendor.TabIndex = 12
        Me.lblvendor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FndVendor
        '
        Me.FndVendor.Enabled = False
        Me.FndVendor.Location = New System.Drawing.Point(112, 112)
        Me.FndVendor.MendatroryField = True
        Me.FndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndVendor.MyLinkLable1 = Nothing
        Me.FndVendor.MyLinkLable2 = Nothing
        Me.FndVendor.MyReadOnly = False
        Me.FndVendor.MyShowMasterFormButton = False
        Me.FndVendor.Name = "FndVendor"
        Me.FndVendor.Size = New System.Drawing.Size(184, 19)
        Me.FndVendor.TabIndex = 11
        Me.FndVendor.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(3, 113)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel9.TabIndex = 10
        Me.MyLabel9.Text = "Vendor"
        '
        'FndPurchaseOrderNo
        '
        Me.FndPurchaseOrderNo.Location = New System.Drawing.Point(112, 90)
        Me.FndPurchaseOrderNo.MendatroryField = False
        Me.FndPurchaseOrderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPurchaseOrderNo.MyLinkLable1 = Nothing
        Me.FndPurchaseOrderNo.MyLinkLable2 = Nothing
        Me.FndPurchaseOrderNo.MyReadOnly = False
        Me.FndPurchaseOrderNo.MyShowMasterFormButton = False
        Me.FndPurchaseOrderNo.Name = "FndPurchaseOrderNo"
        Me.FndPurchaseOrderNo.Size = New System.Drawing.Size(184, 19)
        Me.FndPurchaseOrderNo.TabIndex = 9
        Me.FndPurchaseOrderNo.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(3, 91)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel6.TabIndex = 8
        Me.MyLabel6.Text = "Purchase Order No"
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = False
        Me.LblBankName.BorderVisible = True
        Me.LblBankName.Location = New System.Drawing.Point(301, 44)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(291, 19)
        Me.LblBankName.TabIndex = 7
        Me.LblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(3, 181)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel7.TabIndex = 15
        Me.MyLabel7.Text = "FD %"
        '
        'TxtFDPer
        '
        Me.TxtFDPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFDPer.DecimalPlaces = 2
        Me.TxtFDPer.Location = New System.Drawing.Point(112, 179)
        Me.TxtFDPer.MendatroryField = True
        Me.TxtFDPer.MyLinkLable1 = Nothing
        Me.TxtFDPer.MyLinkLable2 = Nothing
        Me.TxtFDPer.Name = "TxtFDPer"
        Me.TxtFDPer.Size = New System.Drawing.Size(184, 20)
        Me.TxtFDPer.TabIndex = 16
        Me.TxtFDPer.Text = "0"
        Me.TxtFDPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFDPer.Value = 0.0R
        '
        'fndBankCode
        '
        Me.fndBankCode.Location = New System.Drawing.Point(112, 44)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Nothing
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.Size = New System.Drawing.Size(184, 19)
        Me.fndBankCode.TabIndex = 6
        Me.fndBankCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(3, 24)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel2.TabIndex = 3
        Me.MyLabel2.Text = "LC Request Date"
        '
        'txtLCRequestdate
        '
        Me.txtLCRequestdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtLCRequestdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCRequestdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLCRequestdate.Location = New System.Drawing.Point(112, 23)
        Me.txtLCRequestdate.MendatroryField = True
        Me.txtLCRequestdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLCRequestdate.MyLinkLable1 = Me.MyLabel2
        Me.txtLCRequestdate.MyLinkLable2 = Nothing
        Me.txtLCRequestdate.Name = "txtLCRequestdate"
        Me.txtLCRequestdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLCRequestdate.Size = New System.Drawing.Size(184, 18)
        Me.txtLCRequestdate.TabIndex = 4
        Me.txtLCRequestdate.TabStop = False
        Me.txtLCRequestdate.Text = "13/06/2011 11:29 AM"
        Me.txtLCRequestdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(3, 45)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Bank"
        '
        'lblRequest
        '
        Me.lblRequest.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRequest.Location = New System.Drawing.Point(3, 2)
        Me.lblRequest.Name = "lblRequest"
        Me.lblRequest.Size = New System.Drawing.Size(84, 16)
        Me.lblRequest.TabIndex = 0
        Me.lblRequest.Text = "LC Request No"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(433, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 19)
        Me.btnnew.TabIndex = 2
        '
        'fndLCRequestcode
        '
        Me.fndLCRequestcode.Location = New System.Drawing.Point(112, 1)
        Me.fndLCRequestcode.MendatroryField = True
        Me.fndLCRequestcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndLCRequestcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndLCRequestcode.MyLinkLable1 = Me.lblRequest
        Me.fndLCRequestcode.MyLinkLable2 = Nothing
        Me.fndLCRequestcode.MyMaxLength = 32767
        Me.fndLCRequestcode.MyReadOnly = False
        Me.fndLCRequestcode.Name = "fndLCRequestcode"
        Me.fndLCRequestcode.Size = New System.Drawing.Size(318, 19)
        Me.fndLCRequestcode.TabIndex = 1
        Me.fndLCRequestcode.Value = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(172, 19)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1019, 18)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(91, 19)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 19)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnCopyofLC)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer2.Size = New System.Drawing.Size(1103, 509)
        Me.SplitContainer2.SplitterDistance = 463
        Me.SplitContainer2.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RDSGSWaiver)
        Me.RadPageView1.Controls.Add(Me.RDMerchantdeclaration)
        Me.RadPageView1.Controls.Add(Me.RDFormA2)
        Me.RadPageView1.Controls.Add(Me.RDLCIssuingApplication)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RDTotal)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1103, 443)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.cboLCExtend)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel32)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCExtend)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage1.Controls.Add(Me.FndPurchaseInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel30)
        Me.RadPageViewPage1.Controls.Add(Me.cmbLCType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.cboAvailableBy)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage1.Controls.Add(Me.TxtPlace)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.cboTransshipment)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.cboPartialShipment)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.cboDeferredPD)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.cboMixedPD)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.cboFDPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.cboLCPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.lblLCExpiryDate)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDraweeBank)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.FndDraweeBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequest)
        Me.RadPageViewPage1.Controls.Add(Me.fndLCRequestcode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLCRequestdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.fndBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFDPer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.LblBankName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.FndPurchaseOrderNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFDPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.FndVendor)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.lblvendor)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCAmount)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(76.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1082, 397)
        Me.RadPageViewPage1.Text = "LC Request"
        '
        'cboLCExtend
        '
        Me.cboLCExtend.AutoCompleteDisplayMember = Nothing
        Me.cboLCExtend.AutoCompleteValueMember = Nothing
        Me.cboLCExtend.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLCExtend.Location = New System.Drawing.Point(301, 249)
        Me.cboLCExtend.MendatroryField = False
        Me.cboLCExtend.MyLinkLable1 = Nothing
        Me.cboLCExtend.MyLinkLable2 = Nothing
        Me.cboLCExtend.Name = "cboLCExtend"
        Me.cboLCExtend.Size = New System.Drawing.Size(171, 20)
        Me.cboLCExtend.TabIndex = 392
        '
        'MyLabel32
        '
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel32.Location = New System.Drawing.Point(3, 251)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel32.TabIndex = 390
        Me.MyLabel32.Text = "LC Extend"
        '
        'TxtLCExtend
        '
        Me.TxtLCExtend.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCExtend.DecimalPlaces = 0
        Me.TxtLCExtend.Location = New System.Drawing.Point(112, 249)
        Me.TxtLCExtend.MaxLength = 6
        Me.TxtLCExtend.MendatroryField = False
        Me.TxtLCExtend.MyLinkLable1 = Nothing
        Me.TxtLCExtend.MyLinkLable2 = Nothing
        Me.TxtLCExtend.Name = "TxtLCExtend"
        Me.TxtLCExtend.Size = New System.Drawing.Size(184, 20)
        Me.TxtLCExtend.TabIndex = 391
        Me.TxtLCExtend.Text = "0"
        Me.TxtLCExtend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCExtend.Value = 0.0R
        '
        'MyLabel31
        '
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel31.Location = New System.Drawing.Point(302, 91)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel31.TabIndex = 388
        Me.MyLabel31.Text = "Proforma Invoice No"
        '
        'FndPurchaseInvoiceNo
        '
        Me.FndPurchaseInvoiceNo.Location = New System.Drawing.Point(414, 89)
        Me.FndPurchaseInvoiceNo.MendatroryField = False
        Me.FndPurchaseInvoiceNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPurchaseInvoiceNo.MyLinkLable1 = Nothing
        Me.FndPurchaseInvoiceNo.MyLinkLable2 = Nothing
        Me.FndPurchaseInvoiceNo.MyReadOnly = False
        Me.FndPurchaseInvoiceNo.MyShowMasterFormButton = False
        Me.FndPurchaseInvoiceNo.Name = "FndPurchaseInvoiceNo"
        Me.FndPurchaseInvoiceNo.Size = New System.Drawing.Size(178, 20)
        Me.FndPurchaseInvoiceNo.TabIndex = 389
        Me.FndPurchaseInvoiceNo.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rdbAgainstPI)
        Me.RadGroupBox4.Controls.Add(Me.rdbAgainstPO)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(112, 65)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(273, 23)
        Me.RadGroupBox4.TabIndex = 387
        '
        'rdbAgainstPI
        '
        Me.rdbAgainstPI.Location = New System.Drawing.Point(126, 2)
        Me.rdbAgainstPI.Name = "rdbAgainstPI"
        Me.rdbAgainstPI.Size = New System.Drawing.Size(70, 18)
        Me.rdbAgainstPI.TabIndex = 1
        Me.rdbAgainstPI.Text = "Against PI"
        '
        'rdbAgainstPO
        '
        Me.rdbAgainstPO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstPO.Location = New System.Drawing.Point(6, 2)
        Me.rdbAgainstPO.Name = "rdbAgainstPO"
        Me.rdbAgainstPO.Size = New System.Drawing.Size(76, 18)
        Me.rdbAgainstPO.TabIndex = 0
        Me.rdbAgainstPO.Text = "Against PO"
        Me.rdbAgainstPO.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel30
        '
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel30.Location = New System.Drawing.Point(306, 160)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel30.TabIndex = 14
        Me.MyLabel30.Text = "LC Type"
        '
        'cmbLCType
        '
        Me.cmbLCType.AutoCompleteDisplayMember = Nothing
        Me.cmbLCType.AutoCompleteValueMember = Nothing
        Me.cmbLCType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbLCType.Location = New System.Drawing.Point(375, 157)
        Me.cmbLCType.MendatroryField = False
        Me.cmbLCType.MyLinkLable1 = Nothing
        Me.cmbLCType.MyLinkLable2 = Nothing
        Me.cmbLCType.Name = "cmbLCType"
        Me.cmbLCType.Size = New System.Drawing.Size(217, 20)
        Me.cmbLCType.TabIndex = 386
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.TxtRemarks)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel29)
        Me.RadGroupBox3.Controls.Add(Me.TxtNoofDays)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox3.Controls.Add(Me.cboFrom)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel28)
        Me.RadGroupBox3.HeaderText = "Drafts at (Tenor)"
        Me.RadGroupBox3.Location = New System.Drawing.Point(601, 160)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(383, 108)
        Me.RadGroupBox3.TabIndex = 385
        Me.RadGroupBox3.Text = "Drafts at (Tenor)"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AutoSize = False
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(106, 62)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.MendatroryField = False
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.MyLinkLable1 = Nothing
        Me.TxtRemarks.MyLinkLable2 = Nothing
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(272, 40)
        Me.TxtRemarks.TabIndex = 388
        '
        'MyLabel29
        '
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel29.Location = New System.Drawing.Point(16, 63)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel29.TabIndex = 389
        Me.MyLabel29.Text = "Remarks"
        '
        'TxtNoofDays
        '
        Me.TxtNoofDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtNoofDays.DecimalPlaces = 2
        Me.TxtNoofDays.Location = New System.Drawing.Point(106, 39)
        Me.TxtNoofDays.MaxLength = 6
        Me.TxtNoofDays.MendatroryField = False
        Me.TxtNoofDays.MyLinkLable1 = Nothing
        Me.TxtNoofDays.MyLinkLable2 = Nothing
        Me.TxtNoofDays.Name = "TxtNoofDays"
        Me.TxtNoofDays.Size = New System.Drawing.Size(171, 20)
        Me.TxtNoofDays.TabIndex = 387
        Me.TxtNoofDays.Text = "0"
        Me.TxtNoofDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtNoofDays.Value = 0.0R
        '
        'MyLabel27
        '
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel27.Location = New System.Drawing.Point(16, 41)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel27.TabIndex = 386
        Me.MyLabel27.Text = "No of days"
        '
        'cboFrom
        '
        Me.cboFrom.AutoCompleteDisplayMember = Nothing
        Me.cboFrom.AutoCompleteValueMember = Nothing
        Me.cboFrom.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFrom.Location = New System.Drawing.Point(106, 16)
        Me.cboFrom.MendatroryField = False
        Me.cboFrom.MyLinkLable1 = Nothing
        Me.cboFrom.MyLinkLable2 = Nothing
        Me.cboFrom.Name = "cboFrom"
        Me.cboFrom.Size = New System.Drawing.Size(171, 20)
        Me.cboFrom.TabIndex = 385
        '
        'MyLabel28
        '
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel28.Location = New System.Drawing.Point(16, 18)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel28.TabIndex = 384
        Me.MyLabel28.Text = "From"
        '
        'cboAvailableBy
        '
        Me.cboAvailableBy.AutoCompleteDisplayMember = Nothing
        Me.cboAvailableBy.AutoCompleteValueMember = Nothing
        Me.cboAvailableBy.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAvailableBy.Location = New System.Drawing.Point(753, 115)
        Me.cboAvailableBy.MendatroryField = False
        Me.cboAvailableBy.MyLinkLable1 = Nothing
        Me.cboAvailableBy.MyLinkLable2 = Nothing
        Me.cboAvailableBy.Name = "cboAvailableBy"
        Me.cboAvailableBy.Size = New System.Drawing.Size(171, 20)
        Me.cboAvailableBy.TabIndex = 379
        '
        'MyLabel26
        '
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel26.Location = New System.Drawing.Point(617, 117)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel26.TabIndex = 378
        Me.MyLabel26.Text = "Available By"
        '
        'TxtPlace
        '
        Me.TxtPlace.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlace.Location = New System.Drawing.Point(753, 138)
        Me.TxtPlace.MaxLength = 50
        Me.TxtPlace.MendatroryField = False
        Me.TxtPlace.MyLinkLable1 = Nothing
        Me.TxtPlace.MyLinkLable2 = Nothing
        Me.TxtPlace.Name = "TxtPlace"
        Me.TxtPlace.Size = New System.Drawing.Size(171, 18)
        Me.TxtPlace.TabIndex = 376
        '
        'MyLabel25
        '
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel25.Location = New System.Drawing.Point(617, 140)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel25.TabIndex = 377
        Me.MyLabel25.Text = "Place"
        '
        'cboTransshipment
        '
        Me.cboTransshipment.AutoCompleteDisplayMember = Nothing
        Me.cboTransshipment.AutoCompleteValueMember = Nothing
        Me.cboTransshipment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransshipment.Location = New System.Drawing.Point(753, 93)
        Me.cboTransshipment.MendatroryField = False
        Me.cboTransshipment.MyLinkLable1 = Nothing
        Me.cboTransshipment.MyLinkLable2 = Nothing
        Me.cboTransshipment.Name = "cboTransshipment"
        Me.cboTransshipment.Size = New System.Drawing.Size(171, 20)
        Me.cboTransshipment.TabIndex = 375
        '
        'MyLabel24
        '
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel24.Location = New System.Drawing.Point(617, 95)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel24.TabIndex = 374
        Me.MyLabel24.Text = "Transshipment"
        '
        'cboPartialShipment
        '
        Me.cboPartialShipment.AutoCompleteDisplayMember = Nothing
        Me.cboPartialShipment.AutoCompleteValueMember = Nothing
        Me.cboPartialShipment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPartialShipment.Location = New System.Drawing.Point(753, 70)
        Me.cboPartialShipment.MendatroryField = False
        Me.cboPartialShipment.MyLinkLable1 = Nothing
        Me.cboPartialShipment.MyLinkLable2 = Nothing
        Me.cboPartialShipment.Name = "cboPartialShipment"
        Me.cboPartialShipment.Size = New System.Drawing.Size(171, 20)
        Me.cboPartialShipment.TabIndex = 373
        '
        'MyLabel23
        '
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel23.Location = New System.Drawing.Point(617, 72)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel23.TabIndex = 372
        Me.MyLabel23.Text = "Partial Shipment"
        '
        'cboDeferredPD
        '
        Me.cboDeferredPD.AutoCompleteDisplayMember = Nothing
        Me.cboDeferredPD.AutoCompleteValueMember = Nothing
        Me.cboDeferredPD.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDeferredPD.Location = New System.Drawing.Point(753, 47)
        Me.cboDeferredPD.MendatroryField = False
        Me.cboDeferredPD.MyLinkLable1 = Nothing
        Me.cboDeferredPD.MyLinkLable2 = Nothing
        Me.cboDeferredPD.Name = "cboDeferredPD"
        Me.cboDeferredPD.Size = New System.Drawing.Size(171, 20)
        Me.cboDeferredPD.TabIndex = 371
        '
        'MyLabel22
        '
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel22.Location = New System.Drawing.Point(617, 49)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(136, 16)
        Me.MyLabel22.TabIndex = 370
        Me.MyLabel22.Text = "Deferred Payment Details"
        '
        'cboMixedPD
        '
        Me.cboMixedPD.AutoCompleteDisplayMember = Nothing
        Me.cboMixedPD.AutoCompleteValueMember = Nothing
        Me.cboMixedPD.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMixedPD.Location = New System.Drawing.Point(753, 24)
        Me.cboMixedPD.MendatroryField = False
        Me.cboMixedPD.MyLinkLable1 = Nothing
        Me.cboMixedPD.MyLinkLable2 = Nothing
        Me.cboMixedPD.Name = "cboMixedPD"
        Me.cboMixedPD.Size = New System.Drawing.Size(171, 20)
        Me.cboMixedPD.TabIndex = 369
        '
        'MyLabel21
        '
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel21.Location = New System.Drawing.Point(617, 26)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel21.TabIndex = 368
        Me.MyLabel21.Text = "Mixed Payment Details"
        '
        'TxtLocationCode
        '
        Me.TxtLocationCode.AutoSize = False
        Me.TxtLocationCode.BorderVisible = True
        Me.TxtLocationCode.Location = New System.Drawing.Point(112, 134)
        Me.TxtLocationCode.Name = "TxtLocationCode"
        Me.TxtLocationCode.Size = New System.Drawing.Size(184, 19)
        Me.TxtLocationCode.TabIndex = 359
        Me.TxtLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(3, 135)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel8.TabIndex = 356
        Me.MyLabel8.Text = "Location"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.Location = New System.Drawing.Point(301, 134)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(291, 19)
        Me.lblLocationDesc.TabIndex = 358
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFDPeriod
        '
        Me.cboFDPeriod.AutoCompleteDisplayMember = Nothing
        Me.cboFDPeriod.AutoCompleteValueMember = Nothing
        Me.cboFDPeriod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFDPeriod.Location = New System.Drawing.Point(301, 225)
        Me.cboFDPeriod.MendatroryField = False
        Me.cboFDPeriod.MyLinkLable1 = Nothing
        Me.cboFDPeriod.MyLinkLable2 = Nothing
        Me.cboFDPeriod.Name = "cboFDPeriod"
        Me.cboFDPeriod.Size = New System.Drawing.Size(171, 20)
        Me.cboFDPeriod.TabIndex = 355
        '
        'cboLCPeriod
        '
        Me.cboLCPeriod.AutoCompleteDisplayMember = Nothing
        Me.cboLCPeriod.AutoCompleteValueMember = Nothing
        Me.cboLCPeriod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLCPeriod.Location = New System.Drawing.Point(301, 202)
        Me.cboLCPeriod.MendatroryField = False
        Me.cboLCPeriod.MyLinkLable1 = Nothing
        Me.cboLCPeriod.MyLinkLable2 = Nothing
        Me.cboLCPeriod.Name = "cboLCPeriod"
        Me.cboLCPeriod.Size = New System.Drawing.Size(171, 20)
        Me.cboLCPeriod.TabIndex = 354
        '
        'lblLCExpiryDate
        '
        Me.lblLCExpiryDate.AutoSize = False
        Me.lblLCExpiryDate.BorderVisible = True
        Me.lblLCExpiryDate.Location = New System.Drawing.Point(868, 2)
        Me.lblLCExpiryDate.Name = "lblLCExpiryDate"
        Me.lblLCExpiryDate.Size = New System.Drawing.Size(184, 19)
        Me.lblLCExpiryDate.TabIndex = 353
        Me.lblLCExpiryDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLCExpiryDate.Visible = False
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 301)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1077, 94)
        Me.RadGroupBox2.TabIndex = 14
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
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(1057, 64)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RDSGSWaiver
        '
        Me.RDSGSWaiver.Controls.Add(Me.MyLabel12)
        Me.RDSGSWaiver.Controls.Add(Me.txtSGSWaiverContext)
        Me.RDSGSWaiver.Controls.Add(Me.TxtSGSWaiverRefNo)
        Me.RDSGSWaiver.Controls.Add(Me.MyLabel10)
        Me.RDSGSWaiver.ItemSize = New System.Drawing.SizeF(111.0!, 26.0!)
        Me.RDSGSWaiver.Location = New System.Drawing.Point(10, 35)
        Me.RDSGSWaiver.Name = "RDSGSWaiver"
        Me.RDSGSWaiver.Size = New System.Drawing.Size(1082, 417)
        Me.RDSGSWaiver.Text = "SGS Waiver Letter"
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(6, 31)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel12.TabIndex = 330
        Me.MyLabel12.Text = "SGS Waiver Context"
        Me.MyLabel12.Visible = False
        '
        'txtSGSWaiverContext
        '
        Me.txtSGSWaiverContext.Location = New System.Drawing.Point(119, 27)
        Me.txtSGSWaiverContext.Name = "txtSGSWaiverContext"
        Me.txtSGSWaiverContext.Size = New System.Drawing.Size(873, 377)
        Me.txtSGSWaiverContext.TabIndex = 329
        Me.txtSGSWaiverContext.Text = ""
        Me.txtSGSWaiverContext.Visible = False
        '
        'TxtSGSWaiverRefNo
        '
        Me.TxtSGSWaiverRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSGSWaiverRefNo.Location = New System.Drawing.Point(119, 3)
        Me.TxtSGSWaiverRefNo.MaxLength = 50
        Me.TxtSGSWaiverRefNo.MendatroryField = False
        Me.TxtSGSWaiverRefNo.MyLinkLable1 = Nothing
        Me.TxtSGSWaiverRefNo.MyLinkLable2 = Nothing
        Me.TxtSGSWaiverRefNo.Name = "TxtSGSWaiverRefNo"
        Me.TxtSGSWaiverRefNo.Size = New System.Drawing.Size(174, 18)
        Me.TxtSGSWaiverRefNo.TabIndex = 327
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(6, 3)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel10.TabIndex = 328
        Me.MyLabel10.Text = "SGS Waiver Ref No"
        '
        'RDMerchantdeclaration
        '
        Me.RDMerchantdeclaration.Controls.Add(Me.MyLabel13)
        Me.RDMerchantdeclaration.Controls.Add(Me.TxtMerchantDecContext)
        Me.RDMerchantdeclaration.Controls.Add(Me.TxtMerchantDecrefNo)
        Me.RDMerchantdeclaration.Controls.Add(Me.MyLabel14)
        Me.RDMerchantdeclaration.ItemSize = New System.Drawing.SizeF(124.0!, 26.0!)
        Me.RDMerchantdeclaration.Location = New System.Drawing.Point(10, 35)
        Me.RDMerchantdeclaration.Name = "RDMerchantdeclaration"
        Me.RDMerchantdeclaration.Size = New System.Drawing.Size(1082, 397)
        Me.RDMerchantdeclaration.Text = "Merchant Declaration"
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(6, 36)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(156, 16)
        Me.MyLabel13.TabIndex = 334
        Me.MyLabel13.Text = "Merchant Declaration Context"
        Me.MyLabel13.Visible = False
        '
        'TxtMerchantDecContext
        '
        Me.TxtMerchantDecContext.Location = New System.Drawing.Point(167, 32)
        Me.TxtMerchantDecContext.Name = "TxtMerchantDecContext"
        Me.TxtMerchantDecContext.Size = New System.Drawing.Size(716, 377)
        Me.TxtMerchantDecContext.TabIndex = 333
        Me.TxtMerchantDecContext.Text = ""
        Me.TxtMerchantDecContext.Visible = False
        '
        'TxtMerchantDecrefNo
        '
        Me.TxtMerchantDecrefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMerchantDecrefNo.Location = New System.Drawing.Point(167, 8)
        Me.TxtMerchantDecrefNo.MaxLength = 50
        Me.TxtMerchantDecrefNo.MendatroryField = False
        Me.TxtMerchantDecrefNo.MyLinkLable1 = Nothing
        Me.TxtMerchantDecrefNo.MyLinkLable2 = Nothing
        Me.TxtMerchantDecrefNo.Name = "TxtMerchantDecrefNo"
        Me.TxtMerchantDecrefNo.Size = New System.Drawing.Size(174, 18)
        Me.TxtMerchantDecrefNo.TabIndex = 331
        '
        'MyLabel14
        '
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(6, 8)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(152, 16)
        Me.MyLabel14.TabIndex = 332
        Me.MyLabel14.Text = "Merchant Declaration Ref No"
        '
        'RDFormA2
        '
        Me.RDFormA2.Controls.Add(Me.TxtPurposeCode)
        Me.RDFormA2.Controls.Add(Me.MyLabel20)
        Me.RDFormA2.Controls.Add(Me.TxtPurposeGroupName)
        Me.RDFormA2.Controls.Add(Me.MyLabel19)
        Me.RDFormA2.Controls.Add(Me.TxtSerailNo)
        Me.RDFormA2.Controls.Add(Me.MyLabel18)
        Me.RDFormA2.Controls.Add(Me.TxtFormNo)
        Me.RDFormA2.Controls.Add(Me.MyLabel17)
        Me.RDFormA2.Controls.Add(Me.TxtAdCodeNo)
        Me.RDFormA2.Controls.Add(Me.MyLabel16)
        Me.RDFormA2.ItemSize = New System.Drawing.SizeF(60.0!, 26.0!)
        Me.RDFormA2.Location = New System.Drawing.Point(10, 35)
        Me.RDFormA2.Name = "RDFormA2"
        Me.RDFormA2.Size = New System.Drawing.Size(1082, 417)
        Me.RDFormA2.Text = "Form A2"
        '
        'TxtPurposeCode
        '
        Me.TxtPurposeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurposeCode.Location = New System.Drawing.Point(164, 91)
        Me.TxtPurposeCode.MaxLength = 50
        Me.TxtPurposeCode.MendatroryField = False
        Me.TxtPurposeCode.MyLinkLable1 = Nothing
        Me.TxtPurposeCode.MyLinkLable2 = Nothing
        Me.TxtPurposeCode.Name = "TxtPurposeCode"
        Me.TxtPurposeCode.Size = New System.Drawing.Size(174, 18)
        Me.TxtPurposeCode.TabIndex = 341
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(3, 91)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel20.TabIndex = 342
        Me.MyLabel20.Text = "Purpose Code"
        '
        'TxtPurposeGroupName
        '
        Me.TxtPurposeGroupName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurposeGroupName.Location = New System.Drawing.Point(164, 69)
        Me.TxtPurposeGroupName.MaxLength = 50
        Me.TxtPurposeGroupName.MendatroryField = False
        Me.TxtPurposeGroupName.MyLinkLable1 = Nothing
        Me.TxtPurposeGroupName.MyLinkLable2 = Nothing
        Me.TxtPurposeGroupName.Name = "TxtPurposeGroupName"
        Me.TxtPurposeGroupName.Size = New System.Drawing.Size(174, 18)
        Me.TxtPurposeGroupName.TabIndex = 339
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel19.Location = New System.Drawing.Point(3, 69)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel19.TabIndex = 340
        Me.MyLabel19.Text = "Purpose Group Name"
        '
        'TxtSerailNo
        '
        Me.TxtSerailNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSerailNo.Location = New System.Drawing.Point(164, 47)
        Me.TxtSerailNo.MaxLength = 50
        Me.TxtSerailNo.MendatroryField = False
        Me.TxtSerailNo.MyLinkLable1 = Nothing
        Me.TxtSerailNo.MyLinkLable2 = Nothing
        Me.TxtSerailNo.Name = "TxtSerailNo"
        Me.TxtSerailNo.Size = New System.Drawing.Size(174, 18)
        Me.TxtSerailNo.TabIndex = 337
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel18.Location = New System.Drawing.Point(3, 47)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel18.TabIndex = 338
        Me.MyLabel18.Text = "Serial No."
        '
        'TxtFormNo
        '
        Me.TxtFormNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormNo.Location = New System.Drawing.Point(164, 25)
        Me.TxtFormNo.MaxLength = 50
        Me.TxtFormNo.MendatroryField = False
        Me.TxtFormNo.MyLinkLable1 = Nothing
        Me.TxtFormNo.MyLinkLable2 = Nothing
        Me.TxtFormNo.Name = "TxtFormNo"
        Me.TxtFormNo.Size = New System.Drawing.Size(174, 18)
        Me.TxtFormNo.TabIndex = 335
        '
        'MyLabel17
        '
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(3, 25)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel17.TabIndex = 336
        Me.MyLabel17.Text = "Form No."
        '
        'TxtAdCodeNo
        '
        Me.TxtAdCodeNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdCodeNo.Location = New System.Drawing.Point(164, 3)
        Me.TxtAdCodeNo.MaxLength = 50
        Me.TxtAdCodeNo.MendatroryField = False
        Me.TxtAdCodeNo.MyLinkLable1 = Nothing
        Me.TxtAdCodeNo.MyLinkLable2 = Nothing
        Me.TxtAdCodeNo.Name = "TxtAdCodeNo"
        Me.TxtAdCodeNo.Size = New System.Drawing.Size(174, 18)
        Me.TxtAdCodeNo.TabIndex = 333
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel16.TabIndex = 334
        Me.MyLabel16.Text = "AD Code No."
        '
        'RDLCIssuingApplication
        '
        Me.RDLCIssuingApplication.Controls.Add(Me.RadGroupBox1)
        Me.RDLCIssuingApplication.ItemSize = New System.Drawing.SizeF(128.0!, 26.0!)
        Me.RDLCIssuingApplication.Location = New System.Drawing.Point(10, 35)
        Me.RDLCIssuingApplication.Name = "RDLCIssuingApplication"
        Me.RDLCIssuingApplication.Size = New System.Drawing.Size(1082, 397)
        Me.RDLCIssuingApplication.Text = "LC Issuing Application"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gvLCIssuing)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "LC Issuing Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1077, 391)
        Me.RadGroupBox1.TabIndex = 15
        Me.RadGroupBox1.Text = "LC Issuing Details"
        '
        'gvLCIssuing
        '
        Me.gvLCIssuing.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvLCIssuing.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLCIssuing.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLCIssuing.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvLCIssuing.ForeColor = System.Drawing.Color.Black
        Me.gvLCIssuing.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLCIssuing.Location = New System.Drawing.Point(10, 20)
        '
        'gvLCIssuing
        '
        Me.gvLCIssuing.MasterTemplate.AllowDeleteRow = False
        Me.gvLCIssuing.Name = "gvLCIssuing"
        Me.gvLCIssuing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLCIssuing.ShowGroupPanel = False
        Me.gvLCIssuing.Size = New System.Drawing.Size(1057, 361)
        Me.gvLCIssuing.TabIndex = 13
        Me.gvLCIssuing.TabStop = False
        Me.gvLCIssuing.Text = "RadGridView1"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1082, 397)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1082, 397)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RDTotal
        '
        Me.RDTotal.Controls.Add(Me.RadLabel27)
        Me.RDTotal.Controls.Add(Me.lblTotRAmt)
        Me.RDTotal.Controls.Add(Me.pnlCurrConv)
        Me.RDTotal.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RDTotal.Location = New System.Drawing.Point(10, 35)
        Me.RDTotal.Name = "RDTotal"
        Me.RDTotal.Size = New System.Drawing.Size(1082, 397)
        Me.RDTotal.Text = "Total"
        '
        'RadLabel27
        '
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(14, 47)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 6
        Me.RadLabel27.Text = "Document Amount"
        Me.RadLabel27.Visible = False
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(120, 45)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 5
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotRAmt.Visible = False
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(14, 3)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(492, 38)
        Me.pnlCurrConv.TabIndex = 1
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrencyCode.Location = New System.Drawing.Point(80, 9)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 24)
        Me.txtCurrencyCode.TabIndex = 4
        Me.txtCurrencyCode.Value = ""
        '
        'txtConversionRate
        '
        Me.txtConversionRate.BackColor = System.Drawing.Color.White
        Me.txtConversionRate.DecimalPlaces = 2
        Me.txtConversionRate.Location = New System.Drawing.Point(353, 8)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.Size = New System.Drawing.Size(124, 20)
        Me.txtConversionRate.TabIndex = 1
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'lblCurrency
        '
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(22, 12)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 2
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(256, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 3
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1103, 20)
        Me.RadMenu1.TabIndex = 11
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMSaveLayout, Me.RMDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RMSaveLayout
        '
        Me.RMSaveLayout.AccessibleDescription = "Save Layout"
        Me.RMSaveLayout.AccessibleName = "Save Layout"
        Me.RMSaveLayout.Name = "RMSaveLayout"
        Me.RMSaveLayout.Text = "Save Layout"
        '
        'RMDeleteLayout
        '
        Me.RMDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RMDeleteLayout.AccessibleName = "Delete Layout"
        Me.RMDeleteLayout.Name = "RMDeleteLayout"
        Me.RMDeleteLayout.Text = "Delete Layout"
        '
        'btnCopyofLC
        '
        Me.btnCopyofLC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopyofLC.Location = New System.Drawing.Point(340, 19)
        Me.btnCopyofLC.Name = "btnCopyofLC"
        Me.btnCopyofLC.Size = New System.Drawing.Size(104, 20)
        Me.btnCopyofLC.TabIndex = 6
        Me.btnCopyofLC.Text = "Copy of LC"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDPSGSWaiverLetter, Me.RDPMerchantDeclaration, Me.RDPFormA2, Me.RDPLCIssuingApp})
        Me.btnsetting.Location = New System.Drawing.Point(251, 19)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(83, 19)
        Me.btnsetting.TabIndex = 5
        Me.btnsetting.Text = "Print"
        '
        'RDPSGSWaiverLetter
        '
        Me.RDPSGSWaiverLetter.AccessibleDescription = "SGS Waiver Letter"
        Me.RDPSGSWaiverLetter.AccessibleName = "SGS Waiver Letter"
        Me.RDPSGSWaiverLetter.Name = "RDPSGSWaiverLetter"
        Me.RDPSGSWaiverLetter.Text = "SGS Waiver Letter"
        '
        'RDPMerchantDeclaration
        '
        Me.RDPMerchantDeclaration.AccessibleDescription = "RadMenuItem2"
        Me.RDPMerchantDeclaration.AccessibleName = "RadMenuItem2"
        Me.RDPMerchantDeclaration.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnMDFormat1, Me.BtnMDFormat2})
        Me.RDPMerchantDeclaration.Name = "RDPMerchantDeclaration"
        Me.RDPMerchantDeclaration.Text = "Merchant Declaration"
        '
        'BtnMDFormat1
        '
        Me.BtnMDFormat1.AccessibleDescription = "RadMenuItem1"
        Me.BtnMDFormat1.AccessibleName = "RadMenuItem1"
        Me.BtnMDFormat1.Name = "BtnMDFormat1"
        Me.BtnMDFormat1.Text = "Declaration of Merchant Trade"
        '
        'BtnMDFormat2
        '
        Me.BtnMDFormat2.AccessibleDescription = "RadMenuItem2"
        Me.BtnMDFormat2.AccessibleName = "RadMenuItem2"
        Me.BtnMDFormat2.Name = "BtnMDFormat2"
        Me.BtnMDFormat2.Text = " Declaration cum undertaking"
        '
        'RDPFormA2
        '
        Me.RDPFormA2.AccessibleDescription = "RadMenuItem1"
        Me.RDPFormA2.AccessibleName = "RadMenuItem1"
        Me.RDPFormA2.Name = "RDPFormA2"
        Me.RDPFormA2.Text = "Form A2"
        '
        'RDPLCIssuingApp
        '
        Me.RDPLCIssuingApp.AccessibleDescription = "LC Issuing Application"
        Me.RDPLCIssuingApp.AccessibleName = "LC Issuing Application"
        Me.RDPLCIssuingApp.Name = "RDPLCIssuingApp"
        Me.RDPLCIssuingApp.Text = "LC Issuing Application"
        '
        'FrmLCRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1103, 509)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmLCRequest"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "LC Request"
        CType(Me.TxtDraweeBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFDPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFDPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLCRequestdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.cboLCExtend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCExtend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbAgainstPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLCType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNoofDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAvailableBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPlace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPartialShipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDeferredPD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMixedPD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFDPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLCPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLCExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDSGSWaiver.ResumeLayout(False)
        Me.RDSGSWaiver.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSGSWaiverRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDMerchantdeclaration.ResumeLayout(False)
        Me.RDMerchantdeclaration.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMerchantDecrefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDFormA2.ResumeLayout(False)
        Me.RDFormA2.PerformLayout()
        CType(Me.TxtPurposeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPurposeGroupName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSerailNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFormNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdCodeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDLCIssuingApplication.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvLCIssuing.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLCIssuing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.RDTotal.ResumeLayout(False)
        Me.RDTotal.PerformLayout()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopyofLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents FndVendor As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents FndPurchaseOrderNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents LblBankName As common.Controls.MyLabel
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLCRequestdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblRequest As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLCRequestcode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtLCAmount As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtFDPer As common.MyNumBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtFDPeriod As common.MyNumBox
    Friend WithEvents TxtLCPeriod As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtDraweeBank As common.Controls.MyLabel
    Friend WithEvents FndDraweeBankCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblLCExpiryDate As common.Controls.MyLabel
    Friend WithEvents cboFDPeriod As common.Controls.MyComboBox
    Friend WithEvents cboLCPeriod As common.Controls.MyComboBox
    Friend WithEvents TxtLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents RDSGSWaiver As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RDMerchantdeclaration As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RDFormA2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RDLCIssuingApplication As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtSGSWaiverRefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtSGSWaiverContext As System.Windows.Forms.RichTextBox
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RDPSGSWaiverLetter As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDPMerchantDeclaration As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents TxtMerchantDecContext As System.Windows.Forms.RichTextBox
    Friend WithEvents TxtMerchantDecrefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents RDPFormA2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TxtPurposeCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents TxtPurposeGroupName As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents TxtSerailNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents TxtFormNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents TxtAdCodeNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RDTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents btnCopyofLC As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvLCIssuing As common.UserControls.MyRadGridView
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents RDPLCIssuingApp As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnMDFormat1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnMDFormat2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents MyComboBox4 As common.Controls.MyComboBox
    Friend WithEvents MyComboBox3 As common.Controls.MyComboBox
    Friend WithEvents MyComboBox2 As common.Controls.MyComboBox
    Friend WithEvents MyComboBox1 As common.Controls.MyComboBox
    Friend WithEvents cboTransshipment As common.Controls.MyComboBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents cboPartialShipment As common.Controls.MyComboBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents cboDeferredPD As common.Controls.MyComboBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents cboMixedPD As common.Controls.MyComboBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents cboAvailableBy As common.Controls.MyComboBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents TxtPlace As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents MyTextBox1 As common.Controls.MyTextBox
    Friend WithEvents MyComboBox5 As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents TxtNoofDays As common.MyNumBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents cboFrom As common.Controls.MyComboBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents cmbLCType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAgainstPI As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAgainstPO As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents FndPurchaseInvoiceNo As common.UserControls.txtFinder
    Friend WithEvents MyComboBox6 As common.Controls.MyComboBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents TxtLCExtend As common.MyNumBox
    Friend WithEvents cboLCExtend As common.Controls.MyComboBox
End Class

