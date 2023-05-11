<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmYearMonthWiseSaleComparison
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rbtnLocSelect = New common.Controls.MyRadioButton
        Me.rbtnLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbtnCustSelect = New common.Controls.MyRadioButton
        Me.rbtnCustAll = New common.Controls.MyRadioButton
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgFinancialYear = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnFinSelect = New common.Controls.MyRadioButton
        Me.rbtnFinAll = New common.Controls.MyRadioButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv2 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint1 = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton
        Me.RadMenuButtonItem1 = New Telerik.WinControls.UI.RadMenuButtonItem
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPdf = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnFinSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFinAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(874, 462)
        Me.RadPageView1.TabIndex = 6
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(853, 414)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox8)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(853, 414)
        Me.SplitContainer1.SplitterDistance = 385
        Me.SplitContainer1.TabIndex = 4
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(522, 6)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(329, 267)
        Me.RadGroupBox5.TabIndex = 319
        Me.RadGroupBox5.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(309, 217)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnLocSelect)
        Me.Panel3.Controls.Add(Me.rbtnLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(309, 20)
        Me.Panel3.TabIndex = 1
        '
        'rbtnLocSelect
        '
        Me.rbtnLocSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocSelect.Location = New System.Drawing.Point(157, 2)
        Me.rbtnLocSelect.MyLinkLable1 = Nothing
        Me.rbtnLocSelect.MyLinkLable2 = Nothing
        Me.rbtnLocSelect.Name = "rbtnLocSelect"
        Me.rbtnLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocSelect.TabIndex = 321
        Me.rbtnLocSelect.Text = "Select"
        '
        'rbtnLocAll
        '
        Me.rbtnLocAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocAll.Location = New System.Drawing.Point(106, 2)
        Me.rbtnLocAll.MyLinkLable1 = Nothing
        Me.rbtnLocAll.MyLinkLable2 = Nothing
        Me.rbtnLocAll.Name = "rbtnLocAll"
        Me.rbtnLocAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocAll.TabIndex = 320
        Me.rbtnLocAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Customer"
        Me.RadGroupBox1.Location = New System.Drawing.Point(201, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(315, 266)
        Me.RadGroupBox1.TabIndex = 318
        Me.RadGroupBox1.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(295, 216)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCustSelect)
        Me.Panel1.Controls.Add(Me.rbtnCustAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(295, 20)
        Me.Panel1.TabIndex = 0
        '
        'rbtnCustSelect
        '
        Me.rbtnCustSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCustSelect.Location = New System.Drawing.Point(148, 3)
        Me.rbtnCustSelect.MyLinkLable1 = Nothing
        Me.rbtnCustSelect.MyLinkLable2 = Nothing
        Me.rbtnCustSelect.Name = "rbtnCustSelect"
        Me.rbtnCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCustSelect.TabIndex = 321
        Me.rbtnCustSelect.Text = "Select"
        '
        'rbtnCustAll
        '
        Me.rbtnCustAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCustAll.Location = New System.Drawing.Point(97, 3)
        Me.rbtnCustAll.MyLinkLable1 = Nothing
        Me.rbtnCustAll.MyLinkLable2 = Nothing
        Me.rbtnCustAll.Name = "rbtnCustAll"
        Me.rbtnCustAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCustAll.TabIndex = 320
        Me.rbtnCustAll.Text = "All"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgFinancialYear)
        Me.RadGroupBox8.Controls.Add(Me.Panel7)
        Me.RadGroupBox8.HeaderText = "Financial Year"
        Me.RadGroupBox8.Location = New System.Drawing.Point(12, 7)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(182, 264)
        Me.RadGroupBox8.TabIndex = 312
        Me.RadGroupBox8.Text = "Financial Year"
        '
        'cbgFinancialYear
        '
        Me.cbgFinancialYear.CheckedValue = Nothing
        Me.cbgFinancialYear.DataSource = Nothing
        Me.cbgFinancialYear.DisplayMember = "Name"
        Me.cbgFinancialYear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgFinancialYear.Location = New System.Drawing.Point(10, 40)
        Me.cbgFinancialYear.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgFinancialYear.MyShowHeadrText = False
        Me.cbgFinancialYear.Name = "cbgFinancialYear"
        Me.cbgFinancialYear.Size = New System.Drawing.Size(162, 214)
        Me.cbgFinancialYear.TabIndex = 1
        Me.cbgFinancialYear.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnFinSelect)
        Me.Panel7.Controls.Add(Me.rbtnFinAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(162, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnFinSelect
        '
        Me.rbtnFinSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnFinSelect.Location = New System.Drawing.Point(82, 1)
        Me.rbtnFinSelect.MyLinkLable1 = Nothing
        Me.rbtnFinSelect.MyLinkLable2 = Nothing
        Me.rbtnFinSelect.Name = "rbtnFinSelect"
        Me.rbtnFinSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnFinSelect.TabIndex = 3
        Me.rbtnFinSelect.Text = "Select"
        '
        'rbtnFinAll
        '
        Me.rbtnFinAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnFinAll.Location = New System.Drawing.Point(29, 1)
        Me.rbtnFinAll.MyLinkLable1 = Nothing
        Me.rbtnFinAll.MyLinkLable2 = Nothing
        Me.rbtnFinAll.Name = "rbtnFinAll"
        Me.rbtnFinAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnFinAll.TabIndex = 2
        Me.rbtnFinAll.Text = "All"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(-444, 14)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 46
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(-367, 14)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 48
        Me.btnReset.Text = "Reset"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(853, 414)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.AutoScroll = True
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.EnableGestures = False
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowCellContextMenu = False
        Me.gv.MasterTemplate.AllowColumnChooser = False
        Me.gv.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv.MasterTemplate.AllowColumnReorder = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.AllowRowResize = False
        Me.gv.MasterTemplate.AutoGenerateColumns = False
        Me.gv.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.EnableSorting = False
        Me.gv.MasterTemplate.ShowFilteringRow = False
        Me.gv.MasterTemplate.ShowRowHeaderColumn = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(853, 414)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gv1)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(853, 414)
        Me.RadPageViewPage3.Text = "Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(853, 414)
        Me.gv1.TabIndex = 4
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gv2)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(853, 414)
        Me.RadPageViewPage4.Text = "Summary"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.EnableFiltering = True
        Me.gv2.MasterTemplate.ShowGroupedColumns = True
        Me.gv2.Name = "gv2"
        Me.gv2.ReadOnly = True
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.Size = New System.Drawing.Size(853, 414)
        Me.gv2.TabIndex = 5
        Me.gv2.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(803, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 47
        Me.btnClose.Text = "Close"
        '
        'btnPrint1
        '
        Me.btnPrint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint1.Location = New System.Drawing.Point(159, 2)
        Me.btnPrint1.Name = "btnPrint1"
        Me.btnPrint1.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint1.TabIndex = 44
        Me.btnPrint1.Text = "Print"
        Me.btnPrint1.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(11, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 44
        Me.btnRefresh.Text = ">>>"
        '
        'rbtnReset
        '
        Me.rbtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnReset.Location = New System.Drawing.Point(85, 2)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(68, 18)
        Me.rbtnReset.TabIndex = 46
        Me.rbtnReset.Text = "Reset"
        '
        'RadMenuButtonItem1
        '
        Me.RadMenuButtonItem1.AccessibleDescription = Nothing
        Me.RadMenuButtonItem1.AccessibleName = Nothing
        '
        '
        '
        Me.RadMenuButtonItem1.ButtonElement.AccessibleDescription = Nothing
        Me.RadMenuButtonItem1.ButtonElement.AccessibleName = Nothing
        Me.RadMenuButtonItem1.Name = "RadMenuButtonItem1"
        Me.RadMenuButtonItem1.Text = Nothing
        Me.RadMenuButtonItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPdf
        '
        Me.btnPdf.AccessibleDescription = "PDF"
        Me.btnPdf.AccessibleName = "PDF"
        Me.btnPdf.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPdf.Name = "btnPdf"
        Me.btnPdf.Text = "PDF"
        Me.btnPdf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPdf.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuButtonItem1, Me.btnExcel, Me.btnPdf})
        Me.btnExport.Location = New System.Drawing.Point(233, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 19)
        Me.btnExport.TabIndex = 49
        Me.btnExport.Text = "Export"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer2.Panel2.Controls.Add(Me.rbtnReset)
        Me.SplitContainer2.Size = New System.Drawing.Size(874, 491)
        Me.SplitContainer2.SplitterDistance = 462
        Me.SplitContainer2.TabIndex = 50
        '
        'frmYearMonthWiseSaleComparison
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 491)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "frmYearMonthWiseSaleComparison"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Year-Month Wise Sale Comparison"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnFinSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFinAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgFinancialYear As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents rbtnLocSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnLocAll As common.Controls.MyRadioButton
    Protected WithEvents rbtnCustSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCustAll As common.Controls.MyRadioButton
    Protected WithEvents rbtnFinSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnFinAll As common.Controls.MyRadioButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuButtonItem1 As Telerik.WinControls.UI.RadMenuButtonItem
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPdf As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
End Class

