Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetBookMaster
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
        Me.lblGroup = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.cboTaxDepType = New common.Controls.MyComboBox()
        Me.lblTaxDepType = New common.Controls.MyLabel()
        Me.cboDepType = New common.Controls.MyComboBox()
        Me.lblDepType = New common.Controls.MyLabel()
        Me.txtnetvalue = New common.MyNumBox()
        Me.lblNetValue = New common.Controls.MyLabel()
        Me.txtSalvageRate = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtSalvageValue = New common.MyNumBox()
        Me.txtSourceValue = New common.MyNumBox()
        Me.txtSourceOrgValue = New common.MyNumBox()
        Me.txtDepTaxRate = New common.MyNumBox()
        Me.txtDepRate = New common.MyNumBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblDepMethodTax = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDepMethodTax = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtEstLife = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblKMReading = New common.Controls.MyLabel()
        Me.lblDepPeriod = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtStartDate = New common.Controls.MyDateTimePicker()
        Me.lblDepMethod = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDepPeriod = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtDepMethod = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTaxDepType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxDepType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDepType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnetvalue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalvageRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalvageValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceOrgValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepMethodTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEstLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblGroup
        '
        Me.lblGroup.FieldName = Nothing
        Me.lblGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.Location = New System.Drawing.Point(9, 12)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(33, 16)
        Me.lblGroup.TabIndex = 42
        Me.lblGroup.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(384, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(702, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExit
        '
        Me.rmiExit.Name = "rmiExit"
        Me.rmiExit.Text = "Exit"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(628, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.cboTaxDepType)
        Me.RadGroupBox1.Controls.Add(Me.lblTaxDepType)
        Me.RadGroupBox1.Controls.Add(Me.cboDepType)
        Me.RadGroupBox1.Controls.Add(Me.lblDepType)
        Me.RadGroupBox1.Controls.Add(Me.txtnetvalue)
        Me.RadGroupBox1.Controls.Add(Me.lblNetValue)
        Me.RadGroupBox1.Controls.Add(Me.txtSalvageRate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtSalvageValue)
        Me.RadGroupBox1.Controls.Add(Me.txtSourceValue)
        Me.RadGroupBox1.Controls.Add(Me.txtSourceOrgValue)
        Me.RadGroupBox1.Controls.Add(Me.txtDepTaxRate)
        Me.RadGroupBox1.Controls.Add(Me.txtDepRate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.lblDepMethodTax)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.txtDepMethodTax)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.txtEstLife)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.lblKMReading)
        Me.RadGroupBox1.Controls.Add(Me.lblDepPeriod)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtStartDate)
        Me.RadGroupBox1.Controls.Add(Me.lblDepMethod)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtDepPeriod)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.txtDepMethod)
        Me.RadGroupBox1.Controls.Add(Me.lblGroup)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(7, 31)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(685, 295)
        Me.RadGroupBox1.TabIndex = 57
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 32)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 243
        Me.MyLabel1.Text = "Description"
        '
        'txtDescription
        '
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
        Me.txtDescription.Location = New System.Drawing.Point(114, 32)
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(433, 18)
        Me.txtDescription.TabIndex = 242
        '
        'cboTaxDepType
        '
        Me.cboTaxDepType.CalculationExpression = Nothing
        Me.cboTaxDepType.DropDownAnimationEnabled = True
        Me.cboTaxDepType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTaxDepType.FieldCode = Nothing
        Me.cboTaxDepType.FieldDesc = Nothing
        Me.cboTaxDepType.FieldMaxLength = 0
        Me.cboTaxDepType.FieldName = Nothing
        Me.cboTaxDepType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTaxDepType.isCalculatedField = False
        Me.cboTaxDepType.IsSourceFromTable = False
        Me.cboTaxDepType.IsSourceFromValueList = False
        Me.cboTaxDepType.IsUnique = False
        Me.cboTaxDepType.Location = New System.Drawing.Point(346, 152)
        Me.cboTaxDepType.MendatroryField = True
        Me.cboTaxDepType.MyLinkLable1 = Me.lblTaxDepType
        Me.cboTaxDepType.MyLinkLable2 = Nothing
        Me.cboTaxDepType.Name = "cboTaxDepType"
        Me.cboTaxDepType.ReferenceFieldDesc = Nothing
        Me.cboTaxDepType.ReferenceFieldName = Nothing
        Me.cboTaxDepType.ReferenceTableName = Nothing
        Me.cboTaxDepType.Size = New System.Drawing.Size(201, 18)
        Me.cboTaxDepType.TabIndex = 240
        '
        'lblTaxDepType
        '
        Me.lblTaxDepType.FieldName = Nothing
        Me.lblTaxDepType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxDepType.Location = New System.Drawing.Point(260, 153)
        Me.lblTaxDepType.Name = "lblTaxDepType"
        Me.lblTaxDepType.Size = New System.Drawing.Size(80, 16)
        Me.lblTaxDepType.TabIndex = 241
        Me.lblTaxDepType.Text = "Tax Dep. Type"
        '
        'cboDepType
        '
        Me.cboDepType.CalculationExpression = Nothing
        Me.cboDepType.DropDownAnimationEnabled = True
        Me.cboDepType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDepType.FieldCode = Nothing
        Me.cboDepType.FieldDesc = Nothing
        Me.cboDepType.FieldMaxLength = 0
        Me.cboDepType.FieldName = Nothing
        Me.cboDepType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDepType.isCalculatedField = False
        Me.cboDepType.IsSourceFromTable = False
        Me.cboDepType.IsSourceFromValueList = False
        Me.cboDepType.IsUnique = False
        Me.cboDepType.Location = New System.Drawing.Point(346, 132)
        Me.cboDepType.MendatroryField = True
        Me.cboDepType.MyLinkLable1 = Me.lblDepType
        Me.cboDepType.MyLinkLable2 = Nothing
        Me.cboDepType.Name = "cboDepType"
        Me.cboDepType.ReferenceFieldDesc = Nothing
        Me.cboDepType.ReferenceFieldName = Nothing
        Me.cboDepType.ReferenceTableName = Nothing
        Me.cboDepType.Size = New System.Drawing.Size(201, 18)
        Me.cboDepType.TabIndex = 238
        '
        'lblDepType
        '
        Me.lblDepType.FieldName = Nothing
        Me.lblDepType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepType.Location = New System.Drawing.Point(259, 133)
        Me.lblDepType.Name = "lblDepType"
        Me.lblDepType.Size = New System.Drawing.Size(87, 16)
        Me.lblDepType.TabIndex = 239
        Me.lblDepType.Text = "Book Dep. Type"
        '
        'txtnetvalue
        '
        Me.txtnetvalue.BackColor = System.Drawing.Color.White
        Me.txtnetvalue.CalculationExpression = Nothing
        Me.txtnetvalue.DecimalPlaces = 2
        Me.txtnetvalue.FieldCode = Nothing
        Me.txtnetvalue.FieldDesc = Nothing
        Me.txtnetvalue.FieldMaxLength = 0
        Me.txtnetvalue.FieldName = Nothing
        Me.txtnetvalue.isCalculatedField = False
        Me.txtnetvalue.IsSourceFromTable = False
        Me.txtnetvalue.IsSourceFromValueList = False
        Me.txtnetvalue.IsUnique = False
        Me.txtnetvalue.Location = New System.Drawing.Point(114, 263)
        Me.txtnetvalue.MaxLength = 18
        Me.txtnetvalue.MendatroryField = False
        Me.txtnetvalue.MyLinkLable1 = Nothing
        Me.txtnetvalue.MyLinkLable2 = Nothing
        Me.txtnetvalue.Name = "txtnetvalue"
        Me.txtnetvalue.ReferenceFieldDesc = Nothing
        Me.txtnetvalue.ReferenceFieldName = Nothing
        Me.txtnetvalue.ReferenceTableName = Nothing
        Me.txtnetvalue.Size = New System.Drawing.Size(143, 20)
        Me.txtnetvalue.TabIndex = 236
        Me.txtnetvalue.Text = "0"
        Me.txtnetvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtnetvalue.Value = 0R
        '
        'lblNetValue
        '
        Me.lblNetValue.FieldName = Nothing
        Me.lblNetValue.Location = New System.Drawing.Point(10, 262)
        Me.lblNetValue.Name = "lblNetValue"
        Me.lblNetValue.Size = New System.Drawing.Size(55, 18)
        Me.lblNetValue.TabIndex = 237
        Me.lblNetValue.Text = "Net Value"
        '
        'txtSalvageRate
        '
        Me.txtSalvageRate.BackColor = System.Drawing.Color.White
        Me.txtSalvageRate.CalculationExpression = Nothing
        Me.txtSalvageRate.DecimalPlaces = 2
        Me.txtSalvageRate.FieldCode = Nothing
        Me.txtSalvageRate.FieldDesc = Nothing
        Me.txtSalvageRate.FieldMaxLength = 0
        Me.txtSalvageRate.FieldName = Nothing
        Me.txtSalvageRate.isCalculatedField = False
        Me.txtSalvageRate.IsSourceFromTable = False
        Me.txtSalvageRate.IsSourceFromValueList = False
        Me.txtSalvageRate.IsUnique = False
        Me.txtSalvageRate.Location = New System.Drawing.Point(114, 239)
        Me.txtSalvageRate.MaxLength = 3
        Me.txtSalvageRate.MendatroryField = False
        Me.txtSalvageRate.MyLinkLable1 = Nothing
        Me.txtSalvageRate.MyLinkLable2 = Nothing
        Me.txtSalvageRate.Name = "txtSalvageRate"
        Me.txtSalvageRate.ReferenceFieldDesc = Nothing
        Me.txtSalvageRate.ReferenceFieldName = Nothing
        Me.txtSalvageRate.ReferenceTableName = Nothing
        Me.txtSalvageRate.Size = New System.Drawing.Size(143, 20)
        Me.txtSalvageRate.TabIndex = 234
        Me.txtSalvageRate.Text = "0"
        Me.txtSalvageRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalvageRate.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(9, 241)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel4.TabIndex = 235
        Me.MyLabel4.Text = "Salvage %"
        '
        'txtSalvageValue
        '
        Me.txtSalvageValue.BackColor = System.Drawing.Color.White
        Me.txtSalvageValue.CalculationExpression = Nothing
        Me.txtSalvageValue.DecimalPlaces = 2
        Me.txtSalvageValue.FieldCode = Nothing
        Me.txtSalvageValue.FieldDesc = Nothing
        Me.txtSalvageValue.FieldMaxLength = 0
        Me.txtSalvageValue.FieldName = Nothing
        Me.txtSalvageValue.isCalculatedField = False
        Me.txtSalvageValue.IsSourceFromTable = False
        Me.txtSalvageValue.IsSourceFromValueList = False
        Me.txtSalvageValue.IsUnique = False
        Me.txtSalvageValue.Location = New System.Drawing.Point(368, 239)
        Me.txtSalvageValue.MaxLength = 20
        Me.txtSalvageValue.MendatroryField = False
        Me.txtSalvageValue.MyLinkLable1 = Nothing
        Me.txtSalvageValue.MyLinkLable2 = Nothing
        Me.txtSalvageValue.Name = "txtSalvageValue"
        Me.txtSalvageValue.ReferenceFieldDesc = Nothing
        Me.txtSalvageValue.ReferenceFieldName = Nothing
        Me.txtSalvageValue.ReferenceTableName = Nothing
        Me.txtSalvageValue.Size = New System.Drawing.Size(143, 20)
        Me.txtSalvageValue.TabIndex = 220
        Me.txtSalvageValue.Text = "0"
        Me.txtSalvageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalvageValue.Value = 0R
        '
        'txtSourceValue
        '
        Me.txtSourceValue.BackColor = System.Drawing.Color.White
        Me.txtSourceValue.CalculationExpression = Nothing
        Me.txtSourceValue.DecimalPlaces = 2
        Me.txtSourceValue.FieldCode = Nothing
        Me.txtSourceValue.FieldDesc = Nothing
        Me.txtSourceValue.FieldMaxLength = 0
        Me.txtSourceValue.FieldName = Nothing
        Me.txtSourceValue.isCalculatedField = False
        Me.txtSourceValue.IsSourceFromTable = False
        Me.txtSourceValue.IsSourceFromValueList = False
        Me.txtSourceValue.IsUnique = False
        Me.txtSourceValue.Location = New System.Drawing.Point(114, 217)
        Me.txtSourceValue.MendatroryField = False
        Me.txtSourceValue.MyLinkLable1 = Nothing
        Me.txtSourceValue.MyLinkLable2 = Nothing
        Me.txtSourceValue.Name = "txtSourceValue"
        Me.txtSourceValue.ReferenceFieldDesc = Nothing
        Me.txtSourceValue.ReferenceFieldName = Nothing
        Me.txtSourceValue.ReferenceTableName = Nothing
        Me.txtSourceValue.Size = New System.Drawing.Size(143, 20)
        Me.txtSourceValue.TabIndex = 219
        Me.txtSourceValue.Text = "0"
        Me.txtSourceValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSourceValue.Value = 0R
        '
        'txtSourceOrgValue
        '
        Me.txtSourceOrgValue.BackColor = System.Drawing.Color.White
        Me.txtSourceOrgValue.CalculationExpression = Nothing
        Me.txtSourceOrgValue.DecimalPlaces = 2
        Me.txtSourceOrgValue.FieldCode = Nothing
        Me.txtSourceOrgValue.FieldDesc = Nothing
        Me.txtSourceOrgValue.FieldMaxLength = 0
        Me.txtSourceOrgValue.FieldName = Nothing
        Me.txtSourceOrgValue.isCalculatedField = False
        Me.txtSourceOrgValue.IsSourceFromTable = False
        Me.txtSourceOrgValue.IsSourceFromValueList = False
        Me.txtSourceOrgValue.IsUnique = False
        Me.txtSourceOrgValue.Location = New System.Drawing.Point(114, 196)
        Me.txtSourceOrgValue.MendatroryField = False
        Me.txtSourceOrgValue.MyLinkLable1 = Nothing
        Me.txtSourceOrgValue.MyLinkLable2 = Nothing
        Me.txtSourceOrgValue.Name = "txtSourceOrgValue"
        Me.txtSourceOrgValue.ReferenceFieldDesc = Nothing
        Me.txtSourceOrgValue.ReferenceFieldName = Nothing
        Me.txtSourceOrgValue.ReferenceTableName = Nothing
        Me.txtSourceOrgValue.Size = New System.Drawing.Size(143, 20)
        Me.txtSourceOrgValue.TabIndex = 218
        Me.txtSourceOrgValue.Text = "0"
        Me.txtSourceOrgValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSourceOrgValue.Value = 0R
        '
        'txtDepTaxRate
        '
        Me.txtDepTaxRate.BackColor = System.Drawing.Color.White
        Me.txtDepTaxRate.CalculationExpression = Nothing
        Me.txtDepTaxRate.DecimalPlaces = 2
        Me.txtDepTaxRate.FieldCode = Nothing
        Me.txtDepTaxRate.FieldDesc = Nothing
        Me.txtDepTaxRate.FieldMaxLength = 0
        Me.txtDepTaxRate.FieldName = Nothing
        Me.txtDepTaxRate.isCalculatedField = False
        Me.txtDepTaxRate.IsSourceFromTable = False
        Me.txtDepTaxRate.IsSourceFromValueList = False
        Me.txtDepTaxRate.IsUnique = False
        Me.txtDepTaxRate.Location = New System.Drawing.Point(114, 153)
        Me.txtDepTaxRate.MendatroryField = False
        Me.txtDepTaxRate.MyLinkLable1 = Nothing
        Me.txtDepTaxRate.MyLinkLable2 = Nothing
        Me.txtDepTaxRate.Name = "txtDepTaxRate"
        Me.txtDepTaxRate.ReferenceFieldDesc = Nothing
        Me.txtDepTaxRate.ReferenceFieldName = Nothing
        Me.txtDepTaxRate.ReferenceTableName = Nothing
        Me.txtDepTaxRate.Size = New System.Drawing.Size(143, 20)
        Me.txtDepTaxRate.TabIndex = 216
        Me.txtDepTaxRate.Text = "0"
        Me.txtDepTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDepTaxRate.Value = 0R
        '
        'txtDepRate
        '
        Me.txtDepRate.BackColor = System.Drawing.Color.White
        Me.txtDepRate.CalculationExpression = Nothing
        Me.txtDepRate.DecimalPlaces = 2
        Me.txtDepRate.FieldCode = Nothing
        Me.txtDepRate.FieldDesc = Nothing
        Me.txtDepRate.FieldMaxLength = 0
        Me.txtDepRate.FieldName = Nothing
        Me.txtDepRate.isCalculatedField = False
        Me.txtDepRate.IsSourceFromTable = False
        Me.txtDepRate.IsSourceFromValueList = False
        Me.txtDepRate.IsUnique = False
        Me.txtDepRate.Location = New System.Drawing.Point(114, 132)
        Me.txtDepRate.MendatroryField = False
        Me.txtDepRate.MyLinkLable1 = Nothing
        Me.txtDepRate.MyLinkLable2 = Nothing
        Me.txtDepRate.Name = "txtDepRate"
        Me.txtDepRate.ReferenceFieldDesc = Nothing
        Me.txtDepRate.ReferenceFieldName = Nothing
        Me.txtDepRate.ReferenceTableName = Nothing
        Me.txtDepRate.Size = New System.Drawing.Size(143, 20)
        Me.txtDepRate.TabIndex = 215
        Me.txtDepRate.Text = "0"
        Me.txtDepRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDepRate.Value = 0R
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(263, 241)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel12.TabIndex = 233
        Me.MyLabel12.Text = "Salvage Value"
        '
        'lblDepMethodTax
        '
        Me.lblDepMethodTax.AutoSize = False
        Me.lblDepMethodTax.BorderVisible = True
        Me.lblDepMethodTax.FieldName = Nothing
        Me.lblDepMethodTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepMethodTax.Location = New System.Drawing.Point(259, 73)
        Me.lblDepMethodTax.Name = "lblDepMethodTax"
        Me.lblDepMethodTax.Size = New System.Drawing.Size(287, 18)
        Me.lblDepMethodTax.TabIndex = 231
        Me.lblDepMethodTax.TextWrap = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(8, 74)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel13.TabIndex = 232
        Me.MyLabel13.Text = "Dep. Mehod ( Tax )"
        '
        'txtDepMethodTax
        '
        Me.txtDepMethodTax.CalculationExpression = Nothing
        Me.txtDepMethodTax.FieldCode = Nothing
        Me.txtDepMethodTax.FieldDesc = Nothing
        Me.txtDepMethodTax.FieldMaxLength = 0
        Me.txtDepMethodTax.FieldName = Nothing
        Me.txtDepMethodTax.isCalculatedField = False
        Me.txtDepMethodTax.IsSourceFromTable = False
        Me.txtDepMethodTax.IsSourceFromValueList = False
        Me.txtDepMethodTax.IsUnique = False
        Me.txtDepMethodTax.Location = New System.Drawing.Point(114, 72)
        Me.txtDepMethodTax.MendatroryField = False
        Me.txtDepMethodTax.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepMethodTax.MyLinkLable1 = Me.MyLabel13
        Me.txtDepMethodTax.MyLinkLable2 = Me.lblDepMethodTax
        Me.txtDepMethodTax.MyReadOnly = False
        Me.txtDepMethodTax.MyShowMasterFormButton = False
        Me.txtDepMethodTax.Name = "txtDepMethodTax"
        Me.txtDepMethodTax.ReferenceFieldDesc = Nothing
        Me.txtDepMethodTax.ReferenceFieldName = Nothing
        Me.txtDepMethodTax.ReferenceTableName = Nothing
        Me.txtDepMethodTax.Size = New System.Drawing.Size(143, 20)
        Me.txtDepMethodTax.TabIndex = 212
        Me.txtDepMethodTax.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(9, 155)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(81, 18)
        Me.MyLabel10.TabIndex = 228
        Me.MyLabel10.Text = "Dep. Rate (Tax)"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(9, 198)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel8.TabIndex = 229
        Me.MyLabel8.Text = "Source Org. Value"
        '
        'txtEstLife
        '
        Me.txtEstLife.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEstLife.CalculationExpression = Nothing
        Me.txtEstLife.DecimalPlaces = 0
        Me.txtEstLife.FieldCode = Nothing
        Me.txtEstLife.FieldDesc = Nothing
        Me.txtEstLife.FieldMaxLength = 0
        Me.txtEstLife.FieldName = Nothing
        Me.txtEstLife.isCalculatedField = False
        Me.txtEstLife.IsSourceFromTable = False
        Me.txtEstLife.IsSourceFromValueList = False
        Me.txtEstLife.IsUnique = False
        Me.txtEstLife.Location = New System.Drawing.Point(114, 175)
        Me.txtEstLife.MendatroryField = False
        Me.txtEstLife.MyLinkLable1 = Me.MyLabel5
        Me.txtEstLife.MyLinkLable2 = Nothing
        Me.txtEstLife.Name = "txtEstLife"
        Me.txtEstLife.ReferenceFieldDesc = Nothing
        Me.txtEstLife.ReferenceFieldName = Nothing
        Me.txtEstLife.ReferenceTableName = Nothing
        Me.txtEstLife.Size = New System.Drawing.Size(143, 20)
        Me.txtEstLife.TabIndex = 217
        Me.txtEstLife.Text = "0"
        Me.txtEstLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEstLife.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(9, 177)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(98, 18)
        Me.MyLabel5.TabIndex = 230
        Me.MyLabel5.Text = "Estimat. Life (Year)"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(9, 219)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel7.TabIndex = 227
        Me.MyLabel7.Text = "Source Value"
        '
        'lblKMReading
        '
        Me.lblKMReading.FieldName = Nothing
        Me.lblKMReading.Location = New System.Drawing.Point(9, 134)
        Me.lblKMReading.Name = "lblKMReading"
        Me.lblKMReading.Size = New System.Drawing.Size(90, 18)
        Me.lblKMReading.TabIndex = 226
        Me.lblKMReading.Text = "Dep. Rate (Book)"
        '
        'lblDepPeriod
        '
        Me.lblDepPeriod.AutoSize = False
        Me.lblDepPeriod.BorderVisible = True
        Me.lblDepPeriod.FieldName = Nothing
        Me.lblDepPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepPeriod.Location = New System.Drawing.Point(260, 93)
        Me.lblDepPeriod.Name = "lblDepPeriod"
        Me.lblDepPeriod.Size = New System.Drawing.Size(287, 18)
        Me.lblDepPeriod.TabIndex = 225
        Me.lblDepPeriod.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 114)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel2.TabIndex = 224
        Me.MyLabel2.Text = "Start Date"
        '
        'txtStartDate
        '
        Me.txtStartDate.CalculationExpression = Nothing
        Me.txtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtStartDate.FieldCode = Nothing
        Me.txtStartDate.FieldDesc = Nothing
        Me.txtStartDate.FieldMaxLength = 0
        Me.txtStartDate.FieldName = Nothing
        Me.txtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStartDate.isCalculatedField = False
        Me.txtStartDate.IsSourceFromTable = False
        Me.txtStartDate.IsSourceFromValueList = False
        Me.txtStartDate.IsUnique = False
        Me.txtStartDate.Location = New System.Drawing.Point(114, 113)
        Me.txtStartDate.MendatroryField = False
        Me.txtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.MyLinkLable1 = Me.MyLabel2
        Me.txtStartDate.MyLinkLable2 = Nothing
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.ReferenceFieldDesc = Nothing
        Me.txtStartDate.ReferenceFieldName = Nothing
        Me.txtStartDate.ReferenceTableName = Nothing
        Me.txtStartDate.Size = New System.Drawing.Size(77, 18)
        Me.txtStartDate.TabIndex = 214
        Me.txtStartDate.TabStop = False
        Me.txtStartDate.Text = "13/06/2011"
        Me.txtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDepMethod
        '
        Me.lblDepMethod.AutoSize = False
        Me.lblDepMethod.BorderVisible = True
        Me.lblDepMethod.FieldName = Nothing
        Me.lblDepMethod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepMethod.Location = New System.Drawing.Point(260, 52)
        Me.lblDepMethod.Name = "lblDepMethod"
        Me.lblDepMethod.Size = New System.Drawing.Size(287, 18)
        Me.lblDepMethod.TabIndex = 221
        Me.lblDepMethod.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 94)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel3.TabIndex = 223
        Me.MyLabel3.Text = "Dep. Period"
        '
        'txtDepPeriod
        '
        Me.txtDepPeriod.CalculationExpression = Nothing
        Me.txtDepPeriod.FieldCode = Nothing
        Me.txtDepPeriod.FieldDesc = Nothing
        Me.txtDepPeriod.FieldMaxLength = 0
        Me.txtDepPeriod.FieldName = Nothing
        Me.txtDepPeriod.isCalculatedField = False
        Me.txtDepPeriod.IsSourceFromTable = False
        Me.txtDepPeriod.IsSourceFromValueList = False
        Me.txtDepPeriod.IsUnique = False
        Me.txtDepPeriod.Location = New System.Drawing.Point(114, 93)
        Me.txtDepPeriod.MendatroryField = False
        Me.txtDepPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepPeriod.MyLinkLable1 = Me.MyLabel3
        Me.txtDepPeriod.MyLinkLable2 = Me.lblDepPeriod
        Me.txtDepPeriod.MyReadOnly = False
        Me.txtDepPeriod.MyShowMasterFormButton = False
        Me.txtDepPeriod.Name = "txtDepPeriod"
        Me.txtDepPeriod.ReferenceFieldDesc = Nothing
        Me.txtDepPeriod.ReferenceFieldName = Nothing
        Me.txtDepPeriod.ReferenceTableName = Nothing
        Me.txtDepPeriod.Size = New System.Drawing.Size(143, 19)
        Me.txtDepPeriod.TabIndex = 213
        Me.txtDepPeriod.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(9, 53)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel9.TabIndex = 222
        Me.MyLabel9.Text = "Dep. Mehod (Book)"
        '
        'txtDepMethod
        '
        Me.txtDepMethod.CalculationExpression = Nothing
        Me.txtDepMethod.FieldCode = Nothing
        Me.txtDepMethod.FieldDesc = Nothing
        Me.txtDepMethod.FieldMaxLength = 0
        Me.txtDepMethod.FieldName = Nothing
        Me.txtDepMethod.isCalculatedField = False
        Me.txtDepMethod.IsSourceFromTable = False
        Me.txtDepMethod.IsSourceFromValueList = False
        Me.txtDepMethod.IsUnique = False
        Me.txtDepMethod.Location = New System.Drawing.Point(114, 52)
        Me.txtDepMethod.MendatroryField = False
        Me.txtDepMethod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepMethod.MyLinkLable1 = Me.MyLabel9
        Me.txtDepMethod.MyLinkLable2 = Me.lblDepMethod
        Me.txtDepMethod.MyReadOnly = False
        Me.txtDepMethod.MyShowMasterFormButton = False
        Me.txtDepMethod.Name = "txtDepMethod"
        Me.txtDepMethod.ReferenceFieldDesc = Nothing
        Me.txtDepMethod.ReferenceFieldName = Nothing
        Me.txtDepMethod.ReferenceTableName = Nothing
        Me.txtDepMethod.Size = New System.Drawing.Size(143, 19)
        Me.txtDepMethod.TabIndex = 211
        Me.txtDepMethod.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(114, 10)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(264, 20)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(702, 364)
        Me.SplitContainer1.SplitterDistance = 329
        Me.SplitContainer1.TabIndex = 4
        '
        'frmAssetBookMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 364)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAssetBookMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Book "
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTaxDepType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxDepType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDepType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnetvalue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalvageRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalvageValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceOrgValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepMethodTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEstLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblGroup As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cboTaxDepType As common.Controls.MyComboBox
    Friend WithEvents lblTaxDepType As common.Controls.MyLabel
    Friend WithEvents cboDepType As common.Controls.MyComboBox
    Friend WithEvents lblDepType As common.Controls.MyLabel
    Friend WithEvents txtnetvalue As common.MyNumBox
    Friend WithEvents lblNetValue As common.Controls.MyLabel
    Friend WithEvents txtSalvageRate As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtSalvageValue As common.MyNumBox
    Friend WithEvents txtSourceValue As common.MyNumBox
    Friend WithEvents txtSourceOrgValue As common.MyNumBox
    Friend WithEvents txtDepTaxRate As common.MyNumBox
    Friend WithEvents txtDepRate As common.MyNumBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblDepMethodTax As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtDepMethodTax As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtEstLife As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblKMReading As common.Controls.MyLabel
    Friend WithEvents lblDepPeriod As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDepMethod As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDepPeriod As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtDepMethod As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
End Class

