<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransactionCancelScreen
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.BtnSelectAll = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.BtnShow = New Telerik.WinControls.UI.RadButton()
        Me.CmbModule = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MyComboBox1 = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.BtnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.BtnSelectAll)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 379)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(591, 34)
        Me.RadGroupBox1.TabIndex = 1
        '
        'BtnSelectAll
        '
        Me.BtnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectAll.Location = New System.Drawing.Point(81, 6)
        Me.BtnSelectAll.Name = "BtnSelectAll"
        Me.BtnSelectAll.Size = New System.Drawing.Size(69, 22)
        Me.BtnSelectAll.TabIndex = 2
        Me.BtnSelectAll.Text = "Select All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(507, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyComboBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnShow)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CmbModule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MasterTemplate)
        Me.SplitContainer1.Size = New System.Drawing.Size(591, 359)
        Me.SplitContainer1.SplitterDistance = 41
        Me.SplitContainer1.TabIndex = 0
        '
        'BtnShow
        '
        Me.BtnShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.Location = New System.Drawing.Point(440, 11)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(69, 22)
        Me.BtnShow.TabIndex = 54
        Me.BtnShow.Text = "Show"
        '
        'CmbModule
        '
        Me.CmbModule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbModule.Location = New System.Drawing.Point(64, 12)
        Me.CmbModule.MendatroryField = True
        Me.CmbModule.MyLinkLable1 = Nothing
        Me.CmbModule.MyLinkLable2 = Nothing
        Me.CmbModule.Name = "CmbModule"
        Me.CmbModule.Size = New System.Drawing.Size(153, 20)
        Me.CmbModule.TabIndex = 52
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(17, 14)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel18.TabIndex = 53
        Me.MyLabel18.Text = "Module"
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(0, 0)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.MasterTemplate.MasterTemplate.AllowColumnReorder = False
        Me.MasterTemplate.MasterTemplate.AllowDeleteRow = False
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.EnableSorting = False
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.Size = New System.Drawing.Size(591, 314)
        Me.MasterTemplate.TabIndex = 0
        Me.MasterTemplate.TabStop = False
        Me.MasterTemplate.Text = "RadGridView1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(591, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MyComboBox1
        '
        Me.MyComboBox1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox1.Location = New System.Drawing.Point(315, 12)
        Me.MyComboBox1.MendatroryField = True
        Me.MyComboBox1.MyLinkLable1 = Nothing
        Me.MyComboBox1.MyLinkLable2 = Nothing
        Me.MyComboBox1.Name = "MyComboBox1"
        Me.MyComboBox1.Size = New System.Drawing.Size(119, 20)
        Me.MyComboBox1.TabIndex = 55
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(234, 14)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel1.TabIndex = 56
        Me.MyLabel1.Text = "Screen Name"
        '
        'FrmTransactionCancelScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 413)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.MinimumSize = New System.Drawing.Size(599, 443)
        Me.Name = "FrmTransactionCancelScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cancel Transaction"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.BtnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents CmbModule As common.Controls.MyComboBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents BtnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents BtnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyComboBox1 As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

