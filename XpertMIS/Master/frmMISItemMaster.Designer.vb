Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMISItemMaster
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.

    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtUOM = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtGPCode = New common.Controls.MyTextBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
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
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtUOM)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtGPCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtName)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(519, 128)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(282, 73)
        Me.txtUOM.MaxLength = 50
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyLinkLable1 = Me.MyLabel1
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(141, 20)
        Me.txtUOM.TabIndex = 24
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(10, 73)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel1.TabIndex = 20
        Me.MyLabel1.Text = "Group Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(246, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel2.TabIndex = 22
        Me.MyLabel2.Text = "UOM"
        '
        'txtGPCode
        '
        Me.txtGPCode.CalculationExpression = Nothing
        Me.txtGPCode.FieldCode = Nothing
        Me.txtGPCode.FieldDesc = Nothing
        Me.txtGPCode.FieldMaxLength = 0
        Me.txtGPCode.FieldName = Nothing
        Me.txtGPCode.isCalculatedField = False
        Me.txtGPCode.IsSourceFromTable = False
        Me.txtGPCode.IsSourceFromValueList = False
        Me.txtGPCode.IsUnique = False
        Me.txtGPCode.Location = New System.Drawing.Point(89, 72)
        Me.txtGPCode.MaxLength = 50
        Me.txtGPCode.MendatroryField = False
        Me.txtGPCode.MyLinkLable1 = Me.MyLabel1
        Me.txtGPCode.MyLinkLable2 = Nothing
        Me.txtGPCode.Name = "txtGPCode"
        Me.txtGPCode.ReferenceFieldDesc = Nothing
        Me.txtGPCode.ReferenceFieldName = Nothing
        Me.txtGPCode.ReferenceTableName = Nothing
        Me.txtGPCode.Size = New System.Drawing.Size(148, 20)
        Me.txtGPCode.TabIndex = 21
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
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPMIS.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(311, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
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
        Me.txtName.Location = New System.Drawing.Point(89, 45)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(334, 20)
        Me.txtName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(10, 46)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(36, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Name"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(90, 22)
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
        Me.RadLabel1.Location = New System.Drawing.Point(10, 23)
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
        Me.btnClose.Location = New System.Drawing.Point(755, 6)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(824, 478)
        Me.SplitContainer1.SplitterDistance = 447
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(824, 20)
        Me.RadMenu2.TabIndex = 10
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
        'frmMISItemMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 478)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMISItemMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MIS Item Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtGPCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtUOM As common.Controls.MyTextBox
End Class
