Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClaimRport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.VendorGroupBox = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkVendorSelect = New common.Controls.MyRadioButton
        Me.chkVendorAll = New common.Controls.MyRadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbMonth = New Telerik.WinControls.UI.RadDropDownList
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.cmbYear = New Telerik.WinControls.UI.RadDropDownList
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.radioSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.radioCustomerWise = New Telerik.WinControls.UI.RadRadioButton
        Me.radioDocumentWise = New Telerik.WinControls.UI.RadRadioButton
        Me.gbLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoc = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.chkLandingCost = New Telerik.WinControls.UI.RadRadioButton
        Me.chkMRP = New Telerik.WinControls.UI.RadRadioButton
        Me.gbCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.VendorGroupBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VendorGroupBox.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.radioSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioCustomerWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioDocumentWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLandingCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMRP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCustomer.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 451)
        Me.SplitContainer1.SplitterDistance = 421
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(921, 421)
        Me.RadPageView1.TabIndex = 117
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.VendorGroupBox)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.gbLocation)
        Me.RadPageViewPage1.Controls.Add(Me.chkLandingCost)
        Me.RadPageViewPage1.Controls.Add(Me.chkMRP)
        Me.RadPageViewPage1.Controls.Add(Me.gbCustomer)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(900, 373)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'VendorGroupBox
        '
        Me.VendorGroupBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.VendorGroupBox.Controls.Add(Me.cbgVendor)
        Me.VendorGroupBox.Controls.Add(Me.Panel1)
        Me.VendorGroupBox.HeaderText = "Vendor"
        Me.VendorGroupBox.Location = New System.Drawing.Point(595, 63)
        Me.VendorGroupBox.Name = "VendorGroupBox"
        Me.VendorGroupBox.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.VendorGroupBox.Size = New System.Drawing.Size(282, 270)
        Me.VendorGroupBox.TabIndex = 127
        Me.VendorGroupBox.Text = "Vendor"
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(262, 220)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVendorSelect)
        Me.Panel1.Controls.Add(Me.chkVendorAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(262, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(132, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(81, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbMonth)
        Me.GroupBox3.Controls.Add(Me.MyLabel2)
        Me.GroupBox3.Controls.Add(Me.MyLabel1)
        Me.GroupBox3.Controls.Add(Me.cmbYear)
        Me.GroupBox3.Location = New System.Drawing.Point(590, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(310, 32)
        Me.GroupBox3.TabIndex = 126
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Visible = False
        '
        'cmbMonth
        '
        Me.cmbMonth.Location = New System.Drawing.Point(51, 9)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(116, 20)
        Me.cmbMonth.TabIndex = 122
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(174, 9)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(28, 18)
        Me.MyLabel2.TabIndex = 125
        Me.MyLabel2.Text = "Year"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(5, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 18)
        Me.MyLabel1.TabIndex = 123
        Me.MyLabel1.Text = "Month"
        '
        'cmbYear
        '
        Me.cmbYear.Location = New System.Drawing.Point(208, 9)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(99, 20)
        Me.cmbYear.TabIndex = 124
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadLabel2)
        Me.GroupBox2.Controls.Add(Me.RadLabel1)
        Me.GroupBox2.Controls.Add(Me.ToDate)
        Me.GroupBox2.Controls.Add(Me.fromDate)
        Me.GroupBox2.Location = New System.Drawing.Point(320, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(264, 33)
        Me.GroupBox2.TabIndex = 121
        Me.GroupBox2.TabStop = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(135, 12)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(7, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ToDate.Location = New System.Drawing.Point(160, 11)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.fromDate.Location = New System.Drawing.Point(45, 10)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radioSummary)
        Me.GroupBox1.Controls.Add(Me.radioCustomerWise)
        Me.GroupBox1.Controls.Add(Me.radioDocumentWise)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(297, 32)
        Me.GroupBox1.TabIndex = 120
        Me.GroupBox1.TabStop = False
        '
        'radioSummary
        '
        Me.radioSummary.Location = New System.Drawing.Point(212, 10)
        Me.radioSummary.Name = "radioSummary"
        Me.radioSummary.Size = New System.Drawing.Size(67, 18)
        Me.radioSummary.TabIndex = 121
        Me.radioSummary.Text = "Summary"
        '
        'radioCustomerWise
        '
        Me.radioCustomerWise.Location = New System.Drawing.Point(110, 10)
        Me.radioCustomerWise.Name = "radioCustomerWise"
        Me.radioCustomerWise.Size = New System.Drawing.Size(96, 18)
        Me.radioCustomerWise.TabIndex = 120
        Me.radioCustomerWise.TabStop = True
        Me.radioCustomerWise.Text = "Customer Wise"
        Me.radioCustomerWise.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'radioDocumentWise
        '
        Me.radioDocumentWise.Location = New System.Drawing.Point(6, 10)
        Me.radioDocumentWise.Name = "radioDocumentWise"
        Me.radioDocumentWise.Size = New System.Drawing.Size(99, 18)
        Me.radioDocumentWise.TabIndex = 119
        Me.radioDocumentWise.Text = "Document Wise"
        '
        'gbLocation
        '
        Me.gbLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbLocation.Controls.Add(Me.cbgLoc)
        Me.gbLocation.Controls.Add(Me.Panel3)
        Me.gbLocation.HeaderText = "Location"
        Me.gbLocation.Location = New System.Drawing.Point(7, 63)
        Me.gbLocation.Name = "gbLocation"
        Me.gbLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbLocation.Size = New System.Drawing.Size(289, 270)
        Me.gbLocation.TabIndex = 119
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
        Me.cbgLoc.Size = New System.Drawing.Size(269, 220)
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
        Me.Panel3.Size = New System.Drawing.Size(269, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(140, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(89, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'chkLandingCost
        '
        Me.chkLandingCost.Location = New System.Drawing.Point(119, 41)
        Me.chkLandingCost.Name = "chkLandingCost"
        Me.chkLandingCost.Size = New System.Drawing.Size(136, 18)
        Me.chkLandingCost.TabIndex = 118
        Me.chkLandingCost.Text = "Based On Landing Cost"
        '
        'chkMRP
        '
        Me.chkMRP.Location = New System.Drawing.Point(17, 41)
        Me.chkMRP.Name = "chkMRP"
        Me.chkMRP.Size = New System.Drawing.Size(94, 18)
        Me.chkMRP.TabIndex = 117
        Me.chkMRP.Text = "Based On MRP"
        '
        'gbCustomer
        '
        Me.gbCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbCustomer.Controls.Add(Me.cbgCustomer)
        Me.gbCustomer.Controls.Add(Me.Panel8)
        Me.gbCustomer.HeaderText = "Customer"
        Me.gbCustomer.Location = New System.Drawing.Point(302, 63)
        Me.gbCustomer.Name = "gbCustomer"
        Me.gbCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbCustomer.Size = New System.Drawing.Size(282, 270)
        Me.gbCustomer.TabIndex = 115
        Me.gbCustomer.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(262, 220)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkCustomerSelect)
        Me.Panel8.Controls.Add(Me.chkCustomerAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(262, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(132, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(81, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(900, 373)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        'GV1
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.Size = New System.Drawing.Size(900, 373)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(241, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 131
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(844, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 130
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(155, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 129
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(79, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 127
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(5, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 128
        Me.btnRefresh.Text = ">>>"
        '
        'FrmClaimRport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 451)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmClaimRport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Claim Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.VendorGroupBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VendorGroupBox.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.radioSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioCustomerWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioDocumentWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLandingCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMRP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCustomer.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkLandingCost As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents chkMRP As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radioSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioCustomerWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioDocumentWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cmbYear As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cmbMonth As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents VendorGroupBox As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
End Class

