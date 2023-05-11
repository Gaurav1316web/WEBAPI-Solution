Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAsset_Issue_Return_Report
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkCCWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkAssetWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkLocationWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.cboTransType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.gbVisi = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgAsset = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkAssetSelect = New common.Controls.MyRadioButton()
        Me.chkAssetAll = New common.Controls.MyRadioButton()
        Me.gbCC = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCostCenter = New common.MyCheckBoxGrid()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.chkCCSelect = New common.Controls.MyRadioButton()
        Me.chkCCAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.gbLocation = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLoc = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GV1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCCWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVisi.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkAssetSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbCC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCC.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkCCSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCCAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(842, 469)
        Me.SplitContainer1.SplitterDistance = 439
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(842, 439)
        Me.RadPageView1.TabIndex = 117
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.Panel4)
        Me.RadPageViewPage1.Controls.Add(Me.Panel2)
        Me.RadPageViewPage1.Controls.Add(Me.cboTransType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.gbVisi)
        Me.RadPageViewPage1.Controls.Add(Me.gbCC)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.gbLocation)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(821, 391)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.chkCCWise)
        Me.Panel4.Controls.Add(Me.chkAssetWise)
        Me.Panel4.Controls.Add(Me.chkLocationWise)
        Me.Panel4.Location = New System.Drawing.Point(441, 283)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(352, 21)
        Me.Panel4.TabIndex = 122
        Me.Panel4.Visible = False
        '
        'chkCCWise
        '
        Me.chkCCWise.Location = New System.Drawing.Point(227, 0)
        Me.chkCCWise.Name = "chkCCWise"
        Me.chkCCWise.Size = New System.Drawing.Size(106, 18)
        Me.chkCCWise.TabIndex = 118
        Me.chkCCWise.Text = "Cost Center Wise"
        '
        'chkAssetWise
        '
        Me.chkAssetWise.Location = New System.Drawing.Point(5, 0)
        Me.chkAssetWise.Name = "chkAssetWise"
        Me.chkAssetWise.Size = New System.Drawing.Size(74, 18)
        Me.chkAssetWise.TabIndex = 118
        Me.chkAssetWise.Text = "Asset Wise"
        '
        'chkLocationWise
        '
        Me.chkLocationWise.Location = New System.Drawing.Point(110, 0)
        Me.chkLocationWise.Name = "chkLocationWise"
        Me.chkLocationWise.Size = New System.Drawing.Size(90, 18)
        Me.chkLocationWise.TabIndex = 117
        Me.chkLocationWise.Text = "Location Wise"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkSummary)
        Me.Panel2.Controls.Add(Me.chkDetail)
        Me.Panel2.Location = New System.Drawing.Point(456, 17)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(186, 21)
        Me.Panel2.TabIndex = 121
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(80, 0)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 118
        Me.chkSummary.Text = "Summary"
        '
        'chkDetail
        '
        Me.chkDetail.Location = New System.Drawing.Point(8, 0)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(49, 18)
        Me.chkDetail.TabIndex = 117
        Me.chkDetail.Text = "Detail"
        '
        'cboTransType
        '
        Me.cboTransType.CalculationExpression = Nothing
        Me.cboTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransType.FieldCode = Nothing
        Me.cboTransType.FieldDesc = Nothing
        Me.cboTransType.FieldMaxLength = 0
        Me.cboTransType.FieldName = Nothing
        Me.cboTransType.isCalculatedField = False
        Me.cboTransType.IsSourceFromTable = False
        Me.cboTransType.IsSourceFromValueList = False
        Me.cboTransType.IsUnique = False
        Me.cboTransType.Location = New System.Drawing.Point(332, 17)
        Me.cboTransType.MendatroryField = True
        Me.cboTransType.MyLinkLable1 = Me.RadLabel8
        Me.cboTransType.MyLinkLable2 = Nothing
        Me.cboTransType.Name = "cboTransType"
        Me.cboTransType.ReferenceFieldDesc = Nothing
        Me.cboTransType.ReferenceFieldName = Nothing
        Me.cboTransType.ReferenceTableName = Nothing
        Me.cboTransType.Size = New System.Drawing.Size(114, 20)
        Me.cboTransType.TabIndex = 119
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(289, 19)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(35, 16)
        Me.RadLabel8.TabIndex = 120
        Me.RadLabel8.Text = "Show"
        '
        'gbVisi
        '
        Me.gbVisi.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVisi.Controls.Add(Me.cbgAsset)
        Me.gbVisi.Controls.Add(Me.Panel1)
        Me.gbVisi.HeaderText = "Asset"
        Me.gbVisi.Location = New System.Drawing.Point(3, 54)
        Me.gbVisi.Name = "gbVisi"
        Me.gbVisi.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVisi.Size = New System.Drawing.Size(403, 174)
        Me.gbVisi.TabIndex = 116
        Me.gbVisi.Text = "Asset"
        '
        'cbgAsset
        '
        Me.cbgAsset.CheckedValue = Nothing
        Me.cbgAsset.DataSource = Nothing
        Me.cbgAsset.DisplayMember = "Name"
        Me.cbgAsset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAsset.Location = New System.Drawing.Point(10, 40)
        Me.cbgAsset.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAsset.MyShowHeadrText = False
        Me.cbgAsset.Name = "cbgAsset"
        Me.cbgAsset.Size = New System.Drawing.Size(383, 124)
        Me.cbgAsset.TabIndex = 1
        Me.cbgAsset.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkAssetSelect)
        Me.Panel1.Controls.Add(Me.chkAssetAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(383, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkAssetSelect
        '
        Me.chkAssetSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkAssetSelect.MyLinkLable1 = Nothing
        Me.chkAssetSelect.MyLinkLable2 = Nothing
        Me.chkAssetSelect.Name = "chkAssetSelect"
        Me.chkAssetSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkAssetSelect.TabIndex = 1
        Me.chkAssetSelect.Text = "Select"
        '
        'chkAssetAll
        '
        Me.chkAssetAll.Location = New System.Drawing.Point(114, 1)
        Me.chkAssetAll.MyLinkLable1 = Nothing
        Me.chkAssetAll.MyLinkLable2 = Nothing
        Me.chkAssetAll.Name = "chkAssetAll"
        Me.chkAssetAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAssetAll.TabIndex = 0
        Me.chkAssetAll.Text = "All"
        '
        'gbCC
        '
        Me.gbCC.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbCC.Controls.Add(Me.cbgCostCenter)
        Me.gbCC.Controls.Add(Me.Panel8)
        Me.gbCC.HeaderText = "Cost Center"
        Me.gbCC.Location = New System.Drawing.Point(411, 54)
        Me.gbCC.Name = "gbCC"
        Me.gbCC.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbCC.Size = New System.Drawing.Size(403, 174)
        Me.gbCC.TabIndex = 115
        Me.gbCC.Text = "Cost Center"
        '
        'cbgCostCenter
        '
        Me.cbgCostCenter.CheckedValue = Nothing
        Me.cbgCostCenter.DataSource = Nothing
        Me.cbgCostCenter.DisplayMember = "Name"
        Me.cbgCostCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCostCenter.Location = New System.Drawing.Point(10, 40)
        Me.cbgCostCenter.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCostCenter.MyShowHeadrText = False
        Me.cbgCostCenter.Name = "cbgCostCenter"
        Me.cbgCostCenter.Size = New System.Drawing.Size(383, 124)
        Me.cbgCostCenter.TabIndex = 1
        Me.cbgCostCenter.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkCCSelect)
        Me.Panel8.Controls.Add(Me.chkCCAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(383, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkCCSelect
        '
        Me.chkCCSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkCCSelect.MyLinkLable1 = Nothing
        Me.chkCCSelect.MyLinkLable2 = Nothing
        Me.chkCCSelect.Name = "chkCCSelect"
        Me.chkCCSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCCSelect.TabIndex = 1
        Me.chkCCSelect.Text = "Select"
        '
        'chkCCAll
        '
        Me.chkCCAll.Location = New System.Drawing.Point(114, 1)
        Me.chkCCAll.MyLinkLable1 = Nothing
        Me.chkCCAll.MyLinkLable2 = Nothing
        Me.chkCCAll.Name = "chkCCAll"
        Me.chkCCAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCCAll.TabIndex = 0
        Me.chkCCAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(269, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Select Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(149, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MMM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(175, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(87, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/Oct/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MMM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(86, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/Oct/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'gbLocation
        '
        Me.gbLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbLocation.Controls.Add(Me.cbgLoc)
        Me.gbLocation.Controls.Add(Me.Panel3)
        Me.gbLocation.HeaderText = "Location"
        Me.gbLocation.Location = New System.Drawing.Point(3, 233)
        Me.gbLocation.Name = "gbLocation"
        Me.gbLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbLocation.Size = New System.Drawing.Size(403, 174)
        Me.gbLocation.TabIndex = 110
        Me.gbLocation.Text = "Location"
        '
        'cbgLoc
        '
        Me.cbgLoc.CheckedValue = Nothing
        Me.cbgLoc.DataSource = Nothing
        Me.cbgLoc.DisplayMember = "Name"
        Me.cbgLoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoc.MyShowHeadrText = False
        Me.cbgLoc.Name = "cbgLoc"
        Me.cbgLoc.Size = New System.Drawing.Size(383, 124)
        Me.cbgLoc.TabIndex = 1
        Me.cbgLoc.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocationSelect)
        Me.Panel3.Controls.Add(Me.chkLocationAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(383, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(114, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(819, 412)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.MasterTemplate.ShowHeaderCellButtons = True
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.ShowHeaderCellButtons = True
        Me.GV1.Size = New System.Drawing.Size(819, 412)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(765, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 130
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(154, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 129
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(78, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 127
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 128
        Me.btnRefresh.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(842, 20)
        Me.RadMenu1.TabIndex = 67
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
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
        'FrmAsset_Issue_Return_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 489)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmAsset_Issue_Return_Report"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Issue/Return Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCCWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVisi.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkAssetSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbCC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCC.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkCCSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCCAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbVisi As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgAsset As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkAssetSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAssetAll As common.Controls.MyRadioButton
    Friend WithEvents gbCC As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCostCenter As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkCCSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCCAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents gbLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboTransType As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkAssetWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLocationWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkCCWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

