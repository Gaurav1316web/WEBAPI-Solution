<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptMCCProvision
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
        Me.gvDB = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.cboTransactionType = New common.Controls.MyComboBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.cboToDateShift = New common.Controls.MyComboBox()
        Me.cboFromDateShift = New common.Controls.MyComboBox()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.cboReportType = New common.Controls.MyComboBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.cboTransactionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboToDateShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFromDateShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gvDB
        '
        Me.gvDB.AllowAddNewRow = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(977, 358)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(956, 310)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.cboTransactionType)
        Me.RadPanel1.Controls.Add(Me.cboToDateShift)
        Me.RadPanel1.Controls.Add(Me.cboFromDateShift)
        Me.RadPanel1.Controls.Add(Me.txtVendor)
        Me.RadPanel1.Controls.Add(Me.MyLabel4)
        Me.RadPanel1.Controls.Add(Me.MyLabel3)
        Me.RadPanel1.Controls.Add(Me.txtMCC)
        Me.RadPanel1.Controls.Add(Me.MyLabel2)
        Me.RadPanel1.Controls.Add(Me.txtFromDate)
        Me.RadPanel1.Controls.Add(Me.txtToDate)
        Me.RadPanel1.Controls.Add(Me.lblToDate)
        Me.RadPanel1.Controls.Add(Me.lblFromdate)
        Me.RadPanel1.Controls.Add(Me.RadLabel3)
        Me.RadPanel1.Controls.Add(Me.cboReportType)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(956, 310)
        Me.RadPanel1.TabIndex = 15
        '
        'cboTransactionType
        '
        Me.cboTransactionType.AutoCompleteDisplayMember = Nothing
        Me.cboTransactionType.AutoCompleteValueMember = Nothing
        Me.cboTransactionType.CalculationExpression = Nothing
        Me.cboTransactionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransactionType.FieldCode = Nothing
        Me.cboTransactionType.FieldDesc = Nothing
        Me.cboTransactionType.FieldMaxLength = 0
        Me.cboTransactionType.FieldName = Nothing
        Me.cboTransactionType.isCalculatedField = False
        Me.cboTransactionType.IsSourceFromTable = False
        Me.cboTransactionType.IsSourceFromValueList = False
        Me.cboTransactionType.IsUnique = False
        Me.cboTransactionType.Location = New System.Drawing.Point(79, 49)
        Me.cboTransactionType.MendatroryField = False
        Me.cboTransactionType.MyLinkLable1 = Me.RadLabel3
        Me.cboTransactionType.MyLinkLable2 = Nothing
        Me.cboTransactionType.Name = "cboTransactionType"
        Me.cboTransactionType.ReferenceFieldDesc = Nothing
        Me.cboTransactionType.ReferenceFieldName = Nothing
        Me.cboTransactionType.ReferenceTableName = Nothing
        Me.cboTransactionType.Size = New System.Drawing.Size(405, 20)
        Me.cboTransactionType.TabIndex = 370
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(7, 28)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Report Type"
        '
        'cboToDateShift
        '
        Me.cboToDateShift.AutoCompleteDisplayMember = Nothing
        Me.cboToDateShift.AutoCompleteValueMember = Nothing
        Me.cboToDateShift.CalculationExpression = Nothing
        Me.cboToDateShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboToDateShift.FieldCode = Nothing
        Me.cboToDateShift.FieldDesc = Nothing
        Me.cboToDateShift.FieldMaxLength = 0
        Me.cboToDateShift.FieldName = Nothing
        Me.cboToDateShift.isCalculatedField = False
        Me.cboToDateShift.IsSourceFromTable = False
        Me.cboToDateShift.IsSourceFromValueList = False
        Me.cboToDateShift.IsUnique = False
        Me.cboToDateShift.Location = New System.Drawing.Point(395, 4)
        Me.cboToDateShift.MendatroryField = False
        Me.cboToDateShift.MyLinkLable1 = Me.RadLabel3
        Me.cboToDateShift.MyLinkLable2 = Nothing
        Me.cboToDateShift.Name = "cboToDateShift"
        Me.cboToDateShift.ReferenceFieldDesc = Nothing
        Me.cboToDateShift.ReferenceFieldName = Nothing
        Me.cboToDateShift.ReferenceTableName = Nothing
        Me.cboToDateShift.Size = New System.Drawing.Size(89, 20)
        Me.cboToDateShift.TabIndex = 369
        '
        'cboFromDateShift
        '
        Me.cboFromDateShift.AutoCompleteDisplayMember = Nothing
        Me.cboFromDateShift.AutoCompleteValueMember = Nothing
        Me.cboFromDateShift.CalculationExpression = Nothing
        Me.cboFromDateShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFromDateShift.FieldCode = Nothing
        Me.cboFromDateShift.FieldDesc = Nothing
        Me.cboFromDateShift.FieldMaxLength = 0
        Me.cboFromDateShift.FieldName = Nothing
        Me.cboFromDateShift.isCalculatedField = False
        Me.cboFromDateShift.IsSourceFromTable = False
        Me.cboFromDateShift.IsSourceFromValueList = False
        Me.cboFromDateShift.IsUnique = False
        Me.cboFromDateShift.Location = New System.Drawing.Point(160, 4)
        Me.cboFromDateShift.MendatroryField = False
        Me.cboFromDateShift.MyLinkLable1 = Me.RadLabel3
        Me.cboFromDateShift.MyLinkLable2 = Nothing
        Me.cboFromDateShift.Name = "cboFromDateShift"
        Me.cboFromDateShift.ReferenceFieldDesc = Nothing
        Me.cboFromDateShift.ReferenceFieldName = Nothing
        Me.cboFromDateShift.ReferenceTableName = Nothing
        Me.cboFromDateShift.Size = New System.Drawing.Size(89, 20)
        Me.cboFromDateShift.TabIndex = 368
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(79, 94)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.MyLabel4
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(405, 19)
        Me.txtVendor.TabIndex = 6
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 94)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel4.TabIndex = 363
        Me.MyLabel4.Text = "Vendor"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel3.TabIndex = 359
        Me.MyLabel3.Text = "Transaction"
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(79, 72)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel2
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(405, 19)
        Me.txtMCC.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 72)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel2.TabIndex = 356
        Me.MyLabel2.Text = "MCC"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(79, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(307, 4)
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
        Me.txtToDate.TabIndex = 2
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(256, 5)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Date"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(8, 5)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Date"
        '
        'cboReportType
        '
        Me.cboReportType.AutoCompleteDisplayMember = Nothing
        Me.cboReportType.AutoCompleteValueMember = Nothing
        Me.cboReportType.CalculationExpression = Nothing
        Me.cboReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboReportType.FieldCode = Nothing
        Me.cboReportType.FieldDesc = Nothing
        Me.cboReportType.FieldMaxLength = 0
        Me.cboReportType.FieldName = Nothing
        Me.cboReportType.isCalculatedField = False
        Me.cboReportType.IsSourceFromTable = False
        Me.cboReportType.IsSourceFromValueList = False
        Me.cboReportType.IsUnique = False
        Me.cboReportType.Location = New System.Drawing.Point(79, 27)
        Me.cboReportType.MendatroryField = False
        Me.cboReportType.MyLinkLable1 = Me.RadLabel3
        Me.cboReportType.MyLinkLable2 = Nothing
        Me.cboReportType.Name = "cboReportType"
        Me.cboReportType.ReferenceFieldDesc = Nothing
        Me.cboReportType.ReferenceFieldName = Nothing
        Me.cboReportType.ReferenceTableName = Nothing
        Me.cboReportType.Size = New System.Drawing.Size(405, 20)
        Me.cboReportType.TabIndex = 3
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(956, 310)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(956, 310)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadSplitButton1)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 378)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(977, 23)
        Me.Panel1.TabIndex = 1
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(228, 3)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 18)
        Me.RadSplitButton1.TabIndex = 144
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
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(154, 3)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadButton1.TabIndex = 143
        Me.RadButton1.Text = "<< Back "
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(80, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(903, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = " Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(977, 20)
        Me.RadMenu1.TabIndex = 23
        Me.RadMenu1.Text = "RadMenu1"
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
        'frmRptMCCProvision
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(977, 401)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRptMCCProvision"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Provision Report"
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.cboTransactionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboToDateShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFromDateShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboReportType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboReportType As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvDB As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboToDateShift As common.Controls.MyComboBox
    Friend WithEvents cboFromDateShift As common.Controls.MyComboBox
    Friend WithEvents cboTransactionType As common.Controls.MyComboBox
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

