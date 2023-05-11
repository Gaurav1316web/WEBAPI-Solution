<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTruckSheetRouteWiseRpt
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtDoctToDate = New common.Controls.MyDateTimePicker()
        Me.txtVlcCode = New common.UserControls.txtMultiSelectFinder()
        Me.txtRouteCode = New common.UserControls.txtMultiSelectFinder()
        Me.txtMccCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblShift = New common.Controls.MyLabel()
        Me.DtpDocfDate = New common.Controls.MyDateTimePicker()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.dgvreport = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnok = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDoctToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpDocfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.dgvreport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvreport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnok)
        Me.SplitContainer1.Size = New System.Drawing.Size(929, 587)
        Me.SplitContainer1.SplitterDistance = 551
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(929, 531)
        Me.RadPageView1.TabIndex = 417
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(111.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(908, 483)
        Me.RadPageViewPage1.Text = "Truck Sheet Report"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(899, 126)
        Me.Panel1.TabIndex = 415
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.dtDoctToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtVlcCode)
        Me.RadGroupBox1.Controls.Add(Me.txtRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.txtMccCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.DtpDocfDate)
        Me.RadGroupBox1.Controls.Add(Me.lblShift)
        Me.RadGroupBox1.Controls.Add(Me.lblvandorno)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(886, 114)
        Me.RadGroupBox1.TabIndex = 416
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(212, 85)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 339
        Me.MyLabel1.Text = "To Date"
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
        Me.dtDoctToDate.Location = New System.Drawing.Point(266, 84)
        Me.dtDoctToDate.MendatroryField = False
        Me.dtDoctToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDoctToDate.MyLinkLable1 = Me.MyLabel1
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
        'txtVlcCode
        '
        Me.txtVlcCode.arrDispalyMember = Nothing
        Me.txtVlcCode.arrValueMember = Nothing
        Me.txtVlcCode.Location = New System.Drawing.Point(117, 60)
        Me.txtVlcCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVlcCode.MyLinkLable1 = Nothing
        Me.txtVlcCode.MyLinkLable2 = Nothing
        Me.txtVlcCode.MyNullText = "All"
        Me.txtVlcCode.Name = "txtVlcCode"
        Me.txtVlcCode.Size = New System.Drawing.Size(472, 19)
        Me.txtVlcCode.TabIndex = 338
        '
        'txtRouteCode
        '
        Me.txtRouteCode.arrDispalyMember = Nothing
        Me.txtRouteCode.arrValueMember = Nothing
        Me.txtRouteCode.Location = New System.Drawing.Point(117, 35)
        Me.txtRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteCode.MyLinkLable1 = Nothing
        Me.txtRouteCode.MyLinkLable2 = Nothing
        Me.txtRouteCode.MyNullText = "All"
        Me.txtRouteCode.Name = "txtRouteCode"
        Me.txtRouteCode.Size = New System.Drawing.Size(472, 19)
        Me.txtRouteCode.TabIndex = 337
        '
        'txtMccCode
        '
        Me.txtMccCode.arrDispalyMember = Nothing
        Me.txtMccCode.arrValueMember = Nothing
        Me.txtMccCode.Location = New System.Drawing.Point(117, 11)
        Me.txtMccCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccCode.MyLinkLable1 = Nothing
        Me.txtMccCode.MyLinkLable2 = Nothing
        Me.txtMccCode.MyNullText = "All"
        Me.txtMccCode.Name = "txtMccCode"
        Me.txtMccCode.Size = New System.Drawing.Size(472, 19)
        Me.txtMccCode.TabIndex = 336
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocDate.Location = New System.Drawing.Point(13, 83)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(60, 16)
        Me.lblDocDate.TabIndex = 39
        Me.lblDocDate.Text = "From Date"
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
        Me.cboShift.Location = New System.Drawing.Point(402, 83)
        Me.cboShift.MendatroryField = False
        Me.cboShift.MyLinkLable1 = Me.lblShift
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(83, 18)
        Me.cboShift.TabIndex = 42
        '
        'lblShift
        '
        Me.lblShift.FieldName = Nothing
        Me.lblShift.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblShift.Location = New System.Drawing.Point(367, 84)
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
        Me.DtpDocfDate.Location = New System.Drawing.Point(116, 83)
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
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(13, 60)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(58, 16)
        Me.lblvandorno.TabIndex = 36
        Me.lblvandorno.Text = "VLC Code"
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(13, 35)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(67, 16)
        Me.lblRouteCode.TabIndex = 13
        Me.lblRouteCode.Text = "Route Code"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(13, 12)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblMCCCode.TabIndex = 10
        Me.lblMCCCode.Text = "MCC Code"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.dgvreport)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(908, 483)
        Me.RadPageViewPage2.Text = "Report"
        '
        'dgvreport
        '
        Me.dgvreport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvreport.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.dgvreport.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.dgvreport.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvreport.Name = "dgvreport"
        Me.dgvreport.ShowHeaderCellButtons = True
        Me.dgvreport.Size = New System.Drawing.Size(908, 483)
        Me.dgvreport.TabIndex = 1
        Me.dgvreport.TabStop = False
        Me.dgvreport.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(929, 20)
        Me.rdmenufile.TabIndex = 418
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
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
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(184, 8)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(100, 18)
        Me.btnExp.TabIndex = 161
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
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(288, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 18)
        Me.btnPrint.TabIndex = 40
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(92, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(88, 18)
        Me.btnReset.TabIndex = 39
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(851, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnok.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Location = New System.Drawing.Point(21, 7)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(66, 18)
        Me.btnok.TabIndex = 38
        Me.btnok.Text = ">>>>"
        '
        'FrmTruckSheetRouteWiseRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(929, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTruckSheetRouteWiseRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Truck Sheet Route Vise"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDoctToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpDocfDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.dgvreport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvreport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents btnok As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblShift As common.Controls.MyLabel
    Friend WithEvents DtpDocfDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtDoctToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtVlcCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtRouteCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMccCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dgvreport As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class
