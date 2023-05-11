Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDaywisePendingComplaint
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
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.chksecndry = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rdallsecndry = New Telerik.WinControls.UI.RadRadioButton
        Me.rdselctsecndry = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkprimary = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdallprimary = New Telerik.WinControls.UI.RadRadioButton
        Me.rdselctprimary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkcompl = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdallcompl = New Telerik.WinControls.UI.RadRadioButton
        Me.rdslctcompl = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgasset = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rdallasset = New Telerik.WinControls.UI.RadRadioButton
        Me.rdselectasset = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbpending = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbnotcomplete = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbComplete = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rdselectoutlet = New Telerik.WinControls.UI.RadRadioButton
        Me.rdalloutlet = New Telerik.WinControls.UI.RadRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPdf = New Telerik.WinControls.UI.RadMenuItem
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.rdallsecndry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdselctsecndry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.rdallprimary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdselctprimary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rdallcompl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdslctcompl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rdallasset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdselectasset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbpending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbnotcomplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rdselectoutlet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdalloutlet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(861, 572)
        Me.RadPageView1.TabIndex = 7
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(840, 524)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.chksecndry)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Secondary Complaint"
        Me.RadGroupBox6.Location = New System.Drawing.Point(283, 314)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(273, 207)
        Me.RadGroupBox6.TabIndex = 320
        Me.RadGroupBox6.Text = "Secondary Complaint"
        '
        'chksecndry
        '
        Me.chksecndry.CheckedValue = Nothing
        Me.chksecndry.DataSource = Nothing
        Me.chksecndry.DisplayMember = "Name"
        Me.chksecndry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chksecndry.Location = New System.Drawing.Point(10, 40)
        Me.chksecndry.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chksecndry.MyShowHeadrText = False
        Me.chksecndry.Name = "chksecndry"
        Me.chksecndry.Size = New System.Drawing.Size(253, 157)
        Me.chksecndry.TabIndex = 2
        Me.chksecndry.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rdallsecndry)
        Me.Panel4.Controls.Add(Me.rdselctsecndry)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(253, 20)
        Me.Panel4.TabIndex = 1
        '
        'rdallsecndry
        '
        Me.rdallsecndry.Location = New System.Drawing.Point(46, 1)
        Me.rdallsecndry.Name = "rdallsecndry"
        Me.rdallsecndry.Size = New System.Drawing.Size(33, 18)
        Me.rdallsecndry.TabIndex = 115
        Me.rdallsecndry.Text = "All"
        '
        'rdselctsecndry
        '
        Me.rdselctsecndry.Location = New System.Drawing.Point(124, 1)
        Me.rdselctsecndry.Name = "rdselctsecndry"
        Me.rdselctsecndry.Size = New System.Drawing.Size(50, 18)
        Me.rdselctsecndry.TabIndex = 113
        Me.rdselctsecndry.Text = "Select"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkprimary)
        Me.RadGroupBox4.Controls.Add(Me.Panel2)
        Me.RadGroupBox4.HeaderText = "Primary Complaint"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 314)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(273, 207)
        Me.RadGroupBox4.TabIndex = 319
        Me.RadGroupBox4.Text = "Primary Complaint"
        '
        'chkprimary
        '
        Me.chkprimary.CheckedValue = Nothing
        Me.chkprimary.DataSource = Nothing
        Me.chkprimary.DisplayMember = "Name"
        Me.chkprimary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkprimary.Location = New System.Drawing.Point(10, 40)
        Me.chkprimary.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chkprimary.MyShowHeadrText = False
        Me.chkprimary.Name = "chkprimary"
        Me.chkprimary.Size = New System.Drawing.Size(253, 157)
        Me.chkprimary.TabIndex = 2
        Me.chkprimary.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rdallprimary)
        Me.Panel2.Controls.Add(Me.rdselctprimary)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(253, 20)
        Me.Panel2.TabIndex = 1
        '
        'rdallprimary
        '
        Me.rdallprimary.Location = New System.Drawing.Point(56, 1)
        Me.rdallprimary.Name = "rdallprimary"
        Me.rdallprimary.Size = New System.Drawing.Size(33, 18)
        Me.rdallprimary.TabIndex = 114
        Me.rdallprimary.Text = "All"
        '
        'rdselctprimary
        '
        Me.rdselctprimary.Location = New System.Drawing.Point(129, 1)
        Me.rdselctprimary.Name = "rdselctprimary"
        Me.rdselctprimary.Size = New System.Drawing.Size(50, 18)
        Me.rdselctprimary.TabIndex = 113
        Me.rdselctprimary.Text = "Select"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkcompl)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Complaint Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(562, 49)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(273, 259)
        Me.RadGroupBox2.TabIndex = 318
        Me.RadGroupBox2.Text = "Complaint Type"
        '
        'chkcompl
        '
        Me.chkcompl.CheckedValue = Nothing
        Me.chkcompl.DataSource = Nothing
        Me.chkcompl.DisplayMember = "Name"
        Me.chkcompl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkcompl.Location = New System.Drawing.Point(10, 40)
        Me.chkcompl.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chkcompl.MyShowHeadrText = False
        Me.chkcompl.Name = "chkcompl"
        Me.chkcompl.Size = New System.Drawing.Size(253, 209)
        Me.chkcompl.TabIndex = 2
        Me.chkcompl.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdallcompl)
        Me.Panel1.Controls.Add(Me.rdslctcompl)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(253, 20)
        Me.Panel1.TabIndex = 1
        '
        'rdallcompl
        '
        Me.rdallcompl.Location = New System.Drawing.Point(57, 1)
        Me.rdallcompl.Name = "rdallcompl"
        Me.rdallcompl.Size = New System.Drawing.Size(33, 18)
        Me.rdallcompl.TabIndex = 113
        Me.rdallcompl.Text = "All"
        '
        'rdslctcompl
        '
        Me.rdslctcompl.Location = New System.Drawing.Point(127, 1)
        Me.rdslctcompl.Name = "rdslctcompl"
        Me.rdslctcompl.Size = New System.Drawing.Size(50, 18)
        Me.rdslctcompl.TabIndex = 112
        Me.rdslctcompl.Text = "Select"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgasset)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Asset"
        Me.RadGroupBox5.Location = New System.Drawing.Point(283, 49)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(273, 259)
        Me.RadGroupBox5.TabIndex = 317
        Me.RadGroupBox5.Text = "Asset"
        '
        'cbgasset
        '
        Me.cbgasset.CheckedValue = Nothing
        Me.cbgasset.DataSource = Nothing
        Me.cbgasset.DisplayMember = "Name"
        Me.cbgasset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgasset.Location = New System.Drawing.Point(10, 40)
        Me.cbgasset.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgasset.MyShowHeadrText = False
        Me.cbgasset.Name = "cbgasset"
        Me.cbgasset.Size = New System.Drawing.Size(253, 209)
        Me.cbgasset.TabIndex = 2
        Me.cbgasset.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rdallasset)
        Me.Panel3.Controls.Add(Me.rdselectasset)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(253, 20)
        Me.Panel3.TabIndex = 1
        '
        'rdallasset
        '
        Me.rdallasset.Location = New System.Drawing.Point(61, 1)
        Me.rdallasset.Name = "rdallasset"
        Me.rdallasset.Size = New System.Drawing.Size(33, 18)
        Me.rdallasset.TabIndex = 110
        Me.rdallasset.Text = "All"
        '
        'rdselectasset
        '
        Me.rdselectasset.Location = New System.Drawing.Point(113, 1)
        Me.rdselectasset.Name = "rdselectasset"
        Me.rdselectasset.Size = New System.Drawing.Size(50, 18)
        Me.rdselectasset.TabIndex = 111
        Me.rdselectasset.Text = "Select"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbpending)
        Me.RadGroupBox1.Controls.Add(Me.rdbnotcomplete)
        Me.RadGroupBox1.Controls.Add(Me.rdbComplete)
        Me.RadGroupBox1.Controls.Add(Me.rdbAll)
        Me.RadGroupBox1.HeaderText = "Status"
        Me.RadGroupBox1.Location = New System.Drawing.Point(305, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(335, 42)
        Me.RadGroupBox1.TabIndex = 311
        Me.RadGroupBox1.Text = "Status"
        '
        'rdbpending
        '
        Me.rdbpending.Location = New System.Drawing.Point(65, 14)
        Me.rdbpending.Name = "rdbpending"
        Me.rdbpending.Size = New System.Drawing.Size(61, 18)
        Me.rdbpending.TabIndex = 111
        Me.rdbpending.Text = "Pending"
        '
        'rdbnotcomplete
        '
        Me.rdbnotcomplete.Location = New System.Drawing.Point(139, 14)
        Me.rdbnotcomplete.Name = "rdbnotcomplete"
        Me.rdbnotcomplete.Size = New System.Drawing.Size(91, 18)
        Me.rdbnotcomplete.TabIndex = 110
        Me.rdbnotcomplete.Text = "Not Complete"
        '
        'rdbComplete
        '
        Me.rdbComplete.Location = New System.Drawing.Point(248, 14)
        Me.rdbComplete.Name = "rdbComplete"
        Me.rdbComplete.Size = New System.Drawing.Size(69, 18)
        Me.rdbComplete.TabIndex = 109
        Me.rdbComplete.Text = "Complete"
        '
        'rdbAll
        '
        Me.rdbAll.Location = New System.Drawing.Point(13, 14)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbAll.TabIndex = 108
        Me.rdbAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(292, 42)
        Me.RadGroupBox3.TabIndex = 310
        Me.RadGroupBox3.Text = "Select Date"
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
        Me.RadLabel1.Location = New System.Drawing.Point(13, 15)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
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
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 14)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel7)
        Me.RadGroupBox8.HeaderText = "Outlet"
        Me.RadGroupBox8.Location = New System.Drawing.Point(3, 50)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(273, 258)
        Me.RadGroupBox8.TabIndex = 312
        Me.RadGroupBox8.Text = "Outlet"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(253, 208)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rdselectoutlet)
        Me.Panel7.Controls.Add(Me.rdalloutlet)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(253, 20)
        Me.Panel7.TabIndex = 0
        '
        'rdselectoutlet
        '
        Me.rdselectoutlet.Location = New System.Drawing.Point(131, 1)
        Me.rdselectoutlet.Name = "rdselectoutlet"
        Me.rdselectoutlet.Size = New System.Drawing.Size(50, 18)
        Me.rdselectoutlet.TabIndex = 110
        Me.rdselectoutlet.Text = "Select"
        '
        'rdalloutlet
        '
        Me.rdalloutlet.Location = New System.Drawing.Point(61, 1)
        Me.rdalloutlet.Name = "rdalloutlet"
        Me.rdalloutlet.Size = New System.Drawing.Size(33, 18)
        Me.rdalloutlet.TabIndex = 109
        Me.rdalloutlet.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(997, 377)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(997, 377)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(867, 622)
        Me.SplitContainer1.SplitterDistance = 578
        Me.SplitContainer1.TabIndex = 8
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(787, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 53
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.btnExcel, Me.btnPdf})
        Me.btnExport.Location = New System.Drawing.Point(155, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 21)
        Me.btnExport.TabIndex = 52
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "__________________"
        Me.RadMenuItem2.AccessibleName = "__________________"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "__________________"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = My.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPdf
        '
        Me.btnPdf.AccessibleDescription = "PDF"
        Me.btnPdf.AccessibleName = "PDF"
        Me.btnPdf.Image = My.Resources.pdf
        Me.btnPdf.Name = "btnPdf"
        Me.btnPdf.Text = "PDF"
        Me.btnPdf.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(82, 9)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(68, 20)
        Me.RadButton2.TabIndex = 51
        Me.RadButton2.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(8, 9)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 20)
        Me.btnRefresh.TabIndex = 50
        Me.btnRefresh.Text = ">>>"
        '
        'FrmDaywisePendingComplaint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 622)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDaywisePendingComplaint"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DayWise Pending Complaint Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rdallsecndry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdselctsecndry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rdallprimary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdselctprimary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rdallcompl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdslctcompl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rdallasset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdselectasset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbpending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbnotcomplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbComplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rdselectoutlet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdalloutlet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgasset As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbpending As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbnotcomplete As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbComplete As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdselectoutlet As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdalloutlet As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkcompl As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkprimary As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chksecndry As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPdf As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdselctsecndry As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdselctprimary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdslctcompl As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdselectasset As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdallasset As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdallcompl As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdallprimary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdallsecndry As Telerik.WinControls.UI.RadRadioButton
End Class

