<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Account_Mapping
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
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblAccountCode = New Telerik.WinControls.UI.RadLabel
        Me.lblAcctNme = New Telerik.WinControls.UI.RadLabel
        Me.txtfndAccountCode = New common.UserControls.txtFinder
        Me.dgvAccountMap = New common.UserControls.MyRadGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnUnSelectAll = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblAccountCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAcctNme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.SplitContainer1)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(494, 415)
        Me.RadPanel2.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAccountCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAcctNme)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtfndAccountCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvAccountMap)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(494, 415)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 0
        '
        'lblAccountCode
        '
        Me.lblAccountCode.Location = New System.Drawing.Point(3, 3)
        Me.lblAccountCode.Name = "lblAccountCode"
        Me.lblAccountCode.Size = New System.Drawing.Size(77, 18)
        Me.lblAccountCode.TabIndex = 1
        Me.lblAccountCode.Text = "Account Code"
        '
        'lblAcctNme
        '
        Me.lblAcctNme.Location = New System.Drawing.Point(247, 3)
        Me.lblAcctNme.Name = "lblAcctNme"
        Me.lblAcctNme.Size = New System.Drawing.Size(2, 2)
        Me.lblAcctNme.TabIndex = 6
        '
        'txtfndAccountCode
        '
        Me.txtfndAccountCode.Location = New System.Drawing.Point(96, 3)
        Me.txtfndAccountCode.MendatroryField = False
        Me.txtfndAccountCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfndAccountCode.MyLinkLable1 = Nothing
        Me.txtfndAccountCode.MyLinkLable2 = Nothing
        Me.txtfndAccountCode.MyReadOnly = False
        Me.txtfndAccountCode.Name = "txtfndAccountCode"
        Me.txtfndAccountCode.Size = New System.Drawing.Size(145, 18)
        Me.txtfndAccountCode.TabIndex = 0
        Me.txtfndAccountCode.Value = ""
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAccountMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvAccountMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccountMap.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvAccountMap.ForeColor = System.Drawing.Color.Black
        Me.dgvAccountMap.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvAccountMap.Location = New System.Drawing.Point(0, 0)
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.MasterTemplate.EnableFiltering = True
        Me.dgvAccountMap.MasterTemplate.EnableGrouping = False
        Me.dgvAccountMap.Name = "dgvAccountMap"
        Me.dgvAccountMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvAccountMap.Size = New System.Drawing.Size(494, 348)
        Me.dgvAccountMap.TabIndex = 0
        Me.dgvAccountMap.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btnsave)
        Me.Panel1.Controls.Add(Me.btnSelectAll)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnUnSelectAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 348)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(494, 38)
        Me.Panel1.TabIndex = 0
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(4, 8)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(76, 24)
        Me.Btnsave.TabIndex = 0
        Me.Btnsave.Text = "Save"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.Location = New System.Drawing.Point(85, 8)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(76, 24)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "Select All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(411, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnUnSelectAll
        '
        Me.btnUnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnSelectAll.Location = New System.Drawing.Point(166, 8)
        Me.btnUnSelectAll.Name = "btnUnSelectAll"
        Me.btnUnSelectAll.Size = New System.Drawing.Size(76, 24)
        Me.btnUnSelectAll.TabIndex = 2
        Me.btnUnSelectAll.Text = "Unselect All"
        '
        'Frm_Account_Mapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 415)
        Me.Controls.Add(Me.RadPanel2)
        Me.Name = "Frm_Account_Mapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Mapping"
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblAccountCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAcctNme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents txtfndAccountCode As common.UserControls.txtFinder
    Friend WithEvents lblAccountCode As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvAccountMap As common.UserControls.MyRadGridView
    Friend WithEvents lblAcctNme As Telerik.WinControls.UI.RadLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class

