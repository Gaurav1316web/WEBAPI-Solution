Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiscountMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDiscountMaster))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtdiscountcategory = New common.UserControls.txtFinder()
        Me.chkIsOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtCategoryDesc = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnSampling = New Telerik.WinControls.UI.RadRadioButton()
        Me.chksku = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnVSNDType = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnOther = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDiscount = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtGlAccDesc = New common.Controls.MyLabel()
        Me.txtGLAccount = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCategoryDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnSampling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chksku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnVSNDType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGlAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.txtGlAccDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtGLAccount)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(648, 448)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadLabel6)
        Me.Panel1.Controls.Add(Me.txtdiscountcategory)
        Me.Panel1.Controls.Add(Me.chkIsOpening)
        Me.Panel1.Controls.Add(Me.txtCategoryDesc)
        Me.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.Panel1.Controls.Add(Me.RadLabel7)
        Me.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.Panel1.Location = New System.Drawing.Point(3, 109)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(645, 326)
        Me.Panel1.TabIndex = 53
        Me.Panel1.Visible = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Location = New System.Drawing.Point(5, 3)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(128, 18)
        Me.RadLabel6.TabIndex = 24
        Me.RadLabel6.Text = "Discount Category Code"
        '
        'txtdiscountcategory
        '
        Me.txtdiscountcategory.CalculationExpression = Nothing
        Me.txtdiscountcategory.FieldCode = Nothing
        Me.txtdiscountcategory.FieldDesc = Nothing
        Me.txtdiscountcategory.FieldMaxLength = 0
        Me.txtdiscountcategory.FieldName = Nothing
        Me.txtdiscountcategory.isCalculatedField = False
        Me.txtdiscountcategory.IsSourceFromTable = False
        Me.txtdiscountcategory.IsSourceFromValueList = False
        Me.txtdiscountcategory.IsUnique = False
        Me.txtdiscountcategory.Location = New System.Drawing.Point(139, 3)
        Me.txtdiscountcategory.MendatroryField = True
        Me.txtdiscountcategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdiscountcategory.MyLinkLable1 = Me.RadLabel6
        Me.txtdiscountcategory.MyLinkLable2 = Nothing
        Me.txtdiscountcategory.MyReadOnly = False
        Me.txtdiscountcategory.MyShowMasterFormButton = False
        Me.txtdiscountcategory.Name = "txtdiscountcategory"
        Me.txtdiscountcategory.ReferenceFieldDesc = Nothing
        Me.txtdiscountcategory.ReferenceFieldName = Nothing
        Me.txtdiscountcategory.ReferenceTableName = Nothing
        Me.txtdiscountcategory.Size = New System.Drawing.Size(222, 18)
        Me.txtdiscountcategory.TabIndex = 5
        Me.txtdiscountcategory.Value = ""
        '
        'chkIsOpening
        '
        Me.chkIsOpening.Location = New System.Drawing.Point(139, 53)
        Me.chkIsOpening.Name = "chkIsOpening"
        Me.chkIsOpening.Size = New System.Drawing.Size(74, 18)
        Me.chkIsOpening.TabIndex = 25
        Me.chkIsOpening.Text = "Is Opening"
        '
        'txtCategoryDesc
        '
        Me.txtCategoryDesc.CalculationExpression = Nothing
        Me.txtCategoryDesc.FieldCode = Nothing
        Me.txtCategoryDesc.FieldDesc = Nothing
        Me.txtCategoryDesc.FieldMaxLength = 0
        Me.txtCategoryDesc.FieldName = Nothing
        Me.txtCategoryDesc.isCalculatedField = False
        Me.txtCategoryDesc.IsSourceFromTable = False
        Me.txtCategoryDesc.IsSourceFromValueList = False
        Me.txtCategoryDesc.IsUnique = False
        Me.txtCategoryDesc.Location = New System.Drawing.Point(139, 27)
        Me.txtCategoryDesc.MaxLength = 50
        Me.txtCategoryDesc.MendatroryField = False
        Me.txtCategoryDesc.MyLinkLable1 = Me.RadLabel7
        Me.txtCategoryDesc.MyLinkLable2 = Nothing
        Me.txtCategoryDesc.Name = "txtCategoryDesc"
        Me.txtCategoryDesc.ReadOnly = True
        Me.txtCategoryDesc.ReferenceFieldDesc = Nothing
        Me.txtCategoryDesc.ReferenceFieldName = Nothing
        Me.txtCategoryDesc.ReferenceTableName = Nothing
        Me.txtCategoryDesc.Size = New System.Drawing.Size(359, 20)
        Me.txtCategoryDesc.TabIndex = 6
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Location = New System.Drawing.Point(5, 28)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel7.TabIndex = 21
        Me.RadLabel7.Text = "Description"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(5, 104)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(496, 217)
        Me.RadGroupBox4.TabIndex = 8
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
        Me.gvDB.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDB.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDB.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvDB.MyStopExport = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(476, 187)
        Me.gvDB.TabIndex = 0
        Me.gvDB.VarID = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnSampling)
        Me.RadGroupBox2.Controls.Add(Me.chksku)
        Me.RadGroupBox2.Controls.Add(Me.rbtnVSNDType)
        Me.RadGroupBox2.Controls.Add(Me.rbtnOther)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDiscount)
        Me.RadGroupBox2.HeaderText = "RadGroupBox2"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 79)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(496, 23)
        Me.RadGroupBox2.TabIndex = 7
        Me.RadGroupBox2.Text = "RadGroupBox2"
        '
        'rbtnSampling
        '
        Me.rbtnSampling.Location = New System.Drawing.Point(251, 2)
        Me.rbtnSampling.Name = "rbtnSampling"
        Me.rbtnSampling.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSampling.TabIndex = 3
        Me.rbtnSampling.Text = "Sampling"
        '
        'chksku
        '
        Me.chksku.Location = New System.Drawing.Point(415, 2)
        Me.chksku.Name = "chksku"
        Me.chksku.Size = New System.Drawing.Size(68, 18)
        Me.chksku.TabIndex = 4
        Me.chksku.Text = "SKU Wise"
        '
        'rbtnVSNDType
        '
        Me.rbtnVSNDType.Location = New System.Drawing.Point(90, 2)
        Me.rbtnVSNDType.Name = "rbtnVSNDType"
        Me.rbtnVSNDType.Size = New System.Drawing.Size(77, 18)
        Me.rbtnVSNDType.TabIndex = 1
        Me.rbtnVSNDType.Text = "VSND Type"
        '
        'rbtnOther
        '
        Me.rbtnOther.Location = New System.Drawing.Point(178, 2)
        Me.rbtnOther.Name = "rbtnOther"
        Me.rbtnOther.Size = New System.Drawing.Size(49, 18)
        Me.rbtnOther.TabIndex = 2
        Me.rbtnOther.Text = "Other"
        '
        'rbtnDiscount
        '
        Me.rbtnDiscount.Location = New System.Drawing.Point(13, 2)
        Me.rbtnDiscount.Name = "rbtnDiscount"
        Me.rbtnDiscount.Size = New System.Drawing.Size(64, 18)
        Me.rbtnDiscount.TabIndex = 0
        Me.rbtnDiscount.Text = "Discount"
        '
        'txtGlAccDesc
        '
        Me.txtGlAccDesc.AutoSize = False
        Me.txtGlAccDesc.BorderVisible = True
        Me.txtGlAccDesc.FieldName = Nothing
        Me.txtGlAccDesc.Location = New System.Drawing.Point(143, 84)
        Me.txtGlAccDesc.Name = "txtGlAccDesc"
        Me.txtGlAccDesc.Size = New System.Drawing.Size(499, 18)
        Me.txtGlAccDesc.TabIndex = 52
        '
        'txtGLAccount
        '
        Me.txtGLAccount.CalculationExpression = Nothing
        Me.txtGLAccount.FieldCode = Nothing
        Me.txtGLAccount.FieldDesc = Nothing
        Me.txtGLAccount.FieldMaxLength = 0
        Me.txtGLAccount.FieldName = Nothing
        Me.txtGLAccount.isCalculatedField = False
        Me.txtGLAccount.IsSourceFromTable = False
        Me.txtGLAccount.IsSourceFromValueList = False
        Me.txtGLAccount.IsUnique = False
        Me.txtGLAccount.Location = New System.Drawing.Point(143, 61)
        Me.txtGLAccount.MendatroryField = True
        Me.txtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount.MyLinkLable1 = Me.RadLabel5
        Me.txtGLAccount.MyLinkLable2 = Nothing
        Me.txtGLAccount.MyReadOnly = False
        Me.txtGLAccount.MyShowMasterFormButton = False
        Me.txtGLAccount.Name = "txtGLAccount"
        Me.txtGLAccount.ReferenceFieldDesc = Nothing
        Me.txtGLAccount.ReferenceFieldName = Nothing
        Me.txtGLAccount.ReferenceTableName = Nothing
        Me.txtGLAccount.Size = New System.Drawing.Size(222, 18)
        Me.txtGLAccount.TabIndex = 3
        Me.txtGLAccount.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Location = New System.Drawing.Point(9, 61)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(65, 18)
        Me.RadLabel5.TabIndex = 23
        Me.RadLabel5.Text = "GL/Account"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(9, 84)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(371, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(143, 36)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel2
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(499, 20)
        Me.txtDescription.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(9, 37)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(143, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel1.Location = New System.Drawing.Point(9, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(579, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(74, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(648, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.RadMenuItem6})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'menuImport
        '
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        '
        'menuExport
        '
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(648, 478)
        Me.SplitContainer1.SplitterDistance = 448
        Me.SplitContainer1.TabIndex = 0
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnHistory.Location = New System.Drawing.Point(143, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(68, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'FrmDiscountMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmDiscountMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Discount Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsOpening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCategoryDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnSampling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chksku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnVSNDType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGlAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtGLAccount As common.UserControls.txtFinder
    Friend WithEvents txtdiscountcategory As common.UserControls.txtFinder
    Friend WithEvents txtCategoryDesc As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnVSNDType As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnOther As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnDiscount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chksku As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents rbtnSampling As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkIsOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtGlAccDesc As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHistory As RadButton
End Class

