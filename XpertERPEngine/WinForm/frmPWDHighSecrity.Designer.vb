<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPWDHighSecrity
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
        Me.btnOK = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.lblAnyText = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtPWd = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAnyText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPWd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(50, 66)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(87, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(142, 66)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 24)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "cancel"
        '
        'lblAnyText
        '
        Me.lblAnyText.CalculationExpression = Nothing
        Me.lblAnyText.DecimalPlaces = 0
        Me.lblAnyText.FieldCode = Nothing
        Me.lblAnyText.FieldDesc = Nothing
        Me.lblAnyText.FieldMaxLength = 0
        Me.lblAnyText.FieldName = Nothing
        Me.lblAnyText.isCalculatedField = False
        Me.lblAnyText.IsSourceFromTable = False
        Me.lblAnyText.IsSourceFromValueList = False
        Me.lblAnyText.IsUnique = False
        Me.lblAnyText.Location = New System.Drawing.Point(93, 8)
        Me.lblAnyText.MendatroryField = False
        Me.lblAnyText.MyLinkLable1 = Me.MyLabel2
        Me.lblAnyText.MyLinkLable2 = Nothing
        Me.lblAnyText.Name = "lblAnyText"
        Me.lblAnyText.ReadOnly = True
        Me.lblAnyText.ReferenceFieldDesc = Nothing
        Me.lblAnyText.ReferenceFieldName = Nothing
        Me.lblAnyText.ReferenceTableName = Nothing
        Me.lblAnyText.Size = New System.Drawing.Size(157, 20)
        Me.lblAnyText.TabIndex = 12
        Me.lblAnyText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.lblAnyText.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(21, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel2.TabIndex = 5
        Me.MyLabel2.Text = "Secuity Code"
        '
        'txtPWd
        '
        Me.txtPWd.CalculationExpression = Nothing
        Me.txtPWd.DecimalPlaces = 0
        Me.txtPWd.FieldCode = Nothing
        Me.txtPWd.FieldDesc = Nothing
        Me.txtPWd.FieldMaxLength = 0
        Me.txtPWd.FieldName = Nothing
        Me.txtPWd.isCalculatedField = False
        Me.txtPWd.IsSourceFromTable = False
        Me.txtPWd.IsSourceFromValueList = False
        Me.txtPWd.IsUnique = False
        Me.txtPWd.Location = New System.Drawing.Point(93, 36)
        Me.txtPWd.MendatroryField = False
        Me.txtPWd.MyLinkLable1 = Me.MyLabel1
        Me.txtPWd.MyLinkLable2 = Nothing
        Me.txtPWd.Name = "txtPWd"
        Me.txtPWd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPWd.ReferenceFieldDesc = Nothing
        Me.txtPWd.ReferenceFieldName = Nothing
        Me.txtPWd.ReferenceTableName = Nothing
        Me.txtPWd.Size = New System.Drawing.Size(157, 20)
        Me.txtPWd.TabIndex = 11
        Me.txtPWd.Text = "0"
        Me.txtPWd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPWd.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(21, 37)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "Password"
        '
        'frmPWDHighSecrity
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(278, 97)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblAnyText)
        Me.Controls.Add(Me.txtPWd)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.MyLabel1)
        Me.Name = "frmPWDHighSecrity"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enter Password"
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAnyText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPWd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As Controls.MyLabel
    Friend WithEvents txtPWd As MyNumBox
    Friend WithEvents lblAnyText As MyNumBox
End Class

