<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRptPurchaseReturnBook
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkVendorSelect = New common.Controls.MyRadioButton
        Me.chkVendorAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgPR = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkPRSelect = New common.Controls.MyRadioButton
        Me.chkPRAll = New common.Controls.MyRadioButton
        Me.grpTemplate = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemall = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnFG = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnRMOther = New Telerik.WinControls.UI.RadRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.grpCustomerType = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgSubcategory = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkSubCatgSelect = New common.Controls.MyRadioButton
        Me.chkSubCatgAll = New common.Controls.MyRadioButton
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.grpLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.grpCategory = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCategory = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkCatgSelect = New common.Controls.MyRadioButton
        Me.chkCatgAll = New common.Controls.MyRadioButton
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvReport = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExportToExcel = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkPRSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPRAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTemplate.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRMOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCustomerType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomerType.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkSubCatgSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubCatgAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCategory.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkCatgSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCatgAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(902, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportToExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(902, 569)
        Me.SplitContainer1.SplitterDistance = 533
        Me.SplitContainer1.TabIndex = 5
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(902, 533)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.grpTemplate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.grpCustomerType)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.grpLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.grpCategory)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(881, 485)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Vendor"
        Me.RadGroupBox3.Location = New System.Drawing.Point(431, 188)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(424, 142)
        Me.RadGroupBox3.TabIndex = 310
        Me.RadGroupBox3.Text = "Vendor"
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
        Me.cbgVendor.Size = New System.Drawing.Size(404, 92)
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
        Me.Panel1.Size = New System.Drawing.Size(404, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(198, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(150, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgPR)
        Me.RadGroupBox1.Controls.Add(Me.Panel6)
        Me.RadGroupBox1.HeaderText = "Purchase Return"
        Me.RadGroupBox1.Location = New System.Drawing.Point(431, 331)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(424, 142)
        Me.RadGroupBox1.TabIndex = 309
        Me.RadGroupBox1.Text = "Purchase Return"
        '
        'cbgPR
        '
        Me.cbgPR.CheckedValue = Nothing
        Me.cbgPR.DataSource = Nothing
        Me.cbgPR.DisplayMember = "Name"
        Me.cbgPR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgPR.Location = New System.Drawing.Point(10, 40)
        Me.cbgPR.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgPR.MyShowHeadrText = False
        Me.cbgPR.Name = "cbgPR"
        Me.cbgPR.Size = New System.Drawing.Size(404, 92)
        Me.cbgPR.TabIndex = 2
        Me.cbgPR.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkPRSelect)
        Me.Panel6.Controls.Add(Me.chkPRAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(404, 20)
        Me.Panel6.TabIndex = 1
        '
        'chkPRSelect
        '
        Me.chkPRSelect.Location = New System.Drawing.Point(198, 1)
        Me.chkPRSelect.MyLinkLable1 = Nothing
        Me.chkPRSelect.MyLinkLable2 = Nothing
        Me.chkPRSelect.Name = "chkPRSelect"
        Me.chkPRSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkPRSelect.TabIndex = 2
        Me.chkPRSelect.Text = "Select"
        '
        'chkPRAll
        '
        Me.chkPRAll.Location = New System.Drawing.Point(150, 1)
        Me.chkPRAll.MyLinkLable1 = Nothing
        Me.chkPRAll.MyLinkLable2 = Nothing
        Me.chkPRAll.Name = "chkPRAll"
        Me.chkPRAll.Size = New System.Drawing.Size(33, 18)
        Me.chkPRAll.TabIndex = 1
        Me.chkPRAll.Text = "All"
        '
        'grpTemplate
        '
        Me.grpTemplate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpTemplate.Controls.Add(Me.cbgItem)
        Me.grpTemplate.Controls.Add(Me.Panel5)
        Me.grpTemplate.HeaderText = "Item"
        Me.grpTemplate.Location = New System.Drawing.Point(3, 331)
        Me.grpTemplate.Name = "grpTemplate"
        Me.grpTemplate.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpTemplate.Size = New System.Drawing.Size(424, 142)
        Me.grpTemplate.TabIndex = 53
        Me.grpTemplate.Text = "Item"
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
        Me.cbgItem.Size = New System.Drawing.Size(404, 92)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkItemSelect)
        Me.Panel5.Controls.Add(Me.chkItemall)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(404, 20)
        Me.Panel5.TabIndex = 1
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(197, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 2
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemall
        '
        Me.chkItemall.Location = New System.Drawing.Point(152, 2)
        Me.chkItemall.MyLinkLable1 = Nothing
        Me.chkItemall.MyLinkLable2 = Nothing
        Me.chkItemall.Name = "chkItemall"
        Me.chkItemall.Size = New System.Drawing.Size(33, 18)
        Me.chkItemall.TabIndex = 1
        Me.chkItemall.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnFG)
        Me.RadGroupBox2.Controls.Add(Me.rbtnRMOther)
        Me.RadGroupBox2.HeaderText = "Item Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(308, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(171, 37)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Type"
        '
        'rbtnFG
        '
        Me.rbtnFG.Location = New System.Drawing.Point(9, 12)
        Me.rbtnFG.Name = "rbtnFG"
        Me.rbtnFG.Size = New System.Drawing.Size(84, 18)
        Me.rbtnFG.TabIndex = 1
        Me.rbtnFG.TabStop = True
        Me.rbtnFG.Text = "Finish Goods"
        Me.rbtnFG.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnRMOther
        '
        Me.rbtnRMOther.Location = New System.Drawing.Point(98, 12)
        Me.rbtnRMOther.Name = "rbtnRMOther"
        Me.rbtnRMOther.Size = New System.Drawing.Size(69, 18)
        Me.rbtnRMOther.TabIndex = 0
        Me.rbtnRMOther.Text = "RM Other"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(217, 8)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'grpCustomerType
        '
        Me.grpCustomerType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomerType.Controls.Add(Me.cbgSubcategory)
        Me.grpCustomerType.Controls.Add(Me.Panel4)
        Me.grpCustomerType.HeaderText = "Sub Category"
        Me.grpCustomerType.Location = New System.Drawing.Point(431, 45)
        Me.grpCustomerType.Name = "grpCustomerType"
        Me.grpCustomerType.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomerType.Size = New System.Drawing.Size(424, 142)
        Me.grpCustomerType.TabIndex = 47
        Me.grpCustomerType.Text = "Sub Category"
        '
        'cbgSubcategory
        '
        Me.cbgSubcategory.CheckedValue = Nothing
        Me.cbgSubcategory.DataSource = Nothing
        Me.cbgSubcategory.DisplayMember = "Name"
        Me.cbgSubcategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSubcategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgSubcategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSubcategory.MyShowHeadrText = False
        Me.cbgSubcategory.Name = "cbgSubcategory"
        Me.cbgSubcategory.Size = New System.Drawing.Size(404, 92)
        Me.cbgSubcategory.TabIndex = 1
        Me.cbgSubcategory.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkSubCatgSelect)
        Me.Panel4.Controls.Add(Me.chkSubCatgAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(404, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkSubCatgSelect
        '
        Me.chkSubCatgSelect.Location = New System.Drawing.Point(198, 1)
        Me.chkSubCatgSelect.MyLinkLable1 = Nothing
        Me.chkSubCatgSelect.MyLinkLable2 = Nothing
        Me.chkSubCatgSelect.Name = "chkSubCatgSelect"
        Me.chkSubCatgSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCatgSelect.TabIndex = 1
        Me.chkSubCatgSelect.Text = "Select"
        '
        'chkSubCatgAll
        '
        Me.chkSubCatgAll.Location = New System.Drawing.Point(150, 1)
        Me.chkSubCatgAll.MyLinkLable1 = Nothing
        Me.chkSubCatgAll.MyLinkLable2 = Nothing
        Me.chkSubCatgAll.Name = "chkSubCatgAll"
        Me.chkSubCatgAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSubCatgAll.TabIndex = 0
        Me.chkSubCatgAll.Text = "All"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(73, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'grpLocation
        '
        Me.grpLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpLocation.Controls.Add(Me.cbgLocation)
        Me.grpLocation.Controls.Add(Me.Panel3)
        Me.grpLocation.HeaderText = "Location"
        Me.grpLocation.Location = New System.Drawing.Point(3, 188)
        Me.grpLocation.Name = "grpLocation"
        Me.grpLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpLocation.Size = New System.Drawing.Size(424, 142)
        Me.grpLocation.TabIndex = 308
        Me.grpLocation.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(404, 92)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(404, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(197, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(152, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(166, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'grpCategory
        '
        Me.grpCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCategory.Controls.Add(Me.cbgCategory)
        Me.grpCategory.Controls.Add(Me.Panel2)
        Me.grpCategory.HeaderText = "Category"
        Me.grpCategory.Location = New System.Drawing.Point(3, 45)
        Me.grpCategory.Name = "grpCategory"
        Me.grpCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCategory.Size = New System.Drawing.Size(424, 142)
        Me.grpCategory.TabIndex = 303
        Me.grpCategory.Text = "Category"
        '
        'cbgCategory
        '
        Me.cbgCategory.CheckedValue = Nothing
        Me.cbgCategory.DataSource = Nothing
        Me.cbgCategory.DisplayMember = "Name"
        Me.cbgCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCategory.MyShowHeadrText = False
        Me.cbgCategory.Name = "cbgCategory"
        Me.cbgCategory.Size = New System.Drawing.Size(404, 92)
        Me.cbgCategory.TabIndex = 1
        Me.cbgCategory.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkCatgSelect)
        Me.Panel2.Controls.Add(Me.chkCatgAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(404, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkCatgSelect
        '
        Me.chkCatgSelect.Location = New System.Drawing.Point(197, 1)
        Me.chkCatgSelect.MyLinkLable1 = Nothing
        Me.chkCatgSelect.MyLinkLable2 = Nothing
        Me.chkCatgSelect.Name = "chkCatgSelect"
        Me.chkCatgSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCatgSelect.TabIndex = 1
        Me.chkCatgSelect.Text = "Select"
        '
        'chkCatgAll
        '
        Me.chkCatgAll.Location = New System.Drawing.Point(152, 1)
        Me.chkCatgAll.MyLinkLable1 = Nothing
        Me.chkCatgAll.MyLinkLable2 = Nothing
        Me.chkCatgAll.Name = "chkCatgAll"
        Me.chkCatgAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCatgAll.TabIndex = 0
        Me.chkCatgAll.Text = "All"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(7, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvReport)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(881, 485)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvReport
        '
        Me.gvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReport.Location = New System.Drawing.Point(0, 0)
        '
        'gvReport
        '
        Me.gvReport.MasterTemplate.AllowAddNewRow = False
        Me.gvReport.MasterTemplate.AllowEditRow = False
        Me.gvReport.Name = "gvReport"
        Me.gvReport.ShowGroupPanel = False
        Me.gvReport.Size = New System.Drawing.Size(881, 485)
        Me.gvReport.TabIndex = 0
        Me.gvReport.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(162, 7)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 18)
        Me.btnExport.TabIndex = 14
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(263, 7)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(118, 18)
        Me.btnExportToExcel.TabIndex = 13
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(15, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 12
        Me.btnRefresh.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(819, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(88, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 10
        Me.btnReset.Text = "Reset"
        '
        'FrmRptPurchaseReturnBook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(902, 589)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmRptPurchaseReturnBook"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Return Book"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkPRSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPRAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTemplate.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRMOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCustomerType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomerType.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkSubCatgSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubCatgAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCategory.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkCatgSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCatgAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents grpTemplate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnFG As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnRMOther As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents grpCustomerType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubcategory As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCatgSelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCatgAll As common.Controls.MyRadioButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents grpLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents grpCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkCatgSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCatgAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvReport As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgPR As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkPRSelect As common.Controls.MyRadioButton
    Friend WithEvents chkPRAll As common.Controls.MyRadioButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportToExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
End Class

