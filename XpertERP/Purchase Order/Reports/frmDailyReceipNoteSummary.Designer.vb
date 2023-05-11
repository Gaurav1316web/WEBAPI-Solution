<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDailyReceipNoteSummary
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
        Me.Gr1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgSubCategory = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkSubCatSelect = New common.Controls.MyRadioButton
        Me.chkSubCatAll = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkItemselect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnVendorWise = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnItemWise = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkVendorSelect = New common.Controls.MyRadioButton
        Me.chkVendorall = New common.Controls.MyRadioButton
        Me.btnclose1 = New Telerik.WinControls.UI.RadButton
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.Gr1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Gr1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkSubCatSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubCatAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkItemselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rbtnVendorWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Gr1
        '
        Me.Gr1.AccessibleName = "VendorWise"
        Me.Gr1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Gr1.Controls.Add(Me.RadGroupBox5)
        Me.Gr1.Controls.Add(Me.RadGroupBox6)
        Me.Gr1.Controls.Add(Me.RadGroupBox2)
        Me.Gr1.Controls.Add(Me.RadGroupBox4)
        Me.Gr1.Controls.Add(Me.RadGroupBox3)
        Me.Gr1.HeaderText = ""
        Me.Gr1.Location = New System.Drawing.Point(6, 15)
        Me.Gr1.Name = "Gr1"
        Me.Gr1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Gr1.Size = New System.Drawing.Size(898, 400)
        Me.Gr1.TabIndex = 2
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgSubCategory)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Sub Category"
        Me.RadGroupBox5.Location = New System.Drawing.Point(452, 214)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(439, 171)
        Me.RadGroupBox5.TabIndex = 26
        Me.RadGroupBox5.Text = "Sub Category"
        '
        'cbgSubCategory
        '
        Me.cbgSubCategory.AccessibleName = "cbgSubCategory"
        Me.cbgSubCategory.CheckedValue = Nothing
        Me.cbgSubCategory.DataSource = Nothing
        Me.cbgSubCategory.DisplayMember = "Name"
        Me.cbgSubCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSubCategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgSubCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSubCategory.MyShowHeadrText = False
        Me.cbgSubCategory.Name = "cbgSubCategory"
        Me.cbgSubCategory.Size = New System.Drawing.Size(419, 121)
        Me.cbgSubCategory.TabIndex = 1
        Me.cbgSubCategory.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkSubCatSelect)
        Me.Panel3.Controls.Add(Me.chkSubCatAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(419, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkSubCatSelect
        '
        Me.chkSubCatSelect.AccessibleName = "chk_doc_select"
        Me.chkSubCatSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkSubCatSelect.MyLinkLable1 = Nothing
        Me.chkSubCatSelect.MyLinkLable2 = Nothing
        Me.chkSubCatSelect.Name = "chkSubCatSelect"
        Me.chkSubCatSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCatSelect.TabIndex = 1
        Me.chkSubCatSelect.Text = "Select"
        '
        'chkSubCatAll
        '
        Me.chkSubCatAll.AccessibleName = "chk_All"
        Me.chkSubCatAll.Location = New System.Drawing.Point(141, 1)
        Me.chkSubCatAll.MyLinkLable1 = Nothing
        Me.chkSubCatAll.MyLinkLable2 = Nothing
        Me.chkSubCatAll.Name = "chkSubCatAll"
        Me.chkSubCatAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSubCatAll.TabIndex = 0
        Me.chkSubCatAll.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgItem)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Item"
        Me.RadGroupBox6.Location = New System.Drawing.Point(452, 37)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(439, 171)
        Me.RadGroupBox6.TabIndex = 25
        Me.RadGroupBox6.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = "cbgItem"
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(419, 121)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkItemselect)
        Me.Panel4.Controls.Add(Me.chkItemAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(419, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkItemselect
        '
        Me.chkItemselect.AccessibleName = "chkItemselect"
        Me.chkItemselect.Location = New System.Drawing.Point(192, 1)
        Me.chkItemselect.MyLinkLable1 = Nothing
        Me.chkItemselect.MyLinkLable2 = Nothing
        Me.chkItemselect.Name = "chkItemselect"
        Me.chkItemselect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemselect.TabIndex = 1
        Me.chkItemselect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.AccessibleName = "chkItemAll"
        Me.chkItemAll.Location = New System.Drawing.Point(141, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 214)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(439, 171)
        Me.RadGroupBox2.TabIndex = 23
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgDoc"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(419, 121)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(419, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.AccessibleName = "chk_doc_select"
        Me.chkLocSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.AccessibleName = "chk_All"
        Me.chkLocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rbtnVendorWise)
        Me.RadGroupBox4.Controls.Add(Me.rbtnItemWise)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox4.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox4.Controls.Add(Me.dtptodate)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(7, 6)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(884, 28)
        Me.RadGroupBox4.TabIndex = 24
        '
        'rbtnVendorWise
        '
        Me.rbtnVendorWise.Location = New System.Drawing.Point(518, 4)
        Me.rbtnVendorWise.Name = "rbtnVendorWise"
        Me.rbtnVendorWise.Size = New System.Drawing.Size(84, 18)
        Me.rbtnVendorWise.TabIndex = 20
        Me.rbtnVendorWise.Text = "Vendor Wise"
        '
        'rbtnItemWise
        '
        Me.rbtnItemWise.Location = New System.Drawing.Point(376, 4)
        Me.rbtnItemWise.Name = "rbtnItemWise"
        Me.rbtnItemWise.Size = New System.Drawing.Size(70, 18)
        Me.rbtnItemWise.TabIndex = 19
        Me.rbtnItemWise.Text = "Item Wise"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(4, 4)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "From Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(202, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "To Date"
        '
        'dtpfromdate
        '
        Me.dtpfromdate.AccessibleName = "dtpfromdate"
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(67, 3)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 10
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "23-01-2012"
        Me.dtpfromdate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'dtptodate
        '
        Me.dtptodate.AccessibleName = "dtptodate"
        Me.dtptodate.CustomFormat = "dd-MM-yyyy"
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.Location = New System.Drawing.Point(253, 3)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(82, 20)
        Me.dtptodate.TabIndex = 11
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "23-01-2012"
        Me.dtptodate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Vendor"
        Me.RadGroupBox3.Location = New System.Drawing.Point(7, 37)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(439, 171)
        Me.RadGroupBox3.TabIndex = 12
        Me.RadGroupBox3.Text = "Vendor"
        '
        'cbgVendor
        '
        Me.cbgVendor.AccessibleName = "cbgDoc"
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(419, 121)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkVendorSelect)
        Me.Panel2.Controls.Add(Me.chkVendorall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(419, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.AccessibleName = "chk_doc_select"
        Me.chkVendorSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorall
        '
        Me.chkVendorall.AccessibleName = "chk_All"
        Me.chkVendorall.Location = New System.Drawing.Point(141, 1)
        Me.chkVendorall.MyLinkLable1 = Nothing
        Me.chkVendorall.MyLinkLable2 = Nothing
        Me.chkVendorall.Name = "chkVendorall"
        Me.chkVendorall.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorall.TabIndex = 0
        Me.chkVendorall.Text = "All"
        '
        'btnclose1
        '
        Me.btnclose1.AccessibleName = "btnclose"
        Me.btnclose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose1.Location = New System.Drawing.Point(829, 7)
        Me.btnclose1.Name = "btnclose1"
        Me.btnclose1.Size = New System.Drawing.Size(68, 18)
        Me.btnclose1.TabIndex = 16
        Me.btnclose1.Text = "Close"
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(12, 7)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 15
        Me.btnreset1.Text = "Reset"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnprint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(86, 7)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 14
        Me.btnprint1.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gr1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose1)
        Me.SplitContainer1.Size = New System.Drawing.Size(906, 458)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 3
        '
        'FrmDailyReceipNoteSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 458)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDailyReceipNoteSummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Receipt Note-Summary"
        CType(Me.Gr1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Gr1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkSubCatSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubCatAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkItemselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rbtnVendorWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Gr1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorall As common.Controls.MyRadioButton
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents btnclose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCatSelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCatAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkItemselect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents rbtnVendorWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnItemWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

