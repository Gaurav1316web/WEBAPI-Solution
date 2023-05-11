<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseHistory
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.multiLocationFinder = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lbltype = New common.Controls.MyLabel()
        Me.cmbType = New common.Controls.MyComboBox()
        Me.txtSrcDesc = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.fndSrcCode = New common.UserControls.txtFinder()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.dtpfromdate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.newgv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.newgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.newgv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.multiLocationFinder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtptodate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbltype)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSrcDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSrcCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpfromdate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1078, 483)
        Me.SplitContainer1.SplitterDistance = 113
        Me.SplitContainer1.TabIndex = 0
        '
        'multiLocationFinder
        '
        Me.multiLocationFinder.arrDispalyMember = Nothing
        Me.multiLocationFinder.arrValueMember = Nothing
        Me.multiLocationFinder.Location = New System.Drawing.Point(119, 87)
        Me.multiLocationFinder.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.multiLocationFinder.MyLinkLable1 = Nothing
        Me.multiLocationFinder.MyLinkLable2 = Nothing
        Me.multiLocationFinder.MyNullText = "All"
        Me.multiLocationFinder.Name = "multiLocationFinder"
        Me.multiLocationFinder.Size = New System.Drawing.Size(362, 19)
        Me.multiLocationFinder.TabIndex = 60
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(12, 87)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 59
        Me.lblLocation.Text = "Location"
        '
        'dtptodate
        '
        Me.dtptodate.CalculationExpression = Nothing
        Me.dtptodate.CustomFormat = "dd-MM-yyyy"
        Me.dtptodate.FieldCode = Nothing
        Me.dtptodate.FieldDesc = Nothing
        Me.dtptodate.FieldMaxLength = 0
        Me.dtptodate.FieldName = Nothing
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.isCalculatedField = False
        Me.dtptodate.IsSourceFromTable = False
        Me.dtptodate.IsSourceFromValueList = False
        Me.dtptodate.IsUnique = False
        Me.dtptodate.Location = New System.Drawing.Point(359, 13)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.ReferenceFieldDesc = Nothing
        Me.dtptodate.ReferenceFieldName = Nothing
        Me.dtptodate.ReferenceTableName = Nothing
        Me.dtptodate.Size = New System.Drawing.Size(127, 20)
        Me.dtptodate.TabIndex = 1
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "14-09-2011"
        Me.dtptodate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(521, 87)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 4
        Me.btnReport.Text = ">>>"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(12, 13)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel4.TabIndex = 12
        Me.RadLabel4.Text = "From Date"
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(12, 63)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(43, 16)
        Me.lbltype.TabIndex = 52
        Me.lbltype.Text = "Vendor"
        '
        'cmbType
        '
        Me.cmbType.AutoCompleteDisplayMember = Nothing
        Me.cmbType.AutoCompleteValueMember = Nothing
        Me.cmbType.CalculationExpression = Nothing
        Me.cmbType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbType.FieldCode = Nothing
        Me.cmbType.FieldDesc = Nothing
        Me.cmbType.FieldMaxLength = 0
        Me.cmbType.FieldName = Nothing
        Me.cmbType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbType.isCalculatedField = False
        Me.cmbType.IsSourceFromTable = False
        Me.cmbType.IsSourceFromValueList = False
        Me.cmbType.IsUnique = False
        RadListDataItem4.Text = "Select"
        RadListDataItem5.Text = "Item"
        RadListDataItem6.Text = "Vendor"
        Me.cmbType.Items.Add(RadListDataItem4)
        Me.cmbType.Items.Add(RadListDataItem5)
        Me.cmbType.Items.Add(RadListDataItem6)
        Me.cmbType.Location = New System.Drawing.Point(119, 39)
        Me.cmbType.MendatroryField = False
        Me.cmbType.MyLinkLable1 = Nothing
        Me.cmbType.MyLinkLable2 = Nothing
        Me.cmbType.Name = "cmbType"
        Me.cmbType.ReferenceFieldDesc = Nothing
        Me.cmbType.ReferenceFieldName = Nothing
        Me.cmbType.ReferenceTableName = Nothing
        Me.cmbType.Size = New System.Drawing.Size(127, 18)
        Me.cmbType.TabIndex = 2
        '
        'txtSrcDesc
        '
        Me.txtSrcDesc.CalculationExpression = Nothing
        Me.txtSrcDesc.FieldCode = Nothing
        Me.txtSrcDesc.FieldDesc = Nothing
        Me.txtSrcDesc.FieldMaxLength = 0
        Me.txtSrcDesc.FieldName = Nothing
        Me.txtSrcDesc.isCalculatedField = False
        Me.txtSrcDesc.IsSourceFromTable = False
        Me.txtSrcDesc.IsSourceFromValueList = False
        Me.txtSrcDesc.IsUnique = False
        Me.txtSrcDesc.Location = New System.Drawing.Point(249, 63)
        Me.txtSrcDesc.MaxLength = 150
        Me.txtSrcDesc.MendatroryField = False
        Me.txtSrcDesc.MyLinkLable1 = Nothing
        Me.txtSrcDesc.MyLinkLable2 = Nothing
        Me.txtSrcDesc.Name = "txtSrcDesc"
        Me.txtSrcDesc.ReadOnly = True
        Me.txtSrcDesc.ReferenceFieldDesc = Nothing
        Me.txtSrcDesc.ReferenceFieldName = Nothing
        Me.txtSrcDesc.ReferenceTableName = Nothing
        Me.txtSrcDesc.Size = New System.Drawing.Size(298, 20)
        Me.txtSrcDesc.TabIndex = 50
        Me.txtSrcDesc.TabStop = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(280, 13)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 13
        Me.RadLabel3.Text = "To Date"
        '
        'fndSrcCode
        '
        Me.fndSrcCode.CalculationExpression = Nothing
        Me.fndSrcCode.FieldCode = Nothing
        Me.fndSrcCode.FieldDesc = Nothing
        Me.fndSrcCode.FieldMaxLength = 0
        Me.fndSrcCode.FieldName = Nothing
        Me.fndSrcCode.isCalculatedField = False
        Me.fndSrcCode.IsSourceFromTable = False
        Me.fndSrcCode.IsSourceFromValueList = False
        Me.fndSrcCode.IsUnique = False
        Me.fndSrcCode.Location = New System.Drawing.Point(119, 63)
        Me.fndSrcCode.MendatroryField = True
        Me.fndSrcCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSrcCode.MyLinkLable1 = Nothing
        Me.fndSrcCode.MyLinkLable2 = Nothing
        Me.fndSrcCode.MyReadOnly = False
        Me.fndSrcCode.MyShowMasterFormButton = False
        Me.fndSrcCode.Name = "fndSrcCode"
        Me.fndSrcCode.ReferenceFieldDesc = Nothing
        Me.fndSrcCode.ReferenceFieldName = Nothing
        Me.fndSrcCode.ReferenceTableName = Nothing
        Me.fndSrcCode.Size = New System.Drawing.Size(128, 19)
        Me.fndSrcCode.TabIndex = 3
        Me.fndSrcCode.Value = ""
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(12, 39)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel9.TabIndex = 55
        Me.RadLabel9.Text = "Select By"
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CalculationExpression = Nothing
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.FieldCode = Nothing
        Me.dtpfromdate.FieldDesc = Nothing
        Me.dtpfromdate.FieldMaxLength = 0
        Me.dtpfromdate.FieldName = Nothing
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.isCalculatedField = False
        Me.dtpfromdate.IsSourceFromTable = False
        Me.dtpfromdate.IsSourceFromValueList = False
        Me.dtpfromdate.IsUnique = False
        Me.dtpfromdate.Location = New System.Drawing.Point(119, 13)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.ReferenceFieldDesc = Nothing
        Me.dtpfromdate.ReferenceFieldName = Nothing
        Me.dtpfromdate.ReferenceTableName = Nothing
        Me.dtpfromdate.Size = New System.Drawing.Size(127, 20)
        Me.dtpfromdate.TabIndex = 0
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "14-09-2011"
        Me.dtpfromdate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Size = New System.Drawing.Size(1078, 366)
        Me.SplitContainer2.SplitterDistance = 330
        Me.SplitContainer2.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1078, 330)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        Me.RadPageView1.ThemeName = "ControlDefault"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.newgv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(63.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1057, 282)
        Me.RadPageViewPage1.Text = "Summary"
        '
        'newgv1
        '
        Me.newgv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.newgv1.Location = New System.Drawing.Point(0, 0)
        '
        'newgv1
        '
        Me.newgv1.MasterTemplate.AllowAddNewRow = False
        Me.newgv1.MasterTemplate.EnableFiltering = True
        Me.newgv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.newgv1.Name = "newgv1"
        Me.newgv1.ShowHeaderCellButtons = True
        Me.newgv1.Size = New System.Drawing.Size(1057, 282)
        Me.newgv1.TabIndex = 0
        Me.newgv1.TabStop = False
        Me.newgv1.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1057, 282)
        Me.RadPageViewPage2.Text = "Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1057, 282)
        Me.gv1.TabIndex = 59
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(88, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(79, 19)
        Me.btnExport.TabIndex = 83
        Me.btnExport.Text = "Export"
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
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 0
        Me.btnreset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(996, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1078, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1078, 512)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Settings"
        Me.RadMenuItem1.AccessibleName = "Settings"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem4.AccessibleName = "Delete Layout"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'FrmPurchaseHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 512)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Name = "FrmPurchaseHistory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPurchaseHistory"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.newgv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.newgv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents fndSrcCode As common.UserControls.txtFinder
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents txtSrcDesc As common.Controls.MyTextBox
    Friend WithEvents cmbType As common.Controls.MyComboBox
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents newgv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReport As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents multiLocationFinder As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

