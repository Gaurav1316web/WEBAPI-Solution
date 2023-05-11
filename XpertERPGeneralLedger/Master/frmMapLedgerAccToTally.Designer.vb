<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapLedgerAccToTally
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dgvAccountMap = New common.UserControls.MyRadGridView
        Me.Btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvAccountMap)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(494, 527)
        Me.SplitContainer1.SplitterDistance = 480
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.TabStop = False
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAccountMap.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAccountMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvAccountMap.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvAccountMap.ForeColor = System.Drawing.Color.Black
        Me.dgvAccountMap.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvAccountMap.Location = New System.Drawing.Point(0, 25)
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.MasterTemplate.AllowAddNewRow = False
        Me.dgvAccountMap.MasterTemplate.EnableFiltering = True
        Me.dgvAccountMap.MasterTemplate.EnableGrouping = False
        Me.dgvAccountMap.Name = "dgvAccountMap"
        Me.dgvAccountMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvAccountMap.Size = New System.Drawing.Size(494, 452)
        Me.dgvAccountMap.TabIndex = 1
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(3, 7)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(76, 24)
        Me.Btnsave.TabIndex = 0
        Me.Btnsave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(406, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.SplitContainer1)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(494, 527)
        Me.RadPanel2.TabIndex = 1
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(494, 20)
        Me.RadMenu2.TabIndex = 11
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuImport, Me.RadMenuExport, Me.RadMenuClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuImport
        '
        Me.RadMenuImport.AccessibleDescription = "Import"
        Me.RadMenuImport.AccessibleName = "Import"
        Me.RadMenuImport.Name = "RadMenuImport"
        Me.RadMenuImport.Text = "Import"
        Me.RadMenuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuExport
        '
        Me.RadMenuExport.AccessibleDescription = "Export"
        Me.RadMenuExport.AccessibleName = "Export"
        Me.RadMenuExport.Name = "RadMenuExport"
        Me.RadMenuExport.Text = "Export"
        Me.RadMenuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuClose
        '
        Me.RadMenuClose.AccessibleDescription = "Close"
        Me.RadMenuClose.AccessibleName = "Close"
        Me.RadMenuClose.Name = "RadMenuClose"
        Me.RadMenuClose.Text = "Close"
        Me.RadMenuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmMapLedgerAccToTally
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 527)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.RadPanel2)
        Me.Name = "frmMapLedgerAccToTally"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Map Ledger Account To Tally"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvAccountMap As common.UserControls.MyRadGridView
    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuClose As Telerik.WinControls.UI.RadMenuItem
End Class

