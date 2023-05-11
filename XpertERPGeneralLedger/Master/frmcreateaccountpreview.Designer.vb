<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmcreateaccountpreview
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
        Me.rdaccountpreview = New common.UserControls.MyRadGridView()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdaccountpreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdaccountpreview.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdaccountpreview
        '
        Me.rdaccountpreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdaccountpreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdaccountpreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdaccountpreview.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rdaccountpreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdaccountpreview.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdaccountpreview.Location = New System.Drawing.Point(12, 12)
        '
        'rdaccountpreview
        '
        Me.rdaccountpreview.MasterTemplate.AllowAddNewRow = False
        Me.rdaccountpreview.MasterTemplate.ShowHeaderCellButtons = True
        Me.rdaccountpreview.Name = "rdaccountpreview"
        Me.rdaccountpreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdaccountpreview.ShowHeaderCellButtons = True
        Me.rdaccountpreview.Size = New System.Drawing.Size(907, 397)
        Me.rdaccountpreview.TabIndex = 0
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(12, 418)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(84, 24)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'Frmcreateaccountpreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 454)
        Me.Controls.Add(Me.rdbtnclose)
        Me.Controls.Add(Me.rdaccountpreview)
        Me.KeyPreview = True
        Me.Name = "Frmcreateaccountpreview"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Account Preview"
        CType(Me.rdaccountpreview.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdaccountpreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdaccountpreview As common.UserControls.MyRadGridView
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
End Class

