<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventorySetting
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim ConditionalFormattingObject1 As Telerik.WinControls.UI.ConditionalFormattingObject = New Telerik.WinControls.UI.ConditionalFormattingObject()
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Me.chkallownegativeinventory = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkallowreceipts = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.groupbox = New Telerik.WinControls.UI.RadGroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.gv_itemsettings = New common.UserControls.MyRadGridView()
        Me.chkCreateTransferFromBooking = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkGPAfterTransfer = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndProd_FatSnf_Base_Unit = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.chkBomProductionProcess = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCSACommision_Inv = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsMRPWiseBalance = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowNegativeStock = New common.Controls.MyCheckBox()
        Me.txtNegativeStock = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtProdQty_Decimal = New common.MyNumBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gvLocation = New common.UserControls.MyRadGridView()
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry = New common.Controls.MyCheckBox()
        Me.chkthirdparty = New Telerik.WinControls.UI.RadCheckBox()
        Me.gbDocSeriesSetting = New System.Windows.Forms.GroupBox()
        Me.chkLocal_InterStateTransfer = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.cbgLocation = New common.UserControls.MyRadGridView()
        Me.chkTransferWithProductionSaleSeries = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndVehicle_Unit = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.chkTransferJEForLocationMapping = New common.Controls.MyCheckBox()
        Me.chkItemWithDifferntUnitConsiderAsOtherItem = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txttradingGoods = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtOther = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtAsset = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtFinishedGoods = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSemiFinishGoods = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRawMaterial = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.ChkAllowtoeditCategorycodeinitemmaster = New common.Controls.MyCheckBox()
        Me.chkPrncpl_BOM = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkNlevel_Location = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkalwPGMCusMst = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowTermsEditSales = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowTermEditPurchase = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowTermsEditMM = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkBatchMandatory = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowchangeInvoiceType = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnStockAvailable = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbntBalanceOnDocDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnIsConsiderOutTypeDoc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkAutoScheme = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkMRPwithAbatement = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkBackCalculation = New Telerik.WinControls.UI.RadCheckBox()
        Me.chknLevelCategory = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAllowcostZero = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAutoCreateSRNMRNOnPOPost = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsEnterQtyOnSRN = New Telerik.WinControls.UI.RadCheckBox()
        Me.dgvclasss = New common.UserControls.MyRadGridView()
        Me.chkauto_item_nlevel = New Telerik.WinControls.UI.RadCheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.grpItemType = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItemType = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.chkNegativeStockInDairyProduction = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.chkallownegativeinventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallowreceipts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupbox.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.gv_itemsettings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_itemsettings.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateTransferFromBooking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGPAfterTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBomProductionProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCSACommision_Inv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsMRPWiseBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowNegativeStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNegativeStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProdQty_Decimal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowtoshowMilkTypeonAdjustmentEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkthirdparty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDocSeriesSetting.SuspendLayout()
        CType(Me.chkLocal_InterStateTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransferWithProductionSaleSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransferJEForLocationMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWithDifferntUnitConsiderAsOtherItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txttradingGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFinishedGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSemiFinishGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRawMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllowtoeditCategorycodeinitemmaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPrncpl_BOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNlevel_Location, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkalwPGMCusMst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowTermsEditSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowTermEditPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowTermsEditMM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBatchMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowchangeInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnStockAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbntBalanceOnDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnIsConsiderOutTypeDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMRPwithAbatement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chknLevelCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowcostZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoCreateSRNMRNOnPOPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsEnterQtyOnSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvclasss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvclasss.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkauto_item_nlevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.grpItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpItemType.SuspendLayout()
        CType(Me.gvItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemType.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNegativeStockInDairyProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkallownegativeinventory
        '
        Me.chkallownegativeinventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkallownegativeinventory.Location = New System.Drawing.Point(814, 89)
        Me.chkallownegativeinventory.Name = "chkallownegativeinventory"
        Me.chkallownegativeinventory.Size = New System.Drawing.Size(181, 16)
        Me.chkallownegativeinventory.TabIndex = 6
        Me.chkallownegativeinventory.Text = "Allow Negative Inventory Levels"
        Me.chkallownegativeinventory.Visible = False
        '
        'chkallowreceipts
        '
        Me.chkallowreceipts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkallowreceipts.Location = New System.Drawing.Point(814, 72)
        Me.chkallowreceipts.Name = "chkallowreceipts"
        Me.chkallowreceipts.Size = New System.Drawing.Size(194, 16)
        Me.chkallowreceipts.TabIndex = 3
        Me.chkallowreceipts.Text = "Allow Receipts of Non-Stock Items"
        Me.chkallowreceipts.Visible = False
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1015, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(60, 21)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnimport, Me.mnexport, Me.mnclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'mnimport
        '
        Me.mnimport.AccessibleDescription = "Import"
        Me.mnimport.AccessibleName = "Import"
        Me.mnimport.Name = "mnimport"
        Me.mnimport.Text = "Import"
        '
        'mnexport
        '
        Me.mnexport.AccessibleDescription = "Export"
        Me.mnexport.AccessibleName = "Export"
        Me.mnexport.Name = "mnexport"
        Me.mnexport.Text = "Export"
        '
        'mnclose
        '
        Me.mnclose.AccessibleDescription = "Close"
        Me.mnclose.AccessibleName = "Close"
        Me.mnclose.Name = "mnclose"
        Me.mnclose.Text = "Close"
        '
        'groupbox
        '
        Me.groupbox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.groupbox.Controls.Add(Me.GroupBox1)
        Me.groupbox.Controls.Add(Me.chkNegativeStockInDairyProduction)
        Me.groupbox.Controls.Add(Me.GroupBox3)
        Me.groupbox.Controls.Add(Me.chkCreateTransferFromBooking)
        Me.groupbox.Controls.Add(Me.chkGPAfterTransfer)
        Me.groupbox.Controls.Add(Me.fndProd_FatSnf_Base_Unit)
        Me.groupbox.Controls.Add(Me.chkBomProductionProcess)
        Me.groupbox.Controls.Add(Me.chkCSACommision_Inv)
        Me.groupbox.Controls.Add(Me.chkIsMRPWiseBalance)
        Me.groupbox.Controls.Add(Me.MyLabel10)
        Me.groupbox.Controls.Add(Me.chkAllowNegativeStock)
        Me.groupbox.Controls.Add(Me.txtNegativeStock)
        Me.groupbox.Controls.Add(Me.txtProdQty_Decimal)
        Me.groupbox.Controls.Add(Me.MyLabel7)
        Me.groupbox.Controls.Add(Me.GroupBox4)
        Me.groupbox.Controls.Add(Me.chkAllowtoshowMilkTypeonAdjustmentEntry)
        Me.groupbox.Controls.Add(Me.chkthirdparty)
        Me.groupbox.Controls.Add(Me.gbDocSeriesSetting)
        Me.groupbox.Controls.Add(Me.chkTransferWithProductionSaleSeries)
        Me.groupbox.Controls.Add(Me.fndVehicle_Unit)
        Me.groupbox.Controls.Add(Me.MyLabel6)
        Me.groupbox.Controls.Add(Me.chkTransferJEForLocationMapping)
        Me.groupbox.Controls.Add(Me.chkItemWithDifferntUnitConsiderAsOtherItem)
        Me.groupbox.Controls.Add(Me.GroupBox2)
        Me.groupbox.Controls.Add(Me.ChkAllowtoeditCategorycodeinitemmaster)
        Me.groupbox.Controls.Add(Me.chkPrncpl_BOM)
        Me.groupbox.Controls.Add(Me.chkNlevel_Location)
        Me.groupbox.Controls.Add(Me.chkalwPGMCusMst)
        Me.groupbox.Controls.Add(Me.chkAllowTermsEditSales)
        Me.groupbox.Controls.Add(Me.chkAllowTermEditPurchase)
        Me.groupbox.Controls.Add(Me.chkAllowTermsEditMM)
        Me.groupbox.Controls.Add(Me.chkBatchMandatory)
        Me.groupbox.Controls.Add(Me.chkAllowchangeInvoiceType)
        Me.groupbox.Controls.Add(Me.chkallownegativeinventory)
        Me.groupbox.Controls.Add(Me.chkAutoScheme)
        Me.groupbox.Controls.Add(Me.chkMRPwithAbatement)
        Me.groupbox.Controls.Add(Me.chkBackCalculation)
        Me.groupbox.Controls.Add(Me.chknLevelCategory)
        Me.groupbox.Controls.Add(Me.chkAllowcostZero)
        Me.groupbox.Controls.Add(Me.chkAutoCreateSRNMRNOnPOPost)
        Me.groupbox.Controls.Add(Me.chkIsEnterQtyOnSRN)
        Me.groupbox.Controls.Add(Me.dgvclasss)
        Me.groupbox.Controls.Add(Me.chkallowreceipts)
        Me.groupbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupbox.HeaderText = ""
        Me.groupbox.Location = New System.Drawing.Point(0, 0)
        Me.groupbox.Name = "groupbox"
        Me.groupbox.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.groupbox.Size = New System.Drawing.Size(1057, 471)
        Me.groupbox.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.gv_itemsettings)
        Me.GroupBox3.Location = New System.Drawing.Point(558, 371)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 143)
        Me.GroupBox3.TabIndex = 48
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Item Settings"
        '
        'gv_itemsettings
        '
        Me.gv_itemsettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_itemsettings.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_itemsettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_itemsettings.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv_itemsettings.ForeColor = System.Drawing.Color.Black
        Me.gv_itemsettings.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_itemsettings.Location = New System.Drawing.Point(3, 18)
        '
        'gv_itemsettings
        '
        Me.gv_itemsettings.MasterTemplate.AllowAddNewRow = False
        Me.gv_itemsettings.MasterTemplate.AllowDeleteRow = False
        Me.gv_itemsettings.MasterTemplate.ShowFilteringRow = False
        Me.gv_itemsettings.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_itemsettings.Name = "gv_itemsettings"
        Me.gv_itemsettings.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_itemsettings.ShowGroupPanel = False
        Me.gv_itemsettings.ShowHeaderCellButtons = True
        Me.gv_itemsettings.Size = New System.Drawing.Size(216, 122)
        Me.gv_itemsettings.TabIndex = 1
        Me.gv_itemsettings.TabStop = False
        Me.gv_itemsettings.Text = "RadGridView1"
        '
        'chkCreateTransferFromBooking
        '
        Me.chkCreateTransferFromBooking.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateTransferFromBooking.Location = New System.Drawing.Point(9, 267)
        Me.chkCreateTransferFromBooking.Name = "chkCreateTransferFromBooking"
        Me.chkCreateTransferFromBooking.Size = New System.Drawing.Size(178, 16)
        Me.chkCreateTransferFromBooking.TabIndex = 47
        Me.chkCreateTransferFromBooking.Text = "Create Transfer from GatePass"
        '
        'chkGPAfterTransfer
        '
        Me.chkGPAfterTransfer.Enabled = False
        Me.chkGPAfterTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGPAfterTransfer.Location = New System.Drawing.Point(9, 247)
        Me.chkGPAfterTransfer.Name = "chkGPAfterTransfer"
        Me.chkGPAfterTransfer.Size = New System.Drawing.Size(145, 16)
        Me.chkGPAfterTransfer.TabIndex = 46
        Me.chkGPAfterTransfer.Text = "Gate Pass After Transfer"
        '
        'fndProd_FatSnf_Base_Unit
        '
        Me.fndProd_FatSnf_Base_Unit.CalculationExpression = Nothing
        Me.fndProd_FatSnf_Base_Unit.FieldCode = Nothing
        Me.fndProd_FatSnf_Base_Unit.FieldDesc = Nothing
        Me.fndProd_FatSnf_Base_Unit.FieldMaxLength = 0
        Me.fndProd_FatSnf_Base_Unit.FieldName = Nothing
        Me.fndProd_FatSnf_Base_Unit.isCalculatedField = False
        Me.fndProd_FatSnf_Base_Unit.IsSourceFromTable = False
        Me.fndProd_FatSnf_Base_Unit.IsSourceFromValueList = False
        Me.fndProd_FatSnf_Base_Unit.IsUnique = False
        Me.fndProd_FatSnf_Base_Unit.Location = New System.Drawing.Point(952, 473)
        Me.fndProd_FatSnf_Base_Unit.MendatroryField = False
        Me.fndProd_FatSnf_Base_Unit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProd_FatSnf_Base_Unit.MyLinkLable1 = Me.MyLabel10
        Me.fndProd_FatSnf_Base_Unit.MyLinkLable2 = Nothing
        Me.fndProd_FatSnf_Base_Unit.MyReadOnly = False
        Me.fndProd_FatSnf_Base_Unit.MyShowMasterFormButton = False
        Me.fndProd_FatSnf_Base_Unit.Name = "fndProd_FatSnf_Base_Unit"
        Me.fndProd_FatSnf_Base_Unit.ReferenceFieldDesc = Nothing
        Me.fndProd_FatSnf_Base_Unit.ReferenceFieldName = Nothing
        Me.fndProd_FatSnf_Base_Unit.ReferenceTableName = Nothing
        Me.fndProd_FatSnf_Base_Unit.Size = New System.Drawing.Size(72, 19)
        Me.fndProd_FatSnf_Base_Unit.TabIndex = 45
        Me.fndProd_FatSnf_Base_Unit.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(786, 474)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(156, 18)
        Me.MyLabel10.TabIndex = 44
        Me.MyLabel10.Text = "Production FAT/SNF base unit"
        '
        'chkBomProductionProcess
        '
        Me.chkBomProductionProcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBomProductionProcess.Location = New System.Drawing.Point(474, 278)
        Me.chkBomProductionProcess.Name = "chkBomProductionProcess"
        Me.chkBomProductionProcess.Size = New System.Drawing.Size(188, 16)
        Me.chkBomProductionProcess.TabIndex = 23
        Me.chkBomProductionProcess.Text = "Allow Process Production In ERP"
        '
        'chkCSACommision_Inv
        '
        Me.chkCSACommision_Inv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSACommision_Inv.Location = New System.Drawing.Point(474, 297)
        Me.chkCSACommision_Inv.Name = "chkCSACommision_Inv"
        Me.chkCSACommision_Inv.Size = New System.Drawing.Size(218, 16)
        Me.chkCSACommision_Inv.TabIndex = 26
        Me.chkCSACommision_Inv.Text = "Create AP Invoice For CSA Commision"
        '
        'chkIsMRPWiseBalance
        '
        Me.chkIsMRPWiseBalance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsMRPWiseBalance.Location = New System.Drawing.Point(474, 314)
        Me.chkIsMRPWiseBalance.Name = "chkIsMRPWiseBalance"
        Me.chkIsMRPWiseBalance.Size = New System.Drawing.Size(130, 16)
        Me.chkIsMRPWiseBalance.TabIndex = 34
        Me.chkIsMRPWiseBalance.Text = "Is MRP Wise Balance"
        '
        'chkAllowNegativeStock
        '
        Me.chkAllowNegativeStock.Location = New System.Drawing.Point(785, 275)
        Me.chkAllowNegativeStock.MyLinkLable1 = Nothing
        Me.chkAllowNegativeStock.MyLinkLable2 = Nothing
        Me.chkAllowNegativeStock.Name = "chkAllowNegativeStock"
        Me.chkAllowNegativeStock.Size = New System.Drawing.Size(126, 18)
        Me.chkAllowNegativeStock.TabIndex = 43
        Me.chkAllowNegativeStock.Tag1 = Nothing
        Me.chkAllowNegativeStock.Text = "Allow -ve stock || Qty"
        '
        'txtNegativeStock
        '
        Me.txtNegativeStock.CalculationExpression = Nothing
        Me.txtNegativeStock.DecimalPlaces = 0
        Me.txtNegativeStock.FieldCode = Nothing
        Me.txtNegativeStock.FieldDesc = Nothing
        Me.txtNegativeStock.FieldMaxLength = 0
        Me.txtNegativeStock.FieldName = Nothing
        Me.txtNegativeStock.isCalculatedField = False
        Me.txtNegativeStock.IsSourceFromTable = False
        Me.txtNegativeStock.IsSourceFromValueList = False
        Me.txtNegativeStock.IsUnique = False
        Me.txtNegativeStock.Location = New System.Drawing.Point(913, 274)
        Me.txtNegativeStock.MendatroryField = False
        Me.txtNegativeStock.MyLinkLable1 = Me.MyLabel7
        Me.txtNegativeStock.MyLinkLable2 = Nothing
        Me.txtNegativeStock.Name = "txtNegativeStock"
        Me.txtNegativeStock.ReferenceFieldDesc = Nothing
        Me.txtNegativeStock.ReferenceFieldName = Nothing
        Me.txtNegativeStock.ReferenceTableName = Nothing
        Me.txtNegativeStock.Size = New System.Drawing.Size(130, 20)
        Me.txtNegativeStock.TabIndex = 42
        Me.txtNegativeStock.Text = "0"
        Me.txtNegativeStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNegativeStock.Value = 0.0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(783, 254)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(184, 18)
        Me.MyLabel7.TabIndex = 40
        Me.MyLabel7.Text = "Decimal point for Qty in Production"
        '
        'txtProdQty_Decimal
        '
        Me.txtProdQty_Decimal.CalculationExpression = Nothing
        Me.txtProdQty_Decimal.DecimalPlaces = 0
        Me.txtProdQty_Decimal.FieldCode = Nothing
        Me.txtProdQty_Decimal.FieldDesc = Nothing
        Me.txtProdQty_Decimal.FieldMaxLength = 0
        Me.txtProdQty_Decimal.FieldName = Nothing
        Me.txtProdQty_Decimal.isCalculatedField = False
        Me.txtProdQty_Decimal.IsSourceFromTable = False
        Me.txtProdQty_Decimal.IsSourceFromValueList = False
        Me.txtProdQty_Decimal.IsUnique = False
        Me.txtProdQty_Decimal.Location = New System.Drawing.Point(973, 253)
        Me.txtProdQty_Decimal.MendatroryField = False
        Me.txtProdQty_Decimal.MyLinkLable1 = Me.MyLabel7
        Me.txtProdQty_Decimal.MyLinkLable2 = Nothing
        Me.txtProdQty_Decimal.Name = "txtProdQty_Decimal"
        Me.txtProdQty_Decimal.ReferenceFieldDesc = Nothing
        Me.txtProdQty_Decimal.ReferenceFieldName = Nothing
        Me.txtProdQty_Decimal.ReferenceTableName = Nothing
        Me.txtProdQty_Decimal.Size = New System.Drawing.Size(67, 20)
        Me.txtProdQty_Decimal.TabIndex = 41
        Me.txtProdQty_Decimal.Text = "0"
        Me.txtProdQty_Decimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtProdQty_Decimal.Value = 0.0R
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.gvLocation)
        Me.GroupBox4.Location = New System.Drawing.Point(783, 300)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(260, 168)
        Me.GroupBox4.TabIndex = 39
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "CSA Transfer(Non Excise) Series Setting [if checked then Stock Transfer Else CSA " & _
    "Transfer]"
        '
        'gvLocation
        '
        Me.gvLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvLocation.ForeColor = System.Drawing.Color.Black
        Me.gvLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLocation.Location = New System.Drawing.Point(3, 18)
        '
        'gvLocation
        '
        Me.gvLocation.MasterTemplate.AllowAddNewRow = False
        Me.gvLocation.MasterTemplate.AllowDeleteRow = False
        Me.gvLocation.MasterTemplate.ShowFilteringRow = False
        Me.gvLocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLocation.Name = "gvLocation"
        Me.gvLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLocation.ShowGroupPanel = False
        Me.gvLocation.ShowHeaderCellButtons = True
        Me.gvLocation.Size = New System.Drawing.Size(254, 147)
        Me.gvLocation.TabIndex = 1
        Me.gvLocation.TabStop = False
        Me.gvLocation.Text = "RadGridView1"
        '
        'chkAllowtoshowMilkTypeonAdjustmentEntry
        '
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.Location = New System.Drawing.Point(474, 260)
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.MyLinkLable1 = Nothing
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.MyLinkLable2 = Nothing
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.Name = "chkAllowtoshowMilkTypeonAdjustmentEntry"
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.Size = New System.Drawing.Size(248, 18)
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.TabIndex = 25
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.Tag1 = Nothing
        Me.chkAllowtoshowMilkTypeonAdjustmentEntry.Text = "Allow to show Milk Type on Adjustment Entry"
        '
        'chkthirdparty
        '
        Me.chkthirdparty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkthirdparty.Location = New System.Drawing.Point(814, 141)
        Me.chkthirdparty.Name = "chkthirdparty"
        Me.chkthirdparty.Size = New System.Drawing.Size(196, 16)
        Me.chkthirdparty.TabIndex = 19
        Me.chkthirdparty.Text = "Allow Third Party Location On ERP"
        '
        'gbDocSeriesSetting
        '
        Me.gbDocSeriesSetting.Controls.Add(Me.chkLocal_InterStateTransfer)
        Me.gbDocSeriesSetting.Controls.Add(Me.MyLabel9)
        Me.gbDocSeriesSetting.Controls.Add(Me.MyLabel8)
        Me.gbDocSeriesSetting.Controls.Add(Me.cbgLocation)
        Me.gbDocSeriesSetting.Location = New System.Drawing.Point(5, 300)
        Me.gbDocSeriesSetting.Name = "gbDocSeriesSetting"
        Me.gbDocSeriesSetting.Size = New System.Drawing.Size(547, 213)
        Me.gbDocSeriesSetting.TabIndex = 10
        Me.gbDocSeriesSetting.TabStop = False
        Me.gbDocSeriesSetting.Text = "Document Series Setting [Product Sale / Stock Transfer / CSA Transfer]"
        '
        'chkLocal_InterStateTransfer
        '
        Me.chkLocal_InterStateTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocal_InterStateTransfer.Location = New System.Drawing.Point(11, 49)
        Me.chkLocal_InterStateTransfer.Name = "chkLocal_InterStateTransfer"
        Me.chkLocal_InterStateTransfer.Size = New System.Drawing.Size(522, 16)
        Me.chkLocal_InterStateTransfer.TabIndex = 36
        Me.chkLocal_InterStateTransfer.Text = "different [Stock/CSA Transfer] series for Local/InterState, This will not be effe" & _
    "ctive if first check is On"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(8, 32)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(312, 18)
        Me.MyLabel9.TabIndex = 35
        Me.MyLabel9.Text = "Select1 : Series of CSA Transfer will be same as Stock Transfer"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(8, 15)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(423, 18)
        Me.MyLabel8.TabIndex = 34
        Me.MyLabel8.Text = "Select : Series of Stock Transfer and CSA Transfer will be same as Sale Invoice R" & _
    "etail."
        '
        'cbgLocation
        '
        Me.cbgLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.cbgLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbgLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.cbgLocation.ForeColor = System.Drawing.Color.Black
        Me.cbgLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbgLocation.Location = New System.Drawing.Point(3, 71)
        '
        'cbgLocation
        '
        Me.cbgLocation.MasterTemplate.AllowAddNewRow = False
        Me.cbgLocation.MasterTemplate.AllowDeleteRow = False
        Me.cbgLocation.MasterTemplate.ShowFilteringRow = False
        Me.cbgLocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbgLocation.ShowGroupPanel = False
        Me.cbgLocation.ShowHeaderCellButtons = True
        Me.cbgLocation.Size = New System.Drawing.Size(644, 140)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.TabStop = False
        Me.cbgLocation.Text = "RadGridView1"
        '
        'chkTransferWithProductionSaleSeries
        '
        Me.chkTransferWithProductionSaleSeries.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransferWithProductionSaleSeries.Location = New System.Drawing.Point(474, 206)
        Me.chkTransferWithProductionSaleSeries.Name = "chkTransferWithProductionSaleSeries"
        Me.chkTransferWithProductionSaleSeries.Size = New System.Drawing.Size(283, 16)
        Me.chkTransferWithProductionSaleSeries.TabIndex = 38
        Me.chkTransferWithProductionSaleSeries.Text = "Create Transfer With Production Sale (Retail) Series"
        '
        'fndVehicle_Unit
        '
        Me.fndVehicle_Unit.CalculationExpression = Nothing
        Me.fndVehicle_Unit.FieldCode = Nothing
        Me.fndVehicle_Unit.FieldDesc = Nothing
        Me.fndVehicle_Unit.FieldMaxLength = 0
        Me.fndVehicle_Unit.FieldName = Nothing
        Me.fndVehicle_Unit.isCalculatedField = False
        Me.fndVehicle_Unit.IsSourceFromTable = False
        Me.fndVehicle_Unit.IsSourceFromValueList = False
        Me.fndVehicle_Unit.IsUnique = False
        Me.fndVehicle_Unit.Location = New System.Drawing.Point(664, 184)
        Me.fndVehicle_Unit.MendatroryField = False
        Me.fndVehicle_Unit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVehicle_Unit.MyLinkLable1 = Me.MyLabel6
        Me.fndVehicle_Unit.MyLinkLable2 = Nothing
        Me.fndVehicle_Unit.MyReadOnly = False
        Me.fndVehicle_Unit.MyShowMasterFormButton = False
        Me.fndVehicle_Unit.Name = "fndVehicle_Unit"
        Me.fndVehicle_Unit.ReferenceFieldDesc = Nothing
        Me.fndVehicle_Unit.ReferenceFieldName = Nothing
        Me.fndVehicle_Unit.ReferenceTableName = Nothing
        Me.fndVehicle_Unit.Size = New System.Drawing.Size(72, 19)
        Me.fndVehicle_Unit.TabIndex = 37
        Me.fndVehicle_Unit.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(498, 185)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(160, 18)
        Me.MyLabel6.TabIndex = 36
        Me.MyLabel6.Text = "Vehicle Capacity Provision Unit"
        '
        'chkTransferJEForLocationMapping
        '
        Me.chkTransferJEForLocationMapping.Location = New System.Drawing.Point(474, 222)
        Me.chkTransferJEForLocationMapping.MyLinkLable1 = Nothing
        Me.chkTransferJEForLocationMapping.MyLinkLable2 = Nothing
        Me.chkTransferJEForLocationMapping.Name = "chkTransferJEForLocationMapping"
        Me.chkTransferJEForLocationMapping.Size = New System.Drawing.Size(237, 18)
        Me.chkTransferJEForLocationMapping.TabIndex = 35
        Me.chkTransferJEForLocationMapping.Tag1 = Nothing
        Me.chkTransferJEForLocationMapping.Text = "Transfer Journal Entry by Location mapping"
        '
        'chkItemWithDifferntUnitConsiderAsOtherItem
        '
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.Location = New System.Drawing.Point(474, 244)
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.Name = "chkItemWithDifferntUnitConsiderAsOtherItem"
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.Size = New System.Drawing.Size(251, 16)
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.TabIndex = 33
        Me.chkItemWithDifferntUnitConsiderAsOtherItem.Text = "Item With Differnt Unit Consider as Other Item"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txttradingGoods)
        Me.GroupBox2.Controls.Add(Me.MyLabel11)
        Me.GroupBox2.Controls.Add(Me.txtOther)
        Me.GroupBox2.Controls.Add(Me.MyLabel5)
        Me.GroupBox2.Controls.Add(Me.txtAsset)
        Me.GroupBox2.Controls.Add(Me.MyLabel3)
        Me.GroupBox2.Controls.Add(Me.txtFinishedGoods)
        Me.GroupBox2.Controls.Add(Me.MyLabel2)
        Me.GroupBox2.Controls.Add(Me.txtSemiFinishGoods)
        Me.GroupBox2.Controls.Add(Me.MyLabel1)
        Me.GroupBox2.Controls.Add(Me.txtRawMaterial)
        Me.GroupBox2.Controls.Add(Me.MyLabel4)
        Me.GroupBox2.Location = New System.Drawing.Point(582, 30)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(233, 144)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Item Master Couter"
        Me.GroupBox2.Visible = False
        '
        'txttradingGoods
        '
        Me.txttradingGoods.CalculationExpression = Nothing
        Me.txttradingGoods.FieldCode = Nothing
        Me.txttradingGoods.FieldDesc = Nothing
        Me.txttradingGoods.FieldMaxLength = 0
        Me.txttradingGoods.FieldName = Nothing
        Me.txttradingGoods.isCalculatedField = False
        Me.txttradingGoods.IsSourceFromTable = False
        Me.txttradingGoods.IsSourceFromValueList = False
        Me.txttradingGoods.IsUnique = False
        Me.txttradingGoods.Location = New System.Drawing.Point(115, 118)
        Me.txttradingGoods.MaxLength = 30
        Me.txttradingGoods.MendatroryField = False
        Me.txttradingGoods.MyLinkLable1 = Nothing
        Me.txttradingGoods.MyLinkLable2 = Nothing
        Me.txttradingGoods.Name = "txttradingGoods"
        Me.txttradingGoods.ReferenceFieldDesc = Nothing
        Me.txttradingGoods.ReferenceFieldName = Nothing
        Me.txttradingGoods.ReferenceTableName = Nothing
        Me.txttradingGoods.Size = New System.Drawing.Size(112, 20)
        Me.txttradingGoods.TabIndex = 36
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(3, 119)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel11.TabIndex = 35
        Me.MyLabel11.Text = "Trading Goods"
        '
        'txtOther
        '
        Me.txtOther.CalculationExpression = Nothing
        Me.txtOther.FieldCode = Nothing
        Me.txtOther.FieldDesc = Nothing
        Me.txtOther.FieldMaxLength = 0
        Me.txtOther.FieldName = Nothing
        Me.txtOther.isCalculatedField = False
        Me.txtOther.IsSourceFromTable = False
        Me.txtOther.IsSourceFromValueList = False
        Me.txtOther.IsUnique = False
        Me.txtOther.Location = New System.Drawing.Point(115, 96)
        Me.txtOther.MaxLength = 1
        Me.txtOther.MendatroryField = False
        Me.txtOther.MyLinkLable1 = Nothing
        Me.txtOther.MyLinkLable2 = Nothing
        Me.txtOther.Name = "txtOther"
        Me.txtOther.ReferenceFieldDesc = Nothing
        Me.txtOther.ReferenceFieldName = Nothing
        Me.txtOther.ReferenceTableName = Nothing
        Me.txtOther.Size = New System.Drawing.Size(112, 20)
        Me.txtOther.TabIndex = 34
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(3, 97)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(35, 18)
        Me.MyLabel5.TabIndex = 33
        Me.MyLabel5.Text = "Other"
        '
        'txtAsset
        '
        Me.txtAsset.CalculationExpression = Nothing
        Me.txtAsset.FieldCode = Nothing
        Me.txtAsset.FieldDesc = Nothing
        Me.txtAsset.FieldMaxLength = 0
        Me.txtAsset.FieldName = Nothing
        Me.txtAsset.isCalculatedField = False
        Me.txtAsset.IsSourceFromTable = False
        Me.txtAsset.IsSourceFromValueList = False
        Me.txtAsset.IsUnique = False
        Me.txtAsset.Location = New System.Drawing.Point(115, 74)
        Me.txtAsset.MaxLength = 1
        Me.txtAsset.MendatroryField = False
        Me.txtAsset.MyLinkLable1 = Nothing
        Me.txtAsset.MyLinkLable2 = Nothing
        Me.txtAsset.Name = "txtAsset"
        Me.txtAsset.ReferenceFieldDesc = Nothing
        Me.txtAsset.ReferenceFieldName = Nothing
        Me.txtAsset.ReferenceTableName = Nothing
        Me.txtAsset.Size = New System.Drawing.Size(112, 20)
        Me.txtAsset.TabIndex = 34
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(5, 75)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel3.TabIndex = 33
        Me.MyLabel3.Text = "Asset"
        '
        'txtFinishedGoods
        '
        Me.txtFinishedGoods.CalculationExpression = Nothing
        Me.txtFinishedGoods.FieldCode = Nothing
        Me.txtFinishedGoods.FieldDesc = Nothing
        Me.txtFinishedGoods.FieldMaxLength = 0
        Me.txtFinishedGoods.FieldName = Nothing
        Me.txtFinishedGoods.isCalculatedField = False
        Me.txtFinishedGoods.IsSourceFromTable = False
        Me.txtFinishedGoods.IsSourceFromValueList = False
        Me.txtFinishedGoods.IsUnique = False
        Me.txtFinishedGoods.Location = New System.Drawing.Point(115, 32)
        Me.txtFinishedGoods.MaxLength = 1
        Me.txtFinishedGoods.MendatroryField = False
        Me.txtFinishedGoods.MyLinkLable1 = Nothing
        Me.txtFinishedGoods.MyLinkLable2 = Nothing
        Me.txtFinishedGoods.Name = "txtFinishedGoods"
        Me.txtFinishedGoods.ReferenceFieldDesc = Nothing
        Me.txtFinishedGoods.ReferenceFieldName = Nothing
        Me.txtFinishedGoods.ReferenceTableName = Nothing
        Me.txtFinishedGoods.Size = New System.Drawing.Size(112, 20)
        Me.txtFinishedGoods.TabIndex = 34
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(5, 33)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel2.TabIndex = 33
        Me.MyLabel2.Text = "Finished Good"
        '
        'txtSemiFinishGoods
        '
        Me.txtSemiFinishGoods.CalculationExpression = Nothing
        Me.txtSemiFinishGoods.FieldCode = Nothing
        Me.txtSemiFinishGoods.FieldDesc = Nothing
        Me.txtSemiFinishGoods.FieldMaxLength = 0
        Me.txtSemiFinishGoods.FieldName = Nothing
        Me.txtSemiFinishGoods.isCalculatedField = False
        Me.txtSemiFinishGoods.IsSourceFromTable = False
        Me.txtSemiFinishGoods.IsSourceFromValueList = False
        Me.txtSemiFinishGoods.IsUnique = False
        Me.txtSemiFinishGoods.Location = New System.Drawing.Point(115, 53)
        Me.txtSemiFinishGoods.MaxLength = 1
        Me.txtSemiFinishGoods.MendatroryField = False
        Me.txtSemiFinishGoods.MyLinkLable1 = Nothing
        Me.txtSemiFinishGoods.MyLinkLable2 = Nothing
        Me.txtSemiFinishGoods.Name = "txtSemiFinishGoods"
        Me.txtSemiFinishGoods.ReferenceFieldDesc = Nothing
        Me.txtSemiFinishGoods.ReferenceFieldName = Nothing
        Me.txtSemiFinishGoods.ReferenceTableName = Nothing
        Me.txtSemiFinishGoods.Size = New System.Drawing.Size(112, 20)
        Me.txtSemiFinishGoods.TabIndex = 34
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(5, 54)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(106, 18)
        Me.MyLabel1.TabIndex = 33
        Me.MyLabel1.Text = "Semi Finished Good"
        '
        'txtRawMaterial
        '
        Me.txtRawMaterial.CalculationExpression = Nothing
        Me.txtRawMaterial.FieldCode = Nothing
        Me.txtRawMaterial.FieldDesc = Nothing
        Me.txtRawMaterial.FieldMaxLength = 0
        Me.txtRawMaterial.FieldName = Nothing
        Me.txtRawMaterial.isCalculatedField = False
        Me.txtRawMaterial.IsSourceFromTable = False
        Me.txtRawMaterial.IsSourceFromValueList = False
        Me.txtRawMaterial.IsUnique = False
        Me.txtRawMaterial.Location = New System.Drawing.Point(115, 12)
        Me.txtRawMaterial.MaxLength = 1
        Me.txtRawMaterial.MendatroryField = False
        Me.txtRawMaterial.MyLinkLable1 = Nothing
        Me.txtRawMaterial.MyLinkLable2 = Nothing
        Me.txtRawMaterial.Name = "txtRawMaterial"
        Me.txtRawMaterial.ReferenceFieldDesc = Nothing
        Me.txtRawMaterial.ReferenceFieldName = Nothing
        Me.txtRawMaterial.ReferenceTableName = Nothing
        Me.txtRawMaterial.Size = New System.Drawing.Size(112, 20)
        Me.txtRawMaterial.TabIndex = 32
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(5, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel4.TabIndex = 31
        Me.MyLabel4.Text = "Raw Material"
        '
        'ChkAllowtoeditCategorycodeinitemmaster
        '
        Me.ChkAllowtoeditCategorycodeinitemmaster.Location = New System.Drawing.Point(814, 156)
        Me.ChkAllowtoeditCategorycodeinitemmaster.MyLinkLable1 = Nothing
        Me.ChkAllowtoeditCategorycodeinitemmaster.MyLinkLable2 = Nothing
        Me.ChkAllowtoeditCategorycodeinitemmaster.Name = "ChkAllowtoeditCategorycodeinitemmaster"
        Me.ChkAllowtoeditCategorycodeinitemmaster.Size = New System.Drawing.Size(226, 18)
        Me.ChkAllowtoeditCategorycodeinitemmaster.TabIndex = 31
        Me.ChkAllowtoeditCategorycodeinitemmaster.Tag1 = Nothing
        Me.ChkAllowtoeditCategorycodeinitemmaster.Text = "Allo to edit Category code in item master"
        '
        'chkPrncpl_BOM
        '
        Me.chkPrncpl_BOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrncpl_BOM.Location = New System.Drawing.Point(814, 124)
        Me.chkPrncpl_BOM.Name = "chkPrncpl_BOM"
        Me.chkPrncpl_BOM.Size = New System.Drawing.Size(181, 16)
        Me.chkPrncpl_BOM.TabIndex = 22
        Me.chkPrncpl_BOM.Text = "Allow Principle Tagging At BOM"
        '
        'chkNlevel_Location
        '
        Me.chkNlevel_Location.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNlevel_Location.Location = New System.Drawing.Point(785, 182)
        Me.chkNlevel_Location.Name = "chkNlevel_Location"
        Me.chkNlevel_Location.Size = New System.Drawing.Size(236, 16)
        Me.chkNlevel_Location.TabIndex = 21
        Me.chkNlevel_Location.Text = "Allow N-Level Category At Location Master"
        '
        'chkalwPGMCusMst
        '
        Me.chkalwPGMCusMst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkalwPGMCusMst.Location = New System.Drawing.Point(785, 217)
        Me.chkalwPGMCusMst.Name = "chkalwPGMCusMst"
        Me.chkalwPGMCusMst.Size = New System.Drawing.Size(245, 16)
        Me.chkalwPGMCusMst.TabIndex = 18
        Me.chkalwPGMCusMst.Text = "Allow Price Group Code At Customer Master"
        '
        'chkAllowTermsEditSales
        '
        Me.chkAllowTermsEditSales.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowTermsEditSales.Location = New System.Drawing.Point(814, 107)
        Me.chkAllowTermsEditSales.Name = "chkAllowTermsEditSales"
        Me.chkAllowTermsEditSales.Size = New System.Drawing.Size(173, 16)
        Me.chkAllowTermsEditSales.TabIndex = 16
        Me.chkAllowTermsEditSales.Text = "Allow Terms editable on Sales"
        '
        'chkAllowTermEditPurchase
        '
        Me.chkAllowTermEditPurchase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowTermEditPurchase.Location = New System.Drawing.Point(785, 235)
        Me.chkAllowTermEditPurchase.Name = "chkAllowTermEditPurchase"
        Me.chkAllowTermEditPurchase.Size = New System.Drawing.Size(192, 16)
        Me.chkAllowTermEditPurchase.TabIndex = 17
        Me.chkAllowTermEditPurchase.Text = "Allow Terms editable on Purchase"
        '
        'chkAllowTermsEditMM
        '
        Me.chkAllowTermsEditMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowTermsEditMM.Location = New System.Drawing.Point(785, 201)
        Me.chkAllowTermsEditMM.Name = "chkAllowTermsEditMM"
        Me.chkAllowTermsEditMM.Size = New System.Drawing.Size(254, 16)
        Me.chkAllowTermsEditMM.TabIndex = 20
        Me.chkAllowTermsEditMM.Text = "Allow Terms editable on Material Management"
        '
        'chkBatchMandatory
        '
        Me.chkBatchMandatory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBatchMandatory.Location = New System.Drawing.Point(242, 276)
        Me.chkBatchMandatory.Name = "chkBatchMandatory"
        Me.chkBatchMandatory.Size = New System.Drawing.Size(199, 16)
        Me.chkBatchMandatory.TabIndex = 15
        Me.chkBatchMandatory.Text = "Is Batch No , MFD ,EXD Mandatory"
        '
        'chkAllowchangeInvoiceType
        '
        Me.chkAllowchangeInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowchangeInvoiceType.Location = New System.Drawing.Point(9, 226)
        Me.chkAllowchangeInvoiceType.Name = "chkAllowchangeInvoiceType"
        Me.chkAllowchangeInvoiceType.Size = New System.Drawing.Size(208, 16)
        Me.chkAllowchangeInvoiceType.TabIndex = 14
        Me.chkAllowchangeInvoiceType.Text = "Allow Change Retail/tax Invoice Type"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnStockAvailable)
        Me.GroupBox1.Controls.Add(Me.rbntBalanceOnDocDate)
        Me.GroupBox1.Controls.Add(Me.rbtnIsConsiderOutTypeDoc)
        Me.GroupBox1.Location = New System.Drawing.Point(817, -7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 63)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'rbtnStockAvailable
        '
        Me.rbtnStockAvailable.Location = New System.Drawing.Point(3, 43)
        Me.rbtnStockAvailable.Name = "rbtnStockAvailable"
        Me.rbtnStockAvailable.Size = New System.Drawing.Size(96, 18)
        Me.rbtnStockAvailable.TabIndex = 2
        Me.rbtnStockAvailable.Text = "Stock Available"
        '
        'rbntBalanceOnDocDate
        '
        Me.rbntBalanceOnDocDate.Location = New System.Drawing.Point(3, 26)
        Me.rbntBalanceOnDocDate.Name = "rbntBalanceOnDocDate"
        Me.rbntBalanceOnDocDate.Size = New System.Drawing.Size(158, 18)
        Me.rbntBalanceOnDocDate.TabIndex = 1
        Me.rbntBalanceOnDocDate.Text = "Balance On Document Date"
        '
        'rbtnIsConsiderOutTypeDoc
        '
        Me.rbtnIsConsiderOutTypeDoc.Location = New System.Drawing.Point(3, 9)
        Me.rbtnIsConsiderOutTypeDoc.Name = "rbtnIsConsiderOutTypeDoc"
        Me.rbtnIsConsiderOutTypeDoc.Size = New System.Drawing.Size(208, 18)
        Me.rbtnIsConsiderOutTypeDoc.TabIndex = 0
        Me.rbtnIsConsiderOutTypeDoc.Text = "Is Consider Out Type Doc For Balance"
        '
        'chkAutoScheme
        '
        Me.chkAutoScheme.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoScheme.Location = New System.Drawing.Point(9, 205)
        Me.chkAutoScheme.Name = "chkAutoScheme"
        Me.chkAutoScheme.Size = New System.Drawing.Size(88, 16)
        Me.chkAutoScheme.TabIndex = 12
        Me.chkAutoScheme.Text = "Auto Scheme"
        '
        'chkMRPwithAbatement
        '
        Me.chkMRPwithAbatement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMRPwithAbatement.Location = New System.Drawing.Point(242, 255)
        Me.chkMRPwithAbatement.Name = "chkMRPwithAbatement"
        Me.chkMRPwithAbatement.Size = New System.Drawing.Size(127, 16)
        Me.chkMRPwithAbatement.TabIndex = 13
        Me.chkMRPwithAbatement.Text = "MRP with Abatement"
        '
        'chkBackCalculation
        '
        Me.chkBackCalculation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackCalculation.Location = New System.Drawing.Point(242, 235)
        Me.chkBackCalculation.Name = "chkBackCalculation"
        Me.chkBackCalculation.Size = New System.Drawing.Size(155, 16)
        Me.chkBackCalculation.TabIndex = 11
        Me.chkBackCalculation.Text = "Rate with Back Calculation"
        '
        'chknLevelCategory
        '
        Me.chknLevelCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chknLevelCategory.Location = New System.Drawing.Point(9, 186)
        Me.chknLevelCategory.Name = "chknLevelCategory"
        Me.chknLevelCategory.Size = New System.Drawing.Size(176, 16)
        Me.chknLevelCategory.TabIndex = 10
        Me.chknLevelCategory.Text = "Use n Level Category for Items"
        '
        'chkAllowcostZero
        '
        Me.chkAllowcostZero.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowcostZero.Location = New System.Drawing.Point(242, 218)
        Me.chkAllowcostZero.Name = "chkAllowcostZero"
        Me.chkAllowcostZero.Size = New System.Drawing.Size(229, 16)
        Me.chkAllowcostZero.TabIndex = 8
        Me.chkAllowcostZero.Text = "Allow Cost to Zero In Item Location Detail"
        Me.chkAllowcostZero.Visible = False
        '
        'chkAutoCreateSRNMRNOnPOPost
        '
        Me.chkAutoCreateSRNMRNOnPOPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoCreateSRNMRNOnPOPost.Location = New System.Drawing.Point(242, 182)
        Me.chkAutoCreateSRNMRNOnPOPost.Name = "chkAutoCreateSRNMRNOnPOPost"
        Me.chkAutoCreateSRNMRNOnPOPost.Size = New System.Drawing.Size(221, 16)
        Me.chkAutoCreateSRNMRNOnPOPost.TabIndex = 2
        Me.chkAutoCreateSRNMRNOnPOPost.Text = "Auto Create GRN and MRN on PO Post"
        '
        'chkIsEnterQtyOnSRN
        '
        Me.chkIsEnterQtyOnSRN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsEnterQtyOnSRN.Location = New System.Drawing.Point(242, 201)
        Me.chkIsEnterQtyOnSRN.Name = "chkIsEnterQtyOnSRN"
        Me.chkIsEnterQtyOnSRN.Size = New System.Drawing.Size(215, 16)
        Me.chkIsEnterQtyOnSRN.TabIndex = 5
        Me.chkIsEnterQtyOnSRN.Text = "Enter GRN and MRN Quantity on SRN"
        '
        'dgvclasss
        '
        Me.dgvclasss.BackColor = System.Drawing.SystemColors.Control
        Me.dgvclasss.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvclasss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvclasss.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvclasss.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvclasss.Location = New System.Drawing.Point(5, 7)
        '
        'dgvclasss
        '
        Me.dgvclasss.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Class "
        GridViewTextBoxColumn1.Name = "column1"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 130
        GridViewTextBoxColumn2.HeaderText = "Class Description"
        GridViewTextBoxColumn2.MaxLength = 45
        GridViewTextBoxColumn2.Name = "column2"
        GridViewTextBoxColumn2.Width = 200
        ConditionalFormattingObject1.CellBackColor = System.Drawing.Color.Empty
        ConditionalFormattingObject1.CellForeColor = System.Drawing.Color.Empty
        ConditionalFormattingObject1.Name = "NewCondition"
        ConditionalFormattingObject1.RowBackColor = System.Drawing.Color.Empty
        ConditionalFormattingObject1.RowForeColor = System.Drawing.Color.Empty
        ConditionalFormattingObject1.TValue1 = "50"
        GridViewDecimalColumn1.ConditionalFormattingObjectList.Add(ConditionalFormattingObject1)
        GridViewDecimalColumn1.HeaderText = "Length"
        GridViewDecimalColumn1.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        GridViewDecimalColumn1.Name = "column3"
        GridViewDecimalColumn1.Width = 100
        GridViewComboBoxColumn1.HeaderText = "Class Type"
        GridViewComboBoxColumn1.Name = "column4"
        GridViewComboBoxColumn1.Width = 118
        Me.dgvclasss.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewDecimalColumn1, GridViewComboBoxColumn1})
        Me.dgvclasss.MasterTemplate.EnableGrouping = False
        Me.dgvclasss.MasterTemplate.ShowFilteringRow = False
        Me.dgvclasss.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvclasss.Name = "dgvclasss"
        Me.dgvclasss.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvclasss.ShowHeaderCellButtons = True
        Me.dgvclasss.Size = New System.Drawing.Size(571, 172)
        Me.dgvclasss.TabIndex = 0
        Me.dgvclasss.TabStop = False
        Me.dgvclasss.Text = "RadGridView1"
        '
        'chkauto_item_nlevel
        '
        Me.chkauto_item_nlevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkauto_item_nlevel.Location = New System.Drawing.Point(7, 4)
        Me.chkauto_item_nlevel.Name = "chkauto_item_nlevel"
        Me.chkauto_item_nlevel.Size = New System.Drawing.Size(195, 16)
        Me.chkauto_item_nlevel.TabIndex = 24
        Me.chkauto_item_nlevel.Text = "Is Generate Item Code By Coutner"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1078, 551)
        Me.SplitContainer1.SplitterDistance = 519
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1078, 519)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.groupbox)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(98.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1057, 471)
        Me.RadPageViewPage1.Text = "General Settings"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.chkauto_item_nlevel)
        Me.RadPageViewPage2.Controls.Add(Me.grpItemType)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(151.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1036, 471)
        Me.RadPageViewPage2.Text = "Item Prefix Counter Setting"
        '
        'grpItemType
        '
        Me.grpItemType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpItemType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpItemType.Controls.Add(Me.gvItemType)
        Me.grpItemType.HeaderText = "Item Master Couter"
        Me.grpItemType.Location = New System.Drawing.Point(2, 26)
        Me.grpItemType.Name = "grpItemType"
        Me.grpItemType.Size = New System.Drawing.Size(403, 445)
        Me.grpItemType.TabIndex = 0
        Me.grpItemType.Text = "Item Master Couter"
        '
        'gvItemType
        '
        Me.gvItemType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvItemType.BackColor = System.Drawing.SystemColors.Control
        Me.gvItemType.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvItemType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvItemType.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItemType.Location = New System.Drawing.Point(5, 13)
        '
        'gvItemType
        '
        Me.gvItemType.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn3.FieldName = "ITEM_TYPE_NAME"
        GridViewTextBoxColumn3.HeaderText = "Item Type"
        GridViewTextBoxColumn3.MaxLength = 45
        GridViewTextBoxColumn3.Name = "ColItemType"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 180
        GridViewTextBoxColumn4.FieldName = "Description"
        GridViewTextBoxColumn4.HeaderText = "Prefix"
        GridViewTextBoxColumn4.MaxLength = 10
        GridViewTextBoxColumn4.Name = "ColPrefix"
        GridViewTextBoxColumn4.Width = 150
        GridViewTextBoxColumn5.FieldName = "ITEM_TYPE_CODE"
        GridViewTextBoxColumn5.HeaderText = "column3"
        GridViewTextBoxColumn5.IsVisible = False
        GridViewTextBoxColumn5.Name = "ColItemTypeCode"
        GridViewTextBoxColumn5.VisibleInColumnChooser = False
        Me.gvItemType.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5})
        Me.gvItemType.MasterTemplate.EnableGrouping = False
        Me.gvItemType.MasterTemplate.ShowFilteringRow = False
        Me.gvItemType.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItemType.Name = "gvItemType"
        Me.gvItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItemType.ShowHeaderCellButtons = True
        Me.gvItemType.Size = New System.Drawing.Size(393, 427)
        Me.gvItemType.TabIndex = 1
        Me.gvItemType.TabStop = False
        Me.gvItemType.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1078, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'chkNegativeStockInDairyProduction
        '
        Me.chkNegativeStockInDairyProduction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNegativeStockInDairyProduction.Location = New System.Drawing.Point(814, 56)
        Me.chkNegativeStockInDairyProduction.Name = "chkNegativeStockInDairyProduction"
        Me.chkNegativeStockInDairyProduction.Size = New System.Drawing.Size(224, 16)
        Me.chkNegativeStockInDairyProduction.TabIndex = 49
        Me.chkNegativeStockInDairyProduction.Text = "Allow Negative stock In Dairy Production"
        Me.chkNegativeStockInDairyProduction.Visible = False
        '
        'frmInventorySetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 571)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmInventorySetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Inventory Setting"
        CType(Me.chkallownegativeinventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallowreceipts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupbox.ResumeLayout(False)
        Me.groupbox.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.gv_itemsettings.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_itemsettings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateTransferFromBooking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGPAfterTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBomProductionProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCSACommision_Inv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsMRPWiseBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowNegativeStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNegativeStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProdQty_Decimal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowtoshowMilkTypeonAdjustmentEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkthirdparty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDocSeriesSetting.ResumeLayout(False)
        Me.gbDocSeriesSetting.PerformLayout()
        CType(Me.chkLocal_InterStateTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransferWithProductionSaleSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransferJEForLocationMapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWithDifferntUnitConsiderAsOtherItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txttradingGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFinishedGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSemiFinishGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRawMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllowtoeditCategorycodeinitemmaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPrncpl_BOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNlevel_Location, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkalwPGMCusMst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowTermsEditSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowTermEditPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowTermsEditMM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBatchMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowchangeInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnStockAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbntBalanceOnDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnIsConsiderOutTypeDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMRPwithAbatement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chknLevelCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowcostZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoCreateSRNMRNOnPOPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsEnterQtyOnSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvclasss.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvclasss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkauto_item_nlevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.grpItemType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpItemType.ResumeLayout(False)
        CType(Me.gvItemType.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNegativeStockInDairyProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkallownegativeinventory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkallowreceipts As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents groupbox As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgvclasss As common.UserControls.MyRadGridView
    Friend WithEvents chkIsEnterQtyOnSRN As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkAutoCreateSRNMRNOnPOPost As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowcostZero As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chknLevelCategory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkMRPwithAbatement As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkBackCalculation As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAutoScheme As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnStockAvailable As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbntBalanceOnDocDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnIsConsiderOutTypeDoc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkAllowchangeInvoiceType As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkBatchMandatory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowTermsEditMM As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowTermEditPurchase As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowTermsEditSales As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkalwPGMCusMst As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkthirdparty As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkBomProductionProcess As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkNlevel_Location As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkauto_item_nlevel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPrncpl_BOM As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCSACommision_Inv As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowtoshowMilkTypeonAdjustmentEntry As common.Controls.MyCheckBox
    Friend WithEvents ChkAllowtoeditCategorycodeinitemmaster As common.Controls.MyCheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOther As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtAsset As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtFinishedGoods As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSemiFinishGoods As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRawMaterial As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents chkItemWithDifferntUnitConsiderAsOtherItem As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIsMRPWiseBalance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkTransferJEForLocationMapping As common.Controls.MyCheckBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndVehicle_Unit As common.UserControls.txtFinder
    Friend WithEvents chkTransferWithProductionSaleSeries As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gbDocSeriesSetting As System.Windows.Forms.GroupBox
    Friend WithEvents cbgLocation As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents gvLocation As common.UserControls.MyRadGridView
    Friend WithEvents txtProdQty_Decimal As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents chkLocal_InterStateTransfer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllowNegativeStock As common.Controls.MyCheckBox
    Friend WithEvents txtNegativeStock As common.MyNumBox
    Friend WithEvents fndProd_FatSnf_Base_Unit As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txttradingGoods As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents chkGPAfterTransfer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCreateTransferFromBooking As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents gv_itemsettings As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents grpItemType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItemType As common.UserControls.MyRadGridView
    Friend WithEvents chkNegativeStockInDairyProduction As Telerik.WinControls.UI.RadCheckBox
End Class

