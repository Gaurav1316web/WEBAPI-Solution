<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Enter_password
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtsample = New common.Controls.MyTextBox()
        Me.txtpan = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtsample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtsample)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtpan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Size = New System.Drawing.Size(319, 119)
        Me.SplitContainer1.SplitterDistance = 86
        Me.SplitContainer1.TabIndex = 0
        '
        'txtsample
        '
        Me.txtsample.CalculationExpression = Nothing
        Me.txtsample.FieldCode = Nothing
        Me.txtsample.FieldDesc = Nothing
        Me.txtsample.FieldMaxLength = 0
        Me.txtsample.FieldName = Nothing
        Me.txtsample.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsample.isCalculatedField = False
        Me.txtsample.IsSourceFromTable = False
        Me.txtsample.IsSourceFromValueList = False
        Me.txtsample.IsUnique = False
        Me.txtsample.Location = New System.Drawing.Point(166, 58)
        Me.txtsample.MaxLength = 15
        Me.txtsample.MendatroryField = True
        Me.txtsample.MyLinkLable1 = Nothing
        Me.txtsample.MyLinkLable2 = Nothing
        Me.txtsample.Name = "txtsample"
        Me.txtsample.NullText = "Password"
        Me.txtsample.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtsample.ReferenceFieldDesc = Nothing
        Me.txtsample.ReferenceFieldName = Nothing
        Me.txtsample.ReferenceTableName = Nothing
        Me.txtsample.Size = New System.Drawing.Size(148, 18)
        Me.txtsample.TabIndex = 112
        '
        'txtpan
        '
        Me.txtpan.CalculationExpression = Nothing
        Me.txtpan.FieldCode = Nothing
        Me.txtpan.FieldDesc = Nothing
        Me.txtpan.FieldMaxLength = 0
        Me.txtpan.FieldName = Nothing
        Me.txtpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpan.isCalculatedField = False
        Me.txtpan.IsSourceFromTable = False
        Me.txtpan.IsSourceFromValueList = False
        Me.txtpan.IsUnique = False
        Me.txtpan.Location = New System.Drawing.Point(166, 25)
        Me.txtpan.MaxLength = 15
        Me.txtpan.MendatroryField = True
        Me.txtpan.MyLinkLable1 = Nothing
        Me.txtpan.MyLinkLable2 = Nothing
        Me.txtpan.Name = "txtpan"
        Me.txtpan.NullText = "Password"
        Me.txtpan.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpan.ReferenceFieldDesc = Nothing
        Me.txtpan.ReferenceFieldName = Nothing
        Me.txtpan.ReferenceTableName = Nothing
        Me.txtpan.Size = New System.Drawing.Size(148, 18)
        Me.txtpan.TabIndex = 111
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 58)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(150, 16)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Dock Milk Sample Password"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 25)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(143, 16)
        Me.MyLabel2.TabIndex = 21
        Me.MyLabel2.Text = "Dock Milk Recipt Password"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(91, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 19)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(12, 7)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(73, 19)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "Update"
        '
        'Enter_password
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(319, 119)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Enter_password"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enter_password"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtsample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtsample As common.Controls.MyTextBox
    Friend WithEvents txtpan As common.Controls.MyTextBox
    Friend WithEvents btnUpdate As RadButton
    Friend WithEvents btnCancel As RadButton
End Class

