<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCommonServicesSetting
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
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gbPermissionSettingForTransactionWithBank = New System.Windows.Forms.GroupBox
        Me.rbtnNone = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnBankPermission = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnGLSecurity = New Telerik.WinControls.UI.RadRadioButton
        Me.ChkInTransit = New common.Controls.MyCheckBox
        Me.chkBrachnAccounting = New common.Controls.MyCheckBox
        Me.ChkAllowToUseSubAcc = New common.Controls.MyCheckBox
        Me.chkShowTaxRateColumnonTransaction = New common.Controls.MyCheckBox
        Me.chkGLACAccordingTaxRate = New common.Controls.MyCheckBox
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.chkIdleTimer = New common.Controls.MyCheckBox
        Me.txtIdleTimeInMnt = New common.Controls.MyTextBox
        Me.lblCompanyName = New common.Controls.MyLabel
        Me.grpApplicationIdle = New System.Windows.Forms.GroupBox
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.gbPermissionSettingForTransactionWithBank.SuspendLayout()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBankPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGLSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkInTransit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBrachnAccounting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllowToUseSubAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowTaxRateColumnonTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGLACAccordingTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIdleTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdleTimeInMnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpApplicationIdle.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(3, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(83, 21)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(832, 382)
        Me.SplitContainer1.SplitterDistance = 347
        Me.SplitContainer1.TabIndex = 13
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.grpApplicationIdle)
        Me.RadGroupBox1.Controls.Add(Me.gbPermissionSettingForTransactionWithBank)
        Me.RadGroupBox1.Controls.Add(Me.ChkInTransit)
        Me.RadGroupBox1.Controls.Add(Me.chkBrachnAccounting)
        Me.RadGroupBox1.Controls.Add(Me.ChkAllowToUseSubAcc)
        Me.RadGroupBox1.Controls.Add(Me.chkShowTaxRateColumnonTransaction)
        Me.RadGroupBox1.Controls.Add(Me.chkGLACAccordingTaxRate)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(832, 347)
        Me.RadGroupBox1.TabIndex = 0
        '
        'gbPermissionSettingForTransactionWithBank
        '
        Me.gbPermissionSettingForTransactionWithBank.Controls.Add(Me.rbtnNone)
        Me.gbPermissionSettingForTransactionWithBank.Controls.Add(Me.rbtnBankPermission)
        Me.gbPermissionSettingForTransactionWithBank.Controls.Add(Me.rbtnGLSecurity)
        Me.gbPermissionSettingForTransactionWithBank.Location = New System.Drawing.Point(259, 4)
        Me.gbPermissionSettingForTransactionWithBank.Name = "gbPermissionSettingForTransactionWithBank"
        Me.gbPermissionSettingForTransactionWithBank.Size = New System.Drawing.Size(324, 45)
        Me.gbPermissionSettingForTransactionWithBank.TabIndex = 5
        Me.gbPermissionSettingForTransactionWithBank.TabStop = False
        Me.gbPermissionSettingForTransactionWithBank.Text = "Permission Settings For Transaction With Bank"
        '
        'rbtnNone
        '
        Me.rbtnNone.Location = New System.Drawing.Point(9, 19)
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(48, 18)
        Me.rbtnNone.TabIndex = 2
        Me.rbtnNone.Text = "None"
        '
        'rbtnBankPermission
        '
        Me.rbtnBankPermission.Location = New System.Drawing.Point(213, 19)
        Me.rbtnBankPermission.Name = "rbtnBankPermission"
        Me.rbtnBankPermission.Size = New System.Drawing.Size(102, 18)
        Me.rbtnBankPermission.TabIndex = 1
        Me.rbtnBankPermission.Text = "Bank Permission"
        '
        'rbtnGLSecurity
        '
        Me.rbtnGLSecurity.Location = New System.Drawing.Point(98, 19)
        Me.rbtnGLSecurity.Name = "rbtnGLSecurity"
        Me.rbtnGLSecurity.Size = New System.Drawing.Size(76, 18)
        Me.rbtnGLSecurity.TabIndex = 0
        Me.rbtnGLSecurity.Text = "GL Security"
        '
        'ChkInTransit
        '
        Me.ChkInTransit.Location = New System.Drawing.Point(13, 103)
        Me.ChkInTransit.MyLinkLable1 = Nothing
        Me.ChkInTransit.MyLinkLable2 = Nothing
        Me.ChkInTransit.Name = "ChkInTransit"
        Me.ChkInTransit.Size = New System.Drawing.Size(166, 18)
        Me.ChkInTransit.TabIndex = 4
        Me.ChkInTransit.Tag1 = Nothing
        Me.ChkInTransit.Text = "In Transit Feature Is Required"
        '
        'chkBrachnAccounting
        '
        Me.chkBrachnAccounting.Location = New System.Drawing.Point(13, 79)
        Me.chkBrachnAccounting.MyLinkLable1 = Nothing
        Me.chkBrachnAccounting.MyLinkLable2 = Nothing
        Me.chkBrachnAccounting.Name = "chkBrachnAccounting"
        Me.chkBrachnAccounting.Size = New System.Drawing.Size(146, 18)
        Me.chkBrachnAccounting.TabIndex = 3
        Me.chkBrachnAccounting.Tag1 = Nothing
        Me.chkBrachnAccounting.Text = "Apply Branch Accounting"
        '
        'ChkAllowToUseSubAcc
        '
        Me.ChkAllowToUseSubAcc.Location = New System.Drawing.Point(12, 55)
        Me.ChkAllowToUseSubAcc.MyLinkLable1 = Nothing
        Me.ChkAllowToUseSubAcc.MyLinkLable2 = Nothing
        Me.ChkAllowToUseSubAcc.Name = "ChkAllowToUseSubAcc"
        Me.ChkAllowToUseSubAcc.Size = New System.Drawing.Size(149, 18)
        Me.ChkAllowToUseSubAcc.TabIndex = 2
        Me.ChkAllowToUseSubAcc.Tag1 = Nothing
        Me.ChkAllowToUseSubAcc.Text = "Allow to use Sub-Account"
        '
        'chkShowTaxRateColumnonTransaction
        '
        Me.chkShowTaxRateColumnonTransaction.Location = New System.Drawing.Point(12, 31)
        Me.chkShowTaxRateColumnonTransaction.MyLinkLable1 = Nothing
        Me.chkShowTaxRateColumnonTransaction.MyLinkLable2 = Nothing
        Me.chkShowTaxRateColumnonTransaction.Name = "chkShowTaxRateColumnonTransaction"
        Me.chkShowTaxRateColumnonTransaction.Size = New System.Drawing.Size(212, 18)
        Me.chkShowTaxRateColumnonTransaction.TabIndex = 1
        Me.chkShowTaxRateColumnonTransaction.Tag1 = Nothing
        Me.chkShowTaxRateColumnonTransaction.Text = "Show Tax Rate Column on Transaction"
        '
        'chkGLACAccordingTaxRate
        '
        Me.chkGLACAccordingTaxRate.Location = New System.Drawing.Point(13, 7)
        Me.chkGLACAccordingTaxRate.MyLinkLable1 = Nothing
        Me.chkGLACAccordingTaxRate.MyLinkLable2 = Nothing
        Me.chkGLACAccordingTaxRate.Name = "chkGLACAccordingTaxRate"
        Me.chkGLACAccordingTaxRate.Size = New System.Drawing.Size(216, 18)
        Me.chkGLACAccordingTaxRate.TabIndex = 0
        Me.chkGLACAccordingTaxRate.Tag1 = Nothing
        Me.chkGLACAccordingTaxRate.Text = "Pick GL Account According To Tax Rate"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(746, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 21)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'chkIdleTimer
        '
        Me.chkIdleTimer.Location = New System.Drawing.Point(6, 19)
        Me.chkIdleTimer.MyLinkLable1 = Nothing
        Me.chkIdleTimer.MyLinkLable2 = Nothing
        Me.chkIdleTimer.Name = "chkIdleTimer"
        Me.chkIdleTimer.Size = New System.Drawing.Size(101, 18)
        Me.chkIdleTimer.TabIndex = 6
        Me.chkIdleTimer.Tag1 = Nothing
        Me.chkIdleTimer.Text = "Is Idle Timer ON"
        '
        'txtIdleTimeInMnt
        '
        Me.txtIdleTimeInMnt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdleTimeInMnt.Location = New System.Drawing.Point(231, 18)
        Me.txtIdleTimeInMnt.MaxLength = 100
        Me.txtIdleTimeInMnt.MendatroryField = False
        Me.txtIdleTimeInMnt.MyLinkLable1 = Nothing
        Me.txtIdleTimeInMnt.MyLinkLable2 = Nothing
        Me.txtIdleTimeInMnt.Name = "txtIdleTimeInMnt"
        Me.txtIdleTimeInMnt.Size = New System.Drawing.Size(77, 18)
        Me.txtIdleTimeInMnt.TabIndex = 7
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(127, 19)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(102, 16)
        Me.lblCompanyName.TabIndex = 62
        Me.lblCompanyName.Text = "Idle Time in Minute"
        '
        'grpApplicationIdle
        '
        Me.grpApplicationIdle.Controls.Add(Me.chkIdleTimer)
        Me.grpApplicationIdle.Controls.Add(Me.lblCompanyName)
        Me.grpApplicationIdle.Controls.Add(Me.txtIdleTimeInMnt)
        Me.grpApplicationIdle.Location = New System.Drawing.Point(259, 55)
        Me.grpApplicationIdle.Name = "grpApplicationIdle"
        Me.grpApplicationIdle.Size = New System.Drawing.Size(324, 45)
        Me.grpApplicationIdle.TabIndex = 63
        Me.grpApplicationIdle.TabStop = False
        Me.grpApplicationIdle.Text = "Application Idle Setting"
        '
        'frmCommonServicesSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 382)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCommonServicesSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Common Services Setting"
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.gbPermissionSettingForTransactionWithBank.ResumeLayout(False)
        Me.gbPermissionSettingForTransactionWithBank.PerformLayout()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBankPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGLSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkInTransit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBrachnAccounting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllowToUseSubAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowTaxRateColumnonTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGLACAccordingTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIdleTimer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdleTimeInMnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpApplicationIdle.ResumeLayout(False)
        Me.grpApplicationIdle.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkGLACAccordingTaxRate As common.Controls.MyCheckBox
    Friend WithEvents chkShowTaxRateColumnonTransaction As common.Controls.MyCheckBox
    Friend WithEvents ChkAllowToUseSubAcc As common.Controls.MyCheckBox
    Friend WithEvents chkBrachnAccounting As common.Controls.MyCheckBox
    Friend WithEvents ChkInTransit As common.Controls.MyCheckBox
    Friend WithEvents gbPermissionSettingForTransactionWithBank As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnBankPermission As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnGLSecurity As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkIdleTimer As common.Controls.MyCheckBox
    Friend WithEvents txtIdleTimeInMnt As common.Controls.MyTextBox
    Friend WithEvents lblCompanyName As common.Controls.MyLabel
    Friend WithEvents grpApplicationIdle As System.Windows.Forms.GroupBox
End Class

