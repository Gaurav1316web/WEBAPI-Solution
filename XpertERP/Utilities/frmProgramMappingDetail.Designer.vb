<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProgramMappingDetail
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
        Me.RadPageView = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv2 = New common.UserControls.MyRadGridView
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv3 = New common.UserControls.MyRadGridView
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv4 = New common.UserControls.MyRadGridView
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv5 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gv5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv5.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(894, 442)
        Me.SplitContainer1.SplitterDistance = 401
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView
        '
        Me.RadPageView.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView.Name = "RadPageView"
        Me.RadPageView.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView.Size = New System.Drawing.Size(894, 398)
        Me.RadPageView.TabIndex = 0
        Me.RadPageView.Text = "RadPageView"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(49.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(873, 350)
        Me.RadPageViewPage1.Text = "Table1"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(873, 350)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "gv1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(49.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(873, 350)
        Me.RadPageViewPage2.Text = "Table2"
        '
        'gv2
        '
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        Me.gv2.Name = "gv2"
        Me.gv2.Size = New System.Drawing.Size(873, 350)
        Me.gv2.TabIndex = 0
        Me.gv2.Text = "gv2"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gv3)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(49.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(873, 350)
        Me.RadPageViewPage3.Text = "Table3"
        '
        'gv3
        '
        Me.gv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv3.Location = New System.Drawing.Point(0, 0)
        Me.gv3.Name = "gv3"
        Me.gv3.Size = New System.Drawing.Size(873, 350)
        Me.gv3.TabIndex = 0
        Me.gv3.Text = "gv3"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gv4)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(49.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(873, 350)
        Me.RadPageViewPage4.Text = "Table4"
        '
        'gv4
        '
        Me.gv4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv4.Location = New System.Drawing.Point(0, 0)
        Me.gv4.Name = "gv4"
        Me.gv4.Size = New System.Drawing.Size(873, 350)
        Me.gv4.TabIndex = 0
        Me.gv4.Text = "gv4"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gv5)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(49.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(873, 350)
        Me.RadPageViewPage5.Text = "Table5"
        '
        'gv5
        '
        Me.gv5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv5.Location = New System.Drawing.Point(0, 0)
        Me.gv5.Name = "gv5"
        Me.gv5.Size = New System.Drawing.Size(873, 350)
        Me.gv5.TabIndex = 0
        Me.gv5.Text = "gv5"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(817, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'FrmProgramMappingDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 442)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProgramMappingDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Mapping Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gv5.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents gv3 As common.UserControls.MyRadGridView
    Friend WithEvents gv4 As common.UserControls.MyRadGridView
    Friend WithEvents gv5 As common.UserControls.MyRadGridView
End Class

