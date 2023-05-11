Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetServiceMaster
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
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lblassetdesc = New common.Controls.MyLabel()
        Me.txtitemcode = New common.UserControls.txtFinder()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblCategorydesc = New common.Controls.MyLabel()
        Me.lblcatstr = New common.Controls.MyLabel()
        Me.txtcatcode = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txttagno = New common.Controls.MyTextBox()
        Me.lbllevel1 = New common.Controls.MyLabel()
        Me.lbllevel2 = New common.Controls.MyLabel()
        Me.lbllevel3 = New common.Controls.MyLabel()
        Me.lbllevel4 = New common.Controls.MyLabel()
        Me.lbllevel5 = New common.Controls.MyLabel()
        Me.txtlev1 = New common.Controls.MyTextBox()
        Me.txtdesc1 = New common.Controls.MyLabel()
        Me.txtlev2 = New common.Controls.MyTextBox()
        Me.txtdesc2 = New common.Controls.MyLabel()
        Me.txtlev3 = New common.Controls.MyTextBox()
        Me.txtdesc3 = New common.Controls.MyLabel()
        Me.txtlev4 = New common.Controls.MyTextBox()
        Me.txtdesc4 = New common.Controls.MyLabel()
        Me.txtlev5 = New common.Controls.MyTextBox()
        Me.txtdesc5 = New common.Controls.MyLabel()
        Me.txtsno = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.comlevel1 = New common.Controls.MyComboBox()
        Me.comlevel2 = New common.Controls.MyComboBox()
        Me.comlevel3 = New common.Controls.MyComboBox()
        Me.comlevel4 = New common.Controls.MyComboBox()
        Me.comlevel5 = New common.Controls.MyComboBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblassetdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategorydesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcatstr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcatcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttagno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllevel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllevel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllevel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllevel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllevel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlev1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlev2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlev3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlev4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlev5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comlevel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comlevel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comlevel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comlevel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comlevel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(556, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnclose.Size = New System.Drawing.Size(70, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(70, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(4, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lblassetdesc
        '
        Me.lblassetdesc.AutoSize = False
        Me.lblassetdesc.BorderVisible = True
        Me.lblassetdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblassetdesc.Location = New System.Drawing.Point(313, 12)
        Me.lblassetdesc.Name = "lblassetdesc"
        Me.lblassetdesc.Size = New System.Drawing.Size(272, 18)
        Me.lblassetdesc.TabIndex = 118
        Me.lblassetdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblassetdesc.TextWrap = False
        '
        'txtitemcode
        '
        Me.txtitemcode.Location = New System.Drawing.Point(147, 11)
        Me.txtitemcode.MendatroryField = True
        Me.txtitemcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitemcode.MyLinkLable2 = Me.lblassetdesc
        Me.txtitemcode.MyReadOnly = True
        Me.txtitemcode.MyShowMasterFormButton = False
        Me.txtitemcode.Name = "txtitemcode"
        Me.txtitemcode.Size = New System.Drawing.Size(160, 19)
        Me.txtitemcode.TabIndex = 1
        Me.txtitemcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(585, 12)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 0
        '
        'lblCategorydesc
        '
        Me.lblCategorydesc.AutoSize = False
        Me.lblCategorydesc.BorderVisible = True
        Me.lblCategorydesc.Location = New System.Drawing.Point(313, 329)
        Me.lblCategorydesc.Name = "lblCategorydesc"
        Me.lblCategorydesc.Size = New System.Drawing.Size(287, 18)
        Me.lblCategorydesc.TabIndex = 122
        Me.lblCategorydesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCategorydesc.Visible = False
        '
        'lblcatstr
        '
        Me.lblcatstr.Location = New System.Drawing.Point(12, 328)
        Me.lblcatstr.Name = "lblcatstr"
        Me.lblcatstr.Size = New System.Drawing.Size(129, 18)
        Me.lblcatstr.TabIndex = 121
        Me.lblcatstr.Text = "Category Structure Code"
        Me.lblcatstr.Visible = False
        '
        'txtcatcode
        '
        Me.txtcatcode.Location = New System.Drawing.Point(147, 327)
        Me.txtcatcode.MendatroryField = False
        Me.txtcatcode.MyLinkLable1 = Me.lblcatstr
        Me.txtcatcode.MyLinkLable2 = Me.lblCategorydesc
        Me.txtcatcode.Name = "txtcatcode"
        Me.txtcatcode.ReadOnly = True
        Me.txtcatcode.Size = New System.Drawing.Size(160, 20)
        Me.txtcatcode.TabIndex = 14
        Me.txtcatcode.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 122
        Me.MyLabel1.Text = "Tag No."
        '
        'txttagno
        '
        Me.txttagno.Location = New System.Drawing.Point(147, 59)
        Me.txttagno.MendatroryField = True
        Me.txttagno.MyLinkLable1 = Me.MyLabel1
        Me.txttagno.MyLinkLable2 = Nothing
        Me.txttagno.Name = "txttagno"
        Me.txttagno.Size = New System.Drawing.Size(160, 20)
        Me.txttagno.TabIndex = 3
        '
        'lbllevel1
        '
        Me.lbllevel1.Location = New System.Drawing.Point(12, 84)
        Me.lbllevel1.Name = "lbllevel1"
        Me.lbllevel1.Size = New System.Drawing.Size(86, 18)
        Me.lbllevel1.TabIndex = 125
        Me.lbllevel1.Text = "Category Level1"
        Me.lbllevel1.Visible = False
        '
        'lbllevel2
        '
        Me.lbllevel2.Location = New System.Drawing.Point(12, 134)
        Me.lbllevel2.Name = "lbllevel2"
        Me.lbllevel2.Size = New System.Drawing.Size(86, 18)
        Me.lbllevel2.TabIndex = 126
        Me.lbllevel2.Text = "Category Level2"
        Me.lbllevel2.Visible = False
        '
        'lbllevel3
        '
        Me.lbllevel3.Location = New System.Drawing.Point(12, 182)
        Me.lbllevel3.Name = "lbllevel3"
        Me.lbllevel3.Size = New System.Drawing.Size(86, 18)
        Me.lbllevel3.TabIndex = 127
        Me.lbllevel3.Text = "Category Level3"
        Me.lbllevel3.Visible = False
        '
        'lbllevel4
        '
        Me.lbllevel4.Location = New System.Drawing.Point(12, 231)
        Me.lbllevel4.Name = "lbllevel4"
        Me.lbllevel4.Size = New System.Drawing.Size(86, 18)
        Me.lbllevel4.TabIndex = 128
        Me.lbllevel4.Text = "Category Level4"
        Me.lbllevel4.Visible = False
        '
        'lbllevel5
        '
        Me.lbllevel5.Location = New System.Drawing.Point(12, 279)
        Me.lbllevel5.Name = "lbllevel5"
        Me.lbllevel5.Size = New System.Drawing.Size(86, 18)
        Me.lbllevel5.TabIndex = 129
        Me.lbllevel5.Text = "Category Level5"
        Me.lbllevel5.Visible = False
        '
        'txtlev1
        '
        Me.txtlev1.Location = New System.Drawing.Point(147, 83)
        Me.txtlev1.MendatroryField = False
        Me.txtlev1.MyLinkLable1 = Me.lbllevel1
        Me.txtlev1.MyLinkLable2 = Me.txtdesc1
        Me.txtlev1.Name = "txtlev1"
        Me.txtlev1.ReadOnly = True
        Me.txtlev1.Size = New System.Drawing.Size(160, 20)
        Me.txtlev1.TabIndex = 4
        Me.txtlev1.Visible = False
        '
        'txtdesc1
        '
        Me.txtdesc1.AutoSize = False
        Me.txtdesc1.BorderVisible = True
        Me.txtdesc1.Location = New System.Drawing.Point(313, 85)
        Me.txtdesc1.Name = "txtdesc1"
        Me.txtdesc1.Size = New System.Drawing.Size(287, 18)
        Me.txtdesc1.TabIndex = 130
        Me.txtdesc1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtdesc1.Visible = False
        '
        'txtlev2
        '
        Me.txtlev2.Location = New System.Drawing.Point(147, 133)
        Me.txtlev2.MendatroryField = False
        Me.txtlev2.MyLinkLable1 = Me.lbllevel2
        Me.txtlev2.MyLinkLable2 = Me.txtdesc2
        Me.txtlev2.Name = "txtlev2"
        Me.txtlev2.ReadOnly = True
        Me.txtlev2.Size = New System.Drawing.Size(160, 20)
        Me.txtlev2.TabIndex = 6
        Me.txtlev2.Visible = False
        '
        'txtdesc2
        '
        Me.txtdesc2.AutoSize = False
        Me.txtdesc2.BorderVisible = True
        Me.txtdesc2.Location = New System.Drawing.Point(313, 135)
        Me.txtdesc2.Name = "txtdesc2"
        Me.txtdesc2.Size = New System.Drawing.Size(287, 18)
        Me.txtdesc2.TabIndex = 132
        Me.txtdesc2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtdesc2.Visible = False
        '
        'txtlev3
        '
        Me.txtlev3.Location = New System.Drawing.Point(147, 181)
        Me.txtlev3.MendatroryField = False
        Me.txtlev3.MyLinkLable1 = Me.lbllevel3
        Me.txtlev3.MyLinkLable2 = Me.txtdesc3
        Me.txtlev3.Name = "txtlev3"
        Me.txtlev3.ReadOnly = True
        Me.txtlev3.Size = New System.Drawing.Size(160, 20)
        Me.txtlev3.TabIndex = 8
        Me.txtlev3.Visible = False
        '
        'txtdesc3
        '
        Me.txtdesc3.AutoSize = False
        Me.txtdesc3.BorderVisible = True
        Me.txtdesc3.Location = New System.Drawing.Point(313, 183)
        Me.txtdesc3.Name = "txtdesc3"
        Me.txtdesc3.Size = New System.Drawing.Size(287, 18)
        Me.txtdesc3.TabIndex = 124
        Me.txtdesc3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtdesc3.Visible = False
        '
        'txtlev4
        '
        Me.txtlev4.Location = New System.Drawing.Point(147, 230)
        Me.txtlev4.MendatroryField = False
        Me.txtlev4.MyLinkLable1 = Me.lbllevel4
        Me.txtlev4.MyLinkLable2 = Me.txtdesc4
        Me.txtlev4.Name = "txtlev4"
        Me.txtlev4.ReadOnly = True
        Me.txtlev4.Size = New System.Drawing.Size(160, 20)
        Me.txtlev4.TabIndex = 10
        Me.txtlev4.Visible = False
        '
        'txtdesc4
        '
        Me.txtdesc4.AutoSize = False
        Me.txtdesc4.BorderVisible = True
        Me.txtdesc4.Location = New System.Drawing.Point(313, 232)
        Me.txtdesc4.Name = "txtdesc4"
        Me.txtdesc4.Size = New System.Drawing.Size(287, 18)
        Me.txtdesc4.TabIndex = 124
        Me.txtdesc4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtdesc4.Visible = False
        '
        'txtlev5
        '
        Me.txtlev5.Location = New System.Drawing.Point(147, 278)
        Me.txtlev5.MendatroryField = False
        Me.txtlev5.MyLinkLable1 = Me.lbllevel5
        Me.txtlev5.MyLinkLable2 = Me.txtdesc5
        Me.txtlev5.Name = "txtlev5"
        Me.txtlev5.ReadOnly = True
        Me.txtlev5.Size = New System.Drawing.Size(160, 20)
        Me.txtlev5.TabIndex = 12
        Me.txtlev5.Visible = False
        '
        'txtdesc5
        '
        Me.txtdesc5.AutoSize = False
        Me.txtdesc5.BorderVisible = True
        Me.txtdesc5.Location = New System.Drawing.Point(313, 280)
        Me.txtdesc5.Name = "txtdesc5"
        Me.txtdesc5.Size = New System.Drawing.Size(287, 18)
        Me.txtdesc5.TabIndex = 134
        Me.txtdesc5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtdesc5.Visible = False
        '
        'txtsno
        '
        Me.txtsno.Location = New System.Drawing.Point(147, 35)
        Me.txtsno.MendatroryField = False
        Me.txtsno.MyLinkLable1 = Me.MyLabel2
        Me.txtsno.MyLinkLable2 = Nothing
        Me.txtsno.Name = "txtsno"
        Me.txtsno.ReadOnly = True
        Me.txtsno.Size = New System.Drawing.Size(160, 20)
        Me.txtsno.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(12, 35)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel2.TabIndex = 123
        Me.MyLabel2.Text = "Asset Serial No."
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(645, 20)
        Me.rdmenufile.TabIndex = 136
        Me.rdmenufile.Text = "File"
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdexport, Me.rdimport, Me.rdexit})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'rdexport
        '
        Me.rdexport.AccessibleDescription = "Export"
        Me.rdexport.AccessibleName = "Export"
        Me.rdexport.Name = "rdexport"
        Me.rdexport.Text = "Export"
        '
        'rdimport
        '
        Me.rdimport.AccessibleDescription = "Import"
        Me.rdimport.AccessibleName = "Import"
        Me.rdimport.Name = "rdimport"
        Me.rdimport.Text = "Import"
        '
        'rdexit
        '
        Me.rdexit.AccessibleDescription = "Exit"
        Me.rdexit.AccessibleName = "Exit"
        Me.rdexit.Name = "rdexit"
        Me.rdexit.Text = "Exit"
        '
        'comlevel1
        '
        Me.comlevel1.Location = New System.Drawing.Point(147, 108)
        Me.comlevel1.MendatroryField = False
        Me.comlevel1.MyLinkLable1 = Nothing
        Me.comlevel1.MyLinkLable2 = Nothing
        Me.comlevel1.Name = "comlevel1"
        Me.comlevel1.Size = New System.Drawing.Size(160, 20)
        Me.comlevel1.TabIndex = 5
        Me.comlevel1.Visible = False
        '
        'comlevel2
        '
        Me.comlevel2.Location = New System.Drawing.Point(147, 157)
        Me.comlevel2.MendatroryField = False
        Me.comlevel2.MyLinkLable1 = Nothing
        Me.comlevel2.MyLinkLable2 = Nothing
        Me.comlevel2.Name = "comlevel2"
        Me.comlevel2.Size = New System.Drawing.Size(160, 20)
        Me.comlevel2.TabIndex = 7
        Me.comlevel2.Visible = False
        '
        'comlevel3
        '
        Me.comlevel3.Location = New System.Drawing.Point(147, 205)
        Me.comlevel3.MendatroryField = False
        Me.comlevel3.MyLinkLable1 = Nothing
        Me.comlevel3.MyLinkLable2 = Nothing
        Me.comlevel3.Name = "comlevel3"
        Me.comlevel3.Size = New System.Drawing.Size(160, 20)
        Me.comlevel3.TabIndex = 9
        Me.comlevel3.Visible = False
        '
        'comlevel4
        '
        Me.comlevel4.Location = New System.Drawing.Point(147, 254)
        Me.comlevel4.MendatroryField = False
        Me.comlevel4.MyLinkLable1 = Nothing
        Me.comlevel4.MyLinkLable2 = Nothing
        Me.comlevel4.Name = "comlevel4"
        Me.comlevel4.Size = New System.Drawing.Size(160, 20)
        Me.comlevel4.TabIndex = 11
        Me.comlevel4.Visible = False
        '
        'comlevel5
        '
        Me.comlevel5.Location = New System.Drawing.Point(147, 302)
        Me.comlevel5.MendatroryField = False
        Me.comlevel5.MyLinkLable1 = Nothing
        Me.comlevel5.MyLinkLable2 = Nothing
        Me.comlevel5.Name = "comlevel5"
        Me.comlevel5.Size = New System.Drawing.Size(160, 20)
        Me.comlevel5.TabIndex = 13
        Me.comlevel5.Visible = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(645, 421)
        Me.SplitContainer1.SplitterDistance = 379
        Me.SplitContainer1.TabIndex = 137
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.comlevel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtlev5)
        Me.RadGroupBox1.Controls.Add(Me.comlevel4)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc5)
        Me.RadGroupBox1.Controls.Add(Me.txtsno)
        Me.RadGroupBox1.Controls.Add(Me.lbllevel5)
        Me.RadGroupBox1.Controls.Add(Me.txtcatcode)
        Me.RadGroupBox1.Controls.Add(Me.comlevel3)
        Me.RadGroupBox1.Controls.Add(Me.lblcatstr)
        Me.RadGroupBox1.Controls.Add(Me.lblassetdesc)
        Me.RadGroupBox1.Controls.Add(Me.lblCategorydesc)
        Me.RadGroupBox1.Controls.Add(Me.txtlev4)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.comlevel2)
        Me.RadGroupBox1.Controls.Add(Me.lbllevel4)
        Me.RadGroupBox1.Controls.Add(Me.txtitemcode)
        Me.RadGroupBox1.Controls.Add(Me.txttagno)
        Me.RadGroupBox1.Controls.Add(Me.lbllevel1)
        Me.RadGroupBox1.Controls.Add(Me.comlevel1)
        Me.RadGroupBox1.Controls.Add(Me.txtlev1)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc1)
        Me.RadGroupBox1.Controls.Add(Me.lbllevel2)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc2)
        Me.RadGroupBox1.Controls.Add(Me.txtlev2)
        Me.RadGroupBox1.Controls.Add(Me.lbllevel3)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc3)
        Me.RadGroupBox1.Controls.Add(Me.txtlev3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(614, 368)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(12, 10)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 136
        Me.MyLabel3.Text = "Item Code"
        '
        'FrmAssetServiceMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 441)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmAssetServiceMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmAssetServiceMaster"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblassetdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategorydesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcatstr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcatcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttagno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllevel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllevel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllevel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllevel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllevel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlev1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlev2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlev3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlev4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlev5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comlevel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comlevel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comlevel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comlevel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comlevel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblassetdesc As common.Controls.MyLabel
    Friend WithEvents txtitemcode As common.UserControls.txtFinder
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCategorydesc As common.Controls.MyLabel
    Friend WithEvents lblcatstr As common.Controls.MyLabel
    Friend WithEvents txtcatcode As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txttagno As common.Controls.MyTextBox
    Friend WithEvents lbllevel1 As common.Controls.MyLabel
    Friend WithEvents lbllevel2 As common.Controls.MyLabel
    Friend WithEvents lbllevel3 As common.Controls.MyLabel
    Friend WithEvents lbllevel4 As common.Controls.MyLabel
    Friend WithEvents lbllevel5 As common.Controls.MyLabel
    Friend WithEvents txtdesc1 As common.Controls.MyLabel
    Friend WithEvents txtlev1 As common.Controls.MyTextBox
    Friend WithEvents txtlev2 As common.Controls.MyTextBox
    Friend WithEvents txtdesc2 As common.Controls.MyLabel
    Friend WithEvents txtlev3 As common.Controls.MyTextBox
    Friend WithEvents txtdesc3 As common.Controls.MyLabel
    Friend WithEvents txtlev4 As common.Controls.MyTextBox
    Friend WithEvents txtdesc4 As common.Controls.MyLabel
    Friend WithEvents txtlev5 As common.Controls.MyTextBox
    Friend WithEvents txtdesc5 As common.Controls.MyLabel
    Friend WithEvents txtsno As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents comlevel1 As common.Controls.MyComboBox
    Friend WithEvents comlevel2 As common.Controls.MyComboBox
    Friend WithEvents comlevel3 As common.Controls.MyComboBox
    Friend WithEvents comlevel4 As common.Controls.MyComboBox
    Friend WithEvents comlevel5 As common.Controls.MyComboBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

