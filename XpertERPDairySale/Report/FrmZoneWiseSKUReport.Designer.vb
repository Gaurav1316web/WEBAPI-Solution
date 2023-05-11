<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmZoneWiseSKUReport
    Inherits XpertERPEngine.FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkDetailReport = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtZone = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.gbDocStatus = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbOnlySchemeAndSample = New System.Windows.Forms.RadioButton()
        Me.rdbWithoutSchemeAndSample = New System.Windows.Forms.RadioButton()
        Me.rdbWithSchemeAndSample = New System.Windows.Forms.RadioButton()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ddlDocType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblDocType = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtn_DateWise = New System.Windows.Forms.RadioButton()
        Me.rbtn_YearWise = New System.Windows.Forms.RadioButton()
        Me.rbtn_MonthWise = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvData = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.gbReturn = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbSalesReturn = New System.Windows.Forms.RadioButton()
        Me.rdbSalesAndSalesReturn = New System.Windows.Forms.RadioButton()
        Me.rdbSales = New System.Windows.Forms.RadioButton()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkDetailReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDocStatus.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbReturn.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1044, 495)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1044, 20)
        Me.RadMenu1.TabIndex = 18
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(1044, 466)
        Me.SplitContainer1.SplitterDistance = 428
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1044, 428)
        Me.RadPageView1.TabIndex = 72
        Me.RadPageView1.Text = "RadPageView2"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gbReturn)
        Me.RadPageViewPage1.Controls.Add(Me.chkDetailReport)
        Me.RadPageViewPage1.Controls.Add(Me.txtZone)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.gbDocStatus)
        Me.RadPageViewPage1.Controls.Add(Me.txtUOM)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.ddlDocType)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1023, 380)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'chkDetailReport
        '
        Me.chkDetailReport.Location = New System.Drawing.Point(747, 16)
        Me.chkDetailReport.Name = "chkDetailReport"
        Me.chkDetailReport.Size = New System.Drawing.Size(86, 18)
        Me.chkDetailReport.TabIndex = 426
        Me.chkDetailReport.Text = "Detail Report"
        '
        'txtZone
        '
        Me.txtZone.arrDispalyMember = Nothing
        Me.txtZone.arrValueMember = Nothing
        Me.txtZone.Location = New System.Drawing.Point(112, 78)
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Me.MyLabel5
        Me.txtZone.MyLinkLable2 = Nothing
        Me.txtZone.MyNullText = "All"
        Me.txtZone.Name = "txtZone"
        Me.txtZone.Size = New System.Drawing.Size(214, 19)
        Me.txtZone.TabIndex = 406
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(21, 79)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel5.TabIndex = 405
        Me.MyLabel5.Text = "Zone"
        '
        'gbDocStatus
        '
        Me.gbDocStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbDocStatus.Controls.Add(Me.rdbOnlySchemeAndSample)
        Me.gbDocStatus.Controls.Add(Me.rdbWithoutSchemeAndSample)
        Me.gbDocStatus.Controls.Add(Me.rdbWithSchemeAndSample)
        Me.gbDocStatus.HeaderText = "Scheme And Sample"
        Me.gbDocStatus.Location = New System.Drawing.Point(348, 1)
        Me.gbDocStatus.Name = "gbDocStatus"
        Me.gbDocStatus.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbDocStatus.Size = New System.Drawing.Size(189, 84)
        Me.gbDocStatus.TabIndex = 404
        Me.gbDocStatus.Text = "Scheme And Sample"
        '
        'rdbOnlySchemeAndSample
        '
        Me.rdbOnlySchemeAndSample.AutoSize = True
        Me.rdbOnlySchemeAndSample.Location = New System.Drawing.Point(13, 62)
        Me.rdbOnlySchemeAndSample.Name = "rdbOnlySchemeAndSample"
        Me.rdbOnlySchemeAndSample.Size = New System.Drawing.Size(155, 17)
        Me.rdbOnlySchemeAndSample.TabIndex = 6
        Me.rdbOnlySchemeAndSample.Text = "Only Scheme And Sample"
        Me.rdbOnlySchemeAndSample.UseVisualStyleBackColor = True
        '
        'rdbWithoutSchemeAndSample
        '
        Me.rdbWithoutSchemeAndSample.AutoSize = True
        Me.rdbWithoutSchemeAndSample.Location = New System.Drawing.Point(13, 41)
        Me.rdbWithoutSchemeAndSample.Name = "rdbWithoutSchemeAndSample"
        Me.rdbWithoutSchemeAndSample.Size = New System.Drawing.Size(174, 17)
        Me.rdbWithoutSchemeAndSample.TabIndex = 5
        Me.rdbWithoutSchemeAndSample.Text = "Without Scheme And Sample"
        Me.rdbWithoutSchemeAndSample.UseVisualStyleBackColor = True
        '
        'rdbWithSchemeAndSample
        '
        Me.rdbWithSchemeAndSample.AutoSize = True
        Me.rdbWithSchemeAndSample.Checked = True
        Me.rdbWithSchemeAndSample.Location = New System.Drawing.Point(13, 20)
        Me.rdbWithSchemeAndSample.Name = "rdbWithSchemeAndSample"
        Me.rdbWithSchemeAndSample.Size = New System.Drawing.Size(156, 17)
        Me.rdbWithSchemeAndSample.TabIndex = 4
        Me.rdbWithSchemeAndSample.TabStop = True
        Me.rdbWithSchemeAndSample.Text = "With Scheme And Sample"
        Me.rdbWithSchemeAndSample.UseVisualStyleBackColor = True
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(112, 53)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.MyLabel1
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(214, 19)
        Me.txtUOM.TabIndex = 401
        Me.txtUOM.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(21, 54)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 400
        Me.MyLabel1.Text = "UOM"
        '
        'ddlDocType
        '
        Me.ddlDocType.AutoCompleteDisplayMember = Nothing
        Me.ddlDocType.AutoCompleteValueMember = Nothing
        Me.ddlDocType.Location = New System.Drawing.Point(112, 103)
        Me.ddlDocType.Name = "ddlDocType"
        Me.ddlDocType.Size = New System.Drawing.Size(214, 20)
        Me.ddlDocType.TabIndex = 398
        '
        'lblDocType
        '
        Me.lblDocType.FieldName = Nothing
        Me.lblDocType.Location = New System.Drawing.Point(21, 103)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(69, 18)
        Me.lblDocType.TabIndex = 397
        Me.lblDocType.Text = "Invoice Type"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtn_DateWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtn_YearWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtn_MonthWise)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(348, 91)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(272, 32)
        Me.RadGroupBox2.TabIndex = 340
        '
        'rbtn_DateWise
        '
        Me.rbtn_DateWise.AutoSize = True
        Me.rbtn_DateWise.Checked = True
        Me.rbtn_DateWise.Location = New System.Drawing.Point(13, 8)
        Me.rbtn_DateWise.Name = "rbtn_DateWise"
        Me.rbtn_DateWise.Size = New System.Drawing.Size(77, 17)
        Me.rbtn_DateWise.TabIndex = 340
        Me.rbtn_DateWise.TabStop = True
        Me.rbtn_DateWise.Text = "Date Wise"
        Me.rbtn_DateWise.UseVisualStyleBackColor = True
        '
        'rbtn_YearWise
        '
        Me.rbtn_YearWise.AutoSize = True
        Me.rbtn_YearWise.Location = New System.Drawing.Point(190, 8)
        Me.rbtn_YearWise.Name = "rbtn_YearWise"
        Me.rbtn_YearWise.Size = New System.Drawing.Size(73, 17)
        Me.rbtn_YearWise.TabIndex = 338
        Me.rbtn_YearWise.Text = "Year Wise"
        Me.rbtn_YearWise.UseVisualStyleBackColor = True
        '
        'rbtn_MonthWise
        '
        Me.rbtn_MonthWise.AutoSize = True
        Me.rbtn_MonthWise.Location = New System.Drawing.Point(95, 8)
        Me.rbtn_MonthWise.Name = "rbtn_MonthWise"
        Me.rbtn_MonthWise.Size = New System.Drawing.Size(88, 17)
        Me.rbtn_MonthWise.TabIndex = 0
        Me.rbtn_MonthWise.Text = "Month Wise"
        Me.rbtn_MonthWise.UseVisualStyleBackColor = True
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox4.Controls.Add(Me.txtToDate)
        Me.RadGroupBox4.Controls.Add(Me.txtfDate)
        Me.RadGroupBox4.HeaderText = "Date Range"
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 1)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(310, 42)
        Me.RadGroupBox4.TabIndex = 53
        Me.RadGroupBox4.Text = "Date Range"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(142, 16)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel17.TabIndex = 3
        Me.MyLabel17.Text = "To"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel18.TabIndex = 2
        Me.MyLabel18.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(169, 15)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtfDate
        '
        Me.txtfDate.CustomFormat = "dd/MM/yyyy"
        Me.txtfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfDate.Location = New System.Drawing.Point(44, 15)
        Me.txtfDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfDate.Name = "txtfDate"
        Me.txtfDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfDate.Size = New System.Drawing.Size(88, 20)
        Me.txtfDate.TabIndex = 0
        Me.txtfDate.TabStop = False
        Me.txtfDate.Text = "24/10/2011"
        Me.txtfDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvData)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1023, 380)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvData
        '
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvData.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvData.Name = "gvData"
        Me.gvData.ShowHeaderCellButtons = True
        Me.gvData.Size = New System.Drawing.Size(1023, 380)
        Me.gvData.TabIndex = 0
        Me.gvData.Text = "RadGridView1"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(158, 8)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadSplitButton1.TabIndex = 155
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(967, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 163
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(9, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 160
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(84, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 159
        Me.btnReset.Text = "Reset"
        '
        'gbReturn
        '
        Me.gbReturn.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbReturn.Controls.Add(Me.rdbSalesReturn)
        Me.gbReturn.Controls.Add(Me.rdbSalesAndSalesReturn)
        Me.gbReturn.Controls.Add(Me.rdbSales)
        Me.gbReturn.HeaderText = "Sales And Sales Return"
        Me.gbReturn.Location = New System.Drawing.Point(541, 1)
        Me.gbReturn.Name = "gbReturn"
        Me.gbReturn.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbReturn.Size = New System.Drawing.Size(189, 84)
        Me.gbReturn.TabIndex = 427
        Me.gbReturn.Text = "Sales And Sales Return"
        '
        'rdbSalesReturn
        '
        Me.rdbSalesReturn.AutoSize = True
        Me.rdbSalesReturn.Location = New System.Drawing.Point(13, 41)
        Me.rdbSalesReturn.Name = "rdbSalesReturn"
        Me.rdbSalesReturn.Size = New System.Drawing.Size(89, 17)
        Me.rdbSalesReturn.TabIndex = 6
        Me.rdbSalesReturn.Text = "Sales Return"
        Me.rdbSalesReturn.UseVisualStyleBackColor = True
        '
        'rdbSalesAndSalesReturn
        '
        Me.rdbSalesAndSalesReturn.AutoSize = True
        Me.rdbSalesAndSalesReturn.Location = New System.Drawing.Point(13, 62)
        Me.rdbSalesAndSalesReturn.Name = "rdbSalesAndSalesReturn"
        Me.rdbSalesAndSalesReturn.Size = New System.Drawing.Size(142, 17)
        Me.rdbSalesAndSalesReturn.TabIndex = 5
        Me.rdbSalesAndSalesReturn.Text = "Sales And Sales Return"
        Me.rdbSalesAndSalesReturn.UseVisualStyleBackColor = True
        '
        'rdbSales
        '
        Me.rdbSales.AutoSize = True
        Me.rdbSales.Checked = True
        Me.rdbSales.Location = New System.Drawing.Point(13, 20)
        Me.rdbSales.Name = "rdbSales"
        Me.rdbSales.Size = New System.Drawing.Size(51, 17)
        Me.rdbSales.TabIndex = 4
        Me.rdbSales.TabStop = True
        Me.rdbSales.Text = "Sales"
        Me.rdbSales.UseVisualStyleBackColor = True
        '
        'FrmZoneWiseSKUReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 495)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmZoneWiseSKUReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Zone Wise SKU Report"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkDetailReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDocStatus.ResumeLayout(False)
        Me.gbDocStatus.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbReturn.ResumeLayout(False)
        Me.gbReturn.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtfDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvData As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtn_DateWise As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_YearWise As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_MonthWise As System.Windows.Forms.RadioButton
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblDocType As common.Controls.MyLabel
    Friend WithEvents ddlDocType As RadDropDownList
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents gbDocStatus As RadGroupBox
    Friend WithEvents rdbWithoutSchemeAndSample As RadioButton
    Friend WithEvents rdbWithSchemeAndSample As RadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents rdbOnlySchemeAndSample As RadioButton
    Friend WithEvents chkDetailReport As RadCheckBox
    Friend WithEvents gbReturn As RadGroupBox
    Friend WithEvents rdbSalesReturn As RadioButton
    Friend WithEvents rdbSalesAndSalesReturn As RadioButton
    Friend WithEvents rdbSales As RadioButton
End Class

