Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmchannelCategory
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
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.rdgrpbxchannelcategory = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndID = New common.UserControls.txtNavigator()
        Me.rdlblchanelcategoryid = New common.Controls.MyLabel()
        Me.rdtxtcategoryname1 = New common.Controls.MyTextBox()
        Me.rdlblcategoryname = New common.Controls.MyLabel()
        Me.rdreset = New Telerik.WinControls.UI.RadButton()
        Me.rdtxtcategoryname = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxchannelcategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxchannelcategory.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdlblchanelcategoryid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtcategoryname1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblcategoryname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtcategoryname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(15, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(72, 19)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(92, 5)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(74, 19)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(325, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(71, 19)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(411, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "FILE"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'rdgrpbxchannelcategory
        '
        Me.rdgrpbxchannelcategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxchannelcategory.Controls.Add(Me.RadGroupBox1)
        Me.rdgrpbxchannelcategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxchannelcategory.HeaderText = ""
        Me.rdgrpbxchannelcategory.Location = New System.Drawing.Point(15, 17)
        Me.rdgrpbxchannelcategory.Name = "rdgrpbxchannelcategory"
        Me.rdgrpbxchannelcategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxchannelcategory.Size = New System.Drawing.Size(381, 127)
        Me.rdgrpbxchannelcategory.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndID)
        Me.RadGroupBox1.Controls.Add(Me.rdtxtcategoryname1)
        Me.RadGroupBox1.Controls.Add(Me.rdreset)
        Me.RadGroupBox1.Controls.Add(Me.rdlblcategoryname)
        Me.RadGroupBox1.Controls.Add(Me.rdlblchanelcategoryid)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 23)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(356, 89)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndID
        '
        Me.fndID.FieldName = Nothing
        Me.fndID.Location = New System.Drawing.Point(112, 17)
        Me.fndID.MendatroryField = True
        Me.fndID.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndID.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndID.MyLinkLable1 = Me.rdlblchanelcategoryid
        Me.fndID.MyLinkLable2 = Nothing
        Me.fndID.MyMaxLength = 30
        Me.fndID.MyReadOnly = False
        Me.fndID.Name = "fndID"
        Me.fndID.Size = New System.Drawing.Size(202, 21)
        Me.fndID.TabIndex = 0
        Me.fndID.Value = ""
        '
        'rdlblchanelcategoryid
        '
        Me.rdlblchanelcategoryid.FieldName = Nothing
        Me.rdlblchanelcategoryid.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblchanelcategoryid.Location = New System.Drawing.Point(25, 20)
        Me.rdlblchanelcategoryid.Name = "rdlblchanelcategoryid"
        Me.rdlblchanelcategoryid.Size = New System.Drawing.Size(82, 16)
        Me.rdlblchanelcategoryid.TabIndex = 2
        Me.rdlblchanelcategoryid.Text = "Category Code"
        '
        'rdtxtcategoryname1
        '
        Me.rdtxtcategoryname1.CalculationExpression = Nothing
        Me.rdtxtcategoryname1.FieldCode = Nothing
        Me.rdtxtcategoryname1.FieldDesc = Nothing
        Me.rdtxtcategoryname1.FieldMaxLength = 0
        Me.rdtxtcategoryname1.FieldName = Nothing
        Me.rdtxtcategoryname1.isCalculatedField = False
        Me.rdtxtcategoryname1.IsSourceFromTable = False
        Me.rdtxtcategoryname1.IsSourceFromValueList = False
        Me.rdtxtcategoryname1.IsUnique = False
        Me.rdtxtcategoryname1.Location = New System.Drawing.Point(112, 48)
        Me.rdtxtcategoryname1.MaxLength = 50
        Me.rdtxtcategoryname1.MendatroryField = False
        Me.rdtxtcategoryname1.MyLinkLable1 = Me.rdlblcategoryname
        Me.rdtxtcategoryname1.MyLinkLable2 = Nothing
        Me.rdtxtcategoryname1.Name = "rdtxtcategoryname1"
        Me.rdtxtcategoryname1.ReferenceFieldDesc = Nothing
        Me.rdtxtcategoryname1.ReferenceFieldName = Nothing
        Me.rdtxtcategoryname1.ReferenceTableName = Nothing
        Me.rdtxtcategoryname1.Size = New System.Drawing.Size(203, 20)
        Me.rdtxtcategoryname1.TabIndex = 2
        '
        'rdlblcategoryname
        '
        Me.rdlblcategoryname.FieldName = Nothing
        Me.rdlblcategoryname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblcategoryname.Location = New System.Drawing.Point(25, 48)
        Me.rdlblcategoryname.Name = "rdlblcategoryname"
        Me.rdlblcategoryname.Size = New System.Drawing.Size(85, 16)
        Me.rdlblcategoryname.TabIndex = 3
        Me.rdlblcategoryname.Text = "Category Name"
        '
        'rdreset
        '
        Me.rdreset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.rdreset.Location = New System.Drawing.Point(318, 18)
        Me.rdreset.Name = "rdreset"
        Me.rdreset.Size = New System.Drawing.Size(15, 18)
        Me.rdreset.TabIndex = 1
        '
        'rdtxtcategoryname
        '
        Me.rdtxtcategoryname.CalculationExpression = Nothing
        Me.rdtxtcategoryname.FieldCode = Nothing
        Me.rdtxtcategoryname.FieldDesc = Nothing
        Me.rdtxtcategoryname.FieldMaxLength = 0
        Me.rdtxtcategoryname.FieldName = Nothing
        Me.rdtxtcategoryname.isCalculatedField = False
        Me.rdtxtcategoryname.IsSourceFromTable = False
        Me.rdtxtcategoryname.IsSourceFromValueList = False
        Me.rdtxtcategoryname.IsUnique = False
        Me.rdtxtcategoryname.Location = New System.Drawing.Point(116, 48)
        Me.rdtxtcategoryname.MendatroryField = False
        Me.rdtxtcategoryname.MyLinkLable1 = Nothing
        Me.rdtxtcategoryname.MyLinkLable2 = Nothing
        Me.rdtxtcategoryname.Name = "rdtxtcategoryname"
        Me.rdtxtcategoryname.ReferenceFieldDesc = Nothing
        Me.rdtxtcategoryname.ReferenceFieldName = Nothing
        Me.rdtxtcategoryname.ReferenceTableName = Nothing
        Me.rdtxtcategoryname.Size = New System.Drawing.Size(196, 20)
        Me.rdtxtcategoryname.TabIndex = 5
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgrpbxchannelcategory)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(411, 183)
        Me.SplitContainer1.SplitterDistance = 151
        Me.SplitContainer1.TabIndex = 1
        '
        'frmchannelCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 203)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmchannelCategory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Channel Category"
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxchannelcategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxchannelcategory.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdlblchanelcategoryid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtcategoryname1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblcategoryname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtcategoryname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents rdgrpbxchannelcategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdtxtcategoryname As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtcategoryname1 As common.Controls.MyTextBox
    Friend WithEvents rdlblchanelcategoryid As common.Controls.MyLabel
    Friend WithEvents rdlblcategoryname As common.Controls.MyLabel
    Friend WithEvents fndID As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

