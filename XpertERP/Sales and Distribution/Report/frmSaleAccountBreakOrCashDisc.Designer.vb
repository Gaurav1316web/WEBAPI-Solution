<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSaleAccountBreakOrCashDisc
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.RadLabel33 = New common.Controls.MyLabel
        Me.btnExportToExcel = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.rdbGross = New Telerik.WinControls.UI.RadCheckBox
        Me.rdbRoute = New Telerik.WinControls.UI.RadCheckBox
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbSalewithTargetQty = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummaryDoc = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbFlavour = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSku = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbPack = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbtarget = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbFoc = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbTradeDisc = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbPost = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkCustomerClass = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkClassSelect = New common.Controls.MyRadioButton
        Me.chkClassAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnCompanySelect = New common.Controls.MyRadioButton
        Me.rbtnCompanyAll = New common.Controls.MyRadioButton
        Me.Item = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem1 = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkItemSelect1 = New common.Controls.MyRadioButton
        Me.chkItemAll1 = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustGroup = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkGroupSelect = New common.Controls.MyRadioButton
        Me.chkGroupAll = New common.Controls.MyRadioButton
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.ddlType = New common.Controls.MyComboBox
        Me.dtpendtime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpStarttime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkChkSelect = New common.Controls.MyRadioButton
        Me.chkCustAll = New common.Controls.MyRadioButton
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbglocation = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chklocSelect = New common.Controls.MyRadioButton
        Me.chklocAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.rdbExcelVisalEffect = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.rdbGross, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        CType(Me.rdbSalewithTargetQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummaryDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rdbtarget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbTradeDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Item.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkChkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chklocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chklocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(703, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 24
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(82, 13)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 23
        Me.btnreset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(8, 13)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 22
        Me.btnPrint.Text = "Print"
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
        Me.cbgItem.Size = New System.Drawing.Size(325, 93)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkItemSelect)
        Me.Panel1.Controls.Add(Me.chkItemAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(325, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(156, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(105, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(45, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'RadLabel33
        '
        Me.RadLabel33.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel33.Location = New System.Drawing.Point(320, 83)
        Me.RadLabel33.Name = "RadLabel33"
        Me.RadLabel33.Size = New System.Drawing.Size(83, 18)
        Me.RadLabel33.TabIndex = 85
        Me.RadLabel33.Text = "Customer Class"
        Me.RadLabel33.Visible = False
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(360, 13)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(118, 18)
        Me.btnExportToExcel.TabIndex = 109
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(286, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 111
        Me.btnRefresh.Text = ">>>"
        Me.btnRefresh.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportToExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 641)
        Me.SplitContainer1.SplitterDistance = 603
        Me.SplitContainer1.TabIndex = 112
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(7, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(906, 605)
        Me.RadPageView1.TabIndex = 109
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.rdbGross)
        Me.RadPageViewPage1.Controls.Add(Me.rdbRoute)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.Item)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.ddlType)
        Me.RadPageViewPage1.Controls.Add(Me.dtpendtime)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.dtpStarttime)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.dtpstart)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.dtpend)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(885, 557)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'rdbGross
        '
        Me.rdbGross.Location = New System.Drawing.Point(646, 8)
        Me.rdbGross.Name = "rdbGross"
        Me.rdbGross.Size = New System.Drawing.Size(75, 18)
        Me.rdbGross.TabIndex = 115
        Me.rdbGross.Text = "Gross Wise"
        Me.rdbGross.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbRoute
        '
        Me.rdbRoute.Location = New System.Drawing.Point(528, 7)
        Me.rdbRoute.Name = "rdbRoute"
        Me.rdbRoute.Size = New System.Drawing.Size(75, 18)
        Me.rdbRoute.TabIndex = 114
        Me.rdbRoute.Text = "Route wise"
        Me.rdbRoute.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox9.Controls.Add(Me.rdbSalewithTargetQty)
        Me.RadGroupBox9.Controls.Add(Me.rdbSummaryDoc)
        Me.RadGroupBox9.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox9.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox9.HeaderText = "Select"
        Me.RadGroupBox9.Location = New System.Drawing.Point(8, 39)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(595, 37)
        Me.RadGroupBox9.TabIndex = 113
        Me.RadGroupBox9.Text = "Select"
        '
        'rdbSalewithTargetQty
        '
        Me.rdbSalewithTargetQty.Location = New System.Drawing.Point(377, 12)
        Me.rdbSalewithTargetQty.Name = "rdbSalewithTargetQty"
        Me.rdbSalewithTargetQty.Size = New System.Drawing.Size(197, 18)
        Me.rdbSalewithTargetQty.TabIndex = 105
        Me.rdbSalewithTargetQty.Text = "Sale w/o Trade and with Target Qty"
        '
        'rdbSummaryDoc
        '
        Me.rdbSummaryDoc.Location = New System.Drawing.Point(190, 11)
        Me.rdbSummaryDoc.Name = "rdbSummaryDoc"
        Me.rdbSummaryDoc.Size = New System.Drawing.Size(147, 18)
        Me.rdbSummaryDoc.TabIndex = 104
        Me.rdbSummaryDoc.Text = "Summary Document wise"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(13, 12)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 103
        Me.rdbSummary.Text = "Summary"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(105, 12)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 0
        Me.rdbDetail.Text = "Detail"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox7.Controls.Add(Me.rdbFlavour)
        Me.RadGroupBox7.Controls.Add(Me.rdbSku)
        Me.RadGroupBox7.Controls.Add(Me.rdbPack)
        Me.RadGroupBox7.HeaderText = "Select"
        Me.RadGroupBox7.Location = New System.Drawing.Point(9, 82)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(495, 37)
        Me.RadGroupBox7.TabIndex = 112
        Me.RadGroupBox7.Text = "Select"
        '
        'rdbFlavour
        '
        Me.rdbFlavour.Location = New System.Drawing.Point(189, 9)
        Me.rdbFlavour.Name = "rdbFlavour"
        Me.rdbFlavour.Size = New System.Drawing.Size(84, 18)
        Me.rdbFlavour.TabIndex = 106
        Me.rdbFlavour.Text = "Flavour Wise"
        '
        'rdbSku
        '
        Me.rdbSku.Location = New System.Drawing.Point(13, 9)
        Me.rdbSku.Name = "rdbSku"
        Me.rdbSku.Size = New System.Drawing.Size(68, 18)
        Me.rdbSku.TabIndex = 104
        Me.rdbSku.Text = "SKU Wise"
        '
        'rdbPack
        '
        Me.rdbPack.Location = New System.Drawing.Point(104, 9)
        Me.rdbPack.Name = "rdbPack"
        Me.rdbPack.Size = New System.Drawing.Size(70, 18)
        Me.rdbPack.TabIndex = 105
        Me.rdbPack.Text = "Pack Wise"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox5.Controls.Add(Me.rdbtarget)
        Me.RadGroupBox5.Controls.Add(Me.rdbFoc)
        Me.RadGroupBox5.Controls.Add(Me.rdbTradeDisc)
        Me.RadGroupBox5.HeaderText = "Select"
        Me.RadGroupBox5.Location = New System.Drawing.Point(509, 82)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(362, 37)
        Me.RadGroupBox5.TabIndex = 111
        Me.RadGroupBox5.Text = "Select"
        '
        'rdbtarget
        '
        Me.rdbtarget.Location = New System.Drawing.Point(241, 9)
        Me.rdbtarget.Name = "rdbtarget"
        Me.rdbtarget.Size = New System.Drawing.Size(52, 18)
        Me.rdbtarget.TabIndex = 106
        Me.rdbtarget.Text = "Target"
        '
        'rdbFoc
        '
        Me.rdbFoc.Location = New System.Drawing.Point(142, 9)
        Me.rdbFoc.Name = "rdbFoc"
        Me.rdbFoc.Size = New System.Drawing.Size(57, 18)
        Me.rdbFoc.TabIndex = 105
        Me.rdbFoc.Text = "All FOC"
        '
        'rdbTradeDisc
        '
        Me.rdbTradeDisc.Location = New System.Drawing.Point(13, 9)
        Me.rdbTradeDisc.Name = "rdbTradeDisc"
        Me.rdbTradeDisc.Size = New System.Drawing.Size(96, 18)
        Me.rdbTradeDisc.TabIndex = 105
        Me.rdbTradeDisc.Text = "Trade Discount"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox4.Controls.Add(Me.rdbPost)
        Me.RadGroupBox4.Controls.Add(Me.rdbAll)
        Me.RadGroupBox4.HeaderText = "Select"
        Me.RadGroupBox4.Location = New System.Drawing.Point(625, 39)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(246, 37)
        Me.RadGroupBox4.TabIndex = 110
        Me.RadGroupBox4.Text = "Select"
        '
        'rdbPost
        '
        Me.rdbPost.Location = New System.Drawing.Point(141, 11)
        Me.rdbPost.Name = "rdbPost"
        Me.rdbPost.Size = New System.Drawing.Size(54, 18)
        Me.rdbPost.TabIndex = 105
        Me.rdbPost.Text = "Posted"
        '
        'rdbAll
        '
        Me.rdbAll.Location = New System.Drawing.Point(12, 12)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbAll.TabIndex = 105
        Me.rdbAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkCustomerClass)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Customer Class"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 412)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(367, 139)
        Me.RadGroupBox2.TabIndex = 106
        Me.RadGroupBox2.Text = "Customer Class"
        Me.RadGroupBox2.Visible = False
        '
        'chkCustomerClass
        '
        Me.chkCustomerClass.CheckedValue = Nothing
        Me.chkCustomerClass.DataSource = Nothing
        Me.chkCustomerClass.DisplayMember = "Name"
        Me.chkCustomerClass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkCustomerClass.Location = New System.Drawing.Point(10, 40)
        Me.chkCustomerClass.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chkCustomerClass.MyShowHeadrText = False
        Me.chkCustomerClass.Name = "chkCustomerClass"
        Me.chkCustomerClass.Size = New System.Drawing.Size(347, 89)
        Me.chkCustomerClass.TabIndex = 1
        Me.chkCustomerClass.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkClassSelect)
        Me.Panel3.Controls.Add(Me.chkClassAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(347, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkClassSelect
        '
        Me.chkClassSelect.Location = New System.Drawing.Point(182, 1)
        Me.chkClassSelect.MyLinkLable1 = Nothing
        Me.chkClassSelect.MyLinkLable2 = Nothing
        Me.chkClassSelect.Name = "chkClassSelect"
        Me.chkClassSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkClassSelect.TabIndex = 1
        Me.chkClassSelect.Text = "Select"
        '
        'chkClassAll
        '
        Me.chkClassAll.Location = New System.Drawing.Point(131, 1)
        Me.chkClassAll.MyLinkLable1 = Nothing
        Me.chkClassAll.MyLinkLable2 = Nothing
        Me.chkClassAll.Name = "chkClassAll"
        Me.chkClassAll.Size = New System.Drawing.Size(33, 18)
        Me.chkClassAll.TabIndex = 0
        Me.chkClassAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox3.Controls.Add(Me.Panel7)
        Me.RadGroupBox3.HeaderText = "Company"
        Me.RadGroupBox3.Location = New System.Drawing.Point(5, 263)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(361, 141)
        Me.RadGroupBox3.TabIndex = 49
        Me.RadGroupBox3.Text = "Company"
        '
        'cbgCompany
        '
        Me.cbgCompany.CheckedValue = Nothing
        Me.cbgCompany.DataSource = Nothing
        Me.cbgCompany.DisplayMember = "Name"
        Me.cbgCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCompany.Location = New System.Drawing.Point(10, 40)
        Me.cbgCompany.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCompany.MyShowHeadrText = False
        Me.cbgCompany.Name = "cbgCompany"
        Me.cbgCompany.Size = New System.Drawing.Size(341, 91)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnCompanySelect)
        Me.Panel7.Controls.Add(Me.rbtnCompanyAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(341, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnCompanySelect
        '
        Me.rbtnCompanySelect.Location = New System.Drawing.Point(117, 1)
        Me.rbtnCompanySelect.MyLinkLable1 = Nothing
        Me.rbtnCompanySelect.MyLinkLable2 = Nothing
        Me.rbtnCompanySelect.Name = "rbtnCompanySelect"
        Me.rbtnCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCompanySelect.TabIndex = 1
        Me.rbtnCompanySelect.Text = "Select"
        '
        'rbtnCompanyAll
        '
        Me.rbtnCompanyAll.Location = New System.Drawing.Point(69, 1)
        Me.rbtnCompanyAll.MyLinkLable1 = Nothing
        Me.rbtnCompanyAll.MyLinkLable2 = Nothing
        Me.rbtnCompanyAll.Name = "rbtnCompanyAll"
        Me.rbtnCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCompanyAll.TabIndex = 0
        Me.rbtnCompanyAll.Text = "All"
        '
        'Item
        '
        Me.Item.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Item.Controls.Add(Me.cbgItem1)
        Me.Item.Controls.Add(Me.Panel2)
        Me.Item.HeaderText = "Item"
        Me.Item.Location = New System.Drawing.Point(383, 408)
        Me.Item.Name = "Item"
        Me.Item.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Item.Size = New System.Drawing.Size(354, 143)
        Me.Item.TabIndex = 49
        Me.Item.Text = "Item"
        Me.Item.Visible = False
        '
        'cbgItem1
        '
        Me.cbgItem1.CheckedValue = Nothing
        Me.cbgItem1.DataSource = Nothing
        Me.cbgItem1.DisplayMember = "Name"
        Me.cbgItem1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem1.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem1.MyShowHeadrText = False
        Me.cbgItem1.Name = "cbgItem1"
        Me.cbgItem1.Size = New System.Drawing.Size(334, 93)
        Me.cbgItem1.TabIndex = 1
        Me.cbgItem1.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItemSelect1)
        Me.Panel2.Controls.Add(Me.chkItemAll1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(334, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkItemSelect1
        '
        Me.chkItemSelect1.Location = New System.Drawing.Point(182, 1)
        Me.chkItemSelect1.MyLinkLable1 = Nothing
        Me.chkItemSelect1.MyLinkLable2 = Nothing
        Me.chkItemSelect1.Name = "chkItemSelect1"
        Me.chkItemSelect1.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect1.TabIndex = 1
        Me.chkItemSelect1.Text = "Select"
        '
        'chkItemAll1
        '
        Me.chkItemAll1.Location = New System.Drawing.Point(131, 1)
        Me.chkItemAll1.MyLinkLable1 = Nothing
        Me.chkItemAll1.MyLinkLable2 = Nothing
        Me.chkItemAll1.Name = "chkItemAll1"
        Me.chkItemAll1.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll1.TabIndex = 0
        Me.chkItemAll1.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgCustGroup)
        Me.RadGroupBox1.Controls.Add(Me.Panel5)
        Me.RadGroupBox1.HeaderText = "Customer Group"
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 118)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(357, 139)
        Me.RadGroupBox1.TabIndex = 16
        Me.RadGroupBox1.Text = "Customer Group"
        '
        'cbgCustGroup
        '
        Me.cbgCustGroup.CheckedValue = Nothing
        Me.cbgCustGroup.DataSource = Nothing
        Me.cbgCustGroup.DisplayMember = "Name"
        Me.cbgCustGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustGroup.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustGroup.MyShowHeadrText = False
        Me.cbgCustGroup.Name = "cbgCustGroup"
        Me.cbgCustGroup.Size = New System.Drawing.Size(337, 89)
        Me.cbgCustGroup.TabIndex = 1
        Me.cbgCustGroup.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkGroupSelect)
        Me.Panel5.Controls.Add(Me.chkGroupAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(337, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkGroupSelect
        '
        Me.chkGroupSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkGroupSelect.MyLinkLable1 = Nothing
        Me.chkGroupSelect.MyLinkLable2 = Nothing
        Me.chkGroupSelect.Name = "chkGroupSelect"
        Me.chkGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkGroupSelect.TabIndex = 1
        Me.chkGroupSelect.Text = "Select"
        '
        'chkGroupAll
        '
        Me.chkGroupAll.Location = New System.Drawing.Point(141, 1)
        Me.chkGroupAll.MyLinkLable1 = Nothing
        Me.chkGroupAll.MyLinkLable2 = Nothing
        Me.chkGroupAll.Name = "chkGroupAll"
        Me.chkGroupAll.Size = New System.Drawing.Size(33, 18)
        Me.chkGroupAll.TabIndex = 0
        Me.chkGroupAll.Text = "All"
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(326, 5)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel5.TabIndex = 20
        Me.RadLabel5.Text = "Report Type"
        '
        'ddlType
        '
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Tag = "Q"
        RadListDataItem1.Text = "Quantity"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Tag = "V"
        RadListDataItem2.Text = "Value"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Both"
        RadListDataItem3.TextWrap = True
        Me.ddlType.Items.Add(RadListDataItem1)
        Me.ddlType.Items.Add(RadListDataItem2)
        Me.ddlType.Items.Add(RadListDataItem3)
        Me.ddlType.Location = New System.Drawing.Point(399, 7)
        Me.ddlType.MendatroryField = False
        Me.ddlType.MyLinkLable1 = Nothing
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(97, 18)
        Me.ddlType.TabIndex = 20
        '
        'dtpendtime
        '
        Me.dtpendtime.CustomFormat = "HH:mm tt"
        Me.dtpendtime.Location = New System.Drawing.Point(758, 224)
        Me.dtpendtime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Name = "dtpendtime"
        Me.dtpendtime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Size = New System.Drawing.Size(25, 20)
        Me.dtpendtime.TabIndex = 28
        Me.dtpendtime.TabStop = False
        Me.dtpendtime.Text = "Wednesday, November 16, 2011"
        Me.dtpendtime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        Me.dtpendtime.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(8, 139)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(57, 18)
        Me.RadLabel1.TabIndex = 25
        Me.RadLabel1.Text = "Start Time"
        Me.RadLabel1.Visible = False
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(5, 5)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel7.TabIndex = 11
        Me.RadLabel7.Text = "Start Date"
        '
        'dtpStarttime
        '
        Me.dtpStarttime.CustomFormat = "HH:mm tt"
        Me.dtpStarttime.Location = New System.Drawing.Point(79, 139)
        Me.dtpStarttime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Name = "dtpStarttime"
        Me.dtpStarttime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Size = New System.Drawing.Size(134, 20)
        Me.dtpStarttime.TabIndex = 27
        Me.dtpStarttime.TabStop = False
        Me.dtpStarttime.Text = "Wednesday, November 16, 2011"
        Me.dtpStarttime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        Me.dtpStarttime.Visible = False
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(171, 7)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "End Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(238, 141)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel2.TabIndex = 26
        Me.RadLabel2.Text = "End Time"
        Me.RadLabel2.Visible = False
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(76, 5)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(86, 20)
        Me.dtpstart.TabIndex = 13
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Customer"
        Me.RadGroupBox6.Location = New System.Drawing.Point(373, 118)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(358, 139)
        Me.RadGroupBox6.TabIndex = 17
        Me.RadGroupBox6.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(338, 89)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkChkSelect)
        Me.Panel4.Controls.Add(Me.chkCustAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(338, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkChkSelect
        '
        Me.chkChkSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkChkSelect.MyLinkLable1 = Nothing
        Me.chkChkSelect.MyLinkLable2 = Nothing
        Me.chkChkSelect.Name = "chkChkSelect"
        Me.chkChkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkChkSelect.TabIndex = 1
        Me.chkChkSelect.Text = "Select"
        '
        'chkCustAll
        '
        Me.chkCustAll.Location = New System.Drawing.Point(141, 1)
        Me.chkCustAll.MyLinkLable1 = Nothing
        Me.chkCustAll.MyLinkLable2 = Nothing
        Me.chkCustAll.Name = "chkCustAll"
        Me.chkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustAll.TabIndex = 0
        Me.chkCustAll.Text = "All"
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(238, 5)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(82, 20)
        Me.dtpend.TabIndex = 14
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbglocation)
        Me.RadGroupBox8.Controls.Add(Me.Panel6)
        Me.RadGroupBox8.HeaderText = "Location"
        Me.RadGroupBox8.Location = New System.Drawing.Point(377, 263)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(357, 139)
        Me.RadGroupBox8.TabIndex = 15
        Me.RadGroupBox8.Text = "Location"
        '
        'cbglocation
        '
        Me.cbglocation.CheckedValue = Nothing
        Me.cbglocation.DataSource = Nothing
        Me.cbglocation.DisplayMember = "Name"
        Me.cbglocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbglocation.Location = New System.Drawing.Point(10, 40)
        Me.cbglocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbglocation.MyShowHeadrText = False
        Me.cbglocation.Name = "cbglocation"
        Me.cbglocation.Size = New System.Drawing.Size(337, 89)
        Me.cbglocation.TabIndex = 1
        Me.cbglocation.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chklocSelect)
        Me.Panel6.Controls.Add(Me.chklocAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(337, 20)
        Me.Panel6.TabIndex = 0
        '
        'chklocSelect
        '
        Me.chklocSelect.Location = New System.Drawing.Point(192, 1)
        Me.chklocSelect.MyLinkLable1 = Nothing
        Me.chklocSelect.MyLinkLable2 = Nothing
        Me.chklocSelect.Name = "chklocSelect"
        Me.chklocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chklocSelect.TabIndex = 1
        Me.chklocSelect.Text = "Select"
        '
        'chklocAll
        '
        Me.chklocAll.Location = New System.Drawing.Point(141, 1)
        Me.chklocAll.MyLinkLable1 = Nothing
        Me.chklocAll.MyLinkLable2 = Nothing
        Me.chklocAll.Name = "chklocAll"
        Me.chklocAll.Size = New System.Drawing.Size(33, 18)
        Me.chklocAll.TabIndex = 0
        Me.chklocAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(745, 472)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.GV1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.GV1.ForeColor = System.Drawing.Color.Black
        Me.GV1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.MasterTemplate.EnableFiltering = True
        Me.GV1.Name = "GV1"
        Me.GV1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GV1.ShowGroupPanel = False
        Me.GV1.Size = New System.Drawing.Size(745, 472)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF, Me.rdbExcelVisalEffect})
        Me.btnExport.Location = New System.Drawing.Point(156, 13)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 126
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
        'rdbExcelVisalEffect
        '
        Me.rdbExcelVisalEffect.AccessibleDescription = "RadMenuItem1"
        Me.rdbExcelVisalEffect.AccessibleName = "RadMenuItem1"
        Me.rdbExcelVisalEffect.Image = Global.ERP.My.Resources.Resources.MSE
        Me.rdbExcelVisalEffect.Name = "rdbExcelVisalEffect"
        Me.rdbExcelVisalEffect.Text = "Excel with Visual Effects"
        Me.rdbExcelVisalEffect.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmSaleAccountBreakOrCashDisc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 641)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSaleAccountBreakOrCashDisc"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Trade Discount Report"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.rdbGross, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.RadGroupBox9.PerformLayout()
        CType(Me.rdbSalewithTargetQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummaryDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rdbtarget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbTradeDisc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Item.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkChkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chklocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chklocAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel33 As common.Controls.MyLabel
    Friend WithEvents btnExportToExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbPost As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents Item As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem1 As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect1 As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll1 As common.Controls.MyRadioButton
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustGroup As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents chkGroupAll As common.Controls.MyRadioButton
    Friend WithEvents rdbFlavour As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbPack As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSku As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkCustomerClass As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkClassSelect As common.Controls.MyRadioButton
    Friend WithEvents chkClassAll As common.Controls.MyRadioButton
    Friend WithEvents dtpendtime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpStarttime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkChkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustAll As common.Controls.MyRadioButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbglocation As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chklocSelect As common.Controls.MyRadioButton
    Friend WithEvents chklocAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbFoc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbTradeDisc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdbtarget As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbSummaryDoc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbRoute As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdbGross As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdbSalewithTargetQty As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbExcelVisalEffect As Telerik.WinControls.UI.RadMenuItem
End Class

