<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChildBOScreen
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
        Me.btn_Child_Close = New Telerik.WinControls.UI.RadButton
        Me.gv_Child_BO = New common.UserControls.MyRadGridView
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.btn_Child_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Child_BO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Child_BO.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Child_Close
        '
        Me.btn_Child_Close.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btn_Child_Close.Location = New System.Drawing.Point(361, 2)
        Me.btn_Child_Close.MaximumSize = New System.Drawing.Size(77, 21)
        Me.btn_Child_Close.MinimumSize = New System.Drawing.Size(77, 21)
        Me.btn_Child_Close.Name = "btn_Child_Close"
        '
        '
        '
        Me.btn_Child_Close.RootElement.MaxSize = New System.Drawing.Size(77, 21)
        Me.btn_Child_Close.RootElement.MinSize = New System.Drawing.Size(77, 21)
        Me.btn_Child_Close.Size = New System.Drawing.Size(77, 21)
        Me.btn_Child_Close.TabIndex = 6
        Me.btn_Child_Close.Text = "Save"
        '
        'gv_Child_BO
        '
        Me.gv_Child_BO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Child_BO.Location = New System.Drawing.Point(0, 20)
        '
        'gv_Child_BO
        '
        Me.gv_Child_BO.MasterTemplate.AllowDragToGroup = False
        Me.gv_Child_BO.MasterTemplate.EnableFiltering = True
        Me.gv_Child_BO.MasterTemplate.EnableGrouping = False
        Me.gv_Child_BO.Name = "gv_Child_BO"
        Me.gv_Child_BO.ShowGroupPanel = False
        Me.gv_Child_BO.Size = New System.Drawing.Size(933, 411)
        Me.gv_Child_BO.TabIndex = 5
        Me.gv_Child_BO.Text = "RadGridView1"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnclose)
        Me.RadPanel1.Controls.Add(Me.btn_Child_Close)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 431)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(933, 26)
        Me.RadPanel1.TabIndex = 7
        '
        'btnclose
        '
        Me.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnclose.Location = New System.Drawing.Point(444, 2)
        Me.btnclose.MaximumSize = New System.Drawing.Size(77, 21)
        Me.btnclose.MinimumSize = New System.Drawing.Size(77, 21)
        Me.btnclose.Name = "btnclose"
        '
        '
        '
        Me.btnclose.RootElement.MaxSize = New System.Drawing.Size(77, 21)
        Me.btnclose.RootElement.MinSize = New System.Drawing.Size(77, 21)
        Me.btnclose.Size = New System.Drawing.Size(77, 21)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(933, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Settings"
        Me.RadMenuItem1.AccessibleName = "Settings"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmChildBOScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 457)
        Me.Controls.Add(Me.gv_Child_BO)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmChildBOScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Child BO"
        CType(Me.btn_Child_Close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Child_BO.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Child_BO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Child_Close As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv_Child_BO As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
End Class

