<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNDDBMaster
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNDDBMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtpNDDBDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblUsedBy = New common.Controls.MyLabel()
        Me.txtFarmerID = New common.UserControls.txtFinder()
        Me.lblCattleType = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblCattleDesc = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblFarmer = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtUsedBy = New common.UserControls.txtFinder()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtTagSNo = New common.Controls.MyTextBox()
        Me.txtTagPrefix = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtNDDBNo = New common.UserControls.txtNavigator()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.dtpNDDBDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUsedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTagSNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTagPrefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(898, 502)
        Me.SplitContainer1.SplitterDistance = 473
        Me.SplitContainer1.TabIndex = 5
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dtpNDDBDate)
        Me.RadGroupBox1.Controls.Add(Me.lblUsedBy)
        Me.RadGroupBox1.Controls.Add(Me.txtFarmerID)
        Me.RadGroupBox1.Controls.Add(Me.lblCattleType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.lblCattleDesc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.lblFarmer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox1.Controls.Add(Me.txtUsedBy)
        Me.RadGroupBox1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtTagSNo)
        Me.RadGroupBox1.Controls.Add(Me.txtTagPrefix)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtNDDBNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(898, 473)
        Me.RadGroupBox1.TabIndex = 0
        '
        'dtpNDDBDate
        '
        Me.dtpNDDBDate.CalculationExpression = Nothing
        Me.dtpNDDBDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpNDDBDate.FieldCode = Nothing
        Me.dtpNDDBDate.FieldDesc = Nothing
        Me.dtpNDDBDate.FieldMaxLength = 0
        Me.dtpNDDBDate.FieldName = Nothing
        Me.dtpNDDBDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpNDDBDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNDDBDate.isCalculatedField = False
        Me.dtpNDDBDate.IsSourceFromTable = False
        Me.dtpNDDBDate.IsSourceFromValueList = False
        Me.dtpNDDBDate.IsUnique = False
        Me.dtpNDDBDate.Location = New System.Drawing.Point(500, 20)
        Me.dtpNDDBDate.MendatroryField = False
        Me.dtpNDDBDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNDDBDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpNDDBDate.MyLinkLable2 = Nothing
        Me.dtpNDDBDate.Name = "dtpNDDBDate"
        Me.dtpNDDBDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNDDBDate.ReferenceFieldDesc = Nothing
        Me.dtpNDDBDate.ReferenceFieldName = Nothing
        Me.dtpNDDBDate.ReferenceTableName = Nothing
        Me.dtpNDDBDate.Size = New System.Drawing.Size(81, 18)
        Me.dtpNDDBDate.TabIndex = 277
        Me.dtpNDDBDate.TabStop = False
        Me.dtpNDDBDate.Text = "13/06/2011"
        Me.dtpNDDBDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(468, 23)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 244
        Me.RadLabel4.Text = "Date"
        '
        'lblUsedBy
        '
        Me.lblUsedBy.AutoSize = False
        Me.lblUsedBy.BorderVisible = True
        Me.lblUsedBy.FieldName = Nothing
        Me.lblUsedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsedBy.Location = New System.Drawing.Point(383, 111)
        Me.lblUsedBy.Name = "lblUsedBy"
        Me.lblUsedBy.Size = New System.Drawing.Size(266, 18)
        Me.lblUsedBy.TabIndex = 276
        Me.lblUsedBy.TextWrap = False
        '
        'txtFarmerID
        '
        Me.txtFarmerID.CalculationExpression = Nothing
        Me.txtFarmerID.FieldCode = Nothing
        Me.txtFarmerID.FieldDesc = Nothing
        Me.txtFarmerID.FieldMaxLength = 0
        Me.txtFarmerID.FieldName = Nothing
        Me.txtFarmerID.isCalculatedField = False
        Me.txtFarmerID.IsSourceFromTable = False
        Me.txtFarmerID.IsSourceFromValueList = False
        Me.txtFarmerID.IsUnique = False
        Me.txtFarmerID.Location = New System.Drawing.Point(116, 130)
        Me.txtFarmerID.MendatroryField = False
        Me.txtFarmerID.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFarmerID.MyLinkLable1 = Nothing
        Me.txtFarmerID.MyLinkLable2 = Nothing
        Me.txtFarmerID.MyReadOnly = True
        Me.txtFarmerID.MyShowMasterFormButton = False
        Me.txtFarmerID.Name = "txtFarmerID"
        Me.txtFarmerID.ReferenceFieldDesc = Nothing
        Me.txtFarmerID.ReferenceFieldName = Nothing
        Me.txtFarmerID.ReferenceTableName = Nothing
        Me.txtFarmerID.Size = New System.Drawing.Size(265, 18)
        Me.txtFarmerID.TabIndex = 275
        Me.txtFarmerID.Value = ""
        '
        'lblCattleType
        '
        Me.lblCattleType.AutoSize = False
        Me.lblCattleType.BorderVisible = True
        Me.lblCattleType.FieldName = Nothing
        Me.lblCattleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleType.Location = New System.Drawing.Point(115, 169)
        Me.lblCattleType.Name = "lblCattleType"
        Me.lblCattleType.Size = New System.Drawing.Size(266, 18)
        Me.lblCattleType.TabIndex = 273
        Me.lblCattleType.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(23, 171)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel6.TabIndex = 271
        Me.MyLabel6.Text = "Cattle Type"
        '
        'lblCattleDesc
        '
        Me.lblCattleDesc.AutoSize = False
        Me.lblCattleDesc.BorderVisible = True
        Me.lblCattleDesc.FieldName = Nothing
        Me.lblCattleDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleDesc.Location = New System.Drawing.Point(115, 149)
        Me.lblCattleDesc.Name = "lblCattleDesc"
        Me.lblCattleDesc.Size = New System.Drawing.Size(266, 18)
        Me.lblCattleDesc.TabIndex = 270
        Me.lblCattleDesc.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(23, 151)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel5.TabIndex = 268
        Me.MyLabel5.Text = "Cattle Id"
        '
        'lblFarmer
        '
        Me.lblFarmer.AutoSize = False
        Me.lblFarmer.BorderVisible = True
        Me.lblFarmer.FieldName = Nothing
        Me.lblFarmer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFarmer.Location = New System.Drawing.Point(383, 130)
        Me.lblFarmer.Name = "lblFarmer"
        Me.lblFarmer.Size = New System.Drawing.Size(266, 18)
        Me.lblFarmer.TabIndex = 267
        Me.lblFarmer.TextWrap = False
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(24, 131)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel19.TabIndex = 265
        Me.MyLabel19.Text = "Farmer Id"
        '
        'txtUsedBy
        '
        Me.txtUsedBy.CalculationExpression = Nothing
        Me.txtUsedBy.FieldCode = Nothing
        Me.txtUsedBy.FieldDesc = Nothing
        Me.txtUsedBy.FieldMaxLength = 0
        Me.txtUsedBy.FieldName = Nothing
        Me.txtUsedBy.isCalculatedField = False
        Me.txtUsedBy.IsSourceFromTable = False
        Me.txtUsedBy.IsSourceFromValueList = False
        Me.txtUsedBy.IsUnique = False
        Me.txtUsedBy.Location = New System.Drawing.Point(116, 110)
        Me.txtUsedBy.MendatroryField = True
        Me.txtUsedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsedBy.MyLinkLable1 = Nothing
        Me.txtUsedBy.MyLinkLable2 = Nothing
        Me.txtUsedBy.MyReadOnly = False
        Me.txtUsedBy.MyShowMasterFormButton = False
        Me.txtUsedBy.Name = "txtUsedBy"
        Me.txtUsedBy.ReferenceFieldDesc = Nothing
        Me.txtUsedBy.ReferenceFieldName = Nothing
        Me.txtUsedBy.ReferenceTableName = Nothing
        Me.txtUsedBy.Size = New System.Drawing.Size(265, 18)
        Me.txtUsedBy.TabIndex = 262
        Me.txtUsedBy.Value = ""
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(23, 111)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(49, 16)
        Me.lblItemCategoryCode.TabIndex = 261
        Me.lblItemCategoryCode.Text = "Used By"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(23, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 260
        Me.MyLabel1.Text = "Tag S.No."
        '
        'txtTagSNo
        '
        Me.txtTagSNo.AutoSize = False
        Me.txtTagSNo.CalculationExpression = Nothing
        Me.txtTagSNo.FieldCode = Nothing
        Me.txtTagSNo.FieldDesc = Nothing
        Me.txtTagSNo.FieldMaxLength = 0
        Me.txtTagSNo.FieldName = Nothing
        Me.txtTagSNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTagSNo.isCalculatedField = False
        Me.txtTagSNo.IsSourceFromTable = False
        Me.txtTagSNo.IsSourceFromValueList = False
        Me.txtTagSNo.IsUnique = False
        Me.txtTagSNo.Location = New System.Drawing.Point(116, 87)
        Me.txtTagSNo.MaxLength = 50
        Me.txtTagSNo.MendatroryField = False
        Me.txtTagSNo.Multiline = True
        Me.txtTagSNo.MyLinkLable1 = Nothing
        Me.txtTagSNo.MyLinkLable2 = Nothing
        Me.txtTagSNo.Name = "txtTagSNo"
        Me.txtTagSNo.ReferenceFieldDesc = Nothing
        Me.txtTagSNo.ReferenceFieldName = Nothing
        Me.txtTagSNo.ReferenceTableName = Nothing
        Me.txtTagSNo.Size = New System.Drawing.Size(265, 21)
        Me.txtTagSNo.TabIndex = 259
        Me.txtTagSNo.Text = " "
        '
        'txtTagPrefix
        '
        Me.txtTagPrefix.AutoSize = False
        Me.txtTagPrefix.CalculationExpression = Nothing
        Me.txtTagPrefix.FieldCode = Nothing
        Me.txtTagPrefix.FieldDesc = Nothing
        Me.txtTagPrefix.FieldMaxLength = 0
        Me.txtTagPrefix.FieldName = Nothing
        Me.txtTagPrefix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTagPrefix.isCalculatedField = False
        Me.txtTagPrefix.IsSourceFromTable = False
        Me.txtTagPrefix.IsSourceFromValueList = False
        Me.txtTagPrefix.IsUnique = False
        Me.txtTagPrefix.Location = New System.Drawing.Point(116, 64)
        Me.txtTagPrefix.MaxLength = 50
        Me.txtTagPrefix.MendatroryField = False
        Me.txtTagPrefix.Multiline = True
        Me.txtTagPrefix.MyLinkLable1 = Nothing
        Me.txtTagPrefix.MyLinkLable2 = Nothing
        Me.txtTagPrefix.Name = "txtTagPrefix"
        Me.txtTagPrefix.ReferenceFieldDesc = Nothing
        Me.txtTagPrefix.ReferenceFieldName = Nothing
        Me.txtTagPrefix.ReferenceTableName = Nothing
        Me.txtTagPrefix.Size = New System.Drawing.Size(265, 21)
        Me.txtTagPrefix.TabIndex = 257
        Me.txtTagPrefix.Text = " "
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(23, 67)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel3.TabIndex = 256
        Me.MyLabel3.Text = "Tag Prefix"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(23, 43)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 247
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(116, 42)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(465, 21)
        Me.txtDescription.TabIndex = 248
        Me.txtDescription.Text = " "
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(23, 21)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel2.TabIndex = 246
        Me.MyLabel2.Text = "NDDB No"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(441, 20)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 242
        Me.btnNew.Text = " "
        '
        'txtNDDBNo
        '
        Me.txtNDDBNo.FieldName = Nothing
        Me.txtNDDBNo.Location = New System.Drawing.Point(116, 19)
        Me.txtNDDBNo.MendatroryField = True
        Me.txtNDDBNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNDDBNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtNDDBNo.MyLinkLable1 = Nothing
        Me.txtNDDBNo.MyLinkLable2 = Nothing
        Me.txtNDDBNo.MyMaxLength = 30
        Me.txtNDDBNo.MyReadOnly = False
        Me.txtNDDBNo.Name = "txtNDDBNo"
        Me.txtNDDBNo.Size = New System.Drawing.Size(323, 21)
        Me.txtNDDBNo.TabIndex = 245
        Me.txtNDDBNo.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(831, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(73, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(898, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleName = "rdmenufile"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
        '
        'FrmNDDBMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 522)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmNDDBMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "NDDB Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.dtpNDDBDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUsedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTagSNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTagPrefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtNDDBNo As common.UserControls.txtNavigator
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTagSNo As common.Controls.MyTextBox
    Friend WithEvents txtTagPrefix As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtUsedBy As common.UserControls.txtFinder
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents lblCattleType As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblCattleDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblFarmer As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFarmerID As common.UserControls.txtFinder
    Friend WithEvents lblUsedBy As common.Controls.MyLabel
    Friend WithEvents dtpNDDBDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
End Class

