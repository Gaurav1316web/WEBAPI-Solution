<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCSADOReport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RGBxLoc = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMultiLocs = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbLocationsSelect = New common.Controls.MyRadioButton()
        Me.rbLocationsAll = New common.Controls.MyRadioButton()
        Me.rbtnComplt = New common.Controls.MyRadioButton()
        Me.rbtnpartl = New common.Controls.MyRadioButton()
        Me.rbtnpending = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCust = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnCustselect = New common.Controls.MyRadioButton()
        Me.rbtnCustAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDoc = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnDocSelect = New common.Controls.MyRadioButton()
        Me.rbtnDocAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbtnItemSelet = New common.Controls.MyRadioButton()
        Me.rbtnItemAll = New common.Controls.MyRadioButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RGBxLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGBxLoc.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.rbLocationsSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbLocationsAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnComplt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnpartl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnpending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCustselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnDocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnItemSelet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Size = New System.Drawing.Size(748, 539)
        Me.SplitContainer1.SplitterDistance = 507
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(746, 505)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(725, 457)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RGBxLoc)
        Me.RadGroupBox1.Controls.Add(Me.rbtnComplt)
        Me.RadGroupBox1.Controls.Add(Me.rbtnpartl)
        Me.RadGroupBox1.Controls.Add(Me.rbtnpending)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(725, 457)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RGBxLoc
        '
        Me.RGBxLoc.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGBxLoc.Controls.Add(Me.cbgMultiLocs)
        Me.RGBxLoc.Controls.Add(Me.Panel2)
        Me.RGBxLoc.HeaderText = "Locations"
        Me.RGBxLoc.Location = New System.Drawing.Point(365, 254)
        Me.RGBxLoc.Name = "RGBxLoc"
        Me.RGBxLoc.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RGBxLoc.Size = New System.Drawing.Size(354, 213)
        Me.RGBxLoc.TabIndex = 12
        Me.RGBxLoc.Text = "Locations"
        '
        'cbgMultiLocs
        '
        Me.cbgMultiLocs.CheckedValue = Nothing
        Me.cbgMultiLocs.DataSource = Nothing
        Me.cbgMultiLocs.DisplayMember = "Name"
        Me.cbgMultiLocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMultiLocs.Location = New System.Drawing.Point(10, 40)
        Me.cbgMultiLocs.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMultiLocs.MyShowHeadrText = False
        Me.cbgMultiLocs.Name = "cbgMultiLocs"
        Me.cbgMultiLocs.Size = New System.Drawing.Size(334, 163)
        Me.cbgMultiLocs.TabIndex = 0
        Me.cbgMultiLocs.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbLocationsSelect)
        Me.Panel2.Controls.Add(Me.rbLocationsAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(334, 20)
        Me.Panel2.TabIndex = 1
        '
        'rbLocationsSelect
        '
        Me.rbLocationsSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbLocationsSelect.Location = New System.Drawing.Point(164, 1)
        Me.rbLocationsSelect.MyLinkLable1 = Nothing
        Me.rbLocationsSelect.MyLinkLable2 = Nothing
        Me.rbLocationsSelect.Name = "rbLocationsSelect"
        Me.rbLocationsSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbLocationsSelect.TabIndex = 1
        Me.rbLocationsSelect.Text = "Select"
        '
        'rbLocationsAll
        '
        Me.rbLocationsAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbLocationsAll.Location = New System.Drawing.Point(113, 1)
        Me.rbLocationsAll.MyLinkLable1 = Nothing
        Me.rbLocationsAll.MyLinkLable2 = Nothing
        Me.rbLocationsAll.Name = "rbLocationsAll"
        Me.rbLocationsAll.Size = New System.Drawing.Size(33, 18)
        Me.rbLocationsAll.TabIndex = 0
        Me.rbLocationsAll.Text = "All"
        '
        'rbtnComplt
        '
        Me.rbtnComplt.Location = New System.Drawing.Point(426, 10)
        Me.rbtnComplt.MyLinkLable1 = Nothing
        Me.rbtnComplt.MyLinkLable2 = Nothing
        Me.rbtnComplt.Name = "rbtnComplt"
        Me.rbtnComplt.Size = New System.Drawing.Size(69, 18)
        Me.rbtnComplt.TabIndex = 11
        Me.rbtnComplt.Text = "Complete"
        '
        'rbtnpartl
        '
        Me.rbtnpartl.Location = New System.Drawing.Point(369, 10)
        Me.rbtnpartl.MyLinkLable1 = Nothing
        Me.rbtnpartl.MyLinkLable2 = Nothing
        Me.rbtnpartl.Name = "rbtnpartl"
        Me.rbtnpartl.Size = New System.Drawing.Size(51, 18)
        Me.rbtnpartl.TabIndex = 11
        Me.rbtnpartl.Text = "Partial"
        '
        'rbtnpending
        '
        Me.rbtnpending.Location = New System.Drawing.Point(303, 10)
        Me.rbtnpending.MyLinkLable1 = Nothing
        Me.rbtnpending.MyLinkLable2 = Nothing
        Me.rbtnpending.Name = "rbtnpending"
        Me.rbtnpending.Size = New System.Drawing.Size(61, 18)
        Me.rbtnpending.TabIndex = 10
        Me.rbtnpending.Text = "Pending"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgCust)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Customer"
        Me.RadGroupBox4.Location = New System.Drawing.Point(5, 254)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(354, 213)
        Me.RadGroupBox4.TabIndex = 9
        Me.RadGroupBox4.Text = "Customer"
        '
        'cbgCust
        '
        Me.cbgCust.CheckedValue = Nothing
        Me.cbgCust.DataSource = Nothing
        Me.cbgCust.DisplayMember = "Name"
        Me.cbgCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCust.Location = New System.Drawing.Point(10, 40)
        Me.cbgCust.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCust.MyShowHeadrText = False
        Me.cbgCust.Name = "cbgCust"
        Me.cbgCust.Size = New System.Drawing.Size(334, 163)
        Me.cbgCust.TabIndex = 0
        Me.cbgCust.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCustselect)
        Me.Panel1.Controls.Add(Me.rbtnCustAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(334, 20)
        Me.Panel1.TabIndex = 1
        '
        'rbtnCustselect
        '
        Me.rbtnCustselect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCustselect.Location = New System.Drawing.Point(164, 1)
        Me.rbtnCustselect.MyLinkLable1 = Nothing
        Me.rbtnCustselect.MyLinkLable2 = Nothing
        Me.rbtnCustselect.Name = "rbtnCustselect"
        Me.rbtnCustselect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCustselect.TabIndex = 1
        Me.rbtnCustselect.Text = "Select"
        '
        'rbtnCustAll
        '
        Me.rbtnCustAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCustAll.Location = New System.Drawing.Point(113, 1)
        Me.rbtnCustAll.MyLinkLable1 = Nothing
        Me.rbtnCustAll.MyLinkLable2 = Nothing
        Me.rbtnCustAll.Name = "rbtnCustAll"
        Me.rbtnCustAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCustAll.TabIndex = 0
        Me.rbtnCustAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgDoc)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Document No."
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 35)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(354, 213)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "Document No."
        '
        'cbgDoc
        '
        Me.cbgDoc.CheckedValue = Nothing
        Me.cbgDoc.DataSource = Nothing
        Me.cbgDoc.DisplayMember = "Name"
        Me.cbgDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgDoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDoc.MyShowHeadrText = False
        Me.cbgDoc.Name = "cbgDoc"
        Me.cbgDoc.Size = New System.Drawing.Size(334, 163)
        Me.cbgDoc.TabIndex = 0
        Me.cbgDoc.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnDocSelect)
        Me.Panel4.Controls.Add(Me.rbtnDocAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(334, 20)
        Me.Panel4.TabIndex = 1
        '
        'rbtnDocSelect
        '
        Me.rbtnDocSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnDocSelect.Location = New System.Drawing.Point(164, 1)
        Me.rbtnDocSelect.MyLinkLable1 = Nothing
        Me.rbtnDocSelect.MyLinkLable2 = Nothing
        Me.rbtnDocSelect.Name = "rbtnDocSelect"
        Me.rbtnDocSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnDocSelect.TabIndex = 1
        Me.rbtnDocSelect.Text = "Select"
        '
        'rbtnDocAll
        '
        Me.rbtnDocAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnDocAll.Location = New System.Drawing.Point(113, 1)
        Me.rbtnDocAll.MyLinkLable1 = Nothing
        Me.rbtnDocAll.MyLinkLable2 = Nothing
        Me.rbtnDocAll.Name = "rbtnDocAll"
        Me.rbtnDocAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnDocAll.TabIndex = 0
        Me.rbtnDocAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgItem)
        Me.RadGroupBox3.Controls.Add(Me.Panel3)
        Me.RadGroupBox3.HeaderText = "Item"
        Me.RadGroupBox3.Location = New System.Drawing.Point(365, 35)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(354, 213)
        Me.RadGroupBox3.TabIndex = 7
        Me.RadGroupBox3.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(334, 163)
        Me.cbgItem.TabIndex = 0
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnItemSelet)
        Me.Panel3.Controls.Add(Me.rbtnItemAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(334, 20)
        Me.Panel3.TabIndex = 1
        '
        'rbtnItemSelet
        '
        Me.rbtnItemSelet.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnItemSelet.Location = New System.Drawing.Point(164, 1)
        Me.rbtnItemSelet.MyLinkLable1 = Nothing
        Me.rbtnItemSelet.MyLinkLable2 = Nothing
        Me.rbtnItemSelet.Name = "rbtnItemSelet"
        Me.rbtnItemSelet.Size = New System.Drawing.Size(50, 18)
        Me.rbtnItemSelet.TabIndex = 1
        Me.rbtnItemSelet.Text = "Select"
        '
        'rbtnItemAll
        '
        Me.rbtnItemAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnItemAll.Location = New System.Drawing.Point(113, 1)
        Me.rbtnItemAll.MyLinkLable1 = Nothing
        Me.rbtnItemAll.MyLinkLable2 = Nothing
        Me.rbtnItemAll.Name = "rbtnItemAll"
        Me.rbtnItemAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnItemAll.TabIndex = 0
        Me.rbtnItemAll.Text = "All"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(71, 9)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(83, 20)
        Me.txtFromDate.TabIndex = 4
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(209, 9)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(83, 20)
        Me.txtToDate.TabIndex = 5
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(158, 10)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "To Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(725, 477)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(725, 477)
        Me.gv1.TabIndex = 4
        Me.gv1.Text = "RadGridView1"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(105, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(95, 21)
        Me.btnreset.TabIndex = 13
        Me.btnreset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnpdf})
        Me.btnExport.Location = New System.Drawing.Point(203, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 21)
        Me.btnExport.TabIndex = 12
        Me.btnExport.Text = "Export"
        '
        'btnexcel
        '
        Me.btnexcel.AccessibleDescription = "Export Excel"
        Me.btnexcel.AccessibleName = "Export Excel"
        Me.btnexcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Export Excel"
        '
        'btnpdf
        '
        Me.btnpdf.AccessibleDescription = "Export PDF"
        Me.btnpdf.AccessibleName = "Export PDF"
        Me.btnpdf.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "Export PDF"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(664, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(76, 21)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "Close"
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(4, 3)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(95, 21)
        Me.btngo.TabIndex = 11
        Me.btngo.Text = ">>>"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(748, 20)
        Me.rdmenufile.TabIndex = 70
        Me.rdmenufile.Text = "File"
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
        'FrmCSADOReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 559)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "FrmCSADOReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCSADOReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RGBxLoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGBxLoc.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rbLocationsSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbLocationsAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnComplt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnpartl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnpending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCustselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnDocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnItemSelet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Protected WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Protected WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgCust As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCustselect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCustAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgDoc As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents rbtnDocSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnDocAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents rbtnItemSelet As common.Controls.MyRadioButton
    Protected WithEvents rbtnItemAll As common.Controls.MyRadioButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents rbtnComplt As common.Controls.MyRadioButton
    Friend WithEvents rbtnpartl As common.Controls.MyRadioButton
    Friend WithEvents rbtnpending As common.Controls.MyRadioButton
    Friend WithEvents RGBxLoc As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgMultiLocs As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents rbLocationsSelect As common.Controls.MyRadioButton
    Protected WithEvents rbLocationsAll As common.Controls.MyRadioButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

