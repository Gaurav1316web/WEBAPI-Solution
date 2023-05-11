<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroupOfDeduction
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.GBDedGrp = New System.Windows.Forms.GroupBox
        Me.fndDedGrp = New common.UserControls.txtFinder
        Me.GBMCC = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgMCC = New common.MyCheckBoxGrid
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GBDedGrp.SuspendLayout()
        CType(Me.GBMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBMCC.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GBDedGrp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GBMCC)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(629, 448)
        Me.SplitContainer1.SplitterDistance = 401
        Me.SplitContainer1.TabIndex = 0
        '
        'GBDedGrp
        '
        Me.GBDedGrp.Controls.Add(Me.fndDedGrp)
        Me.GBDedGrp.Location = New System.Drawing.Point(21, 23)
        Me.GBDedGrp.Name = "GBDedGrp"
        Me.GBDedGrp.Size = New System.Drawing.Size(204, 60)
        Me.GBDedGrp.TabIndex = 31
        Me.GBDedGrp.TabStop = False
        Me.GBDedGrp.Text = "Deduction Group"
        '
        'fndDedGrp
        '
        Me.fndDedGrp.Location = New System.Drawing.Point(11, 21)
        Me.fndDedGrp.MendatroryField = False
        Me.fndDedGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDedGrp.MyLinkLable1 = Nothing
        Me.fndDedGrp.MyLinkLable2 = Nothing
        Me.fndDedGrp.MyReadOnly = False
        Me.fndDedGrp.MyShowMasterFormButton = False
        Me.fndDedGrp.Name = "fndDedGrp"
        Me.fndDedGrp.Size = New System.Drawing.Size(177, 19)
        Me.fndDedGrp.TabIndex = 28
        Me.fndDedGrp.Value = ""
        '
        'GBMCC
        '
        Me.GBMCC.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GBMCC.Controls.Add(Me.cbgMCC)
        Me.GBMCC.HeaderText = "MCC"
        Me.GBMCC.Location = New System.Drawing.Point(231, 21)
        Me.GBMCC.Name = "GBMCC"
        Me.GBMCC.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GBMCC.Size = New System.Drawing.Size(279, 289)
        Me.GBMCC.TabIndex = 30
        Me.GBMCC.Text = "MCC"
        '
        'cbgMCC
        '
        Me.cbgMCC.CheckedValue = Nothing
        Me.cbgMCC.DataSource = Nothing
        Me.cbgMCC.DisplayMember = "Name"
        Me.cbgMCC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMCC.Location = New System.Drawing.Point(10, 20)
        Me.cbgMCC.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMCC.MyShowHeadrText = False
        Me.cbgMCC.Name = "cbgMCC"
        Me.cbgMCC.Size = New System.Drawing.Size(259, 259)
        Me.cbgMCC.TabIndex = 1
        Me.cbgMCC.ValueMember = "Code"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(74, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 19)
        Me.btnDelete.TabIndex = 39
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 19)
        Me.btnSave.TabIndex = 37
        Me.btnSave.Text = "save"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(146, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(66, 19)
        Me.btnReset.TabIndex = 36
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(551, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 35
        Me.btnClose.Text = "Close"
        '
        'FrmGroupOfDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 448)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGroupOfDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmGroupOfDeduction"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GBDedGrp.ResumeLayout(False)
        CType(Me.GBMCC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBMCC.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GBDedGrp As System.Windows.Forms.GroupBox
    Friend WithEvents fndDedGrp As common.UserControls.txtFinder
    Friend WithEvents GBMCC As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMCC As common.MyCheckBoxGrid
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

