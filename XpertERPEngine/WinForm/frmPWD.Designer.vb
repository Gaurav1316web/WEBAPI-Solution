<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPWD
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
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnOK = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.txtPWd = New common.Controls.MyTextBox()
        Me.lblAnyText = New common.Controls.MyLabel()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPWd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAnyText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "Password"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(50, 60)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(87, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(142, 60)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 24)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "cancel"
        '
        'txtPWd
        '
        Me.txtPWd.CalculationExpression = Nothing
        Me.txtPWd.FieldCode = Nothing
        Me.txtPWd.FieldDesc = Nothing
        Me.txtPWd.FieldMaxLength = 0
        Me.txtPWd.FieldName = Nothing
        Me.txtPWd.isCalculatedField = False
        Me.txtPWd.IsSourceFromTable = False
        Me.txtPWd.IsSourceFromValueList = False
        Me.txtPWd.IsUnique = False
        Me.txtPWd.Location = New System.Drawing.Point(72, 22)
        Me.txtPWd.MendatroryField = True
        Me.txtPWd.MyLinkLable1 = Nothing
        Me.txtPWd.MyLinkLable2 = Nothing
        Me.txtPWd.Name = "txtPWd"
        Me.txtPWd.NullText = "Password"
        Me.txtPWd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPWd.ReferenceFieldDesc = Nothing
        Me.txtPWd.ReferenceFieldName = Nothing
        Me.txtPWd.ReferenceTableName = Nothing
        Me.txtPWd.Size = New System.Drawing.Size(194, 20)
        Me.txtPWd.TabIndex = 0
        '
        'lblAnyText
        '
        Me.lblAnyText.AutoSize = False
        Me.lblAnyText.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAnyText.FieldName = Nothing
        Me.lblAnyText.Location = New System.Drawing.Point(0, 0)
        Me.lblAnyText.Name = "lblAnyText"
        Me.lblAnyText.Size = New System.Drawing.Size(278, 18)
        Me.lblAnyText.TabIndex = 4
        Me.lblAnyText.Text = "Any Text"
        Me.lblAnyText.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'FrmPWD
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(278, 97)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblAnyText)
        Me.Controls.Add(Me.txtPWd)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.MyLabel1)
        Me.Name = "FrmPWD"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enter Password"
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPWd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAnyText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPWd As common.Controls.MyTextBox
    Friend WithEvents lblAnyText As common.Controls.MyLabel
End Class

