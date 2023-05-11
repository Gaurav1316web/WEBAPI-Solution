<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPJCSettings
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox
        Me.fndBankAccount = New common.UserControls.txtFinder
        Me.lblBankCode = New common.Controls.MyLabel
        Me.txtBankAccountName = New common.Controls.MyTextBox
        Me.chkAutoCreateTask = New Telerik.WinControls.UI.RadCheckBox
        Me.cboLevel = New common.Controls.MyComboBox
        Me.lblLevel = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblSettingCode = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoCreateTask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSettingCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(511, 191)
        Me.SplitContainer1.SplitterDistance = 162
        Me.SplitContainer1.TabIndex = 42
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.fndBankAccount)
        Me.gbdesignation.Controls.Add(Me.lblBankCode)
        Me.gbdesignation.Controls.Add(Me.txtBankAccountName)
        Me.gbdesignation.Controls.Add(Me.chkAutoCreateTask)
        Me.gbdesignation.Controls.Add(Me.cboLevel)
        Me.gbdesignation.Controls.Add(Me.lblLevel)
        Me.gbdesignation.Controls.Add(Me.txtCode)
        Me.gbdesignation.Controls.Add(Me.lblSettingCode)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(6, 6)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(499, 153)
        Me.gbdesignation.TabIndex = 38
        '
        'fndBankAccount
        '
        Me.fndBankAccount.Location = New System.Drawing.Point(115, 75)
        Me.fndBankAccount.MendatroryField = False
        Me.fndBankAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankAccount.MyLinkLable1 = Nothing
        Me.fndBankAccount.MyLinkLable2 = Nothing
        Me.fndBankAccount.MyReadOnly = False
        Me.fndBankAccount.Name = "fndBankAccount"
        Me.fndBankAccount.Size = New System.Drawing.Size(185, 19)
        Me.fndBankAccount.TabIndex = 4
        Me.fndBankAccount.Value = ""
        '
        'lblBankCode
        '
        Me.lblBankCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCode.Location = New System.Drawing.Point(16, 75)
        Me.lblBankCode.Name = "lblBankCode"
        Me.lblBankCode.Size = New System.Drawing.Size(62, 16)
        Me.lblBankCode.TabIndex = 218
        Me.lblBankCode.Text = "Bank Code"
        '
        'txtBankAccountName
        '
        Me.txtBankAccountName.Location = New System.Drawing.Point(303, 75)
        Me.txtBankAccountName.MaxLength = 55
        Me.txtBankAccountName.MendatroryField = False
        Me.txtBankAccountName.MyLinkLable1 = Nothing
        Me.txtBankAccountName.MyLinkLable2 = Nothing
        Me.txtBankAccountName.Name = "txtBankAccountName"
        Me.txtBankAccountName.ReadOnly = True
        Me.txtBankAccountName.Size = New System.Drawing.Size(190, 20)
        Me.txtBankAccountName.TabIndex = 5
        Me.txtBankAccountName.TabStop = False
        '
        'chkAutoCreateTask
        '
        Me.chkAutoCreateTask.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkAutoCreateTask.Location = New System.Drawing.Point(116, 55)
        Me.chkAutoCreateTask.Name = "chkAutoCreateTask"
        Me.chkAutoCreateTask.Size = New System.Drawing.Size(149, 18)
        Me.chkAutoCreateTask.TabIndex = 3
        Me.chkAutoCreateTask.Text = "Create Task Automatically"
        Me.chkAutoCreateTask.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboLevel
        '
        Me.cboLevel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.cboLevel.Items.Add(RadListDataItem1)
        Me.cboLevel.Items.Add(RadListDataItem2)
        Me.cboLevel.Items.Add(RadListDataItem3)
        Me.cboLevel.Items.Add(RadListDataItem4)
        Me.cboLevel.Location = New System.Drawing.Point(116, 35)
        Me.cboLevel.MendatroryField = True
        Me.cboLevel.MyLinkLable1 = Me.lblLevel
        Me.cboLevel.MyLinkLable2 = Nothing
        Me.cboLevel.Name = "cboLevel"
        Me.cboLevel.Size = New System.Drawing.Size(219, 18)
        Me.cboLevel.TabIndex = 2
        '
        'lblLevel
        '
        Me.lblLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel.Location = New System.Drawing.Point(15, 34)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(33, 16)
        Me.lblLevel.TabIndex = 215
        Me.lblLevel.Text = "Level"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(116, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.TabStop = False
        Me.txtCode.Value = ""
        '
        'lblSettingCode
        '
        Me.lblSettingCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSettingCode.Location = New System.Drawing.Point(13, 13)
        Me.lblSettingCode.Name = "lblSettingCode"
        Me.lblSettingCode.Size = New System.Drawing.Size(72, 16)
        Me.lblSettingCode.TabIndex = 37
        Me.lblSettingCode.Text = "Setting Code"
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(317, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(434, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'frmPJCSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 191)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPJCSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settings"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoCreateTask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSettingCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblSettingCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboLevel As common.Controls.MyComboBox
    Friend WithEvents lblLevel As common.Controls.MyLabel
    Friend WithEvents chkAutoCreateTask As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblBankCode As common.Controls.MyLabel
    Friend WithEvents fndBankAccount As common.UserControls.txtFinder
    Friend WithEvents txtBankAccountName As common.Controls.MyTextBox
End Class

