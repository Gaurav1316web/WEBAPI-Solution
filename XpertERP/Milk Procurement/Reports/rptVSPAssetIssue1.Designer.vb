<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptVSPAssetIssue1
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkCurrentStock = New System.Windows.Forms.CheckBox()
        Me.lblItem = New common.Controls.MyLabel()
        Me.TxtMultiItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblVSP = New common.Controls.MyLabel()
        Me.TxtMultiVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.TxtMultiMCC = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkAssetIssueWithPurchase = New System.Windows.Forms.CheckBox()
        Me.rbtnSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.cboType = New common.Controls.MyComboBox()
        Me.lblModeofTransport = New common.Controls.MyLabel()
        Me.GBItem = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkItemSelect = New common.Controls.MyRadioButton()
        Me.chkItemAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVSP = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkVSPSelect = New common.Controls.MyRadioButton()
        Me.chkVSPAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMCC = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkMCCSelect = New common.Controls.MyRadioButton()
        Me.chkMCCAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GBItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBItem.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkVSPSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVSPAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1242, 483)
        Me.SplitContainer1.SplitterDistance = 432
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1242, 432)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkCurrentStock)
        Me.RadPageViewPage1.Controls.Add(Me.lblItem)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiItem)
        Me.RadPageViewPage1.Controls.Add(Me.lblVSP)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCC)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiMCC)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.cboType)
        Me.RadPageViewPage1.Controls.Add(Me.lblModeofTransport)
        Me.RadPageViewPage1.Controls.Add(Me.GBItem)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1221, 384)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkCurrentStock
        '
        Me.chkCurrentStock.AutoSize = True
        Me.chkCurrentStock.Location = New System.Drawing.Point(579, 14)
        Me.chkCurrentStock.Name = "chkCurrentStock"
        Me.chkCurrentStock.Size = New System.Drawing.Size(104, 17)
        Me.chkCurrentStock.TabIndex = 309
        Me.chkCurrentStock.Text = "Stock VSP Wise"
        Me.chkCurrentStock.UseVisualStyleBackColor = True
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Location = New System.Drawing.Point(3, 110)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 62
        Me.lblItem.Text = "Item"
        '
        'TxtMultiItem
        '
        Me.TxtMultiItem.arrDispalyMember = Nothing
        Me.TxtMultiItem.arrValueMember = Nothing
        Me.TxtMultiItem.Location = New System.Drawing.Point(72, 111)
        Me.TxtMultiItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiItem.MyLinkLable1 = Nothing
        Me.TxtMultiItem.MyLinkLable2 = Nothing
        Me.TxtMultiItem.MyNullText = "All"
        Me.TxtMultiItem.Name = "TxtMultiItem"
        Me.TxtMultiItem.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiItem.TabIndex = 61
        '
        'lblVSP
        '
        Me.lblVSP.FieldName = Nothing
        Me.lblVSP.Location = New System.Drawing.Point(3, 61)
        Me.lblVSP.Name = "lblVSP"
        Me.lblVSP.Size = New System.Drawing.Size(43, 18)
        Me.lblVSP.TabIndex = 60
        Me.lblVSP.Text = "Vendor"
        '
        'TxtMultiVendor
        '
        Me.TxtMultiVendor.arrDispalyMember = Nothing
        Me.TxtMultiVendor.arrValueMember = Nothing
        Me.TxtMultiVendor.Location = New System.Drawing.Point(72, 62)
        Me.TxtMultiVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiVendor.MyLinkLable1 = Nothing
        Me.TxtMultiVendor.MyLinkLable2 = Nothing
        Me.TxtMultiVendor.MyNullText = "All"
        Me.TxtMultiVendor.Name = "TxtMultiVendor"
        Me.TxtMultiVendor.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiVendor.TabIndex = 59
        '
        'lblMCC
        '
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Location = New System.Drawing.Point(3, 85)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(30, 18)
        Me.lblMCC.TabIndex = 58
        Me.lblMCC.Text = "MCC"
        '
        'TxtMultiMCC
        '
        Me.TxtMultiMCC.arrDispalyMember = Nothing
        Me.TxtMultiMCC.arrValueMember = Nothing
        Me.TxtMultiMCC.Location = New System.Drawing.Point(72, 86)
        Me.TxtMultiMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiMCC.MyLinkLable1 = Nothing
        Me.TxtMultiMCC.MyLinkLable2 = Nothing
        Me.TxtMultiMCC.MyNullText = "All"
        Me.TxtMultiMCC.Name = "TxtMultiMCC"
        Me.TxtMultiMCC.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiMCC.TabIndex = 57
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkAssetIssueWithPurchase)
        Me.RadGroupBox4.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox4.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(376, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(186, 54)
        Me.RadGroupBox4.TabIndex = 56
        '
        'chkAssetIssueWithPurchase
        '
        Me.chkAssetIssueWithPurchase.AutoSize = True
        Me.chkAssetIssueWithPurchase.Location = New System.Drawing.Point(14, 34)
        Me.chkAssetIssueWithPurchase.Name = "chkAssetIssueWithPurchase"
        Me.chkAssetIssueWithPurchase.Size = New System.Drawing.Size(159, 17)
        Me.chkAssetIssueWithPurchase.TabIndex = 308
        Me.chkAssetIssueWithPurchase.Text = "Asset Issue With Purchase"
        Me.chkAssetIssueWithPurchase.UseVisualStyleBackColor = True
        '
        'rbtnSummary
        '
        Me.rbtnSummary.Location = New System.Drawing.Point(13, 9)
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 306
        Me.rbtnSummary.Text = "Summary"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(86, 9)
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 307
        Me.rbtnDetail.Text = "Detail"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
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
        RadListDataItem1.Text = "MCC Item Wise"
        RadListDataItem2.Text = "Document and Item Wise"
        Me.cboType.Items.Add(RadListDataItem1)
        Me.cboType.Items.Add(RadListDataItem2)
        Me.cboType.Location = New System.Drawing.Point(692, 31)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.lblModeofTransport
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(244, 18)
        Me.cboType.TabIndex = 34
        Me.cboType.Visible = False
        '
        'lblModeofTransport
        '
        Me.lblModeofTransport.FieldName = Nothing
        Me.lblModeofTransport.Location = New System.Drawing.Point(614, 33)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(67, 18)
        Me.lblModeofTransport.TabIndex = 33
        Me.lblModeofTransport.Text = "Report Type"
        Me.lblModeofTransport.Visible = False
        '
        'GBItem
        '
        Me.GBItem.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GBItem.Controls.Add(Me.cbgItem)
        Me.GBItem.Controls.Add(Me.Panel1)
        Me.GBItem.HeaderText = "Item"
        Me.GBItem.Location = New System.Drawing.Point(775, 170)
        Me.GBItem.Name = "GBItem"
        Me.GBItem.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GBItem.Size = New System.Drawing.Size(222, 87)
        Me.GBItem.TabIndex = 6
        Me.GBItem.Text = "Item"
        Me.GBItem.Visible = False
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
        Me.cbgItem.Size = New System.Drawing.Size(202, 37)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.TabStop = False
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkItemSelect)
        Me.Panel1.Controls.Add(Me.chkItemAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(202, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(172, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(114, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgVSP)
        Me.RadGroupBox5.Controls.Add(Me.Panel4)
        Me.RadGroupBox5.HeaderText = "VSP"
        Me.RadGroupBox5.Location = New System.Drawing.Point(765, 52)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(358, 112)
        Me.RadGroupBox5.TabIndex = 5
        Me.RadGroupBox5.Text = "VSP"
        Me.RadGroupBox5.Visible = False
        '
        'cbgVSP
        '
        Me.cbgVSP.CheckedValue = Nothing
        Me.cbgVSP.DataSource = Nothing
        Me.cbgVSP.DisplayMember = "Name"
        Me.cbgVSP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVSP.Location = New System.Drawing.Point(10, 40)
        Me.cbgVSP.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVSP.MyShowHeadrText = False
        Me.cbgVSP.Name = "cbgVSP"
        Me.cbgVSP.Size = New System.Drawing.Size(338, 62)
        Me.cbgVSP.TabIndex = 1
        Me.cbgVSP.TabStop = False
        Me.cbgVSP.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkVSPSelect)
        Me.Panel4.Controls.Add(Me.chkVSPAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(338, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkVSPSelect
        '
        Me.chkVSPSelect.Location = New System.Drawing.Point(172, 1)
        Me.chkVSPSelect.MyLinkLable1 = Nothing
        Me.chkVSPSelect.MyLinkLable2 = Nothing
        Me.chkVSPSelect.Name = "chkVSPSelect"
        Me.chkVSPSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVSPSelect.TabIndex = 1
        Me.chkVSPSelect.Text = "Select"
        '
        'chkVSPAll
        '
        Me.chkVSPAll.Location = New System.Drawing.Point(114, 1)
        Me.chkVSPAll.MyLinkLable1 = Nothing
        Me.chkVSPAll.MyLinkLable2 = Nothing
        Me.chkVSPAll.Name = "chkVSPAll"
        Me.chkVSPAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVSPAll.TabIndex = 0
        Me.chkVSPAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgMCC)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "MCC"
        Me.RadGroupBox2.Location = New System.Drawing.Point(659, 98)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(300, 66)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "MCC"
        Me.RadGroupBox2.Visible = False
        '
        'cbgMCC
        '
        Me.cbgMCC.CheckedValue = Nothing
        Me.cbgMCC.DataSource = Nothing
        Me.cbgMCC.DisplayMember = "Name"
        Me.cbgMCC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMCC.Location = New System.Drawing.Point(10, 40)
        Me.cbgMCC.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMCC.MyShowHeadrText = False
        Me.cbgMCC.Name = "cbgMCC"
        Me.cbgMCC.Size = New System.Drawing.Size(280, 16)
        Me.cbgMCC.TabIndex = 1
        Me.cbgMCC.TabStop = False
        Me.cbgMCC.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkMCCSelect)
        Me.Panel2.Controls.Add(Me.chkMCCAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(280, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkMCCSelect
        '
        Me.chkMCCSelect.Location = New System.Drawing.Point(172, 1)
        Me.chkMCCSelect.MyLinkLable1 = Nothing
        Me.chkMCCSelect.MyLinkLable2 = Nothing
        Me.chkMCCSelect.Name = "chkMCCSelect"
        Me.chkMCCSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkMCCSelect.TabIndex = 1
        Me.chkMCCSelect.Text = "Select"
        '
        'chkMCCAll
        '
        Me.chkMCCAll.Location = New System.Drawing.Point(122, 1)
        Me.chkMCCAll.MyLinkLable1 = Nothing
        Me.chkMCCAll.MyLinkLable2 = Nothing
        Me.chkMCCAll.Name = "chkMCCAll"
        Me.chkMCCAll.Size = New System.Drawing.Size(33, 18)
        Me.chkMCCAll.TabIndex = 0
        Me.chkMCCAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(361, 43)
        Me.RadGroupBox1.TabIndex = 0
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
        Me.txtToDate.Location = New System.Drawing.Point(226, 12)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
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
        Me.txtFromDate.Location = New System.Drawing.Point(75, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(174, 13)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(9, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1221, 384)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1221, 384)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1069, 13)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(71, 22)
        Me.btnBack.TabIndex = 9
        Me.btnBack.Text = "Back"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(208, 13)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 6
        Me.RadSplitButton1.Text = "Export"
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
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(107, 13)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(95, 22)
        Me.BtnReset.TabIndex = 5
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1146, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 13)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(95, 22)
        Me.btnGo.TabIndex = 4
        Me.btnGo.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1242, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.lblSaveLayout, Me.lblDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'lblSaveLayout
        '
        Me.lblSaveLayout.AccessibleDescription = "Save Layout"
        Me.lblSaveLayout.AccessibleName = "Save Layout"
        Me.lblSaveLayout.Name = "lblSaveLayout"
        Me.lblSaveLayout.Text = "Save Layout"
        '
        'lblDeleteLayout
        '
        Me.lblDeleteLayout.AccessibleDescription = "DeleteLayout"
        Me.lblDeleteLayout.AccessibleName = "DeleteLayout"
        Me.lblDeleteLayout.Name = "lblDeleteLayout"
        Me.lblDeleteLayout.Text = "DeleteLayout"
        '
        'RptVSPAssetIssue1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1242, 503)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptVSPAssetIssue1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptVSPAssetIssue1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GBItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBItem.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkVSPSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVSPAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMCC As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkMCCSelect As common.Controls.MyRadioButton
    Friend WithEvents chkMCCAll As common.Controls.MyRadioButton
    Friend WithEvents GBItem As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVSP As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkVSPSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVSPAll As common.Controls.MyRadioButton
    Protected WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents TxtMultiItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblVSP As common.Controls.MyLabel
    Friend WithEvents TxtMultiVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents TxtMultiMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkAssetIssueWithPurchase As CheckBox
    Friend WithEvents chkCurrentStock As CheckBox
End Class

