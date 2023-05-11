<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptFreshSaleRegister1
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GbCustomerGroup = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomerGroup = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rbtnCustomerGroupSelect = New common.Controls.MyRadioButton()
        Me.rbtnCustomerGroupAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.tvCategory = New Telerik.WinControls.UI.RadTreeView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.chkSerializeInv = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnUnposted = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkTax = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkRetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.ddlSaleType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.GrpItem = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbtnItemSelect = New common.Controls.MyRadioButton()
        Me.rbtnItemAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomer = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnCustomerSelect = New common.Controls.MyRadioButton()
        Me.rbtnCustomerAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.rbtnSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rbtnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.GbCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbCustomerGroup.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.rbtnCustomerGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustomerGroupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSaleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpItem.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1028, 585)
        Me.SplitContainer1.SplitterDistance = 547
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
        Me.RadPageView1.Size = New System.Drawing.Size(1028, 547)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GbCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.chkSerializeInv)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.ddlSaleType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtUOM)
        Me.RadPageViewPage1.Controls.Add(Me.GrpItem)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1007, 499)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'GbCustomerGroup
        '
        Me.GbCustomerGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GbCustomerGroup.Controls.Add(Me.cbgCustomerGroup)
        Me.GbCustomerGroup.Controls.Add(Me.Panel5)
        Me.GbCustomerGroup.HeaderText = "Customer Group"
        Me.GbCustomerGroup.Location = New System.Drawing.Point(758, 337)
        Me.GbCustomerGroup.Name = "GbCustomerGroup"
        Me.GbCustomerGroup.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GbCustomerGroup.Size = New System.Drawing.Size(142, 39)
        Me.GbCustomerGroup.TabIndex = 325
        Me.GbCustomerGroup.Text = "Customer Group"
        Me.GbCustomerGroup.Visible = False
        '
        'cbgCustomerGroup
        '
        Me.cbgCustomerGroup.CheckedValue = Nothing
        Me.cbgCustomerGroup.DataSource = Nothing
        Me.cbgCustomerGroup.DisplayMember = "Name"
        Me.cbgCustomerGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomerGroup.Location = New System.Drawing.Point(10, 43)
        Me.cbgCustomerGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomerGroup.MyShowHeadrText = False
        Me.cbgCustomerGroup.Name = "cbgCustomerGroup"
        Me.cbgCustomerGroup.Size = New System.Drawing.Size(122, 0)
        Me.cbgCustomerGroup.TabIndex = 3
        Me.cbgCustomerGroup.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rbtnCustomerGroupSelect)
        Me.Panel5.Controls.Add(Me.rbtnCustomerGroupAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(122, 23)
        Me.Panel5.TabIndex = 1
        '
        'rbtnCustomerGroupSelect
        '
        Me.rbtnCustomerGroupSelect.Location = New System.Drawing.Point(175, 3)
        Me.rbtnCustomerGroupSelect.MyLinkLable1 = Nothing
        Me.rbtnCustomerGroupSelect.MyLinkLable2 = Nothing
        Me.rbtnCustomerGroupSelect.Name = "rbtnCustomerGroupSelect"
        Me.rbtnCustomerGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCustomerGroupSelect.TabIndex = 1
        Me.rbtnCustomerGroupSelect.Text = "Select"
        '
        'rbtnCustomerGroupAll
        '
        Me.rbtnCustomerGroupAll.Location = New System.Drawing.Point(129, 3)
        Me.rbtnCustomerGroupAll.MyLinkLable1 = Nothing
        Me.rbtnCustomerGroupAll.MyLinkLable2 = Nothing
        Me.rbtnCustomerGroupAll.Name = "rbtnCustomerGroupAll"
        Me.rbtnCustomerGroupAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCustomerGroupAll.TabIndex = 0
        Me.rbtnCustomerGroupAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.tvCategory)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Category"
        Me.RadGroupBox2.Location = New System.Drawing.Point(899, 102)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(94, 63)
        Me.RadGroupBox2.TabIndex = 319
        Me.RadGroupBox2.Text = "Category"
        Me.RadGroupBox2.Visible = False
        '
        'tvCategory
        '
        Me.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCategory.Location = New System.Drawing.Point(10, 40)
        Me.tvCategory.Name = "tvCategory"
        Me.tvCategory.Size = New System.Drawing.Size(74, 13)
        Me.tvCategory.SpacingBetweenNodes = -1
        Me.tvCategory.TabIndex = 70
        Me.tvCategory.Text = "RadTreeView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel4.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(74, 20)
        Me.Panel4.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(34, 1)
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
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(-17, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'chkSerializeInv
        '
        Me.chkSerializeInv.Location = New System.Drawing.Point(320, 61)
        Me.chkSerializeInv.Name = "chkSerializeInv"
        Me.chkSerializeInv.Size = New System.Drawing.Size(111, 18)
        Me.chkSerializeInv.TabIndex = 322
        Me.chkSerializeInv.Text = "Serialize Inventory"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.btnAll)
        Me.RadGroupBox6.Controls.Add(Me.btnPosted)
        Me.RadGroupBox6.Controls.Add(Me.btnUnposted)
        Me.RadGroupBox6.HeaderText = "Status"
        Me.RadGroupBox6.Location = New System.Drawing.Point(672, 6)
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
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkTax)
        Me.RadGroupBox4.Controls.Add(Me.chkRetail)
        Me.RadGroupBox4.Controls.Add(Me.chkAll)
        Me.RadGroupBox4.HeaderText = "Invoice Type"
        Me.RadGroupBox4.Location = New System.Drawing.Point(445, 6)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(221, 37)
        Me.RadGroupBox4.TabIndex = 320
        Me.RadGroupBox4.Text = "Invoice Type"
        '
        'chkTax
        '
        Me.chkTax.Location = New System.Drawing.Point(13, 12)
        Me.chkTax.Name = "chkTax"
        Me.chkTax.Size = New System.Drawing.Size(37, 18)
        Me.chkTax.TabIndex = 1
        Me.chkTax.Text = "Tax"
        '
        'chkRetail
        '
        Me.chkRetail.Location = New System.Drawing.Point(88, 12)
        Me.chkRetail.Name = "chkRetail"
        Me.chkRetail.Size = New System.Drawing.Size(48, 18)
        Me.chkRetail.TabIndex = 0
        Me.chkRetail.Text = "Retail"
        '
        'chkAll
        '
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Location = New System.Drawing.Point(174, 12)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 1
        Me.chkAll.Text = "All"
        Me.chkAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'ddlSaleType
        '
        Me.ddlSaleType.AutoCompleteDisplayMember = Nothing
        Me.ddlSaleType.AutoCompleteValueMember = Nothing
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Sale Invoice"
        RadListDataItem3.Text = "Sale Return"
        Me.ddlSaleType.Items.Add(RadListDataItem1)
        Me.ddlSaleType.Items.Add(RadListDataItem2)
        Me.ddlSaleType.Items.Add(RadListDataItem3)
        Me.ddlSaleType.Location = New System.Drawing.Point(199, 59)
        Me.ddlSaleType.Name = "ddlSaleType"
        Me.ddlSaleType.Size = New System.Drawing.Size(108, 20)
        Me.ddlSaleType.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(16, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 314
        Me.MyLabel1.Text = "UOM"
        '
        'txtUOM
        '
        Me.txtUOM.Location = New System.Drawing.Point(59, 58)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.MyLabel1
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.Size = New System.Drawing.Size(132, 19)
        Me.txtUOM.TabIndex = 313
        Me.txtUOM.Value = ""
        '
        'GrpItem
        '
        Me.GrpItem.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpItem.Controls.Add(Me.cbgItem)
        Me.GrpItem.Controls.Add(Me.Panel3)
        Me.GrpItem.HeaderText = "Item"
        Me.GrpItem.Location = New System.Drawing.Point(363, 93)
        Me.GrpItem.Name = "GrpItem"
        Me.GrpItem.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpItem.Size = New System.Drawing.Size(342, 206)
        Me.GrpItem.TabIndex = 312
        Me.GrpItem.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 43)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(322, 153)
        Me.cbgItem.TabIndex = 4
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnItemSelect)
        Me.Panel3.Controls.Add(Me.rbtnItemAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(322, 23)
        Me.Panel3.TabIndex = 3
        '
        'rbtnItemSelect
        '
        Me.rbtnItemSelect.Location = New System.Drawing.Point(162, 3)
        Me.rbtnItemSelect.MyLinkLable1 = Nothing
        Me.rbtnItemSelect.MyLinkLable2 = Nothing
        Me.rbtnItemSelect.Name = "rbtnItemSelect"
        Me.rbtnItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnItemSelect.TabIndex = 1
        Me.rbtnItemSelect.Text = "Select"
        '
        'rbtnItemAll
        '
        Me.rbtnItemAll.Location = New System.Drawing.Point(116, 3)
        Me.rbtnItemAll.MyLinkLable1 = Nothing
        Me.rbtnItemAll.MyLinkLable2 = Nothing
        Me.rbtnItemAll.Name = "rbtnItemAll"
        Me.rbtnItemAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnItemAll.TabIndex = 0
        Me.rbtnItemAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(16, 304)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(339, 200)
        Me.RadGroupBox5.TabIndex = 311
        Me.RadGroupBox5.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 43)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(319, 147)
        Me.cbgLocation.TabIndex = 3
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnLocationSelect)
        Me.Panel2.Controls.Add(Me.rbtnLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(319, 23)
        Me.Panel2.TabIndex = 1
        '
        'rbtnLocationSelect
        '
        Me.rbtnLocationSelect.Location = New System.Drawing.Point(167, 3)
        Me.rbtnLocationSelect.MyLinkLable1 = Nothing
        Me.rbtnLocationSelect.MyLinkLable2 = Nothing
        Me.rbtnLocationSelect.Name = "rbtnLocationSelect"
        Me.rbtnLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocationSelect.TabIndex = 1
        Me.rbtnLocationSelect.Text = "Select"
        '
        'rbtnLocationAll
        '
        Me.rbtnLocationAll.Location = New System.Drawing.Point(121, 3)
        Me.rbtnLocationAll.MyLinkLable1 = Nothing
        Me.rbtnLocationAll.MyLinkLable2 = Nothing
        Me.rbtnLocationAll.Name = "rbtnLocationAll"
        Me.rbtnLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocationAll.TabIndex = 0
        Me.rbtnLocationAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox1.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(275, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(156, 37)
        Me.RadGroupBox1.TabIndex = 53
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(13, 9)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 306
        Me.rdbSummary.Text = "Summary"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(88, 9)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 307
        Me.rdbDetail.Text = "Detail"
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
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
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
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel1)
        Me.RadGroupBox8.HeaderText = "Customer"
        Me.RadGroupBox8.Location = New System.Drawing.Point(16, 93)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(339, 206)
        Me.RadGroupBox8.TabIndex = 305
        Me.RadGroupBox8.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 43)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(319, 153)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCustomerSelect)
        Me.Panel1.Controls.Add(Me.rbtnCustomerAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(319, 23)
        Me.Panel1.TabIndex = 0
        '
        'rbtnCustomerSelect
        '
        Me.rbtnCustomerSelect.Location = New System.Drawing.Point(193, 4)
        Me.rbtnCustomerSelect.MyLinkLable1 = Nothing
        Me.rbtnCustomerSelect.MyLinkLable2 = Nothing
        Me.rbtnCustomerSelect.Name = "rbtnCustomerSelect"
        Me.rbtnCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCustomerSelect.TabIndex = 1
        Me.rbtnCustomerSelect.Text = "Select"
        '
        'rbtnCustomerAll
        '
        Me.rbtnCustomerAll.Location = New System.Drawing.Point(147, 4)
        Me.rbtnCustomerAll.MyLinkLable1 = Nothing
        Me.rbtnCustomerAll.MyLinkLable2 = Nothing
        Me.rbtnCustomerAll.Name = "rbtnCustomerAll"
        Me.rbtnCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCustomerAll.TabIndex = 0
        Me.rbtnCustomerAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1007, 499)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Name = "Gv1"
        Me.Gv1.Size = New System.Drawing.Size(1007, 499)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'btnQuickExport
        '
        Me.btnQuickExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnQuickExport.Location = New System.Drawing.Point(330, 7)
        Me.btnQuickExport.Name = "btnQuickExport"
        Me.btnQuickExport.Size = New System.Drawing.Size(95, 19)
        Me.btnQuickExport.TabIndex = 135
        Me.btnQuickExport.Text = "Quick Export"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rbtnSetting, Me.rbtnSend})
        Me.btnsetting.Location = New System.Drawing.Point(244, 8)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(80, 18)
        Me.btnsetting.TabIndex = 134
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'rbtnSetting
        '
        Me.rbtnSetting.AccessibleDescription = "EMail/SMS Setting"
        Me.rbtnSetting.AccessibleName = "EMail/SMS Setting"
        Me.rbtnSetting.Name = "rbtnSetting"
        Me.rbtnSetting.Text = "EMail/SMS Setting"
        '
        'rbtnSend
        '
        Me.rbtnSend.AccessibleDescription = "EMail/SMS Send"
        Me.rbtnSend.AccessibleName = "EMail/SMS Send"
        Me.rbtnSend.Name = "rbtnSend"
        Me.rbtnSend.Text = "EMail/SMS Send"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.btnExport.Location = New System.Drawing.Point(158, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 133
        Me.btnExport.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(12, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 131
        Me.btnprint.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(85, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 130
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(951, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 132
        Me.btnClose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(1028, 20)
        Me.rdmenufile.TabIndex = 68
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnSaveLayout, Me.BtnDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'BtnSaveLayout
        '
        Me.BtnSaveLayout.AccessibleDescription = "Save Layout"
        Me.BtnSaveLayout.AccessibleName = "Save Layout"
        Me.BtnSaveLayout.Name = "BtnSaveLayout"
        Me.BtnSaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.BtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        '
        'RptFreshSaleRegister1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 605)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "RptFreshSaleRegister1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptFreshSaleRegister1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.GbCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbCustomerGroup.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.rbtnCustomerGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustomerGroupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSaleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpItem.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkSerializeInv As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnUnposted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkTax As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkRetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ddlSaleType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents tvCategory As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents GrpItem As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbtnItemSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnItemAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents GbCustomerGroup As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomerGroup As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCustomerGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCustomerGroupAll As common.Controls.MyRadioButton
    Friend WithEvents rbtnSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadButton
End Class

