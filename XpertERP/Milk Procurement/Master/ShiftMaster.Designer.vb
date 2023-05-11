<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShiftMaster
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
Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu
Me.File = New Telerik.WinControls.UI.RadMenuItem
Me.Import = New Telerik.WinControls.UI.RadMenuItem
Me.Export = New Telerik.WinControls.UI.RadMenuItem
Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
Me.lblShortName = New common.Controls.MyLabel
Me.txtShortName = New common.Controls.MyTextBox
Me.lblShiftName = New common.Controls.MyLabel
Me.txtShiftName = New common.Controls.MyTextBox
Me.ddlShiftCode = New common.Controls.MyComboBox
Me.lblShiftCode = New common.Controls.MyLabel
Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).BeginInit
Me.SplitContainer1.Panel1.SuspendLayout
Me.SplitContainer1.Panel2.SuspendLayout
Me.SplitContainer1.SuspendLayout
CType(Me.lblShortName,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.txtShortName,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblShiftName,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.txtShiftName,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.ddlShiftCode,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblShiftCode,System.ComponentModel.ISupportInitialize).BeginInit
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
Me.rdmenu1.Size = New System.Drawing.Size(568, 20)
Me.rdmenu1.TabIndex = 4
Me.rdmenu1.Text = "rdmenu"
'
'File
'
Me.File.AccessibleDescription = "File"
Me.File.AccessibleName = "File"
Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1, Me.RadMenuItem2})
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
'RadMenuItem2
'
Me.RadMenuItem2.AccessibleDescription = "RadMenuItem2"
Me.RadMenuItem2.AccessibleName = "RadMenuItem2"
Me.RadMenuItem2.Name = "RadMenuItem2"
Me.RadMenuItem2.Text = "RadMenuItem2"
Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'SplitContainer1
'
Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
Me.SplitContainer1.IsSplitterFixed = true
Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
Me.SplitContainer1.Name = "SplitContainer1"
Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
'
'SplitContainer1.Panel1
'
Me.SplitContainer1.Panel1.Controls.Add(Me.lblShortName)
Me.SplitContainer1.Panel1.Controls.Add(Me.txtShortName)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblShiftName)
Me.SplitContainer1.Panel1.Controls.Add(Me.txtShiftName)
Me.SplitContainer1.Panel1.Controls.Add(Me.ddlShiftCode)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblShiftCode)
'
'SplitContainer1.Panel2
'
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
Me.SplitContainer1.Size = New System.Drawing.Size(568, 359)
Me.SplitContainer1.SplitterDistance = 314
Me.SplitContainer1.TabIndex = 5
'
'lblShortName
'
Me.lblShortName.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblShortName.Location = New System.Drawing.Point(12, 67)
Me.lblShortName.Name = "lblShortName"
Me.lblShortName.Size = New System.Drawing.Size(63, 18)
Me.lblShortName.TabIndex = 23
Me.lblShortName.Text = "ShortName"
'
'txtShortName
'
Me.txtShortName.Location = New System.Drawing.Point(84, 67)
Me.txtShortName.MaxLength = 100
Me.txtShortName.MendatroryField = false
Me.txtShortName.MyLinkLable1 = Nothing
Me.txtShortName.MyLinkLable2 = Nothing
Me.txtShortName.Name = "txtShortName"
Me.txtShortName.Size = New System.Drawing.Size(215, 20)
Me.txtShortName.TabIndex = 2
'
'lblShiftName
'
Me.lblShiftName.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblShiftName.Location = New System.Drawing.Point(12, 43)
Me.lblShiftName.Name = "lblShiftName"
Me.lblShiftName.Size = New System.Drawing.Size(59, 18)
Me.lblShiftName.TabIndex = 21
Me.lblShiftName.Text = "ShiftName"
'
'txtShiftName
'
Me.txtShiftName.Location = New System.Drawing.Point(84, 43)
Me.txtShiftName.MaxLength = 100
Me.txtShiftName.MendatroryField = false
Me.txtShiftName.MyLinkLable1 = Nothing
Me.txtShiftName.MyLinkLable2 = Nothing
Me.txtShiftName.Name = "txtShiftName"
Me.txtShiftName.Size = New System.Drawing.Size(215, 20)
Me.txtShiftName.TabIndex = 1
'
'ddlShiftCode
'
Me.ddlShiftCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
RadListDataItem3.Tag = "Fat"
RadListDataItem3.Text = "Fat"
RadListDataItem3.TextWrap = true
RadListDataItem4.Tag = "SNF"
RadListDataItem4.Text = "SNF"
RadListDataItem4.TextWrap = true
Me.ddlShiftCode.Items.Add(RadListDataItem3)
Me.ddlShiftCode.Items.Add(RadListDataItem4)
Me.ddlShiftCode.Location = New System.Drawing.Point(84, 17)
Me.ddlShiftCode.MendatroryField = true
Me.ddlShiftCode.MyLinkLable1 = Nothing
Me.ddlShiftCode.MyLinkLable2 = Nothing
Me.ddlShiftCode.Name = "ddlShiftCode"
Me.ddlShiftCode.Size = New System.Drawing.Size(215, 20)
Me.ddlShiftCode.TabIndex = 0
'
'lblShiftCode
'
Me.lblShiftCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblShiftCode.Location = New System.Drawing.Point(12, 15)
Me.lblShiftCode.Name = "lblShiftCode"
Me.lblShiftCode.Size = New System.Drawing.Size(55, 18)
Me.lblShiftCode.TabIndex = 19
Me.lblShiftCode.Text = "ShiftCode"
'
'rdbtnsave
'
Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtnsave.Location = New System.Drawing.Point(12, 11)
Me.rdbtnsave.Name = "rdbtnsave"
Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
Me.rdbtnsave.TabIndex = 0
Me.rdbtnsave.Text = "Save"
'
'rdbtndelete
'
Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtndelete.Location = New System.Drawing.Point(85, 11)
Me.rdbtndelete.Name = "rdbtndelete"
Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
Me.rdbtndelete.TabIndex = 1
Me.rdbtndelete.Text = "Delete"
'
'rdbtnclose
'
Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.rdbtnclose.Location = New System.Drawing.Point(496, 11)
Me.rdbtnclose.Name = "rdbtnclose"
Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
Me.rdbtnclose.TabIndex = 2
Me.rdbtnclose.Text = "Close"
'
'ShiftMaster
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(568, 379)
Me.Controls.Add(Me.SplitContainer1)
Me.Controls.Add(Me.rdmenu1)
Me.Name = "ShiftMaster"
'
'
'
Me.RootElement.ApplyShapeToControl = true
Me.Text = "ShiftMaster"
Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).EndInit
Me.SplitContainer1.Panel1.ResumeLayout(false)
Me.SplitContainer1.Panel1.PerformLayout
Me.SplitContainer1.Panel2.ResumeLayout(false)
Me.SplitContainer1.ResumeLayout(false)
CType(Me.lblShortName,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.txtShortName,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblShiftName,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.txtShiftName,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.ddlShiftCode,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblShiftCode,System.ComponentModel.ISupportInitialize).EndInit
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
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddlShiftCode As common.Controls.MyComboBox
    Friend WithEvents lblShiftCode As common.Controls.MyLabel
    Friend WithEvents lblShortName As common.Controls.MyLabel
    Friend WithEvents txtShortName As common.Controls.MyTextBox
    Friend WithEvents lblShiftName As common.Controls.MyLabel
    Friend WithEvents txtShiftName As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
End Class

