<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptSaleRegisterReportForAdv
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkShowCSASaleFromSalePatti = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_amtinlacs = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkQuickLoad = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkFarmerSale = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeDebitCredit = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblType = New common.Controls.MyLabel()
        Me.ddlSubCategory = New Telerik.WinControls.UI.RadDropDownList()
        Me.ddlReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblSubCategory = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnUnposted = New Telerik.WinControls.UI.RadRadioButton()
        Me.chk_stockingunit = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSerializeInv = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtTransaction = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtState = New common.UserControls.txtMultiSelectFinder()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtCustGroup = New common.UserControls.txtMultiSelectFinder()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtItemGroup = New common.UserControls.txtMultiSelectFinder()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCreateExportTemplate = New Telerik.WinControls.UI.RadButton()
        Me.btnQuickExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.QExpCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.radbtnBulkExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.BulkExportCsv = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExportXls = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton5 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadSplitButton2 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkShowCSASaleFromSalePatti, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_amtinlacs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkQuickLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFarmerSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeDebitCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_stockingunit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreateExportTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCreateExportTemplate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.radbtnBulkExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1044, 495)
        Me.SplitContainer1.SplitterDistance = 457
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1044, 437)
        Me.RadPageView1.TabIndex = 71
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1023, 389)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkShowCSASaleFromSalePatti)
        Me.Panel1.Controls.Add(Me.chk_amtinlacs)
        Me.Panel1.Controls.Add(Me.chkQuickLoad)
        Me.Panel1.Controls.Add(Me.txtUOM)
        Me.Panel1.Controls.Add(Me.chkFarmerSale)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.chkIncludeDebitCredit)
        Me.Panel1.Controls.Add(Me.lblType)
        Me.Panel1.Controls.Add(Me.ddlSubCategory)
        Me.Panel1.Controls.Add(Me.ddlReportType)
        Me.Panel1.Controls.Add(Me.lblSubCategory)
        Me.Panel1.Controls.Add(Me.RadGroupBox6)
        Me.Panel1.Controls.Add(Me.chk_stockingunit)
        Me.Panel1.Controls.Add(Me.chkSerializeInv)
        Me.Panel1.Controls.Add(Me.txtTransaction)
        Me.Panel1.Controls.Add(Me.MyLabel8)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.txtState)
        Me.Panel1.Controls.Add(Me.txtItem)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.txtCustGroup)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.MyLabel6)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtItemGroup)
        Me.Panel1.Controls.Add(Me.txtCustomer)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Location = New System.Drawing.Point(332, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(688, 339)
        Me.Panel1.TabIndex = 348
        '
        'chkShowCSASaleFromSalePatti
        '
        Me.chkShowCSASaleFromSalePatti.Location = New System.Drawing.Point(400, 22)
        Me.chkShowCSASaleFromSalePatti.Name = "chkShowCSASaleFromSalePatti"
        Me.chkShowCSASaleFromSalePatti.Size = New System.Drawing.Size(171, 18)
        Me.chkShowCSASaleFromSalePatti.TabIndex = 353
        Me.chkShowCSASaleFromSalePatti.Text = "Show CSA Sale from Sale Patti"
        Me.chkShowCSASaleFromSalePatti.Visible = False
        '
        'chk_amtinlacs
        '
        Me.chk_amtinlacs.Location = New System.Drawing.Point(109, 3)
        Me.chk_amtinlacs.Name = "chk_amtinlacs"
        Me.chk_amtinlacs.Size = New System.Drawing.Size(107, 18)
        Me.chk_amtinlacs.TabIndex = 341
        Me.chk_amtinlacs.Text = "Amount (In Lacs.)"
        '
        'chkQuickLoad
        '
        Me.chkQuickLoad.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkQuickLoad.Location = New System.Drawing.Point(599, 24)
        Me.chkQuickLoad.Name = "chkQuickLoad"
        Me.chkQuickLoad.Size = New System.Drawing.Size(76, 18)
        Me.chkQuickLoad.TabIndex = 347
        Me.chkQuickLoad.Text = "Quick Load"
        Me.chkQuickLoad.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkQuickLoad.Visible = False
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(109, 24)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.MyLabel1
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(192, 19)
        Me.txtUOM.TabIndex = 313
        Me.txtUOM.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(10, 27)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 314
        Me.MyLabel1.Text = "UOM"
        '
        'chkFarmerSale
        '
        Me.chkFarmerSale.Location = New System.Drawing.Point(437, 49)
        Me.chkFarmerSale.Name = "chkFarmerSale"
        Me.chkFarmerSale.Size = New System.Drawing.Size(162, 18)
        Me.chkFarmerSale.TabIndex = 346
        Me.chkFarmerSale.Text = "Include Farmer Material Sale"
        Me.chkFarmerSale.Visible = False
        '
        'chkIncludeDebitCredit
        '
        Me.chkIncludeDebitCredit.Location = New System.Drawing.Point(307, 49)
        Me.chkIncludeDebitCredit.Name = "chkIncludeDebitCredit"
        Me.chkIncludeDebitCredit.Size = New System.Drawing.Size(121, 18)
        Me.chkIncludeDebitCredit.TabIndex = 345
        Me.chkIncludeDebitCredit.Text = "Include Debit/Credit"
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(10, 51)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(67, 18)
        Me.lblType.TabIndex = 323
        Me.lblType.Text = "Report Type"
        '
        'ddlSubCategory
        '
        Me.ddlSubCategory.AutoCompleteDisplayMember = Nothing
        Me.ddlSubCategory.AutoCompleteValueMember = Nothing
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Sale Invoice"
        RadListDataItem3.Text = "Sale Return"
        Me.ddlSubCategory.Items.Add(RadListDataItem1)
        Me.ddlSubCategory.Items.Add(RadListDataItem2)
        Me.ddlSubCategory.Items.Add(RadListDataItem3)
        Me.ddlSubCategory.Location = New System.Drawing.Point(565, 76)
        Me.ddlSubCategory.Name = "ddlSubCategory"
        Me.ddlSubCategory.Size = New System.Drawing.Size(121, 20)
        Me.ddlSubCategory.TabIndex = 344
        Me.ddlSubCategory.Visible = False
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        RadListDataItem4.Text = "Both"
        RadListDataItem5.Text = "Sale Invoice"
        RadListDataItem6.Text = "Sale Return"
        Me.ddlReportType.Items.Add(RadListDataItem4)
        Me.ddlReportType.Items.Add(RadListDataItem5)
        Me.ddlReportType.Items.Add(RadListDataItem6)
        Me.ddlReportType.Location = New System.Drawing.Point(109, 49)
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.Size = New System.Drawing.Size(171, 20)
        Me.ddlReportType.TabIndex = 0
        '
        'lblSubCategory
        '
        Me.lblSubCategory.FieldName = Nothing
        Me.lblSubCategory.Location = New System.Drawing.Point(424, 76)
        Me.lblSubCategory.Name = "lblSubCategory"
        Me.lblSubCategory.Size = New System.Drawing.Size(134, 18)
        Me.lblSubCategory.TabIndex = 343
        Me.lblSubCategory.Text = "Transaction Sub Category"
        Me.lblSubCategory.Visible = False
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.btnAll)
        Me.RadGroupBox6.Controls.Add(Me.btnPosted)
        Me.RadGroupBox6.Controls.Add(Me.btnUnposted)
        Me.RadGroupBox6.HeaderText = "Status"
        Me.RadGroupBox6.Location = New System.Drawing.Point(83, 251)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(228, 37)
        Me.RadGroupBox6.TabIndex = 321
        Me.RadGroupBox6.Text = "Status"
        Me.RadGroupBox6.Visible = False
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(13, 11)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(33, 18)
        Me.btnAll.TabIndex = 308
        Me.btnAll.Text = "All"
        '
        'btnPosted
        '
        Me.btnPosted.Location = New System.Drawing.Point(73, 10)
        Me.btnPosted.Name = "btnPosted"
        Me.btnPosted.Size = New System.Drawing.Size(54, 18)
        Me.btnPosted.TabIndex = 306
        Me.btnPosted.Text = "Posted"
        '
        'btnUnposted
        '
        Me.btnUnposted.Location = New System.Drawing.Point(148, 10)
        Me.btnUnposted.Name = "btnUnposted"
        Me.btnUnposted.Size = New System.Drawing.Size(69, 18)
        Me.btnUnposted.TabIndex = 307
        Me.btnUnposted.Text = "Unposted"
        '
        'chk_stockingunit
        '
        Me.chk_stockingunit.Location = New System.Drawing.Point(307, 24)
        Me.chk_stockingunit.Name = "chk_stockingunit"
        Me.chk_stockingunit.Size = New System.Drawing.Size(87, 18)
        Me.chk_stockingunit.TabIndex = 342
        Me.chk_stockingunit.Text = "Stocking Unit"
        '
        'chkSerializeInv
        '
        Me.chkSerializeInv.Location = New System.Drawing.Point(317, 266)
        Me.chkSerializeInv.Name = "chkSerializeInv"
        Me.chkSerializeInv.Size = New System.Drawing.Size(111, 18)
        Me.chkSerializeInv.TabIndex = 322
        Me.chkSerializeInv.Text = "Serialize Inventory"
        Me.chkSerializeInv.Visible = False
        '
        'txtTransaction
        '
        Me.txtTransaction.arrDispalyMember = Nothing
        Me.txtTransaction.arrValueMember = Nothing
        Me.txtTransaction.Location = New System.Drawing.Point(109, 75)
        Me.txtTransaction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransaction.MyLinkLable1 = Me.MyLabel5
        Me.txtTransaction.MyLinkLable2 = Nothing
        Me.txtTransaction.MyNullText = "All"
        Me.txtTransaction.Name = "txtTransaction"
        Me.txtTransaction.Size = New System.Drawing.Size(309, 19)
        Me.txtTransaction.TabIndex = 327
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 76)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel5.TabIndex = 328
        Me.MyLabel5.Text = "Transaction"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(10, 99)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel8.TabIndex = 340
        Me.MyLabel8.Text = "State"
        '
        'txtState
        '
        Me.txtState.arrDispalyMember = Nothing
        Me.txtState.arrValueMember = Nothing
        Me.txtState.Location = New System.Drawing.Point(109, 98)
        Me.txtState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.MyLinkLable1 = Me.MyLabel8
        Me.txtState.MyLinkLable2 = Nothing
        Me.txtState.MyNullText = "All"
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(309, 19)
        Me.txtState.TabIndex = 339
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(109, 171)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel4
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(309, 19)
        Me.txtItem.TabIndex = 329
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 172)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel4.TabIndex = 330
        Me.MyLabel4.Text = "Item"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(10, 197)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel7.TabIndex = 338
        Me.MyLabel7.Text = "Customer Group"
        '
        'txtCustGroup
        '
        Me.txtCustGroup.arrDispalyMember = Nothing
        Me.txtCustGroup.arrValueMember = Nothing
        Me.txtCustGroup.Location = New System.Drawing.Point(109, 196)
        Me.txtCustGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustGroup.MyLinkLable1 = Me.MyLabel7
        Me.txtCustGroup.MyLinkLable2 = Nothing
        Me.txtCustGroup.MyNullText = "All"
        Me.txtCustGroup.Name = "txtCustGroup"
        Me.txtCustGroup.Size = New System.Drawing.Size(309, 19)
        Me.txtCustGroup.TabIndex = 337
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(109, 121)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(309, 19)
        Me.txtLocation.TabIndex = 331
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 122)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 332
        Me.MyLabel2.Text = "Location"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(10, 146)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel6.TabIndex = 336
        Me.MyLabel6.Text = "Item Group"
        '
        'txtItemGroup
        '
        Me.txtItemGroup.arrDispalyMember = Nothing
        Me.txtItemGroup.arrValueMember = Nothing
        Me.txtItemGroup.Location = New System.Drawing.Point(109, 146)
        Me.txtItemGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemGroup.MyLinkLable1 = Me.MyLabel6
        Me.txtItemGroup.MyLinkLable2 = Nothing
        Me.txtItemGroup.MyNullText = "All"
        Me.txtItemGroup.Name = "txtItemGroup"
        Me.txtItemGroup.Size = New System.Drawing.Size(309, 19)
        Me.txtItemGroup.TabIndex = 335
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(109, 219)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(309, 19)
        Me.txtCustomer.TabIndex = 333
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 220)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 334
        Me.MyLabel3.Text = "Customer"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox7.Controls.Add(Me.gvCategory)
        Me.RadGroupBox7.Controls.Add(Me.Panel6)
        Me.RadGroupBox7.HeaderText = "Category"
        Me.RadGroupBox7.Location = New System.Drawing.Point(16, 51)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(310, 291)
        Me.RadGroupBox7.TabIndex = 326
        Me.RadGroupBox7.Text = "Category"
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(10, 40)
        '
        'gvCategory
        '
        Me.gvCategory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.ShowHeaderCellButtons = True
        Me.gvCategory.Size = New System.Drawing.Size(290, 241)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.Text = "RadGridView1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel6.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(290, 20)
        Me.Panel6.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(140, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 1
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(101, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 1)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(310, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(157, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(78, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(78, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1023, 389)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1023, 389)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(1044, 20)
        Me.rdmenufile.TabIndex = 70
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visible = False
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnCreateExportTemplate
        '
        Me.btnCreateExportTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateExportTemplate.Location = New System.Drawing.Point(607, 9)
        Me.btnCreateExportTemplate.Name = "btnCreateExportTemplate"
        Me.btnCreateExportTemplate.Size = New System.Drawing.Size(106, 18)
        Me.btnCreateExportTemplate.TabIndex = 160
        Me.btnCreateExportTemplate.Text = "Manage Template"
        '
        'btnQuickExport
        '
        Me.btnQuickExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.QExpCSV})
        Me.btnQuickExport.Location = New System.Drawing.Point(395, 9)
        Me.btnQuickExport.Name = "btnQuickExport"
        Me.btnQuickExport.Size = New System.Drawing.Size(103, 18)
        Me.btnQuickExport.TabIndex = 159
        Me.btnQuickExport.Text = "Quick Export"
        '
        'QExpExcel
        '
        Me.QExpExcel.AccessibleDescription = "Excel"
        Me.QExpExcel.AccessibleName = "Excel"
        Me.QExpExcel.Name = "QExpExcel"
        Me.QExpExcel.Text = "Excel"
        '
        'QExpCSV
        '
        Me.QExpCSV.AccessibleDescription = "CSV"
        Me.QExpCSV.AccessibleName = "CSV"
        Me.QExpCSV.Name = "QExpCSV"
        Me.QExpCSV.Text = "CSV"
        '
        'radbtnBulkExp
        '
        Me.radbtnBulkExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.radbtnBulkExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BulkExportCsv, Me.BulkExportXls})
        Me.radbtnBulkExp.Location = New System.Drawing.Point(501, 9)
        Me.radbtnBulkExp.Name = "radbtnBulkExp"
        Me.radbtnBulkExp.Size = New System.Drawing.Size(100, 18)
        Me.radbtnBulkExp.TabIndex = 158
        Me.radbtnBulkExp.Text = "Bulk Export"
        '
        'BulkExportCsv
        '
        Me.BulkExportCsv.AccessibleDescription = "CSV"
        Me.BulkExportCsv.AccessibleName = "CSV"
        Me.BulkExportCsv.Name = "BulkExportCsv"
        Me.BulkExportCsv.Text = "CSV"
        '
        'BulkExportXls
        '
        Me.BulkExportXls.AccessibleDescription = "Excel"
        Me.BulkExportXls.AccessibleName = "Excel"
        Me.BulkExportXls.Name = "BulkExportXls"
        Me.BulkExportXls.Text = "Excel"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(154, 9)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(65, 18)
        Me.btnBack.TabIndex = 155
        Me.btnBack.Text = "<< Back "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(964, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 154
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(225, 9)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton1.TabIndex = 153
        Me.RadSplitButton1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Excel"
        Me.RadMenuItem2.AccessibleName = "Excel"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Excel"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(6, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 150
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(80, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 149
        Me.btnReset.Text = "Reset"
        '
        'RadSplitButton5
        '
        Me.RadSplitButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton5.Location = New System.Drawing.Point(-805, 0)
        Me.RadSplitButton5.Name = "RadSplitButton5"
        Me.RadSplitButton5.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton5.TabIndex = 151
        Me.RadSplitButton5.Text = "Export"
        '
        'RadSplitButton2
        '
        Me.RadSplitButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSetting, Me.rmSend})
        Me.RadSplitButton2.Location = New System.Drawing.Point(311, 9)
        Me.RadSplitButton2.Name = "RadSplitButton2"
        Me.RadSplitButton2.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton2.TabIndex = 152
        Me.RadSplitButton2.Text = "E-Mail/SMS"
        '
        'rmSetting
        '
        Me.rmSetting.AccessibleDescription = "EMail/SMS Setting"
        Me.rmSetting.AccessibleName = "EMail/SMS Setting"
        Me.rmSetting.Name = "rmSetting"
        Me.rmSetting.Text = "EMail/SMS Setting"
        '
        'rmSend
        '
        Me.rmSend.AccessibleDescription = "EMail/SMS Send"
        Me.rmSend.AccessibleName = "EMail/SMS Send"
        Me.rmSend.Name = "rmSend"
        Me.rmSend.Text = "EMail/SMS Send"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'RptSaleRegisterReportForAdv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 495)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptSaleRegisterReportForAdv"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptSaleRegisterReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkShowCSASaleFromSalePatti, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_amtinlacs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkQuickLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFarmerSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeDebitCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_stockingunit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreateExportTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnUnposted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ddlReportType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton5 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadSplitButton2 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents chkSerializeInv As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtTransaction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtItemGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCustGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtState As common.UserControls.txtMultiSelectFinder
    Friend WithEvents radbtnBulkExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BulkExportCsv As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExportXls As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chk_amtinlacs As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_stockingunit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ddlSubCategory As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblSubCategory As common.Controls.MyLabel
    Friend WithEvents chkIncludeDebitCredit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFarmerSale As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkQuickLoad As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents QExpCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkShowCSASaleFromSalePatti As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnCreateExportTemplate As Telerik.WinControls.UI.RadButton
End Class

