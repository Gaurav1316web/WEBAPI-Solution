<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptDBTBankResponse
    'Inherits System.Windows.Forms.Form
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.txtStatus = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBoth = New common.Controls.MyRadioButton()
        Me.txtFarmerCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblFarmerCode = New common.Controls.MyLabel()
        Me.rbtnFailed = New common.Controls.MyRadioButton()
        Me.rbtnSuccess = New common.Controls.MyRadioButton()
        Me.txtDcsCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblDcsCode = New common.Controls.MyLabel()
        Me.txtDoc = New common.UserControls.txtMultiSelectFinder()
        Me.lblDocumnetcode = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUnion = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSuccess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDcsCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumnetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 382)
        Me.RadPageView1.TabIndex = 5
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 334)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.txtStatus)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(779, 334)
        Me.RadPanel1.TabIndex = 15
        '
        'txtStatus
        '
        Me.txtStatus.arrDispalyMember = Nothing
        Me.txtStatus.arrValueMember = Nothing
        Me.txtStatus.Location = New System.Drawing.Point(355, 237)
        Me.txtStatus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.MyLinkLable1 = Me.MyLabel4
        Me.txtStatus.MyLinkLable2 = Nothing
        Me.txtStatus.MyNullText = "All"
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(230, 19)
        Me.txtStatus.TabIndex = 453
        Me.txtStatus.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 65)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(37, 18)
        Me.MyLabel4.TabIndex = 454
        Me.MyLabel4.Text = "Status"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnBoth)
        Me.RadGroupBox1.Controls.Add(Me.txtFarmerCode)
        Me.RadGroupBox1.Controls.Add(Me.rbtnFailed)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.rbtnSuccess)
        Me.RadGroupBox1.Controls.Add(Me.lblFarmerCode)
        Me.RadGroupBox1.Controls.Add(Me.txtDcsCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDcsCode)
        Me.RadGroupBox1.Controls.Add(Me.txtDoc)
        Me.RadGroupBox1.Controls.Add(Me.lblDocumnetcode)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.txtUnion)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(347, 170)
        Me.RadGroupBox1.TabIndex = 389
        '
        'rbtnBoth
        '
        Me.rbtnBoth.Location = New System.Drawing.Point(272, 65)
        Me.rbtnBoth.MyLinkLable1 = Nothing
        Me.rbtnBoth.MyLinkLable2 = Nothing
        Me.rbtnBoth.Name = "rbtnBoth"
        Me.rbtnBoth.Size = New System.Drawing.Size(44, 18)
        Me.rbtnBoth.TabIndex = 455
        Me.rbtnBoth.TabStop = False
        Me.rbtnBoth.Text = "Both"
        '
        'txtFarmerCode
        '
        Me.txtFarmerCode.arrDispalyMember = Nothing
        Me.txtFarmerCode.arrValueMember = Nothing
        Me.txtFarmerCode.Location = New System.Drawing.Point(108, 137)
        Me.txtFarmerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFarmerCode.MyLinkLable1 = Me.lblFarmerCode
        Me.txtFarmerCode.MyLinkLable2 = Nothing
        Me.txtFarmerCode.MyNullText = "All"
        Me.txtFarmerCode.Name = "txtFarmerCode"
        Me.txtFarmerCode.Size = New System.Drawing.Size(230, 19)
        Me.txtFarmerCode.TabIndex = 451
        '
        'lblFarmerCode
        '
        Me.lblFarmerCode.FieldName = Nothing
        Me.lblFarmerCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFarmerCode.Location = New System.Drawing.Point(5, 137)
        Me.lblFarmerCode.Name = "lblFarmerCode"
        Me.lblFarmerCode.Size = New System.Drawing.Size(70, 18)
        Me.lblFarmerCode.TabIndex = 452
        Me.lblFarmerCode.Text = "Farmer Code"
        '
        'rbtnFailed
        '
        Me.rbtnFailed.Location = New System.Drawing.Point(189, 65)
        Me.rbtnFailed.MyLinkLable1 = Nothing
        Me.rbtnFailed.MyLinkLable2 = Nothing
        Me.rbtnFailed.Name = "rbtnFailed"
        Me.rbtnFailed.Size = New System.Drawing.Size(50, 18)
        Me.rbtnFailed.TabIndex = 397
        Me.rbtnFailed.TabStop = False
        Me.rbtnFailed.Text = "Failed"
        '
        'rbtnSuccess
        '
        Me.rbtnSuccess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSuccess.Location = New System.Drawing.Point(108, 65)
        Me.rbtnSuccess.MyLinkLable1 = Nothing
        Me.rbtnSuccess.MyLinkLable2 = Nothing
        Me.rbtnSuccess.Name = "rbtnSuccess"
        Me.rbtnSuccess.Size = New System.Drawing.Size(59, 18)
        Me.rbtnSuccess.TabIndex = 398
        Me.rbtnSuccess.Text = "Success"
        Me.rbtnSuccess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtDcsCode
        '
        Me.txtDcsCode.arrDispalyMember = Nothing
        Me.txtDcsCode.arrValueMember = Nothing
        Me.txtDcsCode.Location = New System.Drawing.Point(108, 113)
        Me.txtDcsCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDcsCode.MyLinkLable1 = Me.lblDcsCode
        Me.txtDcsCode.MyLinkLable2 = Nothing
        Me.txtDcsCode.MyNullText = "All"
        Me.txtDcsCode.Name = "txtDcsCode"
        Me.txtDcsCode.Size = New System.Drawing.Size(230, 19)
        Me.txtDcsCode.TabIndex = 449
        '
        'lblDcsCode
        '
        Me.lblDcsCode.FieldName = Nothing
        Me.lblDcsCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDcsCode.Location = New System.Drawing.Point(5, 113)
        Me.lblDcsCode.Name = "lblDcsCode"
        Me.lblDcsCode.Size = New System.Drawing.Size(54, 18)
        Me.lblDcsCode.TabIndex = 450
        Me.lblDcsCode.Text = "Dcs Code"
        '
        'txtDoc
        '
        Me.txtDoc.arrDispalyMember = Nothing
        Me.txtDoc.arrValueMember = Nothing
        Me.txtDoc.Location = New System.Drawing.Point(108, 89)
        Me.txtDoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoc.MyLinkLable1 = Me.lblDocumnetcode
        Me.txtDoc.MyLinkLable2 = Nothing
        Me.txtDoc.MyNullText = "All"
        Me.txtDoc.Name = "txtDoc"
        Me.txtDoc.Size = New System.Drawing.Size(230, 19)
        Me.txtDoc.TabIndex = 447
        '
        'lblDocumnetcode
        '
        Me.lblDocumnetcode.FieldName = Nothing
        Me.lblDocumnetcode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumnetcode.Location = New System.Drawing.Point(5, 89)
        Me.lblDocumnetcode.Name = "lblDocumnetcode"
        Me.lblDocumnetcode.Size = New System.Drawing.Size(94, 18)
        Me.lblDocumnetcode.TabIndex = 448
        Me.lblDocumnetcode.Text = "Documenet Code"
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
        Me.txtToDate.Location = New System.Drawing.Point(226, 9)
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
        Me.txtToDate.TabIndex = 446
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
        Me.txtFromDate.Location = New System.Drawing.Point(84, 9)
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
        Me.txtFromDate.TabIndex = 445
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(174, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 444
        Me.Label2.Text = "To Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 443
        Me.Label1.Text = "From Date"
        '
        'txtUnion
        '
        Me.txtUnion.arrDispalyMember = Nothing
        Me.txtUnion.arrValueMember = Nothing
        Me.txtUnion.Location = New System.Drawing.Point(108, 40)
        Me.txtUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnion.MyLinkLable1 = Me.lblLocation
        Me.txtUnion.MyLinkLable2 = Nothing
        Me.txtUnion.MyNullText = "All"
        Me.txtUnion.Name = "txtUnion"
        Me.txtUnion.Size = New System.Drawing.Size(230, 19)
        Me.txtUnion.TabIndex = 387
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 40)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(36, 18)
        Me.lblLocation.TabIndex = 388
        Me.lblLocation.Text = "Union"
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
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(956, 310)
        Me.gv1.TabIndex = 2
        Me.gv1.VarID = ""
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
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel})
        Me.RadSplitButton1.Location = New System.Drawing.Point(160, 10)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 158
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(704, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 157
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 155
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(86, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 156
        Me.btnReset.Text = "Reset"
        '
        'rptDBTBankResponse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptDBTBankResponse"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptDBTBankResponse"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSuccess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDcsCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumnetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents rmsaveLayout As RadMenuItem
    Friend WithEvents rmDeleteLayout As RadMenuItem
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txtUnion As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtFarmerCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblFarmerCode As common.Controls.MyLabel
    Friend WithEvents txtDcsCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDcsCode As common.Controls.MyLabel
    Friend WithEvents txtDoc As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDocumnetcode As common.Controls.MyLabel
    Friend WithEvents txtStatus As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents rbtnSuccess As common.Controls.MyRadioButton
    Friend WithEvents rbtnFailed As common.Controls.MyRadioButton
    Friend WithEvents rbtnBoth As common.Controls.MyRadioButton
End Class
