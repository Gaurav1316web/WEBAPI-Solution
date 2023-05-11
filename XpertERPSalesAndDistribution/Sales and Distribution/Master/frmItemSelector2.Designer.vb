<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemSelector2
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
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton
        Me.btnCancel = New Telerik.WinControls.UI.RadButton
        Me.dgvItem = New common.UserControls.MyRadGridView
        Me.btnOk = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(175, 2)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(69, 21)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "Select All"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(331, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(69, 21)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        '
        'dgvItem
        '
        Me.dgvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvItem.ForeColor = System.Drawing.Color.Black
        Me.dgvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvItem.Location = New System.Drawing.Point(0, 0)
        '
        'dgvItem
        '
        Me.dgvItem.MasterTemplate.AllowAddNewRow = False
        Me.dgvItem.MasterTemplate.AllowDeleteRow = False
        Me.dgvItem.MasterTemplate.EnableFiltering = True
        Me.dgvItem.MasterTemplate.EnableGrouping = False
        Me.dgvItem.Name = "dgvItem"
        Me.dgvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.dgvItem.RootElement.ForeColor = System.Drawing.Color.Black
        Me.dgvItem.Size = New System.Drawing.Size(594, 304)
        Me.dgvItem.TabIndex = 0
        Me.dgvItem.Text = "Customer"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(252, 2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(69, 21)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Ok"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvItem)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOk)
        Me.SplitContainer1.Size = New System.Drawing.Size(594, 333)
        Me.SplitContainer1.SplitterDistance = 304
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmItemSelector2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 333)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmItemSelector2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Item Selector"
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvItem As common.UserControls.MyRadGridView
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

