<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VehicleTypeMaster
    Inherits Telerik.WinControls.UI.RadForm

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
Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu
Me.File = New Telerik.WinControls.UI.RadMenuItem
Me.Import = New Telerik.WinControls.UI.RadMenuItem
Me.Export = New Telerik.WinControls.UI.RadMenuItem
Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
Me.lblDescription = New common.Controls.MyLabel
Me.txtDescription = New common.Controls.MyTextBox
Me.fndComponentCode = New common.UserControls.txtNavigator
Me.lblVehicleTypeCode = New common.Controls.MyLabel
Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).BeginInit
Me.SplitContainer1.Panel1.SuspendLayout
Me.SplitContainer1.Panel2.SuspendLayout
Me.SplitContainer1.SuspendLayout
CType(Me.lblDescription,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.txtDescription,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblVehicleTypeCode,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtnsave,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtndelete,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtnclose,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me,System.ComponentModel.ISupportInitialize).BeginInit
Me.SuspendLayout
'
'rdmenu1
'
Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
Me.rdmenu1.Name = "rdmenu1"
Me.rdmenu1.Size = New System.Drawing.Size(589, 20)
Me.rdmenu1.TabIndex = 5
Me.rdmenu1.Text = "rdmenu"
'
'File
'
Me.File.AccessibleDescription = "File"
Me.File.AccessibleName = "File"
Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1})
Me.File.Name = "File"
Me.File.Text = "File"
Me.File.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'Import
'
Me.Import.AccessibleDescription = "Import"
Me.Import.AccessibleName = "Import"
Me.Import.Name = "Import"
Me.Import.Text = "Import"
Me.Import.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'Export
'
Me.Export.AccessibleDescription = "Export"
Me.Export.AccessibleName = "Export"
Me.Export.Name = "Export"
Me.Export.Text = "Export"
Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'RadMenuItem1
'
Me.RadMenuItem1.AccessibleDescription = "Exit"
Me.RadMenuItem1.AccessibleName = "Exit"
Me.RadMenuItem1.Name = "RadMenuItem1"
Me.RadMenuItem1.Text = "Exit"
Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'SplitContainer1
'
Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
Me.SplitContainer1.IsSplitterFixed = true
Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
Me.SplitContainer1.Name = "SplitContainer1"
Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
'
'SplitContainer1.Panel1
'
Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
Me.SplitContainer1.Panel1.Controls.Add(Me.fndComponentCode)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblVehicleTypeCode)
'
'SplitContainer1.Panel2
'
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
Me.SplitContainer1.Size = New System.Drawing.Size(589, 344)
Me.SplitContainer1.SplitterDistance = 306
Me.SplitContainer1.TabIndex = 6
'
'lblDescription
'
Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblDescription.Location = New System.Drawing.Point(12, 42)
Me.lblDescription.Name = "lblDescription"
Me.lblDescription.Size = New System.Drawing.Size(63, 18)
Me.lblDescription.TabIndex = 10
Me.lblDescription.Text = "Description"
'
'txtDescription
'
Me.txtDescription.Location = New System.Drawing.Point(119, 42)
Me.txtDescription.MaxLength = 100
Me.txtDescription.MendatroryField = false
Me.txtDescription.MyLinkLable1 = Nothing
Me.txtDescription.MyLinkLable2 = Nothing
Me.txtDescription.Name = "txtDescription"
Me.txtDescription.Size = New System.Drawing.Size(215, 20)
Me.txtDescription.TabIndex = 1
'
'fndComponentCode
'
Me.fndComponentCode.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
Me.fndComponentCode.Location = New System.Drawing.Point(119, 17)
Me.fndComponentCode.MendatroryField = false
Me.fndComponentCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
Me.fndComponentCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
Me.fndComponentCode.MyLinkLable1 = Me.lblVehicleTypeCode
Me.fndComponentCode.MyLinkLable2 = Nothing
Me.fndComponentCode.MyMaxLength = 12
Me.fndComponentCode.MyReadOnly = false
Me.fndComponentCode.Name = "fndComponentCode"
Me.fndComponentCode.Size = New System.Drawing.Size(215, 21)
Me.fndComponentCode.TabIndex = 0
Me.fndComponentCode.Value = ""
'
'lblVehicleTypeCode
'
Me.lblVehicleTypeCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
Me.lblVehicleTypeCode.Location = New System.Drawing.Point(12, 18)
Me.lblVehicleTypeCode.Name = "lblVehicleTypeCode"
Me.lblVehicleTypeCode.Size = New System.Drawing.Size(104, 18)
Me.lblVehicleTypeCode.TabIndex = 6
Me.lblVehicleTypeCode.Text = "Vehicle Type Code"
'
'rdbtnsave
'
Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtnsave.Location = New System.Drawing.Point(12, 4)
Me.rdbtnsave.Name = "rdbtnsave"
Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
Me.rdbtnsave.TabIndex = 0
Me.rdbtnsave.Text = "Save"
'
'rdbtndelete
'
Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtndelete.Location = New System.Drawing.Point(85, 4)
Me.rdbtndelete.Name = "rdbtndelete"
Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
Me.rdbtndelete.TabIndex = 1
Me.rdbtndelete.Text = "Delete"
'
'rdbtnclose
'
Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.rdbtnclose.Location = New System.Drawing.Point(503, 4)
Me.rdbtnclose.Name = "rdbtnclose"
Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
Me.rdbtnclose.TabIndex = 2
Me.rdbtnclose.Text = "Close"
'
'VehicleTypeMaster
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(589, 364)
Me.Controls.Add(Me.SplitContainer1)
Me.Controls.Add(Me.rdmenu1)
Me.Name = "VehicleTypeMaster"
'
'
'
Me.RootElement.ApplyShapeToControl = true
Me.Text = "VehicleTypeMaster"
Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).EndInit
Me.SplitContainer1.Panel1.ResumeLayout(false)
Me.SplitContainer1.Panel1.PerformLayout
Me.SplitContainer1.Panel2.ResumeLayout(false)
Me.SplitContainer1.ResumeLayout(false)
CType(Me.lblDescription,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.txtDescription,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblVehicleTypeCode,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtnsave,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtndelete,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtnclose,System.ComponentModel.ISupportInitialize).EndInit
CType(Me,System.ComponentModel.ISupportInitialize).EndInit
Me.ResumeLayout(false)
Me.PerformLayout

End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndComponentCode As common.UserControls.txtNavigator
    Friend WithEvents lblVehicleTypeCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
End Class

