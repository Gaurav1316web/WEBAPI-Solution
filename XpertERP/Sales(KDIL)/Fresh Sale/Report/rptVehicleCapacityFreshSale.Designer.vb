<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptVehicleCapacityFreshSale
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.CbgCustomerGroup = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ChkCustSelect = New common.Controls.MyRadioButton()
        Me.ChkCustAll = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.SearchToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkSelectLocation = New common.Controls.MyRadioButton()
        Me.chkAllLocation = New common.Controls.MyRadioButton()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVeh = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ChkVehSelect = New common.Controls.MyRadioButton()
        Me.ChkVehAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.ChkCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkSelectLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ChkVehSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkVehAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 575)
        Me.SplitContainer1.SplitterDistance = 510
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(749, 490)
        Me.RadPageView1.TabIndex = 11
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.SearchToDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(728, 442)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.CbgCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Customer Group"
        Me.RadGroupBox2.Location = New System.Drawing.Point(16, 259)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(339, 206)
        Me.RadGroupBox2.TabIndex = 311
        Me.RadGroupBox2.Text = "Customer Group"
        '
        'CbgCustomerGroup
        '
        Me.CbgCustomerGroup.CheckedValue = Nothing
        Me.CbgCustomerGroup.DataSource = Nothing
        Me.CbgCustomerGroup.DisplayMember = "Name"
        Me.CbgCustomerGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CbgCustomerGroup.Location = New System.Drawing.Point(10, 43)
        Me.CbgCustomerGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.CbgCustomerGroup.MyShowHeadrText = False
        Me.CbgCustomerGroup.Name = "CbgCustomerGroup"
        Me.CbgCustomerGroup.Size = New System.Drawing.Size(319, 153)
        Me.CbgCustomerGroup.TabIndex = 1
        Me.CbgCustomerGroup.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ChkCustSelect)
        Me.Panel3.Controls.Add(Me.ChkCustAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(319, 23)
        Me.Panel3.TabIndex = 0
        '
        'ChkCustSelect
        '
        Me.ChkCustSelect.Location = New System.Drawing.Point(186, 4)
        Me.ChkCustSelect.MyLinkLable1 = Nothing
        Me.ChkCustSelect.MyLinkLable2 = Nothing
        Me.ChkCustSelect.Name = "ChkCustSelect"
        Me.ChkCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkCustSelect.TabIndex = 1
        Me.ChkCustSelect.Text = "Select"
        '
        'ChkCustAll
        '
        Me.ChkCustAll.Location = New System.Drawing.Point(147, 4)
        Me.ChkCustAll.MyLinkLable1 = Nothing
        Me.ChkCustAll.MyLinkLable2 = Nothing
        Me.ChkCustAll.Name = "ChkCustAll"
        Me.ChkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkCustAll.TabIndex = 0
        Me.ChkCustAll.Text = "All"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(251, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 310
        Me.MyLabel1.Text = "To Date"
        '
        'SearchToDate
        '
        Me.SearchToDate.CustomFormat = "dd/MM/yyyy"
        Me.SearchToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.SearchToDate.Location = New System.Drawing.Point(298, 3)
        Me.SearchToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.SearchToDate.Name = "SearchToDate"
        Me.SearchToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.SearchToDate.Size = New System.Drawing.Size(151, 20)
        Me.SearchToDate.TabIndex = 309
        Me.SearchToDate.TabStop = False
        Me.SearchToDate.Text = "24/10/2011"
        Me.SearchToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(16, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 308
        Me.RadLabel1.Text = "From Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(82, 2)
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(151, 20)
        Me.txtDate.TabIndex = 307
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "24/10/2011"
        Me.txtDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel2)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(370, 47)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(339, 206)
        Me.RadGroupBox1.TabIndex = 306
        Me.RadGroupBox1.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(319, 153)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkSelectLocation)
        Me.Panel2.Controls.Add(Me.chkAllLocation)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(319, 23)
        Me.Panel2.TabIndex = 0
        '
        'chkSelectLocation
        '
        Me.chkSelectLocation.Location = New System.Drawing.Point(193, 4)
        Me.chkSelectLocation.MyLinkLable1 = Nothing
        Me.chkSelectLocation.MyLinkLable2 = Nothing
        Me.chkSelectLocation.Name = "chkSelectLocation"
        Me.chkSelectLocation.Size = New System.Drawing.Size(50, 18)
        Me.chkSelectLocation.TabIndex = 1
        Me.chkSelectLocation.Text = "Select"
        '
        'chkAllLocation
        '
        Me.chkAllLocation.Location = New System.Drawing.Point(154, 4)
        Me.chkAllLocation.MyLinkLable1 = Nothing
        Me.chkAllLocation.MyLinkLable2 = Nothing
        Me.chkAllLocation.Name = "chkAllLocation"
        Me.chkAllLocation.Size = New System.Drawing.Size(33, 18)
        Me.chkAllLocation.TabIndex = 0
        Me.chkAllLocation.Text = "All"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgVeh)
        Me.RadGroupBox8.Controls.Add(Me.Panel1)
        Me.RadGroupBox8.HeaderText = "Vehicle"
        Me.RadGroupBox8.Location = New System.Drawing.Point(16, 47)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(339, 206)
        Me.RadGroupBox8.TabIndex = 305
        Me.RadGroupBox8.Text = "Vehicle"
        '
        'cbgVeh
        '
        Me.cbgVeh.CheckedValue = Nothing
        Me.cbgVeh.DataSource = Nothing
        Me.cbgVeh.DisplayMember = "Name"
        Me.cbgVeh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVeh.Location = New System.Drawing.Point(10, 43)
        Me.cbgVeh.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVeh.MyShowHeadrText = False
        Me.cbgVeh.Name = "cbgVeh"
        Me.cbgVeh.Size = New System.Drawing.Size(319, 153)
        Me.cbgVeh.TabIndex = 1
        Me.cbgVeh.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkVehSelect)
        Me.Panel1.Controls.Add(Me.ChkVehAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(319, 23)
        Me.Panel1.TabIndex = 0
        '
        'ChkVehSelect
        '
        Me.ChkVehSelect.Location = New System.Drawing.Point(186, 4)
        Me.ChkVehSelect.MyLinkLable1 = Nothing
        Me.ChkVehSelect.MyLinkLable2 = Nothing
        Me.ChkVehSelect.Name = "ChkVehSelect"
        Me.ChkVehSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkVehSelect.TabIndex = 1
        Me.ChkVehSelect.Text = "Select"
        '
        'ChkVehAll
        '
        Me.ChkVehAll.Location = New System.Drawing.Point(147, 4)
        Me.ChkVehAll.MyLinkLable1 = Nothing
        Me.ChkVehAll.MyLinkLable2 = Nothing
        Me.ChkVehAll.Name = "ChkVehAll"
        Me.ChkVehAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkVehAll.TabIndex = 0
        Me.ChkVehAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(728, 346)
        Me.RadPageViewPage2.Text = "Report"
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
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(728, 346)
        Me.gv1.TabIndex = 5
        Me.gv1.Text = "gv"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(749, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.AccessibleDescription = "Save Layout"
        Me.rmsaveLayout.AccessibleName = "Save Layout"
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(651, 27)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(6, 28)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 8
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(78, 28)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(153, 28)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 21)
        Me.RadSplitButton1.TabIndex = 10
        Me.RadSplitButton1.Text = "Export"
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
        'RptVehicleCapacityFreshSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 575)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptVehicleCapacityFreshSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptVehicleCapacityFreshSale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.ChkCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkSelectLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ChkVehSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkVehAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectLocation As common.Controls.MyRadioButton
    Friend WithEvents chkAllLocation As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVeh As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ChkVehSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkVehAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SearchToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CbgCustomerGroup As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ChkCustSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkCustAll As common.Controls.MyRadioButton
End Class

