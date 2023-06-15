<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCreateBMCDCSbyMobile
    Inherits FrmMainTranScreen
    'Inherits System.Windows.Forms.Form

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
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.btnGo = New System.Windows.Forms.Button()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(34, 32)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 12
        Me.lblDate.Text = "Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(70, 31)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.lblDate
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(102, 18)
        Me.txtdate.TabIndex = 11
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(213, 31)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(75, 23)
        Me.btnGo.TabIndex = 13
        Me.btnGo.Text = ">>>"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'FrmCreateBMCDCSbyMobile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.txtdate)
        Me.Name = "FrmCreateBMCDCSbyMobile"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCreateBMCDCSbyMobile"
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnGo As Button
End Class
