<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucRequisitionDetail
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblAppCode = New System.Windows.Forms.Label
        Me.LblEmail = New System.Windows.Forms.Label
        Me.LblAppName = New System.Windows.Forms.Label
        Me.LblAppDate = New System.Windows.Forms.Label
        Me.LblTelephoneNo = New System.Windows.Forms.Label
        Me.LblReqNo = New System.Windows.Forms.Label
        Me.LblDOB = New System.Windows.Forms.Label
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.Label4)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.LblAppCode)
        Me.RadGroupBox1.Controls.Add(Me.LblEmail)
        Me.RadGroupBox1.Controls.Add(Me.LblAppName)
        Me.RadGroupBox1.Controls.Add(Me.LblAppDate)
        Me.RadGroupBox1.Controls.Add(Me.LblTelephoneNo)
        Me.RadGroupBox1.Controls.Add(Me.LblReqNo)
        Me.RadGroupBox1.Controls.Add(Me.LblDOB)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(741, 93)
        Me.RadGroupBox1.TabIndex = 20
        Me.RadGroupBox1.Text = " "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(526, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 14)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Telephone No. :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(537, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 14)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Date of Birth :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(315, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Email :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(259, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 14)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Applicant Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 14)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Applicant Date :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Requisition No. :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Applicant Code :"
        '
        'LblAppCode
        '
        Me.LblAppCode.AutoSize = True
        Me.LblAppCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAppCode.Location = New System.Drawing.Point(106, 17)
        Me.LblAppCode.Name = "LblAppCode"
        Me.LblAppCode.Size = New System.Drawing.Size(80, 14)
        Me.LblAppCode.TabIndex = 26
        Me.LblAppCode.Text = "Applicant Code"
        '
        'LblEmail
        '
        Me.LblEmail.AutoSize = True
        Me.LblEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmail.Location = New System.Drawing.Point(359, 46)
        Me.LblEmail.Name = "LblEmail"
        Me.LblEmail.Size = New System.Drawing.Size(31, 14)
        Me.LblEmail.TabIndex = 24
        Me.LblEmail.Text = "Email"
        '
        'LblAppName
        '
        Me.LblAppName.AutoSize = True
        Me.LblAppName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAppName.Location = New System.Drawing.Point(359, 18)
        Me.LblAppName.Name = "LblAppName"
        Me.LblAppName.Size = New System.Drawing.Size(82, 14)
        Me.LblAppName.TabIndex = 21
        Me.LblAppName.Text = "Applicant Name"
        '
        'LblAppDate
        '
        Me.LblAppDate.AutoSize = True
        Me.LblAppDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAppDate.Location = New System.Drawing.Point(106, 71)
        Me.LblAppDate.Name = "LblAppDate"
        Me.LblAppDate.Size = New System.Drawing.Size(77, 14)
        Me.LblAppDate.TabIndex = 25
        Me.LblAppDate.Text = "Applicant Date"
        '
        'LblTelephoneNo
        '
        Me.LblTelephoneNo.AutoSize = True
        Me.LblTelephoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTelephoneNo.Location = New System.Drawing.Point(621, 46)
        Me.LblTelephoneNo.Name = "LblTelephoneNo"
        Me.LblTelephoneNo.Size = New System.Drawing.Size(75, 14)
        Me.LblTelephoneNo.TabIndex = 23
        Me.LblTelephoneNo.Text = "Telephone No."
        '
        'LblReqNo
        '
        Me.LblReqNo.AutoSize = True
        Me.LblReqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReqNo.Location = New System.Drawing.Point(106, 43)
        Me.LblReqNo.Name = "LblReqNo"
        Me.LblReqNo.Size = New System.Drawing.Size(78, 14)
        Me.LblReqNo.TabIndex = 22
        Me.LblReqNo.Text = "Requisition No."
        '
        'LblDOB
        '
        Me.LblDOB.AutoSize = True
        Me.LblDOB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDOB.Location = New System.Drawing.Point(621, 18)
        Me.LblDOB.Name = "LblDOB"
        Me.LblDOB.Size = New System.Drawing.Size(67, 14)
        Me.LblDOB.TabIndex = 20
        Me.LblDOB.Text = "Date of Birth"
        '
        'ucRequisitionDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "ucRequisitionDetail"
        Me.Size = New System.Drawing.Size(744, 96)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Private WithEvents LblAppCode As System.Windows.Forms.Label
    Private WithEvents LblEmail As System.Windows.Forms.Label
    Private WithEvents LblAppName As System.Windows.Forms.Label
    Private WithEvents LblAppDate As System.Windows.Forms.Label
    Private WithEvents LblTelephoneNo As System.Windows.Forms.Label
    Private WithEvents LblReqNo As System.Windows.Forms.Label
    Private WithEvents LblDOB As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label

End Class
