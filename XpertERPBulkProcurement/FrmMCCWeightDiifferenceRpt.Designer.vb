<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCCWeightDiifferenceRpt
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblfShift = New common.Controls.MyLabel()
        Me.FromShift = New common.Controls.MyComboBox()
        Me.txtDiffAmt = New common.MyNumBox()
        Me.lbldifamt = New common.Controls.MyLabel()
        Me.txtRouteCode = New common.UserControls.txtMultiSelectFinder()
        Me.rbtndaterangewise = New System.Windows.Forms.RadioButton()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.rbtnDateWise = New System.Windows.Forms.RadioButton()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.lbltodate = New common.Controls.MyLabel()
        Me.dtDoctToDate = New common.Controls.MyDateTimePicker()
        Me.lblShift = New common.Controls.MyLabel()
        Me.DtpDocfDate = New common.Controls.MyDateTimePicker()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.txtMccCode = New common.UserControls.txtMultiSelectFinder()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnok = New Telerik.WinControls.UI.RadButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblfShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldifamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDoctToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpDocfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnok)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 578)
        Me.SplitContainer1.SplitterDistance = 542
        Me.SplitContainer1.TabIndex = 2
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(921, 522)
        Me.RadPageView1.TabIndex = 417
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblfShift)
        Me.RadPageViewPage1.Controls.Add(Me.FromShift)
        Me.RadPageViewPage1.Controls.Add(Me.txtDiffAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lbldifamt)
        Me.RadPageViewPage1.Controls.Add(Me.txtRouteCode)
        Me.RadPageViewPage1.Controls.Add(Me.rbtndaterangewise)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCCCode)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDateWise)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteCode)
        Me.RadPageViewPage1.Controls.Add(Me.lbltodate)
        Me.RadPageViewPage1.Controls.Add(Me.dtDoctToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblShift)
        Me.RadPageViewPage1.Controls.Add(Me.DtpDocfDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtMccCode)
        Me.RadPageViewPage1.Controls.Add(Me.cboShift)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocDate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(143.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(900, 474)
        Me.RadPageViewPage1.Text = "Weight Difference Report"
        '
        'lblfShift
        '
        Me.lblfShift.FieldName = Nothing
        Me.lblfShift.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblfShift.Location = New System.Drawing.Point(203, 78)
        Me.lblfShift.Name = "lblfShift"
        Me.lblfShift.Size = New System.Drawing.Size(29, 16)
        Me.lblfShift.TabIndex = 346
        Me.lblfShift.Text = "Shift"
        '
        'FromShift
        '
        Me.FromShift.AutoCompleteDisplayMember = Nothing
        Me.FromShift.AutoCompleteValueMember = Nothing
        Me.FromShift.CalculationExpression = Nothing
        Me.FromShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.FromShift.FieldCode = Nothing
        Me.FromShift.FieldDesc = Nothing
        Me.FromShift.FieldMaxLength = 0
        Me.FromShift.FieldName = Nothing
        Me.FromShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FromShift.isCalculatedField = False
        Me.FromShift.IsSourceFromTable = False
        Me.FromShift.IsSourceFromValueList = False
        Me.FromShift.IsUnique = False
        Me.FromShift.Location = New System.Drawing.Point(242, 77)
        Me.FromShift.MendatroryField = False
        Me.FromShift.MyLinkLable1 = Me.lblfShift
        Me.FromShift.MyLinkLable2 = Nothing
        Me.FromShift.Name = "FromShift"
        Me.FromShift.ReferenceFieldDesc = Nothing
        Me.FromShift.ReferenceFieldName = Nothing
        Me.FromShift.ReferenceTableName = Nothing
        Me.FromShift.Size = New System.Drawing.Size(51, 18)
        Me.FromShift.TabIndex = 347
        '
        'txtDiffAmt
        '
        Me.txtDiffAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDiffAmt.CalculationExpression = Nothing
        Me.txtDiffAmt.DecimalPlaces = 0
        Me.txtDiffAmt.FieldCode = Nothing
        Me.txtDiffAmt.FieldDesc = Nothing
        Me.txtDiffAmt.FieldMaxLength = 0
        Me.txtDiffAmt.FieldName = Nothing
        Me.txtDiffAmt.isCalculatedField = False
        Me.txtDiffAmt.IsSourceFromTable = False
        Me.txtDiffAmt.IsSourceFromValueList = False
        Me.txtDiffAmt.IsUnique = False
        Me.txtDiffAmt.Location = New System.Drawing.Point(347, 52)
        Me.txtDiffAmt.MendatroryField = False
        Me.txtDiffAmt.MyLinkLable1 = Me.lbldifamt
        Me.txtDiffAmt.MyLinkLable2 = Nothing
        Me.txtDiffAmt.Name = "txtDiffAmt"
        Me.txtDiffAmt.ReferenceFieldDesc = Nothing
        Me.txtDiffAmt.ReferenceFieldName = Nothing
        Me.txtDiffAmt.ReferenceTableName = Nothing
        Me.txtDiffAmt.Size = New System.Drawing.Size(39, 20)
        Me.txtDiffAmt.TabIndex = 345
        Me.txtDiffAmt.Text = "0"
        Me.txtDiffAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiffAmt.Value = 0.0R
        '
        'lbldifamt
        '
        Me.lbldifamt.FieldName = Nothing
        Me.lbldifamt.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldifamt.Location = New System.Drawing.Point(224, 55)
        Me.lbldifamt.Name = "lbldifamt"
        Me.lbldifamt.Size = New System.Drawing.Size(113, 16)
        Me.lbldifamt.TabIndex = 344
        Me.lbldifamt.Text = "Difference Weight +/-"
        '
        'txtRouteCode
        '
        Me.txtRouteCode.arrDispalyMember = Nothing
        Me.txtRouteCode.arrValueMember = Nothing
        Me.txtRouteCode.Location = New System.Drawing.Point(112, 29)
        Me.txtRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteCode.MyLinkLable1 = Nothing
        Me.txtRouteCode.MyLinkLable2 = Nothing
        Me.txtRouteCode.MyNullText = "All"
        Me.txtRouteCode.Name = "txtRouteCode"
        Me.txtRouteCode.Size = New System.Drawing.Size(472, 19)
        Me.txtRouteCode.TabIndex = 337
        '
        'rbtndaterangewise
        '
        Me.rbtndaterangewise.AutoSize = True
        Me.rbtndaterangewise.Location = New System.Drawing.Point(110, 54)
        Me.rbtndaterangewise.Name = "rbtndaterangewise"
        Me.rbtndaterangewise.Size = New System.Drawing.Size(111, 17)
        Me.rbtndaterangewise.TabIndex = 342
        Me.rbtndaterangewise.TabStop = True
        Me.rbtndaterangewise.Text = "Date Range wise"
        Me.rbtndaterangewise.UseVisualStyleBackColor = True
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(8, 6)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblMCCCode.TabIndex = 10
        Me.lblMCCCode.Text = "MCC Code"
        '
        'rbtnDateWise
        '
        Me.rbtnDateWise.AutoSize = True
        Me.rbtnDateWise.Location = New System.Drawing.Point(9, 54)
        Me.rbtnDateWise.Name = "rbtnDateWise"
        Me.rbtnDateWise.Size = New System.Drawing.Size(75, 17)
        Me.rbtnDateWise.TabIndex = 341
        Me.rbtnDateWise.TabStop = True
        Me.rbtnDateWise.Text = "Date wise"
        Me.rbtnDateWise.UseVisualStyleBackColor = True
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(8, 29)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(67, 16)
        Me.lblRouteCode.TabIndex = 13
        Me.lblRouteCode.Text = "Route Code"
        '
        'lbltodate
        '
        Me.lbltodate.FieldName = Nothing
        Me.lbltodate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbltodate.Location = New System.Drawing.Point(299, 78)
        Me.lbltodate.Name = "lbltodate"
        Me.lbltodate.Size = New System.Drawing.Size(46, 16)
        Me.lbltodate.TabIndex = 339
        Me.lbltodate.Text = "To Date"
        '
        'dtDoctToDate
        '
        Me.dtDoctToDate.CalculationExpression = Nothing
        Me.dtDoctToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtDoctToDate.FieldCode = Nothing
        Me.dtDoctToDate.FieldDesc = Nothing
        Me.dtDoctToDate.FieldMaxLength = 0
        Me.dtDoctToDate.FieldName = Nothing
        Me.dtDoctToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDoctToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDoctToDate.isCalculatedField = False
        Me.dtDoctToDate.IsSourceFromTable = False
        Me.dtDoctToDate.IsSourceFromValueList = False
        Me.dtDoctToDate.IsUnique = False
        Me.dtDoctToDate.Location = New System.Drawing.Point(350, 77)
        Me.dtDoctToDate.MendatroryField = False
        Me.dtDoctToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDoctToDate.MyLinkLable1 = Me.lbltodate
        Me.dtDoctToDate.MyLinkLable2 = Nothing
        Me.dtDoctToDate.Name = "dtDoctToDate"
        Me.dtDoctToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDoctToDate.ReferenceFieldDesc = Nothing
        Me.dtDoctToDate.ReferenceFieldName = Nothing
        Me.dtDoctToDate.ReferenceTableName = Nothing
        Me.dtDoctToDate.Size = New System.Drawing.Size(90, 18)
        Me.dtDoctToDate.TabIndex = 340
        Me.dtDoctToDate.TabStop = False
        Me.dtDoctToDate.Text = "13/06/2011"
        Me.dtDoctToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'lblShift
        '
        Me.lblShift.FieldName = Nothing
        Me.lblShift.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblShift.Location = New System.Drawing.Point(446, 78)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(29, 16)
        Me.lblShift.TabIndex = 41
        Me.lblShift.Text = "Shift"
        '
        'DtpDocfDate
        '
        Me.DtpDocfDate.CalculationExpression = Nothing
        Me.DtpDocfDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpDocfDate.FieldCode = Nothing
        Me.DtpDocfDate.FieldDesc = Nothing
        Me.DtpDocfDate.FieldMaxLength = 0
        Me.DtpDocfDate.FieldName = Nothing
        Me.DtpDocfDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpDocfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDocfDate.isCalculatedField = False
        Me.DtpDocfDate.IsSourceFromTable = False
        Me.DtpDocfDate.IsSourceFromValueList = False
        Me.DtpDocfDate.IsUnique = False
        Me.DtpDocfDate.Location = New System.Drawing.Point(111, 77)
        Me.DtpDocfDate.MendatroryField = False
        Me.DtpDocfDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDocfDate.MyLinkLable1 = Me.lblDocDate
        Me.DtpDocfDate.MyLinkLable2 = Nothing
        Me.DtpDocfDate.Name = "DtpDocfDate"
        Me.DtpDocfDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDocfDate.ReferenceFieldDesc = Nothing
        Me.DtpDocfDate.ReferenceFieldName = Nothing
        Me.DtpDocfDate.ReferenceTableName = Nothing
        Me.DtpDocfDate.Size = New System.Drawing.Size(90, 18)
        Me.DtpDocfDate.TabIndex = 40
        Me.DtpDocfDate.TabStop = False
        Me.DtpDocfDate.Text = "13/06/2011"
        Me.DtpDocfDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocDate.Location = New System.Drawing.Point(8, 78)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(60, 16)
        Me.lblDocDate.TabIndex = 39
        Me.lblDocDate.Text = "From Date"
        '
        'txtMccCode
        '
        Me.txtMccCode.arrDispalyMember = Nothing
        Me.txtMccCode.arrValueMember = Nothing
        Me.txtMccCode.Location = New System.Drawing.Point(112, 5)
        Me.txtMccCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccCode.MyLinkLable1 = Nothing
        Me.txtMccCode.MyLinkLable2 = Nothing
        Me.txtMccCode.MyNullText = "All"
        Me.txtMccCode.Name = "txtMccCode"
        Me.txtMccCode.Size = New System.Drawing.Size(472, 19)
        Me.txtMccCode.TabIndex = 336
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        Me.cboShift.Location = New System.Drawing.Point(481, 77)
        Me.cboShift.MendatroryField = False
        Me.cboShift.MyLinkLable1 = Me.lblShift
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(51, 18)
        Me.cboShift.TabIndex = 42
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(900, 474)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(900, 474)
        Me.gv1.TabIndex = 5
        Me.gv1.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(921, 20)
        Me.rdmenufile.TabIndex = 418
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
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
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(82, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(88, 18)
        Me.btnReset.TabIndex = 3
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(843, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Location = New System.Drawing.Point(10, 8)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(66, 18)
        Me.btnok.TabIndex = 38
        Me.btnok.Text = ">>>>"
        '
        'btnExp
        '
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(176, 8)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(100, 18)
        Me.btnExp.TabIndex = 162
        Me.btnExp.Text = "Export"
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
        'FrmMCCWeightDiifferenceRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 578)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMCCWeightDiifferenceRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Weight Difference Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblfShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldifamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDoctToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpDocfDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lbltodate As common.Controls.MyLabel
    Friend WithEvents dtDoctToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRouteCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMccCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblShift As common.Controls.MyLabel
    Friend WithEvents DtpDocfDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnok As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtndaterangewise As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDateWise As System.Windows.Forms.RadioButton
    Friend WithEvents lbldifamt As common.Controls.MyLabel
    Friend WithEvents txtDiffAmt As common.MyNumBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblfShift As common.Controls.MyLabel
    Friend WithEvents FromShift As common.Controls.MyComboBox
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class
