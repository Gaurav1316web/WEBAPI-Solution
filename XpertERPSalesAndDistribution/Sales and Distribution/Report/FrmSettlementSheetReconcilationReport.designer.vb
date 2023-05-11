<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettlementSheetReconcilationReport
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
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbRouteWise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbBoth = New Telerik.WinControls.UI.RadRadioButton
        Me.rdnNonRoutewise = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkRouteSelect = New common.Controls.MyRadioButton
        Me.chkRouteAll = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocatioAll = New common.Controls.MyRadioButton
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRouteType = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkRouteTypeSelect = New common.Controls.MyRadioButton
        Me.chkRouteTypeAll = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnCompanySelect = New common.Controls.MyRadioButton
        Me.rbtnCompanyAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        Me.btnCLos = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.rdbRouteWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdnNonRoutewise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkRouteTypeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteTypeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCLos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(11, 2)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(910, 546)
        Me.RadPageView1.TabIndex = 121
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(889, 498)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox6)
        Me.SplitContainer1.Size = New System.Drawing.Size(889, 498)
        Me.SplitContainer1.SplitterDistance = 196
        Me.SplitContainer1.TabIndex = 22
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 11)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rdbRouteWise)
        Me.RadGroupBox7.Controls.Add(Me.rdbBoth)
        Me.RadGroupBox7.Controls.Add(Me.rdnNonRoutewise)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(256, 3)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(380, 34)
        Me.RadGroupBox7.TabIndex = 125
        '
        'rdbRouteWise
        '
        Me.rdbRouteWise.Location = New System.Drawing.Point(85, 7)
        Me.rdbRouteWise.Name = "rdbRouteWise"
        Me.rdbRouteWise.Size = New System.Drawing.Size(77, 18)
        Me.rdbRouteWise.TabIndex = 120
        Me.rdbRouteWise.Text = "Route Wise"
        '
        'rdbBoth
        '
        Me.rdbBoth.Location = New System.Drawing.Point(9, 7)
        Me.rdbBoth.Name = "rdbBoth"
        Me.rdbBoth.Size = New System.Drawing.Size(44, 18)
        Me.rdbBoth.TabIndex = 121
        Me.rdbBoth.Text = "Both"
        '
        'rdnNonRoutewise
        '
        Me.rdnNonRoutewise.Location = New System.Drawing.Point(202, 7)
        Me.rdnNonRoutewise.Name = "rdnNonRoutewise"
        Me.rdnNonRoutewise.Size = New System.Drawing.Size(102, 18)
        Me.rdnNonRoutewise.TabIndex = 122
        Me.rdnNonRoutewise.Text = "Non Route Wise"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgRoute)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 34)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(311, 159)
        Me.RadGroupBox4.TabIndex = 111
        Me.RadGroupBox4.Text = "Route"
        '
        'cbgRoute
        '
        Me.cbgRoute.CheckedValue = Nothing
        Me.cbgRoute.DataSource = Nothing
        Me.cbgRoute.DisplayMember = "Name"
        Me.cbgRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRoute.Location = New System.Drawing.Point(10, 40)
        Me.cbgRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRoute.MyShowHeadrText = False
        Me.cbgRoute.Name = "cbgRoute"
        Me.cbgRoute.Size = New System.Drawing.Size(291, 109)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkRouteSelect)
        Me.Panel4.Controls.Add(Me.chkRouteAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(291, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(142, 1)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(88, 1)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(322, 34)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(314, 159)
        Me.RadGroupBox5.TabIndex = 118
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
        Me.cbgLocation.Size = New System.Drawing.Size(294, 109)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocatioAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(294, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(144, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocatioAll
        '
        Me.chkLocatioAll.Location = New System.Drawing.Point(93, 1)
        Me.chkLocatioAll.MyLinkLable1 = Nothing
        Me.chkLocatioAll.MyLinkLable2 = Nothing
        Me.chkLocatioAll.Name = "chkLocatioAll"
        Me.chkLocatioAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocatioAll.TabIndex = 0
        Me.chkLocatioAll.Text = "All"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 10)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011 2:29 AM"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 8)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011 2:29 AM"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgRouteType)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Route Type"
        Me.RadGroupBox1.Location = New System.Drawing.Point(322, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(314, 178)
        Me.RadGroupBox1.TabIndex = 50
        Me.RadGroupBox1.Text = "Route Type"
        '
        'cbgRouteType
        '
        Me.cbgRouteType.CheckedValue = Nothing
        Me.cbgRouteType.DataSource = Nothing
        Me.cbgRouteType.DisplayMember = "Name"
        Me.cbgRouteType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRouteType.Location = New System.Drawing.Point(10, 40)
        Me.cbgRouteType.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRouteType.MyShowHeadrText = False
        Me.cbgRouteType.Name = "cbgRouteType"
        Me.cbgRouteType.Size = New System.Drawing.Size(294, 128)
        Me.cbgRouteType.TabIndex = 1
        Me.cbgRouteType.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRouteTypeSelect)
        Me.Panel1.Controls.Add(Me.chkRouteTypeAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(294, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkRouteTypeSelect
        '
        Me.chkRouteTypeSelect.Location = New System.Drawing.Point(140, 1)
        Me.chkRouteTypeSelect.MyLinkLable1 = Nothing
        Me.chkRouteTypeSelect.MyLinkLable2 = Nothing
        Me.chkRouteTypeSelect.Name = "chkRouteTypeSelect"
        Me.chkRouteTypeSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRouteTypeSelect.TabIndex = 1
        Me.chkRouteTypeSelect.Text = "Select"
        '
        'chkRouteTypeAll
        '
        Me.chkRouteTypeAll.Location = New System.Drawing.Point(88, 1)
        Me.chkRouteTypeAll.MyLinkLable1 = Nothing
        Me.chkRouteTypeAll.MyLinkLable2 = Nothing
        Me.chkRouteTypeAll.Name = "chkRouteTypeAll"
        Me.chkRouteTypeAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRouteTypeAll.TabIndex = 0
        Me.chkRouteTypeAll.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox6.Controls.Add(Me.Panel7)
        Me.RadGroupBox6.HeaderText = "Company"
        Me.RadGroupBox6.Location = New System.Drawing.Point(4, 3)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(310, 173)
        Me.RadGroupBox6.TabIndex = 115
        Me.RadGroupBox6.Text = "Company"
        '
        'cbgCompany
        '
        Me.cbgCompany.CheckedValue = Nothing
        Me.cbgCompany.DataSource = Nothing
        Me.cbgCompany.DisplayMember = "Name"
        Me.cbgCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCompany.Location = New System.Drawing.Point(10, 40)
        Me.cbgCompany.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCompany.MyShowHeadrText = False
        Me.cbgCompany.Name = "cbgCompany"
        Me.cbgCompany.Size = New System.Drawing.Size(290, 123)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnCompanySelect)
        Me.Panel7.Controls.Add(Me.rbtnCompanyAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(290, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnCompanySelect
        '
        Me.rbtnCompanySelect.Location = New System.Drawing.Point(134, 1)
        Me.rbtnCompanySelect.MyLinkLable1 = Nothing
        Me.rbtnCompanySelect.MyLinkLable2 = Nothing
        Me.rbtnCompanySelect.Name = "rbtnCompanySelect"
        Me.rbtnCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCompanySelect.TabIndex = 1
        Me.rbtnCompanySelect.Text = "Select"
        '
        'rbtnCompanyAll
        '
        Me.rbtnCompanyAll.Location = New System.Drawing.Point(80, 1)
        Me.rbtnCompanyAll.MyLinkLable1 = Nothing
        Me.rbtnCompanyAll.MyLinkLable2 = Nothing
        Me.rbtnCompanyAll.Name = "rbtnCompanyAll"
        Me.rbtnCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCompanyAll.TabIndex = 0
        Me.rbtnCompanyAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(720, 458)
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
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(720, 458)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.RadSplitButton1)
        Me.Panel8.Controls.Add(Me.btnRefresh)
        Me.Panel8.Controls.Add(Me.btnExport)
        Me.Panel8.Controls.Add(Me.btnCLos)
        Me.Panel8.Controls.Add(Me.btnReset)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 549)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(933, 31)
        Me.Panel8.TabIndex = 122
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(280, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(88, 24)
        Me.RadSplitButton1.TabIndex = 329
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 24)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = ">>>"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(179, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 23)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "Export To Excel"
        Me.btnExport.Visible = False
        '
        'btnCLos
        '
        Me.btnCLos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCLos.Location = New System.Drawing.Point(862, 3)
        Me.btnCLos.Name = "btnCLos"
        Me.btnCLos.Size = New System.Drawing.Size(68, 25)
        Me.btnCLos.TabIndex = 113
        Me.btnCLos.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(105, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 24)
        Me.btnReset.TabIndex = 117
        Me.btnReset.Text = "Reset"
        '
        'FrmSettlementSheetReconcilationReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 580)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "FrmSettlementSheetReconcilationReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settlement Sheet Reconcilation Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.rdbRouteWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdnNonRoutewise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkRouteTypeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteTypeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCLos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocatioAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRouteType As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteTypeSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteTypeAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCLos As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbRouteWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbBoth As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdnNonRoutewise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
End Class

