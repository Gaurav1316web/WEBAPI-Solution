<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostCetreTypeMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostCetreTypeMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.labdepartment = New common.Controls.MyLabel()
        Me.Labdepartmentcost = New common.Controls.MyLabel()
        Me.Txtdepartmentcost = New common.UserControls.txtFinder()
        Me.lbldesid = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyLabel()
        Me.lblUnitDesc = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCostCenterType = New common.UserControls.txtFinder()
        Me.txtUnitCode = New common.UserControls.txtFinder()
        Me.lblDepartmentDes = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtDepartment = New common.UserControls.txtFinder()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtName = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.labdepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Labdepartmentcost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentDes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(671, 293)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(671, 20)
        Me.RadMenu2.TabIndex = 12
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport})
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
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Size = New System.Drawing.Size(671, 264)
        Me.SplitContainer2.SplitterDistance = 235
        Me.SplitContainer2.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.labdepartment)
        Me.RadGroupBox1.Controls.Add(Me.Labdepartmentcost)
        Me.RadGroupBox1.Controls.Add(Me.Txtdepartmentcost)
        Me.RadGroupBox1.Controls.Add(Me.lbldesid)
        Me.RadGroupBox1.Controls.Add(Me.txtdes)
        Me.RadGroupBox1.Controls.Add(Me.lblUnitDesc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtCostCenterType)
        Me.RadGroupBox1.Controls.Add(Me.txtUnitCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDepartmentDes)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.txtDepartment)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.txtName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(671, 235)
        Me.RadGroupBox1.TabIndex = 2
        '
        'labdepartment
        '
        Me.labdepartment.FieldName = Nothing
        Me.labdepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labdepartment.Location = New System.Drawing.Point(9, 66)
        Me.labdepartment.Name = "labdepartment"
        Me.labdepartment.Size = New System.Drawing.Size(65, 16)
        Me.labdepartment.TabIndex = 106
        Me.labdepartment.Text = "Department"
        '
        'Labdepartmentcost
        '
        Me.Labdepartmentcost.AutoSize = False
        Me.Labdepartmentcost.BorderVisible = True
        Me.Labdepartmentcost.FieldName = Nothing
        Me.Labdepartmentcost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labdepartmentcost.Location = New System.Drawing.Point(297, 64)
        Me.Labdepartmentcost.Name = "Labdepartmentcost"
        Me.Labdepartmentcost.Size = New System.Drawing.Size(181, 18)
        Me.Labdepartmentcost.TabIndex = 105
        Me.Labdepartmentcost.TextWrap = False
        '
        'Txtdepartmentcost
        '
        Me.Txtdepartmentcost.CalculationExpression = Nothing
        Me.Txtdepartmentcost.FieldCode = Nothing
        Me.Txtdepartmentcost.FieldDesc = Nothing
        Me.Txtdepartmentcost.FieldMaxLength = 0
        Me.Txtdepartmentcost.FieldName = Nothing
        Me.Txtdepartmentcost.isCalculatedField = False
        Me.Txtdepartmentcost.IsSourceFromTable = False
        Me.Txtdepartmentcost.IsSourceFromValueList = False
        Me.Txtdepartmentcost.IsUnique = False
        Me.Txtdepartmentcost.Location = New System.Drawing.Point(128, 63)
        Me.Txtdepartmentcost.MendatroryField = True
        Me.Txtdepartmentcost.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtdepartmentcost.MyLinkLable1 = Nothing
        Me.Txtdepartmentcost.MyLinkLable2 = Nothing
        Me.Txtdepartmentcost.MyReadOnly = False
        Me.Txtdepartmentcost.MyShowMasterFormButton = False
        Me.Txtdepartmentcost.Name = "Txtdepartmentcost"
        Me.Txtdepartmentcost.ReferenceFieldDesc = Nothing
        Me.Txtdepartmentcost.ReferenceFieldName = Nothing
        Me.Txtdepartmentcost.ReferenceTableName = Nothing
        Me.Txtdepartmentcost.Size = New System.Drawing.Size(163, 19)
        Me.Txtdepartmentcost.TabIndex = 104
        Me.Txtdepartmentcost.Value = ""
        '
        'lbldesid
        '
        Me.lbldesid.FieldName = Nothing
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(9, 115)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(63, 16)
        Me.lbldesid.TabIndex = 38
        Me.lbldesid.Text = "Cost  Code"
        '
        'txtdes
        '
        Me.txtdes.AutoSize = False
        Me.txtdes.BorderVisible = True
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(297, 112)
        Me.txtdes.Name = "txtdes"
        Me.txtdes.Size = New System.Drawing.Size(181, 18)
        Me.txtdes.TabIndex = 103
        Me.txtdes.TextWrap = False
        '
        'lblUnitDesc
        '
        Me.lblUnitDesc.AutoSize = False
        Me.lblUnitDesc.BorderVisible = True
        Me.lblUnitDesc.FieldName = Nothing
        Me.lblUnitDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitDesc.Location = New System.Drawing.Point(297, 88)
        Me.lblUnitDesc.Name = "lblUnitDesc"
        Me.lblUnitDesc.Size = New System.Drawing.Size(181, 18)
        Me.lblUnitDesc.TabIndex = 102
        Me.lblUnitDesc.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 90)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 100
        Me.MyLabel1.Text = "Unit Code"
        '
        'txtCostCenterType
        '
        Me.txtCostCenterType.CalculationExpression = Nothing
        Me.txtCostCenterType.FieldCode = Nothing
        Me.txtCostCenterType.FieldDesc = Nothing
        Me.txtCostCenterType.FieldMaxLength = 0
        Me.txtCostCenterType.FieldName = Nothing
        Me.txtCostCenterType.isCalculatedField = False
        Me.txtCostCenterType.IsSourceFromTable = False
        Me.txtCostCenterType.IsSourceFromValueList = False
        Me.txtCostCenterType.IsUnique = False
        Me.txtCostCenterType.Location = New System.Drawing.Point(128, 112)
        Me.txtCostCenterType.MendatroryField = True
        Me.txtCostCenterType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCenterType.MyLinkLable1 = Nothing
        Me.txtCostCenterType.MyLinkLable2 = Nothing
        Me.txtCostCenterType.MyReadOnly = False
        Me.txtCostCenterType.MyShowMasterFormButton = False
        Me.txtCostCenterType.Name = "txtCostCenterType"
        Me.txtCostCenterType.ReferenceFieldDesc = Nothing
        Me.txtCostCenterType.ReferenceFieldName = Nothing
        Me.txtCostCenterType.ReferenceTableName = Nothing
        Me.txtCostCenterType.Size = New System.Drawing.Size(163, 19)
        Me.txtCostCenterType.TabIndex = 99
        Me.txtCostCenterType.Value = ""
        '
        'txtUnitCode
        '
        Me.txtUnitCode.CalculationExpression = Nothing
        Me.txtUnitCode.FieldCode = Nothing
        Me.txtUnitCode.FieldDesc = Nothing
        Me.txtUnitCode.FieldMaxLength = 0
        Me.txtUnitCode.FieldName = Nothing
        Me.txtUnitCode.isCalculatedField = False
        Me.txtUnitCode.IsSourceFromTable = False
        Me.txtUnitCode.IsSourceFromValueList = False
        Me.txtUnitCode.IsUnique = False
        Me.txtUnitCode.Location = New System.Drawing.Point(128, 87)
        Me.txtUnitCode.MendatroryField = True
        Me.txtUnitCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitCode.MyLinkLable1 = Nothing
        Me.txtUnitCode.MyLinkLable2 = Nothing
        Me.txtUnitCode.MyReadOnly = False
        Me.txtUnitCode.MyShowMasterFormButton = False
        Me.txtUnitCode.Name = "txtUnitCode"
        Me.txtUnitCode.ReferenceFieldDesc = Nothing
        Me.txtUnitCode.ReferenceFieldName = Nothing
        Me.txtUnitCode.ReferenceTableName = Nothing
        Me.txtUnitCode.Size = New System.Drawing.Size(163, 19)
        Me.txtUnitCode.TabIndex = 98
        Me.txtUnitCode.Value = ""
        '
        'lblDepartmentDes
        '
        Me.lblDepartmentDes.AutoSize = False
        Me.lblDepartmentDes.BorderVisible = True
        Me.lblDepartmentDes.FieldName = Nothing
        Me.lblDepartmentDes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartmentDes.Location = New System.Drawing.Point(297, 164)
        Me.lblDepartmentDes.Name = "lblDepartmentDes"
        Me.lblDepartmentDes.Size = New System.Drawing.Size(181, 18)
        Me.lblDepartmentDes.TabIndex = 97
        Me.lblDepartmentDes.TextWrap = False
        Me.lblDepartmentDes.Visible = False
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(128, 9)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(325, 20)
        Me.txtCode.TabIndex = 20
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(9, 13)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(13, 162)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel9.TabIndex = 96
        Me.MyLabel9.Text = "Department"
        Me.MyLabel9.Visible = False
        '
        'txtDepartment
        '
        Me.txtDepartment.CalculationExpression = Nothing
        Me.txtDepartment.FieldCode = Nothing
        Me.txtDepartment.FieldDesc = Nothing
        Me.txtDepartment.FieldMaxLength = 0
        Me.txtDepartment.FieldName = Nothing
        Me.txtDepartment.isCalculatedField = False
        Me.txtDepartment.IsSourceFromTable = False
        Me.txtDepartment.IsSourceFromValueList = False
        Me.txtDepartment.IsUnique = False
        Me.txtDepartment.Location = New System.Drawing.Point(128, 162)
        Me.txtDepartment.MendatroryField = True
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.MyLabel9
        Me.txtDepartment.MyLinkLable2 = Me.lblDepartmentDes
        Me.txtDepartment.MyReadOnly = False
        Me.txtDepartment.MyShowMasterFormButton = False
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReferenceFieldDesc = Nothing
        Me.txtDepartment.ReferenceFieldName = Nothing
        Me.txtDepartment.ReferenceTableName = Nothing
        Me.txtDepartment.Size = New System.Drawing.Size(163, 20)
        Me.txtDepartment.TabIndex = 95
        Me.txtDepartment.Value = ""
        Me.txtDepartment.Visible = False
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(456, 8)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 21)
        Me.btnNew.TabIndex = 19
        Me.btnNew.Text = " "
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
        Me.txtName.Location = New System.Drawing.Point(128, 35)
        Me.txtName.MaxLength = 100
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(350, 20)
        Me.txtName.TabIndex = 2
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
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(2, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(73, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(601, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'FrmCostCetreTypeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 293)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCostCetreTypeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cost Ceter Type Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.labdepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Labdepartmentcost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentDes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDepartmentDes As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtDepartment As common.UserControls.txtFinder
    Friend WithEvents txtUnitCode As common.UserControls.txtFinder
    Friend WithEvents txtCostCenterType As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdes As common.Controls.MyLabel
    Friend WithEvents lblUnitDesc As common.Controls.MyLabel
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents labdepartment As common.Controls.MyLabel
    Friend WithEvents Labdepartmentcost As common.Controls.MyLabel
    Friend WithEvents Txtdepartmentcost As common.UserControls.txtFinder
End Class

