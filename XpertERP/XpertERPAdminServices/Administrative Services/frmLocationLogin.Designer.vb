<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocationLogin
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.chkLocationLogin = New Telerik.WinControls.UI.RadCheckBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgUserLevel1 = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rdoUserSelect = New common.Controls.MyRadioButton
        Me.rdoUserAll = New common.Controls.MyRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkLocationLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rdoUserSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoUserAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(851, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkLocationLogin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(925, 587)
        Me.SplitContainer1.SplitterDistance = 538
        Me.SplitContainer1.TabIndex = 0
        '
        'chkLocationLogin
        '
        Me.chkLocationLogin.Location = New System.Drawing.Point(32, 15)
        Me.chkLocationLogin.Name = "chkLocationLogin"
        Me.chkLocationLogin.Size = New System.Drawing.Size(127, 18)
        Me.chkLocationLogin.TabIndex = 0
        Me.chkLocationLogin.Text = "Ask Location at Login"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgUserLevel1)
        Me.RadGroupBox1.Controls.Add(Me.Panel3)
        Me.RadGroupBox1.HeaderText = "Users"
        Me.RadGroupBox1.Location = New System.Drawing.Point(22, 55)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(488, 480)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Users"
        '
        'cbgUserLevel1
        '
        Me.cbgUserLevel1.CheckedValue = Nothing
        Me.cbgUserLevel1.DataSource = Nothing
        Me.cbgUserLevel1.DisplayMember = "Name"
        Me.cbgUserLevel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgUserLevel1.Location = New System.Drawing.Point(10, 40)
        Me.cbgUserLevel1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgUserLevel1.MyShowHeadrText = False
        Me.cbgUserLevel1.Name = "cbgUserLevel1"
        Me.cbgUserLevel1.Size = New System.Drawing.Size(468, 430)
        Me.cbgUserLevel1.TabIndex = 1
        Me.cbgUserLevel1.TabStop = False
        Me.cbgUserLevel1.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rdoUserSelect)
        Me.Panel3.Controls.Add(Me.rdoUserAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(468, 20)
        Me.Panel3.TabIndex = 0
        '
        'rdoUserSelect
        '
        Me.rdoUserSelect.Location = New System.Drawing.Point(245, 2)
        Me.rdoUserSelect.MyLinkLable1 = Nothing
        Me.rdoUserSelect.MyLinkLable2 = Nothing
        Me.rdoUserSelect.Name = "rdoUserSelect"
        Me.rdoUserSelect.Size = New System.Drawing.Size(50, 18)
        Me.rdoUserSelect.TabIndex = 1
        Me.rdoUserSelect.Text = "Select"
        '
        'rdoUserAll
        '
        Me.rdoUserAll.Location = New System.Drawing.Point(157, 2)
        Me.rdoUserAll.MyLinkLable1 = Nothing
        Me.rdoUserAll.MyLinkLable2 = Nothing
        Me.rdoUserAll.Name = "rdoUserAll"
        Me.rdoUserAll.Size = New System.Drawing.Size(33, 18)
        Me.rdoUserAll.TabIndex = 0
        Me.rdoUserAll.Text = "All"
        '
        'btnReset
        '
        Me.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(78, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(69, 22)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmLocationLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(925, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLocationLogin"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Approval Screen"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkLocationLogin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rdoUserSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoUserAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgUserLevel1 As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdoUserSelect As common.Controls.MyRadioButton
    Friend WithEvents rdoUserAll As common.Controls.MyRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkLocationLogin As Telerik.WinControls.UI.RadCheckBox
End Class

