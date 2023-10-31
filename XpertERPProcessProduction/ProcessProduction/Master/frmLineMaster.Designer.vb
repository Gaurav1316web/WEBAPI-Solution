<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLineMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLineMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtTimeFrame = New common.Controls.MyTextBox()
        Me.txtCapacity = New common.Controls.MyTextBox()
        Me.txtMachineRated = New common.Controls.MyTextBox()
        Me.txtMachineName = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndLine = New common.UserControls.txtNavigator()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtTimeFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMachineRated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMachineName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTimeFrame)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCapacity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMachineRated)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMachineName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLine)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(527, 325)
        Me.SplitContainer1.SplitterDistance = 296
        Me.SplitContainer1.TabIndex = 0
        '
        'txtTimeFrame
        '
        Me.txtTimeFrame.CalculationExpression = Nothing
        Me.txtTimeFrame.FieldCode = Nothing
        Me.txtTimeFrame.FieldDesc = Nothing
        Me.txtTimeFrame.FieldMaxLength = 0
        Me.txtTimeFrame.FieldName = Nothing
        Me.txtTimeFrame.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTimeFrame.isCalculatedField = False
        Me.txtTimeFrame.IsSourceFromTable = False
        Me.txtTimeFrame.IsSourceFromValueList = False
        Me.txtTimeFrame.IsUnique = False
        Me.txtTimeFrame.Location = New System.Drawing.Point(105, 123)
        Me.txtTimeFrame.MaxLength = 150
        Me.txtTimeFrame.MendatroryField = True
        Me.txtTimeFrame.MyLinkLable1 = Nothing
        Me.txtTimeFrame.MyLinkLable2 = Nothing
        Me.txtTimeFrame.Name = "txtTimeFrame"
        Me.txtTimeFrame.ReferenceFieldDesc = Nothing
        Me.txtTimeFrame.ReferenceFieldName = Nothing
        Me.txtTimeFrame.ReferenceTableName = Nothing
        Me.txtTimeFrame.Size = New System.Drawing.Size(249, 18)
        Me.txtTimeFrame.TabIndex = 51
        Me.txtTimeFrame.TabStop = False
        '
        'txtCapacity
        '
        Me.txtCapacity.CalculationExpression = Nothing
        Me.txtCapacity.FieldCode = Nothing
        Me.txtCapacity.FieldDesc = Nothing
        Me.txtCapacity.FieldMaxLength = 0
        Me.txtCapacity.FieldName = Nothing
        Me.txtCapacity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity.isCalculatedField = False
        Me.txtCapacity.IsSourceFromTable = False
        Me.txtCapacity.IsSourceFromValueList = False
        Me.txtCapacity.IsUnique = False
        Me.txtCapacity.Location = New System.Drawing.Point(105, 101)
        Me.txtCapacity.MaxLength = 150
        Me.txtCapacity.MendatroryField = True
        Me.txtCapacity.MyLinkLable1 = Nothing
        Me.txtCapacity.MyLinkLable2 = Nothing
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.ReferenceFieldDesc = Nothing
        Me.txtCapacity.ReferenceFieldName = Nothing
        Me.txtCapacity.ReferenceTableName = Nothing
        Me.txtCapacity.Size = New System.Drawing.Size(249, 18)
        Me.txtCapacity.TabIndex = 50
        Me.txtCapacity.TabStop = False
        '
        'txtMachineRated
        '
        Me.txtMachineRated.CalculationExpression = Nothing
        Me.txtMachineRated.FieldCode = Nothing
        Me.txtMachineRated.FieldDesc = Nothing
        Me.txtMachineRated.FieldMaxLength = 0
        Me.txtMachineRated.FieldName = Nothing
        Me.txtMachineRated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineRated.isCalculatedField = False
        Me.txtMachineRated.IsSourceFromTable = False
        Me.txtMachineRated.IsSourceFromValueList = False
        Me.txtMachineRated.IsUnique = False
        Me.txtMachineRated.Location = New System.Drawing.Point(105, 79)
        Me.txtMachineRated.MaxLength = 150
        Me.txtMachineRated.MendatroryField = True
        Me.txtMachineRated.MyLinkLable1 = Nothing
        Me.txtMachineRated.MyLinkLable2 = Nothing
        Me.txtMachineRated.Name = "txtMachineRated"
        Me.txtMachineRated.ReferenceFieldDesc = Nothing
        Me.txtMachineRated.ReferenceFieldName = Nothing
        Me.txtMachineRated.ReferenceTableName = Nothing
        Me.txtMachineRated.Size = New System.Drawing.Size(249, 18)
        Me.txtMachineRated.TabIndex = 49
        Me.txtMachineRated.TabStop = False
        '
        'txtMachineName
        '
        Me.txtMachineName.CalculationExpression = Nothing
        Me.txtMachineName.FieldCode = Nothing
        Me.txtMachineName.FieldDesc = Nothing
        Me.txtMachineName.FieldMaxLength = 0
        Me.txtMachineName.FieldName = Nothing
        Me.txtMachineName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineName.isCalculatedField = False
        Me.txtMachineName.IsSourceFromTable = False
        Me.txtMachineName.IsSourceFromValueList = False
        Me.txtMachineName.IsUnique = False
        Me.txtMachineName.Location = New System.Drawing.Point(105, 57)
        Me.txtMachineName.MaxLength = 150
        Me.txtMachineName.MendatroryField = True
        Me.txtMachineName.MyLinkLable1 = Nothing
        Me.txtMachineName.MyLinkLable2 = Nothing
        Me.txtMachineName.Name = "txtMachineName"
        Me.txtMachineName.ReferenceFieldDesc = Nothing
        Me.txtMachineName.ReferenceFieldName = Nothing
        Me.txtMachineName.ReferenceTableName = Nothing
        Me.txtMachineName.Size = New System.Drawing.Size(249, 18)
        Me.txtMachineName.TabIndex = 48
        Me.txtMachineName.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 124)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel5.TabIndex = 47
        Me.MyLabel5.Text = "Time Frame"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 102)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel4.TabIndex = 46
        Me.MyLabel4.Text = "Capacity "
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 80)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 45
        Me.MyLabel3.Text = "Machine Rated"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 58)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel2.TabIndex = 44
        Me.MyLabel2.Text = "Machine Name"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 36)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel1.TabIndex = 43
        Me.MyLabel1.Text = "Line No"
        '
        'btnReset
        '
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.Location = New System.Drawing.Point(340, 33)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(14, 21)
        Me.btnReset.TabIndex = 41
        Me.btnReset.Text = " "
        '
        'fndLine
        '
        Me.fndLine.FieldName = Nothing
        Me.fndLine.Location = New System.Drawing.Point(105, 33)
        Me.fndLine.MendatroryField = True
        Me.fndLine.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndLine.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndLine.MyLinkLable1 = Me.MyLabel1
        Me.fndLine.MyLinkLable2 = Nothing
        Me.fndLine.MyMaxLength = 30
        Me.fndLine.MyReadOnly = False
        Me.fndLine.Name = "fndLine"
        Me.fndLine.Size = New System.Drawing.Size(235, 21)
        Me.fndLine.TabIndex = 42
        Me.fndLine.Value = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(527, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(455, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 18)
        Me.btnClose.TabIndex = 18
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(71, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 17
        Me.btnDelete.Text = "Delete"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Export"
        Me.RadMenuItem1.AccessibleName = "Export"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'FrmLineMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 325)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmLineMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Line Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtTimeFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMachineRated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMachineName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtTimeFrame As common.Controls.MyTextBox
    Friend WithEvents txtCapacity As common.Controls.MyTextBox
    Friend WithEvents txtMachineRated As common.Controls.MyTextBox
    Friend WithEvents txtMachineName As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLine As common.UserControls.txtNavigator
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
End Class

