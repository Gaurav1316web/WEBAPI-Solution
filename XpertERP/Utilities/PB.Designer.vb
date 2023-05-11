<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PB
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
        Me.Pbar = New Telerik.WinControls.UI.RadProgressBar
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        CType(Me.Pbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pbar
        '
        Me.Pbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pbar.Location = New System.Drawing.Point(0, 0)
        Me.Pbar.Name = "Pbar"
        Me.Pbar.SeparatorColor1 = System.Drawing.Color.Maroon
        Me.Pbar.SeparatorColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Pbar.SeparatorColor3 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Pbar.ShowProgressIndicators = True
        Me.Pbar.Size = New System.Drawing.Size(502, 19)
        Me.Pbar.TabIndex = 0
        Me.Pbar.Text = "0 %"
        '
        'PB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 19)
        Me.ControlBox = False
        Me.Controls.Add(Me.Pbar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PB"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wait !!! Processing..."
        Me.TopMost = True
        CType(Me.Pbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Pbar As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class

