<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptItemLocationDetailsNewVersion
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
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgitem = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkitemSelect = New common.Controls.MyRadioButton
        Me.chkitemAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbglocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chklocationselect = New common.Controls.MyRadioButton
        Me.chklocationall = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgbatch = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkbatchselect = New common.Controls.MyRadioButton
        Me.chkbatchall = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgmrp = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkmrpselect = New common.Controls.MyRadioButton
        Me.chkmrpall = New common.Controls.MyRadioButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkitemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkitemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chklocationselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chklocationall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkbatchselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkbatchall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkmrpselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmrpall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgitem)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.FooterImageIndex = -1
        Me.RadGroupBox5.FooterImageKey = ""
        Me.RadGroupBox5.HeaderImageIndex = -1
        Me.RadGroupBox5.HeaderImageKey = ""
        Me.RadGroupBox5.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox5.HeaderText = "Item"
        Me.RadGroupBox5.Location = New System.Drawing.Point(11, 178)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox5.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(327, 159)
        Me.RadGroupBox5.TabIndex = 120
        Me.RadGroupBox5.Text = "Item"
        '
        'cbgitem
        '
        Me.cbgitem.CheckedValue = Nothing
        Me.cbgitem.DataSource = Nothing
        Me.cbgitem.DisplayMember = "Name"
        Me.cbgitem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgitem.Location = New System.Drawing.Point(10, 40)
        Me.cbgitem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgitem.MyShowHeadrText = False
        Me.cbgitem.Name = "cbgitem"
        Me.cbgitem.Size = New System.Drawing.Size(307, 109)
        Me.cbgitem.TabIndex = 1
        Me.cbgitem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkitemSelect)
        Me.Panel2.Controls.Add(Me.chkitemAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(307, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkitemSelect
        '
        Me.chkitemSelect.Location = New System.Drawing.Point(156, 1)
        Me.chkitemSelect.MyLinkLable1 = Nothing
        Me.chkitemSelect.MyLinkLable2 = Nothing
        Me.chkitemSelect.Name = "chkitemSelect"
        Me.chkitemSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkitemSelect.TabIndex = 1
        Me.chkitemSelect.Text = "Select"
        '
        'chkitemAll
        '
        Me.chkitemAll.Location = New System.Drawing.Point(105, 1)
        Me.chkitemAll.MyLinkLable1 = Nothing
        Me.chkitemAll.MyLinkLable2 = Nothing
        Me.chkitemAll.Name = "chkitemAll"
        Me.chkitemAll.Size = New System.Drawing.Size(45, 18)
        Me.chkitemAll.TabIndex = 0
        Me.chkitemAll.Text = "All"
        Me.chkitemAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbglocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(353, 178)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(327, 159)
        Me.RadGroupBox1.TabIndex = 121
        Me.RadGroupBox1.Text = "Location"
        '
        'cbglocation
        '
        Me.cbglocation.CheckedValue = Nothing
        Me.cbglocation.DataSource = Nothing
        Me.cbglocation.DisplayMember = "Name"
        Me.cbglocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbglocation.Location = New System.Drawing.Point(10, 40)
        Me.cbglocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbglocation.MyShowHeadrText = False
        Me.cbglocation.Name = "cbglocation"
        Me.cbglocation.Size = New System.Drawing.Size(307, 109)
        Me.cbglocation.TabIndex = 1
        Me.cbglocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chklocationselect)
        Me.Panel1.Controls.Add(Me.chklocationall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(307, 20)
        Me.Panel1.TabIndex = 0
        '
        'chklocationselect
        '
        Me.chklocationselect.Location = New System.Drawing.Point(159, 1)
        Me.chklocationselect.MyLinkLable1 = Nothing
        Me.chklocationselect.MyLinkLable2 = Nothing
        Me.chklocationselect.Name = "chklocationselect"
        Me.chklocationselect.Size = New System.Drawing.Size(71, 18)
        Me.chklocationselect.TabIndex = 1
        Me.chklocationselect.Text = "Select"
        '
        'chklocationall
        '
        Me.chklocationall.Location = New System.Drawing.Point(108, 1)
        Me.chklocationall.MyLinkLable1 = Nothing
        Me.chklocationall.MyLinkLable2 = Nothing
        Me.chklocationall.Name = "chklocationall"
        Me.chklocationall.Size = New System.Drawing.Size(45, 18)
        Me.chklocationall.TabIndex = 0
        Me.chklocationall.Text = "All"
        Me.chklocationall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgbatch)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.FooterImageIndex = -1
        Me.RadGroupBox2.FooterImageKey = ""
        Me.RadGroupBox2.HeaderImageIndex = -1
        Me.RadGroupBox2.HeaderImageKey = ""
        Me.RadGroupBox2.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox2.HeaderText = "Batch"
        Me.RadGroupBox2.Location = New System.Drawing.Point(11, 13)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(327, 159)
        Me.RadGroupBox2.TabIndex = 122
        Me.RadGroupBox2.Text = "Batch"
        '
        'cbgbatch
        '
        Me.cbgbatch.CheckedValue = Nothing
        Me.cbgbatch.DataSource = Nothing
        Me.cbgbatch.DisplayMember = "Name"
        Me.cbgbatch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgbatch.Location = New System.Drawing.Point(10, 40)
        Me.cbgbatch.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgbatch.MyShowHeadrText = False
        Me.cbgbatch.Name = "cbgbatch"
        Me.cbgbatch.Size = New System.Drawing.Size(307, 109)
        Me.cbgbatch.TabIndex = 1
        Me.cbgbatch.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkbatchselect)
        Me.Panel3.Controls.Add(Me.chkbatchall)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(307, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkbatchselect
        '
        Me.chkbatchselect.Location = New System.Drawing.Point(154, 1)
        Me.chkbatchselect.MyLinkLable1 = Nothing
        Me.chkbatchselect.MyLinkLable2 = Nothing
        Me.chkbatchselect.Name = "chkbatchselect"
        Me.chkbatchselect.Size = New System.Drawing.Size(71, 18)
        Me.chkbatchselect.TabIndex = 1
        Me.chkbatchselect.Text = "Select"
        '
        'chkbatchall
        '
        Me.chkbatchall.Location = New System.Drawing.Point(103, 1)
        Me.chkbatchall.MyLinkLable1 = Nothing
        Me.chkbatchall.MyLinkLable2 = Nothing
        Me.chkbatchall.Name = "chkbatchall"
        Me.chkbatchall.Size = New System.Drawing.Size(45, 18)
        Me.chkbatchall.TabIndex = 0
        Me.chkbatchall.Text = "All"
        Me.chkbatchall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgmrp)
        Me.RadGroupBox3.Controls.Add(Me.Panel4)
        Me.RadGroupBox3.FooterImageIndex = -1
        Me.RadGroupBox3.FooterImageKey = ""
        Me.RadGroupBox3.HeaderImageIndex = -1
        Me.RadGroupBox3.HeaderImageKey = ""
        Me.RadGroupBox3.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox3.HeaderText = "MRP"
        Me.RadGroupBox3.Location = New System.Drawing.Point(353, 13)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(327, 159)
        Me.RadGroupBox3.TabIndex = 123
        Me.RadGroupBox3.Text = "MRP"
        '
        'cbgmrp
        '
        Me.cbgmrp.CheckedValue = Nothing
        Me.cbgmrp.DataSource = Nothing
        Me.cbgmrp.DisplayMember = "Name"
        Me.cbgmrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgmrp.Location = New System.Drawing.Point(10, 40)
        Me.cbgmrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgmrp.MyShowHeadrText = False
        Me.cbgmrp.Name = "cbgmrp"
        Me.cbgmrp.Size = New System.Drawing.Size(307, 109)
        Me.cbgmrp.TabIndex = 1
        Me.cbgmrp.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkmrpselect)
        Me.Panel4.Controls.Add(Me.chkmrpall)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(307, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkmrpselect
        '
        Me.chkmrpselect.Location = New System.Drawing.Point(161, 1)
        Me.chkmrpselect.MyLinkLable1 = Nothing
        Me.chkmrpselect.MyLinkLable2 = Nothing
        Me.chkmrpselect.Name = "chkmrpselect"
        Me.chkmrpselect.Size = New System.Drawing.Size(71, 18)
        Me.chkmrpselect.TabIndex = 1
        Me.chkmrpselect.Text = "Select"
        '
        'chkmrpall
        '
        Me.chkmrpall.Location = New System.Drawing.Point(110, 1)
        Me.chkmrpall.MyLinkLable1 = Nothing
        Me.chkmrpall.MyLinkLable2 = Nothing
        Me.chkmrpall.Name = "chkmrpall"
        Me.chkmrpall.Size = New System.Drawing.Size(45, 18)
        Me.chkmrpall.TabIndex = 0
        Me.chkmrpall.Text = "All"
        Me.chkmrpall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(12, 3)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(63, 19)
        Me.btnprint.TabIndex = 124
        Me.btnprint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(647, 1)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(63, 21)
        Me.btnclose.TabIndex = 125
        Me.btnclose.Text = "Close"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox4.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox4.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox4.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox4.FooterImageIndex = -1
        Me.RadGroupBox4.FooterImageKey = ""
        Me.RadGroupBox4.HeaderImageIndex = -1
        Me.RadGroupBox4.HeaderImageKey = ""
        Me.RadGroupBox4.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(20, 14)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox4.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(690, 353)
        Me.RadGroupBox4.TabIndex = 126
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(725, 399)
        Me.SplitContainer1.SplitterDistance = 370
        Me.SplitContainer1.TabIndex = 127
        '
        'RptItemLocationDetailsNewVersion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 399)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "RptItemLocationDetailsNewVersion"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Location Details Report"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.chkitemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkitemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.chklocationselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chklocationall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.chkbatchselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkbatchall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.chkmrpselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmrpall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgitem As common.MyCheckBoxGrid
    Friend WithEvents chkitemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkitemAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbglocation As common.MyCheckBoxGrid
    Friend WithEvents chklocationselect As common.Controls.MyRadioButton
    Friend WithEvents chklocationall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgbatch As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkbatchselect As common.Controls.MyRadioButton
    Friend WithEvents chkbatchall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgmrp As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkmrpselect As common.Controls.MyRadioButton
    Friend WithEvents chkmrpall As common.Controls.MyRadioButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

