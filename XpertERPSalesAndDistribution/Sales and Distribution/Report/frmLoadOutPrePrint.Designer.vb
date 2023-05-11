<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLoadOutPrePrint
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkPrePrint = New Telerik.WinControls.UI.RadCheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.fndLoadOutNo = New finder.finder
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkPrePrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.Controls.Add(Me.btnPrint)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.FooterImageIndex = -1
        Me.RadGroupBox2.FooterImageKey = ""
        Me.RadGroupBox2.HeaderImageIndex = -1
        Me.RadGroupBox2.HeaderImageKey = ""
        Me.RadGroupBox2.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(389, 122)
        Me.RadGroupBox2.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(301, 95)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(13, 95)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Print"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkPrePrint)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.fndLoadOutNo)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 23)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(356, 66)
        Me.RadGroupBox1.TabIndex = 1
        '
        'chkPrePrint
        '
        Me.chkPrePrint.Location = New System.Drawing.Point(206, 20)
        Me.chkPrePrint.Name = "chkPrePrint"
        Me.chkPrePrint.Size = New System.Drawing.Size(126, 18)
        Me.chkPrePrint.TabIndex = 4
        Me.chkPrePrint.Text = "PrePrinted Stationary"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "LoadOut No"
        '
        'fndLoadOutNo
        '
        Me.fndLoadOutNo.BackColor = System.Drawing.Color.Transparent
        Me.fndLoadOutNo.Caption = Nothing
        Me.fndLoadOutNo.ConnectionString = Nothing
        Me.fndLoadOutNo.Icon = Nothing
        Me.fndLoadOutNo.Location = New System.Drawing.Point(86, 20)
        Me.fndLoadOutNo.Margin = New System.Windows.Forms.Padding(0)
        Me.fndLoadOutNo.MinimumSize = New System.Drawing.Size(117, 20)
        Me.fndLoadOutNo.Name = "fndLoadOutNo"
        Me.fndLoadOutNo.NewTimer = Nothing
        Me.fndLoadOutNo.Query = Nothing
        Me.fndLoadOutNo.ResultDT = Nothing
        Me.fndLoadOutNo.SelectedRowDR = Nothing
        Me.fndLoadOutNo.SelectedValue = Nothing
        Me.fndLoadOutNo.SelectedValue1 = Nothing
        Me.fndLoadOutNo.Size = New System.Drawing.Size(117, 20)
        Me.fndLoadOutNo.TabIndex = 2
        Me.fndLoadOutNo.ValueToSelect = Nothing
        Me.fndLoadOutNo.ValueToSelect1 = Nothing
        '
        'FrmLoadOutPrePrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 151)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.KeyPreview = True
        Me.Name = "FrmLoadOutPrePrint"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "LoadOut Report"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkPrePrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkPrePrint As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents fndLoadOutNo As finder.finder
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

