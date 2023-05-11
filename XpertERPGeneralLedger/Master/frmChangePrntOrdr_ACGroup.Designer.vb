<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangePrntOrdr_ACGroup
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
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.dgvAccountGroup = New common.UserControls.MyRadGridView
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnsave)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 385)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(696, 25)
        Me.RadPanel1.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnsave.Location = New System.Drawing.Point(263, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.Location = New System.Drawing.Point(334, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'dgvAccountGroup
        '
        Me.dgvAccountGroup.BackColor = System.Drawing.Color.White
        Me.dgvAccountGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvAccountGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccountGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvAccountGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvAccountGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvAccountGroup.Location = New System.Drawing.Point(0, 0)
        '
        'dgvAccountGroup
        '
        Me.dgvAccountGroup.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvAccountGroup.MasterTemplate.AllowAddNewRow = False
        Me.dgvAccountGroup.MasterTemplate.EnableGrouping = False
        Me.dgvAccountGroup.Name = "dgvAccountGroup"
        Me.dgvAccountGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvAccountGroup.Size = New System.Drawing.Size(696, 385)
        Me.dgvAccountGroup.TabIndex = 0
        '
        'FrmChangePrntOrdr_ACGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 410)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgvAccountGroup)
        Me.Controls.Add(Me.RadPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmChangePrntOrdr_ACGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Print Order - Account Group"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvAccountGroup As common.UserControls.MyRadGridView
End Class

