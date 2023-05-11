<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetCriteriaLoc
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton
        Me.btnSe = New Telerik.WinControls.UI.RadButton
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.RadDropDownList1 = New Telerik.WinControls.UI.RadDropDownList
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadGridView1 = New common.UserControls.MyRadGridView
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadButton5)
        Me.RadGroupBox1.Controls.Add(Me.RadButton4)
        Me.RadGroupBox1.Controls.Add(Me.btnSe)
        Me.RadGroupBox1.Controls.Add(Me.RadButton2)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.RadDropDownList1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadGridView1)
        Me.RadGroupBox1.HeaderText = "RadGroupBox1"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(503, 357)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "RadGroupBox1"
        '
        'RadButton5
        '
        Me.RadButton5.Location = New System.Drawing.Point(431, 331)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(59, 20)
        Me.RadButton5.TabIndex = 5
        Me.RadButton5.Text = "Close"
        '
        'RadButton4
        '
        Me.RadButton4.Location = New System.Drawing.Point(93, 330)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(59, 20)
        Me.RadButton4.TabIndex = 4
        Me.RadButton4.Text = "Ok"
        '
        'btnSe
        '
        Me.btnSe.Location = New System.Drawing.Point(13, 331)
        Me.btnSe.Name = "btnSe"
        Me.btnSe.Size = New System.Drawing.Size(74, 20)
        Me.btnSe.TabIndex = 3
        Me.btnSe.Text = "Set Criteria"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(289, 41)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(59, 20)
        Me.RadButton2.TabIndex = 2
        Me.RadButton2.Text = "Delete"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(224, 39)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(59, 20)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Add"
        '
        'RadDropDownList1
        '
        Me.RadDropDownList1.Location = New System.Drawing.Point(77, 39)
        Me.RadDropDownList1.Name = "RadDropDownList1"
        Me.RadDropDownList1.Size = New System.Drawing.Size(132, 20)
        Me.RadDropDownList1.TabIndex = 0
        Me.RadDropDownList1.Text = "RadDropDownList1"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 41)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 6
        Me.RadLabel1.Text = "Column"
        '
        'RadGridView1
        '
        Me.RadGridView1.Location = New System.Drawing.Point(13, 88)
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.Size = New System.Drawing.Size(477, 237)
        Me.RadGridView1.TabIndex = 0
        Me.RadGridView1.TabStop = False
        Me.RadGridView1.Text = "RadGridView1"
        '
        'SetCriteriaLoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 374)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "SetCriteriaLoc"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "SetCriteriaLoc"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadDropDownList1 As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSe As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

