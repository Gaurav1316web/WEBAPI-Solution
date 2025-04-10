Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConveyanceRateMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConveyanceRateMaster))
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblOT_RATE = New common.Controls.MyLabel()
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
        Me.txtDistLimit = New common.MyNumBox()
        Me.txtConveyanceRate = New common.MyNumBox()
        Me.cboConvType = New common.Controls.MyComboBox()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOT_RATE, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.txtDistLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConveyanceRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboConvType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(21, 93)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Tag = ""
        Me.RadLabel3.Text = "Conveyance Rate"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Location = New System.Drawing.Point(21, 73)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(94, 18)
        Me.lblDescription.TabIndex = 27
        Me.lblDescription.Text = "Conveyance Type"
        '
        'lblOT_RATE
        '
        Me.lblOT_RATE.FieldName = Nothing
        Me.lblOT_RATE.Location = New System.Drawing.Point(21, 116)
        Me.lblOT_RATE.Name = "lblOT_RATE"
        Me.lblOT_RATE.Size = New System.Drawing.Size(76, 18)
        Me.lblOT_RATE.TabIndex = 23
        Me.lblOT_RATE.Text = "Distance Limit"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(351, 25)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
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
        Me.txtName.Location = New System.Drawing.Point(129, 50)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(381, 20)
        Me.txtName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(21, 51)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(129, 25)
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
        Me.RadLabel1.Location = New System.Drawing.Point(21, 26)
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
        Me.btnClose.Location = New System.Drawing.Point(488, 6)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDistLimit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtConveyanceRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboConvType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOT_RATE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(557, 216)
        Me.SplitContainer1.SplitterDistance = 185
        Me.SplitContainer1.TabIndex = 0
        '
        'txtDistLimit
        '
        Me.txtDistLimit.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDistLimit.CalculationExpression = Nothing
        Me.txtDistLimit.DecimalPlaces = 2
        Me.txtDistLimit.FieldCode = Nothing
        Me.txtDistLimit.FieldDesc = Nothing
        Me.txtDistLimit.FieldMaxLength = 0
        Me.txtDistLimit.FieldName = Nothing
        Me.txtDistLimit.isCalculatedField = False
        Me.txtDistLimit.IsSourceFromTable = False
        Me.txtDistLimit.IsSourceFromValueList = False
        Me.txtDistLimit.IsUnique = False
        Me.txtDistLimit.Location = New System.Drawing.Point(129, 116)
        Me.txtDistLimit.MaxLength = 6
        Me.txtDistLimit.MendatroryField = True
        Me.txtDistLimit.MyLinkLable1 = Nothing
        Me.txtDistLimit.MyLinkLable2 = Nothing
        Me.txtDistLimit.Name = "txtDistLimit"
        Me.txtDistLimit.ReferenceFieldDesc = Nothing
        Me.txtDistLimit.ReferenceFieldName = Nothing
        Me.txtDistLimit.ReferenceTableName = Nothing
        Me.txtDistLimit.Size = New System.Drawing.Size(194, 20)
        Me.txtDistLimit.TabIndex = 30
        Me.txtDistLimit.Text = "0"
        Me.txtDistLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistLimit.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'txtConveyanceRate
        '
        Me.txtConveyanceRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtConveyanceRate.CalculationExpression = Nothing
        Me.txtConveyanceRate.DecimalPlaces = 2
        Me.txtConveyanceRate.FieldCode = Nothing
        Me.txtConveyanceRate.FieldDesc = Nothing
        Me.txtConveyanceRate.FieldMaxLength = 0
        Me.txtConveyanceRate.FieldName = Nothing
        Me.txtConveyanceRate.isCalculatedField = False
        Me.txtConveyanceRate.IsSourceFromTable = False
        Me.txtConveyanceRate.IsSourceFromValueList = False
        Me.txtConveyanceRate.IsUnique = False
        Me.txtConveyanceRate.Location = New System.Drawing.Point(129, 93)
        Me.txtConveyanceRate.MaxLength = 6
        Me.txtConveyanceRate.MendatroryField = True
        Me.txtConveyanceRate.MyLinkLable1 = Nothing
        Me.txtConveyanceRate.MyLinkLable2 = Nothing
        Me.txtConveyanceRate.Name = "txtConveyanceRate"
        Me.txtConveyanceRate.ReferenceFieldDesc = Nothing
        Me.txtConveyanceRate.ReferenceFieldName = Nothing
        Me.txtConveyanceRate.ReferenceTableName = Nothing
        Me.txtConveyanceRate.Size = New System.Drawing.Size(194, 20)
        Me.txtConveyanceRate.TabIndex = 29
        Me.txtConveyanceRate.Text = "0"
        Me.txtConveyanceRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConveyanceRate.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'cboConvType
        '
        Me.cboConvType.AutoCompleteDisplayMember = Nothing
        Me.cboConvType.AutoCompleteValueMember = Nothing
        Me.cboConvType.BackColor = System.Drawing.Color.Transparent
        Me.cboConvType.CalculationExpression = Nothing
        Me.cboConvType.DropDownAnimationEnabled = True
        Me.cboConvType.FieldCode = Nothing
        Me.cboConvType.FieldDesc = Nothing
        Me.cboConvType.FieldMaxLength = 0
        Me.cboConvType.FieldName = Nothing
        Me.cboConvType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboConvType.isCalculatedField = False
        Me.cboConvType.IsSourceFromTable = False
        Me.cboConvType.IsSourceFromValueList = False
        Me.cboConvType.IsUnique = False
        Me.cboConvType.Location = New System.Drawing.Point(129, 73)
        Me.cboConvType.MendatroryField = False
        Me.cboConvType.MyLinkLable1 = Nothing
        Me.cboConvType.MyLinkLable2 = Nothing
        Me.cboConvType.Name = "cboConvType"
        Me.cboConvType.ReferenceFieldDesc = Nothing
        Me.cboConvType.ReferenceFieldName = Nothing
        Me.cboConvType.ReferenceTableName = Nothing
        Me.cboConvType.Size = New System.Drawing.Size(194, 18)
        Me.cboConvType.TabIndex = 28
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(557, 20)
        Me.RadMenu2.TabIndex = 14
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
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnHistory.Location = New System.Drawing.Point(414, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(68, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'frmConveyanceRateMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 216)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmConveyanceRateMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Conveyance Rate Master"
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOT_RATE, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.txtDistLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConveyanceRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboConvType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblOT_RATE As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboConvType As common.Controls.MyComboBox
    Friend WithEvents txtConveyanceRate As common.MyNumBox
    Friend WithEvents txtDistLimit As common.MyNumBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

