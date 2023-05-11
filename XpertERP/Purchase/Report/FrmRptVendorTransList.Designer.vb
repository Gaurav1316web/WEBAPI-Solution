<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRptVendorTransList
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.ddlDocType = New Telerik.WinControls.UI.RadDropDownList()
        Me.ChkSummary = New common.Controls.MyCheckBox()
        Me.PnlSumm = New System.Windows.Forms.Panel()
        Me.rbtnVenWise = New System.Windows.Forms.RadioButton()
        Me.rbtnDocWise = New System.Windows.Forms.RadioButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtAccountSet = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.txtChildVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtVendorGroup = New common.UserControls.txtMultiSelectFinder()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSplit = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlSumm.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(944, 441)
        Me.SplitContainer1.SplitterDistance = 392
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
        Me.RadPageView1.Size = New System.Drawing.Size(944, 372)
        Me.RadPageView1.TabIndex = 4
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(923, 324)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.ddlDocType)
        Me.RadGroupBox1.Controls.Add(Me.ChkSummary)
        Me.RadGroupBox1.Controls.Add(Me.PnlSumm)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtAccountSet)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtChildVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtVendorGroup)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(923, 324)
        Me.RadGroupBox1.TabIndex = 3
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 138)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel5.TabIndex = 402
        Me.MyLabel5.Text = "Doc Type"
        '
        'ddlDocType
        '
        Me.ddlDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem5.Text = "All"
        RadListDataItem6.Text = "Advance"
        RadListDataItem7.Text = "On Account"
        RadListDataItem8.Text = "Debit Note"
        Me.ddlDocType.Items.Add(RadListDataItem5)
        Me.ddlDocType.Items.Add(RadListDataItem6)
        Me.ddlDocType.Items.Add(RadListDataItem7)
        Me.ddlDocType.Items.Add(RadListDataItem8)
        Me.ddlDocType.Location = New System.Drawing.Point(124, 137)
        Me.ddlDocType.Name = "ddlDocType"
        Me.ddlDocType.Size = New System.Drawing.Size(344, 20)
        Me.ddlDocType.TabIndex = 403
        '
        'ChkSummary
        '
        Me.ChkSummary.Location = New System.Drawing.Point(270, 2)
        Me.ChkSummary.MyLinkLable1 = Nothing
        Me.ChkSummary.MyLinkLable2 = Nothing
        Me.ChkSummary.Name = "ChkSummary"
        Me.ChkSummary.Size = New System.Drawing.Size(67, 18)
        Me.ChkSummary.TabIndex = 397
        Me.ChkSummary.Tag1 = Nothing
        Me.ChkSummary.Text = "Summary"
        '
        'PnlSumm
        '
        Me.PnlSumm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSumm.Controls.Add(Me.rbtnVenWise)
        Me.PnlSumm.Controls.Add(Me.rbtnDocWise)
        Me.PnlSumm.Location = New System.Drawing.Point(261, 10)
        Me.PnlSumm.Name = "PnlSumm"
        Me.PnlSumm.Size = New System.Drawing.Size(207, 36)
        Me.PnlSumm.TabIndex = 396
        '
        'rbtnVenWise
        '
        Me.rbtnVenWise.AutoSize = True
        Me.rbtnVenWise.Location = New System.Drawing.Point(98, 10)
        Me.rbtnVenWise.Name = "rbtnVenWise"
        Me.rbtnVenWise.Size = New System.Drawing.Size(90, 17)
        Me.rbtnVenWise.TabIndex = 28
        Me.rbtnVenWise.Text = "Vendor Wise"
        Me.rbtnVenWise.UseVisualStyleBackColor = True
        '
        'rbtnDocWise
        '
        Me.rbtnDocWise.AutoSize = True
        Me.rbtnDocWise.Checked = True
        Me.rbtnDocWise.Location = New System.Drawing.Point(10, 10)
        Me.rbtnDocWise.Name = "rbtnDocWise"
        Me.rbtnDocWise.Size = New System.Drawing.Size(73, 17)
        Me.rbtnDocWise.TabIndex = 30
        Me.rbtnDocWise.TabStop = True
        Me.rbtnDocWise.Text = "Doc Wise"
        Me.rbtnDocWise.UseVisualStyleBackColor = True
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 93)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel4.TabIndex = 392
        Me.MyLabel4.Text = "Account Set"
        '
        'txtAccountSet
        '
        Me.txtAccountSet.arrDispalyMember = Nothing
        Me.txtAccountSet.arrValueMember = Nothing
        Me.txtAccountSet.Location = New System.Drawing.Point(124, 94)
        Me.txtAccountSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSet.MyLinkLable1 = Me.MyLabel4
        Me.txtAccountSet.MyLinkLable2 = Nothing
        Me.txtAccountSet.MyNullText = "All"
        Me.txtAccountSet.Name = "txtAccountSet"
        Me.txtAccountSet.Size = New System.Drawing.Size(344, 19)
        Me.txtAccountSet.TabIndex = 391
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 115)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 390
        Me.MyLabel2.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(124, 115)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 389
        '
        'txtChildVendor
        '
        Me.txtChildVendor.arrDispalyMember = Nothing
        Me.txtChildVendor.arrValueMember = Nothing
        Me.txtChildVendor.Location = New System.Drawing.Point(922, 35)
        Me.txtChildVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChildVendor.MyLinkLable1 = Nothing
        Me.txtChildVendor.MyLinkLable2 = Nothing
        Me.txtChildVendor.MyNullText = "All"
        Me.txtChildVendor.Name = "txtChildVendor"
        Me.txtChildVendor.Size = New System.Drawing.Size(100, 19)
        Me.txtChildVendor.TabIndex = 387
        Me.txtChildVendor.Visible = False
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(13, 71)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(43, 18)
        Me.lblCustomerGroup.TabIndex = 386
        Me.lblCustomerGroup.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(124, 72)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(344, 19)
        Me.txtVendor.TabIndex = 385
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 49)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(77, 18)
        Me.lblLocation.TabIndex = 384
        Me.lblLocation.Text = "Vendor Group"
        '
        'txtVendorGroup
        '
        Me.txtVendorGroup.arrDispalyMember = Nothing
        Me.txtVendorGroup.arrValueMember = Nothing
        Me.txtVendorGroup.Location = New System.Drawing.Point(124, 51)
        Me.txtVendorGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorGroup.MyLinkLable1 = Me.lblLocation
        Me.txtVendorGroup.MyLinkLable2 = Nothing
        Me.txtVendorGroup.MyNullText = "All"
        Me.txtVendorGroup.Name = "txtVendorGroup"
        Me.txtVendorGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtVendorGroup.TabIndex = 383
        '
        'dtptodate
        '
        Me.dtptodate.CalculationExpression = Nothing
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.FieldCode = Nothing
        Me.dtptodate.FieldDesc = Nothing
        Me.dtptodate.FieldMaxLength = 0
        Me.dtptodate.FieldName = Nothing
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.isCalculatedField = False
        Me.dtptodate.IsSourceFromTable = False
        Me.dtptodate.IsSourceFromValueList = False
        Me.dtptodate.IsUnique = False
        Me.dtptodate.Location = New System.Drawing.Point(124, 27)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.ReferenceFieldDesc = Nothing
        Me.dtptodate.ReferenceFieldName = Nothing
        Me.dtptodate.ReferenceTableName = Nothing
        Me.dtptodate.Size = New System.Drawing.Size(129, 20)
        Me.dtptodate.TabIndex = 2
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "05/08/2011"
        Me.dtptodate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CalculationExpression = Nothing
        Me.dtpFromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate.FieldCode = Nothing
        Me.dtpFromdate.FieldDesc = Nothing
        Me.dtpFromdate.FieldMaxLength = 0
        Me.dtpFromdate.FieldName = Nothing
        Me.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate.isCalculatedField = False
        Me.dtpFromdate.IsSourceFromTable = False
        Me.dtpFromdate.IsSourceFromValueList = False
        Me.dtpFromdate.IsUnique = False
        Me.dtpFromdate.Location = New System.Drawing.Point(124, 4)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Nothing
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.ReferenceFieldDesc = Nothing
        Me.dtpFromdate.ReferenceFieldName = Nothing
        Me.dtpFromdate.ReferenceTableName = Nothing
        Me.dtpFromdate.Size = New System.Drawing.Size(129, 20)
        Me.dtpFromdate.TabIndex = 1
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Text = "05/08/2011"
        Me.dtpFromdate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(13, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(923, 324)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(923, 324)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(944, 20)
        Me.rdmenufile.TabIndex = 71
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnSplit
        '
        Me.btnSplit.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF, Me.ExcelGrid, Me.PDFGrid})
        Me.btnSplit.Location = New System.Drawing.Point(239, 16)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(80, 18)
        Me.btnSplit.TabIndex = 339
        Me.btnSplit.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(862, 13)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(67, 18)
        Me.BtnClose.TabIndex = 337
        Me.BtnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(163, 16)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(67, 18)
        Me.btnPrint.TabIndex = 332
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(85, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(67, 18)
        Me.btnReset.TabIndex = 333
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(7, 16)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(67, 18)
        Me.btnRefresh.TabIndex = 335
        Me.btnRefresh.Text = ">>>"
        '
        'ExcelGrid
        '
        Me.ExcelGrid.AccessibleDescription = "ExcelGrid"
        Me.ExcelGrid.AccessibleName = "ExcelGrid"
        Me.ExcelGrid.Name = "ExcelGrid"
        Me.ExcelGrid.Text = "Excel(Grid)"
        '
        'PDFGrid
        '
        Me.PDFGrid.AccessibleDescription = "PDFGrid"
        Me.PDFGrid.AccessibleName = "PDFGrid"
        Me.PDFGrid.Name = "PDFGrid"
        Me.PDFGrid.Text = "PDF(Grid)"
        '
        'FrmRptVendorTransList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 441)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRptVendorTransList"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Transaction List"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlSumm.ResumeLayout(False)
        Me.PnlSumm.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAccountSet As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtChildVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtVendorGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents ChkSummary As common.Controls.MyCheckBox
    Friend WithEvents PnlSumm As System.Windows.Forms.Panel
    Friend WithEvents rbtnVenWise As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDocWise As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlDocType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSplit As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
End Class
