<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptHierarchyWiseReport
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RdropGroupLevel = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RbtnDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.RbtnSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkPosted = New common.Controls.MyRadioButton()
        Me.chkUnposted = New common.Controls.MyRadioButton()
        Me.chkfrombankAll = New common.Controls.MyRadioButton()
        Me.txtMultAccountCodefnd = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultCostFnd = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountCode = New common.Controls.MyLabel()
        Me.lblCostCenter = New common.Controls.MyLabel()
        Me.txtHierarchyMult = New common.UserControls.txtMultiSelectFinder()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblHierarchy = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdropGroupLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkfrombankAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHierarchy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(763, 402)
        Me.SplitContainer1.SplitterDistance = 358
        Me.SplitContainer1.TabIndex = 5
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(763, 358)
        Me.RadPageView1.TabIndex = 13
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RdropGroupLevel)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkPosted)
        Me.RadPageViewPage1.Controls.Add(Me.chkUnposted)
        Me.RadPageViewPage1.Controls.Add(Me.chkfrombankAll)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultAccountCodefnd)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultCostFnd)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblCostCenter)
        Me.RadPageViewPage1.Controls.Add(Me.txtHierarchyMult)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocationMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblHierarchy)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(742, 310)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(343, 74)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel1.TabIndex = 321
        Me.MyLabel1.Text = "Grouping Level"
        '
        'RdropGroupLevel
        '
        RadListDataItem4.Text = "Location"
        RadListDataItem5.Text = "Location and Hierarchy"
        RadListDataItem6.Text = "Location, Hierarchy and Cost Center"
        Me.RdropGroupLevel.Items.Add(RadListDataItem4)
        Me.RdropGroupLevel.Items.Add(RadListDataItem5)
        Me.RdropGroupLevel.Items.Add(RadListDataItem6)
        Me.RdropGroupLevel.Location = New System.Drawing.Point(431, 72)
        Me.RdropGroupLevel.Name = "RdropGroupLevel"
        Me.RdropGroupLevel.Size = New System.Drawing.Size(166, 20)
        Me.RdropGroupLevel.TabIndex = 320
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RbtnDetail)
        Me.RadGroupBox1.Controls.Add(Me.RbtnSummary)
        Me.RadGroupBox1.HeaderText = "Type"
        Me.RadGroupBox1.Location = New System.Drawing.Point(279, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(154, 42)
        Me.RadGroupBox1.TabIndex = 319
        Me.RadGroupBox1.Text = "Type"
        '
        'RbtnDetail
        '
        Me.RbtnDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RbtnDetail.Location = New System.Drawing.Point(86, 15)
        Me.RbtnDetail.Name = "RbtnDetail"
        Me.RbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.RbtnDetail.TabIndex = 320
        Me.RbtnDetail.Text = "Detail"
        Me.RbtnDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RbtnSummary
        '
        Me.RbtnSummary.Location = New System.Drawing.Point(13, 16)
        Me.RbtnSummary.Name = "RbtnSummary"
        Me.RbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.RbtnSummary.TabIndex = 0
        Me.RbtnSummary.TabStop = False
        Me.RbtnSummary.Text = "Summary"
        '
        'chkPosted
        '
        Me.chkPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPosted.Location = New System.Drawing.Point(365, 48)
        Me.chkPosted.MyLinkLable1 = Nothing
        Me.chkPosted.MyLinkLable2 = Nothing
        Me.chkPosted.Name = "chkPosted"
        Me.chkPosted.Size = New System.Drawing.Size(54, 18)
        Me.chkPosted.TabIndex = 318
        Me.chkPosted.Text = "Posted"
        Me.chkPosted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkUnposted
        '
        Me.chkUnposted.Location = New System.Drawing.Point(446, 48)
        Me.chkUnposted.MyLinkLable1 = Nothing
        Me.chkUnposted.MyLinkLable2 = Nothing
        Me.chkUnposted.Name = "chkUnposted"
        Me.chkUnposted.Size = New System.Drawing.Size(69, 18)
        Me.chkUnposted.TabIndex = 317
        Me.chkUnposted.TabStop = False
        Me.chkUnposted.Text = "Unposted"
        '
        'chkfrombankAll
        '
        Me.chkfrombankAll.Location = New System.Drawing.Point(544, 48)
        Me.chkfrombankAll.MyLinkLable1 = Nothing
        Me.chkfrombankAll.MyLinkLable2 = Nothing
        Me.chkfrombankAll.Name = "chkfrombankAll"
        Me.chkfrombankAll.Size = New System.Drawing.Size(33, 18)
        Me.chkfrombankAll.TabIndex = 316
        Me.chkfrombankAll.TabStop = False
        Me.chkfrombankAll.Text = "All"
        '
        'txtMultAccountCodefnd
        '
        Me.txtMultAccountCodefnd.arrDispalyMember = Nothing
        Me.txtMultAccountCodefnd.arrValueMember = Nothing
        Me.txtMultAccountCodefnd.Location = New System.Drawing.Point(93, 119)
        Me.txtMultAccountCodefnd.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultAccountCodefnd.MyLinkLable1 = Nothing
        Me.txtMultAccountCodefnd.MyLinkLable2 = Nothing
        Me.txtMultAccountCodefnd.MyNullText = "All"
        Me.txtMultAccountCodefnd.Name = "txtMultAccountCodefnd"
        Me.txtMultAccountCodefnd.Size = New System.Drawing.Size(243, 19)
        Me.txtMultAccountCodefnd.TabIndex = 315
        Me.txtMultAccountCodefnd.Visible = False
        '
        'txtMultCostFnd
        '
        Me.txtMultCostFnd.arrDispalyMember = Nothing
        Me.txtMultCostFnd.arrValueMember = Nothing
        Me.txtMultCostFnd.Location = New System.Drawing.Point(93, 96)
        Me.txtMultCostFnd.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultCostFnd.MyLinkLable1 = Nothing
        Me.txtMultCostFnd.MyLinkLable2 = Nothing
        Me.txtMultCostFnd.MyNullText = "All"
        Me.txtMultCostFnd.Name = "txtMultCostFnd"
        Me.txtMultCostFnd.Size = New System.Drawing.Size(243, 19)
        Me.txtMultCostFnd.TabIndex = 314
        '
        'lblAccountCode
        '
        Me.lblAccountCode.FieldName = Nothing
        Me.lblAccountCode.Location = New System.Drawing.Point(16, 121)
        Me.lblAccountCode.Name = "lblAccountCode"
        Me.lblAccountCode.Size = New System.Drawing.Size(77, 18)
        Me.lblAccountCode.TabIndex = 313
        Me.lblAccountCode.Text = "Account Code"
        Me.lblAccountCode.Visible = False
        '
        'lblCostCenter
        '
        Me.lblCostCenter.FieldName = Nothing
        Me.lblCostCenter.Location = New System.Drawing.Point(16, 97)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(65, 18)
        Me.lblCostCenter.TabIndex = 312
        Me.lblCostCenter.Text = "Cost Center"
        '
        'txtHierarchyMult
        '
        Me.txtHierarchyMult.arrDispalyMember = Nothing
        Me.txtHierarchyMult.arrValueMember = Nothing
        Me.txtHierarchyMult.Location = New System.Drawing.Point(93, 71)
        Me.txtHierarchyMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHierarchyMult.MyLinkLable1 = Nothing
        Me.txtHierarchyMult.MyLinkLable2 = Nothing
        Me.txtHierarchyMult.MyNullText = "All"
        Me.txtHierarchyMult.Name = "txtHierarchyMult"
        Me.txtHierarchyMult.Size = New System.Drawing.Size(243, 19)
        Me.txtHierarchyMult.TabIndex = 311
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(93, 48)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(243, 19)
        Me.txtLocationMult.TabIndex = 310
        '
        'lblHierarchy
        '
        Me.lblHierarchy.FieldName = Nothing
        Me.lblHierarchy.Location = New System.Drawing.Point(16, 73)
        Me.lblHierarchy.Name = "lblHierarchy"
        Me.lblHierarchy.Size = New System.Drawing.Size(54, 18)
        Me.lblHierarchy.TabIndex = 309
        Me.lblHierarchy.Text = "Hierarchy"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(16, 49)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 308
        Me.lblLocation.Text = "Location"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 1)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
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
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(742, 310)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(742, 310)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(161, 7)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(82, 22)
        Me.btnExp.TabIndex = 327
        Me.btnExp.Text = "Export"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 322
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(84, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 323
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(670, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 324
        Me.btnClose.Text = "Close"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
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
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(763, 20)
        Me.RadMenu1.TabIndex = 6
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RptHierarchyWiseReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 422)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptHierarchyWiseReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptHierarchyWiseReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdropGroupLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkfrombankAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHierarchy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtHierarchyMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblHierarchy As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtMultAccountCodefnd As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultCostFnd As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountCode As common.Controls.MyLabel
    Friend WithEvents lblCostCenter As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkPosted As common.Controls.MyRadioButton
    Friend WithEvents chkUnposted As common.Controls.MyRadioButton
    Friend WithEvents chkfrombankAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RbtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RdropGroupLevel As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

