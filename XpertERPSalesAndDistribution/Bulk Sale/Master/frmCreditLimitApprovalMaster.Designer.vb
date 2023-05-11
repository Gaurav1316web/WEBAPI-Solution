<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCreditLimitApprovalMaster
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ddmodule = New common.Controls.MyComboBox
        Me.rgBank = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgUser = New common.MyCheckBoxGrid
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.lblCode = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.ddmodule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgBank.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddmodule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rgBank)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(747, 455)
        Me.SplitContainer1.SplitterDistance = 416
        Me.SplitContainer1.TabIndex = 0
        '
        'ddmodule
        '
        Me.ddmodule.AllowShowFocusCues = False
        Me.ddmodule.AutoCompleteDisplayMember = Nothing
        Me.ddmodule.AutoCompleteValueMember = Nothing
        Me.ddmodule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddmodule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Fresh Sale"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Product Sale"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Bulk Sale"
        RadListDataItem3.TextWrap = True
        Me.ddmodule.Items.Add(RadListDataItem1)
        Me.ddmodule.Items.Add(RadListDataItem2)
        Me.ddmodule.Items.Add(RadListDataItem3)
        Me.ddmodule.Location = New System.Drawing.Point(74, 10)
        Me.ddmodule.MendatroryField = True
        Me.ddmodule.MyLinkLable1 = Me.lblCode
        Me.ddmodule.MyLinkLable2 = Nothing
        Me.ddmodule.Name = "ddmodule"
        Me.ddmodule.Size = New System.Drawing.Size(181, 18)
        Me.ddmodule.TabIndex = 9
        '
        'rgBank
        '
        Me.rgBank.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgBank.Controls.Add(Me.cbgUser)
        Me.rgBank.HeaderText = "User"
        Me.rgBank.Location = New System.Drawing.Point(22, 34)
        Me.rgBank.Name = "rgBank"
        Me.rgBank.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rgBank.Size = New System.Drawing.Size(388, 184)
        Me.rgBank.TabIndex = 30
        Me.rgBank.Text = "User"
        '
        'cbgUser
        '
        Me.cbgUser.CheckedValue = Nothing
        Me.cbgUser.DataSource = Nothing
        Me.cbgUser.DisplayMember = "Name"
        Me.cbgUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgUser.Location = New System.Drawing.Point(10, 20)
        Me.cbgUser.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgUser.MyShowHeadrText = False
        Me.cbgUser.Name = "cbgUser"
        Me.cbgUser.Size = New System.Drawing.Size(368, 154)
        Me.cbgUser.TabIndex = 1
        Me.cbgUser.ValueMember = "Code"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(84, 13)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 19)
        Me.btnDelete.TabIndex = 39
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(12, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 19)
        Me.btnSave.TabIndex = 37
        Me.btnSave.Text = "save"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(156, 14)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(66, 19)
        Me.btnReset.TabIndex = 36
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(678, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 35
        Me.btnClose.Text = "Close"
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(22, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(46, 16)
        Me.lblCode.TabIndex = 381
        Me.lblCode.Text = "Module "
        '
        'FrmCreditLimitApprovalMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCreditLimitApprovalMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCreditLimitApprovalMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.ddmodule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgBank.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rgBank As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgUser As common.MyCheckBoxGrid
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddctcrange As common.Controls.MyComboBox
    Friend WithEvents ddmodule As common.Controls.MyComboBox
    Friend WithEvents lblCode As common.Controls.MyLabel
End Class

