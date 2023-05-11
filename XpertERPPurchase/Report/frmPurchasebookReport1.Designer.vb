<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchasebookReport1
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
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblItem = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblPOInvoice = New common.Controls.MyLabel()
        Me.txtPOInvoice = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RbcategorySelect = New common.Controls.MyRadioButton()
        Me.RbCategoryAll = New common.Controls.MyRadioButton()
        Me.chkSerializeInv = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdasset = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdother = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdfinish = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdraw = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.tvCategory = New Telerik.WinControls.UI.RadTreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgAccounts = New common.MyCheckBoxGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.chkAccountSelect = New common.Controls.MyRadioButton()
        Me.chkAccountAll = New common.Controls.MyRadioButton()
        Me.grpItemType = New System.Windows.Forms.GroupBox()
        Me.chkAll = New common.Controls.MyRadioButton()
        Me.rdbtnFinishedGood = New common.Controls.MyRadioButton()
        Me.rdbtnOther = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.chkItem = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCategory = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadCheckBox2 = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadCheckBox1 = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkGroupBy = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVendor = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkVendorSelect = New common.Controls.MyRadioButton()
        Me.chkVendorAll = New common.Controls.MyRadioButton()
        Me.grpbxDepartment = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgPoInvoice = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkPoInvoiceSelect = New common.Controls.MyRadioButton()
        Me.chkPoInvoiceAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.grpbxItemWise = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkItemSelect = New common.Controls.MyRadioButton()
        Me.chkItemAll = New common.Controls.MyRadioButton()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GV1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPOInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.RbcategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RbCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdasset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdother, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdfinish, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdraw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.chkAccountSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAccountAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpItemType.SuspendLayout()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnFinishedGood, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chkCategory.SuspendLayout()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroupBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxDepartment.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkPoInvoiceSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPoInvoiceAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemWise.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(228, 11)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(90, 20)
        Me.dtpToDate.TabIndex = 25
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(78, 11)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(87, 20)
        Me.dtpFromdate1.TabIndex = 24
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(176, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 23
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(13, 12)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 22
        Me.lblFromDate.Text = "From Date"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblItem)
        Me.RadGroupBox1.Controls.Add(Me.txtItem)
        Me.RadGroupBox1.Controls.Add(Me.lblPOInvoice)
        Me.RadGroupBox1.Controls.Add(Me.txtPOInvoice)
        Me.RadGroupBox1.Controls.Add(Me.lblVendor)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.chkSerializeInv)
        Me.RadGroupBox1.Controls.Add(Me.rdasset)
        Me.RadGroupBox1.Controls.Add(Me.rdother)
        Me.RadGroupBox1.Controls.Add(Me.rdfinish)
        Me.RadGroupBox1.Controls.Add(Me.rdraw)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.grpItemType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtUOM)
        Me.RadGroupBox1.Controls.Add(Me.chkItem)
        Me.RadGroupBox1.Controls.Add(Me.chkCategory)
        Me.RadGroupBox1.Controls.Add(Me.chkGroupBy)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.grpbxDepartment)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.grpbxItemWise)
        Me.RadGroupBox1.Controls.Add(Me.lblFromDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1021, 440)
        Me.RadGroupBox1.TabIndex = 26
        '
        'lblItem
        '
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(348, 148)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 330
        Me.lblItem.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(411, 147)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.lblItem
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(325, 19)
        Me.txtItem.TabIndex = 329
        '
        'lblPOInvoice
        '
        Me.lblPOInvoice.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOInvoice.Location = New System.Drawing.Point(348, 124)
        Me.lblPOInvoice.Name = "lblPOInvoice"
        Me.lblPOInvoice.Size = New System.Drawing.Size(60, 18)
        Me.lblPOInvoice.TabIndex = 328
        Me.lblPOInvoice.Text = "PO Invoice"
        '
        'txtPOInvoice
        '
        Me.txtPOInvoice.arrDispalyMember = Nothing
        Me.txtPOInvoice.arrValueMember = Nothing
        Me.txtPOInvoice.Location = New System.Drawing.Point(411, 123)
        Me.txtPOInvoice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPOInvoice.MyLinkLable1 = Me.lblPOInvoice
        Me.txtPOInvoice.MyLinkLable2 = Nothing
        Me.txtPOInvoice.Name = "txtPOInvoice"
        Me.txtPOInvoice.Size = New System.Drawing.Size(325, 19)
        Me.txtPOInvoice.TabIndex = 327
        '
        'lblVendor
        '
        Me.lblVendor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(348, 100)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 326
        Me.lblVendor.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(411, 99)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblVendor
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(325, 19)
        Me.txtVendor.TabIndex = 325
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(348, 76)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 324
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(411, 75)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(325, 19)
        Me.txtLocation.TabIndex = 323
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.gvCategory)
        Me.RadGroupBox6.Controls.Add(Me.Panel3)
        Me.RadGroupBox6.HeaderText = "Category"
        Me.RadGroupBox6.Location = New System.Drawing.Point(13, 69)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(329, 366)
        Me.RadGroupBox6.TabIndex = 322
        Me.RadGroupBox6.Text = "Category"
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(10, 40)
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.Size = New System.Drawing.Size(309, 316)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.Text = "RadGridView1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.RbcategorySelect)
        Me.Panel3.Controls.Add(Me.RbCategoryAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(309, 20)
        Me.Panel3.TabIndex = 1
        '
        'RbcategorySelect
        '
        Me.RbcategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RbcategorySelect.Location = New System.Drawing.Point(151, 1)
        Me.RbcategorySelect.MyLinkLable1 = Nothing
        Me.RbcategorySelect.MyLinkLable2 = Nothing
        Me.RbcategorySelect.Name = "RbcategorySelect"
        Me.RbcategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.RbcategorySelect.TabIndex = 1
        Me.RbcategorySelect.Text = "Select"
        '
        'RbCategoryAll
        '
        Me.RbCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RbCategoryAll.Location = New System.Drawing.Point(112, 1)
        Me.RbCategoryAll.MyLinkLable1 = Nothing
        Me.RbCategoryAll.MyLinkLable2 = Nothing
        Me.RbCategoryAll.Name = "RbCategoryAll"
        Me.RbCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.RbCategoryAll.TabIndex = 0
        Me.RbCategoryAll.Text = "All"
        '
        'chkSerializeInv
        '
        Me.chkSerializeInv.Location = New System.Drawing.Point(459, 40)
        Me.chkSerializeInv.Name = "chkSerializeInv"
        Me.chkSerializeInv.Size = New System.Drawing.Size(111, 18)
        Me.chkSerializeInv.TabIndex = 321
        Me.chkSerializeInv.Text = "Serialize Inventory"
        '
        'rdasset
        '
        Me.rdasset.Location = New System.Drawing.Point(406, 40)
        Me.rdasset.Name = "rdasset"
        Me.rdasset.Size = New System.Drawing.Size(52, 18)
        Me.rdasset.TabIndex = 320
        Me.rdasset.Text = "Assets"
        '
        'rdother
        '
        Me.rdother.Location = New System.Drawing.Point(357, 40)
        Me.rdother.Name = "rdother"
        Me.rdother.Size = New System.Drawing.Size(49, 18)
        Me.rdother.TabIndex = 320
        Me.rdother.Text = "Other"
        '
        'rdfinish
        '
        Me.rdfinish.Location = New System.Drawing.Point(262, 40)
        Me.rdfinish.Name = "rdfinish"
        Me.rdfinish.Size = New System.Drawing.Size(92, 18)
        Me.rdfinish.TabIndex = 320
        Me.rdfinish.Text = "Finished Good"
        '
        'rdraw
        '
        Me.rdraw.Location = New System.Drawing.Point(176, 40)
        Me.rdraw.Name = "rdraw"
        Me.rdraw.Size = New System.Drawing.Size(82, 18)
        Me.rdraw.TabIndex = 319
        Me.rdraw.Text = "RawMaterial"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.tvCategory)
        Me.RadGroupBox5.Controls.Add(Me.Panel1)
        Me.RadGroupBox5.HeaderText = "Category"
        Me.RadGroupBox5.Location = New System.Drawing.Point(847, 8)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(33, 74)
        Me.RadGroupBox5.TabIndex = 318
        Me.RadGroupBox5.Text = "Category"
        Me.RadGroupBox5.Visible = False
        '
        'tvCategory
        '
        Me.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCategory.Location = New System.Drawing.Point(10, 40)
        Me.tvCategory.Name = "tvCategory"
        Me.tvCategory.Size = New System.Drawing.Size(13, 24)
        Me.tvCategory.SpacingBetweenNodes = -1
        Me.tvCategory.TabIndex = 70
        Me.tvCategory.Text = "RadTreeView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel1.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(13, 20)
        Me.Panel1.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(3, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 1
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(-48, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgAccounts)
        Me.RadGroupBox4.Controls.Add(Me.Panel7)
        Me.RadGroupBox4.HeaderText = "GL Account"
        Me.RadGroupBox4.Location = New System.Drawing.Point(911, 69)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(105, 366)
        Me.RadGroupBox4.TabIndex = 70
        Me.RadGroupBox4.Text = "GL Account"
        Me.RadGroupBox4.Visible = False
        '
        'cbgAccounts
        '
        Me.cbgAccounts.AccessibleDescription = "cbgAccounts"
        Me.cbgAccounts.AccessibleName = ""
        Me.cbgAccounts.CheckedValue = Nothing
        Me.cbgAccounts.DataSource = Nothing
        Me.cbgAccounts.DisplayMember = "Name"
        Me.cbgAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAccounts.Location = New System.Drawing.Point(10, 40)
        Me.cbgAccounts.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAccounts.MyShowHeadrText = False
        Me.cbgAccounts.Name = "cbgAccounts"
        Me.cbgAccounts.Size = New System.Drawing.Size(85, 316)
        Me.cbgAccounts.TabIndex = 1
        Me.cbgAccounts.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.chkAccountSelect)
        Me.Panel7.Controls.Add(Me.chkAccountAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(85, 20)
        Me.Panel7.TabIndex = 0
        '
        'chkAccountSelect
        '
        Me.chkAccountSelect.AccessibleDescription = "chkLoc_select"
        Me.chkAccountSelect.Location = New System.Drawing.Point(106, 1)
        Me.chkAccountSelect.MyLinkLable1 = Nothing
        Me.chkAccountSelect.MyLinkLable2 = Nothing
        Me.chkAccountSelect.Name = "chkAccountSelect"
        Me.chkAccountSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkAccountSelect.TabIndex = 1
        Me.chkAccountSelect.Text = "Select"
        '
        'chkAccountAll
        '
        Me.chkAccountAll.AccessibleDescription = "chkLocAll"
        Me.chkAccountAll.Location = New System.Drawing.Point(46, 1)
        Me.chkAccountAll.MyLinkLable1 = Nothing
        Me.chkAccountAll.MyLinkLable2 = Nothing
        Me.chkAccountAll.Name = "chkAccountAll"
        Me.chkAccountAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAccountAll.TabIndex = 0
        Me.chkAccountAll.Text = "All"
        '
        'grpItemType
        '
        Me.grpItemType.Controls.Add(Me.chkAll)
        Me.grpItemType.Controls.Add(Me.rdbtnFinishedGood)
        Me.grpItemType.Controls.Add(Me.rdbtnOther)
        Me.grpItemType.Location = New System.Drawing.Point(325, 2)
        Me.grpItemType.Name = "grpItemType"
        Me.grpItemType.Size = New System.Drawing.Size(225, 33)
        Me.grpItemType.TabIndex = 317
        Me.grpItemType.TabStop = False
        Me.grpItemType.Visible = False
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(179, 11)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 74
        Me.chkAll.Text = "All"
        '
        'rdbtnFinishedGood
        '
        Me.rdbtnFinishedGood.Location = New System.Drawing.Point(11, 11)
        Me.rdbtnFinishedGood.MyLinkLable1 = Nothing
        Me.rdbtnFinishedGood.MyLinkLable2 = Nothing
        Me.rdbtnFinishedGood.Name = "rdbtnFinishedGood"
        Me.rdbtnFinishedGood.Size = New System.Drawing.Size(92, 18)
        Me.rdbtnFinishedGood.TabIndex = 72
        Me.rdbtnFinishedGood.Text = "Finsihed Good"
        '
        'rdbtnOther
        '
        Me.rdbtnOther.Location = New System.Drawing.Point(118, 11)
        Me.rdbtnOther.MyLinkLable1 = Nothing
        Me.rdbtnOther.MyLinkLable2 = Nothing
        Me.rdbtnOther.Name = "rdbtnOther"
        Me.rdbtnOther.Size = New System.Drawing.Size(49, 18)
        Me.rdbtnOther.TabIndex = 73
        Me.rdbtnOther.Text = "Other"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(13, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 316
        Me.MyLabel1.Text = "UOM"
        '
        'txtUOM
        '
        Me.txtUOM.Location = New System.Drawing.Point(54, 40)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.MyLabel1
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.Size = New System.Drawing.Size(104, 19)
        Me.txtUOM.TabIndex = 315
        Me.txtUOM.Value = ""
        '
        'chkItem
        '
        Me.chkItem.Location = New System.Drawing.Point(886, 18)
        Me.chkItem.Name = "chkItem"
        Me.chkItem.Size = New System.Drawing.Size(98, 18)
        Me.chkItem.TabIndex = 75
        Me.chkItem.Text = "Group By 'Item'"
        Me.chkItem.Visible = False
        '
        'chkCategory
        '
        Me.chkCategory.Controls.Add(Me.RadCheckBox2)
        Me.chkCategory.Controls.Add(Me.RadCheckBox1)
        Me.chkCategory.Location = New System.Drawing.Point(886, 3)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.Size = New System.Drawing.Size(120, 18)
        Me.chkCategory.TabIndex = 74
        Me.chkCategory.Text = "Group By 'Category'"
        Me.chkCategory.Visible = False
        '
        'RadCheckBox2
        '
        Me.RadCheckBox2.Location = New System.Drawing.Point(0, 15)
        Me.RadCheckBox2.Name = "RadCheckBox2"
        Me.RadCheckBox2.Size = New System.Drawing.Size(98, 18)
        Me.RadCheckBox2.TabIndex = 75
        Me.RadCheckBox2.Text = "Group By 'Item'"
        Me.RadCheckBox2.Visible = False
        '
        'RadCheckBox1
        '
        Me.RadCheckBox1.Location = New System.Drawing.Point(0, 33)
        Me.RadCheckBox1.Name = "RadCheckBox1"
        Me.RadCheckBox1.Size = New System.Drawing.Size(127, 18)
        Me.RadCheckBox1.TabIndex = 74
        Me.RadCheckBox1.Text = "Group By 'AP Invoice'"
        Me.RadCheckBox1.Visible = False
        '
        'chkGroupBy
        '
        Me.chkGroupBy.Location = New System.Drawing.Point(886, 36)
        Me.chkGroupBy.Name = "chkGroupBy"
        Me.chkGroupBy.Size = New System.Drawing.Size(127, 18)
        Me.chkGroupBy.TabIndex = 74
        Me.chkGroupBy.Text = "Group By 'AP Invoice'"
        Me.chkGroupBy.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox2.Controls.Add(Me.Panel6)
        Me.RadGroupBox2.HeaderText = "Vendor"
        Me.RadGroupBox2.Location = New System.Drawing.Point(678, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(99, 39)
        Me.RadGroupBox2.TabIndex = 71
        Me.RadGroupBox2.Text = "Vendor"
        Me.RadGroupBox2.Visible = False
        '
        'cbgVendor
        '
        Me.cbgVendor.AccessibleName = ""
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(79, 0)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkVendorSelect)
        Me.Panel6.Controls.Add(Me.chkVendorAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(79, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(115, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(52, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'grpbxDepartment
        '
        Me.grpbxDepartment.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxDepartment.Controls.Add(Me.cbgPoInvoice)
        Me.grpbxDepartment.Controls.Add(Me.Panel4)
        Me.grpbxDepartment.HeaderText = "Po Invoice "
        Me.grpbxDepartment.Location = New System.Drawing.Point(764, 366)
        Me.grpbxDepartment.Name = "grpbxDepartment"
        Me.grpbxDepartment.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxDepartment.Size = New System.Drawing.Size(75, 39)
        Me.grpbxDepartment.TabIndex = 70
        Me.grpbxDepartment.Text = "Po Invoice "
        Me.grpbxDepartment.Visible = False
        '
        'cbgPoInvoice
        '
        Me.cbgPoInvoice.AccessibleName = ""
        Me.cbgPoInvoice.CheckedValue = Nothing
        Me.cbgPoInvoice.DataSource = Nothing
        Me.cbgPoInvoice.DisplayMember = "Name"
        Me.cbgPoInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgPoInvoice.Location = New System.Drawing.Point(10, 40)
        Me.cbgPoInvoice.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgPoInvoice.MyShowHeadrText = False
        Me.cbgPoInvoice.Name = "cbgPoInvoice"
        Me.cbgPoInvoice.Size = New System.Drawing.Size(55, 0)
        Me.cbgPoInvoice.TabIndex = 1
        Me.cbgPoInvoice.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkPoInvoiceSelect)
        Me.Panel4.Controls.Add(Me.chkPoInvoiceAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(55, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkPoInvoiceSelect
        '
        Me.chkPoInvoiceSelect.Location = New System.Drawing.Point(115, 1)
        Me.chkPoInvoiceSelect.MyLinkLable1 = Nothing
        Me.chkPoInvoiceSelect.MyLinkLable2 = Nothing
        Me.chkPoInvoiceSelect.Name = "chkPoInvoiceSelect"
        Me.chkPoInvoiceSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkPoInvoiceSelect.TabIndex = 1
        Me.chkPoInvoiceSelect.Text = "Select"
        '
        'chkPoInvoiceAll
        '
        Me.chkPoInvoiceAll.Location = New System.Drawing.Point(52, 1)
        Me.chkPoInvoiceAll.MyLinkLable1 = Nothing
        Me.chkPoInvoiceAll.MyLinkLable2 = Nothing
        Me.chkPoInvoiceAll.Name = "chkPoInvoiceAll"
        Me.chkPoInvoiceAll.Size = New System.Drawing.Size(33, 18)
        Me.chkPoInvoiceAll.TabIndex = 0
        Me.chkPoInvoiceAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel5)
        Me.RadGroupBox3.HeaderText = " Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(602, 11)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(59, 39)
        Me.RadGroupBox3.TabIndex = 69
        Me.RadGroupBox3.Text = " Location"
        Me.RadGroupBox3.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(39, 0)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkLocationSelect)
        Me.Panel5.Controls.Add(Me.chkLocationAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(39, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkLoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(106, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkLocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(46, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'grpbxItemWise
        '
        Me.grpbxItemWise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemWise.Controls.Add(Me.cbgItem)
        Me.grpbxItemWise.Controls.Add(Me.Panel2)
        Me.grpbxItemWise.HeaderText = "Item"
        Me.grpbxItemWise.Location = New System.Drawing.Point(845, 367)
        Me.grpbxItemWise.Name = "grpbxItemWise"
        Me.grpbxItemWise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemWise.Size = New System.Drawing.Size(49, 39)
        Me.grpbxItemWise.TabIndex = 27
        Me.grpbxItemWise.Text = "Item"
        Me.grpbxItemWise.Visible = False
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
        Me.cbgItem.Size = New System.Drawing.Size(29, 0)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItemSelect)
        Me.Panel2.Controls.Add(Me.chkItemAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(29, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.AccessibleName = "chkItemSelect"
        Me.chkItemSelect.Location = New System.Drawing.Point(106, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.AccessibleName = "chkItemAll"
        Me.chkItemAll.Location = New System.Drawing.Point(46, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(280, 3)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(164, 19)
        Me.pnlAdminSetting.TabIndex = 329
        Me.pnlAdminSetting.Visible = False
        '
        'chkMismatch
        '
        Me.chkMismatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMismatch.Location = New System.Drawing.Point(70, -1)
        Me.chkMismatch.Name = "chkMismatch"
        Me.chkMismatch.Size = New System.Drawing.Size(81, 18)
        Me.chkMismatch.TabIndex = 19
        Me.chkMismatch.Text = "Mismatched"
        Me.chkMismatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkReconcile
        '
        Me.chkReconcile.Location = New System.Drawing.Point(3, -1)
        Me.chkReconcile.Name = "chkReconcile"
        Me.chkReconcile.Size = New System.Drawing.Size(68, 18)
        Me.chkReconcile.TabIndex = 18
        Me.chkReconcile.Text = "Reconcile"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(81, 0)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(76, 24)
        Me.btnreset.TabIndex = 32
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(955, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 31
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(3, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 30
        Me.btnPrint.Text = ">>>"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlAdminSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(1042, 517)
        Me.SplitContainer1.SplitterDistance = 488
        Me.SplitContainer1.TabIndex = 27
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1042, 488)
        Me.RadPageView1.TabIndex = 27
        Me.RadPageView1.Text = "Filters"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1021, 440)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1021, 440)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.EnableFiltering = True
        Me.GV1.Name = "GV1"
        Me.GV1.Size = New System.Drawing.Size(1021, 440)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(163, 0)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(76, 24)
        Me.btnExport.TabIndex = 330
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "RadMenuItem1"
        Me.Export.AccessibleName = "RadMenuItem1"
        Me.Export.Image = Global.XpertERPPurchase.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "RadMenuItem2"
        Me.PDF.AccessibleName = "RadMenuItem2"
        Me.PDF.Image = Global.XpertERPPurchase.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'FrmPurchasebookReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPurchasebookReport1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Book"
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPOInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.RbcategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RbCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSerializeInv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdasset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdother, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdfinish, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdraw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.chkAccountSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAccountAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpItemType.ResumeLayout(False)
        Me.grpItemType.PerformLayout()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnFinishedGood, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chkCategory.ResumeLayout(False)
        Me.chkCategory.PerformLayout()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroupBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxDepartment.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkPoInvoiceSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPoInvoiceAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemWise.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents grpbxItemWise As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxDepartment As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgPoInvoice As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkPoInvoiceSelect As common.Controls.MyRadioButton
    Friend WithEvents chkPoInvoiceAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents rdbtnOther As common.Controls.MyRadioButton
    Friend WithEvents rdbtnFinishedGood As common.Controls.MyRadioButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents chkGroupBy As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents chkCategory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkItem As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents grpItemType As System.Windows.Forms.GroupBox
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgAccounts As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkAccountSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAccountAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents tvCategory As Telerik.WinControls.UI.RadTreeView
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents rdasset As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdother As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdfinish As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdraw As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkSerializeInv As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadCheckBox2 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadCheckBox1 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents RbcategorySelect As common.Controls.MyRadioButton
    Protected WithEvents RbCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblPOInvoice As common.Controls.MyLabel
    Friend WithEvents txtPOInvoice As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
End Class

