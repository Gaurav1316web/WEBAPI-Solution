<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockAgeingReport
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
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkShelfLife = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkGITLocation = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLocation = New common.UserControls.MyRadGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.cmbUnit = New common.Controls.MyComboBox()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.cboItemStatus = New common.Controls.MyComboBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.cboAgeingColumns = New common.Controls.MyComboBox()
        Me.txtItemType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.cboInventoryType = New common.Controls.MyComboBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtOver = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txt1 = New common.Controls.MyTextBox()
        Me.txt2 = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txt3 = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txt4th = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.radbtnBulkExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExportSplit = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.BulkExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkCSV = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkShelfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGITLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAgeingColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInventoryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtOver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt4th, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(972, 505)
        Me.RadPageView1.TabIndex = 24
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(951, 457)
        Me.RadPageViewPage3.Text = "Filter"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox3.Controls.Add(Me.chkShelfLife)
        Me.RadGroupBox3.Controls.Add(Me.chkGITLocation)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(951, 457)
        Me.RadGroupBox3.TabIndex = 1
        '
        'chkShelfLife
        '
        Me.chkShelfLife.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShelfLife.Location = New System.Drawing.Point(561, 82)
        Me.chkShelfLife.Name = "chkShelfLife"
        Me.chkShelfLife.Size = New System.Drawing.Size(96, 16)
        Me.chkShelfLife.TabIndex = 343
        Me.chkShelfLife.Text = "Shelf Life Wise"
        '
        'chkGITLocation
        '
        Me.chkGITLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGITLocation.Location = New System.Drawing.Point(470, 82)
        Me.chkGITLocation.Name = "chkGITLocation"
        Me.chkGITLocation.Size = New System.Drawing.Size(85, 16)
        Me.chkGITLocation.TabIndex = 342
        Me.chkGITLocation.Text = "GIT Location"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox5.Controls.Add(Me.gvCategory)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.HeaderText = "Category"
        Me.RadGroupBox5.Location = New System.Drawing.Point(470, 192)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(452, 260)
        Me.RadGroupBox5.TabIndex = 341
        Me.RadGroupBox5.Text = "Category"
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(10, 40)
        '
        'gvCategory
        '
        Me.gvCategory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.ShowHeaderCellButtons = True
        Me.gvCategory.Size = New System.Drawing.Size(432, 210)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.SplitContainer3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(432, 20)
        Me.Panel2.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.SplitContainer3.Panel1.Controls.Add(Me.rbtnCategoryAll)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadButton6)
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadButton7)
        Me.SplitContainer3.Size = New System.Drawing.Size(432, 20)
        Me.SplitContainer3.SplitterDistance = 207
        Me.SplitContainer3.TabIndex = 2
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(102, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 2
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(63, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 1
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadButton6
        '
        Me.RadButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton6.Location = New System.Drawing.Point(95, 3)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(86, 15)
        Me.RadButton6.TabIndex = 5
        Me.RadButton6.Text = "Unselect All"
        '
        'RadButton7
        '
        Me.RadButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton7.Location = New System.Drawing.Point(6, 3)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(86, 15)
        Me.RadButton7.TabIndex = 4
        Me.RadButton7.Text = "Select All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gvLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 192)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(451, 260)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "Location"
        '
        'gvLocation
        '
        Me.gvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLocation.Location = New System.Drawing.Point(10, 40)
        '
        'gvLocation
        '
        Me.gvLocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLocation.Name = "gvLocation"
        Me.gvLocation.ShowHeaderCellButtons = True
        Me.gvLocation.Size = New System.Drawing.Size(431, 210)
        Me.gvLocation.TabIndex = 3
        Me.gvLocation.Text = "RadGridView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.SplitContainer2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(431, 20)
        Me.Panel4.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnLocationSelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnLocationAll)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton5)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer2.Size = New System.Drawing.Size(431, 20)
        Me.SplitContainer2.SplitterDistance = 211
        Me.SplitContainer2.TabIndex = 0
        '
        'rbtnLocationSelect
        '
        Me.rbtnLocationSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationSelect.Location = New System.Drawing.Point(109, 1)
        Me.rbtnLocationSelect.MyLinkLable1 = Nothing
        Me.rbtnLocationSelect.MyLinkLable2 = Nothing
        Me.rbtnLocationSelect.Name = "rbtnLocationSelect"
        Me.rbtnLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocationSelect.TabIndex = 2
        Me.rbtnLocationSelect.Text = "Select"
        '
        'rbtnLocationAll
        '
        Me.rbtnLocationAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationAll.Location = New System.Drawing.Point(70, 1)
        Me.rbtnLocationAll.MyLinkLable1 = Nothing
        Me.rbtnLocationAll.MyLinkLable2 = Nothing
        Me.rbtnLocationAll.Name = "rbtnLocationAll"
        Me.rbtnLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocationAll.TabIndex = 1
        Me.rbtnLocationAll.Text = "All"
        '
        'RadButton5
        '
        Me.RadButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton5.Location = New System.Drawing.Point(93, 2)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(86, 15)
        Me.RadButton5.TabIndex = 3
        Me.RadButton5.Text = "Unselect All"
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Location = New System.Drawing.Point(4, 2)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(86, 15)
        Me.RadButton4.TabIndex = 2
        Me.RadButton4.Text = "Select All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.cmbUnit)
        Me.RadGroupBox1.Controls.Add(Me.txtFiscalYear)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.cboItemStatus)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.cboAgeingColumns)
        Me.RadGroupBox1.Controls.Add(Me.txtItemType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.txtItem)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.cboType)
        Me.RadGroupBox1.Controls.Add(Me.cboInventoryType)
        Me.RadGroupBox1.HeaderText = "Main Filter"
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(451, 177)
        Me.RadGroupBox1.TabIndex = 340
        Me.RadGroupBox1.Text = "Main Filter"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(242, 20)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel14.TabIndex = 368
        Me.MyLabel14.Text = "UOM"
        '
        'cmbUnit
        '
        Me.cmbUnit.AutoCompleteDisplayMember = Nothing
        Me.cmbUnit.AutoCompleteValueMember = Nothing
        Me.cmbUnit.CalculationExpression = Nothing
        Me.cmbUnit.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbUnit.FieldCode = Nothing
        Me.cmbUnit.FieldDesc = Nothing
        Me.cmbUnit.FieldMaxLength = 0
        Me.cmbUnit.FieldName = Nothing
        Me.cmbUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit.isCalculatedField = False
        Me.cmbUnit.IsSourceFromTable = False
        Me.cmbUnit.IsSourceFromValueList = False
        Me.cmbUnit.IsUnique = False
        Me.cmbUnit.Location = New System.Drawing.Point(304, 22)
        Me.cmbUnit.MendatroryField = False
        Me.cmbUnit.MyLinkLable1 = Me.MyLabel14
        Me.cmbUnit.MyLinkLable2 = Nothing
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.ReferenceFieldDesc = Nothing
        Me.cmbUnit.ReferenceFieldName = Nothing
        Me.cmbUnit.ReferenceTableName = Nothing
        Me.cmbUnit.Size = New System.Drawing.Size(137, 18)
        Me.cmbUnit.TabIndex = 367
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(107, 20)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Me.RadLabel1
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(129, 20)
        Me.txtFiscalYear.TabIndex = 0
        Me.txtFiscalYear.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(58, 18)
        Me.RadLabel1.TabIndex = 366
        Me.RadLabel1.Text = "Fiscal Year"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(242, 91)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel13.TabIndex = 344
        Me.MyLabel13.Text = "Item Status"
        '
        'cboItemStatus
        '
        Me.cboItemStatus.CalculationExpression = Nothing
        Me.cboItemStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemStatus.FieldCode = Nothing
        Me.cboItemStatus.FieldDesc = Nothing
        Me.cboItemStatus.FieldMaxLength = 0
        Me.cboItemStatus.FieldName = Nothing
        Me.cboItemStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemStatus.isCalculatedField = False
        Me.cboItemStatus.IsSourceFromTable = False
        Me.cboItemStatus.IsSourceFromValueList = False
        Me.cboItemStatus.IsUnique = False
        RadListDataItem9.Text = "Detail"
        RadListDataItem10.Text = "Summary"
        Me.cboItemStatus.Items.Add(RadListDataItem9)
        Me.cboItemStatus.Items.Add(RadListDataItem10)
        Me.cboItemStatus.Location = New System.Drawing.Point(333, 91)
        Me.cboItemStatus.MendatroryField = False
        Me.cboItemStatus.MyLinkLable1 = Me.MyLabel13
        Me.cboItemStatus.MyLinkLable2 = Nothing
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.ReferenceFieldDesc = Nothing
        Me.cboItemStatus.ReferenceFieldName = Nothing
        Me.cboItemStatus.ReferenceTableName = Nothing
        Me.cboItemStatus.Size = New System.Drawing.Size(110, 18)
        Me.cboItemStatus.TabIndex = 6
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(13, 89)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel11.TabIndex = 342
        Me.MyLabel11.Text = "Ageing Columns"
        '
        'cboAgeingColumns
        '
        Me.cboAgeingColumns.CalculationExpression = Nothing
        Me.cboAgeingColumns.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAgeingColumns.FieldCode = Nothing
        Me.cboAgeingColumns.FieldDesc = Nothing
        Me.cboAgeingColumns.FieldMaxLength = 0
        Me.cboAgeingColumns.FieldName = Nothing
        Me.cboAgeingColumns.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAgeingColumns.isCalculatedField = False
        Me.cboAgeingColumns.IsSourceFromTable = False
        Me.cboAgeingColumns.IsSourceFromValueList = False
        Me.cboAgeingColumns.IsUnique = False
        RadListDataItem1.Text = "Detail"
        RadListDataItem2.Text = "Summary"
        Me.cboAgeingColumns.Items.Add(RadListDataItem1)
        Me.cboAgeingColumns.Items.Add(RadListDataItem2)
        Me.cboAgeingColumns.Location = New System.Drawing.Point(107, 91)
        Me.cboAgeingColumns.MendatroryField = False
        Me.cboAgeingColumns.MyLinkLable1 = Me.MyLabel11
        Me.cboAgeingColumns.MyLinkLable2 = Nothing
        Me.cboAgeingColumns.Name = "cboAgeingColumns"
        Me.cboAgeingColumns.ReferenceFieldDesc = Nothing
        Me.cboAgeingColumns.ReferenceFieldName = Nothing
        Me.cboAgeingColumns.ReferenceTableName = Nothing
        Me.cboAgeingColumns.Size = New System.Drawing.Size(129, 18)
        Me.cboAgeingColumns.TabIndex = 5
        '
        'txtItemType
        '
        Me.txtItemType.arrDispalyMember = Nothing
        Me.txtItemType.arrValueMember = Nothing
        Me.txtItemType.Location = New System.Drawing.Point(107, 114)
        Me.txtItemType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.MyLinkLable1 = Me.MyLabel10
        Me.txtItemType.MyLinkLable2 = Nothing
        Me.txtItemType.MyNullText = "All"
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(336, 19)
        Me.txtItemType.TabIndex = 7
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(13, 112)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel10.TabIndex = 339
        Me.MyLabel10.Text = "Item Type"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(13, 45)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel9.TabIndex = 0
        Me.MyLabel9.Text = "Cuttoff Date"
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
        Me.txtFromDate.Location = New System.Drawing.Point(107, 44)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel9
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(129, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(107, 138)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel2
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(336, 19)
        Me.txtItem.TabIndex = 8
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 136)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "Item"
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
        Me.txtToDate.Location = New System.Drawing.Point(333, 44)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel7
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(110, 20)
        Me.txtToDate.TabIndex = 2
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(242, 45)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel7.TabIndex = 2
        Me.MyLabel7.Text = "As On Date"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(242, 69)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel12.TabIndex = 23
        Me.MyLabel12.Text = "Inventory Type"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(13, 67)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel3.TabIndex = 23
        Me.MyLabel3.Text = "Report Type"
        '
        'cboType
        '
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        RadListDataItem3.Text = "Detail"
        RadListDataItem4.Text = "Summary"
        Me.cboType.Items.Add(RadListDataItem3)
        Me.cboType.Items.Add(RadListDataItem4)
        Me.cboType.Location = New System.Drawing.Point(107, 69)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel3
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(129, 18)
        Me.cboType.TabIndex = 3
        '
        'cboInventoryType
        '
        Me.cboInventoryType.CalculationExpression = Nothing
        Me.cboInventoryType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInventoryType.FieldCode = Nothing
        Me.cboInventoryType.FieldDesc = Nothing
        Me.cboInventoryType.FieldMaxLength = 0
        Me.cboInventoryType.FieldName = Nothing
        Me.cboInventoryType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInventoryType.isCalculatedField = False
        Me.cboInventoryType.IsSourceFromTable = False
        Me.cboInventoryType.IsSourceFromValueList = False
        Me.cboInventoryType.IsUnique = False
        RadListDataItem5.Text = "Detail"
        RadListDataItem6.Text = "Summary"
        Me.cboInventoryType.Items.Add(RadListDataItem5)
        Me.cboInventoryType.Items.Add(RadListDataItem6)
        Me.cboInventoryType.Location = New System.Drawing.Point(333, 69)
        Me.cboInventoryType.MendatroryField = False
        Me.cboInventoryType.MyLinkLable1 = Me.MyLabel12
        Me.cboInventoryType.MyLinkLable2 = Nothing
        Me.cboInventoryType.Name = "cboInventoryType"
        Me.cboInventoryType.ReferenceFieldDesc = Nothing
        Me.cboInventoryType.ReferenceFieldName = Nothing
        Me.cboInventoryType.ReferenceTableName = Nothing
        Me.cboInventoryType.Size = New System.Drawing.Size(110, 18)
        Me.cboInventoryType.TabIndex = 4
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtOver)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox4.Controls.Add(Me.txt1)
        Me.RadGroupBox4.Controls.Add(Me.txt2)
        Me.RadGroupBox4.Controls.Add(Me.txt3)
        Me.RadGroupBox4.Controls.Add(Me.txt4th)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox4.FooterTextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.RadGroupBox4.HeaderText = "Ageing Buckets"
        Me.RadGroupBox4.HeaderTextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.RadGroupBox4.Location = New System.Drawing.Point(470, 9)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(402, 67)
        Me.RadGroupBox4.TabIndex = 9
        Me.RadGroupBox4.Text = "Ageing Buckets"
        '
        'txtOver
        '
        Me.txtOver.CalculationExpression = Nothing
        Me.txtOver.FieldCode = Nothing
        Me.txtOver.FieldDesc = Nothing
        Me.txtOver.FieldMaxLength = 0
        Me.txtOver.FieldName = Nothing
        Me.txtOver.isCalculatedField = False
        Me.txtOver.IsSourceFromTable = False
        Me.txtOver.IsSourceFromValueList = False
        Me.txtOver.IsUnique = False
        Me.txtOver.Location = New System.Drawing.Point(323, 39)
        Me.txtOver.MendatroryField = False
        Me.txtOver.MyLinkLable1 = Me.MyLabel1
        Me.txtOver.MyLinkLable2 = Nothing
        Me.txtOver.Name = "txtOver"
        Me.txtOver.ReadOnly = True
        Me.txtOver.ReferenceFieldDesc = Nothing
        Me.txtOver.ReferenceFieldName = Nothing
        Me.txtOver.ReferenceTableName = Nothing
        Me.txtOver.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtOver.Size = New System.Drawing.Size(65, 20)
        Me.txtOver.TabIndex = 4
        Me.txtOver.Text = "120"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(344, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel1.TabIndex = 144
        Me.MyLabel1.Text = "Over"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(27, 21)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel8.TabIndex = 139
        Me.MyLabel8.Text = "1st"
        '
        'txt1
        '
        Me.txt1.CalculationExpression = Nothing
        Me.txt1.FieldCode = Nothing
        Me.txt1.FieldDesc = Nothing
        Me.txt1.FieldMaxLength = 0
        Me.txt1.FieldName = Nothing
        Me.txt1.isCalculatedField = False
        Me.txt1.IsSourceFromTable = False
        Me.txt1.IsSourceFromValueList = False
        Me.txt1.IsUnique = False
        Me.txt1.Location = New System.Drawing.Point(7, 40)
        Me.txt1.MendatroryField = False
        Me.txt1.MyLinkLable1 = Me.MyLabel8
        Me.txt1.MyLinkLable2 = Nothing
        Me.txt1.Name = "txt1"
        Me.txt1.ReferenceFieldDesc = Nothing
        Me.txt1.ReferenceFieldName = Nothing
        Me.txt1.ReferenceTableName = Nothing
        Me.txt1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt1.Size = New System.Drawing.Size(65, 20)
        Me.txt1.TabIndex = 0
        Me.txt1.Text = "30"
        '
        'txt2
        '
        Me.txt2.CalculationExpression = Nothing
        Me.txt2.FieldCode = Nothing
        Me.txt2.FieldDesc = Nothing
        Me.txt2.FieldMaxLength = 0
        Me.txt2.FieldName = Nothing
        Me.txt2.isCalculatedField = False
        Me.txt2.IsSourceFromTable = False
        Me.txt2.IsSourceFromValueList = False
        Me.txt2.IsUnique = False
        Me.txt2.Location = New System.Drawing.Point(87, 40)
        Me.txt2.MendatroryField = False
        Me.txt2.MyLinkLable1 = Me.MyLabel4
        Me.txt2.MyLinkLable2 = Nothing
        Me.txt2.Name = "txt2"
        Me.txt2.ReferenceFieldDesc = Nothing
        Me.txt2.ReferenceFieldName = Nothing
        Me.txt2.ReferenceTableName = Nothing
        Me.txt2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt2.Size = New System.Drawing.Size(65, 20)
        Me.txt2.TabIndex = 1
        Me.txt2.Text = "60"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(110, 22)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel4.TabIndex = 141
        Me.MyLabel4.Text = "2nd"
        '
        'txt3
        '
        Me.txt3.CalculationExpression = Nothing
        Me.txt3.FieldCode = Nothing
        Me.txt3.FieldDesc = Nothing
        Me.txt3.FieldMaxLength = 0
        Me.txt3.FieldName = Nothing
        Me.txt3.isCalculatedField = False
        Me.txt3.IsSourceFromTable = False
        Me.txt3.IsSourceFromValueList = False
        Me.txt3.IsUnique = False
        Me.txt3.Location = New System.Drawing.Point(166, 40)
        Me.txt3.MendatroryField = False
        Me.txt3.MyLinkLable1 = Me.MyLabel5
        Me.txt3.MyLinkLable2 = Nothing
        Me.txt3.Name = "txt3"
        Me.txt3.ReferenceFieldDesc = Nothing
        Me.txt3.ReferenceFieldName = Nothing
        Me.txt3.ReferenceTableName = Nothing
        Me.txt3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt3.Size = New System.Drawing.Size(65, 20)
        Me.txt3.TabIndex = 2
        Me.txt3.Text = "90"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(188, 22)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(23, 18)
        Me.MyLabel5.TabIndex = 142
        Me.MyLabel5.Text = "3rd"
        '
        'txt4th
        '
        Me.txt4th.CalculationExpression = Nothing
        Me.txt4th.FieldCode = Nothing
        Me.txt4th.FieldDesc = Nothing
        Me.txt4th.FieldMaxLength = 0
        Me.txt4th.FieldName = Nothing
        Me.txt4th.isCalculatedField = False
        Me.txt4th.IsSourceFromTable = False
        Me.txt4th.IsSourceFromValueList = False
        Me.txt4th.IsUnique = False
        Me.txt4th.Location = New System.Drawing.Point(246, 40)
        Me.txt4th.MendatroryField = False
        Me.txt4th.MyLinkLable1 = Me.MyLabel6
        Me.txt4th.MyLinkLable2 = Nothing
        Me.txt4th.Name = "txt4th"
        Me.txt4th.ReferenceFieldDesc = Nothing
        Me.txt4th.ReferenceFieldName = Nothing
        Me.txt4th.ReferenceTableName = Nothing
        Me.txt4th.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt4th.Size = New System.Drawing.Size(65, 20)
        Me.txt4th.TabIndex = 3
        Me.txt4th.Text = "120"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(267, 21)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(23, 18)
        Me.MyLabel6.TabIndex = 140
        Me.MyLabel6.Text = "4th"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(951, 457)
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
        Me.gv1.Size = New System.Drawing.Size(951, 457)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(81, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 20)
        Me.RadButton1.TabIndex = 124
        Me.RadButton1.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(8, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 20)
        Me.btnGo.TabIndex = 123
        Me.btnGo.Text = ">>>"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(892, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 20)
        Me.btnclose.TabIndex = 117
        Me.btnclose.Text = "&Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.radbtnBulkExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportSplit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(972, 543)
        Me.SplitContainer1.SplitterDistance = 505
        Me.SplitContainer1.TabIndex = 25
        '
        'radbtnBulkExp
        '
        Me.radbtnBulkExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.radbtnBulkExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BulkExcel, Me.BulkCSV})
        Me.radbtnBulkExp.Location = New System.Drawing.Point(372, 7)
        Me.radbtnBulkExp.Name = "radbtnBulkExp"
        Me.radbtnBulkExp.Size = New System.Drawing.Size(92, 20)
        Me.radbtnBulkExp.TabIndex = 332
        Me.radbtnBulkExp.Text = "Bulk Export"
        '
        'btnExportSplit
        '
        Me.btnExportSplit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportSplit.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.btnExportSplit.Location = New System.Drawing.Point(176, 7)
        Me.btnExportSplit.Name = "btnExportSplit"
        Me.btnExportSplit.Size = New System.Drawing.Size(83, 20)
        Me.btnExportSplit.TabIndex = 331
        Me.btnExportSplit.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "rmExcel"
        Me.rmExcel.AccessibleName = "rmExcel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "rmPDF"
        Me.rmPDF.AccessibleName = "rmPDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(816, 7)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(70, 20)
        Me.btnBack.TabIndex = 330
        Me.btnBack.Text = "<< Back"
        '
        'BulkExcel
        '
        Me.BulkExcel.AccessibleDescription = "BulkExcel"
        Me.BulkExcel.AccessibleName = "BulkExcel"
        Me.BulkExcel.Name = "BulkExcel"
        Me.BulkExcel.Text = "Excel"
        '
        'BulkCSV
        '
        Me.BulkCSV.AccessibleDescription = "BulkCSV"
        Me.BulkCSV.AccessibleName = "BulkCSV"
        Me.BulkCSV.Name = "BulkCSV"
        Me.BulkCSV.Text = "CSV"
        '
        'frmStockAgeingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 543)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmStockAgeingReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stock Ageing Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.chkShelfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGITLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAgeingColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInventoryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtOver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt4th, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportSplit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Protected WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Protected WithEvents txtToDate As common.Controls.MyDateTimePicker
    Protected WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txt4th As common.Controls.MyTextBox
    Friend WithEvents txt3 As common.Controls.MyTextBox
    Friend WithEvents txt2 As common.Controls.MyTextBox
    Friend WithEvents txt1 As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Protected WithEvents cboInventoryType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvLocation As common.UserControls.MyRadGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtOver As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtItemType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Protected WithEvents cboAgeingColumns As common.Controls.MyComboBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Protected WithEvents cboItemStatus As common.Controls.MyComboBox
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Protected WithEvents cmbUnit As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton7 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkGITLocation As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkShelfLife As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents radbtnBulkExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExportSplit As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkCSV As Telerik.WinControls.UI.RadMenuItem
End Class

