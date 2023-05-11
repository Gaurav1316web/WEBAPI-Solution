<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerBillWiseDetail
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.MyRadioButton3 = New common.Controls.MyRadioButton
        Me.MyRadioButton4 = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkChapterSelect = New common.Controls.MyRadioButton
        Me.chkChapterAll = New common.Controls.MyRadioButton
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpFrmDate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.chkcatewise = New common.Controls.MyCheckBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.tvCategory = New Telerik.WinControls.UI.RadTreeView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnsavelayout = New Telerik.WinControls.UI.RadMenuItem
        Me.btndeletelayout = New Telerik.WinControls.UI.RadMenuItem
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.RadDropDownButton1 = New Telerik.WinControls.UI.RadDropDownButton
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkcatewise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Select Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 252)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(373, 168)
        Me.RadGroupBox2.TabIndex = 313
        Me.RadGroupBox2.Text = "Select Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(353, 118)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.MyRadioButton3)
        Me.Panel2.Controls.Add(Me.MyRadioButton4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(353, 20)
        Me.Panel2.TabIndex = 1
        '
        'MyRadioButton3
        '
        Me.MyRadioButton3.Location = New System.Drawing.Point(189, 1)
        Me.MyRadioButton3.MyLinkLable1 = Nothing
        Me.MyRadioButton3.MyLinkLable2 = Nothing
        Me.MyRadioButton3.Name = "MyRadioButton3"
        '
        '
        '
        Me.MyRadioButton3.RootElement.StretchHorizontally = True
        Me.MyRadioButton3.RootElement.StretchVertically = True
        Me.MyRadioButton3.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton3.TabIndex = 2
        Me.MyRadioButton3.Text = "Select"
        '
        'MyRadioButton4
        '
        Me.MyRadioButton4.Location = New System.Drawing.Point(140, 1)
        Me.MyRadioButton4.MyLinkLable1 = Nothing
        Me.MyRadioButton4.MyLinkLable2 = Nothing
        Me.MyRadioButton4.Name = "MyRadioButton4"
        '
        '
        '
        Me.MyRadioButton4.RootElement.StretchHorizontally = True
        Me.MyRadioButton4.RootElement.StretchVertically = True
        Me.MyRadioButton4.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton4.TabIndex = 1
        Me.MyRadioButton4.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Select Customers"
        Me.RadGroupBox5.Location = New System.Drawing.Point(7, 80)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(373, 163)
        Me.RadGroupBox5.TabIndex = 312
        Me.RadGroupBox5.Text = "Select Customers"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(353, 113)
        Me.cbgCustomer.TabIndex = 2
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkChapterSelect)
        Me.Panel3.Controls.Add(Me.chkChapterAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(353, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkChapterSelect
        '
        Me.chkChapterSelect.Location = New System.Drawing.Point(190, 1)
        Me.chkChapterSelect.MyLinkLable1 = Nothing
        Me.chkChapterSelect.MyLinkLable2 = Nothing
        Me.chkChapterSelect.Name = "chkChapterSelect"
        '
        '
        '
        Me.chkChapterSelect.RootElement.StretchHorizontally = True
        Me.chkChapterSelect.RootElement.StretchVertically = True
        Me.chkChapterSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkChapterSelect.TabIndex = 2
        Me.chkChapterSelect.Text = "Select"
        '
        'chkChapterAll
        '
        Me.chkChapterAll.Location = New System.Drawing.Point(140, 1)
        Me.chkChapterAll.MyLinkLable1 = Nothing
        Me.chkChapterAll.MyLinkLable2 = Nothing
        Me.chkChapterAll.Name = "chkChapterAll"
        '
        '
        '
        Me.chkChapterAll.RootElement.StretchHorizontally = True
        Me.chkChapterAll.RootElement.StretchVertically = True
        Me.chkChapterAll.Size = New System.Drawing.Size(33, 18)
        Me.chkChapterAll.TabIndex = 1
        Me.chkChapterAll.Text = "All"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.dtpToDate)
        Me.RadPanel2.Controls.Add(Me.dtpFrmDate)
        Me.RadPanel2.Controls.Add(Me.RadLabel2)
        Me.RadPanel2.Controls.Add(Me.RadLabel1)
        Me.RadPanel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPanel2.Location = New System.Drawing.Point(7, 33)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(446, 44)
        Me.RadPanel2.TabIndex = 1
        Me.RadPanel2.Text = "Select Date Range"
        Me.RadPanel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(318, 20)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.RadLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpToDate.TabIndex = 88
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(267, 21)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 89
        Me.RadLabel2.Text = "To Date"
        '
        'dtpFrmDate
        '
        Me.dtpFrmDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrmDate.Location = New System.Drawing.Point(123, 20)
        Me.dtpFrmDate.MendatroryField = False
        Me.dtpFrmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.MyLinkLable1 = Me.RadLabel1
        Me.dtpFrmDate.MyLinkLable2 = Nothing
        Me.dtpFrmDate.Name = "dtpFrmDate"
        Me.dtpFrmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpFrmDate.TabIndex = 87
        Me.dtpFrmDate.TabStop = False
        Me.dtpFrmDate.Text = "13-06-2011"
        Me.dtpFrmDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(47, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 90
        Me.RadLabel1.Text = "From Date"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(169, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 19)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(749, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(73, 19)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(90, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 19)
        Me.btnReset.TabIndex = 4
        Me.btnReset.Text = "Reset"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadDropDownButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(825, 517)
        Me.SplitContainer1.SplitterDistance = 480
        Me.SplitContainer1.TabIndex = 5
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 23)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(819, 454)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkcatewise)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(56.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(798, 406)
        Me.RadPageViewPage1.Text = "Options"
        '
        'chkcatewise
        '
        Me.chkcatewise.Location = New System.Drawing.Point(471, 51)
        Me.chkcatewise.MyLinkLable1 = Nothing
        Me.chkcatewise.MyLinkLable2 = Nothing
        Me.chkcatewise.Name = "chkcatewise"
        Me.chkcatewise.Size = New System.Drawing.Size(144, 18)
        Me.chkcatewise.TabIndex = 323
        Me.chkcatewise.Tag1 = Nothing
        Me.chkcatewise.Text = "Show Data Categorywise"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.tvCategory)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Category"
        Me.RadGroupBox1.Location = New System.Drawing.Point(390, 80)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(401, 340)
        Me.RadGroupBox1.TabIndex = 322
        Me.RadGroupBox1.Text = "Category"
        '
        'tvCategory
        '
        Me.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCategory.Location = New System.Drawing.Point(10, 40)
        Me.tvCategory.Name = "tvCategory"
        Me.tvCategory.Size = New System.Drawing.Size(381, 290)
        Me.tvCategory.SpacingBetweenNodes = -1
        Me.tvCategory.TabIndex = 70
        Me.tvCategory.Text = "RadTreeView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel1.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(381, 20)
        Me.Panel1.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(187, 1)
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
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(136, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(798, 426)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(798, 426)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(3, 3)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(819, 20)
        Me.RadMenu1.TabIndex = 325
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnsavelayout, Me.btndeletelayout})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnsavelayout
        '
        Me.btnsavelayout.AccessibleDescription = "Save Layout"
        Me.btnsavelayout.AccessibleName = "Save Layout"
        Me.btnsavelayout.Name = "btnsavelayout"
        Me.btnsavelayout.Text = "Save Layout"
        Me.btnsavelayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btndeletelayout
        '
        Me.btndeletelayout.AccessibleDescription = "Delete Layout"
        Me.btndeletelayout.AccessibleName = "Delete Layout"
        Me.btndeletelayout.Name = "btndeletelayout"
        Me.btndeletelayout.Text = "Delete Layout"
        Me.btndeletelayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(10, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(74, 19)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = ">>>"
        '
        'RadDropDownButton1
        '
        Me.RadDropDownButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnpdf})
        Me.RadDropDownButton1.Location = New System.Drawing.Point(249, 7)
        Me.RadDropDownButton1.Name = "RadDropDownButton1"
        Me.RadDropDownButton1.Size = New System.Drawing.Size(80, 19)
        Me.RadDropDownButton1.TabIndex = 11
        Me.RadDropDownButton1.Text = "Export"
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Excel"
        Me.btnexport.AccessibleName = "Excel"
        Me.btnexport.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Excel"
        Me.btnexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnpdf
        '
        Me.btnpdf.AccessibleDescription = "PDF"
        Me.btnpdf.AccessibleName = "PDF"
        Me.btnpdf.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "PDF"
        Me.btnpdf.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmCustomerBillWiseDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCustomerBillWiseDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Bill Wise Detail"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkcatewise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpFrmDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkChapterSelect As common.Controls.MyRadioButton
    Friend WithEvents chkChapterAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton3 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton4 As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkcatewise As common.Controls.MyCheckBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents tvCategory As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsavelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndeletelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadDropDownButton1 As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
End Class

