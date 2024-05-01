<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseSettings
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
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkAutoGenerateMRN = New common.Controls.MyCheckBox()
        Me.chkPI_debitnot_unitcost = New common.Controls.MyCheckBox()
        Me.txtPoLimit = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule = New common.Controls.MyCheckBox()
        Me.TxtGRNLim = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.chkShortageIncludeInLandedCost = New common.Controls.MyCheckBox()
        Me.ChkSkipMRNGRNinCaseofMT = New common.Controls.MyCheckBox()
        Me.chkSRNPrintQtywise = New common.Controls.MyCheckBox()
        Me.ChkGLAccToItem = New common.Controls.MyCheckBox()
        Me.ChkCostEditIssue = New common.Controls.MyCheckBox()
        Me.chkQCColumnAddedonMRN = New common.Controls.MyCheckBox()
        Me.chkRemarkReasononPO = New common.Controls.MyCheckBox()
        Me.TxtSRNLim = New common.MyNumBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.chkPOInvCreateDebitNoteForReject = New common.Controls.MyCheckBox()
        Me.ChkRateEditSRN = New common.Controls.MyCheckBox()
        Me.chkEnableProjectFinder = New common.Controls.MyCheckBox()
        Me.chkPOInvCreateDebitNoteForRejectAndShort = New common.Controls.MyCheckBox()
        Me.chkRFQ = New common.Controls.MyCheckBox()
        Me.txtJobWork = New common.UserControls.txtFinder()
        Me.LblJobWork = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkShowInvoiceInPOfinder = New common.Controls.MyCheckBox()
        Me.chksmsatpost = New common.Controls.MyCheckBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.Controls.MyTextBox()
        Me.ChkInvoiceBasedPO = New common.Controls.MyCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDatabaseName = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.ChkShowStatusPur = New common.Controls.MyCheckBox()
        Me.chkDemoAmendment = New common.Controls.MyCheckBox()
        Me.chkVendor_Nlevel = New common.Controls.MyCheckBox()
        Me.chkSRNRejected = New common.Controls.MyCheckBox()
        Me.chkReturnWithoutInvoice = New common.Controls.MyCheckBox()
        Me.chkPurchaseOrderItemQtyBelow = New common.Controls.MyCheckBox()
        Me.cboNoticationSettingInPurchaseRequisition = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboNoticationSettingInPO = New common.Controls.MyComboBox()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.chkmccPO = New common.Controls.MyCheckBox()
        Me.chkautoPO = New common.Controls.MyCheckBox()
        Me.chkstd_rate = New common.Controls.MyCheckBox()
        Me.chkMRN = New common.Controls.MyCheckBox()
        Me.chkAllowLargerItemCost = New common.Controls.MyCheckBox()
        Me.chkGRN = New common.Controls.MyCheckBox()
        Me.chkDisableShipToLocation = New common.Controls.MyCheckBox()
        Me.chkmailoff = New common.Controls.MyCheckBox()
        Me.chkRequiredFOC = New common.Controls.MyCheckBox()
        Me.chkRequiredSecurityAmt = New common.Controls.MyCheckBox()
        Me.chkOneItemOneVendor = New common.Controls.MyCheckBox()
        Me.chkPickItemFromVendorItemDetails = New common.Controls.MyCheckBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkOtherItemsexpDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAssetexpDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFGexpDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRMexpDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkOtherItemsmfgDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAssetmfgDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFGmfgDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRMmfgDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkOtherItemsBatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAssetBatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFGBatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRMBatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkItemReorderLevel = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkPOReq = New Telerik.WinControls.UI.RadCheckBox()
        Me.Chkabatement = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtSlab3To = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.chkApplySlab = New common.Controls.MyCheckBox()
        Me.txtSlab2To = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSlab3From = New common.MyNumBox()
        Me.txtSlab1From = New common.MyNumBox()
        Me.txtSlab1To = New common.MyNumBox()
        Me.txtSlab2From = New common.MyNumBox()
        Me.chkAllstructurewiseItem = New common.Controls.MyCheckBox()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ChkAutoGenerateMRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPI_debitnot_unitcost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPoLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtGRNLim, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShortageIncludeInLandedCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSkipMRNGRNinCaseofMT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSRNPrintQtywise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkGLAccToItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCostEditIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkQCColumnAddedonMRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRemarkReasononPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSRNLim, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPOInvCreateDebitNoteForReject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkRateEditSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEnableProjectFinder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPOInvCreateDebitNoteForRejectAndShort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRFQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowInvoiceInPOfinder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chksmsatpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkInvoiceBasedPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDatabaseName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkShowStatusPur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDemoAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendor_Nlevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSRNRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReturnWithoutInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPurchaseOrderItemQtyBelow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNoticationSettingInPurchaseRequisition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNoticationSettingInPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmccPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkautoPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkstd_rate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowLargerItemCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDisableShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmailoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequiredFOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequiredSecurityAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOneItemOneVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPickItemFromVendorItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkOtherItemsexpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetexpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFGexpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRMexpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkOtherItemsmfgDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetmfgDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFGmfgDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRMmfgDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.chkOtherItemsBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFGBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRMBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemReorderLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPOReq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chkabatement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtSlab3To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplySlab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlab2To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlab3From, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlab1From, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlab1To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlab2From, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllstructurewiseItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(12, 13)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(922, 14)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1022, 457)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkAllstructurewiseItem)
        Me.RadGroupBox1.Controls.Add(Me.ChkAutoGenerateMRN)
        Me.RadGroupBox1.Controls.Add(Me.chkPI_debitnot_unitcost)
        Me.RadGroupBox1.Controls.Add(Me.txtPoLimit)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule)
        Me.RadGroupBox1.Controls.Add(Me.TxtGRNLim)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.chkShortageIncludeInLandedCost)
        Me.RadGroupBox1.Controls.Add(Me.ChkSkipMRNGRNinCaseofMT)
        Me.RadGroupBox1.Controls.Add(Me.chkSRNPrintQtywise)
        Me.RadGroupBox1.Controls.Add(Me.ChkGLAccToItem)
        Me.RadGroupBox1.Controls.Add(Me.ChkCostEditIssue)
        Me.RadGroupBox1.Controls.Add(Me.chkQCColumnAddedonMRN)
        Me.RadGroupBox1.Controls.Add(Me.chkRemarkReasononPO)
        Me.RadGroupBox1.Controls.Add(Me.TxtSRNLim)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox1.Controls.Add(Me.chkPOInvCreateDebitNoteForReject)
        Me.RadGroupBox1.Controls.Add(Me.ChkRateEditSRN)
        Me.RadGroupBox1.Controls.Add(Me.chkEnableProjectFinder)
        Me.RadGroupBox1.Controls.Add(Me.chkPOInvCreateDebitNoteForRejectAndShort)
        Me.RadGroupBox1.Controls.Add(Me.chkRFQ)
        Me.RadGroupBox1.Controls.Add(Me.txtJobWork)
        Me.RadGroupBox1.Controls.Add(Me.LblJobWork)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.chkShowInvoiceInPOfinder)
        Me.RadGroupBox1.Controls.Add(Me.chksmsatpost)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.ChkInvoiceBasedPO)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtDatabaseName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtVendorNo)
        Me.RadGroupBox1.Controls.Add(Me.ChkShowStatusPur)
        Me.RadGroupBox1.Controls.Add(Me.chkDemoAmendment)
        Me.RadGroupBox1.Controls.Add(Me.chkVendor_Nlevel)
        Me.RadGroupBox1.Controls.Add(Me.chkSRNRejected)
        Me.RadGroupBox1.Controls.Add(Me.chkReturnWithoutInvoice)
        Me.RadGroupBox1.Controls.Add(Me.chkPurchaseOrderItemQtyBelow)
        Me.RadGroupBox1.Controls.Add(Me.cboNoticationSettingInPurchaseRequisition)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.cboNoticationSettingInPO)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel18)
        Me.RadGroupBox1.Controls.Add(Me.chkmccPO)
        Me.RadGroupBox1.Controls.Add(Me.chkautoPO)
        Me.RadGroupBox1.Controls.Add(Me.chkstd_rate)
        Me.RadGroupBox1.Controls.Add(Me.chkMRN)
        Me.RadGroupBox1.Controls.Add(Me.chkAllowLargerItemCost)
        Me.RadGroupBox1.Controls.Add(Me.chkGRN)
        Me.RadGroupBox1.Controls.Add(Me.chkDisableShipToLocation)
        Me.RadGroupBox1.Controls.Add(Me.chkmailoff)
        Me.RadGroupBox1.Controls.Add(Me.chkRequiredFOC)
        Me.RadGroupBox1.Controls.Add(Me.chkRequiredSecurityAmt)
        Me.RadGroupBox1.Controls.Add(Me.chkOneItemOneVendor)
        Me.RadGroupBox1.Controls.Add(Me.chkPickItemFromVendorItemDetails)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.chkItemReorderLevel)
        Me.RadGroupBox1.Controls.Add(Me.chkPOReq)
        Me.RadGroupBox1.Controls.Add(Me.Chkabatement)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1022, 413)
        Me.RadGroupBox1.TabIndex = 0
        '
        'ChkAutoGenerateMRN
        '
        Me.ChkAutoGenerateMRN.Location = New System.Drawing.Point(374, 99)
        Me.ChkAutoGenerateMRN.MyLinkLable1 = Nothing
        Me.ChkAutoGenerateMRN.MyLinkLable2 = Nothing
        Me.ChkAutoGenerateMRN.Name = "ChkAutoGenerateMRN"
        Me.ChkAutoGenerateMRN.Size = New System.Drawing.Size(122, 18)
        Me.ChkAutoGenerateMRN.TabIndex = 73
        Me.ChkAutoGenerateMRN.Tag1 = Nothing
        Me.ChkAutoGenerateMRN.Text = "Auto Generate MRN"
        '
        'chkPI_debitnot_unitcost
        '
        Me.chkPI_debitnot_unitcost.Location = New System.Drawing.Point(10, 332)
        Me.chkPI_debitnot_unitcost.MyLinkLable1 = Nothing
        Me.chkPI_debitnot_unitcost.MyLinkLable2 = Nothing
        Me.chkPI_debitnot_unitcost.Name = "chkPI_debitnot_unitcost"
        Me.chkPI_debitnot_unitcost.Size = New System.Drawing.Size(257, 18)
        Me.chkPI_debitnot_unitcost.TabIndex = 70
        Me.chkPI_debitnot_unitcost.Tag1 = Nothing
        Me.chkPI_debitnot_unitcost.Text = "Purchase Invoice Create debit note for unit cost"
        '
        'txtPoLimit
        '
        Me.txtPoLimit.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPoLimit.CalculationExpression = Nothing
        Me.txtPoLimit.DecimalPlaces = 0
        Me.txtPoLimit.FieldCode = Nothing
        Me.txtPoLimit.FieldDesc = Nothing
        Me.txtPoLimit.FieldMaxLength = 0
        Me.txtPoLimit.FieldName = Nothing
        Me.txtPoLimit.isCalculatedField = False
        Me.txtPoLimit.IsSourceFromTable = False
        Me.txtPoLimit.IsSourceFromValueList = False
        Me.txtPoLimit.IsUnique = False
        Me.txtPoLimit.Location = New System.Drawing.Point(736, 222)
        Me.txtPoLimit.MaxLength = 15
        Me.txtPoLimit.MendatroryField = False
        Me.txtPoLimit.MyLinkLable1 = Nothing
        Me.txtPoLimit.MyLinkLable2 = Nothing
        Me.txtPoLimit.Name = "txtPoLimit"
        Me.txtPoLimit.ReferenceFieldDesc = Nothing
        Me.txtPoLimit.ReferenceFieldName = Nothing
        Me.txtPoLimit.ReferenceTableName = Nothing
        Me.txtPoLimit.Size = New System.Drawing.Size(273, 20)
        Me.txtPoLimit.TabIndex = 68
        Me.txtPoLimit.Text = "0"
        Me.txtPoLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPoLimit.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(637, 222)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel6.TabIndex = 69
        Me.MyLabel6.Text = "PO Limit"
        '
        'ChkShowCostCenterAndHierarchyLevelInPurchaseModule
        '
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Location = New System.Drawing.Point(10, 351)
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.MyLinkLable1 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.MyLinkLable2 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Name = "ChkShowCostCenterAndHierarchyLevelInPurchaseModule"
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Size = New System.Drawing.Size(315, 18)
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.TabIndex = 67
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Tag1 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Text = "Show Cost Center and  Hierarchy Level in Purchase Module"
        '
        'TxtGRNLim
        '
        Me.TxtGRNLim.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtGRNLim.CalculationExpression = Nothing
        Me.TxtGRNLim.DecimalPlaces = 0
        Me.TxtGRNLim.FieldCode = Nothing
        Me.TxtGRNLim.FieldDesc = Nothing
        Me.TxtGRNLim.FieldMaxLength = 0
        Me.TxtGRNLim.FieldName = Nothing
        Me.TxtGRNLim.isCalculatedField = False
        Me.TxtGRNLim.IsSourceFromTable = False
        Me.TxtGRNLim.IsSourceFromValueList = False
        Me.TxtGRNLim.IsUnique = False
        Me.TxtGRNLim.Location = New System.Drawing.Point(736, 318)
        Me.TxtGRNLim.MaxLength = 15
        Me.TxtGRNLim.MendatroryField = False
        Me.TxtGRNLim.MyLinkLable1 = Nothing
        Me.TxtGRNLim.MyLinkLable2 = Nothing
        Me.TxtGRNLim.Name = "TxtGRNLim"
        Me.TxtGRNLim.ReferenceFieldDesc = Nothing
        Me.TxtGRNLim.ReferenceFieldName = Nothing
        Me.TxtGRNLim.ReferenceTableName = Nothing
        Me.TxtGRNLim.Size = New System.Drawing.Size(273, 20)
        Me.TxtGRNLim.TabIndex = 65
        Me.TxtGRNLim.Text = "0"
        Me.TxtGRNLim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtGRNLim.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(637, 320)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel5.TabIndex = 66
        Me.MyLabel5.Text = "GRN Limit"
        '
        'chkShortageIncludeInLandedCost
        '
        Me.chkShortageIncludeInLandedCost.Location = New System.Drawing.Point(10, 161)
        Me.chkShortageIncludeInLandedCost.MyLinkLable1 = Nothing
        Me.chkShortageIncludeInLandedCost.MyLinkLable2 = Nothing
        Me.chkShortageIncludeInLandedCost.Name = "chkShortageIncludeInLandedCost"
        Me.chkShortageIncludeInLandedCost.Size = New System.Drawing.Size(331, 18)
        Me.chkShortageIncludeInLandedCost.TabIndex = 64
        Me.chkShortageIncludeInLandedCost.Tag1 = Nothing
        Me.chkShortageIncludeInLandedCost.Text = "Shortage Include in Landed Cost (SRN/Pur Invoice/Pur Return)"
        '
        'ChkSkipMRNGRNinCaseofMT
        '
        Me.ChkSkipMRNGRNinCaseofMT.Location = New System.Drawing.Point(374, 81)
        Me.ChkSkipMRNGRNinCaseofMT.MyLinkLable1 = Nothing
        Me.ChkSkipMRNGRNinCaseofMT.MyLinkLable2 = Nothing
        Me.ChkSkipMRNGRNinCaseofMT.Name = "ChkSkipMRNGRNinCaseofMT"
        Me.ChkSkipMRNGRNinCaseofMT.Size = New System.Drawing.Size(118, 18)
        Me.ChkSkipMRNGRNinCaseofMT.TabIndex = 63
        Me.ChkSkipMRNGRNinCaseofMT.Tag1 = Nothing
        Me.ChkSkipMRNGRNinCaseofMT.Text = "Skip GRN and MRN"
        '
        'chkSRNPrintQtywise
        '
        Me.chkSRNPrintQtywise.Location = New System.Drawing.Point(10, 142)
        Me.chkSRNPrintQtywise.MyLinkLable1 = Nothing
        Me.chkSRNPrintQtywise.MyLinkLable2 = Nothing
        Me.chkSRNPrintQtywise.Name = "chkSRNPrintQtywise"
        Me.chkSRNPrintQtywise.Size = New System.Drawing.Size(140, 18)
        Me.chkSRNPrintQtywise.TabIndex = 62
        Me.chkSRNPrintQtywise.Tag1 = Nothing
        Me.chkSRNPrintQtywise.Text = "SRN Print Only Quantity"
        '
        'ChkGLAccToItem
        '
        Me.ChkGLAccToItem.Location = New System.Drawing.Point(10, 123)
        Me.ChkGLAccToItem.MyLinkLable1 = Nothing
        Me.ChkGLAccToItem.MyLinkLable2 = Nothing
        Me.ChkGLAccToItem.Name = "ChkGLAccToItem"
        Me.ChkGLAccToItem.Size = New System.Drawing.Size(254, 18)
        Me.ChkGLAccToItem.TabIndex = 61
        Me.ChkGLAccToItem.Tag1 = Nothing
        Me.ChkGLAccToItem.Text = "Create Journal Entry Acc. To Item(Issue/Return)"
        '
        'ChkCostEditIssue
        '
        Me.ChkCostEditIssue.Location = New System.Drawing.Point(10, 104)
        Me.ChkCostEditIssue.MyLinkLable1 = Nothing
        Me.ChkCostEditIssue.MyLinkLable2 = Nothing
        Me.ChkCostEditIssue.Name = "ChkCostEditIssue"
        Me.ChkCostEditIssue.Size = New System.Drawing.Size(240, 18)
        Me.ChkCostEditIssue.TabIndex = 60
        Me.ChkCostEditIssue.Tag1 = Nothing
        Me.ChkCostEditIssue.Text = "Is unit cost editable on Issue/Retun/Transfer"
        '
        'chkQCColumnAddedonMRN
        '
        Me.chkQCColumnAddedonMRN.Location = New System.Drawing.Point(10, 85)
        Me.chkQCColumnAddedonMRN.MyLinkLable1 = Nothing
        Me.chkQCColumnAddedonMRN.MyLinkLable2 = Nothing
        Me.chkQCColumnAddedonMRN.Name = "chkQCColumnAddedonMRN"
        Me.chkQCColumnAddedonMRN.Size = New System.Drawing.Size(159, 18)
        Me.chkQCColumnAddedonMRN.TabIndex = 59
        Me.chkQCColumnAddedonMRN.Tag1 = Nothing
        Me.chkQCColumnAddedonMRN.Text = "QC column added on MRN "
        '
        'chkRemarkReasononPO
        '
        Me.chkRemarkReasononPO.Location = New System.Drawing.Point(374, 350)
        Me.chkRemarkReasononPO.MyLinkLable1 = Nothing
        Me.chkRemarkReasononPO.MyLinkLable2 = Nothing
        Me.chkRemarkReasononPO.Name = "chkRemarkReasononPO"
        Me.chkRemarkReasononPO.Size = New System.Drawing.Size(211, 18)
        Me.chkRemarkReasononPO.TabIndex = 58
        Me.chkRemarkReasononPO.Tag1 = Nothing
        Me.chkRemarkReasononPO.Text = "Reason for Remarks mandatory on PO"
        '
        'TxtSRNLim
        '
        Me.TxtSRNLim.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSRNLim.CalculationExpression = Nothing
        Me.TxtSRNLim.DecimalPlaces = 0
        Me.TxtSRNLim.FieldCode = Nothing
        Me.TxtSRNLim.FieldDesc = Nothing
        Me.TxtSRNLim.FieldMaxLength = 0
        Me.TxtSRNLim.FieldName = Nothing
        Me.TxtSRNLim.isCalculatedField = False
        Me.TxtSRNLim.IsSourceFromTable = False
        Me.TxtSRNLim.IsSourceFromValueList = False
        Me.TxtSRNLim.IsUnique = False
        Me.TxtSRNLim.Location = New System.Drawing.Point(736, 297)
        Me.TxtSRNLim.MaxLength = 15
        Me.TxtSRNLim.MendatroryField = False
        Me.TxtSRNLim.MyLinkLable1 = Nothing
        Me.TxtSRNLim.MyLinkLable2 = Nothing
        Me.TxtSRNLim.Name = "TxtSRNLim"
        Me.TxtSRNLim.ReferenceFieldDesc = Nothing
        Me.TxtSRNLim.ReferenceFieldName = Nothing
        Me.TxtSRNLim.ReferenceTableName = Nothing
        Me.TxtSRNLim.Size = New System.Drawing.Size(273, 20)
        Me.TxtSRNLim.TabIndex = 56
        Me.TxtSRNLim.Text = "0"
        Me.TxtSRNLim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSRNLim.Value = 0R
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(637, 299)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel27.TabIndex = 57
        Me.MyLabel27.Text = "SRN Limit"
        '
        'chkPOInvCreateDebitNoteForReject
        '
        Me.chkPOInvCreateDebitNoteForReject.Location = New System.Drawing.Point(10, 313)
        Me.chkPOInvCreateDebitNoteForReject.MyLinkLable1 = Nothing
        Me.chkPOInvCreateDebitNoteForReject.MyLinkLable2 = Nothing
        Me.chkPOInvCreateDebitNoteForReject.Name = "chkPOInvCreateDebitNoteForReject"
        Me.chkPOInvCreateDebitNoteForReject.Size = New System.Drawing.Size(242, 18)
        Me.chkPOInvCreateDebitNoteForReject.TabIndex = 52
        Me.chkPOInvCreateDebitNoteForReject.Tag1 = Nothing
        Me.chkPOInvCreateDebitNoteForReject.Text = "Purchase Invoice Create debit note for reject"
        '
        'ChkRateEditSRN
        '
        Me.ChkRateEditSRN.Location = New System.Drawing.Point(10, 66)
        Me.ChkRateEditSRN.MyLinkLable1 = Nothing
        Me.ChkRateEditSRN.MyLinkLable2 = Nothing
        Me.ChkRateEditSRN.Name = "ChkRateEditSRN"
        Me.ChkRateEditSRN.Size = New System.Drawing.Size(134, 18)
        Me.ChkRateEditSRN.TabIndex = 51
        Me.ChkRateEditSRN.Tag1 = Nothing
        Me.ChkRateEditSRN.Text = "Is rate editable on SRN"
        '
        'chkEnableProjectFinder
        '
        Me.chkEnableProjectFinder.Location = New System.Drawing.Point(374, 332)
        Me.chkEnableProjectFinder.MyLinkLable1 = Nothing
        Me.chkEnableProjectFinder.MyLinkLable2 = Nothing
        Me.chkEnableProjectFinder.Name = "chkEnableProjectFinder"
        Me.chkEnableProjectFinder.Size = New System.Drawing.Size(122, 18)
        Me.chkEnableProjectFinder.TabIndex = 50
        Me.chkEnableProjectFinder.Tag1 = Nothing
        Me.chkEnableProjectFinder.Text = "EnableProject Finder"
        '
        'chkPOInvCreateDebitNoteForRejectAndShort
        '
        Me.chkPOInvCreateDebitNoteForRejectAndShort.Location = New System.Drawing.Point(10, 294)
        Me.chkPOInvCreateDebitNoteForRejectAndShort.MyLinkLable1 = Nothing
        Me.chkPOInvCreateDebitNoteForRejectAndShort.MyLinkLable2 = Nothing
        Me.chkPOInvCreateDebitNoteForRejectAndShort.Name = "chkPOInvCreateDebitNoteForRejectAndShort"
        Me.chkPOInvCreateDebitNoteForRejectAndShort.Size = New System.Drawing.Size(240, 18)
        Me.chkPOInvCreateDebitNoteForRejectAndShort.TabIndex = 49
        Me.chkPOInvCreateDebitNoteForRejectAndShort.Tag1 = Nothing
        Me.chkPOInvCreateDebitNoteForRejectAndShort.Text = "Purchase Invoice Create debit note for short"
        '
        'chkRFQ
        '
        Me.chkRFQ.Location = New System.Drawing.Point(374, 314)
        Me.chkRFQ.MyLinkLable1 = Nothing
        Me.chkRFQ.MyLinkLable2 = Nothing
        Me.chkRFQ.Name = "chkRFQ"
        Me.chkRFQ.Size = New System.Drawing.Size(71, 18)
        Me.chkRFQ.TabIndex = 39
        Me.chkRFQ.Tag1 = Nothing
        Me.chkRFQ.Text = "Show RFQ"
        '
        'txtJobWork
        '
        Me.txtJobWork.CalculationExpression = Nothing
        Me.txtJobWork.FieldCode = Nothing
        Me.txtJobWork.FieldDesc = Nothing
        Me.txtJobWork.FieldMaxLength = 0
        Me.txtJobWork.FieldName = Nothing
        Me.txtJobWork.isCalculatedField = False
        Me.txtJobWork.IsSourceFromTable = False
        Me.txtJobWork.IsSourceFromValueList = False
        Me.txtJobWork.IsUnique = False
        Me.txtJobWork.Location = New System.Drawing.Point(736, 341)
        Me.txtJobWork.MendatroryField = False
        Me.txtJobWork.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobWork.MyLinkLable1 = Nothing
        Me.txtJobWork.MyLinkLable2 = Nothing
        Me.txtJobWork.MyReadOnly = False
        Me.txtJobWork.MyShowMasterFormButton = False
        Me.txtJobWork.Name = "txtJobWork"
        Me.txtJobWork.ReferenceFieldDesc = Nothing
        Me.txtJobWork.ReferenceFieldName = Nothing
        Me.txtJobWork.ReferenceTableName = Nothing
        Me.txtJobWork.Size = New System.Drawing.Size(161, 19)
        Me.txtJobWork.TabIndex = 32
        Me.txtJobWork.Value = ""
        '
        'LblJobWork
        '
        Me.LblJobWork.CalculationExpression = Nothing
        Me.LblJobWork.FieldCode = Nothing
        Me.LblJobWork.FieldDesc = Nothing
        Me.LblJobWork.FieldMaxLength = 0
        Me.LblJobWork.FieldName = Nothing
        Me.LblJobWork.isCalculatedField = False
        Me.LblJobWork.IsSourceFromTable = False
        Me.LblJobWork.IsSourceFromValueList = False
        Me.LblJobWork.IsUnique = False
        Me.LblJobWork.Location = New System.Drawing.Point(901, 339)
        Me.LblJobWork.MendatroryField = False
        Me.LblJobWork.MyLinkLable1 = Nothing
        Me.LblJobWork.MyLinkLable2 = Nothing
        Me.LblJobWork.Name = "LblJobWork"
        Me.LblJobWork.ReadOnly = True
        Me.LblJobWork.ReferenceFieldDesc = Nothing
        Me.LblJobWork.ReferenceFieldName = Nothing
        Me.LblJobWork.ReferenceTableName = Nothing
        Me.LblJobWork.Size = New System.Drawing.Size(107, 20)
        Me.LblJobWork.TabIndex = 48
        Me.LblJobWork.TabStop = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(637, 342)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel4.TabIndex = 47
        Me.MyLabel4.Text = "Job Work"
        '
        'chkShowInvoiceInPOfinder
        '
        Me.chkShowInvoiceInPOfinder.Location = New System.Drawing.Point(10, 275)
        Me.chkShowInvoiceInPOfinder.MyLinkLable1 = Nothing
        Me.chkShowInvoiceInPOfinder.MyLinkLable2 = Nothing
        Me.chkShowInvoiceInPOfinder.Name = "chkShowInvoiceInPOfinder"
        Me.chkShowInvoiceInPOfinder.Size = New System.Drawing.Size(231, 18)
        Me.chkShowInvoiceInPOfinder.TabIndex = 38
        Me.chkShowInvoiceInPOfinder.Tag1 = Nothing
        Me.chkShowInvoiceInPOfinder.Text = "Show Sale Invoice No on PO finder in SRN"
        '
        'chksmsatpost
        '
        Me.chksmsatpost.Location = New System.Drawing.Point(374, 296)
        Me.chksmsatpost.MyLinkLable1 = Nothing
        Me.chksmsatpost.MyLinkLable2 = Nothing
        Me.chksmsatpost.Name = "chksmsatpost"
        Me.chksmsatpost.Size = New System.Drawing.Size(110, 18)
        Me.chksmsatpost.TabIndex = 37
        Me.chksmsatpost.Tag1 = Nothing
        Me.chksmsatpost.Text = "SMS Send At Post"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(637, 281)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 35
        Me.MyLabel3.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.CalculationExpression = Nothing
        Me.txtCustomer.FieldCode = Nothing
        Me.txtCustomer.FieldDesc = Nothing
        Me.txtCustomer.FieldMaxLength = 0
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.isCalculatedField = False
        Me.txtCustomer.IsSourceFromTable = False
        Me.txtCustomer.IsSourceFromValueList = False
        Me.txtCustomer.IsUnique = False
        Me.txtCustomer.Location = New System.Drawing.Point(736, 279)
        Me.txtCustomer.MaxLength = 200
        Me.txtCustomer.MendatroryField = False
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(273, 18)
        Me.txtCustomer.TabIndex = 36
        '
        'ChkInvoiceBasedPO
        '
        Me.ChkInvoiceBasedPO.Location = New System.Drawing.Point(374, 170)
        Me.ChkInvoiceBasedPO.MyLinkLable1 = Nothing
        Me.ChkInvoiceBasedPO.MyLinkLable2 = Nothing
        Me.ChkInvoiceBasedPO.Name = "ChkInvoiceBasedPO"
        Me.ChkInvoiceBasedPO.Size = New System.Drawing.Size(106, 18)
        Me.ChkInvoiceBasedPO.TabIndex = 34
        Me.ChkInvoiceBasedPO.Tag1 = Nothing
        Me.ChkInvoiceBasedPO.Text = "Invoice Based PO"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(637, 263)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Database Name"
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.CalculationExpression = Nothing
        Me.txtDatabaseName.FieldCode = Nothing
        Me.txtDatabaseName.FieldDesc = Nothing
        Me.txtDatabaseName.FieldMaxLength = 0
        Me.txtDatabaseName.FieldName = Nothing
        Me.txtDatabaseName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabaseName.isCalculatedField = False
        Me.txtDatabaseName.IsSourceFromTable = False
        Me.txtDatabaseName.IsSourceFromValueList = False
        Me.txtDatabaseName.IsUnique = False
        Me.txtDatabaseName.Location = New System.Drawing.Point(736, 261)
        Me.txtDatabaseName.MaxLength = 200
        Me.txtDatabaseName.MendatroryField = False
        Me.txtDatabaseName.MyLinkLable1 = Me.MyLabel2
        Me.txtDatabaseName.MyLinkLable2 = Nothing
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.ReferenceFieldDesc = Nothing
        Me.txtDatabaseName.ReferenceFieldName = Nothing
        Me.txtDatabaseName.ReferenceTableName = Nothing
        Me.txtDatabaseName.Size = New System.Drawing.Size(273, 18)
        Me.txtDatabaseName.TabIndex = 7
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(637, 244)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(89, 16)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "Principal Vendor"
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
        Me.txtVendorNo.Location = New System.Drawing.Point(736, 242)
        Me.txtVendorNo.MendatroryField = False
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Nothing
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(273, 18)
        Me.txtVendorNo.TabIndex = 3
        Me.txtVendorNo.Value = ""
        '
        'ChkShowStatusPur
        '
        Me.ChkShowStatusPur.Location = New System.Drawing.Point(374, 278)
        Me.ChkShowStatusPur.MyLinkLable1 = Nothing
        Me.ChkShowStatusPur.MyLinkLable2 = Nothing
        Me.ChkShowStatusPur.Name = "ChkShowStatusPur"
        Me.ChkShowStatusPur.Size = New System.Drawing.Size(148, 18)
        Me.ChkShowStatusPur.TabIndex = 33
        Me.ChkShowStatusPur.Tag1 = Nothing
        Me.ChkShowStatusPur.Text = "Show Status For Purchase"
        '
        'chkDemoAmendment
        '
        Me.chkDemoAmendment.Location = New System.Drawing.Point(374, 152)
        Me.chkDemoAmendment.MyLinkLable1 = Nothing
        Me.chkDemoAmendment.MyLinkLable2 = Nothing
        Me.chkDemoAmendment.Name = "chkDemoAmendment"
        Me.chkDemoAmendment.Size = New System.Drawing.Size(172, 18)
        Me.chkDemoAmendment.TabIndex = 32
        Me.chkDemoAmendment.Tag1 = Nothing
        Me.chkDemoAmendment.Text = "Amendment Details For Demo"
        '
        'chkVendor_Nlevel
        '
        Me.chkVendor_Nlevel.Location = New System.Drawing.Point(374, 242)
        Me.chkVendor_Nlevel.MyLinkLable1 = Nothing
        Me.chkVendor_Nlevel.MyLinkLable2 = Nothing
        Me.chkVendor_Nlevel.Name = "chkVendor_Nlevel"
        Me.chkVendor_Nlevel.Size = New System.Drawing.Size(195, 18)
        Me.chkVendor_Nlevel.TabIndex = 26
        Me.chkVendor_Nlevel.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkVendor_Nlevel.Tag1 = Nothing
        Me.chkVendor_Nlevel.Text = "Allow N-Level Category On Vendor"
        '
        'chkSRNRejected
        '
        Me.chkSRNRejected.Location = New System.Drawing.Point(10, 256)
        Me.chkSRNRejected.MyLinkLable1 = Nothing
        Me.chkSRNRejected.MyLinkLable2 = Nothing
        Me.chkSRNRejected.Name = "chkSRNRejected"
        Me.chkSRNRejected.Size = New System.Drawing.Size(269, 18)
        Me.chkSRNRejected.TabIndex = 24
        Me.chkSRNRejected.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkSRNRejected.Tag1 = Nothing
        Me.chkSRNRejected.Text = "Allow SRN to Send Rejected Qty in Rejected Store"
        '
        'chkReturnWithoutInvoice
        '
        Me.chkReturnWithoutInvoice.Location = New System.Drawing.Point(374, 224)
        Me.chkReturnWithoutInvoice.MyLinkLable1 = Nothing
        Me.chkReturnWithoutInvoice.MyLinkLable2 = Nothing
        Me.chkReturnWithoutInvoice.Name = "chkReturnWithoutInvoice"
        Me.chkReturnWithoutInvoice.Size = New System.Drawing.Size(214, 18)
        Me.chkReturnWithoutInvoice.TabIndex = 22
        Me.chkReturnWithoutInvoice.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkReturnWithoutInvoice.Tag1 = Nothing
        Me.chkReturnWithoutInvoice.Text = "Allow Purchase Return WIthout Invoice"
        '
        'chkPurchaseOrderItemQtyBelow
        '
        Me.chkPurchaseOrderItemQtyBelow.Location = New System.Drawing.Point(10, 199)
        Me.chkPurchaseOrderItemQtyBelow.MyLinkLable1 = Nothing
        Me.chkPurchaseOrderItemQtyBelow.MyLinkLable2 = Nothing
        Me.chkPurchaseOrderItemQtyBelow.Name = "chkPurchaseOrderItemQtyBelow"
        Me.chkPurchaseOrderItemQtyBelow.Size = New System.Drawing.Size(363, 18)
        Me.chkPurchaseOrderItemQtyBelow.TabIndex = 31
        Me.chkPurchaseOrderItemQtyBelow.Tag1 = Nothing
        Me.chkPurchaseOrderItemQtyBelow.Text = "Generate PO Automatically when Item Qty Below from Reorder Level"
        '
        'cboNoticationSettingInPurchaseRequisition
        '
        Me.cboNoticationSettingInPurchaseRequisition.AutoCompleteDisplayMember = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.AutoCompleteValueMember = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.CalculationExpression = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.DropDownAnimationEnabled = True
        Me.cboNoticationSettingInPurchaseRequisition.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboNoticationSettingInPurchaseRequisition.FieldCode = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.FieldDesc = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.FieldMaxLength = 0
        Me.cboNoticationSettingInPurchaseRequisition.FieldName = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.isCalculatedField = False
        Me.cboNoticationSettingInPurchaseRequisition.IsSourceFromTable = False
        Me.cboNoticationSettingInPurchaseRequisition.IsSourceFromValueList = False
        Me.cboNoticationSettingInPurchaseRequisition.IsUnique = False
        Me.cboNoticationSettingInPurchaseRequisition.Location = New System.Drawing.Point(197, 388)
        Me.cboNoticationSettingInPurchaseRequisition.MendatroryField = False
        Me.cboNoticationSettingInPurchaseRequisition.MyLinkLable1 = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.MyLinkLable2 = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.Name = "cboNoticationSettingInPurchaseRequisition"
        Me.cboNoticationSettingInPurchaseRequisition.ReferenceFieldDesc = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.ReferenceFieldName = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.ReferenceTableName = Nothing
        Me.cboNoticationSettingInPurchaseRequisition.Size = New System.Drawing.Size(147, 20)
        Me.cboNoticationSettingInPurchaseRequisition.TabIndex = 30
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 390)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(186, 16)
        Me.MyLabel1.TabIndex = 29
        Me.MyLabel1.Text = "Notification Setting Indent Reorder  "
        '
        'cboNoticationSettingInPO
        '
        Me.cboNoticationSettingInPO.AutoCompleteDisplayMember = Nothing
        Me.cboNoticationSettingInPO.AutoCompleteValueMember = Nothing
        Me.cboNoticationSettingInPO.CalculationExpression = Nothing
        Me.cboNoticationSettingInPO.DropDownAnimationEnabled = True
        Me.cboNoticationSettingInPO.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboNoticationSettingInPO.FieldCode = Nothing
        Me.cboNoticationSettingInPO.FieldDesc = Nothing
        Me.cboNoticationSettingInPO.FieldMaxLength = 0
        Me.cboNoticationSettingInPO.FieldName = Nothing
        Me.cboNoticationSettingInPO.isCalculatedField = False
        Me.cboNoticationSettingInPO.IsSourceFromTable = False
        Me.cboNoticationSettingInPO.IsSourceFromValueList = False
        Me.cboNoticationSettingInPO.IsUnique = False
        Me.cboNoticationSettingInPO.Location = New System.Drawing.Point(198, 368)
        Me.cboNoticationSettingInPO.MendatroryField = False
        Me.cboNoticationSettingInPO.MyLinkLable1 = Nothing
        Me.cboNoticationSettingInPO.MyLinkLable2 = Nothing
        Me.cboNoticationSettingInPO.Name = "cboNoticationSettingInPO"
        Me.cboNoticationSettingInPO.ReferenceFieldDesc = Nothing
        Me.cboNoticationSettingInPO.ReferenceFieldName = Nothing
        Me.cboNoticationSettingInPO.ReferenceTableName = Nothing
        Me.cboNoticationSettingInPO.Size = New System.Drawing.Size(147, 20)
        Me.cboNoticationSettingInPO.TabIndex = 28
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(10, 370)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(171, 16)
        Me.RadLabel18.TabIndex = 27
        Me.RadLabel18.Text = "Notification Setting PO Reorder  "
        '
        'chkmccPO
        '
        Me.chkmccPO.Location = New System.Drawing.Point(374, 188)
        Me.chkmccPO.MyLinkLable1 = Nothing
        Me.chkmccPO.MyLinkLable2 = Nothing
        Me.chkmccPO.Name = "chkmccPO"
        Me.chkmccPO.Size = New System.Drawing.Size(139, 18)
        Me.chkmccPO.TabIndex = 19
        Me.chkmccPO.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkmccPO.Tag1 = Nothing
        Me.chkmccPO.Text = "MCC At Purchase Order"
        '
        'chkautoPO
        '
        Me.chkautoPO.Location = New System.Drawing.Point(374, 206)
        Me.chkautoPO.MyLinkLable1 = Nothing
        Me.chkautoPO.MyLinkLable2 = Nothing
        Me.chkautoPO.Name = "chkautoPO"
        Me.chkautoPO.Size = New System.Drawing.Size(101, 18)
        Me.chkautoPO.TabIndex = 20
        Me.chkautoPO.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkautoPO.Tag1 = Nothing
        Me.chkautoPO.Text = "Auto PO At SRN"
        '
        'chkstd_rate
        '
        Me.chkstd_rate.Location = New System.Drawing.Point(10, 180)
        Me.chkstd_rate.MyLinkLable1 = Nothing
        Me.chkstd_rate.MyLinkLable2 = Nothing
        Me.chkstd_rate.Name = "chkstd_rate"
        Me.chkstd_rate.Size = New System.Drawing.Size(332, 18)
        Me.chkstd_rate.TabIndex = 17
        Me.chkstd_rate.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkstd_rate.Tag1 = Nothing
        Me.chkstd_rate.Text = "Check Standard Rate From Item Master On Vendor Item Detail"
        '
        'chkMRN
        '
        Me.chkMRN.Location = New System.Drawing.Point(374, 134)
        Me.chkMRN.MyLinkLable1 = Nothing
        Me.chkMRN.MyLinkLable2 = Nothing
        Me.chkMRN.Name = "chkMRN"
        Me.chkMRN.Size = New System.Drawing.Size(195, 18)
        Me.chkMRN.TabIndex = 13
        Me.chkMRN.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkMRN.Tag1 = Nothing
        Me.chkMRN.Text = "Show MRN (Material Receipt Note)"
        '
        'chkAllowLargerItemCost
        '
        Me.chkAllowLargerItemCost.Location = New System.Drawing.Point(374, 63)
        Me.chkAllowLargerItemCost.MyLinkLable1 = Nothing
        Me.chkAllowLargerItemCost.MyLinkLable2 = Nothing
        Me.chkAllowLargerItemCost.Name = "chkAllowLargerItemCost"
        Me.chkAllowLargerItemCost.Size = New System.Drawing.Size(252, 18)
        Me.chkAllowLargerItemCost.TabIndex = 11
        Me.chkAllowLargerItemCost.Tag = "AllowLargerItemCostThenVendorItemCost"
        Me.chkAllowLargerItemCost.Tag1 = Nothing
        Me.chkAllowLargerItemCost.Text = "Allow Larger Item Cost Then Vendor Item Cost"
        '
        'chkGRN
        '
        Me.chkGRN.Location = New System.Drawing.Point(374, 116)
        Me.chkGRN.MyLinkLable1 = Nothing
        Me.chkGRN.MyLinkLable2 = Nothing
        Me.chkGRN.Name = "chkGRN"
        Me.chkGRN.Size = New System.Drawing.Size(175, 18)
        Me.chkGRN.TabIndex = 12
        Me.chkGRN.Tag1 = Nothing
        Me.chkGRN.Text = "Show GRN (Gate Receipt Note)"
        '
        'chkDisableShipToLocation
        '
        Me.chkDisableShipToLocation.Location = New System.Drawing.Point(374, 45)
        Me.chkDisableShipToLocation.MyLinkLable1 = Nothing
        Me.chkDisableShipToLocation.MyLinkLable2 = Nothing
        Me.chkDisableShipToLocation.Name = "chkDisableShipToLocation"
        Me.chkDisableShipToLocation.Size = New System.Drawing.Size(233, 18)
        Me.chkDisableShipToLocation.TabIndex = 9
        Me.chkDisableShipToLocation.Tag1 = Nothing
        Me.chkDisableShipToLocation.Text = "Disable 'Ship to Location' from PO, PI, SRN"
        '
        'chkmailoff
        '
        Me.chkmailoff.Location = New System.Drawing.Point(374, 27)
        Me.chkmailoff.MyLinkLable1 = Nothing
        Me.chkmailoff.MyLinkLable2 = Nothing
        Me.chkmailoff.Name = "chkmailoff"
        Me.chkmailoff.Size = New System.Drawing.Size(101, 18)
        Me.chkmailoff.TabIndex = 5
        Me.chkmailoff.Tag1 = Nothing
        Me.chkmailoff.Text = "Send SMS/MAIL"
        '
        'chkRequiredFOC
        '
        Me.chkRequiredFOC.Location = New System.Drawing.Point(374, 9)
        Me.chkRequiredFOC.MyLinkLable1 = Nothing
        Me.chkRequiredFOC.MyLinkLable2 = Nothing
        Me.chkRequiredFOC.Name = "chkRequiredFOC"
        Me.chkRequiredFOC.Size = New System.Drawing.Size(89, 18)
        Me.chkRequiredFOC.TabIndex = 1
        Me.chkRequiredFOC.Tag1 = Nothing
        Me.chkRequiredFOC.Text = "Required FOC"
        '
        'chkRequiredSecurityAmt
        '
        Me.chkRequiredSecurityAmt.Location = New System.Drawing.Point(374, 260)
        Me.chkRequiredSecurityAmt.MyLinkLable1 = Nothing
        Me.chkRequiredSecurityAmt.MyLinkLable2 = Nothing
        Me.chkRequiredSecurityAmt.Name = "chkRequiredSecurityAmt"
        Me.chkRequiredSecurityAmt.Size = New System.Drawing.Size(151, 18)
        Me.chkRequiredSecurityAmt.TabIndex = 25
        Me.chkRequiredSecurityAmt.Tag1 = Nothing
        Me.chkRequiredSecurityAmt.Text = "Required Security Amount"
        '
        'chkOneItemOneVendor
        '
        Me.chkOneItemOneVendor.Location = New System.Drawing.Point(10, 237)
        Me.chkOneItemOneVendor.MyLinkLable1 = Nothing
        Me.chkOneItemOneVendor.MyLinkLable2 = Nothing
        Me.chkOneItemOneVendor.Name = "chkOneItemOneVendor"
        Me.chkOneItemOneVendor.Size = New System.Drawing.Size(130, 18)
        Me.chkOneItemOneVendor.TabIndex = 23
        Me.chkOneItemOneVendor.Tag1 = Nothing
        Me.chkOneItemOneVendor.Text = "One Item One Vendor"
        '
        'chkPickItemFromVendorItemDetails
        '
        Me.chkPickItemFromVendorItemDetails.Location = New System.Drawing.Point(10, 218)
        Me.chkPickItemFromVendorItemDetails.MyLinkLable1 = Nothing
        Me.chkPickItemFromVendorItemDetails.MyLinkLable2 = Nothing
        Me.chkPickItemFromVendorItemDetails.Name = "chkPickItemFromVendorItemDetails"
        Me.chkPickItemFromVendorItemDetails.Size = New System.Drawing.Size(197, 18)
        Me.chkPickItemFromVendorItemDetails.TabIndex = 21
        Me.chkPickItemFromVendorItemDetails.Tag1 = Nothing
        Me.chkPickItemFromVendorItemDetails.Text = "Pick Item From Vendor Item Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkOtherItemsexpDate)
        Me.RadGroupBox4.Controls.Add(Me.chkAssetexpDate)
        Me.RadGroupBox4.Controls.Add(Me.chkFGexpDate)
        Me.RadGroupBox4.Controls.Add(Me.chkRMexpDate)
        Me.RadGroupBox4.HeaderText = "Expiary Date Mandatory For"
        Me.RadGroupBox4.Location = New System.Drawing.Point(637, 94)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(374, 41)
        Me.RadGroupBox4.TabIndex = 18
        Me.RadGroupBox4.Tag = ""
        Me.RadGroupBox4.Text = "Expiary Date Mandatory For"
        '
        'chkOtherItemsexpDate
        '
        Me.chkOtherItemsexpDate.Location = New System.Drawing.Point(270, 17)
        Me.chkOtherItemsexpDate.Name = "chkOtherItemsexpDate"
        Me.chkOtherItemsexpDate.Size = New System.Drawing.Size(54, 18)
        Me.chkOtherItemsexpDate.TabIndex = 3
        Me.chkOtherItemsexpDate.Text = "Others"
        '
        'chkAssetexpDate
        '
        Me.chkAssetexpDate.Location = New System.Drawing.Point(206, 17)
        Me.chkAssetexpDate.Name = "chkAssetexpDate"
        Me.chkAssetexpDate.Size = New System.Drawing.Size(47, 18)
        Me.chkAssetexpDate.TabIndex = 2
        Me.chkAssetexpDate.Text = "Asset"
        '
        'chkFGexpDate
        '
        Me.chkFGexpDate.Location = New System.Drawing.Point(98, 17)
        Me.chkFGexpDate.Name = "chkFGexpDate"
        Me.chkFGexpDate.Size = New System.Drawing.Size(92, 18)
        Me.chkFGexpDate.TabIndex = 1
        Me.chkFGexpDate.Text = "Finished Good"
        '
        'chkRMexpDate
        '
        Me.chkRMexpDate.Location = New System.Drawing.Point(5, 17)
        Me.chkRMexpDate.Name = "chkRMexpDate"
        Me.chkRMexpDate.Size = New System.Drawing.Size(85, 18)
        Me.chkRMexpDate.TabIndex = 0
        Me.chkRMexpDate.Text = "Raw Material"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkOtherItemsmfgDate)
        Me.RadGroupBox3.Controls.Add(Me.chkAssetmfgDate)
        Me.RadGroupBox3.Controls.Add(Me.chkFGmfgDate)
        Me.RadGroupBox3.Controls.Add(Me.chkRMmfgDate)
        Me.RadGroupBox3.HeaderText = "Manufacturing Date Mandatory For"
        Me.RadGroupBox3.Location = New System.Drawing.Point(637, 179)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(374, 41)
        Me.RadGroupBox3.TabIndex = 14
        Me.RadGroupBox3.Text = "Manufacturing Date Mandatory For"
        '
        'chkOtherItemsmfgDate
        '
        Me.chkOtherItemsmfgDate.Location = New System.Drawing.Point(270, 17)
        Me.chkOtherItemsmfgDate.Name = "chkOtherItemsmfgDate"
        Me.chkOtherItemsmfgDate.Size = New System.Drawing.Size(54, 18)
        Me.chkOtherItemsmfgDate.TabIndex = 3
        Me.chkOtherItemsmfgDate.Text = "Others"
        '
        'chkAssetmfgDate
        '
        Me.chkAssetmfgDate.Location = New System.Drawing.Point(206, 17)
        Me.chkAssetmfgDate.Name = "chkAssetmfgDate"
        Me.chkAssetmfgDate.Size = New System.Drawing.Size(47, 18)
        Me.chkAssetmfgDate.TabIndex = 2
        Me.chkAssetmfgDate.Text = "Asset"
        '
        'chkFGmfgDate
        '
        Me.chkFGmfgDate.Location = New System.Drawing.Point(98, 17)
        Me.chkFGmfgDate.Name = "chkFGmfgDate"
        Me.chkFGmfgDate.Size = New System.Drawing.Size(92, 18)
        Me.chkFGmfgDate.TabIndex = 1
        Me.chkFGmfgDate.Text = "Finished Good"
        '
        'chkRMmfgDate
        '
        Me.chkRMmfgDate.Location = New System.Drawing.Point(5, 17)
        Me.chkRMmfgDate.Name = "chkRMmfgDate"
        Me.chkRMmfgDate.Size = New System.Drawing.Size(85, 18)
        Me.chkRMmfgDate.TabIndex = 0
        Me.chkRMmfgDate.Text = "Raw Material"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkOtherItemsBatch)
        Me.RadGroupBox2.Controls.Add(Me.chkAssetBatch)
        Me.RadGroupBox2.Controls.Add(Me.chkFGBatch)
        Me.RadGroupBox2.Controls.Add(Me.chkRMBatch)
        Me.RadGroupBox2.HeaderText = "Batch No Mandatory For"
        Me.RadGroupBox2.Location = New System.Drawing.Point(637, 137)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(374, 41)
        Me.RadGroupBox2.TabIndex = 10
        Me.RadGroupBox2.Text = "Batch No Mandatory For"
        '
        'chkOtherItemsBatch
        '
        Me.chkOtherItemsBatch.Location = New System.Drawing.Point(270, 17)
        Me.chkOtherItemsBatch.Name = "chkOtherItemsBatch"
        Me.chkOtherItemsBatch.Size = New System.Drawing.Size(54, 18)
        Me.chkOtherItemsBatch.TabIndex = 3
        Me.chkOtherItemsBatch.Text = "Others"
        '
        'chkAssetBatch
        '
        Me.chkAssetBatch.Location = New System.Drawing.Point(206, 17)
        Me.chkAssetBatch.Name = "chkAssetBatch"
        Me.chkAssetBatch.Size = New System.Drawing.Size(47, 18)
        Me.chkAssetBatch.TabIndex = 2
        Me.chkAssetBatch.Text = "Asset"
        '
        'chkFGBatch
        '
        Me.chkFGBatch.Location = New System.Drawing.Point(98, 17)
        Me.chkFGBatch.Name = "chkFGBatch"
        Me.chkFGBatch.Size = New System.Drawing.Size(92, 18)
        Me.chkFGBatch.TabIndex = 1
        Me.chkFGBatch.Text = "Finished Good"
        '
        'chkRMBatch
        '
        Me.chkRMBatch.Location = New System.Drawing.Point(5, 17)
        Me.chkRMBatch.Name = "chkRMBatch"
        Me.chkRMBatch.Size = New System.Drawing.Size(85, 18)
        Me.chkRMBatch.TabIndex = 0
        Me.chkRMBatch.Text = "Raw Material"
        '
        'chkItemReorderLevel
        '
        Me.chkItemReorderLevel.Location = New System.Drawing.Point(10, 47)
        Me.chkItemReorderLevel.Name = "chkItemReorderLevel"
        Me.chkItemReorderLevel.Size = New System.Drawing.Size(203, 18)
        Me.chkItemReorderLevel.TabIndex = 8
        Me.chkItemReorderLevel.Text = "Enable Popup for Item Reorder Level"
        '
        'chkPOReq
        '
        Me.chkPOReq.Location = New System.Drawing.Point(10, 28)
        Me.chkPOReq.Name = "chkPOReq"
        Me.chkPOReq.Size = New System.Drawing.Size(151, 18)
        Me.chkPOReq.TabIndex = 4
        Me.chkPOReq.Text = "Create PO with requisition"
        '
        'Chkabatement
        '
        Me.Chkabatement.Location = New System.Drawing.Point(10, 9)
        Me.Chkabatement.Name = "Chkabatement"
        Me.Chkabatement.Size = New System.Drawing.Size(162, 18)
        Me.Chkabatement.TabIndex = 0
        Me.Chkabatement.Text = "Create Abatement based PO"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtSlab3To)
        Me.RadGroupBox5.Controls.Add(Me.chkApplySlab)
        Me.RadGroupBox5.Controls.Add(Me.txtSlab2To)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox5.Controls.Add(Me.txtSlab3From)
        Me.RadGroupBox5.Controls.Add(Me.txtSlab1From)
        Me.RadGroupBox5.Controls.Add(Me.txtSlab1To)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox5.Controls.Add(Me.txtSlab2From)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(637, 3)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(374, 90)
        Me.RadGroupBox5.TabIndex = 72
        Me.RadGroupBox5.Tag = ""
        '
        'txtSlab3To
        '
        Me.txtSlab3To.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab3To.CalculationExpression = Nothing
        Me.txtSlab3To.DecimalPlaces = 0
        Me.txtSlab3To.FieldCode = Nothing
        Me.txtSlab3To.FieldDesc = Nothing
        Me.txtSlab3To.FieldMaxLength = 0
        Me.txtSlab3To.FieldName = Nothing
        Me.txtSlab3To.isCalculatedField = False
        Me.txtSlab3To.IsSourceFromTable = False
        Me.txtSlab3To.IsSourceFromValueList = False
        Me.txtSlab3To.IsUnique = False
        Me.txtSlab3To.Location = New System.Drawing.Point(265, 63)
        Me.txtSlab3To.MaxLength = 20
        Me.txtSlab3To.MendatroryField = False
        Me.txtSlab3To.MyLinkLable1 = Me.MyLabel9
        Me.txtSlab3To.MyLinkLable2 = Nothing
        Me.txtSlab3To.Name = "txtSlab3To"
        Me.txtSlab3To.ReadOnly = True
        Me.txtSlab3To.ReferenceFieldDesc = Nothing
        Me.txtSlab3To.ReferenceFieldName = Nothing
        Me.txtSlab3To.ReferenceTableName = Nothing
        Me.txtSlab3To.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab3To.TabIndex = 73
        Me.txtSlab3To.Text = "0"
        Me.txtSlab3To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab3To.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(14, 65)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel9.TabIndex = 74
        Me.MyLabel9.Text = "RAL Mandatory"
        '
        'chkApplySlab
        '
        Me.chkApplySlab.Location = New System.Drawing.Point(8, 3)
        Me.chkApplySlab.MyLinkLable1 = Nothing
        Me.chkApplySlab.MyLinkLable2 = Nothing
        Me.chkApplySlab.Name = "chkApplySlab"
        Me.chkApplySlab.Size = New System.Drawing.Size(121, 18)
        Me.chkApplySlab.TabIndex = 72
        Me.chkApplySlab.Tag1 = Nothing
        Me.chkApplySlab.Text = "Apply Purchase Slab"
        '
        'txtSlab2To
        '
        Me.txtSlab2To.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab2To.CalculationExpression = Nothing
        Me.txtSlab2To.DecimalPlaces = 0
        Me.txtSlab2To.FieldCode = Nothing
        Me.txtSlab2To.FieldDesc = Nothing
        Me.txtSlab2To.FieldMaxLength = 0
        Me.txtSlab2To.FieldName = Nothing
        Me.txtSlab2To.isCalculatedField = False
        Me.txtSlab2To.IsSourceFromTable = False
        Me.txtSlab2To.IsSourceFromValueList = False
        Me.txtSlab2To.IsUnique = False
        Me.txtSlab2To.Location = New System.Drawing.Point(265, 42)
        Me.txtSlab2To.MaxLength = 20
        Me.txtSlab2To.MendatroryField = False
        Me.txtSlab2To.MyLinkLable1 = Me.MyLabel8
        Me.txtSlab2To.MyLinkLable2 = Nothing
        Me.txtSlab2To.Name = "txtSlab2To"
        Me.txtSlab2To.ReferenceFieldDesc = Nothing
        Me.txtSlab2To.ReferenceFieldName = Nothing
        Me.txtSlab2To.ReferenceTableName = Nothing
        Me.txtSlab2To.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab2To.TabIndex = 73
        Me.txtSlab2To.Text = "0"
        Me.txtSlab2To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab2To.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(14, 44)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel8.TabIndex = 74
        Me.MyLabel8.Text = "PO Mandatory"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 23)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel7.TabIndex = 71
        Me.MyLabel7.Text = "Doument Not Required"
        '
        'txtSlab3From
        '
        Me.txtSlab3From.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab3From.CalculationExpression = Nothing
        Me.txtSlab3From.DecimalPlaces = 0
        Me.txtSlab3From.FieldCode = Nothing
        Me.txtSlab3From.FieldDesc = Nothing
        Me.txtSlab3From.FieldMaxLength = 0
        Me.txtSlab3From.FieldName = Nothing
        Me.txtSlab3From.isCalculatedField = False
        Me.txtSlab3From.IsSourceFromTable = False
        Me.txtSlab3From.IsSourceFromValueList = False
        Me.txtSlab3From.IsUnique = False
        Me.txtSlab3From.Location = New System.Drawing.Point(152, 63)
        Me.txtSlab3From.MaxLength = 20
        Me.txtSlab3From.MendatroryField = False
        Me.txtSlab3From.MyLinkLable1 = Me.MyLabel9
        Me.txtSlab3From.MyLinkLable2 = Nothing
        Me.txtSlab3From.Name = "txtSlab3From"
        Me.txtSlab3From.ReadOnly = True
        Me.txtSlab3From.ReferenceFieldDesc = Nothing
        Me.txtSlab3From.ReferenceFieldName = Nothing
        Me.txtSlab3From.ReferenceTableName = Nothing
        Me.txtSlab3From.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab3From.TabIndex = 72
        Me.txtSlab3From.Text = "0"
        Me.txtSlab3From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab3From.Value = 0R
        '
        'txtSlab1From
        '
        Me.txtSlab1From.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab1From.CalculationExpression = Nothing
        Me.txtSlab1From.DecimalPlaces = 0
        Me.txtSlab1From.FieldCode = Nothing
        Me.txtSlab1From.FieldDesc = Nothing
        Me.txtSlab1From.FieldMaxLength = 0
        Me.txtSlab1From.FieldName = Nothing
        Me.txtSlab1From.isCalculatedField = False
        Me.txtSlab1From.IsSourceFromTable = False
        Me.txtSlab1From.IsSourceFromValueList = False
        Me.txtSlab1From.IsUnique = False
        Me.txtSlab1From.Location = New System.Drawing.Point(152, 21)
        Me.txtSlab1From.MaxLength = 20
        Me.txtSlab1From.MendatroryField = False
        Me.txtSlab1From.MyLinkLable1 = Me.MyLabel7
        Me.txtSlab1From.MyLinkLable2 = Nothing
        Me.txtSlab1From.Name = "txtSlab1From"
        Me.txtSlab1From.ReadOnly = True
        Me.txtSlab1From.ReferenceFieldDesc = Nothing
        Me.txtSlab1From.ReferenceFieldName = Nothing
        Me.txtSlab1From.ReferenceTableName = Nothing
        Me.txtSlab1From.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab1From.TabIndex = 70
        Me.txtSlab1From.Text = "0"
        Me.txtSlab1From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab1From.Value = 0R
        '
        'txtSlab1To
        '
        Me.txtSlab1To.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab1To.CalculationExpression = Nothing
        Me.txtSlab1To.DecimalPlaces = 0
        Me.txtSlab1To.FieldCode = Nothing
        Me.txtSlab1To.FieldDesc = Nothing
        Me.txtSlab1To.FieldMaxLength = 0
        Me.txtSlab1To.FieldName = Nothing
        Me.txtSlab1To.isCalculatedField = False
        Me.txtSlab1To.IsSourceFromTable = False
        Me.txtSlab1To.IsSourceFromValueList = False
        Me.txtSlab1To.IsUnique = False
        Me.txtSlab1To.Location = New System.Drawing.Point(264, 21)
        Me.txtSlab1To.MaxLength = 20
        Me.txtSlab1To.MendatroryField = False
        Me.txtSlab1To.MyLinkLable1 = Me.MyLabel7
        Me.txtSlab1To.MyLinkLable2 = Nothing
        Me.txtSlab1To.Name = "txtSlab1To"
        Me.txtSlab1To.ReferenceFieldDesc = Nothing
        Me.txtSlab1To.ReferenceFieldName = Nothing
        Me.txtSlab1To.ReferenceTableName = Nothing
        Me.txtSlab1To.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab1To.TabIndex = 71
        Me.txtSlab1To.Text = "0"
        Me.txtSlab1To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab1To.Value = 0R
        '
        'txtSlab2From
        '
        Me.txtSlab2From.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSlab2From.CalculationExpression = Nothing
        Me.txtSlab2From.DecimalPlaces = 0
        Me.txtSlab2From.FieldCode = Nothing
        Me.txtSlab2From.FieldDesc = Nothing
        Me.txtSlab2From.FieldMaxLength = 0
        Me.txtSlab2From.FieldName = Nothing
        Me.txtSlab2From.isCalculatedField = False
        Me.txtSlab2From.IsSourceFromTable = False
        Me.txtSlab2From.IsSourceFromValueList = False
        Me.txtSlab2From.IsUnique = False
        Me.txtSlab2From.Location = New System.Drawing.Point(152, 42)
        Me.txtSlab2From.MaxLength = 20
        Me.txtSlab2From.MendatroryField = False
        Me.txtSlab2From.MyLinkLable1 = Me.MyLabel8
        Me.txtSlab2From.MyLinkLable2 = Nothing
        Me.txtSlab2From.Name = "txtSlab2From"
        Me.txtSlab2From.ReadOnly = True
        Me.txtSlab2From.ReferenceFieldDesc = Nothing
        Me.txtSlab2From.ReferenceFieldName = Nothing
        Me.txtSlab2From.ReferenceTableName = Nothing
        Me.txtSlab2From.Size = New System.Drawing.Size(107, 20)
        Me.txtSlab2From.TabIndex = 72
        Me.txtSlab2From.Text = "0"
        Me.txtSlab2From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSlab2From.Value = 0R
        '
        'chkAllstructurewiseItem
        '
        Me.chkAllstructurewiseItem.Location = New System.Drawing.Point(374, 369)
        Me.chkAllstructurewiseItem.MyLinkLable1 = Nothing
        Me.chkAllstructurewiseItem.MyLinkLable2 = Nothing
        Me.chkAllstructurewiseItem.Name = "chkAllstructurewiseItem"
        Me.chkAllstructurewiseItem.Size = New System.Drawing.Size(131, 18)
        Me.chkAllstructurewiseItem.TabIndex = 74
        Me.chkAllstructurewiseItem.Tag1 = Nothing
        Me.chkAllstructurewiseItem.Text = "All structure wise Item"
        '
        'frmPurchaseSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 457)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPurchaseSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settings"
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ChkAutoGenerateMRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPI_debitnot_unitcost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPoLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkShowCostCenterAndHierarchyLevelInPurchaseModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtGRNLim, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShortageIncludeInLandedCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSkipMRNGRNinCaseofMT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSRNPrintQtywise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkGLAccToItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCostEditIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkQCColumnAddedonMRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRemarkReasononPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSRNLim, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPOInvCreateDebitNoteForReject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkRateEditSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEnableProjectFinder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPOInvCreateDebitNoteForRejectAndShort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRFQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowInvoiceInPOfinder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chksmsatpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkInvoiceBasedPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDatabaseName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkShowStatusPur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDemoAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendor_Nlevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSRNRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReturnWithoutInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPurchaseOrderItemQtyBelow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNoticationSettingInPurchaseRequisition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNoticationSettingInPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmccPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkautoPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkstd_rate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowLargerItemCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDisableShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmailoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequiredFOC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequiredSecurityAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOneItemOneVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPickItemFromVendorItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkOtherItemsexpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetexpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFGexpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRMexpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.chkOtherItemsmfgDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetmfgDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFGmfgDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRMmfgDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.chkOtherItemsBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFGBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRMBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemReorderLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPOReq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chkabatement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtSlab3To, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplySlab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlab2To, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlab3From, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlab1From, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlab1To, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlab2From, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllstructurewiseItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkItemReorderLevel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPOReq As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Chkabatement As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAssetBatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFGBatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRMBatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOtherItemsBatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkOtherItemsexpDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAssetexpDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFGexpDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRMexpDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkOtherItemsmfgDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAssetmfgDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFGmfgDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRMmfgDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOneItemOneVendor As common.Controls.MyCheckBox
    Friend WithEvents chkPickItemFromVendorItemDetails As common.Controls.MyCheckBox
    Friend WithEvents chkRequiredFOC As common.Controls.MyCheckBox
    Friend WithEvents chkRequiredSecurityAmt As common.Controls.MyCheckBox
    Friend WithEvents chkmailoff As common.Controls.MyCheckBox
    Friend WithEvents chkDisableShipToLocation As common.Controls.MyCheckBox
    Friend WithEvents chkAllowLargerItemCost As common.Controls.MyCheckBox
    Friend WithEvents chkMRN As common.Controls.MyCheckBox
    Friend WithEvents chkGRN As common.Controls.MyCheckBox
    Friend WithEvents chkstd_rate As common.Controls.MyCheckBox
    Friend WithEvents chkautoPO As common.Controls.MyCheckBox
    Friend WithEvents chkmccPO As common.Controls.MyCheckBox
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents cboNoticationSettingInPO As common.Controls.MyComboBox
    Friend WithEvents cboNoticationSettingInPurchaseRequisition As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkPurchaseOrderItemQtyBelow As common.Controls.MyCheckBox
    Friend WithEvents chkReturnWithoutInvoice As common.Controls.MyCheckBox
    Friend WithEvents chkSRNRejected As common.Controls.MyCheckBox
    Friend WithEvents chkVendor_Nlevel As common.Controls.MyCheckBox
    Friend WithEvents chkDemoAmendment As common.Controls.MyCheckBox
    Friend WithEvents ChkShowStatusPur As common.Controls.MyCheckBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDatabaseName As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents ChkInvoiceBasedPO As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.Controls.MyTextBox
    Friend WithEvents chksmsatpost As common.Controls.MyCheckBox
    Friend WithEvents chkShowInvoiceInPOfinder As common.Controls.MyCheckBox
    Friend WithEvents txtJobWork As common.UserControls.txtFinder
    Friend WithEvents LblJobWork As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents chkRFQ As common.Controls.MyCheckBox
    Friend WithEvents chkPOInvCreateDebitNoteForRejectAndShort As common.Controls.MyCheckBox
    Friend WithEvents chkEnableProjectFinder As common.Controls.MyCheckBox
    Friend WithEvents ChkRateEditSRN As common.Controls.MyCheckBox
    Friend WithEvents chkPOInvCreateDebitNoteForReject As common.Controls.MyCheckBox
    Friend WithEvents TxtSRNLim As common.MyNumBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents chkRemarkReasononPO As common.Controls.MyCheckBox
    Friend WithEvents chkQCColumnAddedonMRN As common.Controls.MyCheckBox
    Friend WithEvents ChkCostEditIssue As common.Controls.MyCheckBox
    Friend WithEvents ChkGLAccToItem As common.Controls.MyCheckBox
    Friend WithEvents chkSRNPrintQtywise As common.Controls.MyCheckBox
    Friend WithEvents ChkSkipMRNGRNinCaseofMT As common.Controls.MyCheckBox
    Friend WithEvents chkShortageIncludeInLandedCost As common.Controls.MyCheckBox
    Friend WithEvents TxtGRNLim As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ChkShowCostCenterAndHierarchyLevelInPurchaseModule As common.Controls.MyCheckBox
    Friend WithEvents txtPoLimit As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkPI_debitnot_unitcost As common.Controls.MyCheckBox
    Friend WithEvents txtSlab1From As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSlab3To As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtSlab2To As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtSlab3From As common.MyNumBox
    Friend WithEvents txtSlab1To As common.MyNumBox
    Friend WithEvents txtSlab2From As common.MyNumBox
    Friend WithEvents chkApplySlab As common.Controls.MyCheckBox
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents ChkAutoGenerateMRN As common.Controls.MyCheckBox
    Friend WithEvents chkAllstructurewiseItem As common.Controls.MyCheckBox
End Class

