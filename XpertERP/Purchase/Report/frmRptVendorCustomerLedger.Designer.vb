<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptVendorCustomerLedger
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkVendorTypeAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorTypeTTM = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorTypeVSP = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorTypePTM = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadRadioButton1 = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButton2 = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButton3 = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.ddlCurrencyType = New Telerik.WinControls.UI.RadDropDownList()
        Me.ChkchildVendor = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtAccountSet = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtVendorGroup = New common.UserControls.txtMultiSelectFinder()
        Me.chkItemWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbLandScape = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbPortrait = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkVendorWithZero = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorGrupWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkPDC = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvVendor = New common.UserControls.MyRadGridView()
        Me.gvVendorGroup = New common.UserControls.MyRadGridView()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnexport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkIncludeApplyDocument = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.chkVendorTypeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorTypeTTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorTypeVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorTypePTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.RadRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkchildVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbLandScape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbPortrait, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkVendorWithZero, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chkNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorGrupWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkPDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendorGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendorGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1052, 465)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1031, 417)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.chkIncludeApplyDocument)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.TxtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.ddlCurrencyType)
        Me.RadGroupBox1.Controls.Add(Me.ChkchildVendor)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtAccountSet)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtVendorGroup)
        Me.RadGroupBox1.Controls.Add(Me.chkItemWise)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.ChkVendorWithZero)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.ChkPDC)
        Me.RadGroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1031, 417)
        Me.RadGroupBox1.TabIndex = 3
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(124, 139)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 389
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 140)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 390
        Me.MyLabel2.Text = "Location"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(13, 167)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel7.TabIndex = 403
        Me.MyLabel7.Text = "Vendor Type"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkVendorTypeAll)
        Me.GroupBox5.Controls.Add(Me.chkVendorTypeTTM)
        Me.GroupBox5.Controls.Add(Me.chkVendorTypeVSP)
        Me.GroupBox5.Controls.Add(Me.chkVendorTypePTM)
        Me.GroupBox5.Location = New System.Drawing.Point(123, 156)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(193, 33)
        Me.GroupBox5.TabIndex = 402
        Me.GroupBox5.TabStop = False
        '
        'chkVendorTypeAll
        '
        Me.chkVendorTypeAll.Location = New System.Drawing.Point(145, 8)
        Me.chkVendorTypeAll.Name = "chkVendorTypeAll"
        Me.chkVendorTypeAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorTypeAll.TabIndex = 6
        Me.chkVendorTypeAll.TabStop = False
        Me.chkVendorTypeAll.Text = "All"
        '
        'chkVendorTypeTTM
        '
        Me.chkVendorTypeTTM.Location = New System.Drawing.Point(98, 8)
        Me.chkVendorTypeTTM.Name = "chkVendorTypeTTM"
        Me.chkVendorTypeTTM.Size = New System.Drawing.Size(42, 18)
        Me.chkVendorTypeTTM.TabIndex = 5
        Me.chkVendorTypeTTM.TabStop = False
        Me.chkVendorTypeTTM.Text = "TTM"
        '
        'chkVendorTypeVSP
        '
        Me.chkVendorTypeVSP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVendorTypeVSP.Location = New System.Drawing.Point(5, 8)
        Me.chkVendorTypeVSP.Name = "chkVendorTypeVSP"
        Me.chkVendorTypeVSP.Size = New System.Drawing.Size(40, 18)
        Me.chkVendorTypeVSP.TabIndex = 3
        Me.chkVendorTypeVSP.Text = "VSP"
        Me.chkVendorTypeVSP.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkVendorTypePTM
        '
        Me.chkVendorTypePTM.Location = New System.Drawing.Point(50, 8)
        Me.chkVendorTypePTM.Name = "chkVendorTypePTM"
        Me.chkVendorTypePTM.Size = New System.Drawing.Size(43, 18)
        Me.chkVendorTypePTM.TabIndex = 4
        Me.chkVendorTypePTM.TabStop = False
        Me.chkVendorTypePTM.Text = "PTM"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(13, 72)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel6.TabIndex = 401
        Me.MyLabel6.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(123, 71)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.MyLabel6
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerGroup.TabIndex = 400
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rdbSummary)
        Me.GroupBox4.Controls.Add(Me.rdbDetail)
        Me.GroupBox4.Location = New System.Drawing.Point(260, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(151, 33)
        Me.GroupBox4.TabIndex = 399
        Me.GroupBox4.TabStop = False
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(5, 11)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 3
        Me.rdbSummary.Text = "Summary"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(86, 11)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 4
        Me.rdbDetail.Text = "Detail"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadRadioButton1)
        Me.GroupBox3.Controls.Add(Me.RadRadioButton2)
        Me.GroupBox3.Controls.Add(Me.RadRadioButton3)
        Me.GroupBox3.Location = New System.Drawing.Point(326, 202)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(379, 33)
        Me.GroupBox3.TabIndex = 398
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Summary"
        Me.GroupBox3.Visible = False
        '
        'RadRadioButton1
        '
        Me.RadRadioButton1.Location = New System.Drawing.Point(305, 12)
        Me.RadRadioButton1.Name = "RadRadioButton1"
        Me.RadRadioButton1.Size = New System.Drawing.Size(48, 18)
        Me.RadRadioButton1.TabIndex = 5
        Me.RadRadioButton1.Text = "None"
        '
        'RadRadioButton2
        '
        Me.RadRadioButton2.Location = New System.Drawing.Point(69, 11)
        Me.RadRadioButton2.Name = "RadRadioButton2"
        Me.RadRadioButton2.Size = New System.Drawing.Size(118, 18)
        Me.RadRadioButton2.TabIndex = 3
        Me.RadRadioButton2.Text = "Vendor Group Wise"
        '
        'RadRadioButton3
        '
        Me.RadRadioButton3.Location = New System.Drawing.Point(201, 11)
        Me.RadRadioButton3.Name = "RadRadioButton3"
        Me.RadRadioButton3.Size = New System.Drawing.Size(84, 18)
        Me.RadRadioButton3.TabIndex = 4
        Me.RadRadioButton3.Text = "Vendor Wise"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 95)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 397
        Me.MyLabel3.Text = "Customer"
        '
        'TxtCustomer
        '
        Me.TxtCustomer.arrDispalyMember = Nothing
        Me.TxtCustomer.arrValueMember = Nothing
        Me.TxtCustomer.Location = New System.Drawing.Point(124, 94)
        Me.TxtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.TxtCustomer.MyLinkLable2 = Nothing
        Me.TxtCustomer.MyNullText = "All"
        Me.TxtCustomer.Name = "TxtCustomer"
        Me.TxtCustomer.Size = New System.Drawing.Size(344, 19)
        Me.TxtCustomer.TabIndex = 396
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(449, 43)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel5.TabIndex = 394
        Me.MyLabel5.Text = "Currency "
        Me.MyLabel5.Visible = False
        '
        'ddlCurrencyType
        '
        Me.ddlCurrencyType.Location = New System.Drawing.Point(503, 43)
        Me.ddlCurrencyType.Name = "ddlCurrencyType"
        Me.ddlCurrencyType.Size = New System.Drawing.Size(136, 20)
        Me.ddlCurrencyType.TabIndex = 395
        Me.ddlCurrencyType.Visible = False
        '
        'ChkchildVendor
        '
        Me.ChkchildVendor.Location = New System.Drawing.Point(475, 114)
        Me.ChkchildVendor.Name = "ChkchildVendor"
        '
        '
        '
        Me.ChkchildVendor.RootElement.StretchHorizontally = True
        Me.ChkchildVendor.RootElement.StretchVertically = True
        Me.ChkchildVendor.Size = New System.Drawing.Size(185, 18)
        Me.ChkchildVendor.TabIndex = 393
        Me.ChkchildVendor.Text = "Show Child Vendor Data also"
        Me.ChkchildVendor.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(424, 241)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel4.TabIndex = 392
        Me.MyLabel4.Text = "Account Set"
        Me.MyLabel4.Visible = False
        '
        'txtAccountSet
        '
        Me.txtAccountSet.arrDispalyMember = Nothing
        Me.txtAccountSet.arrValueMember = Nothing
        Me.txtAccountSet.Location = New System.Drawing.Point(535, 240)
        Me.txtAccountSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSet.MyLinkLable1 = Me.MyLabel4
        Me.txtAccountSet.MyLinkLable2 = Nothing
        Me.txtAccountSet.MyNullText = "All"
        Me.txtAccountSet.Name = "txtAccountSet"
        Me.txtAccountSet.Size = New System.Drawing.Size(344, 19)
        Me.txtAccountSet.TabIndex = 391
        Me.txtAccountSet.Visible = False
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(13, 117)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(43, 18)
        Me.lblCustomerGroup.TabIndex = 386
        Me.lblCustomerGroup.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(124, 116)
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
        Me.lblLocation.Location = New System.Drawing.Point(438, 217)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(77, 18)
        Me.lblLocation.TabIndex = 384
        Me.lblLocation.Text = "Vendor Group"
        Me.lblLocation.Visible = False
        '
        'txtVendorGroup
        '
        Me.txtVendorGroup.arrDispalyMember = Nothing
        Me.txtVendorGroup.arrValueMember = Nothing
        Me.txtVendorGroup.Location = New System.Drawing.Point(549, 216)
        Me.txtVendorGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorGroup.MyLinkLable1 = Me.lblLocation
        Me.txtVendorGroup.MyLinkLable2 = Nothing
        Me.txtVendorGroup.MyNullText = "All"
        Me.txtVendorGroup.Name = "txtVendorGroup"
        Me.txtVendorGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtVendorGroup.TabIndex = 383
        Me.txtVendorGroup.Visible = False
        '
        'chkItemWise
        '
        Me.chkItemWise.Location = New System.Drawing.Point(475, 138)
        Me.chkItemWise.Name = "chkItemWise"
        '
        '
        '
        Me.chkItemWise.RootElement.StretchHorizontally = True
        Me.chkItemWise.RootElement.StretchVertically = True
        Me.chkItemWise.Size = New System.Drawing.Size(77, 18)
        Me.chkItemWise.TabIndex = 115
        Me.chkItemWise.Text = "Item Wise"
        Me.chkItemWise.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbLandScape)
        Me.GroupBox1.Controls.Add(Me.rbPortrait)
        Me.GroupBox1.Location = New System.Drawing.Point(260, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(187, 36)
        Me.GroupBox1.TabIndex = 126
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Page Setup"
        '
        'rbLandScape
        '
        Me.rbLandScape.Location = New System.Drawing.Point(96, 13)
        Me.rbLandScape.Name = "rbLandScape"
        Me.rbLandScape.Size = New System.Drawing.Size(77, 18)
        Me.rbLandScape.TabIndex = 4
        Me.rbLandScape.Text = "Land Scape"
        '
        'rbPortrait
        '
        Me.rbPortrait.Location = New System.Drawing.Point(38, 13)
        Me.rbPortrait.Name = "rbPortrait"
        Me.rbPortrait.Size = New System.Drawing.Size(57, 18)
        Me.rbPortrait.TabIndex = 3
        Me.rbPortrait.Text = "Portrait"
        '
        'ChkVendorWithZero
        '
        Me.ChkVendorWithZero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkVendorWithZero.Location = New System.Drawing.Point(475, 70)
        Me.ChkVendorWithZero.Name = "ChkVendorWithZero"
        '
        '
        '
        Me.ChkVendorWithZero.RootElement.StretchHorizontally = True
        Me.ChkVendorWithZero.RootElement.StretchVertically = True
        Me.ChkVendorWithZero.Size = New System.Drawing.Size(151, 18)
        Me.ChkVendorWithZero.TabIndex = 18
        Me.ChkVendorWithZero.Text = "Vendor with Zero Amount"
        Me.ChkVendorWithZero.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.ChkVendorWithZero.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkNone)
        Me.GroupBox2.Controls.Add(Me.chkVendorGrupWise)
        Me.GroupBox2.Controls.Add(Me.chkVendorWise)
        Me.GroupBox2.Location = New System.Drawing.Point(558, 138)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(379, 33)
        Me.GroupBox2.TabIndex = 127
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Summary"
        Me.GroupBox2.Visible = False
        '
        'chkNone
        '
        Me.chkNone.Location = New System.Drawing.Point(305, 12)
        Me.chkNone.Name = "chkNone"
        Me.chkNone.Size = New System.Drawing.Size(48, 18)
        Me.chkNone.TabIndex = 5
        Me.chkNone.Text = "None"
        '
        'chkVendorGrupWise
        '
        Me.chkVendorGrupWise.Location = New System.Drawing.Point(69, 11)
        Me.chkVendorGrupWise.Name = "chkVendorGrupWise"
        Me.chkVendorGrupWise.Size = New System.Drawing.Size(118, 18)
        Me.chkVendorGrupWise.TabIndex = 3
        Me.chkVendorGrupWise.Text = "Vendor Group Wise"
        '
        'chkVendorWise
        '
        Me.chkVendorWise.Location = New System.Drawing.Point(201, 11)
        Me.chkVendorWise.Name = "chkVendorWise"
        Me.chkVendorWise.Size = New System.Drawing.Size(84, 18)
        Me.chkVendorWise.TabIndex = 4
        Me.chkVendorWise.Text = "Vendor Wise"
        '
        'ChkPDC
        '
        Me.ChkPDC.Location = New System.Drawing.Point(475, 92)
        Me.ChkPDC.Name = "ChkPDC"
        '
        '
        '
        Me.ChkPDC.RootElement.StretchHorizontally = True
        Me.ChkPDC.RootElement.StretchVertically = True
        Me.ChkPDC.Size = New System.Drawing.Size(143, 18)
        Me.ChkPDC.TabIndex = 124
        Me.ChkPDC.Text = "PDC Cheque"
        Me.ChkPDC.Visible = False
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(123, 49)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(130, 19)
        Me.txtCurrencyCode.TabIndex = 121
        Me.txtCurrencyCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel1.TabIndex = 122
        Me.MyLabel1.Text = "Currency"
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
        Me.RadLabel2.Location = New System.Drawing.Point(13, 26)
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
        Me.RadPageViewPage2.Controls.Add(Me.gvVendor)
        Me.RadPageViewPage2.Controls.Add(Me.gvVendorGroup)
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1031, 437)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvVendor
        '
        Me.gvVendor.Location = New System.Drawing.Point(0, 295)
        '
        '
        '
        Me.gvVendor.MasterTemplate.AllowAddNewRow = False
        Me.gvVendor.MasterTemplate.AllowEditRow = False
        Me.gvVendor.MasterTemplate.EnableFiltering = True
        Me.gvVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVendor.Name = "gvVendor"
        Me.gvVendor.ShowGroupPanel = False
        Me.gvVendor.ShowHeaderCellButtons = True
        Me.gvVendor.Size = New System.Drawing.Size(883, 141)
        Me.gvVendor.TabIndex = 3
        Me.gvVendor.Text = "RadGridView1"
        '
        'gvVendorGroup
        '
        Me.gvVendorGroup.Location = New System.Drawing.Point(0, 148)
        '
        '
        '
        Me.gvVendorGroup.MasterTemplate.AllowAddNewRow = False
        Me.gvVendorGroup.MasterTemplate.AllowEditRow = False
        Me.gvVendorGroup.MasterTemplate.EnableFiltering = True
        Me.gvVendorGroup.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVendorGroup.Name = "gvVendorGroup"
        Me.gvVendorGroup.ShowGroupPanel = False
        Me.gvVendorGroup.ShowHeaderCellButtons = True
        Me.gvVendorGroup.Size = New System.Drawing.Size(883, 141)
        Me.gvVendorGroup.TabIndex = 2
        Me.gvVendorGroup.Text = "RadGridView1"
        '
        'gv
        '
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
        Me.gv.Size = New System.Drawing.Size(883, 141)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(3, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 112
        Me.btnRefresh.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(980, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(77, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(231, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlAdminSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1052, 494)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 4
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnexport.Location = New System.Drawing.Point(151, 3)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(75, 18)
        Me.btnexport.TabIndex = 332
        Me.btnexport.Text = "Export"
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
        'btnBack
        '
        Me.btnBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(905, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(69, 18)
        Me.btnBack.TabIndex = 330
        Me.btnBack.Text = "<<Back"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(535, 3)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(170, 19)
        Me.pnlAdminSetting.TabIndex = 329
        Me.pnlAdminSetting.Visible = False
        '
        'chkMismatch
        '
        Me.chkMismatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMismatch.Location = New System.Drawing.Point(81, -1)
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
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1052, 20)
        Me.RadMenu1.TabIndex = 5
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
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
        'chkIncludeApplyDocument
        '
        Me.chkIncludeApplyDocument.Location = New System.Drawing.Point(123, 195)
        Me.chkIncludeApplyDocument.Name = "chkIncludeApplyDocument"
        '
        '
        '
        Me.chkIncludeApplyDocument.RootElement.StretchHorizontally = True
        Me.chkIncludeApplyDocument.RootElement.StretchVertically = True
        Me.chkIncludeApplyDocument.Size = New System.Drawing.Size(181, 18)
        Me.chkIncludeApplyDocument.TabIndex = 404
        Me.chkIncludeApplyDocument.Text = "Include Apply Document"
        '
        'frmRptVendorCustomerLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 514)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRptVendorCustomerLedger"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Ledger Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.chkVendorTypeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorTypeTTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorTypeVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorTypePTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.RadRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkchildVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbLandScape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbPortrait, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkVendorWithZero, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.chkNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorGrupWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkPDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendorGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendorGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkVendorWithZero As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkItemWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents chkNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVendorWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVendorGrupWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gvVendor As common.UserControls.MyRadGridView
    Friend WithEvents gvVendorGroup As common.UserControls.MyRadGridView
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rbLandScape As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbPortrait As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ChkPDC As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtVendorGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAccountSet As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlCurrencyType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents ChkchildVendor As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadRadioButton1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButton2 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButton3 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents chkVendorTypeAll As RadRadioButton
    Friend WithEvents chkVendorTypeTTM As RadRadioButton
    Friend WithEvents chkVendorTypeVSP As RadRadioButton
    Friend WithEvents chkVendorTypePTM As RadRadioButton
    Friend WithEvents chkIncludeApplyDocument As RadCheckBox
End Class

