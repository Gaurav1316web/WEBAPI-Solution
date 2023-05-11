<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmvisimaster
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtTagNo = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtSerialNo = New common.Controls.MyTextBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtModelNo = New common.Controls.MyTextBox
        Me.lblModelNo = New common.Controls.MyLabel
        Me.txtAssetNo = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtVisiSize = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtcustname = New common.Controls.MyTextBox
        Me.fndcustomerid = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.fndid = New common.UserControls.txtNavigator
        Me.rdlblID = New common.Controls.MyLabel
        Me.rdtxtmake = New common.Controls.MyTextBox
        Me.rdlblmake = New common.Controls.MyLabel
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem
        Me.rdgrpbxVisimaster = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtTagNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModelNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModelNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVisiSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtmake, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblmake, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxVisimaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxVisimaster.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtTagNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtSerialNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtModelNo)
        Me.RadGroupBox1.Controls.Add(Me.lblModelNo)
        Me.RadGroupBox1.Controls.Add(Me.txtAssetNo)
        Me.RadGroupBox1.Controls.Add(Me.txtVisiSize)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtcustname)
        Me.RadGroupBox1.Controls.Add(Me.fndcustomerid)
        Me.RadGroupBox1.Controls.Add(Me.fndid)
        Me.RadGroupBox1.Controls.Add(Me.rdtxtmake)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnnew)
        Me.RadGroupBox1.Controls.Add(Me.rdlblmake)
        Me.RadGroupBox1.Controls.Add(Me.rdlblID)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(545, 126)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtTagNo
        '
        Me.txtTagNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTagNo.Location = New System.Drawing.Point(304, 97)
        Me.txtTagNo.MaxLength = 50
        Me.txtTagNo.MendatroryField = False
        Me.txtTagNo.MyLinkLable1 = Me.MyLabel3
        Me.txtTagNo.MyLinkLable2 = Nothing
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.Size = New System.Drawing.Size(144, 18)
        Me.txtTagNo.TabIndex = 20
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(235, 97)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 21
        Me.MyLabel3.Text = "Tag No"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo.Location = New System.Drawing.Point(75, 96)
        Me.txtSerialNo.MaxLength = 50
        Me.txtSerialNo.MendatroryField = False
        Me.txtSerialNo.MyLinkLable1 = Me.MyLabel4
        Me.txtSerialNo.MyLinkLable2 = Nothing
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(137, 18)
        Me.txtSerialNo.TabIndex = 18
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 97)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel4.TabIndex = 19
        Me.MyLabel4.Text = "Serial No"
        '
        'txtModelNo
        '
        Me.txtModelNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModelNo.Location = New System.Drawing.Point(304, 51)
        Me.txtModelNo.MaxLength = 50
        Me.txtModelNo.MendatroryField = False
        Me.txtModelNo.MyLinkLable1 = Me.lblModelNo
        Me.txtModelNo.MyLinkLable2 = Nothing
        Me.txtModelNo.Name = "txtModelNo"
        Me.txtModelNo.Size = New System.Drawing.Size(144, 18)
        Me.txtModelNo.TabIndex = 16
        '
        'lblModelNo
        '
        Me.lblModelNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModelNo.Location = New System.Drawing.Point(235, 52)
        Me.lblModelNo.Name = "lblModelNo"
        Me.lblModelNo.Size = New System.Drawing.Size(55, 16)
        Me.lblModelNo.TabIndex = 17
        Me.lblModelNo.Text = "Model No"
        '
        'txtAssetNo
        '
        Me.txtAssetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetNo.Location = New System.Drawing.Point(75, 51)
        Me.txtAssetNo.MaxLength = 50
        Me.txtAssetNo.MendatroryField = False
        Me.txtAssetNo.MyLinkLable1 = Me.MyLabel1
        Me.txtAssetNo.MyLinkLable2 = Nothing
        Me.txtAssetNo.Name = "txtAssetNo"
        Me.txtAssetNo.ReadOnly = True
        Me.txtAssetNo.Size = New System.Drawing.Size(137, 18)
        Me.txtAssetNo.TabIndex = 5
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 52)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "Asset No"
        '
        'txtVisiSize
        '
        Me.txtVisiSize.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisiSize.Location = New System.Drawing.Point(304, 29)
        Me.txtVisiSize.MaxLength = 12
        Me.txtVisiSize.MendatroryField = False
        Me.txtVisiSize.MyLinkLable1 = Me.MyLabel2
        Me.txtVisiSize.MyLinkLable2 = Nothing
        Me.txtVisiSize.Name = "txtVisiSize"
        Me.txtVisiSize.Size = New System.Drawing.Size(144, 18)
        Me.txtVisiSize.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(235, 30)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel2.TabIndex = 15
        Me.MyLabel2.Text = "Visi Size"
        '
        'txtcustname
        '
        Me.txtcustname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustname.Location = New System.Drawing.Point(217, 74)
        Me.txtcustname.MaxLength = 12
        Me.txtcustname.MendatroryField = False
        Me.txtcustname.MyLinkLable1 = Nothing
        Me.txtcustname.MyLinkLable2 = Nothing
        Me.txtcustname.Name = "txtcustname"
        Me.txtcustname.ReadOnly = True
        Me.txtcustname.Size = New System.Drawing.Size(317, 18)
        Me.txtcustname.TabIndex = 8
        '
        'fndcustomerid
        '
        Me.fndcustomerid.Enabled = False
        Me.fndcustomerid.Location = New System.Drawing.Point(75, 74)
        Me.fndcustomerid.MendatroryField = False
        Me.fndcustomerid.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcustomerid.MyLinkLable1 = Me.RadLabel1
        Me.fndcustomerid.MyLinkLable2 = Nothing
        Me.fndcustomerid.MyReadOnly = False
        Me.fndcustomerid.Name = "fndcustomerid"
        Me.fndcustomerid.Size = New System.Drawing.Size(137, 19)
        Me.fndcustomerid.TabIndex = 7
        Me.fndcustomerid.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 75)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel1.TabIndex = 12
        Me.RadLabel1.Text = "Customer"
        '
        'fndid
        '
        Me.fndid.Location = New System.Drawing.Point(76, 5)
        Me.fndid.MendatroryField = True
        Me.fndid.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndid.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndid.MyLinkLable1 = Me.rdlblID
        Me.fndid.MyLinkLable2 = Nothing
        Me.fndid.MyMaxLength = 32767
        Me.fndid.MyReadOnly = False
        Me.fndid.Name = "fndid"
        Me.fndid.Size = New System.Drawing.Size(190, 21)
        Me.fndid.TabIndex = 0
        Me.fndid.Value = ""
        '
        'rdlblID
        '
        Me.rdlblID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblID.Location = New System.Drawing.Point(6, 9)
        Me.rdlblID.Name = "rdlblID"
        Me.rdlblID.Size = New System.Drawing.Size(39, 16)
        Me.rdlblID.TabIndex = 9
        Me.rdlblID.Text = "Visi ID"
        '
        'rdtxtmake
        '
        Me.rdtxtmake.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdtxtmake.Location = New System.Drawing.Point(76, 29)
        Me.rdtxtmake.MaxLength = 50
        Me.rdtxtmake.MendatroryField = False
        Me.rdtxtmake.MyLinkLable1 = Me.rdlblmake
        Me.rdtxtmake.MyLinkLable2 = Nothing
        Me.rdtxtmake.Name = "rdtxtmake"
        Me.rdtxtmake.Size = New System.Drawing.Size(136, 18)
        Me.rdtxtmake.TabIndex = 3
        '
        'rdlblmake
        '
        Me.rdlblmake.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblmake.Location = New System.Drawing.Point(6, 31)
        Me.rdlblmake.Name = "rdlblmake"
        Me.rdlblmake.Size = New System.Drawing.Size(58, 16)
        Me.rdlblmake.TabIndex = 10
        Me.rdlblmake.Text = " Visi Make"
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(267, 6)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(16, 18)
        Me.rdbtnnew.TabIndex = 1
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(5, 11)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 1
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(74, 11)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 2
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(499, 11)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 3
        Me.rdbtnclose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(568, 20)
        Me.rdmenufile.TabIndex = 7
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "File"
        Me.rdmenufile1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "Import"
        Me.rdmenuimport.AccessibleName = "Import"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        Me.rdmenuimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "RadMenuItem1"
        Me.rdmenuexport.AccessibleName = "RadMenuItem1"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        Me.rdmenuexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "Exit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        Me.rdmenuexit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdgrpbxVisimaster
        '
        Me.rdgrpbxVisimaster.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox4)
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox1)
        Me.rdgrpbxVisimaster.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxVisimaster.HeaderText = ""
        Me.rdgrpbxVisimaster.Location = New System.Drawing.Point(3, 3)
        Me.rdgrpbxVisimaster.Name = "rdgrpbxVisimaster"
        Me.rdgrpbxVisimaster.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxVisimaster.Size = New System.Drawing.Size(558, 366)
        Me.rdgrpbxVisimaster.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(6, 137)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(545, 223)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(525, 193)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgrpbxVisimaster)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(568, 406)
        Me.SplitContainer1.SplitterDistance = 372
        Me.SplitContainer1.TabIndex = 8
        '
        'frmvisimaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 426)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmvisimaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Market Equipment Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtTagNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModelNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModelNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVisiSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtmake, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblmake, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxVisimaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxVisimaster.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtmake As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdgrpbxVisimaster As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents rdlblmake As common.Controls.MyLabel
    Friend WithEvents rdlblID As common.Controls.MyLabel
    Friend WithEvents fndid As common.UserControls.txtNavigator
    Friend WithEvents txtAssetNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVisiSize As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtModelNo As common.Controls.MyTextBox
    Friend WithEvents lblModelNo As common.Controls.MyLabel
    Friend WithEvents txtcustname As common.Controls.MyTextBox
    Friend WithEvents fndcustomerid As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTagNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtSerialNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

