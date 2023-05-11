<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFreeCombo1
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
        Me.cboFiscalYear = New common.Controls.MyComboBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        CType(Me.cboFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboFiscalYear
        '
        Me.cboFiscalYear.AllowShowFocusCues = False
        Me.cboFiscalYear.AutoCompleteDisplayMember = Nothing
        Me.cboFiscalYear.AutoCompleteValueMember = Nothing
        Me.cboFiscalYear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFiscalYear.Location = New System.Drawing.Point(144, 11)
        Me.cboFiscalYear.MendatroryField = False
        Me.cboFiscalYear.MyLinkLable1 = Me.RadLabel2
        Me.cboFiscalYear.MyLinkLable2 = Nothing
        Me.cboFiscalYear.Name = "cboFiscalYear"
        Me.cboFiscalYear.Size = New System.Drawing.Size(106, 20)
        Me.cboFiscalYear.TabIndex = 0
        Me.cboFiscalYear.Text = "MyComboBox2"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(62, 12)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(75, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "Financial Year"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(72, 47)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 24)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "OK"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(159, 47)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(81, 24)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Cancel"
        '
        'FrmFreeCombo1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 71)
        Me.ControlBox = False
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cboFiscalYear)
        Me.Controls.Add(Me.RadLabel2)
        Me.MaximumSize = New System.Drawing.Size(321, 101)
        Me.MinimumSize = New System.Drawing.Size(321, 101)
        Me.Name = "FrmFreeCombo1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Please select Fiscal Year"
        CType(Me.cboFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboFiscalYear As common.Controls.MyComboBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class

