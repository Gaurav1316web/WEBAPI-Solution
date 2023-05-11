<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptCustomerAgeingDrillDown
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TxtMultiCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.chkPrintCustomerPerPage = New common.Controls.MyCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtZone = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.ddlCurrencyType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.GrpIsParent = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgParentCust = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ChkParentCustSelect = New common.Controls.MyRadioButton()
        Me.ChkParentCustAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkShowZeroBalance = New common.Controls.MyCheckBox()
        Me.ChkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkISParentCust = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkShowInvOnly = New common.Controls.MyCheckBox()
        Me.chkActive = New System.Windows.Forms.RadioButton()
        Me.dtpCutoffDate = New common.Controls.MyDateTimePicker()
        Me.chkInactive = New System.Windows.Forms.RadioButton()
        Me.chkType = New common.Controls.MyCheckBox()
        Me.chkAll = New System.Windows.Forms.RadioButton()
        Me.chkFifo = New common.Controls.MyCheckBox()
        Me.ddlAgedRcvbl = New common.Controls.MyComboBox()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.dtpAgeof = New common.Controls.MyDateTimePicker()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.txtCurr = New common.Controls.MyTextBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.txt1 = New common.Controls.MyTextBox()
        Me.txtOvr = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.txt2 = New common.Controls.MyTextBox()
        Me.txt8 = New common.Controls.MyTextBox()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.txt7 = New common.Controls.MyTextBox()
        Me.txt3 = New common.Controls.MyTextBox()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txt6 = New common.Controls.MyTextBox()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.txt4 = New common.Controls.MyTextBox()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.txt5 = New common.Controls.MyTextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkCGSelect = New common.Controls.MyRadioButton()
        Me.chkCGAll = New common.Controls.MyRadioButton()
        Me.cbgCustomerGroup = New common.MyCheckBoxGrid()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomer = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkCustomerSelect = New common.Controls.MyRadioButton()
        Me.chkCustomerAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.mniExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.rdb = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayour = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPrintCustomerPerPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpIsParent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpIsParent.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.ChkParentCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkParentCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkShowZeroBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowInvOnly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpCutoffDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFifo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlAgedRcvbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAgeof, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCurr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOvr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCGSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCGAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1140, 489)
        Me.RadPageView1.TabIndex = 22
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1119, 441)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtMultiCustomerCategory)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkPrintCustomerPerPage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtZone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlCurrencyType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomerGroup)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomerGroup)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpIsParent)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1119, 441)
        Me.SplitContainer1.SplitterDistance = 347
        Me.SplitContainer1.TabIndex = 22
        '
        'TxtMultiCustomerCategory
        '
        Me.TxtMultiCustomerCategory.arrDispalyMember = Nothing
        Me.TxtMultiCustomerCategory.arrValueMember = Nothing
        Me.TxtMultiCustomerCategory.Location = New System.Drawing.Point(138, 260)
        Me.TxtMultiCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerCategory.MyLinkLable1 = Nothing
        Me.TxtMultiCustomerCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerCategory.MyNullText = "All"
        Me.TxtMultiCustomerCategory.Name = "TxtMultiCustomerCategory"
        Me.TxtMultiCustomerCategory.Size = New System.Drawing.Size(344, 19)
        Me.TxtMultiCustomerCategory.TabIndex = 424
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(17, 260)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel11.TabIndex = 423
        Me.MyLabel11.Text = "Customer Category"
        '
        'chkPrintCustomerPerPage
        '
        Me.chkPrintCustomerPerPage.Location = New System.Drawing.Point(140, 329)
        Me.chkPrintCustomerPerPage.MyLinkLable1 = Nothing
        Me.chkPrintCustomerPerPage.MyLinkLable2 = Nothing
        Me.chkPrintCustomerPerPage.Name = "chkPrintCustomerPerPage"
        Me.chkPrintCustomerPerPage.Size = New System.Drawing.Size(159, 18)
        Me.chkPrintCustomerPerPage.TabIndex = 405
        Me.chkPrintCustomerPerPage.Tag1 = Nothing
        Me.chkPrintCustomerPerPage.Text = "Print Customer on per page"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(18, 281)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel4.TabIndex = 404
        Me.MyLabel4.Text = "Zone"
        '
        'TxtZone
        '
        Me.TxtZone.arrDispalyMember = Nothing
        Me.TxtZone.arrValueMember = Nothing
        Me.TxtZone.Location = New System.Drawing.Point(138, 281)
        Me.TxtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZone.MyLinkLable1 = Me.MyLabel4
        Me.TxtZone.MyLinkLable2 = Nothing
        Me.TxtZone.MyNullText = "All"
        Me.TxtZone.Name = "TxtZone"
        Me.TxtZone.Size = New System.Drawing.Size(344, 19)
        Me.TxtZone.TabIndex = 403
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(17, 194)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel5.TabIndex = 398
        Me.MyLabel5.Text = "Currency "
        '
        'ddlCurrencyType
        '
        Me.ddlCurrencyType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCurrencyType.Location = New System.Drawing.Point(138, 193)
        Me.ddlCurrencyType.Name = "ddlCurrencyType"
        Me.ddlCurrencyType.Size = New System.Drawing.Size(344, 20)
        Me.ddlCurrencyType.TabIndex = 399
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(17, 304)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 380
        Me.lblCustomer.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(138, 303)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomer.TabIndex = 379
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(17, 239)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 378
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(138, 238)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerGroup.TabIndex = 377
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(17, 217)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 376
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(138, 216)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 375
        '
        'GrpIsParent
        '
        Me.GrpIsParent.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpIsParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpIsParent.Controls.Add(Me.cbgParentCust)
        Me.GrpIsParent.Controls.Add(Me.Panel6)
        Me.GrpIsParent.HeaderText = "Parent Customer"
        Me.GrpIsParent.Location = New System.Drawing.Point(743, 3)
        Me.GrpIsParent.Name = "GrpIsParent"
        Me.GrpIsParent.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpIsParent.Size = New System.Drawing.Size(141, 120)
        Me.GrpIsParent.TabIndex = 143
        Me.GrpIsParent.Text = "Parent Customer"
        Me.GrpIsParent.Visible = False
        '
        'cbgParentCust
        '
        Me.cbgParentCust.CheckedValue = Nothing
        Me.cbgParentCust.DataSource = Nothing
        Me.cbgParentCust.DisplayMember = "Name"
        Me.cbgParentCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgParentCust.Location = New System.Drawing.Point(10, 40)
        Me.cbgParentCust.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgParentCust.MyShowHeadrText = False
        Me.cbgParentCust.Name = "cbgParentCust"
        Me.cbgParentCust.Size = New System.Drawing.Size(121, 70)
        Me.cbgParentCust.TabIndex = 1
        Me.cbgParentCust.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.ChkParentCustSelect)
        Me.Panel6.Controls.Add(Me.ChkParentCustAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(121, 20)
        Me.Panel6.TabIndex = 0
        '
        'ChkParentCustSelect
        '
        Me.ChkParentCustSelect.Location = New System.Drawing.Point(47, 1)
        Me.ChkParentCustSelect.MyLinkLable1 = Nothing
        Me.ChkParentCustSelect.MyLinkLable2 = Nothing
        Me.ChkParentCustSelect.Name = "ChkParentCustSelect"
        Me.ChkParentCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkParentCustSelect.TabIndex = 1
        Me.ChkParentCustSelect.Text = "Select"
        '
        'ChkParentCustAll
        '
        Me.ChkParentCustAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkParentCustAll.Location = New System.Drawing.Point(11, 1)
        Me.ChkParentCustAll.MyLinkLable1 = Nothing
        Me.ChkParentCustAll.MyLinkLable2 = Nothing
        Me.ChkParentCustAll.Name = "ChkParentCustAll"
        Me.ChkParentCustAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkParentCustAll.TabIndex = 0
        Me.ChkParentCustAll.Text = "All"
        Me.ChkParentCustAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkShowZeroBalance)
        Me.RadGroupBox4.Controls.Add(Me.ChkSecurity)
        Me.RadGroupBox4.Controls.Add(Me.ChkISParentCust)
        Me.RadGroupBox4.Controls.Add(Me.chkShowInvOnly)
        Me.RadGroupBox4.Controls.Add(Me.chkActive)
        Me.RadGroupBox4.Controls.Add(Me.dtpCutoffDate)
        Me.RadGroupBox4.Controls.Add(Me.chkInactive)
        Me.RadGroupBox4.Controls.Add(Me.chkType)
        Me.RadGroupBox4.Controls.Add(Me.chkAll)
        Me.RadGroupBox4.Controls.Add(Me.chkFifo)
        Me.RadGroupBox4.Controls.Add(Me.ddlAgedRcvbl)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel25)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel15)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel24)
        Me.RadGroupBox4.Controls.Add(Me.dtpAgeof)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel23)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel22)
        Me.RadGroupBox4.Controls.Add(Me.txtCurr)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel21)
        Me.RadGroupBox4.Controls.Add(Me.txt1)
        Me.RadGroupBox4.Controls.Add(Me.txtOvr)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel20)
        Me.RadGroupBox4.Controls.Add(Me.txt2)
        Me.RadGroupBox4.Controls.Add(Me.txt8)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox4.Controls.Add(Me.txt7)
        Me.RadGroupBox4.Controls.Add(Me.txt3)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel19)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox4.Controls.Add(Me.txt6)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox4.Controls.Add(Me.txt4)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel18)
        Me.RadGroupBox4.Controls.Add(Me.txt5)
        Me.RadGroupBox4.HeaderText = "Main Filter"
        Me.RadGroupBox4.Location = New System.Drawing.Point(10, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(513, 184)
        Me.RadGroupBox4.TabIndex = 142
        Me.RadGroupBox4.Text = "Main Filter"
        '
        'chkShowZeroBalance
        '
        Me.chkShowZeroBalance.Location = New System.Drawing.Point(155, 45)
        Me.chkShowZeroBalance.MyLinkLable1 = Nothing
        Me.chkShowZeroBalance.MyLinkLable2 = Nothing
        Me.chkShowZeroBalance.Name = "chkShowZeroBalance"
        Me.chkShowZeroBalance.Size = New System.Drawing.Size(115, 18)
        Me.chkShowZeroBalance.TabIndex = 145
        Me.chkShowZeroBalance.Tag1 = Nothing
        Me.chkShowZeroBalance.Text = "Show Zero Balance"
        Me.chkShowZeroBalance.Visible = False
        '
        'ChkSecurity
        '
        Me.ChkSecurity.Location = New System.Drawing.Point(417, 164)
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(90, 18)
        Me.ChkSecurity.TabIndex = 144
        Me.ChkSecurity.Text = "Show Security"
        Me.ChkSecurity.Visible = False
        '
        'ChkISParentCust
        '
        Me.ChkISParentCust.Location = New System.Drawing.Point(393, 141)
        Me.ChkISParentCust.Name = "ChkISParentCust"
        Me.ChkISParentCust.Size = New System.Drawing.Size(115, 18)
        Me.ChkISParentCust.TabIndex = 143
        Me.ChkISParentCust.Text = "Is Parent Customer"
        Me.ChkISParentCust.Visible = False
        '
        'chkShowInvOnly
        '
        Me.chkShowInvOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowInvOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkShowInvOnly.Location = New System.Drawing.Point(273, 75)
        Me.chkShowInvOnly.MyLinkLable1 = Nothing
        Me.chkShowInvOnly.MyLinkLable2 = Nothing
        Me.chkShowInvOnly.Name = "chkShowInvOnly"
        Me.chkShowInvOnly.Size = New System.Drawing.Size(234, 18)
        Me.chkShowInvOnly.TabIndex = 142
        Me.chkShowInvOnly.Tag1 = Nothing
        Me.chkShowInvOnly.Text = "Show only Invoices and related documents"
        Me.chkShowInvOnly.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkShowInvOnly.Visible = False
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(13, 161)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(107, 17)
        Me.chkActive.TabIndex = 138
        Me.chkActive.Text = "Active Customer"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'dtpCutoffDate
        '
        Me.dtpCutoffDate.CalculationExpression = Nothing
        Me.dtpCutoffDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpCutoffDate.FieldCode = Nothing
        Me.dtpCutoffDate.FieldDesc = Nothing
        Me.dtpCutoffDate.FieldMaxLength = 0
        Me.dtpCutoffDate.FieldName = Nothing
        Me.dtpCutoffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCutoffDate.isCalculatedField = False
        Me.dtpCutoffDate.IsSourceFromTable = False
        Me.dtpCutoffDate.IsSourceFromValueList = False
        Me.dtpCutoffDate.IsUnique = False
        Me.dtpCutoffDate.Location = New System.Drawing.Point(409, 42)
        Me.dtpCutoffDate.MendatroryField = False
        Me.dtpCutoffDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCutoffDate.MyLinkLable1 = Nothing
        Me.dtpCutoffDate.MyLinkLable2 = Nothing
        Me.dtpCutoffDate.Name = "dtpCutoffDate"
        Me.dtpCutoffDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCutoffDate.ReferenceFieldDesc = Nothing
        Me.dtpCutoffDate.ReferenceFieldName = Nothing
        Me.dtpCutoffDate.ReferenceTableName = Nothing
        Me.dtpCutoffDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpCutoffDate.TabIndex = 141
        Me.dtpCutoffDate.TabStop = False
        Me.dtpCutoffDate.Text = "04/08/2011"
        Me.dtpCutoffDate.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        Me.dtpCutoffDate.Visible = False
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Location = New System.Drawing.Point(127, 161)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(116, 17)
        Me.chkInactive.TabIndex = 139
        Me.chkInactive.Text = "Inactive Customer"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'chkType
        '
        Me.chkType.Location = New System.Drawing.Point(82, 45)
        Me.chkType.MyLinkLable1 = Nothing
        Me.chkType.MyLinkLable2 = Nothing
        Me.chkType.Name = "chkType"
        Me.chkType.Size = New System.Drawing.Size(67, 18)
        Me.chkType.TabIndex = 110
        Me.chkType.Tag1 = Nothing
        Me.chkType.Text = "Summary"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(249, 161)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(95, 17)
        Me.chkAll.TabIndex = 140
        Me.chkAll.Text = "All Customers"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkFifo
        '
        Me.chkFifo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkFifo.Location = New System.Drawing.Point(270, 45)
        Me.chkFifo.MyLinkLable1 = Nothing
        Me.chkFifo.MyLinkLable2 = Nothing
        Me.chkFifo.Name = "chkFifo"
        Me.chkFifo.Size = New System.Drawing.Size(76, 18)
        Me.chkFifo.TabIndex = 137
        Me.chkFifo.Tag1 = Nothing
        Me.chkFifo.Text = "FIFO Based"
        Me.chkFifo.Visible = False
        '
        'ddlAgedRcvbl
        '
        Me.ddlAgedRcvbl.AutoCompleteDisplayMember = Nothing
        Me.ddlAgedRcvbl.AutoCompleteValueMember = Nothing
        Me.ddlAgedRcvbl.CalculationExpression = Nothing
        Me.ddlAgedRcvbl.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlAgedRcvbl.FieldCode = Nothing
        Me.ddlAgedRcvbl.FieldDesc = Nothing
        Me.ddlAgedRcvbl.FieldMaxLength = 0
        Me.ddlAgedRcvbl.FieldName = Nothing
        Me.ddlAgedRcvbl.isCalculatedField = False
        Me.ddlAgedRcvbl.IsSourceFromTable = False
        Me.ddlAgedRcvbl.IsSourceFromValueList = False
        Me.ddlAgedRcvbl.IsUnique = False
        RadListDataItem1.Text = "Aged Trial Balance By Due Date"
        RadListDataItem2.Text = "Aged Trial Balance By Document date"
        Me.ddlAgedRcvbl.Items.Add(RadListDataItem1)
        Me.ddlAgedRcvbl.Items.Add(RadListDataItem2)
        Me.ddlAgedRcvbl.Location = New System.Drawing.Point(82, 21)
        Me.ddlAgedRcvbl.MendatroryField = False
        Me.ddlAgedRcvbl.MyLinkLable1 = Nothing
        Me.ddlAgedRcvbl.MyLinkLable2 = Nothing
        Me.ddlAgedRcvbl.Name = "ddlAgedRcvbl"
        Me.ddlAgedRcvbl.ReferenceFieldDesc = Nothing
        Me.ddlAgedRcvbl.ReferenceFieldName = Nothing
        Me.ddlAgedRcvbl.ReferenceTableName = Nothing
        Me.ddlAgedRcvbl.Size = New System.Drawing.Size(268, 20)
        Me.ddlAgedRcvbl.TabIndex = 111
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Location = New System.Drawing.Point(363, 94)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel25.TabIndex = 133
        Me.RadLabel25.Text = "7th"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Location = New System.Drawing.Point(17, 75)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(255, 18)
        Me.RadLabel15.TabIndex = 129
        Me.RadLabel15.Text = "------------------- Aging Periods -------------------"
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Location = New System.Drawing.Point(311, 94)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel24.TabIndex = 132
        Me.RadLabel24.Text = "6th"
        '
        'dtpAgeof
        '
        Me.dtpAgeof.CalculationExpression = Nothing
        Me.dtpAgeof.CustomFormat = "dd/MM/yyyy"
        Me.dtpAgeof.FieldCode = Nothing
        Me.dtpAgeof.FieldDesc = Nothing
        Me.dtpAgeof.FieldMaxLength = 0
        Me.dtpAgeof.FieldName = Nothing
        Me.dtpAgeof.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAgeof.isCalculatedField = False
        Me.dtpAgeof.IsSourceFromTable = False
        Me.dtpAgeof.IsSourceFromValueList = False
        Me.dtpAgeof.IsUnique = False
        Me.dtpAgeof.Location = New System.Drawing.Point(409, 18)
        Me.dtpAgeof.MendatroryField = False
        Me.dtpAgeof.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAgeof.MyLinkLable1 = Nothing
        Me.dtpAgeof.MyLinkLable2 = Nothing
        Me.dtpAgeof.Name = "dtpAgeof"
        Me.dtpAgeof.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAgeof.ReferenceFieldDesc = Nothing
        Me.dtpAgeof.ReferenceFieldName = Nothing
        Me.dtpAgeof.ReferenceTableName = Nothing
        Me.dtpAgeof.Size = New System.Drawing.Size(96, 20)
        Me.dtpAgeof.TabIndex = 114
        Me.dtpAgeof.TabStop = False
        Me.dtpAgeof.Text = "04/08/2011"
        Me.dtpAgeof.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Location = New System.Drawing.Point(257, 94)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel23.TabIndex = 131
        Me.RadLabel23.Text = "5th"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(342, 44)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(64, 18)
        Me.RadLabel4.TabIndex = 115
        Me.RadLabel4.Text = "Cutoff Date"
        Me.RadLabel4.Visible = False
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Location = New System.Drawing.Point(204, 94)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel22.TabIndex = 136
        Me.RadLabel22.Text = "4th"
        '
        'txtCurr
        '
        Me.txtCurr.CalculationExpression = Nothing
        Me.txtCurr.Enabled = False
        Me.txtCurr.FieldCode = Nothing
        Me.txtCurr.FieldDesc = Nothing
        Me.txtCurr.FieldMaxLength = 0
        Me.txtCurr.FieldName = Nothing
        Me.txtCurr.isCalculatedField = False
        Me.txtCurr.IsSourceFromTable = False
        Me.txtCurr.IsSourceFromValueList = False
        Me.txtCurr.IsUnique = False
        Me.txtCurr.Location = New System.Drawing.Point(6, 117)
        Me.txtCurr.MaxLength = 4
        Me.txtCurr.MendatroryField = False
        Me.txtCurr.MyLinkLable1 = Nothing
        Me.txtCurr.MyLinkLable2 = Nothing
        Me.txtCurr.Name = "txtCurr"
        Me.txtCurr.ReadOnly = True
        Me.txtCurr.ReferenceFieldDesc = Nothing
        Me.txtCurr.ReferenceFieldName = Nothing
        Me.txtCurr.ReferenceTableName = Nothing
        Me.txtCurr.Size = New System.Drawing.Size(44, 20)
        Me.txtCurr.TabIndex = 116
        Me.txtCurr.Text = "0"
        Me.txtCurr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Location = New System.Drawing.Point(413, 94)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel21.TabIndex = 135
        Me.RadLabel21.Text = "8th"
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
        Me.txt1.Location = New System.Drawing.Point(54, 117)
        Me.txt1.MaxLength = 4
        Me.txt1.MendatroryField = False
        Me.txt1.MyLinkLable1 = Nothing
        Me.txt1.MyLinkLable2 = Nothing
        Me.txt1.Name = "txt1"
        Me.txt1.ReferenceFieldDesc = Nothing
        Me.txt1.ReferenceFieldName = Nothing
        Me.txt1.ReferenceTableName = Nothing
        Me.txt1.Size = New System.Drawing.Size(44, 20)
        Me.txt1.TabIndex = 117
        Me.txt1.Text = "15"
        Me.txt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOvr
        '
        Me.txtOvr.CalculationExpression = Nothing
        Me.txtOvr.Enabled = False
        Me.txtOvr.FieldCode = Nothing
        Me.txtOvr.FieldDesc = Nothing
        Me.txtOvr.FieldMaxLength = 0
        Me.txtOvr.FieldName = Nothing
        Me.txtOvr.isCalculatedField = False
        Me.txtOvr.IsSourceFromTable = False
        Me.txtOvr.IsSourceFromValueList = False
        Me.txtOvr.IsUnique = False
        Me.txtOvr.Location = New System.Drawing.Point(464, 117)
        Me.txtOvr.MendatroryField = False
        Me.txtOvr.MyLinkLable1 = Nothing
        Me.txtOvr.MyLinkLable2 = Nothing
        Me.txtOvr.Name = "txtOvr"
        Me.txtOvr.ReadOnly = True
        Me.txtOvr.ReferenceFieldDesc = Nothing
        Me.txtOvr.ReferenceFieldName = Nothing
        Me.txtOvr.ReferenceTableName = Nothing
        Me.txtOvr.Size = New System.Drawing.Size(44, 20)
        Me.txtOvr.TabIndex = 128
        Me.txtOvr.Text = "120"
        Me.txtOvr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(350, 20)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel2.TabIndex = 113
        Me.RadLabel2.Text = "Age As Of"
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Location = New System.Drawing.Point(464, 94)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(30, 18)
        Me.RadLabel20.TabIndex = 134
        Me.RadLabel20.Text = "Over"
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
        Me.txt2.Location = New System.Drawing.Point(103, 117)
        Me.txt2.MaxLength = 4
        Me.txt2.MendatroryField = False
        Me.txt2.MyLinkLable1 = Nothing
        Me.txt2.MyLinkLable2 = Nothing
        Me.txt2.Name = "txt2"
        Me.txt2.ReferenceFieldDesc = Nothing
        Me.txt2.ReferenceFieldName = Nothing
        Me.txt2.ReferenceTableName = Nothing
        Me.txt2.Size = New System.Drawing.Size(44, 20)
        Me.txt2.TabIndex = 118
        Me.txt2.Text = "30"
        Me.txt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt8
        '
        Me.txt8.CalculationExpression = Nothing
        Me.txt8.FieldCode = Nothing
        Me.txt8.FieldDesc = Nothing
        Me.txt8.FieldMaxLength = 0
        Me.txt8.FieldName = Nothing
        Me.txt8.isCalculatedField = False
        Me.txt8.IsSourceFromTable = False
        Me.txt8.IsSourceFromValueList = False
        Me.txt8.IsUnique = False
        Me.txt8.Location = New System.Drawing.Point(413, 117)
        Me.txt8.MaxLength = 4
        Me.txt8.MendatroryField = False
        Me.txt8.MyLinkLable1 = Nothing
        Me.txt8.MyLinkLable2 = Nothing
        Me.txt8.Name = "txt8"
        Me.txt8.ReferenceFieldDesc = Nothing
        Me.txt8.ReferenceFieldName = Nothing
        Me.txt8.ReferenceTableName = Nothing
        Me.txt8.Size = New System.Drawing.Size(44, 20)
        Me.txt8.TabIndex = 127
        Me.txt8.Text = "120"
        Me.txt8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Location = New System.Drawing.Point(6, 94)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(44, 18)
        Me.RadLabel16.TabIndex = 123
        Me.RadLabel16.Text = "Current"
        '
        'txt7
        '
        Me.txt7.CalculationExpression = Nothing
        Me.txt7.FieldCode = Nothing
        Me.txt7.FieldDesc = Nothing
        Me.txt7.FieldMaxLength = 0
        Me.txt7.FieldName = Nothing
        Me.txt7.isCalculatedField = False
        Me.txt7.IsSourceFromTable = False
        Me.txt7.IsSourceFromValueList = False
        Me.txt7.IsUnique = False
        Me.txt7.Location = New System.Drawing.Point(362, 117)
        Me.txt7.MaxLength = 4
        Me.txt7.MendatroryField = False
        Me.txt7.MyLinkLable1 = Nothing
        Me.txt7.MyLinkLable2 = Nothing
        Me.txt7.Name = "txt7"
        Me.txt7.ReferenceFieldDesc = Nothing
        Me.txt7.ReferenceFieldName = Nothing
        Me.txt7.ReferenceTableName = Nothing
        Me.txt7.Size = New System.Drawing.Size(44, 20)
        Me.txt7.TabIndex = 125
        Me.txt7.Text = "105"
        Me.txt7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txt3.Location = New System.Drawing.Point(153, 117)
        Me.txt3.MaxLength = 4
        Me.txt3.MendatroryField = False
        Me.txt3.MyLinkLable1 = Nothing
        Me.txt3.MyLinkLable2 = Nothing
        Me.txt3.Name = "txt3"
        Me.txt3.ReferenceFieldDesc = Nothing
        Me.txt3.ReferenceFieldName = Nothing
        Me.txt3.ReferenceTableName = Nothing
        Me.txt3.Size = New System.Drawing.Size(44, 20)
        Me.txt3.TabIndex = 119
        Me.txt3.Text = "45"
        Me.txt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Location = New System.Drawing.Point(153, 94)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(23, 18)
        Me.RadLabel19.TabIndex = 130
        Me.RadLabel19.Text = "3rd"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(64, 18)
        Me.RadLabel1.TabIndex = 112
        Me.RadLabel1.Text = "ReportType"
        '
        'txt6
        '
        Me.txt6.CalculationExpression = Nothing
        Me.txt6.FieldCode = Nothing
        Me.txt6.FieldDesc = Nothing
        Me.txt6.FieldMaxLength = 0
        Me.txt6.FieldName = Nothing
        Me.txt6.isCalculatedField = False
        Me.txt6.IsSourceFromTable = False
        Me.txt6.IsSourceFromValueList = False
        Me.txt6.IsUnique = False
        Me.txt6.Location = New System.Drawing.Point(309, 117)
        Me.txt6.MaxLength = 4
        Me.txt6.MendatroryField = False
        Me.txt6.MyLinkLable1 = Nothing
        Me.txt6.MyLinkLable2 = Nothing
        Me.txt6.Name = "txt6"
        Me.txt6.ReferenceFieldDesc = Nothing
        Me.txt6.ReferenceFieldName = Nothing
        Me.txt6.ReferenceTableName = Nothing
        Me.txt6.Size = New System.Drawing.Size(44, 20)
        Me.txt6.TabIndex = 122
        Me.txt6.Text = "90"
        Me.txt6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Location = New System.Drawing.Point(68, 94)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(18, 18)
        Me.RadLabel17.TabIndex = 124
        Me.RadLabel17.Text = "Ist"
        '
        'txt4
        '
        Me.txt4.CalculationExpression = Nothing
        Me.txt4.FieldCode = Nothing
        Me.txt4.FieldDesc = Nothing
        Me.txt4.FieldMaxLength = 0
        Me.txt4.FieldName = Nothing
        Me.txt4.isCalculatedField = False
        Me.txt4.IsSourceFromTable = False
        Me.txt4.IsSourceFromValueList = False
        Me.txt4.IsUnique = False
        Me.txt4.Location = New System.Drawing.Point(204, 117)
        Me.txt4.MendatroryField = False
        Me.txt4.MyLinkLable1 = Nothing
        Me.txt4.MyLinkLable2 = Nothing
        Me.txt4.Name = "txt4"
        Me.txt4.ReferenceFieldDesc = Nothing
        Me.txt4.ReferenceFieldName = Nothing
        Me.txt4.ReferenceTableName = Nothing
        Me.txt4.Size = New System.Drawing.Size(44, 20)
        Me.txt4.TabIndex = 120
        Me.txt4.Text = "60"
        Me.txt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Location = New System.Drawing.Point(103, 94)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(25, 18)
        Me.RadLabel18.TabIndex = 126
        Me.RadLabel18.Text = "2nd"
        '
        'txt5
        '
        Me.txt5.CalculationExpression = Nothing
        Me.txt5.FieldCode = Nothing
        Me.txt5.FieldDesc = Nothing
        Me.txt5.FieldMaxLength = 0
        Me.txt5.FieldName = Nothing
        Me.txt5.isCalculatedField = False
        Me.txt5.IsSourceFromTable = False
        Me.txt5.IsSourceFromValueList = False
        Me.txt5.IsUnique = False
        Me.txt5.Location = New System.Drawing.Point(257, 117)
        Me.txt5.MaxLength = 4
        Me.txt5.MendatroryField = False
        Me.txt5.MyLinkLable1 = Nothing
        Me.txt5.MyLinkLable2 = Nothing
        Me.txt5.Name = "txt5"
        Me.txt5.ReferenceFieldDesc = Nothing
        Me.txt5.ReferenceFieldName = Nothing
        Me.txt5.ReferenceTableName = Nothing
        Me.txt5.Size = New System.Drawing.Size(44, 20)
        Me.txt5.TabIndex = 121
        Me.txt5.Text = "75"
        Me.txt5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.Controls.Add(Me.cbgCustomerGroup)
        Me.RadGroupBox2.HeaderText = "Customer Group"
        Me.RadGroupBox2.Location = New System.Drawing.Point(903, 9)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(146, 77)
        Me.RadGroupBox2.TabIndex = 109
        Me.RadGroupBox2.Text = "Customer Group"
        Me.RadGroupBox2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCGSelect)
        Me.Panel4.Controls.Add(Me.chkCGAll)
        Me.Panel4.Location = New System.Drawing.Point(7, 17)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(292, 25)
        Me.Panel4.TabIndex = 26
        '
        'chkCGSelect
        '
        Me.chkCGSelect.Location = New System.Drawing.Point(77, 3)
        Me.chkCGSelect.MyLinkLable1 = Nothing
        Me.chkCGSelect.MyLinkLable2 = Nothing
        Me.chkCGSelect.Name = "chkCGSelect"
        Me.chkCGSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCGSelect.TabIndex = 1
        Me.chkCGSelect.Text = "Select"
        '
        'chkCGAll
        '
        Me.chkCGAll.Location = New System.Drawing.Point(27, 3)
        Me.chkCGAll.MyLinkLable1 = Nothing
        Me.chkCGAll.MyLinkLable2 = Nothing
        Me.chkCGAll.Name = "chkCGAll"
        Me.chkCGAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCGAll.TabIndex = 0
        Me.chkCGAll.Text = "All"
        '
        'cbgCustomerGroup
        '
        Me.cbgCustomerGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbgCustomerGroup.CheckedValue = Nothing
        Me.cbgCustomerGroup.DataSource = Nothing
        Me.cbgCustomerGroup.DisplayMember = "Name"
        Me.cbgCustomerGroup.Location = New System.Drawing.Point(7, 45)
        Me.cbgCustomerGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomerGroup.MyShowHeadrText = False
        Me.cbgCustomerGroup.Name = "cbgCustomerGroup"
        Me.cbgCustomerGroup.Size = New System.Drawing.Size(129, 32)
        Me.cbgCustomerGroup.TabIndex = 0
        Me.cbgCustomerGroup.ValueMember = "Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox1.Controls.Add(Me.Panel3)
        Me.RadGroupBox1.HeaderText = "Customer"
        Me.RadGroupBox1.Location = New System.Drawing.Point(903, 99)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(146, 113)
        Me.RadGroupBox1.TabIndex = 105
        Me.RadGroupBox1.Text = "Customer"
        Me.RadGroupBox1.Visible = False
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 45)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(126, 58)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkCustomerSelect)
        Me.Panel3.Controls.Add(Me.chkCustomerAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(126, 25)
        Me.Panel3.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(109, 4)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCustomerAll.Location = New System.Drawing.Point(58, 3)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        Me.chkCustomerAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(743, 99)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(141, 82)
        Me.RadGroupBox3.TabIndex = 2
        Me.RadGroupBox3.Text = "Location"
        Me.RadGroupBox3.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(131, 37)
        Me.cbgLocation.TabIndex = 0
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocSelect)
        Me.Panel2.Controls.Add(Me.chkLocAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(121, 25)
        Me.Panel2.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(62, 4)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLocAll.Location = New System.Drawing.Point(11, 4)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        Me.chkLocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(959, 441)
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
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(959, 441)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadSplitButton1)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 509)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1140, 51)
        Me.Panel1.TabIndex = 23
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mniExcel, Me.rmiPDF, Me.ExcelGrid, Me.PDFGrid})
        Me.RadSplitButton1.Location = New System.Drawing.Point(172, 16)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 24)
        Me.RadSplitButton1.TabIndex = 158
        Me.RadSplitButton1.Text = "Export"
        '
        'mniExcel
        '
        Me.mniExcel.AccessibleDescription = "Excel"
        Me.mniExcel.AccessibleName = "Excel"
        Me.mniExcel.Name = "mniExcel"
        Me.mniExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
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
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(269, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 24)
        Me.btnReset.TabIndex = 40
        Me.btnReset.Text = "Reset"
        Me.btnReset.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(102, 16)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 24)
        Me.RadButton1.TabIndex = 39
        Me.RadButton1.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1071, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(5, 16)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(95, 24)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = ">>>"
        '
        'rdb
        '
        Me.rdb.Location = New System.Drawing.Point(110, 14)
        Me.rdb.Name = "rdb"
        Me.rdb.Size = New System.Drawing.Size(78, 18)
        Me.rdb.TabIndex = 105
        Me.rdb.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(9, 14)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(78, 18)
        Me.rdbSummary.TabIndex = 105
        Me.rdbSummary.Text = "Summary"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1140, 20)
        Me.RadMenu1.TabIndex = 24
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btSaveLayout, Me.btnDeleteLayour})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        '
        'btSaveLayout
        '
        Me.btSaveLayout.AccessibleDescription = "btSaveLayout"
        Me.btSaveLayout.AccessibleName = "btSaveLayout"
        Me.btSaveLayout.Name = "btSaveLayout"
        Me.btSaveLayout.Text = "Save Layout"
        '
        'btnDeleteLayour
        '
        Me.btnDeleteLayour.AccessibleDescription = "btnDeleteLayour"
        Me.btnDeleteLayour.AccessibleName = "btnDeleteLayour"
        Me.btnDeleteLayour.Name = "btnDeleteLayour"
        Me.btnDeleteLayour.Text = "Delete Layout"
        '
        'rptCustomerAgeingDrillDown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1140, 560)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptCustomerAgeingDrillDown"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Ageing(Drill Down)"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPrintCustomerPerPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpIsParent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpIsParent.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.ChkParentCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkParentCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkShowZeroBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowInvOnly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpCutoffDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFifo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlAgedRcvbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAgeof, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCurr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOvr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCGSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCGAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents rdb As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCGSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCGAll As common.Controls.MyRadioButton
    Friend WithEvents cbgCustomerGroup As common.MyCheckBoxGrid
    Friend WithEvents chkAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkInactive As System.Windows.Forms.RadioButton
    Friend WithEvents chkActive As System.Windows.Forms.RadioButton
    Friend WithEvents chkType As common.Controls.MyCheckBox
    Friend WithEvents chkFifo As common.Controls.MyCheckBox
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents txtOvr As common.Controls.MyTextBox
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents txt8 As common.Controls.MyTextBox
    Friend WithEvents txt7 As common.Controls.MyTextBox
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents txt6 As common.Controls.MyTextBox
    Friend WithEvents txt4 As common.Controls.MyTextBox
    Friend WithEvents txt5 As common.Controls.MyTextBox
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txt3 As common.Controls.MyTextBox
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents txt2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txt1 As common.Controls.MyTextBox
    Friend WithEvents txtCurr As common.Controls.MyTextBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents ddlAgedRcvbl As common.Controls.MyComboBox
    Friend WithEvents dtpAgeof As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents dtpCutoffDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkShowInvOnly As common.Controls.MyCheckBox
    Friend WithEvents ChkISParentCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GrpIsParent As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgParentCust As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents ChkParentCustSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkParentCustAll As common.Controls.MyRadioButton
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents ChkSecurity As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlCurrencyType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkShowZeroBalance As common.Controls.MyCheckBox
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents mniExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnDeleteLayour As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkPrintCustomerPerPage As common.Controls.MyCheckBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtMultiCustomerCategory As common.UserControls.txtMultiSelectFinder
End Class

