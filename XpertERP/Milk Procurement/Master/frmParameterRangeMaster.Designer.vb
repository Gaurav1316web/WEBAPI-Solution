<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmParameterRangeMaster
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnGO = New Telerik.WinControls.UI.RadButton()
        Me.lblProcType = New common.Controls.MyLabel()
        Me.ddlBulProcType = New common.Controls.MyComboBox()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.lblVendorClass = New common.Controls.MyLabel()
        Me.cmbVendorClass = New common.Controls.MyComboBox()
        Me.txtMilktypeCode = New common.UserControls.txtFinder()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProcType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBulProcType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendorClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1140, 451)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.SplitContainer2.Size = New System.Drawing.Size(1134, 408)
        Me.SplitContainer2.SplitterDistance = 28
        Me.SplitContainer2.TabIndex = 19
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(5, 5)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1124, 20)
        Me.RadMenu1.TabIndex = 17
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnGO)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblProcType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlBulProcType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblSRNDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpDocDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtLocName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndLoc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorClass)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cmbVendorClass)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMilktypeCode)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1122, 364)
        Me.SplitContainer3.SplitterDistance = 56
        Me.SplitContainer3.TabIndex = 1
        '
        'btnGO
        '
        Me.btnGO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGO.Location = New System.Drawing.Point(834, 4)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(53, 18)
        Me.btnGO.TabIndex = 112
        Me.btnGO.Text = ">>"
        '
        'lblProcType
        '
        Me.lblProcType.FieldName = Nothing
        Me.lblProcType.Location = New System.Drawing.Point(906, 5)
        Me.lblProcType.Name = "lblProcType"
        Me.lblProcType.Size = New System.Drawing.Size(98, 18)
        Me.lblProcType.TabIndex = 282
        Me.lblProcType.Text = "Procurement Type"
        '
        'ddlBulProcType
        '
        Me.ddlBulProcType.AutoCompleteDisplayMember = Nothing
        Me.ddlBulProcType.AutoCompleteValueMember = Nothing
        Me.ddlBulProcType.CalculationExpression = Nothing
        Me.ddlBulProcType.DropDownAnimationEnabled = True
        Me.ddlBulProcType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlBulProcType.FieldCode = Nothing
        Me.ddlBulProcType.FieldDesc = Nothing
        Me.ddlBulProcType.FieldMaxLength = 0
        Me.ddlBulProcType.FieldName = Nothing
        Me.ddlBulProcType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlBulProcType.isCalculatedField = False
        Me.ddlBulProcType.IsSourceFromTable = False
        Me.ddlBulProcType.IsSourceFromValueList = False
        Me.ddlBulProcType.IsUnique = False
        Me.ddlBulProcType.Location = New System.Drawing.Point(1006, 4)
        Me.ddlBulProcType.MendatroryField = False
        Me.ddlBulProcType.MyLinkLable1 = Me.lblProcType
        Me.ddlBulProcType.MyLinkLable2 = Nothing
        Me.ddlBulProcType.Name = "ddlBulProcType"
        Me.ddlBulProcType.ReferenceFieldDesc = Nothing
        Me.ddlBulProcType.ReferenceFieldName = Nothing
        Me.ddlBulProcType.ReferenceTableName = Nothing
        Me.ddlBulProcType.Size = New System.Drawing.Size(113, 18)
        Me.ddlBulProcType.TabIndex = 281
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(651, 6)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(77, 16)
        Me.lblSRNDate.TabIndex = 114
        Me.lblSRNDate.Text = "Effective Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(731, 4)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblSRNDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(90, 18)
        Me.dtpDocDate.TabIndex = 113
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(4, 7)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel1.TabIndex = 111
        Me.MyLabel1.Text = "Location"
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(190, 5)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(243, 20)
        Me.txtLocName.TabIndex = 110
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(56, 6)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Nothing
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(132, 19)
        Me.fndLoc.TabIndex = 109
        Me.fndLoc.Value = ""
        '
        'lblVendorClass
        '
        Me.lblVendorClass.FieldName = Nothing
        Me.lblVendorClass.Location = New System.Drawing.Point(436, 6)
        Me.lblVendorClass.Name = "lblVendorClass"
        Me.lblVendorClass.Size = New System.Drawing.Size(71, 18)
        Me.lblVendorClass.TabIndex = 27
        Me.lblVendorClass.Text = "Vendor Class"
        '
        'cmbVendorClass
        '
        Me.cmbVendorClass.AutoCompleteDisplayMember = Nothing
        Me.cmbVendorClass.AutoCompleteValueMember = Nothing
        Me.cmbVendorClass.CalculationExpression = Nothing
        Me.cmbVendorClass.DropDownAnimationEnabled = True
        Me.cmbVendorClass.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbVendorClass.FieldCode = Nothing
        Me.cmbVendorClass.FieldDesc = Nothing
        Me.cmbVendorClass.FieldMaxLength = 0
        Me.cmbVendorClass.FieldName = Nothing
        Me.cmbVendorClass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbVendorClass.isCalculatedField = False
        Me.cmbVendorClass.IsSourceFromTable = False
        Me.cmbVendorClass.IsSourceFromValueList = False
        Me.cmbVendorClass.IsUnique = False
        Me.cmbVendorClass.Location = New System.Drawing.Point(524, 5)
        Me.cmbVendorClass.MendatroryField = False
        Me.cmbVendorClass.MyLinkLable1 = Me.lblVendorClass
        Me.cmbVendorClass.MyLinkLable2 = Nothing
        Me.cmbVendorClass.Name = "cmbVendorClass"
        Me.cmbVendorClass.ReferenceFieldDesc = Nothing
        Me.cmbVendorClass.ReferenceFieldName = Nothing
        Me.cmbVendorClass.ReferenceTableName = Nothing
        Me.cmbVendorClass.Size = New System.Drawing.Size(119, 18)
        Me.cmbVendorClass.TabIndex = 26
        '
        'txtMilktypeCode
        '
        Me.txtMilktypeCode.CalculationExpression = Nothing
        Me.txtMilktypeCode.FieldCode = Nothing
        Me.txtMilktypeCode.FieldDesc = Nothing
        Me.txtMilktypeCode.FieldMaxLength = 0
        Me.txtMilktypeCode.FieldName = Nothing
        Me.txtMilktypeCode.isCalculatedField = False
        Me.txtMilktypeCode.IsSourceFromTable = False
        Me.txtMilktypeCode.IsSourceFromValueList = False
        Me.txtMilktypeCode.IsUnique = False
        Me.txtMilktypeCode.Location = New System.Drawing.Point(524, 4)
        Me.txtMilktypeCode.MendatroryField = True
        Me.txtMilktypeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilktypeCode.MyLinkLable1 = Nothing
        Me.txtMilktypeCode.MyLinkLable2 = Nothing
        Me.txtMilktypeCode.MyReadOnly = False
        Me.txtMilktypeCode.MyShowMasterFormButton = False
        Me.txtMilktypeCode.Name = "txtMilktypeCode"
        Me.txtMilktypeCode.ReferenceFieldDesc = Nothing
        Me.txtMilktypeCode.ReferenceFieldName = Nothing
        Me.txtMilktypeCode.ReferenceTableName = Nothing
        Me.txtMilktypeCode.Size = New System.Drawing.Size(119, 19)
        Me.txtMilktypeCode.TabIndex = 280
        Me.txtMilktypeCode.Value = ""
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1122, 304)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1101, 256)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Parameter Range Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(3, 18, 3, 3)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1101, 256)
        Me.RadGroupBox1.TabIndex = 18
        Me.RadGroupBox1.Text = "Parameter Range Detail"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1095, 235)
        Me.gv.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(812, 316)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(812, 316)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1050, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(95, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(11, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'FrmParameterRangeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1140, 451)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmParameterRangeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmParameterRangeMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProcType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBulProcType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVendorClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblVendorClass As common.Controls.MyLabel
    Protected WithEvents cmbVendorClass As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents btnGO As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtMilktypeCode As common.UserControls.txtFinder
    Friend WithEvents lblProcType As common.Controls.MyLabel
    Protected WithEvents ddlBulProcType As common.Controls.MyComboBox
End Class

