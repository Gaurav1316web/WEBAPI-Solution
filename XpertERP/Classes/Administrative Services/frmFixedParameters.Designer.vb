<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedParameters
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
        Me.gvInvoice = New common.UserControls.MyRadGridView
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvInvoice)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(773, 328)
        Me.SplitContainer1.SplitterDistance = 293
        Me.SplitContainer1.TabIndex = 0
        '
        'gvInvoice
        '
        Me.gvInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvInvoice.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvInvoice.ForeColor = System.Drawing.Color.Black
        Me.gvInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvInvoice.Location = New System.Drawing.Point(0, 0)
        '
        'gvInvoice
        '
        Me.gvInvoice.MasterTemplate.EnableFiltering = True
        Me.gvInvoice.MasterTemplate.EnableGrouping = False
        Me.gvInvoice.MasterTemplate.EnableSorting = False
        Me.gvInvoice.Name = "gvInvoice"
        Me.gvInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.gvInvoice.RootElement.ForeColor = System.Drawing.Color.Black
        Me.gvInvoice.ShowGroupPanel = False
        Me.gvInvoice.Size = New System.Drawing.Size(773, 293)
        Me.gvInvoice.TabIndex = 1
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(12, 7)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(68, 18)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "Update"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(693, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'FrmFixedParameters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 328)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFixedParameters"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Fixed Parameters"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvInvoice As common.UserControls.MyRadGridView
    Friend WithEvents btnUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

