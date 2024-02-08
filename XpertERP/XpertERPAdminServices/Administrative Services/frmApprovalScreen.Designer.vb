<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApprovalScreen
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.chkAllLevelApprovalRequired = New common.Controls.MyCheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.ddl_category = New common.Controls.MyComboBox()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.chk_isdepartmentwise = New common.Controls.MyCheckBox()
        Me.ChkAutoPost = New common.Controls.MyCheckBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtLevelNo = New common.MyNumBox()
        Me.lblLevel = New common.Controls.MyLabel()
        Me.cboApprovalType = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblModule = New common.Controls.MyLabel()
        Me.cboModule = New common.Controls.MyComboBox()
        Me.lblTransaction = New common.Controls.MyLabel()
        Me.cboTransaction = New common.Controls.MyComboBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllLevelApprovalRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_isdepartmentwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAutoPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevelNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboApprovalType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(850, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(923, 317)
        Me.SplitContainer1.SplitterDistance = 288
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(1, 1)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkAllLevelApprovalRequired)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chk_isdepartmentwise)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkAutoPost)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLevelNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboApprovalType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLevel)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblModule)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboModule)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTransaction)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboTransaction)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(921, 286)
        Me.SplitContainer2.SplitterDistance = 72
        Me.SplitContainer2.TabIndex = 25
        '
        'RadButton1
        '
        Me.RadButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Image = Global.XpertERPAdminServices.My.Resources.Resources.Detail
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(477, 47)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(16, 20)
        Me.RadButton1.TabIndex = 500
        '
        'chkAllLevelApprovalRequired
        '
        Me.chkAllLevelApprovalRequired.Location = New System.Drawing.Point(752, 48)
        Me.chkAllLevelApprovalRequired.MyLinkLable1 = Nothing
        Me.chkAllLevelApprovalRequired.MyLinkLable2 = Nothing
        Me.chkAllLevelApprovalRequired.Name = "chkAllLevelApprovalRequired"
        Me.chkAllLevelApprovalRequired.Size = New System.Drawing.Size(158, 18)
        Me.chkAllLevelApprovalRequired.TabIndex = 28
        Me.chkAllLevelApprovalRequired.Tag1 = Nothing
        Me.chkAllLevelApprovalRequired.Text = "All Level Approval Required"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadLabel15)
        Me.Panel1.Controls.Add(Me.ddl_category)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.MyLabel39)
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(851, 21)
        Me.Panel1.TabIndex = 4
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(3, 2)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 34
        Me.RadLabel15.Text = "Location"
        '
        'ddl_category
        '
        Me.ddl_category.AutoCompleteDisplayMember = Nothing
        Me.ddl_category.AutoCompleteValueMember = Nothing
        Me.ddl_category.CalculationExpression = Nothing
        Me.ddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_category.FieldCode = Nothing
        Me.ddl_category.FieldDesc = Nothing
        Me.ddl_category.FieldMaxLength = 0
        Me.ddl_category.FieldName = Nothing
        Me.ddl_category.isCalculatedField = False
        Me.ddl_category.IsSourceFromTable = False
        Me.ddl_category.IsSourceFromValueList = False
        Me.ddl_category.IsUnique = False
        Me.ddl_category.Location = New System.Drawing.Point(439, 0)
        Me.ddl_category.MendatroryField = True
        Me.ddl_category.MyLinkLable1 = Me.MyLabel39
        Me.ddl_category.MyLinkLable2 = Nothing
        Me.ddl_category.Name = "ddl_category"
        Me.ddl_category.ReferenceFieldDesc = Nothing
        Me.ddl_category.ReferenceFieldName = Nothing
        Me.ddl_category.ReferenceTableName = Nothing
        Me.ddl_category.Size = New System.Drawing.Size(406, 20)
        Me.ddl_category.TabIndex = 74
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(359, 2)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel39.TabIndex = 75
        Me.MyLabel39.Text = "Category"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(97, 1)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(255, 19)
        Me.txtLocation.TabIndex = 33
        Me.txtLocation.Value = ""
        '
        'chk_isdepartmentwise
        '
        Me.chk_isdepartmentwise.Location = New System.Drawing.Point(631, 48)
        Me.chk_isdepartmentwise.MyLinkLable1 = Nothing
        Me.chk_isdepartmentwise.MyLinkLable2 = Nothing
        Me.chk_isdepartmentwise.Name = "chk_isdepartmentwise"
        Me.chk_isdepartmentwise.Size = New System.Drawing.Size(118, 18)
        Me.chk_isdepartmentwise.TabIndex = 27
        Me.chk_isdepartmentwise.Tag1 = Nothing
        Me.chk_isdepartmentwise.Text = "Is Department Wise"
        Me.chk_isdepartmentwise.Visible = False
        '
        'ChkAutoPost
        '
        Me.ChkAutoPost.Location = New System.Drawing.Point(560, 48)
        Me.ChkAutoPost.MyLinkLable1 = Nothing
        Me.ChkAutoPost.MyLinkLable2 = Nothing
        Me.ChkAutoPost.Name = "ChkAutoPost"
        Me.ChkAutoPost.Size = New System.Drawing.Size(69, 18)
        Me.ChkAutoPost.TabIndex = 26
        Me.ChkAutoPost.Tag1 = Nothing
        Me.ChkAutoPost.Text = "Auto Post"
        '
        'btnGo
        '
        Me.btnGo.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(494, 47)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(64, 20)
        Me.btnGo.TabIndex = 4
        Me.btnGo.Text = ">>>>"
        '
        'txtLevelNo
        '
        Me.txtLevelNo.CalculationExpression = Nothing
        Me.txtLevelNo.DecimalPlaces = 0
        Me.txtLevelNo.FieldCode = Nothing
        Me.txtLevelNo.FieldDesc = Nothing
        Me.txtLevelNo.FieldMaxLength = 0
        Me.txtLevelNo.FieldName = Nothing
        Me.txtLevelNo.isCalculatedField = False
        Me.txtLevelNo.IsSourceFromTable = False
        Me.txtLevelNo.IsSourceFromValueList = False
        Me.txtLevelNo.IsUnique = False
        Me.txtLevelNo.Location = New System.Drawing.Point(439, 47)
        Me.txtLevelNo.MendatroryField = True
        Me.txtLevelNo.MyLinkLable1 = Me.lblLevel
        Me.txtLevelNo.MyLinkLable2 = Nothing
        Me.txtLevelNo.Name = "txtLevelNo"
        Me.txtLevelNo.ReferenceFieldDesc = Nothing
        Me.txtLevelNo.ReferenceFieldName = Nothing
        Me.txtLevelNo.ReferenceTableName = Nothing
        Me.txtLevelNo.Size = New System.Drawing.Size(37, 20)
        Me.txtLevelNo.TabIndex = 3
        Me.txtLevelNo.Text = "0"
        Me.txtLevelNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevelNo.Value = 0.0R
        '
        'lblLevel
        '
        Me.lblLevel.FieldName = Nothing
        Me.lblLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel.Location = New System.Drawing.Point(359, 49)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(69, 16)
        Me.lblLevel.TabIndex = 23
        Me.lblLevel.Text = "No of Levels"
        '
        'cboApprovalType
        '
        Me.cboApprovalType.AutoCompleteDisplayMember = Nothing
        Me.cboApprovalType.AutoCompleteValueMember = Nothing
        Me.cboApprovalType.CalculationExpression = Nothing
        Me.cboApprovalType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboApprovalType.Enabled = False
        Me.cboApprovalType.FieldCode = Nothing
        Me.cboApprovalType.FieldDesc = Nothing
        Me.cboApprovalType.FieldMaxLength = 0
        Me.cboApprovalType.FieldName = Nothing
        Me.cboApprovalType.isCalculatedField = False
        Me.cboApprovalType.IsSourceFromTable = False
        Me.cboApprovalType.IsSourceFromValueList = False
        Me.cboApprovalType.IsUnique = False
        RadListDataItem1.Text = "Level1"
        RadListDataItem2.Text = "Level2"
        RadListDataItem3.Text = "Level3"
        Me.cboApprovalType.Items.Add(RadListDataItem1)
        Me.cboApprovalType.Items.Add(RadListDataItem2)
        Me.cboApprovalType.Items.Add(RadListDataItem3)
        Me.cboApprovalType.Location = New System.Drawing.Point(97, 47)
        Me.cboApprovalType.MendatroryField = True
        Me.cboApprovalType.MyLinkLable1 = Me.MyLabel1
        Me.cboApprovalType.MyLinkLable2 = Nothing
        Me.cboApprovalType.Name = "cboApprovalType"
        Me.cboApprovalType.ReferenceFieldDesc = Nothing
        Me.cboApprovalType.ReferenceFieldName = Nothing
        Me.cboApprovalType.ReferenceTableName = Nothing
        Me.cboApprovalType.Size = New System.Drawing.Size(255, 20)
        Me.cboApprovalType.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 49)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel1.TabIndex = 25
        Me.MyLabel1.Text = "Approval Type"
        '
        'lblModule
        '
        Me.lblModule.FieldName = Nothing
        Me.lblModule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModule.Location = New System.Drawing.Point(3, 8)
        Me.lblModule.Name = "lblModule"
        Me.lblModule.Size = New System.Drawing.Size(43, 16)
        Me.lblModule.TabIndex = 20
        Me.lblModule.Text = "Module"
        '
        'cboModule
        '
        Me.cboModule.AutoCompleteDisplayMember = Nothing
        Me.cboModule.AutoCompleteValueMember = Nothing
        Me.cboModule.CalculationExpression = Nothing
        Me.cboModule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModule.FieldCode = Nothing
        Me.cboModule.FieldDesc = Nothing
        Me.cboModule.FieldMaxLength = 0
        Me.cboModule.FieldName = Nothing
        Me.cboModule.isCalculatedField = False
        Me.cboModule.IsSourceFromTable = False
        Me.cboModule.IsSourceFromValueList = False
        Me.cboModule.IsUnique = False
        Me.cboModule.Location = New System.Drawing.Point(97, 6)
        Me.cboModule.MendatroryField = True
        Me.cboModule.MyLinkLable1 = Me.lblModule
        Me.cboModule.MyLinkLable2 = Nothing
        Me.cboModule.Name = "cboModule"
        Me.cboModule.ReferenceFieldDesc = Nothing
        Me.cboModule.ReferenceFieldName = Nothing
        Me.cboModule.ReferenceTableName = Nothing
        Me.cboModule.Size = New System.Drawing.Size(255, 20)
        Me.cboModule.TabIndex = 0
        '
        'lblTransaction
        '
        Me.lblTransaction.FieldName = Nothing
        Me.lblTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransaction.Location = New System.Drawing.Point(359, 8)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(65, 16)
        Me.lblTransaction.TabIndex = 22
        Me.lblTransaction.Text = "Transaction"
        '
        'cboTransaction
        '
        Me.cboTransaction.AutoCompleteDisplayMember = Nothing
        Me.cboTransaction.AutoCompleteValueMember = Nothing
        Me.cboTransaction.CalculationExpression = Nothing
        Me.cboTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransaction.FieldCode = Nothing
        Me.cboTransaction.FieldDesc = Nothing
        Me.cboTransaction.FieldMaxLength = 0
        Me.cboTransaction.FieldName = Nothing
        Me.cboTransaction.isCalculatedField = False
        Me.cboTransaction.IsSourceFromTable = False
        Me.cboTransaction.IsSourceFromValueList = False
        Me.cboTransaction.IsUnique = False
        Me.cboTransaction.Location = New System.Drawing.Point(439, 6)
        Me.cboTransaction.MendatroryField = True
        Me.cboTransaction.MyLinkLable1 = Me.lblTransaction
        Me.cboTransaction.MyLinkLable2 = Nothing
        Me.cboTransaction.Name = "cboTransaction"
        Me.cboTransaction.ReferenceFieldDesc = Nothing
        Me.cboTransaction.ReferenceFieldName = Nothing
        Me.cboTransaction.ReferenceTableName = Nothing
        Me.cboTransaction.Size = New System.Drawing.Size(406, 20)
        Me.cboTransaction.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Level Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(919, 208)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Level Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(899, 178)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnReset
        '
        Me.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(143, 1)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(69, 22)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(923, 20)
        Me.RadMenu1.TabIndex = 24
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
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
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Import"
        Me.RadMenuItem4.AccessibleName = "Import"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        '
        'frmApprovalScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(923, 337)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmApprovalScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Approval Screen"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllLevelApprovalRequired, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_isdepartmentwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAutoPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevelNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboApprovalType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cboTransaction As common.Controls.MyComboBox
    Friend WithEvents cboModule As common.Controls.MyComboBox
    Friend WithEvents lblTransaction As common.Controls.MyLabel
    Friend WithEvents lblModule As common.Controls.MyLabel
    Friend WithEvents lblLevel As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents cboApprovalType As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtLevelNo As common.MyNumBox
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkAutoPost As common.Controls.MyCheckBox
    Friend WithEvents chk_isdepartmentwise As common.Controls.MyCheckBox
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents ddl_category As common.Controls.MyComboBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkAllLevelApprovalRequired As common.Controls.MyCheckBox
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class

