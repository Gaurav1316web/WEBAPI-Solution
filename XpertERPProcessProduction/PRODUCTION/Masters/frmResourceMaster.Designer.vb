<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResourceMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResourceMaster))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtOtherUOM = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMaintainedBy = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtLastMaintained = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtCost = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.cboUOM = New common.Controls.MyComboBox()
        Me.cboType = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboStatus = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtOtherUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaintainedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastMaintained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtOtherUOM)
        Me.RadGroupBox1.Controls.Add(Me.txtMaintainedBy)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.txtLastMaintained)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.txtCost)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.cboUOM)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.cboType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.cboStatus)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtComments)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 15)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(472, 209)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'txtOtherUOM
        '
        Me.txtOtherUOM.CalculationExpression = Nothing
        Me.txtOtherUOM.FieldCode = Nothing
        Me.txtOtherUOM.FieldDesc = Nothing
        Me.txtOtherUOM.FieldMaxLength = 0
        Me.txtOtherUOM.FieldName = Nothing
        Me.txtOtherUOM.isCalculatedField = False
        Me.txtOtherUOM.IsSourceFromTable = False
        Me.txtOtherUOM.IsSourceFromValueList = False
        Me.txtOtherUOM.IsUnique = False
        Me.txtOtherUOM.Location = New System.Drawing.Point(194, 110)
        Me.txtOtherUOM.MaxLength = 50
        Me.txtOtherUOM.MendatroryField = False
        Me.txtOtherUOM.MyLinkLable1 = Me.MyLabel3
        Me.txtOtherUOM.MyLinkLable2 = Nothing
        Me.txtOtherUOM.Name = "txtOtherUOM"
        Me.txtOtherUOM.ReferenceFieldDesc = Nothing
        Me.txtOtherUOM.ReferenceFieldName = Nothing
        Me.txtOtherUOM.ReferenceTableName = Nothing
        Me.txtOtherUOM.Size = New System.Drawing.Size(97, 20)
        Me.txtOtherUOM.TabIndex = 8
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 111)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel3.TabIndex = 13
        Me.MyLabel3.Text = "UOM"
        '
        'txtMaintainedBy
        '
        Me.txtMaintainedBy.CalculationExpression = Nothing
        Me.txtMaintainedBy.FieldCode = Nothing
        Me.txtMaintainedBy.FieldDesc = Nothing
        Me.txtMaintainedBy.FieldMaxLength = 0
        Me.txtMaintainedBy.FieldName = Nothing
        Me.txtMaintainedBy.isCalculatedField = False
        Me.txtMaintainedBy.IsSourceFromTable = False
        Me.txtMaintainedBy.IsSourceFromValueList = False
        Me.txtMaintainedBy.IsUnique = False
        Me.txtMaintainedBy.Location = New System.Drawing.Point(297, 88)
        Me.txtMaintainedBy.MaxLength = 50
        Me.txtMaintainedBy.MendatroryField = False
        Me.txtMaintainedBy.MyLinkLable1 = Me.MyLabel6
        Me.txtMaintainedBy.MyLinkLable2 = Nothing
        Me.txtMaintainedBy.Name = "txtMaintainedBy"
        Me.txtMaintainedBy.ReadOnly = True
        Me.txtMaintainedBy.ReferenceFieldDesc = Nothing
        Me.txtMaintainedBy.ReferenceFieldName = Nothing
        Me.txtMaintainedBy.ReferenceTableName = Nothing
        Me.txtMaintainedBy.Size = New System.Drawing.Size(151, 20)
        Me.txtMaintainedBy.TabIndex = 6
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(194, 89)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel6.TabIndex = 18
        Me.MyLabel6.Text = "Maintained By"
        '
        'txtLastMaintained
        '
        Me.txtLastMaintained.CalculationExpression = Nothing
        Me.txtLastMaintained.FieldCode = Nothing
        Me.txtLastMaintained.FieldDesc = Nothing
        Me.txtLastMaintained.FieldMaxLength = 0
        Me.txtLastMaintained.FieldName = Nothing
        Me.txtLastMaintained.isCalculatedField = False
        Me.txtLastMaintained.IsSourceFromTable = False
        Me.txtLastMaintained.IsSourceFromValueList = False
        Me.txtLastMaintained.IsUnique = False
        Me.txtLastMaintained.Location = New System.Drawing.Point(297, 66)
        Me.txtLastMaintained.MaxLength = 50
        Me.txtLastMaintained.MendatroryField = False
        Me.txtLastMaintained.MyLinkLable1 = Me.MyLabel5
        Me.txtLastMaintained.MyLinkLable2 = Nothing
        Me.txtLastMaintained.Name = "txtLastMaintained"
        Me.txtLastMaintained.ReadOnly = True
        Me.txtLastMaintained.ReferenceFieldDesc = Nothing
        Me.txtLastMaintained.ReferenceFieldName = Nothing
        Me.txtLastMaintained.ReferenceTableName = Nothing
        Me.txtLastMaintained.Size = New System.Drawing.Size(151, 20)
        Me.txtLastMaintained.TabIndex = 4
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(194, 67)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel5.TabIndex = 19
        Me.MyLabel5.Text = "Last Maintained"
        '
        'txtCost
        '
        Me.txtCost.BackColor = System.Drawing.Color.White
        Me.txtCost.CalculationExpression = Nothing
        Me.txtCost.DecimalPlaces = 2
        Me.txtCost.FieldCode = Nothing
        Me.txtCost.FieldDesc = Nothing
        Me.txtCost.FieldMaxLength = 0
        Me.txtCost.FieldName = Nothing
        Me.txtCost.isCalculatedField = False
        Me.txtCost.IsSourceFromTable = False
        Me.txtCost.IsSourceFromValueList = False
        Me.txtCost.IsUnique = False
        Me.txtCost.Location = New System.Drawing.Point(88, 132)
        Me.txtCost.MendatroryField = False
        Me.txtCost.MyLinkLable1 = Me.MyLabel4
        Me.txtCost.MyLinkLable2 = Nothing
        Me.txtCost.Name = "txtCost"
        Me.txtCost.ReferenceFieldDesc = Nothing
        Me.txtCost.ReferenceFieldName = Nothing
        Me.txtCost.ReferenceTableName = Nothing
        Me.txtCost.Size = New System.Drawing.Size(100, 20)
        Me.txtCost.TabIndex = 9
        Me.txtCost.Text = "0"
        Me.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCost.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(12, 133)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(28, 18)
        Me.MyLabel4.TabIndex = 12
        Me.MyLabel4.Text = "Cost"
        '
        'cboUOM
        '
        Me.cboUOM.BackColor = System.Drawing.Color.Transparent
        Me.cboUOM.CalculationExpression = Nothing
        Me.cboUOM.DropDownAnimationEnabled = True
        Me.cboUOM.FieldCode = Nothing
        Me.cboUOM.FieldDesc = Nothing
        Me.cboUOM.FieldMaxLength = 0
        Me.cboUOM.FieldName = Nothing
        Me.cboUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUOM.isCalculatedField = False
        Me.cboUOM.IsSourceFromTable = False
        Me.cboUOM.IsSourceFromValueList = False
        Me.cboUOM.IsUnique = False
        Me.cboUOM.Location = New System.Drawing.Point(88, 111)
        Me.cboUOM.MendatroryField = False
        Me.cboUOM.MyLinkLable1 = Me.MyLabel3
        Me.cboUOM.MyLinkLable2 = Nothing
        Me.cboUOM.Name = "cboUOM"
        Me.cboUOM.ReferenceFieldDesc = Nothing
        Me.cboUOM.ReferenceFieldName = Nothing
        Me.cboUOM.ReferenceTableName = Nothing
        Me.cboUOM.Size = New System.Drawing.Size(100, 18)
        Me.cboUOM.TabIndex = 7
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.Color.Transparent
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownAnimationEnabled = True
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        Me.cboType.Location = New System.Drawing.Point(88, 89)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel2
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(100, 18)
        Me.cboType.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel2.TabIndex = 14
        Me.MyLabel2.Text = "Type"
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboStatus.CalculationExpression = Nothing
        Me.cboStatus.DropDownAnimationEnabled = True
        Me.cboStatus.FieldCode = Nothing
        Me.cboStatus.FieldDesc = Nothing
        Me.cboStatus.FieldMaxLength = 0
        Me.cboStatus.FieldName = Nothing
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.isCalculatedField = False
        Me.cboStatus.IsSourceFromTable = False
        Me.cboStatus.IsSourceFromValueList = False
        Me.cboStatus.IsUnique = False
        Me.cboStatus.Location = New System.Drawing.Point(88, 67)
        Me.cboStatus.MendatroryField = False
        Me.cboStatus.MyLinkLable1 = Me.MyLabel1
        Me.cboStatus.MyLinkLable2 = Nothing
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.ReferenceFieldDesc = Nothing
        Me.cboStatus.ReferenceFieldName = Nothing
        Me.cboStatus.ReferenceTableName = Nothing
        Me.cboStatus.Size = New System.Drawing.Size(100, 18)
        Me.cboStatus.TabIndex = 3
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 67)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(37, 18)
        Me.MyLabel1.TabIndex = 15
        Me.MyLabel1.Text = "Status"
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
        Me.txtDescription.Location = New System.Drawing.Point(88, 44)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(360, 20)
        Me.txtDescription.TabIndex = 2
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(12, 45)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 16
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
        Me.btnNew.Location = New System.Drawing.Point(372, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtComments
        '
        Me.txtComments.AutoSize = False
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(88, 154)
        Me.txtComments.MaxLength = 50
        Me.txtComments.MendatroryField = False
        Me.txtComments.Multiline = True
        Me.txtComments.MyLinkLable1 = Me.RadLabel2
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(360, 48)
        Me.txtComments.TabIndex = 10
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(12, 155)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 18)
        Me.RadLabel2.TabIndex = 11
        Me.RadLabel2.Text = "Comments"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(88, 22)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(281, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(12, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 17
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(409, 5)
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
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(478, 256)
        Me.SplitContainer1.SplitterDistance = 226
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(478, 20)
        Me.RadMenu2.TabIndex = 1
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'frmResourceMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 256)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmResourceMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Resource Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtOtherUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaintainedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastMaintained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboUOM As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtOtherUOM As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtMaintainedBy As common.Controls.MyTextBox
    Friend WithEvents txtLastMaintained As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtCost As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

