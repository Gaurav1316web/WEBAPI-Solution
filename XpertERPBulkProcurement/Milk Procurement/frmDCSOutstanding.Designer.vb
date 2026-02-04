<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDCSOutstanding
    Inherits FrmMainTranScreen


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.UcDCSBalance1 = New XpertERPEngine.ucDCSBalance()
        Me.FlowLayoutPanel2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.UcDCSBalance1)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(282, 86)
        Me.FlowLayoutPanel2.TabIndex = 2
        '
        'UcDCSBalance1
        '
        Me.UcDCSBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcDCSBalance1.DCSCode = ""
        Me.UcDCSBalance1.DCSName = ""
        Me.UcDCSBalance1.DCSUploaderCode = ""
        Me.UcDCSBalance1.Location = New System.Drawing.Point(3, 3)
        Me.UcDCSBalance1.MaximumSize = New System.Drawing.Size(273, 74)
        Me.UcDCSBalance1.MinimumSize = New System.Drawing.Size(273, 74)
        Me.UcDCSBalance1.Name = "UcDCSBalance1"
        Me.UcDCSBalance1.Size = New System.Drawing.Size(273, 74)
        Me.UcDCSBalance1.TabIndex = 320
        Me.UcDCSBalance1.TotalCredit = 0R
        Me.UcDCSBalance1.TotalOS = 0R
        Me.UcDCSBalance1.TransDate = New Date(CType(0, Long))
        Me.UcDCSBalance1.UnBilledAmt = 0R
        Me.UcDCSBalance1.VendorCode = ""
        '
        'frmDCSOutstanding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 86)
        Me.Controls.Add(Me.FlowLayoutPanel2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(290, 116)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(290, 116)
        Me.Name = "frmDCSOutstanding"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDCSOutstanding"
        Me.FlowLayoutPanel2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents UcDCSBalance1 As ucDCSBalance
End Class
