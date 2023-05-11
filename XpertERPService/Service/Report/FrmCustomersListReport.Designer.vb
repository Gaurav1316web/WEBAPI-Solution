Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomersListReport
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
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.lblstatus = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdbagainstReq = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.rdobtncompletd = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbwithoutReq = New Telerik.WinControls.UI.RadRadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgvendor = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkVendor_select = New common.Controls.MyRadioButton
        Me.chkVendor_all = New common.Controls.MyRadioButton
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.dtpTodate = New common.Controls.MyDateTimePicker
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGridView1 = New common.UserControls.MyRadGridView
        Me.gv = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkCustomer_select = New common.Controls.MyRadioButton
        Me.chkCustomer_all = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnclose1 = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager
        Me.RadPageViewPage3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rdbagainstReq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtncompletd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbwithoutReq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkCustomer_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomer_all, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.lblstatus)
        Me.RadPageViewPage3.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage3.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox13)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage3.Controls.Add(Me.dtpfromdate)
        Me.RadPageViewPage3.Controls.Add(Me.dtpTodate)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1033, 485)
        Me.RadPageViewPage3.Text = "Filter"
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Location = New System.Drawing.Point(10, 40)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(37, 13)
        Me.lblstatus.TabIndex = 10
        Me.lblstatus.Text = "Status"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdbagainstReq)
        Me.GroupBox2.Controls.Add(Me.rdbAll)
        Me.GroupBox2.Controls.Add(Me.rdobtncompletd)
        Me.GroupBox2.Controls.Add(Me.rdbwithoutReq)
        Me.GroupBox2.Location = New System.Drawing.Point(65, 29)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(410, 32)
        Me.GroupBox2.TabIndex = 112
        Me.GroupBox2.TabStop = False
        '
        'rdbagainstReq
        '
        Me.rdbagainstReq.Location = New System.Drawing.Point(58, 8)
        Me.rdbagainstReq.Name = "rdbagainstReq"
        Me.rdbagainstReq.Size = New System.Drawing.Size(117, 18)
        Me.rdbagainstReq.TabIndex = 12
        Me.rdbagainstReq.Text = "Against Requisition"
        '
        'rdbAll
        '
        Me.rdbAll.Location = New System.Drawing.Point(6, 8)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbAll.TabIndex = 11
        Me.rdbAll.TabStop = True
        Me.rdbAll.Text = "All"
        Me.rdbAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdobtncompletd
        '
        Me.rdobtncompletd.Location = New System.Drawing.Point(317, 8)
        Me.rdobtncompletd.Name = "rdobtncompletd"
        Me.rdobtncompletd.Size = New System.Drawing.Size(75, 18)
        Me.rdobtncompletd.TabIndex = 12
        Me.rdobtncompletd.Text = "Completed"
        Me.rdobtncompletd.Visible = False
        '
        'rdbwithoutReq
        '
        Me.rdbwithoutReq.Location = New System.Drawing.Point(187, 8)
        Me.rdbwithoutReq.Name = "rdbwithoutReq"
        Me.rdbwithoutReq.Size = New System.Drawing.Size(119, 18)
        Me.rdbwithoutReq.TabIndex = 12
        Me.rdbwithoutReq.Text = "Without Requisition"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbDetail)
        Me.GroupBox1.Controls.Add(Me.rdbSummary)
        Me.GroupBox1.Location = New System.Drawing.Point(293, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 32)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(103, 9)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 1
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(7, 9)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 0
        Me.rdbSummary.TabStop = True
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.HeaderText = " Location"
        Me.RadGroupBox13.Location = New System.Drawing.Point(3, 262)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(472, 200)
        Me.RadGroupBox13.TabIndex = 73
        Me.RadGroupBox13.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(452, 145)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkLocationSelect)
        Me.Panel9.Controls.Add(Me.chkLocationAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(452, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(220, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(168, 4)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgvendor)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Vendor"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 68)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(472, 186)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Vendor"
        '
        'cbgvendor
        '
        Me.cbgvendor.AccessibleDescription = "cbdvendor"
        Me.cbgvendor.AccessibleName = ""
        Me.cbgvendor.CheckedValue = Nothing
        Me.cbgvendor.DataSource = Nothing
        Me.cbgvendor.DisplayMember = "Name"
        Me.cbgvendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgvendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgvendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgvendor.MyShowHeadrText = False
        Me.cbgvendor.Name = "cbgvendor"
        Me.cbgvendor.Size = New System.Drawing.Size(452, 136)
        Me.cbgvendor.TabIndex = 1
        Me.cbgvendor.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVendor_select)
        Me.Panel1.Controls.Add(Me.chkVendor_all)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkVendor_select
        '
        Me.chkVendor_select.AccessibleDescription = "chkVendor_select"
        Me.chkVendor_select.Location = New System.Drawing.Point(219, 1)
        Me.chkVendor_select.MyLinkLable1 = Nothing
        Me.chkVendor_select.MyLinkLable2 = Nothing
        Me.chkVendor_select.Name = "chkVendor_select"
        Me.chkVendor_select.Size = New System.Drawing.Size(50, 18)
        Me.chkVendor_select.TabIndex = 1
        Me.chkVendor_select.Text = "Select"
        '
        'chkVendor_all
        '
        Me.chkVendor_all.AccessibleDescription = "chkVendor_all"
        Me.chkVendor_all.Location = New System.Drawing.Point(168, 1)
        Me.chkVendor_all.MyLinkLable1 = Nothing
        Me.chkVendor_all.MyLinkLable2 = Nothing
        Me.chkVendor_all.Name = "chkVendor_all"
        Me.chkVendor_all.Size = New System.Drawing.Size(33, 18)
        Me.chkVendor_all.TabIndex = 0
        Me.chkVendor_all.Text = "All"
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(154, 3)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel5.TabIndex = 9
        Me.RadLabel5.Text = "To Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.Location = New System.Drawing.Point(2, 3)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel6.TabIndex = 8
        Me.RadLabel6.Text = "From Date"
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(65, 2)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 0
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "14-09-2011"
        Me.dtpfromdate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpTodate
        '
        Me.dtpTodate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.Location = New System.Drawing.Point(205, 3)
        Me.dtpTodate.MendatroryField = False
        Me.dtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.MyLinkLable1 = Nothing
        Me.dtpTodate.MyLinkLable2 = Nothing
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.Size = New System.Drawing.Size(82, 20)
        Me.dtpTodate.TabIndex = 1
        Me.dtpTodate.TabStop = False
        Me.dtpTodate.Text = "14-09-2011"
        Me.dtpTodate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadGridView1)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1033, 485)
        Me.RadPageViewPage4.Text = "Report"
        '
        'RadGridView1
        '
        Me.RadGridView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.RadGridView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.RadGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGridView1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RadGridView1.ForeColor = System.Drawing.Color.Black
        Me.RadGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RadGridView1.Location = New System.Drawing.Point(0, 0)
        '
        'RadGridView1
        '
        Me.RadGridView1.MasterTemplate.EnableGrouping = False
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ReadOnly = True
        Me.RadGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RadGridView1.ShowGroupPanel = False
        Me.RadGridView1.Size = New System.Drawing.Size(1033, 485)
        Me.RadGridView1.TabIndex = 0
        Me.RadGridView1.Text = "RadGridView1"
        '
        'gv
        '
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(240, 150)
        Me.gv.TabIndex = 0
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnrefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(1054, 566)
        Me.SplitContainer1.SplitterDistance = 522
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1027, 522)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "List Of Customers"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1006, 474)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel5)
        Me.RadGroupBox8.HeaderText = "Customer"
        Me.RadGroupBox8.Location = New System.Drawing.Point(3, 13)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(1000, 458)
        Me.RadGroupBox8.TabIndex = 5
        Me.RadGroupBox8.Text = "Customer"
        Me.RadGroupBox8.ThemeName = "ControlDefault"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.AccessibleDescription = "cbgCustomer"
        Me.cbgCustomer.AccessibleName = ""
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(980, 408)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkCustomer_select)
        Me.Panel5.Controls.Add(Me.chkCustomer_all)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(980, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkCustomer_select
        '
        Me.chkCustomer_select.AccessibleDescription = "chkCustomer_select"
        Me.chkCustomer_select.AccessibleName = ""
        Me.chkCustomer_select.Location = New System.Drawing.Point(460, 1)
        Me.chkCustomer_select.MyLinkLable1 = Nothing
        Me.chkCustomer_select.MyLinkLable2 = Nothing
        Me.chkCustomer_select.Name = "chkCustomer_select"
        Me.chkCustomer_select.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomer_select.TabIndex = 1
        Me.chkCustomer_select.Text = "Select"
        '
        'chkCustomer_all
        '
        Me.chkCustomer_all.AccessibleDescription = "chkCustomer_all"
        Me.chkCustomer_all.AccessibleName = ""
        Me.chkCustomer_all.Location = New System.Drawing.Point(415, 1)
        Me.chkCustomer_all.MyLinkLable1 = Nothing
        Me.chkCustomer_all.MyLinkLable2 = Nothing
        Me.chkCustomer_all.Name = "chkCustomer_all"
        Me.chkCustomer_all.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomer_all.TabIndex = 0
        Me.chkCustomer_all.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1006, 474)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.AutoScroll = True
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
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(1006, 474)
        Me.gv1.TabIndex = 1
        Me.gv1.Text = "RadGridView1"
        Me.gv1.ThemeName = "ControlDefault"
        '
        'btnclose1
        '
        Me.btnclose1.AccessibleDescription = "btnclose"
        Me.btnclose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose1.Location = New System.Drawing.Point(959, 18)
        Me.btnclose1.Name = "btnclose1"
        Me.btnclose1.Size = New System.Drawing.Size(68, 18)
        Me.btnclose1.TabIndex = 120
        Me.btnclose1.Text = "Close"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = "btnclose"
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(974, 57)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 119
        Me.btnclose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(175, 18)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(68, 18)
        Me.btnExport.TabIndex = 118
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "RadMenuItem1"
        Me.Export.AccessibleName = "RadMenuItem1"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "RadMenuItem2"
        Me.PDF.AccessibleName = "RadMenuItem2"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        Me.PDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleDescription = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(90, 19)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 117
        Me.btnreset1.Text = "&Reset"
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.Location = New System.Drawing.Point(10, 18)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnrefresh.TabIndex = 116
        Me.btnrefresh.Text = ">>>"
        '
        'FrmCustomersListReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 566)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCustomersListReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "List Of Customers Report"
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rdbagainstReq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtncompletd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbwithoutReq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkCustomer_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomer_all, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbagainstReq As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdobtncompletd As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbwithoutReq As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgvendor As common.MyCheckBoxGrid
    Friend WithEvents chkVendor_select As common.Controls.MyRadioButton
    Friend WithEvents chkVendor_all As common.Controls.MyRadioButton
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomer_select As common.Controls.MyRadioButton
    Friend WithEvents chkCustomer_all As common.Controls.MyRadioButton
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose1 As Telerik.WinControls.UI.RadButton
End Class

