<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPaymentProcessNew
    Inherits System.Windows.Forms.Form

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
        Me.UcPaymentProcess1 = New XpertERPEngine.ucPaymentProcess()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UcPaymentProcess1
        '
        Me.UcPaymentProcess1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcPaymentProcess1.Location = New System.Drawing.Point(0, 0)
        Me.UcPaymentProcess1.Name = "UcPaymentProcess1"
        Me.UcPaymentProcess1.Size = New System.Drawing.Size(800, 450)
        Me.UcPaymentProcess1.TabIndex = 0
        '
        'frmPaymentProcessNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.UcPaymentProcess1)
        Me.Name = "frmPaymentProcessNew"
        '
        '
        '
        'Me.RootElement.ApplyShapeToControl = False
        Me.Text = "frmPaymentProcessNew"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UcPaymentProcess1 As ucPaymentProcess
End Class
