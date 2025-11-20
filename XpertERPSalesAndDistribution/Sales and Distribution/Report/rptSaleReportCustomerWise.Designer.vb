<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptSaleReportCustomerWise
    'Inherits System.Windows.Forms.Form
    Inherits FrmMainTranScreen
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBoxFiscalYear = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtMulFiscalYear = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cmbReportType = New common.Controls.MyComboBox()
        Me.TxtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtCustGrp = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtLocation = New common.UserControls.txtFinder()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetails = New common.UserControls.MyRadGridView()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBoxFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxFiscalYear.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        Me.rmsaveLayout.UseCompatibleTextRendering = False
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        Me.rmDeleteLayout.UseCompatibleTextRendering = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 430)
        Me.SplitContainer1.SplitterDistance = 392
        Me.SplitContainer1.TabIndex = 4
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 392)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBoxFiscalYear)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.cmbReportType)
        Me.RadPageViewPage1.Controls.Add(Me.TxtItem)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.TxtCustGrp)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 344)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBoxFiscalYear
        '
        Me.RadGroupBoxFiscalYear.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxFiscalYear.Controls.Add(Me.txtMulFiscalYear)
        Me.RadGroupBoxFiscalYear.Controls.Add(Me.MyLabel7)
        Me.RadGroupBoxFiscalYear.Controls.Add(Me.txtFiscalYear)
        Me.RadGroupBoxFiscalYear.HeaderText = ""
        Me.RadGroupBoxFiscalYear.Location = New System.Drawing.Point(16, 6)
        Me.RadGroupBoxFiscalYear.Name = "RadGroupBoxFiscalYear"
        Me.RadGroupBoxFiscalYear.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBoxFiscalYear.Size = New System.Drawing.Size(350, 28)
        Me.RadGroupBoxFiscalYear.TabIndex = 457
        Me.RadGroupBoxFiscalYear.Visible = False
        '
        'txtMulFiscalYear
        '
        Me.txtMulFiscalYear.arrDispalyMember = Nothing
        Me.txtMulFiscalYear.arrValueMember = Nothing
        Me.txtMulFiscalYear.Location = New System.Drawing.Point(101, 5)
        Me.txtMulFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMulFiscalYear.MyLinkLable1 = Me.MyLabel3
        Me.txtMulFiscalYear.MyLinkLable2 = Nothing
        Me.txtMulFiscalYear.MyNullText = "All"
        Me.txtMulFiscalYear.Name = "txtMulFiscalYear"
        Me.txtMulFiscalYear.Size = New System.Drawing.Size(245, 19)
        Me.txtMulFiscalYear.TabIndex = 457
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 109)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 336
        Me.MyLabel3.Text = "Customer"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel7.TabIndex = 456
        Me.MyLabel7.Text = "Fiscal Year"
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(101, 4)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Nothing
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(245, 20)
        Me.txtFiscalYear.TabIndex = 455
        Me.txtFiscalYear.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(16, 86)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel6.TabIndex = 454
        Me.MyLabel6.Text = "Report Type"
        '
        'cmbReportType
        '
        Me.cmbReportType.AutoCompleteDisplayMember = Nothing
        Me.cmbReportType.AutoCompleteValueMember = Nothing
        Me.cmbReportType.CalculationExpression = Nothing
        Me.cmbReportType.DropDownAnimationEnabled = True
        Me.cmbReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbReportType.FieldCode = Nothing
        Me.cmbReportType.FieldDesc = Nothing
        Me.cmbReportType.FieldMaxLength = 0
        Me.cmbReportType.FieldName = Nothing
        Me.cmbReportType.isCalculatedField = False
        Me.cmbReportType.IsSourceFromTable = False
        Me.cmbReportType.IsSourceFromValueList = False
        Me.cmbReportType.IsUnique = False
        RadListDataItem5.Text = "Customer And Item Wise (Horizontal)"
        RadListDataItem6.Text = "Customer And Item Wise (Vertical)"
        RadListDataItem7.Text = "Year And Month Wise"
        RadListDataItem8.Text = "Year Wise"
        Me.cmbReportType.Items.Add(RadListDataItem5)
        Me.cmbReportType.Items.Add(RadListDataItem6)
        Me.cmbReportType.Items.Add(RadListDataItem7)
        Me.cmbReportType.Items.Add(RadListDataItem8)
        Me.cmbReportType.Location = New System.Drawing.Point(116, 85)
        Me.cmbReportType.MendatroryField = True
        Me.cmbReportType.MyLinkLable1 = Nothing
        Me.cmbReportType.MyLinkLable2 = Nothing
        Me.cmbReportType.Name = "cmbReportType"
        Me.cmbReportType.NullText = "Please Select"
        Me.cmbReportType.ReferenceFieldDesc = Nothing
        Me.cmbReportType.ReferenceFieldName = Nothing
        Me.cmbReportType.ReferenceTableName = Nothing
        Me.cmbReportType.Size = New System.Drawing.Size(249, 20)
        Me.cmbReportType.TabIndex = 453
        '
        'TxtItem
        '
        Me.TxtItem.arrDispalyMember = Nothing
        Me.TxtItem.arrValueMember = Nothing
        Me.TxtItem.Location = New System.Drawing.Point(115, 178)
        Me.TxtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.MyLinkLable1 = Me.MyLabel4
        Me.TxtItem.MyLinkLable2 = Nothing
        Me.TxtItem.MyNullText = "All"
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(251, 19)
        Me.TxtItem.TabIndex = 451
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(16, 179)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel4.TabIndex = 452
        Me.MyLabel4.Text = "Item"
        '
        'TxtCustGrp
        '
        Me.TxtCustGrp.arrDispalyMember = Nothing
        Me.TxtCustGrp.arrValueMember = Nothing
        Me.TxtCustGrp.Location = New System.Drawing.Point(115, 154)
        Me.TxtCustGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustGrp.MyLinkLable1 = Me.MyLabel1
        Me.TxtCustGrp.MyLinkLable2 = Nothing
        Me.TxtCustGrp.MyNullText = "All"
        Me.TxtCustGrp.Name = "TxtCustGrp"
        Me.TxtCustGrp.Size = New System.Drawing.Size(251, 19)
        Me.TxtCustGrp.TabIndex = 449
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(16, 155)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel1.TabIndex = 450
        Me.MyLabel1.Text = "Customer Group"
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(221, 131)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(145, 18)
        Me.lblBillToLocation.TabIndex = 45
        Me.lblBillToLocation.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(16, 133)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 447
        Me.MyLabel5.Text = "Location"
        '
        'TxtLocation
        '
        Me.TxtLocation.CalculationExpression = Nothing
        Me.TxtLocation.FieldCode = Nothing
        Me.TxtLocation.FieldDesc = Nothing
        Me.TxtLocation.FieldMaxLength = 0
        Me.TxtLocation.FieldName = Nothing
        Me.TxtLocation.isCalculatedField = False
        Me.TxtLocation.IsSourceFromTable = False
        Me.TxtLocation.IsSourceFromValueList = False
        Me.TxtLocation.IsUnique = False
        Me.TxtLocation.Location = New System.Drawing.Point(115, 131)
        Me.TxtLocation.MendatroryField = True
        Me.TxtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLocation.MyLinkLable1 = Me.MyLabel5
        Me.TxtLocation.MyLinkLable2 = Nothing
        Me.TxtLocation.MyReadOnly = False
        Me.TxtLocation.MyShowMasterFormButton = False
        Me.TxtLocation.Name = "TxtLocation"
        Me.TxtLocation.ReferenceFieldDesc = Nothing
        Me.TxtLocation.ReferenceFieldName = Nothing
        Me.TxtLocation.ReferenceTableName = Nothing
        Me.TxtLocation.Size = New System.Drawing.Size(100, 18)
        Me.TxtLocation.TabIndex = 446
        Me.TxtLocation.Value = ""
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(115, 108)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(251, 19)
        Me.txtCustomer.TabIndex = 335
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(15, 37)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox3.TabIndex = 53
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
        Me.txtToDate.Location = New System.Drawing.Point(235, 12)
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
        Me.txtToDate.TabIndex = 452
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-07-2023"
        Me.txtToDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
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
        Me.txtFromDate.Location = New System.Drawing.Point(67, 12)
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
        Me.txtFromDate.TabIndex = 451
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(186, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 344)
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
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(779, 344)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(273, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 159
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(704, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 158
        Me.btnClose.Text = "Close"
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(171, 7)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 22)
        Me.btnSplitExport.TabIndex = 157
        Me.btnSplitExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(93, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 153
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(14, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 152
        Me.btnGo.Text = ">>>"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(779, 344)
        Me.RadPageViewPage3.Text = "Details"
        '
        'gvDetails
        '
        Me.gvDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvDetails.ForeColor = System.Drawing.Color.Black
        Me.gvDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetails.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetails.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvDetails.MyExportFilePath = ""
        Me.gvDetails.MyStopExport = False
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetails.ShowHeaderCellButtons = True
        Me.gvDetails.Size = New System.Drawing.Size(779, 344)
        Me.gvDetails.TabIndex = 1
        Me.gvDetails.VarID = ""
        '
        'rptSaleReportCustomerWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptSaleReportCustomerWise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptSaleReportCustomerWise"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBoxFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxFiscalYear.ResumeLayout(False)
        Me.RadGroupBoxFiscalYear.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents rmsaveLayout As RadMenuItem
    Friend WithEvents rmDeleteLayout As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtLocation As common.UserControls.txtFinder
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents TxtCustGrp As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents TxtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cmbReportType As common.Controls.MyComboBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents RadGroupBoxFiscalYear As RadGroupBox
    Friend WithEvents txtMulFiscalYear As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
End Class
