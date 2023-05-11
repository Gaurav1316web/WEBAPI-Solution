Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBonusMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBonusMaster))
        Me.txtBONUS_RATE = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCOND_MAX_BONUS_PER_YEAR = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCOND_MAX_EARNING_PER_MONTH = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCOND_BASIC_PER_MONTH = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtName = New common.Controls.MyTextBox()
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
        Me.cboCalculationMethod = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkConsiderPayDays = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.txtBONUS_RATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOND_MAX_BONUS_PER_YEAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOND_MAX_EARNING_PER_MONTH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOND_BASIC_PER_MONTH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cboCalculationMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConsiderPayDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtBONUS_RATE
        '
        Me.txtBONUS_RATE.CalculationExpression = Nothing
        Me.txtBONUS_RATE.FieldCode = Nothing
        Me.txtBONUS_RATE.FieldDesc = Nothing
        Me.txtBONUS_RATE.FieldMaxLength = 0
        Me.txtBONUS_RATE.FieldName = Nothing
        Me.txtBONUS_RATE.isCalculatedField = False
        Me.txtBONUS_RATE.IsSourceFromTable = False
        Me.txtBONUS_RATE.IsSourceFromValueList = False
        Me.txtBONUS_RATE.IsUnique = False
        Me.txtBONUS_RATE.Location = New System.Drawing.Point(280, 186)
        Me.txtBONUS_RATE.MaxLength = 50
        Me.txtBONUS_RATE.MendatroryField = False
        Me.txtBONUS_RATE.MyLinkLable1 = Me.MyLabel3
        Me.txtBONUS_RATE.MyLinkLable2 = Nothing
        Me.txtBONUS_RATE.Name = "txtBONUS_RATE"
        Me.txtBONUS_RATE.ReferenceFieldDesc = Nothing
        Me.txtBONUS_RATE.ReferenceFieldName = Nothing
        Me.txtBONUS_RATE.ReferenceTableName = Nothing
        Me.txtBONUS_RATE.Size = New System.Drawing.Size(318, 20)
        Me.txtBONUS_RATE.TabIndex = 7
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(15, 187)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel3.TabIndex = 28
        Me.MyLabel3.Text = "Bonus Rate"
        '
        'txtCOND_MAX_BONUS_PER_YEAR
        '
        Me.txtCOND_MAX_BONUS_PER_YEAR.CalculationExpression = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.FieldCode = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.FieldDesc = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.FieldMaxLength = 0
        Me.txtCOND_MAX_BONUS_PER_YEAR.FieldName = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.isCalculatedField = False
        Me.txtCOND_MAX_BONUS_PER_YEAR.IsSourceFromTable = False
        Me.txtCOND_MAX_BONUS_PER_YEAR.IsSourceFromValueList = False
        Me.txtCOND_MAX_BONUS_PER_YEAR.IsUnique = False
        Me.txtCOND_MAX_BONUS_PER_YEAR.Location = New System.Drawing.Point(280, 161)
        Me.txtCOND_MAX_BONUS_PER_YEAR.MaxLength = 50
        Me.txtCOND_MAX_BONUS_PER_YEAR.MendatroryField = False
        Me.txtCOND_MAX_BONUS_PER_YEAR.MyLinkLable1 = Me.MyLabel4
        Me.txtCOND_MAX_BONUS_PER_YEAR.MyLinkLable2 = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.Name = "txtCOND_MAX_BONUS_PER_YEAR"
        Me.txtCOND_MAX_BONUS_PER_YEAR.ReferenceFieldDesc = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.ReferenceFieldName = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.ReferenceTableName = Nothing
        Me.txtCOND_MAX_BONUS_PER_YEAR.Size = New System.Drawing.Size(318, 20)
        Me.txtCOND_MAX_BONUS_PER_YEAR.TabIndex = 6
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(15, 162)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(122, 18)
        Me.MyLabel4.TabIndex = 25
        Me.MyLabel4.Text = "Maximum Bonus / Year"
        '
        'txtCOND_MAX_EARNING_PER_MONTH
        '
        Me.txtCOND_MAX_EARNING_PER_MONTH.CalculationExpression = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.FieldCode = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.FieldDesc = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.FieldMaxLength = 0
        Me.txtCOND_MAX_EARNING_PER_MONTH.FieldName = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.isCalculatedField = False
        Me.txtCOND_MAX_EARNING_PER_MONTH.IsSourceFromTable = False
        Me.txtCOND_MAX_EARNING_PER_MONTH.IsSourceFromValueList = False
        Me.txtCOND_MAX_EARNING_PER_MONTH.IsUnique = False
        Me.txtCOND_MAX_EARNING_PER_MONTH.Location = New System.Drawing.Point(280, 136)
        Me.txtCOND_MAX_EARNING_PER_MONTH.MaxLength = 50
        Me.txtCOND_MAX_EARNING_PER_MONTH.MendatroryField = False
        Me.txtCOND_MAX_EARNING_PER_MONTH.MyLinkLable1 = Me.MyLabel1
        Me.txtCOND_MAX_EARNING_PER_MONTH.MyLinkLable2 = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.Name = "txtCOND_MAX_EARNING_PER_MONTH"
        Me.txtCOND_MAX_EARNING_PER_MONTH.ReferenceFieldDesc = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.ReferenceFieldName = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.ReferenceTableName = Nothing
        Me.txtCOND_MAX_EARNING_PER_MONTH.Size = New System.Drawing.Size(318, 20)
        Me.txtCOND_MAX_EARNING_PER_MONTH.TabIndex = 5
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(15, 137)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(246, 18)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "Maximum Earning for bonus applicable / Month"
        '
        'txtCOND_BASIC_PER_MONTH
        '
        Me.txtCOND_BASIC_PER_MONTH.CalculationExpression = Nothing
        Me.txtCOND_BASIC_PER_MONTH.FieldCode = Nothing
        Me.txtCOND_BASIC_PER_MONTH.FieldDesc = Nothing
        Me.txtCOND_BASIC_PER_MONTH.FieldMaxLength = 0
        Me.txtCOND_BASIC_PER_MONTH.FieldName = Nothing
        Me.txtCOND_BASIC_PER_MONTH.isCalculatedField = False
        Me.txtCOND_BASIC_PER_MONTH.IsSourceFromTable = False
        Me.txtCOND_BASIC_PER_MONTH.IsSourceFromValueList = False
        Me.txtCOND_BASIC_PER_MONTH.IsUnique = False
        Me.txtCOND_BASIC_PER_MONTH.Location = New System.Drawing.Point(280, 111)
        Me.txtCOND_BASIC_PER_MONTH.MaxLength = 50
        Me.txtCOND_BASIC_PER_MONTH.MendatroryField = False
        Me.txtCOND_BASIC_PER_MONTH.MyLinkLable1 = Me.MyLabel2
        Me.txtCOND_BASIC_PER_MONTH.MyLinkLable2 = Nothing
        Me.txtCOND_BASIC_PER_MONTH.Name = "txtCOND_BASIC_PER_MONTH"
        Me.txtCOND_BASIC_PER_MONTH.ReferenceFieldDesc = Nothing
        Me.txtCOND_BASIC_PER_MONTH.ReferenceFieldName = Nothing
        Me.txtCOND_BASIC_PER_MONTH.ReferenceTableName = Nothing
        Me.txtCOND_BASIC_PER_MONTH.Size = New System.Drawing.Size(318, 20)
        Me.txtCOND_BASIC_PER_MONTH.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(15, 112)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(225, 18)
        Me.MyLabel2.TabIndex = 21
        Me.MyLabel2.Text = "Basic Amount for Bonus Applicable / Month"
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
        Me.txtDescription.Location = New System.Drawing.Point(280, 86)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(318, 20)
        Me.txtDescription.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(15, 87)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(14, 102)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'btnNew
        '
        Me.btnNew.DisplayStyle = Telerik.WinControls.DisplayStyle.Image
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(577, 35)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(21, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        Me.btnNew.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'txtName
        '
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(280, 61)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(318, 20)
        Me.txtName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(15, 62)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(70, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Bonus Name"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(280, 35)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(297, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(15, 36)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(591, 6)
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
        Me.btnDelete.Location = New System.Drawing.Point(74, 6)
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
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkConsiderPayDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboCalculationMethod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBONUS_RATE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCOND_MAX_BONUS_PER_YEAR)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCOND_MAX_EARNING_PER_MONTH)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCOND_BASIC_PER_MONTH)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(660, 453)
        Me.SplitContainer1.SplitterDistance = 422
        Me.SplitContainer1.TabIndex = 0
        '
        'cboCalculationMethod
        '
        Me.cboCalculationMethod.AutoCompleteDisplayMember = Nothing
        Me.cboCalculationMethod.AutoCompleteValueMember = Nothing
        Me.cboCalculationMethod.CalculationExpression = Nothing
        Me.cboCalculationMethod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCalculationMethod.FieldCode = Nothing
        Me.cboCalculationMethod.FieldDesc = Nothing
        Me.cboCalculationMethod.FieldMaxLength = 0
        Me.cboCalculationMethod.FieldName = Nothing
        Me.cboCalculationMethod.isCalculatedField = False
        Me.cboCalculationMethod.IsSourceFromTable = False
        Me.cboCalculationMethod.IsSourceFromValueList = False
        Me.cboCalculationMethod.IsUnique = False
        Me.cboCalculationMethod.Location = New System.Drawing.Point(280, 211)
        Me.cboCalculationMethod.MendatroryField = True
        Me.cboCalculationMethod.MyLinkLable1 = Me.RadLabel8
        Me.cboCalculationMethod.MyLinkLable2 = Nothing
        Me.cboCalculationMethod.Name = "cboCalculationMethod"
        Me.cboCalculationMethod.ReferenceFieldDesc = Nothing
        Me.cboCalculationMethod.ReferenceFieldName = Nothing
        Me.cboCalculationMethod.ReferenceTableName = Nothing
        Me.cboCalculationMethod.Size = New System.Drawing.Size(318, 20)
        Me.cboCalculationMethod.TabIndex = 58
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(15, 213)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel8.TabIndex = 59
        Me.RadLabel8.Text = "Calculation On"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(660, 20)
        Me.RadMenu2.TabIndex = 10
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.AccessibleDescription = "Import"
        Me.MenuItemImport.AccessibleName = "Import"
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.AccessibleDescription = "Export"
        Me.MenuItemExport.AccessibleName = "Export"
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'chkConsiderPayDays
        '
        Me.chkConsiderPayDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsiderPayDays.Location = New System.Drawing.Point(280, 237)
        Me.chkConsiderPayDays.Name = "chkConsiderPayDays"
        Me.chkConsiderPayDays.Size = New System.Drawing.Size(117, 16)
        Me.chkConsiderPayDays.TabIndex = 60
        Me.chkConsiderPayDays.Text = "Consider Pay Days"
        '
        'frmBonusMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 453)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBonusMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bonus Master"
        CType(Me.txtBONUS_RATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOND_MAX_BONUS_PER_YEAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOND_MAX_EARNING_PER_MONTH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOND_BASIC_PER_MONTH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cboCalculationMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConsiderPayDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
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
    Friend WithEvents txtBONUS_RATE As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCOND_MAX_BONUS_PER_YEAR As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtCOND_MAX_EARNING_PER_MONTH As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCOND_BASIC_PER_MONTH As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboCalculationMethod As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents chkConsiderPayDays As Telerik.WinControls.UI.RadCheckBox
End Class

