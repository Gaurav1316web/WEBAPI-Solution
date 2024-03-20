Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmODSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmODSheet))
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblOtName = New common.Controls.MyLabel()
        Me.txtOTCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblMaterialCarrying = New common.Controls.MyLabel()
        Me.txtMaterialCarrying = New common.Controls.MyTextBox()
        Me.lblPurpose = New common.Controls.MyLabel()
        Me.txtPurpose = New common.Controls.MyTextBox()
        Me.dtpTo = New common.Controls.MyDateTimePicker()
        Me.dtpFrom = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblMaterialCarrying, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterialCarrying, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurpose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurpose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.AutoSize = False
        Me.lblPayPeriod.BorderVisible = True
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Location = New System.Drawing.Point(317, 189)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(189, 18)
        Me.lblPayPeriod.TabIndex = 7
        Me.lblPayPeriod.Visible = False
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.CalculationExpression = Nothing
        Me.txtPayPeriod.FieldCode = Nothing
        Me.txtPayPeriod.FieldDesc = Nothing
        Me.txtPayPeriod.FieldMaxLength = 0
        Me.txtPayPeriod.FieldName = Nothing
        Me.txtPayPeriod.isCalculatedField = False
        Me.txtPayPeriod.IsSourceFromTable = False
        Me.txtPayPeriod.IsSourceFromValueList = False
        Me.txtPayPeriod.IsUnique = False
        Me.txtPayPeriod.Location = New System.Drawing.Point(132, 189)
        Me.txtPayPeriod.MendatroryField = True
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Me.MyLabel5
        Me.txtPayPeriod.MyLinkLable2 = Me.lblPayPeriod
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.ReferenceFieldDesc = Nothing
        Me.txtPayPeriod.ReferenceFieldName = Nothing
        Me.txtPayPeriod.ReferenceTableName = Nothing
        Me.txtPayPeriod.Size = New System.Drawing.Size(181, 18)
        Me.txtPayPeriod.TabIndex = 6
        Me.txtPayPeriod.Value = ""
        Me.txtPayPeriod.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(13, 189)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(88, 18)
        Me.MyLabel5.TabIndex = 34
        Me.MyLabel5.Text = "Pay Period Code"
        Me.MyLabel5.Visible = False
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(316, 49)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(189, 18)
        Me.lblEmpName.TabIndex = 3
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(131, 49)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.MyLabel3
        Me.txtEmpCode.MyLinkLable2 = Me.lblEmpName
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(181, 18)
        Me.txtEmpCode.TabIndex = 2
        Me.txtEmpCode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 49)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel3.TabIndex = 27
        Me.MyLabel3.Text = "Employee Code"
        '
        'lblOtName
        '
        Me.lblOtName.AutoSize = False
        Me.lblOtName.BorderVisible = True
        Me.lblOtName.FieldName = Nothing
        Me.lblOtName.Location = New System.Drawing.Point(316, 72)
        Me.lblOtName.Name = "lblOtName"
        Me.lblOtName.Size = New System.Drawing.Size(189, 18)
        Me.lblOtName.TabIndex = 5
        '
        'txtOTCode
        '
        Me.txtOTCode.CalculationExpression = Nothing
        Me.txtOTCode.FieldCode = Nothing
        Me.txtOTCode.FieldDesc = Nothing
        Me.txtOTCode.FieldMaxLength = 0
        Me.txtOTCode.FieldName = Nothing
        Me.txtOTCode.isCalculatedField = False
        Me.txtOTCode.IsSourceFromTable = False
        Me.txtOTCode.IsSourceFromValueList = False
        Me.txtOTCode.IsUnique = False
        Me.txtOTCode.Location = New System.Drawing.Point(131, 72)
        Me.txtOTCode.MendatroryField = True
        Me.txtOTCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTCode.MyLinkLable1 = Me.MyLabel1
        Me.txtOTCode.MyLinkLable2 = Me.lblOtName
        Me.txtOTCode.MyReadOnly = False
        Me.txtOTCode.MyShowMasterFormButton = False
        Me.txtOTCode.Name = "txtOTCode"
        Me.txtOTCode.ReferenceFieldDesc = Nothing
        Me.txtOTCode.ReferenceFieldName = Nothing
        Me.txtOTCode.ReferenceTableName = Nothing
        Me.txtOTCode.Size = New System.Drawing.Size(181, 18)
        Me.txtOTCode.TabIndex = 4
        Me.txtOTCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 72)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "OD Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(131, 25)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(12, 26)
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
        Me.btnClose.Location = New System.Drawing.Point(538, 6)
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
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMaterialCarrying)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMaterialCarrying)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPurpose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPurpose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOTCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(607, 306)
        Me.SplitContainer1.SplitterDistance = 275
        Me.SplitContainer1.TabIndex = 0
        '
        'lblMaterialCarrying
        '
        Me.lblMaterialCarrying.FieldName = Nothing
        Me.lblMaterialCarrying.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMaterialCarrying.Location = New System.Drawing.Point(13, 167)
        Me.lblMaterialCarrying.Name = "lblMaterialCarrying"
        Me.lblMaterialCarrying.Size = New System.Drawing.Size(87, 16)
        Me.lblMaterialCarrying.TabIndex = 423
        Me.lblMaterialCarrying.Text = "Material Carring"
        '
        'txtMaterialCarrying
        '
        Me.txtMaterialCarrying.AutoSize = False
        Me.txtMaterialCarrying.CalculationExpression = Nothing
        Me.txtMaterialCarrying.FieldCode = Nothing
        Me.txtMaterialCarrying.FieldDesc = Nothing
        Me.txtMaterialCarrying.FieldMaxLength = 0
        Me.txtMaterialCarrying.FieldName = Nothing
        Me.txtMaterialCarrying.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterialCarrying.isCalculatedField = False
        Me.txtMaterialCarrying.IsSourceFromTable = False
        Me.txtMaterialCarrying.IsSourceFromValueList = False
        Me.txtMaterialCarrying.IsUnique = False
        Me.txtMaterialCarrying.Location = New System.Drawing.Point(131, 167)
        Me.txtMaterialCarrying.MaxLength = 200
        Me.txtMaterialCarrying.MendatroryField = False
        Me.txtMaterialCarrying.Multiline = True
        Me.txtMaterialCarrying.MyLinkLable1 = Me.lblMaterialCarrying
        Me.txtMaterialCarrying.MyLinkLable2 = Nothing
        Me.txtMaterialCarrying.Name = "txtMaterialCarrying"
        Me.txtMaterialCarrying.ReferenceFieldDesc = Nothing
        Me.txtMaterialCarrying.ReferenceFieldName = Nothing
        Me.txtMaterialCarrying.ReferenceTableName = Nothing
        Me.txtMaterialCarrying.Size = New System.Drawing.Size(374, 19)
        Me.txtMaterialCarrying.TabIndex = 424
        '
        'lblPurpose
        '
        Me.lblPurpose.FieldName = Nothing
        Me.lblPurpose.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPurpose.Location = New System.Drawing.Point(13, 143)
        Me.lblPurpose.Name = "lblPurpose"
        Me.lblPurpose.Size = New System.Drawing.Size(48, 16)
        Me.lblPurpose.TabIndex = 421
        Me.lblPurpose.Text = "Purpose"
        '
        'txtPurpose
        '
        Me.txtPurpose.AutoSize = False
        Me.txtPurpose.CalculationExpression = Nothing
        Me.txtPurpose.FieldCode = Nothing
        Me.txtPurpose.FieldDesc = Nothing
        Me.txtPurpose.FieldMaxLength = 0
        Me.txtPurpose.FieldName = Nothing
        Me.txtPurpose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurpose.isCalculatedField = False
        Me.txtPurpose.IsSourceFromTable = False
        Me.txtPurpose.IsSourceFromValueList = False
        Me.txtPurpose.IsUnique = False
        Me.txtPurpose.Location = New System.Drawing.Point(131, 143)
        Me.txtPurpose.MaxLength = 200
        Me.txtPurpose.MendatroryField = False
        Me.txtPurpose.Multiline = True
        Me.txtPurpose.MyLinkLable1 = Me.lblPurpose
        Me.txtPurpose.MyLinkLable2 = Nothing
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.ReferenceFieldDesc = Nothing
        Me.txtPurpose.ReferenceFieldName = Nothing
        Me.txtPurpose.ReferenceTableName = Nothing
        Me.txtPurpose.Size = New System.Drawing.Size(374, 19)
        Me.txtPurpose.TabIndex = 422
        '
        'dtpTo
        '
        Me.dtpTo.CalculationExpression = Nothing
        Me.dtpTo.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTo.FieldCode = Nothing
        Me.dtpTo.FieldDesc = Nothing
        Me.dtpTo.FieldMaxLength = 0
        Me.dtpTo.FieldName = Nothing
        Me.dtpTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.isCalculatedField = False
        Me.dtpTo.IsSourceFromTable = False
        Me.dtpTo.IsSourceFromValueList = False
        Me.dtpTo.IsUnique = False
        Me.dtpTo.Location = New System.Drawing.Point(131, 118)
        Me.dtpTo.MendatroryField = True
        Me.dtpTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.MyLinkLable1 = Nothing
        Me.dtpTo.MyLinkLable2 = Nothing
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.ReferenceFieldDesc = Nothing
        Me.dtpTo.ReferenceFieldName = Nothing
        Me.dtpTo.ReferenceTableName = Nothing
        Me.dtpTo.Size = New System.Drawing.Size(181, 18)
        Me.dtpTo.TabIndex = 40
        Me.dtpTo.TabStop = False
        Me.dtpTo.Text = "03/05/2011 12:00 AM"
        Me.dtpTo.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'dtpFrom
        '
        Me.dtpFrom.CalculationExpression = Nothing
        Me.dtpFrom.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFrom.FieldCode = Nothing
        Me.dtpFrom.FieldDesc = Nothing
        Me.dtpFrom.FieldMaxLength = 0
        Me.dtpFrom.FieldName = Nothing
        Me.dtpFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.isCalculatedField = False
        Me.dtpFrom.IsSourceFromTable = False
        Me.dtpFrom.IsSourceFromValueList = False
        Me.dtpFrom.IsUnique = False
        Me.dtpFrom.Location = New System.Drawing.Point(131, 96)
        Me.dtpFrom.MendatroryField = True
        Me.dtpFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.MyLinkLable1 = Nothing
        Me.dtpFrom.MyLinkLable2 = Nothing
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.ReferenceFieldDesc = Nothing
        Me.dtpFrom.ReferenceFieldName = Nothing
        Me.dtpFrom.ReferenceTableName = Nothing
        Me.dtpFrom.Size = New System.Drawing.Size(181, 18)
        Me.dtpFrom.TabIndex = 39
        Me.dtpFrom.TabStop = False
        Me.dtpFrom.Text = "03/05/2011 12:00 AM"
        Me.dtpFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(13, 119)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(22, 18)
        Me.MyLabel2.TabIndex = 38
        Me.MyLabel2.Text = "To "
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(12, 98)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(35, 18)
        Me.RadLabel3.TabIndex = 37
        Me.RadLabel3.Text = "From "
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(607, 20)
        Me.RadMenu2.TabIndex = 0
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
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(355, 25)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPrint.Location = New System.Drawing.Point(148, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'frmODSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 306)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmODSheet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "OD Sheet"
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblMaterialCarrying, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterialCarrying, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurpose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurpose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblOtName As common.Controls.MyLabel
    Friend WithEvents txtOTCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpTo As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFrom As common.Controls.MyDateTimePicker
    Friend WithEvents lblMaterialCarrying As common.Controls.MyLabel
    Friend WithEvents txtMaterialCarrying As common.Controls.MyTextBox
    Friend WithEvents lblPurpose As common.Controls.MyLabel
    Friend WithEvents txtPurpose As common.Controls.MyTextBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

