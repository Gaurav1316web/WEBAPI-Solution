<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucDCSBalance
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblUnbilledAmt = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTotalOS = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblTotalCredit = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 14)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Unbilled Amt"
        '
        'lblUnbilledAmt
        '
        Me.lblUnbilledAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnbilledAmt.Location = New System.Drawing.Point(103, 19)
        Me.lblUnbilledAmt.Name = "lblUnbilledAmt"
        Me.lblUnbilledAmt.Size = New System.Drawing.Size(128, 14)
        Me.lblUnbilledAmt.TabIndex = 7
        Me.lblUnbilledAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnbilledAmt.UseCompatibleTextRendering = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 14)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Total O/S"
        '
        'lblTotalOS
        '
        Me.lblTotalOS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOS.Location = New System.Drawing.Point(103, 36)
        Me.lblTotalOS.Name = "lblTotalOS"
        Me.lblTotalOS.Size = New System.Drawing.Size(128, 14)
        Me.lblTotalOS.TabIndex = 5
        Me.lblTotalOS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalOS.UseCompatibleTextRendering = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 14)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Total Credit"
        '
        'lblTotalCredit
        '
        Me.lblTotalCredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCredit.Location = New System.Drawing.Point(103, 53)
        Me.lblTotalCredit.Name = "lblTotalCredit"
        Me.lblTotalCredit.Size = New System.Drawing.Size(128, 14)
        Me.lblTotalCredit.TabIndex = 14
        Me.lblTotalCredit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblTotalCredit)
        Me.RadGroupBox1.Controls.Add(Me.Label11)
        Me.RadGroupBox1.Controls.Add(Me.lblTotalOS)
        Me.RadGroupBox1.Controls.Add(Me.Label9)
        Me.RadGroupBox1.Controls.Add(Me.lblUnbilledAmt)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(273, 74)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'ucDCSBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadGroupBox1)
        Me.MaximumSize = New System.Drawing.Size(273, 74)
        Me.MinimumSize = New System.Drawing.Size(273, 74)
        Me.Name = "ucDCSBalance"
        Me.Size = New System.Drawing.Size(273, 74)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Label8 As Label
    Private WithEvents lblUnbilledAmt As Label
    Private WithEvents Label9 As Label
    Private WithEvents lblTotalOS As Label
    Private WithEvents Label11 As Label
    Private WithEvents lblTotalCredit As Label
    Friend WithEvents RadGroupBox1 As RadGroupBox
End Class
