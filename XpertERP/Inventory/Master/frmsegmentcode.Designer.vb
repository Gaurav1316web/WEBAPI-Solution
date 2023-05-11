<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmsegmentcode
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.AllCOl_Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.SetCol_export = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdgdvsegmentcode = New common.UserControls.MyRadGridView()
        Me.rdlblsegmentcode = New common.Controls.MyLabel()
        Me.rddrplstsegmentcode = New common.Controls.MyComboBox()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rdmenusetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdgdvsegmentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgdvsegmentcode.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblsegmentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rddrplstsegmentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile, Me.rdmenusetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(522, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "File"
        Me.rdmenufile.AccessibleName = "File"
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "Import"
        Me.rdmenuimport.AccessibleName = "Import"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "Set Export Criteria"
        Me.rdmenuexport.AccessibleName = "Set Export Criteria"
        Me.rdmenuexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.AllCOl_Export, Me.SetCol_export})
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Set Export Criteria"
        '
        'AllCOl_Export
        '
        Me.AllCOl_Export.AccessibleDescription = "All"
        Me.AllCOl_Export.AccessibleName = "All"
        Me.AllCOl_Export.Name = "AllCOl_Export"
        Me.AllCOl_Export.Text = "All"
        '
        'SetCol_export
        '
        Me.SetCol_export.AccessibleDescription = "Set Criteria"
        Me.SetCol_export.AccessibleName = "Set Criteria"
        Me.SetCol_export.Name = "SetCol_export"
        Me.SetCol_export.Text = "Set Criteria"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "Exit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdgdvsegmentcode)
        Me.RadGroupBox1.Controls.Add(Me.rdlblsegmentcode)
        Me.RadGroupBox1.Controls.Add(Me.rddrplstsegmentcode)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(522, 305)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.ThemeName = "ControlDefault"
        '
        'rdgdvsegmentcode
        '
        Me.rdgdvsegmentcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdgdvsegmentcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdgdvsegmentcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdgdvsegmentcode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rdgdvsegmentcode.ForeColor = System.Drawing.Color.Black
        Me.rdgdvsegmentcode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdgdvsegmentcode.Location = New System.Drawing.Point(13, 47)
        '
        'rdgdvsegmentcode
        '
        Me.rdgdvsegmentcode.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.rdgdvsegmentcode.MasterTemplate.EnableFiltering = True
        Me.rdgdvsegmentcode.MasterTemplate.ShowHeaderCellButtons = True
        Me.rdgdvsegmentcode.Name = "rdgdvsegmentcode"
        Me.rdgdvsegmentcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdgdvsegmentcode.ShowGroupPanel = False
        Me.rdgdvsegmentcode.ShowHeaderCellButtons = True
        Me.rdgdvsegmentcode.Size = New System.Drawing.Size(496, 245)
        Me.rdgdvsegmentcode.TabIndex = 2
        Me.rdgdvsegmentcode.TabStop = False
        Me.rdgdvsegmentcode.Text = "RadGridView1"
        '
        'rdlblsegmentcode
        '
        Me.rdlblsegmentcode.FieldName = Nothing
        Me.rdlblsegmentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblsegmentcode.Location = New System.Drawing.Point(13, 23)
        Me.rdlblsegmentcode.Name = "rdlblsegmentcode"
        Me.rdlblsegmentcode.Size = New System.Drawing.Size(88, 16)
        Me.rdlblsegmentcode.TabIndex = 1
        Me.rdlblsegmentcode.Text = "Segment Name"
        '
        'rddrplstsegmentcode
        '
        Me.rddrplstsegmentcode.CalculationExpression = Nothing
        Me.rddrplstsegmentcode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.rddrplstsegmentcode.FieldCode = Nothing
        Me.rddrplstsegmentcode.FieldDesc = Nothing
        Me.rddrplstsegmentcode.FieldMaxLength = 0
        Me.rddrplstsegmentcode.FieldName = Nothing
        Me.rddrplstsegmentcode.isCalculatedField = False
        Me.rddrplstsegmentcode.IsSourceFromTable = False
        Me.rddrplstsegmentcode.IsSourceFromValueList = False
        Me.rddrplstsegmentcode.IsUnique = False
        Me.rddrplstsegmentcode.Location = New System.Drawing.Point(103, 21)
        Me.rddrplstsegmentcode.MendatroryField = False
        Me.rddrplstsegmentcode.MyLinkLable1 = Me.rdlblsegmentcode
        Me.rddrplstsegmentcode.MyLinkLable2 = Nothing
        Me.rddrplstsegmentcode.Name = "rddrplstsegmentcode"
        Me.rddrplstsegmentcode.ReferenceFieldDesc = Nothing
        Me.rddrplstsegmentcode.ReferenceFieldName = Nothing
        Me.rddrplstsegmentcode.ReferenceTableName = Nothing
        Me.rddrplstsegmentcode.Size = New System.Drawing.Size(405, 20)
        Me.rddrplstsegmentcode.TabIndex = 0
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(444, 7)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(82, 7)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(12, 7)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(522, 340)
        Me.SplitContainer1.SplitterDistance = 305
        Me.SplitContainer1.TabIndex = 1
        '
        'rdmenusetting
        '
        Me.rdmenusetting.AccessibleDescription = "Setting"
        Me.rdmenusetting.AccessibleName = "Setting"
        Me.rdmenusetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiSaveLayout, Me.rmiDeleteLayout})
        Me.rdmenusetting.Name = "rdmenusetting"
        Me.rdmenusetting.Text = "Setting"
        '
        'rmiSaveLayout
        '
        Me.rmiSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmiSaveLayout.AccessibleName = "Save Layout"
        Me.rmiSaveLayout.Name = "rmiSaveLayout"
        Me.rmiSaveLayout.Text = "Save Layout"
        '
        'rmiDeleteLayout
        '
        Me.rmiDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmiDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmiDeleteLayout.Name = "rmiDeleteLayout"
        Me.rmiDeleteLayout.Text = "Delete Layout"
        '
        'Frmsegmentcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 360)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "Frmsegmentcode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Segment Code"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdgdvsegmentcode.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgdvsegmentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblsegmentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rddrplstsegmentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rddrplstsegmentcode As common.Controls.MyComboBox
    Friend WithEvents rdgdvsegmentcode As common.UserControls.MyRadGridView
    Friend WithEvents AllCOl_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SetCol_export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdlblsegmentcode As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenusetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

