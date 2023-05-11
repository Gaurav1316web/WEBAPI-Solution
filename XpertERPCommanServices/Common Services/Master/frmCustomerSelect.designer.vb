<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerSelect
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Size = New System.Drawing.Size(544, 412)
        Me.SplitContainer1.SplitterDistance = 375
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer3)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = "Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(544, 375)
        Me.RadGroupBox3.TabIndex = 4
        Me.RadGroupBox3.Text = "Customer"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvCategory)
        Me.SplitContainer3.Size = New System.Drawing.Size(524, 345)
        Me.SplitContainer3.SplitterDistance = 26
        Me.SplitContainer3.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.SplitContainer2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(524, 20)
        Me.Panel2.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategoryAll)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton7)
        Me.SplitContainer2.Size = New System.Drawing.Size(524, 20)
        Me.SplitContainer2.SplitterDistance = 251
        Me.SplitContainer2.TabIndex = 2
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(124, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 2
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(85, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 1
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadButton6
        '
        Me.RadButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton6.Location = New System.Drawing.Point(95, 3)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(86, 15)
        Me.RadButton6.TabIndex = 5
        Me.RadButton6.Text = "Unselect All"
        '
        'RadButton7
        '
        Me.RadButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton7.Location = New System.Drawing.Point(6, 3)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(86, 15)
        Me.RadButton7.TabIndex = 4
        Me.RadButton7.Text = "Select All"
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(0, 0)
        '
        'gvCategory
        '
        Me.gvCategory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.ShowHeaderCellButtons = True
        Me.gvCategory.Size = New System.Drawing.Size(524, 315)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.Text = "RadGridView1"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(202, 4)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(67, 24)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "OK"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(275, 4)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 24)
        Me.RadButton2.TabIndex = 2
        Me.RadButton2.Text = "Cancel"
        '
        'frmCustomerSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 412)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCustomerSelect"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select ..."
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton7 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
End Class

