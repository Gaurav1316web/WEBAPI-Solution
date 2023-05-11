Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStateCode
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.rgboxStateCode = New Telerik.WinControls.UI.RadGroupBox()
        Me.FndStateCodeNew = New common.UserControls.txtNavigator()
        Me.lblStateCode = New common.Controls.MyLabel()
        Me.lblStateName = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtStateName = New common.Controls.MyTextBox()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgboxStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgboxStateCode.SuspendLayout()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(408, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemImport, Me.RadMenuItemExport, Me.RadMenuItemClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItemImport
        '
        Me.RadMenuItemImport.AccessibleDescription = "Import"
        Me.RadMenuItemImport.AccessibleName = "Import"
        Me.RadMenuItemImport.Name = "RadMenuItemImport"
        Me.RadMenuItemImport.Text = "Import"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "Export"
        Me.RadMenuItemExport.AccessibleName = "Export"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "Export"
        '
        'RadMenuItemClose
        '
        Me.RadMenuItemClose.AccessibleDescription = "Close"
        Me.RadMenuItemClose.AccessibleName = "Close"
        Me.RadMenuItemClose.Name = "RadMenuItemClose"
        Me.RadMenuItemClose.Text = "Close"
        '
        'rgboxStateCode
        '
        Me.rgboxStateCode.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgboxStateCode.Controls.Add(Me.FndStateCodeNew)
        Me.rgboxStateCode.Controls.Add(Me.lblStateCode)
        Me.rgboxStateCode.Controls.Add(Me.lblStateName)
        Me.rgboxStateCode.Controls.Add(Me.btnReset)
        Me.rgboxStateCode.Controls.Add(Me.txtStateName)
        Me.rgboxStateCode.HeaderText = ""
        Me.rgboxStateCode.Location = New System.Drawing.Point(14, 14)
        Me.rgboxStateCode.Name = "rgboxStateCode"
        Me.rgboxStateCode.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rgboxStateCode.Size = New System.Drawing.Size(373, 124)
        Me.rgboxStateCode.TabIndex = 1
        '
        'FndStateCodeNew
        '
        Me.FndStateCodeNew.AccessibleName = "FndStateCodeNew"
        Me.FndStateCodeNew.FieldName = Nothing
        Me.FndStateCodeNew.Location = New System.Drawing.Point(113, 24)
        Me.FndStateCodeNew.MendatroryField = False
        Me.FndStateCodeNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.FndStateCodeNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FndStateCodeNew.MyLinkLable1 = Me.lblStateCode
        Me.FndStateCodeNew.MyLinkLable2 = Nothing
        Me.FndStateCodeNew.MyMaxLength = 32767
        Me.FndStateCodeNew.MyReadOnly = False
        Me.FndStateCodeNew.Name = "FndStateCodeNew"
        Me.FndStateCodeNew.Size = New System.Drawing.Size(214, 21)
        Me.FndStateCodeNew.TabIndex = 0
        Me.FndStateCodeNew.TabStop = False
        Me.FndStateCodeNew.Value = ""
        '
        'lblStateCode
        '
        Me.lblStateCode.FieldName = Nothing
        Me.lblStateCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateCode.Location = New System.Drawing.Point(29, 26)
        Me.lblStateCode.Name = "lblStateCode"
        Me.lblStateCode.Size = New System.Drawing.Size(63, 16)
        Me.lblStateCode.TabIndex = 0
        Me.lblStateCode.Text = "State Code"
        '
        'lblStateName
        '
        Me.lblStateName.FieldName = Nothing
        Me.lblStateName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateName.Location = New System.Drawing.Point(29, 54)
        Me.lblStateName.Name = "lblStateName"
        Me.lblStateName.Size = New System.Drawing.Size(66, 16)
        Me.lblStateName.TabIndex = 3
        Me.lblStateName.Text = "State Name"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(329, 25)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(16, 18)
        Me.btnReset.TabIndex = 1
        '
        'txtStateName
        '
        Me.txtStateName.CalculationExpression = Nothing
        Me.txtStateName.FieldCode = Nothing
        Me.txtStateName.FieldDesc = Nothing
        Me.txtStateName.FieldMaxLength = 0
        Me.txtStateName.FieldName = Nothing
        Me.txtStateName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateName.isCalculatedField = False
        Me.txtStateName.IsSourceFromTable = False
        Me.txtStateName.IsSourceFromValueList = False
        Me.txtStateName.IsUnique = False
        Me.txtStateName.Location = New System.Drawing.Point(113, 51)
        Me.txtStateName.MaxLength = 50
        Me.txtStateName.MendatroryField = False
        Me.txtStateName.MyLinkLable1 = Me.lblStateName
        Me.txtStateName.MyLinkLable2 = Nothing
        Me.txtStateName.Name = "txtStateName"
        Me.txtStateName.ReferenceFieldDesc = Nothing
        Me.txtStateName.ReferenceFieldName = Nothing
        Me.txtStateName.ReferenceTableName = Nothing
        Me.txtStateName.Size = New System.Drawing.Size(214, 18)
        Me.txtStateName.TabIndex = 2
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(312, 3)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 5
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(96, 3)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 4
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(26, 3)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 3
        Me.rbtnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rgboxStateCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(408, 182)
        Me.SplitContainer1.SplitterDistance = 146
        Me.SplitContainer1.TabIndex = 2
        '
        'frmStateCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 202)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmStateCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "State Code"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgboxStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgboxStateCode.ResumeLayout(False)
        Me.rgboxStateCode.PerformLayout()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rgboxStateCode As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtStateName As common.Controls.MyTextBox
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblStateCode As common.Controls.MyLabel
    Friend WithEvents lblStateName As common.Controls.MyLabel
    Friend WithEvents FndStateCodeNew As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

