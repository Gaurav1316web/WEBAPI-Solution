<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCompany
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndBaseCurrency = New common.UserControls.txtFinder()
        Me.lblBaseCurrency = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnDairy = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnGeneral = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtPasswordConfirm = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtPassword = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCompanyName = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCompanyCode = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnOK = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnDairy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPasswordConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndBaseCurrency)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBaseCurrency)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPasswordConfirm)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCompanyName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCompanyCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOK)
        Me.SplitContainer1.Size = New System.Drawing.Size(360, 231)
        Me.SplitContainer1.SplitterDistance = 201
        Me.SplitContainer1.TabIndex = 0
        '
        'fndBaseCurrency
        '
        Me.fndBaseCurrency.Location = New System.Drawing.Point(108, 132)
        Me.fndBaseCurrency.MendatroryField = True
        Me.fndBaseCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBaseCurrency.MyLinkLable1 = Nothing
        Me.fndBaseCurrency.MyLinkLable2 = Nothing
        Me.fndBaseCurrency.MyReadOnly = False
        Me.fndBaseCurrency.MyShowMasterFormButton = False
        Me.fndBaseCurrency.Name = "fndBaseCurrency"
        Me.fndBaseCurrency.Size = New System.Drawing.Size(246, 19)
        Me.fndBaseCurrency.TabIndex = 5
        Me.fndBaseCurrency.Value = ""
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(5, 133)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(81, 16)
        Me.lblBaseCurrency.TabIndex = 38
        Me.lblBaseCurrency.Text = "Base Currency"
        '
        'MyLabel7
        '
        Me.MyLabel7.Location = New System.Drawing.Point(5, 165)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel7.TabIndex = 11
        Me.MyLabel7.Text = "Vertical"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnDairy)
        Me.GroupBox1.Controls.Add(Me.rbtnGeneral)
        Me.GroupBox1.Location = New System.Drawing.Point(108, 153)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(136, 31)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'rbtnDairy
        '
        Me.rbtnDairy.Location = New System.Drawing.Point(78, 9)
        Me.rbtnDairy.Name = "rbtnDairy"
        Me.rbtnDairy.Size = New System.Drawing.Size(46, 18)
        Me.rbtnDairy.TabIndex = 1
        Me.rbtnDairy.TabStop = False
        Me.rbtnDairy.Text = "Dairy"
        '
        'rbtnGeneral
        '
        Me.rbtnGeneral.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnGeneral.Location = New System.Drawing.Point(11, 9)
        Me.rbtnGeneral.Name = "rbtnGeneral"
        Me.rbtnGeneral.Size = New System.Drawing.Size(59, 18)
        Me.rbtnGeneral.TabIndex = 0
        Me.rbtnGeneral.Text = "General"
        Me.rbtnGeneral.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel6
        '
        Me.MyLabel6.AutoSize = False
        Me.MyLabel6.BorderVisible = True
        Me.MyLabel6.Location = New System.Drawing.Point(108, 58)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(246, 22)
        Me.MyLabel6.TabIndex = 2
        Me.MyLabel6.Text = "admin"
        Me.MyLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPasswordConfirm
        '
        Me.txtPasswordConfirm.Location = New System.Drawing.Point(108, 108)
        Me.txtPasswordConfirm.MendatroryField = True
        Me.txtPasswordConfirm.MyLinkLable1 = Me.MyLabel5
        Me.txtPasswordConfirm.MyLinkLable2 = Nothing
        Me.txtPasswordConfirm.Name = "txtPasswordConfirm"
        Me.txtPasswordConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswordConfirm.Size = New System.Drawing.Size(246, 20)
        Me.txtPasswordConfirm.TabIndex = 4
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(5, 109)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel5.TabIndex = 9
        Me.MyLabel5.Text = "Confirm Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(108, 84)
        Me.txtPassword.MendatroryField = True
        Me.txtPassword.MyLinkLable1 = Me.MyLabel4
        Me.txtPassword.MyLinkLable2 = Nothing
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(246, 20)
        Me.txtPassword.TabIndex = 3
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(5, 85)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel4.TabIndex = 8
        Me.MyLabel4.Text = "Password"
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(5, 60)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(48, 18)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Login ID"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(108, 34)
        Me.txtCompanyName.MendatroryField = True
        Me.txtCompanyName.MyLinkLable1 = Me.MyLabel2
        Me.txtCompanyName.MyLinkLable2 = Nothing
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(246, 20)
        Me.txtCompanyName.TabIndex = 1
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(5, 35)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Company"
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.Location = New System.Drawing.Point(108, 10)
        Me.txtCompanyCode.MaxLength = 9
        Me.txtCompanyCode.MendatroryField = True
        Me.txtCompanyCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCompanyCode.MyLinkLable2 = Nothing
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.Size = New System.Drawing.Size(246, 20)
        Me.txtCompanyCode.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(5, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Company Code"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(183, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(93, 20)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(84, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(93, 20)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'FrmCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 231)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCompany"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Company - User Details"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnDairy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPasswordConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPasswordConfirm As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtPassword As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCompanyName As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCompanyCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDairy As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnGeneral As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents fndBaseCurrency As common.UserControls.txtFinder
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
End Class

