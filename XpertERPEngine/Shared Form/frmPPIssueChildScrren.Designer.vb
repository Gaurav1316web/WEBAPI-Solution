<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPPIssueChildScrren
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
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btndeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.gv = New common.UserControls.MyRadGridView()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1120, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Settings"
        Me.RadMenuItem1.AccessibleName = "Settings"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.btndeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.AccessibleDescription = "Save Layout"
        Me.btnSaveLayout.AccessibleName = "Save Layout"
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btndeleteLayout
        '
        Me.btndeleteLayout.AccessibleDescription = "Delete Layout"
        Me.btndeleteLayout.AccessibleName = "Delete Layout"
        Me.btndeleteLayout.Name = "btndeleteLayout"
        Me.btndeleteLayout.Text = "Delete Layout"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnclose)
        Me.RadPanel1.Controls.Add(Me.btnsave)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 437)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1120, 26)
        Me.RadPanel1.TabIndex = 8
        '
        'btnclose
        '
        Me.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnclose.Location = New System.Drawing.Point(549, 3)
        Me.btnclose.MaximumSize = New System.Drawing.Size(77, 21)
        Me.btnclose.MinimumSize = New System.Drawing.Size(77, 21)
        Me.btnclose.Name = "btnclose"
        '
        '
        '
        Me.btnclose.RootElement.MaxSize = New System.Drawing.Size(77, 21)
        Me.btnclose.RootElement.MinSize = New System.Drawing.Size(77, 21)
        Me.btnclose.Size = New System.Drawing.Size(77, 21)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Esc : Cancel"
        '
        'btnsave
        '
        Me.btnsave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnsave.Location = New System.Drawing.Point(466, 3)
        Me.btnsave.MaximumSize = New System.Drawing.Size(77, 21)
        Me.btnsave.MinimumSize = New System.Drawing.Size(77, 21)
        Me.btnsave.Name = "btnsave"
        '
        '
        '
        Me.btnsave.RootElement.MaxSize = New System.Drawing.Size(77, 21)
        Me.btnsave.RootElement.MinSize = New System.Drawing.Size(77, 21)
        Me.btnsave.Size = New System.Drawing.Size(77, 21)
        Me.btnsave.TabIndex = 8
        Me.btnsave.Text = "F5 : Ok"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 20)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1120, 417)
        Me.gv.TabIndex = 9
        Me.gv.Text = "RadGridView1"
        '
        'FrmPPIssueChildScrren
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1120, 463)
        Me.Controls.Add(Me.gv)
        Me.Controls.Add(Me.RadPanel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmPPIssueChildScrren"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmPPIssueChildScrren"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

