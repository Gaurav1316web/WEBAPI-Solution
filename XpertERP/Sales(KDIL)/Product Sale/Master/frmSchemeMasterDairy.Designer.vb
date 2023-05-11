<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSchemeMasterDairy
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.export_main = New Telerik.WinControls.UI.RadMenuItem()
        Me.export_beneficial = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Criteria = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmWholeSheetExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSchemeWithSlabExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import_Main = New Telerik.WinControls.UI.RadMenuItem()
        Me.import_beneficial = New Telerik.WinControls.UI.RadMenuItem()
        Me.import_criteria = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportWholeSheet = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSchemeWithSlabImp = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.btn_Apply = New Telerik.WinControls.UI.RadButton()
        Me.txtAmount = New common.MyNumBox()
        Me.lblUnit = New common.Controls.MyLabel()
        Me.txtPercentage = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtQuantitiveStructureFreeUOM = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtQuantitiveStructureFreeQty = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtQuantitiveStructureFreeICode = New common.UserControls.txtFinder()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtQuantitiveStructureMainUOM = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtQuantitiveStructureMainQty = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtQuantitiveStructureCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.txtQty = New common.MyNumBox()
        Me.lblQty = New common.Controls.MyLabel()
        Me.txtUnitCode = New common.UserControls.txtFinder()
        Me.lblTargetType = New common.Controls.MyLabel()
        Me.ddlTargetType = New common.Controls.MyComboBox()
        Me.btnItemSelect = New Telerik.WinControls.UI.RadButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblUnitCodeDesc = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblMainItemDesc = New common.Controls.MyLabel()
        Me.ddlType = New common.Controls.MyComboBox()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.dtpScheme = New common.Controls.MyDateTimePicker()
        Me.fndScheme = New common.UserControls.txtNavigator()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.dtpInactive = New common.Controls.MyDateTimePicker()
        Me.txtMainItemGrp = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.ddlBasicPrice = New common.Controls.MyComboBox()
        Me.ddlmrp = New common.Controls.MyComboBox()
        Me.lblmrp = New common.Controls.MyLabel()
        Me.GrpQuantiiveType = New System.Windows.Forms.GroupBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cboQuantitiveType = New common.Controls.MyComboBox()
        Me.gvItem = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblCriteria = New common.Controls.MyLabel()
        Me.txtCriteria = New common.UserControls.txtFinder()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.ddlCriteria = New common.Controls.MyComboBox()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.gvCustomer = New Telerik.WinControls.UI.RadGridView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.pgAttachmentSchMst = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkQuantativeSch = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnApply = New Telerik.WinControls.UI.RadButton()
        Me.lblQuantum = New common.Controls.MyLabel()
        Me.txtQuantum = New common.MyNumBox()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.gvTS = New common.UserControls.MyRadGridView()
        Me.gvTS2 = New common.UserControls.MyRadGridView()
        Me.chkSlabWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.VolumeSlab = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.lblStructUnit = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtstructUnit = New common.UserControls.txtFinder()
        Me.lblStructDesc = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtStrcutCode = New common.UserControls.txtFinder()
        Me.gvVolumeSlab = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.txtRangeUnit = New common.UserControls.txtFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtItemSturcture = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblCashDisunit = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtCashDisunit = New common.UserControls.txtFinder()
        Me.gvCashDisGrid = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_Apply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuantitiveStructureFreeQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuantitiveStructureMainQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTargetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTargetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMainItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpScheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpQuantiiveType.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboQuantitiveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.pgAttachmentSchMst.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ChkQuantativeSch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQuantum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuantum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gvTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTS.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTS2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTS2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSlabWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VolumeSlab.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.lblStructUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStructDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVolumeSlab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVolumeSlab.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCashDisunit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCashDisGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCashDisGrid.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(980, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.export_main, Me.export_beneficial, Me.Export_Criteria, Me.rmWholeSheetExport, Me.rmSchemeWithSlabExport})
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'export_main
        '
        Me.export_main.AccessibleDescription = "Main Item Detail"
        Me.export_main.AccessibleName = "Main Item Detail"
        Me.export_main.Name = "export_main"
        Me.export_main.Text = "Main Item Detail"
        '
        'export_beneficial
        '
        Me.export_beneficial.AccessibleDescription = "Beneficial Detail"
        Me.export_beneficial.AccessibleName = "Beneficial Detail"
        Me.export_beneficial.Name = "export_beneficial"
        Me.export_beneficial.Text = "Scheme Items Detail"
        '
        'Export_Criteria
        '
        Me.Export_Criteria.AccessibleDescription = "Criteria Detail"
        Me.Export_Criteria.AccessibleName = "Criteria Detail"
        Me.Export_Criteria.Name = "Export_Criteria"
        Me.Export_Criteria.Text = "Criteria Detail"
        '
        'rmWholeSheetExport
        '
        Me.rmWholeSheetExport.AccessibleDescription = "Whole Sheet Export"
        Me.rmWholeSheetExport.AccessibleName = "Whole Sheet Export"
        Me.rmWholeSheetExport.Name = "rmWholeSheetExport"
        Me.rmWholeSheetExport.Text = "Whole Sheet Export"
        '
        'rmSchemeWithSlabExport
        '
        Me.rmSchemeWithSlabExport.AccessibleDescription = "Scheme With Slab"
        Me.rmSchemeWithSlabExport.AccessibleName = "Scheme With Slab"
        Me.rmSchemeWithSlabExport.Name = "rmSchemeWithSlabExport"
        Me.rmSchemeWithSlabExport.Text = "Scheme With Slab"
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "Import"
        Me.rmiImport.AccessibleName = "Import"
        Me.rmiImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import_Main, Me.import_beneficial, Me.import_criteria, Me.rmImportWholeSheet, Me.rmSchemeWithSlabImp})
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'Import_Main
        '
        Me.Import_Main.AccessibleDescription = "Main item Detail"
        Me.Import_Main.AccessibleName = "Main item Detail"
        Me.Import_Main.Name = "Import_Main"
        Me.Import_Main.Text = "Main item Detail"
        '
        'import_beneficial
        '
        Me.import_beneficial.AccessibleDescription = "Beneficial Detail"
        Me.import_beneficial.AccessibleName = "Beneficial Detail"
        Me.import_beneficial.Name = "import_beneficial"
        Me.import_beneficial.Text = "Scheme Items Detail"
        '
        'import_criteria
        '
        Me.import_criteria.AccessibleDescription = "Criteria Detail"
        Me.import_criteria.AccessibleName = "Criteria Detail"
        Me.import_criteria.Name = "import_criteria"
        Me.import_criteria.Text = "Criteria Detail"
        '
        'rmImportWholeSheet
        '
        Me.rmImportWholeSheet.AccessibleDescription = "Whole Sheet Import"
        Me.rmImportWholeSheet.AccessibleName = "Whole Sheet Import"
        Me.rmImportWholeSheet.Name = "rmImportWholeSheet"
        Me.rmImportWholeSheet.Text = "Whole Sheet Import"
        '
        'rmSchemeWithSlabImp
        '
        Me.rmSchemeWithSlabImp.AccessibleDescription = "Scheme With Slab"
        Me.rmSchemeWithSlabImp.AccessibleName = "Scheme With Slab"
        Me.rmSchemeWithSlabImp.Name = "rmSchemeWithSlabImp"
        Me.rmSchemeWithSlabImp.Text = "Scheme With Slab"
        '
        'rmiClose
        '
        Me.rmiClose.AccessibleDescription = "Close"
        Me.rmiClose.AccessibleName = "Close"
        Me.rmiClose.Name = "rmiClose"
        Me.rmiClose.Text = "Close"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(980, 439)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.pgAttachmentSchMst)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.VolumeSlab)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(980, 409)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(959, 361)
        Me.RadPageViewPage1.Text = "Scheme Details"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel10)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btn_Apply)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtAmount)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblUnit)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPercentage)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtQty)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQty)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtUnitCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTargetType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlTargetType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnItemSelect)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblUnitCodeDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblMainItemDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpScheme)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndScheme)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpInactive)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMainItemGrp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlBasicPrice)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel9)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlmrp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblmrp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.GrpQuantiiveType)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer3.Size = New System.Drawing.Size(959, 361)
        Me.SplitContainer3.SplitterDistance = 122
        Me.SplitContainer3.TabIndex = 0
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(759, 97)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel10.TabIndex = 28
        Me.RadLabel10.Text = "Amount"
        Me.RadLabel10.Visible = False
        '
        'btn_Apply
        '
        Me.btn_Apply.Location = New System.Drawing.Point(858, 92)
        Me.btn_Apply.Name = "btn_Apply"
        Me.btn_Apply.Size = New System.Drawing.Size(96, 18)
        Me.btn_Apply.TabIndex = 35
        Me.btn_Apply.Text = "Apply"
        Me.btn_Apply.Visible = False
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.White
        Me.txtAmount.CalculationExpression = Nothing
        Me.txtAmount.DecimalPlaces = 2
        Me.txtAmount.FieldCode = Nothing
        Me.txtAmount.FieldDesc = Nothing
        Me.txtAmount.FieldMaxLength = 0
        Me.txtAmount.FieldName = Nothing
        Me.txtAmount.isCalculatedField = False
        Me.txtAmount.IsSourceFromTable = False
        Me.txtAmount.IsSourceFromValueList = False
        Me.txtAmount.IsUnique = False
        Me.txtAmount.Location = New System.Drawing.Point(811, 93)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReferenceFieldDesc = Nothing
        Me.txtAmount.ReferenceFieldName = Nothing
        Me.txtAmount.ReferenceTableName = Nothing
        Me.txtAmount.Size = New System.Drawing.Size(46, 20)
        Me.txtAmount.TabIndex = 12
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0R
        Me.txtAmount.Visible = False
        '
        'lblUnit
        '
        Me.lblUnit.FieldName = Nothing
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.Location = New System.Drawing.Point(630, 72)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(26, 16)
        Me.lblUnit.TabIndex = 17
        Me.lblUnit.Text = "Unit"
        Me.lblUnit.Visible = False
        '
        'txtPercentage
        '
        Me.txtPercentage.BackColor = System.Drawing.Color.White
        Me.txtPercentage.CalculationExpression = Nothing
        Me.txtPercentage.DecimalPlaces = 2
        Me.txtPercentage.FieldCode = Nothing
        Me.txtPercentage.FieldDesc = Nothing
        Me.txtPercentage.FieldMaxLength = 0
        Me.txtPercentage.FieldName = Nothing
        Me.txtPercentage.isCalculatedField = False
        Me.txtPercentage.IsSourceFromTable = False
        Me.txtPercentage.IsSourceFromValueList = False
        Me.txtPercentage.IsUnique = False
        Me.txtPercentage.Location = New System.Drawing.Point(663, 94)
        Me.txtPercentage.MendatroryField = False
        Me.txtPercentage.MyLinkLable1 = Nothing
        Me.txtPercentage.MyLinkLable2 = Nothing
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.ReferenceFieldDesc = Nothing
        Me.txtPercentage.ReferenceFieldName = Nothing
        Me.txtPercentage.ReferenceTableName = Nothing
        Me.txtPercentage.Size = New System.Drawing.Size(43, 20)
        Me.txtPercentage.TabIndex = 11
        Me.txtPercentage.Text = "0"
        Me.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPercentage.Value = 0R
        Me.txtPercentage.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(640, 96)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "%"
        Me.MyLabel1.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureFreeUOM)
        Me.GroupBox1.Controls.Add(Me.MyLabel10)
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureFreeQty)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureFreeICode)
        Me.GroupBox1.Controls.Add(Me.MyLabel12)
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureMainUOM)
        Me.GroupBox1.Controls.Add(Me.MyLabel9)
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureMainQty)
        Me.GroupBox1.Controls.Add(Me.MyLabel8)
        Me.GroupBox1.Controls.Add(Me.txtQuantitiveStructureCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(944, 83)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        '
        'txtQuantitiveStructureFreeUOM
        '
        Me.txtQuantitiveStructureFreeUOM.CalculationExpression = Nothing
        Me.txtQuantitiveStructureFreeUOM.FieldCode = Nothing
        Me.txtQuantitiveStructureFreeUOM.FieldDesc = Nothing
        Me.txtQuantitiveStructureFreeUOM.FieldMaxLength = 0
        Me.txtQuantitiveStructureFreeUOM.FieldName = Nothing
        Me.txtQuantitiveStructureFreeUOM.isCalculatedField = False
        Me.txtQuantitiveStructureFreeUOM.IsSourceFromTable = False
        Me.txtQuantitiveStructureFreeUOM.IsSourceFromValueList = False
        Me.txtQuantitiveStructureFreeUOM.IsUnique = False
        Me.txtQuantitiveStructureFreeUOM.Location = New System.Drawing.Point(431, 58)
        Me.txtQuantitiveStructureFreeUOM.MendatroryField = True
        Me.txtQuantitiveStructureFreeUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantitiveStructureFreeUOM.MyLinkLable1 = Me.MyLabel10
        Me.txtQuantitiveStructureFreeUOM.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureFreeUOM.MyReadOnly = False
        Me.txtQuantitiveStructureFreeUOM.MyShowMasterFormButton = False
        Me.txtQuantitiveStructureFreeUOM.Name = "txtQuantitiveStructureFreeUOM"
        Me.txtQuantitiveStructureFreeUOM.ReferenceFieldDesc = Nothing
        Me.txtQuantitiveStructureFreeUOM.ReferenceFieldName = Nothing
        Me.txtQuantitiveStructureFreeUOM.ReferenceTableName = Nothing
        Me.txtQuantitiveStructureFreeUOM.Size = New System.Drawing.Size(90, 18)
        Me.txtQuantitiveStructureFreeUOM.TabIndex = 5
        Me.txtQuantitiveStructureFreeUOM.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(395, 59)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel10.TabIndex = 6
        Me.MyLabel10.Text = "Unit"
        '
        'txtQuantitiveStructureFreeQty
        '
        Me.txtQuantitiveStructureFreeQty.BackColor = System.Drawing.Color.White
        Me.txtQuantitiveStructureFreeQty.CalculationExpression = Nothing
        Me.txtQuantitiveStructureFreeQty.DecimalPlaces = 2
        Me.txtQuantitiveStructureFreeQty.FieldCode = Nothing
        Me.txtQuantitiveStructureFreeQty.FieldDesc = Nothing
        Me.txtQuantitiveStructureFreeQty.FieldMaxLength = 0
        Me.txtQuantitiveStructureFreeQty.FieldName = Nothing
        Me.txtQuantitiveStructureFreeQty.isCalculatedField = False
        Me.txtQuantitiveStructureFreeQty.IsSourceFromTable = False
        Me.txtQuantitiveStructureFreeQty.IsSourceFromValueList = False
        Me.txtQuantitiveStructureFreeQty.IsUnique = False
        Me.txtQuantitiveStructureFreeQty.Location = New System.Drawing.Point(292, 57)
        Me.txtQuantitiveStructureFreeQty.MendatroryField = True
        Me.txtQuantitiveStructureFreeQty.MyLinkLable1 = Nothing
        Me.txtQuantitiveStructureFreeQty.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureFreeQty.Name = "txtQuantitiveStructureFreeQty"
        Me.txtQuantitiveStructureFreeQty.ReferenceFieldDesc = Nothing
        Me.txtQuantitiveStructureFreeQty.ReferenceFieldName = Nothing
        Me.txtQuantitiveStructureFreeQty.ReferenceTableName = Nothing
        Me.txtQuantitiveStructureFreeQty.Size = New System.Drawing.Size(99, 20)
        Me.txtQuantitiveStructureFreeQty.TabIndex = 4
        Me.txtQuantitiveStructureFreeQty.Text = "0"
        Me.txtQuantitiveStructureFreeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQuantitiveStructureFreeQty.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(264, 59)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel11.TabIndex = 8
        Me.MyLabel11.Text = "Qty"
        '
        'txtQuantitiveStructureFreeICode
        '
        Me.txtQuantitiveStructureFreeICode.CalculationExpression = Nothing
        Me.txtQuantitiveStructureFreeICode.FieldCode = Nothing
        Me.txtQuantitiveStructureFreeICode.FieldDesc = Nothing
        Me.txtQuantitiveStructureFreeICode.FieldMaxLength = 0
        Me.txtQuantitiveStructureFreeICode.FieldName = Nothing
        Me.txtQuantitiveStructureFreeICode.isCalculatedField = False
        Me.txtQuantitiveStructureFreeICode.IsSourceFromTable = False
        Me.txtQuantitiveStructureFreeICode.IsSourceFromValueList = False
        Me.txtQuantitiveStructureFreeICode.IsUnique = False
        Me.txtQuantitiveStructureFreeICode.Location = New System.Drawing.Point(110, 58)
        Me.txtQuantitiveStructureFreeICode.MendatroryField = True
        Me.txtQuantitiveStructureFreeICode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantitiveStructureFreeICode.MyLinkLable1 = Me.MyLabel12
        Me.txtQuantitiveStructureFreeICode.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureFreeICode.MyReadOnly = False
        Me.txtQuantitiveStructureFreeICode.MyShowMasterFormButton = False
        Me.txtQuantitiveStructureFreeICode.Name = "txtQuantitiveStructureFreeICode"
        Me.txtQuantitiveStructureFreeICode.ReferenceFieldDesc = Nothing
        Me.txtQuantitiveStructureFreeICode.ReferenceFieldName = Nothing
        Me.txtQuantitiveStructureFreeICode.ReferenceTableName = Nothing
        Me.txtQuantitiveStructureFreeICode.Size = New System.Drawing.Size(139, 18)
        Me.txtQuantitiveStructureFreeICode.TabIndex = 3
        Me.txtQuantitiveStructureFreeICode.Value = ""
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(6, 59)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel12.TabIndex = 10
        Me.MyLabel12.Text = "Free Item  "
        '
        'txtQuantitiveStructureMainUOM
        '
        Me.txtQuantitiveStructureMainUOM.CalculationExpression = Nothing
        Me.txtQuantitiveStructureMainUOM.FieldCode = Nothing
        Me.txtQuantitiveStructureMainUOM.FieldDesc = Nothing
        Me.txtQuantitiveStructureMainUOM.FieldMaxLength = 0
        Me.txtQuantitiveStructureMainUOM.FieldName = Nothing
        Me.txtQuantitiveStructureMainUOM.isCalculatedField = False
        Me.txtQuantitiveStructureMainUOM.IsSourceFromTable = False
        Me.txtQuantitiveStructureMainUOM.IsSourceFromValueList = False
        Me.txtQuantitiveStructureMainUOM.IsUnique = False
        Me.txtQuantitiveStructureMainUOM.Location = New System.Drawing.Point(431, 36)
        Me.txtQuantitiveStructureMainUOM.MendatroryField = True
        Me.txtQuantitiveStructureMainUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantitiveStructureMainUOM.MyLinkLable1 = Me.MyLabel9
        Me.txtQuantitiveStructureMainUOM.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureMainUOM.MyReadOnly = False
        Me.txtQuantitiveStructureMainUOM.MyShowMasterFormButton = False
        Me.txtQuantitiveStructureMainUOM.Name = "txtQuantitiveStructureMainUOM"
        Me.txtQuantitiveStructureMainUOM.ReferenceFieldDesc = Nothing
        Me.txtQuantitiveStructureMainUOM.ReferenceFieldName = Nothing
        Me.txtQuantitiveStructureMainUOM.ReferenceTableName = Nothing
        Me.txtQuantitiveStructureMainUOM.Size = New System.Drawing.Size(90, 18)
        Me.txtQuantitiveStructureMainUOM.TabIndex = 2
        Me.txtQuantitiveStructureMainUOM.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(395, 37)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel9.TabIndex = 7
        Me.MyLabel9.Text = "Unit"
        '
        'txtQuantitiveStructureMainQty
        '
        Me.txtQuantitiveStructureMainQty.BackColor = System.Drawing.Color.White
        Me.txtQuantitiveStructureMainQty.CalculationExpression = Nothing
        Me.txtQuantitiveStructureMainQty.DecimalPlaces = 2
        Me.txtQuantitiveStructureMainQty.FieldCode = Nothing
        Me.txtQuantitiveStructureMainQty.FieldDesc = Nothing
        Me.txtQuantitiveStructureMainQty.FieldMaxLength = 0
        Me.txtQuantitiveStructureMainQty.FieldName = Nothing
        Me.txtQuantitiveStructureMainQty.isCalculatedField = False
        Me.txtQuantitiveStructureMainQty.IsSourceFromTable = False
        Me.txtQuantitiveStructureMainQty.IsSourceFromValueList = False
        Me.txtQuantitiveStructureMainQty.IsUnique = False
        Me.txtQuantitiveStructureMainQty.Location = New System.Drawing.Point(292, 35)
        Me.txtQuantitiveStructureMainQty.MendatroryField = True
        Me.txtQuantitiveStructureMainQty.MyLinkLable1 = Nothing
        Me.txtQuantitiveStructureMainQty.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureMainQty.Name = "txtQuantitiveStructureMainQty"
        Me.txtQuantitiveStructureMainQty.ReferenceFieldDesc = Nothing
        Me.txtQuantitiveStructureMainQty.ReferenceFieldName = Nothing
        Me.txtQuantitiveStructureMainQty.ReferenceTableName = Nothing
        Me.txtQuantitiveStructureMainQty.Size = New System.Drawing.Size(99, 20)
        Me.txtQuantitiveStructureMainQty.TabIndex = 1
        Me.txtQuantitiveStructureMainQty.Text = "0"
        Me.txtQuantitiveStructureMainQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQuantitiveStructureMainQty.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(212, 37)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel8.TabIndex = 9
        Me.MyLabel8.Text = "Main Item Qty"
        '
        'txtQuantitiveStructureCode
        '
        Me.txtQuantitiveStructureCode.arrDispalyMember = Nothing
        Me.txtQuantitiveStructureCode.arrValueMember = Nothing
        Me.txtQuantitiveStructureCode.Location = New System.Drawing.Point(110, 13)
        Me.txtQuantitiveStructureCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantitiveStructureCode.MyLinkLable1 = Nothing
        Me.txtQuantitiveStructureCode.MyLinkLable2 = Nothing
        Me.txtQuantitiveStructureCode.MyNullText = "All"
        Me.txtQuantitiveStructureCode.Name = "txtQuantitiveStructureCode"
        Me.txtQuantitiveStructureCode.Size = New System.Drawing.Size(411, 19)
        Me.txtQuantitiveStructureCode.TabIndex = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel4.TabIndex = 361
        Me.MyLabel4.Text = "Item Structure"
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
        Me.txtComment.Location = New System.Drawing.Point(120, 93)
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel9
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(501, 18)
        Me.txtComment.TabIndex = 14
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(10, 94)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel9.TabIndex = 29
        Me.RadLabel9.Text = "Comment"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.CalculationExpression = Nothing
        Me.txtQty.DecimalPlaces = 2
        Me.txtQty.FieldCode = Nothing
        Me.txtQty.FieldDesc = Nothing
        Me.txtQty.FieldMaxLength = 0
        Me.txtQty.FieldName = Nothing
        Me.txtQty.isCalculatedField = False
        Me.txtQty.IsSourceFromTable = False
        Me.txtQty.IsSourceFromValueList = False
        Me.txtQty.IsUnique = False
        Me.txtQty.Location = New System.Drawing.Point(810, 71)
        Me.txtQty.MendatroryField = False
        Me.txtQty.MyLinkLable1 = Nothing
        Me.txtQty.MyLinkLable2 = Nothing
        Me.txtQty.Name = "txtQty"
        Me.txtQty.ReferenceFieldDesc = Nothing
        Me.txtQty.ReferenceFieldName = Nothing
        Me.txtQty.ReferenceTableName = Nothing
        Me.txtQty.Size = New System.Drawing.Size(47, 20)
        Me.txtQty.TabIndex = 9
        Me.txtQty.Text = "0"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.Value = 0R
        Me.txtQty.Visible = False
        '
        'lblQty
        '
        Me.lblQty.FieldName = Nothing
        Me.lblQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.Location = New System.Drawing.Point(780, 73)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(24, 16)
        Me.lblQty.TabIndex = 25
        Me.lblQty.Text = "Qty"
        Me.lblQty.Visible = False
        '
        'txtUnitCode
        '
        Me.txtUnitCode.CalculationExpression = Nothing
        Me.txtUnitCode.FieldCode = Nothing
        Me.txtUnitCode.FieldDesc = Nothing
        Me.txtUnitCode.FieldMaxLength = 0
        Me.txtUnitCode.FieldName = Nothing
        Me.txtUnitCode.isCalculatedField = False
        Me.txtUnitCode.IsSourceFromTable = False
        Me.txtUnitCode.IsSourceFromValueList = False
        Me.txtUnitCode.IsUnique = False
        Me.txtUnitCode.Location = New System.Drawing.Point(663, 71)
        Me.txtUnitCode.MendatroryField = True
        Me.txtUnitCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitCode.MyLinkLable1 = Me.lblUnit
        Me.txtUnitCode.MyLinkLable2 = Nothing
        Me.txtUnitCode.MyReadOnly = False
        Me.txtUnitCode.MyShowMasterFormButton = False
        Me.txtUnitCode.Name = "txtUnitCode"
        Me.txtUnitCode.ReferenceFieldDesc = Nothing
        Me.txtUnitCode.ReferenceFieldName = Nothing
        Me.txtUnitCode.ReferenceTableName = Nothing
        Me.txtUnitCode.Size = New System.Drawing.Size(90, 18)
        Me.txtUnitCode.TabIndex = 7
        Me.txtUnitCode.Value = ""
        Me.txtUnitCode.Visible = False
        '
        'lblTargetType
        '
        Me.lblTargetType.FieldName = Nothing
        Me.lblTargetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetType.Location = New System.Drawing.Point(448, 72)
        Me.lblTargetType.Name = "lblTargetType"
        Me.lblTargetType.Size = New System.Drawing.Size(67, 16)
        Me.lblTargetType.TabIndex = 36
        Me.lblTargetType.Text = "Target Type"
        '
        'ddlTargetType
        '
        Me.ddlTargetType.AutoCompleteDisplayMember = Nothing
        Me.ddlTargetType.AutoCompleteValueMember = Nothing
        Me.ddlTargetType.CalculationExpression = Nothing
        Me.ddlTargetType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTargetType.FieldCode = Nothing
        Me.ddlTargetType.FieldDesc = Nothing
        Me.ddlTargetType.FieldMaxLength = 0
        Me.ddlTargetType.FieldName = Nothing
        Me.ddlTargetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlTargetType.isCalculatedField = False
        Me.ddlTargetType.IsSourceFromTable = False
        Me.ddlTargetType.IsSourceFromValueList = False
        Me.ddlTargetType.IsUnique = False
        Me.ddlTargetType.Location = New System.Drawing.Point(518, 71)
        Me.ddlTargetType.MendatroryField = False
        Me.ddlTargetType.MyLinkLable1 = Me.lblTargetType
        Me.ddlTargetType.MyLinkLable2 = Nothing
        Me.ddlTargetType.Name = "ddlTargetType"
        Me.ddlTargetType.ReferenceFieldDesc = Nothing
        Me.ddlTargetType.ReferenceFieldName = Nothing
        Me.ddlTargetType.ReferenceTableName = Nothing
        Me.ddlTargetType.Size = New System.Drawing.Size(103, 18)
        Me.ddlTargetType.TabIndex = 35
        '
        'btnItemSelect
        '
        Me.btnItemSelect.Location = New System.Drawing.Point(858, 47)
        Me.btnItemSelect.Name = "btnItemSelect"
        Me.btnItemSelect.Size = New System.Drawing.Size(96, 18)
        Me.btnItemSelect.TabIndex = 34
        Me.btnItemSelect.Text = "Select All"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(448, 49)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 33
        Me.lblToDate.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(518, 48)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(102, 18)
        Me.txtToDate.TabIndex = 32
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(280, 49)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 31
        Me.lblFromDate.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(344, 48)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblFromDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(99, 18)
        Me.txtFromDate.TabIndex = 30
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(789, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "Basic Price"
        Me.MyLabel2.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(10, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Scheme Code"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(10, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(370, 6)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "Date"
        '
        'lblUnitCodeDesc
        '
        Me.lblUnitCodeDesc.AutoSize = False
        Me.lblUnitCodeDesc.BorderVisible = True
        Me.lblUnitCodeDesc.FieldName = Nothing
        Me.lblUnitCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitCodeDesc.Location = New System.Drawing.Point(759, 71)
        Me.lblUnitCodeDesc.Name = "lblUnitCodeDesc"
        Me.lblUnitCodeDesc.Size = New System.Drawing.Size(10, 18)
        Me.lblUnitCodeDesc.TabIndex = 24
        Me.lblUnitCodeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnitCodeDesc.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(10, 72)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel4.TabIndex = 16
        Me.RadLabel4.Text = "Scheme Type"
        '
        'lblMainItemDesc
        '
        Me.lblMainItemDesc.AutoSize = False
        Me.lblMainItemDesc.BorderVisible = True
        Me.lblMainItemDesc.FieldName = Nothing
        Me.lblMainItemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainItemDesc.Location = New System.Drawing.Point(630, 48)
        Me.lblMainItemDesc.Name = "lblMainItemDesc"
        Me.lblMainItemDesc.Size = New System.Drawing.Size(214, 18)
        Me.lblMainItemDesc.TabIndex = 23
        Me.lblMainItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMainItemDesc.Visible = False
        '
        'ddlType
        '
        Me.ddlType.AutoCompleteDisplayMember = Nothing
        Me.ddlType.AutoCompleteValueMember = Nothing
        Me.ddlType.CalculationExpression = Nothing
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.FieldCode = Nothing
        Me.ddlType.FieldDesc = Nothing
        Me.ddlType.FieldMaxLength = 0
        Me.ddlType.FieldName = Nothing
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlType.isCalculatedField = False
        Me.ddlType.IsSourceFromTable = False
        Me.ddlType.IsSourceFromValueList = False
        Me.ddlType.IsUnique = False
        Me.ddlType.Location = New System.Drawing.Point(120, 71)
        Me.ddlType.MendatroryField = False
        Me.ddlType.MyLinkLable1 = Me.RadLabel4
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.ReferenceFieldDesc = Nothing
        Me.ddlType.ReferenceFieldName = Nothing
        Me.ddlType.ReferenceTableName = Nothing
        Me.ddlType.Size = New System.Drawing.Size(157, 18)
        Me.ddlType.TabIndex = 8
        '
        'chkInactive
        '
        Me.chkInactive.Location = New System.Drawing.Point(528, 5)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 18)
        Me.chkInactive.TabIndex = 3
        Me.chkInactive.Text = "Inactive"
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
        Me.txtDesc.Location = New System.Drawing.Point(120, 27)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(663, 18)
        Me.txtDesc.TabIndex = 5
        '
        'dtpScheme
        '
        Me.dtpScheme.CalculationExpression = Nothing
        Me.dtpScheme.CustomFormat = "dd/MM/yyyy"
        Me.dtpScheme.FieldCode = Nothing
        Me.dtpScheme.FieldDesc = Nothing
        Me.dtpScheme.FieldMaxLength = 0
        Me.dtpScheme.FieldName = Nothing
        Me.dtpScheme.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpScheme.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpScheme.isCalculatedField = False
        Me.dtpScheme.IsSourceFromTable = False
        Me.dtpScheme.IsSourceFromValueList = False
        Me.dtpScheme.IsUnique = False
        Me.dtpScheme.Location = New System.Drawing.Point(406, 5)
        Me.dtpScheme.MendatroryField = False
        Me.dtpScheme.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpScheme.MyLinkLable1 = Me.RadLabel3
        Me.dtpScheme.MyLinkLable2 = Nothing
        Me.dtpScheme.Name = "dtpScheme"
        Me.dtpScheme.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpScheme.ReferenceFieldDesc = Nothing
        Me.dtpScheme.ReferenceFieldName = Nothing
        Me.dtpScheme.ReferenceTableName = Nothing
        Me.dtpScheme.Size = New System.Drawing.Size(106, 18)
        Me.dtpScheme.TabIndex = 2
        Me.dtpScheme.TabStop = False
        Me.dtpScheme.Text = "17/05/2011"
        Me.dtpScheme.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'fndScheme
        '
        Me.fndScheme.FieldName = Nothing
        Me.fndScheme.Location = New System.Drawing.Point(120, 5)
        Me.fndScheme.MendatroryField = True
        Me.fndScheme.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndScheme.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndScheme.MyLinkLable1 = Me.RadLabel1
        Me.fndScheme.MyLinkLable2 = Nothing
        Me.fndScheme.MyMaxLength = 32767
        Me.fndScheme.MyReadOnly = False
        Me.fndScheme.Name = "fndScheme"
        Me.fndScheme.Size = New System.Drawing.Size(217, 18)
        Me.fndScheme.TabIndex = 0
        Me.fndScheme.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(614, 6)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel5.TabIndex = 22
        Me.RadLabel5.Text = "InActive Date"
        Me.RadLabel5.Visible = False
        '
        'dtpInactive
        '
        Me.dtpInactive.CalculationExpression = Nothing
        Me.dtpInactive.CustomFormat = "dd/MM/yyyy"
        Me.dtpInactive.FieldCode = Nothing
        Me.dtpInactive.FieldDesc = Nothing
        Me.dtpInactive.FieldMaxLength = 0
        Me.dtpInactive.FieldName = Nothing
        Me.dtpInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInactive.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInactive.isCalculatedField = False
        Me.dtpInactive.IsSourceFromTable = False
        Me.dtpInactive.IsSourceFromValueList = False
        Me.dtpInactive.IsUnique = False
        Me.dtpInactive.Location = New System.Drawing.Point(698, 5)
        Me.dtpInactive.MendatroryField = False
        Me.dtpInactive.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInactive.MyLinkLable1 = Me.RadLabel5
        Me.dtpInactive.MyLinkLable2 = Nothing
        Me.dtpInactive.Name = "dtpInactive"
        Me.dtpInactive.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInactive.ReferenceFieldDesc = Nothing
        Me.dtpInactive.ReferenceFieldName = Nothing
        Me.dtpInactive.ReferenceTableName = Nothing
        Me.dtpInactive.Size = New System.Drawing.Size(85, 18)
        Me.dtpInactive.TabIndex = 4
        Me.dtpInactive.TabStop = False
        Me.dtpInactive.Text = "17/05/2011"
        Me.dtpInactive.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        Me.dtpInactive.Visible = False
        '
        'txtMainItemGrp
        '
        Me.txtMainItemGrp.CalculationExpression = Nothing
        Me.txtMainItemGrp.FieldCode = Nothing
        Me.txtMainItemGrp.FieldDesc = Nothing
        Me.txtMainItemGrp.FieldMaxLength = 0
        Me.txtMainItemGrp.FieldName = Nothing
        Me.txtMainItemGrp.isCalculatedField = False
        Me.txtMainItemGrp.IsSourceFromTable = False
        Me.txtMainItemGrp.IsSourceFromValueList = False
        Me.txtMainItemGrp.IsUnique = False
        Me.txtMainItemGrp.Location = New System.Drawing.Point(120, 48)
        Me.txtMainItemGrp.MendatroryField = True
        Me.txtMainItemGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainItemGrp.MyLinkLable1 = Me.RadLabel6
        Me.txtMainItemGrp.MyLinkLable2 = Nothing
        Me.txtMainItemGrp.MyReadOnly = False
        Me.txtMainItemGrp.MyShowMasterFormButton = False
        Me.txtMainItemGrp.Name = "txtMainItemGrp"
        Me.txtMainItemGrp.ReferenceFieldDesc = Nothing
        Me.txtMainItemGrp.ReferenceFieldName = Nothing
        Me.txtMainItemGrp.ReferenceTableName = Nothing
        Me.txtMainItemGrp.Size = New System.Drawing.Size(157, 18)
        Me.txtMainItemGrp.TabIndex = 6
        Me.txtMainItemGrp.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(10, 49)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel6.TabIndex = 18
        Me.RadLabel6.Text = "Item Group"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(337, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 1
        '
        'ddlBasicPrice
        '
        Me.ddlBasicPrice.AutoCompleteDisplayMember = Nothing
        Me.ddlBasicPrice.AutoCompleteValueMember = Nothing
        Me.ddlBasicPrice.CalculationExpression = Nothing
        Me.ddlBasicPrice.FieldCode = Nothing
        Me.ddlBasicPrice.FieldDesc = Nothing
        Me.ddlBasicPrice.FieldMaxLength = 0
        Me.ddlBasicPrice.FieldName = Nothing
        Me.ddlBasicPrice.isCalculatedField = False
        Me.ddlBasicPrice.IsSourceFromTable = False
        Me.ddlBasicPrice.IsSourceFromValueList = False
        Me.ddlBasicPrice.IsUnique = False
        Me.ddlBasicPrice.Location = New System.Drawing.Point(858, 3)
        Me.ddlBasicPrice.MendatroryField = False
        Me.ddlBasicPrice.MyLinkLable1 = Nothing
        Me.ddlBasicPrice.MyLinkLable2 = Nothing
        Me.ddlBasicPrice.Name = "ddlBasicPrice"
        Me.ddlBasicPrice.ReferenceFieldDesc = Nothing
        Me.ddlBasicPrice.ReferenceFieldName = Nothing
        Me.ddlBasicPrice.ReferenceTableName = Nothing
        Me.ddlBasicPrice.Size = New System.Drawing.Size(96, 20)
        Me.ddlBasicPrice.TabIndex = 10
        Me.ddlBasicPrice.Visible = False
        '
        'ddlmrp
        '
        Me.ddlmrp.AutoCompleteDisplayMember = Nothing
        Me.ddlmrp.AutoCompleteValueMember = Nothing
        Me.ddlmrp.CalculationExpression = Nothing
        Me.ddlmrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlmrp.FieldCode = Nothing
        Me.ddlmrp.FieldDesc = Nothing
        Me.ddlmrp.FieldMaxLength = 0
        Me.ddlmrp.FieldName = Nothing
        Me.ddlmrp.isCalculatedField = False
        Me.ddlmrp.IsSourceFromTable = False
        Me.ddlmrp.IsSourceFromValueList = False
        Me.ddlmrp.IsUnique = False
        Me.ddlmrp.Location = New System.Drawing.Point(858, 25)
        Me.ddlmrp.MendatroryField = False
        Me.ddlmrp.MyLinkLable1 = Me.lblmrp
        Me.ddlmrp.MyLinkLable2 = Nothing
        Me.ddlmrp.Name = "ddlmrp"
        Me.ddlmrp.ReferenceFieldDesc = Nothing
        Me.ddlmrp.ReferenceFieldName = Nothing
        Me.ddlmrp.ReferenceTableName = Nothing
        Me.ddlmrp.Size = New System.Drawing.Size(96, 20)
        Me.ddlmrp.TabIndex = 13
        Me.ddlmrp.Visible = False
        '
        'lblmrp
        '
        Me.lblmrp.FieldName = Nothing
        Me.lblmrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmrp.Location = New System.Drawing.Point(789, 26)
        Me.lblmrp.Name = "lblmrp"
        Me.lblmrp.Size = New System.Drawing.Size(31, 16)
        Me.lblmrp.TabIndex = 15
        Me.lblmrp.Text = "MRP"
        Me.lblmrp.Visible = False
        '
        'GrpQuantiiveType
        '
        Me.GrpQuantiiveType.Controls.Add(Me.MyLabel6)
        Me.GrpQuantiiveType.Controls.Add(Me.cboQuantitiveType)
        Me.GrpQuantiiveType.Location = New System.Drawing.Point(279, 62)
        Me.GrpQuantiiveType.Name = "GrpQuantiiveType"
        Me.GrpQuantiiveType.Size = New System.Drawing.Size(168, 29)
        Me.GrpQuantiiveType.TabIndex = 37
        Me.GrpQuantiiveType.TabStop = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(5, 8)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel6.TabIndex = 39
        Me.MyLabel6.Text = "Type"
        '
        'cboQuantitiveType
        '
        Me.cboQuantitiveType.AutoCompleteDisplayMember = Nothing
        Me.cboQuantitiveType.AutoCompleteValueMember = Nothing
        Me.cboQuantitiveType.CalculationExpression = Nothing
        Me.cboQuantitiveType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboQuantitiveType.FieldCode = Nothing
        Me.cboQuantitiveType.FieldDesc = Nothing
        Me.cboQuantitiveType.FieldMaxLength = 0
        Me.cboQuantitiveType.FieldName = Nothing
        Me.cboQuantitiveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboQuantitiveType.isCalculatedField = False
        Me.cboQuantitiveType.IsSourceFromTable = False
        Me.cboQuantitiveType.IsSourceFromValueList = False
        Me.cboQuantitiveType.IsUnique = False
        Me.cboQuantitiveType.Location = New System.Drawing.Point(65, 9)
        Me.cboQuantitiveType.MendatroryField = False
        Me.cboQuantitiveType.MyLinkLable1 = Me.MyLabel6
        Me.cboQuantitiveType.MyLinkLable2 = Nothing
        Me.cboQuantitiveType.Name = "cboQuantitiveType"
        Me.cboQuantitiveType.ReferenceFieldDesc = Nothing
        Me.cboQuantitiveType.ReferenceFieldName = Nothing
        Me.cboQuantitiveType.ReferenceTableName = Nothing
        Me.cboQuantitiveType.Size = New System.Drawing.Size(99, 18)
        Me.cboQuantitiveType.TabIndex = 38
        '
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        'gvItem
        '
        Me.gvItem.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvItem.MasterTemplate.AutoGenerateColumns = False
        Me.gvItem.MasterTemplate.EnableFiltering = True
        Me.gvItem.MasterTemplate.EnableGrouping = False
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.Size = New System.Drawing.Size(959, 235)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        Me.gvItem.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(119.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(959, 361)
        Me.RadPageViewPage2.Text = "Criteria by Customer"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel13)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvCustomer)
        Me.SplitContainer2.Size = New System.Drawing.Size(959, 361)
        Me.SplitContainer2.SplitterDistance = 26
        Me.SplitContainer2.TabIndex = 0
        '
        'lblCriteria
        '
        Me.lblCriteria.AutoSize = False
        Me.lblCriteria.BorderVisible = True
        Me.lblCriteria.FieldName = Nothing
        Me.lblCriteria.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.Location = New System.Drawing.Point(383, 4)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(376, 18)
        Me.lblCriteria.TabIndex = 618
        Me.lblCriteria.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCriteria
        '
        Me.txtCriteria.CalculationExpression = Nothing
        Me.txtCriteria.FieldCode = Nothing
        Me.txtCriteria.FieldDesc = Nothing
        Me.txtCriteria.FieldMaxLength = 0
        Me.txtCriteria.FieldName = Nothing
        Me.txtCriteria.isCalculatedField = False
        Me.txtCriteria.IsSourceFromTable = False
        Me.txtCriteria.IsSourceFromValueList = False
        Me.txtCriteria.IsUnique = False
        Me.txtCriteria.Location = New System.Drawing.Point(223, 4)
        Me.txtCriteria.MendatroryField = True
        Me.txtCriteria.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCriteria.MyLinkLable1 = Me.lblUnit
        Me.txtCriteria.MyLinkLable2 = Nothing
        Me.txtCriteria.MyReadOnly = False
        Me.txtCriteria.MyShowMasterFormButton = False
        Me.txtCriteria.Name = "txtCriteria"
        Me.txtCriteria.ReferenceFieldDesc = Nothing
        Me.txtCriteria.ReferenceFieldName = Nothing
        Me.txtCriteria.ReferenceTableName = Nothing
        Me.txtCriteria.Size = New System.Drawing.Size(156, 18)
        Me.txtCriteria.TabIndex = 1
        Me.txtCriteria.Value = ""
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(780, 4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(80, 18)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Select All"
        '
        'ddlCriteria
        '
        Me.ddlCriteria.AutoCompleteDisplayMember = Nothing
        Me.ddlCriteria.AutoCompleteValueMember = Nothing
        Me.ddlCriteria.CalculationExpression = Nothing
        Me.ddlCriteria.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCriteria.FieldCode = Nothing
        Me.ddlCriteria.FieldDesc = Nothing
        Me.ddlCriteria.FieldMaxLength = 0
        Me.ddlCriteria.FieldName = Nothing
        Me.ddlCriteria.isCalculatedField = False
        Me.ddlCriteria.IsSourceFromTable = False
        Me.ddlCriteria.IsSourceFromValueList = False
        Me.ddlCriteria.IsUnique = False
        Me.ddlCriteria.Location = New System.Drawing.Point(64, 3)
        Me.ddlCriteria.MendatroryField = False
        Me.ddlCriteria.MyLinkLable1 = Me.lblmrp
        Me.ddlCriteria.MyLinkLable2 = Nothing
        Me.ddlCriteria.Name = "ddlCriteria"
        Me.ddlCriteria.ReferenceFieldDesc = Nothing
        Me.ddlCriteria.ReferenceFieldName = Nothing
        Me.ddlCriteria.ReferenceTableName = Nothing
        Me.ddlCriteria.Size = New System.Drawing.Size(156, 20)
        Me.ddlCriteria.TabIndex = 0
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 5)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel13.TabIndex = 613
        Me.RadLabel13.Text = "Criteria"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.MasterTemplate.EnableGrouping = False
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.Size = New System.Drawing.Size(959, 331)
        Me.gvCustomer.TabIndex = 0
        Me.gvCustomer.TabStop = False
        Me.gvCustomer.Text = "RadGridView1"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(959, 361)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(959, 361)
        Me.UcCustomFields1.TabIndex = 1
        '
        'pgAttachmentSchMst
        '
        Me.pgAttachmentSchMst.Controls.Add(Me.UcAttachment1)
        Me.pgAttachmentSchMst.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pgAttachmentSchMst.Location = New System.Drawing.Point(10, 37)
        Me.pgAttachmentSchMst.Name = "pgAttachmentSchMst"
        Me.pgAttachmentSchMst.Size = New System.Drawing.Size(959, 361)
        Me.pgAttachmentSchMst.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(959, 361)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage3.Controls.Add(Me.chkSlabWise)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(37.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(959, 361)
        Me.RadPageViewPage3.Text = "Slab"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ChkQuantativeSch)
        Me.RadGroupBox1.Controls.Add(Me.btnApply)
        Me.RadGroupBox1.Controls.Add(Me.lblQuantum)
        Me.RadGroupBox1.Controls.Add(Me.txtQuantum)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 21)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(414, 24)
        Me.RadGroupBox1.TabIndex = 369
        Me.RadGroupBox1.Visible = False
        '
        'ChkQuantativeSch
        '
        Me.ChkQuantativeSch.Location = New System.Drawing.Point(5, 2)
        Me.ChkQuantativeSch.Name = "ChkQuantativeSch"
        Me.ChkQuantativeSch.Size = New System.Drawing.Size(154, 18)
        Me.ChkQuantativeSch.TabIndex = 40
        Me.ChkQuantativeSch.Text = "Quantative Scheme In Slab"
        '
        'btnApply
        '
        Me.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnApply.Location = New System.Drawing.Point(320, 3)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(80, 18)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply"
        '
        'lblQuantum
        '
        Me.lblQuantum.FieldName = Nothing
        Me.lblQuantum.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantum.Location = New System.Drawing.Point(157, 4)
        Me.lblQuantum.Name = "lblQuantum"
        Me.lblQuantum.Size = New System.Drawing.Size(53, 16)
        Me.lblQuantum.TabIndex = 41
        Me.lblQuantum.Text = "Quantum"
        Me.lblQuantum.Visible = False
        '
        'txtQuantum
        '
        Me.txtQuantum.BackColor = System.Drawing.Color.White
        Me.txtQuantum.CalculationExpression = Nothing
        Me.txtQuantum.DecimalPlaces = 2
        Me.txtQuantum.FieldCode = Nothing
        Me.txtQuantum.FieldDesc = Nothing
        Me.txtQuantum.FieldMaxLength = 0
        Me.txtQuantum.FieldName = Nothing
        Me.txtQuantum.isCalculatedField = False
        Me.txtQuantum.IsSourceFromTable = False
        Me.txtQuantum.IsSourceFromValueList = False
        Me.txtQuantum.IsUnique = False
        Me.txtQuantum.Location = New System.Drawing.Point(216, 2)
        Me.txtQuantum.MendatroryField = False
        Me.txtQuantum.MyLinkLable1 = Nothing
        Me.txtQuantum.MyLinkLable2 = Nothing
        Me.txtQuantum.Name = "txtQuantum"
        Me.txtQuantum.ReferenceFieldDesc = Nothing
        Me.txtQuantum.ReferenceFieldName = Nothing
        Me.txtQuantum.ReferenceTableName = Nothing
        Me.txtQuantum.Size = New System.Drawing.Size(96, 20)
        Me.txtQuantum.TabIndex = 42
        Me.txtQuantum.Text = "0"
        Me.txtQuantum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQuantum.Value = 0R
        Me.txtQuantum.Visible = False
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.SplitContainer4)
        Me.RadGroupBox9.HeaderText = ""
        Me.RadGroupBox9.Location = New System.Drawing.Point(0, 51)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Size = New System.Drawing.Size(959, 310)
        Me.RadGroupBox9.TabIndex = 368
        Me.RadGroupBox9.Visible = False
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(2, 18)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gvTS)
        Me.SplitContainer4.Panel1MinSize = 40
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gvTS2)
        Me.SplitContainer4.Size = New System.Drawing.Size(955, 290)
        Me.SplitContainer4.SplitterDistance = 485
        Me.SplitContainer4.TabIndex = 0
        '
        'gvTS
        '
        Me.gvTS.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvTS.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTS.ForeColor = System.Drawing.Color.Black
        Me.gvTS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTS.Location = New System.Drawing.Point(0, 0)
        '
        'gvTS
        '
        Me.gvTS.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTS.Name = "gvTS"
        Me.gvTS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTS.ShowGroupPanel = False
        Me.gvTS.ShowHeaderCellButtons = True
        Me.gvTS.Size = New System.Drawing.Size(485, 290)
        Me.gvTS.TabIndex = 1
        Me.gvTS.TabStop = False
        Me.gvTS.Text = "RadGridView1"
        '
        'gvTS2
        '
        Me.gvTS2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvTS2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTS2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTS2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTS2.ForeColor = System.Drawing.Color.Black
        Me.gvTS2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTS2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvTS2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTS2.Name = "gvTS2"
        Me.gvTS2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTS2.ShowGroupPanel = False
        Me.gvTS2.ShowHeaderCellButtons = True
        Me.gvTS2.Size = New System.Drawing.Size(466, 290)
        Me.gvTS2.TabIndex = 2
        Me.gvTS2.TabStop = False
        Me.gvTS2.Text = "RadGridView1"
        '
        'chkSlabWise
        '
        Me.chkSlabWise.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkSlabWise.Location = New System.Drawing.Point(0, 0)
        Me.chkSlabWise.Name = "chkSlabWise"
        Me.chkSlabWise.Size = New System.Drawing.Size(959, 18)
        Me.chkSlabWise.TabIndex = 98
        Me.chkSlabWise.Text = "Apply Slab Wise"
        '
        'VolumeSlab
        '
        Me.VolumeSlab.Controls.Add(Me.SplitContainer5)
        Me.VolumeSlab.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.VolumeSlab.Location = New System.Drawing.Point(10, 37)
        Me.VolumeSlab.Name = "VolumeSlab"
        Me.VolumeSlab.Size = New System.Drawing.Size(959, 361)
        Me.VolumeSlab.Text = "Volume Slab"
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblStructUnit)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtstructUnit)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblStructDesc)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtStrcutCode)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.gvVolumeSlab)
        Me.SplitContainer5.Size = New System.Drawing.Size(959, 361)
        Me.SplitContainer5.SplitterDistance = 57
        Me.SplitContainer5.TabIndex = 0
        '
        'lblStructUnit
        '
        Me.lblStructUnit.AutoSize = False
        Me.lblStructUnit.BorderVisible = True
        Me.lblStructUnit.FieldName = Nothing
        Me.lblStructUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStructUnit.Location = New System.Drawing.Point(243, 29)
        Me.lblStructUnit.Name = "lblStructUnit"
        Me.lblStructUnit.Size = New System.Drawing.Size(703, 18)
        Me.lblStructUnit.TabIndex = 27
        Me.lblStructUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(4, 30)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel5.TabIndex = 26
        Me.MyLabel5.Text = "Unit"
        '
        'txtstructUnit
        '
        Me.txtstructUnit.CalculationExpression = Nothing
        Me.txtstructUnit.FieldCode = Nothing
        Me.txtstructUnit.FieldDesc = Nothing
        Me.txtstructUnit.FieldMaxLength = 0
        Me.txtstructUnit.FieldName = Nothing
        Me.txtstructUnit.isCalculatedField = False
        Me.txtstructUnit.IsSourceFromTable = False
        Me.txtstructUnit.IsSourceFromValueList = False
        Me.txtstructUnit.IsUnique = False
        Me.txtstructUnit.Location = New System.Drawing.Point(99, 29)
        Me.txtstructUnit.MendatroryField = True
        Me.txtstructUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstructUnit.MyLinkLable1 = Me.MyLabel5
        Me.txtstructUnit.MyLinkLable2 = Nothing
        Me.txtstructUnit.MyReadOnly = False
        Me.txtstructUnit.MyShowMasterFormButton = False
        Me.txtstructUnit.Name = "txtstructUnit"
        Me.txtstructUnit.ReferenceFieldDesc = Nothing
        Me.txtstructUnit.ReferenceFieldName = Nothing
        Me.txtstructUnit.ReferenceTableName = Nothing
        Me.txtstructUnit.Size = New System.Drawing.Size(138, 18)
        Me.txtstructUnit.TabIndex = 25
        Me.txtstructUnit.Value = ""
        '
        'lblStructDesc
        '
        Me.lblStructDesc.AutoSize = False
        Me.lblStructDesc.BorderVisible = True
        Me.lblStructDesc.FieldName = Nothing
        Me.lblStructDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStructDesc.Location = New System.Drawing.Point(243, 5)
        Me.lblStructDesc.Name = "lblStructDesc"
        Me.lblStructDesc.Size = New System.Drawing.Size(703, 18)
        Me.lblStructDesc.TabIndex = 24
        Me.lblStructDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "Structure Code"
        '
        'txtStrcutCode
        '
        Me.txtStrcutCode.CalculationExpression = Nothing
        Me.txtStrcutCode.FieldCode = Nothing
        Me.txtStrcutCode.FieldDesc = Nothing
        Me.txtStrcutCode.FieldMaxLength = 0
        Me.txtStrcutCode.FieldName = Nothing
        Me.txtStrcutCode.isCalculatedField = False
        Me.txtStrcutCode.IsSourceFromTable = False
        Me.txtStrcutCode.IsSourceFromValueList = False
        Me.txtStrcutCode.IsUnique = False
        Me.txtStrcutCode.Location = New System.Drawing.Point(99, 5)
        Me.txtStrcutCode.MendatroryField = True
        Me.txtStrcutCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStrcutCode.MyLinkLable1 = Me.MyLabel3
        Me.txtStrcutCode.MyLinkLable2 = Nothing
        Me.txtStrcutCode.MyReadOnly = False
        Me.txtStrcutCode.MyShowMasterFormButton = False
        Me.txtStrcutCode.Name = "txtStrcutCode"
        Me.txtStrcutCode.ReferenceFieldDesc = Nothing
        Me.txtStrcutCode.ReferenceFieldName = Nothing
        Me.txtStrcutCode.ReferenceTableName = Nothing
        Me.txtStrcutCode.Size = New System.Drawing.Size(138, 18)
        Me.txtStrcutCode.TabIndex = 18
        Me.txtStrcutCode.Value = ""
        '
        'gvVolumeSlab
        '
        Me.gvVolumeSlab.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvVolumeSlab.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvVolumeSlab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvVolumeSlab.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvVolumeSlab.ForeColor = System.Drawing.Color.Black
        Me.gvVolumeSlab.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvVolumeSlab.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvVolumeSlab.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVolumeSlab.Name = "gvVolumeSlab"
        Me.gvVolumeSlab.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvVolumeSlab.ShowGroupPanel = False
        Me.gvVolumeSlab.ShowHeaderCellButtons = True
        Me.gvVolumeSlab.Size = New System.Drawing.Size(959, 300)
        Me.gvVolumeSlab.TabIndex = 2
        Me.gvVolumeSlab.TabStop = False
        Me.gvVolumeSlab.Text = "RadGridView1"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer6)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(151.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(959, 361)
        Me.RadPageViewPage4.Text = "Cash Dis. And Volume Slab"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtRangeUnit)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel15)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtItemSturcture)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblCashDisunit)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer6.Panel1.Controls.Add(Me.TxtCashDisunit)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.gvCashDisGrid)
        Me.SplitContainer6.Size = New System.Drawing.Size(959, 361)
        Me.SplitContainer6.SplitterDistance = 76
        Me.SplitContainer6.TabIndex = 0
        '
        'txtRangeUnit
        '
        Me.txtRangeUnit.CalculationExpression = Nothing
        Me.txtRangeUnit.FieldCode = Nothing
        Me.txtRangeUnit.FieldDesc = Nothing
        Me.txtRangeUnit.FieldMaxLength = 0
        Me.txtRangeUnit.FieldName = Nothing
        Me.txtRangeUnit.isCalculatedField = False
        Me.txtRangeUnit.IsSourceFromTable = False
        Me.txtRangeUnit.IsSourceFromValueList = False
        Me.txtRangeUnit.IsUnique = False
        Me.txtRangeUnit.Location = New System.Drawing.Point(101, 55)
        Me.txtRangeUnit.MendatroryField = True
        Me.txtRangeUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRangeUnit.MyLinkLable1 = Me.MyLabel15
        Me.txtRangeUnit.MyLinkLable2 = Nothing
        Me.txtRangeUnit.MyReadOnly = False
        Me.txtRangeUnit.MyShowMasterFormButton = False
        Me.txtRangeUnit.Name = "txtRangeUnit"
        Me.txtRangeUnit.ReferenceFieldDesc = Nothing
        Me.txtRangeUnit.ReferenceFieldName = Nothing
        Me.txtRangeUnit.ReferenceTableName = Nothing
        Me.txtRangeUnit.Size = New System.Drawing.Size(142, 18)
        Me.txtRangeUnit.TabIndex = 47
        Me.txtRangeUnit.Value = ""
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(8, 56)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel15.TabIndex = 44
        Me.MyLabel15.Text = "Range Unit"
        '
        'txtItemSturcture
        '
        Me.txtItemSturcture.arrDispalyMember = Nothing
        Me.txtItemSturcture.arrValueMember = Nothing
        Me.txtItemSturcture.Location = New System.Drawing.Point(100, 12)
        Me.txtItemSturcture.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemSturcture.MyLinkLable1 = Nothing
        Me.txtItemSturcture.MyLinkLable2 = Nothing
        Me.txtItemSturcture.MyNullText = "Please Select"
        Me.txtItemSturcture.Name = "txtItemSturcture"
        Me.txtItemSturcture.Size = New System.Drawing.Size(687, 19)
        Me.txtItemSturcture.TabIndex = 41
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(9, 12)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel14.TabIndex = 40
        Me.MyLabel14.Text = "Item Structure "
        '
        'lblCashDisunit
        '
        Me.lblCashDisunit.AutoSize = False
        Me.lblCashDisunit.BorderVisible = True
        Me.lblCashDisunit.FieldName = Nothing
        Me.lblCashDisunit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashDisunit.Location = New System.Drawing.Point(247, 35)
        Me.lblCashDisunit.Name = "lblCashDisunit"
        Me.lblCashDisunit.Size = New System.Drawing.Size(540, 18)
        Me.lblCashDisunit.TabIndex = 30
        Me.lblCashDisunit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(8, 36)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel13.TabIndex = 29
        Me.MyLabel13.Text = "Unit"
        '
        'TxtCashDisunit
        '
        Me.TxtCashDisunit.CalculationExpression = Nothing
        Me.TxtCashDisunit.FieldCode = Nothing
        Me.TxtCashDisunit.FieldDesc = Nothing
        Me.TxtCashDisunit.FieldMaxLength = 0
        Me.TxtCashDisunit.FieldName = Nothing
        Me.TxtCashDisunit.isCalculatedField = False
        Me.TxtCashDisunit.IsSourceFromTable = False
        Me.TxtCashDisunit.IsSourceFromValueList = False
        Me.TxtCashDisunit.IsUnique = False
        Me.TxtCashDisunit.Location = New System.Drawing.Point(101, 35)
        Me.TxtCashDisunit.MendatroryField = True
        Me.TxtCashDisunit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCashDisunit.MyLinkLable1 = Me.MyLabel13
        Me.TxtCashDisunit.MyLinkLable2 = Nothing
        Me.TxtCashDisunit.MyReadOnly = False
        Me.TxtCashDisunit.MyShowMasterFormButton = False
        Me.TxtCashDisunit.Name = "TxtCashDisunit"
        Me.TxtCashDisunit.ReferenceFieldDesc = Nothing
        Me.TxtCashDisunit.ReferenceFieldName = Nothing
        Me.TxtCashDisunit.ReferenceTableName = Nothing
        Me.TxtCashDisunit.Size = New System.Drawing.Size(143, 18)
        Me.TxtCashDisunit.TabIndex = 28
        Me.TxtCashDisunit.Value = ""
        '
        'gvCashDisGrid
        '
        Me.gvCashDisGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCashDisGrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCashDisGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCashDisGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCashDisGrid.ForeColor = System.Drawing.Color.Black
        Me.gvCashDisGrid.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCashDisGrid.Location = New System.Drawing.Point(0, 0)
        '
        'gvCashDisGrid
        '
        Me.gvCashDisGrid.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCashDisGrid.Name = "gvCashDisGrid"
        Me.gvCashDisGrid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCashDisGrid.ShowGroupPanel = False
        Me.gvCashDisGrid.ShowHeaderCellButtons = True
        Me.gvCashDisGrid.Size = New System.Drawing.Size(959, 281)
        Me.gvCashDisGrid.TabIndex = 3
        Me.gvCashDisGrid.TabStop = False
        Me.gvCashDisGrid.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(897, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(89, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmSchemeMasterDairy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 459)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSchemeMasterDairy"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Scheme Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_Apply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuantitiveStructureFreeQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuantitiveStructureMainQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTargetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTargetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMainItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpScheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpQuantiiveType.ResumeLayout(False)
        Me.GrpQuantiiveType.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboQuantitiveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.pgAttachmentSchMst.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ChkQuantativeSch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQuantum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuantum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gvTS.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTS2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTS2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSlabWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VolumeSlab.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.lblStructUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStructDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVolumeSlab.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVolumeSlab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCashDisunit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCashDisGrid.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCashDisGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndScheme As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUnitCode As common.UserControls.txtFinder
    Friend WithEvents lblUnit As common.Controls.MyLabel
    Friend WithEvents txtMainItemGrp As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlBasicPrice As common.Controls.MyComboBox
    Friend WithEvents ddlmrp As common.Controls.MyComboBox
    Friend WithEvents lblmrp As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblQty As common.Controls.MyLabel
    Friend WithEvents dtpInactive As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpScheme As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents grdScheme As Telerik.WinControls.UI.RadGridView
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblMainItemDesc As common.Controls.MyLabel
    Friend WithEvents lblUnitCodeDesc As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.MyNumBox
    Friend WithEvents txtQty As common.MyNumBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddlCriteria As common.Controls.MyComboBox
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents gvCustomer As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvItem As Telerik.WinControls.UI.RadGridView
    Friend WithEvents lblCriteria As common.Controls.MyLabel
    Friend WithEvents txtCriteria As common.UserControls.txtFinder
    Friend WithEvents txtPercentage As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents export_main As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents export_beneficial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_Criteria As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import_Main As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents import_beneficial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents import_criteria As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnItemSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents pgAttachmentSchMst As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblTargetType As common.Controls.MyLabel
    Friend WithEvents ddlTargetType As common.Controls.MyComboBox
    Friend WithEvents rmWholeSheetExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImportWholeSheet As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkSlabWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gvTS As common.UserControls.MyRadGridView
    Friend WithEvents rmSchemeWithSlabExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSchemeWithSlabImp As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtQuantum As common.MyNumBox
    Friend WithEvents lblQuantum As common.Controls.MyLabel
    Friend WithEvents ChkQuantativeSch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvTS2 As common.UserControls.MyRadGridView
    Friend WithEvents btnApply As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents VolumeSlab As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtStrcutCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblStructDesc As common.Controls.MyLabel
    Friend WithEvents gvVolumeSlab As common.UserControls.MyRadGridView
    Friend WithEvents lblStructUnit As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtstructUnit As common.UserControls.txtFinder
    Friend WithEvents GrpQuantiiveType As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cboQuantitiveType As common.Controls.MyComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuantitiveStructureFreeUOM As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtQuantitiveStructureFreeQty As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtQuantitiveStructureFreeICode As common.UserControls.txtFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtQuantitiveStructureMainUOM As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtQuantitiveStructureMainQty As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtQuantitiveStructureCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btn_Apply As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents SplitContainer6 As SplitContainer
    Friend WithEvents lblCashDisunit As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents TxtCashDisunit As common.UserControls.txtFinder
    Friend WithEvents gvCashDisGrid As common.UserControls.MyRadGridView
    Friend WithEvents txtItemSturcture As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtRangeUnit As common.UserControls.txtFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
End Class

