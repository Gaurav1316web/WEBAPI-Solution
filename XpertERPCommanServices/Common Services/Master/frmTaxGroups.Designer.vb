<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxGroups
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
        Dim GridViewMultiComboBoxColumn1 As Telerik.WinControls.UI.GridViewMultiComboBoxColumn = New Telerik.WinControls.UI.GridViewMultiComboBoxColumn()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewMultiComboBoxColumn2 As Telerik.WinControls.UI.GridViewMultiComboBoxColumn = New Telerik.WinControls.UI.GridViewMultiComboBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.menuFile = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gbActive = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTaxExempted = New common.Controls.MyCheckBox()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkTransfer = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblTaxGroup = New common.Controls.MyLabel()
        Me.txtRowNo = New common.Controls.MyTextBox()
        Me.findTaxGroup = New common.UserControls.txtNavigator()
        Me.chkExcisable = New Telerik.WinControls.UI.RadCheckBox()
        Me.gbVS = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkSale = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkVat = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.lblDesc = New common.Controls.MyLabel()
        Me.ddlTransType = New common.Controls.MyComboBox()
        Me.lblTransType = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnAdd = New Telerik.WinControls.UI.RadButton()
        Me.pnlTaxFormula = New Telerik.WinControls.UI.RadPanel()
        Me.gvTaxGroups = New common.UserControls.MyRadGridView()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gbActive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbActive.SuspendLayout()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaxExempted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRowNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVS.SuspendLayout()
        CType(Me.chkSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlTaxFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxGroups.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(922, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'menuFile
        '
        Me.menuFile.AccessibleDescription = "File"
        Me.menuFile.AccessibleName = "File"
        Me.menuFile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.menuFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuExport, Me.menuImport, Me.menuClose})
        Me.menuFile.Name = "menuFile"
        Me.menuFile.Text = "File"
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export"
        Me.menuExport.AccessibleName = "Export"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "RadMenuItem3"
        Me.menuImport.AccessibleName = "RadMenuItem3"
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "RadMenuItem4"
        Me.menuClose.AccessibleName = "RadMenuItem4"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Exit"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gbActive)
        Me.RadGroupBox1.Controls.Add(Me.chkTaxExempted)
        Me.RadGroupBox1.Controls.Add(Me.pnlCurrConv)
        Me.RadGroupBox1.Controls.Add(Me.chkTransfer)
        Me.RadGroupBox1.Controls.Add(Me.lblTaxGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtRowNo)
        Me.RadGroupBox1.Controls.Add(Me.findTaxGroup)
        Me.RadGroupBox1.Controls.Add(Me.chkExcisable)
        Me.RadGroupBox1.Controls.Add(Me.gbVS)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.ddlTransType)
        Me.RadGroupBox1.Controls.Add(Me.lblDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblTransType)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(919, 437)
        Me.RadGroupBox1.TabIndex = 0
        '
        'gbActive
        '
        Me.gbActive.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbActive.Controls.Add(Me.chkActive)
        Me.gbActive.HeaderText = ""
        Me.gbActive.Location = New System.Drawing.Point(787, 45)
        Me.gbActive.Name = "gbActive"
        Me.gbActive.Size = New System.Drawing.Size(118, 25)
        Me.gbActive.TabIndex = 176
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(25, 3)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(51, 18)
        Me.chkActive.TabIndex = 175
        Me.chkActive.Text = "Active"
        '
        'chkTaxExempted
        '
        Me.chkTaxExempted.Location = New System.Drawing.Point(691, 49)
        Me.chkTaxExempted.MyLinkLable1 = Nothing
        Me.chkTaxExempted.MyLinkLable2 = Nothing
        Me.chkTaxExempted.Name = "chkTaxExempted"
        Me.chkTaxExempted.Size = New System.Drawing.Size(90, 18)
        Me.chkTaxExempted.TabIndex = 174
        Me.chkTaxExempted.Tag1 = Nothing
        Me.chkTaxExempted.Text = "Tax Exempted"
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.MyLabel1)
        Me.pnlCurrConv.Location = New System.Drawing.Point(687, 7)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(224, 25)
        Me.pnlCurrConv.TabIndex = 13
        Me.pnlCurrConv.Visible = False
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(61, 1)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(154, 20)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel1.TabIndex = 7
        Me.MyLabel1.Text = "Currency"
        '
        'chkTransfer
        '
        Me.chkTransfer.Location = New System.Drawing.Point(470, 8)
        Me.chkTransfer.Name = "chkTransfer"
        Me.chkTransfer.Size = New System.Drawing.Size(60, 18)
        Me.chkTransfer.TabIndex = 2
        Me.chkTransfer.Text = "Transfer"
        '
        'lblTaxGroup
        '
        Me.lblTaxGroup.FieldName = Nothing
        Me.lblTaxGroup.Location = New System.Drawing.Point(19, 8)
        Me.lblTaxGroup.Name = "lblTaxGroup"
        Me.lblTaxGroup.Size = New System.Drawing.Size(61, 18)
        Me.lblTaxGroup.TabIndex = 9
        Me.lblTaxGroup.Text = "Tax Group "
        '
        'txtRowNo
        '
        Me.txtRowNo.CalculationExpression = Nothing
        Me.txtRowNo.FieldCode = Nothing
        Me.txtRowNo.FieldDesc = Nothing
        Me.txtRowNo.FieldMaxLength = 0
        Me.txtRowNo.FieldName = Nothing
        Me.txtRowNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRowNo.isCalculatedField = False
        Me.txtRowNo.IsSourceFromTable = False
        Me.txtRowNo.IsSourceFromValueList = False
        Me.txtRowNo.IsUnique = False
        Me.txtRowNo.Location = New System.Drawing.Point(388, 29)
        Me.txtRowNo.MaxLength = 50
        Me.txtRowNo.MendatroryField = False
        Me.txtRowNo.MyLinkLable1 = Nothing
        Me.txtRowNo.MyLinkLable2 = Nothing
        Me.txtRowNo.Name = "txtRowNo"
        Me.txtRowNo.ReferenceFieldDesc = Nothing
        Me.txtRowNo.ReferenceFieldName = Nothing
        Me.txtRowNo.ReferenceTableName = Nothing
        Me.txtRowNo.Size = New System.Drawing.Size(161, 18)
        Me.txtRowNo.TabIndex = 5
        Me.txtRowNo.Text = "This Text Box Is Invisibly Usable"
        '
        'findTaxGroup
        '
        Me.findTaxGroup.FieldName = Nothing
        Me.findTaxGroup.Location = New System.Drawing.Point(116, 7)
        Me.findTaxGroup.MendatroryField = True
        Me.findTaxGroup.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.findTaxGroup.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.findTaxGroup.MyLinkLable1 = Me.lblTaxGroup
        Me.findTaxGroup.MyLinkLable2 = Nothing
        Me.findTaxGroup.MyMaxLength = 32767
        Me.findTaxGroup.MyReadOnly = False
        Me.findTaxGroup.Name = "findTaxGroup"
        Me.findTaxGroup.Size = New System.Drawing.Size(246, 20)
        Me.findTaxGroup.TabIndex = 0
        Me.findTaxGroup.Value = ""
        '
        'chkExcisable
        '
        Me.chkExcisable.Location = New System.Drawing.Point(398, 8)
        Me.chkExcisable.Name = "chkExcisable"
        Me.chkExcisable.Size = New System.Drawing.Size(65, 18)
        Me.chkExcisable.TabIndex = 2
        Me.chkExcisable.Text = "Excisable"
        '
        'gbVS
        '
        Me.gbVS.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVS.Controls.Add(Me.chkSale)
        Me.gbVS.Controls.Add(Me.chkVat)
        Me.gbVS.HeaderText = ""
        Me.gbVS.Location = New System.Drawing.Point(553, 7)
        Me.gbVS.Name = "gbVS"
        Me.gbVS.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVS.Size = New System.Drawing.Size(128, 37)
        Me.gbVS.TabIndex = 3
        Me.gbVS.Visible = False
        '
        'chkSale
        '
        Me.chkSale.Location = New System.Drawing.Point(56, 10)
        Me.chkSale.Name = "chkSale"
        Me.chkSale.Size = New System.Drawing.Size(61, 18)
        Me.chkSale.TabIndex = 1
        Me.chkSale.Text = "Sale Tax"
        '
        'chkVat
        '
        Me.chkVat.Location = New System.Drawing.Point(13, 10)
        Me.chkVat.Name = "chkVat"
        Me.chkVat.Size = New System.Drawing.Size(37, 18)
        Me.chkVat.TabIndex = 0
        Me.chkVat.Text = "Vat"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(362, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 20)
        Me.btnReset.TabIndex = 1
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(116, 49)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.lblDesc
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(565, 18)
        Me.txtDesc.TabIndex = 6
        '
        'lblDesc
        '
        Me.lblDesc.FieldName = Nothing
        Me.lblDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(19, 48)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblDesc.TabIndex = 7
        Me.lblDesc.Text = "Description"
        '
        'ddlTransType
        '
        Me.ddlTransType.AutoCompleteDisplayMember = Nothing
        Me.ddlTransType.AutoCompleteValueMember = Nothing
        Me.ddlTransType.CalculationExpression = Nothing
        Me.ddlTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTransType.FieldCode = Nothing
        Me.ddlTransType.FieldDesc = Nothing
        Me.ddlTransType.FieldMaxLength = 0
        Me.ddlTransType.FieldName = Nothing
        Me.ddlTransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlTransType.isCalculatedField = False
        Me.ddlTransType.IsSourceFromTable = False
        Me.ddlTransType.IsSourceFromValueList = False
        Me.ddlTransType.IsUnique = False
        Me.ddlTransType.Location = New System.Drawing.Point(116, 29)
        Me.ddlTransType.MendatroryField = False
        Me.ddlTransType.MyLinkLable1 = Me.lblTransType
        Me.ddlTransType.MyLinkLable2 = Nothing
        Me.ddlTransType.Name = "ddlTransType"
        Me.ddlTransType.ReferenceFieldDesc = Nothing
        Me.ddlTransType.ReferenceFieldName = Nothing
        Me.ddlTransType.ReferenceTableName = Nothing
        Me.ddlTransType.Size = New System.Drawing.Size(267, 18)
        Me.ddlTransType.TabIndex = 4
        '
        'lblTransType
        '
        Me.lblTransType.FieldName = Nothing
        Me.lblTransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransType.Location = New System.Drawing.Point(19, 29)
        Me.lblTransType.Name = "lblTransType"
        Me.lblTransType.Size = New System.Drawing.Size(94, 16)
        Me.lblTransType.TabIndex = 8
        Me.lblTransType.Text = "Transaction Type"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(851, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(3, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Save"
        '
        'pnlTaxFormula
        '
        Me.pnlTaxFormula.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTaxFormula.Location = New System.Drawing.Point(3, 207)
        Me.pnlTaxFormula.Name = "pnlTaxFormula"
        Me.pnlTaxFormula.Size = New System.Drawing.Size(889, 110)
        Me.pnlTaxFormula.TabIndex = 0
        Me.pnlTaxFormula.TabStop = False
        Me.pnlTaxFormula.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'gvTaxGroups
        '
        Me.gvTaxGroups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvTaxGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvTaxGroups.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTaxGroups.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTaxGroups.ForeColor = System.Drawing.Color.Black
        Me.gvTaxGroups.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTaxGroups.Location = New System.Drawing.Point(3, 10)
        '
        'gvTaxGroups
        '
        Me.gvTaxGroups.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvTaxGroups.MasterTemplate.AutoGenerateColumns = False
        GridViewMultiComboBoxColumn1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        GridViewMultiComboBoxColumn1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown
        GridViewMultiComboBoxColumn1.FieldName = "Tax_Code"
        GridViewMultiComboBoxColumn1.HeaderText = "Tax Authority"
        GridViewMultiComboBoxColumn1.Name = "taxAuthority"
        GridViewMultiComboBoxColumn1.Width = 200
        GridViewTextBoxColumn1.FieldName = "Tax_Code_Desc"
        GridViewTextBoxColumn1.HeaderText = "Description"
        GridViewTextBoxColumn1.Name = "Description"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 160
        GridViewTextBoxColumn2.FieldName = "Taxable"
        GridViewTextBoxColumn2.HeaderText = "Taxable"
        GridViewTextBoxColumn2.Name = "taxable"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn3.FieldName = "Surtax"
        GridViewTextBoxColumn3.HeaderText = "Surcharge"
        GridViewTextBoxColumn3.Name = "surtax"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 62
        GridViewMultiComboBoxColumn2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        GridViewMultiComboBoxColumn2.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown
        GridViewMultiComboBoxColumn2.FieldName = "Surtax_Tax_Code"
        GridViewMultiComboBoxColumn2.HeaderText = "Surcharge on Authority"
        GridViewMultiComboBoxColumn2.Name = "surtaxonAuthority"
        GridViewMultiComboBoxColumn2.Width = 122
        GridViewTextBoxColumn4.FieldName = "Surtax_Tax_Code_Desc"
        GridViewTextBoxColumn4.HeaderText = "Surcharge Description"
        GridViewTextBoxColumn4.Name = "surtaxDesc"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 160
        GridViewTextBoxColumn5.HeaderText = "Currency Code"
        GridViewTextBoxColumn5.IsVisible = False
        GridViewTextBoxColumn5.Name = "CurrencyCode"
        GridViewTextBoxColumn5.Width = 100
        GridViewTextBoxColumn6.HeaderText = "Conversion Rate"
        GridViewTextBoxColumn6.IsVisible = False
        GridViewTextBoxColumn6.Name = "ConvRate"
        GridViewTextBoxColumn6.Width = 100
        GridViewTextBoxColumn7.HeaderText = "Applicable From"
        GridViewTextBoxColumn7.IsVisible = False
        GridViewTextBoxColumn7.Name = "ApplicableFrom"
        GridViewTextBoxColumn7.Width = 100
        GridViewTextBoxColumn8.HeaderText = "Tax On Base Amount"
        GridViewTextBoxColumn8.Name = "TaxOnBaseAmount"
        GridViewTextBoxColumn8.ReadOnly = True
        GridViewTextBoxColumn8.Width = 150
        Me.gvTaxGroups.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewMultiComboBoxColumn1, GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewMultiComboBoxColumn2, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8})
        Me.gvTaxGroups.MasterTemplate.EnableGrouping = False
        Me.gvTaxGroups.MasterTemplate.EnableSorting = False
        Me.gvTaxGroups.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTaxGroups.Name = "gvTaxGroups"
        Me.gvTaxGroups.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTaxGroups.ShowGroupPanel = False
        Me.gvTaxGroups.ShowHeaderCellButtons = True
        Me.gvTaxGroups.Size = New System.Drawing.Size(889, 191)
        Me.gvTaxGroups.TabIndex = 0
        Me.gvTaxGroups.TabStop = False
        Me.gvTaxGroups.Text = "RadGridView1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 74)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(916, 366)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gvTaxGroups)
        Me.RadPageViewPage1.Controls.Add(Me.pnlTaxFormula)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(91.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(895, 318)
        Me.RadPageViewPage1.Text = "Tax Authorities"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(895, 318)
        Me.RadPageViewPage2.Text = "Other Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(5, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(671, 305)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(651, 275)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(922, 472)
        Me.SplitContainer1.SplitterDistance = 443
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmTaxGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 492)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmTaxGroups"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tax Groups"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.gbActive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbActive.ResumeLayout(False)
        Me.gbActive.PerformLayout()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaxExempted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRowNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVS.ResumeLayout(False)
        Me.gbVS.PerformLayout()
        CType(Me.chkSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlTaxFormula, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxGroups.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents menuFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvTaxGroups As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents ddlTransType As common.Controls.MyComboBox
    Friend WithEvents lblDesc As common.Controls.MyLabel
    Friend WithEvents lblTransType As common.Controls.MyLabel
    Friend WithEvents pnlTaxFormula As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gbVS As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkSale As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkVat As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkExcisable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents findTaxGroup As common.UserControls.txtNavigator
    Friend WithEvents txtRowNo As common.Controls.MyTextBox
    Friend WithEvents lblTaxGroup As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkTransfer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkTaxExempted As common.Controls.MyCheckBox
    Friend WithEvents chkActive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gbActive As Telerik.WinControls.UI.RadGroupBox
End Class

