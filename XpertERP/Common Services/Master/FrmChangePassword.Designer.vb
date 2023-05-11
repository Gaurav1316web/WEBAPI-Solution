<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangePassword
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
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtcurpassword = New common.Controls.MyTextBox()
        Me.txtnewpassword = New common.Controls.MyTextBox()
        Me.txtconfpassword = New common.Controls.MyTextBox()
        Me.lblusername = New common.Controls.MyLabel()
        Me.lblusercode = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcurpassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnewpassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtconfpassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblusername, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblusercode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 51)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(100, 18)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Current Password"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(7, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "New Password"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(7, 97)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Confirm Password"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(7, 6)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel4.TabIndex = 9
        Me.MyLabel4.Text = "User Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(7, 28)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel5.TabIndex = 6
        Me.MyLabel5.Text = "User Name"
        '
        'txtcurpassword
        '
        Me.txtcurpassword.CalculationExpression = Nothing
        Me.txtcurpassword.FieldCode = Nothing
        Me.txtcurpassword.FieldDesc = Nothing
        Me.txtcurpassword.FieldMaxLength = 0
        Me.txtcurpassword.FieldName = Nothing
        Me.txtcurpassword.isCalculatedField = False
        Me.txtcurpassword.IsSourceFromTable = False
        Me.txtcurpassword.IsSourceFromValueList = False
        Me.txtcurpassword.IsUnique = False
        Me.txtcurpassword.Location = New System.Drawing.Point(124, 50)
        Me.txtcurpassword.MendatroryField = True
        Me.txtcurpassword.MyLinkLable1 = Me.MyLabel1
        Me.txtcurpassword.MyLinkLable2 = Nothing
        Me.txtcurpassword.Name = "txtcurpassword"
        Me.txtcurpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtcurpassword.ReferenceFieldDesc = Nothing
        Me.txtcurpassword.ReferenceFieldName = Nothing
        Me.txtcurpassword.ReferenceTableName = Nothing
        Me.txtcurpassword.Size = New System.Drawing.Size(167, 20)
        Me.txtcurpassword.TabIndex = 0
        '
        'txtnewpassword
        '
        Me.txtnewpassword.CalculationExpression = Nothing
        Me.txtnewpassword.FieldCode = Nothing
        Me.txtnewpassword.FieldDesc = Nothing
        Me.txtnewpassword.FieldMaxLength = 0
        Me.txtnewpassword.FieldName = Nothing
        Me.txtnewpassword.isCalculatedField = False
        Me.txtnewpassword.IsSourceFromTable = False
        Me.txtnewpassword.IsSourceFromValueList = False
        Me.txtnewpassword.IsUnique = False
        Me.txtnewpassword.Location = New System.Drawing.Point(124, 73)
        Me.txtnewpassword.MendatroryField = True
        Me.txtnewpassword.MyLinkLable1 = Me.MyLabel2
        Me.txtnewpassword.MyLinkLable2 = Nothing
        Me.txtnewpassword.Name = "txtnewpassword"
        Me.txtnewpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtnewpassword.ReferenceFieldDesc = Nothing
        Me.txtnewpassword.ReferenceFieldName = Nothing
        Me.txtnewpassword.ReferenceTableName = Nothing
        Me.txtnewpassword.Size = New System.Drawing.Size(167, 20)
        Me.txtnewpassword.TabIndex = 1
        '
        'txtconfpassword
        '
        Me.txtconfpassword.CalculationExpression = Nothing
        Me.txtconfpassword.FieldCode = Nothing
        Me.txtconfpassword.FieldDesc = Nothing
        Me.txtconfpassword.FieldMaxLength = 0
        Me.txtconfpassword.FieldName = Nothing
        Me.txtconfpassword.isCalculatedField = False
        Me.txtconfpassword.IsSourceFromTable = False
        Me.txtconfpassword.IsSourceFromValueList = False
        Me.txtconfpassword.IsUnique = False
        Me.txtconfpassword.Location = New System.Drawing.Point(124, 96)
        Me.txtconfpassword.MendatroryField = True
        Me.txtconfpassword.MyLinkLable1 = Me.MyLabel3
        Me.txtconfpassword.MyLinkLable2 = Nothing
        Me.txtconfpassword.Name = "txtconfpassword"
        Me.txtconfpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtconfpassword.ReferenceFieldDesc = Nothing
        Me.txtconfpassword.ReferenceFieldName = Nothing
        Me.txtconfpassword.ReferenceTableName = Nothing
        Me.txtconfpassword.Size = New System.Drawing.Size(167, 20)
        Me.txtconfpassword.TabIndex = 2
        '
        'lblusername
        '
        Me.lblusername.FieldName = Nothing
        Me.lblusername.Location = New System.Drawing.Point(124, 28)
        Me.lblusername.Name = "lblusername"
        Me.lblusername.Size = New System.Drawing.Size(2, 2)
        Me.lblusername.TabIndex = 7
        '
        'lblusercode
        '
        Me.lblusercode.FieldName = Nothing
        Me.lblusercode.Location = New System.Drawing.Point(124, 6)
        Me.lblusercode.Name = "lblusercode"
        Me.lblusercode.Size = New System.Drawing.Size(2, 2)
        Me.lblusercode.TabIndex = 8
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(219, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 19)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(2, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 19)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblusername)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblusercode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtconfpassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcurpassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtnewpassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(301, 151)
        Me.SplitContainer1.SplitterDistance = 122
        Me.SplitContainer1.TabIndex = 11
        '
        'FrmChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 151)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmChangePassword"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcurpassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnewpassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtconfpassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblusername, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblusercode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtcurpassword As common.Controls.MyTextBox
    Friend WithEvents txtnewpassword As common.Controls.MyTextBox
    Friend WithEvents txtconfpassword As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblusername As common.Controls.MyLabel
    Friend WithEvents lblusercode As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

