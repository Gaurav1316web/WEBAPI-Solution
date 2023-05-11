<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTemplateExport3
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
        Me.btnCancel = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dgvTemplate = New common.UserControls.MyRadGridView
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(353, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(69, 21)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvTemplate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Size = New System.Drawing.Size(650, 446)
        Me.SplitContainer1.SplitterDistance = 417
        Me.SplitContainer1.TabIndex = 1
        '
        'dgvTemplate
        '
        Me.dgvTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvTemplate.ForeColor = System.Drawing.Color.Black
        Me.dgvTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvTemplate.Location = New System.Drawing.Point(0, 0)
        '
        'dgvTemplate
        '
        Me.dgvTemplate.MasterTemplate.AllowAddNewRow = False
        Me.dgvTemplate.MasterTemplate.AllowDeleteRow = False
        Me.dgvTemplate.MasterTemplate.EnableFiltering = True
        Me.dgvTemplate.MasterTemplate.EnableGrouping = False
        Me.dgvTemplate.Name = "dgvTemplate"
        Me.dgvTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.dgvTemplate.RootElement.ForeColor = System.Drawing.Color.Black
        Me.dgvTemplate.Size = New System.Drawing.Size(650, 417)
        Me.dgvTemplate.TabIndex = 0
        Me.dgvTemplate.Text = "Customer"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(197, 2)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(69, 21)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "Select All"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(274, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(69, 21)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "Export"
        '
        'FrmTemplateExport3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 446)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmTemplateExport3"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export Template"
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
End Class

