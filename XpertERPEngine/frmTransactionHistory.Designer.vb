<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransactionHistory
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
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnShowData = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvDetail = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Location = New System.Drawing.Point(86, 1)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(74, 24)
        Me.btnUnSelect.TabIndex = 1
        Me.btnUnSelect.Text = "Un-Select All"
        Me.btnUnSelect.Visible = False
        '
        'btnShowData
        '
        Me.btnShowData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowData.Location = New System.Drawing.Point(9, 3)
        Me.btnShowData.Name = "btnShowData"
        Me.btnShowData.Size = New System.Drawing.Size(77, 24)
        Me.btnShowData.TabIndex = 0
        Me.btnShowData.Text = "Show"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(920, 210)
        Me.gv1.TabIndex = 1
        Me.gv1.Text = "RadGridView1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvDetail)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(920, 209)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Details"
        '
        'gvDetail
        '
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Location = New System.Drawing.Point(3, 18)
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.Size = New System.Drawing.Size(914, 188)
        Me.gvDetail.TabIndex = 1
        Me.gvDetail.Text = "RadGridView2"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(832, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(77, 24)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(920, 423)
        Me.SplitContainer2.SplitterDistance = 210
        Me.SplitContainer2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnUnSelect)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(920, 27)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnShowData)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 450)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(920, 29)
        Me.Panel2.TabIndex = 4
        '
        'frmTransactionHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 479)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmTransactionHistory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowInTaskbar = False
        Me.ShowItemToolTips = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "History for Transaction Screen"
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnShowData As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gvDetail As Telerik.WinControls.UI.RadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class

