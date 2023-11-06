<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptItemConsumptionReport
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chk_stockingunit = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtBOMNo = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gbxReports = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.ddlReportsType = New Telerik.WinControls.UI.RadDropDownList()
        Me.TxtMultiLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtBatchNoMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblBatchNo = New common.Controls.MyLabel()
        Me.txtSectionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblSection = New common.Controls.MyLabel()
        Me.txtItemMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblItem = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chk_stockingunit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbxReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxReports.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReportsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(691, 352)
        Me.SplitContainer1.SplitterDistance = 319
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(691, 299)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chk_stockingunit)
        Me.RadPageViewPage1.Controls.Add(Me.txtBOMNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.gbxReports)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBatchNoMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblBatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtSectionMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblSection)
        Me.RadPageViewPage1.Controls.Add(Me.txtItemMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblItem)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(670, 251)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chk_stockingunit
        '
        Me.chk_stockingunit.Location = New System.Drawing.Point(412, 63)
        Me.chk_stockingunit.Name = "chk_stockingunit"
        Me.chk_stockingunit.Size = New System.Drawing.Size(87, 18)
        Me.chk_stockingunit.TabIndex = 313
        Me.chk_stockingunit.Text = "Stocking Unit"
        '
        'txtBOMNo
        '
        Me.txtBOMNo.arrDispalyMember = Nothing
        Me.txtBOMNo.arrValueMember = Nothing
        Me.txtBOMNo.Location = New System.Drawing.Point(67, 159)
        Me.txtBOMNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBOMNo.MyLinkLable1 = Nothing
        Me.txtBOMNo.MyLinkLable2 = Nothing
        Me.txtBOMNo.MyNullText = "All"
        Me.txtBOMNo.Name = "txtBOMNo"
        Me.txtBOMNo.Size = New System.Drawing.Size(338, 19)
        Me.txtBOMNo.TabIndex = 25
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(2, 159)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 18)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "BOM"
        '
        'gbxReports
        '
        Me.gbxReports.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbxReports.Controls.Add(Me.MyLabel2)
        Me.gbxReports.Controls.Add(Me.ddlReportsType)
        Me.gbxReports.HeaderText = ""
        Me.gbxReports.Location = New System.Drawing.Point(329, 3)
        Me.gbxReports.Name = "gbxReports"
        Me.gbxReports.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbxReports.Size = New System.Drawing.Size(218, 44)
        Me.gbxReports.TabIndex = 23
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(10, 13)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "Report Type"
        '
        'ddlReportsType
        '
        Me.ddlReportsType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Tag = ""
        RadListDataItem1.Text = "Batch-Wise"
        RadListDataItem1.TextWrap = False
        RadListDataItem2.Tag = ""
        RadListDataItem2.Text = "BOM-Wise"
        RadListDataItem2.TextWrap = False
        RadListDataItem3.Text = "Item-Wise"
        Me.ddlReportsType.Items.Add(RadListDataItem1)
        Me.ddlReportsType.Items.Add(RadListDataItem2)
        Me.ddlReportsType.Items.Add(RadListDataItem3)
        Me.ddlReportsType.Location = New System.Drawing.Point(83, 12)
        Me.ddlReportsType.Name = "ddlReportsType"
        Me.ddlReportsType.Size = New System.Drawing.Size(125, 20)
        Me.ddlReportsType.TabIndex = 22
        CType(Me.ddlReportsType.GetChildAt(0), Telerik.WinControls.UI.RadDropDownListElement).DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        '
        'TxtMultiLocation
        '
        Me.TxtMultiLocation.arrDispalyMember = Nothing
        Me.TxtMultiLocation.arrValueMember = Nothing
        Me.TxtMultiLocation.Location = New System.Drawing.Point(68, 135)
        Me.TxtMultiLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiLocation.MyLinkLable1 = Nothing
        Me.TxtMultiLocation.MyLinkLable2 = Nothing
        Me.TxtMultiLocation.MyNullText = "All"
        Me.TxtMultiLocation.Name = "TxtMultiLocation"
        Me.TxtMultiLocation.Size = New System.Drawing.Size(338, 19)
        Me.TxtMultiLocation.TabIndex = 21
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 135)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 20
        Me.lblLocation.Text = "Location"
        '
        'txtBatchNoMult
        '
        Me.txtBatchNoMult.arrDispalyMember = Nothing
        Me.txtBatchNoMult.arrValueMember = Nothing
        Me.txtBatchNoMult.Location = New System.Drawing.Point(68, 110)
        Me.txtBatchNoMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNoMult.MyLinkLable1 = Nothing
        Me.txtBatchNoMult.MyLinkLable2 = Nothing
        Me.txtBatchNoMult.MyNullText = "All"
        Me.txtBatchNoMult.Name = "txtBatchNoMult"
        Me.txtBatchNoMult.Size = New System.Drawing.Size(338, 19)
        Me.txtBatchNoMult.TabIndex = 19
        '
        'lblBatchNo
        '
        Me.lblBatchNo.FieldName = Nothing
        Me.lblBatchNo.Location = New System.Drawing.Point(3, 110)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(55, 18)
        Me.lblBatchNo.TabIndex = 18
        Me.lblBatchNo.Text = "Batch No."
        '
        'txtSectionMult
        '
        Me.txtSectionMult.arrDispalyMember = Nothing
        Me.txtSectionMult.arrValueMember = Nothing
        Me.txtSectionMult.Location = New System.Drawing.Point(68, 86)
        Me.txtSectionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSectionMult.MyLinkLable1 = Nothing
        Me.txtSectionMult.MyLinkLable2 = Nothing
        Me.txtSectionMult.MyNullText = "All"
        Me.txtSectionMult.Name = "txtSectionMult"
        Me.txtSectionMult.Size = New System.Drawing.Size(338, 19)
        Me.txtSectionMult.TabIndex = 17
        '
        'lblSection
        '
        Me.lblSection.FieldName = Nothing
        Me.lblSection.Location = New System.Drawing.Point(3, 86)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(43, 18)
        Me.lblSection.TabIndex = 16
        Me.lblSection.Text = "Section"
        '
        'txtItemMult
        '
        Me.txtItemMult.arrDispalyMember = Nothing
        Me.txtItemMult.arrValueMember = Nothing
        Me.txtItemMult.Location = New System.Drawing.Point(68, 62)
        Me.txtItemMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemMult.MyLinkLable1 = Nothing
        Me.txtItemMult.MyLinkLable2 = Nothing
        Me.txtItemMult.MyNullText = "All"
        Me.txtItemMult.Name = "txtItemMult"
        Me.txtItemMult.Size = New System.Drawing.Size(338, 19)
        Me.txtItemMult.TabIndex = 15
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Location = New System.Drawing.Point(3, 62)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 14
        Me.lblItem.Text = "Item"
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
        Me.RadGroupBox1.Size = New System.Drawing.Size(325, 44)
        Me.RadGroupBox1.TabIndex = 2
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
        Me.txtToDate.Location = New System.Drawing.Point(227, 12)
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
        Me.txtFromDate.Location = New System.Drawing.Point(88, 12)
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
        Me.lblToDate.Location = New System.Drawing.Point(176, 14)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(15, 13)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(670, 266)
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
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(670, 266)
        Me.gv1.TabIndex = 5
        Me.gv1.Text = "gv"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(691, 20)
        Me.RadMenu1.TabIndex = 3
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
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(128, 6)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 17)
        Me.btnSplitExport.TabIndex = 37
        Me.btnSplitExport.Text = "Export"
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
        Me.BtnReset.Location = New System.Drawing.Point(64, 6)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(62, 17)
        Me.BtnReset.TabIndex = 32
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(614, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 17)
        Me.btnclose.TabIndex = 34
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(7, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 17)
        Me.btnGo.TabIndex = 31
        Me.btnGo.Text = ">>"
        '
        'RptItemConsumptionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 352)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptItemConsumptionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Items Consumption Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chk_stockingunit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbxReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxReports.ResumeLayout(False)
        Me.gbxReports.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReportsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents txtBatchNoMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblBatchNo As common.Controls.MyLabel
    Friend WithEvents txtSectionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblSection As common.Controls.MyLabel
    Friend WithEvents txtItemMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents TxtMultiLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents gbxReports As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtBOMNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlReportsType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents chk_stockingunit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSplitExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

