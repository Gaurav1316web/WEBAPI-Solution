Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCDiscReport
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
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnCompanySelect = New common.Controls.MyRadioButton
        Me.rbtnCompanyAll = New common.Controls.MyRadioButton
        Me.rdbPack = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbFlavour = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cboCustomerClass = New common.Controls.MyComboBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.rdbSku = New Telerik.WinControls.UI.RadRadioButton
        Me.Item = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.btnExportToExcel = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyComboBox2 = New common.Controls.MyComboBox
        Me.rdbDistwise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbRoutewise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbNone = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbRoute = New Telerik.WinControls.UI.RadCheckBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgroute = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkSelectRoute = New common.Controls.MyRadioButton
        Me.chkAllRoute = New common.Controls.MyRadioButton
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyComboBox1 = New common.Controls.MyComboBox
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvReport = New common.UserControls.MyRadGridView
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Item.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.MyComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDistwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbRoutewise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox2.Controls.Add(Me.Panel7)
        Me.RadGroupBox2.HeaderText = "Company"
        Me.RadGroupBox2.Location = New System.Drawing.Point(357, 51)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(345, 171)
        Me.RadGroupBox2.TabIndex = 108
        Me.RadGroupBox2.Text = "Company"
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
        Me.cbgCompany.Size = New System.Drawing.Size(325, 121)
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
        Me.Panel7.Size = New System.Drawing.Size(325, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnCompanySelect
        '
        Me.rbtnCompanySelect.Location = New System.Drawing.Point(163, 1)
        Me.rbtnCompanySelect.MyLinkLable1 = Nothing
        Me.rbtnCompanySelect.MyLinkLable2 = Nothing
        Me.rbtnCompanySelect.Name = "rbtnCompanySelect"
        Me.rbtnCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCompanySelect.TabIndex = 1
        Me.rbtnCompanySelect.Text = "Select"
        '
        'rbtnCompanyAll
        '
        Me.rbtnCompanyAll.Location = New System.Drawing.Point(109, 1)
        Me.rbtnCompanyAll.MyLinkLable1 = Nothing
        Me.rbtnCompanyAll.MyLinkLable2 = Nothing
        Me.rbtnCompanyAll.Name = "rbtnCompanyAll"
        Me.rbtnCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCompanyAll.TabIndex = 0
        Me.rbtnCompanyAll.Text = "All"
        '
        'rdbPack
        '
        Me.rdbPack.Location = New System.Drawing.Point(86, 16)
        Me.rdbPack.Name = "rdbPack"
        Me.rdbPack.Size = New System.Drawing.Size(70, 18)
        Me.rdbPack.TabIndex = 105
        Me.rdbPack.Text = "Pack Wise"
        '
        'rdbFlavour
        '
        Me.rdbFlavour.Location = New System.Drawing.Point(252, 16)
        Me.rdbFlavour.Name = "rdbFlavour"
        Me.rdbFlavour.Size = New System.Drawing.Size(84, 18)
        Me.rdbFlavour.TabIndex = 106
        Me.rdbFlavour.Text = "Flavour Wise"
        Me.rdbFlavour.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cboCustomerClass)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(258, 42)
        Me.RadGroupBox3.TabIndex = 52
        Me.RadGroupBox3.Text = "Select Date"
        '
        'cboCustomerClass
        '
        Me.cboCustomerClass.AllowShowFocusCues = False
        Me.cboCustomerClass.AutoCompleteDisplayMember = Nothing
        Me.cboCustomerClass.AutoCompleteValueMember = Nothing
        Me.cboCustomerClass.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCustomerClass.Location = New System.Drawing.Point(368, 12)
        Me.cboCustomerClass.MendatroryField = False
        Me.cboCustomerClass.MyLinkLable1 = Nothing
        Me.cboCustomerClass.MyLinkLable2 = Nothing
        Me.cboCustomerClass.Name = "cboCustomerClass"
        Me.cboCustomerClass.Size = New System.Drawing.Size(11, 20)
        Me.cboCustomerClass.TabIndex = 84
        Me.cboCustomerClass.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "10/24/2011 2:29 AM"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "10/24/2011 2:29 AM"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'rdbSku
        '
        Me.rdbSku.Location = New System.Drawing.Point(5, 16)
        Me.rdbSku.Name = "rdbSku"
        Me.rdbSku.Size = New System.Drawing.Size(68, 18)
        Me.rdbSku.TabIndex = 104
        Me.rdbSku.Text = "SKU Wise"
        '
        'Item
        '
        Me.Item.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Item.Controls.Add(Me.cbgCustomer)
        Me.Item.Controls.Add(Me.Panel3)
        Me.Item.HeaderText = "Customer"
        Me.Item.Location = New System.Drawing.Point(355, 228)
        Me.Item.Name = "Item"
        Me.Item.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Item.Size = New System.Drawing.Size(345, 200)
        Me.Item.TabIndex = 48
        Me.Item.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(325, 150)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkCustomerSelect)
        Me.Panel3.Controls.Add(Me.chkCustomerAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(325, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(152, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(101, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnExportToExcel)
        Me.RadPanel1.Controls.Add(Me.btnRefresh)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Controls.Add(Me.btnReset)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 490)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(730, 28)
        Me.RadPanel1.TabIndex = 109
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(153, 6)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(118, 18)
        Me.btnExportToExcel.TabIndex = 9
        Me.btnExportToExcel.Text = "Export To Excel"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(5, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(644, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(79, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 5)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(730, 479)
        Me.RadPageView1.TabIndex = 108
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.rdbRoute)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.Locationgb)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.Item)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(709, 431)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.MyComboBox2)
        Me.RadGroupBox5.Controls.Add(Me.rdbDistwise)
        Me.RadGroupBox5.Controls.Add(Me.rdbRoutewise)
        Me.RadGroupBox5.Controls.Add(Me.rdbNone)
        Me.RadGroupBox5.HeaderText = "Select  Grouping"
        Me.RadGroupBox5.Location = New System.Drawing.Point(443, 3)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(259, 42)
        Me.RadGroupBox5.TabIndex = 107
        Me.RadGroupBox5.Text = "Select  Grouping"
        '
        'MyComboBox2
        '
        Me.MyComboBox2.AllowShowFocusCues = False
        Me.MyComboBox2.AutoCompleteDisplayMember = Nothing
        Me.MyComboBox2.AutoCompleteValueMember = Nothing
        Me.MyComboBox2.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox2.Location = New System.Drawing.Point(368, 12)
        Me.MyComboBox2.MendatroryField = False
        Me.MyComboBox2.MyLinkLable1 = Nothing
        Me.MyComboBox2.MyLinkLable2 = Nothing
        Me.MyComboBox2.Name = "MyComboBox2"
        Me.MyComboBox2.Size = New System.Drawing.Size(11, 20)
        Me.MyComboBox2.TabIndex = 84
        Me.MyComboBox2.Visible = False
        '
        'rdbDistwise
        '
        Me.rdbDistwise.Location = New System.Drawing.Point(153, 16)
        Me.rdbDistwise.Name = "rdbDistwise"
        Me.rdbDistwise.Size = New System.Drawing.Size(101, 18)
        Me.rdbDistwise.TabIndex = 105
        Me.rdbDistwise.Text = "Distributor Wise"
        '
        'rdbRoutewise
        '
        Me.rdbRoutewise.Location = New System.Drawing.Point(64, 16)
        Me.rdbRoutewise.Name = "rdbRoutewise"
        Me.rdbRoutewise.Size = New System.Drawing.Size(77, 18)
        Me.rdbRoutewise.TabIndex = 104
        Me.rdbRoutewise.Text = "Route Wise"
        '
        'rdbNone
        '
        Me.rdbNone.Location = New System.Drawing.Point(3, 16)
        Me.rdbNone.Name = "rdbNone"
        Me.rdbNone.Size = New System.Drawing.Size(48, 18)
        Me.rdbNone.TabIndex = 106
        Me.rdbNone.Text = "None"
        '
        'rdbRoute
        '
        Me.rdbRoute.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbRoute.Location = New System.Drawing.Point(561, 11)
        Me.rdbRoute.Name = "rdbRoute"
        Me.rdbRoute.Size = New System.Drawing.Size(75, 18)
        Me.rdbRoute.TabIndex = 115
        Me.rdbRoute.Text = "Route wise"
        Me.rdbRoute.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.rdbRoute.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgroute)
        Me.RadGroupBox1.Controls.Add(Me.Panel6)
        Me.RadGroupBox1.HeaderText = "Route"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 228)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(345, 200)
        Me.RadGroupBox1.TabIndex = 20
        Me.RadGroupBox1.Text = "Route"
        '
        'cbgroute
        '
        Me.cbgroute.CheckedValue = Nothing
        Me.cbgroute.DataSource = Nothing
        Me.cbgroute.DisplayMember = "Name"
        Me.cbgroute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgroute.Location = New System.Drawing.Point(10, 40)
        Me.cbgroute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgroute.MyShowHeadrText = False
        Me.cbgroute.Name = "cbgroute"
        Me.cbgroute.Size = New System.Drawing.Size(325, 150)
        Me.cbgroute.TabIndex = 2
        Me.cbgroute.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkSelectRoute)
        Me.Panel6.Controls.Add(Me.chkAllRoute)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(325, 20)
        Me.Panel6.TabIndex = 1
        '
        'chkSelectRoute
        '
        Me.chkSelectRoute.Location = New System.Drawing.Point(154, 1)
        Me.chkSelectRoute.MyLinkLable1 = Nothing
        Me.chkSelectRoute.MyLinkLable2 = Nothing
        Me.chkSelectRoute.Name = "chkSelectRoute"
        Me.chkSelectRoute.Size = New System.Drawing.Size(50, 18)
        Me.chkSelectRoute.TabIndex = 2
        Me.chkSelectRoute.Text = "Select"
        '
        'chkAllRoute
        '
        Me.chkAllRoute.Location = New System.Drawing.Point(99, 1)
        Me.chkAllRoute.MyLinkLable1 = Nothing
        Me.chkAllRoute.MyLinkLable2 = Nothing
        Me.chkAllRoute.Name = "chkAllRoute"
        Me.chkAllRoute.Size = New System.Drawing.Size(33, 18)
        Me.chkAllRoute.TabIndex = 1
        Me.chkAllRoute.Text = "All"
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(6, 51)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(345, 171)
        Me.Locationgb.TabIndex = 109
        Me.Locationgb.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(325, 121)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(325, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(151, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(100, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyComboBox1)
        Me.RadGroupBox4.Controls.Add(Me.rdbPack)
        Me.RadGroupBox4.Controls.Add(Me.rdbSku)
        Me.RadGroupBox4.Controls.Add(Me.rdbFlavour)
        Me.RadGroupBox4.HeaderText = "Select Date"
        Me.RadGroupBox4.Location = New System.Drawing.Point(268, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(169, 42)
        Me.RadGroupBox4.TabIndex = 85
        Me.RadGroupBox4.Text = "Select Date"
        '
        'MyComboBox1
        '
        Me.MyComboBox1.AllowShowFocusCues = False
        Me.MyComboBox1.AutoCompleteDisplayMember = Nothing
        Me.MyComboBox1.AutoCompleteValueMember = Nothing
        Me.MyComboBox1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox1.Location = New System.Drawing.Point(368, 12)
        Me.MyComboBox1.MendatroryField = False
        Me.MyComboBox1.MyLinkLable1 = Nothing
        Me.MyComboBox1.MyLinkLable2 = Nothing
        Me.MyComboBox1.Name = "MyComboBox1"
        Me.MyComboBox1.Size = New System.Drawing.Size(11, 20)
        Me.MyComboBox1.TabIndex = 84
        Me.MyComboBox1.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvReport)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(709, 431)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvReport
        '
        Me.gvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReport.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvReport.MasterTemplate.AllowAddNewRow = False
        Me.gvReport.MasterTemplate.AllowEditRow = False
        Me.gvReport.Name = "gvReport"
        Me.gvReport.ShowGroupPanel = False
        Me.gvReport.Size = New System.Drawing.Size(709, 431)
        Me.gvReport.TabIndex = 0
        Me.gvReport.Text = "RadGridView1"
        '
        'FrmMCDiscReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 518)
        Me.Controls.Add(Me.RadPanel1)
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "FrmMCDiscReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Marginal Contribution Report"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Item.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.MyComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDistwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbRoutewise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbPack As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbFlavour As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cboCustomerClass As common.Controls.MyComboBox
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents rdbSku As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Item As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnExportToExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvReport As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyComboBox1 As common.Controls.MyComboBox
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgroute As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectRoute As common.Controls.MyRadioButton
    Friend WithEvents chkAllRoute As common.Controls.MyRadioButton
    Friend WithEvents rdbRoute As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyComboBox2 As common.Controls.MyComboBox
    Friend WithEvents rdbDistwise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbRoutewise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbNone As Telerik.WinControls.UI.RadRadioButton
End Class

