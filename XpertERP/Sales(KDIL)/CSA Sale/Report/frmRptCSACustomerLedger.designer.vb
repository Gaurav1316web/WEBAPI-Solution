<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptCSACustomerLedger
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
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCustomerType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.lblParentCustomer = New common.Controls.MyLabel()
        Me.txtParentCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.txtCompany = New common.UserControls.txtMultiSelectFinder()
        Me.ChkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkISParentCust = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkItemWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCumulativeClosing = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkNone = New System.Windows.Forms.RadioButton()
        Me.chkCustGroupWise = New System.Windows.Forms.RadioButton()
        Me.chkCustWise = New System.Windows.Forms.RadioButton()
        Me.pnlActiveInActiveCustomer = New System.Windows.Forms.Panel()
        Me.chkActive = New System.Windows.Forms.RadioButton()
        Me.chkAll = New System.Windows.Forms.RadioButton()
        Me.chkInactive = New System.Windows.Forms.RadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetails = New common.UserControls.MyRadGridView()
        Me.gvCustomer = New common.UserControls.MyRadGridView()
        Me.gvCustomerGroup = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox()
        Me.refreshbtn = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParentCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCumulativeClosing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.pnlActiveInActiveCustomer.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.refreshbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(130, 32)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(130, 6)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(842, 595)
        Me.RadPageView1.TabIndex = 13
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(821, 547)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerCategory)
        Me.RadGroupBox1.Controls.Add(Me.lblParentCustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtParentCustomer)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblCompany)
        Me.RadGroupBox1.Controls.Add(Me.txtCompany)
        Me.RadGroupBox1.Controls.Add(Me.ChkSecurity)
        Me.RadGroupBox1.Controls.Add(Me.ChkISParentCust)
        Me.RadGroupBox1.Controls.Add(Me.chkItemWise)
        Me.RadGroupBox1.Controls.Add(Me.chkCumulativeClosing)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.pnlActiveInActiveCustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(821, 547)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 146)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 382
        Me.MyLabel2.Text = "Customer Type"
        '
        'txtCustomerType
        '
        Me.txtCustomerType.arrDispalyMember = Nothing
        Me.txtCustomerType.arrValueMember = Nothing
        Me.txtCustomerType.Location = New System.Drawing.Point(130, 145)
        Me.txtCustomerType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerType.MyLinkLable1 = Me.MyLabel2
        Me.txtCustomerType.MyLinkLable2 = Nothing
        Me.txtCustomerType.MyNullText = "All"
        Me.txtCustomerType.Name = "txtCustomerType"
        Me.txtCustomerType.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerType.TabIndex = 381
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 125)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel1.TabIndex = 380
        Me.MyLabel1.Text = "Customer Category"
        '
        'txtCustomerCategory
        '
        Me.txtCustomerCategory.arrDispalyMember = Nothing
        Me.txtCustomerCategory.arrValueMember = Nothing
        Me.txtCustomerCategory.Location = New System.Drawing.Point(130, 124)
        Me.txtCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCategory.MyLinkLable1 = Me.MyLabel1
        Me.txtCustomerCategory.MyLinkLable2 = Nothing
        Me.txtCustomerCategory.MyNullText = "All"
        Me.txtCustomerCategory.Name = "txtCustomerCategory"
        Me.txtCustomerCategory.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerCategory.TabIndex = 379
        '
        'lblParentCustomer
        '
        Me.lblParentCustomer.FieldName = Nothing
        Me.lblParentCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParentCustomer.Location = New System.Drawing.Point(9, 188)
        Me.lblParentCustomer.Name = "lblParentCustomer"
        Me.lblParentCustomer.Size = New System.Drawing.Size(90, 18)
        Me.lblParentCustomer.TabIndex = 378
        Me.lblParentCustomer.Text = "Parent Customer"
        '
        'txtParentCustomer
        '
        Me.txtParentCustomer.arrDispalyMember = Nothing
        Me.txtParentCustomer.arrValueMember = Nothing
        Me.txtParentCustomer.Location = New System.Drawing.Point(130, 187)
        Me.txtParentCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentCustomer.MyLinkLable1 = Me.lblParentCustomer
        Me.txtParentCustomer.MyLinkLable2 = Nothing
        Me.txtParentCustomer.MyNullText = "All"
        Me.txtParentCustomer.Name = "txtParentCustomer"
        Me.txtParentCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtParentCustomer.TabIndex = 377
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(9, 167)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 376
        Me.lblCustomer.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(130, 166)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomer.TabIndex = 375
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(9, 104)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 374
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(130, 103)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerGroup.TabIndex = 373
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 83)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 372
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(130, 82)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 371
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(9, 62)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(54, 18)
        Me.lblCompany.TabIndex = 370
        Me.lblCompany.Text = "Company"
        '
        'txtCompany
        '
        Me.txtCompany.arrDispalyMember = Nothing
        Me.txtCompany.arrValueMember = Nothing
        Me.txtCompany.Location = New System.Drawing.Point(130, 61)
        Me.txtCompany.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.MyLinkLable1 = Me.lblCompany
        Me.txtCompany.MyLinkLable2 = Nothing
        Me.txtCompany.MyNullText = "All"
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(344, 19)
        Me.txtCompany.TabIndex = 369
        '
        'ChkSecurity
        '
        Me.ChkSecurity.Location = New System.Drawing.Point(490, 61)
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(90, 18)
        Me.ChkSecurity.TabIndex = 57
        Me.ChkSecurity.Text = "Show Security"
        '
        'ChkISParentCust
        '
        Me.ChkISParentCust.Location = New System.Drawing.Point(490, 81)
        Me.ChkISParentCust.Name = "ChkISParentCust"
        Me.ChkISParentCust.Size = New System.Drawing.Size(115, 18)
        Me.ChkISParentCust.TabIndex = 53
        Me.ChkISParentCust.Text = "Is Parent Customer"
        '
        'chkItemWise
        '
        Me.chkItemWise.Location = New System.Drawing.Point(490, 102)
        Me.chkItemWise.Name = "chkItemWise"
        Me.chkItemWise.Size = New System.Drawing.Size(70, 18)
        Me.chkItemWise.TabIndex = 30
        Me.chkItemWise.Text = "Item Wise"
        Me.chkItemWise.Visible = False
        '
        'chkCumulativeClosing
        '
        Me.chkCumulativeClosing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCumulativeClosing.Location = New System.Drawing.Point(490, 123)
        Me.chkCumulativeClosing.Name = "chkCumulativeClosing"
        Me.chkCumulativeClosing.Size = New System.Drawing.Size(147, 18)
        Me.chkCumulativeClosing.TabIndex = 29
        Me.chkCumulativeClosing.Text = "Show Cumulative Closing"
        Me.chkCumulativeClosing.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkCumulativeClosing.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkNone)
        Me.GroupBox1.Controls.Add(Me.chkCustGroupWise)
        Me.GroupBox1.Controls.Add(Me.chkCustWise)
        Me.GroupBox1.Location = New System.Drawing.Point(242, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(426, 30)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Summary"
        '
        'chkNone
        '
        Me.chkNone.AutoSize = True
        Me.chkNone.Location = New System.Drawing.Point(339, 9)
        Me.chkNone.Name = "chkNone"
        Me.chkNone.Size = New System.Drawing.Size(53, 17)
        Me.chkNone.TabIndex = 29
        Me.chkNone.Text = "None"
        Me.chkNone.UseVisualStyleBackColor = True
        '
        'chkCustGroupWise
        '
        Me.chkCustGroupWise.AutoSize = True
        Me.chkCustGroupWise.Location = New System.Drawing.Point(68, 9)
        Me.chkCustGroupWise.Name = "chkCustGroupWise"
        Me.chkCustGroupWise.Size = New System.Drawing.Size(138, 17)
        Me.chkCustGroupWise.TabIndex = 27
        Me.chkCustGroupWise.Text = "Customer Group Wise"
        Me.chkCustGroupWise.UseVisualStyleBackColor = True
        '
        'chkCustWise
        '
        Me.chkCustWise.AutoSize = True
        Me.chkCustWise.Location = New System.Drawing.Point(217, 9)
        Me.chkCustWise.Name = "chkCustWise"
        Me.chkCustWise.Size = New System.Drawing.Size(102, 17)
        Me.chkCustWise.TabIndex = 28
        Me.chkCustWise.Text = "Customer Wise"
        Me.chkCustWise.UseVisualStyleBackColor = True
        '
        'pnlActiveInActiveCustomer
        '
        Me.pnlActiveInActiveCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkActive)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkAll)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkInactive)
        Me.pnlActiveInActiveCustomer.Location = New System.Drawing.Point(242, 34)
        Me.pnlActiveInActiveCustomer.Name = "pnlActiveInActiveCustomer"
        Me.pnlActiveInActiveCustomer.Size = New System.Drawing.Size(426, 22)
        Me.pnlActiveInActiveCustomer.TabIndex = 27
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(27, 1)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(107, 17)
        Me.chkActive.TabIndex = 27
        Me.chkActive.Text = "Active Customer"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(294, 1)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(95, 17)
        Me.chkAll.TabIndex = 26
        Me.chkAll.Text = "All Customers"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Location = New System.Drawing.Point(154, 1)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(116, 17)
        Me.chkInactive.TabIndex = 25
        Me.chkInactive.Text = "Inactive Customer"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(10, 32)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(10, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomer)
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomerGroup)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(821, 547)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvDetails
        '
        Me.gvDetails.Location = New System.Drawing.Point(3, 296)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.AllowEditRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.ShowHeaderCellButtons = True
        Me.gvDetails.Size = New System.Drawing.Size(845, 153)
        Me.gvDetails.TabIndex = 0
        Me.gvDetails.Text = "RadGridView1"
        '
        'gvCustomer
        '
        Me.gvCustomer.Location = New System.Drawing.Point(3, 141)
        '
        '
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.AllowEditRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.ShowGroupPanel = False
        Me.gvCustomer.ShowHeaderCellButtons = True
        Me.gvCustomer.Size = New System.Drawing.Size(845, 149)
        Me.gvCustomer.TabIndex = 2
        Me.gvCustomer.Text = "RadGridView1"
        '
        'gvCustomerGroup
        '
        Me.gvCustomerGroup.Location = New System.Drawing.Point(3, 3)
        '
        '
        '
        Me.gvCustomerGroup.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomerGroup.MasterTemplate.AllowEditRow = False
        Me.gvCustomerGroup.MasterTemplate.EnableFiltering = True
        Me.gvCustomerGroup.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomerGroup.Name = "gvCustomerGroup"
        Me.gvCustomerGroup.ShowGroupPanel = False
        Me.gvCustomerGroup.ShowHeaderCellButtons = True
        Me.gvCustomerGroup.Size = New System.Drawing.Size(845, 134)
        Me.gvCustomerGroup.TabIndex = 1
        Me.gvCustomerGroup.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(766, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(85, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.btnExport)
        Me.RadPanel2.Controls.Add(Me.btnBack)
        Me.RadPanel2.Controls.Add(Me.pnlAdminSetting)
        Me.RadPanel2.Controls.Add(Me.refreshbtn)
        Me.RadPanel2.Controls.Add(Me.btnPrint)
        Me.RadPanel2.Controls.Add(Me.btnClose)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel2.Location = New System.Drawing.Point(0, 615)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(842, 27)
        Me.RadPanel2.TabIndex = 14
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(159, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 330
        Me.btnExport.Text = "Export"
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
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(690, 4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(70, 18)
        Me.btnBack.TabIndex = 329
        Me.btnBack.Text = "<< Back"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(375, 4)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(170, 19)
        Me.pnlAdminSetting.TabIndex = 328
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
        'refreshbtn
        '
        Me.refreshbtn.Location = New System.Drawing.Point(12, 4)
        Me.refreshbtn.Name = "refreshbtn"
        Me.refreshbtn.Size = New System.Drawing.Size(70, 18)
        Me.refreshbtn.TabIndex = 8
        Me.refreshbtn.Text = ">>>"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(842, 20)
        Me.RadMenu1.TabIndex = 15
        Me.RadMenu1.Text = "RadMenu1"
        '
        'frmRptCSACustomerLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 642)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRptCSACustomerLedger"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Ledger"
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParentCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCumulativeClosing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlActiveInActiveCustomer.ResumeLayout(False)
        Me.pnlActiveInActiveCustomer.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.refreshbtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkInactive As System.Windows.Forms.RadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents refreshbtn As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkNone As System.Windows.Forms.RadioButton
    Friend WithEvents chkCustGroupWise As System.Windows.Forms.RadioButton
    Friend WithEvents chkCustWise As System.Windows.Forms.RadioButton
    Friend WithEvents pnlActiveInActiveCustomer As System.Windows.Forms.Panel
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents gvCustomerGroup As common.UserControls.MyRadGridView
    Friend WithEvents chkActive As System.Windows.Forms.RadioButton
    Friend WithEvents chkCumulativeClosing As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkItemWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkISParentCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkSecurity As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblParentCustomer As common.Controls.MyLabel
    Friend WithEvents txtParentCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents txtCompany As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomerType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

