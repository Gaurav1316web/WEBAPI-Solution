<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCSAAccountSet
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.fndrecisvablecontrol = New common.UserControls.txtFinder
        Me.rdlblrecievablescontrol = New common.Controls.MyLabel
        Me.rdtxtrecievablecontrol = New common.Controls.MyTextBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.txtloss = New common.UserControls.txtFinder
        Me.txtloss_name = New common.Controls.MyTextBox
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txtgain = New common.UserControls.txtFinder
        Me.txtgian_name = New common.Controls.MyTextBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.txtconsignmnt = New common.UserControls.txtFinder
        Me.txtcongnmnt_name = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtgsoc = New common.UserControls.txtFinder
        Me.txtgsoc_name = New common.Controls.MyTextBox
        Me.fndaccountsetcode = New common.UserControls.txtNavigator
        Me.rdlblAccountsetcode = New common.Controls.MyLabel
        Me.rdlbldescription = New common.Controls.MyLabel
        Me.rdtxtdescription = New common.Controls.MyTextBox
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrecievablescontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtrecievablecontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtloss_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgian_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcongnmnt_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgsoc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndrecisvablecontrol)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdtxtrecievablecontrol)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlblrecievablescontrol)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtloss)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtloss_name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtgain)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtgian_name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtconsignmnt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcongnmnt_name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtgsoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtgsoc_name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndaccountsetcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlblAccountsetcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlbldescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdtxtdescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(734, 288)
        Me.SplitContainer1.SplitterDistance = 254
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(734, 20)
        Me.RadMenu1.TabIndex = 155
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'fndrecisvablecontrol
        '
        Me.fndrecisvablecontrol.Enabled = False
        Me.fndrecisvablecontrol.Location = New System.Drawing.Point(125, 87)
        Me.fndrecisvablecontrol.MendatroryField = True
        Me.fndrecisvablecontrol.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndrecisvablecontrol.MyLinkLable1 = Me.rdlblrecievablescontrol
        Me.fndrecisvablecontrol.MyLinkLable2 = Nothing
        Me.fndrecisvablecontrol.MyReadOnly = False
        Me.fndrecisvablecontrol.MyShowMasterFormButton = False
        Me.fndrecisvablecontrol.Name = "fndrecisvablecontrol"
        Me.fndrecisvablecontrol.Size = New System.Drawing.Size(143, 19)
        Me.fndrecisvablecontrol.TabIndex = 3
        Me.fndrecisvablecontrol.Value = ""
        '
        'rdlblrecievablescontrol
        '
        Me.rdlblrecievablescontrol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrecievablescontrol.Location = New System.Drawing.Point(12, 88)
        Me.rdlblrecievablescontrol.Name = "rdlblrecievablescontrol"
        Me.rdlblrecievablescontrol.Size = New System.Drawing.Size(80, 16)
        Me.rdlblrecievablescontrol.TabIndex = 154
        Me.rdlblrecievablescontrol.Text = "Debtor Control"
        '
        'rdtxtrecievablecontrol
        '
        Me.rdtxtrecievablecontrol.Location = New System.Drawing.Point(274, 86)
        Me.rdtxtrecievablecontrol.MendatroryField = False
        Me.rdtxtrecievablecontrol.MyLinkLable1 = Me.rdlblrecievablescontrol
        Me.rdtxtrecievablecontrol.MyLinkLable2 = Nothing
        Me.rdtxtrecievablecontrol.Name = "rdtxtrecievablecontrol"
        Me.rdtxtrecievablecontrol.ReadOnly = True
        Me.rdtxtrecievablecontrol.Size = New System.Drawing.Size(339, 20)
        Me.rdtxtrecievablecontrol.TabIndex = 153
        Me.rdtxtrecievablecontrol.TabStop = False
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 180)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel9.TabIndex = 151
        Me.MyLabel9.Text = "Loss"
        '
        'txtloss
        '
        Me.txtloss.Location = New System.Drawing.Point(125, 179)
        Me.txtloss.MendatroryField = True
        Me.txtloss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloss.MyLinkLable1 = Me.MyLabel9
        Me.txtloss.MyLinkLable2 = Nothing
        Me.txtloss.MyReadOnly = False
        Me.txtloss.MyShowMasterFormButton = False
        Me.txtloss.Name = "txtloss"
        Me.txtloss.Size = New System.Drawing.Size(143, 19)
        Me.txtloss.TabIndex = 7
        Me.txtloss.Value = ""
        '
        'txtloss_name
        '
        Me.txtloss_name.Location = New System.Drawing.Point(274, 179)
        Me.txtloss_name.MendatroryField = False
        Me.txtloss_name.MyLinkLable1 = Nothing
        Me.txtloss_name.MyLinkLable2 = Nothing
        Me.txtloss_name.Name = "txtloss_name"
        Me.txtloss_name.ReadOnly = True
        Me.txtloss_name.Size = New System.Drawing.Size(339, 20)
        Me.txtloss_name.TabIndex = 150
        Me.txtloss_name.TabStop = False
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(12, 157)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel8.TabIndex = 149
        Me.MyLabel8.Text = "Gain"
        '
        'txtgain
        '
        Me.txtgain.Location = New System.Drawing.Point(125, 156)
        Me.txtgain.MendatroryField = True
        Me.txtgain.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgain.MyLinkLable1 = Me.MyLabel8
        Me.txtgain.MyLinkLable2 = Nothing
        Me.txtgain.MyReadOnly = False
        Me.txtgain.MyShowMasterFormButton = False
        Me.txtgain.Name = "txtgain"
        Me.txtgain.Size = New System.Drawing.Size(143, 19)
        Me.txtgain.TabIndex = 6
        Me.txtgain.Value = ""
        '
        'txtgian_name
        '
        Me.txtgian_name.Location = New System.Drawing.Point(274, 156)
        Me.txtgian_name.MendatroryField = False
        Me.txtgian_name.MyLinkLable1 = Nothing
        Me.txtgian_name.MyLinkLable2 = Nothing
        Me.txtgian_name.Name = "txtgian_name"
        Me.txtgian_name.ReadOnly = True
        Me.txtgian_name.Size = New System.Drawing.Size(339, 20)
        Me.txtgian_name.TabIndex = 148
        Me.txtgian_name.TabStop = False
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 134)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel7.TabIndex = 147
        Me.MyLabel7.Text = "Consignment"
        '
        'txtconsignmnt
        '
        Me.txtconsignmnt.Location = New System.Drawing.Point(125, 134)
        Me.txtconsignmnt.MendatroryField = True
        Me.txtconsignmnt.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtconsignmnt.MyLinkLable1 = Me.MyLabel7
        Me.txtconsignmnt.MyLinkLable2 = Nothing
        Me.txtconsignmnt.MyReadOnly = False
        Me.txtconsignmnt.MyShowMasterFormButton = False
        Me.txtconsignmnt.Name = "txtconsignmnt"
        Me.txtconsignmnt.Size = New System.Drawing.Size(143, 19)
        Me.txtconsignmnt.TabIndex = 5
        Me.txtconsignmnt.Value = ""
        '
        'txtcongnmnt_name
        '
        Me.txtcongnmnt_name.Location = New System.Drawing.Point(274, 134)
        Me.txtcongnmnt_name.MendatroryField = False
        Me.txtcongnmnt_name.MyLinkLable1 = Nothing
        Me.txtcongnmnt_name.MyLinkLable2 = Nothing
        Me.txtcongnmnt_name.Name = "txtcongnmnt_name"
        Me.txtcongnmnt_name.ReadOnly = True
        Me.txtcongnmnt_name.Size = New System.Drawing.Size(339, 20)
        Me.txtcongnmnt_name.TabIndex = 146
        Me.txtcongnmnt_name.TabStop = False
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(12, 110)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel6.TabIndex = 145
        Me.MyLabel6.Text = "GSOC"
        '
        'txtgsoc
        '
        Me.txtgsoc.Location = New System.Drawing.Point(125, 111)
        Me.txtgsoc.MendatroryField = True
        Me.txtgsoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgsoc.MyLinkLable1 = Me.MyLabel6
        Me.txtgsoc.MyLinkLable2 = Nothing
        Me.txtgsoc.MyReadOnly = False
        Me.txtgsoc.MyShowMasterFormButton = False
        Me.txtgsoc.Name = "txtgsoc"
        Me.txtgsoc.Size = New System.Drawing.Size(143, 19)
        Me.txtgsoc.TabIndex = 4
        Me.txtgsoc.Value = ""
        '
        'txtgsoc_name
        '
        Me.txtgsoc_name.Location = New System.Drawing.Point(274, 111)
        Me.txtgsoc_name.MendatroryField = False
        Me.txtgsoc_name.MyLinkLable1 = Nothing
        Me.txtgsoc_name.MyLinkLable2 = Nothing
        Me.txtgsoc_name.Name = "txtgsoc_name"
        Me.txtgsoc_name.ReadOnly = True
        Me.txtgsoc_name.Size = New System.Drawing.Size(339, 20)
        Me.txtgsoc_name.TabIndex = 144
        Me.txtgsoc_name.TabStop = False
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.Location = New System.Drawing.Point(124, 38)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 32767
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(202, 21)
        Me.fndaccountsetcode.TabIndex = 1
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(12, 40)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(93, 16)
        Me.rdlblAccountsetcode.TabIndex = 13
        Me.rdlblAccountsetcode.Text = "Account set code"
        '
        'rdlbldescription
        '
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(12, 63)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 14
        Me.rdlbldescription.Text = "Description"
        '
        'rdtxtdescription
        '
        Me.rdtxtdescription.Location = New System.Drawing.Point(125, 62)
        Me.rdtxtdescription.MaxLength = 50
        Me.rdtxtdescription.MendatroryField = False
        Me.rdtxtdescription.MyLinkLable1 = Me.rdlbldescription
        Me.rdtxtdescription.MyLinkLable2 = Nothing
        Me.rdtxtdescription.Name = "rdtxtdescription"
        Me.rdtxtdescription.Size = New System.Drawing.Size(488, 20)
        Me.rdtxtdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(328, 39)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(16, 20)
        Me.rdbtnnew.TabIndex = 0
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(10, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(78, 19)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(93, 5)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(76, 19)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.TabStop = False
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(643, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(79, 19)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Export"
        Me.btnexport.AccessibleName = "Export"
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        Me.btnexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnimport
        '
        Me.btnimport.AccessibleDescription = "Import"
        Me.btnimport.AccessibleName = "Import"
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        Me.btnimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmCSAAccountSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 288)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCSAAccountSet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCSAAccountSet"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrecievablescontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtrecievablecontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtloss_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgian_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcongnmnt_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgsoc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents rdtxtdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtloss As common.UserControls.txtFinder
    Friend WithEvents txtloss_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtgain As common.UserControls.txtFinder
    Friend WithEvents txtgian_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtconsignmnt As common.UserControls.txtFinder
    Friend WithEvents txtcongnmnt_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtgsoc As common.UserControls.txtFinder
    Friend WithEvents txtgsoc_name As common.Controls.MyTextBox
    Friend WithEvents fndrecisvablecontrol As common.UserControls.txtFinder
    Friend WithEvents rdlblrecievablescontrol As common.Controls.MyLabel
    Friend WithEvents rdtxtrecievablecontrol As common.Controls.MyTextBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
End Class

