<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServiceMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmServiceMaster))
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblBreedType = New common.Controls.MyLabel()
        Me.lblCattleType = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.txtReminder = New common.MyNumBox()
        Me.txtServiceCharge = New common.MyNumBox()
        Me.txtBreedType = New common.UserControls.txtFinder()
        Me.txtCattleType = New common.UserControls.txtFinder()
        Me.txtServiceName = New common.UserControls.txtFinder()
        Me.txtGroup = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtServiceCode = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCustomerId = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblBreedType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReminder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServiceCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleName = "rdmenufile"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Exit"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(904, 508)
        Me.SplitContainer1.SplitterDistance = 479
        Me.SplitContainer1.TabIndex = 4
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblBreedType)
        Me.RadGroupBox1.Controls.Add(Me.lblCattleType)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtReminder)
        Me.RadGroupBox1.Controls.Add(Me.txtServiceCharge)
        Me.RadGroupBox1.Controls.Add(Me.txtBreedType)
        Me.RadGroupBox1.Controls.Add(Me.txtCattleType)
        Me.RadGroupBox1.Controls.Add(Me.txtServiceName)
        Me.RadGroupBox1.Controls.Add(Me.txtGroup)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtServiceCode)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerId)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(904, 479)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblBreedType
        '
        Me.lblBreedType.AutoSize = False
        Me.lblBreedType.BorderVisible = True
        Me.lblBreedType.FieldName = Nothing
        Me.lblBreedType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBreedType.Location = New System.Drawing.Point(432, 123)
        Me.lblBreedType.Name = "lblBreedType"
        Me.lblBreedType.Size = New System.Drawing.Size(368, 18)
        Me.lblBreedType.TabIndex = 257
        Me.lblBreedType.TextWrap = False
        '
        'lblCattleType
        '
        Me.lblCattleType.AutoSize = False
        Me.lblCattleType.BorderVisible = True
        Me.lblCattleType.FieldName = Nothing
        Me.lblCattleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleType.Location = New System.Drawing.Point(432, 103)
        Me.lblCattleType.Name = "lblCattleType"
        Me.lblCattleType.Size = New System.Drawing.Size(368, 18)
        Me.lblCattleType.TabIndex = 256
        Me.lblCattleType.TextWrap = False
        '
        'txtDesc
        '
        Me.txtDesc.AutoSize = False
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
        Me.txtDesc.Location = New System.Drawing.Point(131, 37)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.Multiline = True
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(297, 21)
        Me.txtDesc.TabIndex = 255
        Me.txtDesc.Text = " "
        '
        'txtReminder
        '
        Me.txtReminder.BackColor = System.Drawing.Color.White
        Me.txtReminder.CalculationExpression = Nothing
        Me.txtReminder.DecimalPlaces = 2
        Me.txtReminder.FieldCode = Nothing
        Me.txtReminder.FieldDesc = Nothing
        Me.txtReminder.FieldMaxLength = 0
        Me.txtReminder.FieldName = Nothing
        Me.txtReminder.isCalculatedField = False
        Me.txtReminder.IsSourceFromTable = False
        Me.txtReminder.IsSourceFromValueList = False
        Me.txtReminder.IsUnique = False
        Me.txtReminder.Location = New System.Drawing.Point(131, 166)
        Me.txtReminder.MendatroryField = False
        Me.txtReminder.MyLinkLable1 = Nothing
        Me.txtReminder.MyLinkLable2 = Nothing
        Me.txtReminder.Name = "txtReminder"
        Me.txtReminder.ReferenceFieldDesc = Nothing
        Me.txtReminder.ReferenceFieldName = Nothing
        Me.txtReminder.ReferenceTableName = Nothing
        Me.txtReminder.Size = New System.Drawing.Size(143, 20)
        Me.txtReminder.TabIndex = 252
        Me.txtReminder.Text = "0"
        Me.txtReminder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtReminder.Value = 0R
        '
        'txtServiceCharge
        '
        Me.txtServiceCharge.BackColor = System.Drawing.Color.White
        Me.txtServiceCharge.CalculationExpression = Nothing
        Me.txtServiceCharge.DecimalPlaces = 2
        Me.txtServiceCharge.FieldCode = Nothing
        Me.txtServiceCharge.FieldDesc = Nothing
        Me.txtServiceCharge.FieldMaxLength = 0
        Me.txtServiceCharge.FieldName = Nothing
        Me.txtServiceCharge.isCalculatedField = False
        Me.txtServiceCharge.IsSourceFromTable = False
        Me.txtServiceCharge.IsSourceFromValueList = False
        Me.txtServiceCharge.IsUnique = False
        Me.txtServiceCharge.Location = New System.Drawing.Point(131, 143)
        Me.txtServiceCharge.MendatroryField = False
        Me.txtServiceCharge.MyLinkLable1 = Nothing
        Me.txtServiceCharge.MyLinkLable2 = Nothing
        Me.txtServiceCharge.Name = "txtServiceCharge"
        Me.txtServiceCharge.ReferenceFieldDesc = Nothing
        Me.txtServiceCharge.ReferenceFieldName = Nothing
        Me.txtServiceCharge.ReferenceTableName = Nothing
        Me.txtServiceCharge.Size = New System.Drawing.Size(143, 20)
        Me.txtServiceCharge.TabIndex = 251
        Me.txtServiceCharge.Text = "0"
        Me.txtServiceCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtServiceCharge.Value = 0R
        '
        'txtBreedType
        '
        Me.txtBreedType.CalculationExpression = Nothing
        Me.txtBreedType.FieldCode = Nothing
        Me.txtBreedType.FieldDesc = Nothing
        Me.txtBreedType.FieldMaxLength = 0
        Me.txtBreedType.FieldName = Nothing
        Me.txtBreedType.isCalculatedField = False
        Me.txtBreedType.IsSourceFromTable = False
        Me.txtBreedType.IsSourceFromValueList = False
        Me.txtBreedType.IsUnique = False
        Me.txtBreedType.Location = New System.Drawing.Point(131, 123)
        Me.txtBreedType.MendatroryField = True
        Me.txtBreedType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreedType.MyLinkLable1 = Nothing
        Me.txtBreedType.MyLinkLable2 = Nothing
        Me.txtBreedType.MyReadOnly = False
        Me.txtBreedType.MyShowMasterFormButton = False
        Me.txtBreedType.Name = "txtBreedType"
        Me.txtBreedType.ReferenceFieldDesc = Nothing
        Me.txtBreedType.ReferenceFieldName = Nothing
        Me.txtBreedType.ReferenceTableName = Nothing
        Me.txtBreedType.Size = New System.Drawing.Size(297, 18)
        Me.txtBreedType.TabIndex = 250
        Me.txtBreedType.Value = ""
        '
        'txtCattleType
        '
        Me.txtCattleType.CalculationExpression = Nothing
        Me.txtCattleType.FieldCode = Nothing
        Me.txtCattleType.FieldDesc = Nothing
        Me.txtCattleType.FieldMaxLength = 0
        Me.txtCattleType.FieldName = Nothing
        Me.txtCattleType.isCalculatedField = False
        Me.txtCattleType.IsSourceFromTable = False
        Me.txtCattleType.IsSourceFromValueList = False
        Me.txtCattleType.IsUnique = False
        Me.txtCattleType.Location = New System.Drawing.Point(131, 103)
        Me.txtCattleType.MendatroryField = True
        Me.txtCattleType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleType.MyLinkLable1 = Nothing
        Me.txtCattleType.MyLinkLable2 = Nothing
        Me.txtCattleType.MyReadOnly = False
        Me.txtCattleType.MyShowMasterFormButton = False
        Me.txtCattleType.Name = "txtCattleType"
        Me.txtCattleType.ReferenceFieldDesc = Nothing
        Me.txtCattleType.ReferenceFieldName = Nothing
        Me.txtCattleType.ReferenceTableName = Nothing
        Me.txtCattleType.Size = New System.Drawing.Size(297, 18)
        Me.txtCattleType.TabIndex = 249
        Me.txtCattleType.Value = ""
        '
        'txtServiceName
        '
        Me.txtServiceName.CalculationExpression = Nothing
        Me.txtServiceName.FieldCode = Nothing
        Me.txtServiceName.FieldDesc = Nothing
        Me.txtServiceName.FieldMaxLength = 0
        Me.txtServiceName.FieldName = Nothing
        Me.txtServiceName.isCalculatedField = False
        Me.txtServiceName.IsSourceFromTable = False
        Me.txtServiceName.IsSourceFromValueList = False
        Me.txtServiceName.IsUnique = False
        Me.txtServiceName.Location = New System.Drawing.Point(131, 82)
        Me.txtServiceName.MendatroryField = True
        Me.txtServiceName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceName.MyLinkLable1 = Nothing
        Me.txtServiceName.MyLinkLable2 = Nothing
        Me.txtServiceName.MyReadOnly = False
        Me.txtServiceName.MyShowMasterFormButton = False
        Me.txtServiceName.Name = "txtServiceName"
        Me.txtServiceName.ReferenceFieldDesc = Nothing
        Me.txtServiceName.ReferenceFieldName = Nothing
        Me.txtServiceName.ReferenceTableName = Nothing
        Me.txtServiceName.Size = New System.Drawing.Size(297, 18)
        Me.txtServiceName.TabIndex = 248
        Me.txtServiceName.Value = ""
        '
        'txtGroup
        '
        Me.txtGroup.CalculationExpression = Nothing
        Me.txtGroup.FieldCode = Nothing
        Me.txtGroup.FieldDesc = Nothing
        Me.txtGroup.FieldMaxLength = 0
        Me.txtGroup.FieldName = Nothing
        Me.txtGroup.isCalculatedField = False
        Me.txtGroup.IsSourceFromTable = False
        Me.txtGroup.IsSourceFromValueList = False
        Me.txtGroup.IsUnique = False
        Me.txtGroup.Location = New System.Drawing.Point(131, 62)
        Me.txtGroup.MendatroryField = True
        Me.txtGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroup.MyLinkLable1 = Nothing
        Me.txtGroup.MyLinkLable2 = Nothing
        Me.txtGroup.MyReadOnly = False
        Me.txtGroup.MyShowMasterFormButton = False
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.ReferenceFieldDesc = Nothing
        Me.txtGroup.ReferenceFieldName = Nothing
        Me.txtGroup.ReferenceTableName = Nothing
        Me.txtGroup.Size = New System.Drawing.Size(297, 18)
        Me.txtGroup.TabIndex = 247
        Me.txtGroup.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(15, 169)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel6.TabIndex = 178
        Me.MyLabel6.Text = "Reminder (Days)"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(15, 147)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 177
        Me.MyLabel5.Text = "Service Charge"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(15, 125)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 176
        Me.MyLabel4.Text = "Breed Type"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(15, 103)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel3.TabIndex = 175
        Me.MyLabel3.Text = "Cattle Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(15, 81)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel2.TabIndex = 174
        Me.MyLabel2.Text = "Service Name"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel1.TabIndex = 173
        Me.MyLabel1.Text = "Group Name"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(15, 37)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(104, 16)
        Me.lblDescription.TabIndex = 171
        Me.lblDescription.Text = "Service Description"
        '
        'txtServiceCode
        '
        Me.txtServiceCode.FieldName = Nothing
        Me.txtServiceCode.Location = New System.Drawing.Point(131, 10)
        Me.txtServiceCode.MendatroryField = True
        Me.txtServiceCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtServiceCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtServiceCode.MyLinkLable1 = Nothing
        Me.txtServiceCode.MyLinkLable2 = Nothing
        Me.txtServiceCode.MyMaxLength = 30
        Me.txtServiceCode.MyReadOnly = False
        Me.txtServiceCode.Name = "txtServiceCode"
        Me.txtServiceCode.Size = New System.Drawing.Size(282, 21)
        Me.txtServiceCode.TabIndex = 169
        Me.txtServiceCode.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(411, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 170
        Me.btnNew.Text = " "
        '
        'lblCustomerId
        '
        Me.lblCustomerId.FieldName = Nothing
        Me.lblCustomerId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerId.Location = New System.Drawing.Point(15, 12)
        Me.lblCustomerId.Name = "lblCustomerId"
        Me.lblCustomerId.Size = New System.Drawing.Size(74, 16)
        Me.lblCustomerId.TabIndex = 168
        Me.lblCustomerId.Text = "Service Code"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(834, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 13
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(2, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 11
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(72, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 12
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(904, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'FrmServiceMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 528)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmServiceMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Service Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblBreedType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReminder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServiceCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtServiceCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCustomerId As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtGroupName As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtBreedType As common.UserControls.txtFinder
    Friend WithEvents txtCattleType As common.UserControls.txtFinder
    Friend WithEvents txtServiceName As common.UserControls.txtFinder
    Friend WithEvents txtGroup As common.UserControls.txtFinder
    Friend WithEvents txtReminder As common.MyNumBox
    Friend WithEvents txtServiceCharge As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblNDDBCode As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents lblBreedType As common.Controls.MyLabel
    Friend WithEvents lblCattleType As common.Controls.MyLabel
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

