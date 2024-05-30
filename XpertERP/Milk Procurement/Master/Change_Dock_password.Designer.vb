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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Enter_password))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnSamplePassword = New Telerik.WinControls.UI.RadButton()
        Me.txtsample = New common.Controls.MyTextBox()
        Me.txtreceipt = New common.Controls.MyTextBox()
        Me.btnReceiptPassword = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnSamplePassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReceiptPassword, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSamplePassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtsample)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtreceipt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReceiptPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Size = New System.Drawing.Size(359, 119)
        Me.SplitContainer1.SplitterDistance = 86
        Me.SplitContainer1.TabIndex = 0
        '
        'btnSamplePassword
        '
        Me.btnSamplePassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSamplePassword.Image = CType(resources.GetObject("btnSamplePassword.Image"), System.Drawing.Image)
        Me.btnSamplePassword.Location = New System.Drawing.Point(316, 58)
        Me.btnSamplePassword.Name = "btnSamplePassword"
        Me.btnSamplePassword.Size = New System.Drawing.Size(20, 18)
        Me.btnSamplePassword.TabIndex = 114
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
        'txtreceipt
        '
        Me.txtreceipt.CalculationExpression = Nothing
        Me.txtreceipt.FieldCode = Nothing
        Me.txtreceipt.FieldDesc = Nothing
        Me.txtreceipt.FieldMaxLength = 0
        Me.txtreceipt.FieldName = Nothing
        Me.txtreceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreceipt.isCalculatedField = False
        Me.txtreceipt.IsSourceFromTable = False
        Me.txtreceipt.IsSourceFromValueList = False
        Me.txtreceipt.IsUnique = False
        Me.txtreceipt.Location = New System.Drawing.Point(166, 25)
        Me.txtreceipt.MaxLength = 15
        Me.txtreceipt.MendatroryField = True
        Me.txtreceipt.MyLinkLable1 = Nothing
        Me.txtreceipt.MyLinkLable2 = Nothing
        Me.txtreceipt.Name = "txtreceipt"
        Me.txtreceipt.NullText = "Password"
        Me.txtreceipt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtreceipt.ReferenceFieldDesc = Nothing
        Me.txtreceipt.ReferenceFieldName = Nothing
        Me.txtreceipt.ReferenceTableName = Nothing
        Me.txtreceipt.Size = New System.Drawing.Size(148, 18)
        Me.txtreceipt.TabIndex = 111
        '
        'btnReceiptPassword
        '
        Me.btnReceiptPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReceiptPassword.Image = CType(resources.GetObject("btnReceiptPassword.Image"), System.Drawing.Image)
        Me.btnReceiptPassword.Location = New System.Drawing.Point(316, 25)
        Me.btnReceiptPassword.Name = "btnReceiptPassword"
        Me.btnReceiptPassword.Size = New System.Drawing.Size(20, 18)
        Me.btnReceiptPassword.TabIndex = 113
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
        Me.btnCancel.Location = New System.Drawing.Point(172, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 19)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(93, 7)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(73, 19)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "Update"
        '
        'Enter_password
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 119)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(367, 149)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(367, 149)
        Me.Name = "Enter_password"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(410, 149)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Manual Dock Password"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnSamplePassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReceiptPassword, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtreceipt As common.Controls.MyTextBox
    Friend WithEvents btnUpdate As RadButton
    Friend WithEvents btnCancel As RadButton
    Friend WithEvents btnReceiptPassword As RadButton
    Friend WithEvents btnSamplePassword As RadButton
End Class

